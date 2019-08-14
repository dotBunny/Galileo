using System;
using System.IO;
using System.Text;
using Galileo.Core.Checks;
using RtfPipe;
using RtfPipe.Interpreter;
using RtfPipe.Parser;
using RtfPipe.Support;

namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// Rich Text File Processor
    /// </summary>
    class RTFFileProcessor : DefaultFileProcessor, IFileProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.FileProcessors.RTFFileProcessor"/> class
        /// </summary>
        /// <param name="target">The submission</param>
        internal RTFFileProcessor(Submissions.Submission target) : base(target) { }

        #region Methods

        /// <summary>
        /// Gets the valid checks to process through this file
        /// </summary>
        /// <returns>The check types</returns>
        public override CheckFactory.CheckType GetCheckTypes()
        {
            return CheckFactory.CheckType.ContentHash | 
                   CheckFactory.CheckType.Content | 
                   CheckFactory.CheckType.FileName | 
                   CheckFactory.CheckType.MetaCreator | 
                   CheckFactory.CheckType.MetaDateCreated | 
                   CheckFactory.CheckType.MetaDateModified | 
                   CheckFactory.CheckType.MetaDateLastPrinted;
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
#if NETCORE
                // Add a reference to the NuGet package System.Text.Encoding.CodePages for .Net core only
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif

                IRtfGroup rtfStructure;
                RtfParserListenerStructureBuilder structureBuilder = new RtfParserListenerStructureBuilder();
                RtfParser parser = new RtfParser(structureBuilder);
                parser.IgnoreContentAfterRootGroup = true; // support WordPad documents
                parser.Parse(new RTFDocumentSource(_target.AbsolutePath));
                rtfStructure = structureBuilder.StructureRoot;

                var intSettings = new RtfInterpreterSettings() { IgnoreDuplicatedFonts = true, IgnoreUnknownFonts = true };
                var rtfDocument = RtfInterpreterTool.BuildDoc(rtfStructure, intSettings);


                if (rtfDocument.DocumentInfo.Author != null)
                {
                    _target.MetaCreator = rtfDocument.DocumentInfo.Author;
                }

                if (rtfDocument.DocumentInfo.CreationTime != null)
                {
                    _target.MetaDateCreated = (DateTime)rtfDocument.DocumentInfo.CreationTime;
                }

                if (rtfDocument.DocumentInfo.PrintTime != null)
                {
                    _target.MetaDateLastPrinted = (DateTime)rtfDocument.DocumentInfo.PrintTime;
                }

                if(rtfDocument.DocumentInfo.RevisionTime != null)
                {
                    _target.MetaDateModified = (DateTime)rtfDocument.DocumentInfo.RevisionTime;    
                }

                // Build our content
                StringBuilder builder = new StringBuilder();
                foreach(IRtfVisual v in rtfDocument.VisualContent)
                {
                    if(v.Kind == RtfVisualKind.Text)
                    {
                        builder.Append(v.ToString());
                    }
                }

                _target.Content = builder.ToString();
                _target.ContentLength = _target.Content.Length;
                _target.ContentHash = Compare.Hash(_target.Content);

                _loaded = true;
            }
            catch (Exception e)
            {
                _target.Logging.Add("- " + Markdown.Bold("An exception occured when processing " + _target.AbsolutePath + ", " + e.Message));
            }

            return _loaded;
        }

        #endregion

        /// <summary>
        /// RTF Document Source
        /// </summary>
        class RTFDocumentSource : IRtfSource
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="T:Galileo.Core.FileProcessors.RTFFileProcessor.DocumentSource"/> class.
            /// </summary>
            /// <param name="path">RTF file path</param>
            public RTFDocumentSource(string path)
            {
                Reader = File.OpenText(path);
            }

            /// <summary>
            /// Returns the text reader for the provided path
            /// </summary>
            /// <value>The reader.</value>
            public TextReader Reader { get; private set; }
        }
    }
}