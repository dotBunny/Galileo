using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.Report.HTML
{
    class BaseHTML
    {
        internal const string ListGroupItemDanger = "list-group-item-danger";
        internal const string ListGroupItemWarning = "list-group-item-warning";

        internal string _cache = string.Empty;

        internal string GetHTML()
        {
            return _cache;
        }
    }
}
