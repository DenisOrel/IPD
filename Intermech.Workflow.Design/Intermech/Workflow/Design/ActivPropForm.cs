// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivPropForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using ImSSP;
using Intermech.Client.Core;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.WebPortal;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Map;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Signs.Interfaces;
using Intermech.Site.Client;
using Intermech.Workflow.Briefcase;
using Intermech.Workflow.Design.ScriptPad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for PropForm.</summary>
public class ActivPropForm : Form
{
  private bool _modified;
  private Notifications notifs;
  private bool _loading;
  public bool NameModified;
  private bool _readOnly;
  private TempFormula _oldExpertCondition;
  private long _objectID;
  private long _processID;
  private ParticipantList Participants;
  private ConditionList _expertConditions;
  private ConditionInfo _selectedExpertCondition;
  private ExpressionInfo _selectedExpressionCondition;
  private ObservableCollection<ExpressionInfo> _expressionConditions = new ObservableCollection<ExpressionInfo>();
  private ExpressionInfo _expressionCondition;
  private int atype;
  private RequiredSigns requiredSigns;
  private const int ErrImageIndex = 10;
  /// <summary>
  /// Дополнительная колонка для списка, будет добавлять по мере надобности и удаляться так же
  /// </summary>
  private ColumnHeader _objectTypeColumn;
  private long _objectIDwithVars;
  private string _initialConditionText = "";
  private WorkflowNode _node;
  private IPortalConnector portalSrv;
  private Guid _currentSessionGuid = Guid.Empty;
  private IDBObject _activity;
  private ExtProperties _extProps;
  private List<Intermech.Expressions.Variable> _activityExpressionAttributes = new List<Intermech.Expressions.Variable>(0);
  private List<AttributeValues> _activityAllAttributeValues = new List<AttributeValues>(0);
  private bool _expressionConditionsModified;
  private bool _chekedRemoteWaitSaved;
  public Dictionary<long, LinkKind> CaseLinksWithModifiedLinkType = new Dictionary<long, LinkKind>();
  private AttachmentsView _attView;
  private int _signsGroupID;
  private IMSAttributeType graphsAttrType;
  private List<Guid> _signDTGuids = new List<Guid>();
  private List<string> _signDTNames = new List<string>();
  private ArrayList _signDTIDs = new ArrayList();
  private bool _signDTModified;
  private LCInfoList LCList;
  private PeriodInformation _periodInfo;
  private TimePeriodForm _tpf;
  private ScriptInfo[] _scripts = new ScriptInfo[2]
  {
    new ScriptInfo(),
    new ScriptInfo()
  };
  private bool _checkLocalScriptNotDelete = true;
  private bool _checkLocalScriptNotCreate;
  public List<long> LocalScriptsToDeleted = new List<long>();
  public Dictionary<int, long> NewScripts = new Dictionary<int, long>();
  public bool AddedNewScriptToDelete;
  private bool _additionalParticipantsModified;
  private Terms _terms;
  private Control[] _termEdits;
  private CheckBox[] _termCheckboxes;
  private List<string> _alienSettingsActs = new List<string>();
  private List<int> _pubFilteredTypes;
  private List<int> _pubFilteredRelTypes;
  private AdvNavigatorTreeView _attachTypesView;
  private bool _attachTypesViewLoading;
  private AllowedTypes _attachTypes;
  private List<NavigatorTreeNode> _expandedNodes = new List<NavigatorTreeNode>();
  private EnhToolTip ToolTip;
  private Panel Panel2;
  private TabControl PageControl;
  private TabPage BaseTabSheet;
  private Label txtCompletedLabel;
  private Label txtStartedLabel;
  private Label txtStatusLabel;
  private Label CompletedLabel;
  private Label StartedLabel;
  private Label StatusLabel;
  private UsersListView ParticipantsView;
  private CheckBox DenyDelCheck;
  private TabPage TermsTabSheet;
  private GroupBox GroupBox10;
  private CheckBox UnreadRollbackCheckBox;
  private GroupBox GroupBox8;
  private CheckBox UncompleteRollbackCheckBox;
  private TabPage ContentTabSheet;
  private Panel AttachsPanel;
  private Panel ContentOptionsPanel;
  private CheckBox AllowAddAttachsCheckBox;
  private CheckBox AllowDelAttachsCheckBox;
  private GroupBox CondGroupBox;
  private Button ValidateButton;
  private GroupBox GroupBox5;
  private Label Label3;
  private Label SubNameLabel;
  private CheckBox WaitCheckBox;
  private Button ConfigureButton;
  private GroupBox GroupBox7;
  private CheckBox WaitCompletionCheckBox;
  private CheckBox InterruptCheckBox;
  private System.Windows.Forms.ComboBox UnitsComboBox;
  private GroupBox GroupBox2;
  private RadioButton SrvRadioButton;
  private RadioButton UsrRadioButton;
  private GroupBox GroupBox1;
  private Label Label5;
  private Label Label7;
  private TextBox PluginDescMemo;
  private TextBox PlgNameLabel;
  private GroupBox WhatSignGB;
  private System.Windows.Forms.ComboBox WhatToSignCombo;
  private RadioButton DTypesRB1;
  private RadioButton DTypesRB2;
  private GroupBox RanksPanel;
  private RadioButton SignAsUserRadioButton;
  private RadioButton SignAsGraphRadioButton;
  private TabPage ArchiveTabSheet;
  private GroupBox RegisterGroupBox;
  private GroupBox RegDocsGroupBox;
  private GroupBox RollbackGroupBox;
  private RadioButton RollRadioButton1;
  private RadioButton RollRadioButton2;
  private RadioButton RollRadioButton3;
  private RadioButton RollRadioButton4;
  private GroupBox AfterScriptGroupBox;
  private GroupBox MsgsGroupBox;
  private ColumnHeader Column;
  private Button OkButton;
  private TabPage ParticipantsTabSheet;
  private IContainer components;
  private Panel ActRuntimePanel;
  private Panel panel1;
  private CheckBox CollectorCheckBox;
  private Label Label1;
  private TextBox NameEdit;
  private Label Label2;
  private PictureBox ActImage;
  private TextBox DescriptionMemo;
  private TabPage CondTabSheet;
  private TabPage AutoTabSheet;
  private TabPage ApproveTabSheet;
  private TabPage RollbackTabSheet;
  private TabPage NotifTabSheet;
  private TabPage ScriptTabSheet;
  private TabPage TimerTabSheet;
  private TabPage SubProcessTabSheet;
  private TabPage CaseTabSheet;
  private Label label8;
  private Button CancButton;
  private ButtonEdit ConditionBox;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private Panel panel3;
  private Button ValidateCaseButton;
  private ListView CondsView;
  private Label IDLabel;
  private ButtonEdit SchemeEdit;
  private ButtonEdit SubNameEdit;
  private ButtonEdit _caseEdit;
  private TabPage LCTabSheet;
  private GroupBox LCGroupBox;
  private Panel LCPanel1;
  private Label label9;
  private EnhListView LCView;
  private ColumnHeader columnHeader7;
  private ColumnHeader columnHeader8;
  private ToolBar LCStepToolBar;
  private ToolBarButton toolBarButton1;
  private ToolBarButton toolBarButton2;
  private EnhListView SignTypesView;
  private ColumnHeader columnHeader6;
  private CheckBox AfterScriptCheckBox;
  private GroupBox BeforeScriptGroupBox;
  private System.Windows.Forms.ComboBox AfterScriptExecCombo;
  private Label label6;
  private System.Windows.Forms.ComboBox BeforeScriptExecCombo;
  private Label label4;
  private CheckBox BeforeScriptCheckBox;
  private ButtonEdit unreadTermEdit;
  private ButtonEdit termEdit;
  private GroupBox RegRevGroupBox;
  private Panel RevSpacerPanel;
  private RadioButton ArcModeRadio2;
  private RadioButton ArcModeRadio1;
  private ButtonEdit RevArcIDEdit;
  private System.Windows.Forms.ComboBox RevArcVarCombo;
  private ButtonEdit DocArcIDEdit;
  private System.Windows.Forms.ComboBox DocArcVarCombo;
  private RadioButton DocArchiveVarRButton;
  private RadioButton DocArchiveRButton;
  private RadioButton RevArchiveVarRButton;
  private RadioButton RevArchiveRButton;
  private Panel DocSpacerPanel;
  private CheckBox DetachRegisteredCheckBox;
  private GroupBox ScriptGroupBox;
  private GroupBox ScriptUserGroupBox;
  private ButtonEdit ScriptUserEdit;
  private System.Windows.Forms.ComboBox ScriptExecCombo;
  private Label label10;
  private CheckBox CaseFilterCheckBox;
  private Panel SepPanel;
  private Splitter signSplitter;
  private ToolBarButton toolBarButton4;
  private Panel MsgAbortPanel;
  private Panel MsgPeriodPanel;
  private Panel MsgStopPanel;
  private CheckBox MsgStopCheckBox;
  private Button MsgStopButton;
  private Panel MsgStartPanel;
  private CheckBox MsgStartCheckBox;
  private Button MsgStartButton;
  private CheckBox MsgAbortCheckBox;
  private Button MsgAbortButton;
  private CheckBox MsgPeriodCheckBox;
  private Button MsgPeriodButton;
  private Panel panel8;
  private ToolBar GraphsBar;
  private ToolBarButton toolBarButton3;
  private ToolBarButton DeleteSignsButton;
  private EnhListView SignGraphView;
  private ColumnHeader columnHeader3;
  private ColumnHeader columnHeader4;
  private ColumnHeader columnHeader5;
  private Panel panel4;
  private ToolBar SignObjectTypesBar;
  private ToolBarButton AddObjTypesButton;
  private ToolBarButton DeleteObjTypesButton;
  private Panel ApproveVSpacer;
  private Panel panel11;
  private Panel RegRevSpacer;
  private Panel panel13;
  private TabPage RemoteSubProcessTabSheet;
  private GroupBox RemoteProcessGroupBox;
  private Label label11;
  private CheckBox RemoteWaitCheckBox;
  private System.Windows.Forms.ComboBox SitesComboBox;
  private Label label12;
  private Panel PortalErrPanel;
  private AutoSizeLabel PortalErrLabel;
  private PictureBox PortalErrImage;
  private System.Windows.Forms.ComboBox RemoteSchemesComboBox;
  private CheckBox GiveOwnershipCheckBox;
  private GroupBox TempRightsGroupBox;
  private CheckBox TempRightsGroupingCheckBox;
  private CheckBox TempRightsAdminCheckBox;
  private CheckBox TempRightsEditCheckBox;
  private CheckBox TempRightsViewCheckBox;
  private RadioButton NoTempRightsButton;
  private RadioButton RollRadioButton5;
  private GroupBox ApproveCheckGB;
  private Panel ApproveCheckSpacer;
  private Button ChooseApprovesButton;
  private TextBox ApprovesBox;
  private CheckBox UseAlienSettingsCheckBox;
  private CheckBox ApproveCheckOnlyCheckBox;
  private Label ApproveCheckHint;
  private Label UseAlienHint;
  private CheckBox PersonalSignsCheckBox;
  private Panel MsgReadPanel;
  private CheckBox MsgReadCheckBox;
  private Button MsgReadButton;
  private Button PubRelTypesButton;
  private Button PubObjectTypesButton;
  private GroupBox RemoteProcess2GroupBox;
  private Panel panel9;
  private Label label13;
  private System.Windows.Forms.ComboBox PubCompositionCombo;
  private CheckBox CreateReceiptCheckBox;
  private RadioButton DocArchiveCurrentRButton;
  private RadioButton RecArchiveCurrentRButton;
  private CheckBox RequireAnswerCheck;
  private Panel PartsPanel;
  private GroupBox PartKindGroupBox;
  private RadioButton AnyPartButton;
  private RadioButton AllPartsButton;
  private Button DelUserButton;
  private Button AddUserButton;
  private Label LCLabel1;
  private Panel LCPanel2;
  private EnhListView LCView2;
  private ColumnHeader columnHeader9;
  private ColumnHeader columnHeader10;
  private Label label15;
  private ToolBar LCStepToolBar2;
  private ToolBarButton toolBarButton5;
  private ToolBarButton toolBarButton6;
  private ToolBarButton toolBarButton7;
  private Label LCLabel2;
  private TabPage SchemeAttachsTabSheet;
  private GroupBox SchemeAttachsGB;
  private GroupBox TermOptsGroupBox;
  private RadioButton TermStartRB2;
  private RadioButton TermStartRB1;
  private Panel panel6;
  private Panel panel5;
  private Panel MsgBackPanel;
  private CheckBox MsgBackCheckBox;
  private Button MsgBackButton;
  private System.Windows.Forms.ComboBox afterScriptType;
  private System.Windows.Forms.ComboBox beforeScriptType;
  private System.Windows.Forms.ComboBox scriptType;
  private ButtonEdit afterScriptEdit;
  private Label label17;
  private ButtonEdit beforeScriptEdit;
  private Label label16;
  private ButtonEdit scriptEdit;
  private Label label14;
  private CheckBox showFormWhereActivityBack;
  private CheckBox sendParticipantsEmail;
  private ImageList MiscIL;
  private ImageList cmdsIL;
  private ImageList TabsIL;
  private CheckBox graphForTypeCheckBox;
  private SignsForType signsForType;
  private CheckBox useActualVersionSchemeCheckBox;
  private CheckBox sendWorkOfferLastParticipantCheckBox;
  private Button changeObjectTypeInExpression;
  private Button loadPublishOptions;
  private Button savePublishOptions;

