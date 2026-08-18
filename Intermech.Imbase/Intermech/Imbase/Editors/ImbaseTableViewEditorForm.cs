// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.ImbaseTableViewEditorForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class ImbaseTableViewEditorForm : Form
{
  private Dictionary<Guid, ListViewItem> _items;
  private XmlNode _currentNode;
  private XmlNode _rolesNode;
  private string ROLE = "Role";
  private string COLUMN = "Column";
  private string GUID_ATTR = "Guid";
  private string INDEX_ATTR = "Index";
  private string VISIBLE_ATTR = "Visible";
  private string WIDTH_ATTR = "Width";
  private IContainer components;
  private Panel _pnlTop;
  private ComboBox _cmbRole;
  private Label _lbProfile;
  private SplitContainer _splContainer;
  private TableLayoutPanel _tlpAllFields;
  private Panel _pnlBottom;
  private Button _btnCancel;
  private Button _btnOK;
  private Button _btnLeftAll;
  private Button _btnLeft;
  private Button _btnRight;
  private Button _btnRightAll;
  private TableLayoutPanel _tlpSelecetdFields;
  private Button _btnTop;
  private Button _btnUp;
  private Button _btnBottom;
  private Button _btnDown;
  private ListView _lvAllFields;
  private ListView _lvSelectedFields;
  private ImageList _imgList;
  private ColumnHeader colName_All;
  private ColumnHeader colName_Selected;
  private Label _lbAllFields;
  private Label _lbSelectedFields;

  internal object SelectedRole => this._cmbRole.SelectedValue;

  internal XmlNode Settings => this._rolesNode == null ? this._currentNode : this._rolesNode;

  internal ImbaseTableViewEditorForm(XmlNode rootNode)
  {
    this.InitializeComponent();
    this._pnlTop.Visible = false;
    if (Statics.IconSrv != null)
      this._lvAllFields.SmallImageList = this._lvSelectedFields.SmallImageList = Statics.IconSrv.ImageList;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.InnerXml = rootNode.OuterXml;
    this._currentNode = xmlDocument.FirstChild;
    this.CreateItems(this._currentNode.ChildNodes);
    this._lvAllFields.Columns[0].Width = -2;
    this._lvSelectedFields.Columns[0].Width = -2;
    this.LoadElements();
  }

  internal ImbaseTableViewEditorForm(XmlNode rootNode, object dtRoles, object selectedRole)
  {
    this.InitializeComponent();
    if (dtRoles == null)
      return;
    this._cmbRole.DisplayMember = "Caption";
    this._cmbRole.ValueMember = "Guid";
    this._cmbRole.DataSource = dtRoles;
    this._cmbRole.SelectedValue = selectedRole != null ? selectedRole : this._cmbRole.Items[0];
    this._cmbRole.SelectedValueChanged += new EventHandler(this.On_cmbRole_SelectedValueChanged);
    if (Statics.IconSrv != null)
      this._lvAllFields.SmallImageList = this._lvSelectedFields.SmallImageList = Statics.IconSrv.ImageList;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.InnerXml = rootNode.OuterXml;
    this._rolesNode = xmlDocument.FirstChild;
    this._currentNode = this._rolesNode.SelectSingleNode($"//Role[@Guid='{this._cmbRole.SelectedValue}']");
    this.CreateItems(this._currentNode.ChildNodes);
    this._lvAllFields.Columns[0].Width = -2;
    this._lvSelectedFields.Columns[0].Width = -2;
    this.LoadElements();
  }

  private void On_btnLeftRight_Click(object sender, EventArgs e)
  {
    int int16 = (int) Convert.ToInt16((sender as Button).Tag);
    ListView listView1;
    switch (int16)
    {
      case 0:
      case 1:
        listView1 = this._lvAllFields;
        break;
      default:
        listView1 = this._lvSelectedFields;
        break;
    }
    ListView listView2 = listView1;
    ListView listView3 = int16 == 0 || int16 == 1 ? this._lvSelectedFields : this._lvAllFields;
    switch (int16)
    {
      case 0:
      case 3:
        while (listView2.Items.Count > 0)
        {
          ListViewItem listViewItem = listView2.Items[0];
          listViewItem.Selected = false;
          listView2.Items.Remove(listViewItem);
          listView3.Items.Add(listViewItem);
        }
        this._btnRightAll.Enabled = int16 == 3;
        this._btnLeftAll.Enabled = int16 == 0;
        if (listView3.SelectedItems.Count == 0)
          listView3.Items[0].Selected = listView3.Items[0].Focused = true;
        listView3.Focus();
        break;
      case 1:
      case 2:
        while (listView3.SelectedItems.Count > 0)
          listView3.SelectedItems[0].Selected = false;
        while (listView2.SelectedItems.Count > 0)
        {
          ListViewItem selectedItem = listView2.SelectedItems[0];
          listView2.Items.Remove(selectedItem);
          listView3.Items.Add(selectedItem);
          selectedItem.Focused = true;
        }
        if (int16 == 1)
          this._btnLeftAll.Enabled = true;
        else
          this._btnRightAll.Enabled = true;
        if (listView2.Items.Count > 0)
        {
          if (listView2.FocusedItem != null)
          {
            listView2.Items[listView2.FocusedItem.Index].Selected = true;
            break;
          }
          listView2.Items[0].Selected = true;
          break;
        }
        if (int16 == 1)
          this._btnRightAll.Enabled = false;
        else
          this._btnLeftAll.Enabled = false;
        listView3.Focus();
        break;
    }
    (sender as Button).Focus();
    this._btnOK.Enabled = this._lvSelectedFields.Items.Count > 0;
  }

  private void On_btnUpDown_Click(object sender, EventArgs e)
  {
    ListViewItem selectedItem = this._lvSelectedFields.SelectedItems[0];
    int index = selectedItem.Index;
    this._lvSelectedFields.Items.Remove(selectedItem);
    switch (Convert.ToInt16((sender as Button).Tag))
    {
      case 4:
        this._lvSelectedFields.Items.Insert(0, selectedItem);
        break;
      case 5:
        this._lvSelectedFields.Items.Insert(index - 1, selectedItem);
        break;
      case 6:
        this._lvSelectedFields.Items.Insert(index + 1, selectedItem);
        break;
      case 7:
        this._lvSelectedFields.Items.Insert(this._lvSelectedFields.Items.Count, selectedItem);
        break;
    }
    selectedItem.Selected = selectedItem.Focused = true;
    (sender as Button).Focus();
    this._btnOK.Enabled = true;
  }

  private void On_cmbRole_SelectedValueChanged(object sender, EventArgs e)
  {
    if (this._cmbRole.SelectedValue == null)
      return;
    this.Save();
    this._currentNode = this._rolesNode.SelectSingleNode($"//{this.ROLE}[@{this.GUID_ATTR}='{this._cmbRole.SelectedValue}']");
    if (this._currentNode == null)
    {
      XmlElement element = this._rolesNode.OwnerDocument.CreateElement(this.ROLE);
      element.SetAttribute(this.GUID_ATTR, this._cmbRole.SelectedValue.ToString());
      this._currentNode = (XmlNode) element;
      this._rolesNode.AppendChild(this._currentNode);
    }
    this.LoadElements();
  }

  private void On_lvAllFields_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnRight.Enabled = this._lvAllFields.SelectedItems.Count > 0;
  }

  private void On_lvs_DoubleClick(object sender, EventArgs e)
  {
    this.On_btnLeftRight_Click(sender as ListView == this._lvAllFields ? (object) this._btnRight : (object) this._btnLeft, e);
  }

  private void On_lvs_SizeChanged(object sender, EventArgs e)
  {
    if (!(sender is ListView listView) || listView.Columns.Count == 0)
      return;
    if (listView.Columns[0] == null)
      return;
    try
    {
      listView.Columns[0].Width = -2;
    }
    catch
    {
    }
  }

  private void On_lvSelectedFields_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnLeft.Enabled = this._lvSelectedFields.SelectedItems.Count > 0;
    if (this._lvSelectedFields.SelectedItems.Count == 1 && this._lvSelectedFields.Items.Count > 1)
    {
      if (this._lvSelectedFields.Items[0].Selected)
      {
        this._btnTop.Enabled = this._btnUp.Enabled = false;
        this._btnDown.Enabled = this._btnBottom.Enabled = true;
      }
      else if (this._lvSelectedFields.Items[this._lvSelectedFields.Items.Count - 1].Selected)
      {
        this._btnTop.Enabled = this._btnUp.Enabled = true;
        this._btnDown.Enabled = this._btnBottom.Enabled = false;
      }
      else
        this._btnTop.Enabled = this._btnUp.Enabled = this._btnDown.Enabled = this._btnBottom.Enabled = true;
    }
    else
      this._btnTop.Enabled = this._btnUp.Enabled = this._btnDown.Enabled = this._btnBottom.Enabled = false;
  }

  private void CreateItems(XmlNodeList nodes)
  {
    if (nodes == null || nodes.Count == 0)
      return;
    this._items = new Dictionary<Guid, ListViewItem>(nodes.Count);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (XmlNode node in nodes)
      {
        XmlAttribute attribute = node.Attributes[this.GUID_ATTR];
        if (GuidHelper.IsGuid(attribute.Value))
        {
          Guid guid = new Guid(attribute.Value);
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(guid);
          if (attributeType != null)
          {
            int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.AttributeType);
            this._items.Add(guid, new ListViewItem(attributeType.Name, imageIndex)
            {
              Name = attribute.Value
            });
          }
        }
      }
    }
  }

  private void DisableButtons()
  {
    this._btnLeftAll.Enabled = this._btnLeft.Enabled = this._btnRight.Enabled = this._btnRightAll.Enabled = false;
    this._btnTop.Enabled = this._btnUp.Enabled = this._btnDown.Enabled = this._btnBottom.Enabled = false;
  }

  private void LoadElements()
  {
    this._lvAllFields.Items.Clear();
    this._lvSelectedFields.Items.Clear();
    this.DisableButtons();
    if (this._items == null || this._items.Values.Count == 0 || this._currentNode == null)
      return;
    foreach (ListViewItem listViewItem in this._items.Values)
      listViewItem.Selected = false;
    XmlNodeList childNodes = this._currentNode.ChildNodes;
    if (childNodes.Count != 0)
    {
      List<ImbaseTableViewEditorForm.ItemInfo> itemInfoList = new List<ImbaseTableViewEditorForm.ItemInfo>(childNodes.Count);
      int num = childNodes.Count - 1;
      foreach (XmlNode xmlNode in childNodes)
      {
        XmlAttribute attribute1 = xmlNode.Attributes[this.GUID_ATTR];
        if (GuidHelper.IsGuid(attribute1.Value))
        {
          ImbaseTableViewEditorForm.ItemInfo itemInfo = new ImbaseTableViewEditorForm.ItemInfo(new Guid(attribute1.Value));
          XmlAttribute attribute2 = xmlNode.Attributes[this.VISIBLE_ATTR];
          XmlAttribute attribute3 = xmlNode.Attributes[this.INDEX_ATTR];
          if (Convert.ToBoolean(attribute2.Value))
          {
            itemInfo.Visible = true;
            itemInfo.Index = Convert.ToInt32(attribute3.Value);
          }
          else
          {
            itemInfo.Visible = false;
            itemInfo.Index = Convert.ToInt32(num);
            --num;
          }
          itemInfoList.Add(itemInfo);
        }
      }
      itemInfoList.Sort(new Comparison<ImbaseTableViewEditorForm.ItemInfo>(ImbaseTableViewEditorForm.CompareItems));
      foreach (ImbaseTableViewEditorForm.ItemInfo itemInfo in itemInfoList)
      {
        if (this._items.ContainsKey(itemInfo.Guid))
        {
          if (itemInfo.Visible)
            this._lvSelectedFields.Items.Add(this._items[itemInfo.Guid]);
          else
            this._lvAllFields.Items.Add(this._items[itemInfo.Guid]);
        }
      }
    }
    else
    {
      foreach (ListViewItem listViewItem in this._items.Values)
        this._lvSelectedFields.Items.Add(listViewItem);
    }
    this.SetEnableButtons();
  }

  private void Save()
  {
    this.StoreData(this._lvSelectedFields, true, 0);
    this.StoreData(this._lvAllFields, false, this._lvSelectedFields.Items.Count);
  }

  private void SetEnableButtons()
  {
    if (this._lvAllFields.Items.Count > 0)
    {
      this._btnRightAll.Enabled = true;
      this._lvAllFields.Items[0].Selected = this._lvAllFields.Items[0].Focused = true;
    }
    else
      this._btnRightAll.Enabled = this._btnRight.Enabled = false;
    if (this._lvSelectedFields.Items.Count > 0)
    {
      this._btnLeftAll.Enabled = true;
      this._lvSelectedFields.Items[0].Selected = this._lvSelectedFields.Items[0].Focused = true;
    }
    else
    {
      this._btnLeftAll.Enabled = this._btnLeft.Enabled = false;
      this._btnTop.Enabled = this._btnUp.Enabled = this._btnDown.Enabled = this._btnBottom.Enabled = false;
    }
  }

  private void StoreData(ListView lv, bool visible, int startIndex)
  {
    foreach (ListViewItem listViewItem in lv.Items)
    {
      XmlNode xmlNode = this._currentNode.SelectSingleNode($"{this.COLUMN}[@{this.GUID_ATTR}='{listViewItem.Name}']");
      if (xmlNode != null)
      {
        XmlElement xmlElement = xmlNode as XmlElement;
        xmlElement.SetAttribute(this.INDEX_ATTR, startIndex.ToString());
        xmlElement.SetAttribute(this.VISIBLE_ATTR, visible.ToString());
      }
      else
      {
        XmlElement element = this._currentNode.OwnerDocument.CreateElement(this.COLUMN);
        element.SetAttribute(this.GUID_ATTR, listViewItem.Name);
        element.SetAttribute(this.INDEX_ATTR, startIndex.ToString());
        element.SetAttribute(this.VISIBLE_ATTR, visible.ToString());
        element.SetAttribute(this.WIDTH_ATTR, "100");
        this._currentNode.AppendChild((XmlNode) element);
      }
      ++startIndex;
    }
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    if (this._pnlTop.Visible)
      this._cmbRole.SelectedValueChanged -= new EventHandler(this.On_cmbRole_SelectedValueChanged);
    if (this.DialogResult != DialogResult.OK || !this._btnOK.Enabled)
      return;
    DialogResult dialogResult = MessageBox.Show((IWin32Window) this, LocalizationHolder.rm.GetString("Imbase.Client_125"), LocalizationHolder.rm.GetString("Imbase.Table.ChangeKeep.Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    this.DialogResult = dialogResult;
    if (dialogResult == DialogResult.No)
      return;
    this.Save();
  }

  private static int CompareItems(
    ImbaseTableViewEditorForm.ItemInfo x,
    ImbaseTableViewEditorForm.ItemInfo y)
  {
    return x.Index - y.Index;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseTableViewEditorForm));
    this._splContainer = new SplitContainer();
    this._tlpAllFields = new TableLayoutPanel();
    this._btnLeftAll = new Button();
    this._imgList = new ImageList(this.components);
    this._btnLeft = new Button();
    this._btnRight = new Button();
    this._btnRightAll = new Button();
    this._lvAllFields = new ListView();
    this.colName_All = new ColumnHeader();
    this._lbAllFields = new Label();
    this._tlpSelecetdFields = new TableLayoutPanel();
    this._lbSelectedFields = new Label();
    this._btnTop = new Button();
    this._btnUp = new Button();
    this._btnBottom = new Button();
    this._btnDown = new Button();
    this._lvSelectedFields = new ListView();
    this.colName_Selected = new ColumnHeader();
    this._pnlTop = new Panel();
    this._cmbRole = new ComboBox();
    this._lbProfile = new Label();
    this._pnlBottom = new Panel();
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._splContainer.BeginInit();
    this._splContainer.Panel1.SuspendLayout();
    this._splContainer.Panel2.SuspendLayout();
    this._splContainer.SuspendLayout();
    this._tlpAllFields.SuspendLayout();
    this._tlpSelecetdFields.SuspendLayout();
    this._pnlTop.SuspendLayout();
    this._pnlBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._splContainer, "_splContainer");
    this._splContainer.Name = "_splContainer";
    this._splContainer.Panel1.Controls.Add((Control) this._tlpAllFields);
    this._splContainer.Panel2.Controls.Add((Control) this._tlpSelecetdFields);
    componentResourceManager.ApplyResources((object) this._tlpAllFields, "_tlpAllFields");
    this._tlpAllFields.Controls.Add((Control) this._btnLeftAll, 1, 5);
    this._tlpAllFields.Controls.Add((Control) this._btnLeft, 1, 4);
    this._tlpAllFields.Controls.Add((Control) this._btnRight, 1, 3);
    this._tlpAllFields.Controls.Add((Control) this._btnRightAll, 1, 2);
    this._tlpAllFields.Controls.Add((Control) this._lvAllFields, 0, 1);
    this._tlpAllFields.Controls.Add((Control) this._lbAllFields, 0, 0);
    this._tlpAllFields.Name = "_tlpAllFields";
    componentResourceManager.ApplyResources((object) this._btnLeftAll, "_btnLeftAll");
    this._btnLeftAll.ImageList = this._imgList;
    this._btnLeftAll.Name = "_btnLeftAll";
    this._btnLeftAll.Tag = (object) "3";
    this._btnLeftAll.UseVisualStyleBackColor = true;
    this._btnLeftAll.Click += new EventHandler(this.On_btnLeftRight_Click);
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Transparent;
    this._imgList.Images.SetKeyName(0, "RightAll.ico");
    this._imgList.Images.SetKeyName(1, "Right.ico");
    this._imgList.Images.SetKeyName(2, "Left.ico");
    this._imgList.Images.SetKeyName(3, "LeftAll.ico");
    this._imgList.Images.SetKeyName(4, "Top.ico");
    this._imgList.Images.SetKeyName(5, "Up.ico");
    this._imgList.Images.SetKeyName(6, "Down.ico");
    this._imgList.Images.SetKeyName(7, "Bottom.ico");
    componentResourceManager.ApplyResources((object) this._btnLeft, "_btnLeft");
    this._btnLeft.ImageList = this._imgList;
    this._btnLeft.Name = "_btnLeft";
    this._btnLeft.Tag = (object) "2";
    this._btnLeft.UseVisualStyleBackColor = true;
    this._btnLeft.Click += new EventHandler(this.On_btnLeftRight_Click);
    componentResourceManager.ApplyResources((object) this._btnRight, "_btnRight");
    this._btnRight.ImageList = this._imgList;
    this._btnRight.Name = "_btnRight";
    this._btnRight.Tag = (object) "1";
    this._btnRight.UseVisualStyleBackColor = true;
    this._btnRight.Click += new EventHandler(this.On_btnLeftRight_Click);
    componentResourceManager.ApplyResources((object) this._btnRightAll, "_btnRightAll");
    this._btnRightAll.ImageList = this._imgList;
    this._btnRightAll.Name = "_btnRightAll";
    this._btnRightAll.Tag = (object) "0";
    this._btnRightAll.UseVisualStyleBackColor = true;
    this._btnRightAll.Click += new EventHandler(this.On_btnLeftRight_Click);
    this._lvAllFields.Columns.AddRange(new ColumnHeader[1]
    {
      this.colName_All
    });
    componentResourceManager.ApplyResources((object) this._lvAllFields, "_lvAllFields");
    this._lvAllFields.FullRowSelect = true;
    this._lvAllFields.HeaderStyle = ColumnHeaderStyle.None;
    this._lvAllFields.HideSelection = false;
    this._lvAllFields.Name = "_lvAllFields";
    this._tlpAllFields.SetRowSpan((Control) this._lvAllFields, 6);
    this._lvAllFields.UseCompatibleStateImageBehavior = false;
    this._lvAllFields.View = View.Details;
    this._lvAllFields.SelectedIndexChanged += new EventHandler(this.On_lvAllFields_SelectedIndexChanged);
    this._lvAllFields.SizeChanged += new EventHandler(this.On_lvs_SizeChanged);
    this._lvAllFields.DoubleClick += new EventHandler(this.On_lvs_DoubleClick);
    componentResourceManager.ApplyResources((object) this.colName_All, "colName_All");
    componentResourceManager.ApplyResources((object) this._lbAllFields, "_lbAllFields");
    this._lbAllFields.Name = "_lbAllFields";
    componentResourceManager.ApplyResources((object) this._tlpSelecetdFields, "_tlpSelecetdFields");
    this._tlpSelecetdFields.Controls.Add((Control) this._lbSelectedFields, 0, 0);
    this._tlpSelecetdFields.Controls.Add((Control) this._btnTop, 1, 2);
    this._tlpSelecetdFields.Controls.Add((Control) this._btnUp, 1, 3);
    this._tlpSelecetdFields.Controls.Add((Control) this._btnBottom, 1, 5);
    this._tlpSelecetdFields.Controls.Add((Control) this._btnDown, 1, 4);
    this._tlpSelecetdFields.Controls.Add((Control) this._lvSelectedFields, 0, 1);
    this._tlpSelecetdFields.Name = "_tlpSelecetdFields";
    componentResourceManager.ApplyResources((object) this._lbSelectedFields, "_lbSelectedFields");
    this._lbSelectedFields.Name = "_lbSelectedFields";
    componentResourceManager.ApplyResources((object) this._btnTop, "_btnTop");
    this._btnTop.ImageList = this._imgList;
    this._btnTop.Name = "_btnTop";
    this._btnTop.Tag = (object) "4";
    this._btnTop.UseVisualStyleBackColor = true;
    this._btnTop.Click += new EventHandler(this.On_btnUpDown_Click);
    componentResourceManager.ApplyResources((object) this._btnUp, "_btnUp");
    this._btnUp.ImageList = this._imgList;
    this._btnUp.Name = "_btnUp";
    this._btnUp.Tag = (object) "5";
    this._btnUp.UseVisualStyleBackColor = true;
    this._btnUp.Click += new EventHandler(this.On_btnUpDown_Click);
    componentResourceManager.ApplyResources((object) this._btnBottom, "_btnBottom");
    this._btnBottom.ImageList = this._imgList;
    this._btnBottom.Name = "_btnBottom";
    this._btnBottom.Tag = (object) "7";
    this._btnBottom.UseVisualStyleBackColor = true;
    this._btnBottom.Click += new EventHandler(this.On_btnUpDown_Click);
    componentResourceManager.ApplyResources((object) this._btnDown, "_btnDown");
    this._btnDown.ImageList = this._imgList;
    this._btnDown.Name = "_btnDown";
    this._btnDown.Tag = (object) "6";
    this._btnDown.UseVisualStyleBackColor = true;
    this._btnDown.Click += new EventHandler(this.On_btnUpDown_Click);
    this._lvSelectedFields.Columns.AddRange(new ColumnHeader[1]
    {
      this.colName_Selected
    });
    componentResourceManager.ApplyResources((object) this._lvSelectedFields, "_lvSelectedFields");
    this._lvSelectedFields.FullRowSelect = true;
    this._lvSelectedFields.HeaderStyle = ColumnHeaderStyle.None;
    this._lvSelectedFields.HideSelection = false;
    this._lvSelectedFields.Name = "_lvSelectedFields";
    this._tlpSelecetdFields.SetRowSpan((Control) this._lvSelectedFields, 6);
    this._lvSelectedFields.UseCompatibleStateImageBehavior = false;
    this._lvSelectedFields.View = View.Details;
    this._lvSelectedFields.SelectedIndexChanged += new EventHandler(this.On_lvSelectedFields_SelectedIndexChanged);
    this._lvSelectedFields.SizeChanged += new EventHandler(this.On_lvs_SizeChanged);
    this._lvSelectedFields.DoubleClick += new EventHandler(this.On_lvs_DoubleClick);
    componentResourceManager.ApplyResources((object) this.colName_Selected, "colName_Selected");
    this._pnlTop.Controls.Add((Control) this._cmbRole);
    this._pnlTop.Controls.Add((Control) this._lbProfile);
    componentResourceManager.ApplyResources((object) this._pnlTop, "_pnlTop");
    this._pnlTop.Name = "_pnlTop";
    this._cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbRole.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this._cmbRole, "_cmbRole");
    this._cmbRole.Name = "_cmbRole";
    componentResourceManager.ApplyResources((object) this._lbProfile, "_lbProfile");
    this._lbProfile.Name = "_lbProfile";
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    this._pnlBottom.Controls.Add((Control) this._btnOK);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._splContainer);
    this.Controls.Add((Control) this._pnlTop);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (ImbaseTableViewEditorForm);
    this.ShowInTaskbar = false;
    this._splContainer.Panel1.ResumeLayout(false);
    this._splContainer.Panel2.ResumeLayout(false);
    this._splContainer.EndInit();
    this._splContainer.ResumeLayout(false);
    this._tlpAllFields.ResumeLayout(false);
    this._tlpSelecetdFields.ResumeLayout(false);
    this._pnlTop.ResumeLayout(false);
    this._pnlTop.PerformLayout();
    this._pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private class ItemInfo
  {
    internal Guid Guid = Guid.Empty;
    internal bool Visible;
    internal int Index;

    internal ItemInfo(Guid guid) => this.Guid = guid;
  }
}
