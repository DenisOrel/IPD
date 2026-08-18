// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NavigatorWindowCaptionsMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Способ формирования заголовка окна "Навигатора"</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum NavigatorWindowCaptionsMode
{
  /// <summary>Стандартный заголовок</summary>
  [CustomDescription("Attribute.Interfaces.Client_33")] Default,
  /// <summary>
  /// Стандартный заголовок + версия (если в корне дерева отображается объект)
  /// </summary>
  [CustomDescription("Attribute.Interfaces.Client_34")] TextAndVersion,
  /// <summary>
  /// Стандартный заголовок + дополнительная информация
  /// (будут опрашиваться подписчики NavigatorWindowCaptionsHelper.OnGetNavigatorWindowCaption)
  /// </summary>
  [CustomDescription("Attribute.Interfaces.Client_35")] ExtraTexts,
}
