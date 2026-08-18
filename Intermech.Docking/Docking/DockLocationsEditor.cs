
// Type: Intermech.Docking.DockLocationsEditor
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Docking;

internal class DockLocationsEditor : UITypeEditor
{
  private DockLocationsEditor.DockLicationsEditorControl _ui;

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    IWindowsFormsEditorService service = (IWindowsFormsEditorService) sp.GetService(typeof (IWindowsFormsEditorService));
    if (this._ui == null)
      this._ui = new DockLocationsEditor.DockLicationsEditorControl();
    this._ui.SetStates(service, (DockLocation) value);
    service.DropDownControl((Control) this._ui);
    return (object) this._ui.States;
  }

  private class DockLicationsEditorControl : UserControl
  {
    private IWindowsFormsEditorService _edSvc;
    private CheckBox checkBoxFloat;
    private CheckBox checkBoxDockLeft;
    private CheckBox checkBoxDockRight;
    private CheckBox checkBoxDockTop;
    private CheckBox checkBoxDockBottom;
    private CheckBox checkBoxDockFill;
    private DockLocation _oldStates;

    public DockLocation States
    {
      get
      {
        DockLocation dockLocation = DockLocation.Unknown;
        if (this.checkBoxFloat.Checked)
          dockLocation |= DockLocation.Float;
        if (this.checkBoxDockLeft.Checked)
          dockLocation |= DockLocation.Left;
        if (this.checkBoxDockRight.Checked)
          dockLocation |= DockLocation.Right;
        if (this.checkBoxDockTop.Checked)
          dockLocation |= DockLocation.Top;
        if (this.checkBoxDockBottom.Checked)
          dockLocation |= DockLocation.Bottom;
        if (this.checkBoxDockFill.Checked)
          dockLocation |= DockLocation.Document;
        return dockLocation == DockLocation.Unknown ? this._oldStates : dockLocation;
      }
    }

    public DockLicationsEditorControl()
    {
      this.checkBoxFloat = new CheckBox();
      this.checkBoxDockLeft = new CheckBox();
      this.checkBoxDockRight = new CheckBox();
      this.checkBoxDockTop = new CheckBox();
      this.checkBoxDockBottom = new CheckBox();
      this.checkBoxDockFill = new CheckBox();
      this.SuspendLayout();
      this.checkBoxFloat.Appearance = Appearance.Button;
      this.checkBoxFloat.Dock = DockStyle.Top;
      this.checkBoxFloat.Height = 24;
      this.checkBoxFloat.Text = "(Float)";
      this.checkBoxFloat.TextAlign = ContentAlignment.MiddleCenter;
      this.checkBoxFloat.FlatStyle = FlatStyle.System;
      this.checkBoxDockLeft.Appearance = Appearance.Button;
      this.checkBoxDockLeft.Dock = DockStyle.Left;
      this.checkBoxDockLeft.Width = 24;
      this.checkBoxDockLeft.FlatStyle = FlatStyle.System;
      this.checkBoxDockRight.Appearance = Appearance.Button;
      this.checkBoxDockRight.Dock = DockStyle.Right;
      this.checkBoxDockRight.Width = 24;
      this.checkBoxDockRight.FlatStyle = FlatStyle.System;
      this.checkBoxDockTop.Appearance = Appearance.Button;
      this.checkBoxDockTop.Dock = DockStyle.Top;
      this.checkBoxDockTop.Height = 24;
      this.checkBoxDockTop.FlatStyle = FlatStyle.System;
      this.checkBoxDockBottom.Appearance = Appearance.Button;
      this.checkBoxDockBottom.Dock = DockStyle.Bottom;
      this.checkBoxDockBottom.Height = 24;
      this.checkBoxDockBottom.FlatStyle = FlatStyle.System;
      this.checkBoxDockFill.Appearance = Appearance.Button;
      this.checkBoxDockFill.Dock = DockStyle.Fill;
      this.checkBoxDockFill.FlatStyle = FlatStyle.System;
      this.Controls.AddRange(new Control[6]
      {
        (Control) this.checkBoxDockFill,
        (Control) this.checkBoxDockBottom,
        (Control) this.checkBoxDockTop,
        (Control) this.checkBoxDockRight,
        (Control) this.checkBoxDockLeft,
        (Control) this.checkBoxFloat
      });
      this.Size = new Size(160 /*0xA0*/, 144 /*0x90*/);
      this.BackColor = SystemColors.Control;
      this.ResumeLayout(false);
    }

    public void SetStates(IWindowsFormsEditorService edSvc, DockLocation states)
    {
      this._edSvc = edSvc;
      this._oldStates = states;
      if ((states & DockLocation.Left) != DockLocation.Unknown)
        this.checkBoxDockLeft.Checked = true;
      if ((states & DockLocation.Right) != DockLocation.Unknown)
        this.checkBoxDockRight.Checked = true;
      if ((states & DockLocation.Top) != DockLocation.Unknown)
        this.checkBoxDockTop.Checked = true;
      if ((states & DockLocation.Bottom) != DockLocation.Unknown)
        this.checkBoxDockBottom.Checked = true;
      if ((states & DockLocation.Document) != DockLocation.Unknown)
        this.checkBoxDockFill.Checked = true;
      if ((states & DockLocation.Center) != DockLocation.Unknown)
        this.checkBoxDockFill.Checked = true;
      if ((states & DockLocation.Float) == DockLocation.Unknown)
        return;
      this.checkBoxFloat.Checked = true;
    }
  }
}
