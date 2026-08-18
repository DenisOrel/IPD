
// Type: Intermech.Docking.ActiveDocumentEventArgs
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;


namespace Intermech.Docking;

public class ActiveDocumentEventArgs : EventArgs
{
  private DockControl _prevActiveDocument;
  private DockControl _newActiveDocument;

  internal ActiveDocumentEventArgs(DockControl prevDoc, DockControl newDoc)
  {
    this._prevActiveDocument = prevDoc;
    this._newActiveDocument = newDoc;
  }

  public DockControl NewActiveDocument => this._newActiveDocument;

  public DockControl PreviousActiveDocument => this._prevActiveDocument;
}
