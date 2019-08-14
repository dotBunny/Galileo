using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Application
{
  class MsPointPointOpenXmlFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Presentation; }
    }

    public override string Mime {
      get { return "application/vnd.openxmlformats-officedocument.presentationml.presentation"; }
    }

    public override string Name {
      get { return "Microsoft PowerPoint (OpenXML)"; }
    }

    readonly string[] _extensions = { "pptx" };

    public override string[] Extensions {
      get { return _extensions; }
    }

    public override bool Check(string path)
    {
      return ExtensionIsAnyOf(path, _extensions) && OpenXmlCheckType(path, "ppt");
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
      return new PPTXFileProcessor(submission);
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.PPTX;
    }
  }
}
