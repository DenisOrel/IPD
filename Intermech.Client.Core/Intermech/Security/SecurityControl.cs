
// Type: Intermech.Security.SecurityControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.LifeCycles;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Security;

/// <summary>Компонент безопасности</summary>
public class SecurityControl : UserControl
{
  private IContainer components;
  private Button btnAdd;
  private Button btnDelete;
  private Button btnStatus;
  private CheckedListBox optionsLB;
  private SecurityClass securityClass = new SecurityClass();
  private ActionCategoryStates actionCategoryStates = new ActionCategoryStates();
  private TreeList objectsTreeList;
  private TreeListColumn nameObjectTreeListColumn;
  internal RepositoryItemCheckEdit enabledRepositoryItemCheckEdit;
  internal RepositoryItemCheckEdit disabledRepositoryItemCheckEdit;
  private bool differencesFound;
  private bool readonlyMode;
  private bool externalReadOnly;
  private bool assignData;
  private object data;
  private RightConditionList rightConditionList = new RightConditionList();
  private TreeListNode mainNode;
  private List<TreeListNode> grantAlwaysAccessNodeList = new List<TreeListNode>();
  private TreeListNode acFullNode;
  private TreeListNode acReadNode;
  private TreeListNode acWriteNode;
  private TreeListNode acAdminNode;
  private TreeListColumn rightsColumn;
  private TreeListColumn enabledColumn;
  private TreeListColumn disabledColumn;
  private TreeList rightsTreeList;
  private bool blockOnChange_FillRights;
  private bool blockOnChange_ProcessVirtualNodes;
  private bool blockOnChange_CheckStateChanged;
  private bool blockOnChange_Inherit;
  private bool stateChangedEventAssigned4Enabled;
  private bool stateChangedEventAssigned4Disabled;
  private long sessionUserId;
  private Panel panel1;
  private Splitter splitter1;
  private Panel panel2;
  private ImageList stateImageList;
  private ValidDateForm validDateForm;
  private RightConditionForm rightConditionForm;
  private object[] ids;
  private Panel panel4;
  private Panel panel6;
  private Panel panel5;
  private CheckBox cbInherit;
  private ToolTip toolTip;
  private StatusStrip statusStrip1;
  private ToolStripStatusLabel labelRestrict;
  private ToolStripStatusLabel labelDiffTypesRestrict;
  private ToolStripStatusLabel labelShortRights;
  private Button btnGrantAlwaysAccess;
  private Button btnCondition;
  private TreeListColumn condObjectTreeListColumn;
  private ISecurityCallback iSecurityCallback;
  /// <summary>Индекс колонки условия</summary>
  private static readonly int ConstConditionColIndex = 1;
  private bool blockOnCheckedChanged;

  public bool Readonly
  {
    get => this.externalReadOnly;
    set => this.externalReadOnly = value;
  }

  /// <summary>
  /// Сфокусированный пользователь/группа/роль - это дает позиционирование на первого встреченного юзера-роль.
  /// Если это не Int64, значит разбирать как object[]{ "имя related security", object } - это дает полное позиционирование с учетом related security
  /// </summary>
  public object FocusedUserId
  {
    get
    {
      string str = (string) null;
      object obj = (object) null;
      if (this.objectsTreeList.FocusedNode != null)
      {
        if (this.objectsTreeList.FocusedNode.Tag is SecurityNodeClass)
        {
          obj = (object) ((SecurityNodeClass) this.objectsTreeList.FocusedNode.Tag).QuickObjectInfo.ObjectID;
          if (this.objectsTreeList.FocusedNode.ParentNode != null && this.objectsTreeList.FocusedNode.ParentNode.Tag is SecurityHolderClass)
            str = ((SecurityHolderClass) this.objectsTreeList.FocusedNode.ParentNode.Tag).ObjectName;
        }
        if (this.objectsTreeList.FocusedNode.Tag is SecurityHolderClass)
          str = ((SecurityHolderClass) this.objectsTreeList.FocusedNode.Tag).ObjectName;
      }
      if (obj == null && str == null)
        return (object) null;
      return (object) new object[2]{ (object) str, obj };
    }
    set
    {
      if (value == null)
        return;
      string rlsName = (string) null;
      object obj = (object) null;
      if (value is long)
        obj = (object) Convert.ToInt64(value);
      else if (value is object[] && ((object[]) value).Length > 1)
      {
        if (((object[]) value)[0] != null)
          rlsName = Convert.ToString(((object[]) value)[0]);
        if (((object[]) value)[1] != null)
          obj = (object) Convert.ToInt64(((object[]) value)[1]);
      }
      TreeListNode treeListNode1 = (TreeListNode) null;
      TreeListNode treeListNode2 = (TreeListNode) null;
      if (rlsName != null)
        treeListNode2 = this.FindRLSNode(rlsName);
      if (treeListNode2 != null)
        treeListNode1 = obj == null ? treeListNode2 : this.FindUGRNodeCustom(Convert.ToInt64(obj), treeListNode2.Nodes) ?? treeListNode2;
      else if (obj != null)
        treeListNode1 = this.FindUGRNode(Convert.ToInt64(obj));
      if (treeListNode1 == null)
        return;
      this.objectsTreeList.FocusedNode = treeListNode1;
    }
  }

  public bool ActualReadonly
  {
    get
    {
      if (this.externalReadOnly || this.objectsTreeList.FocusedNode == null)
        return true;
      bool isBaseClass = false;
      SecurityHolderClass[] securityHolderClass1 = this.GetSecurityHolderClass(this.objectsTreeList.FocusedNode, out isBaseClass);
      if (securityHolderClass1 == null || securityHolderClass1.Length == 0)
        return true;
      SecurityHolderClass securityHolderClass2 = securityHolderClass1[0];
      return isBaseClass ? this.readonlyMode : securityHolderClass2.IsSecurityReadOnly;
    }
  }

  public event SecurityControl.SecurityChangedEventHandler SecurityChanged;

