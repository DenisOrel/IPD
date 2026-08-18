
// Type: Intermech.Docking.DocumentListEventArgs
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;


namespace Intermech.Docking;

public class DocumentListEventArgs : EventArgs
{
  private DockControl[] _documents;
  private DocumentLayoutSystem _layoutSystem;

  public DocumentListEventArgs(DocumentLayoutSystem dls)
  {
    this._layoutSystem = dls;
    this._documents = new DockControl[dls.Controls.Count];
    dls.Controls.CopyTo(this._documents, 0);
  }

  public DockControl[] Documents => this._documents;

  public DocumentLayoutSystem LayoutSystem => this._layoutSystem;
}
