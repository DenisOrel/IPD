// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ReferenceToDBObjectAttribute
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Ссылка на атрибут объекта в БД</summary>
[Serializable]
public class ReferenceToDBObjectAttribute : ReferenceToDBObjectAttributeCore
{
  [NonSerialized]
  protected internal AttributeProcessor dbObjAttributeProcessor;
  [NonSerialized]
  protected internal AttributeProcessor dbRelAttributeProcessor;

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructor() => (object) new ReferenceToDBObjectAttribute();

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructorActiveLink()
  {
    ReferenceToDBObjectAttribute dbObjectAttribute = new ReferenceToDBObjectAttribute();
    dbObjectAttribute.passiveLink = false;
    return (object) dbObjectAttribute;
  }

  /// <summary>Коструктор</summary>
  public ReferenceToDBObjectAttribute()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObjectAttribute(DocumentTreeNode ownerNode, bool passiveLink)
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
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObjectAttribute(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    Guid attrGuid,
    int attrID,
    string attrName,
    bool passiveLink)
    : base(ownerNode, refType, dbObjectInfo, attrGuid, attrID, attrName, passiveLink)
  {
  }

  public override void GetParentDBObjectInfo()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.GetParentDBObjectInfo(sessionKeeper.Session, this.OwnerNode);
  }

  /// <summary>Обновить связь</summary>
  /// <param name="forceUpdate">Обновлять даже для пассивных ссылок</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void UpdateLink(bool forceUpdate, bool updateUI, bool updateLayout)
  {
    if (this.IsSuspendedUpdatesFromDB || !forceUpdate && this.PassiveLink)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateLink((object) sessionKeeper.Session, forceUpdate, updateUI, updateLayout);
  }

  protected override bool UpdateAttributeValueAtrrPocessor()
  {
    if (true)
      return false;
    AttributeProcessor attributeProcessor = this.GetAttributeProcessor(false, false) ?? this.GetAttributeProcessor(true, true);
    if (attributeProcessor != null && attributeProcessor.Loaded)
    {
      this.attributeValue = (string) null;
      AttributeValues attributeValues = attributeProcessor.FindAttributeValues(this.attributeID);
      if (attributeValues != null)
      {
        this.readOnlyAttr = new bool?(attributeValues.ReadOnly);
        object obj = attributeValues.Values[0];
        if (obj != null && obj != DBNull.Value)
        {
          if (!(obj is string))
          {
            if (attributeValues.AttributeType == FieldTypes.ftSystem && attributeValues.Descriptions != null && attributeValues.Descriptions.Length != 0 && attributeValues.Descriptions[0] != null)
            {
              this.attributeValue = attributeValues.Descriptions[0].ToString();
            }
            else
            {
              TypeConverter singleValueConverter = attributeProcessor.GetSingleValueConverter(this.attributeID);
              if (singleValueConverter != null)
                this.attributeValue = singleValueConverter.ConvertToString(obj);
              else
                this.attributeValue = obj.ToString();
            }
          }
          else
            this.attributeValue = (string) obj;
        }
      }
    }
    return true;
  }

  /// <summary>Сохранить изменения в базу</summary>
  public virtual void SaveChangesToDB()
  {
    AttributeProcessor attributeProcessor = this.GetAttributeProcessor(false, false);
    if (attributeProcessor == null || !attributeProcessor.Loaded || !attributeProcessor.Modified)
      return;
    attributeProcessor.Save();
  }

  /// <summary>Можно редактировать по месту. Для ссылок на атрибуты</summary>
  public override bool CanInplaceEdit
  {
    get
    {
      AttributeProcessor attributeProcessor = this.GetAttributeProcessor(true, false);
      if (attributeProcessor != null)
      {
        AttributeValues attributeValues = (AttributeValues) null;
        if (attributeProcessor.Loaded)
          attributeValues = attributeProcessor.FindAttributeValues(this.attributeID);
        if (attributeValues != null)
          this.readOnlyAttr = new bool?(attributeValues.ReadOnly);
        TypeConverter singleValueConverter = attributeProcessor.GetSingleValueConverter(this.attributeID);
        if (singleValueConverter != null)
          return singleValueConverter.CanConvertFrom(typeof (string));
      }
      return true;
    }
  }

  /// <summary>Получить поддерживаемые стили редактирования атрибута</summary>
  /// <returns>Список стилей</returns>
  public List<UITypeEditorEditStyle> GetEditorStyles()
  {
    List<UITypeEditorEditStyle> editorStyles = (List<UITypeEditorEditStyle>) null;
    AttributeProcessor attributeProcessor = this.GetAttributeProcessor(true, false);
    if (attributeProcessor != null && attributeProcessor.GetSingleValueConverter(this.attributeID) is CommonTypeConverter singleValueConverter)
      editorStyles = singleValueConverter.GetPossibleEditorControlStyle();
    return editorStyles;
  }

  /// <summary>Получить редактор для атрибута</summary>
  /// <param name="style">Стиль редактирования</param>
  /// <returns></returns>
  public IAttributeEditorControl GetEditorControl(UITypeEditorEditStyle style)
  {
    return this.GetAttributeProcessor(true, true)?.GetEditorControl(this.attributeID, new int?(0), style);
  }

  /// <summary>Возможен вызов дополнительного редактора для элемента</summary>
  public override bool CanCallEditor
  {
    get
    {
      if (this.IsEmpty)
      {
        this.GetParentDBObjectInfo();
        if (this.IsEmpty)
          return false;
      }
      return true;
    }
  }

  public override bool CanShowReference()
  {
    return (this.ReferenceType != RefToDBObjectType.rtUseParentDocumentObjectLink || !(this.OwnerDocument is ImDocument) || (this.OwnerDocument as ImDocument).DocumentControl == null || !(this.OwnerDocument as ImDocument).DocumentControl.ReadOnly || (this.OwnerDocument as ImDocument).DocumentControl.DocumentViewMode.HasFlag((Enum) DocumentViewMode.ShowDocumentReferences)) && base.CanShowReference();
  }

  /// <summary>Вызвать редактор</summary>
  /// <returns>Результат вызова</returns>
  public override bool CallEditor()
  {
    AttributeProcessor attributeProcessor = this.GetAttributeProcessor(true, true);
    if (attributeProcessor != null)
    {
      if (attributeProcessor.FindAttributeValues(this.attributeID) != null)
      {
        attributeProcessor.SetValue(this.attributeID, (object) this.attributeValue);
        if (attributeProcessor.EditAttributeValue(this.attributeID, UITypeEditorEditStyle.Modal) != null)
          return true;
      }
    }
    else
    {
      if (this.attributeID == -1 && this.attributeGuid != Guid.Empty)
        throw new Exception(LocalizationHolder.rm.GetString("Document.Client_153") + this.attributeGuid.ToString() + LocalizationHolder.rm.GetString("Document.Client_154"));
      if (this.IsReferenceToRelation && this.DBRelationID == -1L && this.DBRelationGuid != Guid.Empty)
        throw new Exception(LocalizationHolder.rm.GetString("Document.Client_155") + this.DBRelationGuid.ToString() + LocalizationHolder.rm.GetString("Document.Client_156"));
      if (!this.IsReferenceToRelation && this.DBObjectID == -1L && this.DBObjectGuid != Guid.Empty)
        throw new Exception(LocalizationHolder.rm.GetString("Document.Client_157") + this.DBObjectGuid.ToString() + LocalizationHolder.rm.GetString("Document.Client_154"));
    }
    return false;
  }

  /// <summary>Обработчик изменения атрибута объекта</summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  protected void ObjAttributeProcessor_AttributeValuesChanged(
    object sender,
    AttributeValuesChangedEventArgs args)
  {
    if (this.IsReferenceToRelation || args == null || args.AttributeId != this.attributeID)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateAttributeValue(sessionKeeper.Session, false, false, true, true);
  }

  /// <summary>Обработчик изменения атрибута связи</summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  protected void RelAttributeProcessor_AttributeValuesChanged(
    object sender,
    AttributeValuesChangedEventArgs args)
  {
    if (!this.IsReferenceToRelation || args == null || args.AttributeId != this.attributeID)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateAttributeValue(sessionKeeper.Session, false, false, true, true);
  }

  /// <summary>Можно вызвать диалог выбора атрибута для ссылки</summary>
  public override bool CanCallSelectAttributeDialog => true;

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public override string[] GetAttributeNameList()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<string> stringList = new List<string>();
      if (this.IsRelationAttribute)
      {
        IDBRelation dbRelation = this.GetDBRelation(sessionKeeper.Session, out IDBObject _);
        if (dbRelation != null)
        {
          foreach (int attributeTypeID in DocumentEditorPlugin.GetAttributesForDBRelationType(dbRelation.RelationType))
          {
            IDBAttributeTypeInfo attributeType = DocumentEditorPlugin.MDCache.GetAttributeType(attributeTypeID, false);
            if (attributeType != null && attributeType.IsGridable)
              stringList.Add(attributeType.Name);
          }
          AttributeValues[] attributesValues = dbRelation.GetAttributesValues(GetAttributeValuesModes.IncludeName);
          if (attributesValues != null)
          {
            for (int index = 0; index < attributesValues.Length; ++index)
            {
              if (!stringList.Contains(attributesValues[index].AttributeName))
                stringList.Add(attributesValues[index].AttributeName);
            }
          }
        }
      }
      else
      {
        IDBObject dbObject = this.GetDBObject(sessionKeeper.Session);
        if (dbObject != null)
        {
          foreach (int attributeTypeID in DocumentEditorPlugin.GetAttributesForDBObjectType(dbObject.ObjectType))
          {
            IDBAttributeTypeInfo attributeType = DocumentEditorPlugin.MDCache.GetAttributeType(attributeTypeID, false);
            if (attributeType != null && attributeType.IsGridable)
              stringList.Add(attributeType.Name);
          }
          AttributeValues[] attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName);
          if (attributesValues != null)
          {
            for (int index = 0; index < attributesValues.Length; ++index)
            {
              if (!stringList.Contains(attributesValues[index].AttributeName))
                stringList.Add(attributesValues[index].AttributeName);
            }
          }
        }
      }
      return stringList.ToArray();
    }
  }

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public override string[] GetLinkAttributeNameList()
  {
    string[] attributeNameList = (string[]) null;
    if (this.UseLinkAttribute)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        List<string> stringList = new List<string>();
        IDBObject documentDbObject = ReferenceToDBObjectCore.GetOwnerDocumentDBObject(this.OwnerNode, sessionKeeper.Session, (string) null);
        if (documentDbObject != null)
        {
          foreach (int attributeTypeID in DocumentEditorPlugin.GetAttributesForDBObjectType(documentDbObject.ObjectType))
          {
            IDBAttributeTypeInfo attributeType = DocumentEditorPlugin.MDCache.GetAttributeType(attributeTypeID, false);
            if (attributeType != null && attributeType.IsGridable)
              stringList.Add(attributeType.Name);
          }
          AttributeValues[] attributesValues = documentDbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName);
          if (attributesValues != null)
          {
            for (int index = 0; index < attributesValues.Length; ++index)
            {
              if (!stringList.Contains(attributesValues[index].AttributeName))
                stringList.Add(attributesValues[index].AttributeName);
            }
          }
        }
        attributeNameList = stringList.ToArray();
      }
    }
    return attributeNameList;
  }

  /// <summary>Вызвать диалог выбора атрибута для ссылки</summary>
  public override void CallSelectAttributeDialog()
  {
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true);
    if (!this.OwnerNode.IsTemplate && !Intermech.Consts.IsUndefinedObjectId(this.DBObjectID))
      attributesSelectDlg.LoadAttrDialogForObject(this.DBObjectID, true);
    if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.UpdateAttributeInfo(MetaDataHelper.GetAttributeType(attributesSelectDlg.SelectedAttributesID[0]));
      this.UpdateLink((object) sessionKeeper.Session, true, true, true);
    }
  }

  /// <summary>Можно вызвать диалог выбора ссылочного атрибута</summary>
  [Browsable(false)]
  public override bool CanCallSelectLinkAttributeDialog => this.UseLinkAttribute;

  /// <summary>Вызвать диалог выбора ссылочного атрибута</summary>
  public override void CallSelectLinkAttributeDialog()
  {
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true);
    if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count <= 0)
      return;
    this.linkAttributeID = attributesSelectDlg.SelectedAttributesID[0];
    this.linkAttributeName = (string) null;
    if (this.linkAttributeID != -1)
      this.linkAttributeGuid = MetaDataHelper.GetAttributeTypeGuid(this.linkAttributeID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateLink((object) sessionKeeper.Session, true, true, true);
  }

  /// <summary>Вызвать диалог выбора объекта для ссылки</summary>
  public override void CallSelectObjectDialog()
  {
    IDescriptor rootDescriptor = (IDescriptor) new ObjectTypesNodeDescriptor();
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Document.Client_107"), rootDescriptor, typeof (IDBObjectID), SelectionOptions.Default);
    if (objArray == null || objArray.Length == 0)
      return;
    IDBObjectID dbObjectId = (IDBObjectID) objArray[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateDBObjectInfo((IDBRelation) null, sessionKeeper.Session.GetObject(dbObjectId.Value));
  }

  /// <summary>Можно ли вызвать диалог выбора объекта для ссылки</summary>
  public override bool CanCallSelectObjectDialog
  {
    get => this.refType == RefToDBObjectType.rtSelectedObject;
  }

  public override bool ReadOnly
  {
    get
    {
      if (!this.PassiveLink && !this.readOnlyAttr.HasValue)
      {
        AttributeProcessor attributeProcessor = this.GetAttributeProcessor(false, false) ?? this.GetAttributeProcessor(true, true);
        if (attributeProcessor != null && attributeProcessor.Loaded)
        {
          AttributeValues attributeValues = attributeProcessor.FindAttributeValues(this.attributeID);
          if (attributeValues != null)
            this.readOnlyAttr = new bool?(attributeValues.ReadOnly);
        }
      }
      return base.ReadOnly;
    }
  }

  /// <summary>Установить значение атрибута БД</summary>
  /// <param name="value">Новое значение атрибута</param>
  /// <param name="saveToDB">Заносить значение атрибута в саму БД</param>
  public virtual void SetDBAttributeValue(string value, bool saveToDB)
  {
    if (!(this.attributeValue != value))
      return;
    if (!this.IsEmpty)
    {
      AttributeProcessor attributeProcessor = this.GetAttributeProcessor(true, true);
      if (!this.ReadOnly && attributeProcessor != null)
      {
        string attributeValue = this.attributeValue;
        this.attributeValue = value;
        AttributeValues attributeValues = attributeProcessor.FindAttributeValues(this.attributeID);
        bool flag = false;
        if (attributeValues == null)
        {
          attributeValues = AttributeProcessor.CreateAttributeValues(this.attributeID, attributeProcessor.Id, attributeProcessor.ElementKind);
          flag = true;
        }
        if (attributeValues != null)
        {
          try
          {
            if (saveToDB)
            {
              if (flag)
              {
                this.readOnlyAttr = new bool?(attributeValues.ReadOnly);
                attributeValues.Values[0] = (object) value;
                AttributeValuesList list = new AttributeValuesList();
                list.Add(attributeValues);
                attributeProcessor.SetAttributeValuesArray(list);
              }
              else
                attributeProcessor.SetValue(this.attributeID, (object) value);
            }
            ImDocumentData ownerDocument = this.OwnerDocument;
            if (saveToDB && ownerDocument != null && ownerDocument.DBAttributeAutoSave)
              attributeProcessor.Save();
            this.UpdateCachedValue();
          }
          catch
          {
            this.attributeValue = attributeValue;
            throw;
          }
        }
        else
        {
          this.readOnlyAttr = new bool?(true);
          this.attributeValue = attributeValue;
          throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Document.Client_108"));
        }
      }
    }
    this.attributeValue = value;
  }

  /// <summary>Назначить значение Text</summary>
  /// <param name="value">Значение</param>
  /// <param name="saveToDB">Заносить значение атрибута в саму БД</param>
  /// <param name="fireTextChanged">Вызывать обработчики события TextChanged</param>
  /// <param name="updateOwner">Генерировать событие в элементе владельце</param>
  /// <param name="updateUI">Обновить изображение</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void SetText(
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
    if (this.PassiveLink)
      this.attributeValue = value;
    else
      this.SetDBAttributeValue(value, saveToDB);
    if (!fireTextChanged)
      return;
    this.OnTextChanged(attributeValue, this.attributeValue, updateOwner, !this.PassiveLink, updateUI, updateLayout);
  }

  public virtual void GetFromCacheOrCreateAttributeProcessors(
    bool relationProcessor,
    bool objectProcessor,
    bool autoCreate)
  {
    AttributeProcessorDictionary processorDictionary = (AttributeProcessorDictionary) null;
    if (relationProcessor && this.IsReferenceToRelation)
    {
      if (this.IsEmptyObjectRef)
        this.GetParentDBObjectInfo();
      if (this.IsEmptyObjectRef)
        return;
      if (!this.IsConnectedObjectRef)
        this.UpdateDBObjectInfo();
      if (!this.IsConnectedObjectRef)
        return;
      processorDictionary = this.GetAttributeProcessorDictionary();
      if (processorDictionary != null)
      {
        lock (processorDictionary)
        {
          if (processorDictionary.ContainsKey(this.DBRelationID))
            this.AssignRelationAttributeProcessor(processorDictionary[this.DBRelationID]);
          else if (autoCreate)
          {
            this.AssignRelationAttributeProcessor(new AttributeProcessor());
            processorDictionary.Add(this.DBRelationID, this.dbRelAttributeProcessor);
          }
          else
            this.AssignObjectAttributeProcessor((AttributeProcessor) null);
        }
      }
    }
    if (!objectProcessor)
      return;
    if (this.IsEmptyObjectRef)
      this.GetParentDBObjectInfo();
    if (this.IsEmptyObjectRef)
      return;
    bool flag = true;
    if (!this.IsConnectedObjectRef)
    {
      this.UpdateDBObjectInfo();
      flag = false;
    }
    if (!this.IsConnectedObjectRef)
      return;
    if (processorDictionary == null)
      processorDictionary = this.GetAttributeProcessorDictionary();
    if (processorDictionary == null)
      return;
    if (flag && this.dbObjectInfo != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.DBObjectID, false);
        if (objectActual != null)
          this.dbObjectInfo.ObjectID = objectActual.ObjectID;
      }
    }
    lock (processorDictionary)
    {
      if (processorDictionary.ContainsKey(this.DBObjectID))
        this.AssignObjectAttributeProcessor(processorDictionary[this.DBObjectID]);
      else if (autoCreate)
      {
        this.AssignObjectAttributeProcessor(new AttributeProcessor());
        processorDictionary.Add(this.DBObjectID, this.dbObjAttributeProcessor);
      }
      else
        this.AssignObjectAttributeProcessor((AttributeProcessor) null);
    }
  }

  public virtual AttributeProcessor GetAttributeProcessor(bool autoCreate, bool autoLoad)
  {
    if (this.IsEmpty)
    {
      this.GetParentDBObjectInfo();
      if (this.IsEmpty)
        return (AttributeProcessor) null;
    }
    if (!this.IsConnectedObjectRef)
    {
      this.UpdateDBObjectInfo();
      if (!this.IsConnectedObjectRef)
        return (AttributeProcessor) null;
    }
    if (!this.IsConnectedAttributeRef)
    {
      this.UpdateAttributeInfo();
      if (!this.IsConnectedAttributeRef)
        return (AttributeProcessor) null;
    }
    AttributeProcessor attributeProcessor;
    if (this.IsReferenceToRelation)
    {
      attributeProcessor = this.GetDBRelationAttributeProcessor(autoCreate);
      if (attributeProcessor != null && autoLoad && (!attributeProcessor.Loaded || attributeProcessor.Id != this.DBRelationID))
      {
        this.InitDBRelationAttrProc();
        if (!attributeProcessor.Loaded)
          return (AttributeProcessor) null;
      }
    }
    else
    {
      attributeProcessor = this.GetDBObjectAttributeProcessor(autoCreate);
      if (attributeProcessor != null && autoLoad && (!attributeProcessor.Loaded || attributeProcessor.Id != this.DBObjectID))
      {
        this.InitDBObjectAttrProc();
        int num = attributeProcessor.Loaded ? 1 : 0;
        return attributeProcessor;
      }
    }
    return attributeProcessor;
  }

  public AttributeProcessorDictionary GetAttributeProcessorDictionary()
  {
    AttributeProcessorDictionary processorDictionary = (AttributeProcessorDictionary) null;
    if (this.OwnerNode is IDocumentElement ownerNode)
    {
      ImDocumentData ownerDocument = ownerNode.OwnerDocument;
      if (ownerDocument != null)
      {
        lock (ownerDocument)
        {
          processorDictionary = (AttributeProcessorDictionary) ownerDocument.DBAttributeProcessorDictionary;
          if (processorDictionary == null)
            ownerDocument.DBAttributeProcessorDictionary = (object) (processorDictionary = new AttributeProcessorDictionary());
        }
      }
    }
    return processorDictionary;
  }

  public virtual AttributeProcessor GetDBObjectAttributeProcessor(bool autoCreate)
  {
    this.GetFromCacheOrCreateAttributeProcessors(false, true, autoCreate);
    return this.dbObjAttributeProcessor;
  }

  public virtual void AssignObjectAttributeProcessor(AttributeProcessor attrProcessor)
  {
    if (this.dbObjAttributeProcessor == attrProcessor)
      return;
    if (this.dbObjAttributeProcessor != null)
      this.dbObjAttributeProcessor.AttributeValuesChanged -= new AttributeValuesChangedHandler(this.ObjAttributeProcessor_AttributeValuesChanged);
    this.dbObjAttributeProcessor = attrProcessor;
    if (this.dbObjAttributeProcessor == null)
      return;
    this.dbObjAttributeProcessor.AttributeValuesChanged -= new AttributeValuesChangedHandler(this.ObjAttributeProcessor_AttributeValuesChanged);
    this.dbObjAttributeProcessor.AttributeValuesChanged += new AttributeValuesChangedHandler(this.ObjAttributeProcessor_AttributeValuesChanged);
  }

  public virtual AttributeProcessor GetDBRelationAttributeProcessor(bool autoCreate)
  {
    this.GetFromCacheOrCreateAttributeProcessors(true, false, autoCreate);
    return this.dbRelAttributeProcessor;
  }

  public virtual void AssignRelationAttributeProcessor(AttributeProcessor attrProcessor)
  {
    if (this.dbRelAttributeProcessor == attrProcessor)
      return;
    if (this.dbRelAttributeProcessor != null)
      this.dbRelAttributeProcessor.AttributeValuesChanged -= new AttributeValuesChangedHandler(this.RelAttributeProcessor_AttributeValuesChanged);
    this.dbRelAttributeProcessor = attrProcessor;
    if (this.dbRelAttributeProcessor == null)
      return;
    this.dbRelAttributeProcessor.AttributeValuesChanged -= new AttributeValuesChangedHandler(this.RelAttributeProcessor_AttributeValuesChanged);
    this.dbRelAttributeProcessor.AttributeValuesChanged += new AttributeValuesChangedHandler(this.RelAttributeProcessor_AttributeValuesChanged);
  }

  protected void InitDBRelationAttrProc()
  {
    if (this.IsEmptyObjectRef)
      this.GetParentDBObjectInfo();
    if (this.IsEmptyObjectRef)
    {
      this.AssignRelationAttributeProcessor((AttributeProcessor) null);
    }
    else
    {
      if (!this.IsConnected)
        this.UpdateDBObjectInfo();
      if (!this.IsConnected)
      {
        this.AssignRelationAttributeProcessor((AttributeProcessor) null);
      }
      else
      {
        if (this.dbRelAttributeProcessor == null)
          this.GetFromCacheOrCreateAttributeProcessors(true, false, true);
        if (this.dbRelAttributeProcessor.Loaded && this.dbRelAttributeProcessor.Id == this.DBRelationID)
          return;
        this.dbRelAttributeProcessor.Load(this.DBRelationID, AttributableElements.Relation, GetAttributeValuesModes.None, false);
      }
    }
  }

  protected void InitDBObjectAttrProc()
  {
    if (this.IsEmptyObjectRef)
      this.GetParentDBObjectInfo();
    if (this.IsEmptyObjectRef)
    {
      this.AssignObjectAttributeProcessor((AttributeProcessor) null);
    }
    else
    {
      if (!this.IsConnected)
        this.UpdateDBObjectInfo();
      if (!this.IsConnected)
      {
        this.AssignObjectAttributeProcessor((AttributeProcessor) null);
      }
      else
      {
        if (this.dbObjAttributeProcessor == null)
          this.GetFromCacheOrCreateAttributeProcessors(false, true, true);
        if (this.dbObjAttributeProcessor.Loaded && this.dbObjAttributeProcessor.Id == this.DBObjectID)
          return;
        this.dbObjAttributeProcessor.Load(this.DBObjectID, AttributableElements.Object, GetAttributeValuesModes.None, false);
      }
    }
  }
}
