using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WordsCounter.Exceptions;

namespace WordsCounter.Services
{
  public class WordService
  {
    private int MAX_WORD_SIZE = 50;

    private int ALLOWED_UNIQUE_WORDS = 10000000;

    private string WORDS_SELECT_REG_EXPRESSION = "[a-z]{1,}";

    public Dictionary<string, int> GetWordsCounts(string fileContent)
    {
      IEnumerable<string> words = GetWords(fileContent);
      var result = new SortedDictionary<string, int>();

      foreach (var word in words)
      {
        string existingKey = result
          .Keys
          .FirstOrDefault(key => 
            string.Equals(word, key, StringComparison.CurrentCultureIgnoreCase));

        if (existingKey != null)
        {
          result[existingKey]++;
        }
        else
        {
          result.Add(word, 1);
        }

        if (result.Count >= ALLOWED_UNIQUE_WORDS)
        {
          throw new UniqueWordsMaxCountExceededException();
        }
      }

      return result
        .OrderByDescending(k => k.Value)
        .ThenBy(k => k.Key)
        .ToDictionary(k => k.Key, v => v.Value);
    }

    private IEnumerable<string> GetWords(string str)
    {
      var words = Regex.Matches(str, WORDS_SELECT_REG_EXPRESSION)
        .Cast<Match>()
        .Select(item => item.Groups[0].Value)
        .ToArray();

      if (words == null ||
          words.Any(w => w.Length > MAX_WORD_SIZE))
      {
        throw new ParseTextException("Could not parse text");
      }

      return words;
    }
  }
}