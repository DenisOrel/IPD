// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ParticipantsEditorForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for ParticipantsEditorForm.</summary>
public class ParticipantsEditorForm : Form
{
  private Button button1;
  private Button button2;
  private Button AddButton;
  private ListView ParticipantsView;
  private ColumnHeader Column;
  private Button DelButton;
  private GroupBox groupBox1;
  private RadioButton radioButton1;
  private RadioButton radioButton2;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  public ParticipantList Participants = new ParticipantList();
  private Button button3;
  private static readonly IUserNamesCache _cache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;

  public ParticipantsEditorForm() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ParticipantsEditorForm));
    this.ParticipantsView = new ListView();
    this.Column = new ColumnHeader();
    this.button1 = new Button();
    this.button2 = new Button();
    this.AddButton = new Button();
    this.DelButton = new Button();
    this.groupBox1 = new GroupBox();
    this.radioButton2 = new RadioButton();
    this.radioButton1 = new RadioButton();
    this.button3 = new Button();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.ParticipantsView.Columns.AddRange(new ColumnHeader[1]
    {
      this.Column
    });
    this.ParticipantsView.FullRowSelect = true;
    this.ParticipantsView.HideSelection = false;
    componentResourceManager.ApplyResources((object) this.ParticipantsView, "ParticipantsView");
    this.ParticipantsView.Name = "ParticipantsView";
    this.ParticipantsView.UseCompatibleStateImageBehavior = false;
    this.ParticipantsView.View = View.Details;
    componentResourceManager.ApplyResources((object) this.Column, "Column");
    this.button1.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.button2.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    componentResourceManager.ApplyResources((object) this.AddButton, "AddButton");
    this.AddButton.Name = "AddButton";
    this.AddButton.Click += new EventHandler(this.button3_Click);
    componentResourceManager.ApplyResources((object) this.DelButton, "DelButton");
    this.DelButton.Name = "DelButton";
    this.DelButton.Click += new EventHandler(this.DelButton_Click);
    this.groupBox1.Controls.Add((Control) this.radioButton2);
    this.groupBox1.Controls.Add((Control) this.radioButton1);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.radioButton2, "radioButton2");
    this.radioButton2.Name = "radioButton2";
    this.radioButton1.Checked = true;
    componentResourceManager.ApplyResources((object) this.radioButton1, "radioButton1");
    this.radioButton1.Name = "radioButton1";
    this.radioButton1.TabStop = true;
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.Name = "button3";
    this.button3.Click += new EventHandler(this.button3_Click_1);
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.button2;
    this.Controls.Add((Control) this.button3);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.DelButton);
    this.Controls.Add((Control) this.AddButton);
    this.Controls.Add((Control) this.button2);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.ParticipantsView);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ParticipantsEditorForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public string Data
  {
    get => this.Participants.AsString;
    set
    {
      this.Participants.AsString = value;
      this.RefreshParticipantsView();
    }
  }

  private void button3_Click(object sender, EventArgs e)
  {
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Workflow.Design_87"), LocalizationHolder.rm.GetString("Workflow.Design_88"), (IDescriptor) new UsersGroupsDescriptor(), typeof (IDBObjectID), SelectionOptions.Default);
    if (objArray == null || objArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      for (int index = 0; index < objArray.Length; ++index)
      {
        long objectID = (objArray[index] as IDBObjectID).Value;
        QuickObjectInfo objectInfo = session.GetObjectInfo(objectID);
        ParticipantKind Kind = objectInfo.ObjectTypeID == wfConsts.GroupTypeID ? ParticipantKind.Group : ParticipantKind.User;
        ListViewItem listViewItem = this.ParticipantsView.Items.Add(ParticipantsEditorForm._cache.GetUserName(objectInfo.ObjectID));
        listViewItem.Tag = (object) this.Participants.AddParticipant(Kind, objectInfo.ObjectID);
        listViewItem.StateImageIndex = 0;
      }
    }
  }

  private void RefreshParticipantsView()
  {
    this.ParticipantsView.BeginUpdate();
    try
    {
      this.ParticipantsView.Items.Clear();
      for (int index = 0; index < this.Participants.Count; ++index)
      {
        Participant participant = this.Participants[index];
        this.ParticipantsView.Items.Add(ParticipantsEditorForm._cache.GetUserName(participant.ID)).Tag = (object) participant;
      }
      if (this.ParticipantsView.Items.Count <= 0)
        return;
      this.ParticipantsView.Items[0].Selected = true;
    }
    finally
    {
      this.ParticipantsView.EndUpdate();
    }
  }

  private void button3_Click_1(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetObjectCollection(wfConsts.LinksTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      })).Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(int64);
        if (objectInfo.Empty)
          throw new KernelException($"Объект с идентификатором '{int64}' не найден.");
        this.ParticipantsView.Items.Add(objectInfo.ObjectID.ToString());
      }
    }
  }

  private void DelButton_Click(object sender, EventArgs e)
  {
    this.ParticipantsView.BeginUpdate();
    try
    {
      for (int index = 0; index < this.ParticipantsView.SelectedItems.Count; index = index - 1 + 1)
      {
        this.Participants.Remove(this.ParticipantsView.SelectedItems[index].Tag as Participant);
        this.ParticipantsView.Items.Remove(this.ParticipantsView.SelectedItems[index]);
      }
    }
    finally
    {
      this.ParticipantsView.EndUpdate();
    }
  }
}
