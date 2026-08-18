// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ReferenceToDBObjectAttributeBase
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

/// <summary>Ссылка на атрибут объекта в БД</summary>
[Serializable]
public class ReferenceToDBObjectAttributeBase : 
  ReferenceToDBObjectBase,
  IEditableReferenceToTextSource,
  IEditableReferenceToObject,
  ITextSource
{
  [NonSerialized]
  private TextChanged_EventHandler textChanged;
  /// <summary>Имя типа сохраняемое в XML</summary>
  public new static string XmlTypeName = "RefToDBAttr";
  protected int attributeID = -1;
  protected Guid attributeGuid = Guid.Empty;
  protected string attributeName;
  protected string attributeValue;
  protected bool? readOnlyAttr = new bool?(false);
  protected long attributeLinkObjectID = -1;

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructor() => (object) new ReferenceToDBObjectAttributeBase();

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructorActiveLink()
  {
    ReferenceToDBObjectAttributeBase objectAttributeBase = new ReferenceToDBObjectAttributeBase();
    objectAttributeBase.passiveLink = false;
    return (object) objectAttributeBase;
  }

  /// <summary>Коструктор</summary>
  public ReferenceToDBObjectAttributeBase()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObjectAttributeBase(DocumentTreeNode ownerNode, bool passiveLink)
    : base(ownerNode, passiveLink)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="refType">Тип ссылки</param>
  /// <param name="dbObjectInfo">Идентификатор объекта</param>
  /// <param name="attrGuid">Глобальный идентификатор атрибута</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="attrName">Имя атрибута</param>
  public ReferenceToDBObjectAttributeBase(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    Guid attrGuid,
    int attrID,
    string attrName)
    : this(ownerNode, refType, dbObjectInfo, attrGuid, attrID, attrName, true)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="refType">Тип ссылки</param>
  /// <param name="dbObjectInfo">Идентификатор объекта</param>
  /// <param name="attrGuid">Глобальный идентификатор атрибута</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="attrName">Имя атрибута</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObjectAttributeBase(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    Guid attrGuid,
    int attrID,
    string attrName,
    bool passiveLink)
    : base(ownerNode, refType, dbObjectInfo, passiveLink)
  {
    this.AssignAttributeInfo(attrGuid, attrID, attrName);
  }

  /// <summary>Назначить информацию об атрибуте</summary>
  /// <param name="attrGuid">Глобальный идентификатор атрибута</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="attrName">Наименование атрибута</param>
  public void AssignAttributeInfo(Guid attrGuid, int attrID, string attrName)
  {
    if (this.OwnerNode != null && (this.attributeGuid != attrGuid || this.attributeGuid == Guid.Empty && (this.attributeID != attrID || this.attributeName != attrName)))
      this.OwnerNode.overrideFlags2 |= OverrideFlags2.Reference;
    this.attributeGuid = attrGuid;
    this.attributeID = attrID;
    this.attributeName = attrName;
  }

  /// <summary>Копировать данные</summary>
  /// <param name="src">Источник данных</param>
  /// <param name="saveText">Сохранять данные</param>
  public override void CopyData(ReferenceBase src, bool copyText = true)
  {
    base.CopyData(src, copyText);
    if (!(src is ReferenceToDBObjectAttributeBase objectAttributeBase))
      return;
    this.attributeName = objectAttributeBase.attributeName;
    this.attributeGuid = objectAttributeBase.attributeGuid;
    this.attributeID = objectAttributeBase.attributeID;
    if (copyText)
      this.attributeValue = objectAttributeBase.attributeValue;
    this.readOnlyAttr = objectAttributeBase.readOnlyAttr;
    this.attributeLinkObjectID = objectAttributeBase.attributeLinkObjectID;
  }

  /// <summary>Разорвать связь</summary>
  public override void DisconnectLink() => base.DisconnectLink();

  /// <summary>Только для чтения</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_379")]
  [CustomDescription("Attribute.Interfaces.Document_380")]
  [CustomCategory("Attribute.Interfaces.Document_381")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool ReadOnly
  {
    [DebuggerStepThrough] get => this.readOnlyAttr.HasValue && this.readOnlyAttr.Value;
  }

  /// <summary>Атрибут связи</summary>
  [Browsable(false)]
  public virtual bool IsRelationAttribute
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

  /// <summary>Можно редактировать по месту. Для ссылок на атрибуты</summary>
  public override bool CanInplaceEdit => false;

  /// <summary>Возможен вызов дополнительного редактора для элемента</summary>
  public override bool CanCallEditor => false;

  /// <summary>Идентификатор атрибута</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_382")]
  [CustomDescription("Attribute.Interfaces.Document_383")]
  [CustomCategory("Attribute.Interfaces.Document_384")]
  public int AttributeID
  {
    [DebuggerStepThrough] get => this.attributeID;
  }

  /// <summary>Получить идентификатор атрибута, при необходимости обновить идентификатор если есть GUID</summary>
  /// <returns></returns>
  public int GetOrUpdateAttributeID()
  {
    if (this.AttributeID != -1 || this.AttributeGuid == Guid.Empty)
      return this.AttributeID;
    this.UpdateAttributeInfo();
    return this.AttributeID;
  }

  /// <summary>Глобальный идентификатор атрибута</summary>
  [Browsable(false)]
  public Guid AttributeGuid
  {
    [DebuggerStepThrough] get => this.attributeGuid;
  }

  /// <summary>Идентификатор атрибута получен</summary>
  [Browsable(false)]
  public virtual bool IsConnectedAttributeRef
  {
    [DebuggerStepThrough] get => this.attributeID != -1 && this.IsConnectedObjectRef;
  }

  /// <summary>Идентификатор объекта получен</summary>
  public override bool IsConnectedObjectRef
  {
    get
    {
      return !this.IsRelationAttribute ? this.DBObjectID != -1L && this.DBObjectGuid != Guid.Empty : this.DBRelationID != -1L && this.DBRelationGuid != Guid.Empty;
    }
  }

  /// <summary>Связь подключена. Есть ссылки на объекты</summary>
  public override bool IsConnected => this.IsConnectedAttributeRef;

  /// <summary>Пустая ссылка на атрибут. Нет идентификаторов атрибута</summary>
  [Browsable(false)]
  public virtual bool IsEmptyAttributeRef
  {
    [DebuggerStepThrough] get
    {
      if (this.attributeGuid == Guid.Empty && (this.attributeName == null || this.attributeName == "") && this.attributeID == -1)
        return true;
      switch (this.refType)
      {
        case RefToDBObjectType.rtSelectedObject:
          return this.IsRelationAttribute || this.IsEmptyObjectRef;
        case RefToDBObjectType.rtUseParentObjectLink:
        case RefToDBObjectType.rtUseParentDocumentObjectLink:
        case RefToDBObjectType.rtUseParentRelationLink:
        case RefToDBObjectType.rtUseParentDocumentRelationLink:
        case RefToDBObjectType.rtUseLinkFromDocumentObjectAttribute:
          return this.IsRelationAttribute ? this.DBRelationGuid == Guid.Empty && this.DBRelationID != -1L : this.DBObjectGuid == Guid.Empty && this.DBObjectID != -1L;
        case RefToDBObjectType.rtSelectedRelation:
          return !this.IsRelationAttribute || this.IsEmptyObjectRef;
        default:
          return true;
      }
    }
  }

  /// <summary>Пустая ссылка</summary>
  public override bool IsEmpty => this.IsEmptyAttributeRef;

  /// <summary>Необходимо сохранять в файл текстовое значение полученное по ссылке</summary>
  public override bool NeedSaveTextValueToFile
  {
    get
    {
      if (string.IsNullOrEmpty(this.attributeValue))
        return false;
      if (this.PassiveLink)
        return true;
      return this.OwnerDocument != null && this.OwnerDocument.SaveValueFromRefToDBAttr;
    }
  }

  /// <summary>Необходимо сохранять в кэш текстовое значение полученное по ссылке</summary>
  public override bool NeedSaveTextValueToCache => true;

  public long LinkAttributeObjectID => this.attributeLinkObjectID;

  /// <summary>Ссылка на атрибут объекта</summary>
  [Browsable(false)]
  public virtual bool IsReferenceToAttribute
  {
    [DebuggerStepThrough] get => true;
  }

  /// <summary>Можно вызвать диалог выбора атрибута для ссылки</summary>
  [Browsable(false)]
  public virtual bool CanCallSelectAttributeDialog
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public virtual string[] GetAttributeNameList() => (string[]) null;

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public override string[] GetLinkAttributeNameList() => (string[]) null;

  /// <summary>Вызвать диалог выбора атрибута для ссылки</summary>
  public virtual void CallSelectAttributeDialog()
  {
  }

  /// <summary>Обновить информацию об атрибуте. Имеет смысл для ссылок на атрибуты объектов БД.</summary>
  public virtual void UpdateAttributeInfo()
  {
  }

  /// <summary>Имя атрибута</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_385")]
  [CustomDescription("Attribute.Interfaces.Document_386")]
  [CustomCategory("Attribute.Interfaces.Document_387")]
  public virtual string AttributeName
  {
    [DebuggerStepThrough] get => this.attributeName;
    set
    {
      if (!(this.attributeName != value))
        return;
      if (this.OwnerNode != null && this.attributeGuid == Guid.Empty)
      {
        this.OwnerNode.overrideFlags2 |= OverrideFlags2.Reference;
        this.OwnerNode.OnChanged(new Changed_EventArgs());
      }
      this.attributeName = value;
      this.attributeGuid = Guid.Empty;
      this.attributeID = -1;
      this.UpdateAttributeInfo();
      this.UpdateLink(true, true);
    }
  }

  /// <summary>Установить заданный подтип ссылки</summary>
  /// <param name="owner">Владелец ссылки</param>
  /// <param name="subType">Имя подтипа ссылки</param>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  public override void SetReferenceSubType(
    DocumentTreeNode owner,
    string subType,
    Type refInterface)
  {
    string[] referenceSubTypes = this.GetReferenceSubTypes(owner, refInterface);
    int num = -1;
    if (referenceSubTypes != null && referenceSubTypes.Length != 0)
      num = Array.IndexOf<string>(referenceSubTypes, subType);
    if (num == -1)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_88"), "subType = " + subType);
    int refType = (int) this.refType;
    switch (num)
    {
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
      case 5:
        this.refType = RefToDBObjectType.rtUseLinkFromDocumentObjectAttribute;
        this.dbObjectInfo = (DBObjectInfoBase) null;
        break;
    }
    if (this.OwnerNode != null)
      this.OwnerNode.overrideFlags2 |= OverrideFlags2.Reference;
    this.UpdateLink(false, false);
  }

  /// <summary>Получить индекс текущего подтипа ссылки</summary>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  /// <returns>Индекс текущего подтипа ссылки</returns>
  public override int GetReferenceSubTypeIndex(Type refInterface)
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
      case RefToDBObjectType.rtUseParentDocumentRelationLink:
        return 0;
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
  public override string[] GetReferenceSubTypes(DocumentTreeNode owner, Type refInterface)
  {
    if (!(refInterface == typeof (IEditableReferenceToTextSource)))
      return (string[]) null;
    return new string[6]
    {
      LocalizationHolder.rm.GetString("Interfaces.Document_89"),
      LocalizationHolder.rm.GetString("Interfaces.Document_91"),
      LocalizationHolder.rm.GetString("Interfaces.Document_90"),
      LocalizationHolder.rm.GetString("Interfaces.Document_94"),
      LocalizationHolder.rm.GetString("Interfaces.Document_92"),
      LocalizationHolder.rm.GetString("Interfaces.Document_188")
    };
  }

  /// <summary>Текст</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_388")]
  [CustomDescription("Attribute.Interfaces.Document_389")]
  [CustomCategory("Attribute.Interfaces.Document_390")]
  public string Text
  {
    [DebuggerStepThrough] get => !this.CanShowReference() ? "" : this.attributeValue;
    set => this.SetText(value, true, true, true, true, true);
  }

  /// <summary>Получить текст с защитой от циклических ссылок</summary>
  /// <param name="callChain">Цепочка вызовов</param>
  /// <returns></returns>
  public string GetAcyclicText(List<DocumentTreeNode> callChain) => this.Text;

  /// <summary>Назначить значение Text</summary>
  /// <param name="value">Значение</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить изображение</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetText(string value, bool saveUndo, bool updateUI, bool updateLayout)
  {
    this.SetText(value, true, true, true, updateUI, updateLayout);
  }

  /// <summary>Назначить значение Text</summary>
  /// <param name="value">Значение</param>
  /// <param name="saveToDB">Заносить значение атрибута в саму БД</param>
  /// <param name="fireTextChanged">Вызывать обработчики события TextChanged</param>
  /// <param name="updateOwner">Генерировать событие в элементе владельце</param>
  /// <param name="updateUI">Обновить изображение</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetText(
    string value,
    bool saveToDB,
    bool fireTextChanged,
    bool updateOwner,
    bool updateUI,
    bool updateLayout)
  {
    if (!(this.attributeValue != value))
      return;
    string attributeValue = this.attributeValue;
    this.attributeValue = value;
    if (!fireTextChanged)
      return;
    this.OnTextChanged(attributeValue, this.attributeValue, updateOwner, false, updateUI, updateLayout);
  }

  /// <summary>Присвоить значение переменной Text без вызова обработчиков. Для внутреннего пользования!</summary>
  /// <param name="value">Значение</param>
  public void AssignText(string value)
  {
    if (!(this.attributeValue != value))
      return;
    this.attributeValue = value;
  }

  /// <summary>Событие Текст изменен</summary>
  public event TextChanged_EventHandler TextChanged
  {
    add => this.textChanged += value;
    remove => this.textChanged -= value;
  }

  /// <summary>Генерирует событие TextChanged</summary>
  /// <param name="oldText">Старое значение</param>
  /// <param name="newText">Новое значение</param>
  /// <param name="updateOwner">Генерировать событие в элементе владельце</param>
  /// <param name="saveModificationDate">Изменения не влияющие на дату модификации документа</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void OnTextChanged(
    string oldText,
    string newText,
    bool updateOwner,
    bool saveModificationDate,
    bool updateUI,
    bool updateLayout)
  {
    this.OnTextChanged(updateOwner, new TextChanged_EventArgs(oldText, newText, true, true, saveModificationDate, updateUI, updateLayout));
  }

  /// <summary>Вызывает событие TextChanged</summary>
  /// <param name="updateOwner">Генерировать событие в элементе владельце</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnTextChanged(bool updateOwner, TextChanged_EventArgs e)
  {
    if (updateOwner && this.OwnerNode is TextData ownerNode)
      ownerNode.OnTextChanged(e);
    if (this.textChanged == null)
      return;
    this.textChanged((object) this, e);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "ObjectLinkID":
        long result;
        if (!readArgs.Reader.IsEmptyElement && long.TryParse(readArgs.Reader.Value, out result))
          this.attributeLinkObjectID = result;
        return true;
      case "Text":
        if (!readArgs.Reader.IsEmptyElement)
        {
          if (!readArgs.Reader.HasValue)
            readArgs.Reader.Read();
          if (readArgs.Reader.NodeType == XmlNodeType.Text || readArgs.Reader.NodeType == XmlNodeType.Whitespace)
            this.attributeValue = readArgs.Reader.Value;
        }
        return true;
      case "attrGuid":
        this.attributeGuid = new Guid(readArgs.Reader.Value);
        return true;
      case "attrName":
      case "attributeName":
        this.attributeName = readArgs.Reader.Value;
        return true;
      case "dbObjectGuid":
      case "objGuid":
        if (this.dbObjectInfo == null)
        {
          if (this.IsReferenceToRelation)
            this.dbObjectInfo = (DBObjectInfoBase) new DBRelationInfo();
          else
            this.dbObjectInfo = (DBObjectInfoBase) new Intermech.Interfaces.Document.DBObjectInfo();
        }
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
      case "passive":
      case "passiveLink":
        if (readArgs.Version < 18)
          this.passiveLink = bool.Parse(readArgs.Reader.Value);
        else
          this.passiveLink = readArgs.Reader.Value == "1";
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
          this.refType = (RefToDBObjectType) Enum.Parse(typeof (RefToDBObjectType), s);
        else
          this.refType = (RefToDBObjectType) int.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      default:
        return base.ReadFieldFromXml(readArgs);
    }
  }

  /// <summary>Загрузить ссылку из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public override void ReadFromXml(XmlReadArgs readArgs)
  {
    if (readArgs == null)
      throw new ArgumentNullException(nameof (readArgs));
    if (readArgs.Reader.HasAttributes)
    {
      int attributeCount = readArgs.Reader.AttributeCount;
      int num = 0;
      XmlReader reader = readArgs.Reader;
      int i1 = num;
      int i2 = i1 + 1;
      reader.MoveToAttribute(i1);
      if (readArgs.Reader.HasAttributes)
      {
        for (; i2 < attributeCount; ++i2)
        {
          readArgs.Reader.MoveToAttribute(i2);
          if (!this.ReadFieldFromXml(readArgs))
            this.AddUnknownXmlAttribute(readArgs.Reader.LocalName, readArgs.Reader.Value);
        }
        readArgs.Reader.MoveToElement();
      }
      for (int i3 = i2; i3 < attributeCount; ++i3)
      {
        readArgs.Reader.MoveToAttribute(i3);
        if (!this.ReadFieldFromXml(readArgs))
          this.AddUnknownXmlAttribute(readArgs.Reader.LocalName, readArgs.Reader.Value);
      }
      readArgs.Reader.MoveToElement();
    }
    bool flag = readArgs.Reader.IsEmptyElement;
    while (!flag && (readArgs.SkipRead || readArgs.Reader.Read()))
    {
      readArgs.SkipRead = false;
      string localName = readArgs.Reader.LocalName;
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
    LogManager.AddLine("ReferenceToDBObjectAttributeBase.ReadFromXml End Element not found.");
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    if (this.attributeGuid != Guid.Empty)
      xw.WriteAttributeString("attrGuid", this.attributeGuid.ToString());
    if (this.attributeName == null || !(this.attributeName != "") || this.OwnerNode != null && this.OwnerNode.HasTemplate())
      return;
    xw.WriteAttributeString("attrName", this.attributeName);
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlElements(xw, objectRefId);
    if (this.NeedSaveTextValueToFile)
      xw.WriteElementString("Text", this.attributeValue);
    if (this.attributeLinkObjectID == -1L)
      return;
    xw.WriteElementString("ObjectLinkID", this.attributeLinkObjectID.ToString());
  }

  /// <summary>Имя базового типа ссылки для хранения в XML.
  /// Этот тип используется если TypeNameForXml не найден</summary>
  protected override string BaseTypeNameForXml => ReferenceToDBObjectAttributeBase.XmlTypeName;

  /// <summary>Имя типа сохраняемое в XML</summary>
  public override string TypeNameForXml => ReferenceToDBObjectAttributeBase.XmlTypeName;
}
