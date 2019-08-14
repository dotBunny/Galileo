using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Application
{
  class ZipFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Archive; }
    }

    public override string Mime {
      get { return "application/zip"; }
    }

    public override string Name {
      get { return "ZIP archive"; }
    }

    readonly string[] _extensions = { "zip" };

    public override string[] Extensions {
      get { return _extensions; }
    }

    public static readonly int[] PkHeader = {
      // P  K     .     .
      0x50, 0x4B, 0x03, 0x04
    };

    public override bool Check(string path)
    {
      if (ExtensionIsAnyOf(path, _extensions) == false)
      {
        return false;
      }
      
      if (MatchMagicBegin(path, PkHeader) == false)
      {
        return false;
      }
      
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
      return null;
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.Ignored;
    }
  }
}
