using System;
using System.Collections.Generic;
using System.Text;

namespace Galileo.Tests.Markov
{
    class StringMarkovFingerprint
    {
      
      HashSet<StringMarkov.Unigram> unigrams; 
      StringMarkov.Unigram terminator;

      public StringMarkovFingerprint(StringMarkov markov)
      {
        terminator = markov.Terminator;
        unigrams = new HashSet<StringMarkov.Unigram>(markov.starts.Count * 4);
        foreach(var unigram in markov.starts)
        {
          if (unigrams.Contains(unigram) == false)
          {
            unigrams.Add(unigram);

          }
          
          foreach(var next in unigram.weights)
          {
            if (unigrams.Contains(next.Key) == false)
            {
              unigrams.Add(next.Key);
            }
          }
        }
      }

      public string Dump()
      {
        StringBuilder sb = new StringBuilder();

        foreach(var unigram in unigrams)
        {
          sb.AppendFormat("{0}", unigram);
          
          sb.AppendLine();

          foreach(var next in unigram.weights)
          {
            if (next.Key == terminator)
            {
              sb.AppendFormat("  <STOP> {1}", next.Key.ToString(), next.Value);
              sb.AppendLine();
            }
            else
            {
              sb.AppendFormat("  {0}:{1}", next.Key.ToString(), next.Value);
              sb.AppendLine();
            }
          }

        }

        return sb.ToString();
      }

    }
}
