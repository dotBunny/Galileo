using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Application
{
  class OpenDocumentTextFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Document; }
    }

    public override string Mime {
      get { return "application/vnd.oasis.opendocument.text"; }
    }

    public override string Name {
      get { return "OpenDocument Text (Open Office)"; }
    }

    readonly string[] _extensions = { "odt" };

    public override string[] Extensions {
      get { return _extensions; }
    }

    public override bool Check(string path)
    {
      return ExtensionIsAnyOf(path, _extensions) && OasisCheckType(path, Mime);
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
      return new ODTFileProcessor(submission);
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.ODT;
    }
  }
}
