// Decompiled with JetBrains decompiler
// Type: IMLauncher.ShowExistsLibrary
// Assembly: IMLauncher, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DAC2135C-3212-4DE0-9552-DF99FF4FD793
// Assembly location: D:\IPS\Client\IMLauncher.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace IMLauncher;

public class ShowExistsLibrary : Form
{
  private string imLibrary;
  private IContainer components;
  private Button button1;
  private DataGridView existLibraryGrid;
  private DataGridViewTextBoxColumn exLibrary;
  private Panel panel1;

  public string ImLibrary => this.imLibrary;

  public ShowExistsLibrary(List<string> exLibraries)
  {
    this.InitializeComponent();
    foreach (string exLibrary in exLibraries)
      this.existLibraryGrid.Rows.Add(new object[1]
      {
        (object) exLibrary
      });
  }

  private void button1_Click(object sender, EventArgs e)
  {
    this.imLibrary = this.existLibraryGrid[0, this.existLibraryGrid.CurrentRow.Index].Value.ToString();
  }

  private void existLibraryGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
  {
    this.imLibrary = this.existLibraryGrid[0, this.existLibraryGrid.CurrentRow.Index].Value.ToString();
    this.DialogResult = DialogResult.OK;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ShowExistsLibrary));
    this.button1 = new Button();
    this.existLibraryGrid = new DataGridView();
    this.exLibrary = new DataGridViewTextBoxColumn();
    this.panel1 = new Panel();
    ((ISupportInitialize) this.existLibraryGrid).BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    componentResourceManager.ApplyResources((object) this.existLibraryGrid, "existLibraryGrid");
    this.existLibraryGrid.AllowUserToAddRows = false;
    this.existLibraryGrid.BackgroundColor = SystemColors.ControlLightLight;
    this.existLibraryGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.existLibraryGrid.Columns.AddRange((DataGridViewColumn) this.exLibrary);
    this.existLibraryGrid.Name = "existLibraryGrid";
    this.existLibraryGrid.ReadOnly = true;
    this.existLibraryGrid.RowHeadersVisible = false;
    this.existLibraryGrid.CellDoubleClick += new DataGridViewCellEventHandler(this.existLibraryGrid_CellDoubleClick);
    this.exLibrary.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this.exLibrary, "exLibrary");
    this.exLibrary.Name = "exLibrary";
    this.exLibrary.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.button1);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.existLibraryGrid);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ShowExistsLibrary);
    ((ISupportInitialize) this.existLibraryGrid).EndInit();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
