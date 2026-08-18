// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.DockControlLayoutSettings
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Docking;
using Intermech.Interfaces.Configuration;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

/// <summary>
/// Класс свойств DockControlов для сохранения их положения
/// </summary>
public class DockControlLayoutSettings
{
  private string controlName;
  private DockLocation dockLocation = DockLocation.Right;
  private bool visible;
  private bool opened;
  private bool autoHide;
  private Point location = new Point(100, 200);
  private bool vertical;
  private Guid layoutGuid;
  private Size size = new Size(200, 400);

  /// <summary>Получить настройки контрола</summary>
  /// <param name="control">контрол</param>
  /// <param name="controlName">имя контрола</param>
  /// <returns></returns>
  public static DockControlLayoutSettings GetSettings(DockControl control, string controlName)
  {
    DockControlLayoutSettings settings = new DockControlLayoutSettings();
    settings.ControlName = controlName;
    if (control == null)
      return settings;
    settings.Visible = control.Visible;
    settings.Opened = control.LayoutSystem != null;
    settings.DockLocation = control.DockLocation;
    if (settings.DockLocation == DockLocation.Document || settings.DockLocation == DockLocation.All || settings.DockLocation == DockLocation.Center || settings.DockLocation == DockLocation.Unknown)
      settings.DockLocation = DockLocation.Right;
    if (control.LayoutSystem != null)
    {
      settings.AutoHide = control.LayoutSystem.Collapsed;
      settings.LayoutGuid = control.LayoutSystem.Guid;
      if (control.LayoutSystem.Parent is SplitLayoutSystem)
        settings.Vertical = (control.LayoutSystem.Parent as SplitLayoutSystem).SplitMode == Orientation.Vertical;
    }
    if (settings.DockLocation == DockLocation.Float)
    {
      settings.Location = control.FloatingLocation;
      settings.Size = control.FloatingSize;
    }
    else if (control.LayoutSystem != null)
    {
      settings.Location = control.LayoutSystem.Bounds.Location;
      settings.Size = control.LayoutSystem.Bounds.Size;
    }
    return settings;
  }

  private DockContainer GetDockContainerForLocation(DockManager dockManager, DockLocation location)
  {
    if (dockManager == null)
      throw new ArgumentNullException();
    switch (location)
    {
      case DockLocation.Left:
        return dockManager.GetDockContainer(DockStyle.Left);
      case DockLocation.Right:
        return dockManager.GetDockContainer(DockStyle.Right);
      case DockLocation.Top:
        return dockManager.GetDockContainer(DockStyle.Top);
      case DockLocation.Bottom:
        return dockManager.GetDockContainer(DockStyle.Bottom);
      default:
        return (DockContainer) null;
    }
  }

