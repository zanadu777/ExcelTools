using System.Collections.Generic;

namespace CollectionTools.Extensions;

public static class DictionaryExtensions
{
  public static Dictionary<TKey, List<TValue>> AddToIndex<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
  {
    if (dictionary.ContainsKey(key))
      dictionary[key].Add(value);
    else
      dictionary[key] = new List<TValue> { value };

    return dictionary;
  }


  public static Dictionary<TKey, List<TValue>> AddToIndex<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, KeyValuePair<TKey, TValue> pair)
  {
    if (dictionary.ContainsKey(pair.Key))
      dictionary[pair.Key].Add(pair.Value);
    else
      dictionary[pair.Key] = new List<TValue> { pair.Value };

    return dictionary;
  }

  public static Dictionary<TKey, List<TValue>> AddToIndex<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, Dictionary<TKey, List<TValue>> anotherDictionary)
  {
    foreach (var item in anotherDictionary)
    {
      if (dictionary.ContainsKey(item.Key))
        dictionary[item.Key].AddRange(item.Value);
      else
        dictionary[item.Key] = item.Value;
    }
    return dictionary;
  }
}