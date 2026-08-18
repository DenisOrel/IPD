// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableWizard.Step3
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.TableWizard.Interfaces;
using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.TableWizard;

[Serializable]
internal class Step3 : UserControl, IImbaseTableStep
{
  private ImbaseTableWizardForm _wizardForm;
  private Dictionary<System.Type, object> _context;
  private IContainer components;
  private Panel _pnlBottom;
  private ObjectPropertyGrid _opg;

  public Step3()
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
  }

  internal bool CommitData { get; set; }

  public ImbaseTableWizardForm WizardForm
  {
    set
    {
      this._wizardForm = value;
      this.CommitData = true;
    }
  }

  public Dictionary<System.Type, object> Context
  {
    get
    {
      this._opg.Save();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._wizardForm.ObjectID, false);
        if (objectActualCopy != null)
        {
          if (this._wizardForm.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableTypeID && this.CommitData)
          {
            if (!this._wizardForm.FinalObjIsTbl)
            {
              try
              {
                objectActualCopy.CommitCreation(false);
                this._wizardForm.ObjectID = objectActualCopy.ObjectID;
                if (this._context.ContainsKey(typeof (Step2)))
                  this._context.Remove(typeof (Step2));
                if (this._context.ContainsKey(typeof (Step1)))
                {
                  if (!(this._context[typeof (Step1)] is Step1Params step1Params))
                    return this._context;
                  step1Params.TableID = objectActualCopy.ObjectID;
                }
              }
              catch (Exception ex)
              {
                throw;
              }
            }
          }
          this._wizardForm.ObjectName = objectActualCopy.Caption;
        }
      }
      return this._context;
    }
    set
    {
      this._context = value;
      Control parent = this._opg.Parent;
      this._opg.Parent = (Control) null;
      this._opg.Load(this._wizardForm.ObjectID, AttributableElements.Object, GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.CheckVisibility, true, typeof (ObjectAllAttributesGridTab));
      this._opg.Parent = parent;
      this._opg.BringToFront();
    }
  }

  public System.Type NextStep
  {
    get
    {
      System.Type nextStep = (System.Type) null;
      if (this._wizardForm.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableTypeID && !this._wizardForm.FinalObjIsTbl)
      {
        this._wizardForm.ObjectTypeID = Intermech.Imbase.Consts.ImbaseTableRefTypeID;
        nextStep = typeof (Step4);
      }
      return nextStep;
    }
  }

  public System.Type PrevStep
  {
    get
    {
      return this._wizardForm.ObjectTypeID != Intermech.Imbase.Consts.ImbaseTableRefTypeID ? typeof (Step2) : typeof (Step4);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Step3));
    this._pnlBottom = new Panel();
    this._opg = new ObjectPropertyGrid();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    this._opg.CommandsActiveLinkColor = SystemColors.ActiveCaption;
    this._opg.CommandsDisabledLinkColor = SystemColors.ControlDark;
    this._opg.CommandsLinkColor = SystemColors.ActiveCaption;
    componentResourceManager.ApplyResources((object) this._opg, "_opg");
    this._opg.InternalMenuEnabled = true;
    this._opg.LockTypeChange = false;
    this._opg.Name = "_opg";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._opg);
    this.Controls.Add((Control) this._pnlBottom);
    this.MinimumSize = new Size(670, 336);
    this.Name = nameof (Step3);
    this.ResumeLayout(false);
  }
}
