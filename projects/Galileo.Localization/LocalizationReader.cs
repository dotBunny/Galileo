
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using I18NPortable;

namespace Galileo.Localization
{
    
    /// <summary>
    /// Localization Reader
    /// </summary>
    public class LocalizationReader : ILocaleReader
    {
        /// <summary>
        /// Comments identified by this
        /// </summary>
        const string CommentPrefix = "#";

        /// <summary>
        /// Dictionary key used to store debug index
        /// </summary>
		const string TranslationLocale = "keys";      

        /// <summary>
        /// Cached Column Information
        /// </summary>
        Dictionary<string, string> _columnLookup;

        /// <summary>
        /// Cached Spreadsheet
        /// </summary>
        SpreadsheetDocument _spreadsheet;

        /// <summary>
        /// Cached Sheet Data
        /// </summary>
        SheetData _sheetData;

        /// <summary>
        /// Read in a new translation locale
        /// </summary>
        /// <returns>The translation</returns>
        /// <remarks>This actually loads all data on a single pass for all translations</remarks>
        /// <param name="stream">The file stream of the code</param>
        public Dictionary<string, string> Read(Stream stream)
        {
            // Get language code from provided stub
            string code = string.Empty;
            using (var streamReader = new StreamReader(stream))
            {
                code = streamReader.ReadToEnd().Trim();
            }

            // If theres no code we have to bail, this shouldnt happen, but ya.
            if ( code == string.Empty ) {
                throw new InvalidDataException("No Locale Found In Stub");
            }


            // Check if we have it from the last parse
            if (LocalizationCache.Locales.ContainsKey(code) && LocalizationCache.Locales[code].Count > 0) 
            {
                return LocalizationCache.Locales[code];
            }

            // --- From this point we effectively only do this once --
          
            // Read in our spreadsheet, caching it just in case after the first time through
            if (_spreadsheet == null)
            {
                _spreadsheet = SpreadsheetDocument.Open(GetType().Assembly.GetManifestResourceStream("Galileo.Localization.Locales.strings.xlsx"), false);

                WorkbookPart workbookPart = _spreadsheet.WorkbookPart;
                IEnumerable<Sheet> sheets = _spreadsheet.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)_spreadsheet.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                _sheetData  = workSheet.GetFirstChild<SheetData>();
            }

            // Get all rows (this seems stupid, but we use it later)
            IEnumerable<Row> rows = _sheetData.Descendants<Row>();

            // Create our column index the first time through
            if (_columnLookup == null)
            {
                _columnLookup = new Dictionary<string, string>();
                int columnIndex = 1;

                foreach (Cell cell in rows.ElementAt(0))
                {
                    string columnHeader = GetCellValue(cell);
                    if (!columnHeader.StartsWith(CommentPrefix, StringComparison.Ordinal))
                    {
                        _columnLookup.Add(
                            GetColumnIdentifier(cell),
                            columnHeader);
                    }
                    columnIndex++;
                }
            }

			// Create Key Locale
			if (!LocalizationCache.Locales.ContainsKey(TranslationLocale))
			{
				LocalizationCache.Locales.Add(TranslationLocale, new Dictionary<string, string>());
			}

            // Building time
            foreach (Row row in rows)
            {
                // Find out key            
                string key = GetCellValue(row.Descendants<Cell>().First());

                // Allow comments in rows
                if (key.StartsWith(CommentPrefix, StringComparison.Ordinal)) continue;
                
                // Add to base key debug language
				if (!LocalizationCache.Locales[TranslationLocale].ContainsKey(key))
                {
					LocalizationCache.Locales[TranslationLocale].Add(key, key);
                }

                for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                {
                    Cell workingCell = row.Descendants<Cell>().ElementAt(i);

                    string columnID = GetColumnIdentifier(workingCell);
                    string cellValue = GetCellValue(workingCell);

                    if (string.IsNullOrEmpty(cellValue)) continue;

                    // Is an item
                    if (_columnLookup.ContainsKey(columnID))
                    {
                        // Check our cache has the right key
                        if (!LocalizationCache.Locales.ContainsKey(_columnLookup[columnID]))
                        {
                            LocalizationCache.Locales.Add(_columnLookup[columnID], new Dictionary<string, string>());
                        }


                        if (LocalizationCache.Locales[_columnLookup[columnID]].ContainsKey(key))
                        {

                            LocalizationCache.Locales[_columnLookup[columnID]][key] = cellValue;

                        }
                        else
                        {
                            LocalizationCache.Locales[_columnLookup[columnID]].Add(key, cellValue);
                        }
                    }
                }            
            }


            return LocalizationCache.Locales[code];
        }

        /// <summary>
        /// Get the string value of a cell
        /// </summary>
        /// <returns>The value</returns>
        /// <param name="cell">Cell Reference</param>
        string GetCellValue(Cell cell)
        {
            string value = string.Empty;

            if (cell.CellValue != null && cell.CellValue.InnerXml != null)
            {
                value = cell.CellValue.InnerXml;
            }

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return _spreadsheet.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText.Trim();
            }

            return value.Trim();
        }

        /// <summary>
        /// Return the column letter
        /// </summary>
        /// <returns>Identifier</returns>
        /// <param name="cell">Cell Reference</param>
        string GetColumnIdentifier(Cell cell)
        {
            return Regex.Replace(cell.CellReference.Value, @"[\d-]", string.Empty);
        }
    }
}