  public SecurityControl()
  {
    this.InitializeComponent();
    this.InitStateImageList();
    this.InitImageList();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitStateImageList()
  {
    this.stateImageList.Images.Clear();
    Bitmap bitmap1 = new Bitmap(typeof (SecurityControl).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.ClockRed.bmp"));
    bitmap1.MakeTransparent();
    this.stateImageList.Images.Add((Image) bitmap1);
    Bitmap bitmap2 = new Bitmap(typeof (SecurityControl).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.ClockYellow.bmp"));
    bitmap2.MakeTransparent();
    this.stateImageList.Images.Add((Image) bitmap2);
    Bitmap bitmap3 = new Bitmap(typeof (SecurityControl).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.ClockGreen.bmp"));
    bitmap3.MakeTransparent();
    this.stateImageList.Images.Add((Image) bitmap3);
    Bitmap bitmap4 = new Bitmap(typeof (SecurityControl).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.Cond.bmp"));
    bitmap4.MakeTransparent();
    this.stateImageList.Images.Add((Image) bitmap4);
    Bitmap bitmap5 = new Bitmap(typeof (SecurityControl).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.ClockRedCond.bmp"));
    bitmap5.MakeTransparent();
    this.stateImageList.Images.Add((Image) bitmap5);
    Bitmap bitmap6 = new Bitmap(typeof (SecurityControl).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.ClockYellowCond.bmp"));
    bitmap6.MakeTransparent();
    this.stateImageList.Images.Add((Image) bitmap6);
    Bitmap bitmap7 = new Bitmap(typeof (SecurityControl).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.ClockGreenCond.bmp"));
    bitmap7.MakeTransparent();
    this.stateImageList.Images.Add((Image) bitmap7);
  }

  private void InitImageList()
  {
    this.objectsTreeList.SelectImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SecurityControl));
    this.optionsLB = new CheckedListBox();
    this.objectsTreeList = new TreeList();
    this.nameObjectTreeListColumn = new TreeListColumn();
    this.condObjectTreeListColumn = new TreeListColumn();
    this.stateImageList = new ImageList(this.components);
    this.btnAdd = new Button();
    this.btnDelete = new Button();
    this.btnStatus = new Button();
    this.rightsTreeList = new TreeList();
    this.rightsColumn = new TreeListColumn();
    this.enabledColumn = new TreeListColumn();
    this.enabledRepositoryItemCheckEdit = new RepositoryItemCheckEdit();
    this.disabledColumn = new TreeListColumn();
    this.disabledRepositoryItemCheckEdit = new RepositoryItemCheckEdit();
    this.panel1 = new Panel();
    this.panel6 = new Panel();
    this.panel5 = new Panel();
    this.btnCondition = new Button();
    this.btnGrantAlwaysAccess = new Button();
    this.cbInherit = new CheckBox();
    this.splitter1 = new Splitter();
    this.panel2 = new Panel();
    this.panel4 = new Panel();
    this.toolTip = new ToolTip(this.components);
    this.statusStrip1 = new StatusStrip();
    this.labelRestrict = new ToolStripStatusLabel();
    this.labelDiffTypesRestrict = new ToolStripStatusLabel();
    this.labelShortRights = new ToolStripStatusLabel();
    this.objectsTreeList.BeginInit();
    this.rightsTreeList.BeginInit();
    this.enabledRepositoryItemCheckEdit.BeginInit();
    this.disabledRepositoryItemCheckEdit.BeginInit();
    this.panel1.SuspendLayout();
    this.panel6.SuspendLayout();
    this.panel5.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel4.SuspendLayout();
    this.statusStrip1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.optionsLB, "optionsLB");
    this.optionsLB.Name = "optionsLB";
    componentResourceManager.ApplyResources((object) this.objectsTreeList, "objectsTreeList");
    this.objectsTreeList.Columns.AddRange(new TreeListColumn[2]
    {
      this.nameObjectTreeListColumn,
      this.condObjectTreeListColumn
    });
    this.objectsTreeList.Name = "objectsTreeList";
    this.objectsTreeList.StateImageList = this.stateImageList;
    this.objectsTreeList.GetCustomNodeCellStyle += new GetCustomNodeCellStyleEventHandler(this.objectsTreeList_GetCustomNodeCellStyle);
    this.objectsTreeList.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.objectsTreeList_FocusedNodeChanged);
    this.objectsTreeList.KeyDown += new KeyEventHandler(this.objectsTreeList_KeyDown);
    componentResourceManager.ApplyResources((object) this.nameObjectTreeListColumn, "nameObjectTreeListColumn");
    this.nameObjectTreeListColumn.Name = "nameObjectTreeListColumn";
    componentResourceManager.ApplyResources((object) this.condObjectTreeListColumn, "condObjectTreeListColumn");
    this.condObjectTreeListColumn.Name = "condObjectTreeListColumn";
    this.stateImageList.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.stateImageList, "stateImageList");
    this.stateImageList.TransparentColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.btnAdd.KeyDown += new KeyEventHandler(this.objectsTreeList_KeyDown);
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.btnDelete.KeyDown += new KeyEventHandler(this.objectsTreeList_KeyDown);
    componentResourceManager.ApplyResources((object) this.btnStatus, "btnStatus");
    this.btnStatus.Name = "btnStatus";
    this.btnStatus.Click += new EventHandler(this.btnStatus_Click);
    this.btnStatus.KeyDown += new KeyEventHandler(this.objectsTreeList_KeyDown);
    this.rightsTreeList.Columns.AddRange(new TreeListColumn[3]
    {
      this.rightsColumn,
      this.enabledColumn,
      this.disabledColumn
    });
    componentResourceManager.ApplyResources((object) this.rightsTreeList, "rightsTreeList");
    this.rightsTreeList.Name = "rightsTreeList";
    this.rightsTreeList.RepositoryItems.AddRange(new RepositoryItem[2]
    {
      (RepositoryItem) this.enabledRepositoryItemCheckEdit,
      (RepositoryItem) this.disabledRepositoryItemCheckEdit
    });
    this.rightsTreeList.Styles.AddReplace("HideSelectionRow", (object) new ViewStyle("HideSelectionRow", "TreeList", new Font("Microsoft Sans Serif", 8f), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this.rightsTreeList.Styles.AddReplace("PrivateRights", (object) new ViewStyle("PrivateRights"));
    this.rightsTreeList.Styles.AddReplace("FocusedRow", (object) new ViewStyle("FocusedRow", "TreeList", new Font("Microsoft Sans Serif", 8f), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this.rightsTreeList.Styles.AddReplace("GroupTitle", (object) new ViewStyle("GroupTitle", "", new Font("Microsoft Sans Serif", 8f), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LightGray, SystemColors.WindowText));
    this.rightsTreeList.Styles.AddReplace("InheritedRights", (object) new ViewStyle("InheritedRights", "", new Font("Microsoft Sans Serif", 8f), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LemonChiffon, SystemColors.WindowText));
    this.rightsTreeList.GetCustomNodeCellEdit += new GetCustomNodeCellEditEventHandler(this.rightsTreeList_GetCustomNodeCellEdit);
    this.rightsTreeList.GetCustomNodeCellStyle += new GetCustomNodeCellStyleEventHandler(this.rightsTreeList_GetCustomNodeCellStyle);
    this.rightsTreeList.KeyDown += new KeyEventHandler(this.objectsTreeList_KeyDown);
    componentResourceManager.ApplyResources((object) this.rightsColumn, "rightsColumn");
    this.rightsColumn.Name = "rightsColumn";
    componentResourceManager.ApplyResources((object) this.enabledColumn, "enabledColumn");
    this.enabledColumn.ColumnEdit = (RepositoryItem) this.enabledRepositoryItemCheckEdit;
    this.enabledColumn.Name = "enabledColumn";
    this.enabledRepositoryItemCheckEdit.AutoHeight = false;
    this.enabledRepositoryItemCheckEdit.Name = "enabledRepositoryItemCheckEdit";
    this.enabledRepositoryItemCheckEdit.ValueGrayed = (object) true;
    componentResourceManager.ApplyResources((object) this.disabledColumn, "disabledColumn");
    this.disabledColumn.ColumnEdit = (RepositoryItem) this.disabledRepositoryItemCheckEdit;
    this.disabledColumn.Name = "disabledColumn";
    this.disabledRepositoryItemCheckEdit.AutoHeight = false;
    this.disabledRepositoryItemCheckEdit.Name = "disabledRepositoryItemCheckEdit";
    this.disabledRepositoryItemCheckEdit.ValueGrayed = (object) true;
    this.panel1.Controls.Add((Control) this.panel6);
    this.panel1.Controls.Add((Control) this.panel5);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.panel6.Controls.Add((Control) this.objectsTreeList);
    componentResourceManager.ApplyResources((object) this.panel6, "panel6");
    this.panel6.Name = "panel6";
    this.panel5.Controls.Add((Control) this.btnCondition);
    this.panel5.Controls.Add((Control) this.btnGrantAlwaysAccess);
    this.panel5.Controls.Add((Control) this.cbInherit);
    this.panel5.Controls.Add((Control) this.btnDelete);
    this.panel5.Controls.Add((Control) this.btnStatus);
    this.panel5.Controls.Add((Control) this.btnAdd);
    componentResourceManager.ApplyResources((object) this.panel5, "panel5");
    this.panel5.Name = "panel5";
    componentResourceManager.ApplyResources((object) this.btnCondition, "btnCondition");
    this.btnCondition.Name = "btnCondition";
    this.btnCondition.Click += new EventHandler(this.btnCondition_Click);
    this.btnCondition.KeyDown += new KeyEventHandler(this.objectsTreeList_KeyDown);
    componentResourceManager.ApplyResources((object) this.btnGrantAlwaysAccess, "btnGrantAlwaysAccess");
    this.btnGrantAlwaysAccess.Name = "btnGrantAlwaysAccess";
    this.btnGrantAlwaysAccess.Click += new EventHandler(this.btnGrantAlwaysAccess_Click);
    this.btnGrantAlwaysAccess.KeyDown += new KeyEventHandler(this.objectsTreeList_KeyDown);
    componentResourceManager.ApplyResources((object) this.cbInherit, "cbInherit");
    this.cbInherit.Name = "cbInherit";
    this.toolTip.SetToolTip((Control) this.cbInherit, componentResourceManager.GetString("cbInherit.ToolTip"));
    this.cbInherit.UseVisualStyleBackColor = true;
    this.cbInherit.CheckedChanged += new EventHandler(this.cbInherit_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.panel2.Controls.Add((Control) this.panel4);
    this.panel2.Controls.Add((Control) this.optionsLB);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this.panel4.Controls.Add((Control) this.rightsTreeList);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    this.statusStrip1.BackColor = SystemColors.Control;
    this.statusStrip1.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.labelRestrict,
      (ToolStripItem) this.labelDiffTypesRestrict,
      (ToolStripItem) this.labelShortRights
    });
    componentResourceManager.ApplyResources((object) this.statusStrip1, "statusStrip1");
    this.statusStrip1.Name = "statusStrip1";
    this.labelRestrict.ForeColor = Color.DarkMagenta;
    this.labelRestrict.Name = "labelRestrict";
    componentResourceManager.ApplyResources((object) this.labelRestrict, "labelRestrict");
    this.labelDiffTypesRestrict.ForeColor = Color.DarkMagenta;
    this.labelDiffTypesRestrict.Name = "labelDiffTypesRestrict";
    componentResourceManager.ApplyResources((object) this.labelDiffTypesRestrict, "labelDiffTypesRestrict");
    this.labelShortRights.Name = "labelShortRights";
    componentResourceManager.ApplyResources((object) this.labelShortRights, "labelShortRights");
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.statusStrip1);
    this.Name = nameof (SecurityControl);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Load += new EventHandler(this.SecurityControl_Load);
    this.KeyDown += new KeyEventHandler(this.objectsTreeList_KeyDown);
    this.objectsTreeList.EndInit();
    this.rightsTreeList.EndInit();
    this.enabledRepositoryItemCheckEdit.EndInit();
    this.disabledRepositoryItemCheckEdit.EndInit();
    this.panel1.ResumeLayout(false);
    this.panel6.ResumeLayout(false);
    this.panel5.ResumeLayout(false);
    this.panel5.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.panel4.ResumeLayout(false);
    this.statusStrip1.ResumeLayout(false);
    this.statusStrip1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>сброс админского доступа</summary>
  /// <returns></returns>
  private bool RestoreAdminAccess()
  {
    if (this.securityClass.IsChanged)
    {
      switch (IMMessageBox.Show(MessageDialogs.msgInformation, LocalizationHolder.rm.GetString("Client.Core_1009"), MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Question))
      {
        case DialogResult.Cancel:
          return true;
        case DialogResult.Yes:
          if (!this.securityClass.Save())
            return false;
          break;
      }
    }
    else if (IMMessageBox.Show(MessageDialogs.msgInformation, LocalizationHolder.rm.GetString("Client.Core_1010"), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return true;
    if (this.ids != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index = 0; index < this.ids.Length; ++index)
        {
          IDBSecurity security = this.iSecurityCallback.GetSecurity(sessionKeeper.Session, this.ids[index]);
          if (security != null)
          {
            try
            {
              security.RestoreAdminAccess();
            }
            catch (Exception ex)
            {
              ExceptionHelper.ExceptionService.ShowException(ex);
              return false;
            }
          }
        }
      }
    }
    this.LoadSecurity(this.ids, this.iSecurityCallback);
    int num = (int) IMMessageBox.Show(MessageDialogs.msgInformation, LocalizationHolder.rm.GetString("Client.Core_1011"), MessageBoxButtons.OK, IMMessageBoxImage.Information);
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aId">идентификаторы</param>
  /// <param name="aISecurityCallback"></param>
  public void LoadSecurity(object[] aId, ISecurityCallback aISecurityCallback)
  {
    this.rightConditionList.Initialize();
    this.ids = aId;
    this.iSecurityCallback = aISecurityCallback;
    this.sessionUserId = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).UserID;
    try
    {
      this.securityClass.Load(aId, aISecurityCallback);
    }
    finally
    {
      this.FillForm();
    }
  }

  public bool SaveSecurity()
  {
    bool flag = this.securityClass.Save();
    try
    {
      if (flag)
      {
        if (this.cbInherit.Checked)
        {
          if (this.iSecurityCallback.MaintainedCategory == 4)
            this.ApplySecurityInheritanceForObjType(this.ids, this.iSecurityCallback);
          if (this.iSecurityCallback.MaintainedCategory == 7)
          {
            if (this.iSecurityCallback.Applicability != null)
            {
              if (this.iSecurityCallback.Applicability.Item1 == 4)
                this.ApplySecurityInheritanceForLCStep(this.ids, this.iSecurityCallback, (int) this.iSecurityCallback.Applicability.Item2);
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
    }
    return flag;
  }

  /// <summary>
  /// для каждого типа объектов из списка применяет его параметры безопасности для всех его потомков.
  /// исключения обрабатываются в цикле, отображаются и давятся чтобы пройти по всем элементам.
  /// </summary>
  /// <param name="aIds"></param>
  /// <param name="aSecurityCallback"></param>
  /// <returns></returns>
  private bool ApplySecurityInheritanceForObjType(
    object[] aIds,
    ISecurityCallback aSecurityCallback)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index1 = 0; index1 < aIds.Length; ++index1)
      {
        try
        {
          int aId = (int) aIds[index1];
          List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(aId);
          IDBSecurity security = aSecurityCallback.GetSecurity(sessionKeeper.Session, (object) aId);
          ActionProperties[] actions1 = (ActionProperties[]) null;
          QuickObjectInfo[] users1 = (QuickObjectInfo[]) null;
          DataTable fromTable = this.ClearFKEY(SecurityProcs.GroupRightsByUID(security.GetAccessList(out actions1, out users1)));
          for (int index2 = 0; index2 < childrenIdRecursive.Count; ++index2)
          {
            if (childrenIdRecursive[index2] != aId)
            {
              try
              {
                IDBObjectType objectType = sessionKeeper.Session.GetObjectType(childrenIdRecursive[index2]);
                if (objectType != null)
                {
                  if (objectType is IDBSecurity dbSecurity)
                  {
                    ActionProperties[] actions2 = (ActionProperties[]) null;
                    QuickObjectInfo[] users2 = (QuickObjectInfo[]) null;
                    DataTable dtOld = SecurityProcs.GroupRightsByUID(dbSecurity.GetAccessList(out actions2, out users2));
                    DataTable dataTable = DataSetProcessor.CopyTable(fromTable);
                    this.MarkDeleted(dataTable, dtOld);
                    dbSecurity.SetAccess(SecurityProcs.DegroupRightsByUID(dataTable));
                  }
                }
              }
              catch (Exception ex)
              {
                ExceptionHelper.ExceptionService.ShowException(ex);
              }
            }
          }
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
        }
      }
    }
    return true;
  }

  /// <summary>
  /// для каждого шага ЖЦ из списка применяет его параметры безопасности на соответствующие шаги схем ЖЦ, принадлежащих потомкам типа объекта objType.
  /// исключения обрабатываются в цикле, отображаются и давятся чтобы пройти по всем элементам.
  /// </summary>
  /// <param name="ids"></param>
  /// <param name="iSecurityCallback"></param>
  /// <param name="objType"></param>
  /// <returns></returns>
  private bool ApplySecurityInheritanceForLCStep(
    object[] ids,
    ISecurityCallback iSecurityCallback,
    int objType)
  {
    HybridDictionary hybridDictionary = new HybridDictionary();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < ids.Length; ++index)
      {
        IDBSecurity security = iSecurityCallback.GetSecurity(sessionKeeper.Session, ids[index]);
        if (security != null)
        {
          DataTable dataTable = this.ClearFKEY(SecurityProcs.GroupRightsByUID(security.GetAccessList(out ActionProperties[] _, out QuickObjectInfo[] _)));
          hybridDictionary[(object) (int) ids[index]] = (object) dataTable;
        }
      }
    }
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objType);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index1 = 0; index1 < childrenIdRecursive.Count; ++index1)
      {
        if (childrenIdRecursive[index1] != objType)
        {
          try
          {
            IDBObjectType objectType = sessionKeeper.Session.GetObjectType(childrenIdRecursive[index1]);
            if (objectType != null)
            {
              IDBLCSchema lcSchema = sessionKeeper.Session.GetLCSchema(objectType.PropertiesStructure.SchemaID);
              if (lcSchema != null)
              {
                IDBLifecycleStepCollection stepsCollection = lcSchema.GetStepsCollection();
                if (stepsCollection != null)
                {
                  DataTable table = stepsCollection.GetSchema().Tables["IMS_LC_STEPS"];
                  for (int index2 = 0; index2 < table.Rows.Count; ++index2)
                  {
                    int int32 = Convert.ToInt32(table.Rows[index2]["F_LC_STEP"]);
                    if (hybridDictionary.Contains((object) int32) && sessionKeeper.Session.GetLifecycleStep(int32, childrenIdRecursive[index1]) is IDBSecurity lifecycleStep)
                    {
                      ActionProperties[] actions = (ActionProperties[]) null;
                      QuickObjectInfo[] users = (QuickObjectInfo[]) null;
                      DataTable dtOld = SecurityProcs.GroupRightsByUID(lifecycleStep.GetAccessList(out actions, out users));
                      DataTable dataTable = DataSetProcessor.CopyTable((DataTable) hybridDictionary[(object) int32]);
                      this.MarkDeleted(dataTable, dtOld);
                      lifecycleStep.SetAccess(SecurityProcs.DegroupRightsByUID(dataTable));
                    }
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
          }
        }
      }
    }
    return true;
  }

  private DataTable ClearFKEY(DataTable dataTable)
  {
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (Convert.ToInt64(row["F_PARENT_KEY"]) != -1L)
        row["F_KEY"] = (object) -(long) row[SecurityProcs.F_UID];
    }
    dataTable.AcceptChanges();
    return dataTable;
  }

  /// <summary>
  /// дает ответ на вопрос есть ли в таблице прав право dr ( F_RIGHT_ID+F_USER_ID+F_RIGHT_TYPE ) //TODO: Alex1219 +F_BEGIN_DATE+F_END_DATE+F_CONDITION_ID
  /// </summary>
  /// <param name="dr"></param>
  /// <param name="dt"></param>
  /// <returns></returns>
  private bool RowExists(DataRow dr, DataTable dt)
  {
    bool flag = false;
    int int32_1 = Convert.ToInt32(dr["F_RIGHT_ID"]);
    long int64 = Convert.ToInt64(dr["F_USER_ID"]);
    int int32_2 = Convert.ToInt32(dr["F_RIGHT_TYPE"]);
    object obj1 = dr["F_BEGIN_DATE"];
    object obj2 = dr["F_END_DATE"];
    object obj3 = dr["F_CONDITION_ID"];
    for (int index = 0; index < dt.Rows.Count; ++index)
    {
      if (Convert.ToInt64(dt.Rows[index]["F_USER_ID"]) == int64 && Convert.ToInt32(dt.Rows[index]["F_RIGHT_ID"]) == int32_1 && Convert.ToInt32(dt.Rows[index]["F_RIGHT_TYPE"]) == int32_2 && dt.Rows[index]["F_BEGIN_DATE"].Equals(obj1) && dt.Rows[index]["F_END_DATE"].Equals(obj2) && dt.Rows[index]["F_CONDITION_ID"].Equals(obj3))
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  /// <summary>
  /// копирует в dtNew (с пометкой dr[ Consts.F_RIGHT_TYPE ] = Consts.DeleteRecord) те записи из dtOld
  /// которых нет в dtNew (по RowExists).
  /// dtOld при этом портится.
  /// </summary>
  /// <param name="dtNew"></param>
  /// <param name="dtOld"></param>
  private void MarkDeleted(DataTable dtNew, DataTable dtOld)
  {
    for (int index = 0; index < dtOld.Rows.Count; ++index)
    {
      DataRow row = dtOld.Rows[index];
      if (!this.RowExists(row, dtNew))
      {
        row["F_RIGHT_TYPE"] = (object) Intermech.Consts.DeleteRecord;
        dtNew.ImportRow(row);
      }
    }
  }

  private void FillForm()
  {
    this.differencesFound = false;
    this.readonlyMode = false;
    SecurityHolderClass baseShc = (SecurityHolderClass) null;
    this.blockOnChange_Inherit = true;
    try
    {
      this.cbInherit.Checked = false;
    }
    finally
    {
      this.blockOnChange_Inherit = false;
    }
    this.cbInherit.Visible = this.iSecurityCallback.MaintainedCategory == 4 || this.iSecurityCallback.MaintainedCategory == 7;
    this.objectsTreeList.ClearNodes();
    this.grantAlwaysAccessNodeList.Clear();
    if (this.securityClass.Initialized)
    {
      baseShc = this.securityClass.SecurityHolderClass;
      this.readonlyMode = baseShc.IsSecurityReadOnly;
    }
    this.mainNode = this.FillMainNode();
    if (!this.securityClass.Initialized)
      return;
    this.FillRelatedSecurity(baseShc, (TreeListNode) null);
  }

  private TreeListNode FillMainNode()
  {
    string str = string.Empty;
    if (this.securityClass.Initialized)
      str = this.securityClass.SecurityHolderClass.ObjectName;
    TreeListNode tln = (TreeListNode) null;
    if (this.securityClass.Initialized)
    {
      this.assignData = true;
      this.data = (object) new SecurityHolderClass[1]
      {
        this.securityClass.SecurityHolderClass
      };
      tln = this.objectsTreeList.AppendNode((object) new object[1]
      {
        (object) str
      }, (TreeListNode) null);
      if (this.securityClass.SecurityHolderClass.Ids.Length == 1)
      {
        tln.ImageIndex = this.securityClass.SecurityHolderClass.IcoImageIndex;
        tln.SelectImageIndex = this.securityClass.SecurityHolderClass.IcoImageIndex;
      }
      else
      {
        tln.ImageIndex = this.securityClass.SecurityHolderClass.CatIcoImageIndex;
        tln.SelectImageIndex = this.securityClass.SecurityHolderClass.CatIcoImageIndex;
      }
      this.FillUGR(tln);
      tln.Expanded = true;
    }
    this.UpdateControlStates();
    return tln;
  }

  private void FillRelatedSecurity(SecurityHolderClass baseShc, TreeListNode parent)
  {
    if (!baseShc.RelatedSecurityExpanded)
      return;
    for (int index = 0; index < baseShc.Count; ++index)
    {
      TreeListNode tln = this.objectsTreeList.AppendNode((object) new object[1]
      {
        (object) baseShc[index].ObjectName
      }, parent);
      tln.Tag = (object) baseShc[index];
      tln.ImageIndex = baseShc[index].IcoImageIndex;
      tln.SelectImageIndex = baseShc[index].IcoImageIndex;
      this.FillUGR(tln);
      tln.Expanded = true;
    }
  }

  private void FillUGR(TreeListNode tln)
  {
    SecurityHolderClass securityHolderClass = !(tln.Tag.GetType() == typeof (SecurityHolderClass[])) ? (SecurityHolderClass) tln.Tag : ((SecurityHolderClass[]) tln.Tag)[0];
    if (securityHolderClass == null)
      return;
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    foreach (DataRow row in (InternalDataCollectionBase) securityHolderClass.AccessDataTable.Rows)
    {
      if (Convert.ToInt32(row["F_RIGHT_TYPE"]) != Intermech.Consts.DeleteRecord)
      {
        if (Convert.ToInt32(row["F_RIGHT_TYPE"]) == 4)
        {
          if (this.grantAlwaysAccessNodeList.IndexOf(tln) == -1)
            this.grantAlwaysAccessNodeList.Add(tln);
        }
        else
        {
          long uid = (long) row[SecurityProcs.F_UID];
          QuickObjectInfo qoi;
          if (arrayList1.IndexOf((object) uid) == -1 && SecurityHolderClass.FindQuickObjectInfo(Convert.ToInt64(row["F_USER_ID"]), securityHolderClass.Users, out qoi))
          {
            arrayList2.Add((object) new SecurityNodeClass(uid, qoi, row["F_BEGIN_DATE"], row["F_END_DATE"], securityHolderClass.ConditionsEnabled, row["F_CONDITION_ID"]));
            arrayList1.Add((object) uid);
          }
        }
      }
    }
    arrayList2.Sort((IComparer) new SecurityControl.SncComparer());
    for (int index = 0; index < arrayList2.Count; ++index)
    {
      SecurityNodeClass securityNodeClass = (SecurityNodeClass) arrayList2[index];
      TreeListNode tln1 = this.objectsTreeList.AppendNode((object) new object[1]
      {
        (object) securityNodeClass.QuickObjectInfo.Caption
      }, tln);
      tln1.Tag = (object) securityNodeClass;
      this.AssignUGRCondition(tln1);
      this.AssignUGRStateIndex(tln1);
      this.AssignUGRImageIndex(tln1);
    }
  }

  private void AssignUGRCondition(TreeListNode tln)
  {
    if (tln == null || !(tln.Tag is SecurityNodeClass))
      return;
    SecurityNodeClass tag = (SecurityNodeClass) tln.Tag;
    bool flag = this.NodeHasPrivateRights(tln);
    if (tag.ConditionsEnabled & flag)
    {
      object condition = tag.Condition;
      tln.SetValue((object) SecurityControl.ConstConditionColIndex, (object) this.rightConditionList.ValueToString(condition));
    }
    else
      tln.SetValue((object) SecurityControl.ConstConditionColIndex, (object) string.Empty);
  }

  private void AssignUGRStateIndex(TreeListNode tln)
  {
    if (tln == null || !(tln.Tag is SecurityNodeClass))
      return;
    SecurityNodeClass tag = (SecurityNodeClass) tln.Tag;
    DateTime now = DateTime.Now;
    tln.StateImageIndex = tag.BeginDate == DBNull.Value || tag.EndDate == DBNull.Value ? -1 : (!((DateTime) tag.BeginDate < now) || !((DateTime) tag.EndDate < now) ? (!((DateTime) tag.BeginDate > now) || !((DateTime) tag.EndDate > now) ? 2 : 1) : 0);
    if (!tag.ConditionsEnabled || tag.Condition == DBNull.Value || tag.Condition == null || Convert.ToInt64(tag.Condition) == 0L)
      return;
    tln.StateImageIndex += 4;
  }

  private void AssignUGRImageIndex(TreeListNode tln)
  {
    if (tln == null || !(tln.Tag is SecurityNodeClass))
      return;
    SecurityNodeClass tag = (SecurityNodeClass) tln.Tag;
    if (Statics.IconSrv == null)
      return;
    int num = Statics.IconSrv.IndexOf(4, tag.QuickObjectInfo.ObjectTypeID);
    tln.ImageIndex = num;
    tln.SelectImageIndex = num;
  }

  private void objectsTreeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    if (this.assignData && e.Node != null)
    {
      e.Node.Tag = this.data;
      this.assignData = false;
    }
    this.UpdateControlStates();
    if (e.Node != null && e.Node.Tag == null)
      this.ClearActions();
    else if (e.Node == null || e.Node.ParentNode == null || e.Node.Tag is SecurityHolderClass)
    {
      this.ClearActions();
    }
    else
    {
      if (e.OldNode == null || !(e.OldNode.Tag is SecurityNodeClass) || e.Node == null || !(e.Node.Tag is SecurityNodeClass) || e.OldNode.ParentNode != e.Node.ParentNode)
        this.FillActions(e.Node);
      this.FillRights(e.Node);
    }
  }

  private void ClearActions()
  {
    this.rightsTreeList.Nodes.Clear();
    this.acFullNode = (TreeListNode) null;
    this.acReadNode = (TreeListNode) null;
    this.acWriteNode = (TreeListNode) null;
    this.acAdminNode = (TreeListNode) null;
  }

  private void FillActions(TreeListNode tln)
  {
    this.ClearActions();
    bool isBaseClass = false;
    SecurityHolderClass[] securityHolderClass1 = this.GetSecurityHolderClass(tln, out isBaseClass);
    if (securityHolderClass1 == null || securityHolderClass1.Length == 0)
      return;
    SecurityHolderClass securityHolderClass2 = securityHolderClass1[0];
    if (this.acFullNode == null)
    {
      this.acFullNode = this.rightsTreeList.AppendNode((object) new object[1]
      {
        (object) LocalizationHolder.rm.GetString("Client.Core_1013")
      }, (TreeListNode) null);
      this.acFullNode.Tag = (object) ActionCategory.NotDefined;
      this.acFullNode.Expanded = true;
    }
    this.acReadNode = this.rightsTreeList.AppendNode((object) new object[1]
    {
      (object) ActionCategoryHelper.GetCaption(ActionCategory.Read)
    }, this.acFullNode);
    this.acReadNode.Tag = (object) ActionCategory.Read;
    this.acWriteNode = this.rightsTreeList.AppendNode((object) new object[1]
    {
      (object) ActionCategoryHelper.GetCaption(ActionCategory.Write)
    }, this.acFullNode);
    this.acWriteNode.Tag = (object) ActionCategory.Write;
    this.acAdminNode = this.rightsTreeList.AppendNode((object) new object[1]
    {
      (object) ActionCategoryHelper.GetCaption(ActionCategory.Admin)
    }, this.acFullNode);
    this.acAdminNode.Tag = (object) ActionCategory.Admin;
    for (int index = 0; index < securityHolderClass2.Actions.Length; ++index)
    {
      TreeListNode parentNode = (TreeListNode) null;
      if (securityHolderClass2.Actions[index].Category == ActionCategory.Read)
        parentNode = this.acReadNode;
      else if (securityHolderClass2.Actions[index].Category == ActionCategory.Write)
        parentNode = this.acWriteNode;
      else if (securityHolderClass2.Actions[index].Category == ActionCategory.Admin)
        parentNode = this.acAdminNode;
      if (parentNode != null)
        this.rightsTreeList.AppendNode((object) new object[1]
        {
          (object) securityHolderClass2.Actions[index].Name
        }, parentNode).Tag = (object) securityHolderClass2.Actions[index];
    }
    if (this.acReadNode.Nodes.Count == 0)
    {
      this.rightsTreeList.DeleteNode(this.acReadNode);
      this.acReadNode = (TreeListNode) null;
    }
    if (this.acWriteNode.Nodes.Count == 0)
    {
      this.rightsTreeList.DeleteNode(this.acWriteNode);
      this.acWriteNode = (TreeListNode) null;
    }
    if (this.acAdminNode.Nodes.Count == 0)
    {
      this.rightsTreeList.DeleteNode(this.acAdminNode);
      this.acAdminNode = (TreeListNode) null;
    }
    this.acFullNode.Expanded = true;
    if (this.acReadNode != null)
      this.acReadNode.Expanded = true;
    if (this.acWriteNode != null)
      this.acWriteNode.Expanded = true;
    if (this.acAdminNode == null)
      return;
    this.acAdminNode.Expanded = true;
  }

  private void UpdateControlStates()
  {
    bool actualReadonly = this.ActualReadonly;
    this.labelRestrict.Visible = actualReadonly;
    this.labelDiffTypesRestrict.Visible = !this.securityClass.IsCompatibleRightsAbstract;
    this.labelShortRights.Visible = this.securityClass.Initialized && this.securityClass.SecurityHolderClass.Ids.Length > 1;
    this.btnAdd.Enabled = !actualReadonly && this.objectsTreeList.FocusedNode != null;
    this.btnDelete.Enabled = !actualReadonly && this.objectsTreeList.FocusedNode != null && this.objectsTreeList.FocusedNode.Tag is SecurityNodeClass;
    this.btnStatus.Enabled = this.objectsTreeList.FocusedNode != null && this.objectsTreeList.FocusedNode.Tag is SecurityNodeClass;
    this.btnCondition.Enabled = this.objectsTreeList.FocusedNode != null && this.objectsTreeList.FocusedNode.Tag is SecurityNodeClass && ((SecurityNodeClass) this.objectsTreeList.FocusedNode.Tag).ConditionsEnabled;
    this.btnGrantAlwaysAccess.Enabled = this.grantAlwaysAccessNodeList != null && this.grantAlwaysAccessNodeList.Count > 0;
    if (!actualReadonly)
      this.rightsTreeList.BehaviorOptions |= BehaviorOptionsFlags.Editable;
    else
      this.rightsTreeList.BehaviorOptions &= ~BehaviorOptionsFlags.Editable;
    if (this.btnDelete.Enabled || this.btnStatus.Enabled || this.btnCondition.Enabled)
    {
      bool flag = this.NodeHasPrivateRights(this.objectsTreeList.FocusedNode);
      if (this.btnDelete.Enabled)
        this.btnDelete.Enabled = flag;
      if (this.btnStatus.Enabled)
        this.btnStatus.Enabled = flag;
      if (this.btnCondition.Enabled)
        this.btnCondition.Enabled = flag;
    }
    this.AssignUGRCondition(this.objectsTreeList.FocusedNode);
  }

  private bool NodeHasPrivateRights(TreeListNode node)
  {
    bool flag = false;
    if (node != null && node.Tag is SecurityNodeClass tag)
    {
      SecurityHolderClass[] securityHolderClass = this.GetSecurityHolderClass(node, out bool _);
      if (securityHolderClass != null && securityHolderClass.Length != 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) securityHolderClass[0].AccessDataTable.Rows)
        {
          if (Convert.ToInt64(row[SecurityProcs.F_UID]) == tag.UID && Convert.ToInt64(row["F_USER_ID"]) == tag.QuickObjectInfo.ObjectID && Convert.ToInt32(row["F_PARENT_KEY"]) == 0)
          {
            flag = true;
            break;
          }
        }
      }
    }
    return flag;
  }

  private void FillRights(TreeListNode tln)
  {
    if (!(tln.Tag is SecurityNodeClass tag))
      return;
    bool isBaseClass = false;
    SecurityHolderClass[] securityHolderClass1 = this.GetSecurityHolderClass(tln, out isBaseClass);
    if (securityHolderClass1 == null || securityHolderClass1.Length == 0)
      return;
    SecurityHolderClass securityHolderClass2 = securityHolderClass1[0];
    this.blockOnChange_FillRights = true;
    try
    {
      this.ClearRights(this.rightsTreeList.Nodes);
      foreach (DataRow row in (InternalDataCollectionBase) securityHolderClass2.AccessDataTable.Rows)
      {
        if (Convert.ToInt64(row[SecurityProcs.F_UID]) == tag.UID && Convert.ToInt64(row["F_USER_ID"]) == tag.QuickObjectInfo.ObjectID && Convert.ToInt32(row["F_RIGHT_TYPE"]) != Intermech.Consts.DeleteRecord && Convert.ToInt32(row["F_RIGHT_TYPE"]) != 4)
        {
          TreeListNode rightNode = this.FindRightNode((ActionType) Convert.ToInt32(row["F_RIGHT_ID"]));
          if (rightNode != null)
          {
            bool flag1 = false;
            bool flag2 = false;
            switch (Convert.ToInt32(row["F_RIGHT_TYPE"]))
            {
              case 0:
                flag1 = ((ActionProperties) rightNode.Tag).DefaultAccess;
                break;
              case 2:
                flag1 = true;
                break;
              case 3:
                flag2 = true;
                break;
            }
            rightNode[(object) this.enabledColumn] = (object) flag1;
            rightNode[(object) this.disabledColumn] = (object) flag2;
          }
        }
      }
    }
    finally
    {
      this.blockOnChange_FillRights = false;
    }
    this.ProcessVirtualNodes();
  }

  private SecurityHolderClass[] GetSecurityHolderClass(TreeListNode atln, out bool isBaseClass)
  {
    isBaseClass = false;
    if (atln == null || atln.Tag == null)
      return (SecurityHolderClass[]) null;
    object obj = atln.ParentNode != null ? atln.ParentNode.Tag : atln.Tag;
    if (obj == null)
      return (SecurityHolderClass[]) null;
    SecurityHolderClass[] securityHolderClass;
    if (obj.GetType() == typeof (SecurityHolderClass[]))
    {
      securityHolderClass = (SecurityHolderClass[]) obj;
      isBaseClass = true;
    }
    else
      securityHolderClass = new SecurityHolderClass[1]
      {
        (SecurityHolderClass) obj
      };
    return securityHolderClass;
  }

  private void ClearRights(TreeListNodes tlns)
  {
    for (int index = 0; index < tlns.Count; ++index)
    {
      tlns[index][(object) this.enabledColumn] = (object) false;
      tlns[index][(object) this.disabledColumn] = (object) false;
      if (tlns[index].Nodes.Count > 0)
        this.ClearRights(tlns[index].Nodes);
    }
  }

  private TreeListNode FindUGRNodeCustom(long id, TreeListNodes nodes)
  {
    TreeListNode ugrNodeCustom = (TreeListNode) null;
    for (int index = 0; index < nodes.Count; ++index)
    {
      if (nodes[index].Tag is SecurityNodeClass)
      {
        if (((SecurityNodeClass) nodes[index].Tag).QuickObjectInfo.ObjectID == id)
        {
          ugrNodeCustom = nodes[index];
          break;
        }
      }
      else
      {
        ugrNodeCustom = this.FindUGRNodeCustom(id, nodes[index].Nodes);
        if (ugrNodeCustom != null)
          break;
      }
    }
    return ugrNodeCustom;
  }

  private TreeListNode FindUGRNode(long id)
  {
    return this.FindUGRNodeCustom(id, this.objectsTreeList.Nodes);
  }

  /// <summary>
  /// находим нод related security (верхнего уровня) по имени
  /// </summary>
  /// <param name="rlsName"></param>
  /// <returns></returns>
  private TreeListNode FindRLSNode(string rlsName)
  {
    TreeListNode rlsNode = (TreeListNode) null;
    for (int index = 0; index < this.objectsTreeList.Nodes.Count; ++index)
    {
      if (this.objectsTreeList.Nodes[index].GetDisplayText((object) 0) == rlsName)
      {
        rlsNode = this.objectsTreeList.Nodes[index];
        break;
      }
    }
    return rlsNode;
  }

  private TreeListNode FindRightNode(ActionType actionType)
  {
    TreeListNode rightNode = (TreeListNode) null;
    for (int index1 = 0; index1 < this.rightsTreeList.Nodes[0].Nodes.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.rightsTreeList.Nodes[0].Nodes[index1].Nodes.Count; ++index2)
      {
        if (((ActionProperties) this.rightsTreeList.Nodes[0].Nodes[index1].Nodes[index2].Tag).ActionID == actionType)
        {
          rightNode = this.rightsTreeList.Nodes[0].Nodes[index1].Nodes[index2];
          break;
        }
      }
      if (rightNode != null)
        break;
    }
    return rightNode;
  }

  private void ProcessVirtualNodes()
  {
    this.blockOnChange_ProcessVirtualNodes = true;
    try
    {
      this.ProcessVirtualNodes(this.acReadNode);
      this.ProcessVirtualNodes(this.acWriteNode);
      this.ProcessVirtualNodes(this.acAdminNode);
      this.ProcessVirtualNodes(this.acFullNode);
    }
    finally
    {
      this.blockOnChange_ProcessVirtualNodes = false;
    }
  }

  private void ProcessVirtualNodes(TreeListNode tln)
  {
    if (tln == null)
      return;
    bool cValue1 = true;
    bool cValue2 = true;
    for (int index = 0; index < tln.Nodes.Count; ++index)
    {
      cValue1 = cValue1 && (bool) tln.Nodes[index][(object) this.enabledColumn];
      cValue2 = cValue2 && (bool) tln.Nodes[index][(object) this.disabledColumn];
    }
    tln[(object) this.enabledColumn] = (object) cValue1;
    tln[(object) this.disabledColumn] = (object) cValue2;
    this.actionCategoryStates.SetState((ActionCategory) tln.Tag, true, cValue1);
    this.actionCategoryStates.SetState((ActionCategory) tln.Tag, false, cValue2);
  }

  private void rightsTreeList_GetCustomNodeCellStyle(
    object sender,
    GetCustomNodeCellStyleEventArgs e)
  {
    if (e.Column == this.enabledColumn || e.Column == this.disabledColumn)
      return;
    if (e.Node == null || !(e.Node.Tag is ActionProperties) || this.objectsTreeList.FocusedNode == null)
    {
      e.Style = this.rightsTreeList.Styles["GroupTitle"];
    }
    else
    {
      bool isBaseClass = false;
      if (!(this.objectsTreeList.FocusedNode.Tag is SecurityNodeClass tag))
      {
        e.Style = this.rightsTreeList.Styles["PrivateRights"];
      }
      else
      {
        long objectId = tag.QuickObjectInfo.ObjectID;
        long uid = tag.UID;
        SecurityHolderClass[] securityHolderClass = this.GetSecurityHolderClass(this.objectsTreeList.FocusedNode, out isBaseClass);
        if (securityHolderClass == null || securityHolderClass.Length == 0)
        {
          e.Style = this.rightsTreeList.Styles["PrivateRights"];
        }
        else
        {
          DataRow[] dataRowArray = securityHolderClass[0].AccessDataTable.Select($"F_RIGHT_TYPE<>{Intermech.Consts.DeleteRecord.ToString()} and {SecurityProcs.F_UID}={uid.ToString()} and F_USER_ID={objectId.ToString()} and F_RIGHT_ID={((int) ((ActionProperties) e.Node.Tag).ActionID).ToString()}");
          if (dataRowArray == null || dataRowArray.Length == 0)
            e.Style = this.rightsTreeList.Styles["PrivateRights"];
          else if (Convert.ToInt32(dataRowArray[0]["F_PARENT_KEY"]) == 0)
            e.Style = this.rightsTreeList.Styles["PrivateRights"];
          else
            e.Style = this.rightsTreeList.Styles["InheritedRights"];
        }
      }
    }
  }

  private bool IsCustomNode(TreeListNode tln)
  {
    return tln != this.acFullNode && tln != this.acReadNode && tln != this.acWriteNode && tln != this.acAdminNode;
  }

  private void SetChildValues(TreeListNode tln, TreeListColumn column, bool cValue)
  {
    for (int index = 0; index < tln.Nodes.Count; ++index)
    {
      tln.Nodes[index][(object) column] = (object) cValue;
      this.SetChildValues(tln.Nodes[index], column, cValue);
    }
  }

  private DataRow CreateNewRow(
    SecurityHolderClass shc,
    long uid,
    QuickObjectInfo qoi,
    ActionProperties ap,
    object beginDate,
    object endDate,
    object condition,
    long parentKey,
    AccessType at)
  {
    DataRow newRow = shc.AccessDataTable.NewRow();
    newRow[SecurityProcs.F_UID] = (object) uid;
    newRow["F_CATEGORY_ID"] = (object) shc.CategoryDescriptor.CategoryID;
    newRow["F_CATEGORY_TYPE"] = (object) shc.CategoryDescriptor.CategoryType;
    newRow["F_RIGHT_ID"] = (object) (int) ap.ActionID;
    newRow["F_USER_ID"] = (object) qoi.ObjectID;
    newRow["F_KEY"] = (object) -uid;
    newRow["F_OWNER_ID"] = (object) this.sessionUserId;
    newRow["F_BEGIN_DATE"] = beginDate;
    newRow["F_END_DATE"] = endDate;
    newRow["F_CONDITION_ID"] = condition;
    newRow["F_PARENT_KEY"] = (object) parentKey;
    newRow["F_RIGHT_TYPE"] = (object) (int) at;
    return newRow;
  }

  private bool SetNodeValue(
    TreeListNode tln,
    TreeListColumn column,
    bool cValue,
    CycleControlArrayList cycleControlArray)
  {
    CycleControlClass cycleControlClass = new CycleControlClass(tln, column);
    if (cycleControlArray.Find(cycleControlClass) != -1)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1014"));
      return false;
    }
    cycleControlArray.Add((object) cycleControlClass);
    try
    {
      if (tln.Tag is ActionCategory)
      {
        for (int index = 0; index < tln.Nodes.Count; ++index)
          this.SetNodeValue(tln.Nodes[index], column, cValue, cycleControlArray);
        tln[(object) column] = (object) cValue;
      }
      else
      {
        if (column == this.enabledColumn && cValue && (bool) tln[(object) this.disabledColumn])
          this.SetNodeValue(tln, this.disabledColumn, false, cycleControlArray);
        if (column == this.disabledColumn && cValue && (bool) tln[(object) this.enabledColumn])
          this.SetNodeValue(tln, this.enabledColumn, false, cycleControlArray);
        bool isBaseClass = false;
        SecurityHolderClass[] securityHolderClass = this.GetSecurityHolderClass(this.objectsTreeList.FocusedNode, out isBaseClass);
        if (securityHolderClass == null || securityHolderClass.Length == 0)
          return false;
        long uid = ((SecurityNodeClass) this.objectsTreeList.FocusedNode.Tag).UID;
        QuickObjectInfo quickObjectInfo = ((SecurityNodeClass) this.objectsTreeList.FocusedNode.Tag).QuickObjectInfo;
        ActionProperties tag = (ActionProperties) tln.Tag;
        for (int index = 0; index < securityHolderClass.Length; ++index)
        {
          DataRow row = securityHolderClass[index].GetRight(uid, quickObjectInfo, tag);
          if (row == null)
          {
            object beginDate = (object) DBNull.Value;
            object endDate = (object) DBNull.Value;
            object condition = (object) DBNull.Value;
            DataRow[] rights4User = securityHolderClass[index].GetRights4User(uid, quickObjectInfo);
            if (rights4User != null && rights4User.Length != 0)
            {
              beginDate = rights4User[0]["F_BEGIN_DATE"];
              endDate = rights4User[0]["F_END_DATE"];
              condition = rights4User[0]["F_CONDITION_ID"];
            }
            row = this.CreateNewRow(securityHolderClass[index], uid, quickObjectInfo, tag, beginDate, endDate, condition, 0L, AccessType.NoGrant);
            securityHolderClass[index].AccessDataTable.Rows.Add(row);
          }
          else
            row["F_PARENT_KEY"] = (object) 0;
          row["F_RIGHT_TYPE"] = (object) (int) this.AccessTypeConst(column, cValue);
          securityHolderClass[index].AccessDataTable.AcceptChanges();
          DataRow[] rights4User1 = securityHolderClass[index].GetRights4User(uid, quickObjectInfo);
          if (rights4User1 != null)
          {
            foreach (DataRow dataRow in rights4User1)
              dataRow["F_PARENT_KEY"] = (object) 0;
            securityHolderClass[index].AccessDataTable.AcceptChanges();
          }
          securityHolderClass[index].isChangedFlagOnly = true;
        }
        tln[(object) column] = (object) cValue;
        if (column == this.enabledColumn)
        {
          if (!cValue)
          {
            if (tag.ConnectedActions != null)
            {
              for (int index = 0; index < tag.ConnectedActions.Length; ++index)
              {
                TreeListNode rightNode = this.FindRightNode(tag.ConnectedActions[index]);
                if (rightNode != null)
                  this.SetNodeValue(rightNode, this.enabledColumn, false, cycleControlArray);
              }
            }
          }
          else
          {
            for (int index1 = 0; index1 < securityHolderClass[0].Actions.Length; ++index1)
            {
              if (!securityHolderClass[0].Actions[index1].Equals((object) tag) && securityHolderClass[0].Actions[index1].ConnectedActions != null)
              {
                for (int index2 = 0; index2 < securityHolderClass[0].Actions[index1].ConnectedActions.Length; ++index2)
                {
                  if (securityHolderClass[0].Actions[index1].ConnectedActions[index2] == tag.ActionID)
                  {
                    TreeListNode rightNode = this.FindRightNode(securityHolderClass[0].Actions[index1].ActionID);
                    if (rightNode != null)
                    {
                      this.SetNodeValue(rightNode, this.enabledColumn, true, cycleControlArray);
                      break;
                    }
                    break;
                  }
                }
              }
            }
          }
        }
        if (column == this.disabledColumn)
        {
          if (cValue)
          {
            if (tag.ConnectedActions != null)
            {
              for (int index = 0; index < tag.ConnectedActions.Length; ++index)
              {
                TreeListNode rightNode = this.FindRightNode(tag.ConnectedActions[index]);
                if (rightNode != null)
                  this.SetNodeValue(rightNode, this.disabledColumn, true, cycleControlArray);
              }
            }
          }
          else
          {
            for (int index3 = 0; index3 < securityHolderClass[0].Actions.Length; ++index3)
            {
              if (!securityHolderClass[0].Actions[index3].Equals((object) tag) && securityHolderClass[0].Actions[index3].ConnectedActions != null)
              {
                for (int index4 = 0; index4 < securityHolderClass[0].Actions[index3].ConnectedActions.Length; ++index4)
                {
                  if (securityHolderClass[0].Actions[index3].ConnectedActions[index4] == tag.ActionID)
                  {
                    TreeListNode rightNode = this.FindRightNode(securityHolderClass[0].Actions[index3].ActionID);
                    if (rightNode != null)
                    {
                      this.SetNodeValue(rightNode, this.disabledColumn, false, cycleControlArray);
                      break;
                    }
                    break;
                  }
                }
              }
            }
          }
        }
      }
    }
    finally
    {
      cycleControlArray.Remove((object) cycleControlClass);
    }
    return true;
  }

  private AccessType AccessTypeConst(TreeListColumn column, bool cValue)
  {
    AccessType accessType = AccessType.Default;
    if (column == this.disabledColumn)
      accessType = !cValue ? AccessType.NoGrant : AccessType.Deny;
    else if (column == this.enabledColumn)
      accessType = !cValue ? AccessType.NoGrant : AccessType.Grant;
    return accessType;
  }

  private void rightsTreeList_GetCustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
  {
    if (!(e.RepositoryItem is RepositoryItemCheckEdit))
      return;
    bool flag = false;
    if (e.Column == this.enabledColumn && !this.stateChangedEventAssigned4Enabled)
    {
      this.stateChangedEventAssigned4Enabled = true;
      flag = true;
    }
    if (e.Column == this.disabledColumn && !this.stateChangedEventAssigned4Disabled)
    {
      this.stateChangedEventAssigned4Disabled = true;
      flag = true;
    }
    if (!flag)
      return;
    ((RepositoryItemCheckEdit) e.RepositoryItem).CheckedChanged += new EventHandler(this.SecurityControl_CheckedChanged);
  }

  private void SecurityControl_CheckedChanged(object sender, EventArgs e)
  {
    if (this.blockOnCheckedChanged)
      return;
    CheckEdit checkEdit = (CheckEdit) sender;
    if (checkEdit == null)
      return;
    bool cValue = checkEdit.Checked;
    TreeListColumn column = (TreeListColumn) null;
    if (checkEdit.Properties.Name.Equals(this.enabledRepositoryItemCheckEdit.Name))
      column = this.enabledColumn;
    if (checkEdit.Properties.Name.Equals(this.disabledRepositoryItemCheckEdit.Name))
      column = this.disabledColumn;
    TreeListNode focusedNode = this.rightsTreeList.FocusedNode;
    if (this.blockOnChange_FillRights || this.blockOnChange_ProcessVirtualNodes || this.blockOnChange_CheckStateChanged || focusedNode == null || column == null)
      return;
    if (focusedNode.Tag is ActionProperties)
    {
      bool isBaseClass = false;
      SecurityHolderClass[] securityHolderClass = this.GetSecurityHolderClass(this.objectsTreeList.FocusedNode, out isBaseClass);
      if (securityHolderClass == null || securityHolderClass.Length == 0)
        return;
      long uid = ((SecurityNodeClass) this.objectsTreeList.FocusedNode.Tag).UID;
      QuickObjectInfo quickObjectInfo = ((SecurityNodeClass) this.objectsTreeList.FocusedNode.Tag).QuickObjectInfo;
      ActionProperties tag = (ActionProperties) focusedNode.Tag;
      DataRow[] dataRowArray = securityHolderClass[0].AccessDataTable.Select($"F_RIGHT_TYPE<>{Intermech.Consts.DeleteRecord.ToString()} and {SecurityProcs.F_UID}={uid.ToString()} and F_USER_ID={quickObjectInfo.ObjectID.ToString()} and F_RIGHT_ID={((int) tag.ActionID).ToString()}");
      if (dataRowArray == null || dataRowArray.Length == 0)
      {
        this.blockOnCheckedChanged = true;
        try
        {
          checkEdit.Checked = !checkEdit.Checked;
        }
        finally
        {
          this.blockOnCheckedChanged = false;
        }
        string text = $"Невозможно произвести изменение в редакторе прав доступа: отсутствует запись в таблице прав для {"F_USER_ID"}={quickObjectInfo.ObjectID} && {"F_RIGHT_ID"}={tag.ActionID}; CategoryID={securityHolderClass[0].CategoryDescriptor.CategoryID} CategoryType={securityHolderClass[0].CategoryDescriptor.CategoryType} objectName={securityHolderClass[0].ObjectName}";
        if (this.iSecurityCallback != null)
        {
          text += $"; CategoryMaintained={this.iSecurityCallback.MaintainedCategory}";
          if (this.iSecurityCallback.Applicability != null)
            text += $" CategoryIDApplicability={this.iSecurityCallback.Applicability.Item2} CategoryTypeApplicability={this.iSecurityCallback.Applicability.Item1}";
        }
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          sessionKeeper.Session.AddToTrace(text, 0);
          return;
        }
      }
      bool flag = false;
      switch (Convert.ToInt16(dataRowArray[0]["F_RIGHT_TYPE"]))
      {
        case 0:
          if (column == this.enabledColumn)
          {
            flag = tag.DefaultAccess;
            break;
          }
          break;
        case 2:
          if (column == this.enabledColumn)
          {
            flag = true;
            break;
          }
          break;
        case 3:
          if (column == this.disabledColumn)
          {
            flag = true;
            break;
          }
          break;
      }
      if (cValue == flag)
        return;
    }
    else if (focusedNode.Tag is ActionCategory)
    {
      bool enabledColumn = column == this.enabledColumn;
      bool state = this.actionCategoryStates.GetState((ActionCategory) focusedNode.Tag, enabledColumn);
      if (cValue == state)
        return;
    }
    this.blockOnChange_CheckStateChanged = true;
    try
    {
      this.SetNodeValue(focusedNode, column, cValue, new CycleControlArrayList());
    }
    finally
    {
      this.blockOnChange_CheckStateChanged = false;
    }
    this.ProcessVirtualNodes();
    this.UpdateControlStates();
    if (this.SecurityChanged == null)
      return;
    this.SecurityChanged((object) this, new EventArgs());
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
    if (this.objectsTreeList.FocusedNode == null)
      return;
    TreeListNode treeListNode1 = this.objectsTreeList.FocusedNode.Tag is SecurityNodeClass ? this.objectsTreeList.FocusedNode.ParentNode : this.objectsTreeList.FocusedNode;
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBObjectID[] dbObjectIdArray = SelectorForm.SelectObjects(new int[3]
    {
      service.RolesTypeID,
      service.GroupsTypeID,
      service.UsersTypeID
    }, true, false);
    if (dbObjectIdArray == null || dbObjectIdArray.Length == 0)
      return;
    bool isBaseClass = false;
    SecurityHolderClass[] securityHolderClass = this.GetSecurityHolderClass(treeListNode1, out isBaseClass);
    if (securityHolderClass == null || securityHolderClass.Length == 0)
      return;
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < dbObjectIdArray.Length; ++index)
      arrayList.Add((object) dbObjectIdArray[index].Value);
    if (arrayList.Count == 0)
      return;
    TreeListNode treeListNode2 = (TreeListNode) null;
    for (int index1 = 0; index1 < arrayList.Count; ++index1)
    {
      QuickObjectInfo qoi;
      if (SecurityHolderClass.FindQuickObjectInfo((long) arrayList[index1], (QuickObjectInfo[]) null, out qoi))
      {
        long getNewRightUid = SecurityProcs.GetNewRightUID;
        for (int index2 = 0; index2 < securityHolderClass.Length; ++index2)
        {
          for (int index3 = 0; index3 < securityHolderClass[index2].Actions.Length; ++index3)
          {
            AccessType at = this.AccessTypeConst(this.enabledColumn, securityHolderClass[index2].Actions[index3].DefaultAccess);
            DataRow newRow = this.CreateNewRow(securityHolderClass[index2], getNewRightUid, qoi, securityHolderClass[index2].Actions[index3], (object) DBNull.Value, (object) DBNull.Value, (object) DBNull.Value, 0L, at);
            securityHolderClass[index2].AccessDataTable.Rows.Add(newRow);
            securityHolderClass[index2].isChangedFlagOnly = true;
          }
          securityHolderClass[index2].AccessDataTable.AcceptChanges();
        }
        SecurityNodeClass securityNodeClass = new SecurityNodeClass(getNewRightUid, qoi, (object) DBNull.Value, (object) DBNull.Value, securityHolderClass[0].ConditionsEnabled, (object) Convert.ToInt64(0));
        TreeListNode tln = this.objectsTreeList.AppendNode((object) new object[1]
        {
          (object) qoi.Caption
        }, treeListNode1);
        tln.Tag = (object) securityNodeClass;
        this.AssignUGRCondition(tln);
        this.AssignUGRImageIndex(tln);
        if (treeListNode2 == null)
          treeListNode2 = tln;
      }
    }
    if (treeListNode2 == null)
      return;
    this.objectsTreeList.FocusedNode = treeListNode2;
    if (this.SecurityChanged == null)
      return;
    this.SecurityChanged((object) this, new EventArgs());
  }

