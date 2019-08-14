using System;
using System.Collections.Generic;
using System.Text;
using Galileo.Core.FileProcessors;

namespace Galileo.Core.FileTypes.Application
{
  class AutoCadDxfFileType : FileType
  {
    public override FileKind Kind {
      get { return FileKind.Model; }
    }

    public override string Mime {
      get { return "application/dxf"; }
    }

    public override string Name {
      get { return "AutoCAD Drawing Interchange Format"; }
    }

    readonly string[] _extensions = { "dxf"};

    public override string[] Extensions {
      get { return _extensions; }
    }
    
    private static readonly int[] DxfBinaryHeader = {
          // https://www.autodesk.com/techpubs/autocad/acadr14/dxf/binary_dxf_file_format_al_u05_b.htm
          // AutoCAD Binary DXF<CR><LF><SUB><NULL>
          0x41, 0x75, 0x74, 0x6F, 0x43, 0x41, 0x44, 0x20, 0x42, 0x69,
          0x6E, 0x61, 0x72, 0x79, 0x20, 0x44, 0x58, 0x46, 0x0A, 0x0D,
          0x1A, 0x00
    };

    private static readonly int[] DxfAsciiFooter = {
          // "0" 0D 0A "E" "O" "F" 0D 0A
          0x30, 0x0D, 0x0A, 0x45, 0x4F, 0x46, 0x0D, 0x0A
    };

    public override bool Check(string path)
    {
      if (ExtensionIsAnyOf(path, _extensions) == false)
      {
          return false;
      }

      // Binary version
      if (MatchMagicBegin(path, DxfBinaryHeader))
      {
          return true;
      }

      // ASCII version
      if (MatchMagicEnd(path, DxfAsciiFooter))
      {
          return true;
      }
      
      return false;
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
      return new DXFFileProcessor(submission);
    }

    public override FileProcessorFactory.FileProcessorType GetProcessorType()
    {
      return FileProcessorFactory.FileProcessorType.DXF;
    }
  }
}
