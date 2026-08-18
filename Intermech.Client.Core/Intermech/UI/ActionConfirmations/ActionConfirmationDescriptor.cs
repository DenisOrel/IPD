
// Type: Intermech.UI.ActionConfirmations.ActionConfirmationDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.UI.ActionConfirmations;

internal sealed class ActionConfirmationDescriptor
{
  public ActionConfirmationDescriptor(
    string key,
    string category,
    string text,
    ICollection<Tuple<int, string>> values)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    if (string.IsNullOrEmpty(category))
      throw new ArgumentException("Категория действия не задана.", nameof (category));
    if (string.IsNullOrEmpty(text))
      throw new ArgumentException("Текст действия не задан.", nameof (text));
    if (values == null)
      throw new ArgumentNullException(nameof (values));
    if (values.Count < 2)
      throw new ArgumentException("Коллекция допустимых значений должна содержать 2 и более элементов.", nameof (values));
    this.Key = key;
    this.Category = category;
    this.Text = text;
    this.Values = values;
  }

  public string Key { get; private set; }

  public string Category { get; private set; }

  public string Text { get; private set; }

  public ICollection<Tuple<int, string>> Values { get; private set; }
}
