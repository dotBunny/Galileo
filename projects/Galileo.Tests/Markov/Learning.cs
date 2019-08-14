using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Galileo.Tests.Markov
{
    static class Learning
    {
    
      static string[] GetWords(string input)
      {
          List<string> words = new List<string>();

          var matches = Regex.Matches(input, @"\w+[^\s]*\w+|\w");

          foreach (Match match in matches) {
              var word = match.Value;
              words.Add(word);
          }
          return words.ToArray();
      }
      
      public static StringMarkov LearnFrom(string fileName)
      {
        StringMarkov markov = new StringMarkov(1);

        
        string[] markovTraining = System.IO.File.ReadAllLines(TestHelper.GetResourceContentPath(fileName));
        
        Dictionary<string, StringMarkov.Unigram> unigrams = new Dictionary<string, StringMarkov.Unigram>();

        // Read all lines, and seperate words.
        // Put words in s_Words. Word get's added to previous word (if any), and is counted.
        foreach(var l in markovTraining)
        {
          string[] words = GetWords(l);
          StringMarkov.Unigram last = null, now = null;
          foreach(var word in words)
          {
            now = null;

            if (last == null)
            {
              bool didNew;
              now = markov.FindOrNew(word, out didNew);
              if (didNew)
              {
                unigrams.Add(word, now);
              }
            }

            if (now == null && unigrams.TryGetValue(word, out now) == false)
            {
              now = new StringMarkov.Unigram(word);
              unigrams.Add(word, now);
            }

            if (last != null)
            {
              last.Add(now);
            }

            now.frequency++;
            last = now;
          }

          if (last != null)
          {
            last.Add(markov.Terminator);
          }
        }

        return markov;
      }
      

    }
}