  public void Open(DockControl control, DockManager dockManager)
  {
    if (this.DockLocation == DockLocation.Document || this.DockLocation == DockLocation.All || this.DockLocation == DockLocation.Center || this.DockLocation == DockLocation.Unknown)
      this.DockLocation = DockLocation.Right;
    bool flag = control.LayoutSystem == null;
    Size size1 = this.Size;
    if (size1.Width <= 0)
      size1.Width = 300;
    if (size1.Height <= 0)
      size1.Height = 300;
    control.SetFloatingValues(size1, this.Location, this.DockLocation);
    if (control.Manager != dockManager)
      control.Manager = dockManager;
    try
    {
      SplitLayoutSystem layoutSystem1 = (SplitLayoutSystem) null;
      if (this.DockLocation == DockLocation.Float)
      {
        control.FloatingSize = size1;
        control.FloatingLocation = this.Location;
      }
      else
      {
        control.FloatingSize = size1;
        control.FloatingLocation = this.Location;
        DockContainer containerForLocation = this.GetDockContainerForLocation(dockManager, this.DockLocation);
        if (containerForLocation != null)
        {
          if (this.Vertical)
          {
            foreach (object layoutSystem2 in (CollectionBase) containerForLocation.LayoutSystem.LayoutSystems)
            {
              if (layoutSystem2 is SplitLayoutSystem && (layoutSystem2 as SplitLayoutSystem).SplitMode == Orientation.Vertical)
                layoutSystem1 = layoutSystem2 as SplitLayoutSystem;
            }
            if (layoutSystem1 == null)
            {
              layoutSystem1 = new SplitLayoutSystem();
              layoutSystem1.SplitMode = Orientation.Vertical;
              containerForLocation.LayoutSystem.LayoutSystems.Add((LayoutSystemBase) layoutSystem1);
              layoutSystem1.SetWorkingSize(new Size(0, size1.Height));
              layoutSystem1.Parent.SetWorkingSize(new Size(0, size1.Height));
            }
            ControlLayoutSystem layoutSystem3 = new ControlLayoutSystem(this.LayoutGuid);
            layoutSystem3.SetWorkingSize(size1);
            SizeF workingSize = layoutSystem1.Parent.WorkingSize;
            int width1 = (int) workingSize.Width + size1.Width;
            layoutSystem1.SetWorkingSize(new Size(width1, size1.Height));
            LayoutSystemBase parent = layoutSystem1.Parent;
            int width2 = width1;
            workingSize = layoutSystem1.WorkingSize;
            int height = (int) workingSize.Height;
            Size size2 = new Size(width2, height);
            parent.SetWorkingSize(size2);
            control.Size = size1;
            layoutSystem1.LayoutSystems.Add((LayoutSystemBase) layoutSystem3);
            layoutSystem3.SetWorkingSize(size1);
            layoutSystem1.SetWorkingSize(new Size(width1, size1.Height));
            int num = 0;
            foreach (object layoutSystem4 in (CollectionBase) layoutSystem1.LayoutSystems)
            {
              if (layoutSystem4 is ControlLayoutSystem controlLayoutSystem && controlLayoutSystem.SelectedControl != null)
              {
                controlLayoutSystem.SetWorkingSize(controlLayoutSystem.SelectedControl.FloatingSize);
                num += controlLayoutSystem.SelectedControl.FloatingSize.Width;
              }
            }
            containerForLocation.Width = num + size1.Width;
            containerForLocation.CalculateAllMetricsAndLayout();
          }
          else if (containerForLocation.Width <= 0)
          {
            foreach (object layoutSystem5 in (CollectionBase) containerForLocation.LayoutSystem.LayoutSystems)
            {
              if (layoutSystem5 is SplitLayoutSystem && (layoutSystem5 as SplitLayoutSystem).SplitMode == Orientation.Vertical)
                (layoutSystem5 as SplitLayoutSystem).SetWorkingSize(new Size(size1.Width, size1.Height));
            }
            containerForLocation.Width = size1.Width;
            containerForLocation.CalculateAllMetricsAndLayout();
          }
        }
      }
      control.Open(this.DockLocation, true, this.LayoutGuid);
      if (this.DockLocation == DockLocation.Float)
      {
        control.FloatingSize = size1;
        control.FloatingLocation = this.Location;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      control.Open(DockLocation.Right, true);
    }
    if (!flag || control.LayoutSystem == null)
      return;
    control.LayoutSystem.Collapsed = this.AutoHide;
  }

  /// <summary>Получить настройки из конфига</summary>
  /// <param name="config">конфиг</param>
  /// <param name="controlName">имя контрола</param>
  /// <returns></returns>
  public static DockControlLayoutSettings GetSettings(
    IConfiguration config,
    string controlName,
    DockLocation defaultDockLocation = DockLocation.Right)
  {
    DockControlLayoutSettings settings = new DockControlLayoutSettings();
    settings.ControlName = controlName;
    string property1 = config.GetProperty(controlName + "DockLocation");
    if (property1 != null && property1 != "")
    {
      settings.DockLocation = (DockLocation) Enum.Parse(typeof (DockLocation), property1);
      if (settings.DockLocation == DockLocation.Document || settings.DockLocation == DockLocation.All || settings.DockLocation == DockLocation.Center || settings.DockLocation == DockLocation.Unknown)
        settings.DockLocation = defaultDockLocation;
    }
    else
      settings.DockLocation = defaultDockLocation;
    string property2 = config.GetProperty(controlName + "Visible");
    if (property2 != null && property2 != "")
      settings.Visible = bool.Parse(property2);
    string property3 = config.GetProperty(controlName + "AutoHide");
    if (property3 != null && property3 != "")
      settings.AutoHide = bool.Parse(property3);
    string property4 = config.GetProperty(controlName + "Opened");
    if (property4 != null && property4 != "")
      settings.Opened = bool.Parse(property4);
    string property5 = config.GetProperty(controlName + "Vertical");
    if (property5 != null && property5 != "")
      settings.Vertical = bool.Parse(property5);
    PointConverter pointConverter = new PointConverter();
    string property6 = config.GetProperty(controlName + "Location");
    if (property6 != null && property6 != "")
    {
      string text = property6.Replace(';', ',');
      settings.Location = (Point) pointConverter.ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, text);
    }
    SizeConverter sizeConverter = new SizeConverter();
    string property7 = config.GetProperty(controlName + "Size");
    if (property7 != null && property7 != "")
    {
      string text = property7.Replace(';', ',');
      settings.Size = (Size) sizeConverter.ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, text);
    }
    string property8 = config.GetProperty(controlName + "LayoutGuid");
    if (property8 != null && property8 != "")
      settings.LayoutGuid = new Guid(property8);
    return settings;
  }

  /// <summary>Занести настройки в конфиг</summary>
  /// <param name="config">конфиг</param>
  /// <param name="controlName">имя контрола</param>
  public void SetSettings(IConfiguration config, string controlName)
  {
    new DockControlLayoutSettings().ControlName = controlName;
    if (this.DockLocation == DockLocation.Document || this.DockLocation == DockLocation.All || this.DockLocation == DockLocation.Center || this.DockLocation == DockLocation.Unknown)
      this.DockLocation = DockLocation.Right;
    config.SetProperty(controlName + "DockLocation", this.DockLocation.ToString());
    config.SetProperty(controlName + "Visible", this.Visible.ToString());
    config.SetProperty(controlName + "AutoHide", this.AutoHide.ToString());
    config.SetProperty(controlName + "Opened", this.Opened.ToString());
    config.SetProperty(controlName + "LayoutGuid", this.LayoutGuid.ToString());
    config.SetProperty(controlName + "Vertical", this.Vertical.ToString());
    PointConverter pointConverter = new PointConverter();
    config.SetProperty(controlName + "Location", pointConverter.ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) this.Location));
    SizeConverter sizeConverter = new SizeConverter();
    config.SetProperty(controlName + "Size", sizeConverter.ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) this.Size));
  }

  public string ControlName
  {
    get => this.controlName;
    set => this.controlName = value;
  }

  public DockLocation DockLocation
  {
    get => this.dockLocation;
    set => this.dockLocation = value;
  }

  public bool Visible
  {
    get => this.visible;
    set => this.visible = value;
  }

  public bool Opened
  {
    get => this.opened;
    set => this.opened = value;
  }

  public bool AutoHide
  {
    get => this.autoHide;
    set => this.autoHide = value;
  }

  public Point Location
  {
    get => this.location;
    set => this.location = value;
  }

  public bool Vertical
  {
    get => this.vertical;
    set => this.vertical = value;
  }

  public Guid LayoutGuid
  {
    get => this.layoutGuid;
    set => this.layoutGuid = value;
  }

  public Size Size
  {
    get => this.size;
    set => this.size = value;
  }
}
