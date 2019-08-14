using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Text
{
  class HtmlFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Document; }
    }

    public override string Mime {
      get { return "text/html"; }
    }

    public override string Name {
      get { return "HyperText Markup Language (HTML)"; }
    }

    readonly string[] _extensions = { "html", "htm", "xhtml" };

    public override string[] Extensions {
      get { return _extensions; }
    }
    
    private static readonly char[] HtmlHeader = {
      // <html
         '<', 'h','t','m','l'
    };

    public override bool Check(string path)
    {
      if (ExtensionIsAnyOf(path, _extensions) == false)
      {
        return false;
      }

      return ForwardCharSearchIgnoreCase(path, HtmlHeader);
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
      return new HTMLFileProcessor(submission);
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.HTML;
    }
  }
}
