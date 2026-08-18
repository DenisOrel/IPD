// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.ImbaseFolder
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

#nullable disable
namespace Intermech.Imbase.API;

internal class ImbaseFolder : SingleThreadedObject, IIPSImbaseFolder
{
  private long _parentId;
  private long _folderId;
  private string _name;
  private IIPSImbaseFolders _folders;
  private DataRow[] _tableRows;
  private Dictionary<object, ImbaseTable> _tables = new Dictionary<object, ImbaseTable>();

  public ImbaseFolder(long parentId, long folderId)
  {
    Logger.Log("Folder.Ctor");
    this._folderId = folderId;
    this._parentId = parentId;
  }

  internal static void GetObjectProperties(long objectId, out string[] names, out object[] values)
  {
    List<string> stringList = new List<string>();
    List<object> objectList = new List<object>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeCollection attributes = sessionKeeper.Session.GetObject(objectId).Attributes;
      int count = attributes.Count;
      for (int AttrIndex = 0; AttrIndex < count; ++AttrIndex)
      {
        IDBAttribute dbAttribute = attributes[AttrIndex];
        if (dbAttribute.AttributeID > 0)
        {
          stringList.Add(dbAttribute.Name);
          objectList.Add(dbAttribute.Value);
        }
      }
    }
    names = stringList.ToArray();
    values = objectList.ToArray();
  }

  internal static object GetObjectProperty(long objectId, object index)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeCollection attributes = sessionKeeper.Session.GetObject(objectId).Attributes;
      bool flag = false;
      string str = string.Empty;
      int num = 0;
      if (index is string)
      {
        flag = true;
        str = index as string;
      }
      else
        num = (int) index;
      int count = attributes.Count;
      for (int AttrIndex = 0; AttrIndex < count; ++AttrIndex)
      {
        IDBAttribute dbAttribute = attributes[AttrIndex];
        if (dbAttribute.AttributeID > 0)
        {
          if (flag)
          {
            if (str.Equals(dbAttribute.Name, StringComparison.InvariantCultureIgnoreCase))
              return dbAttribute.Value;
          }
          else if (num-- == 0)
            return dbAttribute.Value;
        }
      }
      return (object) null;
    }
  }

  internal static void SetObjectProperty(long objectId, object index, object value)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject = session.GetObject(objectId);
      IDBAttributeCollection attributes = dbObject.Attributes;
      bool flag = false;
      string anAttributeName = string.Empty;
      int num = 0;
      if (index is string)
      {
        flag = true;
        anAttributeName = index as string;
      }
      else
        num = (int) index;
      int count = attributes.Count;
      for (int AttrIndex = 0; AttrIndex < count; ++AttrIndex)
      {
        IDBAttribute dbAttribute = attributes[AttrIndex];
        if (dbAttribute.AttributeID > 0)
        {
          if (flag)
          {
            if (anAttributeName.Equals(dbAttribute.Name, StringComparison.InvariantCultureIgnoreCase))
            {
              dbAttribute.Value = value;
              return;
            }
          }
          else if (num-- == 0)
          {
            dbAttribute.Value = value;
            return;
          }
        }
      }
      if (!flag)
        return;
      IDBAttributeType attributeType = session.GetAttributeType(anAttributeName);
      if (attributeType == null)
        return;
      dbObject.Attributes.AddAttribute(attributeType.AttributeID, false).Value = value;
    }
  }

  internal DataRow[] GetTableRows()
  {
    Logger.Log("Folder.getTableRows");
    if (this._tableRows == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable folderTables = ImbaseCatalog.GetFolderTables(sessionKeeper.Session, this._folderId);
        if (folderTables != null)
          this._tableRows = folderTables.Select();
      }
    }
    return this._tableRows;
  }

  public string Name
  {
    get
    {
      Logger.Log("Folder.Name");
      if (this._name == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this._name = sessionKeeper.Session.GetObjectInfo(this._folderId).Caption;
      }
      return this._name;
    }
    set
    {
      if (!(this._name != value))
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.GetObject(this._folderId).Caption = value;
      this._name = value;
    }
  }

  public void Delete() => ImbaseCatalog.DeleteObject(this._folderId);

  public IIPSImbaseFolders Folders
  {
    get
    {
      Logger.Log("Folder.Folders");
      if (this._folders == null)
        this._folders = (IIPSImbaseFolders) new ImbaseFolders(this._folderId, false);
      return this._folders;
    }
  }

  public string Note
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute attributeById = sessionKeeper.Session.GetObject(this._folderId).GetAttributeByID(Intermech.Imbase.Consts.ImbaseNoteAttID);
        return attributeById != null ? attributeById.AsString : string.Empty;
      }
    }
    set
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._folderId);
        (dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseNoteAttID) ?? dbObject.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseNoteAttID, false)).AsString = value;
      }
    }
  }

  public int Sort
  {
    get => 0;
    set
    {
    }
  }

  public void SetImage(string name, object data)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject = session.GetObject(this._folderId);
      long pictureObject = ImbaseFolder.CreatePictureObject(session, name, data);
      if (pictureObject == 0L)
        return;
      dbObject.Attributes.AddAttribute(Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID, false).AsInteger = pictureObject;
    }
  }

  private static long CreatePictureObject(IUserSession session, string fileName, object data)
  {
    long pictureObject = 0;
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Client.Core.Thumbnail.Consts.ImageLibraryItemTypeID);
    if (objectCollection != null)
    {
      IDBObject dbObject = objectCollection.Create();
      dbObject.Caption = Path.GetFileNameWithoutExtension(fileName);
      dbObject.CommitCreation(true);
      pictureObject = dbObject.ObjectID;
      IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(Intermech.Client.Core.Thumbnail.Consts.LibImageAttTypeID, false);
      byte[] buffer = data as byte[];
      IBlobWriter blobWriter = dbAttribute as IBlobWriter;
      using (MemoryStream inStream = new MemoryStream(buffer))
      {
        using (MemoryStream outStream = new MemoryStream())
        {
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) inStream, 9);
          outStream.Position = 0L;
          BlobInformation blobInfo = new BlobInformation(inStream.Length, outStream.Length, DateTime.Now, fileName, ArcMethods.ZLibPacked, fileName);
          blobWriter.OpenBlob(blobInfo, false);
          byte[] numArray = new byte[outStream.Length];
          if (outStream.Read(numArray, 0, numArray.Length) > 0)
            blobWriter.WriteDataBlock(numArray);
        }
      }
    }
    return pictureObject;
  }

  public byte[] GetImage(out string name)
  {
    name = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBAttribute attributeById1 = session.GetObject(this._folderId).GetAttributeByID(Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID);
      if (attributeById1 == null)
        return (byte[]) null;
      IDBAttribute attributeById2 = session.GetObject(attributeById1.AsInteger).GetAttributeByID(Intermech.Client.Core.Thumbnail.Consts.LibImageAttTypeID);
      if (attributeById2 == null || !(attributeById2 is IBlobReader blobReader))
        return (byte[]) null;
      BlobInformation blobInformation = blobReader.OpenBlob(0);
      name = blobInformation.Note;
      if (blobInformation.RealFileSize == 0L)
        return (byte[]) null;
      byte[] buffer = blobReader.ReadDataBlock();
      blobReader.CloseBlob();
      if (buffer.Length == 0)
        return (byte[]) null;
      IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
      switch (blobInformation.ArcMethod)
      {
        case ArcMethods.NotPacked:
          return buffer;
        case ArcMethods.ZLibPacked:
          MemoryStream inStream = new MemoryStream(buffer);
          inStream.Position = 0L;
          MemoryStream outStream = new MemoryStream((int) blobInformation.RealFileSize);
          service.UnpackStream((Stream) outStream, (Stream) inStream);
          outStream.Position = 0L;
          inStream.Close();
          return outStream.GetBuffer();
        default:
          return (byte[]) null;
      }
    }
  }

  public string Id => $"IF{this._folderId}";

  public long InternalId => this._folderId;

  public void GetProperties(out string[] names, out object[] values)
  {
    ImbaseFolder.GetObjectProperties(this._folderId, out names, out values);
  }

  public object GetProperty(object index) => ImbaseFolder.GetObjectProperty(this._folderId, index);

  public void SetProperty(object index, object value)
  {
    ImbaseFolder.SetObjectProperty(this._folderId, index, value);
  }

  public IIPSImbaseTable AddTable(string tableName)
  {
    if (string.IsNullOrEmpty(tableName))
      return (IIPSImbaseTable) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      long tableIdByName = ImbaseRawTable.GetTableIdByName(session, tableName, Intermech.Imbase.Consts.ImbaseTableTypeID);
      if (tableIdByName == 0L)
        return (IIPSImbaseTable) null;
      IDBObject dbObject1 = session.GetObject(tableIdByName);
      IDBObject dbObject2 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID).Create();
      dbObject2.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseTableRefAttID, false).AsInteger = tableIdByName;
      dbObject2.Caption = dbObject1.Caption;
      int relationType = MetaDataHelper.GetDefaultRelationTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeID);
      if (relationType == -1)
        relationType = MetaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545"));
      sessionKeeper.Session.GetRelationCollection(relationType).Create(this._folderId, dbObject2.ObjectID);
      dbObject2.CommitCreation(true);
      return (IIPSImbaseTable) new ImbaseTable(session, dbObject2.ObjectID, (IIPSImbaseFolder) this);
    }
  }

  public int TablesCount
  {
    get
    {
      Logger.Log("Folder.TablesCount");
      DataRow[] tableRows = this.GetTableRows();
      return tableRows == null ? 0 : tableRows.Length;
    }
  }

  public string[] GetTableNames()
  {
    Logger.Log("Folder.TableNames");
    List<string> stringList = new List<string>();
    foreach (DataRow tableRow in this.GetTableRows())
      stringList.Add(tableRow[1].ToString());
    return stringList.ToArray();
  }

  public IIPSImbaseTable GetTable(object index)
  {
    Logger.Log("Folder.GetTable " + index.ToString());
    if (this._tables.ContainsKey(index))
      return (IIPSImbaseTable) this._tables[index];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long tableLinkId = this.GetTableLinkId(index, sessionKeeper.Session);
      if (tableLinkId == 0L)
        return (IIPSImbaseTable) null;
      ImbaseTable table = new ImbaseTable(sessionKeeper.Session, tableLinkId, (IIPSImbaseFolder) this);
      this._tables[index] = table;
      return (IIPSImbaseTable) table;
    }
  }

  private long GetTableLinkId(object index, IUserSession session)
  {
    List<string> stringList = new List<string>();
    DataRow[] tableRows = this.GetTableRows();
    if (tableRows == null)
      return 0;
    DataRow dataRow1 = (DataRow) null;
    switch (index)
    {
      case int index1:
        dataRow1 = tableRows[index1];
        break;
      case string str1:
        foreach (DataRow dataRow2 in tableRows)
        {
          string str = dataRow2["-50"].ToString();
          if (str1.Equals(str, StringComparison.InvariantCultureIgnoreCase))
          {
            dataRow1 = dataRow2;
            break;
          }
        }
        break;
      default:
        return 0;
    }
    return dataRow1 == null ? 0L : Convert.ToInt64(dataRow1[0]);
  }

  public void RemoveTable(object index)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long tableLinkId = this.GetTableLinkId(index, sessionKeeper.Session);
      if (tableLinkId != 0L)
        sessionKeeper.Session.GetObject(tableLinkId).Delete(0L);
    }
    this._tableRows = (DataRow[]) null;
    this._tables.Clear();
  }

  public void RemoveAllTables()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      DataRow[] tableRows = this.GetTableRows();
      if (tableRows == null)
        return;
      foreach (DataRow dataRow in tableRows)
        session.GetObject(Convert.ToInt64(dataRow[0])).Delete(0L);
    }
    this._tableRows = (DataRow[]) null;
    this._tables.Clear();
  }

  public int ImageId
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute attributeById = sessionKeeper.Session.GetObject(this._folderId).GetAttributeByID(Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID);
        return attributeById == null ? 0 : (int) attributeById.AsInteger;
      }
    }
  }

  public int Attributes
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute attributeById = sessionKeeper.Session.GetObject(this._folderId).GetAttributeByID(Intermech.Imbase.Consts.ImbaseFlagsAttId);
        return attributeById != null ? (int) attributeById.AsInteger : 0;
      }
    }
    set
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._folderId);
        IDBAttribute dbAttribute = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseFlagsAttId);
        if (value == 0)
        {
          dbAttribute?.Delete(0L);
        }
        else
        {
          if (dbAttribute == null)
            dbAttribute = dbObject.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseFlagsAttId, false);
          dbAttribute.AsInteger = (long) value;
        }
      }
    }
  }
}
