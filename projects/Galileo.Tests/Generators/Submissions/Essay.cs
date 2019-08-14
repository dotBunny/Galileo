using Galileo.Tests.Markov;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Tests.Generators.Submissions
{
    static class Essay
    {
      public static DocumentMarkov Structure;
      
      static Essay()
      {
        // https://www.utsc.utoronto.ca/ccds/how-write-essay
        var title       = DocumentMarkov.Structure(DocumentStructureType.Title, 1, 1, 80);
        var outline     = DocumentMarkov.Structure(DocumentStructureType.Paragraph, 0, 4, 70, 40);
        var body        = DocumentMarkov.Structure(DocumentStructureType.Paragraph, 1, 10, 100, 40);
        var conclusion  = DocumentMarkov.Structure(DocumentStructureType.Paragraph, 1, 3, 70, 50);
        var citations   = DocumentMarkov.Structure(DocumentStructureType.Citation, 1, 5, 60, 50);

        Structure = new DocumentMarkov(1);
        Structure.Add(title)
                    .Add(outline)
                      .Add(body)
                        .Add(conclusion)
                          .Add(citations);
      }
    }
}
