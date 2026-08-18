
// Type: Intermech.Controls.BlockedMouseDBLClickTreeView
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary>Tree View  с заблокированным даблкликом</summary>
public class BlockedMouseDBLClickTreeView : TreeView
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public BlockedMouseDBLClickTreeView() => this.InitializeComponent();

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 515)
      m.Result = IntPtr.Zero;
    else
      base.WndProc(ref m);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
}
