// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.UndoItem
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

public class UndoItem
{
  private string caption;
  private object tag;

  public UndoItem(string caption, object tag)
  {
    this.caption = caption;
    this.tag = tag;
  }

  /// <summary>Заголовок на кнопках</summary>
  public string Caption
  {
    get => this.caption;
    set => this.caption = value;
  }

  /// <summary>
  /// Некий объект ко которому окно будет идентифицировать что отменять
  /// </summary>
  public object Tag
  {
    get => this.tag;
    set => this.tag = value;
  }
}
