
// Type: Intermech.PropertyEditors.TabPageForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for TabPageForm.</summary>
public class TabPageForm : UserControl, ITabPageForm
{
  /// <summary>Required designer variable.</summary>
  protected CustomFolder _folder;
  protected Guid instGuid = Guid.Empty;
  private System.ComponentModel.Container components;

  protected TabPageForm()
    : this(Guid.Empty)
  {
  }

  public TabPageForm(Guid aInstGuid)
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

  public void SetParent(Panel panel)
  {
    if (panel.Controls.IndexOf((Control) this) == -1)
      panel.Controls.Add((Control) this);
    if (panel.Controls.GetChildIndex((Control) this) != 0)
      panel.Controls.SetChildIndex((Control) this, 0);
    this.Visible = true;
    for (int index = 1; index < panel.Controls.Count; ++index)
      panel.Controls[index].Visible = false;
    this.BringToFront();
  }

  protected override void SetBoundsCore(
    int x,
    int y,
    int width,
    int height,
    BoundsSpecified specified)
  {
    base.SetBoundsCore(x, y, width, height, specified);
  }

  protected override void SetClientSizeCore(int x, int y) => base.SetClientSizeCore(x, y);

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TabPageForm));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (TabPageForm);
    this.Tag = (object) "       ";
    this.ResumeLayout(false);
  }

  public virtual void FillForm(IFolder folder)
  {
  }

  public virtual bool SaveForm(IFolder folder) => true;

  public virtual void FormLostFocus(IFolder folder)
  {
  }

  public virtual bool RefreshAfterCanceling() => true;

  /// <summary>вернуть id топика в хелпе</summary>
  public virtual string HelpTopicID => "1003";
}
