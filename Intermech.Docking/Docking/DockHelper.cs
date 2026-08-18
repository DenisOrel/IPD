
// Type: Intermech.Docking.DockHelper
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Windows.Forms;


namespace Intermech.Docking;

public class DockHelper
{
  public static void DetachControl(Control control)
  {
    if (control.Parent == null)
      return;
    if (control.ContainsFocus)
      control.Parent.Focus();
    if (control is DockControl)
      ((DockControl) control).IgnoreFontEvents = true;
    if (control.Parent != null)
    {
      try
      {
        control.Parent.Controls.Remove(control);
      }
      catch
      {
      }
    }
    if (!(control is DockControl))
      return;
    ((DockControl) control).IgnoreFontEvents = false;
  }

  public static void DetachDockControl(DockControl dockControl)
  {
    ControlLayoutSystem layoutSystem = dockControl != null ? dockControl._layoutSystem : throw new ArgumentNullException();
    if (layoutSystem == null)
      return;
    DockContainer dockContainer = layoutSystem.DockContainer;
    int num = dockControl.ContainsFocus ? 1 : 0;
    int index = layoutSystem.Controls.IndexOf(dockControl);
    layoutSystem.Controls.Remove(dockControl);
    if (layoutSystem.Controls.Count == 0)
    {
      if (layoutSystem.PopupContainer != null)
        layoutSystem.PopupContainer.DetachAutoHideManager();
      ((SplitLayoutSystem) layoutSystem.Parent).LayoutSystems.Remove((LayoutSystemBase) layoutSystem);
      if (dockContainer.IsFloating && dockContainer.LayoutSystem.LayoutSystems.Count == 0)
        dockContainer.Manager.DisposeFloatingContainer((FloatingDockContainer) dockContainer);
    }
    if (num == 0 || !(dockContainer is DocumentContainer))
      return;
    int count = layoutSystem.Controls.Count;
    if (index > count - 1)
      index = count - 1;
    DockControl dockControl1 = ((DocumentContainer) dockContainer).GetOldActiveDocument(dockControl);
    if (dockControl1 == null && layoutSystem.Controls.Count > 0)
      dockControl1 = layoutSystem.Controls[index];
    dockControl1?.Activate();
  }

  public static DockContainer GetDockContainerForLocation(
    DockManager dockManager,
    DockLocation location)
  {
    if (dockManager == null)
      throw new ArgumentNullException();
    switch (location)
    {
      case DockLocation.Left:
        return dockManager.GetDockContainer(DockStyle.Left) ?? throw new InvalidOperationException("No Left DockContainer found.");
      case DockLocation.Right:
        return dockManager.GetDockContainer(DockStyle.Right) ?? throw new InvalidOperationException("No Right DockContainer found.");
      case DockLocation.Top:
        return dockManager.GetDockContainer(DockStyle.Top) ?? throw new InvalidOperationException("No Top DockContainer found.");
      case DockLocation.Bottom:
        return dockManager.GetDockContainer(DockStyle.Bottom) ?? throw new InvalidOperationException("No Bottom DockContainer found.");
      case DockLocation.Center:
        return dockManager.GetDockContainer(DockStyle.Fill) ?? throw new InvalidOperationException("No Fill DockContainer found.");
      case DockLocation.Float:
        if (dockManager._dockContainers.Count == 0)
          throw new InvalidOperationException("No containers found.");
        return (DockContainer) dockManager.CreateFloatingDockContainer();
      case DockLocation.Document:
        return (DockContainer) (dockManager.GetDocumentContainer() ?? throw new InvalidOperationException("No DocumentContainer found."));
      default:
        return (DockContainer) null;
    }
  }