  private void btnDelete_Click(object sender, EventArgs e)
  {
    if (!(this.objectsTreeList.FocusedNode.Tag is SecurityNodeClass tag) || MessageBox.Show(MessageDialogs.msgReallyDelete, MessageDialogs.msgConfirmDelete, MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    bool isBaseClass = false;
    SecurityHolderClass[] securityHolderClass = this.GetSecurityHolderClass(this.objectsTreeList.FocusedNode, out isBaseClass);
    if (securityHolderClass == null || securityHolderClass.Length == 0)
      return;
    long uid = tag.UID;
    QuickObjectInfo quickObjectInfo = tag.QuickObjectInfo;
    for (int index = 0; index < securityHolderClass.Length; ++index)
    {
      foreach (DataRow row in (InternalDataCollectionBase) securityHolderClass[index].AccessDataTable.Rows)
      {
        if (Convert.ToInt64(row[SecurityProcs.F_UID]) == uid && Convert.ToInt64(row["F_USER_ID"]) == quickObjectInfo.ObjectID)
        {
          securityHolderClass[index].isChangedFlagOnly = true;
          if (Convert.ToInt64(row["F_KEY"]) <= 0L)
            row.Delete();
          else
            row["F_RIGHT_TYPE"] = (object) Intermech.Consts.DeleteRecord;
        }
      }
      securityHolderClass[index].AccessDataTable.AcceptChanges();
    }
    this.objectsTreeList.DeleteNode(this.objectsTreeList.FocusedNode);
    if (this.SecurityChanged == null)
      return;
    this.SecurityChanged((object) this, new EventArgs());
  }

  private void btnStatus_Click(object sender, EventArgs e)
  {
    if (this.objectsTreeList.FocusedNode == null || !(this.objectsTreeList.FocusedNode.Tag is SecurityNodeClass))
      return;
    SecurityNodeClass tag = (SecurityNodeClass) this.objectsTreeList.FocusedNode.Tag;
    long objectId = tag.QuickObjectInfo.ObjectID;
    long uid = tag.UID;
    object beginDate = tag.BeginDate;
    object endDate = tag.EndDate;
    if (this.validDateForm == null)
      this.validDateForm = new ValidDateForm();
    if (this.validDateForm.Execute(ref beginDate, ref endDate, this.ActualReadonly) != DialogResult.OK)
      return;
    bool isBaseClass = false;
    SecurityHolderClass[] securityHolderClass = this.GetSecurityHolderClass(this.objectsTreeList.FocusedNode, out isBaseClass);
    if (securityHolderClass == null || securityHolderClass.Length == 0)
      return;
    for (int index = 0; index < securityHolderClass.Length; ++index)
    {
      foreach (DataRow row in (InternalDataCollectionBase) securityHolderClass[index].AccessDataTable.Rows)
      {
        if (Convert.ToInt64(row[SecurityProcs.F_UID]) == uid && Convert.ToInt64(row["F_USER_ID"]) == objectId && Convert.ToInt32(row["F_RIGHT_TYPE"]) != Intermech.Consts.DeleteRecord)
        {
          row["F_BEGIN_DATE"] = beginDate;
          row["F_END_DATE"] = endDate;
        }
      }
      securityHolderClass[index].AccessDataTable.AcceptChanges();
      securityHolderClass[index].IsChangedFlag = true;
    }
    tag.BeginDate = beginDate;
    tag.EndDate = endDate;
    this.AssignUGRStateIndex(this.objectsTreeList.FocusedNode);
    if (this.SecurityChanged == null)
      return;
    this.SecurityChanged((object) this, new EventArgs());
  }

  private void btnCondition_Click(object sender, EventArgs e)
  {
    if (this.objectsTreeList.FocusedNode == null || !(this.objectsTreeList.FocusedNode.Tag is SecurityNodeClass))
      return;
    SecurityNodeClass tag = (SecurityNodeClass) this.objectsTreeList.FocusedNode.Tag;
    long objectId = tag.QuickObjectInfo.ObjectID;
    long uid = tag.UID;
    if (!tag.ConditionsEnabled)
      return;
    object condition = tag.Condition;
    if (this.rightConditionForm == null)
      this.rightConditionForm = new RightConditionForm();
    if (this.rightConditionForm.Execute(ref condition, this.ActualReadonly) != DialogResult.OK)
      return;
    bool isBaseClass = false;
    SecurityHolderClass[] securityHolderClass = this.GetSecurityHolderClass(this.objectsTreeList.FocusedNode, out isBaseClass);
    if (securityHolderClass == null || securityHolderClass.Length == 0)
      return;
    for (int index = 0; index < securityHolderClass.Length; ++index)
    {
      foreach (DataRow row in (InternalDataCollectionBase) securityHolderClass[index].AccessDataTable.Rows)
      {
        if (Convert.ToInt64(row[SecurityProcs.F_UID]) == uid && Convert.ToInt64(row["F_USER_ID"]) == objectId && Convert.ToInt32(row["F_RIGHT_TYPE"]) != Intermech.Consts.DeleteRecord)
          row["F_CONDITION_ID"] = condition;
      }
      securityHolderClass[index].AccessDataTable.AcceptChanges();
      securityHolderClass[index].IsChangedFlag = true;
    }
    tag.Condition = condition;
    this.AssignUGRCondition(this.objectsTreeList.FocusedNode);
    this.AssignUGRStateIndex(this.objectsTreeList.FocusedNode);
    if (this.SecurityChanged == null)
      return;
    this.SecurityChanged((object) this, new EventArgs());
  }

  private void SecurityControl_Load(object sender, EventArgs e)
  {
  }

  private void objectsTreeList_KeyDown(object sender, KeyEventArgs e) => this.CheckAdminReset(e);

  private void CheckAdminReset(KeyEventArgs e)
  {
    if (!e.Shift || !e.Alt || e.KeyCode != Keys.R)
      return;
    this.RestoreAdminAccess();
  }

  private void cbInherit_CheckedChanged(object sender, EventArgs e)
  {
    if (this.blockOnChange_Inherit || this.SecurityChanged == null)
      return;
    this.SecurityChanged((object) this, new EventArgs());
  }

  private void objectsTreeList_GetCustomNodeCellStyle(
    object sender,
    GetCustomNodeCellStyleEventArgs e)
  {
    if (e == null)
      return;
    if (e.Node == this.objectsTreeList.FocusedNode)
      e.Style = this.rightsTreeList.Styles["SelectedRow"];
    else
      e.Style = this.rightsTreeList.Styles["Row"];
  }

  private void btnGrantAlwaysAccess_Click(object sender, EventArgs e)
  {
    if (this.grantAlwaysAccessNodeList == null)
      return;
    List<List<string>> report = new List<List<string>>();
    for (int index = 0; index < this.grantAlwaysAccessNodeList.Count; ++index)
    {
      if (this.grantAlwaysAccessNodeList[index].Tag is SecurityHolderClass[] tag && tag.Length != 0)
      {
        List<List<string>> grantAccessReport = this.GetGrantAccessReport(tag[0]);
        report.AddRange((IEnumerable<List<string>>) grantAccessReport);
      }
    }
    new SecurityAccessRightsReportForm().ShowReport(report);
  }

  /// <summary>
  /// сгенерировать отчет для SecurityHolderClass: одно право - одна строка
  /// </summary>
  /// <param name="shc"></param>
  /// <returns></returns>
  private List<List<string>> GetGrantAccessReport(SecurityHolderClass shc)
  {
    List<List<string>> grantAccessReport = new List<List<string>>();
    foreach (DataRow row in (InternalDataCollectionBase) shc.AccessDataTable.Rows)
    {
      if (Convert.ToInt32(row["F_RIGHT_TYPE"]) != Intermech.Consts.DeleteRecord && Convert.ToInt32(row["F_RIGHT_TYPE"]) == 4)
      {
        int int32_1 = Convert.ToInt32(row["F_RIGHT_ID"]);
        long int64 = Convert.ToInt64(row["F_USER_ID"]);
        int int32_2 = Convert.ToInt32(row["F_RIGHT_TYPE"]);
        QuickObjectInfo[] users = shc.Users;
        QuickObjectInfo quickObjectInfo;
        ref QuickObjectInfo local = ref quickObjectInfo;
        if (SecurityHolderClass.FindQuickObjectInfo(int64, users, out local))
          grantAccessReport.Add(new List<string>()
          {
            shc.ObjectName,
            quickObjectInfo.Caption,
            MetaDataHelper.GetObjectTypeName(quickObjectInfo.ObjectTypeID),
            this.GetActionName(int32_1, shc.Actions),
            AccessTypeHelper.GetCaption((AccessType) int32_2)
          });
      }
    }
    return grantAccessReport;
  }

  private string GetActionName(int rightId, ActionProperties[] actionProperties)
  {
    string actionName = string.Empty;
    for (int index = 0; index < actionProperties.Length; ++index)
    {
      if (actionProperties[index].ActionID == (ActionType) rightId)
      {
        actionName = actionProperties[index].Name;
        break;
      }
    }
    return actionName;
  }

  private enum States
  {
    Forbidden,
    Incoming,
    Active,
    Condition,
    ForbiddenCondition,
    IncomingCondition,
    ActiveCondition,
  }

  public delegate void SecurityChangedEventHandler(object sender, EventArgs e);

  internal class SncComparer : IComparer
  {
    public int Compare(object x, object y)
    {
      return ((SecurityNodeClass) x).QuickObjectInfo.Caption.CompareTo(((SecurityNodeClass) y).QuickObjectInfo.Caption);
    }
  }
}
