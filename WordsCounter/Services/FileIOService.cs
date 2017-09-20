using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordsCounter.Services
{
  public class FileIOService
  {
    public bool ReadFromFile(string filePath, out string fileContent)
    {
      fileContent = null;
      if (!File.Exists(filePath))
      {
        return false;
      }

      using (var streamReader = new StreamReader(filePath))
      {
        fileContent = streamReader.ReadToEnd();
      }

      return true;
    }

    public void WriteToFile(string filePath, Dictionary<string, int> content)
    {
      using (var streamWriter = new StreamWriter(filePath))
      {
        streamWriter.Write(
          string.Join(
            Environment.NewLine,
            content.Select(item =>
            $"{item.Key} {item.Value}")));
      }
    }
  }
}