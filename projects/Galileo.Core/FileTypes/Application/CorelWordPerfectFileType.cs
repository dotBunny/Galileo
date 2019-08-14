using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Application
{
  class CorelWordPerfectFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Document; }
    }

    public override string Mime {
      get { return "application/vnd.wordperfect"; }
    }

    public override string Name {
      get { return "Corel Wordperfect"; }
    }

    readonly string[] _extensions = { "wpd" };

    public override string[] Extensions {
      get { return _extensions; }
    }
    
    private static readonly int[] WpdHeader = {
      // ÿ     W     P     C
         0xFF, 0x57, 0x50, 0x43
    };

    public override bool Check(string path)
    {
      if (ExtensionIsAnyOf(path, _extensions) == false)
      {
        return false;
      }

      return MatchMagicBegin(path, WpdHeader);
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
      return new WPDFileProcessor(submission);
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.WPD;
    }
  }
}
