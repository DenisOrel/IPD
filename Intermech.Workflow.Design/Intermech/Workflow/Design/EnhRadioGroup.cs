// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.EnhRadioGroup
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using System;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class EnhRadioGroup : 
  RadioGroup,
  IMultipleAttributeEditor,
  IBaseDesForm,
  IAttributeEditorModified
{
  private Guid _srcVariable = Guid.Empty;
  private Guid _dstVariable = Guid.Empty;
  private DesForm _desForm;
  private bool _modified;

  protected override void OnSelectedIndexChanged()
  {
    base.OnSelectedIndexChanged();
    this.Modified = true;
  }

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

  public void Load()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.BeginUpdate();
      try
      {
        this.Clear();
        Guid guid = this.SrcVariable;
        if (!guid.Equals(Guid.Empty))
        {
          IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(this.ObjectID, this.SrcVariable);
          if (objectAttributeByGuid != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) objectAttributeByGuid.AttributeType.GetPossibleValues().Rows)
            {
              string str = row[1].ToString();
              RadioButton radioButton = new RadioButton();
              radioButton.Text = str;
              radioButton.Parent = (Control) this;
            }
          }
        }
        guid = this.DstVariable;
        if (!guid.Equals(Guid.Empty))
        {
          IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(this.ObjectID, this.DstVariable);
          if (objectAttributeByGuid != null)
          {
            object obj = objectAttributeByGuid.Value;
            try
            {
              this.SelectedIndex = Convert.ToInt32(obj) - 1;
            }
            catch
            {
            }
          }
        }
      }
      finally
      {
        this.EndUpdate();
      }
      this._modified = false;
    }
  }

  public void Save()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.DstVariable.Equals(Guid.Empty) || this.SelectedIndex <= -1)
        return;
      IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(this.ObjectID, this.DstVariable);
      if (objectAttributeByGuid == null)
        return;
      sessionKeeper.Session.SetObjectAttributesValues(this.ObjectID, false, new AttributeValues[1]
      {
        new AttributeValues(objectAttributeByGuid.AttributeID, (object) (this.SelectedIndex + 1))
      });
    }
  }

  public DesForm DesForm
  {
    set => this._desForm = value;
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

  public event EventHandler ModifiedEvent;
}
