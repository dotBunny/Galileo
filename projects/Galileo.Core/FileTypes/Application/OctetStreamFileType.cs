using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Application
{
  class OctetStreamFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Unknown; }
    }

    public override string Mime {
      get { return "application/octet-stream"; }
    }

    public override string Name {
      get { return "Arbitrary Binary Data"; }
    }

    readonly string[] _extensions = {};

    public override string[] Extensions {
      get { return _extensions; }
    }

    public override bool Check(string path)
    {
      return true;
    }

    public override bool CheckInArchive(string path, IArchiveInfo archiveInfo)
    {
      return true;
    }

    public override IFileProcessor CreateProcessor(Submissions.Submission submission)
    {
      return null;
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.Unknown;
    }
  }
}
