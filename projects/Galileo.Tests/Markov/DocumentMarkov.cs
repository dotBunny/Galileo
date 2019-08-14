using Galileo.Tests.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Tests.Markov
{
    enum DocumentStructureType
    {
      Paragraph,
      Title,
      Heading,
      Quote,
      Citation,
      Image,
      Table,

      __Terminator = 9999999
    }

    struct DocumentStructure
    {
      public DocumentStructureType type;
      public int min;
      public int max;
      public float chance;
      public float headingChance;

      public DocumentStructure(DocumentStructureType type_, int min_ = 1, int max_ = 100, float chance_ = 100, float headingChance_ = 0.0f)
      {
        type = type_;
        min = min_;
        max = max_;
        chance = chance_;
        headingChance = headingChance_;
      }

    }

    class DocumentMarkov : Markov<DocumentStructure>
    {

      public static Unigram Structure(DocumentStructureType type_, int min_ = 1, int max_ = 100, float chance_ = 100, float headingChance_ = 100)
      {
        return new Unigram(new DocumentStructure(type_, min_, max_, chance_, headingChance_));
      }

      
      protected DocumentMarkov() : base()
      {
      }

      public DocumentMarkov(int seed_) : base(seed_)
      {
        Terminator = Structure(DocumentStructureType.__Terminator);
      }

      public new DocumentMarkov Duplicate(int seed_)
      {
        DocumentMarkov markov = new DocumentMarkov();
        markov.random = new Random(seed_);
        markov.seed = seed_;
        markov.starts = starts;
        markov.Terminator = Terminator;
        return markov;
      }

      bool Chance(float chance_)
      {
        return random.NextDouble() * 100.0 <= chance_;
      }

      public void WriteToDocument(IDocument document, float integrity = 1.0f)
      {
        var unigram = Pick();
        while(unigram != null && unigram != Terminator)
        {
          DocumentStructure structure = unigram.data;

          document.AppendDebug(structure.type.ToString());

          if (Chance(structure.chance))
          {

            switch(structure.type)
            {
              case DocumentStructureType.Paragraph:
              {
                if (Chance(structure.headingChance))
                {
                  document.AppendHeading(integrity);
                }

                int length = random.Next(structure.min, structure.max);

                for(int i=0;i < length;i++)
                {
                  document.AppendParagraph(integrity);
                }
              }
              break;
              case DocumentStructureType.Title:
              {
                document.AppendTitle(integrity);
              }
              break;
              case DocumentStructureType.Heading:
              {
                document.AppendHeading(integrity);
              }
              break;
              case DocumentStructureType.Quote:
              {
                // @TODO
              }
              break;
              case DocumentStructureType.Citation:
              {
                // @TODO
              }
              break;
              case DocumentStructureType.Image:
              {
                // @TODO
              }
              break;
              case DocumentStructureType.Table:
              {
                // @TODO
              }
              break;
            }
          }

          unigram = unigram.Pick(random);
        }
        
        if (unigram == Terminator)
        {
            document.AppendDebug("TERMINATOR");
        }


        /*
      
        DocumentMarkov structure = Submissions.Essay.Structure.Duplicate(seed); 

        DocXDocument doc = new DocXDocument();
        doc.BeginWriting(0, path, seed, seed);

        DocumentUnigram unigram = structure.Pick();
        
        while(unigram != null && unigram != structure.Terminator)
        {
          doc.Para(unigram.data.ToString(), "Debug");

          if (unigram.data == DocumentStructure.Paragraph)
          {
            doc.AppendParagraph();
          }
          else if (unigram.data == DocumentStructure.Title)
          {
            doc.AppendHeading();
          }
          else
          {
            break;
          }

          unigram = unigram.Pick(structure.random);
        }

        if (unigram == structure.Terminator)
        {
            doc.Para("TERMINATOR", "Debug");
        }

        doc.EndWriting();
        */
      }

    }

}
