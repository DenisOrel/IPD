
// Type: Intermech.Client.Core.FormDesigner.Controls.FormLinksEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Форма для редактирования привязки формы к типам объектов/связей.
/// </summary>
public class FormLinksEditorForm : Form
{
  private FormLinks _links;
  private bool _readOnly;
  private Bitmap _dubBitmap;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ContextMenuStrip Menu1;
  private ToolStripMenuItem _miAdd;
  private ToolStripSeparator _miSeparator;
  private ToolStripMenuItem _miDel;
  private ToolStripMenuItem _miClear;
  private ImageList _imageList;
  private Panel _pnlMessage;
  private Label _lbExplanation;
  private PictureBox _pbHorizontalLine;
  private ToolStrip _tsMenuButtons;
  private ToolStripButton _btnClear;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton _btnDel;
  private ToolStripSplitButton _btnAdd;
  private TreeView _trv;
  private Label _lbMessage;
  private Button _btnOK;
  private Button _btnCancel;
  private Panel panel1;
  private Panel panel2;

  /// <summary>Изменение данных.</summary>
  public bool Changed { get; set; }

  /// <summary>Список ссылок.</summary>
  public FormLinks Links
  {
    get => this._links;
    set
    {
      this._links = new FormLinks(value.FormID, (IEnumerable<IFormDesignerFormLinksProvider>) value);
      this.UpdateLinks();
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="readOnly"></param>
  public FormLinksEditorForm(bool readOnly)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1142);
    IFormDesignerFormLinksManager service = ServicesManager.GetService(typeof (IFormDesignerFormLinksManager)) as IFormDesignerFormLinksManager;
    this._btnAdd.Enabled = this._miAdd.Enabled = service != null;
    this._imageList = new ImageList(this.components);
    Size imageSize = this._imageList.ImageSize;
    int width = imageSize.Width;
    imageSize = this._imageList.ImageSize;
    int height = imageSize.Height;
    this._dubBitmap = new Bitmap(width, height);
    using (Graphics graphics = Graphics.FromImage((Image) this._dubBitmap))
    {
      using (Brush brush = (Brush) new SolidBrush(SystemColors.Window))
        graphics.FillRectangle(brush, new Rectangle(0, 0, this._dubBitmap.Width, this._dubBitmap.Height));
    }
    this._trv.ImageList = this._imageList;
    if (service != null)
    {
      foreach (FormDesignerFormLinksProviderType linksProviderType in (IEnumerable<FormDesignerFormLinksProviderType>) service)
      {
        ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(linksProviderType.ProviderName);
        toolStripMenuItem1.Tag = (object) linksProviderType.ProviderGuid;
        ToolStripMenuItem toolStripMenuItem2 = toolStripMenuItem1;
        toolStripMenuItem2.Click += new EventHandler(this.Onitem_Click);
        this._btnAdd.DropDownItems.Add((ToolStripItem) toolStripMenuItem2);
        ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem(linksProviderType.ProviderName);
        toolStripMenuItem3.Tag = (object) linksProviderType.ProviderGuid;
        ToolStripMenuItem toolStripMenuItem4 = toolStripMenuItem3;
        toolStripMenuItem4.Click += new EventHandler(this.Onitem_Click);
        this._miAdd.DropDownItems.Add((ToolStripItem) toolStripMenuItem4);
      }
    }
    this._readOnly = readOnly;
    this._tsMenuButtons.Enabled = this._btnOK.Enabled = !this._readOnly;
    if (!this._readOnly)
      return;
    this._trv.ContextMenuStrip = (ContextMenuStrip) null;
  }

  /// <summary>Клик по кнопке "Add".</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnAdd_ButtonClick(object sender, EventArgs e)
  {
    if (this._trv.SelectedNode == null)
      return;
    TreeNode treeNode = this._trv.SelectedNode;
    while (treeNode.Parent != null)
      treeNode = treeNode.Parent;
    this.AddItem((Guid) treeNode.Tag);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Onitem_Click(object sender, EventArgs e)
  {
    if (!(sender is ToolStripMenuItem toolStripMenuItem))
      return;
    this.AddItem((Guid) toolStripMenuItem.Tag);
  }

  /// <summary>Очистить дерево.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_miClear_Click(object sender, EventArgs e)
  {
    foreach (IFormDesignerFormLinksProvider link in (List<IFormDesignerFormLinksProvider>) this._links)
      link.Clear();
    this.Changed = true;
    this._lbMessage.Visible = !this.IsFormLinked();
  }

  /// <summary>Удаление узла дерева.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_miDel_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._trv.SelectedNode;
    IFormDesignerFormLinksProvider provider = this._links.GetProvider((selectedNode.Tag as FormLink).ProviderGuid);
    if (provider != null)
    {
      provider.Delete((object) selectedNode);
      this.On_trv_AfterSelect((object) this, (TreeViewEventArgs) null);
      this.Changed = true;
    }
    this._lbMessage.Visible = !this.IsFormLinked();
  }

