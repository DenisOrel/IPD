// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NavigatorLinksMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Способ обработки гиперссылок в элементах Навигатора</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum NavigatorLinksMode
{
  /// <summary>Стандартный заголовок</summary>
  [CustomDescription("Attribute.Interfaces.Client_40")] None,
  /// <summary>Открывать с помощью левой кнопки мыши</summary>
  [CustomDescription("Attribute.Interfaces.Client_41")] LeftMouseClick,
  /// <summary>
  /// Открывать с помощью средней кнопки мыши или её колёсика
  /// </summary>
  [CustomDescription("Attribute.Interfaces.Client_42")] MiddleMouseClick,
}
