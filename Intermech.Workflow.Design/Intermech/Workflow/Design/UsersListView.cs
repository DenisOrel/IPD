// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.UsersListView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces.Workflow;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for UsersListView.</summary>
public class UsersListView : EnhListView, IReadOnlyEnabledControl
{
  private ParticipantList _participants;
  private bool _modified;
  private Button _addButton;
  private Button _delButton;
  private bool _readOnly;
  private long _processID;

  public Button AddButton
  {
    get => this._addButton;
    set
    {
      this._addButton = value;
      if (value == null)
        return;
      value.Click += new EventHandler(this.AddUserButton_Click);
    }
  }

  public Button DelButton
  {
    get => this._delButton;
    set
    {
      this._delButton = value;
      if (value == null)
        return;
      value.Click += new EventHandler(this.DelUserButton_Click);
    }
  }

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (this.AddButton != null)
        this.AddButton.Enabled = !value;
      if (this.DelButton == null)
        return;
      this.DelButton.Enabled = !value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ParticipantList Participants
  {
    get => this._participants;
    set
    {
      this._participants = value;
      this._modified = false;
      this.RefreshParticipants();
    }
  }

  public bool Modified => this._modified;

  private ListViewItem AddToList(Participant p)
  {
    string text = p.DisplayName;
    if (text == "")
      text = $"(ObjectID={(object) p.ID})";
    ListViewItem list = this.Items.Add(text);
    list.Tag = (object) p;
    list.StateImageIndex = -1;
    list.ImageIndex = (int) p.Kind;
    return list;
  }

  public ListViewItem AddParticipant(Participant p)
  {
    if (this._participants.FindEqual(p) != null)
      return (ListViewItem) null;
    this._modified = true;
    this._participants.Add(p);
    return this.AddToList(p);
  }

  public long ProcessID
  {
    get => this._processID;
    set => this._processID = value;
  }

  public void AddParticipants()
  {
    ParticipantList participantList = wfFunx.BrowseForUsers(LocalizationHolder.rm.GetString("Workflow.Design_92"), this._processID);
    if (participantList == null)
      return;
    this.BeginUpdate();
    try
    {
      for (int index = 0; index < participantList.Count; ++index)
        this.AddParticipant(participantList[index]);
    }
    finally
    {
      this.EndUpdate();
      if (this.SelectedItems.Count == 0 && this.Items.Count > 0)
        this.Items[0].Selected = true;
    }
  }

  public void DeleteSelected()
  {
    if (this.SelectedItems.Count <= 0)
      return;
    this.BeginUpdate();
    this.SaveSelectedPos();
    try
    {
      foreach (ListViewItem selectedItem in this.SelectedItems)
      {
        this._participants.Remove((Participant) selectedItem.Tag);
        selectedItem.Remove();
      }
      this._modified = true;
    }
    finally
    {
      this.RestoreSelectedPos();
      this.EndUpdate();
    }
  }

  private void DelUserButton_Click(object sender, EventArgs e) => this.DeleteSelected();

  private void AddUserButton_Click(object sender, EventArgs e) => this.AddParticipants();

  public void RefreshParticipants()
  {
    if (this.SmallImageList == null)
      this.SmallImageList = Holder.UsersImageList;
    this.SaveSelectedPos();
    this.BeginUpdate();
    try
    {
      this.Items.Clear();
      for (int index = 0; index < this.Participants.Count; ++index)
        this.AddToList(this.Participants[index]);
    }
    finally
    {
      this.EndUpdate();
      this.RestoreSelectedPos();
    }
  }
}
