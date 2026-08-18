
// Type: Intermech.Holders.AttributesHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Data;


namespace Intermech.Holders;

public class AttributesHolder : DataHolder
{
  protected ArrayList DataTables = new ArrayList();
  protected ArrayList GroupIDs = new ArrayList();

  protected int IdPresent(int groupid)
  {
    for (int index = 0; index < this.GroupIDs.Count; ++index)
    {
      if ((int) this.GroupIDs[index] == groupid)
        return index;
    }
    return -1;
  }

  public override DataTable DataTable => this.LoadData(false, (object) -1);

  public override DataTable LoadData(bool reload, params object[] args)
  {
    if (args.Length == 0)
      return (DataTable) null;
    int num = (int) args[0];
    int index = this.IdPresent(num);
    if (index == -1)
    {
      index = this.GroupIDs.Add((object) num);
      this.DataTables.Add((object) null);
    }
    if (this.DataTables[index] == null | reload)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBCollection attributeTypeCollection = (IDBCollection) sessionKeeper.Session.GetAttributeTypeCollection(num, CoreConsts.FilterRecords);
        this.DataTables[index] = (object) attributeTypeCollection.Select("", (object) AttibuteTypesSelectParams.AddSizeTypeDescription);
        if (reload)
          this.lastReload = DateTime.Now;
      }
    }
    return (DataTable) this.DataTables[index];
  }

  public override void ClearInfo(params object[] args)
  {
    if (args.Length == 0)
    {
      this.DataTables.Clear();
      this.GroupIDs.Clear();
    }
    else
    {
      int index = this.IdPresent((int) args[0]);
      if (index != -1)
      {
        this.DataTables.RemoveAt(index);
        this.GroupIDs.RemoveAt(index);
      }
    }
    this.lastReload = DateTime.Now;
  }

  public int GetIDByName(string name)
  {
    DataRow[] dataRowArray = this.LoadData(false, (object) -1).Select($"F_NAME='{name.Replace("'", "''")}'");
    if (dataRowArray.Length != 0)
      return Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]);
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBAttributeTypeInfo attributeTypeInfo = (IDBAttributeTypeInfo) null;
    try
    {
      attributeTypeInfo = service.GetAttributeType(name, false);
    }
    catch
    {
    }
    if (attributeTypeInfo != null)
      return attributeTypeInfo.AttributeID;
    return 0;
  }

  public ArrayList GetGroupByID(int id)
  {
    ArrayList arrayList = new ArrayList();
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(id, false);
    if (attributeType != null)
      arrayList.AddRange((ICollection) attributeType.GetGroupsList());
    return arrayList.Count <= 0 ? (ArrayList) null : arrayList;
  }

  /// <summary>
  /// вернуть параметры атрибута. если null, то возможно не в кэше =&gt; требуется требуется доп. проверка по базе напрямую
  /// </summary>
  public DataRow GetAttribute(int id)
  {
    DataRow attribute = (DataRow) null;
    int index1 = this.IdPresent(-1);
    if (index1 != -1)
    {
      DataRow[] dataRowArray = ((DataTable) this.DataTables[index1]).Select("F_ATTRIBUTE_ID=" + id.ToString());
      if (dataRowArray.Length != 0)
        attribute = dataRowArray[0];
    }
    else
    {
      for (int index2 = 0; index2 < this.GroupIDs.Count; ++index2)
      {
        DataRow[] dataRowArray = ((DataTable) this.DataTables[index2]).Select("F_ATTRIBUTE_ID=" + id.ToString());
        if (dataRowArray.Length != 0)
        {
          attribute = dataRowArray[0];
          break;
        }
      }
    }
    return attribute;
  }
}
