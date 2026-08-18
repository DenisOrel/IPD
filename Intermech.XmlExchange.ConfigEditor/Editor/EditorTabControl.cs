// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Editor.EditorTabControl
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.XmlExchange.ConfigEditor.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Editor;

public class EditorTabControl : UserControl
{
  private TabControl _tabControl;
  private object _selectNode;
  private bool _editData;
  private bool _readOnly;
  private List<TabPage> _tabPageList;
  private IContainer components;
  private SplitContainer splitTabControl;
  private Button btOk;
  private Button btCancel;
  private TabPage tabPageSettings;
  private TabPage tabPageDefAttributes;
  private TabPage tabPageApplPartTypes;
  private TabPage tabPageAttributes;
  private PageSettings pageSettings;
  private PageDefAttributes pageDefAttributes;
  private PageApplPartTypes pageApplPartTypes;
  private PageAttributes pageAttributes;
  private TabPage tabSearchAttributes;
  private PageSearchAttributes pageSearchAttributes;

  public event EventHandler UpdateTreeView;

  public EditorTabControl() => this.InitializeComponent();

  public void InitializeCustomComponent(bool readOnly)
  {
    this._readOnly = readOnly;
    this.ReadOnly();
    this.pageSettings.InitializeCustomComponent();
    this.pageAttributes.InitializeCustomComponent();
    this.pageDefAttributes.InitializeCustomComponent();
    this.pageApplPartTypes.InitializeCustomComponent();
    this.pageSearchAttributes.InitializeCustomComponent();
    this.pageAttributes.ModifyData += new EventHandler(this.Page_ModifyData);
    this.pageSettings.ModifyData += new EventHandler(this.Page_ModifyData);
    this.pageDefAttributes.ModifyData += new EventHandler(this.Page_ModifyData);
    this.pageApplPartTypes.ModifyData += new EventHandler(this.Page_ModifyData);
    this.pageSearchAttributes.ModifyData += new EventHandler(this.Page_ModifyData);
    this.pageSettings.UpdatePages += new EventHandler(this.Page_UpdatePages);
    this._tabPageList = this._tabControl.TabPages.OfType<TabPage>().ToList<TabPage>();
  }

  private void Page_ModifyData(object sender, EventArgs e)
  {
    this.EditData = sender.CastToType<IPageConfigEditor>().EditData;
  }

  private void Page_UpdatePages(object sender, EventArgs e) => this.SelectNode(this._selectNode);

  internal void SelectNode(object selectNode)
  {
    this._selectNode = selectNode;
    List<System.Type> editors = PageSettingEditors.GetEditors(this._selectNode);
    for (int index = 0; index < this._tabPageList.Count; ++index)
      this.TabPageView(this._tabPageList[index], editors);
  }

  private void TabPageView(TabPage tabPage, List<System.Type> pagesViewList)
  {
    if (!(tabPage.Controls.OfType<Control>().First<Control>((Func<Control, bool>) (a => a is IPageConfigEditor)) is IPageConfigEditor pageConfigEditor))
      return;
    if (this._tabControl.Controls.Contains((Control) tabPage))
    {
      if (!pagesViewList.Contains(pageConfigEditor.GetType()))
        this._tabControl.Controls.Remove((Control) tabPage);
    }
    else if (pagesViewList.Contains(pageConfigEditor.GetType()))
      this._tabControl.Controls.Add((Control) tabPage);
    if (!pagesViewList.Contains(pageConfigEditor.GetType()))
      return;
    this.PageLoadData(tabPage);
  }

  internal bool EditData
  {
    get => this._editData;
    set
    {
      if (this._readOnly)
        return;
      this._editData = value;
      this.btOk.Enabled = value;
      this.btCancel.Enabled = value;
    }
  }

  private void PageLoadData(TabPage tabPage)
  {
    if (!(tabPage.Controls.OfType<Control>().First<Control>((Func<Control, bool>) (a => a is IPageConfigEditor)) is IPageConfigEditor pageConfigEditor) || this._selectNode == null)
      return;
    pageConfigEditor.LoadData(this._selectNode, this._readOnly);
  }

  private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
  {
    if (e.TabPage == null)
      return;
    this.PageLoadData(e.TabPage);
    this.EditData = false;
  }

  private void tabControl_Deselecting(object sender, TabControlCancelEventArgs e)
  {
    this.SelectionChanged();
  }

  internal void SelectionChanged()
  {
    if (!this.EditData)
      return;
    this.SaveValues(MessageBox.Show("Сохранить изменения?", "Настройки не сохранены", MessageBoxButtons.OKCancel) == DialogResult.OK, false);
    this.EditData = false;
  }

  private void btOk_Click(object sender, EventArgs e) => this.SaveValues(true, true);

  private void btCancel_Click(object sender, EventArgs e) => this.SaveValues(false, true);

