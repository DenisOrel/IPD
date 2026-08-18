
// Type: Intermech.Holders.LanguagesHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Holders;

public class LanguagesHolder : DataHolder
{
  public override DataTable LoadData(bool reload, params object[] args)
  {
    if (this.dataTable == null | reload)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.dataTable = sessionKeeper.Session.GetLanguageCollection().Select("F_LANGUAGE_NAME");
        this.lastReload = DateTime.Now;
      }
    }
    return this.dataTable;
  }

  public string GetIDbyName(string name)
  {
    DataRow[] dataRowArray = this.DataTable.Select($"F_LANGUAGE_NAME='{name.Replace("'", "''")}'");
    return dataRowArray.Length != 0 ? (string) dataRowArray[0]["F_LANGUAGE_ID"] : string.Empty;
  }

  public string GetNamebyID(string id)
  {
    if (id.Trim() == string.Empty)
      return LocalizationHolder.rm.GetString("Client.Core_106");
    DataRow[] dataRowArray = this.DataTable.Select($"F_LANGUAGE_ID='{id}'");
    return dataRowArray.Length != 0 ? (string) dataRowArray[0]["F_LANGUAGE_NAME"] : string.Empty;
  }
}
