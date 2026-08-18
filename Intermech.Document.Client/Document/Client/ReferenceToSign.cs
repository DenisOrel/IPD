// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ReferenceToSign
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Document.UI;
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
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Ссылка на атрибут объекта базы данны системы</summary>
[Serializable]
public class ReferenceToSign : ReferenceToSignCore
{
  [NonSerialized]
  protected internal AttributeProcessor dbObjAttributeProcessor;
  [NonSerialized]
  protected internal AttributeProcessor dbRelAttributeProcessor;

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructor() => (object) new ReferenceToSign();

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructorActiveLink()
  {
    ReferenceToSign referenceToSign = new ReferenceToSign();
    referenceToSign.passiveLink = false;
    return (object) referenceToSign;
  }

  /// <summary>Коструктор</summary>
  public ReferenceToSign()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToSign(DocumentTreeNode ownerNode, bool passiveLink)
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
  public ReferenceToSign(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    string attrName,
    bool passiveLink)
    : base(ownerNode, refType, dbObjectInfo, attrName, passiveLink)
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
    if (this.IsSuspendedUpdatesFromDB)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateLink((object) sessionKeeper.Session, forceUpdate, updateUI, updateLayout);
  }

  protected override bool HasDocumentControl()
  {
    return this.OwnerDocument is ImDocument && (this.OwnerDocument as ImDocument).DocumentControl != null;
  }

  protected override DocumentViewMode GetDocumentViewMode()
  {
    return !(this.OwnerDocument is ImDocument) || (this.OwnerDocument as ImDocument).DocumentControl == null || !(this.OwnerDocument as ImDocument).DocumentControl.ReadOnly ? DocumentViewMode.Normal : (this.OwnerDocument as ImDocument).DocumentControl.DocumentViewMode;
  }

  /// <summary>Можно редактировать по месту. Для ссылок на атрибуты</summary>
  public override bool CanInplaceEdit => false;

  /// <summary>Можно вызвать диалог выбора атрибута для ссылки</summary>
  public override bool CanCallSelectAttributeDialog => false;

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public override string[] GetAttributeNameList()
  {
    return (string[]) new ArrayList()
    {
      (object) LocalizationHolder.rm.GetString("Document.Client_161"),
      (object) LocalizationHolder.rm.GetString("Document.Client_162"),
      (object) LocalizationHolder.rm.GetString("Document.Client_163"),
      (object) LocalizationHolder.rm.GetString("Document.Client_165"),
      (object) LocalizationHolder.rm.GetString("Document.Client_166")
    }.ToArray(typeof (string));
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
          List<int> intList = new List<int>((IEnumerable<int>) DocumentEditorPlugin.GetAttributesForDBObjectType(documentDbObject.ObjectType));
          int attributeId = MetaDataHelper.GetAttributeID((object) "cad001a6-306c-11d8-b4e9-00304f19f545");
          if (!intList.Contains(attributeId))
            intList.Add(attributeId);
          for (int index = 0; index < intList.Count; ++index)
          {
            IDBAttributeTypeInfo attributeType = DocumentEditorPlugin.MDCache.GetAttributeType(intList[index], false);
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

  public override bool CanShowReference()
  {
    return ((this.OwnerDocument is ImDocument ownerDocument1 ? ownerDocument1.DocumentControl : (DocumentControl) null) == null || !(this.OwnerDocument as ImDocument).DocumentControl.ReadOnly || (this.OwnerDocument as ImDocument).DocumentControl.DocumentViewMode.HasFlag((Enum) DocumentViewMode.ShowSigns)) && ((this.OwnerDocument is ImDocument ownerDocument2 ? ownerDocument2.DocumentControl : (DocumentControl) null) == null || !(this.OwnerDocument as ImDocument).DocumentControl.ReadOnly || !(this.OwnerDocument as ImDocument).DocumentControl.DocumentViewMode.HasFlag((Enum) DocumentViewMode.ShowOnlySignName) || this.AttributeName == LocalizationHolder.rm.GetString("Document.Client_161")) && base.CanShowReference();
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
    get
    {
      return this.refType == RefToDBObjectType.rtSelectedObject || this.refType == RefToDBObjectType.rtUseSignFromObject;
    }
  }
}
