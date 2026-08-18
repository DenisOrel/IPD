// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Server.CombineAttributesHelper
// Assembly: Intermech.Document.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F658B856-4DF9-439D-954C-249051C853FF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Document.Server.dll

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Document.Server;

internal class CombineAttributesHelper
{
  internal void BeforeCombineAttributesEvent(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode)
  {
    if (fromAttribute == null)
      return;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(DocIDCache.ObjType_ImDocTemplate);
    if (childrenIdRecursive == null)
      return;
    Guid guid = fromAttribute.GUID;
    foreach (int objectType in childrenIdRecursive)
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(objectType);
      if (objectCollection != null)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_CHKOUT_BY
        });
        DataTable dataTable = objectCollection.Select(paramSet);
        if (dataTable != null && dataTable.Rows.Count != 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (Convert.ToInt64(row[1]) != 0L)
            {
              long objectID = Math.Abs(Convert.ToInt64(row[0]));
              IDBObject dbObject = session.GetObject(objectID, false);
              if (dbObject != null && this.CheckExistanceAttr(session, dbObject, guid))
              {
                string objectName = MetaDataHelper.GetObjectName(dbObject.ObjectType);
                throw new Exception($"Для объединения атрибутов '{fromAttribute.Name}' и '{toAttribute.Name}', нужно завершить редактирование объекта '{dbObject.Caption}' (Тип '{objectName}', ID = '{Convert.ToString(dbObject.ObjectID)}'");
              }
            }
          }
        }
      }
    }
  }

  internal void AfterCombineAttributesEvent(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode)
  {
    if (fromAttribute == null || toAttribute == null)
      return;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(DocIDCache.ObjType_ImDocTemplate);
    if (childrenIdRecursive == null)
      return;
    Guid guid1 = fromAttribute.GUID;
    Guid guid2 = toAttribute.GUID;
    foreach (int num in childrenIdRecursive)
    {
      MetaDataHelper.GetObjectType(num);
      IDBObjectCollection objectCollection = session.GetObjectCollection(num);
      if (objectCollection != null)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_CHKOUT_BY
        });
        DataTable dataTable = objectCollection.Select(paramSet);
        if (dataTable != null && dataTable.Rows.Count != 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64_1 = Convert.ToInt64(row[1]);
            if (int64_1 <= 0L || int64_1 == session.UserID)
            {
              long int64_2 = Convert.ToInt64(row[0]);
              IDBObject dbObject = session.GetObjectActualCopy(int64_2, false);
              if (dbObject.CheckoutBy == session.UserID)
                this.ReplaceAttr(session, dbObject, fromAttribute, toAttribute);
              else if (this.CheckExistanceAttr(session, dbObject, guid1))
              {
                try
                {
                  if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                    dbObject = dbObject.CheckOut();
                  this.ReplaceAttr(session, dbObject, fromAttribute, toAttribute);
                  if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                    dbObject.CheckIn();
                }
                catch (Exception ex)
                {
                  throw;
                }
              }
            }
          }
        }
      }
    }
  }

  private void ReplaceAttr(
    IUserSession session,
    IDBObject obj,
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute)
  {
    bool flag = false;
    ImDocumentData imDocumentData = (ImDocumentData) null;
    try
    {
      imDocumentData = ImDocumentServerPlugin.LoadDocumentFromDBObjectCore(obj);
    }
    catch
    {
    }
    if (imDocumentData == null)
      return;
    foreach (DocumentTreeNode childNode in DocumentTreeNode.GetChildNodes((DocumentTreeNode) imDocumentData))
    {
      if (childNode is INodeWithReference nodeWithReference && nodeWithReference.Reference is ReferenceToDBObjectAttributeBase reference && reference.AttributeGuid == fromAttribute.GUID)
      {
        reference.AssignAttributeInfo(toAttribute.GUID, toAttribute.AttributeID, toAttribute.Name);
        flag = true;
      }
    }
    if (!flag)
      return;
    ImDocumentServerPlugin.SaveImDocumentObjectFile(session, obj.ObjectID, imDocumentData, imDocumentData.FileName, imDocumentData.FileAttributeIndex, false);
  }

  private bool CheckExistanceAttr(IUserSession session, IDBObject obj, Guid attrGuid)
  {
    ImDocumentData node = (ImDocumentData) null;
    try
    {
      node = ImDocumentServerPlugin.LoadDocumentFromDBObjectCore(obj);
    }
    catch
    {
    }
    if (node != null)
    {
      foreach (DocumentTreeNode childNode in DocumentTreeNode.GetChildNodes((DocumentTreeNode) node))
      {
        if (childNode is INodeWithReference nodeWithReference && nodeWithReference.Reference is ReferenceToDBObjectAttributeBase reference && reference.AttributeGuid == attrGuid)
          return true;
      }
    }
    return false;
  }
}
