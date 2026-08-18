// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NavigatorCaptionVersionsMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Способ формирования заголовков в строках гридов и деревьев для версионных объектов
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum NavigatorCaptionVersionsMode
{
  /// <summary>Заголовок</summary>
  [CustomDescription("Attribute.Interfaces.Client_27")] Caption,
  /// <summary>[№] Заголовок</summary>
  [CustomDescription("Attribute.Interfaces.Client_28")] BracketCaption,
  /// <summary>Заголовок [№]</summary>
  [CustomDescription("Attribute.Interfaces.Client_29")] CaptionBracket,
  /// <summary>[Версия №] Заголовок</summary>
  [CustomDescription("Attribute.Interfaces.Client_30")] VersionBracketCaption,
  /// <summary>Заголовок [версия №]</summary>
  [CustomDescription("Attribute.Interfaces.Client_31")] CaptionVersionBracket,
  /// <summary>Заголовок, версия 1</summary>
  [CustomDescription("Attribute.Interfaces.Client_32")] CaptionVersion,
}
