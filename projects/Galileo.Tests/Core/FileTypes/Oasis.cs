using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Galileo.Core.FileTypes;

namespace Galileo.Tests.Core.FileTypes
{
    public class Oasis
    {
        [Fact]
        public void Odt()
        {

            var odt = Types.OpenDocument;

            Assert.Equal("application/vnd.oasis.opendocument.text", odt.Mime);
            Assert.Equal(FileKind.Document, odt.Kind);

            Assert.True(odt.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.odt"));
            Assert.False(odt.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.docx"));
            Assert.False(odt.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.doc"));
        }
        
    }
}
