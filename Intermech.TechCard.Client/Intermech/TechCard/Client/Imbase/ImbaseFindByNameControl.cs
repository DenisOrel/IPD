// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Imbase.ImbaseFindByNameControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Docking;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.TechCard.Client.Resources;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Imbase;

/// <summary>
/// Контрол поиска в каталоге / справочнике по наименованию
/// </summary>
/// <remarks>
/// Для поддержки возможности выбора / check найденных элементов вынуждены
/// перекрыть стандартный контрол поиска Imbase
/// </remarks>
public class ImbaseFindByNameControl : FindByNameView
{
  /// <summary>
  /// 
  /// </summary>
  public const int cnt_img_Unchecked = 0;
  /// <summary>
  /// 
  /// </summary>
  public const int cnt_img_Checked = 1;
  /// <summary>
  /// 
  /// </summary>
  public const int cnt_img_GrayCheck = 2;
  /// <summary>
  /// 
  /// </summary>
  public const int cnt_img_GrayUnCheck = 3;
  /// <summary>Флаг для multiSelect</summary>
  private bool _multiSelect;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ImageList imageList;
  private ContextMenuStrip cmsResult;
  private ToolStripMenuItem tsmiResSelectAll;
  private ToolStripMenuItem tsmiResClearAll;
  private ToolStripMenuItem tsmiResInvertSelection;
  private ToolStripSeparator tsmiResSep1;

