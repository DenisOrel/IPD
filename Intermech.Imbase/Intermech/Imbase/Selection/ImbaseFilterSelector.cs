// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseFilterSelector
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseFilterSelector : IImbaseFilterSelector
{
  public long RecordID { get; set; }

  public List<long> CheckImbaseObjects(
    long catalogID,
    long objID,
    List<long> prevCheckedIDs,
    bool _objectVersionProcessed = true)
  {
    List<long> catalogIDs;
    if (catalogID == 0L)
    {
      catalogIDs = (List<long>) null;
    }
    else
    {
      catalogIDs = new List<long>();
      catalogIDs.Add(catalogID);
    }
    return this.CheckImbaseObjects(catalogIDs, objID, prevCheckedIDs, _objectVersionProcessed);
  }

  public List<long> CheckImbaseObjects(
    List<long> catalogIDs,
    long objID,
    List<long> prevCheckedIDs,
    bool objectVersionProcessed = true)
  {
    using (ImbaseFilterSelectionWindow filterSelectionWindow = new ImbaseFilterSelectionWindow(catalogIDs, objID, prevCheckedIDs))
    {
      filterSelectionWindow.ObjectVersionMode = objectVersionProcessed;
      return filterSelectionWindow.ShowDialog() == DialogResult.OK ? filterSelectionWindow.CheckedIDs : prevCheckedIDs;
    }
  }

  public long SelectImbaseObject(
    long catalogID,
    long objID,
    long prevSelectedID,
    ImbaseCatalogSelectMode mode = ImbaseCatalogSelectMode.imcmSelectFolder,
    bool _objectVersionProcessed = true)
  {
    List<long> catalogIDs;
    if (catalogID == 0L)
    {
      catalogIDs = (List<long>) null;
    }
    else
    {
      catalogIDs = new List<long>();
      catalogIDs.Add(catalogID);
    }
    return this.SelectImbaseObject(catalogIDs, (int[]) null, objID, prevSelectedID, mode, (Dictionary<TypedInfoItem, IEnumerable<AttributeValues>>) null, 0, _objectVersionProcessed);
  }

  public long SelectImbaseObject(
    long catalogID,
    int needObjType,
    long objID,
    long prevSelectedID,
    ImbaseCatalogSelectMode mode,
    bool _objectVersionProcessed = true)
  {
    List<long> longList;
    if (catalogID == 0L)
    {
      longList = (List<long>) null;
    }
    else
    {
      longList = new List<long>();
      longList.Add(catalogID);
    }
    List<long> catalogIDs = longList;
    int[] numArray;
    if (needObjType == -1)
      numArray = (int[]) null;
    else
      numArray = new int[1]{ needObjType };
    int[] needObjTypes = numArray;
    return this.SelectImbaseObject(catalogIDs, needObjTypes, objID, prevSelectedID, mode, (Dictionary<TypedInfoItem, IEnumerable<AttributeValues>>) null, 0, _objectVersionProcessed);
  }

  public long SelectImbaseObject(
    List<long> catalogIDs,
    int[] needObjTypes,
    long objID,
    long prevSelectedID,
    ImbaseCatalogSelectMode mode,
    Dictionary<TypedInfoItem, IEnumerable<AttributeValues>> dict = null,
    int attrID = 0,
    bool objectVersionProcessed = true)
  {
    long num = prevSelectedID;
    List<int> list = needObjTypes != null ? ((IEnumerable<int>) needObjTypes).ToList<int>() : (List<int>) null;
    using (ImbaseFilterSelectionWindow filterSelectionWindow = new ImbaseFilterSelectionWindow(catalogIDs, objID, prevSelectedID, list, mode))
    {
      if (this.RecordID != -1L)
        filterSelectionWindow.RecordID = this.RecordID;
      filterSelectionWindow.ObjectVersionMode = objectVersionProcessed;
      filterSelectionWindow.ExtraAttrValues = dict;
      filterSelectionWindow.AttributeID = attrID;
      if (filterSelectionWindow.ShowDialog() == DialogResult.OK)
      {
        num = filterSelectionWindow.SelectedID;
        this.RecordID = filterSelectionWindow.RecordID;
      }
    }
    return num;
  }

  public List<long> SelectImbaseObjects(
    List<long> catalogIDs,
    int[] needObjTypes,
    long objID,
    List<long> prevSelectedID,
    ImbaseCatalogSelectMode mode,
    Dictionary<TypedInfoItem, IEnumerable<AttributeValues>> dict = null,
    int attrID = 0,
    bool objectVersionProcessed = true)
  {
    List<long> longList = prevSelectedID;
    List<int> list = needObjTypes != null ? ((IEnumerable<int>) needObjTypes).ToList<int>() : (List<int>) null;
    using (ImbaseFilterSelectionWindow filterSelectionWindow = new ImbaseFilterSelectionWindow(catalogIDs, objID, (List<long>) null, list, mode))
    {
      if (this.RecordID != -1L)
        filterSelectionWindow.RecordID = this.RecordID;
      filterSelectionWindow.ObjectVersionMode = objectVersionProcessed;
      filterSelectionWindow.ExtraAttrValues = dict;
      filterSelectionWindow.AttributeID = attrID;
      if (filterSelectionWindow.ShowDialog() == DialogResult.OK)
      {
        longList = filterSelectionWindow.CheckedIDs_1726096;
        this.RecordID = filterSelectionWindow.RecordID;
      }
    }
    return longList;
  }
}
