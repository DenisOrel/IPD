
// Type: Intermech.PropertyEditors.PrototypeClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;


namespace Intermech.PropertyEditors;

public class PrototypeClass
{
  private bool initialized;
  private long id;
  private Guid guid = Guid.Empty;
  private string caption = string.Empty;
  private int objtypeId;
  private string objtypeName = string.Empty;
  private BlobInformationList files;

  /// <summary>id версии объекта пртотипа</summary>
  public long Id
  {
    get => this.id;
    set => this.id = value;
  }

  /// <summary>guid объекта прототипа</summary>
  public Guid Guid
  {
    get
    {
      this.CheckInit();
      return this.guid;
    }
    set => this.guid = value;
  }

  /// <summary>заголовок объекта прототипа</summary>
  public string Caption
  {
    get
    {
      this.CheckInit();
      return this.caption;
    }
    set => this.caption = value;
  }

  /// <summary>тип объекта объекта-прототипа</summary>
  public int ObjtypeId
  {
    get
    {
      this.CheckInit();
      return this.objtypeId;
    }
    set => this.objtypeId = value;
  }

  /// <summary>наименование типа объекта-прототипа</summary>
  public string ObjtypeName
  {
    get
    {
      this.CheckInit();
      return this.objtypeName;
    }
    set => this.objtypeName = value;
  }

  /// <summary>список файлов, присоединенных в атрибуте</summary>
  public BlobInformationList Files
  {
    get
    {
      this.CheckInit();
      return this.files;
    }
  }

  /// <summary>инициализация производится автоматически</summary>
  /// <param name="id"></param>
  public PrototypeClass(long id)
  {
    this.initialized = false;
    this.id = id;
  }

  /// <summary>
  /// инициализация производится вручную, все данные передаются в конструкторе
  /// </summary>
  /// <param name="id"></param>
  /// <param name="guid"></param>
  /// <param name="caption"></param>
  /// <param name="objtypeId"></param>
  /// <param name="objtypeName"></param>
  /// <param name="files"></param>
  public PrototypeClass(
    long id,
    Guid guid,
    string caption,
    int objtypeId,
    string objtypeName,
    BlobInformationList files)
    : this(id)
  {
    this.initialized = true;
    this.guid = guid;
    this.caption = caption;
    this.objtypeId = objtypeId;
    this.objtypeName = objtypeName;
    this.files = files;
  }

  public override string ToString() => this.Caption;

  public void CheckInit()
  {
    if (this.initialized)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.id, false);
      if (dbObject != null)
      {
        this.caption = dbObject.Caption;
        this.guid = dbObject.ObjectGUID;
        this.objtypeId = dbObject.ObjectType;
        this.objtypeName = sessionKeeper.Session.GetObjectType(this.objtypeId).ObjectTypeName;
        this.files = new BlobInformationList();
        IDBAttribute attributeByGuid = ClientCommons.GetAttributable(this.id, AttributableElements.Object, sessionKeeper.Session).GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid != null)
        {
          for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
          {
            attributeByGuid.Index = index;
            if (attributeByGuid is IBlobReader blobReader)
              this.files.Add(blobReader.OpenBlob(-1));
          }
        }
      }
      this.initialized = true;
    }
  }

  public PrototypeClass Clone()
  {
    this.CheckInit();
    return new PrototypeClass(this.id, this.guid, this.caption, this.objtypeId, this.objtypeName, this.files.Clone());
  }
}
