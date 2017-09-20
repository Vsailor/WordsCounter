using System;
using System.Collections.Generic;
using WordsCounter.Exceptions;
using WordsCounter.Services;

namespace WordsCounter
{
  class Program
  {
    static void Main(string[] args)
    {
      const string filePath = "data.txt";
      var fileService = new FileIOService();

      string fileContent;
      if (!fileService.ReadFromFile(filePath, out fileContent))
      {
        Console.WriteLine($"File <{filePath}> can't be read");
        return;
      }
      
      var wordService = new WordService();

      try
      {
        Dictionary<string, int> wordsCounts = wordService.GetWordsCounts(fileContent);
        fileService.WriteToFile(filePath, wordsCounts);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}