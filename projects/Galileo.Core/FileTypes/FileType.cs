using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes
{
    enum FileKind
    {
      Document,
      Spreadsheet,
      Database,
      Presentation,
      Image,
      Video,
      Audio,
      Code,
      Model,
      Archive,
      Unknown
    }

    abstract class FileType
    {
      public abstract FileKind Kind { get; }
      // See
      // https://www.iana.org/assignments/media-types/media-types.xhtml
      public abstract string   Mime { get; }
      public abstract string   Name { get; } 
      public abstract string[] Extensions { get; }

      public abstract bool           Check(string path);
      public abstract bool           CheckInArchive(string path, IArchiveInfo archiveInfo);
      public abstract IFileProcessor CreateProcessor(Submissions.Submission submission);
      public abstract FileProcessorFactory.FileProcessorType GetProcessorType();

      protected const int XX = -1;
      
      private readonly static char[] DirectoryPathChars = {
        System.IO.Path.DirectorySeparatorChar,
        System.IO.Path.AltDirectorySeparatorChar
      };

      internal static bool OpenXmlCheckType(string path, string expectedType)
      {
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader reader = new BinaryReader(fs);

        byte[] header = new byte[Application.ZipFileType.PkHeader.Length];
        
        
        if (reader.Read(header, 0, header.Length) < header.Length)
        {
          reader.Close();
          fs.Close();
          return false;
        }
        
        // Check if is a zip file
        if (MatchMagicCompare(header, Application.ZipFileType.PkHeader) == false)
        {
          return false;
        }
        
        fs.Seek(0, SeekOrigin.Begin);
        

        // https://www.garykessler.net/library/file_sigs.html
        // http://officeopenxml.com/anatomyofOOXML.php
        // For DOCX, PPTX, XLSX
        ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Read);
        if (zip == null)
        {
          reader.Close();
          fs.Close();
          return false;
        }
        
        bool hasRels = false, hasOffice = false;
        
        foreach(ZipArchiveEntry entry in zip.Entries)
        {
            string fullName = entry.FullName;

            if (fullName == "[Content_Types].xml")
            {
              hasRels = true;
            }

            if (fullName.IndexOfAny(DirectoryPathChars) == -1)
              continue;
              
            if (expectedType == "word" && fullName.StartsWith("word/"))
            {
              hasOffice = true;
              if (hasRels)
                break;
            }

            if (expectedType == "xl" && fullName.StartsWith("xl/"))
            {
              hasOffice = true;
              if (hasRels)
                break;
            }
            
            if (expectedType == "ppt" && fullName.StartsWith("ppt/"))
            {
              hasOffice = true;
              if (hasRels)
                break;
            }
        }

        zip.Dispose();
        reader.Close();
        fs.Close();
        
        return hasRels && hasOffice;
      }

      
      internal static bool OasisCheckType(string path, string mime)
      {
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader reader = new BinaryReader(fs);

        byte[] header = new byte[Application.ZipFileType.PkHeader.Length];
        
        
        if (reader.Read(header, 0, header.Length) < header.Length)
        {
          reader.Close();
          fs.Close();
          return false;
        }
        
        // Check if is a zip file
        if (MatchMagicCompare(header, Application.ZipFileType.PkHeader) == false)
        {
          return false;
        }
        
        fs.Seek(0, SeekOrigin.Begin);
        

        // https://en.wikipedia.org/wiki/OpenDocument_technical_specification
        // For ODT
        ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Read);
        if (zip == null)
        {
          reader.Close();
          fs.Close();
          return false;
        }
        
        bool hasRels = false, hasOffice = false;
        
        foreach(ZipArchiveEntry entry in zip.Entries)
        {
            string fullName = entry.FullName;

            if (fullName == "mimetype")
            {
              bool correctMime = false;
              using (var mimeStream = entry.Open())
              using (var mimeReader = new StreamReader(mimeStream)) {
                correctMime = (mimeReader.ReadToEnd() == mime);
              }
              
              zip.Dispose();
              reader.Close();
              fs.Close();
              return correctMime;
            }
        }

        zip.Dispose();
        reader.Close();
        fs.Close();
        
        return false;
      }

      internal static bool ForwardCharSearchIgnoreCase(string path, char[] characters)
      {
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader reader = new BinaryReader(fs);
        
        int charIndex = 0;
        char test = char.ToLower(characters[charIndex]);

        while(reader.PeekChar() != -1)
        {
          char ch = (char) reader.ReadByte();
          
          if (char.ToLower(ch) == test)
          {
            charIndex++;

            if (charIndex == characters.Length)
            {
              reader.Close();
              fs.Close();
              return true;
            }
            
            test = char.ToLower(characters[charIndex]);
          }
          else
          {
            charIndex = 0;
            test = char.ToLower(characters[charIndex]);
          }

        }

        reader.Close();
        fs.Close();
        return false;
      }


      internal static bool ExtensionIsAnyOf(string path, string[] extensions)
      {
        string ext = System.IO.Path.GetExtension(path);
        
        if (string.IsNullOrWhiteSpace(ext) == false)
        {
          ext = ext.Substring(1); // Chop off proceeding '.'
        }

        for(int i=0;i < extensions.Length;i++)
        {
          if (string.Compare(ext, extensions[i], true) == 0)
            return true;
        }
        return false;
      }

      internal static bool MatchMagicCompare(byte[] test, int[] format)
      {
          for(int i=0;i < format.Length;i++)
          {
            int t = format[i];
            if (t == XX)
              continue; // Skip
            if ((byte) t != test[i])
              return  false;
          }

          return true;
      }

      internal static bool MatchMagicBegin(string path, int[] format)
      {
          byte[] test = new byte[format.Length];
          using (BinaryReader reader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read)))
          {
              if (reader.Read(test, 0, format.Length) < format.Length)
                return false;
          }
          
          return MatchMagicCompare(test, format);
      }
      
      internal static bool MatchMagicEnd(string path, int[] format)
      {
          byte[] test = new byte[format.Length];
          using(FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
          {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                stream.Seek(-format.Length, SeekOrigin.End);
                int read = reader.Read(test, 0, format.Length);
                if (read < format.Length)
                  return false;
            }
          }
          
          return MatchMagicCompare(test, format);
      }
    }
}
