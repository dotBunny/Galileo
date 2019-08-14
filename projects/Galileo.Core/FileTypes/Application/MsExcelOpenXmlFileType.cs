using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Application
{
  class MsExcelOpenXmlFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Spreadsheet; }
    }

    public override string Mime {
      get { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
    }

    public override string Name {
      get { return "Microsoft Excel (OpenXML)"; }
    }

    readonly string[] _extensions = { "xlsx" };

    public override string[] Extensions {
      get { return _extensions; }
    }

    public override bool Check(string path)
    {
      return ExtensionIsAnyOf(path, _extensions) && OpenXmlCheckType(path, "xl");
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
      return new XLSXFileProcessor(submission);
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.XLSX;
    }
  }
}
