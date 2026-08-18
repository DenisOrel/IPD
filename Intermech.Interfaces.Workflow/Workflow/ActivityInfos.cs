// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ActivityInfos
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

#nullable disable
namespace Intermech.Workflow;

public class ActivityInfos
{
  private static ActivityInfoList _items = new ActivityInfoList();
  private static Dictionary<int, ActivityInfo> _activityTypes = new Dictionary<int, ActivityInfo>();

  public static ActivityInfoList Items => ActivityInfos._items;

  public static Dictionary<int, ActivityInfo> Types => ActivityInfos._activityTypes;

  public static void Init(IUserSession sess)
  {
    foreach (DataRow row in (InternalDataCollectionBase) sess.GetObjectTypeCollection(wfConsts.ActivitiesTypeID).SelectRecursive("").Rows)
    {
      if (Convert.ToInt32(row["F_VERSIONABLE"]) > 0)
      {
        ActivityInfo activityInfo = new ActivityInfo();
        activityInfo.TypeGuid = new Guid(row["F_GUID"].ToString());
        activityInfo.ObjectName = row["F_OBJ_NAME"].ToString();
        activityInfo.TypeName = row["F_OBJ_TYPE_NAME"].ToString();
        activityInfo.Type = Convert.ToInt32(row["F_OBJECT_TYPE"]);
        ActivityInfos._items.Add(activityInfo);
        ActivityInfos._activityTypes.Add(activityInfo.Type, activityInfo);
      }
    }
    ActivityInfos.Sort();
  }

  public static void Sort()
  {
    ActivityInfos._items.Sort((IComparer<ActivityInfo>) new ActivityInfos.ActivityInfosComparer());
  }

  public static ActivityInfo FindByGuid(Guid typeguid)
  {
    IEnumerator enumerator = (IEnumerator) ActivityInfos._items.GetEnumerator();
    while (enumerator.MoveNext())
    {
      if (typeguid == (enumerator.Current as ActivityInfo).TypeGuid)
        return enumerator.Current as ActivityInfo;
    }
    return (ActivityInfo) null;
  }

  public static ActivityInfo FindByID(int TypeID)
  {
    ActivityInfo activityInfo = (ActivityInfo) null;
    return ActivityInfos._activityTypes.TryGetValue(TypeID, out activityInfo) ? activityInfo : (ActivityInfo) null;
  }

  public static ActivityInfo FindByKind(ActivityKind kind)
  {
    for (int index = 0; index < ActivityInfos._items.Count; ++index)
    {
      if (ActivityInfos._items[index].Kind == kind)
        return ActivityInfos._items[index];
    }
    return (ActivityInfo) null;
  }

  public static ActivityKind ActivityTypeToKind(int typeID)
  {
    ActivityInfo byId = ActivityInfos.FindByID(typeID);
    return byId != null ? byId.Kind : ActivityKind.None;
  }

  public class ActivityInfosComparer : IComparer<ActivityInfo>
  {
    public int Compare(ActivityInfo x, ActivityInfo y)
    {
      ActivityKind kind1 = x.Kind;
      ActivityKind kind2 = y.Kind;
      int num1 = 0;
      int num2 = 0;
      FieldInfo field1 = kind1.GetType().GetField(kind1.ToString());
      FieldInfo field2 = kind2.GetType().GetField(kind2.ToString());
      Type attributeType = typeof (Order);
      Order[] customAttributes1 = (Order[]) field1.GetCustomAttributes(attributeType, false);
      if (customAttributes1.Length != 0)
        num1 = customAttributes1[0].Pos;
      Order[] customAttributes2 = (Order[]) field2.GetCustomAttributes(typeof (Order), false);
      if (customAttributes2.Length != 0)
        num2 = customAttributes2[0].Pos;
      return num1 - num2;
    }
  }
}
