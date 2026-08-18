
// Type: Intermech.Client.Core.Thumbnail.PicturesCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core.Thumbnail;

/// <summary>Summary description for PicturesCache.</summary>
internal class PicturesCache : IPicturesCache
{
  private Hashtable _cache = new Hashtable(520);
  private Queue<long> _idsQueue = new Queue<long>(520);
  private static int _sessionId = 0;
  private static ListDictionary _creators = new ListDictionary();
  private static ListDictionary _descriptions = new ListDictionary();
  internal static string NoPicture = LocalizationHolder.rm.GetString("Client.Core_1015");

  /// <summary>
  /// 
  /// </summary>
  static PicturesCache()
  {
    IThumbImageCreator thumbImageCreator = (IThumbImageCreator) new BitmapCreator();
    PicturesCache._creators[(object) "bmp"] = (object) thumbImageCreator;
    PicturesCache._creators[(object) "gif"] = (object) thumbImageCreator;
    PicturesCache._creators[(object) "tif"] = (object) thumbImageCreator;
    PicturesCache._creators[(object) "tiff"] = (object) thumbImageCreator;
    PicturesCache._creators[(object) "png"] = (object) thumbImageCreator;
    PicturesCache._creators[(object) "jpg"] = (object) thumbImageCreator;
    PicturesCache._creators[(object) "jpeg"] = (object) thumbImageCreator;
    PicturesCache._creators[(object) "exif"] = (object) thumbImageCreator;
    PicturesCache._creators[(object) "ico"] = (object) thumbImageCreator;
    PicturesCache._creators[(object) "emf"] = (object) thumbImageCreator;
    PicturesCache._creators[(object) "wmf"] = (object) thumbImageCreator;
    PicturesCache._descriptions[(object) "exif"] = (object) "Exchangeable Image File";
    PicturesCache._descriptions[(object) "gif"] = (object) "Graphics Interchange Format";
    PicturesCache._descriptions[(object) "png"] = (object) "Portable Network Graphics";
    PicturesCache._descriptions[(object) "tiff"] = (object) "Tag Image File Format";
    PicturesCache._descriptions[(object) "tif"] = (object) "Tag Image File Format";
    PicturesCache._descriptions[(object) "jpg"] = (object) "Joint Photographic Experts Group";
    PicturesCache._descriptions[(object) "jpeg"] = (object) "Joint Photographic Experts Group";
    PicturesCache._descriptions[(object) "wmf"] = (object) "Windows Metafile";
    PicturesCache._descriptions[(object) "emf"] = (object) "Windows Enhanced Metafile";
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="creator"></param>
  /// <param name="ext"></param>
  /// <param name="description"></param>
  private static void RegisterPictureFileInternal(
    IThumbImageCreator creator,
    string ext,
    string description)
  {
    PicturesCache._creators[(object) ext] = (object) creator;
    PicturesCache._descriptions[(object) ext] = (object) description;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId"></param>
  private void OnCacheChanged(long objectId)
  {
    if (this.CacheChanged == null)
      return;
    this.CacheChanged((object) this, objectId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId"></param>
  /// <param name="sessionId"></param>
  /// <param name="picture"></param>
  private void OnLoadComplete(long objectId, int sessionId, object picture)
  {
    if (this.LoadComplete == null)
      return;
    PictureEventArgs e = new PictureEventArgs(objectId, sessionId, picture);
    foreach (LoadCompleteEventHandler invocation in this.LoadComplete.GetInvocationList())
    {
      try
      {
        invocation.BeginInvoke((object) this, e, (AsyncCallback) null, (object) null);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  private void OnTranslateObjectId(TranslateObjectEventArgs e)
  {
    if (this.TranslateObject == null)
      return;
    foreach (TranslateObjectIdEventHandler invocation in this.TranslateObject.GetInvocationList())
    {
      try
      {
        invocation((object) this, e);
        if (e.NewObjectId != -1L)
          break;
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  /// <returns></returns>
  internal bool IsImageLibraryItem(int objectType) => objectType == Consts.ImageLibraryItemTypeID;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  internal object LookInCache(long objectId)
  {
    PictureCacheItem pictureCacheItem = (PictureCacheItem) null;
    lock (this._cache)
      pictureCacheItem = (PictureCacheItem) this._cache[(object) objectId];
    if (pictureCacheItem == null)
      return (object) null;
    ++pictureCacheItem._used;
    return pictureCacheItem._picture;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId"></param>
  internal void RemoveFromCache(long objectId)
  {
    lock (this._cache)
    {
      if (!this._cache.ContainsKey((object) objectId))
        return;
      if (this._cache[(object) objectId] is PictureCacheItem pictureCacheItem && this._cache.ContainsKey((object) pictureCacheItem._objectGuid))
        this._cache.Remove((object) pictureCacheItem._objectGuid);
      this._cache.Remove((object) objectId);
      if (pictureCacheItem._picture is IDisposable picture)
        picture.Dispose();
      pictureCacheItem._picture = (object) null;
      this.OnCacheChanged(objectId);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectGuid"></param>
  /// <returns></returns>
  internal object LookInCache(Guid objectGuid)
  {
    PictureCacheItem pictureCacheItem = (PictureCacheItem) null;
    lock (this._cache)
      pictureCacheItem = (PictureCacheItem) this._cache[(object) objectGuid];
    if (pictureCacheItem == null)
      return (object) null;
    ++pictureCacheItem._used;
    return pictureCacheItem._picture;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="creator"></param>
  /// <param name="ext"></param>
  /// <param name="description"></param>
  public void RegisterPictureFile(IThumbImageCreator creator, string ext, string description)
  {
    ext.ToLower();
    PicturesCache.RegisterPictureFileInternal(creator, ext.ToLower(), description);
  }

  /// <summary>
  /// 
  /// </summary>
  public int Session => PicturesCache._sessionId++;

  /// <summary>
  /// 
  /// </summary>
  public string Filter
  {
    get
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      foreach (string key in (IEnumerable) PicturesCache._creators.Keys)
      {
        if (key != null)
        {
          str1 = str1.Length > 0 ? $"{str1};*.{key}" : "*." + key;
          string description = (string) PicturesCache._descriptions[(object) key];
          if (description == null || description.Length == 0)
            description = LocalizationHolder.rm.GetString("Client.Core_297");
          string str3 = string.Format("{0} (*.{1})|*.{1}", (object) description, (object) key);
          str2 = str2.Length > 0 ? $"{str2}|{str3}" : str3;
        }
      }
      string str4 = string.Format(LocalizationHolder.rm.GetString("Client.Core_1016"), (object) str1);
      return str2.Length <= 0 ? str4 : $"{str4}|{str2}";
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public event LoadCompleteEventHandler LoadComplete;

  /// <summary>
  /// 
  /// </summary>
  public event CacheChangedEventHandler CacheChanged;

  /// <summary>
  /// 
  /// </summary>
  public event TranslateObjectIdEventHandler TranslateObject;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  /// <param name="objectId"></param>
  /// <param name="sessionId"></param>
  /// <param name="newObjectId"></param>
  /// <returns></returns>
  public object LoadPicture(int objectType, long objectId, int sessionId, out long newObjectId)
  {
    newObjectId = this.TranslateObjectId(objectType, objectId);
    return newObjectId == -1L ? (object) DBNull.Value : this.LookInCache(newObjectId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  /// <param name="objectId"></param>
  /// <param name="newObjectId"></param>
  /// <returns></returns>
  public object GetPicture(int objectType, long objectId, out long newObjectId)
  {
    newObjectId = this.TranslateObjectId(objectType, objectId);
    return newObjectId <= 0L ? (object) DBNull.Value : this.LookInCache(newObjectId) ?? this.LoadPicture(newObjectId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  /// <param name="objectId"></param>
  /// <returns></returns>
  public bool UpdateItem(int objectType, long objectId)
  {
    bool flag = false;
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
      openFileDialog.Filter = this.Filter;
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        string fileName = openFileDialog.FileName;
        flag = this.UpdateItem(objectType, objectId, fileName);
      }
    }
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  /// <param name="objectId"></param>
  /// <param name="fileName"></param>
  /// <returns></returns>
  public bool UpdateItem(int objectType, long objectId, string fileName)
  {
    return !string.IsNullOrEmpty(fileName) && this.GetCreator(fileName) != null && this.UpdateItemInternal(objectType, objectId, fileName);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ms"></param>
  /// <param name="pictureName"></param>
  /// <returns></returns>
  private object CreatePicture(Stream ms, string pictureName)
  {
    string lower = this.GetExtension(pictureName).ToLower();
    IThumbImageCreator creator = this.GetCreator(pictureName);
    return creator != null ? creator.CreateFromStream(ms, lower) : (object) string.Format(LocalizationHolder.rm.GetString("Client.Core_1018"), (object) lower);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pictureName"></param>
  /// <returns></returns>
  private IThumbImageCreator GetCreator(string pictureName)
  {
    string key = this.GetExtension(pictureName);
    return PicturesCache._creators[(object) key] as IThumbImageCreator;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="fileName"></param>
  /// <returns></returns>
  private string GetExtension(string fileName)
  {
    string str = Path.GetExtension(fileName).ToLower();
    if (str.StartsWith("."))
      str = str.Substring(1);
    return str;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="objectType"></param>
  /// <param name="objectId"></param>
  /// <param name="newObjectId"></param>
  /// <returns></returns>
  private IDBAttribute GetOrCreatePictureAttribute(
    IUserSession session,
    int objectType,
    long objectId,
    out long newObjectId)
  {
    newObjectId = 0L;
    IDBAttribute pictureAttribute = (IDBAttribute) null;
    if (objectType == Consts.ImageLibraryItemTypeID)
    {
      newObjectId = objectId;
    }
    else
    {
      IDBAttribute dbAttribute = (IDBAttribute) null;
      IDBObject dbObject1 = session.GetObject(objectId);
      if (dbObject1 != null)
        dbAttribute = dbObject1.GetAttributeByID(Consts.ImageAttTypeID);
      if (dbAttribute != null)
      {
        IDBObjectCollection objectCollection = session.GetObjectCollection(Consts.ImageLibraryItemTypeID);
        if (objectCollection != null)
        {
          IDBObject dbObject2 = objectCollection.Create();
          dbObject2.CommitCreation(true);
          newObjectId = dbObject2.ObjectID;
          dbAttribute.AsInteger = newObjectId;
        }
      }
    }
    IDBObject dbObject = session.GetObject(newObjectId);
    if (dbObject != null)
      pictureAttribute = dbObject.GetAttributeByID(Consts.LibImageAttTypeID);
    return pictureAttribute;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  private object LoadPicture(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.LoadPicture(sessionKeeper.Session.GetObject(objectId, false));
  }

  private object LoadPicture(IDBObject dbObject)
  {
    if (dbObject == null)
      return (object) LocalizationHolder.rm.GetString("Client.Core_1019");
    IDBAttribute attributeById = dbObject.GetAttributeByID(Consts.LibImageAttTypeID);
    if (attributeById == null)
      return (object) PicturesCache.NoPicture;
    this.EnsureCacheSpace();
    object picture = this.LoadPictureData(attributeById);
    if (picture != null)
    {
      PictureCacheItem pictureCacheItem = new PictureCacheItem(dbObject.ObjectGUID, dbObject.ObjectID, picture, attributeById.AsString, attributeById.AsDateTime);
      lock (this._cache)
      {
        if (!this._cache.ContainsKey((object) dbObject.ObjectID))
        {
          long objectId = dbObject.ObjectID;
          this._cache.Add((object) dbObject.ObjectID, (object) pictureCacheItem);
          if (!this._idsQueue.Contains(objectId))
            this._idsQueue.Enqueue(objectId);
        }
        if (!this._cache.ContainsKey((object) pictureCacheItem._objectGuid))
          this._cache.Add((object) pictureCacheItem._objectGuid, (object) pictureCacheItem);
      }
    }
    return picture;
  }

  /// <summary>
  /// 
  /// </summary>
  private void EnsureCacheSpace()
  {
    int count = this._cache.Count;
    if (count <= 512 /*0x0200*/)
      return;
    int num = 1;
    do
    {
      for (int index = 0; index < count; ++index)
      {
        if (this._idsQueue.Count > 0)
        {
          long key = this._idsQueue.Dequeue();
          if (this._cache.ContainsKey((object) key))
          {
            PictureCacheItem pictureCacheItem = this._cache[(object) key] as PictureCacheItem;
            if (pictureCacheItem._used == num)
            {
              this.RemoveFromCache(pictureCacheItem._objectId);
              return;
            }
            this._idsQueue.Enqueue(key);
          }
        }
      }
      ++num;
    }
    while (this._idsQueue.Count > 0);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectGuid"></param>
  /// <returns></returns>
  private object LoadPicture(Guid objectGuid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.LoadPicture(sessionKeeper.Session.GetObject(objectGuid));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="att"></param>
  /// <returns></returns>
  private object LoadPictureData(IDBAttribute att)
  {
    try
    {
      byte[] buffer;
      BlobInformation blobInformation;
      switch (att)
      {
        case IDBShortBlobAttribute _:
          ShortBlobValue blobValue = (att as IDBShortBlobAttribute).GetBlobValue();
          buffer = blobValue.Value;
          if (blobValue.Empty || blobValue.RealFileSize == 0L)
            return (object) PicturesCache.NoPicture;
          blobInformation = new BlobInformation((ShortBlobInfo) blobValue);
          break;
        case IBlobReader blobReader:
          blobInformation = blobReader.OpenBlob(0);
          if (blobInformation.RealFileSize == 0L)
            return (object) PicturesCache.NoPicture;
          buffer = blobReader.ReadDataBlock();
          blobReader.CloseBlob();
          break;
        default:
          return (object) DBNull.Value;
      }
      if (buffer.Length == 0)
        return (object) DBNull.Value;
      MemoryStream memoryStream1 = new MemoryStream(buffer);
      memoryStream1.Position = 0L;
      IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
      switch (blobInformation.ArcMethod)
      {
        case ArcMethods.NotPacked:
          return this.CreatePicture((Stream) memoryStream1, att.AsString);
        case ArcMethods.ZLibPacked:
          MemoryStream memoryStream2 = new MemoryStream((int) blobInformation.RealFileSize);
          service.UnpackStream((Stream) memoryStream2, (Stream) memoryStream1);
          memoryStream2.Position = 0L;
          memoryStream1.Close();
          return this.CreatePicture((Stream) memoryStream2, att.AsString);
        default:
          memoryStream1.Close();
          return (object) LocalizationHolder.rm.GetString("Client.Core_379");
      }
    }
    catch (Exception ex)
    {
      return (object) ex.Message;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  /// <param name="objectId"></param>
  /// <returns></returns>
  private long TranslateObjectId(int objectType, long objectId)
  {
    if (objectId <= 0L)
      return -1;
    if (objectType == Consts.ImageLibraryItemTypeID)
      return objectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
      if (dbObject == null)
        return -1;
      IDBAttribute attributeById = dbObject.GetAttributeByID(Consts.ImageAttTypeID);
      if (attributeById != null)
        return attributeById.AsInteger;
      TranslateObjectEventArgs e = new TranslateObjectEventArgs(sessionKeeper.Session, objectId, objectType);
      this.OnTranslateObjectId(e);
      if (e.NewObjectId != -1L)
        return this.TranslateObjectId(objectType, e.NewObjectId);
    }
    return -1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  /// <param name="objectId"></param>
  /// <param name="fileName"></param>
  /// <returns></returns>
  private bool UpdateItemInternal(int objectType, long objectId, string fileName)
  {
    long newObjectId = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute pictureAttribute = this.GetOrCreatePictureAttribute(sessionKeeper.Session, objectType, objectId, out newObjectId);
      if (pictureAttribute == null)
        return false;
      IBlobWriter blobWriter = pictureAttribute as IBlobWriter;
      using (FileStream inStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      {
        using (MemoryStream outStream = new MemoryStream())
        {
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) inStream, 9);
          outStream.Position = 0L;
          if (outStream.Length > (long) Intermech.Consts.MaxShortBlobSize)
          {
            int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_1017"), (object) fileName, (object) outStream.Length, (object) Intermech.Consts.MaxShortBlobSize), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
          }
          BlobInformation blobInfo = new BlobInformation(inStream.Length, outStream.Length, DateTime.Now, fileName, ArcMethods.ZLibPacked, fileName);
          blobWriter.OpenBlob(blobInfo, false);
          byte[] numArray = new byte[outStream.Length];
          if (outStream.Read(numArray, 0, numArray.Length) > 0)
            blobWriter.WriteDataBlock(numArray);
        }
      }
    }
    this.RemoveFromCache(newObjectId);
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  public object GetPicture(long objectId)
  {
    return this.LookInCache(objectId) ?? this.LoadPicture(objectId);
  }

  /// <summary>
  /// Загружает для объекта изображение из атрибута изображение.
  /// </summary>
  /// <param name="objectGuid">Guid версии объекта</param>
  /// <returns></returns>
  public object GetPicture(Guid objectGuid)
  {
    return this.LookInCache(objectGuid) ?? this.LoadPicture(objectGuid);
  }
}
