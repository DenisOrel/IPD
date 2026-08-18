// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportsDocModes
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Режим загрузки структуры документов</summary>
[Flags]
public enum ReportsDocModes
{
  /// <summary>Reserved</summary>
  None = 0,
  /// <summary>Заполняет обязательные атрибуты</summary>
  IncludeObligatoryAttributes = 1,
  /// <summary>Заполняет пользовательские атрибуты</summary>
  IncludeCustomAttributes = 2,
  /// <summary>Заполняет содержимое документов</summary>
  IncludeDocData = 4,
}
