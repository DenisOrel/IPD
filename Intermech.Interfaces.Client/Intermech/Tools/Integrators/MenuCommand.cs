// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.MenuCommand
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Drawing;

#nullable disable
namespace Intermech.Tools.Integrators;

public sealed class MenuCommand
{
  private string text;
  private string tooltip;
  private Image image;
  private Action commandHandler;

  public MenuCommand(string text, string tooltip, Image image, Action commandHandler)
  {
    if (string.IsNullOrEmpty(text))
      throw new ArgumentException();
    if (commandHandler == null)
      throw new ArgumentNullException();
    this.text = text;
    this.tooltip = tooltip;
    this.image = image;
    this.commandHandler = commandHandler;
  }

  public string Text => this.text;

  public string Tooltip => this.tooltip;

  public Image Image => this.image;

  public Action CommandHandler => this.commandHandler;
}
