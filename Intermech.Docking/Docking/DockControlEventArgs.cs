
// Type: Intermech.Docking.DockControlEventArgs
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;


namespace Intermech.Docking;

public class DockControlEventArgs : EventArgs
{
  private DockControl _dockControl;

  internal DockControlEventArgs(DockControl dc) => this._dockControl = dc;

  public DockControl DockControl => this._dockControl;
}
