// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.BoundsChangingEventArgs
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System.Drawing;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Аргументы события SelectedElementBoundsChanging</summary>
public class BoundsChangingEventArgs
{
  public DocumentTreeNode Element;
  public RectangleF NewElementBounds;

  public BoundsChangingEventArgs(DocumentTreeNode element, RectangleF newElementBounds)
  {
    this.Element = element;
    this.NewElementBounds = newElementBounds;
  }
}
