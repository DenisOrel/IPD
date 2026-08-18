// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ReferenceToDBObject
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Ссылка на объект базы данных из документа</summary>
[Serializable]
public class ReferenceToDBObject : ReferenceToDBObjectCore
{
  [NonSerialized]
  protected internal AttributeProcessor dbObjAttributeProcessor;
  [NonSerialized]
  protected internal AttributeProcessor dbRelAttributeProcessor;

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructor() => (object) new ReferenceToDBObject();

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructorActiveLink()
  {
    ReferenceToDBObject referenceToDbObject = new ReferenceToDBObject();
    referenceToDbObject.passiveLink = false;
    return (object) referenceToDbObject;
  }

  /// <summary>Конструктор</summary>
  public ReferenceToDBObject()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObject(DocumentTreeNode ownerNode, bool passiveLink)
    : base(ownerNode, passiveLink)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="dbObject">Интерфейс объекта БД</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObject(DocumentTreeNode ownerNode, IDBObject dbObject, bool passiveLink)
    : base(ownerNode, dbObject, passiveLink)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="refType">Тип ссылки на объект БД</param>
  /// <param name="dbObjectInfo">Идентификаторы и информация об объекте</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObject(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    bool passiveLink)
    : base(ownerNode, refType, dbObjectInfo, passiveLink)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="refType">Тип ссылки на объект БД</param>
  /// <param name="objectVersionGuid">Guid версии объекта</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObject(RefToDBObjectType refType, Guid objectVersionGuid, bool passiveLink)
    : base((DocumentTreeNode) null, refType, (DBObjectInfoBase) new Intermech.Interfaces.Document.DBObjectInfo(objectVersionGuid), passiveLink)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="refType">Тип ссылки на объект БД</param>
  /// <param name="relationGuid">Guid связи</param>
  /// <param name="objectVersionGuid">Guid версии объекта. Заполняется, когда нужно хранить для какой версии объекта по связи были сохранены данные в документе</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToDBObject(
    RefToDBObjectType refType,
    Guid relationGuid,
    Guid objectVersionGuid,
    bool passiveLink)
    : base((DocumentTreeNode) null, refType, (DBObjectInfoBase) new DBRelationInfo(relationGuid, objectVersionGuid), passiveLink)
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
      this.UpdateLink(sessionKeeper.Session, (Dictionary<Guid, Dictionary<Guid, AttributeValueCache>>) null, (Dictionary<Guid, Dictionary<Guid, AttributeValueCache>>) null, forceUpdate, updateUI, updateLayout);
  }

  /// <summary>Вызвать диалог выбора объекта для ссылки</summary>
  public override void CallSelectObjectDialog()
  {
    IDescriptor rootDescriptor = (IDescriptor) new ObjectTypesNodeDescriptor();
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Document.Client_105"), rootDescriptor, typeof (IDBObjectID), SelectionOptions.Default);
    if (objArray == null || objArray.Length == 0)
      return;
    IDBObjectID dbObjectId = (IDBObjectID) objArray[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ReferenceToDBObjectCore.UpdateDBObjectInfo((IDBRelation) null, sessionKeeper.Session.GetObject(dbObjectId.Value, false), (ReferenceToDBObjectBase) null);
  }

  /// <summary>Можно ли вызвать диалог выбора объекта для ссылки</summary>
  public override bool CanCallSelectObjectDialog
  {
    get => this.refType == RefToDBObjectType.rtSelectedObject;
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
    if (this.linkAttributeID != -1)
      this.linkAttributeGuid = MetaDataHelper.GetAttributeTypeGuid(this.linkAttributeID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateLink((object) sessionKeeper.Session, true, true, true);
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
        ArrayList arrayList = new ArrayList();
        IDBObject documentDbObject = ReferenceToDBObjectCore.GetOwnerDocumentDBObject(this.OwnerNode, sessionKeeper.Session, (string) null);
        if (documentDbObject != null)
        {
          foreach (int attributeTypeID in DocumentEditorPlugin.GetAttributesForDBObjectType(documentDbObject.ObjectType))
          {
            IDBAttributeTypeInfo attributeType = DocumentEditorPlugin.MDCache.GetAttributeType(attributeTypeID, false);
            if (attributeType != null && attributeType.IsGridable)
              arrayList.Add((object) attributeType.Name);
          }
          AttributeValues[] attributesValues = documentDbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName);
          if (attributesValues != null)
          {
            for (int index = 0; index < attributesValues.Length; ++index)
            {
              if (!arrayList.Contains((object) attributesValues[index].AttributeName))
                arrayList.Add((object) attributesValues[index].AttributeName);
            }
          }
        }
        attributeNameList = (string[]) arrayList.ToArray(typeof (string));
      }
    }
    return attributeNameList;
  }

  /// <summary>Получить словарь кэша AttributeProcessor</summary>
  /// <returns></returns>
  public AttributeProcessorDictionary GetAttributeProcessorDictionary()
  {
    AttributeProcessorDictionary processorDictionary = (AttributeProcessorDictionary) null;
    ImDocumentData imDocumentData = this.OwnerNode as ImDocumentData;
    if (this.OwnerNode is IDocumentElement ownerNode)
      imDocumentData = ownerNode.OwnerDocument;
    if (imDocumentData != null)
    {
      lock (imDocumentData)
      {
        processorDictionary = (AttributeProcessorDictionary) imDocumentData.DBAttributeProcessorDictionary;
        if (processorDictionary == null)
          imDocumentData.DBAttributeProcessorDictionary = (object) (processorDictionary = new AttributeProcessorDictionary());
      }
    }
    return processorDictionary;
  }

  /// <summary>Получить AttributeProcessors из кэша или создать новый</summary>
  /// <param name="relationProcessor">AttributeProcessors для атрибутов связи</param>
  /// <param name="objectProcessor">AttributeProcessors для атрибутов объекта</param>
  public virtual void GetFromCacheOrCreateAttributeProcessors(
    bool relationProcessor,
    bool objectProcessor)
  {
    AttributeProcessorDictionary processorDictionary = (AttributeProcessorDictionary) null;
    if (relationProcessor && this.IsReferenceToRelation && this.dbObjAttributeProcessor == null)
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
          {
            this.AssignRelationAttributeProcessor(processorDictionary[this.DBRelationID]);
          }
          else
          {
            this.AssignRelationAttributeProcessor(new AttributeProcessor());
            processorDictionary.Add(this.DBRelationID, this.dbRelAttributeProcessor);
          }
        }
      }
      else
        this.AssignRelationAttributeProcessor(new AttributeProcessor());
    }
    if (!objectProcessor || this.dbObjAttributeProcessor != null)
      return;
    if (this.IsEmptyObjectRef)
      this.GetParentDBObjectInfo();
    if (this.IsEmptyObjectRef)
      return;
    if (!this.IsConnectedObjectRef)
      this.UpdateDBObjectInfo();
    if (!this.IsConnectedObjectRef)
      return;
    if (processorDictionary == null)
      processorDictionary = this.GetAttributeProcessorDictionary();
    if (processorDictionary == null)
      return;
    lock (processorDictionary)
    {
      if (processorDictionary.ContainsKey(this.DBObjectID))
      {
        this.AssignObjectAttributeProcessor(processorDictionary[this.DBObjectID]);
      }
      else
      {
        this.AssignObjectAttributeProcessor(new AttributeProcessor());
        processorDictionary.Add(this.DBObjectID, this.dbObjAttributeProcessor);
      }
    }
  }

  /// <summary>AttributeProcessor для атрибутов объекта</summary>
  [Browsable(false)]
  public virtual AttributeProcessor DBObjectAttributeProcessor
  {
    [DebuggerStepThrough] get
    {
      if (this.dbObjAttributeProcessor == null)
        this.GetFromCacheOrCreateAttributeProcessors(false, true);
      return this.dbObjAttributeProcessor;
    }
  }

  /// <summary>Назначить AttributeProcessor для атрибутов объекта</summary>
  /// <param name="attrProcessor">AttributeProcessor для атрибутов объекта</param>
  public virtual void AssignObjectAttributeProcessor(AttributeProcessor attrProcessor)
  {
    if (this.dbObjAttributeProcessor == attrProcessor)
      return;
    if (this.dbObjAttributeProcessor != null)
      this.dbObjAttributeProcessor.AttributeValuesChanged -= new AttributeValuesChangedHandler(this.ObjAttributeProcessor_AttributeValuesChanged);
    this.dbObjAttributeProcessor = attrProcessor;
    if (this.dbObjAttributeProcessor == null)
      return;
    this.dbObjAttributeProcessor.AttributeValuesChanged += new AttributeValuesChangedHandler(this.ObjAttributeProcessor_AttributeValuesChanged);
  }

  /// <summary>Обработчик события AttributeProcessor AttributeValuesChanged</summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  protected virtual void ObjAttributeProcessor_AttributeValuesChanged(
    object sender,
    AttributeValuesChangedEventArgs args)
  {
  }

  /// <summary>AttributeProcessor для атрибутов связи</summary>
  [Browsable(false)]
  public virtual AttributeProcessor DBRelationAttributeProcessor
  {
    [DebuggerStepThrough] get
    {
      if (this.dbRelAttributeProcessor == null)
        this.GetFromCacheOrCreateAttributeProcessors(true, false);
      return this.dbRelAttributeProcessor;
    }
  }

  /// <summary>Назначить AttributeProcessor для атрибутов связи</summary>
  /// <param name="attrProcessor">AttributeProcessor для атрибутов связи</param>
  public virtual void AssignRelationAttributeProcessor(AttributeProcessor attrProcessor)
  {
    if (this.dbRelAttributeProcessor == attrProcessor)
      return;
    if (this.dbRelAttributeProcessor != null)
      this.dbRelAttributeProcessor.AttributeValuesChanged -= new AttributeValuesChangedHandler(this.RelAttributeProcessor_AttributeValuesChanged);
    this.dbRelAttributeProcessor = attrProcessor;
    if (this.dbRelAttributeProcessor == null)
      return;
    this.dbRelAttributeProcessor.AttributeValuesChanged += new AttributeValuesChangedHandler(this.RelAttributeProcessor_AttributeValuesChanged);
  }

  /// <summary>Обработчик события AttributeProcessor AttributeValuesChanged</summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  protected virtual void RelAttributeProcessor_AttributeValuesChanged(
    object sender,
    AttributeValuesChangedEventArgs args)
  {
  }

  /// <summary>Инициализировать AttributeProcessor связи</summary>
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
          this.GetFromCacheOrCreateAttributeProcessors(true, false);
        if (this.dbRelAttributeProcessor.Loaded && this.dbRelAttributeProcessor.Id == this.DBRelationID)
          return;
        this.dbRelAttributeProcessor.Load(this.DBRelationID, AttributableElements.Relation, GetAttributeValuesModes.None, false);
      }
    }
  }

  /// <summary>Инициализировать AttributeProcessor объекта</summary>
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
          this.GetFromCacheOrCreateAttributeProcessors(false, true);
        if (this.dbObjAttributeProcessor.Loaded && this.dbObjAttributeProcessor.Id == this.DBObjectID)
          return;
        this.dbObjAttributeProcessor.Load(this.DBObjectID, AttributableElements.Object, GetAttributeValuesModes.None, false);
      }
    }
  }
}
