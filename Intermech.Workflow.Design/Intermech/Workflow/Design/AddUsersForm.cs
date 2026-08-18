// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.AddUsersForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Controls;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class AddUsersForm : FormEx
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  private UsersListView ParticipantsView;
  private ColumnHeader Column;
  private Panel panel3;
  private Button DelUserButton;
  private Button AddUserButton;

  public AddUsersForm()
  {
    this.InitializeComponent();
    this.ParticipantsView.AddButton = this.AddUserButton;
    this.ParticipantsView.DelButton = this.DelUserButton;
  }

  public ParticipantList Participants
  {
    get => this.ParticipantsView.Participants;
    set => this.ParticipantsView.Participants = value;
  }

  public bool Modified => this.ParticipantsView.Modified;

  public long ProcessID
  {
    get => this.ParticipantsView.ProcessID;
    set => this.ParticipantsView.ProcessID = value;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AddUsersForm));
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.ParticipantsView = new UsersListView();
    this.Column = new ColumnHeader();
    this.panel3 = new Panel();
    this.DelUserButton = new Button();
    this.AddUserButton = new Button();
    this.Panel2.SuspendLayout();
    this.panel3.SuspendLayout();
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
    this.ParticipantsView.AddButton = (Button) null;
    this.ParticipantsView.Columns.AddRange(new ColumnHeader[1]
    {
      this.Column
    });
    this.ParticipantsView.DelButton = (Button) null;
    componentResourceManager.ApplyResources((object) this.ParticipantsView, "ParticipantsView");
    this.ParticipantsView.FullRowSelect = true;
    this.ParticipantsView.HideSelection = false;
    this.ParticipantsView.Name = "ParticipantsView";
    this.ParticipantsView.OwnerDraw = true;
    this.ParticipantsView.ProcessID = 0L;
    this.ParticipantsView.RadioGroups = false;
    this.ParticipantsView.ReadOnly = false;
    this.ParticipantsView.SortColumn = 0;
    this.ParticipantsView.Sorting = SortOrder.Ascending;
    this.ParticipantsView.SubitemImages = (ImageList) null;
    this.ParticipantsView.UseCompatibleStateImageBehavior = false;
    this.ParticipantsView.View = View.Details;
    componentResourceManager.ApplyResources((object) this.Column, "Column");
    this.panel3.Controls.Add((Control) this.DelUserButton);
    this.panel3.Controls.Add((Control) this.AddUserButton);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.DelUserButton, "DelUserButton");
    this.DelUserButton.Name = "DelUserButton";
    componentResourceManager.ApplyResources((object) this.AddUserButton, "AddUserButton");
    this.AddUserButton.Name = "AddUserButton";
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.ParticipantsView);
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.Panel2);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AddUsersForm);
    this.ShowInTaskbar = false;
    this.Panel2.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
