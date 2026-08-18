
// Type: Intermech.Holders.SubjectAreasHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Holders;

public class SubjectAreasHolder : DataHolder
{
  public override DataTable LoadData(bool reload, params object[] args)
  {
    if (this.dataTable == null | reload)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.dataTable = sessionKeeper.Session.GetSubjectAreaCollection().Select("F_AREA_NAME");
        this.lastReload = DateTime.Now;
      }
    }
    return this.dataTable;
  }

  public string GetNamebyID(char id)
  {
    DataRow[] dataRowArray = this.DataTable.Select($"F_AREA_ID='{id.ToString()}'");
    return dataRowArray.Length != 0 ? (string) dataRowArray[0]["F_AREA_NAME"] : string.Empty;
  }

  public string GetNamesbyIDs(string ids)
  {
    if (ids == string.Empty)
      return LocalizationHolder.rm.GetString("Client.Core_116");
    string empty = string.Empty;
    for (int index = 0; index < ids.Length; ++index)
    {
      empty += this.GetNamebyID(ids[index]);
      if (index != ids.Length - 1)
        empty += ";";
    }
    return empty;
  }
}
