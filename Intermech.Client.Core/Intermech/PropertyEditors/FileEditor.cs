
// Type: Intermech.PropertyEditors.FileEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

public class FileEditor : UITypeEditor
{
  private FileEditor.InternalEditorControl internalEditorControl;

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    if (value == null)
      return value;
    IWindowsFormsEditorService service = (IWindowsFormsEditorService) sp.GetService(typeof (IWindowsFormsEditorService));
    if (this.internalEditorControl == null)
      this.internalEditorControl = new FileEditor.InternalEditorControl((FilePropertyClass) value);
    FileEditor.InternalEditorControl internalEditorControl = this.internalEditorControl;
    service.DropDownControl((Control) internalEditorControl);
    return (object) this.internalEditorControl.GetData();
  }

  private class InternalEditorControl : UserControl
  {
    private TabControl tabControl;
    private TabPage tabPage;
    private FileEditorForm fileEditorForm;

    public InternalEditorControl(FilePropertyClass fpc)
    {
      this.tabControl = new TabControl();
      this.tabControl.Dock = DockStyle.Fill;
      this.tabPage = new TabPage(LocalizationHolder.rm.GetString("Client.Core_431"));
      this.fileEditorForm = new FileEditorForm();
      this.fileEditorForm.Parent = (Control) this.tabPage;
      this.SuspendLayout();
      this.tabControl.Controls.Add((Control) this.tabPage);
      this.Controls.Add((Control) this.tabControl);
      this.ResumeLayout(false);
      this.SetData(fpc);
      this.fileEditorForm.Show();
    }

    public void SetData(FilePropertyClass fpc) => this.fileEditorForm.SetFormData(fpc);

    public FilePropertyClass GetData() => this.fileEditorForm.GetFormData();
  }
}
