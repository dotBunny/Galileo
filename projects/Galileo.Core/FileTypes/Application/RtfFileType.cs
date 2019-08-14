using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Application
{
  class RtfFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Document; }
    }

    public override string Mime {
      get { return "application/rtf"; }
    }

    public override string Name {
      get { return "Rich Text Format (RTF)"; }
    }

    readonly string[] _extensions = { "rtf", "rtx" };

    public override string[] Extensions {
      get { return _extensions; }
    }
    
    private static readonly int[] RtfHeader = {
      // {\rtf1
      0x7B, 0x5C, 0x72, 0x74, 0x66, 0x31
    };

    public override bool Check(string path)
    {
      if (ExtensionIsAnyOf(path, _extensions) == false)
      {
        return false;
      }

      return MatchMagicBegin(path, RtfHeader);
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
      return new RTFFileProcessor(submission);
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.RTF;
    }
  }
}
