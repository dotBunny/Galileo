using System;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using System.Linq;
using Galileo.Core.Checks;

namespace Galileo.Core.FileProcessors
{
    /// <summary>
    /// Microsoft Excel File Processor
    /// </summary>
    class XLSXFileProcessor : DefaultFileProcessor, IFileProcessor
    {
        /// <summary>
        /// OpenXML local document
        /// </summary>
        SpreadsheetDocument _document;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Core.FileProcessors.XLSXFileProcessor"/> class
        /// </summary>
        /// <param name="target">The submission</param>
        internal XLSXFileProcessor(Submissions.Submission target) : base(target) { }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="T:Galileo.Core.FileProcessors.XLSXFileProcessor"/> is reclaimed by garbage collection
        /// </summary>
        ~XLSXFileProcessor()
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
                _document = SpreadsheetDocument.Open(_target.AbsolutePath, false);

                // TODO : Content might be something where we need to write somethign per file type
                // TODO : We should look at sheet names?

                WorkbookPart workbookPart = _document.WorkbookPart;
                Sheets workbookSheets = workbookPart.Workbook.Sheets;

                foreach (Sheet s in workbookSheets)
                {
                    OpenXmlPart part = workbookPart.GetPartById(s.Id);

                    if (!(part is WorksheetPart))
                        continue;

                    var worksheetPart = (WorksheetPart)workbookPart.GetPartById(s.Id);
                    var sharedStringPart = workbookPart.SharedStringTablePart;
                    var values = sharedStringPart.SharedStringTable.Elements<SharedStringItem>().ToArray();

                    var cells = worksheetPart.Worksheet.Descendants<Cell>();
                    foreach (var cell in cells)
                    {

                        //// The cells contains a string input that is not a formula
                        //if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                        //{
                        //    var index = int.Parse(cell.CellValue.Text);
                        //    var value = values[index].InnerText;

                        //    Target.Content += value;
                        //}
                        //else if (cell.CellValue != null )
                        //{
                        //    Target.Content += cell.CellValue.Text;
                        //}

                        if (cell.CellFormula != null)// && cell.CellFormula.Text.StartsWith("=",StringComparison.Ordinal))
                        {
                            _target.Content += cell.CellFormula.Text;
                        }
                    }
                }

                // - Look for cell selection to be identical
                // - Reference formulas that werent there (pages removed)
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

        #endregion
    }
}