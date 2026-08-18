// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.DynamicSelectionHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Selection;

internal class DynamicSelectionHelper
{
  private ArrayList _list;
  private DynamicSelectionEventHandler _handler;
  private long _catalogId;
  private int _needType;
  private bool _rawObject;
  private bool _commitCreation;
  private IImbaseSelector _selector;
  public static Dictionary<long, List<long>> _selectedContext = new Dictionary<long, List<long>>();

  public static event EventHandler ContextChanged;

  public static void AddSelection(long linkId, long recordId)
  {
    bool flag = true;
    List<long> longList;
    if (DynamicSelectionHelper._selectedContext.ContainsKey(linkId))
    {
      longList = DynamicSelectionHelper._selectedContext[linkId];
      flag = false;
    }
    else
      longList = new List<long>(32 /*0x20*/);
    if (!longList.Contains(recordId))
      longList.Add(recordId);
    if (flag)
      DynamicSelectionHelper._selectedContext[linkId] = longList;
    DynamicSelectionHelper.OnContextChanged();
  }

  public static void Clear() => DynamicSelectionHelper._selectedContext.Clear();

  public static bool IsSelected(long linkId, long recId)
  {
    return DynamicSelectionHelper._selectedContext.ContainsKey(linkId) && DynamicSelectionHelper._selectedContext[linkId].Contains(recId);
  }

  internal static void OnContextChanged()
  {
    EventHandler contextChanged = DynamicSelectionHelper.ContextChanged;
    if (contextChanged == null)
      return;
    contextChanged((object) null, EventArgs.Empty);
  }

  public DynamicSelectionHelper(
    IImbaseSelector selector,
    ArrayList list,
    DynamicSelectionEventHandler handler,
    long catalogId,
    int needType,
    bool rawObject,
    bool commitCreation)
  {
    this._list = list;
    this._handler = handler;
    this._catalogId = catalogId;
    this._selector = selector;
    this._needType = needType;
    this._rawObject = rawObject;
    this._commitCreation = commitCreation;
    DynamicSelectionHelper.Clear();
  }

  public bool Handler(long objectId, DynamicSelectionMode mode)
  {
    long recordId = -1;
    if (this._handler(objectId, DynamicSelectionMode.PreSelect))
    {
      if (!this._rawObject)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IImbaseServer customService = sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
          if (ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).CommonParams.CheckApplicabilityBeforeCreateComposition)
          {
            ImbaseObjectCaptionItem imbaseObject;
            if (this._selector.ContextObjectId == -1L)
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectId);
              imbaseObject = new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(objectInfo.ObjectID, objectInfo.ObjectTypeID, objectInfo.Caption), -1L);
            }
            else
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._selector.ContextObjectId);
              imbaseObject = new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(objectInfo.ObjectID, objectInfo.ObjectTypeID, objectInfo.Caption), objectId);
            }
            if (!ImbaseUsageHelper.CanUseImbaseObject(imbaseObject))
              return true;
          }
          if (this._selector.ContextObjectId == -1L)
          {
            objectId = customService.CreateObject(sessionKeeper.Session.SessionGUID, this._catalogId, objectId, this._selector.ContextObjectId, this._commitCreation, this._needType);
          }
          else
          {
            recordId = objectId;
            objectId = customService.CreateObject(sessionKeeper.Session.SessionGUID, this._catalogId, this._selector.ContextObjectId, objectId, this._commitCreation, this._needType);
          }
        }
      }
      if (this._handler(objectId, DynamicSelectionMode.Select))
      {
        this._list.Add((object) objectId);
        if (recordId != -1L)
          DynamicSelectionHelper.AddSelection(this._selector.ContextObjectId, recordId);
      }
    }
    return true;
  }
}
