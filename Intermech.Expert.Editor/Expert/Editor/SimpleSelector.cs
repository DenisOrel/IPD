// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.SimpleSelector
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
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Summary description for SimpleSelector.</summary>
public class SimpleSelector : Form
{
  private Panel panel1;
  private Button button2;
  private Button button1;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private Panel panel2;
  private GridControl gc;
  private GridView gridView1;
  private GridColumn gridColumn1;
  private GridColumn gridColumn2;
  private GridColumn gridColumn3;
  private DataTable dt;
  private string selGuid;

  public SimpleSelector() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SimpleSelector));
    this.panel1 = new Panel();
    this.button2 = new Button();
    this.button1 = new Button();
    this.panel2 = new Panel();
    this.gc = new GridControl();
    this.gridView1 = new GridView();
    this.gridColumn1 = new GridColumn();
    this.gridColumn2 = new GridColumn();
    this.gridColumn3 = new GridColumn();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.gc.BeginInit();
    this.gridView1.BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.Click += new EventHandler(this.button1_Click);
    this.panel2.Controls.Add((Control) this.gc);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.gc, "gc");
    this.gc.EmbeddedNavigator.Name = "";
    this.gc.MainView = (BaseView) this.gridView1;
    this.gc.Name = "gc";
    this.gc.Styles.AddReplace("EvenRow", (object) new ViewStyleEx("EvenRow", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled | StyleOptions.UseBackColor, Color.Beige, SystemColors.WindowText, SystemColors.Window, LinearGradientMode.ForwardDiagonal));
    this.gc.Styles.AddReplace("OddRow", (object) new ViewStyleEx("OddRow", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled | StyleOptions.UseBackColor, Color.Wheat, SystemColors.WindowText, SystemColors.Window, LinearGradientMode.BackwardDiagonal));
    this.gc.Styles.AddReplace("Row", (object) new ViewStyleEx("Row", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled, SystemColors.GradientInactiveCaption, SystemColors.WindowText, Color.Empty, LinearGradientMode.Horizontal));
    this.gc.DoubleClick += new EventHandler(this.gc_DoubleClick);
    this.gridView1.Columns.AddRange(new GridColumn[3]
    {
      this.gridColumn1,
      this.gridColumn2,
      this.gridColumn3
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
    this.gridColumn1.Name = "gridColumn1";
    this.gridColumn1.Options = ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.FixedWidth | ColumnOptions.CanFocused | ColumnOptions.ShowInCustomizationForm;
    this.gridColumn1.VisibleIndex = 0;
    this.gridColumn1.Width = 373;
    componentResourceManager.ApplyResources((object) this.gridColumn2, "gridColumn2");
    this.gridColumn2.Name = "gridColumn2";
    this.gridColumn2.VisibleIndex = 1;
    this.gridColumn2.Width = 66;
    componentResourceManager.ApplyResources((object) this.gridColumn3, "gridColumn3");
    this.gridColumn3.Name = "gridColumn3";
    this.gridColumn3.VisibleIndex = 2;
    this.gridColumn3.Width = 177;
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.button2;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SimpleSelector);
    this.ShowInTaskbar = false;
    this.Tag = (object) "";
    this.FormClosed += new FormClosedEventHandler(this.SimpleSelector_FormClosed);
    this.Load += new EventHandler(this.SimpleSelector_Load);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.gc.EndInit();
    this.gridView1.EndInit();
    this.ResumeLayout(false);
  }

  public int Execute(DataTable dt, string oName, string selectGuid = "")
  {
    this.dt = dt;
    this.Text = LocalizationHolder.rm.GetString("Expert.Editor_406") + oName + LocalizationHolder.rm.GetString("Expert.Editor_407");
    this.selGuid = selectGuid;
    this.UpdateGrid();
    ColumnView mainView = this.gc.MainView as ColumnView;
    return this.ShowDialog() == DialogResult.OK && mainView.FocusedRowHandle != -999999 ? (mainView.GetRow(mainView.FocusedRowHandle) as SimpleSelector.RowElem).index : -1;
  }

  public int Execute(DataTable dt)
  {
    this.dt = dt;
    this.Text = LocalizationHolder.rm.GetString("Expert.Editor_558");
    this.UpdateGrid();
    ColumnView mainView = this.gc.MainView as ColumnView;
    return this.ShowDialog() == DialogResult.OK && mainView.FocusedRowHandle != -999999 ? (mainView.GetRow(mainView.FocusedRowHandle) as SimpleSelector.RowElem).index : -1;
  }

  private void UpdateGrid()
  {
    int num1 = 0;
    if (this.dt != null)
      num1 = this.dt.Rows.Count;
    ArrayList arrayList = new ArrayList();
    long val = -1;
    if (this.dt != null)
    {
      for (int index = 0; index < num1; ++index)
      {
        DataRow row = this.dt.Rows[index];
        DateTime dateTime = Convert.ToDateTime(row[this.dt.Columns.Count - 1]);
        string str = Convert.ToString(row[1]);
        arrayList.Add((object) new SimpleSelector.RowElem(index, Convert.ToString(row[2]), Convert.ToInt64(row[0]), dateTime));
        string selGuid = this.selGuid;
        if (str == selGuid)
          val = Convert.ToInt64(row[0]);
      }
      this.gc.DataSource = (object) arrayList;
    }
    ColumnView mainView = this.gc.MainView as ColumnView;
    mainView.Columns[0].ColumnHandle = 0;
    mainView.Columns[1].ColumnHandle = 1;
    mainView.Columns[2].ColumnHandle = 2;
    if (val < 0L)
      return;
    GridColumn column = mainView.Columns[1];
    int num2 = mainView.LocateByValue(0, column, (object) val);
    if (num2 == -999999)
      return;
    mainView.FocusedRowHandle = num2;
  }

  private void button1_Click(object sender, EventArgs e)
  {
    if ((this.gc.MainView as ColumnView).FocusedRowHandle != -999999)
      return;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_578"), LocalizationHolder.rm.GetString("Expert.Editor_59"), MessageBoxButtons.OK);
    this.DialogResult = DialogResult.None;
  }

  private void gc_DoubleClick(object sender, EventArgs e)
  {
    this.button1_Click(sender, e);
    this.DialogResult = DialogResult.OK;
  }

  private void SimpleSelector_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void SimpleSelector_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  public class RowElem
  {
    internal int index;
    private string caption;
    private long _Id;
    private DateTime dt;

    public RowElem(int Index, string Caption, long Id, DateTime dt)
    {
      this.index = Index;
      this.caption = Caption;
      this._Id = Id;
      this.dt = dt;
    }

    public string Caption => this.caption;

    public long Id => this._Id;

    public DateTime ModifyDate => this.dt;
  }
}
