// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.MapImageEx
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Map;
using System;

#nullable disable
namespace Intermech.Workflow.Design;

[Serializable]
internal class MapImageEx : MapImage
{
  private string _tooltip = "";

  public string ToolTip
  {
    get => this._tooltip;
    set => this._tooltip = value;
  }

  public override string GetToolTip(MapView view) => this.ToolTip;
}
