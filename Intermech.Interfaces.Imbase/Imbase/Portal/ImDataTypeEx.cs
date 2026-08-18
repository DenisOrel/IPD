// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.ImDataTypeEx
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Portal;

/// <summary>Тип данных поля в таблице IMBASE</summary>
public enum ImDataTypeEx
{
  /// <summary>Не поддерживается</summary>
  [Description("Не поддерживается")] IEX_UNKNOWN,
  /// <summary>Строковое</summary>
  [Description("Строковое")] IEX_STRING,
  /// <summary>Целое</summary>
  [Description("Целое")] IEX_INTEGER,
  /// <summary>Вещественное</summary>
  [Description("Вещественное")] IEX_FLOAT,
  /// <summary>Логическое</summary>
  [Description("Логическое")] IEX_BOOL,
  /// <summary>Ссылка</summary>
  [Description("Ссылка")] IEX_REF,
  /// <summary>Набор</summary>
  [Description("Набор")] IEX_SET,
  /// <summary>ups... :(</summary>
  [Description("")] IEX_ADT,
}
