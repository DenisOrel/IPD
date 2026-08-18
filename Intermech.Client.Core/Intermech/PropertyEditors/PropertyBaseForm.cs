
// Type: Intermech.PropertyEditors.PropertyBaseForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Views.Grid;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for PropertyForm.</summary>
public class PropertyBaseForm : UserControl, IConfigPage
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private CustomFolder folder;
  private bool changed;
  protected Guid instGuid = Guid.Empty;

  public PropertyBaseForm(Guid aInstGuid)
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.instGuid = aInstGuid;
  }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PropertyBaseForm));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (PropertyBaseForm);
    this.ResumeLayout(false);
  }

  public IFolder Folder
  {
    get => (IFolder) this.folder;
    set => this.folder = value as CustomFolder;
  }

  public void DockToPanel(Panel panel)
  {
    this.Parent = (Control) panel;
    this.BringToFront();
    this.Visible = true;
  }

  public void Undock()
  {
    this.Parent = (Control) null;
    this.Visible = false;
  }

  public void SetChangedStatus(bool status) => this.changed = status;

  public bool Changed
  {
    get => this.changed;
    set
    {
      this.changed = value;
      if (this.folder == null)
        return;
      this.folder.InChange = value;
    }
  }

  public virtual PropertyGrid PropertyGrid => (PropertyGrid) null;

  public virtual GridControl GridControl => (GridControl) null;

  public virtual GridView GridView => (GridView) null;

  public virtual TabControl TabControl => (TabControl) null;

  public virtual bool ConstructPages() => true;

  public virtual void DefaultsOnLoad()
  {
  }

  public virtual bool DefaultsOnSave() => true;

  public virtual void DefaultsOnLostFocus(IFolder folder)
  {
  }

  public virtual IBaseTabPage LastTabPage => (IBaseTabPage) null;

  public virtual void OpenTabPage(TabPage tabpage)
  {
  }
}
