// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ReferenceToDBObjectBase
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Ссылка на объект базы данных из документа</summary>
[Serializable]
public class ReferenceToDBObjectBase : ReferenceBase, IEditableReferenceToObject
{
  /// <summary>Имя типа сохраняемое в XML</summary>
  public static string XmlTypeName = "RefToDB";
  protected internal RefToDBObjectType refType;
  protected internal DBObjectInfoBase dbObjectInfo;
  protected bool passiveLink = true;
  protected string linkAttributeName = "";
  protected int linkAttributeID = -1;
  protected Guid linkAttributeGuid = Guid.Empty;
  private readonly object syncRoot = new object();

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public static object EmptyConstructor() => (object) new ReferenceToDBObjectBase();

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public static object EmptyConstructorActiveLink()
  {
    return (object) new ReferenceToDBObjectBase()
    {
      passiveLink = false
    };
  }

  /// <summary>Коструктор</summary>
  public ReferenceToDBObjectBase()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public ReferenceToDBObjectBase(DocumentTreeNode ownerNode, bool passiveLink)
    : base(ownerNode)
  {
    this.passiveLink = passiveLink;
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="refType">Тип ссылки на объект БД</param>
  /// <param name="dbObjectInfo">Идентификаторы и информация об объекте</param>
  public ReferenceToDBObjectBase(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo)
    : this(ownerNode, refType, dbObjectInfo, true)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="refType">Тип ссылки на объект БД</param>
  /// <param name="dbObjectInfo">Идентификаторы и информация об объекте</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObjectBase(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    bool passiveLink)
    : base(ownerNode)
  {
    this.passiveLink = passiveLink;
    this.dbObjectInfo = dbObjectInfo;
    this.refType = refType;
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="dbObjectGuid">Глобальный идентификатор объекта</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObjectBase(DocumentTreeNode ownerNode, Guid dbObjectGuid, bool passiveLink)
    : base(ownerNode)
  {
    this.passiveLink = passiveLink;
    this.dbObjectInfo = (DBObjectInfoBase) new Intermech.Interfaces.Document.DBObjectInfo(dbObjectGuid);
    this.refType = RefToDBObjectType.rtSelectedObject;
  }

  /// <summary>Конструктор</summary>
  /// <param name="dbObjectGuid">Глобальный идентификатор объекта</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObjectBase(Guid dbObjectGuid, bool passiveLink)
    : this((DocumentTreeNode) null, dbObjectGuid, passiveLink)
  {
  }

  /// <summary>Значение получается у объекта документа по ссылке</summary>
  [Browsable(false)]
  public virtual bool IsReferenceFromDocumentAttribute
  {
    get
    {
      switch (this.refType)
      {
        case RefToDBObjectType.rtUseLinkFromDocumentObjectAttribute:
        case RefToDBObjectType.rtUseLinkFromDocumentObjectSign:
          return true;
        default:
          return false;
      }
    }
  }

  public virtual bool CanUpdateReference(UpdateReferencesMode mode) => true;

  /// <summary>Обновлять ссылки на объекты пакетно</summary>
  [Browsable(false)]
  public virtual bool IsUpdateDBObjectInfoBatch
  {
    get
    {
      if (this.PassiveLink)
        return false;
      switch (this.refType)
      {
        case RefToDBObjectType.rtSelectedObject:
        case RefToDBObjectType.rtUseLinkFromDocumentObjectAttribute:
        case RefToDBObjectType.rtUseSignFromObject:
        case RefToDBObjectType.rtUseLinkFromDocumentObjectSign:
          return true;
        default:
          return false;
      }
    }
  }

  /// <summary>Ссылка на связь БД</summary>
  [Browsable(false)]
  public virtual bool IsReferenceToRelation
  {
    [DebuggerStepThrough] get
    {
      switch (this.refType)
      {
        case RefToDBObjectType.rtSelectedObject:
        case RefToDBObjectType.rtUseParentObjectLink:
        case RefToDBObjectType.rtUseParentDocumentObjectLink:
        case RefToDBObjectType.rtUseLinkFromDocumentObjectAttribute:
          return false;
        case RefToDBObjectType.rtSelectedRelation:
        case RefToDBObjectType.rtUseParentRelationLink:
        case RefToDBObjectType.rtUseParentDocumentRelationLink:
          return true;
        default:
          return false;
      }
    }
  }

  /// <summary>Связь подключена. Есть ссылки на объекты</summary>
  public override bool IsConnected => this.IsConnectedObjectRef;

  /// <summary>Идентификатор объекта получен</summary>
  [Browsable(false)]
  public virtual bool IsConnectedObjectRef
  {
    [DebuggerStepThrough] get
    {
      if (!this.IsReferenceToRelation)
        return this.DBObjectID != -1L;
      return this.DBObjectID != -1L && this.DBRelationID != -1L;
    }
  }

  /// <summary>Пустая ссылка</summary>
  public override bool IsEmpty => this.IsEmptyObjectRef;

  /// <summary>Пустая ссылка на объект</summary>
  [Browsable(false)]
  public virtual bool IsEmptyObjectRef
  {
    [DebuggerStepThrough] get
    {
      return this.IsReferenceToRelation ? this.DBRelationGuid == Guid.Empty && this.DBRelationID == -1L : this.DBObjectGuid == Guid.Empty && this.DBObjectID == -1L;
    }
  }

  /// <summary>Ссылка пассивная. Если true, то не обновляет данные по ссылке при загрузке и не передаёт в базу при изменении</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_391")]
  [CustomDescription("Attribute.Interfaces.Document_392")]
  [CustomCategory("Attribute.Interfaces.Document_393")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool PassiveLink
  {
    [DebuggerStepThrough] get => this.passiveLink;
    set
    {
      if (this.passiveLink == value)
        return;
      this.passiveLink = value;
      if (this.OwnerNode == null)
        return;
      this.OwnerNode.overrideFlags2 |= OverrideFlags2.Reference;
      this.OwnerNode.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Обновить связь</summary>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="objAttrCache">Кэш атрибутов объектов</param>
  /// <param name="relAttrCache">Кэш атрибутов связей</param>
  /// <param name="forceUpdate">Обновлять даже для пассивных ссылок</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void UpdateLink(
    object userSession,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> relAttrCache,
    bool forceUpdate,
    bool updateUI,
    bool updateLayout)
  {
    this.UpdateLink(forceUpdate, updateUI, updateLayout);
  }

  /// <summary>Обновить связь</summary>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="forceUpdate">Обновлять даже для пассивных ссылок</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void UpdateLink(object userSession, bool forceUpdate, bool updateUI, bool updateLayout)
  {
    lock (this.syncRoot)
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      if (ownerDocument != null)
        this.UpdateLink(userSession, ownerDocument.ObjAttrCache, ownerDocument.RelAttrCache, forceUpdate, updateUI, updateLayout);
      else
        this.UpdateLink(userSession, (Dictionary<Guid, Dictionary<Guid, AttributeValueCache>>) null, (Dictionary<Guid, Dictionary<Guid, AttributeValueCache>>) null, forceUpdate, updateUI, updateLayout);
    }
  }

  public virtual void UpdateDBObjectInfo(object userSession, string filtrationSettings)
  {
  }

  /// <summary>Разорвать связь</summary>
  public override void DisconnectLink() => base.DisconnectLink();

  /// <summary>Копировать данные</summary>
  /// <param name="src">Источник данных</param>
  /// <param name="saveText">Сохранять данные</param>
  public override void CopyData(ReferenceBase src, bool copyText = true)
  {
    base.CopyData(src, copyText);
    if (!(src is ReferenceToDBObjectBase referenceToDbObjectBase))
      return;
    if (referenceToDbObjectBase.dbObjectInfo != null && (this.refType == RefToDBObjectType.rtSelectedObject || this.refType == RefToDBObjectType.rtSelectedRelation))
      this.dbObjectInfo = referenceToDbObjectBase.dbObjectInfo.Clone();
    this.refType = referenceToDbObjectBase.refType;
    this.passiveLink = referenceToDbObjectBase.passiveLink;
    this.linkAttributeName = referenceToDbObjectBase.linkAttributeName;
    this.linkAttributeGuid = referenceToDbObjectBase.linkAttributeGuid;
    this.linkAttributeID = referenceToDbObjectBase.linkAttributeID;
  }

  /// <summary>Тип ссылки на объект БД</summary>
  [Browsable(false)]
  public virtual RefToDBObjectType ReferenceType
  {
    [DebuggerStepThrough] get => this.refType;
  }

  /// <summary>Назначить тип ссылки</summary>
  /// <param name="value">Новый тип ссылки</param>
  public virtual void AssignReferenceType(RefToDBObjectType value) => this.refType = value;

  /// <summary>Ссылка зависит от документа. Т.е. при смене документа ее нужно обновлять.</summary>
  public override bool IsDependOnDocument
  {
    get
    {
      return this.refType == RefToDBObjectType.rtUseParentDocumentObjectLink || this.refType == RefToDBObjectType.rtUseParentDocumentRelationLink || this.refType == RefToDBObjectType.rtUseSignFromDocument;
    }
  }

  /// <summary>Ссылка зависит от родительского узла. Т.е. при смене родителя ее нужно обновлять.</summary>
  public override bool IsDependOnParent
  {
    get
    {
      return this.refType == RefToDBObjectType.rtUseParentObjectLink || this.refType == RefToDBObjectType.rtUseParentRelationLink;
    }
  }

  /// <summary>Загрузить ссылку из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public override void ReadFromXml(XmlReadArgs readArgs)
  {
    if (readArgs == null)
      throw new ArgumentNullException(nameof (readArgs));
    string localName = readArgs.Reader.LocalName;
    if (readArgs.Reader.HasAttributes)
    {
      int i = 0;
      for (int attributeCount = readArgs.Reader.AttributeCount; i < attributeCount; ++i)
      {
        readArgs.Reader.MoveToAttribute(i);
        if (!this.ReadFieldFromXml(readArgs))
          this.AddUnknownXmlAttribute(readArgs.Reader.LocalName, readArgs.Reader.Value);
      }
      readArgs.Reader.MoveToElement();
    }
    bool flag = readArgs.Reader.IsEmptyElement;
    while (!flag && (readArgs.SkipRead || readArgs.Reader.Read()))
    {
      readArgs.SkipRead = false;
      switch (readArgs.Reader.NodeType)
      {
        case XmlNodeType.Element:
          if (!this.ReadFieldFromXml(readArgs))
          {
            this.UnknownXmlElements += readArgs.Reader.ReadOuterXml();
            readArgs.SkipRead = true;
            continue;
          }
          continue;
        case XmlNodeType.EndElement:
          if (localName == readArgs.Reader.LocalName)
          {
            flag = true;
            continue;
          }
          continue;
        default:
          continue;
      }
    }
    if (flag)
      return;
    LogManager.AddLine("ReferenceToDBObjectBase.ReadFromXml End Element not found.");
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (base.ReadFieldFromXml(readArgs))
      return true;
    switch (readArgs.Reader.LocalName)
    {
      case "baseType":
        return true;
      case "dbObjectGuid":
      case "objGuid":
        if (this.dbObjectInfo == null)
          this.dbObjectInfo = !this.IsReferenceToRelation ? (DBObjectInfoBase) new Intermech.Interfaces.Document.DBObjectInfo() : (DBObjectInfoBase) new DBRelationInfo();
        this.dbObjectInfo.SetDBObjectInfo(new Guid(readArgs.Reader.Value), -1L, -1, (string) null);
        return true;
      case "dbRelationGuid":
      case "relGuid":
        Guid relationGuid = new Guid(readArgs.Reader.Value);
        if (this.dbObjectInfo == null)
          this.dbObjectInfo = (DBObjectInfoBase) new DBRelationInfo(relationGuid, -1L, -1, Guid.Empty, -1L, Guid.Empty, -1L, -1, (string) null);
        else if (this.dbObjectInfo is DBRelationInfo)
          this.dbObjectInfo.AssignRelationGuid(relationGuid);
        else
          this.dbObjectInfo = (DBObjectInfoBase) new DBRelationInfo(relationGuid, -1L, -1, this.dbObjectInfo.ProjGuid, this.dbObjectInfo.ProjID, this.dbObjectInfo.ObjectGuid, this.dbObjectInfo.ObjectID, this.dbObjectInfo.ObjectType, this.dbObjectInfo.ObjectCaption);
        return true;
      case "linkAttrGuid":
        this.linkAttributeGuid = new Guid(readArgs.Reader.Value);
        return true;
      case "passive":
        this.passiveLink = readArgs.Reader.Value == "1";
        return true;
      case "passiveLink":
        this.passiveLink = bool.Parse(readArgs.Reader.Value);
        return true;
      case "projGuid":
        Guid projGuid = new Guid(readArgs.Reader.Value);
        if (this.dbObjectInfo == null)
          this.dbObjectInfo = (DBObjectInfoBase) new DBRelationInfo(Guid.Empty, -1L, -1, projGuid, -1L, Guid.Empty, -1L, -1, (string) null);
        else if (this.dbObjectInfo is DBRelationInfo)
          this.dbObjectInfo.AssignProjGuid(projGuid);
        else
          this.dbObjectInfo = (DBObjectInfoBase) new DBRelationInfo(this.dbObjectInfo.RelationGuid, this.dbObjectInfo.RelationID, this.dbObjectInfo.RelationType, projGuid, -1L, this.dbObjectInfo.ObjectGuid, this.dbObjectInfo.ObjectID, this.dbObjectInfo.ObjectType, this.dbObjectInfo.ObjectCaption);
        return true;
      case "refType":
        string s = readArgs.Reader.Value;
        if (readArgs.Version < 14)
        {
          switch (s)
          {
            case "rtUseParentLink":
              s = "rtUseParentObjectLink";
              break;
            case "rtUseParentDocumentLink":
              s = "rtUseParentDocumentObjectLink";
              break;
          }
        }
        if (readArgs.Version < 18)
        {
          this.refType = (RefToDBObjectType) Enum.Parse(typeof (RefToDBObjectType), s);
        }
        else
        {
          int result = 0;
          this.refType = !int.TryParse(s, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result) ? (RefToDBObjectType) Enum.Parse(typeof (RefToDBObjectType), s) : (RefToDBObjectType) result;
        }
        return true;
      case "type":
        return true;
      default:
        return false;
    }
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    xw.WriteAttributeString("refType", ((int) this.refType).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.refType == RefToDBObjectType.rtSelectedObject || this.refType == RefToDBObjectType.rtUseSignFromObject)
    {
      Guid dbObjectGuid = this.DBObjectGuid;
      if (dbObjectGuid != Guid.Empty)
        xw.WriteAttributeString("objGuid", dbObjectGuid.ToString());
    }
    else if (this.refType == RefToDBObjectType.rtSelectedRelation)
    {
      Guid dbRelationGuid = this.DBRelationGuid;
      if (dbRelationGuid != Guid.Empty)
        xw.WriteAttributeString("relGuid", dbRelationGuid.ToString());
      Guid dbObjectGuid = this.DBObjectGuid;
      if (dbObjectGuid != Guid.Empty)
        xw.WriteAttributeString("objGuid", dbObjectGuid.ToString());
    }
    if ((this.refType == RefToDBObjectType.rtUseLinkFromDocumentObjectAttribute || this.refType == RefToDBObjectType.rtUseLinkFromDocumentObjectSign) && this.linkAttributeGuid != Guid.Empty)
      xw.WriteAttributeString("linkAttrGuid", this.linkAttributeGuid.ToString());
    if (this.passiveLink)
      return;
    xw.WriteAttributeString("passive", "0");
  }

  /// <summary>Имя базового типа ссылки для хранения в XML.
  /// Этот тип используется если TypeNameForXml не найден</summary>
  protected override string BaseTypeNameForXml => ReferenceToDBObjectBase.XmlTypeName;

  /// <summary>Имя типа сохраняемое в XML</summary>
  public override string TypeNameForXml => ReferenceToDBObjectBase.XmlTypeName;

  /// <summary>Установить заданный подтип ссылки</summary>
  /// <param name="owner">Владелец ссылки</param>
  /// <param name="subType">Имя подтипа ссылки</param>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  public virtual void SetReferenceSubType(
    DocumentTreeNode owner,
    string subType,
    Type refInterface)
  {
    string[] referenceSubTypes = this.GetReferenceSubTypes(owner, refInterface);
    int num = -1;
    if (referenceSubTypes != null && referenceSubTypes.Length != 0)
      num = Array.IndexOf<string>(referenceSubTypes, subType);
    switch (num)
    {
      case -1:
        throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_96"), "subType = " + subType);
      case 0:
        this.refType = RefToDBObjectType.rtSelectedObject;
        if (this.dbObjectInfo != null)
        {
          this.dbObjectInfo = (DBObjectInfoBase) new Intermech.Interfaces.Document.DBObjectInfo(this.dbObjectInfo.ObjectGuid, this.dbObjectInfo.ObjectID, this.dbObjectInfo.ObjectType, this.dbObjectInfo.ObjectCaption);
          break;
        }
        break;
      case 1:
        this.refType = RefToDBObjectType.rtUseParentDocumentObjectLink;
        this.GetParentDBObjectInfo();
        break;
      case 2:
        this.refType = RefToDBObjectType.rtUseParentObjectLink;
        this.GetParentDBObjectInfo();
        break;
      case 3:
        this.refType = RefToDBObjectType.rtUseParentRelationLink;
        this.GetParentDBObjectInfo();
        break;
      case 4:
        this.refType = RefToDBObjectType.rtSelectedRelation;
        if (this.dbObjectInfo is Intermech.Interfaces.Document.DBObjectInfo)
        {
          this.dbObjectInfo = (DBObjectInfoBase) new DBRelationInfo();
          break;
        }
        if (this.dbObjectInfo != null)
        {
          this.dbObjectInfo = this.dbObjectInfo.Clone();
          break;
        }
        break;
    }
    this.UpdateLink(true, true);
  }

  /// <summary>Получить индекс текущего подтипа ссылки</summary>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  /// <returns>Индекс текущего подтипа ссылки</returns>
  public virtual int GetReferenceSubTypeIndex(Type refInterface)
  {
    switch (this.refType)
    {
      case RefToDBObjectType.rtSelectedObject:
        return 0;
      case RefToDBObjectType.rtUseParentObjectLink:
        return 2;
      case RefToDBObjectType.rtUseParentDocumentObjectLink:
        return 1;
      case RefToDBObjectType.rtSelectedRelation:
        return 4;
      case RefToDBObjectType.rtUseParentRelationLink:
        return 3;
      case RefToDBObjectType.rtUseLinkFromDocumentObjectAttribute:
        return 5;
      default:
        return -1;
    }
  }

  /// <summary>Получить подтипы ссылки</summary>
  /// <param name="owner">Владелец ссылки</param>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  /// <returns>Массив имен подтипов ссылки. Имена должны быть уникальными в пределах одного типа ссылки</returns>
  public virtual string[] GetReferenceSubTypes(DocumentTreeNode owner, Type refInterface)
  {
    if (!(refInterface == typeof (IEditableReferenceToObject)))
      return (string[]) null;
    return new string[6]
    {
      LocalizationHolder.rm.GetString("Interfaces.Document_97"),
      LocalizationHolder.rm.GetString("Interfaces.Document_99"),
      LocalizationHolder.rm.GetString("Interfaces.Document_98"),
      LocalizationHolder.rm.GetString("Interfaces.Document_101"),
      LocalizationHolder.rm.GetString("Interfaces.Document_100"),
      LocalizationHolder.rm.GetString("Interfaces.Document_186")
    };
  }

  /// <summary>Имя объекта с которым связана ссылка. Если объект не найден, то null</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_394")]
  [CustomDescription("Attribute.Interfaces.Document_395")]
  [CustomCategory("Attribute.Interfaces.Document_396")]
  public virtual string ObjectCaption
  {
    [DebuggerStepThrough] get
    {
      return this.dbObjectInfo != null ? this.dbObjectInfo.ObjectCaption : (string) null;
    }
  }

  /// <summary>Вызвать диалог выбора объекта для ссылки</summary>
  public virtual void CallSelectObjectDialog()
  {
  }

  /// <summary>Можно ли вызвать диалог выбора объекта для ссылки</summary>
  [Browsable(false)]
  public virtual bool CanCallSelectObjectDialog
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Можно вызвать диалог выбора ссылочного атрибута</summary>
  [Browsable(false)]
  public virtual bool CanCallSelectLinkAttributeDialog => false;

  /// <summary>Вызвать диалог выбора ссылочного атрибута</summary>
  public virtual void CallSelectLinkAttributeDialog()
  {
  }

  /// <summary>Используется ссылочный атрибут</summary>
  [Browsable(false)]
  public virtual bool UseLinkAttribute
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Имя ссылочного атрибута</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_571")]
  [CustomDescription("Attribute.Interfaces.Document_572")]
  [CustomCategory("Attribute.Interfaces.Document_396")]
  [Browsable(false)]
  public virtual string LinkAttributeName
  {
    get => this.linkAttributeName;
    set => this.linkAttributeName = value;
  }

  /// <summary>Guid ссылочного атрибута</summary>
  [Browsable(false)]
  public virtual Guid LinkAttributeGuid
  {
    get => this.linkAttributeGuid;
    set => this.linkAttributeGuid = value;
  }

  /// <summary>ID ссылочного атрибута</summary>
  [Browsable(false)]
  public virtual int LinkAttributeID
  {
    get => this.linkAttributeID;
    set => this.linkAttributeID = value;
  }

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public virtual string[] GetLinkAttributeNameList() => (string[]) null;

  /// <summary>Назначить новое значение DBObjectInfo</summary>
  /// <param name="value">Значение</param>
  /// <param name="setOverrideFlag">Устанавливать флаг перекрытия шаблона</param>
  public virtual void AssignDBObjectInfo(DBObjectInfoBase value, bool setOverrideFlag)
  {
    if (this.dbObjectInfo == value)
      return;
    this.dbObjectInfo = value;
    switch (this.refType)
    {
      case RefToDBObjectType.rtUseParentObjectLink:
      case RefToDBObjectType.rtUseParentRelationLink:
        if (this.OwnerNode == null || !(this.OwnerNode.Parent is INodeWithReference parent) || !(parent.Reference is ReferenceToDBObjectBase reference1) || reference1.dbObjectInfo == value)
          break;
        reference1.AssignDBObjectInfo(value, setOverrideFlag);
        break;
      case RefToDBObjectType.rtUseParentDocumentObjectLink:
      case RefToDBObjectType.rtUseParentDocumentRelationLink:
        INodeWithReference nodeWithReference = (INodeWithReference) null;
        if (this.OwnerNode is IDocumentElement ownerNode)
          nodeWithReference = (INodeWithReference) ownerNode.OwnerDocument;
        if (nodeWithReference == null || !(nodeWithReference.Reference is ReferenceToDBObjectBase reference2) || reference2.dbObjectInfo == value)
          break;
        reference2.AssignDBObjectInfo(value, setOverrideFlag);
        break;
      default:
        if (!setOverrideFlag || this.OwnerNode == null)
          break;
        this.OwnerNode.overrideFlags2 |= OverrideFlags2.Reference;
        break;
    }
  }

  public virtual void GetParentDBObjectInfo()
  {
  }

  /// <summary>Получить данные об объектах родителя</summary>
  public void GetParentDBObjectInfo(IUserSession session)
  {
    this.GetParentDBObjectInfo(session, this.OwnerNode);
  }

  /// <summary>Получить данные об объектах родителя</summary>
  /// <param name="owner">Узел владелец</param>
  public virtual void GetParentDBObjectInfo(IUserSession session, DocumentTreeNode owner)
  {
    switch (this.refType)
    {
      case RefToDBObjectType.rtUseParentObjectLink:
      case RefToDBObjectType.rtUseParentRelationLink:
        if (owner == null)
          break;
        if (owner.Parent is INodeWithReference parent && parent.Reference is ReferenceToDBObjectBase reference1)
        {
          if (reference1.dbObjectInfo == null)
            reference1.GetParentDBObjectInfo(session);
          this.dbObjectInfo = reference1.dbObjectInfo;
          break;
        }
        this.dbObjectInfo = (DBObjectInfoBase) null;
        break;
      case RefToDBObjectType.rtUseParentDocumentObjectLink:
      case RefToDBObjectType.rtUseParentDocumentRelationLink:
      case RefToDBObjectType.rtUseSignFromDocument:
        INodeWithReference nodeWithReference = (INodeWithReference) null;
        if (owner is IDocumentElement documentElement)
          nodeWithReference = (INodeWithReference) documentElement.OwnerDocument;
        if (nodeWithReference != null && nodeWithReference.Reference is ReferenceToDBObjectBase reference2)
        {
          if (reference2.dbObjectInfo == null)
            reference2.GetParentDBObjectInfo();
          this.dbObjectInfo = reference2.dbObjectInfo;
          break;
        }
        this.dbObjectInfo = (DBObjectInfoBase) null;
        break;
    }
  }

  protected bool IsSuspendedUpdatesFromDB
  {
    get => this.OwnerDocument != null && this.OwnerDocument.IsSuspendedUpdatesFromDB;
  }

  /// <summary>Данные объекта БД. Идентификаторы, гуиды, типы</summary>
  [Browsable(false)]
  public virtual DBObjectInfoBase DBObjectInfo
  {
    [DebuggerStepThrough] get => this.dbObjectInfo;
  }

  /// <summary>Глобальный идентификатор версии объекта БД</summary>
  [Browsable(false)]
  public virtual Guid DBObjectGuid
  {
    [DebuggerStepThrough] get
    {
      return this.dbObjectInfo != null ? this.dbObjectInfo.ObjectGuid : Guid.Empty;
    }
  }

  /// <summary>Идентификатор версии объекта БД</summary>
  [Browsable(false)]
  public virtual long DBObjectID
  {
    [DebuggerStepThrough] get => this.dbObjectInfo != null ? this.dbObjectInfo.ObjectID : -1L;
  }

  /// <summary>Тип объекта БД</summary>
  [Browsable(false)]
  public virtual int DBObjectType
  {
    [DebuggerStepThrough] get => this.dbObjectInfo != null ? this.dbObjectInfo.ObjectType : -1;
  }

  /// <summary>Заголовок объекта БД</summary>
  [Browsable(false)]
  public string DBObjectCaption
  {
    [DebuggerStepThrough] get
    {
      return this.dbObjectInfo != null ? this.dbObjectInfo.ObjectCaption : (string) null;
    }
  }

  /// <summary>Глобальный идентификатор связи БД</summary>
  [Browsable(false)]
  public virtual Guid DBRelationGuid
  {
    [DebuggerStepThrough] get
    {
      return this.dbObjectInfo != null ? this.dbObjectInfo.RelationGuid : Guid.Empty;
    }
  }

  /// <summary>Идентификатор связи БД</summary>
  [Browsable(false)]
  public virtual long DBRelationID
  {
    [DebuggerStepThrough] get => this.dbObjectInfo != null ? this.dbObjectInfo.RelationID : -1L;
  }

  /// <summary>Тип связи БД</summary>
  [Browsable(false)]
  public virtual int DBRelationType
  {
    [DebuggerStepThrough] get => this.dbObjectInfo != null ? this.dbObjectInfo.RelationType : -1;
  }

  /// <summary>Глобальный идентификатор версии объекта проекта БД</summary>
  [Browsable(false)]
  public virtual Guid DBProjectGuid
  {
    [DebuggerStepThrough] get
    {
      return this.dbObjectInfo != null ? this.dbObjectInfo.ProjGuid : Guid.Empty;
    }
  }

  /// <summary>Идентификатор версии объекта проекта БД</summary>
  [Browsable(false)]
  public virtual long DBProjectID
  {
    [DebuggerStepThrough] get => this.dbObjectInfo != null ? this.dbObjectInfo.ProjID : -1L;
  }
}
