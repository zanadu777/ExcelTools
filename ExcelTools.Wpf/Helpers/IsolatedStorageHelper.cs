using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;

//using MessagePack;

namespace ExcelTools.Wpf.Helpers;

public static class IsolatedStorageHelper
{

  public static void WriteText(string fileName, string content)
  {
    using var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
    using var stream = new IsolatedStorageFileStream(fileName, FileMode.Create, isolatedStorage);
    using var writer = new StreamWriter(stream);

    writer.Write(content);
  }

  public static string ReadText(string fileName)
  {
    using IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();

    if (!isolatedStorage.FileExists(fileName))
      return string.Empty;


    using IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.Open, isolatedStorage);
    using StreamReader reader = new StreamReader(stream);

    return reader.ReadToEnd();
  }


  public static void SerializeJson(object obj, string fileName)
  {
    string jsonString = JsonConvert.SerializeObject(obj);
    WriteText(fileName, jsonString);
  }

  public static T DeserializeJson<T>(string fileName)
  {
    string jsonString = ReadText(fileName);
    if (jsonString == null) return default(T);
    return JsonConvert.DeserializeObject<T>(jsonString);
  }
}