using System;
using AppKit;
using CoreGraphics;
using I18NPortable;

namespace Galileo.Client.Mac
{
    /// <summary>
    /// Config DataSource Table View Delegate
    /// </summary>
    public class ConfigDelegate : NSTableViewDelegate
    {
		#region Fields

        /// <summary>
        /// Checkbox Identifier
        /// </summary>
		static string s_CheckBoxViewIdentifier = "CheckBoxView";

        /// <summary>
        /// Slider Identifier
        /// </summary>
		static string s_SliderViewIdentifier = "SliderView";

        /// <summary>
        /// The data source used by the table
        /// </summary>
        readonly ConfigDataSource _dataSource;

        /// <summary>
        /// Cached version of previous value, used to revert if necessary
        /// </summary>
        string _previousEdit;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Client.Mac.ConfigDelegate"/> class.
        /// </summary>
        /// <param name="datasource">The data source to be used by the table</param>
        public ConfigDelegate(ConfigDataSource datasource)
        {
            if (datasource == null)
            {
                Instance.Log("Client.ConfigDelegate", "No datasource was passed to the table's delegate, this is not good.");
            }
            _dataSource = datasource;
        }

        #region Events
              
        /// <summary>
        /// Gets the view for the given column/row
        /// </summary>
        /// <returns>The view for item.</returns>
        /// <param name="tableView">The viewing table</param>
        /// <param name="tableColumn">The specific table column</param>
        /// <param name="row">The specific table row</param>      
        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            if (_dataSource == null)
            {
                Instance.Log("Client.ConfigDelegate.GetViewForItem", "The datasource went missing despite being created, this is not good.");
            }
            if (tableView == null || tableColumn == null || _dataSource == null)
            {
                return new NSTextField
                {
                    Identifier = "null",
                    BackgroundColor = NSColor.Clear,
                    Bordered = false,
                    Selectable = false,
                    Editable = false
                };
            }

            // Creation pattern
            var view = (NSTextField)tableView.MakeView(tableColumn.Title, this);
            if (view == null)
            {
                view = new NSTextField
                {
                    Identifier = tableColumn.Title,
                    BackgroundColor = NSColor.Clear,
                    Bordered = false,
                    Selectable = false,
                    Editable = false
                };
                view.EditingBegan += delegate
                {
                    _previousEdit = view.StringValue;
                };
                view.EditingEnded += delegate
                {
                    if (!_dataSource.UpdateItem((int)view.Tag, view.Cell.StringValue))
                    {
                        view.StringValue = _previousEdit;
                    }
                };
            }

            // Setup base level items
            view.ToolTip = _dataSource.Items[(int)row].Description;
            view.Tag = row;

            // Set up view based on the column and row
			if (tableColumn.Title == Localization.LocalizationCache.KeywordSetting)
			{
				view.Editable = false;
                view.StringValue = _dataSource.Items[(int)row].Setting;
			}
			else if (tableColumn.Title == Localization.LocalizationCache.KeywordValue ||
			         tableColumn.Title == Localization.LocalizationCache.KeywordDefaultValue)
			{
				
				view.Editable = !_dataSource.Items[(int)row].IsReadOnly;
				view.StringValue = _dataSource.Items[(int)row].Value;


				// Subviews
                switch (_dataSource.Items[(int)row].Type)
                {                                 
                    case ProcessConfig.ConfigDataSourceObjectType.Boolean:

                        // Disable Text Field
						view.Editable = false;
						view.StringValue = string.Empty;

                        // Create new button
                        var button = new NSButton(new CGRect(0, 0, 16, 16));
						//var button = new NSButton();
						button.SetButtonType(NSButtonType.Switch);
                        

                        button.Title = "";
                        button.Tag = row;
                        
                        // Setup initial button state
						if(bool.Parse(_dataSource.Items[(int)row].Value)) {
							button.State = NSCellStateValue.On;
						} else {
							button.State = NSCellStateValue.Off;
						}
                                        
                        // Wireup events
                        button.Activated += (sender, e) => {
							if ( button.State == NSCellStateValue.On) {
								_dataSource.UpdateItem((int)button.Tag, "True");
							} else {
								_dataSource.UpdateItem((int)button.Tag, "False");
							}                     
                        };

                        view.AddSubview(button);
                        break;
                }
			}
			else if (tableColumn.Title == Localization.LocalizationCache.KeywordDescription)
			{
				view.Editable = false;
                view.StringValue = _dataSource.Items[(int)row].Description;
			}

            

            return view;
        }

        #endregion  
    }
}