  /// <summary>Событие после выделения узла в дереве.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_trv_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this._btnDel.Enabled = this._miDel.Enabled = this._trv.SelectedNode != null && this._trv.SelectedNode.Tag is FormLink;
  }

  /// <summary>Загрузка формы.</summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Закрытие формы.</summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    this._trv.Nodes.Clear();
    FormStorage.SaveLayout((Control) this);
    foreach (ToolStripItem dropDownItem in (ArrangedElementCollection) this._btnAdd.DropDownItems)
      dropDownItem.Click -= new EventHandler(this.Onitem_Click);
    foreach (ToolStripItem dropDownItem in (ArrangedElementCollection) this._miAdd.DropDownItems)
      dropDownItem.Click -= new EventHandler(this.Onitem_Click);
  }

  /// <summary>Добавление нового узла.</summary>
  /// <param name="guid">Гуид провайдера.</param>
  private void AddItem(Guid guid)
  {
    try
    {
      IFormDesignerFormLinksProvider provider = this._links.GetProvider(guid);
      if (provider != null)
      {
        provider.Add();
        if (provider is IFormDesignerFormLinksImages designerFormLinksImages)
          designerFormLinksImages.GetLinkImages((object) this._imageList);
        this._trv.ExpandAll();
        this.Changed = true;
      }
      this._lbMessage.Visible = !this.IsFormLinked();
    }
    catch (Exception ex)
    {
      Trace.WriteLine(ex.Message);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool IsFormLinked()
  {
    bool flag = false;
    foreach (TreeNode node in this._trv.Nodes)
    {
      if (node.Nodes.Count != 0)
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  /// <summary>Обновить ссылки.</summary>
  public void UpdateLinks()
  {
    this._trv.BeginUpdate();
    try
    {
      this._trv.Nodes.Clear();
      this._imageList.Images.Clear();
      this._imageList.Images.Add((Image) this._dubBitmap);
      foreach (IFormDesignerFormLinksProvider link in (List<IFormDesignerFormLinksProvider>) this._links)
      {
        link.Load(this._links.FormID);
        if (link is IFormDesignerFormLinksImages designerFormLinksImages)
          designerFormLinksImages.GetLinkImages((object) this._imageList);
        if (link.RootNode is TreeNode rootNode1 && !this._trv.Nodes.Contains(rootNode1))
        {
          TreeNode rootNode = link.RootNode as TreeNode;
          rootNode.Tag = (object) link.ProviderGuid;
          this._trv.Nodes.Add(rootNode);
        }
      }
      this.On_trv_AfterSelect((object) this, (TreeViewEventArgs) null);
      this._trv.ExpandAll();
    }
    finally
    {
      this._trv.EndUpdate();
    }
    this._lbMessage.Visible = !this.IsFormLinked();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormLinksEditorForm));
    this.Menu1 = new ContextMenuStrip(this.components);
    this._miAdd = new ToolStripMenuItem();
    this._miSeparator = new ToolStripSeparator();
    this._miDel = new ToolStripMenuItem();
    this._miClear = new ToolStripMenuItem();
    this._pnlMessage = new Panel();
    this._lbExplanation = new Label();
    this._pbHorizontalLine = new PictureBox();
    this._tsMenuButtons = new ToolStrip();
    this._btnClear = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this._btnDel = new ToolStripButton();
    this._btnAdd = new ToolStripSplitButton();
    this._trv = new TreeView();
    this._lbMessage = new Label();
    this._btnOK = new Button();
    this._btnCancel = new Button();
    this.panel1 = new Panel();
    this.panel2 = new Panel();
    this.Menu1.SuspendLayout();
    this._pnlMessage.SuspendLayout();
    ((ISupportInitialize) this._pbHorizontalLine).BeginInit();
    this._tsMenuButtons.SuspendLayout();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.Menu1.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._miAdd,
      (ToolStripItem) this._miSeparator,
      (ToolStripItem) this._miDel,
      (ToolStripItem) this._miClear
    });
    this.Menu1.Name = "Menu1";
    componentResourceManager.ApplyResources((object) this.Menu1, "Menu1");
    this._miAdd.Name = "_miAdd";
    componentResourceManager.ApplyResources((object) this._miAdd, "_miAdd");
    this._miSeparator.Name = "_miSeparator";
    componentResourceManager.ApplyResources((object) this._miSeparator, "_miSeparator");
    this._miDel.Name = "_miDel";
    componentResourceManager.ApplyResources((object) this._miDel, "_miDel");
    this._miDel.Click += new EventHandler(this.On_miDel_Click);
    this._miClear.Name = "_miClear";
    componentResourceManager.ApplyResources((object) this._miClear, "_miClear");
    this._miClear.Click += new EventHandler(this.On_miClear_Click);
    this._pnlMessage.Controls.Add((Control) this._lbExplanation);
    componentResourceManager.ApplyResources((object) this._pnlMessage, "_pnlMessage");
    this._pnlMessage.Name = "_pnlMessage";
    componentResourceManager.ApplyResources((object) this._lbExplanation, "_lbExplanation");
    this._lbExplanation.Name = "_lbExplanation";
    this._pbHorizontalLine.BackgroundImage = (Image) Resources.Horizontal_Line;
    componentResourceManager.ApplyResources((object) this._pbHorizontalLine, "_pbHorizontalLine");
    this._pbHorizontalLine.Name = "_pbHorizontalLine";
    this._pbHorizontalLine.TabStop = false;
    componentResourceManager.ApplyResources((object) this._tsMenuButtons, "_tsMenuButtons");
    this._tsMenuButtons.BackColor = Color.Transparent;
    this._tsMenuButtons.GripStyle = ToolStripGripStyle.Hidden;
    this._tsMenuButtons.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._btnClear,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this._btnDel,
      (ToolStripItem) this._btnAdd
    });
    this._tsMenuButtons.Name = "_tsMenuButtons";
    this._tsMenuButtons.RenderMode = ToolStripRenderMode.System;
    this._tsMenuButtons.TabStop = true;
    this._btnClear.Alignment = ToolStripItemAlignment.Right;
    this._btnClear.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._btnClear, "_btnClear");
    this._btnClear.Name = "_btnClear";
    this._btnClear.Click += new EventHandler(this.On_miClear_Click);
    this.toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this._btnDel.Alignment = ToolStripItemAlignment.Right;
    this._btnDel.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._btnDel.Image = (Image) Resources.FormLink_Delete;
    componentResourceManager.ApplyResources((object) this._btnDel, "_btnDel");
    this._btnDel.Name = "_btnDel";
    this._btnDel.Click += new EventHandler(this.On_miDel_Click);
    this._btnAdd.Alignment = ToolStripItemAlignment.Right;
    this._btnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._btnAdd.Image = (Image) Resources.FormLink_Add;
    componentResourceManager.ApplyResources((object) this._btnAdd, "_btnAdd");
    this._btnAdd.Name = "_btnAdd";
    this._btnAdd.ButtonClick += new EventHandler(this.On_btnAdd_ButtonClick);
    this._btnAdd.Click += new EventHandler(this.Onitem_Click);
    this._trv.ContextMenuStrip = this.Menu1;
    componentResourceManager.ApplyResources((object) this._trv, "_trv");
    this._trv.FullRowSelect = true;
    this._trv.Name = "_trv";
    this._trv.AfterSelect += new TreeViewEventHandler(this.On_trv_AfterSelect);
    this._lbMessage.BackColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this._lbMessage, "_lbMessage");
    this._lbMessage.Name = "_lbMessage";
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.BackColor = SystemColors.Control;
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = false;
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.BackColor = SystemColors.Control;
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = false;
    this.panel1.BackColor = Color.FromArgb(100, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
    this.panel1.Controls.Add((Control) this._trv);
    this.panel1.Controls.Add((Control) this._lbMessage);
    this.panel1.Controls.Add((Control) this.panel2);
    this.panel1.Controls.Add((Control) this._tsMenuButtons);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.panel2.Controls.Add((Control) this._btnOK);
    this.panel2.Controls.Add((Control) this._btnCancel);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this._pbHorizontalLine);
    this.Controls.Add((Control) this._pnlMessage);
    this.DoubleBuffered = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormLinksEditorForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Menu1.ResumeLayout(false);
    this._pnlMessage.ResumeLayout(false);
    ((ISupportInitialize) this._pbHorizontalLine).EndInit();
    this._tsMenuButtons.ResumeLayout(false);
    this._tsMenuButtons.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
