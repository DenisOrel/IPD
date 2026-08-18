// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.SelectedRecords
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Views;

public static class SelectedRecords
{
  private static Dictionary<long, long[]> _selection = new Dictionary<long, long[]>(32 /*0x20*/);
  private static List<ConditionItem> _conditions;

  private static void OnContextChanged()
  {
    SelectedRecords.ContextChangedEventHandler contextChanged = SelectedRecords.ContextChanged;
    if (contextChanged == null)
      return;
    contextChanged();
  }

  public static void Clear()
  {
    SelectedRecords._selection.Clear();
    SelectedRecords._conditions = (List<ConditionItem>) null;
    SelectedRecords.OnContextChanged();
  }

  public static void Add(long objectId, long[] records)
  {
    if (SelectedRecords._selection.ContainsKey(objectId))
      SelectedRecords._selection.Remove(objectId);
    if (records != null)
      SelectedRecords._selection[objectId] = records;
    SelectedRecords.OnContextChanged();
  }

  public static void Remove(long objectId) => SelectedRecords.Add(objectId, (long[]) null);

  public static long[] Select(long objectId)
  {
    return SelectedRecords._selection.ContainsKey(objectId) ? SelectedRecords._selection[objectId] : (long[]) null;
  }

  public static List<ConditionItem> Conditions
  {
    get => SelectedRecords._conditions;
    set => SelectedRecords._conditions = value;
  }

  public static event SelectedRecords.ContextChangedEventHandler ContextChanged;

  public delegate void ContextChangedEventHandler();
}
