using Galileo.Tests.Markov;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Tests.Generators
{
    static class SubmissionGenerator
    {
      public static StringMarkov EssayParagraph;
      
      public static void MakeEssay(string path)
      {
        System.Random rand = new Random();
        MakeEssay(path, rand.Next());
      }

      public static void MakeEssay(string path, int seed)
      {
      
        DocumentMarkov structure = Submissions.Essay.Structure.Duplicate(seed); 

        DocXDocument doc = new DocXDocument();
        doc.BeginWriting(0, path, seed, seed);
        float integrity = 1.0f;
        
        Submissions.Essay.Structure.Duplicate(seed).WriteToDocument(doc, integrity);

        doc.EndWriting();
      }
      
    }




}
