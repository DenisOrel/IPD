
// Type: Intermech.Search.EventLogFilters.EventLogFiltersClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Intermech.Search.EventLogFilters;

public sealed class EventLogFiltersClientService : IEventLogFiltersClientService
{
  private static readonly Regex NewFilterRegex = new Regex("Новый фильтр - (?<number>[0-9]+)", RegexOptions.Compiled);
  private List<EventLogFilter> _filters;

  public EventLogFilter CreateNewFilter()
  {
    this.LoadFilters();
    EventLogFilter eventLogFilter = new EventLogFilter(Guid.NewGuid());
    eventLogFilter.Name = this.CreateNewFilterName();
    using (EventLogFilterEditorForm filterEditorForm = new EventLogFilterEditorForm())
    {
      filterEditorForm.Filter = eventLogFilter;
      if (filterEditorForm.ShowDialog() == DialogResult.OK)
      {
        this._filters.Add(eventLogFilter.Clone());
        this.SaveFilters();
        return eventLogFilter.Clone();
      }
    }
    return (EventLogFilter) null;
  }

  public void EditFilter(Guid filterGuid)
  {
    this.LoadFilters();
    EventLogFilter eventLogFilter = this._filters.FirstOrDefault<EventLogFilter>((Func<EventLogFilter, bool>) (o => o.Guid == filterGuid));
    if (eventLogFilter == null)
      return;
    using (EventLogFilterEditorForm filterEditorForm = new EventLogFilterEditorForm())
    {
      filterEditorForm.Filter = eventLogFilter.Clone();
      if (filterEditorForm.ShowDialog() != DialogResult.OK)
        return;
      this._filters.Remove(eventLogFilter);
      this._filters.Add(filterEditorForm.Filter.Clone());
      this.SaveFilters();
    }
  }

  public bool RemoveFilter(Guid filterGuid)
  {
    this.LoadFilters();
    EventLogFilter eventLogFilter = this._filters.FirstOrDefault<EventLogFilter>((Func<EventLogFilter, bool>) (o => o.Guid == filterGuid));
    if (eventLogFilter == null || MessageBox.Show($"Фильтр '{eventLogFilter.Name}' будет удален. Желаете продолжить?", "Удаление фильтра", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
      return false;
    this._filters.Remove(eventLogFilter);
    this.SaveFilters();
    return true;
  }

  public EventLogFilter[] GetAllFilters()
  {
    this.LoadFilters();
    return this._filters.ToArray();
  }

  private void LoadFilters()
  {
    if (this._filters != null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._filters = ((IEnumerable<EventLogFilter>) ((IEventLogFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (IEventLogFiltersServerService))).GetAllFilters(sessionKeeper.Session.SessionGUID)).ToList<EventLogFilter>();
  }

  private string CreateNewFilterName()
  {
    int[] array = this._filters.Select<EventLogFilter, Match>((Func<EventLogFilter, Match>) (o => EventLogFiltersClientService.NewFilterRegex.Match(o.Name))).Where<Match>((Func<Match, bool>) (o => o.Groups != null && o.Groups["number"] != null && !string.IsNullOrEmpty(o.Groups["number"].Value))).Select<Match, int>((Func<Match, int>) (o => Convert.ToInt32(o.Groups["number"].Value))).ToArray<int>();
    return $"Новый фильтр - {(array.Length != 0 ? ((IEnumerable<int>) array).Max() : 0) + 1}";
  }

  private void SaveFilters()
  {
    if (this._filters == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ((IEventLogFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (IEventLogFiltersServerService))).SaveFilters(sessionKeeper.Session.SessionGUID, this._filters.ToArray());
  }
}
