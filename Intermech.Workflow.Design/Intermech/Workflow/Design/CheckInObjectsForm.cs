// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.CheckInObjectsForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Extensions;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class CheckInObjectsForm : Form
{
  private Dictionary<int, int> _checkinStatusImageIndex = new Dictionary<int, int>();
  public AttachmentList Attachments;
  private ImageListViewSubItem _lastCheckInSI;
  private ListViewItem _lastLI;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel BottomPanel;
  private Button CancButton;
  private EnhListView CheckInView;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private Label CheckInProgressLabel;
  private ProgressBar CheckInProgressBar;
  private Label CheckInLabel;

  public CheckInObjectsForm()
  {
    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    this.InitializeComponent();
    this.CheckInView.SmallImageList = BaseHolder.NamedList.ImageList;
    this.CheckInView.SubitemImages = BaseHolder.IconService.ImageList;
    this._checkinStatusImageIndex.Add(-1, BaseHolder.NamedList.ImageIndex("imgStart"));
    this._checkinStatusImageIndex.Add(0, BaseHolder.NamedList.ImageIndex("imgInvalidRule"));
    this._checkinStatusImageIndex.Add(1, BaseHolder.NamedList.ImageIndex("imgOk"));
    this.BackColor = Color.Transparent;
  }

  public bool Embedded
  {
    get => this.BottomPanel.Visible;
    set => this.BottomPanel.Visible = false;
  }

  public void DoCheckIn(AttachmentList attachments)
  {
    this.CheckInProgressBar.Maximum = attachments.WorkCopies.Count;
    this.CheckInProgressBar.Value = 0;
    if (wfFunx.CheckInAttachments(attachments, new wfFunx.AttachCheckInProcessDelegate(this.OnAttachmentCheckIn)))
    {
      this.CheckInProgressLabel.Text = LocalizationHolder.rm.GetString("Workflow.Design_78");
      this.DialogResult = DialogResult.OK;
    }
    else
    {
      this.CheckInProgressLabel.Text = "При сдаче рабочих копий возникли ошибки. Запуск процесса невозможен.";
      this.DialogResult = DialogResult.Abort;
    }
  }

  public static bool CheckInAttachments(AttachmentList attachments)
  {
    using (CheckInObjectsForm checkInObjectsForm = new CheckInObjectsForm())
    {
      checkInObjectsForm.Attachments = attachments;
      if (checkInObjectsForm.ShowDialog() == DialogResult.Abort)
        return false;
    }
    return true;
  }

  private void CheckInObjectsForm_Shown(object sender, EventArgs e)
  {
    this.Refresh();
    if (this.Attachments == null)
      return;
    this.DoCheckIn(this.Attachments);
  }

  private void OnAttachmentCheckIn(
    Attachment att,
    string caption,
    bool beforeProcess,
    bool result,
    string errorText = "")
  {
    if (beforeProcess)
    {
      ListViewItem listViewItem = this.CheckInView.Items.Add("");
      listViewItem.ImageIndex = this._checkinStatusImageIndex[-1];
      this._lastLI = listViewItem;
      this._lastCheckInSI = new ImageListViewSubItem(caption, BaseHolder.IconService.IndexOf(4, att.TypeID));
      listViewItem.SubItems.Add((ListViewItem.ListViewSubItem) this._lastCheckInSI);
    }
    else
    {
      this._lastLI.ImageIndex = this._checkinStatusImageIndex[Convert.ToInt32(result)];
      if (!string.IsNullOrEmpty(errorText))
      {
        this.CheckInView.ShowItemToolTips = true;
        this._lastLI.ToolTipText = errorText;
      }
      this.CheckInProgressBar.SetProgressNoAnimation(this.CheckInProgressBar.Value + 1);
      if (result)
        att.CheckOutBy = 0L;
    }
    this.CheckInView.Refresh();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CheckInObjectsForm));
    this.BottomPanel = new Panel();
    this.CancButton = new Button();
    this.CheckInProgressLabel = new Label();
    this.CheckInProgressBar = new ProgressBar();
    this.CheckInLabel = new Label();
    this.CheckInView = new EnhListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.BottomPanel.SuspendLayout();
    this.SuspendLayout();
    this.BottomPanel.Controls.Add((Control) this.CancButton);
    componentResourceManager.ApplyResources((object) this.BottomPanel, "BottomPanel");
    this.BottomPanel.Name = "BottomPanel";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.CheckInProgressLabel, "CheckInProgressLabel");
    this.CheckInProgressLabel.Name = "CheckInProgressLabel";
    componentResourceManager.ApplyResources((object) this.CheckInProgressBar, "CheckInProgressBar");
    this.CheckInProgressBar.Name = "CheckInProgressBar";
    componentResourceManager.ApplyResources((object) this.CheckInLabel, "CheckInLabel");
    this.CheckInLabel.Name = "CheckInLabel";
    this.CheckInView.AllowManualSorting = true;
    this.CheckInView.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    componentResourceManager.ApplyResources((object) this.CheckInView, "CheckInView");
    this.CheckInView.FullRowSelect = true;
    this.CheckInView.HeaderStyle = ColumnHeaderStyle.None;
    this.CheckInView.Name = "CheckInView";
    this.CheckInView.OwnerDraw = true;
    this.CheckInView.RadioGroups = false;
    this.CheckInView.SortColumn = 0;
    this.CheckInView.Sorting = SortOrder.Ascending;
    this.CheckInView.SubitemImages = (ImageList) null;
    this.CheckInView.UseCompatibleStateImageBehavior = false;
    this.CheckInView.View = View.Details;
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.CheckInView);
    this.Controls.Add((Control) this.CheckInProgressLabel);
    this.Controls.Add((Control) this.CheckInProgressBar);
    this.Controls.Add((Control) this.CheckInLabel);
    this.Controls.Add((Control) this.BottomPanel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (CheckInObjectsForm);
    this.ShowInTaskbar = false;
    this.Shown += new EventHandler(this.CheckInObjectsForm_Shown);
    this.BottomPanel.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
