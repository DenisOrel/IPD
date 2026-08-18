// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.ImDocumentDockManagerStorage
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Docking;
using Intermech.Document.UI;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

#nullable disable
namespace Intermech.Document.Model.UI;

/// <summary>
/// Вспомогательный класс для сохранения и восстановления внутренних окон редактора документов
/// </summary>
public class ImDocumentDockManagerStorage : DockManagerConfigurationStorage
{
  private List<DockControl> emptyControls = new List<DockControl>();
  /// <summary>Список настроек сохранения</summary>
  private Dictionary<DockControl, DockControlLayoutSettings> settingsDict = new Dictionary<DockControl, DockControlLayoutSettings>();
  private ImDocumentEditorFormBase docform;
  private DockManager dockManager;
  private IConfigurationManager configManager;
  private string configName = "ImDocEditorDocking";
  private static List<string> exceptionGuids = new List<string>();
  public DockManager.GetDockControlCallback GetDockControlEvent;

  public ImDocumentDockManagerStorage(
    ImDocumentEditorFormBase form,
    DockManager manager,
    IConfigurationManager configManager)
  {
    this.DockManager = manager;
    this.ConfigManager = configManager;
    this.docform = form;
  }

  /// <summary>Установить настройки сохранения контрола</summary>
  /// <param name="control">Контрол который сохраняется</param>
  /// <param name="settings">Его настройки</param>
  public void SetSettings(DockControl control, DockControlLayoutSettings settings)
  {
    this.settingsDict[control] = settings;
  }

  /// <summary>Получить настройки сохранения контрола</summary>
  /// <param name="control">Контрол который сохраняется</param>
  /// <returns>Его настройки</returns>
  public DockControlLayoutSettings GetSettings(DockControl control)
  {
    return this.settingsDict.ContainsKey(control) ? this.settingsDict[control] : (DockControlLayoutSettings) null;
  }

  public ImDocumentEditorFormBase Documentform
  {
    get => this.docform;
    set => this.docform = value;
  }

  public DockManager DockManager
  {
    get => this.dockManager;
    set => this.dockManager = value;
  }

  public IConfigurationManager ConfigManager
  {
    get => this.configManager;
    set => this.configManager = value;
  }

  /// <summary>Имя конфигурации в которой происходит сохранение</summary>
  public string ConfigName
  {
    get => this.configName;
    set => this.configName = value;
  }

  /// <summary>Установка контрола для сохранения его положения</summary>
  /// <param name="control"></param>
  public void SetControl(DockControl control, DockLocation defaultDockLocation = DockLocation.Right)
  {
    control.Closed += new EventHandler(this.control_PositionChanged);
    control.SizeChanged += new EventHandler(this.control_PositionChanged);
    control.LocationChanged += new EventHandler(this.control_PositionChanged);
    control.VisibleChanged += new EventHandler(this.control_PositionChanged);
    control.DockSituationChanged += new EventHandler(this.control_PositionChanged);
    control.AutoHidePopupClosed += new EventHandler(this.control_PositionChanged);
    control.AutoHidePopupOpened += new EventHandler(this.control_PositionChanged);
    IConfiguration config = this.configManager.Open(this.ConfigName) ?? this.configManager.Create(this.ConfigName);
    if (config == null)
      return;
    DockControlLayoutSettings settings = DockControlLayoutSettings.GetSettings(config, control.GetType().Name, defaultDockLocation);
    this.SetSettings(control, settings);
  }

  /// <summary>Отписывание от событий</summary>
  /// <param name="control"></param>
  public void DisposeControl(DockControl control)
  {
    control.Closed -= new EventHandler(this.control_PositionChanged);
    control.SizeChanged -= new EventHandler(this.control_PositionChanged);
    control.LocationChanged -= new EventHandler(this.control_PositionChanged);
    control.VisibleChanged -= new EventHandler(this.control_PositionChanged);
    control.DockSituationChanged -= new EventHandler(this.control_PositionChanged);
    control.AutoHidePopupClosed -= new EventHandler(this.control_PositionChanged);
    control.AutoHidePopupOpened -= new EventHandler(this.control_PositionChanged);
  }

  private void control_PositionChanged(object sender, EventArgs e)
  {
    if (this.Documentform == null)
      return;
    this.Documentform.NeedSaveControlsConfig = true;
    this.Documentform.SaveControlsConfig();
    if (!(sender is DockControl control))
      return;
    DockControlLayoutSettings settings = DockControlLayoutSettings.GetSettings(control, control.GetType().Name);
    IConfiguration config = this.configManager.Open(this.ConfigName) ?? this.configManager.Create(this.ConfigName);
    if (config == null)
      return;
    settings.SetSettings(config, settings.ControlName);
    this.SetSettings(control, settings);
  }

