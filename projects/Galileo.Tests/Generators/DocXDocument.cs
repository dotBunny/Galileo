using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Galileo.Tests.Markov;

namespace Galileo.Tests.Generators
{
  class DocXDocument : IDocument
  {
    public int studentSeed { get; set; }
    public int sourceSeed { get; set; }
    public bool isTruthful { get { return studentSeed == sourceSeed; } }
    public DocumentFeatures features;

    StringMarkov student, source;
    
    WordprocessingDocument doc;
    Body body;

    public DocXDocument()
    {
      features = 0;
      studentSeed = 0;
      sourceSeed = 0;
    }
  
    public void Para(string text, string style)
    {
      Paragraph para = body.AppendChild(new Paragraph());
      Run run = para.AppendChild(new Run());

      run.AppendChild(new Text(text));

      ApplyStyleToParagraph(doc, style, para);
    }
    
    public bool AppendDebug(string text)
    {
      Para(text, "Debug");
      return true;
    }

    public bool AppendTitle(float integrity = 1)
    {
      String text = StringMarkov.Heading(student, source, integrity);

      Para(text, "Title");
      return true;
    }

    public bool AppendHeading(float integrity = 1.0f)
    {
      String text = StringMarkov.Heading(student, source, integrity);

      Para(text, "Heading");
      return true;
    }

    public bool AppendImage(float integrity = 1.0f)
    {
      throw new NotImplementedException();
    }

    public bool AppendParagraph(float integrity = 1.0f)
    {

      String text = StringMarkov.Paragraph(student, source, integrity);
      
      Para(text, "Paragraph");
      return true;
    }

    public bool AppendQuote(float integrity = 1.0f)
    {
      throw new NotImplementedException();
    }

    public bool AppendTable(float integrity = 1.0f)
    {
      throw new NotImplementedException();
    }

    public bool SetDocumentContext(DocumentContext context, string data)
    {
      throw new NotImplementedException();
    }

    public bool BeginWriting(DocumentFeatures features_, string path, int studentSeed_, int sourceSeed_, float integrity = 1.0f)
    {
      // generate author
      // generate username

      features = features_;
      studentSeed = studentSeed_;
      sourceSeed = sourceSeed_;

      student = SubmissionGenerator.EssayParagraph.Duplicate(studentSeed);

      if (isTruthful == false)
      {
        source = SubmissionGenerator.EssayParagraph.Duplicate(sourceSeed);
      }
      
      return _Create(path);
    }

    public bool EndWriting(float integrity = 1.0f)
    {
      doc.Close();
      return true;
    }
    
    bool _Create(string path)
    {
      doc = WordprocessingDocument.Create(path, DocumentFormat.OpenXml.WordprocessingDocumentType.Document);
      if (doc == null)
        return false;

      var mainPart = doc.AddMainDocumentPart();
      mainPart.Document = new Document();
      body = mainPart.Document.AppendChild(new Body());
      
      var stylePart = AddStylesPartToPackage(doc);
      
      AddNewStyle(stylePart, "Title", "Title", new Color() { ThemeColor = ThemeColorValues.Text1 }, "Arial", 36, false, true);
      AddNewStyle(stylePart, "Heading", "Heading", new Color() { ThemeColor = ThemeColorValues.Text1 }, "Arial", 24, false, true);
      AddNewStyle(stylePart, "Paragraph", "Paragraph", new Color() { ThemeColor = ThemeColorValues.Text1 }, "Arial", 12, false, false);
      AddNewStyle(stylePart, "Question", "Heading", new Color() { ThemeColor = ThemeColorValues.Text1 }, "Verdana", 14, false, true);
      AddNewStyle(stylePart, "Answer", "Answer", new Color() { ThemeColor = ThemeColorValues.Text1 }, "Arial", 12, false, false);
      AddNewStyle(stylePart, "Debug", "Debug", new Color() { ThemeColor = ThemeColorValues.Accent2 }, "Arial", 8, false, false);
      
      return true;
    }


    
    #region Helpers
    // Apply a style to a paragraph.
    public static void ApplyStyleToParagraph(WordprocessingDocument doc,
        string styleid, Paragraph p)
    {
      // If the paragraph has no ParagraphProperties object, create one.
      if (p.Elements<ParagraphProperties>().Count() == 0)
      {
        p.PrependChild<ParagraphProperties>(new ParagraphProperties());
      }

      // Get the paragraph properties element of the paragraph.
      ParagraphProperties pPr = p.Elements<ParagraphProperties>().First();
      
      // Set the style of the paragraph.
      pPr.ParagraphStyleId = new ParagraphStyleId() { Val = styleid };
    }

    // Return true if the style id is in the document, false otherwise.
    public static bool IsStyleIdInDocument(WordprocessingDocument doc,
        string styleid)
    {
      // Get access to the Styles element for this document.
      Styles s = doc.MainDocumentPart.StyleDefinitionsPart.Styles;

      // Check that there are styles and how many.
      int n = s.Elements<Style>().Count();
      if (n == 0)
        return false;

      // Look for a match on styleid.
      Style style = s.Elements<Style>()
          .Where(st => (st.StyleId == styleid) && (st.Type == StyleValues.Paragraph))
        .FirstOrDefault();
      if (style == null)
        return false;

      return true;
    }

    // Return styleid that matches the styleName, or null when there's no match.
    public static string GetStyleIdFromStyleName(WordprocessingDocument doc, string styleName)
    {
      StyleDefinitionsPart stylePart = doc.MainDocumentPart.StyleDefinitionsPart;
      string styleId = stylePart.Styles.Descendants<StyleName>()
          .Where(s => s.Val.Value.Equals(styleName) &&
      (((Style)s.Parent).Type == StyleValues.Paragraph))
        .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();
      return styleId;
    }
    

    private static void AddNewStyle(StyleDefinitionsPart styleDefinitionsPart,
        string styleid, string stylename, Color colour, String fontName, int fontSize, bool italic, bool bold)
    {
      // Get access to the root element of the styles part.
      Styles styles = styleDefinitionsPart.Styles;

      // Create a new paragraph style and specify some of the properties.
      Style style = new Style()
      {
        Type = StyleValues.Paragraph,
        StyleId = styleid,
        CustomStyle = true
      };
      StyleName styleName1 = new StyleName() { Val = stylename };
      BasedOn basedOn1 = new BasedOn() { Val = "Normal" };
      NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle() { Val = "Normal" };
      style.Append(styleName1);
      style.Append(basedOn1);
      style.Append(nextParagraphStyle1);

      // Create the StyleRunProperties object and specify some of the run properties.
      StyleRunProperties styleRunProperties1 = new StyleRunProperties();

     
      Bold bold1 = new Bold();

      RunFonts font1 = new RunFonts() { Ascii = fontName };
      FontSize fontSize1 = new FontSize() { Val = (fontSize * 2).ToString() };

      if (bold)
        styleRunProperties1.Append(new Bold());

      styleRunProperties1.Append(colour);
      styleRunProperties1.Append(font1);
      styleRunProperties1.Append(fontSize1);

      if (italic)
        styleRunProperties1.Append(new Italic());

      // Add the run properties to the style.
      style.Append(styleRunProperties1);

      // Add the style to the styles part.
      styles.Append(style);
    }

    // Add a StylesDefinitionsPart to the document.  Returns a reference to it.
    public static StyleDefinitionsPart AddStylesPartToPackage(WordprocessingDocument doc)
    {
      StyleDefinitionsPart part;
      part = doc.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
      Styles root = new Styles();
      root.Save(part);
      return part;
    }


    #endregion

  }
}
