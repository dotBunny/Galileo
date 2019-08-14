using System;
using System.Text;

namespace Galileo.Tests.Markov
{
    class StringMarkov : Markov<string>
    {
      
      protected StringMarkov() : base()
      {
      }

      public StringMarkov(int seed_) : base(seed_)
      {
        Terminator = new Unigram(Guid.NewGuid().ToString());
      }

      public new StringMarkov Duplicate(int seed_)
      {
        StringMarkov markov = new StringMarkov();
        markov.random = new Random(seed_);
        markov.seed = seed_;
        markov.starts = starts;
        markov.Terminator = Terminator;
        return markov;
      }

    public void Sentence(StringBuilder into)
      {
        Unigram unigram = Pick();
        int counter = 0;
        while(unigram != null && unigram != Terminator)
        {
          counter++;
          if (counter >= 100)
            break;

          into.Append(unigram.data);
          
          unigram = unigram.Pick(random);

          if (unigram == Terminator)
            break;
          else
          {
            into.Append(' ');
          }

        }
        into.Append('.');
      }
      
      public static string Heading(StringMarkov student, StringMarkov original, float integrity)
      {
        StringBuilder sb = new StringBuilder();
        student.Sentence(sb);
        return sb.ToString();
      }

      public static string Paragraph(StringMarkov student, StringMarkov original, float integrity)
      {
        StringBuilder sb = new StringBuilder();
        int numSentences = student.NextInt(10) + 1;
        for(int i=0;i < numSentences;i++)
        {
          student.Sentence(sb);
          sb.Append(" ");
        }
        
        return sb.ToString();
      }
    }
}
