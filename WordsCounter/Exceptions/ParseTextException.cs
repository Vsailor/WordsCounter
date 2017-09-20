using System;

namespace WordsCounter.Exceptions
{
  public class ParseTextException : Exception
  {
    public ParseTextException(string message) : base(message)
    {
    }
  }
}