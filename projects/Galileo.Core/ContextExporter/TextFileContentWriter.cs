using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Core.ContextExporter
{
    class TextFileContentWriter : IContentExporter
    {
        string                 _basePath;
        System.IO.StreamWriter _file;

        public TextFileContentWriter(string basePath)
        {
            _basePath = basePath;
        }

        public void Close()
        {
            End();
        }
        
        public void Begin(string name)
        {
            if (_file != null)
            {
                End();
            }
            
            _file = new System.IO.StreamWriter(System.IO.Path.Combine(_basePath, string.Format("{0}.xml", name)), false, Encoding.UTF8);
            _file.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            _file.WriteLine("<submission>");
        }
        
        public void End()
        {
            if (_file != null)
            {
                _file.WriteLine("</submission>");
                _file.Close();
                _file = null;
            }
        }

        public void MetaData(string name, string value)
        {
            _file.Write("<data name=\"");
            _file.Write(name);
            _file.Write("\">");
            _file.Write(value);
            _file.WriteLine("</data>");
        }

        public void TextSection(string name, string value)
        {
            _file.Write("<text name=\"");
            _file.Write(name);
            _file.WriteLine("\">");
            _file.WriteLine("<![CDATA[");
            _file.WriteLine(value);
            _file.WriteLine("]]>");
            _file.Write("</text>");
        }
        

    }
}
