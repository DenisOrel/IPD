// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCView
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Map;
using System.Drawing;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCView : MapView
{
  public LCView()
  {
    this.NewLinkClass = typeof (LCLink);
    this.PortGravity = 30f;
    this.GridCellSize = new SizeF(5f, 10f);
    this.GridSnapDrag = MapViewSnapStyle.Jump;
  }

  public override MapDocument CreateDocument()
  {
    LCDocument document = new LCDocument();
    document.UndoManager = new MapUndoManager();
    return (MapDocument) document;
  }

  public virtual LCDocument LCDocument => (LCDocument) this.Document;
}
