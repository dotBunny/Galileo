using System;
namespace Galileo.Core.Report.HTML.Components
{
    class BackgroundComponent : BaseHTML
    {
        public BackgroundComponent()
        {
            _cache = "<div id=\"background\" class=\"branding\" style=\"position: fixed; width: 100%; height: 100%; z-index: -10000000;\"></div>";
        }
    }
}
