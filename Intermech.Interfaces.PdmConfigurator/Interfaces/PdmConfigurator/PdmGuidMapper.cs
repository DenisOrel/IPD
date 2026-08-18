// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmGuidMapper
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Вспомогательный класс, позволяющий хранить коллекцию Guid и соответствующие им идентификаторы типа Int64
/// </summary>
[Serializable]
public sealed class PdmGuidMapper : IAssignable, ICloneable, IXMLStorageLoadSave
{
  /// <summary>Уникальный счётчик для коллекции</summary>
  private long _counter;
  /// <summary>Словарик для хранения пар [Guid] =&gt; [Int64]</summary>
  private Dictionary<Guid, long> _guidToInt = new Dictionary<Guid, long>();
  /// <summary>Словарик для хранения пар [Int64] =&gt; [Guid]</summary>
  private Dictionary<long, Guid> _IntToGuid = new Dictionary<long, Guid>();

  /// <summary>Сгенерировать следующий уникальный идентификатор</summary>
  public long NextID
  {
    get
    {
      lock (this)
      {
        ++this._counter;
        return this._counter;
      }
    }
  }

  /// <summary>Создать пустой экземпляр класса</summary>
  public PdmGuidMapper()
  {
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public PdmGuidMapper(object source) => this.Assign(source);

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    lock (this)
    {
      this._counter = 0L;
      this._guidToInt.Clear();
      this._IntToGuid.Clear();
    }
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    switch (source)
    {
      case PdmGuidMapper pdmGuidMapper:
        this._counter = pdmGuidMapper._counter;
        this._guidToInt = new Dictionary<Guid, long>((IDictionary<Guid, long>) pdmGuidMapper._guidToInt);
        this._IntToGuid = new Dictionary<long, Guid>((IDictionary<long, Guid>) pdmGuidMapper._IntToGuid);
        break;
      case XMLSettingsStorage xmlStorage:
        this.Load(xmlStorage, (XmlNode) xmlStorage.document.DocumentElement);
        break;
    }
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new PdmGuidMapper((object) this);

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    XmlNode node1 = xmlStorage.FindNode(node, "a", false);
    if (node1 == null)
      return;
    this._counter = StringsHelper.HexToInt64(xmlStorage.GetAttributeValue(node1, "a", "0"));
    for (int i = 0; i < node1.ChildNodes.Count; ++i)
    {
      XmlNode childNode = node1.ChildNodes[i];
      if (!(childNode.Name != "b"))
      {
        string attributeValue = xmlStorage.GetAttributeValue(childNode, "c", string.Empty);
        if (!string.IsNullOrEmpty(attributeValue))
        {
          string str1 = attributeValue.Substring(attributeValue.LastIndexOf("-") + 1);
          if (!string.IsNullOrEmpty(str1))
          {
            string str2 = attributeValue.Substring(0, attributeValue.LastIndexOf("-"));
            if (!string.IsNullOrEmpty(str2) && GuidHelper.IsGuid(str2))
              this[new Guid(str2)] = StringsHelper.HexToInt64(str1);
          }
        }
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
    if (this._guidToInt.Count == 0)
      return;
    XmlNode xmlNode = xmlStorage.AddNode(parentNode, "a");
    xmlStorage.SetAttributeValue(xmlNode, "a", StringsHelper.IntToHex(this._counter));
    foreach (KeyValuePair<long, Guid> keyValuePair in this._IntToGuid)
    {
      XmlNode node = xmlStorage.AddNode(xmlNode, "b");
      xmlStorage.SetAttributeValue(node, "c", $"{keyValuePair.Value.ToString()}-{StringsHelper.IntToHex(keyValuePair.Key)}");
    }
  }

  /// <summary>Зарегистрировать Guid</summary>
  /// <param name="guid">Guid</param>
  /// <returns>Зарегистрированное значение Int64</returns>
  public long Register(Guid guid)
  {
    if (this._guidToInt.ContainsKey(guid))
      return this._guidToInt[guid];
    if (guid.Equals(Guid.Empty))
      return 0;
    long nextId = this.NextID;
    lock (this)
    {
      this._guidToInt[guid] = nextId;
      this._IntToGuid[nextId] = guid;
    }
    return nextId;
  }

  /// <summary>Удалить регистрацию Guid</summary>
  /// <param name="guid">Guid</param>
  public void Unregister(Guid guid)
  {
    if (!this._guidToInt.ContainsKey(guid))
      return;
    lock (this)
    {
      long key = this._guidToInt[guid];
      this._guidToInt.Remove(guid);
      this._IntToGuid.Remove(key);
    }
  }

  /// <summary>Зарегистрировать/считать значение Guid</summary>
  /// <param name="guid">Guid</param>
  /// <returns>Зарегистрированное значение Int64</returns>
  public long this[Guid guid]
  {
    get => this.Register(guid);
    set
    {
      this.Unregister(guid);
      if (value == 0L || guid.Equals(Guid.Empty))
        return;
      this._guidToInt[guid] = value;
      this._IntToGuid[value] = guid;
    }
  }

  /// <summary>Получить значение Guid по его идентификатору</summary>
  /// <param name="id">Идентификатор</param>
  /// <returns>Зарегистрированное значение Guid</returns>
  public Guid this[long id] => !this._IntToGuid.ContainsKey(id) ? Guid.Empty : this._IntToGuid[id];
}
