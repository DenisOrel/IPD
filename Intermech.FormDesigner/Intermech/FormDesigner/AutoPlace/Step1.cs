// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.AutoPlace.Step1
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.AutoPlace;

/// <summary>
/// 
/// </summary>
internal class Step1 : UserControl
{
  private Button _btnNext;
  private Button _btnPrev;
  private bool _useButtons;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _pnl1;
  private TableLayoutPanel _tlp1;
  private ComboBox _cmbType;
  private CheckBox _chbAll;
  private ListBox _lbUseAttr;
  private ListBox _lbAttr;
  private Label _lb2;
  private TableLayoutPanel _tlp2;
  private PictureBox _pb1;
  private Panel _pnl2;
  private TreeView _trvLinks;
  private ContextMenuStrip _menu;
  private ToolStripMenuItem _miAdd;
  private ToolStripSeparator _miS1;
  private ToolStripMenuItem _miDel;
  private ToolStripMenuItem _miClear;
  private Panel _pnl3;
  private Label _lbMess1;
  private Label _lbMess2;
  private Label _lbMess3;
  private PictureBox _pb2;
  private Panel _pnl4;
  private TableLayoutPanel _tlp3;
  private Label _lb1;
  private Label _lb3;
  private Label _btnRight;
  private Label _btnLeft;
  private ToolStrip _ts;
  private ToolStripLabel _lbToolStrip;
  private ToolStripButton _btnClear;
  private ToolStripSeparator _tsSeparator;
  private ToolStripButton _btnDel;
  private ToolStripSplitButton _btnAdd;
  private ImageList _imLinks;

