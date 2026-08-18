// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IAdvancedServiceContainer
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;
using System.ComponentModel.Design;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Контейнер сервисов, позволяющий получать отсутствующие в своём составе сервисы у дополнительного контейнера сервисов
/// </summary>
public interface IAdvancedServiceContainer : IServiceContainer, IServiceProvider
{
  /// <summary>Дополнительный контейнер сервисов</summary>
  IServiceProvider AdvancedProvider { get; set; }
}
