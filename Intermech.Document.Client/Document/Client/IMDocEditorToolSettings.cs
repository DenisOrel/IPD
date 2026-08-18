// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.IMDocEditorToolSettings
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>
/// Объект для обмена общими настройками инструмента "Редактор документов" с сервером приложений
/// </summary>
internal sealed class IMDocEditorToolSettings : 
  IAssignable,
  IMetaDataSync,
  IDatabaseSync,
  IResetToDefaults,
  IXMLStorageLoadSave,
  ICloneable
{
  /// <summary>
  /// Экземпляр класса, позволяющего сортировать список типов объектов
  /// </summary>
  [NonSerialized]
  private IMDocEditorToolSettings.ObjTypesComparer _comparer = new IMDocEditorToolSettings.ObjTypesComparer();
  /// <summary>
  /// Словарь поддерживаемых типов объектов
  /// [(Int32)ID типа объекта] =&gt; [(IMDocObjectTypeSettings)Описание типа объекта, настройки для типа]
  /// </summary>
  private Dictionary<int, IMDocObjectTypeSettings> supportedTypes;
  /// <summary>
  /// Список идентификаторов поддерживаемых типов (коллекция управляется синхронно со словариком)
  /// </summary>
  private List<int> supportedTypeIDs;
  private static Dictionary<Guid, Guid> _defaultVedomostTemplateDictionary;

  /// <summary>
  /// Создать экземпляр класса с настройками для инструмента "Редактор документов"
  /// </summary>
  public IMDocEditorToolSettings()
  {
    this.supportedTypes = new Dictionary<int, IMDocObjectTypeSettings>();
    this.supportedTypeIDs = new List<int>();
  }

  /// <summary>
  /// Вернуть/задать(добавить) настройки для указанного типа объекта. Null - удалить описание типа из коллекции
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Настройки для указанного типа объекта или null</returns>
  public IMDocObjectTypeSettings this[int objTypeID]
  {
    get
    {
      return !this.supportedTypes.ContainsKey(objTypeID) ? (IMDocObjectTypeSettings) null : this.supportedTypes[objTypeID];
    }
    set
    {
      if (this.supportedTypes.ContainsKey(objTypeID))
      {
        if (value != null)
        {
          this.supportedTypes[objTypeID].Assign((object) value);
        }
        else
        {
          this.supportedTypes.Remove(objTypeID);
          this.supportedTypeIDs.Remove(objTypeID);
        }
      }
      else
      {
        this.supportedTypes.Add(objTypeID, value);
        this.supportedTypeIDs.Add(objTypeID);
        this.supportedTypeIDs.Sort((IComparer<int>) this.Comparer);
      }
    }
  }

  /// <summary>Количество типов объектов в коллекции</summary>
  public int Count
  {
    [DebuggerStepThrough] get => this.supportedTypes.Count;
  }

  /// <summary>
  /// Словарь поддерживаемых типов объектов
  /// [(Int32)ID типа объекта] =&gt; [(IMDocObjectTypeSettings)Описание типа объекта, настройки для типа]
  /// </summary>
  public Dictionary<int, IMDocObjectTypeSettings> SupportedTypes
  {
    [DebuggerStepThrough] get => this.supportedTypes;
  }

  /// <summary>
  /// Список идентификаторов поддерживаемых типов (коллекция управляется синхронно со словариком)
  /// </summary>
  public List<int> SupportedTypeIDs
  {
    [DebuggerStepThrough] get => this.supportedTypeIDs;
  }

  /// <summary>
  /// Экземпляр вспомогательного класса для сортировки типов объектов по их названиям
  /// </summary>
  private IMDocEditorToolSettings.ObjTypesComparer Comparer
  {
    get
    {
      if (this._comparer == null)
        this._comparer = new IMDocEditorToolSettings.ObjTypesComparer();
      return this._comparer;
    }
  }

  /// <summary>Добавить указанный тип объекта в коллекцию настроек</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="templateGuid">Guid объекта-шаблона, либо Guid.Empty, если шаблон не требуется</param>
  /// <returns>Описание типа объекта</returns>
  public IMDocObjectTypeSettings Add(int objTypeID, Guid templateGuid)
  {
    if (this.supportedTypes.ContainsKey(objTypeID) && templateGuid != Guid.Empty)
      this[objTypeID].TemplateGuid = templateGuid;
    else
      this[objTypeID] = new IMDocObjectTypeSettings(objTypeID, templateGuid);
    return this[objTypeID];
  }

  /// <summary>Удалить указанное описание типа объекта из коллекции</summary>
  /// <param name="objTypeID">Тип объекта, описание которого надо удалить из коллекции</param>
  public void Remove(int objTypeID) => this[objTypeID] = (IMDocObjectTypeSettings) null;

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this.supportedTypes.Clear();
    this.supportedTypeIDs.Clear();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is IMDocEditorToolSettings editorToolSettings))
      return;
    foreach (KeyValuePair<int, IMDocObjectTypeSettings> supportedType in editorToolSettings.supportedTypes)
    {
      this.supportedTypes.Add(supportedType.Key, supportedType.Value.Clone() as IMDocObjectTypeSettings);
      this.supportedTypeIDs.Add(supportedType.Key);
    }
    this.supportedTypeIDs.Sort((IComparer<int>) this.Comparer);
  }

  /// <summary>
  /// Выполнить синхронизацию внутренних коллекций с кэшем метаданных
  /// </summary>
  public void SyncMetaData()
  {
    List<int> intList = new List<int>();
    foreach (KeyValuePair<int, IMDocObjectTypeSettings> supportedType in this.supportedTypes)
    {
      supportedType.Value.SyncMetaData();
      if (supportedType.Value.ObjectType == -1)
        intList.Add(supportedType.Key);
    }
    for (int index = 0; index < intList.Count; ++index)
    {
      this.supportedTypes.Remove(intList[index]);
      this.supportedTypeIDs.Remove(intList[index]);
    }
    this.supportedTypeIDs.Sort((IComparer<int>) this.Comparer);
  }

  /// <summary>
  /// Выполнить синхронизацию внутренних коллекций с базой данных
  /// </summary>
  /// <param name="session">Ссылка на сессию, в рамках которой выполняется работа с базой данных и сервером приложений</param>
  public void SyncObjectsData(IUserSession session)
  {
    if (session == null)
      return;
    foreach (KeyValuePair<int, IMDocObjectTypeSettings> supportedType in this.supportedTypes)
      supportedType.Value.SyncObjectsData(session);
  }

  /// <summary>
  /// Выполнить сброс значений полей класса, реализующего данный интерфейс, на умалчиваемые значения
  /// </summary>
  /// <param name="session">Ссылка на сессию, в рамках которой выполняется работа с базой данных и сервером приложений</param>
  public void ResetToDefaults(IUserSession session)
  {
    Dictionary<int, IMDocObjectTypeSettings> supportedTypes = this.supportedTypes;
    this.supportedTypes = new Dictionary<int, IMDocObjectTypeSettings>();
    this.Clear();
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00196-306c-11d8-b4e9-00304f19f545"));
    for (int index = 0; index < childrenIdRecursive.Count; ++index)
    {
      if (supportedTypes.ContainsKey(childrenIdRecursive[index]))
      {
        this[childrenIdRecursive[index]] = supportedTypes[childrenIdRecursive[index]];
      }
      else
      {
        Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(childrenIdRecursive[index]);
        this.Add(childrenIdRecursive[index], IMDocEditorToolSettings.GetDefaultVedomostTemplateGuid(objectTypeGuid));
      }
    }
  }

  private static Dictionary<Guid, Guid> CreateDefaultVedomostTemplateDictionary()
  {
    return new Dictionary<Guid, Guid>()
    {
      {
        new Guid("cad0082b-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd98bb-306c-11d8-b4e9-00304f19f545")
      },
      {
        new Guid("cad00826-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd98b3-306c-11d8-b4e9-00304f19f545")
      },
      {
        new Guid("cadd93cc-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd98b7-306c-11d8-b4e9-00304f19f545")
      },
      {
        new Guid("cad0029d-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd99ba-306c-11d8-b4e9-00304f19f545")
      },
      {
        new Guid("cadd9a20-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd9a23-306c-11d8-b4e9-00304f19f545")
      },
      {
        new Guid("cadd99bd-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd99bf-306c-11d8-b4e9-00304f19f545")
      },
      {
        new Guid("cad00295-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd99bc-306c-11d8-b4e9-00304f19f545")
      },
      {
        new Guid("cadd9a21-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd9a25-306c-11d8-b4e9-00304f19f545")
      },
      {
        new Guid("cadd9a4a-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd9a4c-306c-11d8-b4e9-00304f19f545")
      },
      {
        new Guid("cadd9a92-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd9a94-306c-11d8-b4e9-00304f19f545")
      }
    };
  }

  public static Dictionary<Guid, Guid> DefaultVedomostTemplateDictionary
  {
    get
    {
      if (IMDocEditorToolSettings._defaultVedomostTemplateDictionary == null)
        IMDocEditorToolSettings._defaultVedomostTemplateDictionary = IMDocEditorToolSettings.CreateDefaultVedomostTemplateDictionary();
      return IMDocEditorToolSettings._defaultVedomostTemplateDictionary;
    }
  }

  public static Guid GetDefaultVedomostTemplateGuid(Guid vedomostType)
  {
    Guid guid;
    return IMDocEditorToolSettings.DefaultVedomostTemplateDictionary.TryGetValue(vedomostType, out guid) ? guid : Guid.Empty;
  }

  /// <summary>Загрузить информацию из потока</summary>
  /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
  /// <param name="stream">Поток, содержащий XML-документ</param>
  /// <param name="throwException">Генерировать исключение, если возникнут проблемы при загрузке информации</param>
  public void LoadFromStream(IUserSession session, Stream stream, bool throwException)
  {
    try
    {
      if (stream == null || stream.Length <= 0L)
        return;
      XmlDocument xmlDocument = new XmlDocument();
      stream.Position = 0L;
      XMLSettingsStorage xmlStorage = new XMLSettingsStorage(stream);
      XmlNode node = (XmlNode) null;
      if (xmlStorage.document != null)
        node = xmlStorage.FindNode((XmlNode) xmlStorage.document.DocumentElement, "IMDocObjectTypeSettings", false) ?? xmlStorage.FindNode((XmlNode) xmlStorage.document.DocumentElement, "AVSObjectTypeSettings", false);
      this.Load(xmlStorage, node);
      if (this.supportedTypes.Count == 0)
        this.ResetToDefaults(session);
      this.SyncObjectsData(session);
    }
    catch
    {
      if (!throwException)
        return;
      throw;
    }
  }

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (xmlStorage == null || node.Name != "IMDocObjectTypeSettings")
      return;
    for (int i = 0; i < node.ChildNodes.Count; ++i)
    {
      XmlNode childNode = node.ChildNodes[i];
      if (!(childNode.Name != "objtype"))
      {
        IMDocObjectTypeSettings objectTypeSettings = new IMDocObjectTypeSettings();
        objectTypeSettings.Load(xmlStorage, childNode);
        if (objectTypeSettings.ObjectType != -1)
        {
          this.supportedTypes.Add(objectTypeSettings.ObjectType, objectTypeSettings);
          this.supportedTypeIDs.Add(objectTypeSettings.ObjectType);
        }
      }
    }
    this.SyncMetaData();
  }

  /// <summary>Сохранить информацию в поток</summary>
  /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
  /// <param name="stream">Поток, содержащий XML-документ</param>
  /// <param name="throwException">Генерировать исключение, если возникнут проблемы при сохранении информации</param>
  public void SaveToStream(IUserSession session, Stream stream, bool throwException)
  {
    try
    {
      if (stream == null)
        return;
      XMLSettingsStorage xmlStorage = new XMLSettingsStorage();
      this.Save(xmlStorage, (XmlNode) xmlStorage.document.DocumentElement);
      stream.Position = 0L;
      xmlStorage.Save(stream);
    }
    catch
    {
      if (!throwException)
        return;
      throw;
    }
  }

  /// <summary>
  /// Сохранить данные в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    this.SyncMetaData();
    if (xmlStorage == null || parentNode == null)
      return;
    XmlNode node1 = xmlStorage.FindNode(parentNode, "IMDocObjectTypeSettings", true);
    parentNode.RemoveChild(node1);
    XmlNode node2 = xmlStorage.FindNode(parentNode, "IMDocObjectTypeSettings", true);
    foreach (KeyValuePair<int, IMDocObjectTypeSettings> supportedType in this.supportedTypes)
      supportedType.Value.Save(xmlStorage, node2);
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone()
  {
    IMDocEditorToolSettings editorToolSettings = new IMDocEditorToolSettings();
    editorToolSettings.Assign((object) this);
    return (object) editorToolSettings;
  }

  /// <summary>Сравнение двух типов объектов по их названию</summary>
  private class ObjTypesComparer : IComparer<int>
  {
    /// <summary>Сравнить названия двух типов объектов</summary>
    /// <param name="x">Идентификатор первого типа объекта</param>
    /// <param name="y">Идентификатор второго типа объекта</param>
    /// <returns>-1, 0, 1</returns>
    public int Compare(int x, int y)
    {
      return MetaDataHelper.GetObjectTypeName(x).CompareTo(MetaDataHelper.GetObjectTypeName(y));
    }
  }
}
