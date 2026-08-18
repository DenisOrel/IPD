// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Node.NumNodeView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Node;

/// <summary>Summary description for NumNodeView.</summary>
[ViewDescriptionProvider(typeof (NumNodeView.NumNodeViewDescriptionProvider))]
public class NumNodeView : UserControl, IView
{
  /// <summary>
  /// 
  /// </summary>
  internal long _objectID;
  /// <summary>
  /// 
  /// </summary>
  private TechNumerationNode _numNode;
  /// <summary>
  /// 
  /// </summary>
  private bool _modified;
  private Label lblNumerationMode;
  private ComboBox cbNumerationMode;
  private ErrorProvider warningProvider;
  /// <summary>
  /// 
  /// </summary>
  private IContainer components;
  internal Button btnApply;
  internal Button btnCancel;
  private GroupBox grbObject;
  private Label lblObjectType;
  private Label lblAttribute;
  private Button btnObjectType;
  private Button tbnAttribute;
  private TextBox tbxObjectType;
  private TextBox tbxAttribute;
  private GroupBox grbParentObject;
  private ListBox lbParentObject;
  private GroupBox groupBox1;
  private ContextMenuStrip cmsObjectTypes;
  private ToolStripMenuItem tsmiObjTypeEdit;
  private ContextMenuStrip cmsRelTypes;
  private ToolStripMenuItem tsmiRelTypeEdit;
  private ListBox lblRelTypes;

