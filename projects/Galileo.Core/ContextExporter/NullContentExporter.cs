using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.ContextExporter
{
    class NullContentExporter : IContentExporter
    {
        public void Begin(string name)
        {
        }

        public void End()
        {
        }
        
        public void MetaData(string name, string value)
        {
        }

        public void TextSection(string name, string value)
        {
        }
    }
}
