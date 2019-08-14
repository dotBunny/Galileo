using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Galileo.Core.FileTypes;

namespace Galileo.Tests.Core.FileTypes
{
    public class Microsoft
    {
        [Fact]
        public void Rtf()
        {

            var rtf = Types.Rtf;

            Assert.Equal("application/rtf", rtf.Mime);
            Assert.Equal(FileKind.Document, rtf.Kind);

            Assert.True(rtf.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.rtf"));
            Assert.False(rtf.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.txt"));
            Assert.False(rtf.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.pdf"));
        }
        
    }
}