  /// <summary>
  /// 
  /// </summary>
  public string[] Attributes
  {
    get
    {
      string[] destination = new string[this._lbUseAttr.Items.Count];
      this._lbUseAttr.Items.CopyTo((object[]) destination, 0);
      return destination;
    }
    set
    {
      this._lbUseAttr.BeginUpdate();
      try
      {
        this._lbUseAttr.Items.Clear();
        this._lbUseAttr.Items.AddRange((object[]) value);
      }
      finally
      {
        this._lbUseAttr.EndUpdate();
      }
      this.FilterBox();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool UseButtons
  {
    get => this._useButtons;
    set
    {
      this._useButtons = this.Visible = value;
      if (!value)
        return;
      this._btnNext.Visible = true;
      this._btnPrev.Visible = true;
      this._btnPrev.Enabled = false;
      this.FilterBox();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public FormLinks Links { get; private set; }

  /// <summary>Конструктор.</summary>
  /// <param name="links"></param>
  /// <param name="next"></param>
  /// <param name="prev"></param>
  public Step1(FormLinks links, Button next, Button prev)
  {
    this.InitializeComponent();
    this._btnNext = next;
    this._btnPrev = prev;
    this.Links = new FormLinks(links.FormID, (IEnumerable<IFormDesignerFormLinksProvider>) links);
    this._trvLinks.BeginUpdate();
    try
    {
      foreach (IFormDesignerFormLinksProvider link in (List<IFormDesignerFormLinksProvider>) this.Links)
      {
        link.Load(this.Links.FormID);
        if (link is IFormDesignerFormLinksImages designerFormLinksImages)
          designerFormLinksImages.GetLinkImages((object) this._imLinks);
        TreeNode rootNode = link.RootNode as TreeNode;
        if (!this._trvLinks.Nodes.Contains(rootNode))
        {
          rootNode.Tag = (object) link.ProviderGuid;
          this._trvLinks.Nodes.Add(rootNode);
        }
      }
      this._trvLinks.ExpandAll();
    }
    finally
    {
      this._trvLinks.EndUpdate();
    }
    this._cmbType.Items.Add((object) LocalizationHolder.rm.GetString("FormDesigner_1"));
    FieldInfo[] fields = typeof (FieldTypes).GetFields();
    string empty = string.Empty;
    foreach (FieldInfo fieldInfo in fields)
    {
      FieldTypes ft = (FieldTypes) fieldInfo.GetValue((object) FieldTypes.ftUnknown);
      switch (ft)
      {
        case FieldTypes.ftShortBlob:
        case FieldTypes.ftFile:
        case FieldTypes.ftExternalLink:
        case FieldTypes.ftBlob:
          continue;
        default:
          string caption = AttributesTypeHelper.GetCaption(ft);
          if (!this._cmbType.Items.Contains((object) caption) && ft != FieldTypes.ftUnknown)
          {
            this._cmbType.Items.Add((object) caption);
            continue;
          }
          continue;
      }
    }
    this._cmbType.SelectedIndex = 0;
    IFormDesignerFormLinksManager service = ServiceUtils.GetService<IFormDesignerFormLinksManager>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      foreach (FormDesignerFormLinksProviderType linksProviderType in (IEnumerable<FormDesignerFormLinksProviderType>) service)
      {
        ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(linksProviderType.ProviderName);
        toolStripMenuItem1.Tag = (object) linksProviderType.ProviderGuid;
        ToolStripMenuItem toolStripMenuItem2 = toolStripMenuItem1;
        toolStripMenuItem2.Click += new EventHandler(this.On_item_Click);
        this._btnAdd.DropDownItems.Add((ToolStripItem) toolStripMenuItem2);
        ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem(linksProviderType.ProviderName);
        toolStripMenuItem3.Tag = (object) linksProviderType.ProviderGuid;
        ToolStripMenuItem toolStripMenuItem4 = toolStripMenuItem3;
        toolStripMenuItem4.Click += new EventHandler(this.On_item_Click);
        this._miAdd.DropDownItems.Add((ToolStripItem) toolStripMenuItem4);
      }
    }
    this.SetLabelMessageText();
  }

  /// <summary>Клик по кнопке "Add".</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnAdd_ButtonClick(object sender, EventArgs e)
  {
    if (this._trvLinks.SelectedNode == null)
      return;
    TreeNode treeNode = this._trvLinks.SelectedNode;
    while (treeNode.Parent != null)
      treeNode = treeNode.Parent;
    this.AddItem((Guid) treeNode.Tag);
  }

  /// <summary>Удалить атрибут из списка выбранных атрибутов.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnLeft_Click(object sender, EventArgs e)
  {
    if (this._lbUseAttr.SelectedItems == null)
      return;
    ArrayList arrayList = new ArrayList((ICollection) this._lbUseAttr.SelectedItems);
    string str1 = Convert.ToString(this._lbUseAttr.SelectedItems[0]);
    this._lbUseAttr.BeginUpdate();
    try
    {
      foreach (string str2 in arrayList)
        this._lbUseAttr.Items.Remove((object) str2);
    }
    finally
    {
      this._lbUseAttr.EndUpdate();
    }
    this.On_chbType_SelectedIndexChanged((object) null, (EventArgs) null);
    int num = this._lbAttr.Items.IndexOf((object) str1);
    if (num <= -1)
      return;
    this._lbAttr.SelectedIndex = num;
  }

  /// <summary>Добавить атрибут в список выбранных атрибутов.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnRight_Click(object sender, EventArgs e)
  {
    this._lbUseAttr.ClearSelected();
    foreach (string selectedItem in this._lbAttr.SelectedItems)
      this._lbUseAttr.SelectedIndex = this._lbUseAttr.Items.Add((object) selectedItem);
    this.FilterBox();
  }

  /// <summary>Изменение выделенного типа объета.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_chbType_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.LoadProps();
    this._btnRight.Enabled = this._lbAttr.SelectedIndex > -1;
    this._btnLeft.Enabled = this._lbUseAttr.SelectedIndex > -1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_item_Click(object sender, EventArgs e)
  {
    if (!(sender is ToolStripMenuItem toolStripMenuItem))
      return;
    this.AddItem((Guid) toolStripMenuItem.Tag);
  }

  /// <summary>
  /// Изменение выделенного атрибута в списке доступных атрибутов
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lbAttr_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnRight.Enabled = this._lbAttr.SelectedIndex > -1;
  }

  /// <summary>
  /// Изменение выделенного атрибута в списке выбранных атрибутов.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lbUserAttr_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnLeft.Enabled = this._lbUseAttr.SelectedIndex > -1;
  }

  /// <summary>Очистить дерево.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_miClear_Click(object sender, EventArgs e)
  {
    foreach (IFormDesignerFormLinksProvider link in (List<IFormDesignerFormLinksProvider>) this.Links)
      link.Clear();
    if (!this._chbAll.Checked)
      this.LoadProps();
    this.SetLabelMessageText();
  }

  /// <summary>Удаление узла дерева.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_miDel_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._trvLinks.SelectedNode;
    IFormDesignerFormLinksProvider provider = this.Links.GetProvider((selectedNode.Tag as FormLink).ProviderGuid);
    if (provider != null)
    {
      provider.Delete((object) selectedNode);
      if (!this._chbAll.Checked)
        this.LoadProps();
    }
    this.SetLabelMessageText();
  }