  public ActivPropForm()
  {
    this.InitializeComponent();
    this.PageControl.ImageList = this.TabsIL;
    this.BaseTabSheet.ImageIndex = 0;
    this.ParticipantsTabSheet.ImageIndex = 1;
    this.SchemeAttachsTabSheet.ImageIndex = 2;
    this.ContentTabSheet.ImageIndex = 2;
    this.CondTabSheet.ImageIndex = 3;
    this.SubProcessTabSheet.ImageIndex = 4;
    this.CaseTabSheet.ImageIndex = 5;
    this.AutoTabSheet.ImageIndex = 6;
    this.RollbackTabSheet.ImageIndex = 7;
    this.ApproveTabSheet.ImageIndex = 8;
    this.TimerTabSheet.ImageIndex = 9;
    this.ArchiveTabSheet.ImageIndex = 10;
    this.ScriptTabSheet.ImageIndex = 11;
    this.LCTabSheet.ImageIndex = 12;
    this.TermsTabSheet.ImageIndex = 13;
    this.NotifTabSheet.ImageIndex = 14;
    this.RemoteSubProcessTabSheet.ImageIndex = 15;
    foreach (TabPage tabPage1 in this.PageControl.TabPages)
    {
      TabPage tabPage2 = tabPage1;
      Padding padding1 = tabPage1.Padding;
      int left = padding1.Left;
      padding1 = tabPage1.Padding;
      int top = padding1.Top;
      padding1 = tabPage1.Padding;
      int right = padding1.Right + 2;
      padding1 = tabPage1.Padding;
      int bottom = padding1.Bottom;
      Padding padding2 = new Padding(left, top, right, bottom);
      tabPage2.Padding = padding2;
    }
    this.PageControl.Multiline = false;
    this.ParticipantsView.AddButton = this.AddUserButton;
    this.ParticipantsView.DelButton = this.DelUserButton;
    this.ParticipantsView.Columns.AddRange(new ColumnHeader[1]
    {
      this.Column
    });
    this.MsgStartCheckBox.Tag = (object) this.MsgStartButton;
    this.MsgReadCheckBox.Tag = (object) this.MsgReadButton;
    this.MsgPeriodCheckBox.Tag = (object) this.MsgPeriodButton;
    this.MsgStopCheckBox.Tag = (object) this.MsgStopButton;
    this.MsgAbortCheckBox.Tag = (object) this.MsgAbortButton;
    this.MsgBackCheckBox.Tag = (object) this.MsgBackButton;
    this.AddedNewScriptToDelete = false;
    this._objectTypeColumn = new ColumnHeader()
    {
      Text = "Тип объекта",
      Name = "ObjectTypeColumn",
      Width = 150,
      DisplayIndex = 0
    };
  }

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (!value)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, (value ? 1 : 0) != 0, new List<Control>((IEnumerable<Control>) new Control[11]
      {
        (Control) this.MsgStartButton,
        (Control) this.MsgPeriodButton,
        (Control) this.MsgStopButton,
        (Control) this.MsgAbortButton,
        (Control) this.MsgReadButton,
        (Control) this.MsgBackButton,
        (Control) this.beforeScriptEdit,
        (Control) this.afterScriptEdit,
        (Control) this.scriptEdit,
        (Control) this.ValidateButton,
        (Control) this.ValidateCaseButton
      }));
    }
  }

  private long RollbackKind
  {
    get
    {
      for (int index = 0; index < this.RollbackGroupBox.Controls.Count; ++index)
      {
        if (this.RollbackGroupBox.Controls[index] is RadioButton && ((RadioButton) this.RollbackGroupBox.Controls[index]).Checked)
          return (long) Convert.ToInt32(this.RollbackGroupBox.Controls[index].Tag);
      }
      return 0;
    }
    set
    {
      bool flag1 = false;
      bool flag2 = false;
      if (this._node != null)
      {
        foreach (WorkflowLink link in this._node.Links)
        {
          if (link != null)
          {
            if (link.Backward && link.FromNode == this._node)
              flag1 = true;
            else if (link.LinkKind == LinkKind.ParallelBlock && link.ToNode == this._node)
              flag2 = true;
          }
        }
        if ((int) value == 2 && !flag1 || (int) value == 4 && !flag2)
          value = 0L;
        if (flag2)
          value = 4L;
        else if (flag1)
          value = 2L;
      }
      bool flag3 = flag2 | flag1;
      this.RollRadioButton1.Enabled = !flag3;
      this.RollRadioButton2.Enabled = !flag3;
      this.RollRadioButton3.Enabled = flag1;
      this.RollRadioButton4.Enabled = !flag3;
      for (int index = 0; index < this.RollbackGroupBox.Controls.Count; ++index)
      {
        if (this.RollbackGroupBox.Controls[index] is RadioButton && value == (long) Convert.ToInt32(this.RollbackGroupBox.Controls[index].Tag))
        {
          (this.RollbackGroupBox.Controls[index] as RadioButton).Checked = true;
          break;
        }
      }
      if (!this.RollRadioButton5.Checked)
        return;
      this.RollRadioButton5.Bounds = this.RollRadioButton1.Bounds;
      this.RollRadioButton5.Visible = true;
      this.RollRadioButton1.Visible = false;
    }
  }

  private void HidePage(ref TabPage page)
  {
    this.PageControl.TabPages.Remove(page);
    page = (TabPage) null;
  }

  private ExtProperties ExtProps
  {
    get
    {
      if (this._extProps == null)
        this._extProps = new ExtProperties(this._activity, wfConsts.AttrAddInfoID);
      return this._extProps;
    }
  }

  public void GetProperties(IDBObject Activity, WorkflowNode node)
  {
    this._loading = true;
    this._node = node;
    this._activity = Activity;
    if (!this.ReadOnly && this._activity is IActivity activity)
    {
      if (node?.View != null)
      {
        try
        {
          IDBObject process = node.View.GetProcess(activity.Session);
          VarList varList = new VarList(activity.Session, false, false);
          varList.Load(process);
          varList.AddSystemVariables(process);
          foreach (Intermech.Workflow.Variable variable in varList)
          {
            this._activityExpressionAttributes.Add(MiscFunx.CreateExpressionVariable(variable.Name, variable.VarType));
            this._activityAllAttributeValues.Add(new AttributeValues(variable.AttrTypeID, variable.TypedValue)
            {
              AttributeName = variable.Name
            });
          }
          if (process is IScheme)
          {
            foreach (Intermech.Workflow.Variable variable in (VarList) new GlobalVariablesList(process, false, false))
            {
              this._activityExpressionAttributes.Add(MiscFunx.CreateExpressionVariable(variable.Name, variable.VarType));
              this._activityAllAttributeValues.Add(new AttributeValues(variable.AttrTypeID, variable.TypedValue)
              {
                AttributeName = variable.Name
              });
            }
          }
        }
        catch
        {
        }
      }
    }
    try
    {
      this.atype = Activity.ObjectType;
      if (node != null)
      {
        this.ActImage.Image = node.Image.Image;
      }
      else
      {
        Icon icon = BaseHolder.IconService.GetIcon(4, this.atype);
        if (icon != null)
          this.ActImage.Image = (Image) icon.ToBitmap();
      }
      IUserSession session = Activity.Session;
      this._objectID = Activity.ObjectID;
      this.IDLabel.Visible = false;
      if (this.atype == wfConsts.SchemesTypeID || this.atype == wfConsts.ProcessesTypeID)
        this._processID = this._objectID;
      else if (node?.View != null)
      {
        this._processID = node.View.ProcessID;
      }
      else
      {
        IDBAttribute attributeById = Activity.GetAttributeByID(wfConsts.AttrProcessID);
        if (attributeById != null)
          this._processID = attributeById.AsInteger;
        if (this._objectID < 0L)
          this._processID = -this._processID;
      }
      this.ParticipantsView.ProcessID = this._processID;
      if (this.atype == wfConsts.SchemesTypeID)
      {
        IDBAttribute attributeById = Activity.GetAttributeByID(wfConsts.AttrShowFormWithActivityBackID);
        this.showFormWhereActivityBack.Visible = true;
        this.showFormWhereActivityBack.Enabled = true;
        if (attributeById != null)
          this.showFormWhereActivityBack.Checked = attributeById.AsBoolean;
      }
      else
      {
        this.showFormWhereActivityBack.Visible = false;
        this.showFormWhereActivityBack.Enabled = false;
      }
      IDBAttribute attributeById1 = Activity.GetAttributeByID(wfConsts.AttrNameID);
      if (attributeById1 != null)
      {
        this.NameEdit.Text = attributeById1.AsString;
        string str = LocalizationHolder.rm.GetString("Workflow.Design_7");
        if (this.atype == wfConsts.SchemesTypeID)
          str = LocalizationHolder.rm.GetString("Workflow.Design_8");
        else if (this.atype == wfConsts.ProcessesTypeID)
          str = LocalizationHolder.rm.GetString("Workflow.Design_9");
        this.Text = $"{str} \"{this.NameEdit.Text}\"";
      }
      else
        this.NameEdit.Enabled = false;
      IDBAttribute byId1 = Activity.Attributes.FindByID(wfConsts.AttrDescriptionID);
      if (byId1 != null)
        this.DescriptionMemo.Text = byId1.AsString;
      else
        this.DescriptionMemo.Enabled = false;
      IDBAttribute attributeById2 = Activity.GetAttributeByID(wfConsts.AttrCollectorID);
      if (attributeById2 != null && this.atype != wfConsts.StartTypeID && this.atype != wfConsts.StopTypeID)
      {
        this.CollectorCheckBox.Checked = attributeById2.AsBoolean;
      }
      else
      {
        this.CollectorCheckBox.Enabled = false;
        this.CollectorCheckBox.Visible = false;
      }
      ActivityStatus activityStatus = ActivityStatus.OnApproach;
      IDBAttribute attributeById3 = Activity.GetAttributeByID(wfConsts.AttrActivityStatusID);
      if (attributeById3 != null)
      {
        this.ActRuntimePanel.Visible = true;
        this.StatusLabel.Visible = true;
        this.txtStatusLabel.Visible = true;
        if (this.atype == wfConsts.SchemesTypeID)
        {
          if (attributeById3.AsInteger < 0L)
            this.StatusLabel.Text = SimpleFuncs.GetEnumDescription((Enum) (SchemeStatus) attributeById3.AsInteger);
          else
            this.StatusLabel.Text = "";
        }
        else
        {
          activityStatus = (ActivityStatus) attributeById3.AsInteger;
          this.StatusLabel.Text = SimpleFuncs.GetEnumDescription((Enum) activityStatus);
          if (activityStatus != ActivityStatus.OnApproach && this.atype == wfConsts.RemoteSubProcessTypeID)
          {
            IDBAttribute attributeById4 = Activity.GetAttributeByID(wfConsts.AttrRemoteProcessStatusID);
            if (attributeById4 != null)
            {
              string enumDescription = SimpleFuncs.GetEnumDescription((Enum) (RemoteProcessStatus) attributeById4.AsInteger);
              if (enumDescription != "")
                this.StatusLabel.Text += $" ({enumDescription})";
            }
          }
        }
      }
      this._objectIDwithVars = activityStatus == ActivityStatus.OnApproach ? this._processID : this._objectID;
      bool flag1 = false;
      bool flag2 = false;
      IDBAttribute byId2 = Activity.Attributes.FindByID(wfConsts.AttrStartedID);
      if (byId2 != null)
      {
        this.ActRuntimePanel.Visible = true;
        this.StartedLabel.Visible = true;
        this.txtStartedLabel.Visible = true;
        this.StartedLabel.Text = byId2.AsString;
        flag1 = true;
      }
      IDBAttribute byId3 = Activity.Attributes.FindByID(wfConsts.AttrCompletedID);
      if (byId3 != null)
      {
        this.ActRuntimePanel.Visible = true;
        this.CompletedLabel.Visible = true;
        this.txtCompletedLabel.Visible = true;
        this.CompletedLabel.Text = byId3.AsString;
        flag2 = true;
      }
      if (!flag1)
        this.ActRuntimePanel.Height -= this.StartedLabel.Height;
      if (!flag2)
        this.ActRuntimePanel.Height -= this.CompletedLabel.Height;
      IDBAttribute attributeById5 = Activity.GetAttributeByID(wfConsts.AttrRecipID);
      if (attributeById5 != null && !attributeById5.IsNull)
      {
        this.Participants = new ParticipantList();
        this.Participants.AddParticipant(ParticipantKind.User, attributeById5.AsInteger);
      }
      else
      {
        IDBAttribute byId4 = Activity.Attributes.FindByID(wfConsts.AttrParticipantsID);
        if (byId4 != null)
        {
          this.Participants = new ParticipantList();
          string addData = byId4.Value.ToString();
          if (activityStatus == ActivityStatus.ParticipantWaiting)
          {
            addData = ParticipantList.ExtractAddData(addData);
            this.Participants.XmlSection = "Expanded";
          }
          this.Participants.AsString = addData;
          this.AnyPartButton.Checked = !this.Participants.EveryOne;
          if (this.atype == wfConsts.StartTypeID)
          {
            this.ParticipantsView.Enabled = false;
            this.PartsPanel.Enabled = false;
            this.RequireAnswerCheck.Enabled = false;
          }
          bool flag3 = this.ExtProps.Ini.ReadBoolean("Props", "SendWorkOfferLastParticipant", false);
          if (this.AnyPartButton.Checked)
          {
            this.sendWorkOfferLastParticipantCheckBox.Enabled = true;
            this.sendWorkOfferLastParticipantCheckBox.Checked = flag3;
          }
          else
          {
            this.sendWorkOfferLastParticipantCheckBox.Enabled = false;
            this.sendWorkOfferLastParticipantCheckBox.Checked = false;
          }
        }
      }
      if (this.Participants == null || this.atype == wfConsts.ScriptTypeID)
      {
        this.HidePage(ref this.ParticipantsTabSheet);
      }
      else
      {
        if (this.atype == wfConsts.RemoteSubProcessTypeID)
        {
          this.Participants.ParticipantsChanged += new ParticipantList.ModifyItems(this.Participants_ParticipantsChanged);
          this.AnyPartButton.Checked = true;
          this.AllPartsButton.Visible = false;
          this.sendParticipantsEmail.Checked = true;
          this.sendParticipantsEmail.Visible = false;
        }
        else
          this.sendParticipantsEmail.Checked = this.ExtProps.Ini.ReadBoolean("Props", "sendParticipantsEmail", true);
        this.ParticipantsView.Participants = this.Participants;
      }
      IDBAttribute attributeById6 = Activity.GetAttributeByID(wfConsts.AttrAddIDID);
      ActivityFlags activityFlags = attributeById6 != null ? (ActivityFlags) attributeById6.AsInteger : (ActivityFlags) 0;
      this.DenyDelCheck.Checked = activityFlags.HasFlag((Enum) ActivityFlags.DenyDeletionFromMail);
      this.RequireAnswerCheck.Checked = activityFlags.HasFlag((Enum) ActivityFlags.RequireAnswerText);
      IDBAttribute attributeById7 = this.atype == wfConsts.StartTypeID ? (IDBAttribute) null : Activity.GetAttributeByID(wfConsts.AttrRollbackKindID);
      if (attributeById7 != null)
      {
        this.RollbackKind = attributeById7.AsInteger;
        this.RollRadioButton4.Enabled = this.RollRadioButton4.Enabled && this.atype != wfConsts.RegisterTypeID;
      }
      else
        this.HidePage(ref this.RollbackTabSheet);
      if (this.atype == wfConsts.AbortTypeID)
      {
        this.MsgStartPanel.Visible = false;
        this.MsgPeriodPanel.Visible = false;
      }
      IDBAttribute attributeById8 = Activity.GetAttributeByID(wfConsts.AttrNotificationsID);
      this.notifs = new Notifications(this._activity.Session);
      if (attributeById8 != null)
      {
        this.notifs.Load(attributeById8);
        this.MsgStartCheckBox.Checked = this.notifs.StartNotify.Enabled;
        this.MsgPeriodCheckBox.Checked = this.notifs.PeriodNotify.Enabled;
        this.MsgStopCheckBox.Checked = this.notifs.StopNotify.Enabled;
        this.MsgAbortCheckBox.Checked = this.notifs.AbortNotify.Enabled;
        this.MsgReadCheckBox.Checked = this.notifs.ReadNotify.Enabled;
        this.MsgBackCheckBox.Checked = this.notifs.BackNotify.Enabled;
        bool flag4 = this.atype == wfConsts.SchemesTypeID || this.atype == wfConsts.ProcessesTypeID;
        this.MsgAbortPanel.Visible = flag4;
        this.MsgReadPanel.Visible = !flag4 && wfConsts.IsParticipantActivity(ActivityInfos.ActivityTypeToKind(this.atype));
        this.MsgBackPanel.Visible = this.RollbackTabSheet != null;
      }
      else
        this.HidePage(ref this.NotifTabSheet);
      if (this.atype == wfConsts.CondTypeID)
      {
        IDBAttribute byId5 = Activity.Attributes.FindByID(wfConsts.AttrConditionID);
        if (byId5 != null)
        {
          this._oldExpertCondition = MiscFunx.FormulaFromAttribute(byId5);
          if (this._oldExpertCondition == null)
          {
            this._expressionCondition = MiscFunx.GetExpressionFromAttr(Activity.Attributes.FindByID(wfConsts.AttrConditionFormulaID));
            this._initialConditionText = this._expressionCondition.ToString();
          }
          else
            this._initialConditionText = this._oldExpertCondition.ToString();
          this.ConditionBox.Text = this._initialConditionText;
        }
        else
        {
          IDBAttribute byId6 = Activity.Attributes.FindByID(wfConsts.AttrConditionFormulaID);
          if (byId6 != null)
          {
            this._expressionCondition = MiscFunx.GetExpressionFromAttr(byId6);
            this._initialConditionText = this._expressionCondition.ToString();
            this.ConditionBox.Text = this._initialConditionText;
          }
        }
      }
      else
        this.PageControl.TabPages.Remove(this.CondTabSheet);
      if (this.atype == wfConsts.CaseTypeID)
      {
        this.CaseFilterCheckBox.CheckedChanged -= new EventHandler(this.CaseFilterCheckBox_CheckedChanged);
        this.CaseFilterCheckBox.Checked = activityFlags.HasFlag((Enum) ActivityFlags.FilterObjects);
        this.CaseFilterCheckBox.CheckedChanged += new EventHandler(this.CaseFilterCheckBox_CheckedChanged);
        IDBAttribute byId7 = Activity.Attributes.FindByID(wfConsts.AttrConditionID);
        if (byId7 != null)
        {
          this._expertConditions = new ConditionList(byId7);
          if (this._expertConditions.IsEmpty)
          {
            this._expressionConditions = new ObservableCollection<ExpressionInfo>((IEnumerable<ExpressionInfo>) MiscFunx.GetExpressionListFromAttr(Activity.Attributes.FindByID(wfConsts.AttrConditionFormulaID)));
            this._expressionConditions.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ExpressionConditions_CollectionChanged);
          }
          this.RefreshCaseConditions();
        }
        else
        {
          IDBAttribute byId8 = Activity.Attributes.FindByID(wfConsts.AttrConditionFormulaID);
          if (byId8 != null)
          {
            this._expressionConditions = new ObservableCollection<ExpressionInfo>((IEnumerable<ExpressionInfo>) MiscFunx.GetExpressionListFromAttr(byId8));
            this.RefreshCaseConditions();
          }
        }
      }
      else
        this.PageControl.TabPages.Remove(this.CaseTabSheet);
      if (this.atype == wfConsts.SubProcessTypeID)
      {
        IDBAttribute byId9 = Activity.Attributes.FindByID(wfConsts.AttrSubprocessSchemeID);
        if (byId9 != null)
          this.SubprocessID = byId9.AsInteger;
        IDBAttribute byId10 = Activity.Attributes.FindByID(wfConsts.AttrSubprocFormatID);
        if (byId10 != null)
          this.SubNameEdit.Text = byId10.AsString;
        IDBAttribute byId11 = Activity.Attributes.FindByID(wfConsts.AttrWaitForCompletionID);
        if (byId11 != null)
          this.WaitCheckBox.Checked = byId11.AsBoolean;
        this.useActualVersionSchemeCheckBox.Checked = this.ExtProps.ReadBool("UseActualSchemeVersion");
      }
      else
        this.HidePage(ref this.SubProcessTabSheet);
      this.graphForTypeCheckBox.Checked = this.ExtProps.ReadBool("GraphForType");
      IDBAttribute attributeById9 = Activity.GetAttributeByID(wfConsts.AttrGraphForTypeID);
      if (attributeById9 != null)
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (SignsDataItemModel));
        string s = attributeById9.Value.ToString();
        if (!string.IsNullOrEmpty(s))
        {
          using (TextReader textReader = (TextReader) new StringReader(s))
          {
            SignsDataItemModel signsDataItemModel = xmlSerializer.Deserialize(textReader) as SignsDataItemModel;
            foreach (SignsDataItem node1 in (Collection<SignsDataItem>) signsDataItemModel.Nodes)
            {
              node1.SetChild();
              this.signsForType.SignsDataItemModel.Nodes.Add(node1);
            }
            this.signsForType.SignsDataItemModel.PersonalSigns = signsDataItemModel.PersonalSigns;
          }
        }
      }
      IDBAttribute attributeById10 = Activity.GetAttributeByID(wfConsts.AttrRequiredSignsID);
      if (attributeById10 != null)
      {
        this.requiredSigns = new RequiredSigns(attributeById10);
        this.FillSignGraphs();
        this.requiredSigns.Modified = false;
        this.PersonalSignsCheckBox.Checked = this.SignAsGraphRadioButton.Checked && this.ExtProps.ReadBool("PersonalSigns");
        IDBAttribute attributeById11 = Activity.GetAttributeByID(wfConsts.AttrObjectTypesID);
        if (attributeById11 != null)
        {
          if (!attributeById11.IsNull)
          {
            foreach (object obj in attributeById11.Values)
            {
              if (!(obj.ToString() == ""))
              {
                Guid guid = new Guid(obj.ToString());
                IDBObjectType objectType = session.GetObjectType(guid, false);
                if (objectType != null)
                {
                  this.DTypesRB2.Checked = true;
                  this._signDTGuids.Add(guid);
                  this._signDTIDs.Add((object) objectType.ObjectType);
                  this._signDTNames.Add(objectType.ObjectTypeName);
                }
                else
                {
                  SimpleBriefcase globalBriefcase = BriefcaseAccessor.GlobalBriefcase;
                  if (globalBriefcase != null)
                  {
                    this._signDTGuids.Add(guid);
                    this._signDTIDs.Add((object) 0);
                    MapperObject mapperObject = globalBriefcase.Map.Get(Domain.ObjectTypes, guid);
                    this._signDTNames.Add("?? " + (mapperObject == null ? guid.ToString() : mapperObject.Caption));
                  }
                  this._signDTModified = true;
                }
              }
              else
                break;
            }
          }
          this.FillSignDocTypes();
        }
        this.DTypesRB_CheckedChanged((object) null, (EventArgs) null);
        IDBAttribute attributeById12 = Activity.GetAttributeByID(wfConsts.AttrWhatToSignID);
        if (attributeById12 != null)
          this.WhatToSignCombo.SelectedIndex = (int) attributeById12.AsInteger;
        this.ApproveCheckOnlyCheckBox.Checked = this.ExtProps.ReadBool("TestOnly");
        this.AlienSettingsActs = this.ExtProps.Read("SettingsActs");
        this.ApproveCheckOnlyCheckBox_CheckedChanged((object) null, (EventArgs) null);
      }
      else
        this.HidePage(ref this.ApproveTabSheet);
      if (this.atype == wfConsts.SchemesTypeID || this.atype == wfConsts.ProcessesTypeID)
      {
        this.HidePage(ref this.LCTabSheet);
      }
      else
      {
        IDBAttribute attributeById13 = Activity.GetAttributeByID(wfConsts.AttrLCConfigAttrID);
        if (attributeById13 != null && !attributeById13.IsNull)
        {
          this.LCList = new LCInfoList();
          this.LCList.Load(attributeById13);
        }
      }
      if (this.atype == wfConsts.TimerTypeID)
      {
        this._periodInfo = new PeriodInformation(this._activity.Session);
        string str = this.ExtProps.Read("TimerPeriod");
        if (str == "" && this.ExtProps.Ini.Root.Name == "Period")
          str = this.ExtProps.Ini.AsString;
        this._periodInfo.AsString = str;
        this._tpf = new TimePeriodForm(this._objectIDwithVars);
        this._tpf.Embedded = true;
        this._tpf.Parent = (Control) this.TimerTabSheet;
        this._tpf.Visible = true;
        this._tpf.Dock = DockStyle.Top;
        this._tpf.SetPeriodInformation(this._periodInfo);
      }
      else
        this.PageControl.TabPages.Remove(this.TimerTabSheet);
      if (this.atype == wfConsts.SchemesTypeID || this.atype == wfConsts.ProcessesTypeID)
      {
        this.HidePage(ref this.ContentTabSheet);
      }
      else
      {
        this.HidePage(ref this.SchemeAttachsTabSheet);
        this.ContentOptionsPanel.Visible = this.ParticipantsTabSheet != null;
        if (this.ParticipantsTabSheet != null)
        {
          this.AllowAddAttachsCheckBox.Checked = !activityFlags.HasFlag((Enum) ActivityFlags.DenyAttach);
          this.AllowDelAttachsCheckBox.Checked = !activityFlags.HasFlag((Enum) ActivityFlags.DenyDetach);
        }
        bool flag5 = this.Participants != null;
        this.TempRightsGroupBox.Visible = flag5;
        if (flag5)
        {
          IDBAttribute attributeById14 = Activity.GetAttributeByID(wfConsts.AttrTempRightsID);
          TemporaryRights temporaryRights = attributeById14 != null ? (TemporaryRights) attributeById14.AsInteger : TemporaryRights.None;
          this.NoTempRightsButton.Checked = temporaryRights == TemporaryRights.None;
          this.TempRightsViewCheckBox.Checked = (temporaryRights & TemporaryRights.View) != 0;
          this.TempRightsEditCheckBox.Checked = (temporaryRights & TemporaryRights.Edit) != 0;
          this.TempRightsAdminCheckBox.Checked = (temporaryRights & TemporaryRights.Admin) != 0;
          this.TempRightsGroupingCheckBox.Checked = (temporaryRights & TemporaryRights.HandleGrouped) != 0;
          this.TempRightsGroupingCheckBox.Enabled = temporaryRights != 0;
        }
      }
      if (this.atype == wfConsts.SchemesTypeID || this.atype == wfConsts.ProcessesTypeID)
      {
        this.HidePage(ref this.ScriptTabSheet);
      }
      else
      {
        if (this.ParticipantsTabSheet == null && this.atype != wfConsts.StartTypeID)
        {
          this.BeforeScriptExecCombo.Tag = (object) 1;
          this.BeforeScriptExecCombo.SelectedIndex = 0;
          this.AfterScriptExecCombo.Tag = (object) 1;
          this.AfterScriptExecCombo.SelectedIndex = 0;
        }
        IDBRelationCollection relationCollection = Activity.Session.GetRelationCollection(wfConsts.ScriptRelationTypeID);
        relationCollection.LocalTypesMode = true;
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[6]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) wfConsts.AttrScriptKindID,
          (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
          (object) wfConsts.AttrScriptExecSideID,
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
          (object) ObligatoryObjectAttributes.CAPTION
        });
        foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, this._objectID).Rows)
        {
          int index = 0;
          if (!row[1].Equals((object) DBNull.Value))
            index = Convert.ToInt32(row[1]);
          this._scripts[index].ScriptID = Convert.ToInt64(row[0]);
          this._scripts[index].OldScriptID = this._scripts[index].ScriptID;
          this._scripts[index].ScriptLinkID = Convert.ToInt64(row[2]);
          if (!row[3].Equals((object) DBNull.Value))
            this._scripts[index].ExecSide = (ScriptExecSide) Convert.ToInt64(row[3]);
          this._scripts[index].ScriptType = Convert.ToInt32(row[4]) == wfConsts.WorkflowCommonScript ? WorkflowScriptType.Common : WorkflowScriptType.Local;
          this._scripts[index].ScriptCaption = row[5].ToString();
        }
        if (this.atype == wfConsts.ScriptTypeID)
        {
          this.BeforeScriptGroupBox.Visible = false;
          this.AfterScriptGroupBox.Visible = false;
          this.ScriptGroupBox.Visible = true;
          this.ScriptExecCombo.SelectedIndex = (int) this._scripts[0].ExecSide;
          this.scriptType.SelectedIndex = (int) this._scripts[0].ScriptType;
          if (this.Participants == null)
            this.Participants = new ParticipantList();
          this.ScriptUserEdit.Text = this.Participants.ToUserString();
          this.scriptEdit.Text = this._scripts[0].ScriptCaption;
          this.ScriptExecCombo.Enabled = true;
          this.scriptType.Enabled = this._scripts[0].ScriptID == 0L;
        }
        else if (this.atype == wfConsts.StartTypeID || this.atype == wfConsts.TaskTypeID || this.atype == wfConsts.ApproveTypeID)
        {
          this.BeforeScriptCheckBox.Checked = this._scripts[0].ScriptID != 0L;
          this.BeforeScriptExecCombo.SelectedIndex = (int) this._scripts[0].ExecSide;
          this.beforeScriptType.SelectedIndex = (int) this._scripts[0].ScriptType;
          this.beforeScriptEdit.Text = this._scripts[0].ScriptCaption;
          this.BeforeScriptExecCombo.Enabled = true;
          this.beforeScriptType.Enabled = this._scripts[0].ScriptID == 0L;
          this.BeforeScriptExecCombo.Tag = (object) 0;
          this.AfterScriptCheckBox.Checked = this._scripts[1].ScriptID != 0L;
          this.AfterScriptExecCombo.SelectedIndex = (int) this._scripts[1].ExecSide;
          this.afterScriptType.SelectedIndex = (int) this._scripts[1].ScriptType;
          this.afterScriptEdit.Text = this._scripts[1].ScriptCaption;
          this.AfterScriptExecCombo.Enabled = true;
          this.afterScriptType.Enabled = this._scripts[1].ScriptID == 0L;
          this.AfterScriptExecCombo.Tag = (object) 0;
          this.BeforeScriptCheckBox_CheckedChanged((object) null, (EventArgs) null);
        }
        else
        {
          this.BeforeScriptExecCombo.Tag = (object) 1;
          this.AfterScriptExecCombo.Tag = (object) 1;
          this.BeforeScriptCheckBox.Checked = this._scripts[0].ScriptID != 0L;
          this.BeforeScriptExecCombo.SelectedIndex = 0;
          this.beforeScriptType.SelectedIndex = (int) this._scripts[0].ScriptType;
          this.beforeScriptEdit.Text = this._scripts[0].ScriptCaption;
          this.BeforeScriptExecCombo.Enabled = false;
          this.beforeScriptType.Enabled = this._scripts[0].ScriptID == 0L;
          this.AfterScriptCheckBox.Checked = this._scripts[1].ScriptID != 0L;
          this.AfterScriptExecCombo.SelectedIndex = 0;
          this.afterScriptType.SelectedIndex = (int) this._scripts[1].ScriptType;
          this.afterScriptEdit.Text = this._scripts[1].ScriptCaption;
          this.AfterScriptExecCombo.Enabled = false;
          this.afterScriptType.Enabled = this._scripts[1].ScriptID == 0L;
          this.BeforeScriptCheckBox_CheckedChanged((object) null, (EventArgs) null);
        }
      }
      if (this.atype == wfConsts.RegisterTypeID)
      {
        VarList vars = new VarList(session.GetObject(this._objectIDwithVars), false, false);
        if (this.FillArcGroupBox(session, Activity, wfConsts.AttrDocArchiveID, vars, this.RegDocsGroupBox) != this.FillArcGroupBox(session, Activity, wfConsts.AttrRevArchiveID, vars, this.RegRevGroupBox))
        {
          this.ArcModeRadio2.Checked = true;
          this.RegRevGroupBox.Visible = true;
          this.RegRevSpacer.Visible = this.RegRevGroupBox.Visible;
        }
        this.DetachRegisteredCheckBox.Checked = activityFlags.HasFlag((Enum) ActivityFlags.DetachRegisteredObjects);
      }
      else
        this.HidePage(ref this.ArchiveTabSheet);
      if (this.ParticipantsTabSheet != null)
      {
        this.unreadTermEdit.Enabled = false;
        this._terms = new Terms((IUserSession) null);
        this._terms.Load(Activity);
        this._termEdits = new Control[2]
        {
          (Control) this.termEdit,
          (Control) this.unreadTermEdit
        };
        this._termCheckboxes = new CheckBox[2]
        {
          this.UncompleteRollbackCheckBox,
          this.UnreadRollbackCheckBox
        };
        if (activityFlags.HasFlag((Enum) ActivityFlags.StartTermsWithWorkOffers))
          this.TermStartRB1.Checked = true;
        else
          this.TermStartRB2.Checked = true;
        this.UpdateTerms();
      }
      else
        this.PageControl.TabPages.Remove(this.TermsTabSheet);
      this.PageControl.TabPages.Remove(this.AutoTabSheet);
      if (this.atype == wfConsts.RemoteSubProcessTypeID)
      {
        this._currentSessionGuid = session.SessionGUID;
        this.portalSrv = session.GetCustomService(typeof (IPortalConnector)) as IPortalConnector;
        if (this.portalSrv != null)
        {
          if (session.GetCustomService(typeof (ISitesCacheService)) is ISitesCacheService customService && customService.Info != null)
          {
            foreach (SiteInfo site in customService.Sites)
            {
              if (site.ID != customService.Info.ID)
                this.SitesComboBox.Items.Add((object) site);
            }
          }
          this.RemoteSchemesComboBox.DropDown += new EventHandler(this.RemoteSchemesComboBox_DropDown);
        }
        else
        {
          this.PortalErrImage.Image = this.MiscIL.Images[10];
          this.PortalErrPanel.Visible = true;
          this.RemoteProcessGroupBox.Enabled = false;
          this.RemoteProcess2GroupBox.Enabled = false;
        }
        string g = this.ExtProps.Read("Site");
        string name = "";
        if (g != "")
        {
          Guid guid = new Guid(g);
          foreach (SiteInfo siteInfo in this.SitesComboBox.Items)
          {
            if (siteInfo.GUID == guid)
            {
              this.SitesComboBox.SelectedItem = (object) siteInfo;
              break;
            }
          }
          name = this.ExtProps.Read("TplName");
          if (name != "")
          {
            this.RemoteSchemesComboBox.Items.Add((object) new ProcessTemplateInfo(new Guid(this.ExtProps.Read("TplGuid")), name));
            this.RemoteSchemesComboBox.SelectedIndex = 0;
          }
        }
        IDBAttribute attributeById15 = Activity.GetAttributeByID(wfConsts.AttrWaitForCompletionID);
        if (attributeById15 != null)
          this.RemoteWaitCheckBox.Checked = attributeById15.AsBoolean;
        if (this.Participants != null && this.Participants.Count > 0)
          this.RemoteWaitCheckBox.Visible = false;
        this.GiveOwnershipCheckBox.Checked = this.ExtProps.ReadBool("GiveOwnership");
        int num1 = (int) this.ExtProps.ReadInteger("MaxCompositionLevel", -1L);
        int num2;
        switch (num1)
        {
          case -1:
            num2 = 0;
            break;
          case 0:
            num2 = 2;
            break;
          case 1:
            num2 = 1;
            break;
          default:
            num2 = num1 + 1;
            break;
        }
        this.PubCompositionCombo.SelectedIndex = num2;
        this.CreateReceiptCheckBox.Checked = this.ExtProps.ReadBool("CreateReceipt");
        if (this.SitesComboBox.SelectedItem == null && this.SitesComboBox.Items.Count > 0)
          this.SitesComboBox.SelectedIndex = 0;
        this.SitesComboBox.SelectedIndexChanged += new EventHandler(this.SitesComboBox_SelectedIndexChanged);
        if (name == "")
          this.SitesComboBox_SelectedIndexChanged((object) null, (EventArgs) null);
        this._pubFilteredTypes = this.ExtProps.ReadList<int>("FTypes");
        this._pubFilteredRelTypes = this.ExtProps.ReadList<int>("FRelTypes");
      }
      else
        this.HidePage(ref this.RemoteSubProcessTabSheet);
      this._modified = false;
      this.PageControl.SelectedIndex = 0;
    }
    finally
    {
      this._loading = false;
      this._activity = (IDBObject) null;
    }
  }

  private void ExpressionConditions_CollectionChanged(
    object sender,
    NotifyCollectionChangedEventArgs e)
  {
    this._expressionConditionsModified = true;
  }

  private void Participants_ParticipantsChanged()
  {
    if (this.RemoteWaitCheckBox.Checked && !this._chekedRemoteWaitSaved)
      this._chekedRemoteWaitSaved = true;
    this.RemoteWaitCheckBox.Checked = this.Participants.Count == 0 && this._chekedRemoteWaitSaved;
    this.RemoteWaitCheckBox.Visible = this.Participants.Count == 0;
  }

  public bool SetProperties(IDBObject Activity)
  {
    if (this.ReadOnly)
      return false;
    this._activity = Activity;
    IUserSession session = Activity.Session;
    if (this.NameEdit.Enabled && this.NameEdit.Modified)
    {
      IDBAttribute byId = Activity.Attributes.FindByID(wfConsts.AttrNameID);
      if (byId.AsString != this.NameEdit.Text)
      {
        this.NameModified = true;
        this._modified = true;
        byId.AsString = this.NameEdit.Text;
      }
    }
    if (this.DescriptionMemo.Enabled && this.DescriptionMemo.Modified)
    {
      IDBAttribute byId = Activity.Attributes.FindByID(wfConsts.AttrDescriptionID);
      if (byId.AsString != this.DescriptionMemo.Text)
      {
        this._modified = true;
        byId.AsString = this.DescriptionMemo.Text;
      }
    }
    if (this.showFormWhereActivityBack.Enabled)
    {
      Activity.Attributes.AddAttribute(wfConsts.AttrShowFormWithActivityBackID, false, new object[1]
      {
        (object) this.showFormWhereActivityBack.Checked
      });
      this._modified = true;
    }
    if (this.CollectorCheckBox.Enabled)
    {
      IDBAttribute byId = Activity.Attributes.FindByID(wfConsts.AttrCollectorID);
      if (byId != null && this.CollectorCheckBox.Checked != byId.AsBoolean)
      {
        byId.AsBoolean = this.CollectorCheckBox.Checked;
        this._modified = true;
      }
    }
    bool flag = this.ParticipantsView.Modified;
    if (this.Participants != null && this.Participants.EveryOne != this.AllPartsButton.Checked)
      flag = true;
    if (this._additionalParticipantsModified)
      flag = true;
    if (flag && this.Participants != null)
    {
      IDBAttribute byId = Activity.Attributes.FindByID(wfConsts.AttrParticipantsID);
      if (byId != null)
      {
        this.Participants.EveryOne = this.AllPartsButton.Checked;
        byId.Value = (object) this.Participants.AsString;
      }
      else
        Activity.Attributes.AddAttribute(wfConsts.AttrParticipantsID, false, new object[1]
        {
          (object) this.Participants.AsString
        });
    }
    if (flag)
      this._modified = true;
    if (this.Participants != null)
      this.ExtProps.Ini.WriteBoolean("Props", "sendParticipantsEmail", this.sendParticipantsEmail.Checked);
    if (flag && this.Participants != null)
      this.ExtProps.Ini.WriteBoolean("Props", "SendWorkOfferLastParticipant", this.sendWorkOfferLastParticipantCheckBox.Enabled && this.sendWorkOfferLastParticipantCheckBox.Checked);
    ActivityFlags activityFlags = (ActivityFlags) 0;
    long num1 = 0;
    IDBAttribute attributeById1 = Activity.GetAttributeByID(wfConsts.AttrAddIDID);
    if (attributeById1 != null)
      num1 = attributeById1.AsInteger;
    if (this.DenyDelCheck.Checked)
      activityFlags |= ActivityFlags.DenyDeletionFromMail;
    if (this.RequireAnswerCheck.Checked)
      activityFlags |= ActivityFlags.RequireAnswerText;
    if (this.RollbackTabSheet != null)
    {
      IDBAttribute attributeById2 = Activity.GetAttributeByID(wfConsts.AttrRollbackKindID);
      if (attributeById2 != null && attributeById2.AsInteger != this.RollbackKind)
      {
        attributeById2.AsInteger = this.RollbackKind;
        this._modified = true;
      }
    }
    if (this.notifs.Modified)
    {
      this._modified = true;
      IDBAttribute byId = Activity.Attributes.FindByID(wfConsts.AttrNotificationsID);
      if (byId != null)
        this.notifs.Save(byId);
    }
    if (this.atype == wfConsts.CondTypeID)
    {
      if (this._oldExpertCondition == null)
      {
        IDBAttribute attr = Activity.Attributes.AddAttribute(wfConsts.AttrConditionFormulaID, false);
        if (this._expressionCondition != null && !this._expressionCondition.ToString().Equals(this._initialConditionText))
        {
          this._modified = true;
          MiscFunx.ExpressionToAttribute(this._expressionCondition, attr);
        }
      }
      else if (!this._oldExpertCondition.ToString().Equals(this._initialConditionText))
      {
        IDBAttribute byId = Activity.Attributes.FindByID(wfConsts.AttrConditionID);
        if (byId != null)
        {
          this._modified = true;
          MiscFunx.FormulaToAttribute(this._oldExpertCondition, byId);
        }
      }
    }
    if (this.atype == wfConsts.CaseTypeID)
    {
      if (this._expertConditions == null || this._expertConditions.IsEmpty)
      {
        IDBAttribute attr = Activity.Attributes.AddAttribute(wfConsts.AttrConditionFormulaID, false);
        if (this._expressionConditionsModified)
        {
          this._modified = true;
          MiscFunx.ExpressionsToAttribute(new List<ExpressionInfo>((IEnumerable<ExpressionInfo>) this._expressionConditions), attr);
        }
      }
      else if (this._expertConditions.Modified)
      {
        IDBAttribute byId = Activity.Attributes.FindByID(wfConsts.AttrConditionID);
        if (byId != null)
        {
          this._modified = true;
          this._expertConditions.Save(byId);
        }
      }
    }
    if (this.CaseFilterCheckBox.Checked)
      activityFlags |= ActivityFlags.FilterObjects;
    if (this.atype == wfConsts.SubProcessTypeID)
    {
      IDBAttribute byId1 = Activity.Attributes.FindByID(wfConsts.AttrSubprocessSchemeID);
      if (byId1 != null && byId1.AsInteger != Convert.ToInt64(this.SchemeEdit.Tag))
      {
        this._modified = true;
        byId1.AsInteger = Convert.ToInt64(this.SchemeEdit.Tag);
      }
      IDBAttribute byId2 = Activity.Attributes.FindByID(wfConsts.AttrSubprocFormatID);
      if (byId2 != null && byId2.AsString != this.SubNameEdit.Text)
      {
        this._modified = true;
        byId2.AsString = this.SubNameEdit.Text;
      }
      IDBAttribute byId3 = Activity.Attributes.FindByID(wfConsts.AttrWaitForCompletionID);
      if (byId3 != null && byId3.AsBoolean != this.WaitCheckBox.Checked)
      {
        this._modified = true;
        byId3.AsBoolean = this.WaitCheckBox.Checked;
      }
      this.ExtProps.WriteBool("UseActualSchemeVersion", this.useActualVersionSchemeCheckBox.Checked, ExtPropertiesFlag.SubProcess);
    }
    if (this.ApproveTabSheet != null)
    {
      if (this.graphForTypeCheckBox.Checked)
      {
        if (!this.UseAlienSettingsCheckBox.Checked)
        {
          this.ExtProps.WriteBool("GraphForType", true, ExtPropertiesFlag.Approve);
          using (MemoryStream memoryStream = new MemoryStream())
          {
            new XmlSerializer(typeof (SignsDataItemModel)).Serialize((Stream) memoryStream, (object) this.signsForType.SignsDataItemModel);
            memoryStream.Position = 0L;
            using (StreamReader streamReader = new StreamReader((Stream) memoryStream))
            {
              Activity.Attributes.AddAttribute(wfConsts.AttrGraphForTypeID, false, new object[1]
              {
                (object) streamReader.ReadToEnd()
              });
              this._modified = true;
            }
          }
        }
        else
          this.ExtProps.WriteBool("GraphForType", false, ExtPropertiesFlag.Approve);
      }
      else
      {
        this.ExtProps.WriteBool("GraphForType", false, ExtPropertiesFlag.Approve);
        if (this.requiredSigns != null)
        {
          if (this.requiredSigns.Modified)
          {
            this._modified = true;
            IDBAttribute byId = Activity.Attributes.FindByID(wfConsts.AttrRequiredSignsID);
            if (byId != null)
            {
              if (this.SignAsGraphRadioButton.Checked)
              {
                this.UpdateRequiredStrongSigns();
                this.requiredSigns.Save(byId);
              }
              else
                byId.Clear();
            }
          }
          IDBAttribute attributeById3 = Activity.GetAttributeByID(wfConsts.AttrWhatToSignID);
          if (attributeById3 != null && (long) this.WhatToSignCombo.SelectedIndex != attributeById3.AsInteger)
          {
            this._modified = true;
            attributeById3.AsInteger = (long) this.WhatToSignCombo.SelectedIndex;
          }
          this.ExtProps.WriteBool("PersonalSigns", this.PersonalSignsCheckBox.Checked, ExtPropertiesFlag.Approve);
        }
        if (this._signDTModified)
        {
          IDBAttribute attributeById4 = Activity.GetAttributeByID(wfConsts.AttrObjectTypesID);
          if (attributeById4 != null)
          {
            if (this.DTypesRB1.Checked)
            {
              attributeById4.ClearValues();
            }
            else
            {
              object[] objArray = new object[this._signDTGuids.Count];
              for (int index = 0; index < this._signDTGuids.Count; ++index)
                objArray[index] = (object) this._signDTGuids[index];
              if (objArray.Length == 0)
                attributeById4.ClearValues();
              else
                attributeById4.Values = objArray;
            }
            this._modified = true;
          }
        }
      }
      this.ExtProps.WriteBool("TestOnly", this.ApproveCheckOnlyCheckBox.Checked, ExtPropertiesFlag.Approve);
      this.ExtProps.Write("SettingsActs", this.AlienSettingsActs, ExtPropertiesFlag.Approve);
    }
    if (this.LCList != null && this.LCList.Modified)
    {
      this._modified = true;
      IDBAttribute attributeById5 = Activity.GetAttributeByID(wfConsts.AttrLCConfigAttrID);
      if (attributeById5 != null)
        this.LCList.Save(attributeById5);
      else
        Activity.Attributes.AddAttribute(wfConsts.AttrLCConfigAttrID, false, new object[1]
        {
          (object) this.LCList.AsString
        });
    }
    if (this._periodInfo != null)
    {
      this._tpf.FillPeriodInformation(ref this._periodInfo, Activity.Session);
      if (this._periodInfo.Modified)
      {
        this.ExtProps.Write("TimerPeriod", this._periodInfo.AsString, ExtPropertiesFlag.Timer);
        this._modified = true;
      }
    }
    if (this._attView != null && this._attView.Modified)
    {
      this._attView.Save(Activity);
      this._modified = true;
    }
    if (this.ContentTabSheet != null && this.ParticipantsTabSheet != null)
    {
      if (!this.AllowAddAttachsCheckBox.Checked)
        activityFlags |= ActivityFlags.DenyAttach;
      if (!this.AllowDelAttachsCheckBox.Checked)
        activityFlags |= ActivityFlags.DenyDetach;
    }
    if (this.SchemeAttachsTabSheet != null && this._attachTypes != null)
    {
      string asString = this._attachTypes.AsString;
      if (asString != this._attachTypes.PrevAsString)
      {
        Activity.Attributes.AddAttribute(wfConsts.AttrAllowedAttachTypesID, false, new object[1]
        {
          (object) asString
        });
        this._modified = true;
      }
    }
    if (this.ContentTabSheet != null && this.Participants != null)
    {
      IDBAttribute attributeById6 = Activity.GetAttributeByID(wfConsts.AttrTempRightsID);
      TemporaryRights temporaryRights1 = attributeById6 != null ? (TemporaryRights) attributeById6.AsInteger : TemporaryRights.None;
      TemporaryRights temporaryRights2 = TemporaryRights.None;
      if (this.TempRightsViewCheckBox.Checked)
        temporaryRights2 |= TemporaryRights.View;
      if (this.TempRightsEditCheckBox.Checked)
        temporaryRights2 |= TemporaryRights.Edit;
      if (this.TempRightsAdminCheckBox.Checked)
        temporaryRights2 |= TemporaryRights.Admin;
      if (this.TempRightsGroupingCheckBox.Checked)
        temporaryRights2 |= TemporaryRights.HandleGrouped;
      if (temporaryRights2 != temporaryRights1)
      {
        if (attributeById6 == null)
          Activity.Attributes.AddAttribute(wfConsts.AttrTempRightsID, false, new object[1]
          {
            (object) (long) temporaryRights2
          });
        else
          attributeById6.AsInteger = (long) temporaryRights2;
        this._modified = true;
      }
    }
    if (this.atype == wfConsts.ScriptTypeID)
    {
      this.SaveScriptInfo(Activity, this._scripts[0], ScriptKind.BeforeExec, (CheckBox) null, this.ScriptExecCombo);
    }
    else
    {
      this.SaveScriptInfo(Activity, this._scripts[0], ScriptKind.BeforeExec, this.BeforeScriptCheckBox, this.BeforeScriptExecCombo);
      this.SaveScriptInfo(Activity, this._scripts[1], ScriptKind.AfterExec, this.AfterScriptCheckBox, this.AfterScriptExecCombo);
    }
    if (this.atype == wfConsts.RegisterTypeID)
    {
      if (this.SaveArcGroupBox(Activity, wfConsts.AttrDocArchiveID, this.RegDocsGroupBox))
        this._modified = true;
      if (this.ArcModeRadio2.Checked)
      {
        if (this.SaveArcGroupBox(Activity, wfConsts.AttrRevArchiveID, this.RegRevGroupBox))
          this._modified = true;
      }
      else
      {
        IDBAttribute attributeById7 = Activity.GetAttributeByID(wfConsts.AttrRevArchiveID);
        IDBAttribute attributeById8 = Activity.GetAttributeByID(wfConsts.AttrDocArchiveID);
        if (attributeById7 != null && attributeById8 != null && attributeById7.AsString != attributeById8.AsString)
        {
          attributeById7.AsString = attributeById8.AsString;
          this._modified = true;
        }
      }
      if (this.DetachRegisteredCheckBox.Checked)
        activityFlags |= ActivityFlags.DetachRegisteredObjects;
    }
    if (this.atype == wfConsts.RemoteSubProcessTypeID)
    {
      string str1 = "";
      string str2 = "";
      string str3 = "";
      if (this.SitesComboBox.SelectedItem is SiteInfo selectedItem1)
      {
        str1 = selectedItem1.GUID.ToString();
        if (this.RemoteSchemesComboBox.SelectedItem is ProcessTemplateInfo selectedItem)
        {
          str2 = selectedItem.Name;
          str3 = selectedItem.Guid.ToString();
        }
      }
      if (this.ExtProps.Write("Site", str1, ExtPropertiesFlag.RemoteSubprocess))
        this._modified = true;
      if (this.ExtProps.Write("TplName", str2, ExtPropertiesFlag.RemoteSubprocess))
        this._modified = true;
      if (this.ExtProps.Write("TplGuid", str3, ExtPropertiesFlag.RemoteSubprocess))
        this._modified = true;
      IDBAttribute attributeById9 = Activity.GetAttributeByID(wfConsts.AttrWaitForCompletionID);
      if (attributeById9 != null && attributeById9.AsBoolean != this.RemoteWaitCheckBox.Checked)
      {
        this._modified = true;
        attributeById9.AsBoolean = this.RemoteWaitCheckBox.Checked;
      }
      if (this.GiveOwnershipCheckBox.Checked != this.ExtProps.ReadBool("GiveOwnership"))
      {
        this.ExtProps.WriteBool("GiveOwnership", this.GiveOwnershipCheckBox.Checked, ExtPropertiesFlag.RemoteSubprocess);
        this._modified = true;
      }
      int selectedIndex = this.PubCompositionCombo.SelectedIndex;
      int num2;
      switch (selectedIndex)
      {
        case 0:
          num2 = -1;
          break;
        case 1:
          num2 = 1;
          break;
        case 2:
          num2 = 0;
          break;
        default:
          num2 = selectedIndex - 1;
          break;
      }
      if (this.ExtProps.Write("MaxCompositionLevel", (long) num2, ExtPropertiesFlag.RemoteSubprocess, "-1"))
        this._modified = true;
      if (this.CreateReceiptCheckBox.Checked != this.ExtProps.ReadBool("CreateReceipt"))
      {
        this.ExtProps.WriteBool("CreateReceipt", this.CreateReceiptCheckBox.Checked, ExtPropertiesFlag.RemoteSubprocess);
        this._modified = true;
      }
      if (this.ExtProps.WriteList<int>("FTypes", this._pubFilteredTypes, ExtPropertiesFlag.RemoteSubprocess))
        this._modified = true;
      if (this.ExtProps.WriteList<int>("FRelTypes", this._pubFilteredRelTypes, ExtPropertiesFlag.RemoteSubprocess))
        this._modified = true;
    }
    if (this._terms != null)
    {
      if (this.AnyPartButton.Checked && this.TermStartRB1.Checked)
        activityFlags |= ActivityFlags.StartTermsWithWorkOffers;
      else
        activityFlags &= ~ActivityFlags.StartTermsWithWorkOffers;
      if (this._terms.Modified)
      {
        this._terms.Save(Activity);
        this._modified = true;
      }
    }
    if (num1 != (long) activityFlags)
    {
      this._modified = true;
      Activity.Attributes.AddAttribute(wfConsts.AttrAddIDID, false, new object[1]
      {
        (object) (int) activityFlags
      });
    }
    if (this.ExtProps != null && this.ExtProps.Modified)
    {
      this.ExtProps.Save(Activity);
      this._modified = true;
    }
    return this._modified;
  }

  private void UpdateRequiredStrongSigns()
  {
    foreach (ListViewItem listViewItem in this.SignGraphView.Items)
    {
      if (listViewItem.SubItems.Count > 1 && listViewItem.SubItems[2] is CheckBoxListViewSubItem)
      {
        CheckBoxListViewSubItem subItem = (CheckBoxListViewSubItem) listViewItem.SubItems[2];
        (subItem.Tag as GraphClass).StrongCheck = subItem.Checked;
      }
    }
  }

  private void ActivPropForm_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Escape)
      this.DialogResult = DialogResult.Cancel;
    else if (e.Shift && e.Control && e.Alt)
    {
      if (e.KeyCode == Keys.F)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._objectID, wfConsts.AttrFormID);
          long asInteger = objectAttributeById != null ? objectAttributeById.AsInteger : 0L;
          if (asInteger <= 0L)
            return;
          IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(asInteger, new Guid("cad0011d-306c-11d8-b4e9-00304f19f545"));
          if (!(objectAttributeByGuid is IBlobReader))
            return;
          using (FileStream aDestStream = new FileStream(Holder.WorkflowTempPath + "form.xml", FileMode.Create))
            new BlobProcReader(objectAttributeByGuid, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
        }
      }
      else if (e.KeyCode == Keys.V)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject src = sessionKeeper.Session.GetObject(this._objectID);
          if (src == null)
            return;
          int num = (int) MessageBox.Show(new VarList(src, false, false).ToString());
        }
      }
      else if (e.KeyCode == Keys.C)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectID);
          if (dbObject == null || MessageBox.Show("Clear vars?", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return;
          new VarList(sessionKeeper.Session, false, false).Save(dbObject, true);
          this._modified = true;
        }
      }
      else if (e.KeyCode == Keys.I)
      {
        if (this._node == null)
          return;
        int num = (int) MessageBox.Show(string.Format(this._node.DebugInfo));
      }
      else
      {
        if (e.KeyCode != Keys.T || this._periodInfo == null)
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject varSource = sessionKeeper.Session.GetObject(this._objectID);
          if (varSource == null)
            return;
          int num = (int) MessageBox.Show("ExecTime in UTC=" + this._periodInfo.GetExecTime(varSource).ToString());
        }
      }
    }
    else
    {
      if (!e.Shift || e.KeyCode != Keys.Insert || this.ReadOnly || this.PageControl.SelectedTab != this.ParticipantsTabSheet)
        return;
      this.ParticipantsView.AddParticipant(new Participant(ParticipantKind.Variable, (long) wfConsts.SysVarStarterID));
    }
  }

  private Notification GetNotification(int index)
  {
    return this.notifs.List.Count >= index ? this.notifs.List[index - 1] : (Notification) null;
  }

  private bool EditMessage(int index)
  {
    using (ComposeMessageForm composeMessageForm = new ComposeMessageForm())
    {
      composeMessageForm.ProcessID = this._objectIDwithVars;
      Notification notification = this.GetNotification(index);
      if (notification == null)
        return false;
      composeMessageForm.Notification = notification;
      composeMessageForm.ReadOnly = this.ReadOnly;
      notification.Enabled = true;
      return composeMessageForm.ShowDialog() == DialogResult.OK;
    }
  }

  private void EditMessageClick(object sender, EventArgs e)
  {
    this.EditMessage(Convert.ToInt32((sender as Control).Tag));
  }

  private void MsgStartCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    Control tag = (Control) (sender as Control).Tag;
    tag.Enabled = (sender as CheckBox).Checked;
    if (this._loading)
      return;
    int int32 = Convert.ToInt32(tag.Tag);
    if (tag.Enabled)
    {
      if (this.EditMessage(int32))
        return;
      (sender as CheckBox).Checked = false;
    }
    else
    {
      Notification notification = this.GetNotification(int32);
      if (notification == null)
        return;
      notification.Enabled = false;
    }
  }

  private void ActivPropForm_Load(object sender, EventArgs e)
  {
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
    dictionary.Add("RanksPanelH", this.RanksPanel.Height);
    FormStorage.LoadLayout((Control) this, (IDictionary) dictionary);
    this.RanksPanel.Height = dictionary["RanksPanelH"];
  }

  private void ActivPropForm_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this, (IDictionary) new Dictionary<string, int>()
    {
      {
        "RanksPanelH",
        this.RanksPanel.Height
      }
    });
  }

  private void ConditionBox_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this._oldExpertCondition == null)
    {
      string exp = this._expressionCondition == null ? string.Empty : this._expressionCondition.ToString();
      if (this._expressionCondition == null)
        this._expressionCondition = new ExpressionInfo(-1, Guid.Empty, -1L, string.Empty);
      if (!wfFunx.EditExpression(ref exp, new List<Intermech.Expressions.Variable>(0), this._activityExpressionAttributes))
        return;
      this.ConditionBox.Text = exp;
      this._expressionCondition.FormulaForLink = exp;
    }
    else
    {
      if (!wfFunx.EditExpression(ref this._oldExpertCondition, this._processID))
        return;
      this.ConditionBox.Text = this._oldExpertCondition.ToString();
    }
  }

  private void CondsView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
  {
    ListViewItem listViewItem = this.CondsView.Items[e.Item];
    Rectangle rectangle = listViewItem.GetBounds(ItemBoundsPortion.Icon);
    Rectangle bounds = listViewItem.GetBounds(ItemBoundsPortion.Entire);
    rectangle = new Rectangle(rectangle.Right, rectangle.Top, bounds.Width - rectangle.Right, bounds.Height - rectangle.Top);
    if (this._caseEdit == null)
    {
      this._caseEdit = new ButtonEdit();
      this._caseEdit.Parent = (Control) this.CondsView;
      this._caseEdit.Leave += new EventHandler(this._caseEdit_Leave);
      this._caseEdit.Properties.BorderStyle = BorderStyles.Simple;
      this._caseEdit.Properties.ReadOnly = true;
      this._caseEdit.ButtonClick += new ButtonPressedEventHandler(this._caseEdit_ButtonClick);
    }
    this._selectedExpertCondition = listViewItem.Tag as ConditionInfo;
    if (this._selectedExpertCondition == null)
    {
      this._selectedExpressionCondition = listViewItem.Tag as ExpressionInfo;
      this._caseEdit.Text = this._selectedExpressionCondition.ToString();
    }
    else
      this._caseEdit.Text = this._selectedExpertCondition.ToString();
    this._caseEdit.Bounds = rectangle;
    this._caseEdit.Visible = true;
    this._caseEdit.Focus();
    this._caseEdit.DeselectAll();
  }

  private void _caseEdit_Leave(object sender, EventArgs e) => this._caseEdit.Visible = false;

  private void _caseEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this._selectedExpertCondition == null)
    {
      LinkKind linkKind1 = this._selectedExpressionCondition.ElseLink ? LinkKind.False : LinkKind.True;
      List<Intermech.Expressions.Variable> variables = new List<Intermech.Expressions.Variable>(0);
      if (this.CaseFilterCheckBox.Checked)
      {
        if (this._selectedExpressionCondition.ObjectTypeForLink != -1)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            BasicAttributeProperties[] enabledAttributes = sessionKeeper.Session.GetObjectType(this._selectedExpressionCondition.ObjectTypeForLink).Attributes.GetEnabledAttributes(true);
            variables.AddRange((IEnumerable<Intermech.Expressions.Variable>) MiscFunx.ConvertBasicAttributePropertiesToVariable(enabledAttributes));
          }
        }
        else if (this._node?.View != null && this._node.View.AllObjectsAttributes.Count > 0)
          variables.AddRange((IEnumerable<Intermech.Expressions.Variable>) this._node.View.AllObjectsAttributes);
        else
          variables.AddRange((IEnumerable<Intermech.Expressions.Variable>) MiscFunx.GetAllAttributesVariables());
      }
      LinkKind linkKind2 = CaseLinkForm.QueryLinkKind(ref this._selectedExpressionCondition, variables, this._activityExpressionAttributes, this._activityAllAttributeValues.ToArray());
      if (linkKind2 == LinkKind.Backward)
        return;
      if (linkKind2 != linkKind1)
      {
        if (this.CaseLinksWithModifiedLinkType.ContainsKey(this._selectedExpressionCondition.LinkID))
          this.CaseLinksWithModifiedLinkType[this._selectedExpressionCondition.LinkID] = linkKind2;
        else
          this.CaseLinksWithModifiedLinkType.Add(this._selectedExpressionCondition.LinkID, linkKind2);
      }
      this._expressionConditionsModified = true;
      this.RefreshCaseConditions();
    }
    else
    {
      LinkKind linkKind3 = this._selectedExpertCondition.ExpertFormula != null ? LinkKind.True : LinkKind.False;
      LinkKind linkKind4 = CaseLinkForm.QueryLinkKind(ref this._selectedExpertCondition.ExpertFormula, this._processID, this.CaseFilterCheckBox.Checked);
      if (linkKind4 == LinkKind.Backward)
        return;
      if (linkKind4 != linkKind3)
      {
        if (this.CaseLinksWithModifiedLinkType.ContainsKey(this._selectedExpertCondition.LinkID))
          this.CaseLinksWithModifiedLinkType[this._selectedExpertCondition.LinkID] = linkKind4;
        else
          this.CaseLinksWithModifiedLinkType.Add(this._selectedExpertCondition.LinkID, linkKind4);
      }
      this._expertConditions.Modified = true;
      this.RefreshCaseConditions();
    }
  }

  private void CondsView_DoubleClick(object sender, EventArgs e)
  {
    if (this.CondsView.SelectedItems.Count <= 0 || !this.CondsView.LabelEdit)
      return;
    this.CondsView_BeforeLabelEdit((object) null, new LabelEditEventArgs(this.CondsView.SelectedIndices[0], ""));
  }

  private void ValidateButton_Click(object sender, EventArgs e)
  {
    if (this._oldExpertCondition == null)
    {
      int num = (int) MessageBox.Show(MiscFunx.VerifyExpressionFormula(this._expressionCondition.ToString(), this._activityAllAttributeValues.ToArray()), LocalizationHolder.rm.GetString("Workflow.Design_119"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
      wfFunx.ValidateFormulaDialog(this._objectIDwithVars, this._oldExpertCondition);
  }

  private void ValidateCaseButton_Click(object sender, EventArgs e)
  {
    if (this.CondsView.SelectedItems.Count <= 0)
      return;
    if (this.CondsView.SelectedItems[0].Tag is ConditionInfo tag1)
    {
      wfFunx.ValidateFormulaDialog(this._objectIDwithVars, tag1.ExpertFormula, this.CaseFilterCheckBox.Checked);
    }
    else
    {
      if (!(this.CondsView.SelectedItems[0].Tag is ExpressionInfo tag))
        return;
      int num = (int) MessageBox.Show(MiscFunx.VerifyExpressionFormula(tag.ToString(), this._activityAllAttributeValues.ToArray()), LocalizationHolder.rm.GetString("Workflow.Design_119"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  private void CaseFilterCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.RefreshCaseConditions();
  }

  private void ChangeExpressionType_Click(object sender, EventArgs e)
  {
    if (this.CondsView.SelectedItems.Count <= 0 || !(this.CondsView.SelectedItems[0].Tag is ExpressionInfo tag))
      return;
    int num = -1;
    List<int> applicableAttachmentTypes = wfFunx.GetApplicableAttachmentTypes(wfConsts.ActivitiesTypeID, wfConsts.AttachmentRelationTypeID);
    new AllowedTypes(this._processID).Filter(applicableAttachmentTypes);
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Любой тип объекта", typeof (ObjectTypeFolder), false)
    {
      SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(applicableAttachmentTypes.ToArray(), true, true)
    };
    if (selectorForm.ShowDialog() != DialogResult.OK)
      return;
    if (selectorForm.IDList.Count > 0)
      num = Convert.ToInt32(selectorForm.IDList[0]);
    int index = this._expressionConditions.IndexOf(tag);
    if (index != -1)
    {
      this._expressionConditions[index].ObjectTypeForLink = num;
      this._expressionConditionsModified = true;
    }
    this._modified = true;
    this.RefreshCaseConditions();
  }

  private void RefreshCaseConditions()
  {
    bool flag = this.CaseFilterCheckBox.Checked;
    if (this._expertConditions == null || this._expertConditions.IsEmpty)
    {
      this.changeObjectTypeInExpression.Visible = flag;
      this.CondsView.Items.Clear();
      if (flag)
      {
        if (!this.CondsView.Columns.Contains(this._objectTypeColumn))
          this.CondsView.Columns.Insert(0, this._objectTypeColumn);
      }
      else if (this.CondsView.Columns.Contains(this._objectTypeColumn))
        this.CondsView.Columns.Remove(this._objectTypeColumn);
      for (int index = 0; index < this._expressionConditions.Count; ++index)
      {
        ExpressionInfo expressionCondition = this._expressionConditions[index];
        ListViewItem listViewItem = flag ? this.CondsView.Items.Add(expressionCondition.ObjectTypeName) : this.CondsView.Items.Add(expressionCondition.ToString());
        listViewItem.Tag = (object) expressionCondition;
        if (this._caseEdit != null && this._caseEdit.Visible && this._selectedExpressionCondition == expressionCondition)
          this._caseEdit.Text = expressionCondition.ToString();
        listViewItem.ImageIndex = MiscFunx.VerifyExpression(expressionCondition.FormulaForLink, this._activityAllAttributeValues.ToArray(), flag) is bool ? 1 : 0;
        string text = "?";
        int num = 1;
        if (this._objectID < 0L)
          num = -1;
        if (flag)
          listViewItem.SubItems.Add(expressionCondition.ToString());
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute((long) num * expressionCondition.LinkID, (object) wfConsts.AttrToActivityID, false, false);
          if (objectAttribute != null)
          {
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo((long) num * objectAttribute.AsInteger);
            if (!objectInfo.Empty)
              text = objectInfo.Caption;
          }
        }
        if (text == "?")
        {
          this.CondsView.Items.Remove(listViewItem);
          this._expressionConditions.RemoveAt(index);
          --index;
        }
        else
          listViewItem.SubItems.Add(text);
      }
    }
    else
    {
      this.changeObjectTypeInExpression.Visible = false;
      this.CondsView.Items.Clear();
      if (this.CondsView.Columns.Contains(this._objectTypeColumn))
        this.CondsView.Columns.Remove(this._objectTypeColumn);
      Guid empty = Guid.Empty;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IExpertServer customService = sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer;
        Guid sessionGuid = sessionKeeper.Session.SessionGUID;
        int num1 = customService.StartTask(sessionGuid);
        try
        {
          for (int index = 0; index < this._expertConditions.Count; ++index)
          {
            ListViewItem listViewItem = this.CondsView.Items.Add(this._expertConditions[index].ToString());
            listViewItem.Tag = (object) this._expertConditions[index];
            if (this._caseEdit != null && this._caseEdit.Visible && this._selectedExpertCondition == this._expertConditions[index])
              this._caseEdit.Text = this._expertConditions[index].ToString();
            listViewItem.ImageIndex = !MiscFunx.VerifyFormula(customService, num1, this._objectIDwithVars, this._expertConditions[index].ExpertFormula, flag) ? 0 : 1;
            string text = "?";
            int num2 = 1;
            if (this._objectID < 0L)
              num2 = -1;
            IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID((long) num2 * this._expertConditions[index].LinkID, wfConsts.AttrToActivityID);
            if (objectAttributeById != null)
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo((long) num2 * objectAttributeById.AsInteger);
              if (!objectInfo.Empty)
                text = objectInfo.Caption;
            }
            if (text == "?")
            {
              this.CondsView.Items.Remove(listViewItem);
              this._expertConditions.RemoveAt(index);
              --index;
            }
            else
              listViewItem.SubItems.Add(text);
          }
        }
        finally
        {
          customService.EndTask(num1);
        }
      }
    }
  }

  private long SubprocessID
  {
    get => Convert.ToInt64(this.SchemeEdit.Tag);
    set
    {
      this.SchemeEdit.Tag = (object) value;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ObjectSystemPropertiesEx systemPropertiesEx = sessionKeeper.Session.GetObjectSystemPropertiesEx(value, false);
        if (systemPropertiesEx == null)
          return;
        this.SchemeEdit.Text = CaptionTransform.GetCaption(systemPropertiesEx.Caption, (long) systemPropertiesEx.VersionID);
      }
    }
  }

  private void SchemeEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long num = wfFunx.BrowseForScheme();
    if (num == -1L)
      return;
    this.SubprocessID = num;
  }

  private void SubNameEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Workflow.Design_11") + LocalizationHolder.rm.GetString("Workflow.Design_12") + LocalizationHolder.rm.GetString("Workflow.Design_13") + LocalizationHolder.rm.GetString("Workflow.Design_14") + LocalizationHolder.rm.GetString("Workflow.Design_15"), LocalizationHolder.rm.GetString("Workflow.Design_16"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void PageControl_SelectedIndexChanged(object sender, EventArgs e)
  {
    TabPage tabPage = this.PageControl.TabPages[this.PageControl.SelectedIndex];
    if (tabPage == this.ContentTabSheet)
      this.InitAttachments();
    else if (tabPage == this.LCTabSheet)
    {
      if (this.atype == wfConsts.LifeCycleTypeID)
      {
        this.LCLabel1.Visible = false;
        this.LCLabel2.Visible = false;
        this.LCPanel2.Visible = false;
        this.LCPanel1.Dock = DockStyle.Fill;
      }
      else
        this.LCGroupBox_Resize((object) null, (EventArgs) null);
      this.FillLCSteps();
    }
    else if (tabPage == this.SchemeAttachsTabSheet)
    {
      this.InitSchemeAttachTypes();
    }
    else
    {
      if (tabPage != this.TermsTabSheet)
        return;
      this.TermOptsGroupBox.Visible = this.AnyPartButton.Checked;
    }
  }

  private void InitAttachments()
  {
    if (this._attView != null)
      return;
    this._attView = new AttachmentsView();
    this._attView.BackColor = SystemColors.Window;
    this._attView.Dock = DockStyle.Fill;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectID);
      if (dbObject != null)
        this._attView.Load(dbObject);
    }
    this._attView.ReadOnly = this.ReadOnly;
    this.AttachsPanel.Controls.Add((Control) this._attView);
  }

  public int SignsGroupID
  {
    get => this._signsGroupID;
    set => this._signsGroupID = value;
  }

  public void FillSignGraphs()
  {
    this.SignGraphView.BeginUpdate();
    try
    {
      this.SignGraphView.Items.Clear();
      if (this.requiredSigns != null)
      {
        bool flag = false;
        int num = 0;
        foreach (string graphs1 in this.requiredSigns.GraphsSet)
        {
          GraphsCollection graphs2 = this.requiredSigns.GraphsSet[graphs1];
          if (graphs2 != null)
          {
            int int32 = Convert.ToInt32(graphs1);
            if (int32 > num)
              num = int32;
            if (flag)
              this.SignGraphView.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_17")).Tag = (object) graphs1;
            foreach (GraphClass graphClass in graphs2)
            {
              ListViewItem listViewItem = this.SignGraphView.Items.Add("");
              listViewItem.SubItems.Add(MiscFunx.GetSignGraphCaption(graphClass.Value, ref this.graphsAttrType));
              listViewItem.Tag = (object) graphs1;
              CheckBoxListViewSubItem boxListViewSubItem = new CheckBoxListViewSubItem();
              boxListViewSubItem.Tag = (object) graphClass;
              boxListViewSubItem.Checked = graphClass.StrongCheck;
              boxListViewSubItem.OnClick += new EventHandler(this.StrongSign_CheckedChanged);
              listViewItem.SubItems.Add((ListViewItem.ListViewSubItem) boxListViewSubItem);
            }
            flag = true;
          }
        }
        this.SignsGroupID = num;
      }
    }
    finally
    {
      this.SignGraphView.EndUpdate();
    }
    if (this.SignGraphView.Items.Count > 0)
      this.SignAsGraphRadioButton.Checked = true;
    this.SignAsUserRadioButton_CheckedChanged((object) null, (EventArgs) null);
  }

  private void StrongSign_CheckedChanged(object sender, EventArgs e)
  {
    if (this.requiredSigns == null)
      return;
    this.requiredSigns.Modified = true;
  }

  private void SignGraphView_SelectedIndexChanged(object sender, EventArgs e)
  {
    ListViewItem listViewItem = (ListViewItem) null;
    if (this.SignGraphView.SelectedItems.Count > 0)
      listViewItem = this.SignGraphView.SelectedItems[0];
    this.DeleteSignsButton.Enabled = listViewItem != null && listViewItem.SubItems.Count > 1;
  }

  private bool ChooseRequiredSigns()
  {
    using (AddSignGraphsForm addSignGraphsForm = new AddSignGraphsForm())
    {
      addSignGraphsForm.NewGroupBox.Visible = this.SignGraphView.Items.Count > 0;
      if (addSignGraphsForm.ShowDialog() == DialogResult.OK)
      {
        GraphInfoList selected = addSignGraphsForm.Selected;
        if (selected.Count > sc_21746.ssp_workflow_21747(1684241483))
        {
          if (addSignGraphsForm.NewGroupBox.Checked)
            ++this.SignsGroupID;
          else if (this.SignGraphView.SelectedItems.Count > 0)
            this.SignsGroupID = Convert.ToInt32(this.SignGraphView.SelectedItems[0].Tag);
          foreach (GraphInfo graphInfo in (List<GraphInfo>) selected)
            this.requiredSigns.Add(graphInfo.GraphVal, graphInfo.StrongSign, this.SignsGroupID);
          this.FillSignGraphs();
        }
        return this.SignGraphView.Items.Count > 0;
      }
    }
    return false;
  }

  private void GraphsBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    if (this.requiredSigns == null)
      return;
    switch (Convert.ToInt32(e.Button.Tag))
    {
      case 1:
        this.ChooseRequiredSigns();
        break;
      case 2:
        if (this.SignGraphView.SelectedItems.Count <= 0)
          break;
        ListViewItem selectedItem = this.SignGraphView.SelectedItems[0];
        int num = -1;
        if (selectedItem.SubItems.Count > 1 && selectedItem.SubItems[2] is CheckBoxListViewSubItem)
          num = this.requiredSigns.Delete(selectedItem.SubItems[2].Tag as GraphClass, Convert.ToInt32(selectedItem.Tag));
        if (num == 0 && selectedItem.Index > 0)
          selectedItem.ListView.Items.RemoveAt(selectedItem.Index - 1);
        this.SignGraphView.SaveSelectedPos();
        selectedItem.Remove();
        this.SignGraphView.RestoreSelectedPos();
        break;
    }
  }

  private void SignAsUserRadioButton_CheckedChanged(object sender, EventArgs e)
  {
    bool flag = this.SignAsGraphRadioButton.Checked && !this.ReadOnly;
    this.SignGraphView.Enabled = flag;
    this.GraphsBar.Enabled = flag;
    if (!flag)
      this.PersonalSignsCheckBox.Checked = false;
    this.PersonalSignsCheckBox.Enabled = flag;
    if (sender == null)
      return;
    this.requiredSigns.Modified = true;
  }

  private void FillSignDocTypes()
  {
    this.SignTypesView.BeginUpdate();
    try
    {
      this.SignTypesView.SaveSelectedPos();
      this.SignTypesView.SmallImageList = BaseHolder.IconService.ImageList;
      this.SignTypesView.Items.Clear();
      for (int index = 0; index < this._signDTNames.Count; ++index)
      {
        ListViewItem listViewItem = this.SignTypesView.Items.Add(this._signDTNames[index]);
        listViewItem.Tag = (object) index;
        listViewItem.ImageIndex = BaseHolder.IconService.IndexOf(4, Convert.ToInt32(this._signDTIDs[index]));
      }
    }
    finally
    {
      this.SignTypesView.RestoreSelectedPos();
      this.SignTypesView.EndUpdate();
    }
  }

  private bool ChooseSignDocTypes()
  {
    using (SelectorForm selectorForm = new SelectorForm(LocalizationHolder.rm.GetString("ObjectTypes"), 4, false))
    {
      if (selectorForm.ShowDialog() == DialogResult.OK)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (object id in selectorForm.IDList)
          {
            int int32 = Convert.ToInt32(id);
            if (!this._signDTIDs.Contains((object) int32))
            {
              IDBObjectType objectType = sessionKeeper.Session.GetObjectType(int32);
              if (objectType != null)
              {
                this._signDTIDs.Add((object) int32);
                this._signDTGuids.Add(objectType.PropertiesStructure.ObjectTypeGuid);
                this._signDTNames.Add(objectType.ObjectTypeName);
              }
              this._signDTModified = true;
            }
          }
        }
        this.FillSignDocTypes();
        return true;
      }
    }
    return false;
  }

  private void DTypesRB_CheckedChanged(object sender, EventArgs e)
  {
    bool flag = this.DTypesRB2.Checked;
    this.SignTypesView.Enabled = flag;
    this.SignObjectTypesBar.Enabled = flag;
    if (sender == null)
      return;
    this._signDTModified = true;
  }

  private void SignObjectTypesBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    if (e.Button == this.AddObjTypesButton)
    {
      this.ChooseSignDocTypes();
    }
    else
    {
      ListViewItem listViewItem = (ListViewItem) null;
      if (this.SignTypesView.SelectedItems.Count > 0)
        listViewItem = this.SignTypesView.SelectedItems[0];
      if (listViewItem == null)
        return;
      int int32 = Convert.ToInt32(listViewItem.Tag);
      this._signDTIDs.RemoveAt(int32);
      this._signDTGuids.RemoveAt(int32);
      this._signDTNames.RemoveAt(int32);
      this._signDTModified = true;
      this.FillSignDocTypes();
    }
  }

  private void SignTypesView_SelectedIndexChanged(object sender, EventArgs e)
  {
    ListViewItem listViewItem = (ListViewItem) null;
    if (this.SignTypesView.SelectedItems.Count > 0)
      listViewItem = this.SignTypesView.SelectedItems[0];
    this.DeleteObjTypesButton.Enabled = listViewItem != null;
  }

  private void LCListNeeded()
  {
    if (this.LCList != null)
      return;
    this.LCList = new LCInfoList();
  }

  private void LCStepToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    if (sender == null)
      return;
    EnhListView enhListView = this.LCView;
    LCExec exec = LCExec.Before;
    if (sender == this.LCStepToolBar2)
    {
      enhListView = this.LCView2;
      exec = LCExec.After;
    }
    if (e != null && Convert.ToInt32(e.Button.Tag) == 2)
    {
      if (enhListView.SelectedItems.Count > 0)
      {
        this.LCListNeeded();
        this.LCList.Remove(enhListView.SelectedItems[0].Tag as LCInfo);
        this.LCList.Modified = true;
      }
      this.FillLCSteps();
    }
    else
    {
      if (e != null && Convert.ToInt32(e.Button.Tag) == sc_21746.ssp_workflow_21748(112664016))
      {
        if (enhListView.SelectedItems.Count <= 0)
          return;
        sender = (object) (enhListView.SelectedItems[0].Tag as LCInfo);
      }
      bool flag = !(sender is LCInfo);
      using (LCStepSetupForm f = new LCStepSetupForm())
      {
        LCInfo lcInfo = (LCInfo) null;
        if (!flag)
        {
          lcInfo = (LCInfo) sender;
          f.LCInfo = lcInfo;
        }
        if (f.ShowDialog() != DialogResult.OK)
          return;
        this.LCListNeeded();
        if (flag && this.LCList.Count<LCInfo>((System.Func<LCInfo, bool>) (x => x.ObjectType == f.LCInfo.ObjectType && x.ExecTime == exec)) == 0)
        {
          f.LCInfo.ExecTime = exec;
          this.LCList.Add(f.LCInfo);
          this.LCList.Modified = true;
        }
        else if (lcInfo != null && !f.LCInfo.Equals((object) lcInfo))
        {
          lcInfo.Assign(f.LCInfo);
          this.LCList.Modified = true;
        }
        this.FillLCSteps();
      }
    }
  }

  private void FillLCSteps()
  {
    this.FillLCSteps(this.LCView, LCExec.Before);
    this.FillLCSteps(this.LCView2, LCExec.After);
  }

  private void FillLCSteps(EnhListView view, LCExec exec)
  {
    if (this.LCList == null)
      return;
    view.BeginUpdate();
    try
    {
      view.Items.Clear();
      view.SmallImageList = BaseHolder.IconService.ImageList;
      view.SubitemImages = BaseHolder.IconService.ImageList;
      foreach (LCInfo lc in (List<LCInfo>) this.LCList)
      {
        if (lc.ExecTime == exec)
        {
          ListViewItem listViewItem = view.Items.Add(lc.TypeName);
          listViewItem.ImageIndex = BaseHolder.IconService.IndexOf(4, lc.ObjectType);
          int imageIndex = -1;
          try
          {
            imageIndex = BaseHolder.IconService.IndexOf(8, lc.LevelID);
          }
          catch
          {
          }
          string str = $" ({LocalizationHolder.rm.GetString("LC" + lc.Kind.ToString())})";
          listViewItem.SubItems.Add((ListViewItem.ListViewSubItem) new ImageListViewSubItem(lc.StepName + str, imageIndex));
          listViewItem.Tag = (object) lc;
        }
      }
      if (view.Items.Count > 0)
        view.Items[0].Selected = true;
      view.HighlightInvalidItems();
    }
    finally
    {
      view.EndUpdate();
    }
  }

  private void LCView_DoubleClick(object sender, EventArgs e)
  {
    EnhListView enhListView = sender as EnhListView;
    if (this.ReadOnly || enhListView == null || enhListView.SelectedItems.Count <= 0)
      return;
    this.LCStepToolBar_ButtonClick((object) (enhListView.SelectedItems[0].Tag as LCInfo), (ToolBarButtonClickEventArgs) null);
  }

  private void EditScriptButton_Click(object sender, ButtonPressedEventArgs e)
  {
    int int32 = Convert.ToInt32((sender as Control).Tag);
    ScriptInfo script = this._scripts[int32];
    string workflowLocalName = string.Empty;
    string str1 = string.Empty;
    if (this._processID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ObjectSystemPropertiesEx systemPropertiesEx = sessionKeeper.Session.GetObjectSystemPropertiesEx(this._processID, false);
        if (systemPropertiesEx != null)
          str1 = CaptionTransform.GetCaption(systemPropertiesEx.Caption, (long) systemPropertiesEx.VersionID) + ".";
      }
    }
    string str2 = string.Empty;
    WorkflowScriptType workflowScriptType;
    ScriptExecSide workflowExecSide;
    if (this.atype == wfConsts.ScriptTypeID)
    {
      workflowScriptType = (WorkflowScriptType) this.scriptType.SelectedIndex;
      workflowExecSide = (ScriptExecSide) this.ScriptExecCombo.SelectedIndex;
    }
    else
    {
      workflowScriptType = sender == this.beforeScriptEdit ? (WorkflowScriptType) this.beforeScriptType.SelectedIndex : (WorkflowScriptType) this.afterScriptType.SelectedIndex;
      workflowExecSide = sender == this.beforeScriptEdit ? (ScriptExecSide) this.BeforeScriptExecCombo.SelectedIndex : (ScriptExecSide) this.AfterScriptExecCombo.SelectedIndex;
      str2 = int32 == 0 ? "[Перед] " : "[После] ";
    }
    ScriptTypes scriptType = workflowScriptType == WorkflowScriptType.Common ? ScriptTypes.WorkflowCommon : ScriptTypes.WorkflowLocal;
    if (scriptType == ScriptTypes.WorkflowLocal)
      workflowLocalName = string.Format("{2}{0} {1}", (object) str1, (object) this.NameEdit.Text, (object) str2);
    long objectID = Math.Abs(new WorkflowScriptPadHelper(scriptType, (Form) this).EditScript(script.ScriptID, workflowLocalName, workflowExecSide, !Holder.IsAdmin && this.ReadOnly, this._objectID));
    if (objectID != 0L && objectID != script.ScriptID)
    {
      script.ScriptID = objectID;
      script.ExecSide = workflowExecSide;
      script.ScriptType = workflowScriptType;
      ButtonEdit buttonEdit = sender as ButtonEdit;
      if (scriptType == ScriptTypes.WorkflowLocal)
      {
        buttonEdit.Text = workflowLocalName;
        script.ScriptCaption = workflowLocalName;
        if (this.NewScripts.ContainsKey(int32))
          this.NewScripts[int32] = objectID;
        else
          this.NewScripts.Add(int32, objectID);
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
          if (!objectInfo.Empty)
          {
            buttonEdit.Text = objectInfo.Caption;
            script.ScriptCaption = objectInfo.Caption;
          }
        }
      }
    }
    if (sender == this.scriptEdit)
      this.scriptType.Enabled = script.ScriptID == 0L;
    else if (sender == this.beforeScriptEdit)
    {
      this.beforeScriptType.Enabled = script.ScriptID == 0L;
    }
    else
    {
      if (sender != this.afterScriptEdit)
        return;
      this.afterScriptType.Enabled = script.ScriptID == 0L;
    }
  }

  private void SaveScriptInfo(
    IDBObject Activity,
    ScriptInfo si,
    ScriptKind sk,
    CheckBox checkBox,
    System.Windows.Forms.ComboBox execCombo)
  {
    if (si.OldScriptID == si.ScriptID && si.ExecSide == (ScriptExecSide) execCombo.SelectedIndex)
      return;
    if (si.ScriptLinkID != 0L)
    {
      IDBRelation relation = Activity.Session.GetRelation(si.ScriptLinkID, false);
      if (relation != null)
      {
        relation.Delete(0L);
        si.ScriptLinkID = 0L;
        this._modified = true;
      }
    }
    if (checkBox != null && !checkBox.Checked || si.ScriptID == 0L)
      return;
    si.OldScriptID = si.ScriptID;
    IDBRelation relation1;
    if (si.ScriptLinkID == 0L)
    {
      relation1 = Activity.Session.GetRelationCollection(wfConsts.ScriptRelationTypeID).Create(Activity.ObjectID, si.ScriptID);
      si.ScriptLinkID = relation1.RelationID;
      IDBAttribute attributeById = relation1.GetAttributeByID(wfConsts.AttrScriptKindID);
      if (attributeById != null)
        attributeById.AsInteger = (long) sk;
      this._modified = true;
    }
    else
      relation1 = Activity.Session.GetRelation(si.ScriptLinkID, false);
    if (relation1 == null)
      return;
    IDBAttribute attributeById1 = relation1.GetAttributeByID(wfConsts.AttrScriptExecSideID);
    if (attributeById1 == null)
      return;
    attributeById1.AsInteger = (long) execCombo.SelectedIndex;
    this._modified = true;
  }

  private void BeforeScriptCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.beforeScriptEdit.Enabled = this.BeforeScriptCheckBox.Checked;
    this.beforeScriptType.Enabled = !this.BeforeScriptCheckBox.Checked;
    this.BeforeScriptExecCombo.Enabled = !1.Equals(this.BeforeScriptExecCombo.Tag);
    this.afterScriptEdit.Enabled = this.AfterScriptCheckBox.Checked;
    this.afterScriptType.Enabled = !this.AfterScriptCheckBox.Checked;
    this.AfterScriptExecCombo.Enabled = !1.Equals(this.AfterScriptExecCombo.Tag);
    if (this._loading || !(sender is CheckBox) || !this._checkLocalScriptNotDelete)
      return;
    ButtonEdit sender1 = this.afterScriptEdit;
    if (sender == this.BeforeScriptCheckBox)
      sender1 = this.beforeScriptEdit;
    if (((CheckBox) sender).Checked)
    {
      this.EditScriptButton_Click((object) sender1, (ButtonPressedEventArgs) null);
      if (this._scripts[Convert.ToInt32(sender1.Tag)].ScriptID != 0L)
        return;
      this._checkLocalScriptNotCreate = true;
      (sender as CheckBox).Checked = false;
      this._checkLocalScriptNotCreate = false;
    }
    else
    {
      ScriptInfo si = this._scripts[Convert.ToInt32(sender1.Tag)];
      DialogResult dialogResult = DialogResult.Yes;
      if (si.ScriptType == WorkflowScriptType.Local && !this._checkLocalScriptNotCreate)
        dialogResult = MessageBox.Show("Данное действие приведёт к удалению локального сценария. Хотите продолжить?", "Внимание", MessageBoxButtons.YesNo);
      switch (dialogResult)
      {
        case DialogResult.Yes:
          if (si.ScriptID > 0L)
          {
            if (si.ScriptType == WorkflowScriptType.Local)
            {
              if (this.NewScripts.ContainsValue(si.ScriptID))
                this.NewScripts.Remove(this.NewScripts.FirstOrDefault<KeyValuePair<int, long>>((System.Func<KeyValuePair<int, long>, bool>) (x => x.Value == si.ScriptID)).Key);
              if (!this.LocalScriptsToDeleted.Contains(si.ScriptID))
              {
                this.LocalScriptsToDeleted.Add(si.ScriptID);
                this.AddedNewScriptToDelete = true;
              }
            }
            si.OldScriptID = si.ScriptID;
            si.ScriptID = 0L;
          }
          sender1.Text = string.Empty;
          break;
        case DialogResult.No:
          this._checkLocalScriptNotDelete = false;
          ((CheckBox) sender).Checked = true;
          this._checkLocalScriptNotDelete = true;
          break;
      }
    }
  }

  private void ScriptExecCombo_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.ScriptUserGroupBox.Enabled = this.ScriptExecCombo.SelectedIndex == sc_21746.ssp_workflow_21749(1727837357);
  }

  private void ScriptUserEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (!wfFunx.BrowseForUsers(this.Participants, this._processID))
      return;
    this.ScriptUserEdit.Text = this.Participants.ToUserString();
    this._additionalParticipantsModified = true;
  }

  private void DocArcRadioChanged(object sender, EventArgs e)
  {
    int int32 = Convert.ToInt32(((Control) sender).Tag);
    this.ArchiveTabSheet.SuspendLayout();
    try
    {
      this.DocArcIDEdit.Visible = int32 == 1;
      this.DocArcVarCombo.Visible = int32 == 2;
      this.DocSpacerPanel.Visible = int32 > 0;
    }
    finally
    {
      this.ArchiveTabSheet.ResumeLayout();
    }
  }

  private void RevArcRadioChanged(object sender, EventArgs e)
  {
    int int32 = Convert.ToInt32(((Control) sender).Tag);
    this.ArchiveTabSheet.SuspendLayout();
    try
    {
      this.RevArcIDEdit.Visible = int32 == 1;
      this.RevArcVarCombo.Visible = int32 == 2;
      this.RevSpacerPanel.Visible = int32 > 0;
    }
    finally
    {
      this.ArchiveTabSheet.ResumeLayout();
    }
  }

  private void DocArcIDEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
  {
    if (!(sender is ButtonEdit))
      return;
    ButtonEdit buttonEdit = sender as ButtonEdit;
    IDescriptor rootDescriptor = (IDescriptor) null;
    ServiceContainer nodesContext = new ServiceContainer();
    if (ApplicationServices.Container.GetService(typeof (IArchivesDescriptorService)) is IArchivesDescriptorService service)
    {
      rootDescriptor = service.GetDescriptor();
      object viewArchives = service.ViewArchives;
      nodesContext.AddService(viewArchives.GetType(), viewArchives);
    }
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Workflow.Design_18"), "", rootDescriptor, (System.IServiceProvider) nodesContext, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    buttonEdit.Tag = (object) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ObjectSystemPropertiesEx systemPropertiesEx = sessionKeeper.Session.GetObjectSystemPropertiesEx(numArray[0], false);
      if (systemPropertiesEx != null)
      {
        buttonEdit.Text = systemPropertiesEx.Caption;
        buttonEdit.Tag = (object) systemPropertiesEx.VersionGuid;
      }
      else
        buttonEdit.Text = "???";
    }
  }

  private string FillArcGroupBox(
    IUserSession session,
    IDBObject activity,
    int attrID,
    VarList vars,
    GroupBox gb)
  {
    string g = "";
    System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox) null;
    ButtonEdit buttonEdit = (ButtonEdit) null;
    RadioButton radioButton1 = (RadioButton) null;
    RadioButton radioButton2 = (RadioButton) null;
    RadioButton radioButton3 = (RadioButton) null;
    foreach (Control control in (ArrangedElementCollection) gb.Controls)
    {
      switch (control)
      {
        case RadioButton _:
          switch (Convert.ToInt32(control.Tag))
          {
            case 1:
              radioButton2 = (RadioButton) control;
              continue;
            case 2:
              radioButton3 = (RadioButton) control;
              continue;
            default:
              radioButton1 = (RadioButton) control;
              continue;
          }
        case System.Windows.Forms.ComboBox _:
          comboBox = (System.Windows.Forms.ComboBox) control;
          continue;
        case ButtonEdit _:
          buttonEdit = (ButtonEdit) control;
          continue;
        default:
          continue;
      }
    }
    if (comboBox != null && buttonEdit != null && radioButton3 != null)
    {
      foreach (Intermech.Workflow.Variable var in vars)
      {
        if (var.VarType == VarType.Archive)
          comboBox.Items.Add((object) var);
      }
      comboBox.SelectedIndex = 0;
      IDBAttribute attributeById = activity.GetAttributeByID(attrID);
      if (attributeById != null)
      {
        g = attributeById.AsString;
        if (g != "")
        {
          try
          {
            Guid guid = new Guid(g);
            QuickObjectInfo objectInfo = session.GetObjectInfo(guid);
            if (objectInfo.Empty)
            {
              IDBAttributeTypeInfo attributeType = ApplicationServices.Container.GetService(typeof (IClientMetadataCache)) is IClientMetadataCache service ? service.GetAttributeType(guid, false) : (IDBAttributeTypeInfo) null;
              if (attributeType != null)
              {
                int attributeId = attributeType.AttributeID;
                foreach (Intermech.Workflow.Variable var in vars)
                {
                  if (var.AttrTypeID == attributeId)
                  {
                    radioButton3.Checked = true;
                    comboBox.SelectedItem = (object) var;
                    break;
                  }
                }
              }
            }
            else
            {
              if (radioButton2 != null)
                radioButton2.Checked = true;
              buttonEdit.Text = objectInfo.Caption;
              buttonEdit.Tag = (object) objectInfo.VersionGuid;
            }
          }
          catch (Exception ex)
          {
            if (ApplicationServices.Container.GetService(typeof (IOutputView)) is IOutputView service)
              service.WriteString("Ошибки", "В процессе загрузки данных по архивам произошла ошибка: " + ex.Message);
          }
          return g;
        }
      }
    }
    if (radioButton2 != null && radioButton3 != null && radioButton1 != null && !radioButton2.Checked && !radioButton3.Checked)
      radioButton1.Checked = true;
    return g;
  }

  private bool SaveArcGroupBox(IDBObject activity, int attrID, GroupBox gb)
  {
    string str = string.Empty;
    IDBAttribute attributeById = activity.GetAttributeByID(attrID);
    if (attributeById != null)
      str = attributeById.AsString;
    if (string.IsNullOrEmpty(str))
      str = Guid.Empty.ToString();
    System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox) null;
    ButtonEdit buttonEdit = (ButtonEdit) null;
    RadioButton radioButton1 = (RadioButton) null;
    RadioButton radioButton2 = (RadioButton) null;
    foreach (Control control in (ArrangedElementCollection) gb.Controls)
    {
      switch (control)
      {
        case RadioButton _:
          switch (Convert.ToInt32(control.Tag))
          {
            case 1:
              radioButton2 = (RadioButton) control;
              continue;
            case 2:
              continue;
            default:
              radioButton1 = (RadioButton) control;
              continue;
          }
        case System.Windows.Forms.ComboBox _:
          comboBox = (System.Windows.Forms.ComboBox) control;
          continue;
        case ButtonEdit _:
          buttonEdit = (ButtonEdit) control;
          continue;
        default:
          continue;
      }
    }
    if (radioButton1.Checked)
    {
      if (!string.IsNullOrEmpty(str))
      {
        if (attributeById != null)
          attributeById.AsString = string.Empty;
        return true;
      }
    }
    else if (radioButton2.Checked)
    {
      Guid guid = Guid.Empty;
      if (buttonEdit.Tag != null)
        guid = (Guid) buttonEdit.Tag;
      if (!str.Equals(guid.ToString()) && attributeById != null)
      {
        attributeById.AsString = guid.Equals(Guid.Empty) ? string.Empty : guid.ToString();
        return true;
      }
    }
    else if (comboBox.SelectedItem is Intermech.Workflow.Variable selectedItem)
    {
      if (!str.Equals(selectedItem.AttrTypeID.ToString()) && attributeById != null)
      {
        attributeById.AsString = selectedItem.AttrTypeGuid.ToString();
        return true;
      }
    }
    else if (!string.IsNullOrEmpty(str) && attributeById != null)
    {
      attributeById.AsString = string.Empty;
      return true;
    }
    return false;
  }

  private void UpdateTerm(int index)
  {
    this._termEdits[index].Text = this._terms.AsList[index].Period != null ? this._terms.AsList[index].Period.PeriodText : LocalizationHolder.rm.GetString("Workflow.Design_19");
  }

  private void UpdateTerms()
  {
    for (int index = 0; index < this._terms.AsList.Count; ++index)
    {
      this.UpdateTerm(index);
      this._termCheckboxes[index].Checked = this._terms.AsList[index].Enabled;
    }
  }

  private bool EditTerm(int index)
  {
    if (!TimePeriodForm.Edit(this._objectIDwithVars, this._terms.AsList[index]))
      return false;
    this.UpdateTerm(index);
    return true;
  }

  private void termEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (!(sender is ButtonEdit) || ((Control) sender).Tag == null)
      return;
    this.EditTerm(Convert.ToInt32(((Control) sender).Tag));
  }

  private void UnreadRollbackCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.unreadTermEdit.Enabled = (sender as CheckBox).Checked && !this.ReadOnly;
    this._terms.AsList[1].Enabled = (sender as CheckBox).Checked;
  }

  private void UncompleteRollbackCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._terms.AsList[0].Enabled = (sender as CheckBox).Checked;
  }

  private void ArcModeRadioCheckedChanged(object sender, EventArgs e)
  {
    this.RegRevGroupBox.Visible = this.ArcModeRadio2.Checked;
    this.RegRevSpacer.Visible = this.RegRevGroupBox.Visible;
    if (!this.RegRevGroupBox.Visible)
      return;
    this.RegRevGroupBox.PerformLayout();
  }

  private void ScriptUserEdit_EditValueChanged(object sender, EventArgs e)
  {
  }

  private void CaseFilterLabel_Click(object sender, EventArgs e)
  {
    this.CaseFilterCheckBox.Checked = !this.CaseFilterCheckBox.Checked;
  }

  private void DTypesRB2_Click(object sender, EventArgs e)
  {
    if (this.ChooseSignDocTypes())
      return;
    this.DTypesRB1.Checked = true;
  }

  private void UnreadRollbackCheckBox_Click(object sender, EventArgs e)
  {
    if (!this.UnreadRollbackCheckBox.Checked || this.EditTerm(1))
      return;
    this.UnreadRollbackCheckBox.Checked = false;
  }

  private void SignAsGraphRadioButton_Click(object sender, EventArgs e)
  {
    if (this.SignGraphView.Items.Count != 0 || this.ChooseRequiredSigns())
      return;
    this.SignAsUserRadioButton.Checked = true;
  }

  /// <summary>нажата f1  - показать справку</summary>
  /// <param name="sender"></param>
  /// <param name="hlpevent"></param>
  private void ActivPropForm_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    this.ShowHelpTopic();
  }

  /// <summary>нажата кнопка помощи - показать справку</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ActivPropForm_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    this.ShowHelpTopic();
  }

  /// <summary>выбор раздела для активной заклдадки</summary>
  private void ShowHelpTopic()
  {
    int topicID = 1291;
    if (this.PageControl.SelectedTab == this.BaseTabSheet)
      topicID = 1292;
    else if (this.PageControl.SelectedTab == this.ContentTabSheet)
      topicID = 1293;
    else if (this.PageControl.SelectedTab == this.CaseTabSheet)
      topicID = 1297;
    else if (this.PageControl.SelectedTab == this.CondTabSheet)
      topicID = 1296;
    else if (this.PageControl.SelectedTab == this.ParticipantsTabSheet)
      topicID = 1294;
    else if (this.PageControl.SelectedTab == this.ApproveTabSheet)
      topicID = 1295;
    else if (this.PageControl.SelectedTab == this.ArchiveTabSheet)
      topicID = 1302;
    else if (this.PageControl.SelectedTab == this.RollbackTabSheet)
      topicID = 1298;
    else if (this.PageControl.SelectedTab == this.LCTabSheet)
      topicID = 1304;
    else if (this.PageControl.SelectedTab == this.SubProcessTabSheet)
      topicID = 1300;
    else if (this.PageControl.SelectedTab == this.TimerTabSheet)
      topicID = 1303;
    else if (this.PageControl.SelectedTab == this.TermsTabSheet)
      topicID = 1299;
    else if (this.PageControl.SelectedTab == this.NotifTabSheet)
      topicID = 1301;
    else if (this.PageControl.SelectedTab == this.ScriptTabSheet)
      topicID = 1305;
    HelpProvidersClass.ShowHelpTopic(topicID);
  }

  private void SitesComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.RemoteSchemesComboBox.Items.Clear();
    this.RemoteSchemesComboBox.Items.Add((object) LocalizationHolder.rm.GetString("EmptyMsg"));
    this.RemoteSchemesComboBox.SelectedIndex = 0;
    SiteInfo selectedItem = this.SitesComboBox.SelectedItem as SiteInfo;
    this.CreateReceiptCheckBox.Enabled = selectedItem != null && selectedItem.SystemType == SystemTypes.IPS;
    if (this.CreateReceiptCheckBox.Enabled || !this.CreateReceiptCheckBox.Checked)
      return;
    this.CreateReceiptCheckBox.Checked = false;
  }

  private void RemoteSchemesComboBox_DropDown(object sender, EventArgs e)
  {
    Cursor.Current = Cursors.WaitCursor;
    try
    {
      SiteInfo selectedItem = this.SitesComboBox.SelectedItem as SiteInfo;
      this.RemoteSchemesComboBox.Enabled = selectedItem != null;
      if (selectedItem == null)
        return;
      ProcessTemplateInfo[] processTemplateInfoArray = (ProcessTemplateInfo[]) null;
      try
      {
        processTemplateInfoArray = this.portalSrv.GetProcessTemplates(selectedItem.GUID);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Workflow.Design_194"), (object) selectedItem.ToString()) + ex.Message);
      }
      if (processTemplateInfoArray != null)
      {
        this.RemoteSchemesComboBox.Items.Clear();
        foreach (object obj in processTemplateInfoArray)
          this.RemoteSchemesComboBox.Items.Add(obj);
      }
      if (this.RemoteSchemesComboBox.Items.Count == 0)
        this.RemoteSchemesComboBox.Items.Add((object) LocalizationHolder.rm.GetString("EmptyMsg"));
      this.RemoteSchemesComboBox.SelectedIndex = 0;
    }
    finally
    {
      Cursor.Current = Cursors.Default;
    }
  }

  private void TempRightsViewCheckBox_Click(object sender, EventArgs e)
  {
    bool flag = !this.TempRightsViewCheckBox.Checked && !this.TempRightsEditCheckBox.Checked && !this.TempRightsAdminCheckBox.Checked;
    if (sender is RadioButton)
      flag = this.NoTempRightsButton.Checked;
    else
      this.NoTempRightsButton.Checked = flag;
    if (flag)
    {
      this.TempRightsViewCheckBox.Checked = false;
      this.TempRightsEditCheckBox.Checked = false;
      this.TempRightsAdminCheckBox.Checked = false;
      this.TempRightsGroupingCheckBox.Checked = false;
    }
    else
    {
      if (this.TempRightsAdminCheckBox.Checked)
        this.TempRightsEditCheckBox.Checked = true;
      this.TempRightsEditCheckBox.Enabled = !this.TempRightsAdminCheckBox.Checked;
      if (this.TempRightsEditCheckBox.Checked)
        this.TempRightsViewCheckBox.Checked = true;
      this.TempRightsViewCheckBox.Enabled = !this.TempRightsEditCheckBox.Checked;
    }
    this.TempRightsGroupingCheckBox.Enabled = !flag;
  }

  private void ConditionBox_TextChanged(object sender, EventArgs e)
  {
    this.ValidateButton.Enabled = this.ConditionBox.Text != "";
  }

  /// <summary>
  /// Гуиды версий объектов через запятую, из которых берем настройки
  /// </summary>
  public string AlienSettingsActs
  {
    get
    {
      return this.UseAlienSettingsCheckBox.Checked ? string.Join(",", (IEnumerable<string>) this._alienSettingsActs) : "";
    }
    set
    {
      if (!this.ApproveCheckOnlyCheckBox.Checked)
        return;
      List<string> stringList;
      if (!(value != ""))
        stringList = new List<string>();
      else
        stringList = new List<string>((IEnumerable<string>) value.Split(','));
      this._alienSettingsActs = stringList;
      this.UseAlienSettingsCheckBox.Checked = this._alienSettingsActs.Count > 0;
      if (!this.UseAlienSettingsCheckBox.Checked)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        string str1 = "";
        foreach (string alienSettingsAct in this._alienSettingsActs)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(new Guid(alienSettingsAct));
          string str2 = objectInfo.Empty ? "??" : objectInfo.Caption;
          if (str1 != "")
            str1 += ", ";
          str1 += str2;
        }
        this.ApprovesBox.Text = str1;
      }
    }
  }

  private void ApproveCheckOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.UseAlienSettingsCheckBox.Enabled = !this.ReadOnly && this.ApproveCheckOnlyCheckBox.Checked;
    if (this.ApproveCheckOnlyCheckBox.Checked || !this.UseAlienSettingsCheckBox.Checked)
      return;
    this.UseAlienSettingsCheckBox.Checked = false;
  }

  private void UseAlienSettingsCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.graphForTypeCheckBox.Checked)
    {
      this.RanksPanel.Visible = !this.UseAlienSettingsCheckBox.Checked;
      this.WhatSignGB.Visible = !this.UseAlienSettingsCheckBox.Checked;
    }
    this.graphForTypeCheckBox.Visible = !this.UseAlienSettingsCheckBox.Checked;
    this.ApprovesBox.Visible = this.UseAlienSettingsCheckBox.Checked;
    this.ChooseApprovesButton.Visible = this.UseAlienSettingsCheckBox.Checked;
    this.ApproveCheckGB.Dock = this.UseAlienSettingsCheckBox.Checked ? DockStyle.Top : DockStyle.Bottom;
    if (!this.UseAlienSettingsCheckBox.Checked)
      this.signsForType.Visible = this.graphForTypeCheckBox.Checked;
    else
      this.signsForType.Visible = false;
    this.ActivPropForm_Resize((object) null, (EventArgs) null);
  }

  private void ActivPropForm_Resize(object sender, EventArgs e)
  {
    if (this.ApproveTabSheet == null)
      return;
    if (!this.UseAlienSettingsCheckBox.Checked)
    {
      this.ApproveCheckGB.Height = this.ApprovesBox.Top + 3;
      this.RanksPanel.Height = (this.ApproveTabSheet.Height - this.ApproveVSpacer.Height - this.PersonalSignsCheckBox.Height - this.ApproveCheckGB.Height) / 2;
    }
    else
      this.ApproveCheckGB.Height = this.ApproveTabSheet.Height / 2;
  }

  private void ChooseApprovesButton_Click(object sender, EventArgs e)
  {
    using (CheckListForm checkListForm = new CheckListForm())
    {
      if (this._node != null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          MapLayerCollectionObjectEnumerator enumerator = this._node.View.Doc.GetEnumerator();
          while (enumerator.MoveNext())
          {
            if (enumerator.Current is WorkflowNode current && current.ActivityKind == ActivityKind.Approve && current != this._node)
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(current.ActivityID);
              if (!objectInfo.Empty)
              {
                Guid versionGuid = objectInfo.VersionGuid;
                int index = checkListForm.ListBox.Items.Add((object) new GuidInfo(versionGuid, current.Text));
                if (this._alienSettingsActs.Contains(versionGuid.ToString()))
                  checkListForm.ListBox.SetItemChecked(index, true);
              }
            }
          }
        }
      }
      if (checkListForm.ShowDialog() != DialogResult.OK)
        return;
      this._alienSettingsActs.Clear();
      string str = "";
      foreach (GuidInfo checkedItem in checkListForm.ListBox.CheckedItems)
      {
        this._alienSettingsActs.Add(checkedItem.Guid.ToString());
        if (str != "")
          str += ", ";
        str += checkedItem.Name;
      }
      this.ApprovesBox.Text = str;
    }
  }

  private void PubObjectTypesButton_Click(object sender, EventArgs e)
  {
    using (ObjectTypesFilterForm objectTypesFilterForm = new ObjectTypesFilterForm())
    {
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IPublishTypesConfiguration)) is IPublishTypesConfiguration customService)
        objectTypesFilterForm.LoadData(customService.PublishObjectTypes, (List<int>) null, this._pubFilteredTypes);
      if (objectTypesFilterForm.ShowDialog() != DialogResult.OK)
        return;
      this._pubFilteredTypes = objectTypesFilterForm.FilteredObjectTypes;
    }
  }

  private void PubRelTypesButton_Click(object sender, EventArgs e)
  {
    using (RelationTypesFilterForm relationTypesFilterForm = new RelationTypesFilterForm())
    {
      relationTypesFilterForm.LoadData(this._pubFilteredRelTypes);
      if (relationTypesFilterForm.ShowDialog() != DialogResult.OK)
        return;
      this._pubFilteredRelTypes = relationTypesFilterForm.FilteredRelationTypes;
    }
  }

  private void LCGroupBox_Resize(object sender, EventArgs e)
  {
    if (!this.LCPanel2.Visible)
      return;
    int num1 = this.LCGroupBox.ClientSize.Height - this.LCLabel1.Height - this.LCLabel2.Height - this.LCGroupBox.Padding.Top - this.LCGroupBox.Padding.Bottom - 5;
    int num2 = (this.LCGroupBox.DisplayRectangle.Height - this.LCLabel1.Height - this.LCLabel2.Height) / 2;
    this.LCPanel1.Height = num2;
    this.LCPanel2.Height = num2;
  }

  private void InitSchemeAttachTypes()
  {
    if (this._attachTypesView != null)
      return;
    this._attachTypes = new AllowedTypes(this._objectID);
    DescriptorCollection descriptors = new DescriptorCollection();
    foreach (int allAttachType in this._attachTypes.AllAttachTypes)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(allAttachType));
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor("Все разрешенные типы вложений", descriptors);
    this._attachTypesView = new AdvNavigatorTreeView();
    this._attachTypesView.Parent = (Control) this.SchemeAttachsGB;
    this._attachTypesView.Dock = DockStyle.Fill;
    this._attachTypesView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;
    this._attachTypesView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    this._attachTypesView.BeforeSetCheckState = new BeforeSetCheckStateEventHandler(this.BeforeSetCheckState);
    this._attachTypesView.AfterExpand += new EventHandler<NodeEventArgs>(this._attachTypesView_AfterExpand);
    ServiceContainer serviceContainer = new ServiceContainer();
    serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog));
    serviceContainer.AddService(typeof (INotificationService), (object) BaseHolder.NotificationService);
    serviceContainer.AddService(typeof (IObjectTypeNodeOptionsHolder), (object) new ObjectTypeNodeOptionsHolder(ObjectTypeNodeOptions.OnlyTypesMode));
    this._attachTypesView.Services = (System.IServiceProvider) serviceContainer;
    this._attachTypesView.Build((IDescriptor) rootDescriptor);
    this._attachTypesViewLoading = true;
    try
    {
      if (this._attachTypesView.Nodes.Count <= 0)
        return;
      this._attachTypesView.Nodes[0].CheckState = CheckState.Checked;
    }
    finally
    {
      this._attachTypesViewLoading = false;
    }
  }

  private void BeforeSetCheckState(NavigatorTreeNode node, ref CheckState checkState)
  {
    int atype = !(node.NodeID is Intermech.Navigator.DBObjectTypes.Implementation.NodeID nodeId) ? 3 : nodeId.TypeID;
    if (this._attachTypesViewLoading)
      checkState = this._attachTypes.CalcCheckState(atype);
    else if (checkState == CheckState.Unchecked)
    {
      if ((node.State & NavigatorTreeNode.UpdateState.UpdatedAsChild) != NavigatorTreeNode.UpdateState.None)
        return;
      this._attachTypes.IDs.Remove(atype);
      this._attachTypes.IDs.Add(-atype);
      foreach (int typeChild in this._attachTypes.GetTypeChildren(atype, true))
      {
        this._attachTypes.IDs.Remove(typeChild);
        this._attachTypes.IDs.Remove(-typeChild);
      }
    }
    else
    {
      if ((node.State & NavigatorTreeNode.UpdateState.UpdatedAsChild) != NavigatorTreeNode.UpdateState.None)
        return;
      this._attachTypes.IDs.Remove(-atype);
      this._attachTypes.IDs.Add(atype);
      foreach (int typeChild in this._attachTypes.GetTypeChildren(atype, true))
      {
        this._attachTypes.IDs.Remove(-typeChild);
        this._attachTypes.IDs.Remove(typeChild);
      }
    }
  }

  private void _attachTypesView_AfterExpand(object sender, NodeEventArgs e)
  {
    NavigatorTreeNode node = e.Node;
    if (this._expandedNodes.Contains(node))
      return;
    CheckState checkState = node.CheckState;
    this._attachTypesViewLoading = true;
    try
    {
      foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
        child.CheckState = checkState;
    }
    finally
    {
      this._attachTypesViewLoading = false;
    }
    this._expandedNodes.Add(node);
  }

  private void graphForTypeCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.RanksPanel.Visible = !this.graphForTypeCheckBox.Checked;
    this.WhatSignGB.Visible = !this.graphForTypeCheckBox.Checked;
    this.signsForType.Visible = this.graphForTypeCheckBox.Checked;
    this.ActivPropForm_Resize((object) null, (EventArgs) null);
    Size size1 = this.Size;
    int width1 = size1.Width;
    size1 = this.Size;
    int height1 = size1.Height + 1;
    this.Size = new Size(width1, height1);
    Size size2 = this.Size;
    int width2 = size2.Width;
    size2 = this.Size;
    int height2 = size2.Height - 1;
    this.Size = new Size(width2, height2);
  }

  private void AllPartsButton_CheckedChanged(object sender, EventArgs e)
  {
    this.sendWorkOfferLastParticipantCheckBox.Enabled = false;
  }

  private void AnyPartButton_CheckedChanged(object sender, EventArgs e)
  {
    this.sendWorkOfferLastParticipantCheckBox.Enabled = true;
  }

  private void savePublishOptions_Click(object sender, EventArgs e)
  {
    if (!(ApplicationServices.Container.GetService(typeof (ISaveDiskPublishOptionsDialogService)) is ISaveDiskPublishOptionsDialogService service))
    {
      int num = (int) MessageBox.Show("Сервис работы с настройками публикации не найден.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      string empty = string.Empty;
      if (this.SitesComboBox.SelectedItem is SiteInfo selectedItem)
        empty = selectedItem.GUID.ToString();
      int selectedIndex = this.PubCompositionCombo.SelectedIndex;
      int countLevels;
      switch (selectedIndex)
      {
        case 0:
          countLevels = -1;
          break;
        case 1:
          countLevels = 1;
          break;
        case 2:
          countLevels = 0;
          break;
        default:
          countLevels = selectedIndex - 1;
          break;
      }
      ExtendedPublishOptions publishOptions = new ExtendedPublishOptions(PublishCompositionOptions.None, countLevels, this._pubFilteredRelTypes, this._pubFilteredTypes, (FiltrationSettings) null);
      if (this.GiveOwnershipCheckBox.Checked)
        publishOptions.OwnerSite = new char?('Y');
      publishOptions.EnableSites = empty;
      service.SaveOptions(publishOptions, true);
    }
  }

  private void loadPublishOptions_Click(object sender, EventArgs e)
  {
    if (!(ApplicationServices.Container.GetService(typeof (ISaveDiskPublishOptionsDialogService)) is ISaveDiskPublishOptionsDialogService service))
    {
      int num1 = (int) MessageBox.Show("Сервис работы с настройками публикации не найден.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      ExtendedPublishOptions extendedPublishOptions = service.LoadOptions();
      if (extendedPublishOptions == null)
        return;
      if (!string.IsNullOrEmpty(extendedPublishOptions.EnableSites))
      {
        Guid guid = new Guid(extendedPublishOptions.EnableSites);
        foreach (SiteInfo siteInfo in this.SitesComboBox.Items)
        {
          if (siteInfo.GUID == guid)
          {
            this.SitesComboBox.SelectedItem = (object) siteInfo;
            break;
          }
        }
      }
      CheckBox ownershipCheckBox = this.GiveOwnershipCheckBox;
      char? ownerSite = extendedPublishOptions.OwnerSite;
      int? nullable = ownerSite.HasValue ? new int?((int) ownerSite.GetValueOrDefault()) : new int?();
      int num2 = 89;
      int num3 = nullable.GetValueOrDefault() == num2 & nullable.HasValue ? 1 : 0;
      ownershipCheckBox.Checked = num3 != 0;
      int countLevels = extendedPublishOptions.CountLevels;
      int num4;
      switch (countLevels)
      {
        case -1:
          num4 = 0;
          break;
        case 0:
          num4 = 2;
          break;
        case 1:
          num4 = 1;
          break;
        default:
          num4 = countLevels + 1;
          break;
      }
      this.PubCompositionCombo.SelectedIndex = num4;
      if (this.SitesComboBox.SelectedItem == null && this.SitesComboBox.Items.Count > 0)
        this.SitesComboBox.SelectedIndex = 0;
      this.SitesComboBox.SelectedIndexChanged += new EventHandler(this.SitesComboBox_SelectedIndexChanged);
      this.SitesComboBox_SelectedIndexChanged((object) null, (EventArgs) null);
      this._pubFilteredTypes = extendedPublishOptions.EnableTypes;
      this._pubFilteredRelTypes = extendedPublishOptions.EnableRelationTypes;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this.ToolTip != null)
        this.ToolTip.Dispose();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ActivPropForm));
    this.DocArcVarCombo = new System.Windows.Forms.ComboBox();
    this.RevArcVarCombo = new System.Windows.Forms.ComboBox();
    this.ApproveCheckHint = new Label();
    this.UseAlienHint = new Label();
    this.MsgStopCheckBox = new CheckBox();
    this.MsgBackCheckBox = new CheckBox();
    this.MsgStopPanel = new Panel();
    this.MsgStopButton = new Button();
    this.TabsIL = new ImageList(this.components);
    this.MsgBackPanel = new Panel();
    this.MsgBackButton = new Button();
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.PageControl = new TabControl();
    this.BaseTabSheet = new TabPage();
    this.panel1 = new Panel();
    this.showFormWhereActivityBack = new CheckBox();
    this.IDLabel = new Label();
    this.CollectorCheckBox = new CheckBox();
    this.Label1 = new Label();
    this.NameEdit = new TextBox();
    this.Label2 = new Label();
    this.ActImage = new PictureBox();
    this.DescriptionMemo = new TextBox();
    this.ActRuntimePanel = new Panel();
    this.label8 = new Label();
    this.txtCompletedLabel = new Label();
    this.txtStartedLabel = new Label();
    this.txtStatusLabel = new Label();
    this.CompletedLabel = new Label();
    this.StartedLabel = new Label();
    this.StatusLabel = new Label();
    this.ContentTabSheet = new TabPage();
    this.AttachsPanel = new Panel();
    this.ContentOptionsPanel = new Panel();
    this.AllowAddAttachsCheckBox = new CheckBox();
    this.AllowDelAttachsCheckBox = new CheckBox();
    this.TempRightsGroupBox = new GroupBox();
    this.TempRightsGroupingCheckBox = new CheckBox();
    this.TempRightsAdminCheckBox = new CheckBox();
    this.TempRightsEditCheckBox = new CheckBox();
    this.TempRightsViewCheckBox = new CheckBox();
    this.NoTempRightsButton = new RadioButton();
    this.SchemeAttachsTabSheet = new TabPage();
    this.SchemeAttachsGB = new GroupBox();
    this.CaseTabSheet = new TabPage();
    this.CondsView = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.MiscIL = new ImageList(this.components);
    this.panel3 = new Panel();
    this.CaseFilterCheckBox = new CheckBox();
    this.SepPanel = new Panel();
    this.ValidateCaseButton = new Button();
    this.changeObjectTypeInExpression = new Button();
    this.CondTabSheet = new TabPage();
    this.CondGroupBox = new GroupBox();
    this.ConditionBox = new ButtonEdit();
    this.ValidateButton = new Button();
    this.ParticipantsTabSheet = new TabPage();
    this.ParticipantsView = new UsersListView();
    this.PartsPanel = new Panel();
    this.PartKindGroupBox = new GroupBox();
    this.AnyPartButton = new RadioButton();
    this.AllPartsButton = new RadioButton();
    this.sendWorkOfferLastParticipantCheckBox = new CheckBox();
    this.DelUserButton = new Button();
    this.AddUserButton = new Button();
    this.DenyDelCheck = new CheckBox();
    this.RequireAnswerCheck = new CheckBox();
    this.sendParticipantsEmail = new CheckBox();
    this.AutoTabSheet = new TabPage();
    this.ConfigureButton = new Button();
    this.GroupBox7 = new GroupBox();
    this.WaitCompletionCheckBox = new CheckBox();
    this.InterruptCheckBox = new CheckBox();
    this.UnitsComboBox = new System.Windows.Forms.ComboBox();
    this.GroupBox2 = new GroupBox();
    this.SrvRadioButton = new RadioButton();
    this.UsrRadioButton = new RadioButton();
    this.GroupBox1 = new GroupBox();
    this.Label5 = new Label();
    this.Label7 = new Label();
    this.PluginDescMemo = new TextBox();
    this.PlgNameLabel = new TextBox();
    this.ApproveTabSheet = new TabPage();
    this.signsForType = new SignsForType();
    this.WhatSignGB = new GroupBox();
    this.SignTypesView = new EnhListView();
    this.columnHeader6 = new ColumnHeader();
    this.panel4 = new Panel();
    this.SignObjectTypesBar = new ToolBar();
    this.AddObjTypesButton = new ToolBarButton();
    this.DeleteObjTypesButton = new ToolBarButton();
    this.cmdsIL = new ImageList(this.components);
    this.DTypesRB2 = new RadioButton();
    this.DTypesRB1 = new RadioButton();
    this.WhatToSignCombo = new System.Windows.Forms.ComboBox();
    this.ApproveCheckSpacer = new Panel();
    this.ApproveVSpacer = new Panel();
    this.signSplitter = new Splitter();
    this.ApproveCheckGB = new GroupBox();
    this.ChooseApprovesButton = new Button();
    this.ApprovesBox = new TextBox();
    this.UseAlienSettingsCheckBox = new CheckBox();
    this.ApproveCheckOnlyCheckBox = new CheckBox();
    this.RanksPanel = new GroupBox();
    this.SignGraphView = new EnhListView();
    this.columnHeader3 = new ColumnHeader();
    this.columnHeader4 = new ColumnHeader();
    this.columnHeader5 = new ColumnHeader();
    this.panel8 = new Panel();
    this.GraphsBar = new ToolBar();
    this.toolBarButton3 = new ToolBarButton();
    this.DeleteSignsButton = new ToolBarButton();
    this.SignAsGraphRadioButton = new RadioButton();
    this.SignAsUserRadioButton = new RadioButton();
    this.PersonalSignsCheckBox = new CheckBox();
    this.graphForTypeCheckBox = new CheckBox();
    this.ArchiveTabSheet = new TabPage();
    this.DetachRegisteredCheckBox = new CheckBox();
    this.RegRevGroupBox = new GroupBox();
    this.RevArcIDEdit = new ButtonEdit();
    this.RevSpacerPanel = new Panel();
    this.RevArchiveVarRButton = new RadioButton();
    this.RevArchiveRButton = new RadioButton();
    this.RecArchiveCurrentRButton = new RadioButton();
    this.RegRevSpacer = new Panel();
    this.RegDocsGroupBox = new GroupBox();
    this.DocArcIDEdit = new ButtonEdit();
    this.DocSpacerPanel = new Panel();
    this.DocArchiveVarRButton = new RadioButton();
    this.DocArchiveRButton = new RadioButton();
    this.DocArchiveCurrentRButton = new RadioButton();
    this.panel11 = new Panel();
    this.RegisterGroupBox = new GroupBox();
    this.ArcModeRadio2 = new RadioButton();
    this.ArcModeRadio1 = new RadioButton();
    this.SubProcessTabSheet = new TabPage();
    this.GroupBox5 = new GroupBox();
    this.useActualVersionSchemeCheckBox = new CheckBox();
    this.SchemeEdit = new ButtonEdit();
    this.Label3 = new Label();
    this.SubNameLabel = new Label();
    this.WaitCheckBox = new CheckBox();
    this.SubNameEdit = new ButtonEdit();
    this.RemoteSubProcessTabSheet = new TabPage();
    this.RemoteProcess2GroupBox = new GroupBox();
    this.CreateReceiptCheckBox = new CheckBox();
    this.label13 = new Label();
    this.PubCompositionCombo = new System.Windows.Forms.ComboBox();
    this.PubRelTypesButton = new Button();
    this.GiveOwnershipCheckBox = new CheckBox();
    this.PubObjectTypesButton = new Button();
    this.panel9 = new Panel();
    this.RemoteProcessGroupBox = new GroupBox();
    this.RemoteSchemesComboBox = new System.Windows.Forms.ComboBox();
    this.SitesComboBox = new System.Windows.Forms.ComboBox();
    this.label12 = new Label();
    this.label11 = new Label();
    this.RemoteWaitCheckBox = new CheckBox();
    this.PortalErrPanel = new Panel();
    this.PortalErrLabel = new AutoSizeLabel();
    this.PortalErrImage = new PictureBox();
    this.TermsTabSheet = new TabPage();
    this.TermOptsGroupBox = new GroupBox();
    this.TermStartRB2 = new RadioButton();
    this.TermStartRB1 = new RadioButton();
    this.panel6 = new Panel();
    this.GroupBox10 = new GroupBox();
    this.unreadTermEdit = new ButtonEdit();
    this.UnreadRollbackCheckBox = new CheckBox();
    this.panel5 = new Panel();
    this.GroupBox8 = new GroupBox();
    this.termEdit = new ButtonEdit();
    this.UncompleteRollbackCheckBox = new CheckBox();
    this.TimerTabSheet = new TabPage();
    this.RollbackTabSheet = new TabPage();
    this.RollbackGroupBox = new GroupBox();
    this.RollRadioButton5 = new RadioButton();
    this.RollRadioButton1 = new RadioButton();
    this.RollRadioButton2 = new RadioButton();
    this.RollRadioButton3 = new RadioButton();
    this.RollRadioButton4 = new RadioButton();
    this.LCTabSheet = new TabPage();
    this.LCGroupBox = new GroupBox();
    this.LCPanel2 = new Panel();
    this.LCView2 = new EnhListView();
    this.columnHeader9 = new ColumnHeader();
    this.columnHeader10 = new ColumnHeader();
    this.label15 = new Label();
    this.LCStepToolBar2 = new ToolBar();
    this.toolBarButton5 = new ToolBarButton();
    this.toolBarButton6 = new ToolBarButton();
    this.toolBarButton7 = new ToolBarButton();
    this.LCLabel2 = new Label();
    this.LCPanel1 = new Panel();
    this.LCView = new EnhListView();
    this.columnHeader7 = new ColumnHeader();
    this.columnHeader8 = new ColumnHeader();
    this.label9 = new Label();
    this.LCStepToolBar = new ToolBar();
    this.toolBarButton1 = new ToolBarButton();
    this.toolBarButton4 = new ToolBarButton();
    this.toolBarButton2 = new ToolBarButton();
    this.LCLabel1 = new Label();
    this.NotifTabSheet = new TabPage();
    this.MsgsGroupBox = new GroupBox();
    this.MsgAbortPanel = new Panel();
    this.MsgAbortCheckBox = new CheckBox();
    this.MsgAbortButton = new Button();
    this.MsgPeriodPanel = new Panel();
    this.MsgPeriodCheckBox = new CheckBox();
    this.MsgPeriodButton = new Button();
    this.MsgReadPanel = new Panel();
    this.MsgReadCheckBox = new CheckBox();
    this.MsgReadButton = new Button();
    this.MsgStartPanel = new Panel();
    this.MsgStartCheckBox = new CheckBox();
    this.MsgStartButton = new Button();
    this.ScriptTabSheet = new TabPage();
    this.AfterScriptGroupBox = new GroupBox();
    this.afterScriptEdit = new ButtonEdit();
    this.AfterScriptExecCombo = new System.Windows.Forms.ComboBox();
    this.afterScriptType = new System.Windows.Forms.ComboBox();
    this.label6 = new Label();
    this.label17 = new Label();
    this.AfterScriptCheckBox = new CheckBox();
    this.panel13 = new Panel();
    this.BeforeScriptGroupBox = new GroupBox();
    this.beforeScriptEdit = new ButtonEdit();
    this.BeforeScriptExecCombo = new System.Windows.Forms.ComboBox();
    this.beforeScriptType = new System.Windows.Forms.ComboBox();
    this.label4 = new Label();
    this.label16 = new Label();
    this.BeforeScriptCheckBox = new CheckBox();
    this.ScriptGroupBox = new GroupBox();
    this.scriptEdit = new ButtonEdit();
    this.ScriptUserGroupBox = new GroupBox();
    this.ScriptUserEdit = new ButtonEdit();
    this.scriptType = new System.Windows.Forms.ComboBox();
    this.ScriptExecCombo = new System.Windows.Forms.ComboBox();
    this.label14 = new Label();
    this.label10 = new Label();
    this.Column = new ColumnHeader();
    this.ToolTip = new EnhToolTip(this.components);
    this.loadPublishOptions = new Button();
    this.savePublishOptions = new Button();
    this.MsgStopPanel.SuspendLayout();
    this.MsgBackPanel.SuspendLayout();
    this.Panel2.SuspendLayout();
    this.PageControl.SuspendLayout();
    this.BaseTabSheet.SuspendLayout();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.ActImage).BeginInit();
    this.ActRuntimePanel.SuspendLayout();
    this.ContentTabSheet.SuspendLayout();
    this.ContentOptionsPanel.SuspendLayout();
    this.TempRightsGroupBox.SuspendLayout();
    this.SchemeAttachsTabSheet.SuspendLayout();
    this.CaseTabSheet.SuspendLayout();
    this.panel3.SuspendLayout();
    this.CondTabSheet.SuspendLayout();
    this.CondGroupBox.SuspendLayout();
    this.ConditionBox.Properties.BeginInit();
    this.ParticipantsTabSheet.SuspendLayout();
    this.PartsPanel.SuspendLayout();
    this.PartKindGroupBox.SuspendLayout();
    this.AutoTabSheet.SuspendLayout();
    this.GroupBox7.SuspendLayout();
    this.GroupBox2.SuspendLayout();
    this.GroupBox1.SuspendLayout();
    this.ApproveTabSheet.SuspendLayout();
    this.WhatSignGB.SuspendLayout();
    this.panel4.SuspendLayout();
    this.ApproveCheckGB.SuspendLayout();
    this.RanksPanel.SuspendLayout();
    this.panel8.SuspendLayout();
    this.ArchiveTabSheet.SuspendLayout();
    this.RegRevGroupBox.SuspendLayout();
    this.RevArcIDEdit.Properties.BeginInit();
    this.RegDocsGroupBox.SuspendLayout();
    this.DocArcIDEdit.Properties.BeginInit();
    this.RegisterGroupBox.SuspendLayout();
    this.SubProcessTabSheet.SuspendLayout();
    this.GroupBox5.SuspendLayout();
    this.SchemeEdit.Properties.BeginInit();
    this.SubNameEdit.Properties.BeginInit();
    this.RemoteSubProcessTabSheet.SuspendLayout();
    this.RemoteProcess2GroupBox.SuspendLayout();
    this.RemoteProcessGroupBox.SuspendLayout();
    this.PortalErrPanel.SuspendLayout();
    ((ISupportInitialize) this.PortalErrImage).BeginInit();
    this.TermsTabSheet.SuspendLayout();
    this.TermOptsGroupBox.SuspendLayout();
    this.GroupBox10.SuspendLayout();
    this.unreadTermEdit.Properties.BeginInit();
    this.GroupBox8.SuspendLayout();
    this.termEdit.Properties.BeginInit();
    this.RollbackTabSheet.SuspendLayout();
    this.RollbackGroupBox.SuspendLayout();
    this.LCTabSheet.SuspendLayout();
    this.LCGroupBox.SuspendLayout();
    this.LCPanel2.SuspendLayout();
    this.LCPanel1.SuspendLayout();
    this.NotifTabSheet.SuspendLayout();
    this.MsgsGroupBox.SuspendLayout();
    this.MsgAbortPanel.SuspendLayout();
    this.MsgPeriodPanel.SuspendLayout();
    this.MsgReadPanel.SuspendLayout();
    this.MsgStartPanel.SuspendLayout();
    this.ScriptTabSheet.SuspendLayout();
    this.AfterScriptGroupBox.SuspendLayout();
    this.afterScriptEdit.Properties.BeginInit();
    this.BeforeScriptGroupBox.SuspendLayout();
    this.beforeScriptEdit.Properties.BeginInit();
    this.ScriptGroupBox.SuspendLayout();
    this.scriptEdit.Properties.BeginInit();
    this.ScriptUserGroupBox.SuspendLayout();
    this.ScriptUserEdit.Properties.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.DocArcVarCombo, "DocArcVarCombo");
    this.DocArcVarCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.DocArcVarCombo.Items.AddRange(new object[1]
    {
      (object) componentResourceManager.GetString("DocArcVarCombo.Items")
    });
    this.DocArcVarCombo.Name = "DocArcVarCombo";
    this.ToolTip.SetToolTip((Control) this.DocArcVarCombo, componentResourceManager.GetString("DocArcVarCombo.ToolTip"));
    componentResourceManager.ApplyResources((object) this.RevArcVarCombo, "RevArcVarCombo");
    this.RevArcVarCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.RevArcVarCombo.Items.AddRange(new object[1]
    {
      (object) componentResourceManager.GetString("RevArcVarCombo.Items")
    });
    this.RevArcVarCombo.Name = "RevArcVarCombo";
    this.ToolTip.SetToolTip((Control) this.RevArcVarCombo, componentResourceManager.GetString("RevArcVarCombo.ToolTip"));
    componentResourceManager.ApplyResources((object) this.ApproveCheckHint, "ApproveCheckHint");
    this.ApproveCheckHint.BackColor = SystemColors.Info;
    this.ApproveCheckHint.Name = "ApproveCheckHint";
    this.ToolTip.SetToolTip((Control) this.ApproveCheckHint, componentResourceManager.GetString("ApproveCheckHint.ToolTip"));
    componentResourceManager.ApplyResources((object) this.UseAlienHint, "UseAlienHint");
    this.UseAlienHint.BackColor = SystemColors.Info;
    this.UseAlienHint.Name = "UseAlienHint";
    this.ToolTip.SetToolTip((Control) this.UseAlienHint, componentResourceManager.GetString("UseAlienHint.ToolTip"));
    componentResourceManager.ApplyResources((object) this.MsgStopCheckBox, "MsgStopCheckBox");
    this.MsgStopCheckBox.Name = "MsgStopCheckBox";
    this.MsgStopCheckBox.Tag = (object) "";
    this.ToolTip.SetToolTip((Control) this.MsgStopCheckBox, componentResourceManager.GetString("MsgStopCheckBox.ToolTip"));
    this.MsgStopCheckBox.CheckedChanged += new EventHandler(this.MsgStartCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.MsgBackCheckBox, "MsgBackCheckBox");
    this.MsgBackCheckBox.Name = "MsgBackCheckBox";
    this.MsgBackCheckBox.Tag = (object) "";
    this.ToolTip.SetToolTip((Control) this.MsgBackCheckBox, componentResourceManager.GetString("MsgBackCheckBox.ToolTip"));
    this.MsgBackCheckBox.CheckedChanged += new EventHandler(this.MsgStartCheckBox_CheckedChanged);
    this.MsgStopPanel.Controls.Add((Control) this.MsgStopCheckBox);
    this.MsgStopPanel.Controls.Add((Control) this.MsgStopButton);
    componentResourceManager.ApplyResources((object) this.MsgStopPanel, "MsgStopPanel");
    this.MsgStopPanel.Name = "MsgStopPanel";
    componentResourceManager.ApplyResources((object) this.MsgStopButton, "MsgStopButton");
    this.MsgStopButton.ImageList = this.TabsIL;
    this.MsgStopButton.Name = "MsgStopButton";
    this.MsgStopButton.Tag = (object) "3";
    this.MsgStopButton.Click += new EventHandler(this.EditMessageClick);
    this.TabsIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("TabsIL.ImageStream");
    this.TabsIL.TransparentColor = Color.Fuchsia;
    this.TabsIL.Images.SetKeyName(0, "");
    this.TabsIL.Images.SetKeyName(1, "user_gr.ico");
    this.TabsIL.Images.SetKeyName(2, "addon.ico");
    this.TabsIL.Images.SetKeyName(3, "");
    this.TabsIL.Images.SetKeyName(4, "");
    this.TabsIL.Images.SetKeyName(5, "");
    this.TabsIL.Images.SetKeyName(6, "");
    this.TabsIL.Images.SetKeyName(7, "back.ico");
    this.TabsIL.Images.SetKeyName(8, "sign.ico");
    this.TabsIL.Images.SetKeyName(9, "");
    this.TabsIL.Images.SetKeyName(10, "arc.ico");
    this.TabsIL.Images.SetKeyName(11, "");
    this.TabsIL.Images.SetKeyName(12, "status.png");
    this.TabsIL.Images.SetKeyName(13, "time.ico");
    this.TabsIL.Images.SetKeyName(14, "info_z.ico");
    this.TabsIL.Images.SetKeyName(15, "Подпроцесс2_16.bmp");
    this.MsgBackPanel.Controls.Add((Control) this.MsgBackCheckBox);
    this.MsgBackPanel.Controls.Add((Control) this.MsgBackButton);
    componentResourceManager.ApplyResources((object) this.MsgBackPanel, "MsgBackPanel");
    this.MsgBackPanel.Name = "MsgBackPanel";
    componentResourceManager.ApplyResources((object) this.MsgBackButton, "MsgBackButton");
    this.MsgBackButton.ImageList = this.TabsIL;
    this.MsgBackButton.Name = "MsgBackButton";
    this.MsgBackButton.Tag = (object) "6";
    this.MsgBackButton.Click += new EventHandler(this.EditMessageClick);
    this.Panel2.BackColor = Color.Transparent;
    this.Panel2.Controls.Add((Control) this.CancButton);
    this.Panel2.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.Panel2, "Panel2");
    this.Panel2.Name = "Panel2";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    this.PageControl.Controls.Add((Control) this.BaseTabSheet);
    this.PageControl.Controls.Add((Control) this.ContentTabSheet);
    this.PageControl.Controls.Add((Control) this.SchemeAttachsTabSheet);
    this.PageControl.Controls.Add((Control) this.CaseTabSheet);
    this.PageControl.Controls.Add((Control) this.CondTabSheet);
    this.PageControl.Controls.Add((Control) this.ParticipantsTabSheet);
    this.PageControl.Controls.Add((Control) this.AutoTabSheet);
    this.PageControl.Controls.Add((Control) this.ApproveTabSheet);
    this.PageControl.Controls.Add((Control) this.ArchiveTabSheet);
    this.PageControl.Controls.Add((Control) this.SubProcessTabSheet);
    this.PageControl.Controls.Add((Control) this.RemoteSubProcessTabSheet);
    this.PageControl.Controls.Add((Control) this.TermsTabSheet);
    this.PageControl.Controls.Add((Control) this.TimerTabSheet);
    this.PageControl.Controls.Add((Control) this.RollbackTabSheet);
    this.PageControl.Controls.Add((Control) this.LCTabSheet);
    this.PageControl.Controls.Add((Control) this.NotifTabSheet);
    this.PageControl.Controls.Add((Control) this.ScriptTabSheet);
    componentResourceManager.ApplyResources((object) this.PageControl, "PageControl");
    this.PageControl.ImageList = this.TabsIL;
    this.PageControl.Multiline = true;
    this.PageControl.Name = "PageControl";
    this.PageControl.SelectedIndex = 0;
    this.PageControl.SelectedIndexChanged += new EventHandler(this.PageControl_SelectedIndexChanged);
    this.BaseTabSheet.BackColor = Color.Transparent;
    this.BaseTabSheet.Controls.Add((Control) this.panel1);
    this.BaseTabSheet.Controls.Add((Control) this.ActRuntimePanel);
    componentResourceManager.ApplyResources((object) this.BaseTabSheet, "BaseTabSheet");
    this.BaseTabSheet.Name = "BaseTabSheet";
    this.BaseTabSheet.UseVisualStyleBackColor = true;
    this.panel1.BackColor = Color.Transparent;
    this.panel1.Controls.Add((Control) this.showFormWhereActivityBack);
    this.panel1.Controls.Add((Control) this.IDLabel);
    this.panel1.Controls.Add((Control) this.CollectorCheckBox);
    this.panel1.Controls.Add((Control) this.Label1);
    this.panel1.Controls.Add((Control) this.NameEdit);
    this.panel1.Controls.Add((Control) this.Label2);
    this.panel1.Controls.Add((Control) this.ActImage);
    this.panel1.Controls.Add((Control) this.DescriptionMemo);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.showFormWhereActivityBack, "showFormWhereActivityBack");
    this.showFormWhereActivityBack.Name = "showFormWhereActivityBack";
    this.showFormWhereActivityBack.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.IDLabel, "IDLabel");
    this.IDLabel.Name = "IDLabel";
    componentResourceManager.ApplyResources((object) this.CollectorCheckBox, "CollectorCheckBox");
    this.CollectorCheckBox.Name = "CollectorCheckBox";
    componentResourceManager.ApplyResources((object) this.Label1, "Label1");
    this.Label1.Name = "Label1";
    componentResourceManager.ApplyResources((object) this.NameEdit, "NameEdit");
    this.NameEdit.Name = "NameEdit";
    componentResourceManager.ApplyResources((object) this.Label2, "Label2");
    this.Label2.Name = "Label2";
    componentResourceManager.ApplyResources((object) this.ActImage, "ActImage");
    this.ActImage.Name = "ActImage";
    this.ActImage.TabStop = false;
    this.DescriptionMemo.AcceptsReturn = true;
    componentResourceManager.ApplyResources((object) this.DescriptionMemo, "DescriptionMemo");
    this.DescriptionMemo.Name = "DescriptionMemo";
    this.ActRuntimePanel.BackColor = Color.Transparent;
    this.ActRuntimePanel.Controls.Add((Control) this.label8);
    this.ActRuntimePanel.Controls.Add((Control) this.txtCompletedLabel);
    this.ActRuntimePanel.Controls.Add((Control) this.txtStartedLabel);
    this.ActRuntimePanel.Controls.Add((Control) this.txtStatusLabel);
    this.ActRuntimePanel.Controls.Add((Control) this.CompletedLabel);
    this.ActRuntimePanel.Controls.Add((Control) this.StartedLabel);
    this.ActRuntimePanel.Controls.Add((Control) this.StatusLabel);
    componentResourceManager.ApplyResources((object) this.ActRuntimePanel, "ActRuntimePanel");
    this.ActRuntimePanel.Name = "ActRuntimePanel";
    this.label8.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this.label8, "label8");
    this.label8.Name = "label8";
    componentResourceManager.ApplyResources((object) this.txtCompletedLabel, "txtCompletedLabel");
    this.txtCompletedLabel.Name = "txtCompletedLabel";
    componentResourceManager.ApplyResources((object) this.txtStartedLabel, "txtStartedLabel");
    this.txtStartedLabel.Name = "txtStartedLabel";
    componentResourceManager.ApplyResources((object) this.txtStatusLabel, "txtStatusLabel");
    this.txtStatusLabel.Name = "txtStatusLabel";
    componentResourceManager.ApplyResources((object) this.CompletedLabel, "CompletedLabel");
    this.CompletedLabel.Name = "CompletedLabel";
    componentResourceManager.ApplyResources((object) this.StartedLabel, "StartedLabel");
    this.StartedLabel.Name = "StartedLabel";
    componentResourceManager.ApplyResources((object) this.StatusLabel, "StatusLabel");
    this.StatusLabel.Name = "StatusLabel";
    this.ContentTabSheet.Controls.Add((Control) this.AttachsPanel);
    this.ContentTabSheet.Controls.Add((Control) this.ContentOptionsPanel);
    this.ContentTabSheet.Controls.Add((Control) this.TempRightsGroupBox);
    componentResourceManager.ApplyResources((object) this.ContentTabSheet, "ContentTabSheet");
    this.ContentTabSheet.Name = "ContentTabSheet";
    this.ContentTabSheet.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.AttachsPanel, "AttachsPanel");
    this.AttachsPanel.Name = "AttachsPanel";
    componentResourceManager.ApplyResources((object) this.ContentOptionsPanel, "ContentOptionsPanel");
    this.ContentOptionsPanel.Controls.Add((Control) this.AllowAddAttachsCheckBox);
    this.ContentOptionsPanel.Controls.Add((Control) this.AllowDelAttachsCheckBox);
    this.ContentOptionsPanel.Name = "ContentOptionsPanel";
    componentResourceManager.ApplyResources((object) this.AllowAddAttachsCheckBox, "AllowAddAttachsCheckBox");
    this.AllowAddAttachsCheckBox.Name = "AllowAddAttachsCheckBox";
    componentResourceManager.ApplyResources((object) this.AllowDelAttachsCheckBox, "AllowDelAttachsCheckBox");
    this.AllowDelAttachsCheckBox.Name = "AllowDelAttachsCheckBox";
    this.TempRightsGroupBox.Controls.Add((Control) this.TempRightsGroupingCheckBox);
    this.TempRightsGroupBox.Controls.Add((Control) this.TempRightsAdminCheckBox);
    this.TempRightsGroupBox.Controls.Add((Control) this.TempRightsEditCheckBox);
    this.TempRightsGroupBox.Controls.Add((Control) this.TempRightsViewCheckBox);
    this.TempRightsGroupBox.Controls.Add((Control) this.NoTempRightsButton);
    componentResourceManager.ApplyResources((object) this.TempRightsGroupBox, "TempRightsGroupBox");
    this.TempRightsGroupBox.Name = "TempRightsGroupBox";
    this.TempRightsGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.TempRightsGroupingCheckBox, "TempRightsGroupingCheckBox");
    this.TempRightsGroupingCheckBox.Name = "TempRightsGroupingCheckBox";
    this.TempRightsGroupingCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.TempRightsAdminCheckBox, "TempRightsAdminCheckBox");
    this.TempRightsAdminCheckBox.Name = "TempRightsAdminCheckBox";
    this.TempRightsAdminCheckBox.UseVisualStyleBackColor = true;
    this.TempRightsAdminCheckBox.Click += new EventHandler(this.TempRightsViewCheckBox_Click);
    componentResourceManager.ApplyResources((object) this.TempRightsEditCheckBox, "TempRightsEditCheckBox");
    this.TempRightsEditCheckBox.Name = "TempRightsEditCheckBox";
    this.TempRightsEditCheckBox.UseVisualStyleBackColor = true;
    this.TempRightsEditCheckBox.Click += new EventHandler(this.TempRightsViewCheckBox_Click);
    componentResourceManager.ApplyResources((object) this.TempRightsViewCheckBox, "TempRightsViewCheckBox");
    this.TempRightsViewCheckBox.Name = "TempRightsViewCheckBox";
    this.TempRightsViewCheckBox.UseVisualStyleBackColor = true;
    this.TempRightsViewCheckBox.Click += new EventHandler(this.TempRightsViewCheckBox_Click);
    componentResourceManager.ApplyResources((object) this.NoTempRightsButton, "NoTempRightsButton");
    this.NoTempRightsButton.Name = "NoTempRightsButton";
    this.NoTempRightsButton.TabStop = true;
    this.NoTempRightsButton.UseVisualStyleBackColor = true;
    this.NoTempRightsButton.Click += new EventHandler(this.TempRightsViewCheckBox_Click);
    this.SchemeAttachsTabSheet.Controls.Add((Control) this.SchemeAttachsGB);
    componentResourceManager.ApplyResources((object) this.SchemeAttachsTabSheet, "SchemeAttachsTabSheet");
    this.SchemeAttachsTabSheet.Name = "SchemeAttachsTabSheet";
    this.SchemeAttachsTabSheet.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.SchemeAttachsGB, "SchemeAttachsGB");
    this.SchemeAttachsGB.Name = "SchemeAttachsGB";
    this.SchemeAttachsGB.TabStop = false;
    this.CaseTabSheet.Controls.Add((Control) this.CondsView);
    this.CaseTabSheet.Controls.Add((Control) this.panel3);
    componentResourceManager.ApplyResources((object) this.CaseTabSheet, "CaseTabSheet");
    this.CaseTabSheet.Name = "CaseTabSheet";
    this.CaseTabSheet.UseVisualStyleBackColor = true;
    this.CondsView.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    componentResourceManager.ApplyResources((object) this.CondsView, "CondsView");
    this.CondsView.FullRowSelect = true;
    this.CondsView.HideSelection = false;
    this.CondsView.LabelEdit = true;
    this.CondsView.Name = "CondsView";
    this.CondsView.SmallImageList = this.MiscIL;
    this.CondsView.UseCompatibleStateImageBehavior = false;
    this.CondsView.View = View.Details;
    this.CondsView.BeforeLabelEdit += new LabelEditEventHandler(this.CondsView_BeforeLabelEdit);
    this.CondsView.DoubleClick += new EventHandler(this.CondsView_DoubleClick);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    this.MiscIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("MiscIL.ImageStream");
    this.MiscIL.TransparentColor = Color.Fuchsia;
    this.MiscIL.Images.SetKeyName(0, "");
    this.MiscIL.Images.SetKeyName(1, "");
    this.MiscIL.Images.SetKeyName(2, "");
    this.MiscIL.Images.SetKeyName(3, "");
    this.MiscIL.Images.SetKeyName(4, "");
    this.MiscIL.Images.SetKeyName(5, "");
    this.MiscIL.Images.SetKeyName(6, "");
    this.MiscIL.Images.SetKeyName(7, "");
    this.MiscIL.Images.SetKeyName(8, "");
    this.MiscIL.Images.SetKeyName(9, "");
    this.MiscIL.Images.SetKeyName(10, "abort16x16.bmp");
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Controls.Add((Control) this.CaseFilterCheckBox);
    this.panel3.Controls.Add((Control) this.SepPanel);
    this.panel3.Controls.Add((Control) this.ValidateCaseButton);
    this.panel3.Controls.Add((Control) this.changeObjectTypeInExpression);
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.CaseFilterCheckBox, "CaseFilterCheckBox");
    this.CaseFilterCheckBox.Name = "CaseFilterCheckBox";
    this.CaseFilterCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.SepPanel, "SepPanel");
    this.SepPanel.Name = "SepPanel";
    componentResourceManager.ApplyResources((object) this.ValidateCaseButton, "ValidateCaseButton");
    this.ValidateCaseButton.Name = "ValidateCaseButton";
    this.ValidateCaseButton.Click += new EventHandler(this.ValidateCaseButton_Click);
    componentResourceManager.ApplyResources((object) this.changeObjectTypeInExpression, "changeObjectTypeInExpression");
    this.changeObjectTypeInExpression.Name = "changeObjectTypeInExpression";
    this.changeObjectTypeInExpression.Click += new EventHandler(this.ChangeExpressionType_Click);
    this.CondTabSheet.Controls.Add((Control) this.CondGroupBox);
    componentResourceManager.ApplyResources((object) this.CondTabSheet, "CondTabSheet");
    this.CondTabSheet.Name = "CondTabSheet";
    this.CondTabSheet.UseVisualStyleBackColor = true;
    this.CondGroupBox.Controls.Add((Control) this.ConditionBox);
    this.CondGroupBox.Controls.Add((Control) this.ValidateButton);
    componentResourceManager.ApplyResources((object) this.CondGroupBox, "CondGroupBox");
    this.CondGroupBox.Name = "CondGroupBox";
    this.CondGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.ConditionBox, "ConditionBox");
    this.ConditionBox.Name = "ConditionBox";
    this.ConditionBox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.ConditionBox.Properties.ReadOnly = true;
    this.ConditionBox.ButtonClick += new ButtonPressedEventHandler(this.ConditionBox_ButtonClick);
    this.ConditionBox.TextChanged += new EventHandler(this.ConditionBox_TextChanged);
    componentResourceManager.ApplyResources((object) this.ValidateButton, "ValidateButton");
    this.ValidateButton.Name = "ValidateButton";
    this.ValidateButton.Click += new EventHandler(this.ValidateButton_Click);
    this.ParticipantsTabSheet.Controls.Add((Control) this.ParticipantsView);
    this.ParticipantsTabSheet.Controls.Add((Control) this.PartsPanel);
    this.ParticipantsTabSheet.Controls.Add((Control) this.DenyDelCheck);
    this.ParticipantsTabSheet.Controls.Add((Control) this.RequireAnswerCheck);
    this.ParticipantsTabSheet.Controls.Add((Control) this.sendParticipantsEmail);
    componentResourceManager.ApplyResources((object) this.ParticipantsTabSheet, "ParticipantsTabSheet");
    this.ParticipantsTabSheet.Name = "ParticipantsTabSheet";
    this.ParticipantsTabSheet.UseVisualStyleBackColor = true;
    this.ParticipantsView.AddButton = (Button) null;
    this.ParticipantsView.AllowManualSorting = true;
    this.ParticipantsView.DelButton = (Button) null;
    componentResourceManager.ApplyResources((object) this.ParticipantsView, "ParticipantsView");
    this.ParticipantsView.FullRowSelect = true;
    this.ParticipantsView.HideSelection = false;
    this.ParticipantsView.Name = "ParticipantsView";
    this.ParticipantsView.OwnerDraw = true;
    this.ParticipantsView.ProcessID = 0L;
    this.ParticipantsView.RadioGroups = false;
    this.ParticipantsView.ReadOnly = false;
    this.ParticipantsView.SortColumn = 0;
    this.ParticipantsView.Sorting = SortOrder.Ascending;
    this.ParticipantsView.SubitemImages = (ImageList) null;
    this.ParticipantsView.UseCompatibleStateImageBehavior = false;
    this.ParticipantsView.View = View.Details;
    this.PartsPanel.Controls.Add((Control) this.PartKindGroupBox);
    this.PartsPanel.Controls.Add((Control) this.DelUserButton);
    this.PartsPanel.Controls.Add((Control) this.AddUserButton);
    componentResourceManager.ApplyResources((object) this.PartsPanel, "PartsPanel");
    this.PartsPanel.Name = "PartsPanel";
    componentResourceManager.ApplyResources((object) this.PartKindGroupBox, "PartKindGroupBox");
    this.PartKindGroupBox.Controls.Add((Control) this.AnyPartButton);
    this.PartKindGroupBox.Controls.Add((Control) this.AllPartsButton);
    this.PartKindGroupBox.Controls.Add((Control) this.sendWorkOfferLastParticipantCheckBox);
    this.PartKindGroupBox.Name = "PartKindGroupBox";
    this.PartKindGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.AnyPartButton, "AnyPartButton");
    this.AnyPartButton.Name = "AnyPartButton";
    this.AnyPartButton.CheckedChanged += new EventHandler(this.AnyPartButton_CheckedChanged);
    this.AllPartsButton.Checked = true;
    componentResourceManager.ApplyResources((object) this.AllPartsButton, "AllPartsButton");
    this.AllPartsButton.Name = "AllPartsButton";
    this.AllPartsButton.TabStop = true;
    this.AllPartsButton.CheckedChanged += new EventHandler(this.AllPartsButton_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.sendWorkOfferLastParticipantCheckBox, "sendWorkOfferLastParticipantCheckBox");
    this.sendWorkOfferLastParticipantCheckBox.Name = "sendWorkOfferLastParticipantCheckBox";
    componentResourceManager.ApplyResources((object) this.DelUserButton, "DelUserButton");
    this.DelUserButton.Name = "DelUserButton";
    componentResourceManager.ApplyResources((object) this.AddUserButton, "AddUserButton");
    this.AddUserButton.Name = "AddUserButton";
    componentResourceManager.ApplyResources((object) this.DenyDelCheck, "DenyDelCheck");
    this.DenyDelCheck.Name = "DenyDelCheck";
    componentResourceManager.ApplyResources((object) this.RequireAnswerCheck, "RequireAnswerCheck");
    this.RequireAnswerCheck.Name = "RequireAnswerCheck";
    this.sendParticipantsEmail.Checked = true;
    this.sendParticipantsEmail.CheckState = CheckState.Checked;
    componentResourceManager.ApplyResources((object) this.sendParticipantsEmail, "sendParticipantsEmail");
    this.sendParticipantsEmail.Name = "sendParticipantsEmail";
    this.AutoTabSheet.Controls.Add((Control) this.ConfigureButton);
    this.AutoTabSheet.Controls.Add((Control) this.GroupBox7);
    this.AutoTabSheet.Controls.Add((Control) this.GroupBox2);
    this.AutoTabSheet.Controls.Add((Control) this.GroupBox1);
    componentResourceManager.ApplyResources((object) this.AutoTabSheet, "AutoTabSheet");
    this.AutoTabSheet.Name = "AutoTabSheet";
    this.AutoTabSheet.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.ConfigureButton, "ConfigureButton");
    this.ConfigureButton.Name = "ConfigureButton";
    this.GroupBox7.Controls.Add((Control) this.WaitCompletionCheckBox);
    this.GroupBox7.Controls.Add((Control) this.InterruptCheckBox);
    this.GroupBox7.Controls.Add((Control) this.UnitsComboBox);
    componentResourceManager.ApplyResources((object) this.GroupBox7, "GroupBox7");
    this.GroupBox7.Name = "GroupBox7";
    this.GroupBox7.TabStop = false;
    componentResourceManager.ApplyResources((object) this.WaitCompletionCheckBox, "WaitCompletionCheckBox");
    this.WaitCompletionCheckBox.Name = "WaitCompletionCheckBox";
    componentResourceManager.ApplyResources((object) this.InterruptCheckBox, "InterruptCheckBox");
    this.InterruptCheckBox.Name = "InterruptCheckBox";
    componentResourceManager.ApplyResources((object) this.UnitsComboBox, "UnitsComboBox");
    this.UnitsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.UnitsComboBox.Items.AddRange(new object[1]
    {
      (object) componentResourceManager.GetString("UnitsComboBox.Items")
    });
    this.UnitsComboBox.Name = "UnitsComboBox";
    this.GroupBox2.Controls.Add((Control) this.SrvRadioButton);
    this.GroupBox2.Controls.Add((Control) this.UsrRadioButton);
    componentResourceManager.ApplyResources((object) this.GroupBox2, "GroupBox2");
    this.GroupBox2.Name = "GroupBox2";
    this.GroupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.SrvRadioButton, "SrvRadioButton");
    this.SrvRadioButton.Name = "SrvRadioButton";
    componentResourceManager.ApplyResources((object) this.UsrRadioButton, "UsrRadioButton");
    this.UsrRadioButton.Name = "UsrRadioButton";
    this.GroupBox1.Controls.Add((Control) this.Label5);
    this.GroupBox1.Controls.Add((Control) this.Label7);
    this.GroupBox1.Controls.Add((Control) this.PluginDescMemo);
    this.GroupBox1.Controls.Add((Control) this.PlgNameLabel);
    componentResourceManager.ApplyResources((object) this.GroupBox1, "GroupBox1");
    this.GroupBox1.Name = "GroupBox1";
    this.GroupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.Label5, "Label5");
    this.Label5.Name = "Label5";
    componentResourceManager.ApplyResources((object) this.Label7, "Label7");
    this.Label7.Name = "Label7";
    this.PluginDescMemo.AcceptsReturn = true;
    componentResourceManager.ApplyResources((object) this.PluginDescMemo, "PluginDescMemo");
    this.PluginDescMemo.Name = "PluginDescMemo";
    this.PluginDescMemo.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.PlgNameLabel, "PlgNameLabel");
    this.PlgNameLabel.Name = "PlgNameLabel";
    this.PlgNameLabel.ReadOnly = true;
    this.ApproveTabSheet.Controls.Add((Control) this.signsForType);
    this.ApproveTabSheet.Controls.Add((Control) this.WhatSignGB);
    this.ApproveTabSheet.Controls.Add((Control) this.ApproveCheckSpacer);
    this.ApproveTabSheet.Controls.Add((Control) this.ApproveVSpacer);
    this.ApproveTabSheet.Controls.Add((Control) this.signSplitter);
    this.ApproveTabSheet.Controls.Add((Control) this.ApproveCheckGB);
    this.ApproveTabSheet.Controls.Add((Control) this.RanksPanel);
    this.ApproveTabSheet.Controls.Add((Control) this.graphForTypeCheckBox);
    componentResourceManager.ApplyResources((object) this.ApproveTabSheet, "ApproveTabSheet");
    this.ApproveTabSheet.Name = "ApproveTabSheet";
    this.ApproveTabSheet.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.signsForType, "signsForType");
    this.signsForType.Name = "signsForType";
    this.WhatSignGB.Controls.Add((Control) this.SignTypesView);
    this.WhatSignGB.Controls.Add((Control) this.panel4);
    this.WhatSignGB.Controls.Add((Control) this.DTypesRB2);
    this.WhatSignGB.Controls.Add((Control) this.DTypesRB1);
    this.WhatSignGB.Controls.Add((Control) this.WhatToSignCombo);
    componentResourceManager.ApplyResources((object) this.WhatSignGB, "WhatSignGB");
    this.WhatSignGB.Name = "WhatSignGB";
    this.WhatSignGB.TabStop = false;
    this.SignTypesView.AllowManualSorting = true;
    componentResourceManager.ApplyResources((object) this.SignTypesView, "SignTypesView");
    this.SignTypesView.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader6
    });
    this.SignTypesView.FullRowSelect = true;
    this.SignTypesView.HideSelection = false;
    this.SignTypesView.Name = "SignTypesView";
    this.SignTypesView.OwnerDraw = true;
    this.SignTypesView.RadioGroups = false;
    this.SignTypesView.SortColumn = 0;
    this.SignTypesView.Sorting = SortOrder.Ascending;
    this.SignTypesView.SubitemImages = (ImageList) null;
    this.SignTypesView.UseCompatibleStateImageBehavior = false;
    this.SignTypesView.View = View.Details;
    this.SignTypesView.SelectedIndexChanged += new EventHandler(this.SignTypesView_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader6, "columnHeader6");
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Controls.Add((Control) this.SignObjectTypesBar);
    this.panel4.Name = "panel4";
    componentResourceManager.ApplyResources((object) this.SignObjectTypesBar, "SignObjectTypesBar");
    this.SignObjectTypesBar.Buttons.AddRange(new ToolBarButton[2]
    {
      this.AddObjTypesButton,
      this.DeleteObjTypesButton
    });
    this.SignObjectTypesBar.Divider = false;
    this.SignObjectTypesBar.ImageList = this.cmdsIL;
    this.SignObjectTypesBar.Name = "SignObjectTypesBar";
    this.SignObjectTypesBar.ButtonClick += new ToolBarButtonClickEventHandler(this.SignObjectTypesBar_ButtonClick);
    componentResourceManager.ApplyResources((object) this.AddObjTypesButton, "AddObjTypesButton");
    this.AddObjTypesButton.Name = "AddObjTypesButton";
    this.AddObjTypesButton.Tag = (object) "1";
    componentResourceManager.ApplyResources((object) this.DeleteObjTypesButton, "DeleteObjTypesButton");
    this.DeleteObjTypesButton.Name = "DeleteObjTypesButton";
    this.cmdsIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("cmdsIL.ImageStream");
    this.cmdsIL.TransparentColor = Color.Fuchsia;
    this.cmdsIL.Images.SetKeyName(0, "add.ico");
    this.cmdsIL.Images.SetKeyName(1, "del.ico");
    this.cmdsIL.Images.SetKeyName(2, "answer.ico");
    componentResourceManager.ApplyResources((object) this.DTypesRB2, "DTypesRB2");
    this.DTypesRB2.Name = "DTypesRB2";
    this.DTypesRB2.CheckedChanged += new EventHandler(this.DTypesRB_CheckedChanged);
    this.DTypesRB2.Click += new EventHandler(this.DTypesRB2_Click);
    componentResourceManager.ApplyResources((object) this.DTypesRB1, "DTypesRB1");
    this.DTypesRB1.Checked = true;
    this.DTypesRB1.Name = "DTypesRB1";
    this.DTypesRB1.TabStop = true;
    this.DTypesRB1.CheckedChanged += new EventHandler(this.DTypesRB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.WhatToSignCombo, "WhatToSignCombo");
    this.WhatToSignCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.WhatToSignCombo.Items.AddRange(new object[3]
    {
      (object) componentResourceManager.GetString("WhatToSignCombo.Items"),
      (object) componentResourceManager.GetString("WhatToSignCombo.Items1"),
      (object) componentResourceManager.GetString("WhatToSignCombo.Items2")
    });
    this.WhatToSignCombo.Name = "WhatToSignCombo";
    componentResourceManager.ApplyResources((object) this.ApproveCheckSpacer, "ApproveCheckSpacer");
    this.ApproveCheckSpacer.Name = "ApproveCheckSpacer";
    componentResourceManager.ApplyResources((object) this.ApproveVSpacer, "ApproveVSpacer");
    this.ApproveVSpacer.Name = "ApproveVSpacer";
    componentResourceManager.ApplyResources((object) this.signSplitter, "signSplitter");
    this.signSplitter.Name = "signSplitter";
    this.signSplitter.TabStop = false;
    this.ApproveCheckGB.Controls.Add((Control) this.UseAlienHint);
    this.ApproveCheckGB.Controls.Add((Control) this.ApproveCheckHint);
    this.ApproveCheckGB.Controls.Add((Control) this.ChooseApprovesButton);
    this.ApproveCheckGB.Controls.Add((Control) this.ApprovesBox);
    this.ApproveCheckGB.Controls.Add((Control) this.UseAlienSettingsCheckBox);
    this.ApproveCheckGB.Controls.Add((Control) this.ApproveCheckOnlyCheckBox);
    componentResourceManager.ApplyResources((object) this.ApproveCheckGB, "ApproveCheckGB");
    this.ApproveCheckGB.Name = "ApproveCheckGB";
    this.ApproveCheckGB.TabStop = false;
    componentResourceManager.ApplyResources((object) this.ChooseApprovesButton, "ChooseApprovesButton");
    this.ChooseApprovesButton.Name = "ChooseApprovesButton";
    this.ChooseApprovesButton.UseVisualStyleBackColor = true;
    this.ChooseApprovesButton.Click += new EventHandler(this.ChooseApprovesButton_Click);
    componentResourceManager.ApplyResources((object) this.ApprovesBox, "ApprovesBox");
    this.ApprovesBox.Name = "ApprovesBox";
    componentResourceManager.ApplyResources((object) this.UseAlienSettingsCheckBox, "UseAlienSettingsCheckBox");
    this.UseAlienSettingsCheckBox.Name = "UseAlienSettingsCheckBox";
    this.UseAlienSettingsCheckBox.UseVisualStyleBackColor = true;
    this.UseAlienSettingsCheckBox.CheckedChanged += new EventHandler(this.UseAlienSettingsCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.ApproveCheckOnlyCheckBox, "ApproveCheckOnlyCheckBox");
    this.ApproveCheckOnlyCheckBox.BackColor = SystemColors.Window;
    this.ApproveCheckOnlyCheckBox.Name = "ApproveCheckOnlyCheckBox";
    this.ApproveCheckOnlyCheckBox.UseVisualStyleBackColor = false;
    this.ApproveCheckOnlyCheckBox.CheckedChanged += new EventHandler(this.ApproveCheckOnlyCheckBox_CheckedChanged);
    this.RanksPanel.BackColor = Color.Transparent;
    this.RanksPanel.Controls.Add((Control) this.SignGraphView);
    this.RanksPanel.Controls.Add((Control) this.panel8);
    this.RanksPanel.Controls.Add((Control) this.SignAsGraphRadioButton);
    this.RanksPanel.Controls.Add((Control) this.SignAsUserRadioButton);
    this.RanksPanel.Controls.Add((Control) this.PersonalSignsCheckBox);
    componentResourceManager.ApplyResources((object) this.RanksPanel, "RanksPanel");
    this.RanksPanel.Name = "RanksPanel";
    this.RanksPanel.TabStop = false;
    this.SignGraphView.AllowManualSorting = true;
    componentResourceManager.ApplyResources((object) this.SignGraphView, "SignGraphView");
    this.SignGraphView.Columns.AddRange(new ColumnHeader[3]
    {
      this.columnHeader3,
      this.columnHeader4,
      this.columnHeader5
    });
    this.SignGraphView.FullRowSelect = true;
    this.SignGraphView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.SignGraphView.HideSelection = false;
    this.SignGraphView.MultiSelect = false;
    this.SignGraphView.Name = "SignGraphView";
    this.SignGraphView.OwnerDraw = true;
    this.SignGraphView.RadioGroups = false;
    this.SignGraphView.SortColumn = 0;
    this.SignGraphView.SubitemImages = (ImageList) null;
    this.SignGraphView.UseCompatibleStateImageBehavior = false;
    this.SignGraphView.View = View.Details;
    this.SignGraphView.SelectedIndexChanged += new EventHandler(this.SignGraphView_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader3, "columnHeader3");
    componentResourceManager.ApplyResources((object) this.columnHeader4, "columnHeader4");
    componentResourceManager.ApplyResources((object) this.columnHeader5, "columnHeader5");
    componentResourceManager.ApplyResources((object) this.panel8, "panel8");
    this.panel8.Controls.Add((Control) this.GraphsBar);
    this.panel8.Name = "panel8";
    componentResourceManager.ApplyResources((object) this.GraphsBar, "GraphsBar");
    this.GraphsBar.Buttons.AddRange(new ToolBarButton[2]
    {
      this.toolBarButton3,
      this.DeleteSignsButton
    });
    this.GraphsBar.Divider = false;
    this.GraphsBar.ImageList = this.cmdsIL;
    this.GraphsBar.Name = "GraphsBar";
    this.GraphsBar.ButtonClick += new ToolBarButtonClickEventHandler(this.GraphsBar_ButtonClick);
    componentResourceManager.ApplyResources((object) this.toolBarButton3, "toolBarButton3");
    this.toolBarButton3.Name = "toolBarButton3";
    this.toolBarButton3.Tag = (object) "1";
    componentResourceManager.ApplyResources((object) this.DeleteSignsButton, "DeleteSignsButton");
    this.DeleteSignsButton.Name = "DeleteSignsButton";
    this.DeleteSignsButton.Tag = (object) "2";
    componentResourceManager.ApplyResources((object) this.SignAsGraphRadioButton, "SignAsGraphRadioButton");
    this.SignAsGraphRadioButton.Name = "SignAsGraphRadioButton";
    this.SignAsGraphRadioButton.Tag = (object) "1";
    this.SignAsGraphRadioButton.CheckedChanged += new EventHandler(this.SignAsUserRadioButton_CheckedChanged);
    this.SignAsGraphRadioButton.Click += new EventHandler(this.SignAsGraphRadioButton_Click);
    componentResourceManager.ApplyResources((object) this.SignAsUserRadioButton, "SignAsUserRadioButton");
    this.SignAsUserRadioButton.Checked = true;
    this.SignAsUserRadioButton.Name = "SignAsUserRadioButton";
    this.SignAsUserRadioButton.TabStop = true;
    this.SignAsUserRadioButton.CheckedChanged += new EventHandler(this.SignAsUserRadioButton_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.PersonalSignsCheckBox, "PersonalSignsCheckBox");
    this.PersonalSignsCheckBox.Name = "PersonalSignsCheckBox";
    this.PersonalSignsCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.graphForTypeCheckBox, "graphForTypeCheckBox");
    this.graphForTypeCheckBox.Name = "graphForTypeCheckBox";
    this.graphForTypeCheckBox.UseVisualStyleBackColor = true;
    this.graphForTypeCheckBox.CheckedChanged += new EventHandler(this.graphForTypeCheckBox_CheckedChanged);
    this.ArchiveTabSheet.Controls.Add((Control) this.DetachRegisteredCheckBox);
    this.ArchiveTabSheet.Controls.Add((Control) this.RegRevGroupBox);
    this.ArchiveTabSheet.Controls.Add((Control) this.RegRevSpacer);
    this.ArchiveTabSheet.Controls.Add((Control) this.RegDocsGroupBox);
    this.ArchiveTabSheet.Controls.Add((Control) this.panel11);
    this.ArchiveTabSheet.Controls.Add((Control) this.RegisterGroupBox);
    componentResourceManager.ApplyResources((object) this.ArchiveTabSheet, "ArchiveTabSheet");
    this.ArchiveTabSheet.Name = "ArchiveTabSheet";
    this.ArchiveTabSheet.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.DetachRegisteredCheckBox, "DetachRegisteredCheckBox");
    this.DetachRegisteredCheckBox.Name = "DetachRegisteredCheckBox";
    this.DetachRegisteredCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.RegRevGroupBox, "RegRevGroupBox");
    this.RegRevGroupBox.Controls.Add((Control) this.RevArcIDEdit);
    this.RegRevGroupBox.Controls.Add((Control) this.RevArcVarCombo);
    this.RegRevGroupBox.Controls.Add((Control) this.RevSpacerPanel);
    this.RegRevGroupBox.Controls.Add((Control) this.RevArchiveVarRButton);
    this.RegRevGroupBox.Controls.Add((Control) this.RevArchiveRButton);
    this.RegRevGroupBox.Controls.Add((Control) this.RecArchiveCurrentRButton);
    this.RegRevGroupBox.Name = "RegRevGroupBox";
    this.RegRevGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.RevArcIDEdit, "RevArcIDEdit");
    this.RevArcIDEdit.Name = "RevArcIDEdit";
    this.RevArcIDEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "", 15, true, true, false, HorzAlignment.Center, (Image) null)
    });
    this.RevArcIDEdit.Properties.ReadOnly = true;
    this.RevArcIDEdit.ButtonPressed += new ButtonPressedEventHandler(this.DocArcIDEdit_ButtonPressed);
    componentResourceManager.ApplyResources((object) this.RevSpacerPanel, "RevSpacerPanel");
    this.RevSpacerPanel.Name = "RevSpacerPanel";
    componentResourceManager.ApplyResources((object) this.RevArchiveVarRButton, "RevArchiveVarRButton");
    this.RevArchiveVarRButton.Name = "RevArchiveVarRButton";
    this.RevArchiveVarRButton.Tag = (object) "2";
    this.RevArchiveVarRButton.CheckedChanged += new EventHandler(this.RevArcRadioChanged);
    componentResourceManager.ApplyResources((object) this.RevArchiveRButton, "RevArchiveRButton");
    this.RevArchiveRButton.Name = "RevArchiveRButton";
    this.RevArchiveRButton.Tag = (object) "1";
    this.RevArchiveRButton.CheckedChanged += new EventHandler(this.RevArcRadioChanged);
    componentResourceManager.ApplyResources((object) this.RecArchiveCurrentRButton, "RecArchiveCurrentRButton");
    this.RecArchiveCurrentRButton.Name = "RecArchiveCurrentRButton";
    this.RecArchiveCurrentRButton.Tag = (object) "0";
    this.RecArchiveCurrentRButton.CheckedChanged += new EventHandler(this.RevArcRadioChanged);
    componentResourceManager.ApplyResources((object) this.RegRevSpacer, "RegRevSpacer");
    this.RegRevSpacer.Name = "RegRevSpacer";
    componentResourceManager.ApplyResources((object) this.RegDocsGroupBox, "RegDocsGroupBox");
    this.RegDocsGroupBox.Controls.Add((Control) this.DocArcIDEdit);
    this.RegDocsGroupBox.Controls.Add((Control) this.DocArcVarCombo);
    this.RegDocsGroupBox.Controls.Add((Control) this.DocSpacerPanel);
    this.RegDocsGroupBox.Controls.Add((Control) this.DocArchiveVarRButton);
    this.RegDocsGroupBox.Controls.Add((Control) this.DocArchiveRButton);
    this.RegDocsGroupBox.Controls.Add((Control) this.DocArchiveCurrentRButton);
    this.RegDocsGroupBox.Name = "RegDocsGroupBox";
    this.RegDocsGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.DocArcIDEdit, "DocArcIDEdit");
    this.DocArcIDEdit.Name = "DocArcIDEdit";
    this.DocArcIDEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "", 15, true, true, false, HorzAlignment.Center, (Image) null)
    });
    this.DocArcIDEdit.Properties.ReadOnly = true;
    this.DocArcIDEdit.ButtonPressed += new ButtonPressedEventHandler(this.DocArcIDEdit_ButtonPressed);
    componentResourceManager.ApplyResources((object) this.DocSpacerPanel, "DocSpacerPanel");
    this.DocSpacerPanel.Name = "DocSpacerPanel";
    componentResourceManager.ApplyResources((object) this.DocArchiveVarRButton, "DocArchiveVarRButton");
    this.DocArchiveVarRButton.Name = "DocArchiveVarRButton";
    this.DocArchiveVarRButton.Tag = (object) "2";
    this.DocArchiveVarRButton.CheckedChanged += new EventHandler(this.DocArcRadioChanged);
    componentResourceManager.ApplyResources((object) this.DocArchiveRButton, "DocArchiveRButton");
    this.DocArchiveRButton.Name = "DocArchiveRButton";
    this.DocArchiveRButton.Tag = (object) "1";
    this.DocArchiveRButton.CheckedChanged += new EventHandler(this.DocArcRadioChanged);
    componentResourceManager.ApplyResources((object) this.DocArchiveCurrentRButton, "DocArchiveCurrentRButton");
    this.DocArchiveCurrentRButton.Name = "DocArchiveCurrentRButton";
    this.DocArchiveCurrentRButton.Tag = (object) "0";
    this.DocArchiveCurrentRButton.CheckedChanged += new EventHandler(this.DocArcRadioChanged);
    componentResourceManager.ApplyResources((object) this.panel11, "panel11");
    this.panel11.Name = "panel11";
    this.RegisterGroupBox.Controls.Add((Control) this.ArcModeRadio2);
    this.RegisterGroupBox.Controls.Add((Control) this.ArcModeRadio1);
    componentResourceManager.ApplyResources((object) this.RegisterGroupBox, "RegisterGroupBox");
    this.RegisterGroupBox.Name = "RegisterGroupBox";
    this.RegisterGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.ArcModeRadio2, "ArcModeRadio2");
    this.ArcModeRadio2.Name = "ArcModeRadio2";
    this.ArcModeRadio2.Tag = (object) "2";
    this.ArcModeRadio2.CheckedChanged += new EventHandler(this.ArcModeRadioCheckedChanged);
    this.ArcModeRadio1.Checked = true;
    componentResourceManager.ApplyResources((object) this.ArcModeRadio1, "ArcModeRadio1");
    this.ArcModeRadio1.Name = "ArcModeRadio1";
    this.ArcModeRadio1.TabStop = true;
    this.ArcModeRadio1.Tag = (object) "1";
    this.ArcModeRadio1.CheckedChanged += new EventHandler(this.ArcModeRadioCheckedChanged);
    this.SubProcessTabSheet.Controls.Add((Control) this.GroupBox5);
    componentResourceManager.ApplyResources((object) this.SubProcessTabSheet, "SubProcessTabSheet");
    this.SubProcessTabSheet.Name = "SubProcessTabSheet";
    this.SubProcessTabSheet.UseVisualStyleBackColor = true;
    this.GroupBox5.Controls.Add((Control) this.useActualVersionSchemeCheckBox);
    this.GroupBox5.Controls.Add((Control) this.SchemeEdit);
    this.GroupBox5.Controls.Add((Control) this.Label3);
    this.GroupBox5.Controls.Add((Control) this.SubNameLabel);
    this.GroupBox5.Controls.Add((Control) this.WaitCheckBox);
    this.GroupBox5.Controls.Add((Control) this.SubNameEdit);
    componentResourceManager.ApplyResources((object) this.GroupBox5, "GroupBox5");
    this.GroupBox5.Name = "GroupBox5";
    this.GroupBox5.TabStop = false;
    componentResourceManager.ApplyResources((object) this.useActualVersionSchemeCheckBox, "useActualVersionSchemeCheckBox");
    this.useActualVersionSchemeCheckBox.Checked = true;
    this.useActualVersionSchemeCheckBox.CheckState = CheckState.Checked;
    this.useActualVersionSchemeCheckBox.Name = "useActualVersionSchemeCheckBox";
    this.useActualVersionSchemeCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.SchemeEdit, "SchemeEdit");
    this.SchemeEdit.Name = "SchemeEdit";
    this.SchemeEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("SchemeEdit.Properties.Buttons"))
    });
    this.SchemeEdit.Properties.ReadOnly = true;
    this.SchemeEdit.ButtonClick += new ButtonPressedEventHandler(this.SchemeEdit_ButtonClick);
    componentResourceManager.ApplyResources((object) this.Label3, "Label3");
    this.Label3.Name = "Label3";
    componentResourceManager.ApplyResources((object) this.SubNameLabel, "SubNameLabel");
    this.SubNameLabel.Name = "SubNameLabel";
    componentResourceManager.ApplyResources((object) this.WaitCheckBox, "WaitCheckBox");
    this.WaitCheckBox.Name = "WaitCheckBox";
    componentResourceManager.ApplyResources((object) this.SubNameEdit, "SubNameEdit");
    this.SubNameEdit.Name = "SubNameEdit";
    this.SubNameEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "?", 16 /*0x10*/, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("SubNameEdit.Properties.Buttons"))
    });
    this.SubNameEdit.ButtonClick += new ButtonPressedEventHandler(this.SubNameEdit_ButtonClick);
    this.RemoteSubProcessTabSheet.Controls.Add((Control) this.RemoteProcess2GroupBox);
    this.RemoteSubProcessTabSheet.Controls.Add((Control) this.panel9);
    this.RemoteSubProcessTabSheet.Controls.Add((Control) this.RemoteProcessGroupBox);
    this.RemoteSubProcessTabSheet.Controls.Add((Control) this.PortalErrPanel);
    componentResourceManager.ApplyResources((object) this.RemoteSubProcessTabSheet, "RemoteSubProcessTabSheet");
    this.RemoteSubProcessTabSheet.Name = "RemoteSubProcessTabSheet";
    this.RemoteSubProcessTabSheet.UseVisualStyleBackColor = true;
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.loadPublishOptions);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.savePublishOptions);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.CreateReceiptCheckBox);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.label13);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.PubCompositionCombo);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.PubRelTypesButton);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.GiveOwnershipCheckBox);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.PubObjectTypesButton);
    componentResourceManager.ApplyResources((object) this.RemoteProcess2GroupBox, "RemoteProcess2GroupBox");
    this.RemoteProcess2GroupBox.Name = "RemoteProcess2GroupBox";
    this.RemoteProcess2GroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.CreateReceiptCheckBox, "CreateReceiptCheckBox");
    this.CreateReceiptCheckBox.Name = "CreateReceiptCheckBox";
    componentResourceManager.ApplyResources((object) this.label13, "label13");
    this.label13.Name = "label13";
    componentResourceManager.ApplyResources((object) this.PubCompositionCombo, "PubCompositionCombo");
    this.PubCompositionCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.PubCompositionCombo.FormattingEnabled = true;
    this.PubCompositionCombo.Items.AddRange(new object[3]
    {
      (object) componentResourceManager.GetString("PubCompositionCombo.Items"),
      (object) componentResourceManager.GetString("PubCompositionCombo.Items1"),
      (object) componentResourceManager.GetString("PubCompositionCombo.Items2")
    });
    this.PubCompositionCombo.Name = "PubCompositionCombo";
    componentResourceManager.ApplyResources((object) this.PubRelTypesButton, "PubRelTypesButton");
    this.PubRelTypesButton.Name = "PubRelTypesButton";
    this.PubRelTypesButton.UseVisualStyleBackColor = true;
    this.PubRelTypesButton.Click += new EventHandler(this.PubRelTypesButton_Click);
    componentResourceManager.ApplyResources((object) this.GiveOwnershipCheckBox, "GiveOwnershipCheckBox");
    this.GiveOwnershipCheckBox.Name = "GiveOwnershipCheckBox";
    componentResourceManager.ApplyResources((object) this.PubObjectTypesButton, "PubObjectTypesButton");
    this.PubObjectTypesButton.Name = "PubObjectTypesButton";
    this.PubObjectTypesButton.UseVisualStyleBackColor = true;
    this.PubObjectTypesButton.Click += new EventHandler(this.PubObjectTypesButton_Click);
    componentResourceManager.ApplyResources((object) this.panel9, "panel9");
    this.panel9.Name = "panel9";
    this.RemoteProcessGroupBox.Controls.Add((Control) this.RemoteSchemesComboBox);
    this.RemoteProcessGroupBox.Controls.Add((Control) this.SitesComboBox);
    this.RemoteProcessGroupBox.Controls.Add((Control) this.label12);
    this.RemoteProcessGroupBox.Controls.Add((Control) this.label11);
    this.RemoteProcessGroupBox.Controls.Add((Control) this.RemoteWaitCheckBox);
    componentResourceManager.ApplyResources((object) this.RemoteProcessGroupBox, "RemoteProcessGroupBox");
    this.RemoteProcessGroupBox.Name = "RemoteProcessGroupBox";
    this.RemoteProcessGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.RemoteSchemesComboBox, "RemoteSchemesComboBox");
    this.RemoteSchemesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.RemoteSchemesComboBox.FormattingEnabled = true;
    this.RemoteSchemesComboBox.Name = "RemoteSchemesComboBox";
    componentResourceManager.ApplyResources((object) this.SitesComboBox, "SitesComboBox");
    this.SitesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.SitesComboBox.FormattingEnabled = true;
    this.SitesComboBox.Name = "SitesComboBox";
    componentResourceManager.ApplyResources((object) this.label12, "label12");
    this.label12.Name = "label12";
    componentResourceManager.ApplyResources((object) this.label11, "label11");
    this.label11.Name = "label11";
    componentResourceManager.ApplyResources((object) this.RemoteWaitCheckBox, "RemoteWaitCheckBox");
    this.RemoteWaitCheckBox.Name = "RemoteWaitCheckBox";
    this.PortalErrPanel.BackColor = SystemColors.Info;
    this.PortalErrPanel.Controls.Add((Control) this.PortalErrLabel);
    this.PortalErrPanel.Controls.Add((Control) this.PortalErrImage);
    componentResourceManager.ApplyResources((object) this.PortalErrPanel, "PortalErrPanel");
    this.PortalErrPanel.Name = "PortalErrPanel";
    componentResourceManager.ApplyResources((object) this.PortalErrLabel, "PortalErrLabel");
    this.PortalErrLabel.Name = "PortalErrLabel";
    this.PortalErrImage.BackColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.PortalErrImage, "PortalErrImage");
    this.PortalErrImage.Name = "PortalErrImage";
    this.PortalErrImage.TabStop = false;
    this.TermsTabSheet.Controls.Add((Control) this.TermOptsGroupBox);
    this.TermsTabSheet.Controls.Add((Control) this.panel6);
    this.TermsTabSheet.Controls.Add((Control) this.GroupBox10);
    this.TermsTabSheet.Controls.Add((Control) this.panel5);
    this.TermsTabSheet.Controls.Add((Control) this.GroupBox8);
    componentResourceManager.ApplyResources((object) this.TermsTabSheet, "TermsTabSheet");
    this.TermsTabSheet.Name = "TermsTabSheet";
    this.TermsTabSheet.UseVisualStyleBackColor = true;
    this.TermOptsGroupBox.Controls.Add((Control) this.TermStartRB2);
    this.TermOptsGroupBox.Controls.Add((Control) this.TermStartRB1);
    componentResourceManager.ApplyResources((object) this.TermOptsGroupBox, "TermOptsGroupBox");
    this.TermOptsGroupBox.Name = "TermOptsGroupBox";
    this.TermOptsGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.TermStartRB2, "TermStartRB2");
    this.TermStartRB2.Name = "TermStartRB2";
    this.TermStartRB2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.TermStartRB1, "TermStartRB1");
    this.TermStartRB1.Name = "TermStartRB1";
    this.TermStartRB1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.panel6, "panel6");
    this.panel6.Name = "panel6";
    this.GroupBox10.Controls.Add((Control) this.unreadTermEdit);
    this.GroupBox10.Controls.Add((Control) this.UnreadRollbackCheckBox);
    componentResourceManager.ApplyResources((object) this.GroupBox10, "GroupBox10");
    this.GroupBox10.Name = "GroupBox10";
    this.GroupBox10.TabStop = false;
    componentResourceManager.ApplyResources((object) this.unreadTermEdit, "unreadTermEdit");
    this.unreadTermEdit.Name = "unreadTermEdit";
    this.unreadTermEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((object) "", ButtonPredefines.Ellipsis)
    });
    this.unreadTermEdit.Properties.ReadOnly = true;
    this.unreadTermEdit.Tag = (object) "1";
    this.unreadTermEdit.ButtonClick += new ButtonPressedEventHandler(this.termEdit_ButtonClick);
    componentResourceManager.ApplyResources((object) this.UnreadRollbackCheckBox, "UnreadRollbackCheckBox");
    this.UnreadRollbackCheckBox.Name = "UnreadRollbackCheckBox";
    this.UnreadRollbackCheckBox.CheckedChanged += new EventHandler(this.UnreadRollbackCheckBox_CheckedChanged);
    this.UnreadRollbackCheckBox.Click += new EventHandler(this.UnreadRollbackCheckBox_Click);
    componentResourceManager.ApplyResources((object) this.panel5, "panel5");
    this.panel5.Name = "panel5";
    this.GroupBox8.Controls.Add((Control) this.termEdit);
    this.GroupBox8.Controls.Add((Control) this.UncompleteRollbackCheckBox);
    componentResourceManager.ApplyResources((object) this.GroupBox8, "GroupBox8");
    this.GroupBox8.Name = "GroupBox8";
    this.GroupBox8.TabStop = false;
    componentResourceManager.ApplyResources((object) this.termEdit, "termEdit");
    this.termEdit.Name = "termEdit";
    this.termEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((object) "", ButtonPredefines.Ellipsis)
    });
    this.termEdit.Properties.ReadOnly = true;
    this.termEdit.Tag = (object) "0";
    this.termEdit.ButtonClick += new ButtonPressedEventHandler(this.termEdit_ButtonClick);
    componentResourceManager.ApplyResources((object) this.UncompleteRollbackCheckBox, "UncompleteRollbackCheckBox");
    this.UncompleteRollbackCheckBox.Name = "UncompleteRollbackCheckBox";
    this.UncompleteRollbackCheckBox.CheckedChanged += new EventHandler(this.UncompleteRollbackCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.TimerTabSheet, "TimerTabSheet");
    this.TimerTabSheet.Name = "TimerTabSheet";
    this.TimerTabSheet.UseVisualStyleBackColor = true;
    this.RollbackTabSheet.Controls.Add((Control) this.RollbackGroupBox);
    componentResourceManager.ApplyResources((object) this.RollbackTabSheet, "RollbackTabSheet");
    this.RollbackTabSheet.Name = "RollbackTabSheet";
    this.RollbackTabSheet.UseVisualStyleBackColor = true;
    this.RollbackGroupBox.Controls.Add((Control) this.RollRadioButton5);
    this.RollbackGroupBox.Controls.Add((Control) this.RollRadioButton1);
    this.RollbackGroupBox.Controls.Add((Control) this.RollRadioButton2);
    this.RollbackGroupBox.Controls.Add((Control) this.RollRadioButton3);
    this.RollbackGroupBox.Controls.Add((Control) this.RollRadioButton4);
    componentResourceManager.ApplyResources((object) this.RollbackGroupBox, "RollbackGroupBox");
    this.RollbackGroupBox.Name = "RollbackGroupBox";
    this.RollbackGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.RollRadioButton5, "RollRadioButton5");
    this.RollRadioButton5.Name = "RollRadioButton5";
    this.RollRadioButton5.Tag = (object) "4";
    this.RollRadioButton1.Checked = true;
    componentResourceManager.ApplyResources((object) this.RollRadioButton1, "RollRadioButton1");
    this.RollRadioButton1.Name = "RollRadioButton1";
    this.RollRadioButton1.TabStop = true;
    componentResourceManager.ApplyResources((object) this.RollRadioButton2, "RollRadioButton2");
    this.RollRadioButton2.Name = "RollRadioButton2";
    this.RollRadioButton2.Tag = (object) "1";
    componentResourceManager.ApplyResources((object) this.RollRadioButton3, "RollRadioButton3");
    this.RollRadioButton3.Name = "RollRadioButton3";
    this.RollRadioButton3.Tag = (object) "2";
    componentResourceManager.ApplyResources((object) this.RollRadioButton4, "RollRadioButton4");
    this.RollRadioButton4.Name = "RollRadioButton4";
    this.RollRadioButton4.Tag = (object) "3";
    this.LCTabSheet.Controls.Add((Control) this.LCGroupBox);
    componentResourceManager.ApplyResources((object) this.LCTabSheet, "LCTabSheet");
    this.LCTabSheet.Name = "LCTabSheet";
    this.LCTabSheet.UseVisualStyleBackColor = true;
    this.LCGroupBox.Controls.Add((Control) this.LCPanel2);
    this.LCGroupBox.Controls.Add((Control) this.LCLabel2);
    this.LCGroupBox.Controls.Add((Control) this.LCPanel1);
    this.LCGroupBox.Controls.Add((Control) this.LCLabel1);
    componentResourceManager.ApplyResources((object) this.LCGroupBox, "LCGroupBox");
    this.LCGroupBox.Name = "LCGroupBox";
    this.LCGroupBox.TabStop = false;
    this.LCGroupBox.Resize += new EventHandler(this.LCGroupBox_Resize);
    this.LCPanel2.BorderStyle = BorderStyle.Fixed3D;
    this.LCPanel2.Controls.Add((Control) this.LCView2);
    this.LCPanel2.Controls.Add((Control) this.label15);
    this.LCPanel2.Controls.Add((Control) this.LCStepToolBar2);
    componentResourceManager.ApplyResources((object) this.LCPanel2, "LCPanel2");
    this.LCPanel2.Name = "LCPanel2";
    this.LCView2.AllowManualSorting = true;
    this.LCView2.BorderStyle = BorderStyle.None;
    this.LCView2.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader9,
      this.columnHeader10
    });
    componentResourceManager.ApplyResources((object) this.LCView2, "LCView2");
    this.LCView2.FullRowSelect = true;
    this.LCView2.HideSelection = false;
    this.LCView2.MultiSelect = false;
    this.LCView2.Name = "LCView2";
    this.LCView2.OwnerDraw = true;
    this.LCView2.RadioGroups = false;
    this.LCView2.SortColumn = 0;
    this.LCView2.SubitemImages = (ImageList) null;
    this.LCView2.UseCompatibleStateImageBehavior = false;
    this.LCView2.View = View.Details;
    this.LCView2.DoubleClick += new EventHandler(this.LCView_DoubleClick);
    componentResourceManager.ApplyResources((object) this.columnHeader9, "columnHeader9");
    componentResourceManager.ApplyResources((object) this.columnHeader10, "columnHeader10");
    this.label15.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this.label15, "label15");
    this.label15.Name = "label15";
    componentResourceManager.ApplyResources((object) this.LCStepToolBar2, "LCStepToolBar2");
    this.LCStepToolBar2.Buttons.AddRange(new ToolBarButton[3]
    {
      this.toolBarButton5,
      this.toolBarButton6,
      this.toolBarButton7
    });
    this.LCStepToolBar2.Divider = false;
    this.LCStepToolBar2.ImageList = this.cmdsIL;
    this.LCStepToolBar2.Name = "LCStepToolBar2";
    this.LCStepToolBar2.ButtonClick += new ToolBarButtonClickEventHandler(this.LCStepToolBar_ButtonClick);
    componentResourceManager.ApplyResources((object) this.toolBarButton5, "toolBarButton5");
    this.toolBarButton5.Name = "toolBarButton5";
    this.toolBarButton5.Tag = (object) "1";
    componentResourceManager.ApplyResources((object) this.toolBarButton6, "toolBarButton6");
    this.toolBarButton6.Name = "toolBarButton6";
    this.toolBarButton6.Tag = (object) "3";
    componentResourceManager.ApplyResources((object) this.toolBarButton7, "toolBarButton7");
    this.toolBarButton7.Name = "toolBarButton7";
    this.toolBarButton7.Tag = (object) "2";
    componentResourceManager.ApplyResources((object) this.LCLabel2, "LCLabel2");
    this.LCLabel2.Name = "LCLabel2";
    this.LCPanel1.BorderStyle = BorderStyle.Fixed3D;
    this.LCPanel1.Controls.Add((Control) this.LCView);
    this.LCPanel1.Controls.Add((Control) this.label9);
    this.LCPanel1.Controls.Add((Control) this.LCStepToolBar);
    componentResourceManager.ApplyResources((object) this.LCPanel1, "LCPanel1");
    this.LCPanel1.Name = "LCPanel1";
    this.LCView.AllowManualSorting = true;
    this.LCView.BorderStyle = BorderStyle.None;
    this.LCView.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader7,
      this.columnHeader8
    });
    componentResourceManager.ApplyResources((object) this.LCView, "LCView");
    this.LCView.FullRowSelect = true;
    this.LCView.HideSelection = false;
    this.LCView.MultiSelect = false;
    this.LCView.Name = "LCView";
    this.LCView.OwnerDraw = true;
    this.LCView.RadioGroups = false;
    this.LCView.SortColumn = 0;
    this.LCView.SubitemImages = (ImageList) null;
    this.LCView.UseCompatibleStateImageBehavior = false;
    this.LCView.View = View.Details;
    this.LCView.DoubleClick += new EventHandler(this.LCView_DoubleClick);
    componentResourceManager.ApplyResources((object) this.columnHeader7, "columnHeader7");
    componentResourceManager.ApplyResources((object) this.columnHeader8, "columnHeader8");
    this.label9.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this.label9, "label9");
    this.label9.Name = "label9";
    componentResourceManager.ApplyResources((object) this.LCStepToolBar, "LCStepToolBar");
    this.LCStepToolBar.Buttons.AddRange(new ToolBarButton[3]
    {
      this.toolBarButton1,
      this.toolBarButton4,
      this.toolBarButton2
    });
    this.LCStepToolBar.Divider = false;
    this.LCStepToolBar.ImageList = this.cmdsIL;
    this.LCStepToolBar.Name = "LCStepToolBar";
    this.LCStepToolBar.ButtonClick += new ToolBarButtonClickEventHandler(this.LCStepToolBar_ButtonClick);
    componentResourceManager.ApplyResources((object) this.toolBarButton1, "toolBarButton1");
    this.toolBarButton1.Name = "toolBarButton1";
    this.toolBarButton1.Tag = (object) "1";
    componentResourceManager.ApplyResources((object) this.toolBarButton4, "toolBarButton4");
    this.toolBarButton4.Name = "toolBarButton4";
    this.toolBarButton4.Tag = (object) "3";
    componentResourceManager.ApplyResources((object) this.toolBarButton2, "toolBarButton2");
    this.toolBarButton2.Name = "toolBarButton2";
    this.toolBarButton2.Tag = (object) "2";
    componentResourceManager.ApplyResources((object) this.LCLabel1, "LCLabel1");
    this.LCLabel1.Name = "LCLabel1";
    this.NotifTabSheet.Controls.Add((Control) this.MsgsGroupBox);
    componentResourceManager.ApplyResources((object) this.NotifTabSheet, "NotifTabSheet");
    this.NotifTabSheet.Name = "NotifTabSheet";
    this.NotifTabSheet.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.MsgsGroupBox, "MsgsGroupBox");
    this.MsgsGroupBox.Controls.Add((Control) this.MsgAbortPanel);
    this.MsgsGroupBox.Controls.Add((Control) this.MsgStopPanel);
    this.MsgsGroupBox.Controls.Add((Control) this.MsgBackPanel);
    this.MsgsGroupBox.Controls.Add((Control) this.MsgPeriodPanel);
    this.MsgsGroupBox.Controls.Add((Control) this.MsgReadPanel);
    this.MsgsGroupBox.Controls.Add((Control) this.MsgStartPanel);
    this.MsgsGroupBox.Name = "MsgsGroupBox";
    this.MsgsGroupBox.TabStop = false;
    this.MsgAbortPanel.Controls.Add((Control) this.MsgAbortCheckBox);
    this.MsgAbortPanel.Controls.Add((Control) this.MsgAbortButton);
    componentResourceManager.ApplyResources((object) this.MsgAbortPanel, "MsgAbortPanel");
    this.MsgAbortPanel.Name = "MsgAbortPanel";
    componentResourceManager.ApplyResources((object) this.MsgAbortCheckBox, "MsgAbortCheckBox");
    this.MsgAbortCheckBox.Name = "MsgAbortCheckBox";
    this.MsgAbortCheckBox.Tag = (object) "";
    this.MsgAbortCheckBox.CheckedChanged += new EventHandler(this.MsgStartCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.MsgAbortButton, "MsgAbortButton");
    this.MsgAbortButton.ImageList = this.TabsIL;
    this.MsgAbortButton.Name = "MsgAbortButton";
    this.MsgAbortButton.Tag = (object) "4";
    this.MsgAbortButton.Click += new EventHandler(this.EditMessageClick);
    this.MsgPeriodPanel.Controls.Add((Control) this.MsgPeriodCheckBox);
    this.MsgPeriodPanel.Controls.Add((Control) this.MsgPeriodButton);
    componentResourceManager.ApplyResources((object) this.MsgPeriodPanel, "MsgPeriodPanel");
    this.MsgPeriodPanel.Name = "MsgPeriodPanel";
    componentResourceManager.ApplyResources((object) this.MsgPeriodCheckBox, "MsgPeriodCheckBox");
    this.MsgPeriodCheckBox.Name = "MsgPeriodCheckBox";
    this.MsgPeriodCheckBox.Tag = (object) "";
    this.MsgPeriodCheckBox.CheckedChanged += new EventHandler(this.MsgStartCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.MsgPeriodButton, "MsgPeriodButton");
    this.MsgPeriodButton.ImageList = this.TabsIL;
    this.MsgPeriodButton.Name = "MsgPeriodButton";
    this.MsgPeriodButton.Tag = (object) "2";
    this.MsgPeriodButton.Click += new EventHandler(this.EditMessageClick);
    this.MsgReadPanel.Controls.Add((Control) this.MsgReadCheckBox);
    this.MsgReadPanel.Controls.Add((Control) this.MsgReadButton);
    componentResourceManager.ApplyResources((object) this.MsgReadPanel, "MsgReadPanel");
    this.MsgReadPanel.Name = "MsgReadPanel";
    componentResourceManager.ApplyResources((object) this.MsgReadCheckBox, "MsgReadCheckBox");
    this.MsgReadCheckBox.Name = "MsgReadCheckBox";
    this.MsgReadCheckBox.Tag = (object) "";
    this.MsgReadCheckBox.CheckedChanged += new EventHandler(this.MsgStartCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.MsgReadButton, "MsgReadButton");
    this.MsgReadButton.ImageList = this.TabsIL;
    this.MsgReadButton.Name = "MsgReadButton";
    this.MsgReadButton.Tag = (object) "5";
    this.MsgReadButton.Click += new EventHandler(this.EditMessageClick);
    this.MsgStartPanel.Controls.Add((Control) this.MsgStartCheckBox);
    this.MsgStartPanel.Controls.Add((Control) this.MsgStartButton);
    componentResourceManager.ApplyResources((object) this.MsgStartPanel, "MsgStartPanel");
    this.MsgStartPanel.Name = "MsgStartPanel";
    componentResourceManager.ApplyResources((object) this.MsgStartCheckBox, "MsgStartCheckBox");
    this.MsgStartCheckBox.Name = "MsgStartCheckBox";
    this.MsgStartCheckBox.Tag = (object) "";
    this.MsgStartCheckBox.CheckedChanged += new EventHandler(this.MsgStartCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.MsgStartButton, "MsgStartButton");
    this.MsgStartButton.ImageList = this.TabsIL;
    this.MsgStartButton.Name = "MsgStartButton";
    this.MsgStartButton.Tag = (object) "1";
    this.MsgStartButton.Click += new EventHandler(this.EditMessageClick);
    this.ScriptTabSheet.Controls.Add((Control) this.AfterScriptGroupBox);
    this.ScriptTabSheet.Controls.Add((Control) this.panel13);
    this.ScriptTabSheet.Controls.Add((Control) this.BeforeScriptGroupBox);
    this.ScriptTabSheet.Controls.Add((Control) this.ScriptGroupBox);
    componentResourceManager.ApplyResources((object) this.ScriptTabSheet, "ScriptTabSheet");
    this.ScriptTabSheet.Name = "ScriptTabSheet";
    this.ScriptTabSheet.UseVisualStyleBackColor = true;
    this.AfterScriptGroupBox.Controls.Add((Control) this.afterScriptEdit);
    this.AfterScriptGroupBox.Controls.Add((Control) this.AfterScriptExecCombo);
    this.AfterScriptGroupBox.Controls.Add((Control) this.afterScriptType);
    this.AfterScriptGroupBox.Controls.Add((Control) this.label6);
    this.AfterScriptGroupBox.Controls.Add((Control) this.label17);
    this.AfterScriptGroupBox.Controls.Add((Control) this.AfterScriptCheckBox);
    componentResourceManager.ApplyResources((object) this.AfterScriptGroupBox, "AfterScriptGroupBox");
    this.AfterScriptGroupBox.Name = "AfterScriptGroupBox";
    this.AfterScriptGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.afterScriptEdit, "afterScriptEdit");
    this.afterScriptEdit.Name = "afterScriptEdit";
    this.afterScriptEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.afterScriptEdit.Properties.ReadOnly = true;
    this.afterScriptEdit.Tag = (object) "1";
    this.afterScriptEdit.ButtonClick += new ButtonPressedEventHandler(this.EditScriptButton_Click);
    componentResourceManager.ApplyResources((object) this.AfterScriptExecCombo, "AfterScriptExecCombo");
    this.AfterScriptExecCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.AfterScriptExecCombo.FormattingEnabled = true;
    this.AfterScriptExecCombo.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("AfterScriptExecCombo.Items"),
      (object) componentResourceManager.GetString("AfterScriptExecCombo.Items1")
    });
    this.AfterScriptExecCombo.Name = "AfterScriptExecCombo";
    this.afterScriptType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.afterScriptType.FormattingEnabled = true;
    this.afterScriptType.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("afterScriptType.Items"),
      (object) componentResourceManager.GetString("afterScriptType.Items1")
    });
    componentResourceManager.ApplyResources((object) this.afterScriptType, "afterScriptType");
    this.afterScriptType.Name = "afterScriptType";
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.label17, "label17");
    this.label17.Name = "label17";
    componentResourceManager.ApplyResources((object) this.AfterScriptCheckBox, "AfterScriptCheckBox");
    this.AfterScriptCheckBox.Name = "AfterScriptCheckBox";
    this.AfterScriptCheckBox.UseVisualStyleBackColor = true;
    this.AfterScriptCheckBox.CheckedChanged += new EventHandler(this.BeforeScriptCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.panel13, "panel13");
    this.panel13.Name = "panel13";
    this.BeforeScriptGroupBox.Controls.Add((Control) this.beforeScriptEdit);
    this.BeforeScriptGroupBox.Controls.Add((Control) this.BeforeScriptExecCombo);
    this.BeforeScriptGroupBox.Controls.Add((Control) this.beforeScriptType);
    this.BeforeScriptGroupBox.Controls.Add((Control) this.label4);
    this.BeforeScriptGroupBox.Controls.Add((Control) this.label16);
    this.BeforeScriptGroupBox.Controls.Add((Control) this.BeforeScriptCheckBox);
    componentResourceManager.ApplyResources((object) this.BeforeScriptGroupBox, "BeforeScriptGroupBox");
    this.BeforeScriptGroupBox.Name = "BeforeScriptGroupBox";
    this.BeforeScriptGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.beforeScriptEdit, "beforeScriptEdit");
    this.beforeScriptEdit.Name = "beforeScriptEdit";
    this.beforeScriptEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beforeScriptEdit.Properties.ReadOnly = true;
    this.beforeScriptEdit.Tag = (object) "0";
    this.beforeScriptEdit.ButtonClick += new ButtonPressedEventHandler(this.EditScriptButton_Click);
    componentResourceManager.ApplyResources((object) this.BeforeScriptExecCombo, "BeforeScriptExecCombo");
    this.BeforeScriptExecCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.BeforeScriptExecCombo.FormattingEnabled = true;
    this.BeforeScriptExecCombo.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("BeforeScriptExecCombo.Items"),
      (object) componentResourceManager.GetString("BeforeScriptExecCombo.Items1")
    });
    this.BeforeScriptExecCombo.Name = "BeforeScriptExecCombo";
    this.beforeScriptType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.beforeScriptType.FormattingEnabled = true;
    this.beforeScriptType.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("beforeScriptType.Items"),
      (object) componentResourceManager.GetString("beforeScriptType.Items1")
    });
    componentResourceManager.ApplyResources((object) this.beforeScriptType, "beforeScriptType");
    this.beforeScriptType.Name = "beforeScriptType";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.label16, "label16");
    this.label16.Name = "label16";
    componentResourceManager.ApplyResources((object) this.BeforeScriptCheckBox, "BeforeScriptCheckBox");
    this.BeforeScriptCheckBox.Name = "BeforeScriptCheckBox";
    this.BeforeScriptCheckBox.UseVisualStyleBackColor = true;
    this.BeforeScriptCheckBox.CheckedChanged += new EventHandler(this.BeforeScriptCheckBox_CheckedChanged);
    this.ScriptGroupBox.Controls.Add((Control) this.scriptEdit);
    this.ScriptGroupBox.Controls.Add((Control) this.ScriptUserGroupBox);
    this.ScriptGroupBox.Controls.Add((Control) this.scriptType);
    this.ScriptGroupBox.Controls.Add((Control) this.ScriptExecCombo);
    this.ScriptGroupBox.Controls.Add((Control) this.label14);
    this.ScriptGroupBox.Controls.Add((Control) this.label10);
    componentResourceManager.ApplyResources((object) this.ScriptGroupBox, "ScriptGroupBox");
    this.ScriptGroupBox.Name = "ScriptGroupBox";
    this.ScriptGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.scriptEdit, "scriptEdit");
    this.scriptEdit.Name = "scriptEdit";
    this.scriptEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.scriptEdit.Properties.ReadOnly = true;
    this.scriptEdit.Tag = (object) "0";
    this.scriptEdit.ButtonClick += new ButtonPressedEventHandler(this.EditScriptButton_Click);
    componentResourceManager.ApplyResources((object) this.ScriptUserGroupBox, "ScriptUserGroupBox");
    this.ScriptUserGroupBox.Controls.Add((Control) this.ScriptUserEdit);
    this.ScriptUserGroupBox.Name = "ScriptUserGroupBox";
    this.ScriptUserGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.ScriptUserEdit, "ScriptUserEdit");
    this.ScriptUserEdit.Name = "ScriptUserEdit";
    this.ScriptUserEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.ScriptUserEdit.Properties.ReadOnly = true;
    this.ScriptUserEdit.ButtonClick += new ButtonPressedEventHandler(this.ScriptUserEdit_ButtonClick);
    this.ScriptUserEdit.EditValueChanged += new EventHandler(this.ScriptUserEdit_EditValueChanged);
    this.scriptType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.scriptType.FormattingEnabled = true;
    this.scriptType.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("scriptType.Items"),
      (object) componentResourceManager.GetString("scriptType.Items1")
    });
    componentResourceManager.ApplyResources((object) this.scriptType, "scriptType");
    this.scriptType.Name = "scriptType";
    componentResourceManager.ApplyResources((object) this.ScriptExecCombo, "ScriptExecCombo");
    this.ScriptExecCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.ScriptExecCombo.FormattingEnabled = true;
    this.ScriptExecCombo.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("ScriptExecCombo.Items"),
      (object) componentResourceManager.GetString("ScriptExecCombo.Items1")
    });
    this.ScriptExecCombo.Name = "ScriptExecCombo";
    this.ScriptExecCombo.SelectedIndexChanged += new EventHandler(this.ScriptExecCombo_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label14, "label14");
    this.label14.Name = "label14";
    componentResourceManager.ApplyResources((object) this.label10, "label10");
    this.label10.Name = "label10";
    componentResourceManager.ApplyResources((object) this.Column, "Column");
    this.ToolTip.AutoPopDelay = 3000;
    this.ToolTip.InitialDelay = 100;
    this.ToolTip.ReshowDelay = 100;
    componentResourceManager.ApplyResources((object) this.loadPublishOptions, "loadPublishOptions");
    this.loadPublishOptions.Name = "loadPublishOptions";
    this.loadPublishOptions.UseVisualStyleBackColor = true;
    this.loadPublishOptions.Click += new EventHandler(this.loadPublishOptions_Click);
    componentResourceManager.ApplyResources((object) this.savePublishOptions, "savePublishOptions");
    this.savePublishOptions.Name = "savePublishOptions";
    this.savePublishOptions.UseVisualStyleBackColor = true;
    this.savePublishOptions.Click += new EventHandler(this.savePublishOptions_Click);
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.PageControl);
    this.Controls.Add((Control) this.Panel2);
    this.HelpButton = true;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ActivPropForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) "   ";
    this.HelpButtonClicked += new CancelEventHandler(this.ActivPropForm_HelpButtonClicked);
    this.Closed += new EventHandler(this.ActivPropForm_Closed);
    this.Load += new EventHandler(this.ActivPropForm_Load);
    this.HelpRequested += new HelpEventHandler(this.ActivPropForm_HelpRequested);
    this.KeyDown += new KeyEventHandler(this.ActivPropForm_KeyDown);
    this.Resize += new EventHandler(this.ActivPropForm_Resize);
    this.MsgStopPanel.ResumeLayout(false);
    this.MsgStopPanel.PerformLayout();
    this.MsgBackPanel.ResumeLayout(false);
    this.MsgBackPanel.PerformLayout();
    this.Panel2.ResumeLayout(false);
    this.PageControl.ResumeLayout(false);
    this.BaseTabSheet.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    ((ISupportInitialize) this.ActImage).EndInit();
    this.ActRuntimePanel.ResumeLayout(false);
    this.ActRuntimePanel.PerformLayout();
    this.ContentTabSheet.ResumeLayout(false);
    this.ContentTabSheet.PerformLayout();
    this.ContentOptionsPanel.ResumeLayout(false);
    this.ContentOptionsPanel.PerformLayout();
    this.TempRightsGroupBox.ResumeLayout(false);
    this.TempRightsGroupBox.PerformLayout();
    this.SchemeAttachsTabSheet.ResumeLayout(false);
    this.CaseTabSheet.ResumeLayout(false);
    this.CaseTabSheet.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.panel3.PerformLayout();
    this.CondTabSheet.ResumeLayout(false);
    this.CondGroupBox.ResumeLayout(false);
    this.ConditionBox.Properties.EndInit();
    this.ParticipantsTabSheet.ResumeLayout(false);
    this.PartsPanel.ResumeLayout(false);
    this.PartKindGroupBox.ResumeLayout(false);
    this.PartKindGroupBox.PerformLayout();
    this.AutoTabSheet.ResumeLayout(false);
    this.GroupBox7.ResumeLayout(false);
    this.GroupBox2.ResumeLayout(false);
    this.GroupBox1.ResumeLayout(false);
    this.GroupBox1.PerformLayout();
    this.ApproveTabSheet.ResumeLayout(false);
    this.ApproveTabSheet.PerformLayout();
    this.WhatSignGB.ResumeLayout(false);
    this.WhatSignGB.PerformLayout();
    this.panel4.ResumeLayout(false);
    this.ApproveCheckGB.ResumeLayout(false);
    this.ApproveCheckGB.PerformLayout();
    this.RanksPanel.ResumeLayout(false);
    this.RanksPanel.PerformLayout();
    this.panel8.ResumeLayout(false);
    this.ArchiveTabSheet.ResumeLayout(false);
    this.ArchiveTabSheet.PerformLayout();
    this.RegRevGroupBox.ResumeLayout(false);
    this.RevArcIDEdit.Properties.EndInit();
    this.RegDocsGroupBox.ResumeLayout(false);
    this.DocArcIDEdit.Properties.EndInit();
    this.RegisterGroupBox.ResumeLayout(false);
    this.SubProcessTabSheet.ResumeLayout(false);
    this.GroupBox5.ResumeLayout(false);
    this.GroupBox5.PerformLayout();
    this.SchemeEdit.Properties.EndInit();
    this.SubNameEdit.Properties.EndInit();
    this.RemoteSubProcessTabSheet.ResumeLayout(false);
    this.RemoteProcess2GroupBox.ResumeLayout(false);
    this.RemoteProcess2GroupBox.PerformLayout();
    this.RemoteProcessGroupBox.ResumeLayout(false);
    this.RemoteProcessGroupBox.PerformLayout();
    this.PortalErrPanel.ResumeLayout(false);
    this.PortalErrPanel.PerformLayout();
    ((ISupportInitialize) this.PortalErrImage).EndInit();
    this.TermsTabSheet.ResumeLayout(false);
    this.TermOptsGroupBox.ResumeLayout(false);
    this.TermOptsGroupBox.PerformLayout();
    this.GroupBox10.ResumeLayout(false);
    this.unreadTermEdit.Properties.EndInit();
    this.GroupBox8.ResumeLayout(false);
    this.termEdit.Properties.EndInit();
    this.RollbackTabSheet.ResumeLayout(false);
    this.RollbackGroupBox.ResumeLayout(false);
    this.RollbackGroupBox.PerformLayout();
    this.LCTabSheet.ResumeLayout(false);
    this.LCGroupBox.ResumeLayout(false);
    this.LCPanel2.ResumeLayout(false);
    this.LCPanel2.PerformLayout();
    this.LCPanel1.ResumeLayout(false);
    this.LCPanel1.PerformLayout();
    this.NotifTabSheet.ResumeLayout(false);
    this.NotifTabSheet.PerformLayout();
    this.MsgsGroupBox.ResumeLayout(false);
    this.MsgAbortPanel.ResumeLayout(false);
    this.MsgAbortPanel.PerformLayout();
    this.MsgPeriodPanel.ResumeLayout(false);
    this.MsgPeriodPanel.PerformLayout();
    this.MsgReadPanel.ResumeLayout(false);
    this.MsgReadPanel.PerformLayout();
    this.MsgStartPanel.ResumeLayout(false);
    this.MsgStartPanel.PerformLayout();
    this.ScriptTabSheet.ResumeLayout(false);
    this.AfterScriptGroupBox.ResumeLayout(false);
    this.AfterScriptGroupBox.PerformLayout();
    this.afterScriptEdit.Properties.EndInit();
    this.BeforeScriptGroupBox.ResumeLayout(false);
    this.BeforeScriptGroupBox.PerformLayout();
    this.beforeScriptEdit.Properties.EndInit();
    this.ScriptGroupBox.ResumeLayout(false);
    this.ScriptGroupBox.PerformLayout();
    this.scriptEdit.Properties.EndInit();
    this.ScriptUserGroupBox.ResumeLayout(false);
    this.ScriptUserEdit.Properties.EndInit();
    this.ResumeLayout(false);
  }
}
