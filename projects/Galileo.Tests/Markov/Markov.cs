using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Galileo.Tests.Markov
{
  class Markov<T>
  {
    public class Unigram
    {
      public T    data;
      public int  frequency;

      public Dictionary<Unigram, int> weights;

      public Unigram()
      {
      }

      public Unigram(T data_)
      {
        data = data_;
      }
      
      public override bool Equals(object obj)
      {
        if (obj == null || GetType() != obj.GetType()) 
            return false;

        Unigram p = (Unigram)obj;
        return data.Equals(p.data);
      }

      public override int GetHashCode()
      {
        return data.GetHashCode();
      }

      public override string ToString()
      {
        return data.ToString();
      }

      public Unigram Pick(Random random)
      {
        // https://softwareengineering.stackexchange.com/questions/150616/return-random-list-item-by-its-weight
        
        if (weights == null)
          return null;

        int w = 0;
        Unigram u = null;
        foreach(var t in weights)
        {
          int s = t.Value;
          if (random.Next(w + s) >= w)
            u = t.Key;
          w += s;
        }
        
        return u;
      }

      public Unigram Add(Unigram unigram)
      {
        Add(unigram, 1);
        return unigram;
      }

      public void Add(Unigram unigram, int weight)
      {
        if (weights == null)
        {
          weights = new Dictionary<Unigram, int>();
          weights.Add(unigram, 1);
        }
        else
        {
          int w;
          if (weights.TryGetValue(unigram, out w) == false)
          {
            weights.Add(unigram, 1);
          }
          else
          {
            w = w + 1;
            weights[unigram] = w;
          }
        }
      }

    }

    protected int           seed;
    public    Random        random;
    public    List<Unigram> starts;
    public    Unigram       Terminator { get; protected set; }

    protected Markov()
    {
    }

    public Markov(int seed_)
    {
      seed   = seed_;
      starts = new List<Unigram>();
      random = new Random(seed_);
      Terminator = new Unigram(default(T));
    }
    
    public virtual Markov<T> Duplicate(int seed_)
    {
      Markov<T> markov = new Markov<T>();
      markov.random = new Random(seed_);
      markov.seed = seed_;
      markov.starts = starts;
      markov.Terminator = Terminator;
      return markov;
    }

    int Seed
    {
      get { return seed; }
    }

    public int NextInt()
    {
      return random.Next();
    }

    public int NextInt(int maxValue)
    {
      return random.Next(maxValue);
    }

    public Unigram Pick()
    {
      return starts[NextInt(starts.Count)];
    }
    
    public Unigram Add()
    {
      Unigram unigram = new Unigram();
      starts.Add(unigram);
      return unigram;
    }

    public Unigram Add(Unigram unigram)
    {
      starts.Add(unigram);
      return unigram;
    }

    public Unigram Add(T data)
    {
      Unigram unigram = new Unigram(data);
      starts.Add(unigram);
      return unigram;
    }

    public Unigram Find(T search)
    {
      foreach(var unigram in starts)
      {
        if (EqualityComparer<T>.Default.Equals(search, unigram.data))
        {
          return unigram;
        }
      }
      return null;
    }
    
    public Unigram FindOrNew(T search)
    {
      foreach(var unigram in starts)
      {
        if (EqualityComparer<T>.Default.Equals(search, unigram.data))
        {
          return unigram;
        }
      }
      return Add(search);
    }

    public Unigram FindOrNew(T search, out bool didNew)
    {
      foreach(var unigram in starts)
      {
        if (EqualityComparer<T>.Default.Equals(search, unigram.data))
        {
          didNew = false;
          return unigram;
        }
      }
      didNew = true;
      return Add(search);
    }
  }
}
