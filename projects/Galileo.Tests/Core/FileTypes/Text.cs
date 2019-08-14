using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Galileo.Core.FileTypes;

namespace Galileo.Tests.Core.FileTypes
{
    public class Text
    {
        [Fact]
        public void Html()
        {

            var html = Types.Html;

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Complete_list_of_MIME_types
            Assert.Equal("text/html", html.Mime);
            Assert.Equal(FileKind.Document, html.Kind);

            Assert.True(html.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.html"));
            Assert.False(html.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.txt"));
            Assert.False(html.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.pdf"));
        }
        
        [Fact]
        public void PlainText()
        {

            var text = Types.PlainText;

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Complete_list_of_MIME_types
            Assert.Equal("text/plain", text.Mime);
            Assert.Equal(FileKind.Document, text.Kind);

            Assert.True(text.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.txt"));
            Assert.False(text.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.html"));
            Assert.False(text.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.pdf"));
            Assert.False(text.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.gif"));
            Assert.False(text.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.dxf"));
        }
    }
}
