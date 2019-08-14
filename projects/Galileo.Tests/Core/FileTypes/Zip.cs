using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Galileo.Core.FileTypes;

namespace Galileo.Tests.FileTypes
{
    public class ZipTests
    {
        [Fact]
        public void TestZip()
        {
            var zip = Types.Zip;

            Assert.Equal("application/zip", zip.Mime);
            Assert.Equal(FileKind.Archive, zip.Kind);

            Assert.True(zip.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.zip"));
            Assert.False(zip.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.doc"));
            Assert.False(zip.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.docx"));

        }
        
    }
}