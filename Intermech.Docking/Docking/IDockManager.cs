
// Type: Intermech.Docking.IDockManager
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Rendering;
using System;
using System.Windows.Forms;


namespace Intermech.Docking;

public interface IDockManager
{
  event DockControlEventHandler DockControlActivated;

  event EventHandler DockingFinished;

  event EventHandler DockingStarted;

  event ShowControlContextMenuEventHandler ShowControlContextMenu;

  string GetLayout();

  void SetLayout(string layout);

  DockingHints DockingHints { get; set; }

  DockingManager DockingManager { get; set; }

  Form OwnerForm { get; set; }

  ImageList ImageList { get; set; }

  RendererBase Renderer { get; set; }
}
