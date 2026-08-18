// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ReferenceToGraphics
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client;

[Serializable]
public class ReferenceToGraphics : ReferenceToGraphicsCore
{
  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructor() => (object) new ReferenceToGraphics();

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructorActiveLink()
  {
    ReferenceToGraphics referenceToGraphics = new ReferenceToGraphics();
    referenceToGraphics.passiveLink = false;
    return (object) referenceToGraphics;
  }

  /// <summary>Коструктор</summary>
  public ReferenceToGraphics()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="refType">Тип ссылки на объект БД</param>
  /// <param name="dbObjectInfo">Идентификаторы и информация об объекте</param>
  /// <param name="fileAttrGuid">Guid атрибута хранящего файл, если Guid.Empty, то используется атрибут "Файл"</param>
  /// <param name="attributeName">Имя атрибута, если null</param>
  /// <param name="fileName">Имя файла в атрибуте, если null, то используется первый файл</param>
  /// <param name="layers">Слои, которые нужно отображать, если null, то отображаются все слои</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToGraphics(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    Guid fileAttrGuid,
    string attributeName,
    string fileName,
    List<string> layers,
    bool passiveLink)
    : base(ownerNode, refType, dbObjectInfo, fileAttrGuid, attributeName, fileName, layers, passiveLink)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="dbObjectGuid">Guid версии объекта БД</param>
  /// <param name="layers">Слои, которые нужно отображать, если null, то отображаются все слои</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToGraphics(
    DocumentTreeNode ownerNode,
    Guid dbObjectGuid,
    List<string> layers,
    bool passiveLink)
    : base(ownerNode, dbObjectGuid, layers, passiveLink)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="dbObjectGuid">Guid версии объекта БД</param>
  /// <param name="layers">Слои, которые нужно отображать, если null, то отображаются все слои</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToGraphics(Guid dbObjectGuid, List<string> layers, bool passiveLink)
    : base(dbObjectGuid, layers, passiveLink)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="dbObjectGuid">Guid версии объекта БД</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToGraphics(Guid dbObjectGuid, bool passiveLink)
    : base(dbObjectGuid, passiveLink)
  {
  }

  /// <summary>Обновить связь</summary>
  /// <param name="forceUpdate">Обновлять даже для пассивных ссылок</param>
  /// <param name="updateUI"></param>
  /// <param name="updateLayout"></param>
  public override void UpdateLink(bool forceUpdate, bool updateUI, bool updateLayout)
  {
    if (this.IsSuspendedUpdatesFromDB || !forceUpdate && this.PassiveLink)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateLink(sessionKeeper.Session, forceUpdate, updateUI, updateLayout);
  }

  /// <summary>Сохранить Image в Stream</summary>
  /// <param name="image">Изображение</param>
  /// <param name="stream">Поток</param>
  protected override void SaveImageToStream(Image image, Stream stream)
  {
    ContainerData.SaveImageToStream(image, stream);
  }

  /// <summary>Вызвать диалог выбора атрибута для ссылки</summary>
  public override void CallSelectAttributeDialog()
  {
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true);
    if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.UpdateAttributeInfo(MetaDataHelper.GetAttributeType(attributesSelectDlg.SelectedAttributesID[0]));
      this.UpdateLink(sessionKeeper.Session, true, true, true);
    }
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
      this.UpdateDBObjectInfo((IDBRelation) null, sessionKeeper.Session.GetObject(dbObjectId.Value, false));
  }

  public override bool CanCallSelectObjectDialog
  {
    get => this.refType == RefToDBObjectType.rtSelectedObject;
  }

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public override string[] GetAttributeNameList()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ArrayList arrayList = new ArrayList();
      if (this.IsReferenceToRelation)
      {
        IDBRelation dbRelation = this.GetDBRelation(sessionKeeper.Session, out IDBObject _);
        if (dbRelation != null)
        {
          foreach (int attributeTypeID in DocumentEditorPlugin.GetAttributesForDBRelationType(dbRelation.RelationType))
          {
            IDBAttributeTypeInfo attributeType = DocumentEditorPlugin.MDCache.GetAttributeType(attributeTypeID, false);
            if (attributeType != null && attributeType.IsGridable)
              arrayList.Add((object) attributeType.Name);
          }
          AttributeValues[] attributesValues = dbRelation.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeBlobs);
          if (attributesValues != null)
          {
            for (int index = 0; index < attributesValues.Length; ++index)
            {
              if (!arrayList.Contains((object) attributesValues[index].AttributeName) && attributesValues[index].AttributeType == FieldTypes.ftFile)
                arrayList.Add((object) attributesValues[index].AttributeName);
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
            if (attributeType != null && attributeType.IsGridable && attributeType.AttributeType == FieldTypes.ftFile)
              arrayList.Add((object) attributeType.Name);
          }
          AttributeValues[] attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeBlobs);
          if (attributesValues != null)
          {
            for (int index = 0; index < attributesValues.Length; ++index)
            {
              if (!arrayList.Contains((object) attributesValues[index].AttributeName) && (attributesValues[index].AttributeType == FieldTypes.ftFile || attributesValues[index].AttributeType == FieldTypes.ftShortBlob || attributesValues[index].AttributeType == FieldTypes.ftBlob))
                arrayList.Add((object) attributesValues[index].AttributeName);
            }
          }
        }
      }
      return (string[]) arrayList.ToArray(typeof (string));
    }
  }
}
