using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Application
{
  class AdobePdfFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Document; }
    }

    public override string Mime {
      get { return "application/pdf"; }
    }

    public override string Name {
      get { return "Adobe Portable Document Format (PDF)"; }
    }

    readonly string[] _extensions = { "pdf" };

    public override string[] Extensions {
      get { return _extensions; }
    }
    
    private static readonly int[] PdfHeader = {
      // %     P     D     F
         0x25, 0x50, 0x44, 0x46
    };

    public override bool Check(string path)
    {
      if (ExtensionIsAnyOf(path, _extensions) == false)
      {
        return false;
      }

      return MatchMagicBegin(path, PdfHeader);
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
      return new PDFFileProcessor(submission);
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.PDF;
    }
  }
}
