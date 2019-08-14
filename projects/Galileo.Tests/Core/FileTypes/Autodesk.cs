using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Galileo.Core.FileTypes;

namespace Galileo.Tests.Core.FileTypes
{
    public class Autodesk
    {
        [Fact]
        public void Dxf()
        {

            var dxf = Types.AutoCadDxf;

            // http://filext.com/file-extension/DXF
            Assert.Equal("application/dxf", dxf.Mime);
            Assert.Equal(FileKind.Model, dxf.Kind);

            Assert.True(dxf.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.dxf"));
            Assert.True(dxf.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample1.dxf"));
            Assert.False(dxf.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.txt"));
            Assert.False(dxf.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.gif"));
        }
        
    }
}
