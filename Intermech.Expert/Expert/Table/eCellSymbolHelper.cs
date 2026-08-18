// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Table.eCellSymbolHelper
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System;
using System.Reflection;

#nullable disable
namespace Intermech.Expert.Table;

/// <summary>Helper для преобразования eCellSymbol в строку</summary>
public class eCellSymbolHelper
{
  /// <summary>Преобразование eCellSymbol в строку</summary>
  /// <param name="symbol">eCellSymbol</param>
  /// <returns>строка с символом</returns>
  public static string GetSymbol(eCellSymbol symbol)
  {
    foreach (FieldInfo field in typeof (eCellSymbol).GetFields())
    {
      eCellSymbol eCellSymbol = (eCellSymbol) field.GetValue((object) eCellSymbol.None);
      if (symbol.Equals((object) eCellSymbol) && field.GetCustomAttributes(typeof (SymbolAttribute), true) is SymbolAttribute[] customAttributes && customAttributes.Length.Equals(1) && customAttributes[0] != null)
        return customAttributes[0].ToString();
    }
    throw new ArgumentException(LocalizationHolder.rm.GetString("Expert_1"));
  }

  /// <summary>Преобразование строки в eCellSymbol</summary>
  /// <param name="symbol">Строка с символом</param>
  /// <returns>eCellSymbol</returns>
  public static eCellSymbol GetSymbol(string symbol)
  {
    foreach (FieldInfo field in typeof (eCellSymbol).GetFields())
    {
      eCellSymbol symbol1 = (eCellSymbol) field.GetValue((object) eCellSymbol.None);
      if (field.GetCustomAttributes(typeof (SymbolAttribute), true) is SymbolAttribute[] customAttributes && customAttributes.Length.Equals(1) && customAttributes[0] != null && customAttributes[0].ToString().Equals(symbol))
        return symbol1;
    }
    throw new ArgumentException(LocalizationHolder.rm.GetString("Expert_2"));
  }
}
