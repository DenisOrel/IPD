
// Type: Intermech.Holders.PhysValueHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Holders;

public class PhysValueHolder
{
  private static bool initialized = false;
  private static HybridDictionary physValues = new HybridDictionary();

  public static bool Initialized => PhysValueHolder.initialized;

  public static HybridDictionary PhysValues
  {
    get
    {
      if (!PhysValueHolder.initialized)
        PhysValueHolder.Init();
      return PhysValueHolder.physValues;
    }
  }

  public static void Init()
  {
    if (PhysValueHolder.initialized)
      return;
    PhysValueHolder.physValues.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.ObjectsSelect(new Guid("cad00048-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) -2,
        (object) -50
      })).Rows)
        PhysValueHolder.physValues[(object) Convert.ToInt64(row[0])] = (object) Convert.ToString(row[1]);
    }
    PhysValueHolder.initialized = true;
  }
}
