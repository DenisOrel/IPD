// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Services.ArticleAttributesSyncService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server.Services;

internal class ArticleAttributesSyncService : LongLifeObject, IArticleAttributesSyncService
{
  private List<int> _AttributesToSync = new List<int>();
  private List<int> _MainDocuments = new List<int>();
  private bool _EventsAdded;

  internal ArticleAttributesSyncService(IUserSession session)
  {
    this.SetSettings(this.LoadSettings(session), session);
    this.SetEvents(session);
  }

  private void SetEvents(IUserSession session)
  {
    if (this._AttributesToSync.Count <= 0 || this._MainDocuments.Count <= 0 || this._EventsAdded)
      return;
    (session as UserSession).EventLogHelper.AddAttributeWriteHandler((object) 0, new WriteAttributeValueHandler(this.WriteAttributeValue));
    (session as UserSession).EventLogHelper.AfterCreateRelationExEvent += new CreateRelationExHandler(this.AfterCreateRelationExEvent);
    (session as UserSession).EventLogHelper.AfterCommitCreationObjectEvent += new ObjectEventHandler(this.AfterDocCommitCreation);
    (session as UserSession).EventLogHelper.ObligatoryAttributeWrite += new Intermech.Interfaces.Server.ObligatoryAttributeWriteHandler(this.ObligatoryAttributeWriteHandler);
    this._EventsAdded = true;
  }

