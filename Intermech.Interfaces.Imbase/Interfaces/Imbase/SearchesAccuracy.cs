// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.SearchesAccuracy
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// Перечисление, указывающее точность выполняемого запроса.
/// </summary>
public enum SearchesAccuracy
{
  /// <summary>Начинается со строки</summary>
  Start,
  /// <summary>Содержится в строке</summary>
  Сontain,
  /// <summary>Оканчивается на строку</summary>
  End,
  /// <summary>Точное значение</summary>
  Exact,
  /// <summary>По шаблону</summary>
  Template,
}
