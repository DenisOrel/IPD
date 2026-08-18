// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.InsertSymbolDockControl
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Controls;
using Intermech.Docking;
using Intermech.Document.RtfEditor;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary> Плавающая панель вставки спецсимвола </summary>
public class InsertSymbolDockControl : DockControl, ISkipTargetActivate
{
  public static Guid DockGuid = new Guid("{AA5E492E-FBBB-4C4E-8545-886E17E24C7B}");
  public DocumentControl documentControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  public CharacterMap characterMap1;
  private Panel panel1;

  public InsertSymbolDockControl()
  {
    this.InitializeComponent();
    this.HideOnClose = true;
    this.Guid = InsertSymbolDockControl.DockGuid;
  }

  /// <summary>Контрол документа</summary>
  public DocumentControl DocumentControl
  {
    [DebuggerStepThrough] get => this.documentControl;
    set => this.documentControl = value;
  }

  protected override void OnPaint(PaintEventArgs e) => base.OnPaint(e);

  private void characterMap1_OnCharSelected(object source, CharacterMap.CharacterMapEventArgs e)
  {
    if (this.documentControl == null || this.documentControl.ReadOnly)
      return;
    ImRtfEditor activeEditorControl = this.documentControl.GetActiveEditorControl();
    if (activeEditorControl == null || activeEditorControl.ReadOnlyMode)
      return;
    activeEditorControl.InsertTerText(e.SelectedChar, e.SelectedFont.Name, true);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.DocumentControl = (DocumentControl) null;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (InsertSymbolDockControl));
    this.panel1 = new Panel();
    this.characterMap1 = new CharacterMap();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.characterMap1.BackColor = SystemColors.Control;
    this.characterMap1.CellBackGroundColor = Color.Beige;
    this.characterMap1.CellBorderColor = SystemColors.ControlDarkDark;
    this.characterMap1.CellBorderWidth = 0;
    this.characterMap1.CellSpacing = 1;
    this.characterMap1.CellWidth = 30;
    this.characterMap1.CharMapBackGroundColor = SystemColors.Control;
    this.characterMap1.CurrentFont = new Font("Agency FB", 23.95f, FontStyle.Regular, GraphicsUnit.Pixel);
    componentResourceManager.ApplyResources((object) this.characterMap1, "characterMap1");
    this.characterMap1.GridBackGroundColor = Color.Beige;
    this.characterMap1.GridFontColor = Color.Black;
    this.characterMap1.Name = "characterMap1";
    this.characterMap1.PreviewBackGroundColor = Color.White;
    this.characterMap1.PreviewCellWidth = 56;
    this.characterMap1.PreviewFontColor = Color.Black;
    this.characterMap1.TabStop = false;
    this.characterMap1.OnCharSelected += new CharacterMap.CharSelectedEventHandler(this.characterMap1_OnCharSelected);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.characterMap1);
    this.Name = nameof (InsertSymbolDockControl);
    this.Tag = (object) "";
    this.ResumeLayout(false);
  }
}
