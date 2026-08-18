// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.PvTlbControl
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class PvTlbControl : Control
{
  private ImRtfEditor e;
  internal Button PvPages;

  internal PvTlbControl(ImRtfEditor ctl)
  {
    this.e = ctl;
    this.PvPages = new Button();
    this.PvPages.Parent = (Control) this;
    this.PvPages.Top = 4;
    this.PvPages.Left = 150;
    this.PvPages.Height = 26;
    this.PvPages.Width = 70;
    this.PvPages.Text = this.e.TotalPreviewPages == 1 ? "Two Page" : "One Page";
    this.PvPages.Click += new EventHandler(this.e.prt.PvTlbPvPagesClick);
  }
}
