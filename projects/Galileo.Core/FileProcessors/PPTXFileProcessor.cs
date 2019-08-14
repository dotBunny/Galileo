using System;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Packaging;
using System.Linq;
using Galileo.Core.Checks;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// Microsoft PowerPoint File Processor
    /// </summary>
    class PPTXFileProcessor : DefaultFileProcessor, IFileProcessor
    {
        /// <summary>
        /// OpenXML local document
        /// </summary>
        PresentationDocument _document;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.FileProcessors.PPTXFileProcessor"/> class
        /// </summary>
        /// <param name="target">The submission</param>
        public PPTXFileProcessor(Submissions.Submission target) : base(target) { }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="T:Galileo.Core.FileProcessors.PPTXFileProcessor"/> is reclaimed by garbage collection
        /// </summary>
        ~PPTXFileProcessor()
        {
            if (_document != null)
            {
                _document.Dispose();
            }
        }

        #region Methods

        /// <summary>
        /// Gets the valid checks to process through this file
        /// </summary>
        /// <returns>The check types</returns>
        public override CheckFactory.CheckType GetCheckTypes()
        {
            return CheckFactory.CheckType.ContentHash |
                   CheckFactory.CheckType.FileName |
                   CheckFactory.CheckType.MetaCreator |
                   CheckFactory.CheckType.MetaDateCreated |
                   CheckFactory.CheckType.MetaDateLastPrinted |
                   CheckFactory.CheckType.MetaDateModified |
                   CheckFactory.CheckType.MetaLastModifiedBy;
        }

        /// <summary>
        /// Process the submissions absolute path
        /// </summary>
        /// <returns>Was the submission able to be loaded?</returns>
        public override bool Process()
        {
            _loaded = false;

            try
            {
                // Open Document
                _document = PresentationDocument.Open(_target.AbsolutePath, false);

                StringBuilder builder = new StringBuilder();

                // Go through slides
                int numberOfSlides = CountSlides(_document);
                for (int i = 0; i < numberOfSlides; i++)
                {
                    string[] lines = GetAllTextInSlide(_document, i);
                    if (lines != null)
                    {
                        foreach (string s in lines)
                        {
                            builder.AppendLine(s);
                        }
                    }
                }

                // Get plain text of content
                _target.Content = builder.ToString();

                // Cache length for later comparison
                _target.ContentLength = _target.Content.Length;
                _target.ContentHash = Compare.Hash(_target.Content);

                // Collect Meta Data
                _target.MetaCreator = _document.PackageProperties.Creator;
                _target.MetaLastModifiedBy = _document.PackageProperties.LastModifiedBy;

                if (_document.PackageProperties.LastPrinted != null)
                {
                    _target.MetaDateLastPrinted = (DateTime)_document.PackageProperties.LastPrinted;
                }

                if (_document.PackageProperties.Created != null)
                {
                    _target.MetaDateCreated = (DateTime)_document.PackageProperties.Created;
                }

                if (_document.PackageProperties.Modified != null)
                {
                    _target.MetaDateModified = (DateTime)_document.PackageProperties.Modified;
                }

                _loaded = true;
            }
            catch (Exception e)
            {
                _target.Logging.Add("- " + Markdown.Bold("An exception occured when processing " + _target.AbsolutePath + ", " + e.Message));
            }

            return _loaded;
        }

        /// <summary>
        /// Count the number of content slides in the presentation
        /// </summary>
        /// <returns>The slide count</returns>
        /// <param name="presentationDocument">Presentation Reference</param>
        static int CountSlides(PresentationDocument presentationDocument)
        {
            // Check for a null document object.
            if (presentationDocument == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

            int slidesCount = 0;

            // Get the presentation part of document.
            PresentationPart presentationPart = presentationDocument.PresentationPart;
            // Get the slide count from the SlideParts.
            if (presentationPart != null)
            {
                slidesCount = presentationPart.SlideParts.Count();
            }
            // Return the slide count to the previous method.
            return slidesCount;
        }

        /// <summary>
        /// Get all the text on a given slide
        /// </summary>
        /// <returns>All of the lines of text</returns>
        /// <param name="presentationDocument">Presentation Reference</param>
        /// <param name="slideIndex">Slide index</param>
        static string[] GetAllTextInSlide(PresentationDocument presentationDocument, int slideIndex)
        {
            // Verify that the presentation document exists.
            if (presentationDocument == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

            // Verify that the slide index is not out of range.
            if (slideIndex < 0)
            {
                throw new ArgumentOutOfRangeException("slideIndex");
            }

            // Get the presentation part of the presentation document.
            PresentationPart presentationPart = presentationDocument.PresentationPart;

            // Verify that the presentation part and presentation exist.
            if (presentationPart != null && presentationPart.Presentation != null)
            {
                // Get the Presentation object from the presentation part.
                Presentation presentation = presentationPart.Presentation;

                // Verify that the slide ID list exists.
                if (presentation.SlideIdList != null)
                {
                    // Get the collection of slide IDs from the slide ID list.
                    DocumentFormat.OpenXml.OpenXmlElementList slideIds =
                        presentation.SlideIdList.ChildElements;

                    // If the slide ID is in range...
                    if (slideIndex < slideIds.Count)
                    {
                        // Get the relationship ID of the slide.
                        string slidePartRelationshipId = (slideIds[slideIndex] as SlideId).RelationshipId;

                        // Get the specified slide part from the relationship ID.
                        SlidePart slidePart =
                            (SlidePart)presentationPart.GetPartById(slidePartRelationshipId);

                        // Pass the slide part to the next method, and
                        // then return the array of strings that method
                        // returns to the previous method.
                        return GetAllTextInSlide(slidePart);
                    }
                }
            }

            // Else, return null.
            return null;
        }

        /// <summary>
        /// Gets all the text on a given slider
        /// </summary>
        /// <returns>All of the lines of text</returns>
        /// <param name="slidePart">The slide reference</param>
        static string[] GetAllTextInSlide(SlidePart slidePart)
        {
            // Verify that the slide part exists.
            if (slidePart == null)
            {
                throw new ArgumentNullException("slidePart");
            }

            // Create a new linked list of strings.
            LinkedList<string> texts = new LinkedList<string>();

            // If the slide exists...
            if (slidePart.Slide != null)
            {
                // Iterate through all the paragraphs in the slide.
                foreach (DocumentFormat.OpenXml.Drawing.Paragraph paragraph in
                    slidePart.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Paragraph>())
                {
                    // Create a new string builder.                    
                    StringBuilder paragraphText = new StringBuilder();

                    // Iterate through the lines of the paragraph.
                    foreach (DocumentFormat.OpenXml.Drawing.Text text in
                        paragraph.Descendants<DocumentFormat.OpenXml.Drawing.Text>())
                    {
                        // Append each line to the previous lines.
                        paragraphText.Append(text.Text);
                    }

                    if (paragraphText.Length > 0)
                    {
                        // Add each paragraph to the linked list.
                        texts.AddLast(paragraphText.ToString());
                    }
                }
            }

            if (texts.Count > 0)
            {
                // Return an array of strings.
                return texts.ToArray();
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}