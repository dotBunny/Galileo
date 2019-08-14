using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Text
{
  class PlainTextFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Document; }
    }

    public override string Mime {
      get { return "text/plain"; }
    }

    public override string Name {
      get { return "Text File"; }
    }

    readonly string[] _extensions = { "txt", "text", "asc" };

    public override string[] Extensions {
      get { return _extensions; }
    }
    
    internal static byte NUL = (byte)0; // Null char
    internal static byte BS = (byte)8; // Back Space
    internal static byte CR = (byte)13; // Carriage Return
    internal static byte SUB = (byte)26; // Substitute
    
    public override bool Check(string path)
    {
      if (ExtensionIsAnyOf(path, _extensions) == false)
      {
        return false;
      }

      // Mostly taken from:
      // https://stackoverflow.com/questions/910873/how-can-i-determine-if-a-file-is-binary-or-text-in-c
      // The general gist is, it's not an ASCII/Unicode file if it contains any non-printable, or control
      // characters. I stop after 512 characters, so the entire file isn't processed.

      FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
      BinaryReader reader = new BinaryReader(fs);
      
      int timeOut = 0;

      while(reader.PeekChar() != -1)
      {
        byte ch = reader.ReadByte();

        // Is a control char?
        if ((ch > NUL && ch < BS) || (ch > CR && ch < SUB))
        {
          reader.Close();
          fs.Close();
          return false;
        }
        
        if (timeOut++ > 512)
        {
          // Good Enough.
          break;
        }
      }

      reader.Close();
      fs.Close();
      return true;
    }

    public override bool CheckInArchive(string path, IArchiveInfo archiveInfo)
    {
      if (ExtensionIsAnyOf(path, _extensions) == false)
      {
        return false;
      }

      return true;
    }

    public override IFileProcessor CreateProcessor(Submissions.Submission submission)
    {
      return new TXTFileProcessor(submission);
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.TXT;
    }
  }
}
