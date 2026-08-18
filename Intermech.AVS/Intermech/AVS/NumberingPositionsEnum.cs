// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.NumberingPositionsEnum
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS;

/// <summary>Установка пропусков по позициям</summary>
public enum NumberingPositionsEnum
{
  /// <summary>Не учитывать</summary>
  NotUse,
  /// <summary>Пустых строк по разнице позиций</summary>
  Use,
  /// <summary>Пустых строк по разнице позиций + 1</summary>
  UsePlusOne,
}
