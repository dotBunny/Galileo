using Galileo.Core;
using NLipsum.Core;
using System;
using System.Reflection;
using System.Xml;
using System.Collections.Generic;
using Xunit;

namespace Galileo.Tests.Comparison
{
    public class Levenshtein
    {
        [Fact]
        public void CalculateSimilarityShort()
        {
            const string String1 = "Hello";
            const string String2 = "jacuzzis";
            
            Assert.Equal(1.0, Compare.CalculateSimilarity(String1, String1));
            Assert.Equal(0.0, Compare.CalculateSimilarity(String1, String2));
        }
        
        [Fact]
        public void CalculateSimilarityShortMany()
        {
            const int numChecks = 10000;
            const int seed      = 40392;

            XmlDocument document = new XmlDocument();

            // Load document using helper function
            document.Load(TestHelper.GetResourceContentPath("Test-English-Words.xml"));

            var node = document.SelectSingleNode("words");
            
            string[] words = node.InnerText.Split(new [] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for(int i=0;i < words.Length;i++)
              words[i] = words[i].Trim();

            System.Random rand = new Random(seed);

            for(int i=0;i < numChecks;i++)
            {
                for(int j=0;j < words.Length;j++)
                {
                    int firstIndex, secondIndex;
                    while(true)
                    {
                      firstIndex = rand.Next(words.Length);
                      secondIndex = rand.Next(words.Length);
                      if (firstIndex != secondIndex)
                        break;
                    }

                    string first = words[firstIndex], second = words[secondIndex];

                    Assert.NotEqual(first, second);
                    Assert.NotEqual(1.0, Compare.CalculateSimilarity(first, second));
                }
            }
            
        }
        [Fact]
        public void CalculateSimilarityBigMany()
        {
            const int numChecks = 2;

            for(int i=0;i < numChecks;i++)
            {
              SetNLipsumSeed(10001 + i);
              string large1 = LipsumGenerator.Generate(16);
            
              SetNLipsumSeed(84301 + i);
              string large2 = LipsumGenerator.Generate(16);
            
              Assert.NotEqual(1.0, Compare.CalculateSimilarity(large1, large2));
            }
        }

        [Fact]
        public void NLipsumSeedTest()
        {
            const int NumParagraphs = 2;
            const int Seed1 = 1234;
            const int Seed2 = 5678;

            SetNLipsumSeed(Seed1);
            string paragraph1 = LipsumGenerator.Generate(NumParagraphs);
            SetNLipsumSeed(Seed2);
            string paragraph2 = LipsumGenerator.Generate(NumParagraphs);
            SetNLipsumSeed(Seed1);
            string paragraph3 = LipsumGenerator.Generate(NumParagraphs);

            Assert.Equal(paragraph1, paragraph3);
            Assert.NotEqual(paragraph1, paragraph2);
        }
        
#region Utilities
        private static void SetNLipsumSeed(int seed)
        {
          // NLipsum uses a random number generator to generate phrases of text
          // Sadly, it uses a static variable in NLipsum.Core.LipsumUtilities called
          // 'rand'. This is a private static variable.
          // 
          // To change this, we have to use reflection to access the field, and
          // replace it.
          Type type = typeof(NLipsum.Core.LipsumUtilities);
          FieldInfo info = type.GetField("rand", BindingFlags.NonPublic | BindingFlags.Static);
          info.SetValue(null, new Random(seed));
        }
#endregion
    }
}