  /// <summary>Событие после выделения узла в дереве.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_trvLinks_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this._btnDel.Enabled = this._miDel.Enabled = this._trvLinks.SelectedNode != null && this._trvLinks.SelectedNode.Tag is FormLink;
  }

  /// <summary>Добавление нового узла.</summary>
  /// <param name="guid">Гуид провайдера</param>
  private void AddItem(Guid guid)
  {
    try
    {
      IFormDesignerFormLinksProvider provider = this.Links.GetProvider(guid);
      if (provider != null)
      {
        provider.Add();
        if (provider is IFormDesignerFormLinksImages designerFormLinksImages)
          designerFormLinksImages.GetLinkImages((object) this._imLinks);
        if (!this._chbAll.Checked)
          this.LoadProps();
      }
      this.SetLabelMessageText();
    }
    catch (Exception ex)
    {
      Trace.WriteLine(ex.Message);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void FilterBox()
  {
    this._lbAttr.BeginUpdate();
    try
    {
      foreach (object obj in this._lbUseAttr.Items)
      {
        if (this._lbAttr.Items.Contains(obj))
          this._lbAttr.Items.Remove(obj);
      }
    }
    finally
    {
      this._lbAttr.EndUpdate();
    }
    if (!this._useButtons)
      return;
    this._btnNext.Enabled = this._lbUseAttr.Items.Count > 0;
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadProps()
  {
    FieldTypes field = AttributesTypeHelper.GetFieldType(Convert.ToString(this._cmbType.SelectedItem));
    this._lbAttr.BeginUpdate();
    try
    {
      this._lbAttr.Items.Clear();
      if (this._chbAll.Checked)
      {
        List<IMSAttributeType> attributeTypesList = MetaDataHelper.GetAttributeTypesList();
        if (attributeTypesList != null)
        {
          if (field != FieldTypes.ftUnknown)
            this._lbAttr.Items.AddRange((object[]) attributeTypesList.Where<IMSAttributeType>((Func<IMSAttributeType, bool>) (x => x.FieldType == field)).Select<IMSAttributeType, string>((Func<IMSAttributeType, string>) (x => x.Name)).ToArray<string>());
          else
            this._lbAttr.Items.AddRange((object[]) attributeTypesList.Select<IMSAttributeType, string>((Func<IMSAttributeType, string>) (x => x.Name)).ToArray<string>());
        }
      }
      else
      {
        foreach (IFormDesignerFormLinksProvider link in (List<IFormDesignerFormLinksProvider>) this.Links)
        {
          foreach (FormLink formLink in link.FormLinks)
          {
            List<int> attributes = formLink.Attributes;
            if (attributes != null)
            {
              foreach (int attrTypeID in attributes)
              {
                IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
                if (attributeType != null && (field == FieldTypes.ftUnknown || attributeType.FieldType == field) && !this._lbAttr.Items.Contains((object) attributeType.Name))
                  this._lbAttr.Items.Add((object) attributeType.Name);
              }
            }
          }
        }
      }
    }
    finally
    {
      this._lbAttr.EndUpdate();
    }
    this.FilterBox();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private void SetLabelMessageText()
  {
    bool flag = false;
    foreach (TreeNode node in this._trvLinks.Nodes)
    {
      if (node.Nodes.Count != 0)
      {
        flag = true;
        break;
      }
    }
    if (flag)
    {
      this._lbMess1.Visible = this._lbMess2.Visible = false;
      this._lbMess3.Visible = true;
      this._lbMess1.Dock = DockStyle.None;
      this._lbMess3.Dock = DockStyle.Top;
    }
    else
    {
      this._lbMess1.Visible = this._lbMess2.Visible = true;
      this._lbMess3.Visible = false;
      this._lbMess1.Dock = DockStyle.Top;
      this._lbMess3.Dock = DockStyle.None;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._trvLinks.Nodes.Clear();
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Step1));
    this._pnl1 = new Panel();
    this._tlp2 = new TableLayoutPanel();
    this._pb1 = new PictureBox();
    this._pnl2 = new Panel();
    this._trvLinks = new TreeView();
    this._menu = new ContextMenuStrip(this.components);
    this._miAdd = new ToolStripMenuItem();
    this._miS1 = new ToolStripSeparator();
    this._miDel = new ToolStripMenuItem();
    this._miClear = new ToolStripMenuItem();
    this._imLinks = new ImageList(this.components);
    this._ts = new ToolStrip();
    this._lbToolStrip = new ToolStripLabel();
    this._btnClear = new ToolStripButton();
    this._tsSeparator = new ToolStripSeparator();
    this._btnDel = new ToolStripButton();
    this._btnAdd = new ToolStripSplitButton();
    this._pnl3 = new Panel();
    this._lbMess3 = new Label();
    this._lbMess1 = new Label();
    this._lbMess2 = new Label();
    this._pnl4 = new Panel();
    this._tlp3 = new TableLayoutPanel();
    this._lb1 = new Label();
    this._lb3 = new Label();
    this._tlp1 = new TableLayoutPanel();
    this._cmbType = new ComboBox();
    this._chbAll = new CheckBox();
    this._lbUseAttr = new ListBox();
    this._lbAttr = new ListBox();
    this._lb2 = new Label();
    this._btnRight = new Label();
    this._btnLeft = new Label();
    this._pb2 = new PictureBox();
    this._pnl1.SuspendLayout();
    this._tlp2.SuspendLayout();
    ((ISupportInitialize) this._pb1).BeginInit();
    this._pnl2.SuspendLayout();
    this._menu.SuspendLayout();
    this._ts.SuspendLayout();
    this._pnl3.SuspendLayout();
    this._pnl4.SuspendLayout();
    this._tlp3.SuspendLayout();
    this._tlp1.SuspendLayout();
    ((ISupportInitialize) this._pb2).BeginInit();
    this.SuspendLayout();
    this._pnl1.BackColor = Color.Gainsboro;
    this._pnl1.Controls.Add((Control) this._tlp2);
    componentResourceManager.ApplyResources((object) this._pnl1, "_pnl1");
    this._pnl1.Name = "_pnl1";
    this._tlp2.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this._tlp2, "_tlp2");
    this._tlp2.Controls.Add((Control) this._pb1, 2, 3);
    this._tlp2.Controls.Add((Control) this._pnl2, 1, 1);
    this._tlp2.Controls.Add((Control) this._pnl3, 3, 3);
    this._tlp2.Controls.Add((Control) this._pnl4, 3, 1);
    this._tlp2.Name = "_tlp2";
    componentResourceManager.ApplyResources((object) this._pb1, "_pb1");
    this._pb1.Image = (Image) Intermech.FormDesigner.Properties.Resources.Arrow_Left;
    this._pb1.Name = "_pb1";
    this._pb1.TabStop = false;
    this._pnl2.Controls.Add((Control) this._trvLinks);
    this._pnl2.Controls.Add((Control) this._ts);
    componentResourceManager.ApplyResources((object) this._pnl2, "_pnl2");
    this._pnl2.Name = "_pnl2";
    this._tlp2.SetRowSpan((Control) this._pnl2, 4);
    this._trvLinks.ContextMenuStrip = this._menu;
    componentResourceManager.ApplyResources((object) this._trvLinks, "_trvLinks");
    this._trvLinks.ImageList = this._imLinks;
    this._trvLinks.Name = "_trvLinks";
    this._trvLinks.AfterSelect += new TreeViewEventHandler(this.On_trvLinks_AfterSelect);
    this._menu.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._miAdd,
      (ToolStripItem) this._miS1,
      (ToolStripItem) this._miDel,
      (ToolStripItem) this._miClear
    });
    this._menu.Name = "Menu1";
    componentResourceManager.ApplyResources((object) this._menu, "_menu");
    this._miAdd.Name = "_miAdd";
    componentResourceManager.ApplyResources((object) this._miAdd, "_miAdd");
    this._miS1.Name = "_miS1";
    componentResourceManager.ApplyResources((object) this._miS1, "_miS1");
    this._miDel.Name = "_miDel";
    componentResourceManager.ApplyResources((object) this._miDel, "_miDel");
    this._miDel.Click += new EventHandler(this.On_miDel_Click);
    this._miClear.Name = "_miClear";
    componentResourceManager.ApplyResources((object) this._miClear, "_miClear");
    this._miClear.Click += new EventHandler(this.On_miClear_Click);
    this._imLinks.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this._imLinks, "_imLinks");
    this._imLinks.TransparentColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this._ts, "_ts");
    this._ts.BackColor = Color.Transparent;
    this._ts.GripStyle = ToolStripGripStyle.Hidden;
    this._ts.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this._lbToolStrip,
      (ToolStripItem) this._btnClear,
      (ToolStripItem) this._tsSeparator,
      (ToolStripItem) this._btnDel,
      (ToolStripItem) this._btnAdd
    });
    this._ts.Name = "_ts";
    this._lbToolStrip.Name = "_lbToolStrip";
    componentResourceManager.ApplyResources((object) this._lbToolStrip, "_lbToolStrip");
    this._btnClear.Alignment = ToolStripItemAlignment.Right;
    this._btnClear.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._btnClear.Image = (Image) Intermech.FormDesigner.Properties.Resources.FormLink_Clean;
    componentResourceManager.ApplyResources((object) this._btnClear, "_btnClear");
    this._btnClear.Name = "_btnClear";
    this._btnClear.Click += new EventHandler(this.On_miClear_Click);
    this._tsSeparator.Alignment = ToolStripItemAlignment.Right;
    this._tsSeparator.Name = "_tsSeparator";
    componentResourceManager.ApplyResources((object) this._tsSeparator, "_tsSeparator");
    this._btnDel.Alignment = ToolStripItemAlignment.Right;
    this._btnDel.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._btnDel.Image = (Image) Intermech.FormDesigner.Properties.Resources.FormLink_Delete;
    componentResourceManager.ApplyResources((object) this._btnDel, "_btnDel");
    this._btnDel.Name = "_btnDel";
    this._btnDel.Click += new EventHandler(this.On_miDel_Click);
    this._btnAdd.Alignment = ToolStripItemAlignment.Right;
    this._btnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._btnAdd.Image = (Image) Intermech.FormDesigner.Properties.Resources.FormLink_Add;
    componentResourceManager.ApplyResources((object) this._btnAdd, "_btnAdd");
    this._btnAdd.Name = "_btnAdd";
    this._btnAdd.ButtonClick += new EventHandler(this.On_btnAdd_ButtonClick);
    componentResourceManager.ApplyResources((object) this._pnl3, "_pnl3");
    this._pnl3.Controls.Add((Control) this._lbMess3);
    this._pnl3.Controls.Add((Control) this._lbMess1);
    this._pnl3.Controls.Add((Control) this._lbMess2);
    this._pnl3.Name = "_pnl3";
    this._tlp2.SetRowSpan((Control) this._pnl3, 2);
    componentResourceManager.ApplyResources((object) this._lbMess3, "_lbMess3");
    this._lbMess3.Name = "_lbMess3";
    componentResourceManager.ApplyResources((object) this._lbMess1, "_lbMess1");
    this._lbMess1.Name = "_lbMess1";
    componentResourceManager.ApplyResources((object) this._lbMess2, "_lbMess2");
    this._lbMess2.Name = "_lbMess2";
    this._pnl4.Controls.Add((Control) this._tlp3);
    componentResourceManager.ApplyResources((object) this._pnl4, "_pnl4");
    this._pnl4.Name = "_pnl4";
    componentResourceManager.ApplyResources((object) this._tlp3, "_tlp3");
    this._tlp3.Controls.Add((Control) this._lb1, 0, 0);
    this._tlp3.Controls.Add((Control) this._lb3, 1, 0);
    this._tlp3.Name = "_tlp3";
    componentResourceManager.ApplyResources((object) this._lb1, "_lb1");
    this._lb1.Name = "_lb1";
    componentResourceManager.ApplyResources((object) this._lb3, "_lb3");
    this._lb3.Name = "_lb3";
    this._tlp1.BackColor = Color.FromArgb(100, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
    componentResourceManager.ApplyResources((object) this._tlp1, "_tlp1");
    this._tlp1.Controls.Add((Control) this._cmbType, 1, 3);
    this._tlp1.Controls.Add((Control) this._chbAll, 1, 5);
    this._tlp1.Controls.Add((Control) this._lbUseAttr, 3, 7);
    this._tlp1.Controls.Add((Control) this._lbAttr, 1, 7);
    this._tlp1.Controls.Add((Control) this._lb2, 1, 1);
    this._tlp1.Controls.Add((Control) this._btnRight, 2, 8);
    this._tlp1.Controls.Add((Control) this._btnLeft, 2, 9);
    this._tlp1.Name = "_tlp1";
    componentResourceManager.ApplyResources((object) this._cmbType, "_cmbType");
    this._cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbType.Name = "_cmbType";
    this._cmbType.SelectedIndexChanged += new EventHandler(this.On_chbType_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._chbAll, "_chbAll");
    this._chbAll.BackColor = Color.Transparent;
    this._chbAll.Name = "_chbAll";
    this._chbAll.UseVisualStyleBackColor = false;
    this._chbAll.CheckedChanged += new EventHandler(this.On_chbType_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._lbUseAttr, "_lbUseAttr");
    this._lbUseAttr.Name = "_lbUseAttr";
    this._tlp1.SetRowSpan((Control) this._lbUseAttr, 4);
    this._lbUseAttr.SelectionMode = SelectionMode.MultiExtended;
    this._lbUseAttr.Sorted = true;
    this._lbUseAttr.SelectedIndexChanged += new EventHandler(this.On_lbUserAttr_SelectedIndexChanged);
    this._lbUseAttr.DoubleClick += new EventHandler(this.On_btnLeft_Click);
    componentResourceManager.ApplyResources((object) this._lbAttr, "_lbAttr");
    this._lbAttr.Name = "_lbAttr";
    this._tlp1.SetRowSpan((Control) this._lbAttr, 4);
    this._lbAttr.SelectionMode = SelectionMode.MultiExtended;
    this._lbAttr.Sorted = true;
    this._lbAttr.SelectedIndexChanged += new EventHandler(this.On_lbAttr_SelectedIndexChanged);
    this._lbAttr.DoubleClick += new EventHandler(this.On_btnRight_Click);
    componentResourceManager.ApplyResources((object) this._lb2, "_lb2");
    this._lb2.BackColor = Color.Transparent;
    this._lb2.Name = "_lb2";
    this._btnRight.Image = (Image) Intermech.FormDesigner.Properties.Resources.Right;
    componentResourceManager.ApplyResources((object) this._btnRight, "_btnRight");
    this._btnRight.Name = "_btnRight";
    this._btnRight.Click += new EventHandler(this.On_btnRight_Click);
    this._btnLeft.Image = (Image) Intermech.FormDesigner.Properties.Resources.Left;
    componentResourceManager.ApplyResources((object) this._btnLeft, "_btnLeft");
    this._btnLeft.Name = "_btnLeft";
    this._btnLeft.Click += new EventHandler(this.On_btnLeft_Click);
    this._pb2.BackgroundImage = (Image) Intermech.FormDesigner.Properties.Resources.Horizontal_Line;
    componentResourceManager.ApplyResources((object) this._pb2, "_pb2");
    this._pb2.Name = "_pb2";
    this._pb2.TabStop = false;
    this.Controls.Add((Control) this._pb2);
    this.Controls.Add((Control) this._tlp1);
    this.Controls.Add((Control) this._pnl1);
    this.MinimumSize = new Size(660, 400);
    this.Name = nameof (Step1);
    componentResourceManager.ApplyResources((object) this, "$this");
    this._pnl1.ResumeLayout(false);
    this._tlp2.ResumeLayout(false);
    this._tlp2.PerformLayout();
    ((ISupportInitialize) this._pb1).EndInit();
    this._pnl2.ResumeLayout(false);
    this._menu.ResumeLayout(false);
    this._ts.ResumeLayout(false);
    this._ts.PerformLayout();
    this._pnl3.ResumeLayout(false);
    this._pnl4.ResumeLayout(false);
    this._tlp3.ResumeLayout(false);
    this._tlp3.PerformLayout();
    this._tlp1.ResumeLayout(false);
    this._tlp1.PerformLayout();
    ((ISupportInitialize) this._pb2).EndInit();
    this.ResumeLayout(false);
  }
}
