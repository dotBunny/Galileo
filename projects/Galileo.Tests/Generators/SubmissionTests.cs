using Galileo.Tests.Markov;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Galileo.Tests.Generators
{
    public class SubmissionTests
    {
        static void Fixture()
        {
          if (SubmissionGenerator.EssayParagraph == null)
          {
            SubmissionGenerator.EssayParagraph = Learning.LearnFrom("Test-Markov-Essay.txt");

            StringMarkovFingerprint fingerPrint = new StringMarkovFingerprint(SubmissionGenerator.EssayParagraph);

            string dump = fingerPrint.Dump();

            System.IO.File.WriteAllText(TestHelper.GetTempFilePath("txt"), dump);

          }
        }
        
        [Fact]
        public void MakeSimpleDocument()
        {
          Fixture();
          const int numPasses = 10;
          
          System.Random random = new Random();

          for(int i=0;i < numPasses;i++)
          {
            string file1 = TestHelper.GetTempFilePath("docx"); 
            string file2 = TestHelper.GetTempFilePath("docx");

            Assert.NotEqual(file1, file2);

            int seed = random.Next();

            SubmissionGenerator.MakeEssay(file1, seed);
            SubmissionGenerator.MakeEssay(file2, seed);

            TestHelper.AssertForWordFiles(file1, file2);
          }
        }
    }
}
