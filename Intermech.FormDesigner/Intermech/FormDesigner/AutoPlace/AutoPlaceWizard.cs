// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.AutoPlace.AutoPlaceWizard
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.AutoPlace;

/// <summary>Мастер авторазмещения.</summary>
internal class AutoPlaceWizard : Form
{
  private DesForm _form;
  private IDesignerHost _host;
  private object _hostObj;
  private int _step;
  private Step1 _step1;
  private Step2 _step2;
  private Step3 _step3;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _pnlForms;
  private Panel _pnlButtons;
  private GroupBox _gb;
  private TableLayoutPanel _tlp;
  private Button _btnCancel;
  private Button _btnNext;
  private Button _btnPrev;

  /// <summary>Конструктор.</summary>
  /// <param name="host">Редактор форм</param>
  /// <param name="desForm">Редактируемая форма</param>
  public AutoPlaceWizard(object host, DesForm desForm)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1144);
    this._host = host as IDesignerHost;
    this._hostObj = host;
    this._form = desForm;
    if (this._form == null)
    {
      this._pnlButtons.Enabled = false;
    }
    else
    {
      Step1 step1 = new Step1(this._form.Links, this._btnNext, this._btnPrev);
      step1.Dock = DockStyle.Fill;
      step1.Visible = false;
      this._step1 = step1;
      Step2 step2 = new Step2(this._btnNext, this._btnPrev);
      step2.Dock = DockStyle.Fill;
      step2.Visible = false;
      this._step2 = step2;
      Step3 step3 = new Step3(this._hostObj, this._btnNext, this._btnPrev);
      step3.Dock = DockStyle.Fill;
      step3.Visible = false;
      this._step3 = step3;
      this._pnlForms.Controls.AddRange(new Control[3]
      {
        (Control) this._step1,
        (Control) this._step2,
        (Control) this._step3
      });
      this.NextStep();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnCancel_Click(object sender, EventArgs e) => this.Close();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnNext_Click(object sender, EventArgs e) => this.NextStep();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnPrev_Click(object sender, EventArgs e) => this.PrevStep();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Следующий шаг.</summary>
  private void NextStep()
  {
    switch (++this._step)
    {
      case 1:
        this._step1.UseButtons = true;
        break;
      case 2:
        this._step1.UseButtons = false;
        this._step2.UseButtons = true;
        this._step2.Attributes = this._step1.Attributes;
        break;
      case 3:
        this._step2.UseButtons = false;
        this._step3.UseButtons = true;
        this._btnCancel.Enabled = false;
        this._step3.OriginBetween = this._step2.OriginBetween;
        this._step3.OriginLocation = this._step2.OriginLocation;
        this._step3.AttributeModels = this._step2.AttributeModels;
        break;
      case 4:
        if (this._host.GetService(typeof (ISelectionService)) is ISelectionService service)
        {
          service.SetSelectedComponents((ICollection) null, SelectionTypes.Click);
          service.SetSelectedComponents((ICollection) new object[1]
          {
            (object) this._host.RootComponent
          }, SelectionTypes.Click);
        }
        this._form.Links = this._step1.Links;
        this.Close();
        break;
    }
  }

  /// <summary>Предыдущий шаг.</summary>
  private void PrevStep()
  {
    switch (--this._step)
    {
      case 1:
        this._step2.UseButtons = false;
        this._step1.UseButtons = true;
        break;
      case 2:
        this._step3.UseButtons = false;
        this._step2.UseButtons = true;
        break;
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoPlaceWizard));
    this._pnlForms = new Panel();
    this._pnlButtons = new Panel();
    this._tlp = new TableLayoutPanel();
    this._btnCancel = new Button();
    this._btnNext = new Button();
    this._btnPrev = new Button();
    this._gb = new GroupBox();
    this._pnlButtons.SuspendLayout();
    this._tlp.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._pnlForms, "_pnlForms");
    this._pnlForms.Name = "_pnlForms";
    this._pnlButtons.Controls.Add((Control) this._tlp);
    componentResourceManager.ApplyResources((object) this._pnlButtons, "_pnlButtons");
    this._pnlButtons.Name = "_pnlButtons";
    componentResourceManager.ApplyResources((object) this._tlp, "_tlp");
    this._tlp.Controls.Add((Control) this._btnCancel, 4, 0);
    this._tlp.Controls.Add((Control) this._btnNext, 3, 0);
    this._tlp.Controls.Add((Control) this._btnPrev, 2, 0);
    this._tlp.Name = "_tlp";
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    componentResourceManager.ApplyResources((object) this._btnNext, "_btnNext");
    this._btnNext.Name = "_btnNext";
    this._btnNext.Click += new EventHandler(this.On_btnNext_Click);
    componentResourceManager.ApplyResources((object) this._btnPrev, "_btnPrev");
    this._btnPrev.Name = "_btnPrev";
    this._btnPrev.Click += new EventHandler(this.On_btnPrev_Click);
    componentResourceManager.ApplyResources((object) this._gb, "_gb");
    this._gb.Name = "_gb";
    this._gb.TabStop = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._pnlForms);
    this.Controls.Add((Control) this._gb);
    this.Controls.Add((Control) this._pnlButtons);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AutoPlaceWizard);
    this.ShowInTaskbar = false;
    this._pnlButtons.ResumeLayout(false);
    this._tlp.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
