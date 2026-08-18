// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.CheckedRecords
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Views;

public static class CheckedRecords
{
  private static Dictionary<long, long[]> _checked = new Dictionary<long, long[]>(32 /*0x20*/);
  private static bool _active;

  private static void OnContextChanged()
  {
    CheckedRecords.ContextChangedEventHandler contextChanged = CheckedRecords.ContextChanged;
    if (contextChanged == null)
      return;
    contextChanged();
  }

  private static void OnActiveChanged()
  {
    CheckedRecords.ContextChangedEventHandler activeChanged = CheckedRecords.ActiveChanged;
    if (activeChanged == null)
      return;
    activeChanged();
  }

  public static void Clear()
  {
    CheckedRecords._checked.Clear();
    CheckedRecords.OnContextChanged();
  }

  public static void Add(long objectId, long[] records)
  {
    if (CheckedRecords._checked.ContainsKey(objectId))
      CheckedRecords._checked.Remove(objectId);
    if (records != null)
      CheckedRecords._checked[objectId] = records;
    CheckedRecords.OnContextChanged();
  }

  public static void Remove(long objectId) => CheckedRecords.Add(objectId, (long[]) null);

  public static long[] Select(long objectId)
  {
    return CheckedRecords._checked.ContainsKey(objectId) ? CheckedRecords._checked[objectId] : (long[]) null;
  }

  public static bool Active
  {
    get => CheckedRecords._active;
    set
    {
      if (CheckedRecords._active == value)
        return;
      CheckedRecords._active = value;
      CheckedRecords.OnActiveChanged();
    }
  }

  public static event CheckedRecords.ContextChangedEventHandler ContextChanged;

  public static event CheckedRecords.ContextChangedEventHandler ActiveChanged;

  public delegate void ContextChangedEventHandler();
}
