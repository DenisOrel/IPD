// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.OfficeDocumentTypes
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Вид канцелярского документа.</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Description("Вид канцелярского документа")]
[Category("Misc")]
public enum OfficeDocumentTypes
{
  [Description("Неопределен")] Unknown = -1, // 0xFFFFFFFF
  /// <summary>Входящий</summary>
  [Description("Входящий")] Incoming = 0,
  /// <summary>Исходящий</summary>
  [Description("Исходящий")] Outgoing = 1,
  /// <summary>Внутренний</summary>
  [Description("Внутренний")] Internal = 2,
}
