// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.FormsCache
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.FormDesigner.Server;

internal class FormsCache
{
  private readonly IDictionary<int, FormsCache.CacheItems> _objsForms = (IDictionary<int, FormsCache.CacheItems>) new ConcurrentDictionary<int, FormsCache.CacheItems>();
  private readonly IDictionary<int, FormsCache.CacheItems> _relsForms = (IDictionary<int, FormsCache.CacheItems>) new ConcurrentDictionary<int, FormsCache.CacheItems>();

  private IDictionary<int, FormsCache.CacheItems> GetDictType(AttributableElements kind)
  {
    if (kind == AttributableElements.Object)
      return this._objsForms;
    return kind != AttributableElements.Relation ? (IDictionary<int, FormsCache.CacheItems>) null : this._relsForms;
  }

  private void DoProceedFormInfo(
    FormInformation fi,
    AttributableElements kind,
    FormsCache.ProceedFormInfo proceedProc)
  {
    if (fi == null || proceedProc == null)
      return;
    IDictionary<int, FormsCache.CacheItems> dictType = this.GetDictType(kind);
    int[] array = new int[dictType.Keys.Count];
    dictType.Keys.CopyTo(array, 0);
    lock (dictType)
    {
      foreach (int key in array)
      {
        FormsCache.CacheItems cacheItems;
        FormsCache.CacheItem cacheItem;
        if (dictType.TryGetValue(key, out cacheItems) && cacheItems != null && cacheItems.TryGetValue(fi.ID, out cacheItem) && cacheItem != null)
        {
          if (!proceedProc(fi, cacheItem, cacheItems))
            cacheItem.FormInfo.Caption = fi.Caption;
          else if (cacheItems.Count <= 0)
            dictType.Remove(key);
        }
      }
    }
  }

  private bool DoChangeCheckInInfo(
    FormInformation fi,
    FormsCache.CacheItem cacheItem,
    FormsCache.CacheItems cacheItems)
  {
    bool flag = false;
    if (fi != null && cacheItem != null && cacheItems != null)
    {
      cacheItem.FormInfo.CheckOutBy = fi.CheckOutBy;
      cacheItem.FormInfo.HasFormula = fi.HasFormula;
      if (!cacheItem.Visible4EditUser)
      {
        cacheItems.TryRemove(fi.ID, out cacheItem);
        flag = true;
      }
      else
        cacheItem.Visible4AllUser = true;
    }
    return flag;
  }

  private bool DoChangeCheckOutInfo(
    FormInformation fi,
    FormsCache.CacheItem cacheItem,
    FormsCache.CacheItems cacheItems)
  {
    int num = 0;
    if (fi == null)
      return num != 0;
    if (cacheItem == null)
      return num != 0;
    if (cacheItems == null)
      return num != 0;
    cacheItem.FormInfo.CheckOutBy = fi.CheckOutBy;
    return num != 0;
  }

  private bool DoMarkAsRemoved(
    FormInformation fi,
    FormsCache.CacheItem cacheItem,
    FormsCache.CacheItems cacheItems)
  {
    bool flag = false;
    if (fi != null && cacheItem != null && cacheItems != null)
    {
      if (cacheItem.Visible4AllUser)
      {
        cacheItem.Visible4EditUser = false;
      }
      else
      {
        cacheItems.TryRemove(fi.ID, out cacheItem);
        flag = true;
      }
    }
    return flag;
  }

  private bool DoRemove(
    FormInformation fi,
    FormsCache.CacheItem cacheItem,
    FormsCache.CacheItems cacheItems)
  {
    bool flag = false;
    if (fi != null && cacheItem != null && cacheItems != null)
    {
      cacheItems.TryRemove(fi.ID, out cacheItem);
      flag = true;
    }
    return flag;
  }

  private bool DoUndoCheckOutInfo(
    FormInformation fi,
    FormsCache.CacheItem cacheItem,
    FormsCache.CacheItems cacheItems)
  {
    bool flag = false;
    if (fi != null && cacheItem != null && cacheItems != null)
    {
      cacheItem.FormInfo.CheckOutBy = 0L;
      if (!cacheItem.Visible4AllUser)
      {
        cacheItems.TryRemove(fi.ID, out cacheItem);
        flag = true;
      }
      else
        cacheItem.Visible4EditUser = true;
    }
    return flag;
  }

  internal List<FormsCache.CacheItem> Add(
    int[] typeIDs,
    FormInformation fi,
    AttributableElements kind)
  {
    List<FormsCache.CacheItem> cacheItemList = (List<FormsCache.CacheItem>) null;
    if (typeIDs != null && typeIDs.Length != 0 && fi != null)
    {
      cacheItemList = new List<FormsCache.CacheItem>(typeIDs.Length);
      IDictionary<int, FormsCache.CacheItems> dictType = this.GetDictType(kind);
      lock (dictType)
      {
        foreach (int typeId in typeIDs)
        {
          FormsCache.CacheItems cacheItems;
          if (!dictType.TryGetValue(typeId, out cacheItems))
          {
            cacheItems = new FormsCache.CacheItems();
            dictType.Add(typeId, cacheItems);
          }
          FormsCache.CacheItem cacheItem;
          if (!cacheItems.TryGetValue(fi.ID, out cacheItem))
          {
            cacheItem = new FormsCache.CacheItem(fi, visible4EditUser: true);
            cacheItems.TryAdd(fi.ID, cacheItem);
            cacheItemList.Add(cacheItem);
          }
          else
          {
            cacheItem.Visible4EditUser = true;
            cacheItemList.Add(cacheItem);
          }
        }
      }
    }
    return cacheItemList ?? new List<FormsCache.CacheItem>(0);
  }

