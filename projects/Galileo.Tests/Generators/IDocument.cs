using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Tests.Generators
{
    [Flags]
    enum DocumentFeatures
    {
      Styles,
      PageNumbers,
      Headers
    }

    enum DocumentContext
    {
      Student,
      Teacher,
      Course,
      AssignmentNumber
    }

    enum DocumentMeta
    {
      AuthoringTool,
      UserName,
      DateCreated,
      DateModified,
      DateAccessed,
    }

    interface IDocument
    {
      int studentSeed { get; set; }
      int sourceSeed  { get; set; }
      bool isTruthful { get; }

      
      bool SetDocumentContext(DocumentContext context, string data);

      bool BeginWriting(DocumentFeatures features, string path, int studentSeed_, int sourceSeed_, float integrity = 1.0f);
      bool EndWriting(float integrity = 1.0f);

      bool AppendDebug(string text);
      bool AppendTitle(float integrity = 1.0f);
      bool AppendParagraph(float integrity = 1.0f);
      bool AppendHeading(float integrity = 1.0f);
      bool AppendTable(float integrity = 1.0f);
      bool AppendQuote(float integrity = 1.0f);
      bool AppendImage(float integrity = 1.0f);
    }
}
