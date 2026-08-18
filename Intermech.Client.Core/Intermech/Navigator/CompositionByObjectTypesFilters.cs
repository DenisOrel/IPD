
// Type: Intermech.Navigator.CompositionByObjectTypesFilters
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;


namespace Intermech.Navigator;

/// <summary>
/// Список фильтров составов по типам родительских и дочерних типов объектов
/// </summary>
[Serializable]
public class CompositionByObjectTypesFilters : 
  List<CompositionByObjectTypesFilter>,
  ICompositionByObjectTypesFilters,
  IMetaDataSync,
  IXMLStorageLoadSave,
  IDatabaseLoadSave,
  ICloneable
{
  /// <summary>Guid списка фильтров</summary>
  protected static string _filtersSettingsGuid = "{76F4808E-7D62-42C3-980D-0AEC9D871403}";

  /// <summary>Создать экземпляр класса</summary>
  public CompositionByObjectTypesFilters()
  {
  }

  /// <summary>
  /// Создать экземпляр класса на основе указанной коллекции
  /// </summary>
  /// <param name="source">Коллекция-источник</param>
  public CompositionByObjectTypesFilters(CompositionByObjectTypesFilters source)
  {
    this.Assign((ICompositionByObjectTypesFilters) source);
  }

  /// <summary>
  /// Выполнить синхронизацию внутренних коллекций с кэшем метаданных
  /// </summary>
  public void SyncMetaData()
  {
    for (int index = 0; index < this.Count; ++index)
      base[index].SyncMetaData();
  }

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (xmlStorage == null)
      return;
    XmlNode node1 = xmlStorage.FindNode((XmlNode) xmlStorage.document.DocumentElement, "OT_Filters", true);
    for (int i = 0; i < node1.ChildNodes.Count; ++i)
    {
      XmlNode childNode = node1.ChildNodes[i];
      if (!(childNode.Name != "OT_Filter"))
      {
        ICompositionByObjectTypesFilter objectTypesFilter = this.Add();
        objectTypesFilter.LoadFilter(xmlStorage, childNode);
        if (objectTypesFilter.GUID == Guid.Empty)
          this.Remove(objectTypesFilter as CompositionByObjectTypesFilter);
      }
    }
  }

  /// <summary>
  /// Сохранить данные в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    if (xmlStorage == null)
      return;
    XmlNode node1 = xmlStorage.FindNode((XmlNode) xmlStorage.document.DocumentElement, "OT_Filters", true);
    xmlStorage.document.DocumentElement.RemoveChild(node1);
    XmlNode node2 = xmlStorage.FindNode((XmlNode) xmlStorage.document.DocumentElement, "OT_Filters", true);
    for (int index = 0; index < this.Count; ++index)
      base[index].SaveFilter(xmlStorage, node2);
  }

  /// <summary>Загрузить данные из настроек указанного пользователя</summary>
  /// <param name="userID">Идентификатор пользователя</param>
  public void Load(long userID)
  {
    if (userID == 0L)
      return;
    this.Clear();
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    byte[] buffer = customService[userID, (object) CompositionByObjectTypesFilters._filtersSettingsGuid] as byte[];
    CompositionByObjectTypesFilters source = (CompositionByObjectTypesFilters) null;
    if (buffer != null)
    {
      try
      {
        using (MemoryStream inStream = new MemoryStream(buffer))
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) memoryStream);
            try
            {
              source = new BinaryFormatter().Deserialize((Stream) memoryStream) as CompositionByObjectTypesFilters;
            }
            catch
            {
              source = (CompositionByObjectTypesFilters) null;
            }
          }
        }
      }
      catch
      {
        source = (CompositionByObjectTypesFilters) null;
      }
    }
    if (source != null)
      this.Assign((ICompositionByObjectTypesFilters) source);
    this.SyncMetaData();
  }

  /// <summary>Сохранить данные в настройки указанного пользователя</summary>
  /// <param name="userID">Идентификатор пользователя</param>
  public void Save(long userID)
  {
    if (userID == 0L)
      return;
    this.SyncMetaData();
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      try
      {
        new BinaryFormatter().Serialize((Stream) memoryStream, (object) this);
        using (MemoryStream outStream = new MemoryStream())
        {
          ZLibStreamHelper.PackStream((Stream) memoryStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
          customService[userID, (object) CompositionByObjectTypesFilters._filtersSettingsGuid] = (object) outStream.ToArray();
        }
      }
      catch
      {
      }
    }
  }

  /// <summary>
  /// Получить фильтр по его уникальному глобальному идентификатору
  /// </summary>
  /// <param name="filterGuid">Guid фильтра</param>
  /// <returns>Фильтр по его Guid, или null, если такой фильтр не найден</returns>
  public ICompositionByObjectTypesFilter this[Guid filterGuid]
  {
    get
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (base[index].GUID.Equals(filterGuid))
          return (ICompositionByObjectTypesFilter) base[index];
      }
      return (ICompositionByObjectTypesFilter) null;
    }
  }

  /// <summary>Получить фильтр по его индексу в коллекции</summary>
  /// <param name="index">Индекс фильтра в коллекции</param>
  /// <returns>Фильтр</returns>
  public ICompositionByObjectTypesFilter this[int index]
  {
    get => (ICompositionByObjectTypesFilter) base[index];
    set => this[index] = value as CompositionByObjectTypesFilter;
  }

  /// <summary>
  /// Добавить в список новый фильтр. Имя и Guid для фильтра генерируются автоматически
  /// </summary>
  /// <returns>Ссылка на интерфейс нового фильтра</returns>
  public ICompositionByObjectTypesFilter Add()
  {
    CompositionByObjectTypesFilter objectTypesFilter = new CompositionByObjectTypesFilter();
    this.Add(objectTypesFilter);
    return (ICompositionByObjectTypesFilter) objectTypesFilter;
  }

  /// <summary>Добавить в список новый фильтр</summary>
  /// <param name="name">Название нового фильтра</param>
  /// <param name="guid">Guid нового фильтра</param>
  /// <returns>Ссылка на интерфейс нового фильтра</returns>
  public ICompositionByObjectTypesFilter Add(string name, Guid guid)
  {
    CompositionByObjectTypesFilter objectTypesFilter = new CompositionByObjectTypesFilter(name, guid);
    this.Add(objectTypesFilter);
    return (ICompositionByObjectTypesFilter) objectTypesFilter;
  }

  /// <summary>
  /// Удалить из коллекции фильтр с указанным уникальным глобальным идентификатором
  /// </summary>
  /// <param name="guid">Guid удаляемого фильтра</param>
  /// <returns>true, если фильтр был найден и удалён</returns>
  public bool Remove(Guid guid)
  {
    ICompositionByObjectTypesFilter objectTypesFilter = this[guid];
    if (objectTypesFilter == null)
      return false;
    this.Remove(objectTypesFilter as CompositionByObjectTypesFilter);
    return true;
  }

  /// <summary>Скопировать содержимое коллекции в свои поля</summary>
  /// <param name="source">Коллекция-источник</param>
  public void Assign(ICompositionByObjectTypesFilters source)
  {
    this.Clear();
    if (source == null || source.Count == 0)
      return;
    for (int index = 0; index < source.Count; ++index)
      this.Add(new CompositionByObjectTypesFilter(source[index]));
  }

  /// <summary>Отыскать индекс указанного фильтра</summary>
  /// <param name="filter">Искомый фильтр</param>
  /// <returns>Индекс указанного фильтра или -1</returns>
  public int IndexOf(ICompositionByObjectTypesFilter filter)
  {
    if (filter == null)
      return -1;
    for (int index = 0; index < this.Count; ++index)
    {
      if (base[index].GUID.Equals(filter.GUID))
        return index;
    }
    return -1;
  }

  /// <summary>Отыскать индекс фильтра по его Guid</summary>
  /// <param name="guid">Guid фильтра</param>
  /// <returns>Индекс фильтра или -1</returns>
  public int IndexOf(Guid guid)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      if (base[index].GUID.Equals(guid))
        return index;
    }
    return -1;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new CompositionByObjectTypesFilters(this);
}
