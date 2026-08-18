// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.SelDocScript
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.Data;
using DevExpress.IM.Utils;
using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Columns;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class SelDocScript : Form
{
  internal List<SelDocScript.ScriptData> scripts = new List<SelDocScript.ScriptData>();
  internal List<int> selTypes = new List<int>();
  public long resScript = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button button2;
  private Button btnCancel;
  private CheckBox cbHideNoConds;
  private GridControl gc;
  private GridView gridView1;
  private GridColumn gridColumn1;
  private GridColumn gridColumn2;
  private GridColumn gridColumn3;

  public SelDocScript() => this.InitializeComponent();

  public long Execute(long[] selItems)
  {
    this.scripts.Clear();
    this.CollectScripts();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long selItem in selItems)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(selItem, false);
        if (dbObject != null && !this.selTypes.Contains(dbObject.ObjectType))
          this.selTypes.Add(dbObject.ObjectType);
      }
    }
    this.ShowScripts();
    return this.ShowDialog() == DialogResult.OK ? this.resScript : -1L;
  }

  internal void CollectScripts()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objDocScript).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[4]
      {
        (object) -2,
        (object) -50,
        (object) -10,
        (object) ExpertConsts.Consts.attrScriptObjTypes
      })).Rows)
        this.scripts.Add(new SelDocScript.ScriptData(sessionKeeper.Session, row));
    }
  }

  internal void ShowScripts()
  {
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < this.scripts.Count; ++index)
    {
      SelDocScript.ScriptData script = this.scripts[index];
      if (!this.cbHideNoConds.Checked || script.allowedTypes.Count != 0)
      {
        bool flag = true;
        if (script.allowedTypes.Count > 0)
        {
          foreach (int selType in this.selTypes)
          {
            flag = false;
            foreach (int allowedType in script.allowedTypes)
            {
              if (MetaDataHelper.IsObjectTypeChildOf(selType, allowedType))
              {
                flag = true;
                break;
              }
            }
            if (!flag)
              break;
          }
        }
        if (flag)
          arrayList.Add((object) new SelDocScript.RowElem(index, script.Name, script.Id, script.modifyDate));
      }
    }
    this.gc.DataSource = (object) arrayList;
    ColumnView mainView1 = this.gc.MainView as ColumnView;
    mainView1.Columns[0].ColumnHandle = 0;
    mainView1.Columns[1].ColumnHandle = 1;
    mainView1.Columns[2].ColumnHandle = 2;
    GridView mainView2 = this.gc.MainView as GridView;
    mainView2.BeginSort();
    try
    {
      mainView2.ClearSorting();
      mainView2.Columns[0].SortOrder = ColumnSortOrder.Ascending;
    }
    finally
    {
      mainView2.EndSort();
    }
  }

  private void button2_Click(object sender, EventArgs e)
  {
    ColumnView mainView = this.gc.MainView as ColumnView;
    if (mainView.FocusedRowHandle == -999999)
      this.DialogResult = DialogResult.None;
    else
      this.resScript = this.scripts[(mainView.GetRow(mainView.FocusedRowHandle) as SelDocScript.RowElem).index].Id;
  }

  private void cbHideNoConds_CheckedChanged(object sender, EventArgs e) => this.ShowScripts();

  private void gc_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    this.button2_Click(sender, (EventArgs) null);
    this.DialogResult = DialogResult.OK;
  }

  private void SelDocScript_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void SelDocScript_FormClosed(object sender, FormClosedEventArgs e)
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelDocScript));
    this.panel1 = new Panel();
    this.cbHideNoConds = new CheckBox();
    this.button2 = new Button();
    this.btnCancel = new Button();
    this.gc = new GridControl();
    this.gridView1 = new GridView();
    this.gridColumn1 = new GridColumn();
    this.gridColumn2 = new GridColumn();
    this.gridColumn3 = new GridColumn();
    this.panel1.SuspendLayout();
    this.gc.BeginInit();
    this.gridView1.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.cbHideNoConds);
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.cbHideNoConds, "cbHideNoConds");
    this.cbHideNoConds.Checked = true;
    this.cbHideNoConds.CheckState = CheckState.Checked;
    this.cbHideNoConds.Name = "cbHideNoConds";
    this.cbHideNoConds.UseVisualStyleBackColor = true;
    this.cbHideNoConds.CheckedChanged += new EventHandler(this.cbHideNoConds_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.OK;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    this.button2.Click += new EventHandler(this.button2_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.gc, "gc");
    this.gc.EmbeddedNavigator.AccessibleDescription = componentResourceManager.GetString("gc.EmbeddedNavigator.AccessibleDescription");
    this.gc.EmbeddedNavigator.AccessibleName = componentResourceManager.GetString("gc.EmbeddedNavigator.AccessibleName");
    this.gc.EmbeddedNavigator.Anchor = (AnchorStyles) componentResourceManager.GetObject("gc.EmbeddedNavigator.Anchor");
    this.gc.EmbeddedNavigator.BackgroundImage = (Image) componentResourceManager.GetObject("gc.EmbeddedNavigator.BackgroundImage");
    this.gc.EmbeddedNavigator.BackgroundImageLayout = (ImageLayout) componentResourceManager.GetObject("gc.EmbeddedNavigator.BackgroundImageLayout");
    this.gc.EmbeddedNavigator.Dock = (DockStyle) componentResourceManager.GetObject("gc.EmbeddedNavigator.Dock");
    this.gc.EmbeddedNavigator.ImeMode = (ImeMode) componentResourceManager.GetObject("gc.EmbeddedNavigator.ImeMode");
    this.gc.EmbeddedNavigator.Name = "";
    this.gc.MainView = (BaseView) this.gridView1;
    this.gc.Name = "gc";
    this.gc.Styles.AddReplace("EvenRow", (object) new ViewStyleEx("EvenRow", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled | StyleOptions.UseBackColor, Color.Beige, SystemColors.WindowText, Color.GhostWhite, LinearGradientMode.ForwardDiagonal));
    this.gc.Styles.AddReplace("OddRow", (object) new ViewStyleEx("OddRow", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled | StyleOptions.UseBackColor, Color.Wheat, SystemColors.WindowText, Color.White, LinearGradientMode.BackwardDiagonal));
    this.gc.Styles.AddReplace("FocusedCell", (object) new ViewStyleEx("FocusedCell", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, SystemColors.Highlight, SystemColors.WindowText, SystemColors.InactiveCaption, LinearGradientMode.Horizontal));
    this.gc.Styles.AddReplace("SelectedRow", (object) new ViewStyleEx("SelectedRow", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor, SystemColors.Highlight, SystemColors.HighlightText, Color.Empty, LinearGradientMode.Horizontal));
    this.gc.MouseDoubleClick += new MouseEventHandler(this.gc_MouseDoubleClick);
    componentResourceManager.ApplyResources((object) this.gridView1, "gridView1");
    this.gridView1.Columns.AddRange(new GridColumn[3]
    {
      this.gridColumn1,
      this.gridColumn2,
      this.gridColumn3
    });
    this.gridView1.FocusRectStyle = DrawFocusRectStyle.RowFocus;
    this.gridView1.GridControl = this.gc;
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
    this.gridColumn1.SortIndex = 0;
    this.gridColumn1.SortOrder = ColumnSortOrder.Ascending;
    this.gridColumn1.VisibleIndex = 0;
    this.gridColumn1.Width = 400;
    componentResourceManager.ApplyResources((object) this.gridColumn2, "gridColumn2");
    this.gridColumn2.Name = "gridColumn2";
    this.gridColumn2.Options = ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.ReadOnly | ColumnOptions.CanFocused | ColumnOptions.ShowInCustomizationForm | ColumnOptions.NonEditable;
    this.gridColumn2.VisibleIndex = 1;
    this.gridColumn2.Width = 80 /*0x50*/;
    componentResourceManager.ApplyResources((object) this.gridColumn3, "gridColumn3");
    this.gridColumn3.Name = "gridColumn3";
    this.gridColumn3.Options = ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.ReadOnly | ColumnOptions.CanFocused | ColumnOptions.ShowInCustomizationForm | ColumnOptions.NonEditable;
    this.gridColumn3.VisibleIndex = 2;
    this.gridColumn3.Width = 129;
    this.AcceptButton = (IButtonControl) this.button2;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.gc);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelDocScript);
    this.FormClosed += new FormClosedEventHandler(this.SelDocScript_FormClosed);
    this.Load += new EventHandler(this.SelDocScript_Load);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.gc.EndInit();
    this.gridView1.EndInit();
    this.ResumeLayout(false);
  }

  internal class ScriptData
  {
    public long Id;
    public string Name;
    public List<int> allowedTypes = new List<int>();
    public DateTime modifyDate = DateTime.Now;

    public ScriptData(IUserSession ius, DataRow row)
    {
      this.Id = Convert.ToInt64(row[0]);
      this.Name = Convert.ToString(row[1]);
      this.modifyDate = Convert.ToDateTime(row[2]);
      IDBAttribute attributeByGuid = ius.GetObject(this.Id).GetAttributeByGuid(new Guid(ExpertAttrGUIDs.attrScriptObjTypes), false);
      if (attributeByGuid == null)
        return;
      foreach (object obj in attributeByGuid.Values)
      {
        if (obj != DBNull.Value)
          this.allowedTypes.Add(MetaDataHelper.GetObjectTypeID(new Guid(Convert.ToString(obj))));
      }
    }
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
