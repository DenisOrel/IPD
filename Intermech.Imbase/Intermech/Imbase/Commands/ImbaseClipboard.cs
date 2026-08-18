// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Commands.ImbaseClipboard
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Commands;

internal class ImbaseClipboard : 
  ICutCopy,
  IDBObjectTypedIDCollection,
  ITypedIDCollection,
  IEnumerator
{
  private string _caption = string.Empty;
  protected List<ClipboardObject> _collection;
  private int _position = -1;

  public List<long> FolderIDs { get; private set; }

  public List<long> RecordIDs { get; private set; }

  public List<long> LinkIDs { get; private set; }

  public List<long> FavoritesIDs { get; private set; }

  public bool HasFolder => this.FolderIDs != null;

  public ILookup<int, ClipboardObject> GetObjsGroupedByType
  {
    get
    {
      return this._collection.ToLookup<ClipboardObject, int, ClipboardObject>((Func<ClipboardObject, int>) (x => x.ObjectType), (Func<ClipboardObject, ClipboardObject>) (x => x));
    }
  }

  public bool FromFavorites
  {
    get
    {
      return this._collection.Any<ClipboardObject>((Func<ClipboardObject, bool>) (x => x.RelationType == Intermech.Imbase.Consts.ImbaseFavoritesRelationID));
    }
  }

  public ImbaseClipboard(List<ClipboardObject> collection, bool isCut)
  {
    this._collection = collection ?? new List<ClipboardObject>(0);
    this.IsCut = isCut;
    this.ImageIndex = -1;
    this.Parse();
    this.GroupByType();
  }

  public bool IsCut { get; set; }

  public int ImageIndex { get; private set; }

  public IDBTypedObjectID[] GetTypedObjects() => (IDBTypedObjectID[]) this._collection.ToArray();

  public IDBRelationID GetRelationID(int index)
  {
    return index <= -1 || index >= this._collection.Count ? (IDBRelationID) null : (IDBRelationID) this._collection[index];
  }

  public IDBRelationID[] GetRelations() => (IDBRelationID[]) this._collection.ToArray();

  object ITypedIDCollection.this[int index] => (object) this._collection[index];

  object IEnumerator.Current => (object) this.Current;

  public ClipboardObject Current
  {
    get
    {
      try
      {
        return this._collection[this._position];
      }
      catch (IndexOutOfRangeException ex)
      {
        throw new InvalidOperationException();
      }
    }
  }

  public bool MoveNext()
  {
    ++this._position;
    return this._position < this._collection.Count;
  }

  public void Reset() => this._position = -1;

  public override bool Equals(object obj)
  {
    bool flag = false;
    if (obj is ImbaseClipboard imbaseClipboard && this._collection.Count == imbaseClipboard.Count)
    {
      flag = true;
      foreach (IDBTypedObjectID dbTypedObjectId in imbaseClipboard._collection)
      {
        IDBTypedObjectID item2 = dbTypedObjectId;
        if (this._collection.FirstOrDefault<ClipboardObject>((Func<ClipboardObject, bool>) (x => x.ObjectID == item2.ObjectID)) == null)
        {
          flag = false;
          break;
        }
      }
      if (flag)
        imbaseClipboard.IsCut = this.IsCut;
    }
    return flag;
  }

  public override int GetHashCode() => base.GetHashCode();

  public override string ToString() => this._caption;

  private void Parse()
  {
    ICategoryTypeIconService service = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    if (this._collection.Count == 1)
    {
      ClipboardObject clipboardObject = this._collection[0];
      this._caption = clipboardObject.ToString();
      if (service == null)
        return;
      this.ImageIndex = service.IndexOf(4, clipboardObject.IDBTypedObjectID.ObjectType);
    }
    else
    {
      int category = Intermech.Imbase.Consts.ImbaseComplexObjectsID;
      List<int> list = this._collection.Select<ClipboardObject, int>((Func<ClipboardObject, int>) (x => x.ObjectType)).Distinct<int>().ToList<int>();
      if (list.Count == 1)
      {
        if (list[0] == Intermech.Imbase.Consts.ImbaseFolderTypeID)
        {
          this._caption = string.Format(LocalizationHolder.rm.GetString("Imbase_ImbaseFolders_Count"), (object) this._collection.Count);
          category = Intermech.Imbase.Consts.ImbaseFoldersID;
        }
        else if (list[0] == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID)
        {
          this._caption = string.Format(LocalizationHolder.rm.GetString("Imbase_ImbaseCatalogRecords_Count"), (object) this._collection.Count);
          category = Intermech.Imbase.Consts.ImbaseCatalogRecordsID;
        }
        else if (list[0] == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        {
          this._caption = string.Format(LocalizationHolder.rm.GetString("Imbase_ImbaseTableRefs_Count"), (object) this._collection.Count);
          category = Intermech.Imbase.Consts.ImbaseTableRefsID;
        }
        else if (list[0] == Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
        {
          this._caption = string.Format(LocalizationHolder.rm.GetString("Imbase_Favorites_Folder_Count"), (object) this._collection.Count);
          category = Intermech.Imbase.Consts.ImbaseFavoritesID;
        }
      }
      else
        this._caption = string.Format(LocalizationHolder.rm.GetString("Imbase_ImbaseObjects_Count"), (object) this._collection.Count);
      this.ImageIndex = service != null ? service.IndexOf(category, -1) : -1;
    }
  }

  private void GroupByType()
  {
    this.FolderIDs = new List<long>(this._collection.Count);
    this.RecordIDs = new List<long>(this._collection.Count);
    this.LinkIDs = new List<long>(this._collection.Count);
    this.FavoritesIDs = new List<long>(this._collection.Count);
    foreach (ClipboardObject clipboardObject in this._collection)
    {
      int objectType = clipboardObject.ObjectType;
      if (objectType == Intermech.Imbase.Consts.ImbaseFolderTypeID)
        this.FolderIDs.Add(clipboardObject.ObjectID);
      else if (objectType == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID)
        this.RecordIDs.Add(clipboardObject.ObjectID);
      else if (objectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        this.LinkIDs.Add(clipboardObject.ObjectID);
      else if (objectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
        this.FavoritesIDs.Add(clipboardObject.ObjectID);
    }
    if (this.FolderIDs.Count == 0)
      this.FolderIDs = (List<long>) null;
    if (this.RecordIDs.Count == 0)
      this.RecordIDs = (List<long>) null;
    if (this.LinkIDs.Count == 0)
      this.LinkIDs = (List<long>) null;
    if (this.FavoritesIDs.Count != 0)
      return;
    this.FavoritesIDs = (List<long>) null;
  }

  public ClipboardObject this[int index]
  {
    get
    {
      return index <= -1 || index >= this._collection.Count ? (ClipboardObject) null : this._collection[index];
    }
  }

  public int Count => this._collection.Count;

  public IDBTypedObjectID GetTypedObjectID(int index)
  {
    return index <= -1 || index >= this._collection.Count ? (IDBTypedObjectID) null : (IDBTypedObjectID) this._collection[index];
  }
}
