// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.ImageState
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

public class ImageState : IImageState
{
  private object data;
  private object state;

  public ImageState(object data, object state)
  {
    this.data = data;
    this.state = state;
  }

  public object Data
  {
    get => this.data;
    set => this.data = value;
  }

  public object State
  {
    get => this.state;
    set => this.state = value;
  }
}
