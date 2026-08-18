// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.SelUserProc
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Columns;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class SelUserProc : Form
{
  internal List<string> Names;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button button2;
  private Button button1;
  private GridControl gc;
  private GridView gridView1;
  private GridColumn gridColumn1;

  public SelUserProc() => this.InitializeComponent();

  internal void LoadUserProcs()
  {
    ArrayList arrayList = new ArrayList();
    this.Names = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IExpertServer)) as IExpertServer).GetProcNames();
    for (int index = 0; index < this.Names.Count; ++index)
      arrayList.Add((object) new SelUserProc.RowElem(index, this.Names[index]));
    this.gc.DataSource = (object) arrayList;
    (this.gc.MainView as ColumnView).Columns[0].ColumnHandle = 0;
  }

  internal void LoadComparers()
  {
    ArrayList arrayList = new ArrayList();
    this.Names = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IExpertServer)) as IExpertServer).GetComparerNames();
    for (int index = 0; index < this.Names.Count; ++index)
      arrayList.Add((object) new SelUserProc.RowElem(index, this.Names[index]));
    this.gc.DataSource = (object) arrayList;
    (this.gc.MainView as ColumnView).Columns[0].ColumnHandle = 0;
  }

  public string Execute(bool comparers)
  {
    if (comparers)
      this.LoadComparers();
    else
      this.LoadUserProcs();
    if (this.ShowDialog() == DialogResult.OK)
    {
      ColumnView mainView = this.gc.MainView as ColumnView;
      if (mainView.FocusedRowHandle != -999999)
        return (mainView.GetRow(mainView.FocusedRowHandle) as SelUserProc.RowElem).Caption;
    }
    return "";
  }

  private void gc_DoubleClick(object sender, EventArgs e)
  {
    if ((this.gc.MainView as ColumnView).FocusedRowHandle == -999999)
      return;
    this.DialogResult = DialogResult.OK;
  }

  private void SelUserProc_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void SelUserProc_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelUserProc));
    this.panel1 = new Panel();
    this.button2 = new Button();
    this.button1 = new Button();
    this.gc = new GridControl();
    this.gridView1 = new GridView();
    this.gridColumn1 = new GridColumn();
    this.panel1.SuspendLayout();
    this.gc.BeginInit();
    this.gridView1.BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.OK;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.Cancel;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.gc, "gc");
    this.gc.EmbeddedNavigator.Name = "";
    this.gc.MainView = (BaseView) this.gridView1;
    this.gc.Name = "gc";
    this.gc.Styles.AddReplace("Row", (object) new ViewStyleEx("Row", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled, SystemColors.GradientInactiveCaption, SystemColors.WindowText, Color.Empty, LinearGradientMode.Horizontal));
    this.gc.Styles.AddReplace("OddRow", (object) new ViewStyleEx("OddRow", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled | StyleOptions.UseBackColor, Color.Wheat, SystemColors.WindowText, SystemColors.Window, LinearGradientMode.BackwardDiagonal));
    this.gc.Styles.AddReplace("EvenRow", (object) new ViewStyleEx("EvenRow", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled | StyleOptions.UseBackColor, Color.PaleGoldenrod, SystemColors.WindowText, SystemColors.Window, LinearGradientMode.ForwardDiagonal));
    this.gc.DoubleClick += new EventHandler(this.gc_DoubleClick);
    this.gridView1.Columns.AddRange(new GridColumn[1]
    {
      this.gridColumn1
    });
    this.gridView1.GridControl = this.gc;
    componentResourceManager.ApplyResources((object) this.gridView1, "gridView1");
    this.gridView1.Name = "gridView1";
    this.gridView1.OptionsBehavior.Editable = false;
    this.gridView1.OptionsMenu.EnableColumnMenu = false;
    this.gridView1.OptionsMenu.EnableFooterMenu = false;
    this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
    this.gridView1.OptionsSelection.InvertSelection = true;
    this.gridView1.OptionsView.ShowGroupPanel = false;
    this.gridView1.PaintStyleName = "MixedXP";
    componentResourceManager.ApplyResources((object) this.gridColumn1, "gridColumn1");
    this.gridColumn1.Name = "gridColumn1";
    this.gridColumn1.Options = ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.FixedWidth | ColumnOptions.CanFocused | ColumnOptions.ShowInCustomizationForm;
    this.gridColumn1.VisibleIndex = 0;
    this.AcceptButton = (IButtonControl) this.button2;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.button1;
    this.Controls.Add((Control) this.gc);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (SelUserProc);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.SelUserProc_Load);
    this.FormClosed += new FormClosedEventHandler(this.SelUserProc_FormClosed);
    this.panel1.ResumeLayout(false);
    this.gc.EndInit();
    this.gridView1.EndInit();
    this.ResumeLayout(false);
  }

  public class RowElem
  {
    internal int index;
    private string caption;

    public RowElem(int Index, string Caption)
    {
      this.index = Index;
      this.caption = Caption;
    }

    public string Caption => this.caption;
  }
}
