// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.DocumentTypeWeightCollection
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Коллекция описаний типов объектов-документов и их "веса"</summary>
[Serializable]
public class DocumentTypeWeightCollection : 
  List<DocumentTypeWeight>,
  ICloneable,
  IMetaDataSync,
  IXMLStorageLoadSave,
  IDocumentTypeWeight
{
  /// <summary>Описение типа объекта-документа, которому принадлежит коллекция (она равна owner.Items).
  /// Если значение равно null, коллекция является корневой коллекцией во всей иерархии
  /// описаний типов объектов-документов
  /// </summary>
  protected DocumentTypeWeight owner;

  /// <summary>Создать пустую коллекцию</summary>
  public DocumentTypeWeightCollection()
  {
  }

  /// <summary>Создать пустую коллекцию, связать с указанным описанием типа объекта-документа</summary>
  /// <param name="owner">Описание типа объекта-документа, которому принадлежит коллекция</param>
  public DocumentTypeWeightCollection(DocumentTypeWeight owner) => this.owner = owner;

  /// <summary>Создать экземпляр класса на основе указанной коллекции. Ссылка на владельца не копируется</summary>
  /// <param name="source">Коллекция-источник</param>
  public DocumentTypeWeightCollection(DocumentTypeWeightCollection source) => this.Assign(source);

  /// <summary>Описение типа объекта-документа, которому принадлежит коллекция (она равна owner.Items).
  /// Если значение равно null, коллекция является корневой коллекцией во всей иерархии
  /// описаний типов объектов-документов
  /// </summary>
  public DocumentTypeWeight Owner
  {
    [DebuggerStepThrough] get => this.owner;
    [DebuggerStepThrough] set => this.owner = value;
  }

  /// <summary>Скопировать содержимое коллекции в свои поля</summary>
  /// <param name="source">Коллекция-источник</param>
  public void Assign(DocumentTypeWeightCollection source)
  {
    this.Clear();
    if (source == null || source.Count == 0)
      return;
    for (int index = 0; index < source.Count; ++index)
      this.Add(new DocumentTypeWeight(source[index]));
  }

  /// <summary>Обменять местами и "весами" два указанных элемента в коллекции</summary>
  /// <param name="index1">Первый элемент коллекции</param>
  /// <param name="index2">Второй элемент коллекции</param>
  public virtual void Swap(int index1, int index2)
  {
    DocumentTypeWeight documentTypeWeight1 = this[index1];
    DocumentTypeWeight documentTypeWeight2 = this[index2];
    this[index1] = documentTypeWeight2;
    this[index2] = documentTypeWeight1;
    long weight = documentTypeWeight1.Weight;
    documentTypeWeight1.Weight = documentTypeWeight2.Weight;
    documentTypeWeight2.Weight = weight;
  }

  /// <summary>Передвинуть элемент коллекции с указанным индексом на delta позиций в списке</summary>
  /// <param name="index">Индекс передвигаемого элемента коллекции</param>
  /// <param name="delta">На сколько позиций в списке передвинуть указанный элемент</param>
  public virtual void Shift(int index, int delta)
  {
    if (delta == 0)
      return;
    DocumentTypeWeight documentTypeWeight = this[index];
    if (delta > 0)
    {
      int num = this.Count - index - 1;
      delta = delta > num ? num : delta;
      for (int index1 = index; index1 < index + delta; ++index1)
        this.Swap(index1, index1 + 1);
    }
    else
    {
      delta = Math.Abs(delta) > index ? -index : delta;
      for (int index1 = index; index1 > index + delta; --index1)
        this.Swap(index1, index1 - 1);
    }
  }

  /// <summary>Добавить элемент в коллекцию</summary>
  /// <param name="item">Добавляемый элемент</param>
  public new virtual void Add(DocumentTypeWeight item)
  {
    if (item == null)
      return;
    item.Owner = this;
    item.ParentType = this.Owner;
    base.Add(item);
  }

  /// <summary>Очистить список</summary>
  public new virtual void Clear()
  {
    for (int index = 0; index < this.Count; ++index)
      this[index].Owner = (DocumentTypeWeightCollection) null;
    base.Clear();
  }

  /// <summary>Вставить указанный элемент в коллекцию</summary>
  /// <param name="index">Индекс для вставки</param>
  /// <param name="item">Вставляемый элемент</param>
  public new virtual void Insert(int index, DocumentTypeWeight item)
  {
    if (item == null)
      return;
    item.Owner = this;
    item.ParentType = this.Owner;
    base.Insert(index, item);
  }

  /// <summary>Удалить указанный элемент из коллекции</summary>
  /// <param name="item">Удаляемый элемент</param>
  /// <returns>true, если элемент был удалён;</returns>
  public new virtual bool Remove(DocumentTypeWeight item)
  {
    if (item == null)
      return false;
    item.Owner = (DocumentTypeWeightCollection) null;
    item.ParentType = (DocumentTypeWeight) null;
    return base.Remove(item);
  }

  /// <summary>Удалить элемент с указанным индексом</summary>
  /// <param name="index">Индекс удаляемого элемента</param>
  public new virtual void RemoveAt(int index)
  {
    this[index].Owner = (DocumentTypeWeightCollection) null;
    base.RemoveAt(index);
  }

  /// <summary>Отыскать в коллекции описание указанного типа объекта-документа</summary>
  /// <param name="docTypeID">Идентификатор типа объекта-документа</param>
  /// <returns>Индекс найденного описания или -1</returns>
  public virtual int IndexOf(int docTypeID)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      if (this[index].DocumentTypeID == docTypeID)
        return index;
    }
    return -1;
  }

  /// <summary>Отыскать в коллекции описание указанного типа объекта-документа</summary>
  /// <param name="docTypeGuid">Guid типа объекта-документа</param>
  /// <returns>Индекс найденного описания или -1</returns>
  public virtual int IndexOf(Guid docTypeGuid)
  {
    return this.IndexOf(MetaDataHelper.GetObjectTypeID(docTypeGuid));
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new DocumentTypeWeightCollection(this);

  /// <summary>Выполнить синхронизацию внутренних коллекций с кэшем метаданных</summary>
  public virtual void SyncMetaData()
  {
    for (int index = this.Count - 1; index >= 0; --index)
    {
      this[index].SyncMetaData();
      if (this[index].DocumentTypeID == -1)
        this.RemoveAt(index);
    }
    if (this.owner == null || this.owner.DocumentTypeID == -1)
      return;
    List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(this.owner.DocumentTypeID);
    if (objectTypeChildrenId.Count == 0)
    {
      this.Clear();
    }
    else
    {
      List<int> intList = new List<int>();
      for (int index = this.Count - 1; index >= 0; --index)
      {
        DocumentTypeWeight documentTypeWeight = this[index];
        if (!objectTypeChildrenId.Contains(documentTypeWeight.DocumentTypeID))
          this.Remove(documentTypeWeight);
        else
          intList.Add(documentTypeWeight.DocumentTypeID);
      }
      intList.Sort();
      for (int index = 0; index < objectTypeChildrenId.Count; ++index)
      {
        if (!intList.Contains(objectTypeChildrenId[index]))
        {
          DocumentTypeWeight documentTypeWeight = new DocumentTypeWeight(objectTypeChildrenId[index]);
          documentTypeWeight.SyncMetaData();
          if (documentTypeWeight.DocumentTypeID != -1)
            this.Add(documentTypeWeight);
        }
      }
      this.Sort();
    }
  }

  /// <summary>Загрузить информацию из потока</summary>
  /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
  /// <param name="stream">Поток, содержащий XML-документ</param>
  /// <param name="throwException">Генерировать исключение, если возникнут проблемы при загрузке информации</param>
  public virtual void LoadFromStream(IUserSession session, Stream stream, bool throwException)
  {
    try
    {
      if (stream == null || stream.Length <= 0L)
        return;
      stream.Position = 0L;
      XMLSettingsStorage xmlStorage = new XMLSettingsStorage(stream);
      XmlNode node = xmlStorage.document != null ? xmlStorage.FindNode((XmlNode) xmlStorage.document.DocumentElement, "DocumentTypesWeights", false) : (XmlNode) null;
      this.Load(xmlStorage, node);
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
  public virtual void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (xmlStorage == null || node.Name != "DocumentTypesWeights")
      return;
    for (int i = 0; i < node.ChildNodes.Count; ++i)
    {
      XmlNode childNode = node.ChildNodes[i];
      if (!(childNode.Name != "doctype"))
      {
        DocumentTypeWeight documentTypeWeight = new DocumentTypeWeight();
        documentTypeWeight.Load(xmlStorage, childNode);
        if (documentTypeWeight.DocumentTypeID != -1)
          this.Add(documentTypeWeight);
      }
    }
    this.SyncMetaData();
  }

  /// <summary>Сохранить информацию в поток</summary>
  /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
  /// <param name="stream">Поток, содержащий XML-документ</param>
  /// <param name="throwException">Генерировать исключение, если возникнут проблемы при сохранении информации</param>
  public virtual void SaveToStream(IUserSession session, Stream stream, bool throwException)
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

  /// <summary>Сохранить данные в состав указанного родительского узла</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public virtual void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    this.SyncMetaData();
    if (xmlStorage == null || parentNode == null)
      return;
    XmlNode node1 = xmlStorage.FindNode(parentNode, "DocumentTypesWeights", true);
    parentNode.RemoveChild(node1);
    XmlNode node2 = xmlStorage.FindNode(parentNode, "DocumentTypesWeights", true);
    for (int index = 0; index < this.Count; ++index)
      this[index].Save(xmlStorage, node2);
  }

  /// <summary>
  /// Отыскать корневой тип объекта-документа для текущего узла
  /// </summary>
  public DocumentTypeWeight RootDocumentType
  {
    get
    {
      if (this.owner != null)
        return this.owner.RootDocumentType;
      return this.Count > 0 ? this[0].RootDocumentType : (DocumentTypeWeight) null;
    }
  }

  /// <summary>Отыскать корневую коллекцию</summary>
  public DocumentTypeWeightCollection RootCollection
  {
    get
    {
      if (this.owner == null)
        return this;
      DocumentTypeWeightCollection owner = this.owner.Owner;
      return owner == null ? this : owner.RootCollection;
    }
  }

  /// <summary>
  /// Получить значение "веса" указанного типа объекта-документа
  /// </summary>
  /// <param name="docTypeID">Идентификатор типа объекта-документа</param>
  /// <returns>Значение "веса" или DocumentTypeWeight.UndefinedWeight,
  /// если тип объекта не найден, либо значение "веса" неопределено</returns>
  public virtual long GetWeight(int docTypeID)
  {
    if (this.owner != null)
      return this.owner.GetWeight(docTypeID);
    DocumentTypeWeightCollection weightCollection = this.RootCollection ?? this;
    return weightCollection.Count == 0 ? DocumentTypeWeight.UndefinedWeight : weightCollection[0].GetWeight(docTypeID);
  }

  /// <summary>
  /// Получить значение "веса" указанного типа объекта-документа
  /// </summary>
  /// <param name="docTypeGuid">Guid типа объекта-документа</param>
  /// <returns>Значение "веса" или DocumentTypeWeight.UndefinedWeight,
  /// если тип объекта не найден, либо значение "веса" неопределено</returns>
  public virtual long GetWeight(Guid docTypeGuid)
  {
    return this.GetWeight(MetaDataHelper.GetObjectTypeID(docTypeGuid));
  }

  /// <summary>Отыскать описание указанного типа объекта-документа</summary>
  /// <param name="docTypeID">Идентификатор типа объекта-документа</param>
  /// <returns>Описание указанного типа объекта-документа или null</returns>
  public virtual DocumentTypeWeight FindDocumentType(int docTypeID)
  {
    if (this.owner != null)
      return this.owner.FindDocumentType(docTypeID);
    DocumentTypeWeightCollection weightCollection = this.RootCollection ?? this;
    return weightCollection.Count == 0 ? (DocumentTypeWeight) null : weightCollection[0].FindDocumentType(docTypeID);
  }

  /// <summary>Отыскать описание указанного типа объекта-документа</summary>
  /// <param name="docTypeGuid">Guid типа объекта-документа</param>
  /// <returns>Описание указанного типа объекта-документа или null</returns>
  public virtual DocumentTypeWeight FindDocumentType(Guid docTypeGuid)
  {
    return this.FindDocumentType(MetaDataHelper.GetObjectTypeID(docTypeGuid));
  }

  /// <summary>Выполнить автоматический пересчёт "весов"</summary>
  /// <param name="startWeight">Стартовое значение "веса"</param>
  /// <param name="delta">Приращение "веса" для каждого элемента</param>
  /// <returns>Следующее значение "веса" (с учётом того, что "веса" были назначены всей дочерней иерархии
  /// типов объектов-документов)
  /// </returns>
  public virtual long UpdateWeights(long startWeight, int delta)
  {
    long startWeight1 = startWeight;
    for (int index = 0; index < this.Count; ++index)
      startWeight1 = this[index].UpdateWeights(startWeight1, delta);
    return startWeight1;
  }
}