  /// <summary>
  /// 
  /// </summary>
  private void LoadStateImages()
  {
    this.imageList.Images.Clear();
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      this.imageList.Images.Add(service.ImageList.Images[service.ImageIndex("imgUnchecked")]);
      this.imageList.Images.Add(service.ImageList.Images[service.ImageIndex("imgChecked")]);
      this.imageList.Images.Add(service.ImageList.Images[service.ImageIndex("imgGrayed")]);
    }
    Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.GrayEmpty.bmp");
    if (bitmap == null)
      return;
    this.imageList.Images.AddStrip((Image) bitmap);
  }

  /// <summary>
  /// 
  /// </summary>
  private void ResultItem_SelectAll()
  {
    if (!this.MultiSelect)
      return;
    foreach (ListViewItem listItem in this.ResultList.Items)
      this.ResultItem_SetStatus(listItem, 1);
  }

  /// <summary>
  /// 
  /// </summary>
  private void ResultItem_ClearSelection()
  {
    if (!this.MultiSelect)
      return;
    foreach (ListViewItem listItem in this.ResultList.Items)
      this.ResultItem_SetStatus(listItem, 0);
  }

  /// <summary>
  /// 
  /// </summary>
  private void ResultItem_InvertSelection()
  {
    if (!this.MultiSelect)
      return;
    foreach (ListViewItem listItem in this.ResultList.Items)
      this.ResultItem_SetStatus(listItem, (listItem.StateImageIndex + 1) % 2);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="listItem"></param>
  /// <param name="state"></param>
  protected virtual void ResultItem_SetStatus(ListViewItem listItem, int state)
  {
    if (!this.MultiSelect || listItem.StateImageIndex == state)
      return;
    if (this.ItemStatusChange != null)
    {
      ImbaseFindByNameControl.ItemEventArgs e = new ImbaseFindByNameControl.ItemEventArgs(listItem, state);
      EventHandler itemStatusChange = this.ItemStatusChange;
      if (itemStatusChange != null)
        itemStatusChange((object) this, (EventArgs) e);
      state = e.State;
    }
    listItem.StateImageIndex = state;
  }

  /// <summary>Конструктор</summary>
  public ImbaseFindByNameControl()
  {
    this.InitializeComponent();
    this.LoadStateImages();
  }

  /// <summary>
  /// 
  /// </summary>
  public bool MultiSelect
  {
    get => this._multiSelect;
    set
    {
      if (value == this._multiSelect)
        return;
      this.ResultList.StateImageList = value ? this.imageList : (ImageList) null;
      this.ResultList.ContextMenuStrip = value ? this.cmsResult : (ContextMenuStrip) null;
      this._multiSelect = value;
    }
  }

  /// <summary>
  /// Иконка.
  /// Свойство добавлено для того, чтобы получить иконку для диалогового окна.
  /// </summary>
  public override Icon Icon
  {
    get
    {
      if (this._ico != null)
        return this._ico;
      using (Stream manifestResourceStream = typeof (FindByNameView).Assembly.GetManifestResourceStream("Intermech.Imbase.Resources.FindByName.ico"))
        this._ico = new Icon(manifestResourceStream);
      return this._ico;
    }
  }

  /// <summary>Инициализация кастом данных</summary>
  protected override void InitializeCustomData()
  {
    base.InitializeCustomData();
    this.ResultList.MouseUp += new MouseEventHandler(this.ResultList_MouseUp);
  }

  /// <summary>
  /// Заполнение элемента соотв. найденному узлу справочника / каталога
  /// </summary>
  /// <param name="listItem"></param>
  /// <param name="node"></param>
  protected override void FillResultItem(ListViewItem listItem, TreeNode node)
  {
    if (listItem == null || node == null)
      return;
    base.FillResultItem(listItem, node);
    EventHandler itemFill = this.ItemFill;
    if (itemFill == null)
      return;
    itemFill((object) this, (EventArgs) new ImbaseFindByNameControl.ItemEventArgs(listItem));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ResultList_MouseUp(object sender, MouseEventArgs e)
  {
    if (sender == null || e == null || !this.MultiSelect || e.Button != MouseButtons.Left || e.Clicks != 1)
      return;
    ListViewItem itemAt = this.ResultList.GetItemAt(e.X, e.Y);
    if (itemAt == null || this.ResultList.HitTest(e.X, e.Y).Location != ListViewHitTestLocations.StateImage)
      return;
    int state = itemAt.StateImageIndex == 1 || itemAt.StateImageIndex == 0 ? (itemAt.StateImageIndex + 1) % 2 : itemAt.StateImageIndex;
    if (this.ItemClick != null)
    {
      ImbaseFindByNameControl.ItemEventArgs e1 = new ImbaseFindByNameControl.ItemEventArgs(itemAt, state);
      EventHandler itemClick = this.ItemClick;
      if (itemClick != null)
        itemClick((object) this, (EventArgs) e1);
      state = e1.State;
    }
    this.ResultItem_SetStatus(itemAt, state);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmsResult_Opening(object sender, CancelEventArgs e)
  {
    this.tsmiResSelectAll.Enabled = this.tsmiResClearAll.Enabled = this.tsmiResInvertSelection.Enabled = this.MultiSelect;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miResSelectAll_Click(object sender, EventArgs e) => this.ResultItem_SelectAll();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miResClearAll_Click(object sender, EventArgs e) => this.ResultItem_ClearSelection();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miResInvertSelection_Click(object sender, EventArgs e)
  {
    this.ResultItem_InvertSelection();
  }

  /// <summary>Показывает окно выбора / поиска папок</summary>
  /// <param name="parentNode">Корневой узел</param>
  /// <param name="modal">Модальный режим</param>
  /// <param name="multiSelect">Режим множественного выбора</param>
  /// <param name="locateHandler"></param>
  public static void Show(
    object parentNode,
    bool modal,
    bool multiSelect,
    LocateNodeEventHandler locateHandler)
  {
    ImbaseFindByNameControl control = new ImbaseFindByNameControl();
    control.SetData(parentNode, locateHandler);
    control.MultiSelect = multiSelect;
    ImbaseFindByNameControl.Show(control, modal);
  }

  /// <summary>Показывает окно выбора / поиска папок</summary>
  /// <param name="control">Контрол</param>
  /// <param name="modal">Модальный режим</param>
  public static void Show(ImbaseFindByNameControl control, bool modal)
  {
    if (control == null)
      return;
    DockManager service = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, false);
    if (modal)
    {
      ImbaseViewForm imbaseViewForm = new ImbaseViewForm((IImbaseView) control);
      imbaseViewForm.ShowIcon = true;
      imbaseViewForm.Icon = control.Icon;
      int num = (int) imbaseViewForm.ShowDialog();
      control.Dispose();
    }
    else
    {
      if (service == null)
        return;
      control.Manager = service;
      control.Float();
      if (control.Parent == null || !(control.Parent.Parent is Form))
        return;
      Control parent = control.Parent.Parent;
      Size minimumSize = control.MinimumSize;
      int width = minimumSize.Width + 20;
      minimumSize = control.MinimumSize;
      int height = minimumSize.Height + 40;
      Size size = new Size(width, height);
      parent.MinimumSize = size;
    }
  }

  /// <summary>Событие на заполнении элемента при его создании</summary>
  public event EventHandler ItemFill;

  /// <summary>Событие клика</summary>
  public event EventHandler ItemClick;

  /// <summary>Изменение статуса элемента</summary>
  public event EventHandler ItemStatusChange;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseFindByNameControl));
    this.imageList = new ImageList(this.components);
    this.cmsResult = new ContextMenuStrip(this.components);
    this.tsmiResSelectAll = new ToolStripMenuItem();
    this.tsmiResClearAll = new ToolStripMenuItem();
    this.tsmiResSep1 = new ToolStripSeparator();
    this.tsmiResInvertSelection = new ToolStripMenuItem();
    this.cmsResult.SuspendLayout();
    this.SuspendLayout();
    this.imageList.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.imageList, "imageList");
    this.imageList.TransparentColor = Color.Transparent;
    this.cmsResult.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsmiResSelectAll,
      (ToolStripItem) this.tsmiResClearAll,
      (ToolStripItem) this.tsmiResSep1,
      (ToolStripItem) this.tsmiResInvertSelection
    });
    this.cmsResult.Name = "cmsResult";
    componentResourceManager.ApplyResources((object) this.cmsResult, "cmsResult");
    this.cmsResult.Opening += new CancelEventHandler(this.cmsResult_Opening);
    this.tsmiResSelectAll.Name = "tsmiResSelectAll";
    componentResourceManager.ApplyResources((object) this.tsmiResSelectAll, "tsmiResSelectAll");
    this.tsmiResSelectAll.Click += new EventHandler(this.miResSelectAll_Click);
    this.tsmiResClearAll.Name = "tsmiResClearAll";
    componentResourceManager.ApplyResources((object) this.tsmiResClearAll, "tsmiResClearAll");
    this.tsmiResClearAll.Click += new EventHandler(this.miResClearAll_Click);
    this.tsmiResSep1.Name = "tsmiResSep1";
    componentResourceManager.ApplyResources((object) this.tsmiResSep1, "tsmiResSep1");
    this.tsmiResInvertSelection.Name = "tsmiResInvertSelection";
    componentResourceManager.ApplyResources((object) this.tsmiResInvertSelection, "tsmiResInvertSelection");
    this.tsmiResInvertSelection.Click += new EventHandler(this.miResInvertSelection_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (ImbaseFindByNameControl);
    this.cmsResult.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Структура для хранения информации о событии</summary>
  public class ItemEventArgs : EventArgs
  {
    /// <summary>Item</summary>
    public readonly ListViewItem Item;
    /// <summary>"Новое" значение статуса</summary>
    public int State;

    /// <summary>Конструктор</summary>
    /// <param name="item"></param>
    public ItemEventArgs(ListViewItem item)
    {
      this.Item = item;
      if (item == null)
        return;
      this.State = item.StateImageIndex;
    }

    /// <summary>Конструктор</summary>
    /// <param name="item"></param>
    /// <param name="state"></param>
    public ItemEventArgs(ListViewItem item, int state)
    {
      this.Item = item;
      this.State = state;
    }
  }
}
