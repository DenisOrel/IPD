// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.DocumentTypeWeight
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Класс, позволяющий определить "вес" типа документа в разделах спецификации.
/// Чем меньше значение "веса", тем ближе к началу раздела должны находиться
/// объекты указанного типа документа. Класс умеет выполнять синхронизацию
/// своих данных с кешем метаданных, а также работать с XML для сохранения и
/// загрузки своего состояния и полей.
/// </summary>
[Serializable]
public class DocumentTypeWeight : 
  ICloneable,
  IComparable,
  IComparable<DocumentTypeWeight>,
  IMetaDataSync,
  IXMLStorageLoadSave,
  IDocumentTypeWeight
{
  /// <summary>Неопределённый "вес" типа объекта-документа</summary>
  public static long UndefinedWeight = long.MaxValue;
  /// <summary>Начальное значение "веса" для автоматического расчёта в коллекции типов объектов-документов</summary>
  public static long StartWeight = 0;
  /// <summary>Приращение "веса" при автоматическом расчёте в коллекции типов объектов-документов</summary>
  public static int WeightDelta = 1000000;
  /// <summary>Главный узел XML, в котором сохраняется дерево типов объектов и значения "весов" - [DocumentTypesWeights]</summary>
  [NonSerialized]
  public const string xmlMainNode = "DocumentTypesWeights";
  /// <summary>Узел XML, в котором сохраняется описание типа объектов, значение его "веса", а также дочерние типы - [doctype]</summary>
  [NonSerialized]
  public const string xmlDocumentTypeNode = "doctype";
  /// <summary>Атрибут для guid типа объекта-документа - "guid"</summary>
  [NonSerialized]
  public const string xmlattrGuid = "guid";
  /// <summary>Атрибут для "веса" типа объекта-документа - "weight"</summary>
  [NonSerialized]
  public const string xmlattrWeight = "weight";
  /// <summary>Идентификатор типа объекта "Спецификация"</summary>
  [NonSerialized]
  internal static int specTypeID = -1;
  /// <summary>Идентификатор типа объекта "Чертежи деталей"</summary>
  [NonSerialized]
  public static int partDrawType = -1;
  /// <summary>Список типов объектов, которые не должны попадать в состав коллекций DocumentTypeWeightCollection</summary>
  [NonSerialized]
  internal static List<int> disabledObjTypes = (List<int>) null;
  /// <summary>Идентификатор типа объекта-документа, для которого хранится "вес"</summary>
  protected int documentTypeID = -1;
  /// <summary>"Вес" типа объекта-документа в разделах спецификации. Значение
  /// DocumentTypeWeight.UndefinedWeight означает то, что "вес" не определён
  /// </summary>
  protected long weight = DocumentTypeWeight.UndefinedWeight;
  /// <summary>Ссылка на родительский тип объекта-документа (null, если нет родительского типа)</summary>
  protected DocumentTypeWeight parentType;
  /// <summary>Коллекция, которой принадлежит указанный объект</summary>
  protected DocumentTypeWeightCollection owner;
  /// <summary>Коллекция дочерних типов объектов-документов</summary>
  protected DocumentTypeWeightCollection items = new DocumentTypeWeightCollection();

  /// <summary>Идентификатор типа объекта-документа, для которого хранится "вес"</summary>
  public virtual int DocumentTypeID
  {
    [DebuggerStepThrough] get => this.documentTypeID;
    [DebuggerStepThrough] set => this.documentTypeID = value;
  }

  /// <summary>"Вес" типа объекта-документа в разделах спецификации. Значение
  /// DocumentTypeWeight.UndefinedWeight означает то, что "вес" не определён
  /// </summary>
  public virtual long Weight
  {
    [DebuggerStepThrough] get => this.weight;
    [DebuggerStepThrough] set => this.weight = value;
  }

  /// <summary>Ссылка на родительский тип объекта-документа (null, если нет родительского типа)</summary>
  public virtual DocumentTypeWeight ParentType
  {
    [DebuggerStepThrough] get => this.parentType;
    [DebuggerStepThrough] set => this.parentType = value;
  }

  /// <summary>Коллекция, которой принадлежит указанный объект</summary>
  public DocumentTypeWeightCollection Owner
  {
    [DebuggerStepThrough] get => this.owner;
    [DebuggerStepThrough] set => this.owner = value;
  }

  /// <summary>Коллекция дочерних типов объектов-документов</summary>
  public virtual DocumentTypeWeightCollection Items
  {
    [DebuggerStepThrough] get => this.items;
  }

  /// <summary>Создать заполненный по умолчанию экземпляр класса</summary>
  public DocumentTypeWeight() => this.items.Owner = this;

  /// <summary>Создать экземпляр класса, привязанный к типу объекта-документа.
  /// "Вес" будет назначен равным DocumentTypeWeight.UndefinedWeight
  /// </summary>
  /// <param name="documentTypeID">Тип объекта-документа</param>
  public DocumentTypeWeight(int documentTypeID)
    : this(documentTypeID, DocumentTypeWeight.UndefinedWeight)
  {
  }

  /// <summary>Создать экземпляр класса, привязанный к типу объекта-документа, с указанным весом</summary>
  /// <param name="documentTypeID">Тип объекта-документа</param>
  /// <param name="weight">"Вес" типа объекта-документа</param>
  public DocumentTypeWeight(int documentTypeID, long weight)
    : this()
  {
    this.documentTypeID = documentTypeID;
    this.weight = weight;
  }

  /// <summary>Создать экземпляр класса по прототипу</summary>
  /// <param name="template">Прототип</param>
  public DocumentTypeWeight(DocumentTypeWeight template)
    : this()
  {
    if (template == null)
      return;
    this.documentTypeID = template.documentTypeID;
    this.weight = template.weight;
    this.Items.Assign(template.Items);
  }

  /// <summary>Выполнить сравнение с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return !(obj is DocumentTypeWeight documentTypeWeight) ? base.Equals(obj) : this.documentTypeID == documentTypeWeight.documentTypeID;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.documentTypeID.GetHashCode();

  /// <summary>Вернуть строковое представление экземпляра класса</summary>
  /// <returns>Строковое представление экземпляра класса</returns>
  public override string ToString()
  {
    return $"[{this.documentTypeID}.{this.weight}] \"{MetaDataHelper.GetObjectTypeName(this.documentTypeID)}\"";
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public virtual object Clone()
  {
    return (object) new DocumentTypeWeight(this.documentTypeID, this.weight);
  }

  /// <summary>Выполнить сравнение с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public virtual int CompareTo(object obj) => this.CompareTo(obj as DocumentTypeWeight);

  /// <summary>Выполнить сравнение с указанным объектом. Сравнение идёт по "весу", а если
  /// "вес" не определён, то по названию типа объекта-документа</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public virtual int CompareTo(DocumentTypeWeight other)
  {
    if (other == null)
      return -1;
    int num = this.weight.CompareTo(other.weight);
    return num != 0 ? num : MetaDataHelper.GetObjectTypeName(this.documentTypeID).ToUpperInvariant().CompareTo(MetaDataHelper.GetObjectTypeName(other.documentTypeID).ToUpperInvariant());
  }

  /// <summary>Выполнить синхронизацию внутренних коллекций с кэшем метаданных</summary>
  public virtual void SyncMetaData()
  {
    if (!MetaDataHelper.ExistsObjectType(this.documentTypeID) || DocumentTypeWeight.disabledObjTypes.Contains(this.documentTypeID))
      this.Clear();
    else
      this.items.SyncMetaData();
  }

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public virtual void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (xmlStorage == null || node == null || node.Name != "doctype")
      return;
    string attributeValue = xmlStorage.GetAttributeValue(node, "guid", string.Empty);
    if (attributeValue == string.Empty)
      return;
    Guid empty = Guid.Empty;
    Guid objTypeGuid;
    try
    {
      objTypeGuid = new Guid(attributeValue);
    }
    catch
    {
      return;
    }
    this.documentTypeID = MetaDataHelper.GetObjectTypeID(objTypeGuid);
    if (!long.TryParse(xmlStorage.GetAttributeValue(node, "weight", DocumentTypeWeight.UndefinedWeight.ToString()), out this.weight))
      this.weight = DocumentTypeWeight.UndefinedWeight;
    XmlNode node1 = xmlStorage.FindNode(node, "DocumentTypesWeights", false);
    this.items.Load(xmlStorage, node1);
    this.SyncMetaData();
    if (this.documentTypeID != -1)
      return;
    this.Clear();
  }

  /// <summary>Сохранить данные в состав указанного родительского узла</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public virtual void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    this.SyncMetaData();
    if (this.documentTypeID == -1)
    {
      this.Clear();
    }
    else
    {
      if (xmlStorage == null || parentNode == null)
        return;
      string attrValue = MetaDataHelper.GetObjectTypeGuid(this.documentTypeID).ToString();
      XmlNode nodeWithAttr1 = xmlStorage.FindNodeWithAttr(parentNode, "doctype", "guid", attrValue, true);
      parentNode.RemoveChild(nodeWithAttr1);
      XmlNode nodeWithAttr2 = xmlStorage.FindNodeWithAttr(parentNode, "doctype", "guid", attrValue, true);
      xmlStorage.SetAttributeValue(nodeWithAttr2, "guid", attrValue);
      xmlStorage.SetAttributeValue(nodeWithAttr2, "weight", this.weight.ToString());
      this.items.Save(xmlStorage, nodeWithAttr2);
    }
  }

  /// <summary>Отыскать корневой тип объекта-документа для текущего узла</summary>
  public DocumentTypeWeight RootDocumentType
  {
    get
    {
      if (this.parentType == null)
        return this;
      DocumentTypeWeight parentType = this.parentType;
      while (parentType.parentType != null)
        parentType = parentType.parentType;
      return parentType;
    }
  }

  /// <summary>Отыскать корневую коллекцию</summary>
  public DocumentTypeWeightCollection RootCollection
  {
    get => this.owner == null ? (DocumentTypeWeightCollection) null : this.owner.RootCollection;
  }

  /// <summary>Получить значение "веса" указанного типа объекта-документа</summary>
  /// <param name="docTypeID">Идентификатор типа объекта-документа</param>
  /// <returns>Значение "веса" или DocumentTypeWeight.UndefinedWeight,
  /// если тип объекта не найден, либо значение "веса" неопределено</returns>
  public virtual long GetWeight(int docTypeID)
  {
    if (this.documentTypeID == docTypeID)
      return this.weight;
    DocumentTypeWeight documentType = this.FindDocumentType(docTypeID);
    return documentType == null ? DocumentTypeWeight.UndefinedWeight : documentType.weight;
  }

  /// <summary>Получить значение "веса" указанного типа объекта-документа</summary>
  /// <param name="docTypeGuid">Guid типа объекта-документа</param>
  /// <returns>Значение "веса" или DocumentTypeWeight.UndefinedWeight,
  /// если тип объекта не найден, либо значение "веса" неопределено</returns>
  public virtual long GetWeight(Guid docTypeGuid)
  {
    return this.GetWeight(MetaDataHelper.GetObjectTypeID(docTypeGuid));
  }

  /// <summary>Отыскать описание указанного типа объекта-документа
  /// (поиск ведётся только вниз по иерархии типов объектов-документов,
  /// режим - на верху "пирамиды" - объект)
  /// </summary>
  /// <param name="docTypeID">Идентификатор типа объекта-документа</param>
  /// <returns>Описание указанного типа объекта-документа или null</returns>
  protected internal DocumentTypeWeight InternalFindDocumentType(int docTypeID)
  {
    if (this.documentTypeID == docTypeID)
      return this;
    for (int index = 0; index < this.items.Count; ++index)
    {
      DocumentTypeWeight documentType = this.items[index].InternalFindDocumentType(docTypeID);
      if (documentType != null)
        return documentType;
    }
    return (DocumentTypeWeight) null;
  }

  /// <summary>Отыскать описание указанного типа объекта-документа</summary>
  /// <param name="docTypeID">Идентификатор типа объекта-документа</param>
  /// <returns>Описание указанного типа объекта-документа или null</returns>
  public virtual DocumentTypeWeight FindDocumentType(int docTypeID)
  {
    if (this.documentTypeID == docTypeID)
      return this;
    DocumentTypeWeightCollection rootCollection = this.RootCollection;
    if (rootCollection != null)
    {
      for (int index = 0; index < rootCollection.Count; ++index)
      {
        DocumentTypeWeight documentType = rootCollection[index].InternalFindDocumentType(docTypeID);
        if (documentType != null)
          return documentType;
      }
      return (DocumentTypeWeight) null;
    }
    DocumentTypeWeight rootDocumentType = this.RootDocumentType;
    return rootDocumentType.documentTypeID == docTypeID ? rootDocumentType : rootDocumentType.InternalFindDocumentType(docTypeID);
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
    this.Weight = startWeight;
    return this.Items.UpdateWeights(startWeight + (long) delta, delta);
  }

  /// <summary>Очистить экземпляр класса, все его внутренние коллекции, но сохранить ссылку на владельца</summary>
  public virtual void Clear()
  {
    this.documentTypeID = -1;
    this.weight = DocumentTypeWeight.UndefinedWeight;
    this.items.Clear();
  }
}
