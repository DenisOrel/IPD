// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IXMLFilterStorageLoadSave
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System.Xml;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс, позволяющий выполнять сохранение и загрузку данных в хранилище XML
/// </summary>
public interface IXMLFilterStorageLoadSave
{
  /// <summary>Загрузить фильтр из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="filterNode">Узел с настройками фильтра</param>
  void LoadFilter(XMLSettingsStorage xmlStorage, XmlNode filterNode);

  /// <summary>Сохранить фильтр в указанные настройки</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="filtersNode">Родительский узел или null</param>
  void SaveFilter(XMLSettingsStorage xmlStorage, XmlNode filtersNode);
}
