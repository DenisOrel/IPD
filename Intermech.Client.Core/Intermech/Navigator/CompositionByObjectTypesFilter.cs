
// Type: Intermech.Navigator.CompositionByObjectTypesFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Navigator;

/// <summary>
/// Фильтр составов по типам родительских и дочерних типов объектов
/// </summary>
[Serializable]
public class CompositionByObjectTypesFilter : 
  ICompositionByObjectTypesFilter,
  IMetaDataSync,
  IXMLFilterStorageLoadSave,
  ICloneable
{
  /// <summary>
  /// Узел с настройками фильтров по типам родительских и дочерних типов объектов - [OT_Filters]
  /// </summary>
  [NonSerialized]
  public const string xmlFiltersNode = "OT_Filters";
  /// <summary>
  /// Узел с настройками фильтра по типам родительских и дочерних типов объектов - [OT_Filter]
  /// </summary>
  [NonSerialized]
  public const string xmlFilterNode = "OT_Filter";
  /// <summary>
  /// Узел с настройками родительского типа объектов - [OT_ParentType]
  /// </summary>
  [NonSerialized]
  public const string xmlParentTypeNode = "OT_ParentType";
  /// <summary>
  /// Узел с настройками дочернего типа объектов - [OT_ChildrenType]
  /// </summary>
  [NonSerialized]
  public const string xmlChildrenTypeNode = "OT_ChildrenType";
  /// <summary>Строка для генерации названия фильтра - "Фильтр {0}"</summary>
  [NonSerialized]
  protected static string NameFormat = LocalizationHolder.rm.GetString("Client.Core_311");
  /// <summary>Счётчик номеров в именах фильтров</summary>
  [NonSerialized]
  protected static long _counter = 1;
  /// <summary>Текущий пользователь и роль</summary>
  [NonSerialized]
  protected static ICurrentUserAndRole _userRole;
  /// <summary>
  /// Название фильтра составов по типам родительских и дочерних типов объектов
  /// </summary>
  protected string _name;
  /// <summary>
  /// Уникальный глобальный идентификатор фильтра составов по типам родительских и дочерних типов объектов
  /// </summary>
  protected Guid _guid = Guid.NewGuid();
  /// <summary>
  /// Список уникальных глобальных идентификаторов родительских типов объектов, составы которых фильтруются
  /// </summary>
  protected List<Guid> _parentObjectTypes = new List<Guid>();
  /// <summary>
  /// Словарь всех допустимых дочерних типов (верхнего уровня), которые не должны отображаться
  /// </summary>
  protected Dictionary<Guid, List<Guid>> _childObjectTypes = new Dictionary<Guid, List<Guid>>();

  /// <summary>Текущий пользователь и роль</summary>
  protected static ICurrentUserAndRole UserRole
  {
    get
    {
      if (CompositionByObjectTypesFilter._userRole == null)
        CompositionByObjectTypesFilter._userRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      return CompositionByObjectTypesFilter._userRole;
    }
  }

  /// <summary>Создать экземпляр класса</summary>
  public CompositionByObjectTypesFilter()
  {
  }

  /// <summary>Создать экземпляр класса с указанными именем и Guid</summary>
  /// <param name="name">Название фильтра</param>
  /// <param name="guid">Guid фильтра</param>
  public CompositionByObjectTypesFilter(string name, Guid guid)
  {
    this.Name = name;
    this.GUID = guid;
  }

  /// <summary>
  /// Создать экземпляр класса на основе указанного интерфейса
  /// </summary>
  public CompositionByObjectTypesFilter(ICompositionByObjectTypesFilter source)
  {
    this.Assign(source);
  }

  /// <summary>
  /// Выполнить синхронизацию внутренних коллекций с кэшем метаданных
  /// </summary>
  public void SyncMetaData()
  {
    for (int index = this._parentObjectTypes.Count - 1; index >= 0; --index)
    {
      Guid parentObjectType = this._parentObjectTypes[index];
      if (MetaDataHelper.GetObjectType(parentObjectType) == null)
      {
        if (this._childObjectTypes.ContainsKey(parentObjectType))
          this._childObjectTypes.Remove(parentObjectType);
        this._parentObjectTypes.RemoveAt(index);
      }
    }
    foreach (KeyValuePair<Guid, List<Guid>> childObjectType in this.ChildObjectTypes)
    {
      Guid key = childObjectType.Key;
      int objectTypeId = MetaDataHelper.GetObjectTypeID(key);
      List<int> visibleRelations = CompositionByObjectTypesFilter.UserRole.Rule.GetObjectTypeVisibleRelations(key, true);
      if (visibleRelations.Count == 0)
      {
        childObjectType.Value.Clear();
      }
      else
      {
        List<Guid> childObjectTypesGuid = MetaDataHelper.GetApplicabilityChildObjectTypesGuid(objectTypeId, (IEnumerable<int>) visibleRelations);
        if (childObjectTypesGuid.Count == 0)
        {
          childObjectType.Value.Clear();
        }
        else
        {
          for (int index = childObjectType.Value.Count - 1; index >= 0; --index)
          {
            if (!childObjectTypesGuid.Contains(childObjectType.Value[index]))
              childObjectType.Value.RemoveAt(index);
          }
        }
      }
    }
  }

  /// <summary>Загрузить фильтр из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="filterNode">Узел с настройками фильтра</param>
  public void LoadFilter(XMLSettingsStorage xmlStorage, XmlNode filterNode)
  {
    this.Clear();
    if (xmlStorage == null || filterNode == null || filterNode.Name != "OT_Filter")
      return;
    string attributeValue1 = xmlStorage.GetAttributeValue(filterNode, "guid", string.Empty);
    if (attributeValue1 == string.Empty)
      return;
    Guid empty1 = Guid.Empty;
    Guid guid1;
    try
    {
      guid1 = new Guid(attributeValue1);
    }
    catch
    {
      return;
    }
    this._guid = guid1;
    this._name = xmlStorage.GetAttributeValue(filterNode, "name", this._name);
    for (int i1 = 0; i1 < filterNode.ChildNodes.Count; ++i1)
    {
      XmlNode childNode1 = filterNode.ChildNodes[i1];
      if (!(childNode1.Name != "OT_ParentType"))
      {
        string attributeValue2 = xmlStorage.GetAttributeValue(childNode1, "guid", string.Empty);
        if (!(attributeValue2 == string.Empty))
        {
          Guid empty2 = Guid.Empty;
          Guid key;
          try
          {
            key = new Guid(attributeValue2);
          }
          catch
          {
            continue;
          }
          if (!this._parentObjectTypes.Contains(key))
            this._parentObjectTypes.Add(key);
          if (!this._childObjectTypes.ContainsKey(key))
            this._childObjectTypes.Add(key, new List<Guid>());
          List<Guid> childObjectType = this._childObjectTypes[key];
          for (int i2 = 0; i2 < childNode1.ChildNodes.Count; ++i2)
          {
            XmlNode childNode2 = childNode1.ChildNodes[i2];
            if (!(childNode2.Name != "OT_ChildrenType"))
            {
              string attributeValue3 = xmlStorage.GetAttributeValue(childNode2, "guid", string.Empty);
              if (!(attributeValue3 == string.Empty))
              {
                Guid empty3 = Guid.Empty;
                Guid guid2;
                try
                {
                  guid2 = new Guid(attributeValue3);
                }
                catch
                {
                  continue;
                }
                if (!childObjectType.Contains(guid2))
                  childObjectType.Add(guid2);
              }
            }
          }
        }
      }
    }
    this.SyncMetaData();
  }

  /// <summary>Сохранить фильтр в указанные настройки</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="filtersNode">Родительский узел или null</param>
  public void SaveFilter(XMLSettingsStorage xmlStorage, XmlNode filtersNode)
  {
    if (xmlStorage == null)
      return;
    filtersNode = filtersNode == null ? (XmlNode) xmlStorage.document.DocumentElement : filtersNode;
    XmlNode nodeWithAttr1 = xmlStorage.FindNodeWithAttr(filtersNode, "OT_Filter", "guid", this._guid.ToString(), true);
    filtersNode.RemoveChild(nodeWithAttr1);
    XmlNode nodeWithAttr2 = xmlStorage.FindNodeWithAttr(filtersNode, "OT_Filter", "guid", this._guid.ToString(), true);
    xmlStorage.SetAttributeValue(nodeWithAttr2, "guid", this._guid.ToString());
    xmlStorage.SetAttributeValue(nodeWithAttr2, "name", this._name);
    foreach (KeyValuePair<Guid, List<Guid>> childObjectType in this._childObjectTypes)
    {
      XMLSettingsStorage xmlSettingsStorage1 = xmlStorage;
      XmlNode parentNode1 = nodeWithAttr2;
      Guid key = childObjectType.Key;
      string attrValue1 = key.ToString();
      XmlNode nodeWithAttr3 = xmlSettingsStorage1.FindNodeWithAttr(parentNode1, "OT_ParentType", "guid", attrValue1, true);
      XMLSettingsStorage xmlSettingsStorage2 = xmlStorage;
      XmlNode node1 = nodeWithAttr3;
      key = childObjectType.Key;
      string str1 = key.ToString();
      xmlSettingsStorage2.SetAttributeValue(node1, "guid", str1);
      for (int index = 0; index < childObjectType.Value.Count; ++index)
      {
        XMLSettingsStorage xmlSettingsStorage3 = xmlStorage;
        XmlNode parentNode2 = nodeWithAttr3;
        key = childObjectType.Value[index];
        string attrValue2 = key.ToString();
        XmlNode nodeWithAttr4 = xmlSettingsStorage3.FindNodeWithAttr(parentNode2, "OT_ChildrenType", "guid", attrValue2, true);
        XMLSettingsStorage xmlSettingsStorage4 = xmlStorage;
        XmlNode node2 = nodeWithAttr4;
        key = childObjectType.Value[index];
        string str2 = key.ToString();
        xmlSettingsStorage4.SetAttributeValue(node2, "guid", str2);
      }
    }
  }

  /// <summary>Название фильтра</summary>
  public virtual string Name
  {
    get
    {
      if (this._name == null)
      {
        this._name = string.Format(CompositionByObjectTypesFilter.NameFormat, (object) CompositionByObjectTypesFilter._counter);
        ++CompositionByObjectTypesFilter._counter;
      }
      return this._name;
    }
    set => this._name = value;
  }

  /// <summary>Идентификатор фильтра</summary>
  public Guid GUID
  {
    get => this._guid;
    set => this._guid = !value.Equals(Guid.Empty) ? value : Guid.NewGuid();
  }

  /// <summary>Количество родительских типов в коллекции</summary>
  public int ParentTypesCount => this._parentObjectTypes.Count;

  /// <summary>
  /// Список уникальных глобальных идентификаторов родительских типов объектов, составы которых фильтруются
  /// (возвращается КОПИЯ внутренней коллекции)
  /// </summary>
  public List<Guid> ParentObjectTypes
  {
    get => new List<Guid>((IEnumerable<Guid>) this._parentObjectTypes);
  }

  /// <summary>
  /// Словарь всех допустимых дочерних типов (верхнего уровня), которые не должны отображаться
  /// (возвращается КОПИЯ внутренней коллекции)
  /// </summary>
  public Dictionary<Guid, List<Guid>> ChildObjectTypes
  {
    get => new Dictionary<Guid, List<Guid>>((IDictionary<Guid, List<Guid>>) this._childObjectTypes);
  }

  /// <summary>Полностью очистить содержимое фильтра</summary>
  public void Clear()
  {
    this._childObjectTypes.Clear();
    this._parentObjectTypes.Clear();
  }

  /// <summary>Скопировать содержимое указанного фильтра в свои поля</summary>
  /// <param name="source">Фильтр-источник</param>
  public void Assign(ICompositionByObjectTypesFilter source)
  {
    this.Clear();
    if (source == null)
      return;
    this._guid = source.GUID;
    this._name = source.Name;
    this._parentObjectTypes = new List<Guid>((IEnumerable<Guid>) source.ParentObjectTypes);
    this._childObjectTypes = new Dictionary<Guid, List<Guid>>();
    foreach (KeyValuePair<Guid, List<Guid>> childObjectType in source.ChildObjectTypes)
      this._childObjectTypes.Add(childObjectType.Key, new List<Guid>((IEnumerable<Guid>) childObjectType.Value));
  }

  /// <summary>Добавить указанный родительский тип в фильтр</summary>
  /// <param name="parentType">Guid родительского типа объекта</param>
  /// <returns>true, если тип был успешно добавлен</returns>
  public bool Add(Guid parentType)
  {
    if (this._parentObjectTypes.Contains(parentType))
      return false;
    this._parentObjectTypes.Add(parentType);
    this._childObjectTypes.Add(parentType, new List<Guid>());
    return true;
  }

  /// <summary>Добавить скрытый дочерний тип объектов в фильтр</summary>
  /// <param name="parentType">Guid родительского типа объекта</param>
  /// <param name="childrenType">Скрываемый дочерний тип объектов</param>
  /// <returns>true, если тип был успешно добавлен</returns>
  public bool Add(Guid parentType, Guid childrenType)
  {
    if (this.Exists(parentType, childrenType))
      return false;
    this.Add(parentType);
    this._childObjectTypes[parentType].Add(childrenType);
    return true;
  }

  /// <summary>Удалить указанный родительский тип из фильтра</summary>
  /// <param name="parentType">Guid удаляемого родительского типа объекта</param>
  /// <returns>true, если тип был успешно удалён</returns>
  public bool Remove(Guid parentType)
  {
    if (!this._parentObjectTypes.Contains(parentType))
      return false;
    this._parentObjectTypes.Remove(parentType);
    this._childObjectTypes.Remove(parentType);
    return true;
  }

  /// <summary>
  /// Удалить указанный скрываемый дочерний тип объекта из фильтра
  /// </summary>
  /// <param name="parentType">Guid родительского типа объекта</param>
  /// <param name="childrenType">Guid удаляемого дочернего типа объекта</param>
  /// <returns>true, если тип был успешно удалён</returns>
  public bool Remove(Guid parentType, Guid childrenType)
  {
    if (!this.Exists(parentType, childrenType))
      return false;
    this._childObjectTypes[parentType].Remove(childrenType);
    return true;
  }

  /// <summary>
  /// Проверить наличие указанного родительского типа в коллекции
  /// </summary>
  /// <param name="parentType">Guid искомого родительского типа объекта</param>
  /// <returns>true, если указанный родительский тип найден в коллекции</returns>
  public bool Exists(Guid parentType) => this._parentObjectTypes.Contains(parentType);

  /// <summary>
  /// Проверить наличие указанного скрытого дочернего типа объекта у родительского типа объекта
  /// </summary>
  /// <param name="parentType">Guid родительского типа объекта</param>
  /// <param name="childrenType">Guid искомого скрытого дочернего типа объекта</param>
  /// <returns>true, если указанный скрытый дочерний тип найден в коллекции</returns>
  public bool Exists(Guid parentType, Guid childrenType)
  {
    return this._parentObjectTypes.Contains(parentType) && this._childObjectTypes[parentType].Contains(childrenType);
  }

  /// <summary>
  /// Получить индекс указанного родительского типа в коллекции
  /// </summary>
  /// <param name="parentType">Guid искомого родительского типа объекта</param>
  /// <returns>-1, если указанный родительский тип не найден в коллекции</returns>
  public int IndexOf(Guid parentType) => this._parentObjectTypes.IndexOf(parentType);

  /// <summary>Получить индекс указанного дочернего типа в коллекции</summary>
  /// <param name="parentType">Guid родительского типа объекта</param>
  /// <param name="childrenType">Guid искомого дочернего типа объекта</param>
  /// <returns>-1, если указанный дочерний тип не найден в коллекции</returns>
  public int IndexOf(Guid parentType, Guid childrenType)
  {
    return !this._parentObjectTypes.Contains(parentType) ? -1 : this._childObjectTypes[parentType].IndexOf(childrenType);
  }

  /// <summary>Обменять местами указанные родительские типы объектов</summary>
  /// <param name="idx1">Индекс первого родительского типа объектов</param>
  /// <param name="idx2">Индекс второго родительского типа объектов</param>
  /// <returns>true, если обмен успешно выполнен</returns>
  public bool Swap(int idx1, int idx2)
  {
    int count = this._parentObjectTypes.Count;
    if (idx1 == idx2 || idx1 < 0 || idx2 < 0 || idx1 >= count || idx2 >= count)
      return false;
    Guid parentObjectType1 = this._parentObjectTypes[idx1];
    Guid parentObjectType2 = this._parentObjectTypes[idx2];
    this._parentObjectTypes[idx2] = parentObjectType1;
    this._parentObjectTypes[idx1] = parentObjectType2;
    return true;
  }

  /// <summary>Обменять местами указанные дочерние типы объектов</summary>
  /// <param name="parentType">Guid родительского типа объектов</param>
  /// <param name="idx1">Индекс первого дочернего типа объектов</param>
  /// <param name="idx2">Индекс второго дочернего типа объектов</param>
  /// <returns>true, если обмен успешно выполнен</returns>
  public bool Swap(Guid parentType, int idx1, int idx2)
  {
    if (!this._parentObjectTypes.Contains(parentType))
      return false;
    List<Guid> childObjectType = this._childObjectTypes[parentType];
    int count = childObjectType.Count;
    if (idx1 == idx2 || idx1 < 0 || idx2 < 0 || idx1 >= count || idx2 >= count)
      return false;
    Guid guid1 = childObjectType[idx1];
    Guid guid2 = childObjectType[idx2];
    childObjectType[idx2] = guid1;
    childObjectType[idx1] = guid2;
    return true;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone()
  {
    return (object) new CompositionByObjectTypesFilter((ICompositionByObjectTypesFilter) this);
  }
}
