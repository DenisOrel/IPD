// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ChooseActivityForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for ChooseActivityDlg.</summary>
public class ChooseActivityForm : Form
{
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  public AutoSizeLabel CaptionLabel;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private ColumnHeader columnHeader3;
  private ColumnHeader columnHeader4;
  private EnhListView actView;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public ChooseActivityForm() => this.InitializeComponent();

  public DataTable DataSource
  {
    set
    {
      this.actView.SmallImageList = Holder.UsersImageList;
      foreach (DataRow row in (InternalDataCollectionBase) value.Rows)
      {
        ListViewItem listViewItem = this.actView.Items.Add(MiscFunx.UserRefToString(row[1]));
        listViewItem.Tag = row[0];
        listViewItem.SubItems.Add(SimpleFuncs.GetEnumDescription((Enum) (ActivityStatus) (DBNull.Value.Equals(row[2]) ? 0 : Convert.ToInt32(row[2]))));
        listViewItem.SubItems.Add(row[3].ToString());
        listViewItem.SubItems.Add(row[4].ToString());
        listViewItem.ImageIndex = Holder.UserImageIndex;
      }
      if (this.actView.Items.Count <= 0)
        return;
      this.actView.Items[0].Selected = true;
    }
  }

  public long CurrentID
  {
    get
    {
      long currentId = 0;
      if (this.actView.SelectedItems.Count > 0)
        currentId = Convert.ToInt64(this.actView.SelectedItems[0].Tag);
      return currentId;
    }
  }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChooseActivityForm));
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.CaptionLabel = new AutoSizeLabel();
    this.actView = new EnhListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.columnHeader4 = new ColumnHeader();
    this.Panel2.SuspendLayout();
    this.SuspendLayout();
    this.Panel2.Controls.Add((Control) this.CancButton);
    this.Panel2.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.Panel2, "Panel2");
    this.Panel2.Name = "Panel2";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    componentResourceManager.ApplyResources((object) this.CaptionLabel, "CaptionLabel");
    this.CaptionLabel.Name = "CaptionLabel";
    this.actView.AllowManualSorting = true;
    this.actView.Columns.AddRange(new ColumnHeader[4]
    {
      this.columnHeader1,
      this.columnHeader2,
      this.columnHeader3,
      this.columnHeader4
    });
    componentResourceManager.ApplyResources((object) this.actView, "actView");
    this.actView.FullRowSelect = true;
    this.actView.HideSelection = false;
    this.actView.MultiSelect = false;
    this.actView.Name = "actView";
    this.actView.OwnerDraw = true;
    this.actView.RadioGroups = false;
    this.actView.SortColumn = 0;
    this.actView.SubitemImages = (ImageList) null;
    this.actView.UseCompatibleStateImageBehavior = false;
    this.actView.View = View.Details;
    this.actView.DoubleClick += new EventHandler(this.actView_DoubleClick);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.columnHeader3, "columnHeader3");
    componentResourceManager.ApplyResources((object) this.columnHeader4, "columnHeader4");
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.actView);
    this.Controls.Add((Control) this.CaptionLabel);
    this.Controls.Add((Control) this.Panel2);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ChooseActivityForm);
    this.FormClosed += new FormClosedEventHandler(this.ChooseActivityForm_FormClosed);
    this.Load += new EventHandler(this.ChooseActivityForm_Load);
    this.Panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void actView_DoubleClick(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.OK;
  }

  private void ChooseActivityForm_Load(object sender, EventArgs e)
  {
    HybridDictionary layoutData = this.actView.LayoutData;
    FormStorage.LoadLayout((Control) this, (IDictionary) layoutData);
    this.actView.LayoutData = layoutData;
  }

  private void ChooseActivityForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this, (IDictionary) this.actView.LayoutData);
  }
}
