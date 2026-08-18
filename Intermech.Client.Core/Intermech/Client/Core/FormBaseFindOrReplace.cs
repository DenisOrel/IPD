
// Type: Intermech.Client.Core.FormBaseFindOrReplace
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary> База диалога для поиска или посика с заменой чего-либо </summary>
public class FormBaseFindOrReplace : FormBaseFind, IFindOrReplaceController
{
  private bool isReplaceMode;
  private bool enableReplace = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TabPage _tabPageFind;
  private TabPage _tabPageReplace;
  protected TabControl _tabControlFindOrReplace;

  public FormBaseFindOrReplace() => this.InitializeComponent();

  /// <summary> Форма была загруженна </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormBaseFindOrReplace_Load(object sender, EventArgs e)
  {
  }

  /// <summary> Был переключён режим просто поиска текста / поиска текста с заменой </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _tabControlFindOrReplace_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.InterfaceObject == null || !(this.InterfaceObject is IFindOrReplaceController))
      return;
    ((IFindOrReplaceController) this.InterfaceObject).IsReplaceMode = this._tabControlFindOrReplace.SelectedIndex == 1;
  }

  /// <summary> Если true, то производиться поиск с заменой, если false, то производиться простой поиск </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public bool IsReplaceMode
  {
    get => this._tabControlFindOrReplace.SelectedIndex == 1;
    set
    {
      this._tabControlFindOrReplace.SelectedIndex = !value || !this.EnableReplace ? 0 : 1;
      this.isReplaceMode = value;
    }
  }

  public bool EnableReplace
  {
    get => this.enableReplace;
    set
    {
      this.enableReplace = value;
      if (!this.enableReplace)
      {
        this._tabControlFindOrReplace.SelectedIndex = 0;
        this._tabControlFindOrReplace.TabPages.Remove(this._tabPageReplace);
      }
      else
      {
        if (!this._tabControlFindOrReplace.TabPages.Contains(this._tabPageReplace))
          this._tabControlFindOrReplace.TabPages.Add(this._tabPageReplace);
        this.IsReplaceMode = this.isReplaceMode;
      }
    }
  }

  /// <summary> </summary>
  protected override void AfterShow()
  {
    base.AfterShow();
    if (this.InterfaceObject == null || !(this.InterfaceObject is IFindOrReplaceController))
      return;
    ((IFindOrReplaceController) this.InterfaceObject).IsReplaceMode = this._tabControlFindOrReplace.SelectedIndex == 1;
  }

  private void _tabControlFindOrReplace_SizeChanged(object sender, EventArgs e)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormBaseFindOrReplace));
    this._tabControlFindOrReplace = new TabControl();
    this._tabPageFind = new TabPage();
    this._tabPageReplace = new TabPage();
    this._tabControlFindOrReplace.SuspendLayout();
    this.SuspendLayout();
    this._tabControlFindOrReplace.AccessibleDescription = (string) null;
    this._tabControlFindOrReplace.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._tabControlFindOrReplace, "_tabControlFindOrReplace");
    this._tabControlFindOrReplace.BackgroundImage = (Image) null;
    this._tabControlFindOrReplace.Controls.Add((Control) this._tabPageFind);
    this._tabControlFindOrReplace.Controls.Add((Control) this._tabPageReplace);
    this._tabControlFindOrReplace.Font = (Font) null;
    this._tabControlFindOrReplace.Name = "_tabControlFindOrReplace";
    this._tabControlFindOrReplace.SelectedIndex = 0;
    this._tabControlFindOrReplace.SelectedIndexChanged += new EventHandler(this._tabControlFindOrReplace_SelectedIndexChanged);
    this._tabControlFindOrReplace.SizeChanged += new EventHandler(this._tabControlFindOrReplace_SizeChanged);
    this._tabPageFind.AccessibleDescription = (string) null;
    this._tabPageFind.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._tabPageFind, "_tabPageFind");
    this._tabPageFind.BackgroundImage = (Image) null;
    this._tabPageFind.Font = (Font) null;
    this._tabPageFind.Name = "_tabPageFind";
    this._tabPageFind.UseVisualStyleBackColor = true;
    this._tabPageReplace.AccessibleDescription = (string) null;
    this._tabPageReplace.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._tabPageReplace, "_tabPageReplace");
    this._tabPageReplace.BackgroundImage = (Image) null;
    this._tabPageReplace.Font = (Font) null;
    this._tabPageReplace.Name = "_tabPageReplace";
    this._tabPageReplace.UseVisualStyleBackColor = true;
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this._tabControlFindOrReplace);
    this.Font = (Font) null;
    this.Name = nameof (FormBaseFindOrReplace);
    this.Load += new EventHandler(this.FormBaseFindOrReplace_Load);
    this._tabControlFindOrReplace.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
