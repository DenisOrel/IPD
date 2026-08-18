// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DictionaryHelper
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary> Всякие полезные функции для работы со словарём </summary>
public abstract class DictionaryHelper
{
  /// <summary> Валидация словаря. Удаляет элементы, которые не проходят валидацию </summary>
  /// <param name="dictionary"> Объект, поддерживающий интерфейс IDictionary </param>
  /// <param name="itemValidator"> Процедура валидации элемента</param>
  public static void ValidateDictionary(
    ref object dictionary,
    DictionaryHelper.ValidateItemDelegate itemValidator)
  {
    if (dictionary == null || itemValidator == null || !(dictionary is IDictionary))
      return;
    IDictionary dictionary1 = dictionary as IDictionary;
    object[] objArray = new object[dictionary1.Count];
    int index1 = 0;
    foreach (DictionaryEntry dictionaryEntry in dictionary1)
    {
      if (!itemValidator(dictionaryEntry))
      {
        objArray.SetValue(dictionaryEntry.Key, index1);
        ++index1;
      }
    }
    if (index1 <= 0)
      return;
    for (int index2 = 0; index2 < index1; ++index2)
      dictionary1.Remove(objArray[index2]);
  }

  /// <summary> Процедура валидаци элемента словаря </summary>
  public delegate bool ValidateItemDelegate(DictionaryEntry dictionaryEntry);
}
