// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.IconicMenuItem
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using System.Drawing;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Iconic MenuItem</summary>
public class IconicMenuItem : MenuButtonItem
{
  public Rectangle iconBounds;

  internal void SetBounds(Graphics graphics, Rectangle bounds, bool vertical, bool rightToLeft)
  {
    this.ApplyLayout(bounds, graphics, vertical, rightToLeft);
    this.iconBounds = bounds;
    this.iconBounds.Inflate(-4, -4);
  }
}
