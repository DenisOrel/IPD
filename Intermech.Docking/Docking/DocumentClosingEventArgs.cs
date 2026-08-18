
// Type: Intermech.Docking.DocumentClosingEventArgs
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll


namespace Intermech.Docking;

public class DocumentClosingEventArgs : DockControlEventArgs
{
  private bool _cancel;

  internal DocumentClosingEventArgs(DockControl dc, bool cancel)
    : base(dc)
  {
    this._cancel = cancel;
  }

  public bool Cancel
  {
    get => this._cancel;
    set => this._cancel = value;
  }
}
