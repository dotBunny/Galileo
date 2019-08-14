using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.FileTypes
{
    static class Types
    {
      static List<FileType> FileTypes;
      
      public static FileType
          None,
          AdobePdf,
          AutoCadDxf,
          CorelWordPerfect,
          MsExcelOpenXml,
          MsPointPointOpenXml,
          MsWordOpenXml,
          OpenDocument,
          Rtf,
          Zip,
          OctetStream,
          Html,
          PlainText;


      static Types()
      {
        FileTypes = new List<FileType>();
        AdobePdf = Add(new Application.AdobePdfFileType());
        AutoCadDxf = Add(new Application.AutoCadDxfFileType());
        CorelWordPerfect = Add(new Application.CorelWordPerfectFileType());
        MsExcelOpenXml = Add(new Application.MsExcelOpenXmlFileType());
        MsPointPointOpenXml = Add(new Application.MsPointPointOpenXmlFileType());
        MsWordOpenXml = Add(new Application.MsWordOpenXmlFileType());
        OpenDocument = Add(new Application.OpenDocumentTextFileType());
        Rtf = Add(new Application.RtfFileType());
        Zip = Add(new Application.ZipFileType());
        None = OctetStream = Add(new Application.OctetStreamFileType());
        Html = Add(new Text.HtmlFileType());
        PlainText = Add(new Text.PlainTextFileType());
      }

      static FileType Add(FileType type)
      {
        FileTypes.Add(type);
        return type;
      }

      public static bool TryGet(string path, out FileType type)
      {
        foreach(var possibleType in FileTypes)
        {
          if (possibleType == OctetStream)
            continue;
          if (possibleType.Check(path))
          {
            type = possibleType;
            return true;
          }
        }

        type = OctetStream;
        return false;
      }
      
      public static bool TryGetInArchive(string path, IArchiveInfo archiveInfo, out FileType type)
      {
        foreach(var possibleType in FileTypes)
        {
          if (possibleType == OctetStream)
            continue;
          if (possibleType.CheckInArchive(path, archiveInfo))
          {
            type = possibleType;
            return true;
          }
        }

        type = OctetStream;
        return false;
      }

      public static bool IsFileSystem(string path)
      {
        return System.IO.Directory.Exists(path);
      }

      public static bool IsArchive(string path, out FileType type)
      {
        if (Zip.Check(path))
        {
          type = Zip;
          return true;
        }

        // Add more here, when as needed. :D

        type = None;
        return false;
      }

      public static bool IsArchiveInArchive(string path, IArchiveInfo archiveInfo, out FileType type)
      {
        if (Zip.CheckInArchive(path, archiveInfo))
        {
          type = Zip;
          return true;
        }

        // Add more here, when as needed. :D

        type = None;
        return false;
      }
      
    }
}