  /// <summary>
  /// 
  /// </summary>
  private void ObjectTypeEdit()
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("TechCard.Client_225"), typeof (ObjectTypeFolder), false);
    selectorForm.InitSelectionAsType(new ArrayList((ICollection) new int[1]
    {
      MetaDataHelper.GetObjectTypeID(this._numNode.ObjectTypeGuid)
    }), new ArrayList((ICollection) new System.Type[1]
    {
      typeof (ObjectTypeFolder)
    }));
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0)
      return;
    this._numNode.ObjectTypeGuid = MetaDataHelper.GetObjectTypeGuid((int) selectorForm.IDList[0]);
    this.UpdateControls();
    this.Modified = true;
  }

  /// <summary>
  /// 
  /// </summary>
  private void AttributeTypeEdit()
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      attributesSelectDlg.SelectedAttributeIDOnStartup(MetaDataHelper.GetAttributeID((object) this._numNode.AttributeTypeGuid));
      if (this._numNode != null && this._numNode.ObjectTypeGuid != Guid.Empty)
        attributesSelectDlg.LoadAttrDialogForObjectsTypes(this._numNode.ObjectTypeGuid);
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count == 0)
        return;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributesSelectDlg.SelectedAttributesID[0]);
      if (attributeType == null || attributeType.AttributeGuid == this._numNode.AttributeTypeGuid)
        return;
      this._numNode.AttributeTypeGuid = attributeType.AttributeGuid;
      this.UpdateControls();
      this.Modified = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void ObjTypesEdit()
  {
    ArrayList idList = new ArrayList();
    ArrayList typeList = new ArrayList();
    foreach (Guid parentObjectTypeGuid in this._numNode.ParentObjectTypeGuids)
    {
      idList.Add((object) MetaDataHelper.GetObjectTypeID(parentObjectTypeGuid));
      typeList.Add((object) typeof (ObjectTypeFolder));
    }
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("TechCard.Client_227"), typeof (ObjectTypeFolder), true);
    List<int> withApplicabilities = MetaDataHelper.GetObjectTypesWithApplicabilities();
    selectorForm.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(withApplicabilities.ToArray(), true, true);
    selectorForm.NodeSelectorFilter = (INodeSelectorFilter) new NodeSelectorFilter();
    selectorForm.InitSelectionAsType(idList, typeList);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0)
      return;
    this._numNode.ParentObjectTypeGuids.Clear();
    for (int index = 0; index <= selectorForm.IDList.Count - 1; ++index)
    {
      if (!((System.Type) selectorForm.TypeList[index] != typeof (ObjectTypeFolder)))
        this._numNode.ParentObjectTypeGuids.Add(MetaDataHelper.GetObjectTypeGuid((int) selectorForm.IDList[index]));
    }
    this.UpdateControls();
    this.Modified = true;
  }

  /// <summary>
  /// 
  /// </summary>
  private void RelTypesEdit()
  {
    ArrayList idList = new ArrayList();
    ArrayList typeList = new ArrayList();
    foreach (Guid relationTypeGuid in this._numNode.RelationTypeGuids)
    {
      idList.Add((object) MetaDataHelper.GetRelationTypeID(relationTypeGuid));
      typeList.Add((object) typeof (RelationTypeFolder));
    }
    SelectorForm selectorForm = new SelectorForm(typeof (RelationTypesFolder), LocalizationHolder.rm.GetString("TechCard.Client_500"), typeof (RelationTypeFolder), true);
    List<int> list = MetaDataHelper.GetRelationTypesList().Select<IMSRelationType, int>((Func<IMSRelationType, int>) (item => item.RelationTypeID)).ToList<int>();
    selectorForm.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(list.ToArray(), true, true);
    selectorForm.NodeSelectorFilter = (INodeSelectorFilter) new NodeSelectorFilter(new int[1]
    {
      6
    });
    selectorForm.InitSelectionAsType(idList, typeList);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0)
      return;
    this._numNode.RelationTypeGuids.Clear();
    for (int index = 0; index <= selectorForm.IDList.Count - 1; ++index)
    {
      if (!((System.Type) selectorForm.TypeList[index] != typeof (RelationTypeFolder)))
        this._numNode.RelationTypeGuids.Add(MetaDataHelper.GetRelationTypeGuid((int) selectorForm.IDList[index]));
    }
    this.UpdateControls();
    this.Modified = true;
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateControls()
  {
    this.tbxObjectType.TextChanged -= new EventHandler(this.tbx_TextChanged);
    try
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(this._numNode.ObjectTypeGuid);
      this.tbxObjectType.Text = objectType != null ? objectType.ObjectTypeName : string.Empty;
    }
    finally
    {
      this.tbxObjectType.TextChanged += new EventHandler(this.tbx_TextChanged);
    }
    this.tbxAttribute.TextChanged -= new EventHandler(this.tbx_TextChanged);
    try
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this._numNode.AttributeTypeGuid);
      this.tbxAttribute.Text = attributeType != null ? attributeType.Name : string.Empty;
    }
    finally
    {
      this.tbxAttribute.TextChanged += new EventHandler(this.tbx_TextChanged);
    }
    this.lbParentObject.BeginUpdate();
    try
    {
      this.lbParentObject.Items.Clear();
      this.lbParentObject.Items.AddRange((object[]) this._numNode.ParentObjectTypeGuids.Select<Guid, ObjectWrapper>((Func<Guid, ObjectWrapper>) (item => new ObjectWrapper(item))).ToArray<ObjectWrapper>());
    }
    finally
    {
      this.lbParentObject.EndUpdate();
    }
    this.lblRelTypes.BeginUpdate();
    try
    {
      this.lblRelTypes.Items.Clear();
      this.lblRelTypes.Items.AddRange((object[]) this._numNode.RelationTypeGuids.Select<Guid, RelationWrapper>((Func<Guid, RelationWrapper>) (item => new RelationWrapper(item))).ToArray<RelationWrapper>());
    }
    finally
    {
      this.lblRelTypes.EndUpdate();
    }
    this.cbNumerationMode.BeginUpdate();
    try
    {
      this.cbNumerationMode.Items.Clear();
      System.Type type = typeof (TechNumerationMode);
      foreach (TechNumerationMode techNumerationMode in Enum.GetValues(typeof (TechNumerationMode)).OfType<TechNumerationMode>())
      {
        MemberInfo memberInfo = type.GetMember(techNumerationMode.ToString())[0];
        if (!(memberInfo == (MemberInfo) null))
        {
          object[] customAttributes = memberInfo.GetCustomAttributes(typeof (BrowsableAttribute), false);
          BrowsableAttribute browsableAttribute = customAttributes == null || customAttributes.Length == 0 ? (BrowsableAttribute) null : customAttributes[0] as BrowsableAttribute;
          if (browsableAttribute == null || browsableAttribute.Browsable)
            this.cbNumerationMode.Items.Add((object) EnumTypeHelper.GetCaption((Enum) techNumerationMode));
        }
      }
      this.cbNumerationMode.SelectedIndex = this.cbNumerationMode.Items.IndexOf((object) EnumTypeHelper.GetCaption((Enum) this._numNode.NumerationMode));
    }
    finally
    {
      this.cbNumerationMode.EndUpdate();
    }
    this.warningProvider.SetError((Control) this.grbObject, string.Empty);
    if (string.IsNullOrEmpty(this._numNode.ScriptData))
      return;
    this.warningProvider.SetError((Control) this.grbObject, LocalizationHolder.rm.GetString("TechCard.Client_510"));
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NumNodeView));
    this.btnApply = new Button();
    this.btnCancel = new Button();
    this.grbObject = new GroupBox();
    this.cbNumerationMode = new ComboBox();
    this.lblNumerationMode = new Label();
    this.tbnAttribute = new Button();
    this.btnObjectType = new Button();
    this.lblObjectType = new Label();
    this.tbxObjectType = new TextBox();
    this.tbxAttribute = new TextBox();
    this.lblAttribute = new Label();
    this.grbParentObject = new GroupBox();
    this.lbParentObject = new ListBox();
    this.cmsObjectTypes = new ContextMenuStrip(this.components);
    this.tsmiObjTypeEdit = new ToolStripMenuItem();
    this.groupBox1 = new GroupBox();
    this.lblRelTypes = new ListBox();
    this.cmsRelTypes = new ContextMenuStrip(this.components);
    this.tsmiRelTypeEdit = new ToolStripMenuItem();
    this.warningProvider = new ErrorProvider(this.components);
    this.grbObject.SuspendLayout();
    this.grbParentObject.SuspendLayout();
    this.cmsObjectTypes.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.cmsRelTypes.SuspendLayout();
    ((ISupportInitialize) this.warningProvider).BeginInit();
    this.SuspendLayout();
    this.btnApply.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.grbObject.Controls.Add((Control) this.cbNumerationMode);
    this.grbObject.Controls.Add((Control) this.lblNumerationMode);
    this.grbObject.Controls.Add((Control) this.tbnAttribute);
    this.grbObject.Controls.Add((Control) this.btnObjectType);
    this.grbObject.Controls.Add((Control) this.lblObjectType);
    this.grbObject.Controls.Add((Control) this.tbxObjectType);
    this.grbObject.Controls.Add((Control) this.tbxAttribute);
    this.grbObject.Controls.Add((Control) this.lblAttribute);
    componentResourceManager.ApplyResources((object) this.grbObject, "grbObject");
    this.grbObject.Name = "grbObject";
    this.grbObject.TabStop = false;
    this.cbNumerationMode.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbNumerationMode.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.cbNumerationMode, "cbNumerationMode");
    this.cbNumerationMode.Name = "cbNumerationMode";
    this.cbNumerationMode.SelectedIndexChanged += new EventHandler(this.cbNumerationMode_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.lblNumerationMode, "lblNumerationMode");
    this.lblNumerationMode.Name = "lblNumerationMode";
    this.lblNumerationMode.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.tbnAttribute, "tbnAttribute");
    this.tbnAttribute.Name = "tbnAttribute";
    this.tbnAttribute.Click += new EventHandler(this.tbnAttribute_Click);
    componentResourceManager.ApplyResources((object) this.btnObjectType, "btnObjectType");
    this.btnObjectType.Name = "btnObjectType";
    this.btnObjectType.Click += new EventHandler(this.btnObjectType_Click);
    componentResourceManager.ApplyResources((object) this.lblObjectType, "lblObjectType");
    this.lblObjectType.Name = "lblObjectType";
    this.lblObjectType.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.tbxObjectType, "tbxObjectType");
    this.tbxObjectType.Name = "tbxObjectType";
    this.tbxObjectType.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.tbxAttribute, "tbxAttribute");
    this.tbxAttribute.Name = "tbxAttribute";
    this.tbxAttribute.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.lblAttribute, "lblAttribute");
    this.lblAttribute.Name = "lblAttribute";
    this.lblAttribute.Tag = (object) "";
    this.grbParentObject.Controls.Add((Control) this.lbParentObject);
    componentResourceManager.ApplyResources((object) this.grbParentObject, "grbParentObject");
    this.grbParentObject.Name = "grbParentObject";
    this.grbParentObject.TabStop = false;
    this.lbParentObject.ContextMenuStrip = this.cmsObjectTypes;
    componentResourceManager.ApplyResources((object) this.lbParentObject, "lbParentObject");
    this.lbParentObject.Name = "lbParentObject";
    this.cmsObjectTypes.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.tsmiObjTypeEdit
    });
    this.cmsObjectTypes.Name = "cmsObjectTypes";
    componentResourceManager.ApplyResources((object) this.cmsObjectTypes, "cmsObjectTypes");
    this.tsmiObjTypeEdit.Name = "tsmiObjTypeEdit";
    componentResourceManager.ApplyResources((object) this.tsmiObjTypeEdit, "tsmiObjTypeEdit");
    this.tsmiObjTypeEdit.Click += new EventHandler(this.tsmiObjTypeEdit_Click);
    this.groupBox1.Controls.Add((Control) this.lblRelTypes);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    this.lblRelTypes.ContextMenuStrip = this.cmsRelTypes;
    componentResourceManager.ApplyResources((object) this.lblRelTypes, "lblRelTypes");
    this.lblRelTypes.Name = "lblRelTypes";
    this.cmsRelTypes.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.tsmiRelTypeEdit
    });
    this.cmsRelTypes.Name = "cmsObjectTypes";
    componentResourceManager.ApplyResources((object) this.cmsRelTypes, "cmsRelTypes");
    this.tsmiRelTypeEdit.Name = "tsmiRelTypeEdit";
    componentResourceManager.ApplyResources((object) this.tsmiRelTypeEdit, "tsmiRelTypeEdit");
    this.tsmiRelTypeEdit.Click += new EventHandler(this.tsmiRelTypeEdit_Click);
    this.warningProvider.ContainerControl = (ContainerControl) this;
    componentResourceManager.ApplyResources((object) this.warningProvider, "warningProvider");
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.grbParentObject);
    this.Controls.Add((Control) this.grbObject);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnApply);
    this.Name = nameof (NumNodeView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) " ";
    this.grbObject.ResumeLayout(false);
    this.grbObject.PerformLayout();
    this.grbParentObject.ResumeLayout(false);
    this.cmsObjectTypes.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.cmsRelTypes.ResumeLayout(false);
    ((ISupportInitialize) this.warningProvider).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Конструктор</summary>
  public NumNodeView() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Флаг состояния</summary>
  public bool Modified
  {
    get => this._modified;
    set
    {
      if (this._modified == value)
        return;
      this._modified = this.btnApply.Enabled = this.btnCancel.Enabled = value;
    }
  }

  /// <summary>Загрузка инфы объекта</summary>
  internal void DataLoad()
  {
    if (this._numNode != null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._numNode = new TechNumerationNode();
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectID);
      if (dbObject != null)
        this._numNode.Load(dbObject, sessionKeeper.Session);
      this.UpdateControls();
      this.Modified = false;
    }
  }

  /// <summary>Сохранение инфы объекта</summary>
  internal void DataSave()
  {
    if (this._objectID == 0L || this._numNode == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._numNode.Save(sessionKeeper.Session.GetObject(this._objectID), sessionKeeper.Session);
      this.Modified = false;
    }
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this._objectID));
  }

  /// <summary>ImageIndex</summary>
  public int ImageIndex => -1;

  /// <summary>OrderID</summary>
  public int OrderID => 0;

  /// <summary>Caption</summary>
  public string Caption => LocalizationHolder.rm.GetString("TechCard.Client_222");

  /// <summary>Initialize</summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._numNode = (TechNumerationNode) null;
    this._objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
  }

  /// <summary>Deactivate</summary>
  /// <param name="nextView"></param>
  public void Deactivate(IView nextView)
  {
    if (!this.Modified || MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_223"), LocalizationHolder.rm.GetString(sc_19532.ssp_techcard_19533()), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.DataSave();
  }

  /// <summary>Activate</summary>
  /// <param name="previousView"></param>
  public void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    this.DataLoad();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnApply_Click(object sender, EventArgs e) => this.DataSave();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCancel_Click(object sender, EventArgs e) => this.DataLoad();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnObjectType_Click(object sender, EventArgs e) => this.ObjectTypeEdit();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbnAttribute_Click(object sender, EventArgs e) => this.AttributeTypeEdit();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbx_TextChanged(object sender, EventArgs e) => this.Modified = true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiObjTypeEdit_Click(object sender, EventArgs e) => this.ObjTypesEdit();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiRelTypeEdit_Click(object sender, EventArgs e) => this.RelTypesEdit();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbNumerationMode_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._numNode.NumerationMode = (TechNumerationMode) EnumTypeHelper.GetEnumValue(typeof (TechNumerationMode), this.cbNumerationMode.Text);
    this.Modified = true;
  }

  private sealed class NumNodeViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("TechCard.Client_222"),
        ImageIndex = -1,
        OrderID = 0
      };
    }
  }
}
