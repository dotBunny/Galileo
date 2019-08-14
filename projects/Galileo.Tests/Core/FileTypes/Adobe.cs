using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Galileo.Core.FileTypes;

namespace Galileo.Tests.Core.FileTypes
{
    public class Adobe
    {
        [Fact]
        public void Pdf()
        {

            var pdf = Types.AdobePdf;

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Complete_list_of_MIME_types
            Assert.Equal("application/pdf", pdf.Mime);
            Assert.Equal(FileKind.Document, pdf.Kind);

            Assert.True(pdf.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.pdf"));
            Assert.False(pdf.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.doc"));
            Assert.False(pdf.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.gif"));
        }
        
    }
}
