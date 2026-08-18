// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IPackage
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Класс представляющий элемент дополнительного модуля.</summary>
public interface IPackage
{
  /// <summary>Инициализировать модуль расширения</summary>
  /// <param name="serviceProvider">Контейнер сервисов</param>
  void Load(IServiceProvider serviceProvider);

  /// <summary>Деинициализировать модуль расширения</summary>
  void Unload();

  /// <summary>Название модуля расширения</summary>
  string Name { get; }
}
