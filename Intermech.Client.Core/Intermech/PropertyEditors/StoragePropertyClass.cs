
// Type: Intermech.PropertyEditors.StoragePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;


namespace Intermech.PropertyEditors;

public class StoragePropertyClass
{
  private string caption;
  private long storage;

  public long Storage
  {
    get => this.storage;
    set
    {
      this.storage = value;
      this.caption = (string) null;
    }
  }

  public StoragePropertyClass()
    : this(0L)
  {
  }

  public StoragePropertyClass(long aStorage)
    : this(aStorage, (string) null)
  {
  }

  public StoragePropertyClass(long aStorage, string aCaption)
  {
    this.storage = aStorage;
    this.caption = aCaption;
  }

  public override string ToString()
  {
    if (this.caption == null)
    {
      if (this.storage == 0L)
      {
        this.caption = string.Empty;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.storage);
          this.caption = dbObject == null ? LocalizationHolder.rm.GetString(string.Format(LocalizationHolder.rm.GetString("StorageNotFound"), (object) this.storage)) : dbObject.Caption;
        }
      }
    }
    return this.caption;
  }
}
