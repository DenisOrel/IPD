// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.TabPageCollectionEditorForm
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// 
/// </summary>
public class TabPageCollectionEditorForm : Form
{
  /// <summary>Коллекция закладок</summary>
  private TabControl.TabPageCollection _pages;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _pnlBottom;
  private Button _btnApply;
  private Panel _pnlLeft;
  private PropertyGrid _pg;
  /// <summary>
  /// 
  /// </summary>
  public ListBox _lb;
  private Button _btnUp;
  private Button _btnDown;
  private Button _btnDel;
  private Button _btnAdd;
  /// <summary>
  /// 
  /// </summary>
  protected ImageList _img;

  /// <summary>Конструктор.</summary>
  /// <param name="pages">Коллекция закладок</param>
  public TabPageCollectionEditorForm(TabControl.TabPageCollection pages)
  {
    this.InitializeComponent();
    this._pages = pages;
  }

  /// <summary>Добавление закладки.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnAdd_Click(object sender, EventArgs e)
  {
    TabPageCollectionEditorForm.ListBoxItem listBoxItem = new TabPageCollectionEditorForm.ListBoxItem((TabPage) null);
    this._lb.BeginUpdate();
    try
    {
      this._lb.SelectedIndex = this._lb.Items.Add((object) listBoxItem);
      this._pages.Add(listBoxItem.Page);
    }
    finally
    {
      this._lb.EndUpdate();
    }
  }

  /// <summary>Удаление закладки.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnDel_Click(object sender, EventArgs e)
  {
    if (this._lb.SelectedItem != null)
    {
      int selectedIndex = this._lb.SelectedIndex;
      TabPageCollectionEditorForm.ListBoxItem selectedItem = this._lb.SelectedItem as TabPageCollectionEditorForm.ListBoxItem;
      this._lb.BeginUpdate();
      try
      {
        this._lb.Items.Remove((object) selectedItem);
        new TabPageWrapper(selectedItem.Page).Text = "Remove";
        this._pages.Remove(selectedItem.Page);
      }
      finally
      {
        this._lb.EndUpdate();
      }
      this._lb.SelectedIndex = selectedIndex < this._lb.Items.Count ? selectedIndex : (this._lb.Items.Count > 0 ? this._lb.Items.Count - 1 : -1);
    }
    if (this._lb.Items.Count != 0)
      return;
    this.On_btnAdd_Click((object) this._btnAdd, e);
  }

  /// <summary>Перемещение закладки вверх.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnUpDown_Click(object sender, EventArgs e)
  {
    int index = this._lb.SelectedIndex + Convert.ToInt32((sender as Button).Tag);
    TabPageCollectionEditorForm.ListBoxItem selectedItem = this._lb.SelectedItem as TabPageCollectionEditorForm.ListBoxItem;
    string text = selectedItem.Page.Text;
    this._lb.BeginUpdate();
    try
    {
      new TabPageWrapper(selectedItem.Page).Text = "tmp remove";
      this._pages.Remove(selectedItem.Page);
      this._lb.Items.Remove((object) selectedItem);
      selectedItem.Page.Text = text;
      this._pages.Insert(index, selectedItem.Page);
      this._lb.Items.Insert(index, (object) selectedItem);
      this._lb.SelectedIndex = index;
    }
    finally
    {
      this._lb.EndUpdate();
    }
  }

