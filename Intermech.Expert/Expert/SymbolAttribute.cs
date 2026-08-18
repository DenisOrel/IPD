// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.SymbolAttribute
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Expert;

/// <summary>Атрибут "Символ" для ячейки</summary>
[AttributeUsage(AttributeTargets.Field)]
public class SymbolAttribute : Attribute
{
  private string _symbol = string.Empty;

  /// <summary>Конструктор</summary>
  /// <param name="symbol">Строковая интерпритация символа</param>
  public SymbolAttribute(string symbol) => this._symbol = symbol;

  /// <summary>Вывод строки</summary>
  /// <returns>Строка со значением</returns>
  public override string ToString()
  {
    string empty = string.Empty;
    return LocalizationHolder.rma.GetString(this._symbol) ?? this._symbol;
  }
}
