using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Galileo.Core.FileTypes;

namespace Galileo.Tests.Core.FileTypes
{
    public class OpenXml
    {
        [Fact]
        public void Word()
        {

            var word = Types.MsWordOpenXml;

            // https://stackoverflow.com/questions/4212861/what-is-a-correct-mime-type-for-docx-pptx-etc
            Assert.Equal("application/vnd.openxmlformats-officedocument.wordprocessingml.document", word.Mime);
            Assert.Equal(FileKind.Document, word.Kind);

            Assert.True(word.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.docx"));
            Assert.False(word.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.doc"));
            Assert.False(word.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.zip"));
        }
        
        [Fact]
        public void Excel()
        {
            var excel = Types.MsExcelOpenXml;
            
            // https://stackoverflow.com/questions/4212861/what-is-a-correct-mime-type-for-docx-pptx-etc
            Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excel.Mime);
            Assert.Equal(FileKind.Spreadsheet, excel.Kind);

            Assert.True(excel.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.xlsx"));
            Assert.False(excel.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.xls"));
            Assert.False(excel.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.zip"));
        }
        
        [Fact]
        public void PowerPoint()
        {
            var powerPoint = Types.MsPointPointOpenXml;

            // 
            // https://stackoverflow.com/questions/4212861/what-is-a-correct-mime-type-for-docx-pptx-etc
            Assert.Equal("application/vnd.openxmlformats-officedocument.presentationml.presentation", powerPoint.Mime);
            Assert.Equal(FileKind.Presentation, powerPoint.Kind);

            Assert.True(powerPoint.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.pptx"));
            Assert.False(powerPoint.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.ppt"));
            Assert.False(powerPoint.Check("C:\\dev\\_work\\dotBunny\\Galileo\\Tests\\UnitTests\\FileTypes\\sample.zip"));
        }
    }
}
