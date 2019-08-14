using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.ContextExporter
{
    interface IContentExporter
    {
        void Begin(string name);
        void MetaData(string name, string value);
        void TextSection(string name, string value);
        void End();
    }
}