  public static ControlLayoutSystem FindOrCreateLayoutSystem(
    DockManager dockManager,
    DockLocation dockLocation,
    int desiredIndex,
    Guid guid,
    bool isDocument,
    bool collapsed)
  {
    DockContainer containerForLocation = DockHelper.GetDockContainerForLocation(dockManager, dockLocation);
    if (containerForLocation == null)
      throw new InvalidOperationException("No appropriate container could be found for the last fixed dock location recorded.");
    ControlLayoutSystem createLayoutSystem = (ControlLayoutSystem) null;
    foreach (LayoutSystemBase layoutSystem in containerForLocation._layoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem)
      {
        if (guid == Guid.Empty && createLayoutSystem == null)
          createLayoutSystem = layoutSystem as ControlLayoutSystem;
        if (((ControlLayoutSystem) layoutSystem).Guid == guid)
          return (ControlLayoutSystem) layoutSystem;
      }
    }
    ControlLayoutSystem layoutSystem1 = (ControlLayoutSystem) null;
    if (!isDocument && containerForLocation is DocumentContainer)
    {
      isDocument = true;
      DocumentContainer documentContainer = containerForLocation as DocumentContainer;
      if (documentContainer.ActiveDocument != null)
      {
        layoutSystem1 = documentContainer.ActiveDocument.LayoutSystem;
        createLayoutSystem = layoutSystem1;
      }
    }
    if (createLayoutSystem != null)
      return createLayoutSystem;
    if (layoutSystem1 == null)
      layoutSystem1 = isDocument ? (ControlLayoutSystem) new DocumentLayoutSystem() : new ControlLayoutSystem(guid);
    if (desiredIndex < 0)
      desiredIndex = 0;
    else if (desiredIndex > containerForLocation.LayoutSystem.LayoutSystems.Count)
      desiredIndex = containerForLocation.LayoutSystem.LayoutSystems.Count;
    containerForLocation.LayoutSystem.LayoutSystems.Insert(desiredIndex, (LayoutSystemBase) layoutSystem1);
    return layoutSystem1;
  }

  public static DockLocation DockStateToLocation(DockState state)
  {
    switch (state)
    {
      case DockState.Float:
        return DockLocation.Float;
      case DockState.DockTopAutoHide:
      case DockState.DockTop:
        return DockLocation.Top;
      case DockState.DockLeftAutoHide:
      case DockState.DockLeft:
        return DockLocation.Left;
      case DockState.DockBottomAutoHide:
      case DockState.DockBottom:
        return DockLocation.Bottom;
      case DockState.DockRightAutoHide:
      case DockState.DockRight:
        return DockLocation.Right;
      case DockState.Document:
        return DockLocation.Document;
      default:
        return DockLocation.Left;
    }
  }

  public static bool IsDockStateAutoHide(DockState dockState)
  {
    return dockState == DockState.DockLeftAutoHide || dockState == DockState.DockRightAutoHide || dockState == DockState.DockTopAutoHide || dockState == DockState.DockBottomAutoHide;
  }

  public static DockLocation DockStyleToState(DockStyle style)
  {
    switch (style)
    {
      case DockStyle.Top:
        return DockLocation.Top;
      case DockStyle.Bottom:
        return DockLocation.Bottom;
      case DockStyle.Left:
        return DockLocation.Left;
      case DockStyle.Right:
        return DockLocation.Right;
      default:
        return DockLocation.Center;
    }
  }

  public static bool IsDockStateDocked(DockState dockState)
  {
    return dockState == DockState.DockLeft || dockState == DockState.DockRight || dockState == DockState.DockTop || dockState == DockState.DockBottom;
  }

  public static bool IsDockBottom(DockState dockState)
  {
    return dockState == DockState.DockBottom || dockState == DockState.DockBottomAutoHide;
  }

  public static bool IsDockLeft(DockState dockState)
  {
    return dockState == DockState.DockLeft || dockState == DockState.DockLeftAutoHide;
  }

  public static bool IsDockRight(DockState dockState)
  {
    return dockState == DockState.DockRight || dockState == DockState.DockRightAutoHide;
  }

  public static bool IsDockTop(DockState dockState)
  {
    return dockState == DockState.DockTop || dockState == DockState.DockTopAutoHide;
  }

  public static bool IsDockStateValid(DockState dockState, DockLocation allowedLocations)
  {
    return ((allowedLocations & DockLocation.Float) != DockLocation.Unknown || dockState != DockState.Float) && ((allowedLocations & DockLocation.Document) != DockLocation.Unknown || dockState != DockState.Document) && ((allowedLocations & DockLocation.Left) != DockLocation.Unknown || dockState != DockState.DockLeft && dockState != DockState.DockLeftAutoHide) && ((allowedLocations & DockLocation.Right) != DockLocation.Unknown || dockState != DockState.DockRight && dockState != DockState.DockRightAutoHide) && ((allowedLocations & DockLocation.Top) != DockLocation.Unknown || dockState != DockState.DockTop && dockState != DockState.DockTopAutoHide) && ((allowedLocations & DockLocation.Bottom) != DockLocation.Unknown || dockState != DockState.DockBottom && dockState != DockState.DockBottomAutoHide);
  }

  public static bool IsValidRestoreState(DockState state)
  {
    return state == DockState.DockLeft || state == DockState.DockRight || state == DockState.DockTop || state == DockState.DockBottom || state == DockState.Document;
  }

  public static DockState ToggleAutoHideState(DockState state)
  {
    switch (state)
    {
      case DockState.DockTopAutoHide:
        return DockState.DockTop;
      case DockState.DockLeftAutoHide:
        return DockState.DockLeft;
      case DockState.DockBottomAutoHide:
        return DockState.DockBottom;
      case DockState.DockRightAutoHide:
        return DockState.DockRight;
      case DockState.DockTop:
        return DockState.DockTopAutoHide;
      case DockState.DockLeft:
        return DockState.DockLeftAutoHide;
      case DockState.DockBottom:
        return DockState.DockBottomAutoHide;
      case DockState.DockRight:
        return DockState.DockRightAutoHide;
      default:
        return state;
    }
  }
}
