// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripts.LCStepScriptManager
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripts;

internal class LCStepScriptManager
{
  private static Dictionary<Guid, long> lcSteps;

  public static long GetScript(Guid lcstep)
  {
    if (LCStepScriptManager.lcSteps == null)
      LCStepScriptManager.UpdateDict();
    return LCStepScriptManager.lcSteps.ContainsKey(lcstep) ? LCStepScriptManager.lcSteps[lcstep] : -1L;
  }

  public static void UpdateDict()
  {
    LCStepScriptManager.lcSteps = new Dictionary<Guid, long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(LCStepScriptValue.LCScriptTypeId).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) LCStepScriptValue.Attr_LCScriptObject, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
      }));
      if (dataTable == null)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        string str1 = Convert.ToString(row[1]);
        char[] chArray = new char[1]{ ';' };
        foreach (string str2 in str1.Split(chArray))
        {
          if (GuidHelper.IsGuid(str2))
            LCStepScriptManager.lcSteps[new Guid(str2)] = int64;
        }
      }
    }
  }
}
