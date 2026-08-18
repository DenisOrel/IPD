// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CopyingSelector
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CopyingSelector : INotifyPropertyChanged
{
  private List<CopyingSelectorEntry> entries;
  private int byRuleEntriesCount;
  private bool isSelected;
  private bool isSelectedByRule;
  private static readonly CopyingSelectorEntry[] emptyEntries = new CopyingSelectorEntry[0];

  public CopyingSelector() => this.InitializeSummaryProperties();

  public bool IsSelected
  {
    [DebuggerStepThrough] get => this.isSelected;
  }

  public bool IsSelectedByRule
  {
    [DebuggerStepThrough] get => this.isSelectedByRule;
  }

  public (bool, CopyingSelectorEntry) CanAdd(bool allowingEntry)
  {
    if (this.entries != null)
    {
      CopyingSelectorEntry conflictBeforeAdd = this.FindConflictBeforeAdd(allowingEntry);
      if (conflictBeforeAdd != null)
        return (false, conflictBeforeAdd);
    }
    return (true, (CopyingSelectorEntry) null);
  }

  public bool TryAdd(CopyingSelectorEntry entry)
  {
    if (entry == null)
      throw new ArgumentNullException(nameof (entry));
    if (this.entries != null)
    {
      if (this.FindConflictBeforeAdd(entry.IsAllowing) != null || this.entries.Contains(entry))
        return false;
    }
    else
      this.entries = new List<CopyingSelectorEntry>();
    this.entries.Add(entry);
    if (this.IsByRule(entry))
      ++this.byRuleEntriesCount;
    this.UpdateSummaryProperties();
    return true;
  }

  public void Remove(CopyingSelectorEntry entry)
  {
    if (entry == null)
      throw new ArgumentNullException(nameof (entry));
    if (this.entries == null || !this.entries.Remove(entry))
      return;
    if (this.IsByRule(entry))
      --this.byRuleEntriesCount;
    this.UpdateSummaryProperties();
  }

  public CopyingSelectorEntry TryGetFirstEntry()
  {
    return this.entries != null && this.entries.Count != 0 ? this.entries[0] : (CopyingSelectorEntry) null;
  }

  public CopyingSelectorEntry TryGetFirstEntryByRule()
  {
    return this.entries != null && this.entries.Count != 0 ? this.entries.Find(new Predicate<CopyingSelectorEntry>(this.IsByRule)) : (CopyingSelectorEntry) null;
  }

  public IList<CopyingSelectorEntry> GetEntries()
  {
    return this.entries != null ? (IList<CopyingSelectorEntry>) new List<CopyingSelectorEntry>((IEnumerable<CopyingSelectorEntry>) this.entries) : (IList<CopyingSelectorEntry>) CopyingSelector.emptyEntries;
  }

  private bool IsByRule(CopyingSelectorEntry entry) => entry.HeuristicsId != string.Empty;

  private CopyingSelectorEntry FindConflictBeforeAdd(bool allowingEntry)
  {
    return this.entries.Find((Predicate<CopyingSelectorEntry>) (x => x.IsAllowing != allowingEntry));
  }

  private void InitializeSummaryProperties()
  {
    this.UpdateIsSelected(false);
    this.UpdateIsSelectedByRule(false);
  }

  private void UpdateSummaryProperties()
  {
    if (this.entries.Count != 0)
    {
      this.UpdateIsSelected(this.entries[0].IsAllowing);
      this.UpdateIsSelectedByRule(this.byRuleEntriesCount != 0);
    }
    else
      this.InitializeSummaryProperties();
  }

  private void UpdateIsSelected(bool value)
  {
    if (this.isSelected == value)
      return;
    this.isSelected = value;
    this.RaiseIsSelectedChanged();
    this.RaisePropertyChanged("IsSelected");
  }

  private void UpdateIsSelectedByRule(bool value)
  {
    if (this.isSelectedByRule == value)
      return;
    this.isSelectedByRule = value;
    this.RaiseIsSelectedByRuleChanged();
    this.RaisePropertyChanged("IsSelectedByRule");
  }

  private void RaiseIsSelectedChanged()
  {
    if (this.IsSelectedChanged == null)
      return;
    this.IsSelectedChanged((object) this, EventArgs.Empty);
  }

  private void RaiseIsSelectedByRuleChanged()
  {
    if (this.IsSelectedByRuleChanged == null)
      return;
    this.IsSelectedByRuleChanged((object) this, EventArgs.Empty);
  }

  private void RaisePropertyChanged(string propertyName)
  {
    if (this.PropertyChanged == null || propertyName == null)
      return;
    this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
  }

  public event EventHandler IsSelectedChanged;

  public event EventHandler IsSelectedByRuleChanged;

  public event PropertyChangedEventHandler PropertyChanged;
}