  public override void SaveLayout(string layout)
  {
    (this.configManager.Open(this.ConfigName) ?? this.configManager.Create(this.ConfigName))?.SetProperty("Docking", layout);
  }

  public static void AddException(string guidString)
  {
    if (ImDocumentDockManagerStorage.exceptionGuids.Contains(guidString))
      return;
    ImDocumentDockManagerStorage.exceptionGuids.Add(guidString);
  }

  public override string TryLoadLayout()
  {
    string origin = "";
    if (this.configManager != null)
    {
      origin = this.configManager.Open(this.ConfigName)?.GetProperty("Docking") ?? "";
      if (origin != "")
        origin = ImDocumentDockManagerStorage.RemoveExceptionalControls(origin);
    }
    return origin;
  }

  /// <summary>
  /// Исключить ненужные элементы из сериализованной расклвдки
  /// </summary>
  private static string RemoveExceptionalControls(string origin)
  {
    string str1 = "";
    if (string.IsNullOrWhiteSpace(origin))
      return str1;
    try
    {
      XDocument xdocument = XDocument.Parse(origin);
      XElement root = xdocument.Root;
      List<XElement> list = root != null ? root.Elements().ToList<XElement>() : (List<XElement>) null;
      if (list == null)
        return origin;
      xdocument.Root.Elements().Remove<XElement>();
      List<XElement> content = new List<XElement>();
      List<string> exceptionIds = new List<string>();
      foreach (XElement xelement in list)
      {
        if (ImDocumentDockManagerStorage.exceptionGuids.Contains(xelement.Attribute((XName) "Guid")?.Value ?? ""))
        {
          string str2 = xelement.Attribute((XName) "ID")?.Value;
          if (str2 != null)
            exceptionIds.Add(str2);
        }
        else
          content.Add(xelement);
      }
      foreach (XElement xelement1 in content)
      {
        if (xelement1.Descendants((XName) "Control").Any<XElement>((Func<XElement, bool>) (c => exceptionIds.Contains(c.Attribute((XName) "ID")?.Value))))
        {
          XElement xelement2 = xelement1.Element((XName) "SplitLayoutSystem");
          if (xelement2 != null)
            xelement2.Elements().Remove<XElement>();
          XAttribute xattribute = xelement1.Element((XName) "SplitLayoutSystem")?.Attribute((XName) "LayoutSystems");
          if (xattribute != null)
            xattribute.Value = "0";
        }
      }
      xdocument.Root.Add((object) content);
      return $"<?xml version=\"1.0\" encoding=\"utf-16\"?>{xdocument}";
    }
    catch
    {
      return origin;
    }
  }

  public virtual void SaveConfiguration()
  {
    this.DockManager.SaveConfiguration((DockManagerConfigurationStorage) this);
  }

  public virtual bool LoadConfiguration()
  {
    this.emptyControls.Clear();
    if (this.TryLoadLayout() == "")
      return false;
    if (this.DockManager == null)
      return false;
    try
    {
      this.DockManager.LoadConfiguration((DockManagerConfigurationStorage) this, new DockManager.GetDockControlCallback(this.GetDockControl));
      this.ClearEmptyLayout();
    }
    catch (Exception ex)
    {
      LogManager.AddLine(ex, true);
    }
    return true;
  }

  public void ClearEmptyLayout()
  {
    foreach (DockControl emptyControl in this.emptyControls)
    {
      emptyControl.HideOnClose = false;
      emptyControl.Close();
      emptyControl.Dispose();
    }
    this.emptyControls.Clear();
  }

  public void ClearEmptyLayout(DockStyle dockStyle)
  {
    foreach (object layoutSystem in (CollectionBase) this.DockManager.GetDockContainer(dockStyle).LayoutSystem.LayoutSystems)
    {
      if (layoutSystem is LayoutSystemBase system)
        this.ClearEmptyLayout1(system);
    }
  }

  public void ClearEmptyLayout1(LayoutSystemBase system)
  {
    if (system is SplitLayoutSystem splitLayoutSystem)
    {
      foreach (object layoutSystem in (CollectionBase) splitLayoutSystem.LayoutSystems)
      {
        if (layoutSystem is LayoutSystemBase system1)
          this.ClearEmptyLayout1(system1);
      }
    }
    if (!(system is ControlLayoutSystem controlLayoutSystem) || controlLayoutSystem.Controls.Count != 0)
      return;
    controlLayoutSystem.Dispose();
  }

  public DockControl GetDockControl(Guid guid, string persistString, string text)
  {
    if (this.GetDockControlEvent == null)
      return (DockControl) null;
    DockControl dockControl = this.GetDockControlEvent(guid, persistString, text);
    if (dockControl == null)
    {
      dockControl = new DockControl();
      this.emptyControls.Add(dockControl);
    }
    return dockControl;
  }
}
