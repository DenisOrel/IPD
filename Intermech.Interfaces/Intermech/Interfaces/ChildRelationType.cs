using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, описывающий дочерний тип связи, а также содержащий список дочерних типов объектов
    /// </summary>
    [Serializable]
    public class ChildRelationType : 
      IChildRelationType,
      IXMLStoredClass,
      ICloneable,
      IComparable,
      IComparable<ChildRelationType>
    {
      /// <summary>Является ли указанная связь видимой</summary>
      protected bool _visible;
      /// <summary>ID дочернего типа связи</summary>
      protected int _relationTypeID;
      /// <summary>
      /// ID родительского типа объекта, состав которого получается указанным типом связи
      /// </summary>
      protected int _parentObjectTypeID;
      /// <summary>Список дочерних типов объектов</summary>
      protected List<ChildObjectType> _childObjectTypes;

      /// <summary>Создать пустой экземпляр класса</summary>
      public ChildRelationType()
      {
        this._relationTypeID = -1;
        this._parentObjectTypeID = -1;
        this._childObjectTypes = new List<ChildObjectType>();
        this._visible = false;
      }

      /// <summary>Создать описание дочернего типа связи</summary>
      /// <param name="relationTypeId">ID дочернего типа связи</param>
      /// <param name="parentObjectTypeId">ID родительского типа объектов</param>
      /// <param name="visible">Видима ли данная связь в "Навигаторе"</param>
      public ChildRelationType(int relationTypeId, int parentObjectTypeId, bool visible)
      {
        this._relationTypeID = relationTypeId;
        this._parentObjectTypeID = parentObjectTypeId;
        this._visible = visible;
        this._childObjectTypes = new List<ChildObjectType>();
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        if (!(obj is ChildRelationType childRelationType))
          return base.Equals(obj);
        return this._relationTypeID == childRelationType._relationTypeID && this._parentObjectTypeID == childRelationType._parentObjectTypeID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        return this._parentObjectTypeID.GetHashCode() << 16 /*0x10*/ & this._relationTypeID.GetHashCode();
      }

      /// <summary>
      /// Сравнить расположение дочерних типов объектов в составе типа связи
      /// </summary>
      /// <param name="objType1">Первый тип доч. объекта</param>
      /// <param name="objType2">Второй тип доч. объекта</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(int objType1, int objType2)
      {
        int parentObjectType1 = this.GetNearestBaseParentObjectType(objType1);
        int parentObjectType2 = this.GetNearestBaseParentObjectType(objType2);
        int num1 = this.IndexOf(parentObjectType1);
        int num2 = this.IndexOf(parentObjectType2);
        return num1 != -1 && num2 != -2 ? Math.Sign(num1 - num2) : 0;
      }

      public IEnumerable<ChildObjectType> GetChildObjectTypesAndDescendants()
      {
        foreach (ChildObjectType childObjectType in this.ChildObjectTypes)
        {
          yield return childObjectType;
          foreach (ChildObjectType descendant in childObjectType.GetDescendants())
            yield return descendant;
        }
      }

      /// <summary>Является ли указанная связь видимой</summary>
      public bool Visible
      {
        get => this._visible;
        set => this._visible = value;
      }

      /// <summary>ID дочернего типа связи</summary>
      public virtual int RelationTypeID
      {
        get => this._relationTypeID;
        set => this._relationTypeID = value;
      }

      /// <summary>
      /// ID родительского типа объекта, состав которого получается указанным типом связи
      /// </summary>
      public int ParentObjectTypeID
      {
        get => this._parentObjectTypeID;
        set => this._parentObjectTypeID = value;
      }

      /// <summary>
      /// Список дочерних типов объектов (список объектов только первого уровня иерархии !!!!
      /// юзаем GetNearestBaseParentObjectType для получения парента из списка)
      /// </summary>
      public virtual List<ChildObjectType> ChildObjectTypes
      {
        get
        {
          if (this._childObjectTypes == null)
            this._childObjectTypes = new List<ChildObjectType>();
          return this._childObjectTypes;
        }
      }

      /// <summary>
      /// Отыщем в текущем описании типа связи наиболее подходящий родительский тип объекта
      /// </summary>
      /// <param name="childObjType">Дочерний тип объекта</param>
      /// <returns>Наиболее подходящий родительский тип объекта или дочерний тип</returns>
      public virtual int GetNearestBaseParentObjectType(int childObjType)
      {
        if (this.ChildObjectTypes.Count == 0 || childObjType < 0 || this[childObjType] != null)
          return childObjType;
        for (int index = 0; index < this.ChildObjectTypes.Count; ++index)
        {
          if (MetaDataHelper.IsObjectTypeChildOf(childObjType, this.ChildObjectTypes[index].ObjectTypeID))
            return this.ChildObjectTypes[index].ObjectTypeID;
        }
        return childObjType;
      }

      /// <summary>
      /// Отыщем в текущем описании типа связи индекс наиболее подходящего родительского типа объекта
      /// </summary>
      /// <param name="childObjType">Дочерний тип объекта</param>
      /// <returns>Индекс наиболее подходящего родительского типа объекта или -1, если ничего не найдено</returns>
      public virtual int GetNearestBaseParentObjectTypeIndex(int childObjType)
      {
        return this.ChildObjectTypes.FindIndex((Predicate<ChildObjectType>) (item => item.ObjectTypeID == childObjType || MetaDataHelper.IsObjectTypeChildOf(childObjType, item.ObjectTypeID)));
      }

      /// <summary>Вернуть описание дочернего типа по его ID</summary>
      /// <param name="objTypeID">ID дочернего типа</param>
      /// <returns>Описание дочернего типа или null</returns>
      public virtual ChildObjectType this[int objTypeID]
      {
        get
        {
          if (this._childObjectTypes == null)
            this._childObjectTypes = new List<ChildObjectType>();
          for (int index = 0; index < this._childObjectTypes.Count; ++index)
          {
            if (this._childObjectTypes[index].ObjectTypeID == objTypeID)
              return this._childObjectTypes[index];
          }
          return (ChildObjectType) null;
        }
      }

      /// <summary>
      /// Отыскать в коллекции индекс указанного дочернего типа объекта
      /// </summary>
      /// <param name="objTypeID">Дочерний тип объекта</param>
      /// <returns>Индекс указанного дочернего типа объекта или -1</returns>
      public virtual int IndexOf(int objTypeID)
      {
        for (int index = 0; index < this._childObjectTypes.Count; ++index)
        {
          if (this._childObjectTypes[index].ObjectTypeID == objTypeID)
            return index;
        }
        return -1;
      }

      /// <summary>Очистить поля класса. ID типа связи сохраняется.</summary>
      public virtual void Clear()
      {
        if (this._childObjectTypes == null)
          this._childObjectTypes = new List<ChildObjectType>();
        this._childObjectTypes.Clear();
        this.Visible = false;
      }

      /// <summary>
      /// Загрузить информацию в текущий объект из указанного объекта
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public virtual void Assign(object source)
      {
        if (!(source is ChildRelationType childRelationType))
          return;
        this.Clear();
        this.RelationTypeID = childRelationType.RelationTypeID;
        this.ParentObjectTypeID = childRelationType.ParentObjectTypeID;
        this.Visible = childRelationType.Visible;
        for (int index = 0; index < childRelationType.ChildObjectTypes.Count; ++index)
          this.ChildObjectTypes.Add(childRelationType.ChildObjectTypes[index].Clone() as ChildObjectType);
        this.GenerateStartSortingValues();
      }

      /// <summary>
      /// Загрузить описание дочернего типа связи из указанного узла настроек
      /// </summary>
      /// <param name="storage">Хранилище настроек</param>
      /// <param name="node">Узел, из которого загружается информация</param>
      public virtual void Load(XMLSettingsStorage storage, XmlNode node)
      {
        this.Clear();
        if (storage == null || node == null)
          return;
        string attributeValue1 = storage.GetAttributeValue(node, "Guid", string.Empty);
        if (attributeValue1 == string.Empty)
          return;
        Guid empty1 = Guid.Empty;
        Guid relTypeGuid;
        try
        {
          relTypeGuid = new Guid(attributeValue1);
        }
        catch
        {
          return;
        }
        this._relationTypeID = MetaDataHelper.GetRelationTypeID(relTypeGuid);
        string attributeValue2 = storage.GetAttributeValue(node, "ParentGuid", string.Empty);
        if (attributeValue2 == string.Empty)
          return;
        Guid empty2 = Guid.Empty;
        Guid objTypeGuid;
        try
        {
          objTypeGuid = new Guid(attributeValue2);
        }
        catch
        {
          return;
        }
        this._parentObjectTypeID = MetaDataHelper.GetObjectTypeID(objTypeGuid);
        int result;
        if (!int.TryParse(storage.GetAttributeValue(node, "Visible", string.Empty), out result))
          result = 0;
        this._visible = Convert.ToBoolean(result);
        for (int i = 0; i < node.ChildNodes.Count; ++i)
        {
          XmlNode childNode = node.ChildNodes[i];
          if (!(childNode.Name != "ChildrenObjectType"))
          {
            ChildObjectType childObjectType = new ChildObjectType();
            childObjectType.Load(storage, childNode);
            if (childObjectType.ObjectTypeID != -1 && !this.ChildObjectTypes.Contains(childObjectType))
              this.ChildObjectTypes.Add(childObjectType);
          }
        }
        List<ChildObjectType> childObjectTypeList1 = new List<ChildObjectType>();
        for (int index1 = this.ChildObjectTypes.Count - 1; index1 >= 0; --index1)
        {
          ChildObjectType childObjectType1 = this.ChildObjectTypes[index1];
          for (int index2 = 0; index2 < this.ChildObjectTypes.Count; ++index2)
          {
            if (index1 != index2)
            {
              ChildObjectType childObjectType2 = this.ChildObjectTypes[index2];
              if (MetaDataHelper.IsObjectTypeChildOf(childObjectType1.ObjectTypeID, childObjectType2.ObjectTypeID) && childObjectType1.ObjectTypeID != childObjectType2.ObjectTypeID && childObjectTypeList1.IndexOf(childObjectType1) < 0)
                childObjectTypeList1.Add(childObjectType1);
            }
          }
        }
        for (int index = 0; index < childObjectTypeList1.Count; ++index)
          this.ChildObjectTypes.Remove(childObjectTypeList1[index]);
        List<ChildObjectType> childObjectTypeList2 = new List<ChildObjectType>();
        foreach (ChildObjectType childObjectType in this.ChildObjectTypes)
        {
          List<int> parentObjectTypeIds = MetaDataHelper.GetObjectTypeParentsID(childObjectType.ObjectTypeID);
          if (this.ChildObjectTypes.Any<ChildObjectType>((Func<ChildObjectType, bool>) (o => parentObjectTypeIds.Contains(o.ObjectTypeID))))
            childObjectTypeList2.Add(childObjectType);
        }
        foreach (ChildObjectType childObjectType in childObjectTypeList2)
          this.ChildObjectTypes.Remove(childObjectType);
        this.GenerateStartSortingValues();
      }

      /// <summary>
      /// Сохранить описание дочернего типа связи в родительский узел в XML-хранилище
      /// </summary>
      /// <param name="storage">Хранилище настроек</param>
      /// <param name="node">Родительский узел или null (тогда узел создаётся прямо в корневом узле документа XML)</param>
      public virtual void Save(XMLSettingsStorage storage, XmlNode node)
      {
        if (this._childObjectTypes == null)
          this._childObjectTypes = new List<ChildObjectType>();
        if (storage == null)
          return;
        node = node == null ? (XmlNode) storage.document.DocumentElement : node;
        XmlNode nodeWithAttr1 = storage.FindNodeWithAttr(node, "RelationType", "Guid", MetaDataHelper.GetRelationTypeGuid(this._relationTypeID).ToString(), true);
        node.RemoveChild(nodeWithAttr1);
        XmlNode nodeWithAttr2 = storage.FindNodeWithAttr(node, "RelationType", "Guid", MetaDataHelper.GetRelationTypeGuid(this._relationTypeID).ToString(), true);
        storage.SetAttributeValue(nodeWithAttr2, "Guid", MetaDataHelper.GetRelationTypeGuid(this._relationTypeID).ToString());
        storage.SetAttributeValue(nodeWithAttr2, "ParentGuid", MetaDataHelper.GetObjectTypeGuid(this._parentObjectTypeID).ToString());
        storage.SetAttributeValue(nodeWithAttr2, "Visible", Convert.ToInt32(this._visible).ToString());
        this.GenerateStartSortingValues();
        for (int index = 0; index < this._childObjectTypes.Count; ++index)
          this._childObjectTypes[index].Save(storage, nodeWithAttr2);
      }

      /// <summary>
      /// Перегенерировать стартовые значения атрибута "Сортировка" у дочерних типов объектов
      /// </summary>
      public virtual void GenerateStartSortingValues()
      {
        long num = 1000000000;
        for (int index = 0; index < this.ChildObjectTypes.Count; ++index)
        {
          this.ChildObjectTypes[index].StartSortingValue = num;
          num += 1000000000L;
        }
      }

      /// <summary>Выполнить синхронизацию с кэшем метаданных</summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с кэшем метаданных</param>
      public virtual void SyncMetadata(IUserSession session)
      {
        if (session == null)
          return;
        List<IMSObjectType> childObjectTypes = MetaDataHelper.GetApplicabilityChildObjectTypes(this._parentObjectTypeID, this._relationTypeID);
        List<int> childObjectTypesId = MetaDataHelper.GetApplicabilityChildObjectTypesID(this._parentObjectTypeID, this._relationTypeID);
        List<ChildObjectType> childObjectTypeList1 = new List<ChildObjectType>();
        List<int> intList = new List<int>();
        try
        {
          for (int index = 0; index < childObjectTypes.Count; ++index)
          {
            if (this[childObjectTypes[index].ObjectTypeID] == null && !intList.Contains(childObjectTypes[index].ObjectTypeID))
              intList.Add(childObjectTypes[index].ObjectTypeID);
          }
          for (int index = 0; index < this.ChildObjectTypes.Count; ++index)
          {
            if (!childObjectTypesId.Contains(this.ChildObjectTypes[index].ObjectTypeID))
              childObjectTypeList1.Add(this._childObjectTypes[index]);
          }
          for (int index = 0; index < childObjectTypeList1.Count; ++index)
            this.ChildObjectTypes.Remove(childObjectTypeList1[index]);
          for (int index = 0; index < intList.Count; ++index)
            this.ChildObjectTypes.Add(ChildObjectType.CreateChildObjectType(intList[index]));
          List<ChildObjectType> childObjectTypeList2 = new List<ChildObjectType>();
          foreach (ChildObjectType childObjectType in this.ChildObjectTypes)
          {
            List<int> parentObjectTypeIds = MetaDataHelper.GetObjectTypeParentsID(childObjectType.ObjectTypeID);
            if (this.ChildObjectTypes.Any<ChildObjectType>((Func<ChildObjectType, bool>) (o => parentObjectTypeIds.Contains(o.ObjectTypeID))))
              childObjectTypeList2.Add(childObjectType);
          }
          foreach (ChildObjectType childObjectType in childObjectTypeList2)
            this.ChildObjectTypes.Remove(childObjectType);
          this.GenerateStartSortingValues();
        }
        catch
        {
          this.ChildObjectTypes.Clear();
        }
      }

      /// <summary>Создать точную копию коллекции</summary>
      /// <returns>Точная копия коллекции</returns>
      public object Clone()
      {
        if (this._childObjectTypes == null)
          this._childObjectTypes = new List<ChildObjectType>();
        ChildRelationType childRelationType = new ChildRelationType(this._relationTypeID, this._parentObjectTypeID, this._visible);
        List<ChildObjectType> childObjectTypes = childRelationType._childObjectTypes;
        if (childObjectTypes.Capacity < this._childObjectTypes.Count)
          childObjectTypes.Capacity = this._childObjectTypes.Count;
        foreach (ChildObjectType childObjectType in this._childObjectTypes)
          childObjectTypes.Add(childObjectType.Clone() as ChildObjectType);
        return (object) childRelationType;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0 или 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as ChildRelationType);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0 или 1</returns>
      public int CompareTo(ChildRelationType other)
      {
        if (other == null)
          return 1;
        int num = StringComparer.InvariantCultureIgnoreCase.Compare(MetaDataHelper.GetObjectTypeName(this._parentObjectTypeID), MetaDataHelper.GetObjectTypeName(other._parentObjectTypeID));
        if (num == 0)
          num = StringComparer.InvariantCultureIgnoreCase.Compare(MetaDataHelper.GetRelationTypeName(this._relationTypeID), MetaDataHelper.GetRelationTypeName(other._relationTypeID));
        return num;
      }
    }
}
