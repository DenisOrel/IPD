// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IPackageCollection
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System.Collections;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Коллекция дополнительных модулей</summary>
public interface IPackageCollection : ICollection, IEnumerable
{
  /// <summary>
  /// Получить дополнительный модуль расширения с указанным индексом
  /// </summary>
  /// <param name="index">Индекс</param>
  /// <returns>Дополнительный модуль расширения с указанным индексом</returns>
  IPackage this[int index] { get; }
}
