using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, описывыющий дочерний тип объектов для сервиса автоматической сортировки составов
    /// по типам объектов и связей
    /// </summary>
    [Serializable]
    public class ChildObjectType : 
      IChildObjectType,
      IXMLStoredClass,
      ICloneable,
      IComparable,
      IComparable<ChildObjectType>
    {
      /// <summary>
      /// Узел с настройками сортировки составов по типам объектов и связей
      /// </summary>
      [NonSerialized]
      public const string xmlNode_AutomaticSorting = "CompositionsAutomaticSorting";
      /// <summary>Узел с правилом сортировки состава</summary>
      [NonSerialized]
      public const string xmlNode_SortingRule = "SortingRule";
      /// <summary>Узел с родительским типом объектов</summary>
      [NonSerialized]
      public const string xmlNode_ParentType = "ParentObjectType";
      /// <summary>Узел с типом связи</summary>
      [NonSerialized]
      public const string xmlNode_RelationType = "RelationType";
      /// <summary>Узел с дочерним типом объектов</summary>
      [NonSerialized]
      public const string xmlNode_ChildrenObjectType = "ChildrenObjectType";
      /// <summary>Атрибут - Guid типа объекта, связи</summary>
      [NonSerialized]
      public const string xmlAttr_Guid = "Guid";
      /// <summary>Атрибут - Guid родительского типа объекта, связи</summary>
      [NonSerialized]
      public const string xmlAttr_ParentGuid = "ParentGuid";
      /// <summary>
      /// Атрибут - видим ли объект, связь, тип объекта, тип связи
      /// </summary>
      [NonSerialized]
      public const string xmlAttr_Visible = "Visible";
      [NonSerialized]
      public const string GroupingXmlAttributeName = "Grouping";
      /// <summary>Атрибут - является ли тип связи по умолчанию</summary>
      [NonSerialized]
      public const string xmlAttr_DefaultType = "DefaultType";
      /// <summary>Атрибут - Guid типа связи по умолчанию</summary>
      [NonSerialized]
      public const string xmlAttr_DefaultRelationType = "DefaultRelationType";
      /// <summary>
      /// Атрибут - Selections - разрешено ли отображать выборки и классификаторы (по умолчанию разрешено)
      /// </summary>
      [NonSerialized]
      public const string xmlAttr_Selections = "Selections";
      /// <summary>
      /// Атрибут - стартовое значение атрибута "Сортировка" для группы объектов одного типа
      /// </summary>
      [NonSerialized]
      public const string xmlAttr_Sorting = "Sorting";
      /// <summary>Атрибут - название правила сортировки составов</summary>
      [NonSerialized]
      public const string xmlAttr_Name = "Name";
      /// <summary>
      /// Атрибут - Guid правила сортировки составов по умолчанию
      /// </summary>
      [NonSerialized]
      public const string xmlAttr_Default = "Default";
      /// <summary>
      /// Приращение между стартовыми значениями атрибута "Сортировка" для разных дочерних типов объектов
      /// </summary>
      [NonSerialized]
      public const long SortingValueDelta = 1000000000;
      /// <summary>
      /// Приращение между стартовыми значениями атрибута "Сортировка" между объектами одного дочернего типа
      /// </summary>
      [NonSerialized]
      public const long SortingValueSmallDelta = 1000000;
      /// <summary>ID дочернего типа объекта</summary>
      protected int _objectTypeId;
      /// <summary>
      /// Стартовое значение атрибута "Сортировка" для группы объектов состава указанного типа
      /// </summary>
      protected long _startSortingValue;
      private List<ChildObjectType> _children;
      private bool _visible;
      private bool _grouping;

      /// <summary>Создать пустой экземпляр класса</summary>
      public ChildObjectType()
      {
        this._objectTypeId = -1;
        this._startSortingValue = 0L;
        this._children = new List<ChildObjectType>();
      }

      /// <summary>Создать описание дочернего типа объекта</summary>
      /// <param name="objectTypeId">ID дочернего типа объекта</param>
      /// <param name="startSortingValue">
      /// Стартовое значение атрибута "Сортировка" для группы объектов состава указанного типа
      /// </param>
      public ChildObjectType(int objectTypeId, long startSortingValue)
      {
        this._objectTypeId = objectTypeId;
        this._startSortingValue = startSortingValue;
        this._children = new List<ChildObjectType>();
      }

      public List<ChildObjectType> Children => this._children;

      public bool Visible
      {
        get => this._visible;
        set => this._visible = value;
      }

      public bool Grouping
      {
        get => this._grouping;
        set => this._grouping = value;
      }

      public IEnumerable<ChildObjectType> GetDescendants()
      {
        foreach (ChildObjectType childObjectType in this.Children)
        {
          yield return childObjectType;
          foreach (ChildObjectType descendant in childObjectType.GetDescendants())
            yield return descendant;
        }
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is ChildObjectType childObjectType) ? base.Equals(obj) : this._objectTypeId == childObjectType._objectTypeId;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this._objectTypeId.GetHashCode();

      /// <summary>ID дочернего типа объекта</summary>
      public virtual int ObjectTypeID
      {
        get => this._objectTypeId;
        set => this._objectTypeId = value;
      }

      /// <summary>
      /// Стартовое значение атрибута "Сортировка" для группы объектов состава указанного типа
      /// </summary>
      public virtual long StartSortingValue
      {
        get => this._startSortingValue;
        set => this._startSortingValue = value;
      }

      /// <summary>
      /// Очистить поля экземпляра класса.
      /// Информация об идентификаторе дочернего типа объекта сохраняется
      /// </summary>
      public virtual void Clear() => this.StartSortingValue = 0L;

      /// <summary>
      /// Загрузить информацию в текущий объект из указанного объекта
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public virtual void Assign(object source)
      {
        if (!(source is ChildObjectType childObjectType))
          return;
        this.ObjectTypeID = childObjectType.ObjectTypeID;
        this.StartSortingValue = childObjectType.StartSortingValue;
      }

      /// <summary>
      /// Загрузить описание дочернего типа объекта из указанного узла настроек
      /// </summary>
      /// <param name="storage">Хранилище настроек</param>
      /// <param name="node">Узел, из которого загружается информация</param>
      public virtual void Load(XMLSettingsStorage storage, XmlNode node)
      {
        if (storage == null || node == null)
          return;
        string attributeValue1 = storage.GetAttributeValue(node, "Guid", string.Empty);
        if (attributeValue1 == string.Empty)
          return;
        Guid objTypeGuid;
        try
        {
          objTypeGuid = new Guid(attributeValue1);
        }
        catch
        {
          return;
        }
        this._objectTypeId = MetaDataHelper.GetObjectTypeID(objTypeGuid);
        string attributeValue2 = storage.GetAttributeValue(node, "Sorting", string.Empty);
        if (attributeValue2 == string.Empty)
          return;
        if (!long.TryParse(attributeValue2, out this._startSortingValue))
          this._startSortingValue = -1L;
        XmlAttribute attribute1 = node.Attributes["Visible"];
        this._visible = attribute1 == null || bool.TrueString == attribute1.Value;
        XmlAttribute attribute2 = node.Attributes["Grouping"];
        this._grouping = attribute2 != null && bool.TrueString == attribute2.Value;
        List<ChildObjectType> source = new List<ChildObjectType>();
        foreach (XmlNode childNode in node.ChildNodes)
        {
          if (childNode.Name == "ChildrenObjectType")
          {
            ChildObjectType childObjectType = new ChildObjectType();
            childObjectType.Load(storage, childNode);
            source.Add(childObjectType);
          }
        }
        List<int> childrenObjectTypeIds = MetaDataHelper.GetObjectTypeChildrenID(this._objectTypeId);
        foreach (ChildObjectType childObjectType in source.Where<ChildObjectType>((Func<ChildObjectType, bool>) (o => !childrenObjectTypeIds.Contains(o.ObjectTypeID))).ToArray<ChildObjectType>())
          source.Remove(childObjectType);
        int[] savedChildrenObjectTypeIds = source.Select<ChildObjectType, int>((Func<ChildObjectType, int>) (o => o.ObjectTypeID)).ToArray<int>();
        foreach (int objectTypeId in childrenObjectTypeIds.Where<int>((Func<int, bool>) (o => !((IEnumerable<int>) savedChildrenObjectTypeIds).Contains<int>(o))).ToArray<int>())
        {
          ChildObjectType childObjectType = ChildObjectType.CreateChildObjectType(objectTypeId);
          source.Add(childObjectType);
        }
        this._children = source;
      }

      /// <summary>
      /// Сохранить описание дочернего типа объекта в родительский узел в XML-хранилище
      /// </summary>
      /// <param name="storage">Хранилище настроек</param>
      /// <param name="node">Родительский узел или null (тогда узел создаётся прямо в корневом узле документа XML)</param>
      public virtual void Save(XMLSettingsStorage storage, XmlNode node)
      {
        if (storage == null)
          return;
        node = node ?? (XmlNode) storage.document.DocumentElement;
        XmlNode nodeWithAttr1 = storage.FindNodeWithAttr(node, "ChildrenObjectType", "Guid", MetaDataHelper.GetObjectTypeGuid(this._objectTypeId).ToString(), true);
        node.RemoveChild(nodeWithAttr1);
        XmlNode nodeWithAttr2 = storage.FindNodeWithAttr(node, "ChildrenObjectType", "Guid", MetaDataHelper.GetObjectTypeGuid(this._objectTypeId).ToString(), true);
        storage.SetAttributeValue(nodeWithAttr2, "Guid", MetaDataHelper.GetObjectTypeGuid(this._objectTypeId).ToString());
        storage.SetAttributeValue(nodeWithAttr2, "Sorting", this._startSortingValue.ToString());
        storage.SetAttributeValue(nodeWithAttr2, "Visible", this._visible.ToString());
        storage.SetAttributeValue(nodeWithAttr2, "Grouping", this._grouping.ToString());
        foreach (ChildObjectType child in this._children)
          child.Save(storage, nodeWithAttr2);
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone()
      {
        ChildObjectType childObjectType = new ChildObjectType(this._objectTypeId, this._startSortingValue);
        childObjectType._visible = this._visible;
        childObjectType.Grouping = this.Grouping;
        int count = this._children.Count;
        List<ChildObjectType> children = childObjectType._children;
        if (children.Capacity < count)
          children.Capacity = count;
        for (int index = 0; index < count; ++index)
          children.Add((ChildObjectType) this._children[index].Clone());
        return (object) childObjectType;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0 или 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as ChildObjectType);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0 или 1</returns>
      public int CompareTo(ChildObjectType other)
      {
        return other == null ? 1 : StringComparer.InvariantCultureIgnoreCase.Compare(MetaDataHelper.GetObjectTypeName(this._objectTypeId), MetaDataHelper.GetObjectTypeName(other._objectTypeId));
      }

      public static ChildObjectType CreateChildObjectType(int objectTypeId)
      {
        ChildObjectType childObjectType1 = new ChildObjectType();
        childObjectType1._objectTypeId = objectTypeId;
        childObjectType1.Visible = true;
        foreach (IMSObjectType imsObjectType in MetaDataHelper.GetObjectTypeChildrenID(objectTypeId).Select<int, IMSObjectType>((Func<int, IMSObjectType>) (o => MetaDataHelper.GetObjectType(o))).OrderBy<IMSObjectType, string>((Func<IMSObjectType, string>) (o => o.ObjectTypeName)).ToArray<IMSObjectType>())
        {
          ChildObjectType childObjectType2 = ChildObjectType.CreateChildObjectType(imsObjectType.ObjectTypeID);
          childObjectType1.Children.Add(childObjectType2);
        }
        return childObjectType1;
      }
    }
}
