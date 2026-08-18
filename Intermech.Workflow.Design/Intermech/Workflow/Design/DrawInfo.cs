// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.DrawInfo
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Drawing;

#nullable disable
namespace Intermech.Workflow.Design;

public struct DrawInfo(EnhListView view, Color foreColor)
{
  public readonly EnhListView View = view;
  public readonly Color ForeColor = foreColor;
}
