// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE.DraftOleClass
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using System;
using System.IO;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE;

/// <summary>Операционный OLE эскиз</summary>
internal class DraftOleClass : IDisposable
{
  /// <summary>Ид. версии документа (объекта OLE эскиза)</summary>
  private long _objectId;
  /// <summary>Содержимое документа</summary>
  private Stream _dataStream;

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData() => this._dataStream = (Stream) null;

  /// <summary>Конструктор</summary>
  public DraftOleClass()
    : this(0L)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Ид. версии объекта</param>
  public DraftOleClass(long objectId)
  {
    this._objectId = objectId;
    this.InitializeData();
  }

  /// <summary>Загрузка данных из базы</summary>
  public bool LoadData()
  {
    this._dataStream = (Stream) null;
    if (this.ObjectId == 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(this.ObjectId, TechCardConsts.AttributeTypes.OLEObjectAttrGuid);
      if (objectAttributeByGuid == null)
        return false;
      this._dataStream = (Stream) new MemoryStream();
      new BlobProcReader(objectAttributeByGuid, 0, this._dataStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
      if (this._dataStream.Length > 0L)
        this._dataStream.Position = 0L;
      return true;
    }
  }

  /// <summary>Сохранение данных в базу</summary>
  public bool SaveData()
  {
    if (this._objectId == 0L || this._dataStream == null)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectId, false);
      if (dbObject == null)
        return false;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(TechCardConsts.AttributeTypes.OLEObjectAttrGuid);
      if (attributeByGuid == null)
        throw new AttributeNotFoundException("", TechCardConsts.AttributeTypes.OLEObjectAttrGuid.ToString(), this.ObjectId);
      Stream dataStream = this.DataStream;
      dataStream.Position = 0L;
      BlobInformation aBlobInformation = new BlobInformation(dataStream.Length, 0L, DateTime.Now, "draft.ole", ArcMethods.ZLibPacked, string.Empty);
      new BlobProcWriter(attributeByGuid, 0, aBlobInformation, dataStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      return true;
    }
  }

  /// <summary>Ид. версии документа (объекта OLE эскиза)</summary>
  public long ObjectId
  {
    get => this._objectId;
    set
    {
      if (this._objectId == value)
        return;
      this._objectId = value;
      this.LoadData();
    }
  }

  /// <summary>Содержимое документа</summary>
  public Stream DataStream
  {
    get => this._dataStream;
    set => this._dataStream = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public void Dispose() => this._dataStream?.Dispose();
}