  private void AfterDocCommitCreation(IDBObject sender, IUserSession session)
  {
    if (this._MainDocuments.IndexOf(sender.ObjectType) <= -1)
      return;
    List<IDBAttribute> dbAttributeList = (List<IDBAttribute>) null;
    lock (this._AttributesToSync)
    {
      for (int index = 0; index < this._AttributesToSync.Count; ++index)
      {
        if (MetaDataHelper.GetAttribute4ObjectType(sender.ObjectType, this._AttributesToSync[index]) != null)
        {
          IDBAttribute attributeById = sender.GetAttributeByID(this._AttributesToSync[index]);
          if (attributeById != null)
          {
            if (dbAttributeList == null)
              dbAttributeList = new List<IDBAttribute>();
            dbAttributeList.Add(attributeById);
          }
        }
      }
    }
    if (this._AttributesToSync.IndexOf(-14) > -1)
      this.SyncProjectID(sender.ObjectID, sender.ProjectID, session);
    if (dbAttributeList == null)
      return;
    DataTable dataTable = session.GetRelationCollection(session.IdentHelper.DocRelationTypeID, "cad005aa-306c-11d8-b4e9-00304f19f545").EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -21,
      (object) -7
    }), sender.ObjectID);
    for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
    {
      for (int index2 = 0; index2 < dbAttributeList.Count; ++index2)
      {
        if (MetaDataHelper.GetAttribute4ObjectType(Convert.ToInt32(dataTable.Rows[index1][1]), dbAttributeList[index2].AttributeID) != null)
        {
          IDBObject article = session.GetObject(Convert.ToInt64(dataTable.Rows[index1][0]), false);
          if (article != null)
            this.SetAttributeValue(article, dbAttributeList[index2].AttributeID, dbAttributeList[index2].Value);
        }
      }
    }
  }

  private void SyncProjectID(long docID, long projectID, IUserSession session)
  {
    DataTable dataTable = session.GetRelationCollection(session.IdentHelper.DocRelationTypeID, "cad005aa-306c-11d8-b4e9-00304f19f545").EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -21,
      (object) -7
    }), docID);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      IDBObject dbObject = session.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false);
      if (dbObject != null)
        dbObject.ProjectID = projectID;
    }
  }

  private void ObligatoryAttributeWriteHandler(
    IDBObject sender,
    ObligatoryObjectAttributes attrID,
    ObligatoryAttributeValueEventArgs args)
  {
    if (attrID != ObligatoryObjectAttributes.F_PROJECT_ID || this._AttributesToSync.IndexOf((int) attrID) <= -1 || this._MainDocuments.IndexOf(sender.ObjectType) <= -1)
      return;
    this.SyncProjectID(sender.ObjectID, Convert.ToInt64(args.NewValue), args.Session);
  }

  private void WriteAttributeValue(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    if (this._AttributesToSync.IndexOf(attribute.AttributeID) <= -1)
      return;
    DBAttribute dbAttribute = attribute as DBAttribute;
    if (!dbAttribute.IsObjectAttribute || this._MainDocuments.IndexOf(dbAttribute.TypeID) <= -1)
      return;
    DataTable dataTable = dbAttribute.UserSession.GetRelationCollection(dbAttribute.UserSession.IdentHelper.DocRelationTypeID, "cad005aa-306c-11d8-b4e9-00304f19f545").EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -21,
      (object) -7
    }), dbAttribute.DBObjectID);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (MetaDataHelper.GetAttribute4ObjectType(Convert.ToInt32(dataTable.Rows[index][1]), dbAttribute.AttributeID) != null)
      {
        IDBObject article = dbAttribute.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false);
        if (article != null)
          this.SetAttributeValue(article, dbAttribute.AttributeID, args.Value);
      }
    }
  }

  private void AfterCreateRelationExEvent(IDBRelation sender, IUserSession session, int assignMode)
  {
    if (sender.RelationType != session.IdentHelper.DocRelationTypeID || sender.PartObjectID == 0L)
      return;
    IDBObject partObject = (sender as DBRelation).PartObject;
    IDBObject article = (IDBObject) null;
    if (this._MainDocuments.IndexOf(partObject.ObjectType) <= -1)
      return;
    lock (this._AttributesToSync)
    {
      for (int index = 0; index < this._AttributesToSync.Count; ++index)
      {
        if (MetaDataHelper.GetAttribute4ObjectType(partObject.ObjectType, this._AttributesToSync[index]) != null)
        {
          IDBAttribute attributeById = partObject.GetAttributeByID(this._AttributesToSync[index]);
          if (attributeById != null)
          {
            if (article == null)
              article = session.GetObject(sender.ProjID);
            this.SetAttributeValue(article, attributeById.AttributeID, attributeById.Value);
          }
        }
      }
      if (this._AttributesToSync.IndexOf(-14) <= -1)
        return;
      this.SyncProjectID(partObject.ObjectID, partObject.ProjectID, session);
    }
  }

  private void SetAttributeValue(IDBObject article, int attributeID, object value)
  {
    IDBAttribute attributeById = article.GetAttributeByID(attributeID);
    if (attributeById != null)
    {
      if (!attributeById.ReadOnly)
      {
        attributeById.Value = value;
      }
      else
      {
        if (article.ObjectModifyMode != ObjectModifyModes.Checkout || article.CheckoutBy != 0L)
          return;
        article = article.CheckOut();
        article.GetAttributeByID(attributeID).Value = value;
      }
    }
    else
    {
      bool flag = !article.ReadOnly;
      if (!flag)
      {
        IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(article.ObjectType, attributeID);
        if (attribute4ObjectType != null)
          flag = !attribute4ObjectType.IsContent && (attribute4ObjectType.Options & AttributeOptions.ModifyInBase) == AttributeOptions.ModifyInBase;
      }
      if (!flag)
        return;
      UserSession userSession = (article as DBObject).UserSession;
      if (userSession.InTransaction)
      {
        try
        {
          bool autoRollback = userSession.AutoRollback;
          userSession.AutoRollback = false;
          try
          {
            article.Attributes.AddAttribute(attributeID, false, new object[1]
            {
              value
            });
          }
          finally
          {
            userSession.AutoRollback = autoRollback;
          }
        }
        catch
        {
        }
      }
      else
      {
        try
        {
          article.Attributes.AddAttribute(attributeID, false, new object[1]
          {
            value
          });
        }
        catch
        {
        }
      }
    }
  }

  private ArticleAttributesSyncSettings LoadSettings(IUserSession session)
  {
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    string str1 = session.Configurations.ReadString("PDM", "AttrSyncSection", "Attributes", string.Empty, DBConfigMode.GlobalOnly);
    if (str1 != string.Empty)
    {
      string str2 = str1;
      char[] chArray = new char[1]{ ',' };
      foreach (string str3 in str2.Split(chArray))
        intList1.Add(Convert.ToInt32(str3));
    }
    string str4 = session.Configurations.ReadString("PDM", "AttrSyncSection", "MainDocs", string.Empty, DBConfigMode.GlobalOnly);
    if (str4 != string.Empty)
    {
      string str5 = str4;
      char[] chArray = new char[1]{ ',' };
      foreach (string str6 in str5.Split(chArray))
        intList2.Add(Convert.ToInt32(str6));
    }
    return new ArticleAttributesSyncSettings(intList1.ToArray(), intList2.ToArray());
  }

  private void SetSettings(ArticleAttributesSyncSettings settings, IUserSession session)
  {
    lock (this._AttributesToSync)
    {
      this._AttributesToSync.Clear();
      this._AttributesToSync.AddRange((IEnumerable<int>) settings.SyncAttributes);
    }
    lock (this._MainDocuments)
    {
      this._MainDocuments.Clear();
      for (int index1 = 0; index1 < settings.MainDocumentsTypes.Length; ++index1)
      {
        IDBObjectType objectType = session.GetObjectType(settings.MainDocumentsTypes[index1], false);
        if (objectType != null)
        {
          ArrayList objsTreeList = new ArrayList();
          objectType.FillChildrenList(objsTreeList);
          for (int index2 = 0; index2 < objsTreeList.Count; ++index2)
            this._MainDocuments.Add((int) objsTreeList[index2]);
        }
      }
    }
  }

  public void WriteSyncSettings(ArticleAttributesSyncSettings settings, Guid sessionGuid)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(126);
    for (int index = 0; index < settings.SyncAttributes.Length; ++index)
    {
      IDBAttributeType attributeType = sessionById.GetAttributeType(settings.SyncAttributes[index], true);
      if (attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList)
        throw new KernelException($"Атрибут '{attributeType.Name}' является многозначным, поэтому не может быть синхронизирован.");
    }
    string str1 = string.Join<int>(",", (IEnumerable<int>) settings.SyncAttributes);
    string str2 = string.Join<int>(",", (IEnumerable<int>) settings.MainDocumentsTypes);
    sessionById.Configurations.WriteString("PDM", "AttrSyncSection", "Attributes", str1, 0L);
    sessionById.Configurations.WriteString("PDM", "AttrSyncSection", "MainDocs", str2, 0L);
    this.SetSettings(settings, sessionById);
    this.SetEvents(sessionById);
  }

  public ArticleAttributesSyncSettings ReadSyncSett(Guid sessionGuid)
  {
    return this.LoadSettings(UserSession.GetSessionByID(sessionGuid));
  }
}