  internal void SaveValues(bool save, bool refresh)
  {
    if (this._tabControl.SelectedTab != null && this._tabControl.SelectedTab.Controls.OfType<Control>().First<Control>((Func<Control, bool>) (a => a is IPageConfigEditor)) is IPageConfigEditor pageConfigEditor)
      pageConfigEditor.SaveData(save, refresh);
    this.EditData = false;
    if (!save)
      return;
    EventHandler updateTreeView = this.UpdateTreeView;
    if (updateTreeView == null)
      return;
    updateTreeView((object) this, (EventArgs) null);
  }

  private void ReadOnly()
  {
    if (!this._readOnly)
      return;
    this.btCancel.Enabled = false;
    this.btOk.Enabled = false;
    this._tabControl.Deselecting -= new TabControlCancelEventHandler(this.tabControl_Deselecting);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.splitTabControl = new SplitContainer();
    this._tabControl = new TabControl();
    this.tabPageSettings = new TabPage();
    this.pageSettings = new PageSettings();
    this.tabPageAttributes = new TabPage();
    this.pageAttributes = new PageAttributes();
    this.tabPageDefAttributes = new TabPage();
    this.pageDefAttributes = new PageDefAttributes();
    this.tabPageApplPartTypes = new TabPage();
    this.pageApplPartTypes = new PageApplPartTypes();
    this.btOk = new Button();
    this.btCancel = new Button();
    this.tabSearchAttributes = new TabPage();
    this.pageSearchAttributes = new PageSearchAttributes();
    this.splitTabControl.BeginInit();
    this.splitTabControl.Panel1.SuspendLayout();
    this.splitTabControl.Panel2.SuspendLayout();
    this.splitTabControl.SuspendLayout();
    this._tabControl.SuspendLayout();
    this.tabPageSettings.SuspendLayout();
    this.tabPageAttributes.SuspendLayout();
    this.tabPageDefAttributes.SuspendLayout();
    this.tabPageApplPartTypes.SuspendLayout();
    this.tabSearchAttributes.SuspendLayout();
    this.SuspendLayout();
    this.splitTabControl.Dock = DockStyle.Fill;
    this.splitTabControl.FixedPanel = FixedPanel.Panel2;
    this.splitTabControl.IsSplitterFixed = true;
    this.splitTabControl.Location = new Point(0, 0);
    this.splitTabControl.Name = "splitTabControl";
    this.splitTabControl.Orientation = Orientation.Horizontal;
    this.splitTabControl.Panel1.AllowDrop = true;
    this.splitTabControl.Panel1.Controls.Add((Control) this._tabControl);
    this.splitTabControl.Panel1.RightToLeft = RightToLeft.Yes;
    this.splitTabControl.Panel2.Controls.Add((Control) this.btOk);
    this.splitTabControl.Panel2.Controls.Add((Control) this.btCancel);
    this.splitTabControl.Panel2.RightToLeft = RightToLeft.Yes;
    this.splitTabControl.RightToLeft = RightToLeft.Yes;
    this.splitTabControl.Size = new Size(909, 567);
    this.splitTabControl.SplitterDistance = 520;
    this.splitTabControl.TabIndex = 4;
    this._tabControl.Controls.Add((Control) this.tabPageSettings);
    this._tabControl.Controls.Add((Control) this.tabPageAttributes);
    this._tabControl.Controls.Add((Control) this.tabPageDefAttributes);
    this._tabControl.Controls.Add((Control) this.tabPageApplPartTypes);
    this._tabControl.Controls.Add((Control) this.tabSearchAttributes);
    this._tabControl.Dock = DockStyle.Fill;
    this._tabControl.Location = new Point(0, 0);
    this._tabControl.Name = "_tabControl";
    this._tabControl.SelectedIndex = 0;
    this._tabControl.Size = new Size(909, 520);
    this._tabControl.TabIndex = 3;
    this._tabControl.Selecting += new TabControlCancelEventHandler(this.tabControl_Selecting);
    this._tabControl.Deselecting += new TabControlCancelEventHandler(this.tabControl_Deselecting);
    this.tabPageSettings.Controls.Add((Control) this.pageSettings);
    this.tabPageSettings.Location = new Point(4, 22);
    this.tabPageSettings.Name = "tabPageSettings";
    this.tabPageSettings.Padding = new Padding(3);
    this.tabPageSettings.Size = new Size(901, 494);
    this.tabPageSettings.TabIndex = 0;
    this.tabPageSettings.Text = "Настройки";
    this.tabPageSettings.UseVisualStyleBackColor = true;
    this.pageSettings.AutoScroll = true;
    this.pageSettings.Dock = DockStyle.Fill;
    this.pageSettings.Location = new Point(3, 3);
    this.pageSettings.Name = "pageSettings";
    this.pageSettings.Size = new Size(895, 488);
    this.pageSettings.TabIndex = 0;
    this.tabPageAttributes.Controls.Add((Control) this.pageAttributes);
    this.tabPageAttributes.Location = new Point(4, 22);
    this.tabPageAttributes.Name = "tabPageAttributes";
    this.tabPageAttributes.Padding = new Padding(3);
    this.tabPageAttributes.Size = new Size(901, 494);
    this.tabPageAttributes.TabIndex = 1;
    this.tabPageAttributes.Text = "Атрибуты";
    this.tabPageAttributes.UseVisualStyleBackColor = true;
    this.pageAttributes.Dock = DockStyle.Fill;
    this.pageAttributes.Location = new Point(3, 3);
    this.pageAttributes.Name = "pageAttributes";
    this.pageAttributes.Size = new Size(895, 488);
    this.pageAttributes.TabIndex = 0;
    this.tabPageDefAttributes.Controls.Add((Control) this.pageDefAttributes);
    this.tabPageDefAttributes.Location = new Point(4, 22);
    this.tabPageDefAttributes.Name = "tabPageDefAttributes";
    this.tabPageDefAttributes.Padding = new Padding(3);
    this.tabPageDefAttributes.Size = new Size(901, 494);
    this.tabPageDefAttributes.TabIndex = 2;
    this.tabPageDefAttributes.Text = "Атрибуты по умолчанию";
    this.tabPageDefAttributes.UseVisualStyleBackColor = true;
    this.pageDefAttributes.Dock = DockStyle.Fill;
    this.pageDefAttributes.Location = new Point(3, 3);
    this.pageDefAttributes.Name = "pageDefAttributes";
    this.pageDefAttributes.Size = new Size(895, 488);
    this.pageDefAttributes.TabIndex = 0;
    this.tabPageApplPartTypes.Controls.Add((Control) this.pageApplPartTypes);
    this.tabPageApplPartTypes.Location = new Point(4, 22);
    this.tabPageApplPartTypes.Name = "tabPageApplPartTypes";
    this.tabPageApplPartTypes.Padding = new Padding(3);
    this.tabPageApplPartTypes.Size = new Size(901, 494);
    this.tabPageApplPartTypes.TabIndex = 3;
    this.tabPageApplPartTypes.Text = "Дочерние типы";
    this.tabPageApplPartTypes.UseVisualStyleBackColor = true;
    this.pageApplPartTypes.Dock = DockStyle.Fill;
    this.pageApplPartTypes.Location = new Point(3, 3);
    this.pageApplPartTypes.Name = "pageApplPartTypes";
    this.pageApplPartTypes.Size = new Size(895, 488);
    this.pageApplPartTypes.TabIndex = 0;
    this.btOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOk.Enabled = false;
    this.btOk.Location = new Point(746, 8);
    this.btOk.Name = "btOk";
    this.btOk.Size = new Size(75, 23);
    this.btOk.TabIndex = 4;
    this.btOk.Text = "Ок";
    this.btOk.UseVisualStyleBackColor = true;
    this.btOk.Click += new EventHandler(this.btOk_Click);
    this.btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btCancel.Enabled = false;
    this.btCancel.Location = new Point(827, 8);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(75, 23);
    this.btCancel.TabIndex = 5;
    this.btCancel.Text = "Отмена";
    this.btCancel.UseVisualStyleBackColor = true;
    this.btCancel.Click += new EventHandler(this.btCancel_Click);
    this.tabSearchAttributes.Controls.Add((Control) this.pageSearchAttributes);
    this.tabSearchAttributes.Location = new Point(4, 22);
    this.tabSearchAttributes.Name = "tabSearchAttributes";
    this.tabSearchAttributes.Padding = new Padding(3);
    this.tabSearchAttributes.Size = new Size(901, 494);
    this.tabSearchAttributes.TabIndex = 4;
    this.tabSearchAttributes.Text = "Атрибуты поиска";
    this.tabSearchAttributes.UseVisualStyleBackColor = true;
    this.pageSearchAttributes.Dock = DockStyle.Fill;
    this.pageSearchAttributes.Location = new Point(3, 3);
    this.pageSearchAttributes.Name = "pageSearchAttributes";
    this.pageSearchAttributes.Size = new Size(895, 488);
    this.pageSearchAttributes.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.BackgroundImageLayout = ImageLayout.Center;
    this.Controls.Add((Control) this.splitTabControl);
    this.Name = nameof (EditorTabControl);
    this.Size = new Size(909, 567);
    this.splitTabControl.Panel1.ResumeLayout(false);
    this.splitTabControl.Panel2.ResumeLayout(false);
    this.splitTabControl.EndInit();
    this.splitTabControl.ResumeLayout(false);
    this._tabControl.ResumeLayout(false);
    this.tabPageSettings.ResumeLayout(false);
    this.tabPageAttributes.ResumeLayout(false);
    this.tabPageDefAttributes.ResumeLayout(false);
    this.tabPageApplPartTypes.ResumeLayout(false);
    this.tabSearchAttributes.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
