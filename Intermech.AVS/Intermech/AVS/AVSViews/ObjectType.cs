// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSViews.ObjectType
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Drawing;

#nullable disable
namespace Intermech.AVS.AVSViews;

internal class ObjectType
{
  private Guid guid;
  private Image image;
  private int typeId;
  private string caption;

  public ObjectType(Image image, int typeId, Guid guid, string caption)
  {
    this.Image = image;
    this.TypeId = typeId;
    this.Caption = caption;
    this.Guid = guid;
  }

  public Guid Guid
  {
    get => this.guid;
    set => this.guid = value;
  }

  public Image Image
  {
    get => this.image;
    set => this.image = value;
  }

  public int TypeId
  {
    get => this.typeId;
    set => this.typeId = value;
  }

  public string Caption
  {
    get => this.caption;
    set => this.caption = value;
  }
}
