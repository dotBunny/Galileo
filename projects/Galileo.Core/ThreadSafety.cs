using System;
using System.Threading;

namespace Galileo.Core
{
    class Atomic<T> where T : class
    {
      private T _val;

      public T Value
      {
        get { return _val; }
      }

      public T Set(T newValue)
      {
        T currentValue = _val;
        if (_val == Interlocked.CompareExchange<T>(ref _val, newValue, currentValue))
        {
          // Was set
          return newValue;
        }
        // Was not set
        return currentValue;
      }

    }
    
    class AtomicBool
    {
      private int _val;

      public AtomicBool()
      {
        _val = 0;
      }

      public AtomicBool(bool val)
      {
        _val = val ? 1 : 0;
      }

      public bool Value
      {
        get { return _val == 1; }
      }

      public bool Set(bool newValue)
      {
        int initialValue;
        int newValueInt = newValue ? 1 : 0;
        do
        {
          initialValue = _val;
        }
        while(initialValue != Interlocked.CompareExchange(ref _val, newValueInt, initialValue));
        return newValue;
      }
    }

    class AtomicInt
    {
      private int _val;

      public int Value
      {
        get { return _val; }
      }

      public int Set(int newValue)
      {
        int initialValue;
        do
        {
          initialValue = _val;
        }
        while(initialValue != Interlocked.CompareExchange(ref _val, newValue, initialValue));
        return newValue;
      }

      public int Add(int addend)
      {
        int initialValue, newValue;
        do
        {
          initialValue = _val;
          newValue = initialValue + addend;
        }
        while(initialValue != Interlocked.CompareExchange(ref _val, newValue, initialValue));
        return newValue;
      }
      
      public int Subtract(int addend)
      {
        int initialValue, newValue;
        do
        {
          initialValue = _val;
          newValue = initialValue - addend;
        }
        while(initialValue != Interlocked.CompareExchange(ref _val, newValue, initialValue));
        return newValue;
      }
    }

    class AtomicFloat
    {
      private float _val;

      public float Value
      {
        get { return _val; }
      }

      public float Set(float newValue)
      {
        float initialValue;
        do
        {
          initialValue = _val;
        }
        while(initialValue != Interlocked.CompareExchange(ref _val, newValue, initialValue));
        return newValue;
      }

      public float Add(float addend)
      {
        float initialValue, newValue;
        do
        {
          initialValue = _val;
          newValue = initialValue + addend;
        }
        while(initialValue != Interlocked.CompareExchange(ref _val, newValue, initialValue));
        return newValue;
      }
      
      public float Subtract(float addend)
      {
        float initialValue, newValue;
        do
        {
          initialValue = _val;
          newValue = initialValue - addend;
        }
        while(initialValue != Interlocked.CompareExchange(ref _val, newValue, initialValue));
        return newValue;
      }
      
      public float Multiply(float scalar)
      {
        float initialValue, newValue;
        do
        {
          initialValue = _val;
          newValue = initialValue * scalar;
        }
        while(initialValue != Interlocked.CompareExchange(ref _val, newValue, initialValue));
        return newValue;
      }
    }
}