  /// <summary>Изменение выделенной закладки.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lb_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._lb.SelectedItem != null)
    {
      this._pg.SelectedObject = (object) new ClassWrapperForPropertyGrid((object) new TabPageWrapper((this._lb.SelectedItem as TabPageCollectionEditorForm.ListBoxItem).Page));
      this._btnUp.Enabled = this._lb.SelectedIndex > 0;
      this._btnDown.Enabled = this._lb.SelectedIndex < this._lb.Items.Count - 1;
    }
    else
    {
      this._pg.SelectedObject = (object) null;
      this._btnUp.Enabled = this._btnDown.Enabled = false;
    }
  }

  /// <summary>Измение свойства закладки.</summary>
  /// <param name="s"></param>
  /// <param name="e"></param>
  private void On_pg_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    if (!(e.ChangedItem.PropertyDescriptor.Name == "Text"))
      return;
    int selectedIndex = this._lb.SelectedIndex;
    TabPageCollectionEditorForm.ListBoxItem selectedItem = this._lb.SelectedItem as TabPageCollectionEditorForm.ListBoxItem;
    this._lb.BeginUpdate();
    try
    {
      this._lb.Items.Remove((object) selectedItem);
      this._lb.Items.Insert(selectedIndex, (object) selectedItem);
      this._lb.SelectedIndex = selectedIndex;
    }
    finally
    {
      this._lb.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
    foreach (TabPage page in this._pages)
      this._lb.Items.Add((object) new TabPageCollectionEditorForm.ListBoxItem(page));
    if (this._lb.Items.Count <= 0)
      return;
    this._lb.SelectedIndex = 0;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TabPageCollectionEditorForm));
    this._pnlBottom = new Panel();
    this._btnApply = new Button();
    this._pnlLeft = new Panel();
    this._btnDel = new Button();
    this._btnAdd = new Button();
    this._btnUp = new Button();
    this._img = new ImageList(this.components);
    this._btnDown = new Button();
    this._lb = new ListBox();
    this._pg = new PropertyGrid();
    this._pnlBottom.SuspendLayout();
    this._pnlLeft.SuspendLayout();
    this.SuspendLayout();
    this._pnlBottom.Controls.Add((Control) this._btnApply);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnApply, "_btnApply");
    this._btnApply.DialogResult = DialogResult.OK;
    this._btnApply.Name = "_btnApply";
    this._btnApply.UseVisualStyleBackColor = true;
    this._pnlLeft.Controls.Add((Control) this._btnDel);
    this._pnlLeft.Controls.Add((Control) this._btnAdd);
    this._pnlLeft.Controls.Add((Control) this._btnUp);
    this._pnlLeft.Controls.Add((Control) this._btnDown);
    this._pnlLeft.Controls.Add((Control) this._lb);
    componentResourceManager.ApplyResources((object) this._pnlLeft, "_pnlLeft");
    this._pnlLeft.Name = "_pnlLeft";
    componentResourceManager.ApplyResources((object) this._btnDel, "_btnDel");
    this._btnDel.Name = "_btnDel";
    this._btnDel.UseVisualStyleBackColor = true;
    this._btnDel.Click += new EventHandler(this.On_btnDel_Click);
    componentResourceManager.ApplyResources((object) this._btnAdd, "_btnAdd");
    this._btnAdd.Name = "_btnAdd";
    this._btnAdd.Click += new EventHandler(this.On_btnAdd_Click);
    componentResourceManager.ApplyResources((object) this._btnUp, "_btnUp");
    this._btnUp.ImageList = this._img;
    this._btnUp.Name = "_btnUp";
    this._btnUp.Tag = (object) "-1";
    this._btnUp.UseVisualStyleBackColor = true;
    this._btnUp.Click += new EventHandler(this.On_btnUpDown_Click);
    this._img.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_img.ImageStream");
    this._img.TransparentColor = Color.Transparent;
    this._img.Images.SetKeyName(0, "arrow_up_blue.ico");
    this._img.Images.SetKeyName(1, "arrow_down_blue.ico");
    componentResourceManager.ApplyResources((object) this._btnDown, "_btnDown");
    this._btnDown.ImageList = this._img;
    this._btnDown.Name = "_btnDown";
    this._btnDown.Tag = (object) "1";
    this._btnDown.UseVisualStyleBackColor = true;
    this._btnDown.Click += new EventHandler(this.On_btnUpDown_Click);
    componentResourceManager.ApplyResources((object) this._lb, "_lb");
    this._lb.Name = "_lb";
    this._lb.SelectedIndexChanged += new EventHandler(this.On_lb_SelectedIndexChanged);
    this._pg.CategoryForeColor = SystemColors.InactiveCaptionText;
    componentResourceManager.ApplyResources((object) this._pg, "_pg");
    this._pg.Name = "_pg";
    this._pg.PropertySort = PropertySort.Alphabetical;
    this._pg.PropertyValueChanged += new PropertyValueChangedEventHandler(this.On_pg_PropertyValueChanged);
    this.AcceptButton = (IButtonControl) this._btnApply;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._pg);
    this.Controls.Add((Control) this._pnlLeft);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (TabPageCollectionEditorForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this._pnlBottom.ResumeLayout(false);
    this._pnlLeft.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// 
  /// </summary>
  private class ListBoxItem
  {
    /// <summary>
    /// 
    /// </summary>
    public TabPage Page { get; set; }

    /// <summary>Конструктор.</summary>
    /// <param name="page">Закладка</param>
    public ListBoxItem(TabPage page)
    {
      this.Page = page ?? new TabPage(LocalizationHolder.rm.GetString("FormDesigner_103"));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj) => base.Equals(obj);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => base.GetHashCode();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.Page.Text;
  }
}