  internal FormsCache.CacheItem Add(int typeId, FormInformation fi, AttributableElements kind)
  {
    FormsCache.CacheItem cacheItem = (FormsCache.CacheItem) null;
    if (fi != null)
    {
      List<FormsCache.CacheItem> cacheItemList = this.Add(new int[1]
      {
        typeId
      }, fi, kind);
      cacheItem = cacheItemList == null || cacheItemList.Count == 0 ? (FormsCache.CacheItem) null : cacheItemList[0];
    }
    return cacheItem;
  }

  internal void ChangeCheckInInfo(FormInformation fi, AttributableElements kind)
  {
    this.DoProceedFormInfo(fi, kind, new FormsCache.ProceedFormInfo(this.DoChangeCheckInInfo));
  }

  internal void ChangeCheckOutInfo(FormInformation fi, AttributableElements kind)
  {
    this.DoProceedFormInfo(fi, kind, new FormsCache.ProceedFormInfo(this.DoChangeCheckOutInfo));
  }

  internal void MarkAsRemoved(FormInformation fi, AttributableElements kind)
  {
    this.DoProceedFormInfo(fi, kind, new FormsCache.ProceedFormInfo(this.DoMarkAsRemoved));
  }

  internal void MarkAsRemoved(int typeId, FormInformation fi, AttributableElements kind)
  {
    this.MarkAsRemoved(new int[1]{ typeId }, fi, kind);
  }

  internal void MarkAsRemoved(int[] typeIDs, FormInformation fi, AttributableElements kind)
  {
    if (typeIDs == null || typeIDs.Length == 0 || fi == null)
      return;
    IDictionary<int, FormsCache.CacheItems> dictType = this.GetDictType(kind);
    lock (dictType)
    {
      foreach (int typeId in typeIDs)
      {
        FormsCache.CacheItems cacheItems;
        FormsCache.CacheItem cacheItem;
        if (dictType.TryGetValue(typeId, out cacheItems) && cacheItems.TryGetValue(fi.ID, out cacheItem))
        {
          if (cacheItem.Visible4AllUser)
          {
            cacheItem.Visible4EditUser = false;
          }
          else
          {
            cacheItems.TryRemove(fi.ID, out cacheItem);
            if (cacheItems.Count <= 0)
              dictType.Remove(typeId);
          }
        }
      }
    }
  }

  internal void Remove(FormInformation fi, AttributableElements kind)
  {
    this.DoProceedFormInfo(fi, kind, new FormsCache.ProceedFormInfo(this.DoRemove));
  }

  internal void RemoveType(int typeId, AttributableElements kind)
  {
    this.RemoveType(new int[1]{ typeId }, kind);
  }

  internal void RemoveType(int[] typesId, AttributableElements kind)
  {
    if (typesId == null || typesId.Length == 0)
      return;
    IDictionary<int, FormsCache.CacheItems> dictType = this.GetDictType(kind);
    lock (dictType)
    {
      foreach (int key in typesId)
      {
        if (!dictType.ContainsKey(key))
          break;
        dictType.Remove(key);
      }
    }
  }

  internal void UndoCheckOutInfo(FormInformation fi, AttributableElements kind)
  {
    this.DoProceedFormInfo(fi, kind, new FormsCache.ProceedFormInfo(this.DoUndoCheckOutInfo));
  }

  internal FormsCache.CacheItems GetTypesForms(int typeId, AttributableElements kind)
  {
    FormsCache.CacheItems typesForms;
    if (!this.GetDictType(kind).TryGetValue(typeId, out typesForms))
      typesForms = new FormsCache.CacheItems();
    return typesForms;
  }

  internal List<FormsCache.CacheItem> GetFormsById(long[] formIDs, AttributableElements kind)
  {
    List<FormsCache.CacheItem> formsById;
    if (formIDs == null || formIDs.Length == 0)
    {
      formsById = new List<FormsCache.CacheItem>();
    }
    else
    {
      formsById = new List<FormsCache.CacheItem>(formIDs.Length);
      IDictionary<int, FormsCache.CacheItems> dictType = this.GetDictType(kind);
      if (dictType != null && dictType.Count > 0)
      {
        foreach (long formId in formIDs)
        {
          foreach (KeyValuePair<int, FormsCache.CacheItems> keyValuePair in (IEnumerable<KeyValuePair<int, FormsCache.CacheItems>>) dictType)
          {
            FormsCache.CacheItem cacheItem;
            if (keyValuePair.Value != null && keyValuePair.Value.TryGetValue(formId, out cacheItem))
            {
              formsById.Add(cacheItem);
              break;
            }
          }
        }
      }
    }
    return formsById;
  }

  internal delegate bool ProceedFormInfo(
    FormInformation formInfo,
    FormsCache.CacheItem cacheItem,
    FormsCache.CacheItems cacheItems);

  public class CacheItem : CacheHelper.CacheBaseItem<bool>
  {
    protected bool _visible4EditUser;

    public bool Visible4AllUser
    {
      [DebuggerStepThrough] get => this._value;
      set => this._value = value;
    }

    public bool Visible4EditUser
    {
      [DebuggerStepThrough] get => this._visible4EditUser;
      set => this._visible4EditUser = value;
    }

    public CacheItem(FormInformation formInfo, bool visible4AllUser = false, bool visible4EditUser = false)
      : base(formInfo, visible4AllUser)
    {
      this._visible4EditUser = visible4EditUser;
    }
  }

  public class CacheItems : ConcurrentDictionary<long, FormsCache.CacheItem>
  {
  }
}
