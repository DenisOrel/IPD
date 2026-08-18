
// Type: Intermech.Holders.ObjectTypesHolder
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

public class ObjectTypesHolder : DataHolder
{
  protected ArrayList dataTables = new ArrayList();
  protected ArrayList objTypesIDs = new ArrayList();
  private DataTable hierarchy;
  private DataTable allObjTypes;
  private DataTable hierarchyFull;
  private DataTable allObjTypesFull;

  public ArrayList DataTables => this.dataTables;

  public int IdPresent(int typeid)
  {
    for (int index = 0; index < this.objTypesIDs.Count; ++index)
    {
      if ((int) this.objTypesIDs[index] == typeid)
        return index;
    }
    return -1;
  }

  public override DataTable LoadData(bool reload, params object[] args)
  {
    if (args.Length == 0)
      return (DataTable) null;
    int num = (int) args[0];
    int index = this.IdPresent(num);
    if (index == -1)
    {
      index = this.objTypesIDs.Add((object) num);
      this.dataTables.Add((object) null);
    }
    if (this.dataTables[index] == null | reload)
    {
      IDBObjectTypeInfoCollection objectTypeCollection = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectTypeCollection(num, CoreConsts.FilterRecords);
      this.dataTables[index] = (object) objectTypeCollection.Select("");
      if (reload)
        this.lastReload = DateTime.Now;
    }
    return (DataTable) this.dataTables[index];
  }

  public DataTable GetHierarchy(bool reload, bool filterRecs)
  {
    if (!reload)
    {
      if (filterRecs)
      {
        if (this.hierarchy != null)
          return this.hierarchy;
      }
      else if (this.hierarchyFull != null)
        return this.hierarchyFull;
    }
    DataTable hierarchy = (DataTable) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectTypeCollection objectTypeCollection = sessionKeeper.Session.GetObjectTypeCollection(-2, filterRecs);
      if (objectTypeCollection != null)
        hierarchy = objectTypeCollection.GetTypesHierarchy();
    }
    if (filterRecs)
      this.hierarchy = hierarchy;
    else
      this.hierarchyFull = hierarchy;
    return hierarchy;
  }

  public static ArrayList GetAllParents(int objType, DataTable lHierarchy)
  {
    ArrayList allParents = new ArrayList();
    int num = objType;
    while (num != -1)
    {
      DataRow[] dataRowArray = lHierarchy.Select("F_OBJECT_TYPE=" + num.ToString());
      if (dataRowArray != null && dataRowArray.Length != 0)
      {
        num = Convert.ToInt32(dataRowArray[0]["F_PARENT_ID"]);
        if (num != -1)
          allParents.Add((object) num);
      }
      else
        break;
    }
    return allParents;
  }

  public void ClearHierarchy()
  {
    this.hierarchy = (DataTable) null;
    this.hierarchyFull = (DataTable) null;
  }

  public DataTable GetAllObjectTypes(bool reload, bool filterRecs)
  {
    if (!reload)
    {
      if (filterRecs)
      {
        if (this.allObjTypes != null)
          return this.allObjTypes;
      }
      else if (this.allObjTypesFull != null)
        return this.allObjTypesFull;
    }
    DataTable allObjectTypes = (DataTable) null;
    IDBObjectTypeInfoCollection objectTypeCollection = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectTypeCollection(-2, filterRecs);
    if (objectTypeCollection != null)
      allObjectTypes = objectTypeCollection.Select("");
    if (filterRecs)
      this.allObjTypes = allObjectTypes;
    else
      this.allObjTypesFull = allObjectTypes;
    return allObjectTypes;
  }

  public void ClearAllObjectTypes()
  {
    this.allObjTypes = (DataTable) null;
    this.allObjTypesFull = (DataTable) null;
  }

  public override void ClearInfo(params object[] args)
  {
    if (args.Length == 0)
    {
      this.dataTables.Clear();
      this.objTypesIDs.Clear();
      this.ClearHierarchy();
      this.ClearAllObjectTypes();
    }
    else
    {
      int index = this.IdPresent((int) args[0]);
      if (index != -1)
      {
        this.dataTables.RemoveAt(index);
        this.objTypesIDs.RemoveAt(index);
      }
    }
    this.lastReload = DateTime.Now;
  }
}
