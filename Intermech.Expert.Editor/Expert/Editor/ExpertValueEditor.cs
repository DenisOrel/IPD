// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ExpertValueEditor
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Контрол редактор значения ExpertValue</summary>
internal class ExpertValueEditor : UserControl
{
  private Panel panel1;
  private TreeView treeView1;
  private Splitter splitter1;
  private System.ComponentModel.Container components;
  private ExpertValue _value;
  private DataType _valueType;
  private ContextMenu contextMenu1;
  private MenuItem mAdd;
  private MenuItem mDelete;
  private MenuItem mAdd_Packet;
  private MenuItem mAdd_Diap;
  private MenuItem mAdd_Value;
  private ExpertValue _current;
  private TreeNode _previousNode;
  private CommonTypeHolder _typeHolder;
  private ArrayList _posValues = new ArrayList();
  private ArrayList _descrValues = new ArrayList();
  private Control _editor;
  private Panel panView;
  private TextBox tbView;
  private Label label6;
  private string _caption = string.Empty;

  public ExpertValueEditor(CommonTypeHolder typeHolder)
  {
    this.InitializeComponent();
    this._typeHolder = typeHolder;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(this._typeHolder.AttributeType.Guid);
      this._caption = attributeType.Name;
      if (!attributeType.MultipleValued.Equals((object) MultiValueModes.SingleValueFromList))
        return;
      this._posValues = new ArrayList((ICollection) attributeType.GetPossibleValuesArray());
      this._descrValues.Clear();
      if (attributeType.AttributeType == FieldTypes.ftString)
        return;
      DataTable possibleValues = attributeType.GetPossibleValues();
      for (int index = 0; index < possibleValues.Rows.Count; ++index)
      {
        string str = Convert.ToString(possibleValues.Rows[index]["F_DESCRIPTION"]);
        if (str == "")
          str = Convert.ToString(this._posValues[index]);
        this._descrValues.Add((object) str);
      }
    }
  }

  public ExpertValueEditor(DataType valueType, CommonTypeHolder typeHolder)
    : this(typeHolder)
  {
    this._valueType = valueType;
  }

  public ExpertValue EditValue
  {
    get => this._value;
    set
    {
      this._value = value;
      this.UpdateControl();
      this.SaveToCurrent();
    }
  }

  public void UpdateCurrent() => this.SaveToCurrent();

  private void UpdateControl()
  {
    if (this._value != null)
    {
      this.treeView1.BeginUpdate();
      try
      {
        this.treeView1.Nodes.Clear();
        this.treeView1.Nodes.Add(this.Parse(this._value));
      }
      finally
      {
        this.treeView1.EndUpdate();
      }
      this.treeView1.SelectedNode = this.treeView1.Nodes[0];
      this.treeView1_AfterSelect((object) null, (TreeViewEventArgs) null);
      Panel panView = this.panView;
      Splitter splitter1 = this.splitter1;
      TreeView treeView1 = this.treeView1;
      DataType valueType = this._value.ValueType;
      int num1;
      bool flag1 = (num1 = valueType.Equals((object) DataType.Packet) ? 1 : 0) != 0;
      treeView1.Visible = num1 != 0;
      int num2;
      bool flag2 = (num2 = flag1 ? 1 : 0) != 0;
      splitter1.Visible = num2 != 0;
      int num3 = flag2 ? 1 : 0;
      panView.Visible = num3 != 0;
    }
    this.Enabled = this._value != null;
  }

  private TreeNode Parse(ExpertValue value)
  {
    TreeNode treeNode = new TreeNode();
    treeNode.Text = value.ToString();
    switch (value.ValueType)
    {
      case DataType.ObjectLink:
        treeNode.Text = new ObjectIDToCaption(Convert.ToInt64(value.Value)).ToString();
        break;
      case DataType.Packet:
        for (int index = 0; index < (value.Value as PacketValue).Count; ++index)
          treeNode.Nodes.Add(this.Parse((value.Value as PacketValue)[index]));
        break;
    }
    treeNode.Tag = (object) value;
    return treeNode;
  }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExpertValueEditor));
    this.panel1 = new Panel();
    this.treeView1 = new TreeView();
    this.splitter1 = new Splitter();
    this.contextMenu1 = new ContextMenu();
    this.mAdd = new MenuItem();
    this.mAdd_Packet = new MenuItem();
    this.mAdd_Diap = new MenuItem();
    this.mAdd_Value = new MenuItem();
    this.mDelete = new MenuItem();
    this.panView = new Panel();
    this.tbView = new TextBox();
    this.label6 = new Label();
    this.panView.SuspendLayout();
    this.SuspendLayout();
    this.panel1.AccessibleDescription = (string) null;
    this.panel1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.BackgroundImage = (Image) null;
    this.panel1.Font = (Font) null;
    this.panel1.Name = "panel1";
    this.treeView1.AccessibleDescription = (string) null;
    this.treeView1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.treeView1, "treeView1");
    this.treeView1.BackgroundImage = (Image) null;
    this.treeView1.Font = (Font) null;
    this.treeView1.FullRowSelect = true;
    this.treeView1.HideSelection = false;
    this.treeView1.Name = "treeView1";
    this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
    this.treeView1.MouseUp += new MouseEventHandler(this.treeView1_MouseUp);
    this.splitter1.AccessibleDescription = (string) null;
    this.splitter1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.BackgroundImage = (Image) null;
    this.splitter1.Font = (Font) null;
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.contextMenu1.MenuItems.AddRange(new MenuItem[2]
    {
      this.mAdd,
      this.mDelete
    });
    componentResourceManager.ApplyResources((object) this.contextMenu1, "contextMenu1");
    this.contextMenu1.Popup += new EventHandler(this.contextMenu1_Popup);
    componentResourceManager.ApplyResources((object) this.mAdd, "mAdd");
    this.mAdd.Index = 0;
    this.mAdd.MenuItems.AddRange(new MenuItem[3]
    {
      this.mAdd_Packet,
      this.mAdd_Diap,
      this.mAdd_Value
    });
    componentResourceManager.ApplyResources((object) this.mAdd_Packet, "mAdd_Packet");
    this.mAdd_Packet.Index = 0;
    this.mAdd_Packet.Click += new EventHandler(this.contextMenu1_Click);
    componentResourceManager.ApplyResources((object) this.mAdd_Diap, "mAdd_Diap");
    this.mAdd_Diap.Index = 1;
    this.mAdd_Diap.Click += new EventHandler(this.contextMenu1_Click);
    componentResourceManager.ApplyResources((object) this.mAdd_Value, "mAdd_Value");
    this.mAdd_Value.Index = 2;
    this.mAdd_Value.Click += new EventHandler(this.contextMenu1_Click);
    componentResourceManager.ApplyResources((object) this.mDelete, "mDelete");
    this.mDelete.Index = 1;
    this.mDelete.Click += new EventHandler(this.contextMenu1_Click);
    this.panView.AccessibleDescription = (string) null;
    this.panView.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.panView, "panView");
    this.panView.BackgroundImage = (Image) null;
    this.panView.Controls.Add((Control) this.tbView);
    this.panView.Controls.Add((Control) this.label6);
    this.panView.Font = (Font) null;
    this.panView.Name = "panView";
    this.tbView.AccessibleDescription = (string) null;
    this.tbView.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.tbView, "tbView");
    this.tbView.BackgroundImage = (Image) null;
    this.tbView.Font = (Font) null;
    this.tbView.Name = "tbView";
    this.tbView.ReadOnly = true;
    this.tbView.TabStop = false;
    this.label6.AccessibleDescription = (string) null;
    this.label6.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Font = (Font) null;
    this.label6.Name = "label6";
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.treeView1);
    this.Controls.Add((Control) this.panView);
    this.Font = (Font) null;
    this.Name = nameof (ExpertValueEditor);
    this.panView.ResumeLayout(false);
    this.panView.PerformLayout();
    this.ResumeLayout(false);
  }

  private void SaveToCurrent()
  {
    if (this._current != null && this._editor != null)
    {
      switch (this._current.ValueType)
      {
        case DataType.Measured:
          this._current.Value = (this._editor as SingleValueEditor).Value;
          break;
        case DataType.ObjectLink:
          this._current.Value = (object) Convert.ToInt64((this._editor as SingleValueEditor).Value);
          break;
        case DataType.Packet:
          break;
        case DataType.Diap:
          this._current.Value = (object) (this._editor as DoubleValueEditor).Value;
          break;
        default:
          string str = Convert.ToString((this._editor as SingleValueEditor).Value);
          if (str == "")
          {
            this._current.Value = (object) null;
            break;
          }
          TypeConverter converter = TypeDescriptor.GetConverter(DataTypeConvertor.DataType2Type(this._current.ValueType));
          object obj = (this._editor as SingleValueEditor).Value;
          try
          {
            this._current.Value = !(obj.GetType() == typeof (DateTime)) ? converter.ConvertFrom((object) str) : obj;
            break;
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Expert.Editor_571"), (object) Convert.ToString(obj)), LocalizationHolder.rm.GetString("Expert.Editor_341"), MessageBoxButtons.OK);
            throw;
          }
      }
    }
    this.treeView1.BeginUpdate();
    try
    {
      this.UpdateNode(this.treeView1.Nodes[0]);
    }
    finally
    {
      this.treeView1.EndUpdate();
    }
    this.tbView.Text = this._value.ToString();
  }

  private void UpdateNode(TreeNode node)
  {
    ExpertValue tag = node.Tag as ExpertValue;
    node.Text = tag.ToString();
    if (tag.ValueType == DataType.ObjectLink)
      node.Text = new ObjectIDToCaption(Convert.ToInt64(tag.Value)).ToString();
    foreach (TreeNode node1 in node.Nodes)
      this.UpdateNode(node1);
  }

  private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
  {
    try
    {
      this.SaveToCurrent();
    }
    catch
    {
      if (this._previousNode != null)
      {
        this.treeView1.AfterSelect -= new TreeViewEventHandler(this.treeView1_AfterSelect);
        this.treeView1.SelectedNode = this._previousNode;
        this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
      }
      throw;
    }
    if (this._editor != null)
    {
      this._editor.Parent = (Control) null;
      this._editor = (Control) null;
    }
    TreeNode selectedNode = this.treeView1.SelectedNode;
    this._previousNode = selectedNode;
    if (selectedNode != null)
    {
      if (!(selectedNode.Tag is ExpertValue tag))
        return;
      switch (tag.ValueType)
      {
        case DataType.Packet:
          if (this._editor != null)
          {
            this._editor.Parent = (Control) this.panel1;
            this._editor.Dock = DockStyle.Top;
            this._editor.BringToFront();
          }
          this._current = tag;
          break;
        case DataType.Diap:
          this._editor = (Control) new DoubleValueEditor(this._typeHolder, this._valueType, (IList) this._posValues);
          if (this._valueType.Equals((object) DataType.Boolean))
            (this._editor as DoubleValueEditor).Caption = this._caption;
          (this._editor as DoubleValueEditor).Value = tag.Value as DiapValue;
          goto case DataType.Packet;
        default:
          this._editor = (Control) new SingleValueEditor(this._typeHolder, tag.ValueType, (IList) this._posValues, (IList) this._descrValues);
          if (tag.ValueType.Equals((object) DataType.Boolean))
            (this._editor as SingleValueEditor).Caption = this._caption;
          (this._editor as SingleValueEditor).Value = tag.Value;
          goto case DataType.Packet;
      }
    }
    else
      this._current = (ExpertValue) null;
  }

  private void treeView1_MouseUp(object sender, MouseEventArgs e)
  {
    if (!e.Button.Equals((object) MouseButtons.Right))
      return;
    this.treeView1.SelectedNode = this.treeView1.GetNodeAt(e.X, e.Y);
    this.treeView1_AfterSelect((object) null, (TreeViewEventArgs) null);
    this.contextMenu1.Show((Control) this.treeView1, new Point(e.X, e.Y));
  }

  private void contextMenu1_Popup(object sender, EventArgs e)
  {
    bool flag1 = false;
    TreeNode selectedNode = this.treeView1.SelectedNode;
    if (selectedNode != null && selectedNode.Tag is ExpertValue tag)
    {
      bool flag2 = this._valueType.Equals((object) DataType.ObjectLink) || this._valueType.Equals((object) DataType.Boolean);
      this.mAdd_Packet.Enabled = tag.ValueType.Equals((object) DataType.Packet);
      this.mAdd_Diap.Enabled = this.mAdd_Packet.Enabled && !flag2;
      this.mAdd_Value.Enabled = this.mAdd_Packet.Enabled;
      flag1 = true;
    }
    this.mAdd.Enabled = flag1;
    this.mDelete.Enabled = selectedNode != null;
  }

  private void contextMenu1_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.treeView1.SelectedNode;
    TreeNode parent = selectedNode.Parent;
    ExpertValue tag = selectedNode.Tag as ExpertValue;
    MenuItem menuItem = sender as MenuItem;
    if (menuItem.Equals((object) this.mAdd_Packet))
    {
      ExpertValue expValue = ExpertValue.Empty(DataType.Packet);
      selectedNode.Nodes.Add(this.Parse(expValue));
      if (!tag.ValueType.Equals((object) DataType.Packet))
        return;
      (tag.Value as PacketValue).Add(expValue);
    }
    else if (menuItem.Equals((object) this.mAdd_Diap))
    {
      ExpertValue expValue = new ExpertValue(new DiapValue()
      {
        Low = new ExpertValue(this._valueType, (object) ExpertValue.Empty(this._valueType)),
        High = new ExpertValue(this._valueType, (object) ExpertValue.Empty(this._valueType))
      });
      selectedNode.Nodes.Add(this.Parse(expValue));
      if (!tag.ValueType.Equals((object) DataType.Packet))
        return;
      (tag.Value as PacketValue).Add(expValue);
    }
    else if (menuItem.Equals((object) this.mAdd_Value))
    {
      ExpertValue expValue = ExpertValue.Empty(this._valueType);
      selectedNode.Nodes.Add(this.Parse(expValue));
      if (!tag.ValueType.Equals((object) DataType.Packet))
        return;
      (tag.Value as PacketValue).Add(expValue);
    }
    else
    {
      if (!menuItem.Equals((object) this.mDelete))
        return;
      if (parent == null)
      {
        if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_122"), LocalizationHolder.rm.GetString("Expert.Editor_123"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals((object) DialogResult.Yes))
        {
          (this._value.Value as PacketValue).Clear();
          selectedNode.Nodes.Clear();
        }
      }
      else
      {
        ((parent.Tag as ExpertValue).Value as PacketValue).Remove(tag);
        selectedNode.Remove();
      }
      this.treeView1.SelectedNode = this.treeView1.Nodes[0];
      this.treeView1_AfterSelect((object) null, (TreeViewEventArgs) null);
    }
  }
}
