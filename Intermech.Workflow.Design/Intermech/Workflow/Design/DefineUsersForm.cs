// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.DefineUsersForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for DefineUsersForm.</summary>
public class DefineUsersForm : Form
{
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  private ColumnHeader Column;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private GroupBox groupBox1;
  private UsersListView UsersView;
  private ColumnHeader columnHeader1;
  private Panel panel1;
  private Button DelButton;
  private Button AddButton;
  private ParticipantList _parts = new ParticipantList();

  public DefineUsersForm(long processID)
  {
    this.InitializeComponent();
    this.UsersView.ProcessID = processID;
    this.UsersView.AddButton = this.AddButton;
    this.UsersView.DelButton = this.DelButton;
  }

  public ParticipantList Participants
  {
    get => this._parts;
    set
    {
      this._parts.Assign(value);
      this.UsersView.Participants = this._parts;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public bool ReadOnly
  {
    get => this.UsersView.ReadOnly;
    set => this.UsersView.ReadOnly = value;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DefineUsersForm));
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.Column = new ColumnHeader();
    this.groupBox1 = new GroupBox();
    this.panel1 = new Panel();
    this.DelButton = new Button();
    this.AddButton = new Button();
    this.UsersView = new UsersListView();
    this.columnHeader1 = new ColumnHeader();
    this.Panel2.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.panel1.SuspendLayout();
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
    componentResourceManager.ApplyResources((object) this.Column, "Column");
    this.groupBox1.BackColor = Color.Transparent;
    this.groupBox1.Controls.Add((Control) this.UsersView);
    this.groupBox1.Controls.Add((Control) this.panel1);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    this.panel1.Controls.Add((Control) this.DelButton);
    this.panel1.Controls.Add((Control) this.AddButton);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.DelButton, "DelButton");
    this.DelButton.Name = "DelButton";
    componentResourceManager.ApplyResources((object) this.AddButton, "AddButton");
    this.AddButton.Name = "AddButton";
    this.UsersView.AddButton = (Button) null;
    this.UsersView.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    this.UsersView.DelButton = (Button) null;
    componentResourceManager.ApplyResources((object) this.UsersView, "UsersView");
    this.UsersView.FullRowSelect = true;
    this.UsersView.HideSelection = false;
    this.UsersView.Name = "UsersView";
    this.UsersView.OwnerDraw = true;
    this.UsersView.ProcessID = 0L;
    this.UsersView.RadioGroups = false;
    this.UsersView.SortColumn = 0;
    this.UsersView.Sorting = SortOrder.Ascending;
    this.UsersView.SubitemImages = (ImageList) null;
    this.UsersView.UseCompatibleStateImageBehavior = false;
    this.UsersView.View = View.Details;
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.Panel2);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (DefineUsersForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) "   ";
    this.Panel2.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
