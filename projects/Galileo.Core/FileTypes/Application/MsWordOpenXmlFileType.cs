using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Application
{
  class MsWordOpenXmlFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Document; }
    }

    public override string Mime {
      get { return "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; }
    }

    public override string Name {
      get { return "Microsoft Word (OpenXML)"; }
    }

    readonly string[] _extensions = { "docx" };

    public override string[] Extensions {
      get { return _extensions; }
    }

    public override bool Check(string path)
    {
      return ExtensionIsAnyOf(path, _extensions) && OpenXmlCheckType(path, "word");
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
      return new DOCXFileProcessor(submission);
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.DOCX;
    }
  }
}
