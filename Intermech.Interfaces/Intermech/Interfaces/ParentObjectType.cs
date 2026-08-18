
// Type: Intermech.Interfaces.ParentObjectType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, описывающий родительский тип объекта, а также содержащий список допустимых типов связей
    /// </summary>
    [Serializable]
    public class ParentObjectType : 
      IParentObjectType,
      IXMLStoredClass,
      ICloneable,
      IComparable,
      IComparable<ParentObjectType>
    {
      /// <summary>ID родительского типа объекта</summary>
      protected int _objectTypeID;
      /// <summary>Список допустимых типов связей</summary>
      protected List<ChildRelationType> _childRelationTypes;
      /// <summary>
      /// Разрешено ли отображать выборки и классификаторы внутри узлов объектов данных типов
      /// </summary>
      protected bool _enableSelectionsAndClassifiers;
      private Guid? _defaultObjectListFilter;

      /// <summary>Создать пустой экземпляр класса</summary>
      public ParentObjectType()
      {
        this._objectTypeID = -1;
        this._childRelationTypes = new List<ChildRelationType>();
        this._enableSelectionsAndClassifiers = true;
      }

      /// <summary>Создать описание родительского типа объектов</summary>
      /// <param name="objectTypeID">ID родительского типа объектов</param>
      public ParentObjectType(int objectTypeID)
      {
        this._objectTypeID = objectTypeID;
        this._childRelationTypes = new List<ChildRelationType>();
        this._enableSelectionsAndClassifiers = true;
      }

      public Guid? DefaultObjectListFilter
      {
        get => this._defaultObjectListFilter;
        set => this._defaultObjectListFilter = value;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is ParentObjectType parentObjectType) ? base.Equals(obj) : this._objectTypeID == parentObjectType._objectTypeID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this._objectTypeID.GetHashCode();

      /// <summary>ID родительского типа объектов</summary>
      public virtual int ObjectTypeID
      {
        [DebuggerStepThrough] get => this._objectTypeID;
        set => this._objectTypeID = value;
      }

      /// <summary>Список допустимых типов связей</summary>
      public virtual List<ChildRelationType> ChildRelationTypes
      {
        get
        {
          if (this._childRelationTypes == null)
            this._childRelationTypes = new List<ChildRelationType>();
          return this._childRelationTypes;
        }
      }

      /// <summary>
      /// Разрешено ли отображать выборки и классификаторы внутри узлов объектов данных типов
      /// </summary>
      public virtual bool EnableSelectionsAndClassifiers
      {
        [DebuggerStepThrough] get => this._enableSelectionsAndClassifiers;
        set => this._enableSelectionsAndClassifiers = value;
      }

      /// <summary>Вернуть описание допустимого типа связи по его ID</summary>
      /// <param name="relTypeID">ID допустимого типа связи</param>
      /// <returns>Описание допустимого типа связи или null</returns>
      public virtual ChildRelationType this[int relTypeID]
      {
        get
        {
          if (this._childRelationTypes == null)
            this._childRelationTypes = new List<ChildRelationType>();
          for (int index = 0; index < this._childRelationTypes.Count; ++index)
          {
            if (this._childRelationTypes[index].RelationTypeID == relTypeID)
              return this._childRelationTypes[index];
          }
          return (ChildRelationType) null;
        }
      }

      /// <summary>Очистить поля класса. Guid типа объекта сохраняется.</summary>
      public virtual void Clear() => this.ChildRelationTypes.Clear();

      /// <summary>
      /// Загрузить информацию в текущий объект из указанного объекта
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public virtual void Assign(object source)
      {
        if (!(source is ParentObjectType parentObjectType))
          return;
        this.Clear();
        this.ObjectTypeID = parentObjectType.ObjectTypeID;
        for (int index = 0; index < parentObjectType.ChildRelationTypes.Count; ++index)
          this.ChildRelationTypes.Add(parentObjectType.ChildRelationTypes[index].Clone() as ChildRelationType);
        this.EnableSelectionsAndClassifiers = parentObjectType.EnableSelectionsAndClassifiers;
        this.GenerateStartSortingValues();
      }

      /// <summary>
      /// Загрузить описание родительского типа объекта из указанного узла настроек
      /// </summary>
      /// <param name="storage">Хранилище настроек</param>
      /// <param name="node">Узел, из которого загружается информация</param>
      public virtual void Load(XMLSettingsStorage storage, XmlNode node)
      {
        this.Clear();
        if (storage == null || node == null)
          return;
        string attributeValue = storage.GetAttributeValue(node, "Guid", string.Empty);
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
        this._objectTypeID = MetaDataHelper.GetObjectTypeID(objTypeGuid);
        for (int i = 0; i < node.ChildNodes.Count; ++i)
        {
          XmlNode childNode = node.ChildNodes[i];
          if (!(childNode.Name != "RelationType"))
          {
            ChildRelationType childRelationType = new ChildRelationType();
            childRelationType.Load(storage, childNode);
            if (childRelationType.RelationTypeID != -1 && !this._childRelationTypes.Contains(childRelationType))
              this._childRelationTypes.Add(childRelationType);
          }
        }
        this._enableSelectionsAndClassifiers = storage.GetAttributeValue(node, "Selections", string.Empty) != "0";
        Guid result;
        if (Guid.TryParse(storage.GetAttributeValue(node, "DefaultObjectListFilter", string.Empty), out result))
          this._defaultObjectListFilter = new Guid?(result);
        this.GenerateStartSortingValues();
      }

      /// <summary>
      /// Сохранить описание родительского типа объектов в родительский узел в XML-хранилище
      /// </summary>
      /// <param name="storage">Хранилище настроек</param>
      /// <param name="node">Родительский узел или null (тогда узел создаётся прямо в корневом узле документа XML)</param>
      public virtual void Save(XMLSettingsStorage storage, XmlNode node)
      {
        if (this._childRelationTypes == null)
          this._childRelationTypes = new List<ChildRelationType>();
        if (storage == null)
          return;
        node = node == null ? (XmlNode) storage.document.DocumentElement : node;
        XmlNode nodeWithAttr1 = storage.FindNodeWithAttr(node, nameof (ParentObjectType), "Guid", MetaDataHelper.GetObjectTypeGuid(this._objectTypeID).ToString(), true);
        node.RemoveChild(nodeWithAttr1);
        XmlNode nodeWithAttr2 = storage.FindNodeWithAttr(node, nameof (ParentObjectType), "Guid", MetaDataHelper.GetObjectTypeGuid(this._objectTypeID).ToString(), true);
        storage.SetAttributeValue(nodeWithAttr2, "Guid", MetaDataHelper.GetObjectTypeGuid(this._objectTypeID).ToString());
        if (!this._enableSelectionsAndClassifiers)
          storage.SetAttributeValue(nodeWithAttr2, "Selections", "0");
        storage.SetAttributeValue(nodeWithAttr2, "DefaultObjectListFilter", this.DefaultObjectListFilter.HasValue ? this.DefaultObjectListFilter.Value.ToString() : (string) null);
        for (int index = 0; index < this._childRelationTypes.Count; ++index)
          this._childRelationTypes[index].Save(storage, nodeWithAttr2);
      }

      /// <summary>
      /// Перегенерировать стартовые значения атрибута "Сортировка" у всей коллекции дочерних типов объектов
      /// </summary>
      public virtual void GenerateStartSortingValues()
      {
        for (int index = 0; index < this.ChildRelationTypes.Count; ++index)
          this.ChildRelationTypes[index].GenerateStartSortingValues();
      }

      /// <summary>
      /// Выполнить синхронизацию списка допустимых типов связей с кэшем метаданных
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с кэшем метаданных</param>
      public virtual void SyncMetadata(IUserSession session)
      {
        if (session == null)
          return;
        List<IMSApplicability> typeApplicabilities = MetaDataHelper.GetObjectTypeApplicabilities(this._objectTypeID);
        List<int> applicabilityRelationTypesId = MetaDataHelper.GetApplicabilityRelationTypesID(this._objectTypeID);
        List<ChildRelationType> childRelationTypeList = new List<ChildRelationType>();
        List<int> intList = new List<int>();
        try
        {
          for (int index = 0; index < typeApplicabilities.Count; ++index)
          {
            if (this[typeApplicabilities[index].RelationTypeID] == null && !intList.Contains(typeApplicabilities[index].RelationTypeID))
              intList.Add(typeApplicabilities[index].RelationTypeID);
          }
          for (int index = 0; index < this.ChildRelationTypes.Count; ++index)
          {
            if (!applicabilityRelationTypesId.Contains(this.ChildRelationTypes[index].RelationTypeID))
              childRelationTypeList.Add(this.ChildRelationTypes[index]);
          }
          for (int index = 0; index < childRelationTypeList.Count; ++index)
            this.ChildRelationTypes.Remove(childRelationTypeList[index]);
          int defaultRelationTypeId = MetaDataHelper.GetDefaultRelationTypeID(this._objectTypeID);
          for (int index = 0; index < intList.Count; ++index)
            this.ChildRelationTypes.Add(new ChildRelationType(intList[index], this._objectTypeID, intList[index] == defaultRelationTypeId));
        }
        catch
        {
          this.ChildRelationTypes.Clear();
          return;
        }
        for (int index = 0; index < this.ChildRelationTypes.Count; ++index)
          this.ChildRelationTypes[index].SyncMetadata(session);
      }

      /// <summary>Создать точную копию коллекции</summary>
      /// <returns>Точная копия коллекции</returns>
      public object Clone()
      {
        if (this._childRelationTypes == null)
          this._childRelationTypes = new List<ChildRelationType>();
        ParentObjectType parentObjectType = new ParentObjectType(this._objectTypeID);
        parentObjectType.EnableSelectionsAndClassifiers = this.EnableSelectionsAndClassifiers;
        List<ChildRelationType> childRelationTypes = parentObjectType._childRelationTypes;
        if (childRelationTypes.Capacity < this._childRelationTypes.Count)
          childRelationTypes.Capacity = this._childRelationTypes.Count;
        foreach (ChildRelationType childRelationType in this._childRelationTypes)
          childRelationTypes.Add(childRelationType.Clone() as ChildRelationType);
        parentObjectType.DefaultObjectListFilter = this.DefaultObjectListFilter;
        return (object) parentObjectType;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0 или 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as ParentObjectType);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0 или 1</returns>
      public int CompareTo(ParentObjectType other)
      {
        return other == null ? 1 : StringComparer.InvariantCultureIgnoreCase.Compare(MetaDataHelper.GetObjectTypeName(this._objectTypeID), MetaDataHelper.GetObjectTypeName(other._objectTypeID));
      }
    }
}
