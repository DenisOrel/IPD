// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ImbaseViewForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Docking;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class ImbaseViewForm : Form
{
  private Panel pnlTop;
  private Panel pnlBottom;
  private Button btnClose;
  private static ImbaseViewForm[] _viewForms = new ImbaseViewForm[4];
  private ImbaseViewForm.FormType _formType;
  private Form _modalForm;
  private IImbaseView _view;

  internal static ImbaseViewForm FindOrCreateViewForm(
    ImbaseViewForm.FormType formType,
    IImbaseView view,
    Icon icon)
  {
    ImbaseViewForm viewForm = ImbaseViewForm._viewForms[(int) formType];
    if (viewForm != null)
      return viewForm;
    Form modalForm = (Form) null;
    foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
    {
      if (openForm.Modal && openForm.GetType().FullName.Contains("SelectionWindow"))
        modalForm = openForm;
    }
    ImbaseViewForm imbaseViewForm = new ImbaseViewForm(view, modalForm);
    imbaseViewForm.ShowIcon = true;
    imbaseViewForm.Icon = icon;
    imbaseViewForm._formType = formType;
    ImbaseViewForm orCreateViewForm = imbaseViewForm;
    ImbaseViewForm._viewForms[(int) formType] = orCreateViewForm;
    return orCreateViewForm;
  }

  public ImbaseViewForm(IImbaseView view, Form modalForm = null)
  {
    this.InitializeComponent();
    DockControl dockControl = view as DockControl;
    this.Bounds = dockControl.FloatingBounds;
    this.Text = dockControl.Text;
    dockControl.Dock = DockStyle.Fill;
    this.pnlTop.Controls.Add((Control) dockControl);
    this._modalForm = modalForm;
    this._view = dockControl as IImbaseView;
    this.Shown += new EventHandler(this._view.FirstShown);
    this.FormClosing += new FormClosingEventHandler(this.ImbaseViewForm_FormClosing);
    this.TopMost = true;
    if (modalForm == null)
      return;
    this._modalForm.VisibleChanged += new EventHandler(this.ModalForm_VisibleChanged);
  }

  private void ImbaseViewForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    this._view?.ViewClosing(sender, new CancelEventArgs(e.Cancel));
  }

  private void ModalForm_VisibleChanged(object sender, EventArgs e)
  {
    if (this._modalForm == null || this._modalForm.Visible)
      return;
    this.Dispose();
  }

  protected override void Dispose(bool disposing)
  {
    if (this._modalForm != null)
      this._modalForm.VisibleChanged -= new EventHandler(this.ModalForm_VisibleChanged);
    this.Shown -= new EventHandler(this._view.FirstShown);
    (this._view as DockControl).Dispose();
    this._view = (IImbaseView) null;
    this._modalForm = (Form) null;
    base.Dispose(disposing);
    ImbaseViewForm._viewForms[(int) this._formType] = (ImbaseViewForm) null;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseViewForm));
    this.pnlBottom = new Panel();
    this.btnClose = new Button();
    this.pnlTop = new Panel();
    this.pnlBottom.SuspendLayout();
    this.SuspendLayout();
    this.pnlBottom.Controls.Add((Control) this.btnClose);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    componentResourceManager.ApplyResources((object) this.btnClose, "btnClose");
    this.btnClose.DialogResult = DialogResult.Cancel;
    this.btnClose.Name = "btnClose";
    this.btnClose.UseVisualStyleBackColor = true;
    this.btnClose.Click += new EventHandler(this.btnClose_Click);
    componentResourceManager.ApplyResources((object) this.pnlTop, "pnlTop");
    this.pnlTop.Name = "pnlTop";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnClose;
    this.Controls.Add((Control) this.pnlTop);
    this.Controls.Add((Control) this.pnlBottom);
    this.DoubleBuffered = true;
    this.Name = nameof (ImbaseViewForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void btnClose_Click(object sender, EventArgs e) => this.Close();

  public enum FormType
  {
    FindInTables,
    FindByIndex,
    FindByImage,
    FindByName,
    LAST,
  }
}
