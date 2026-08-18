
// Type: Intermech.Holders.DataHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Data;


namespace Intermech.Holders;

public class DataHolder
{
  protected DataTable dataTable;
  protected DateTime lastReload = DateTime.MinValue;

  public DateTime LastReload => this.lastReload;

  public virtual DataTable LoadData() => this.LoadData(false);

  public virtual DataTable LoadData(bool reload, params object[] args)
  {
    if (this.dataTable == null | reload)
      this.lastReload = DateTime.Now;
    return (DataTable) null;
  }

  public virtual DataTable DataTable => this.LoadData(false);

  public virtual void ClearInfo(params object[] args)
  {
    this.dataTable = (DataTable) null;
    this.lastReload = DateTime.Now;
  }
}
