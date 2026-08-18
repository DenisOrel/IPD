// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseObjectsCreators.ImbaseCatalogCreator
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Imbase.Cache;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.ImbaseObjectsCreators;

internal class ImbaseCatalogCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  private static string _catalogTypeName = string.Empty;
  private IDictionary<ObjectCreatePages, bool> _createPages;

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  public bool AcceptDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    return false;
  }

  public bool AfterCreate(long newObjectID)
  {
    if (newObjectID == -1L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(newObjectID, false);
      if (objectActualCopy == null)
        return false;
      IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(Intermech.Imbase.Consts.CatalogTypeAttGUID);
      if (attributeByGuid == null)
        return false;
      attributeByGuid.AsString = ImbaseCatalogCreator.CatalogTypeName;
    }
    return true;
  }

  public IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      if (this._createPages == null)
      {
        this._createPages = (IDictionary<ObjectCreatePages, bool>) new Dictionary<ObjectCreatePages, bool>();
        this._createPages.Add(ObjectCreatePages.Properties, true);
        this._createPages.Add(ObjectCreatePages.Template, true);
      }
      return this._createPages;
    }
  }

  public bool OnCommitAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public bool OnCancelAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public Dictionary<UserControl, int> AddPages(object CreatedObject, int propPageIndex)
  {
    return (Dictionary<UserControl, int>) null;
  }

  public long CreateObjectDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    return (long) -sc_7826.ssp_imbase_7827(497519539);
  }

  public static string CatalogTypeName
  {
    get
    {
      return string.IsNullOrEmpty(ImbaseCatalogCreator._catalogTypeName) && CatalogTypes.Names.Length != 0 ? CatalogTypes.Names[0] : ImbaseCatalogCreator._catalogTypeName;
    }
    set => ImbaseCatalogCreator._catalogTypeName = value;
  }
}
