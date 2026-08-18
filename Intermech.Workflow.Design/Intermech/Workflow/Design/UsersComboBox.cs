// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.UsersComboBox
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using System;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

[Serializable]
internal class UsersComboBox : 
  ComboBoxEx,
  IMultipleAttributeEditor,
  IBaseDesForm,
  IAttributeEditorModified,
  IValidateBeforeSave
{
  private Guid _srcVariable = Guid.Empty;
  private Guid _dstVariable = Guid.Empty;
  private bool _requiresValue;
  private bool _modifiedOnLoad;
  private IDComboItem _initialSelectedItem;
  private bool _modified;
  private DesForm _desForm;

  public UsersComboBox()
  {
    this.DropDownStyle = ComboBoxStyle.DropDownList;
    this.Sorted = true;
  }

  protected override void OnSelectionChangeCommitted(EventArgs e) => this.Modified = true;

  protected override void OnSelectedIndexChanged(EventArgs e) => this.Modified = true;

  public Guid SrcVariable
  {
    get => this._srcVariable;
    set => this._srcVariable = value;
  }

  public Guid DstVariable
  {
    get => this._dstVariable;
    set => this._dstVariable = value;
  }

  public bool RequiresValue
  {
    get => this._requiresValue;
    set => this._requiresValue = value;
  }

  private void LoadAll(IUserSession session)
  {
    foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(wfConsts.UserTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(wfConsts.AttrExternalUserID, RelationalOperators.NotEqual, (object) true, LogicalOperators.AND, 0, false)
    }, new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.CAPTION
    }, (object[]) null, new SortOrders[1])).Rows)
      this.Items.Add((object) new IDComboItem(row[1].ToString(), Convert.ToInt64(row[0]), Holder.UserImageIndex));
  }

  public void Load()
  {
    this._initialSelectedItem = (IDComboItem) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributable dbAttributable = (IDBAttributable) sessionKeeper.Session.GetObject(this.ObjectID);
      this.ItemHeight = Holder.UsersImageList.ImageSize.Height + 2;
      this.ImageList = Holder.UsersImageList;
      this.BeginUpdate();
      try
      {
        this.Items.Clear();
        Guid guid = this.SrcVariable;
        if (!guid.Equals(Guid.Empty))
        {
          IDBAttribute attributeByGuid = dbAttributable.GetAttributeByGuid(this.SrcVariable);
          if (attributeByGuid != null)
          {
            ParticipantList pl = new ParticipantList(sessionKeeper.Session)
            {
              AsString = attributeByGuid.Value.ToString()
            };
            MiscFunx.ExpandParticipants(dbAttributable, pl);
            foreach (Participant participant in pl)
            {
              if (participant.Kind == ParticipantKind.User)
                this.Items.Add((object) new IDComboItem(participant.DisplayName, participant.ID, Holder.UserImageIndex));
            }
          }
        }
        if (this.Items.Count == 0)
          this.LoadAll(sessionKeeper.Session);
        if (this.Items.Count > 0 && !this.RequiresValue)
          this.Items.Insert(0, (object) new IDComboItem("", 0L, -1));
        guid = this.DstVariable;
        if (!guid.Equals(Guid.Empty))
        {
          IDBAttribute attributeByGuid = dbAttributable.GetAttributeByGuid(this.DstVariable);
          if (attributeByGuid != null)
          {
            ParticipantList pl = new ParticipantList(sessionKeeper.Session)
            {
              AsString = attributeByGuid.Value.ToString()
            };
            MiscFunx.ExpandParticipants(dbAttributable, pl);
            if (pl.Count > 0)
            {
              long id = pl[0].ID;
              foreach (IDComboItem idComboItem in this.Items)
              {
                if (id == idComboItem.ID)
                {
                  this._initialSelectedItem = idComboItem;
                  this.SelectedItem = (object) idComboItem;
                  break;
                }
              }
            }
            if (!this._modifiedOnLoad)
            {
              long num = 0;
              if (this.SelectedIndex != -1)
                num = (this.SelectedItem as IDComboItem).ID;
              if (num == 0L)
              {
                if (pl.Count == 0)
                  goto label_36;
              }
              if (pl.Count == 1)
              {
                if (pl[0].ID == num)
                  goto label_36;
              }
              this._modifiedOnLoad = true;
            }
          }
        }
      }
      finally
      {
        this.EndUpdate();
      }
label_36:
      this._modified = false;
    }
  }

  private long ObjectID
  {
    get
    {
      long elementIdentifier = this._desForm.Info.ElementIdentifier;
      if (this._desForm.PinExchange.ContainsKey(elementIdentifier))
        elementIdentifier = this._desForm.PinExchange[elementIdentifier];
      return elementIdentifier;
    }
  }

  internal long SelectedID => this.SelectedIndex > -1 ? (this.SelectedItem as IDComboItem).ID : -1L;

  public void Save()
  {
    if (!this.DstVariable.Equals(Guid.Empty))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(this.ObjectID, this.DstVariable);
        if (objectAttributeByGuid != null)
        {
          ParticipantList participantList = new ParticipantList();
          if (this.SelectedID > 0L)
            participantList.AddParticipant(ParticipantKind.User, this.SelectedID);
          sessionKeeper.Session.SetObjectAttributesValues(this.ObjectID, true, new AttributeValues[1]
          {
            new AttributeValues(objectAttributeByGuid.AttributeID, (object) participantList.AsString)
          });
        }
      }
    }
    this._modifiedOnLoad = false;
    this._modified = false;
  }

  public bool Modified
  {
    get => this._modified;
    set
    {
      if (this._modified == value)
        return;
      this._modified = value;
      EventHandler modifiedEvent = this.ModifiedEvent;
      if (modifiedEvent == null)
        return;
      modifiedEvent((object) this, new EventArgs());
    }
  }

  public DesForm DesForm
  {
    set => this._desForm = value;
  }

  public event EventHandler ModifiedEvent;

  public void Validate()
  {
    if (!this.Modified && this._modifiedOnLoad)
      this.Modified = this._modifiedOnLoad;
    if (!this.RequiresValue)
      return;
    Control control = (Control) this;
    while (control.Parent != null)
      control = control.Parent;
    switch (control)
    {
      case FormDlg _:
      case NewProcessForm _:
      case FormDesignerView _:
        if (this.SelectedID > 0L)
          break;
        this.Focus();
        throw new NotificationException(LocalizationHolder.rm.GetString("Workflow.Design_UserComboEmpty"));
    }
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (e.KeyCode != Keys.Delete)
      return;
    if (this.RequiresValue && this._initialSelectedItem != null)
    {
      this.SelectedItem = (object) this._initialSelectedItem;
    }
    else
    {
      if (this.SelectedIndex == -1)
        return;
      this.SelectedIndex = -1;
      this.Modified = true;
    }
  }
}
