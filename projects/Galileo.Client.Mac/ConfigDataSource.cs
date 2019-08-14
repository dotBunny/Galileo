using System;
using AppKit;
using Galileo.Core;

namespace Galileo.Client.Mac
{
    /// <summary>
    /// Process Config Data Source
    /// </summary>
    public class ConfigDataSource : NSTableViewDataSource
    {
        #region Fields

        /// <summary>
        /// Parsed config items
        /// </summary>
        public System.Collections.Generic.List<ProcessConfig.ConfigDataSourceObject> Items = new System.Collections.Generic.List<ProcessConfig.ConfigDataSourceObject>();

        /// <summary>
        /// Action triggered when a change occurs
        /// </summary>
        public Action OnChanged;

        #endregion

        #region Events

        /// <summary>
        /// Gets the number of rows in the data source
        /// </summary>
        /// <returns>The row count.</returns>
        /// <param name="tableView">Table to use for viewing</param>
        public override nint GetRowCount(NSTableView tableView)
        {
            return Items.Count;
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Client.Mac.ConfigDataSource"/> class
        /// </summary>
        /// <param name="config">The hunter config to parse into objects</param>
        public ConfigDataSource(HunterConfig config)
        {
            Items = ProcessConfig.GetDataSourceObject(config);
        }

        #region Methods

        /// <summary>
        /// Update a parsed config item
        /// </summary>
        /// <returns><c>true</c>, if item was updated, <c>false</c> otherwise.</returns>
        /// <param name="row">Item's row index</param>
        /// <param name="value">New item value</param>
        public bool UpdateItem(int row, string value)
        {
            bool updated = false;
            switch (Items[row].Type)
            {
                case ProcessConfig.ConfigDataSourceObjectType.Boolean:
                    bool tempBoolean = false;
                    if (Boolean.TryParse(value, out tempBoolean))
                    {
                        Items[row].Value = tempBoolean.ToString();
                        updated = true;
                    }
                    break;
                case ProcessConfig.ConfigDataSourceObjectType.Float:
                    float tempFloat = 0.0f;
                    if (float.TryParse(value, out tempFloat))
                    {
                        Items[row].Value = tempFloat.ToString();
                        updated = true;
                    }
                    break;
                case ProcessConfig.ConfigDataSourceObjectType.Integer:
                    int tempInteger = 0;
                    if (int.TryParse(value, out tempInteger))
                    {
                        Items[row].Value = tempInteger.ToString();
                        updated = true;
                    }
                    break;
                case ProcessConfig.ConfigDataSourceObjectType.String:
                    Items[row].Value = value;
                    updated = true;
                    break;
            }

            if (updated && OnChanged != null)
            {
                OnChanged();
            }
            return updated;
        }

        #endregion
    }
}
