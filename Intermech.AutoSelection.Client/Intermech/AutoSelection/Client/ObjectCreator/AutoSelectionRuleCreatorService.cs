// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.ObjectCreator.AutoSelectionRuleCreatorService
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection.AutoSelectionCache;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.ObjectCreator;

internal class AutoSelectionRuleCreatorService : 
  IObjectCreatorRiderCustomService,
  IObjectCreatorCustomService
{
  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  public virtual bool AcceptDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIds,
    DateTime startDate,
    bool isVersion)
  {
    return false;
  }

  public virtual bool AfterCreate(long newObjectId) => true;

  public virtual IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      return (IDictionary<ObjectCreatePages, bool>) new Dictionary<ObjectCreatePages, bool>()
      {
        {
          ObjectCreatePages.FileAttributes,
          true
        },
        {
          ObjectCreatePages.Properties,
          true
        },
        {
          ObjectCreatePages.Template,
          true
        }
      };
    }
  }

  public virtual bool OnCommitAction(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    if (session == null || newObjectId == 0L)
      return false;
    IDBObject dbObject = session.GetObject(newObjectId, false);
    if (dbObject == null)
      return false;
    int attributeId = MetaDataHelper.GetAttributeID((object) "cad001a0-306c-11d8-b4e9-00304f19f545");
    Guid guid = Guid.Empty;
    IDBAttribute attributeById = dbObject.GetAttributeByID(attributeId);
    if (attributeById != null)
    {
      string str = attributeById.Value.ToString();
      if (GuidHelper.IsGuid(str))
        guid = new Guid(str);
    }
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule autoSelectionRule = new Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule(guid);
    string str1 = dbObject.Caption;
    if (str1 == string.Empty && guid != Guid.Empty)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(guid);
      if (objectType != null)
        str1 = objectType.ObjectName;
    }
    autoSelectionRule.Name = str1;
    autoSelectionRule.Save(dbObject, session);
    if (guid != Guid.Empty)
    {
      IAutoSelectionRuleCacheService autosServerService = AutoSelectionUtils.ServiceKeeper.GetAutosServerService();
      if (autosServerService == null)
        return true;
      List<long> ruleIdList = new List<long>()
      {
        newObjectId
      };
      autosServerService.RulesRegister(ruleIdList, (long) MetaDataHelper.GetObjectTypeID(guid), AutoSelectionLinkMode.asotObjectType, session.SessionGUID);
    }
    return true;
  }

  public virtual bool OnCancelAction(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public virtual Dictionary<UserControl, int> AddPages(object createdObject, int propPageIndex)
  {
    return (Dictionary<UserControl, int>) null;
  }

  public virtual long CreateObjectDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIds,
    long[] relatedObjectIds,
    DateTime startDate,
    bool isVersion)
  {
    throw new Exception("The method or operation is not implemented.");
  }
}
