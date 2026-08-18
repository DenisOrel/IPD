// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSViews.RowPropsDockControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Bars;
using Intermech.Docking;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.AVSViews;

public class RowPropsDockControl : DockControl, ISkipTargetActivate
{
  public static Guid DockGuid = new Guid("{13E6BFB6-1FF8-4441-88B0-22E55A6EB742}");
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private RowPropsUserControl rowPropsUserControl1;

  public RowPropsDockControl()
  {
    this.InitializeComponent();
    this.HideOnClose = true;
    this.Text = this.TabText;
    this.Guid = RowPropsDockControl.DockGuid;
  }

  public void Reset() => this.rowPropsUserControl1.Deactivate((IView) null);

  public void UpdateRows()
  {
    this.rowPropsUserControl1.Initialize((ISelectedItems) null, (System.IServiceProvider) null);
    this.rowPropsUserControl1.Activate((IView) null);
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
  private void InitializeComponent()
  {
    this.rowPropsUserControl1 = new RowPropsUserControl();
    this.SuspendLayout();
    this.rowPropsUserControl1.Dock = DockStyle.Fill;
    this.rowPropsUserControl1.Location = new Point(0, 0);
    this.rowPropsUserControl1.Name = "rowPropsUserControl1";
    this.rowPropsUserControl1.Size = new Size(299, 280);
    this.rowPropsUserControl1.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.rowPropsUserControl1);
    this.Name = nameof (RowPropsDockControl);
    this.Size = new Size(299, 280);
    this.TabText = "Форматирование";
    this.ResumeLayout(false);
  }
}
