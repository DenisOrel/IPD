// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ProjectDisplayOptions
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Project.Controls;

public class ProjectDisplayOptions
{
  [NotNull]
  public readonly Intermech.Project.Project Project;
  [NotNull]
  public readonly Dictionary<ScaleType, string> TopLevelFormat = new Dictionary<ScaleType, string>();
  [NotNull]
  public readonly Dictionary<ScaleType, string> BottomLevelFormat = new Dictionary<ScaleType, string>();
  [CanBeNull]
  private ProjectView _view;
  [NotNull]
  public TaskCaptions TaskCaptions;
  private ScaleType _scaleType = ScaleType.Weeks;
  [NotNull]
  private string _dateFormat = string.Empty;
  private DateTime _currentDate = DateTime.MinValue;
  private bool _highlightCriticalTasks;
  private bool _showGrid;
  private FactDurationsDisplayMode _showFactDurations;
  [NotNull]
  [ItemNotNull]
  public readonly TaskFilters Filters = new TaskFilters();
  private const string DsSection = "DisplaySettings";

  [CanBeNull]
  public ProjectView View
  {
    get => this._view;
    set
    {
      if (this._view == value)
        return;
      this._view = value;
      this.GanttChart = this._view?.GanttChart;
      this.UpdateControls();
    }
  }

  [CanBeNull]
  public GanttChart GanttChart { get; set; }

  public ProjectDisplayOptions([NotNull] Intermech.Project.Project project)
  {
    foreach (KeyValuePair<ScaleType, List<string>> ganttFormat in DefaultDateFormats.GanttFormats)
    {
      if (!this.TopLevelFormat.ContainsKey(ganttFormat.Key) && ganttFormat.Value.Count > 0)
        this.TopLevelFormat.Add(ganttFormat.Key, ganttFormat.Value[0]);
      if (!this.BottomLevelFormat.ContainsKey(ganttFormat.Key) && ganttFormat.Value.Count > 0)
        this.BottomLevelFormat.Add(ganttFormat.Key, ganttFormat.Value[0]);
    }
    this.DateFormat = DefaultDateFormats.DateFormats[DefaultDateFormats.DefaultDateFormatIndex];
    this.Project = project;
    this.TaskCaptions = new TaskCaptions(this);
  }

  public void UpdateControls()
  {
    if (this.GanttChart != null)
    {
      this.GanttChart.ScaleType = this.ScaleType;
      this.GanttChart.Refresh();
      if (this.TaskCaptions.VerticalPadding == 0)
        this.TaskCaptions.VerticalPadding = this.GanttChart.Font.Height;
    }
    if (this.View == null)
      return;
    if (this.TaskCaptions.Modified)
      this.View.DataGridView.RecalcRowHeights();
    this.View.HighlightCriticalTasks = this.HighlightCriticalTasks;
    this.View.ShowGrid = this.ShowGrid;
    this.View.Refresh();
  }

  internal void SetModified(bool value)
  {
    this.Modified = value;
    if (!value || this.Project == null)
      return;
    this.Project.Modified = true;
  }

  public ScaleType ScaleType
  {
    get => this._scaleType;
    set
    {
      if (this._scaleType == value)
        return;
      this._scaleType = value;
      this.SetModified(true);
    }
  }

  [NotNull]
  public string DateFormat
  {
    get => this._dateFormat;
    set
    {
      if (!(this._dateFormat != value))
        return;
      this._dateFormat = value;
      this.SetModified(true);
    }
  }

  public DateTime CurrentDate
  {
    get => this._currentDate;
    set
    {
      if (!(this._currentDate != value))
        return;
      this._currentDate = value;
      this.SetModified(true);
    }
  }

  public bool ShowProjectTask
  {
    get => this.Project.ShowProjectTask;
    set
    {
      if (this.Project.ShowProjectTask == value)
        return;
      this.Project.ShowProjectTask = value;
      this.SetModified(true);
    }
  }

  [NotNull]
  internal static string ToPickerDateFormat([NotNull] string format)
  {
    return format.Replace("\"'\"", "''''");
  }

  [NotNull]
  public string PickerDateFormat => ProjectDisplayOptions.ToPickerDateFormat(this.DateFormat);

  public bool HighlightCriticalTasks
  {
    get => this._highlightCriticalTasks;
    set
    {
      if (this._highlightCriticalTasks == value)
        return;
      this._highlightCriticalTasks = value;
      this.SetModified(true);
    }
  }

  /// <summary>Отображать сетку на диаграмме</summary>
  public bool ShowGrid
  {
    get => this._showGrid;
    set
    {
      if (this._showGrid == value)
        return;
      this._showGrid = value;
      this.SetModified(true);
    }
  }

  public bool Modified { get; private set; }

  public FactDurationsDisplayMode ShowFactDurations
  {
    get => this._showFactDurations;
    set
    {
      if (this._showFactDurations == value)
        return;
      this._showFactDurations = value;
      this.SetModified(true);
    }
  }

  public void Save([NotNull] XmlIni ini)
  {
    ini.WriteInteger("DisplaySettings", "ScaleType", (long) this.ScaleType);
    ini.WriteInteger("DisplaySettings", "CurrentDate", this.CurrentDate.ToBinary());
    ini.WriteBoolean("DisplaySettings", "ShowProjectTask", this.ShowProjectTask);
    ini.WriteBoolean("DisplaySettings", "HighlightCriticalTasks", this.HighlightCriticalTasks);
    ini.WriteBoolean("DisplaySettings", "ShowGrid", this.ShowGrid);
    ini.WriteString("DisplaySettings", "DateFormat", this.DateFormat);
    ini.WriteInteger("DisplaySettings", "ShowFactDurations", (long) this.ShowFactDurations);
    ini.WriteString("DisplaySettings", "TaskCaptions", this.TaskCaptions.AsString);
    ini.MainSection = "Filters";
    try
    {
      this.Filters.Save(ini, (Predicate<TaskFilter>) (tf => !tf.HasFlag(FilterFlags.Global)));
    }
    finally
    {
      ini.MainSection = string.Empty;
    }
    this.Modified = false;
  }

  public void Load([CanBeNull] XmlIni ini)
  {
    if (ini == null)
      return;
    this.ScaleType = (ScaleType) ini.ReadInteger("DisplaySettings", "ScaleType", (long) this.ScaleType);
    this.CurrentDate = DateTime.FromBinary(ini.ReadInteger("DisplaySettings", "CurrentDate", this.CurrentDate.ToBinary()));
    this.ShowProjectTask = ini.ReadBoolean("DisplaySettings", "ShowProjectTask", this.ShowProjectTask);
    this.HighlightCriticalTasks = ini.ReadBoolean("DisplaySettings", "HighlightCriticalTasks", this.HighlightCriticalTasks);
    this.ShowGrid = ini.ReadBoolean("DisplaySettings", "ShowGrid", this.ShowGrid);
    this.DateFormat = ini.ReadString("DisplaySettings", "DateFormat", this.DateFormat);
    this.ShowFactDurations = (FactDurationsDisplayMode) ini.ReadInteger("DisplaySettings", "ShowFactDurations", (long) this.ShowFactDurations);
    this.TaskCaptions.AsString = ini.ReadString("DisplaySettings", "TaskCaptions", this.TaskCaptions.AsString);
    ini.MainSection = "Filters";
    try
    {
      this.Filters.Load(ini);
    }
    finally
    {
      ini.MainSection = string.Empty;
    }
    this.Modified = false;
    this.Loaded = true;
  }

  public bool Loaded { get; private set; }
}
