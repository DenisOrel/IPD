// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.PredefinedBordersForTableUserControl
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary> Контрол, который отрисовывает возможные предопределённые варианты границ для таблицы </summary>
public class PredefinedBordersForTableUserControl : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListBox _listBox;

  public PredefinedBordersForTableUserControl() => this.InitializeComponent();

  /// <summary> Отрисовка элемента списка </summary>
  private void _listBox_DrawItem(object sender, DrawItemEventArgs e)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PredefinedBordersForTableUserControl));
    this._listBox = new ListBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._listBox, "_listBox");
    this._listBox.BackColor = SystemColors.Control;
    this._listBox.BorderStyle = BorderStyle.None;
    this._listBox.FormattingEnabled = true;
    this._listBox.Name = "_listBox";
    this._listBox.DrawItem += new DrawItemEventHandler(this._listBox_DrawItem);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._listBox);
    this.Name = nameof (PredefinedBordersForTableUserControl);
    this.ResumeLayout(false);
  }
}
