// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NavigatorWindowBaseVersionsMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Способ формирования отображения информации о базовой версии
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum NavigatorWindowBaseVersionsMode
{
  /// <summary>Стандартный заголовок</summary>
  [CustomDescription("Attribute.Interfaces.Client_36")] Hidden,
  /// <summary>Отображать значки для базовых версий</summary>
  [CustomDescription("Attribute.Interfaces.Client_37")] ShowBaseVersions,
  /// <summary>Отображать значки для остальных версий</summary>
  [CustomDescription("Attribute.Interfaces.Client_38")] ShowOtherVersions,
  /// <summary>Отображать значки для всех версий</summary>
  [CustomDescription("Attribute.Interfaces.Client_39")] ShowAllVersions,
}
