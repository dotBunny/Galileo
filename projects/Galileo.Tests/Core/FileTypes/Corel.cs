using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Galileo.Core.FileTypes;

namespace Galileo.Tests.Core.FileTypes
{
    public class Corel
    {
        [Fact]
        public void Wpd()
        {

            var wpd = Types.CorelWordPerfect;

            Assert.Equal("application/vnd.wordperfect", wpd.Mime);
            Assert.Equal(FileKind.Document, wpd.Kind);

            Assert.True(wpd.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.wpd"));
            Assert.False(wpd.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.docx"));
            Assert.False(wpd.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.pdf"));
        }
        
    }
}
