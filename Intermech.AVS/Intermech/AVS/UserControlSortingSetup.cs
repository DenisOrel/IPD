// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.UserControlSortingSetup
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Mask;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.AVS.Properties;
using Intermech.Client.Core;
using Intermech.Document.DBCore;
using Intermech.Expert;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Navigator.Interfaces;
using Intermech.UI.Winforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса UserControlSortingSetup </summary>
public class UserControlSortingSetup : ExtUserControl, IPropertyPage
{
  private Control _changedControl;
  private long _specificationObjectId = -1;
  private long _specificationTemplateObjectId = -1;
  public SortSchema _sortSchema;
  private int _sectionObjImageIndex = -1;
  private static char[] _predefinedSymbols = new char[5]
  {
    '.',
    '-',
    '*',
    ',',
    ' '
  };
  private ArrayList _nodeDescriptors = new ArrayList();
  private SettingsStructure _settingsStructure;
  private List<Triple> _tripleList;
  private List<int> _objTypes;
  private CheckBox cbSortPartForPodborAfterBasePart;
  private CheckBox cbSortDocumentsByType;
  private List<int> _relTypes;
  private IContainer components;
  private GroupBox _GroupBoxFinishSubstr;
  private ComboBoxEdit _comboBoxSubstrFinishSymbol;
  protected SpinEdit _upDownSubstrFinishNumber;
  private Label label2;
  private Label label6;
  private Label label7;
  private ImageComboBoxEdit _comboBoxSubstrFinishAt;
  private GroupBox _GroupBoxStartSubstr;
  private ComboBoxEdit _comboBoxSubstrStartSymbol;
  protected SpinEdit _upDownSubstrStartNumber;
  private Label _label5;
  private Label _label4;
  private Label _label3;
  private ImageComboBoxEdit _comboBoxSubstrStartFrom;
  private Label _label1;
  private ImageComboBoxEdit _comboBoxListSource;
  private Label label3;
  public Button _BtnReset;
  private TreeListColumn treeListColumn1;
  private TreeListColumn treeListColumn2;
  private TreeListColumn treeListColumn3;
  private TreeListColumn treeListColumn4;
  private TreeListColumn treeListColumn5;
  private GroupBox groupBox1;
  private Label label1;
  private Label label4;
  private Label label5;
  private Button _btnDelUslov;
  private Button _btnAddUslov;
  private ImageComboBoxEdit _comboBoxCompareType;
  private ImageComboBoxEdit _comboBoxEmptyRecord;
  private ImageComboBoxEdit _comboBoxAlign;
  private Button _btnMoveUp;
  private ToolTipController _editModeToolTip;
  private ToolTipController _readModeToolTip;
  private ImageList _imageList;
  private TreeList _treeListSortSchema;
  private Label label8;
  private ButtonEdit _editAttribute;
  private Button _btnMoveDown;
  private List<AVSColumnScheme> _customColumnSchemes = new List<AVSColumnScheme>();

  /// <summary> Список типов объектов, которые могут присутствовать в ведомости </summary>
  public List<int> ObjTypes
  {
    get => this._objTypes;
    set => this._objTypes = value;
  }

  /// <summary> Список типов связей, которые могут присутствовать в ведомости </summary>
  public List<int> RelTypes
  {
    get => this._relTypes;
    set => this._relTypes = value;
  }

  /// <summary> Идентификатор объекта для которого открыта спецификация </summary>
  public long SpecificationObjectId
  {
    get => this._specificationObjectId;
    set => this._specificationObjectId = value;
  }

  /// <summary> Идентификатор шаблона спецификации </summary>
  public long SpecificationTemplateObjectId
  {
    get => this._specificationTemplateObjectId;
    set => this._specificationTemplateObjectId = value;
  }

  public SettingsStructure SettingsStructure
  {
    get => this._settingsStructure;
    set => this._settingsStructure = value;
  }

  public List<Triple> TripleList
  {
    get => this._tripleList;
    set => this._tripleList = value;
  }

  /// <summary> Схема сортировки спецификации </summary>
  public SortSchema SortSchema
  {
    get => this._sortSchema;
    set
    {
      this.LockControls();
      try
      {
        this._sortSchema = value;
        this.ReloadSchemaTree();
        this.RefreshReadOnly();
        this.UpdateControls(true);
        this.RaiseOnInitDataEvent((object) this._sortSchema);
      }
      finally
      {
        this.UnlockControls();
      }
    }
  }

  /// <summary> Индекс иконки типа объекта "Раздел спецификации" </summary>
  public int SectionObjImageIndex => this._sectionObjImageIndex;

  /// <summary> Идентификатор активного раздела спецификации. Если сфокусирован атрибут, то активным считается раздел, в который он входит </summary>
  public long ActiveSectionID
  {
    get
    {
      TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
      if (focusedNode == null)
        return -1;
      UserControlSortingSetup.NodeDescriptor nodeDescriptor1 = focusedNode.Tag is UserControlSortingSetup.NodeDescriptor tag1 ? tag1 : (UserControlSortingSetup.NodeDescriptor) null;
      if (nodeDescriptor1 == null)
        return -1;
      UserControlSortingSetup.SectionDescriptor sectionDescriptor1 = nodeDescriptor1.SectionDescriptor;
      if (sectionDescriptor1 != null)
        return sectionDescriptor1.SectionID;
      TreeListNode parentNode = focusedNode.ParentNode;
      if (parentNode == null)
        return -1;
      UserControlSortingSetup.NodeDescriptor nodeDescriptor2 = parentNode.Tag is UserControlSortingSetup.NodeDescriptor tag2 ? tag2 : (UserControlSortingSetup.NodeDescriptor) null;
      if (nodeDescriptor2 == null)
        return -1;
      UserControlSortingSetup.SectionDescriptor sectionDescriptor2 = nodeDescriptor2.SectionDescriptor;
      return sectionDescriptor2 != null ? sectionDescriptor2.SectionID : -1L;
    }
  }

  /// <summary> Ссылка на активный заголовок ведомости. Если сфокусирован атрибут, то активным считается заголовок, в который он входит </summary>
  public Triple ActiveTriple
  {
    get
    {
      TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
      if (focusedNode == null)
        return (Triple) null;
      UserControlSortingSetup.NodeDescriptor nodeDescriptor = focusedNode.Tag is UserControlSortingSetup.NodeDescriptor tag1 ? tag1 : (UserControlSortingSetup.NodeDescriptor) null;
      if (nodeDescriptor == null)
        return (Triple) null;
      Triple triple = nodeDescriptor.Triple;
      if (triple != null)
        return triple;
      TreeListNode parentNode = focusedNode.ParentNode;
      if (parentNode == null)
        return (Triple) null;
      return (parentNode.Tag is UserControlSortingSetup.NodeDescriptor tag2 ? tag2 : (UserControlSortingSetup.NodeDescriptor) null)?.Triple;
    }
  }

  /// <summary> Схема сортировки активного раздела спецификации. Если сфокусирован атрибут, то активным считается раздел, в который он входит </summary>
  public SectionSortSchema ActiveSectionSortSchema
  {
    get
    {
      TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
      if (focusedNode == null)
        return (SectionSortSchema) null;
      UserControlSortingSetup.NodeDescriptor nodeDescriptor = focusedNode.Tag is UserControlSortingSetup.NodeDescriptor tag1 ? tag1 : (UserControlSortingSetup.NodeDescriptor) null;
      if (nodeDescriptor == null)
        return (SectionSortSchema) null;
      SectionSortSchema sectionSortSchema = nodeDescriptor.SectionSortSchema;
      if (sectionSortSchema != null)
        return sectionSortSchema;
      TreeListNode parentNode = focusedNode.ParentNode;
      if (parentNode == null)
        return (SectionSortSchema) null;
      return (parentNode.Tag is UserControlSortingSetup.NodeDescriptor tag2 ? tag2 : (UserControlSortingSetup.NodeDescriptor) null)?.SectionSortSchema;
    }
  }

  /// <summary> Идентификатор сфокусированого раздела спецификации. -1 если сфокукусирован не раздел </summary>
  public long FocusedSectionID
  {
    get
    {
      TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
      if (focusedNode == null)
        return -1;
      UserControlSortingSetup.NodeDescriptor nodeDescriptor = focusedNode.Tag is UserControlSortingSetup.NodeDescriptor tag ? tag : (UserControlSortingSetup.NodeDescriptor) null;
      if (nodeDescriptor == null)
        return -1;
      UserControlSortingSetup.SectionDescriptor sectionDescriptor = nodeDescriptor.SectionDescriptor;
      return sectionDescriptor == null ? -1L : sectionDescriptor.SectionID;
    }
  }

  /// <summary> Идентификатор сфокусированого атрибута условия сортировкм. -1 если сфокукусировано не условие сортировки атрибута </summary>
  public Guid FocusedAttributeSchemaGuid
  {
    get
    {
      TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
      if (focusedNode == null || !(focusedNode.Tag is UserControlSortingSetup.NodeDescriptor tag))
        return Guid.Empty;
      AttributeSortSchema attributeSortSchema = tag.AttributeSortSchema;
      return attributeSortSchema != null ? attributeSortSchema.SchemeGuid : Guid.Empty;
    }
  }

  /// <summary> Сфокусированая схема сортировки </summary>
  public AttributeSortSchema FocusedAttributeSortSchema
  {
    get
    {
      TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
      if (focusedNode == null)
        return (AttributeSortSchema) null;
      return (focusedNode.Tag is UserControlSortingSetup.NodeDescriptor tag ? tag : (UserControlSortingSetup.NodeDescriptor) null)?.AttributeSortSchema;
    }
  }

  /// <summary> Индекс сфокусированой ноды в списке нодов вышестоящей ноды :) </summary>
  private int RelativeFocusedNodeIndex
  {
    get
    {
      TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
      if (focusedNode == null)
        return -1;
      return focusedNode.ParentNode == null ? this._treeListSortSchema.Nodes.IndexOf(focusedNode) : focusedNode.ParentNode.Nodes.IndexOf(focusedNode);
    }
  }

  public UserControlSortingSetup()
  {
    this.InitializeComponent();
    foreach (TreeListColumn column in (CollectionBase) this._treeListSortSchema.Columns)
      column.Options = ColumnOptions.CanMoved | ColumnOptions.CanResized | ColumnOptions.ReadOnly | ColumnOptions.CanFocused | ColumnOptions.ShowInCustomizationForm | ColumnOptions.CanMovedToCustomizationForm;
    this._btnMoveDown.Image = (Image) Resources.arrow_down_blueStandart;
    this._btnMoveUp.Image = (Image) Resources.arrow_up_blueStandart;
    this.Init();
  }

  /// <summary> Инициализация </summary>
  private void Init()
  {
    Icon objTypeIcon = UIHelper.GetObjTypeIcon(AvsIDCache.ObjType_SpecificationSection);
    if (objTypeIcon == null)
      return;
    this._imageList.Images.Add(objTypeIcon);
    this._sectionObjImageIndex = this._imageList.Images.Count - 1;
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модиффицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlSortingSetup));
    this._editModeToolTip = new ToolTipController(this.components);
    this._btnMoveUp = new Button();
    this._btnMoveDown = new Button();
    this._btnDelUslov = new Button();
    this._btnAddUslov = new Button();
    this._readModeToolTip = new ToolTipController(this.components);
    this._GroupBoxFinishSubstr = new GroupBox();
    this._comboBoxSubstrFinishSymbol = new ComboBoxEdit();
    this._upDownSubstrFinishNumber = new SpinEdit();
    this.label2 = new Label();
    this.label6 = new Label();
    this.label7 = new Label();
    this._comboBoxSubstrFinishAt = new ImageComboBoxEdit();
    this._GroupBoxStartSubstr = new GroupBox();
    this._comboBoxSubstrStartSymbol = new ComboBoxEdit();
    this._upDownSubstrStartNumber = new SpinEdit();
    this._label5 = new Label();
    this._label4 = new Label();
    this._label3 = new Label();
    this._comboBoxSubstrStartFrom = new ImageComboBoxEdit();
    this._label1 = new Label();
    this._comboBoxListSource = new ImageComboBoxEdit();
    this.label3 = new Label();
    this._BtnReset = new Button();
    this._treeListSortSchema = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.treeListColumn2 = new TreeListColumn();
    this.treeListColumn3 = new TreeListColumn();
    this.treeListColumn4 = new TreeListColumn();
    this.treeListColumn5 = new TreeListColumn();
    this._imageList = new ImageList(this.components);
    this.groupBox1 = new GroupBox();
    this._comboBoxAlign = new ImageComboBoxEdit();
    this._comboBoxEmptyRecord = new ImageComboBoxEdit();
    this._comboBoxCompareType = new ImageComboBoxEdit();
    this.label1 = new Label();
    this.label4 = new Label();
    this.label5 = new Label();
    this.label8 = new Label();
    this._editAttribute = new ButtonEdit();
    this.cbSortPartForPodborAfterBasePart = new CheckBox();
    this.cbSortDocumentsByType = new CheckBox();
    this._GroupBoxFinishSubstr.SuspendLayout();
    this._comboBoxSubstrFinishSymbol.Properties.BeginInit();
    this._upDownSubstrFinishNumber.Properties.BeginInit();
    this._comboBoxSubstrFinishAt.Properties.BeginInit();
    this._GroupBoxStartSubstr.SuspendLayout();
    this._comboBoxSubstrStartSymbol.Properties.BeginInit();
    this._upDownSubstrStartNumber.Properties.BeginInit();
    this._comboBoxSubstrStartFrom.Properties.BeginInit();
    this._comboBoxListSource.Properties.BeginInit();
    this._treeListSortSchema.BeginInit();
    this.groupBox1.SuspendLayout();
    this._comboBoxAlign.Properties.BeginInit();
    this._comboBoxEmptyRecord.Properties.BeginInit();
    this._comboBoxCompareType.Properties.BeginInit();
    this._editAttribute.Properties.BeginInit();
    this.SuspendLayout();
    this._editModeToolTip.Active = false;
    this._editModeToolTip.Style = new ViewStyle("ToolTip style");
    this._btnMoveUp.Anchor = AnchorStyles.Right;
    this._btnMoveUp.FlatStyle = FlatStyle.Popup;
    this._btnMoveUp.Image = (Image) componentResourceManager.GetObject("_btnMoveUp.Image");
    this._btnMoveUp.Location = new Point(678, 96 /*0x60*/);
    this._btnMoveUp.Name = "_btnMoveUp";
    this._btnMoveUp.Size = new Size(25, 25);
    this._btnMoveUp.TabIndex = 7;
    this._editModeToolTip.SetToolTip((Control) this._btnMoveUp, "Переместить условие вверх (ctrl + стрелка вверх)");
    this._btnMoveUp.Click += new EventHandler(this._btnMoveUp_Click);
    this._btnMoveDown.Anchor = AnchorStyles.Right;
    this._btnMoveDown.FlatStyle = FlatStyle.Popup;
    this._btnMoveDown.Image = (Image) componentResourceManager.GetObject("_btnMoveDown.Image");
    this._btnMoveDown.Location = new Point(678, (int) sbyte.MaxValue);
    this._btnMoveDown.Name = "_btnMoveDown";
    this._btnMoveDown.Size = new Size(25, 25);
    this._btnMoveDown.TabIndex = 8;
    this._editModeToolTip.SetToolTip((Control) this._btnMoveDown, "Переместить условие вниз (ctrl + стрелка вниз)");
    this._btnMoveDown.Click += new EventHandler(this._btnMoveDown_Click);
    this._btnDelUslov.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._btnDelUslov.FlatStyle = FlatStyle.System;
    this._btnDelUslov.Location = new Point(600, 187);
    this._btnDelUslov.Name = "_btnDelUslov";
    this._btnDelUslov.Size = new Size(75, 23);
    this._btnDelUslov.TabIndex = 2;
    this._btnDelUslov.Text = "Удалить";
    this._editModeToolTip.SetToolTip((Control) this._btnDelUslov, "Удалить выбранное условие сортировки");
    this._btnDelUslov.Click += new EventHandler(this._btnDelUslov_Click);
    this._btnAddUslov.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._btnAddUslov.FlatStyle = FlatStyle.System;
    this._btnAddUslov.Location = new Point(516, 187);
    this._btnAddUslov.Name = "_btnAddUslov";
    this._btnAddUslov.Size = new Size(75, 23);
    this._btnAddUslov.TabIndex = 1;
    this._btnAddUslov.Text = "Добавить";
    this._editModeToolTip.SetToolTip((Control) this._btnAddUslov, "Добавить в выбранный раздел новое условие сортировки");
    this._btnAddUslov.Click += new EventHandler(this._btnAddUslov_Click);
    this._readModeToolTip.Style = new ViewStyle("ToolTip style");
    this._GroupBoxFinishSubstr.Anchor = AnchorStyles.Bottom;
    this._GroupBoxFinishSubstr.Controls.Add((Control) this._comboBoxSubstrFinishSymbol);
    this._GroupBoxFinishSubstr.Controls.Add((Control) this._upDownSubstrFinishNumber);
    this._GroupBoxFinishSubstr.Controls.Add((Control) this.label2);
    this._GroupBoxFinishSubstr.Controls.Add((Control) this.label6);
    this._GroupBoxFinishSubstr.Controls.Add((Control) this.label7);
    this._GroupBoxFinishSubstr.Controls.Add((Control) this._comboBoxSubstrFinishAt);
    this._GroupBoxFinishSubstr.FlatStyle = FlatStyle.System;
    this._GroupBoxFinishSubstr.Location = new Point(247, 214);
    this._GroupBoxFinishSubstr.Name = "_GroupBoxFinishSubstr";
    this._GroupBoxFinishSubstr.Size = new Size(211, 97);
    this._GroupBoxFinishSubstr.TabIndex = 5;
    this._GroupBoxFinishSubstr.TabStop = false;
    this._GroupBoxFinishSubstr.Text = "Окончание подстроки";
    this._comboBoxSubstrFinishSymbol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._comboBoxSubstrFinishSymbol.Location = new Point(56, 70);
    this._comboBoxSubstrFinishSymbol.Name = "_comboBoxSubstrFinishSymbol";
    this._comboBoxSubstrFinishSymbol.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxSubstrFinishSymbol.Properties.Items.AddRange(new object[5]
    {
      (object) ". (точка)",
      (object) "- (минус)",
      (object) "* (звездочка)",
      (object) ", (запятая)",
      (object) "  (пробел)"
    });
    this._comboBoxSubstrFinishSymbol.Properties.MaskData.EditMask = "C";
    this._comboBoxSubstrFinishSymbol.Properties.MaskData.MaskType = MaskType.Simple;
    this._comboBoxSubstrFinishSymbol.Properties.PopupSizeable = true;
    this._comboBoxSubstrFinishSymbol.Properties.ReadOnly = true;
    this._comboBoxSubstrFinishSymbol.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this._comboBoxSubstrFinishSymbol.Size = new Size(149, 23);
    this._comboBoxSubstrFinishSymbol.TabIndex = 2;
    this._comboBoxSubstrFinishSymbol.ToolTip = "Выбор окончания сортируемой подстроки";
    this._comboBoxSubstrFinishSymbol.SelectedIndexChanged += new EventHandler(this._comboBoxSubstrFinishSymbol_SelectedIndexChanged);
    this._comboBoxSubstrFinishSymbol.EditValueChanged += new EventHandler(this._comboBoxSubstrFinishSymbol_EditValueChanged);
    this._comboBoxSubstrFinishSymbol.EditValueChanging += new ChangingEventHandler(this._comboBoxSubstrFinishSymbol_EditValueChanging);
    this._comboBoxSubstrFinishSymbol.Leave += new EventHandler(this._comboBoxSubstrFinishSymbol_Leave);
    this._upDownSubstrFinishNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._upDownSubstrFinishNumber.EditValue = (object) 12;
    this._upDownSubstrFinishNumber.Location = new Point(56, 44);
    this._upDownSubstrFinishNumber.Name = "_upDownSubstrFinishNumber";
    this._upDownSubstrFinishNumber.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownSubstrFinishNumber.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownSubstrFinishNumber.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownSubstrFinishNumber.Properties.IsFloatValue = false;
    this._upDownSubstrFinishNumber.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownSubstrFinishNumber.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._upDownSubstrFinishNumber.Properties.UseCtrlIncrement = false;
    this._upDownSubstrFinishNumber.Properties.ValidateOnEnterKey = true;
    this._upDownSubstrFinishNumber.Size = new Size(149, 20);
    this._upDownSubstrFinishNumber.TabIndex = 1;
    this._upDownSubstrFinishNumber.ToolTip = "Позиция окончания подстроки обозначения";
    this._upDownSubstrFinishNumber.EditValueChanged += new EventHandler(this._upDownSubstrFinishNumber_EditValueChanged);
    this._upDownSubstrFinishNumber.EditValueChanging += new ChangingEventHandler(this._upDownSubstrFinishNumber_EditValueChanging);
    this.label2.FlatStyle = FlatStyle.System;
    this.label2.Location = new Point(8, 71);
    this.label2.Name = "label2";
    this.label2.Size = new Size(42, 22);
    this.label2.TabIndex = 2;
    this.label2.Text = "Символ:";
    this.label2.TextAlign = ContentAlignment.MiddleRight;
    this.label6.FlatStyle = FlatStyle.System;
    this.label6.Location = new Point(8, 45);
    this.label6.Name = "label6";
    this.label6.Size = new Size(42, 22);
    this.label6.TabIndex = 1;
    this.label6.Text = "Номер:";
    this.label6.TextAlign = ContentAlignment.MiddleRight;
    this.label7.FlatStyle = FlatStyle.System;
    this.label7.Location = new Point(8, 19);
    this.label7.Name = "label7";
    this.label7.Size = new Size(42, 22);
    this.label7.TabIndex = 0;
    this.label7.Text = "До:";
    this.label7.TextAlign = ContentAlignment.MiddleRight;
    this._comboBoxSubstrFinishAt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._comboBoxSubstrFinishAt.EditValue = (object) 1;
    this._comboBoxSubstrFinishAt.Location = new Point(56, 17);
    this._comboBoxSubstrFinishAt.Name = "_comboBoxSubstrFinishAt";
    this._comboBoxSubstrFinishAt.Properties.AutoComplete = false;
    this._comboBoxSubstrFinishAt.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxSubstrFinishAt.Properties.Items.AddRange(new ImageComboBoxItem[4]
    {
      new ImageComboBoxItem("Конца атрибута", (object) 0, -1),
      new ImageComboBoxItem("Количества символов", (object) 1, -1),
      new ImageComboBoxItem("Символ номер", (object) 3, -1),
      new ImageComboBoxItem("Символ номер (считая с конца атрибута)", (object) 2, -1)
    });
    this._comboBoxSubstrFinishAt.Properties.PopupSizeable = true;
    this._comboBoxSubstrFinishAt.Size = new Size(149, 23);
    this._comboBoxSubstrFinishAt.TabIndex = 0;
    this._comboBoxSubstrFinishAt.ToolTip = "Окончание подстроки атрибута";
    this._comboBoxSubstrFinishAt.SelectedIndexChanged += new EventHandler(this._comboBoxSubstrFinishAt_SelectedIndexChanged);
    this._comboBoxSubstrFinishAt.CloseUp += new CloseUpEventHandler(this._comboBoxSubstrFinishAt_CloseUp);
    this._GroupBoxStartSubstr.Anchor = AnchorStyles.Bottom;
    this._GroupBoxStartSubstr.Controls.Add((Control) this._comboBoxSubstrStartSymbol);
    this._GroupBoxStartSubstr.Controls.Add((Control) this._upDownSubstrStartNumber);
    this._GroupBoxStartSubstr.Controls.Add((Control) this._label5);
    this._GroupBoxStartSubstr.Controls.Add((Control) this._label4);
    this._GroupBoxStartSubstr.Controls.Add((Control) this._label3);
    this._GroupBoxStartSubstr.Controls.Add((Control) this._comboBoxSubstrStartFrom);
    this._GroupBoxStartSubstr.FlatStyle = FlatStyle.System;
    this._GroupBoxStartSubstr.Location = new Point(33, 214);
    this._GroupBoxStartSubstr.Name = "_GroupBoxStartSubstr";
    this._GroupBoxStartSubstr.Size = new Size(211, 97);
    this._GroupBoxStartSubstr.TabIndex = 4;
    this._GroupBoxStartSubstr.TabStop = false;
    this._GroupBoxStartSubstr.Text = "Начало подстроки";
    this._comboBoxSubstrStartSymbol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._comboBoxSubstrStartSymbol.EditValue = (object) "";
    this._comboBoxSubstrStartSymbol.Location = new Point(56, 70);
    this._comboBoxSubstrStartSymbol.Name = "_comboBoxSubstrStartSymbol";
    this._comboBoxSubstrStartSymbol.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxSubstrStartSymbol.Properties.Items.AddRange(new object[5]
    {
      (object) ". (точка)",
      (object) "- (минус)",
      (object) "* (звездочка)",
      (object) ", (запятая)",
      (object) "  (пробел)"
    });
    this._comboBoxSubstrStartSymbol.Properties.MaskData.EditMask = "C";
    this._comboBoxSubstrStartSymbol.Properties.MaskData.MaskType = MaskType.Simple;
    this._comboBoxSubstrStartSymbol.Properties.PopupSizeable = true;
    this._comboBoxSubstrStartSymbol.Properties.ReadOnly = true;
    this._comboBoxSubstrStartSymbol.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this._comboBoxSubstrStartSymbol.Size = new Size(150, 23);
    this._comboBoxSubstrStartSymbol.TabIndex = 2;
    this._comboBoxSubstrStartSymbol.ToolTip = "Начало подстроки значения атрибута";
    this._comboBoxSubstrStartSymbol.SelectedIndexChanged += new EventHandler(this._comboBoxSubstrStartSymbol_SelectedIndexChanged);
    this._comboBoxSubstrStartSymbol.EditValueChanged += new EventHandler(this._comboBoxSubstrStartSymbol_EditValueChanged);
    this._comboBoxSubstrStartSymbol.EditValueChanging += new ChangingEventHandler(this._comboBoxSubstrStartSymbol_EditValueChanging);
    this._comboBoxSubstrStartSymbol.Leave += new EventHandler(this._comboBoxSubstrStartSymbol_Leave);
    this._upDownSubstrStartNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._upDownSubstrStartNumber.EditValue = (object) 1;
    this._upDownSubstrStartNumber.Location = new Point(56, 44);
    this._upDownSubstrStartNumber.Name = "_upDownSubstrStartNumber";
    this._upDownSubstrStartNumber.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._upDownSubstrStartNumber.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this._upDownSubstrStartNumber.Properties.EditFormat.FormatType = FormatType.Numeric;
    this._upDownSubstrStartNumber.Properties.IsFloatValue = false;
    this._upDownSubstrStartNumber.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._upDownSubstrStartNumber.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._upDownSubstrStartNumber.Properties.ReadOnly = true;
    this._upDownSubstrStartNumber.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this._upDownSubstrStartNumber.Properties.UseCtrlIncrement = false;
    this._upDownSubstrStartNumber.Properties.ValidateOnEnterKey = true;
    this._upDownSubstrStartNumber.Size = new Size(150, 20);
    this._upDownSubstrStartNumber.TabIndex = 1;
    this._upDownSubstrStartNumber.ToolTip = "Позиция начала подстроки";
    this._upDownSubstrStartNumber.EditValueChanged += new EventHandler(this._upDownSubstrStartNumber_EditValueChanged);
    this._upDownSubstrStartNumber.EditValueChanging += new ChangingEventHandler(this._upDownSubstrStartNumber_EditValueChanging);
    this._label5.FlatStyle = FlatStyle.System;
    this._label5.Location = new Point(8, 71);
    this._label5.Name = "_label5";
    this._label5.Size = new Size(42, 22);
    this._label5.TabIndex = 2;
    this._label5.Text = "Символ:";
    this._label5.TextAlign = ContentAlignment.MiddleRight;
    this._label4.FlatStyle = FlatStyle.System;
    this._label4.Location = new Point(8, 45);
    this._label4.Name = "_label4";
    this._label4.Size = new Size(42, 22);
    this._label4.TabIndex = 1;
    this._label4.Text = "Номер:";
    this._label4.TextAlign = ContentAlignment.MiddleRight;
    this._label3.FlatStyle = FlatStyle.System;
    this._label3.Location = new Point(8, 19);
    this._label3.Name = "_label3";
    this._label3.Size = new Size(42, 22);
    this._label3.TabIndex = 0;
    this._label3.Text = "От:";
    this._label3.TextAlign = ContentAlignment.MiddleRight;
    this._comboBoxSubstrStartFrom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._comboBoxSubstrStartFrom.EditValue = (object) 3;
    this._comboBoxSubstrStartFrom.Location = new Point(56, 17);
    this._comboBoxSubstrStartFrom.Name = "_comboBoxSubstrStartFrom";
    this._comboBoxSubstrStartFrom.Properties.AutoComplete = false;
    this._comboBoxSubstrStartFrom.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxSubstrStartFrom.Properties.Items.AddRange(new ImageComboBoxItem[4]
    {
      new ImageComboBoxItem("Начала атрибута", (object) 3, -1),
      new ImageComboBoxItem("Буквы номер", (object) 0, -1),
      new ImageComboBoxItem("Символа номер", (object) 1, -1),
      new ImageComboBoxItem("Символа номер (считая с конца атрибута)", (object) 2, -1)
    });
    this._comboBoxSubstrStartFrom.Properties.PopupSizeable = true;
    this._comboBoxSubstrStartFrom.Size = new Size(150, 23);
    this._comboBoxSubstrStartFrom.TabIndex = 0;
    this._comboBoxSubstrStartFrom.ToolTip = "Выбор начала сортируемой подстроки";
    this._comboBoxSubstrStartFrom.SelectedIndexChanged += new EventHandler(this._comboBoxSubstrStartFrom_SelectedIndexChanged);
    this._comboBoxSubstrStartFrom.CloseUp += new CloseUpEventHandler(this._comboBoxSubstrStartFrom_CloseUp);
    this._label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._label1.FlatStyle = FlatStyle.System;
    this._label1.Location = new Point(33, 8);
    this._label1.Name = "_label1";
    this._label1.Size = new Size(576, 14);
    this._label1.TabIndex = 6;
    this._label1.Text = "При сравнении параметров берутся подстроки по следующим правилам:";
    this._comboBoxListSource.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._comboBoxListSource.EditValue = (object) false;
    this._comboBoxListSource.Location = new Point(99, 362);
    this._comboBoxListSource.Name = "_comboBoxListSource";
    this._comboBoxListSource.Properties.AutoComplete = false;
    this._comboBoxListSource.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxListSource.Properties.Items.AddRange(new ImageComboBoxItem[2]
    {
      new ImageComboBoxItem("Унаследован", (object) false, -1),
      new ImageComboBoxItem("Собственный", (object) true, -1)
    });
    this._comboBoxListSource.Size = new Size(141, 23);
    this._comboBoxListSource.TabIndex = 9;
    this._comboBoxListSource.ToolTip = "Выбор настроек сортировки";
    this._comboBoxListSource.Visible = false;
    this._comboBoxListSource.SelectedIndexChanged += new EventHandler(this._comboBoxListSource_SelectedIndexChanged);
    this.label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.label3.Location = new Point(27, 367);
    this.label3.Name = "label3";
    this.label3.Size = new Size(62, 13);
    this.label3.TabIndex = 21;
    this.label3.Text = "Настройки:";
    this.label3.TextAlign = ContentAlignment.MiddleRight;
    this._BtnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._BtnReset.Enabled = false;
    this._BtnReset.FlatStyle = FlatStyle.System;
    this._BtnReset.Location = new Point(15, 362);
    this._BtnReset.Name = "_BtnReset";
    this._BtnReset.Size = new Size(121, 27);
    this._BtnReset.TabIndex = 9;
    this._BtnReset.Text = "По умолчанию";
    this._BtnReset.Click += new EventHandler(this._BtnReset_Click);
    this._treeListSortSchema.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._treeListSortSchema.BehaviorOptions = BehaviorOptionsFlags.ExpandNodeOnDrag | BehaviorOptionsFlags.ShowToolTips | BehaviorOptionsFlags.ResizeNodes | BehaviorOptionsFlags.AutoSelectAllInEditor | BehaviorOptionsFlags.AutoNodeHeight | BehaviorOptionsFlags.AutoChangeParent | BehaviorOptionsFlags.CloseEditorOnLostFocus | BehaviorOptionsFlags.KeepSelectedOnClick | BehaviorOptionsFlags.SmartMouseHover;
    this._treeListSortSchema.Columns.AddRange(new TreeListColumn[5]
    {
      this.treeListColumn1,
      this.treeListColumn2,
      this.treeListColumn3,
      this.treeListColumn4,
      this.treeListColumn5
    });
    this._treeListSortSchema.Location = new Point(30, 30);
    this._treeListSortSchema.Name = "_treeListSortSchema";
    this._treeListSortSchema.BeginUnboundLoad();
    this._treeListSortSchema.AppendNode((object) new object[5], -1, 0, 0, 1);
    this._treeListSortSchema.AppendNode((object) new object[5], 0, 0, 0, -1);
    this._treeListSortSchema.AppendNode((object) new object[5], 0, 0, 0, -1);
    this._treeListSortSchema.AppendNode((object) new object[5], 0, 0, 0, -1);
    this._treeListSortSchema.AppendNode((object) new object[5]
    {
      (object) "Сортировка",
      null,
      null,
      null,
      null
    }, 0, 0, 0, -1);
    this._treeListSortSchema.AppendNode((object) new object[5], -1, 0, 0, -1);
    this._treeListSortSchema.AppendNode((object) new object[5], -1, 0, 0, 1);
    this._treeListSortSchema.AppendNode((object) new object[5], -1, 0, 0, -1);
    this._treeListSortSchema.AppendNode((object) new object[5], -1, 0, 0, 1);
    this._treeListSortSchema.AppendNode((object) new object[5], -1, 0, 0, -1);
    this._treeListSortSchema.AppendNode((object) new object[5]
    {
      (object) "Документация",
      null,
      null,
      null,
      null
    }, -1, 0, 0, 1);
    this._treeListSortSchema.AppendNode((object) new object[5]
    {
      (object) "dfsgsdfg",
      null,
      null,
      null,
      null
    }, -1, 0, 0, -1);
    this._treeListSortSchema.EndUnboundLoad();
    this._treeListSortSchema.PreviewLineCount = 3;
    this._treeListSortSchema.RowHeight = 18;
    this._treeListSortSchema.SelectImageList = this._imageList;
    this._treeListSortSchema.Size = new Size(645, 148);
    this._treeListSortSchema.Styles.AddReplace("Style1", (object) new ViewStyle("Style1", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, true, false, DevExpress.IM.Utils.HorzAlignment.Center, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText));
    this._treeListSortSchema.Styles.AddReplace("Preview", (object) new ViewStyle("Preview", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, true, false, DevExpress.IM.Utils.HorzAlignment.Near, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.GrayText, Color.Blue));
    this._treeListSortSchema.Styles.AddReplace("HideSelectionRow", (object) new ViewStyle("HideSelectionRow", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this._treeListSortSchema.Styles.AddReplace("Row", (object) new ViewStyle("Row", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, DevExpress.IM.Utils.HorzAlignment.Center, DevExpress.IM.Utils.VertAlignment.Bottom, (Image) null, Color.FromArgb(224 /*0xE0*/, 224 /*0xE0*/, 224 /*0xE0*/), SystemColors.WindowText));
    this._treeListSortSchema.Styles.AddReplace("SortDescend", (object) new ViewStyle("SortDescend", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "Style2", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this._treeListSortSchema.Styles.AddReplace("Empty", (object) new ViewStyle("Empty", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, Color.WhiteSmoke, SystemColors.Window));
    this._treeListSortSchema.Styles.AddReplace("SectionStyle", (object) new ViewStyle("SectionStyle", "", new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Bottom, (Image) null, Color.Gainsboro, SystemColors.WindowText));
    this._treeListSortSchema.Styles.AddReplace("Style3", (object) new ViewStyle("Style3", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "Style2", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, true, false, DevExpress.IM.Utils.HorzAlignment.Near, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this._treeListSortSchema.Styles.AddReplace("FocusedRow", (object) new ViewStyle("FocusedRow", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Bottom, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this._treeListSortSchema.Styles.AddReplace("TreeLine", (object) new ViewStyle("TreeLine", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, Color.Empty, SystemColors.ControlDark));
    this._treeListSortSchema.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Bottom, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this._treeListSortSchema.Styles.AddReplace("SortAscend", (object) new ViewStyle("SortAscend", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "Style2", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this._treeListSortSchema.Styles.AddReplace("Style2", (object) new ViewStyle("Style2", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, true, false, DevExpress.IM.Utils.HorzAlignment.Near, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.InactiveCaption, SystemColors.WindowText));
    this._treeListSortSchema.Styles.AddReplace("HeaderPanel", (object) new ViewStyle("HeaderPanel", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, DevExpress.IM.Utils.HorzAlignment.Center, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText));
    this._treeListSortSchema.TabIndex = 0;
    this._treeListSortSchema.TreeLineStyle = LineStyle.None;
    this._treeListSortSchema.UncheckedStateIndex = 4610;
    this._treeListSortSchema.ViewOptions = ViewOptionsFlags.AutoWidth | ViewOptionsFlags.ShowColumns | ViewOptionsFlags.ShowFocusedFrame;
    this._treeListSortSchema.BeforeCollapse += new BeforeCollapseEventHandler(this._treeListSortSchema_BeforeCollapse);
    this._treeListSortSchema.CalcNodeHeight += new CalcNodeHeightEventHandler(this.CalcNodeHeight);
    this._treeListSortSchema.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this._treeListSortSchema_FocusedNodeChanged);
    this._treeListSortSchema.CustomDrawNodeCell += new CustomDrawNodeCellEventHandler(this._treeListSortSchema_CustomDrawNodeCell);
    this._treeListSortSchema.DoubleClick += new EventHandler(this._treeListSortSchema_DoubleClick);
    this.treeListColumn1.Caption = "Атрибут";
    this.treeListColumn1.FieldName = "treeListColumn1";
    this.treeListColumn1.Name = "treeListColumn1";
    this.treeListColumn1.Options = ColumnOptions.CanResized | ColumnOptions.ReadOnly | ColumnOptions.CanFocused;
    this.treeListColumn1.StyleName = "Style2";
    this.treeListColumn1.VisibleIndex = 0;
    this.treeListColumn1.Width = 240 /*0xF0*/;
    this.treeListColumn2.Caption = "От";
    this.treeListColumn2.FieldName = "treeListColumn2";
    this.treeListColumn2.Name = "treeListColumn2";
    this.treeListColumn2.Options = ColumnOptions.CanResized | ColumnOptions.ReadOnly;
    this.treeListColumn2.StyleName = "Style1";
    this.treeListColumn2.VisibleIndex = 1;
    this.treeListColumn2.Width = 110;
    this.treeListColumn3.Caption = "До";
    this.treeListColumn3.FieldName = "treeListColumn3";
    this.treeListColumn3.Name = "treeListColumn3";
    this.treeListColumn3.Options = ColumnOptions.CanResized | ColumnOptions.ReadOnly;
    this.treeListColumn3.StyleName = "Style1";
    this.treeListColumn3.VisibleIndex = 2;
    this.treeListColumn3.Width = 110;
    this.treeListColumn4.Caption = "Сравнение";
    this.treeListColumn4.FieldName = "treeListColumn4";
    this.treeListColumn4.Name = "treeListColumn4";
    this.treeListColumn4.Options = ColumnOptions.CanResized | ColumnOptions.ReadOnly;
    this.treeListColumn4.StyleName = "Style1";
    this.treeListColumn4.VisibleIndex = 3;
    this.treeListColumn4.Width = 96 /*0x60*/;
    this.treeListColumn5.Caption = "Пустые строки";
    this.treeListColumn5.FieldName = "treeListColumn5";
    this.treeListColumn5.Name = "treeListColumn5";
    this.treeListColumn5.Options = ColumnOptions.CanResized | ColumnOptions.ReadOnly;
    this.treeListColumn5.StyleName = "Style1";
    this.treeListColumn5.VisibleIndex = 4;
    this.treeListColumn5.Width = 101;
    this._imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imageList.ImageStream");
    this._imageList.TransparentColor = Color.Transparent;
    this._imageList.Images.SetKeyName(0, "");
    this._imageList.Images.SetKeyName(1, "");
    this._imageList.Images.SetKeyName(2, "");
    this.groupBox1.Anchor = AnchorStyles.Bottom;
    this.groupBox1.Controls.Add((Control) this._comboBoxAlign);
    this.groupBox1.Controls.Add((Control) this._comboBoxEmptyRecord);
    this.groupBox1.Controls.Add((Control) this._comboBoxCompareType);
    this.groupBox1.Controls.Add((Control) this.label1);
    this.groupBox1.Controls.Add((Control) this.label4);
    this.groupBox1.Controls.Add((Control) this.label5);
    this.groupBox1.FlatStyle = FlatStyle.System;
    this.groupBox1.Location = new Point(461, 214);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(211, 97);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Дополнительно";
    this._comboBoxAlign.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._comboBoxAlign.EditValue = (object) 0;
    this._comboBoxAlign.Location = new Point(93, 70);
    this._comboBoxAlign.Name = "_comboBoxAlign";
    this._comboBoxAlign.Properties.AutoComplete = false;
    this._comboBoxAlign.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxAlign.Properties.Items.AddRange(new ImageComboBoxItem[2]
    {
      new ImageComboBoxItem("По возрастанию", (object) 0, -1),
      new ImageComboBoxItem("По убыванию", (object) 1, -1)
    });
    this._comboBoxAlign.Properties.PopupSizeable = true;
    this._comboBoxAlign.Size = new Size(113, 23);
    this._comboBoxAlign.TabIndex = 2;
    this._comboBoxAlign.ToolTip = "Направление сортировки";
    this._comboBoxAlign.SelectedIndexChanged += new EventHandler(this._comboBoxAlign_SelectedIndexChanged);
    this._comboBoxAlign.CloseUp += new CloseUpEventHandler(this._comboBoxAlign_CloseUp);
    this._comboBoxEmptyRecord.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._comboBoxEmptyRecord.EditValue = (object) 0;
    this._comboBoxEmptyRecord.Location = new Point(93, 44);
    this._comboBoxEmptyRecord.Name = "_comboBoxEmptyRecord";
    this._comboBoxEmptyRecord.Properties.AutoComplete = false;
    this._comboBoxEmptyRecord.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxEmptyRecord.Properties.Items.AddRange(new ImageComboBoxItem[2]
    {
      new ImageComboBoxItem("В начало", (object) 0, -1),
      new ImageComboBoxItem("В конец", (object) 1, -1)
    });
    this._comboBoxEmptyRecord.Properties.PopupSizeable = true;
    this._comboBoxEmptyRecord.Size = new Size(113, 23);
    this._comboBoxEmptyRecord.TabIndex = 1;
    this._comboBoxEmptyRecord.ToolTip = "Куда должны помещаться пустые значения при сортировке";
    this._comboBoxEmptyRecord.SelectedIndexChanged += new EventHandler(this._comboBoxEmptyRecord_SelectedIndexChanged);
    this._comboBoxEmptyRecord.CloseUp += new CloseUpEventHandler(this._comboBoxEmptyRecord_CloseUp);
    this._comboBoxCompareType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._comboBoxCompareType.EditValue = (object) 0;
    this._comboBoxCompareType.Location = new Point(93, 17);
    this._comboBoxCompareType.Name = "_comboBoxCompareType";
    this._comboBoxCompareType.Properties.AutoComplete = false;
    this._comboBoxCompareType.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this._comboBoxCompareType.Properties.Items.AddRange(new ImageComboBoxItem[2]
    {
      new ImageComboBoxItem("Символьное", (object) 0, -1),
      new ImageComboBoxItem("Числовое", (object) 1, -1)
    });
    this._comboBoxCompareType.Properties.PopupSizeable = true;
    this._comboBoxCompareType.Size = new Size(113, 23);
    this._comboBoxCompareType.TabIndex = 0;
    this._comboBoxCompareType.ToolTip = "Как производится сравнение цифр в тексте: посимвольно или последовательные цифры определяются как число";
    this._comboBoxCompareType.SelectedIndexChanged += new EventHandler(this._comboBoxCompareType_SelectedIndexChanged);
    this._comboBoxCompareType.CloseUp += new CloseUpEventHandler(this._comboBoxCompareType_CloseUp);
    this.label1.FlatStyle = FlatStyle.System;
    this.label1.Location = new Point(8, 71);
    this.label1.Name = "label1";
    this.label1.Size = new Size(80 /*0x50*/, 22);
    this.label1.TabIndex = 2;
    this.label1.Text = "Расположение:";
    this.label1.TextAlign = ContentAlignment.MiddleRight;
    this.label4.FlatStyle = FlatStyle.System;
    this.label4.Location = new Point(8, 45);
    this.label4.Name = "label4";
    this.label4.Size = new Size(80 /*0x50*/, 22);
    this.label4.TabIndex = 1;
    this.label4.Text = "Пустую запись:";
    this.label4.TextAlign = ContentAlignment.MiddleRight;
    this.label5.FlatStyle = FlatStyle.System;
    this.label5.Location = new Point(8, 19);
    this.label5.Name = "label5";
    this.label5.Size = new Size(80 /*0x50*/, 22);
    this.label5.TabIndex = 0;
    this.label5.Text = "Тип сравнения:";
    this.label5.TextAlign = ContentAlignment.MiddleRight;
    this.label8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.label8.ImageAlign = ContentAlignment.MiddleLeft;
    this.label8.Location = new Point(30, 189);
    this.label8.Name = "label8";
    this.label8.Size = new Size(67, 20);
    this.label8.TabIndex = 37;
    this.label8.Text = "Атрибут:";
    this.label8.TextAlign = ContentAlignment.MiddleLeft;
    this._editAttribute.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._editAttribute.EditValue = (object) "";
    this._editAttribute.Location = new Point(82, 189);
    this._editAttribute.Name = "_editAttribute";
    this._editAttribute.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._editAttribute.Properties.ReadOnly = true;
    this._editAttribute.Size = new Size(424, 20);
    this._editAttribute.TabIndex = 3;
    this._editAttribute.ToolTip = "Атрибут, по которому будет производиться сортировка";
    this._editAttribute.ButtonClick += new ButtonPressedEventHandler(this._editAttribute_ButtonClick);
    this.cbSortPartForPodborAfterBasePart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.cbSortPartForPodborAfterBasePart.AutoSize = true;
    this.cbSortPartForPodborAfterBasePart.Location = new Point(33, 317);
    this.cbSortPartForPodborAfterBasePart.Name = "cbSortPartForPodborAfterBasePart";
    this.cbSortPartForPodborAfterBasePart.Size = new Size(400, 17);
    this.cbSortPartForPodborAfterBasePart.TabIndex = 38;
    this.cbSortPartForPodborAfterBasePart.Text = "Размещать компоненты для подбора рядом с основными компонентами";
    this.cbSortPartForPodborAfterBasePart.UseVisualStyleBackColor = true;
    this.cbSortPartForPodborAfterBasePart.CheckedChanged += new EventHandler(this.cbSortPartForPodborAfterBasePart_CheckedChanged);
    this.cbSortDocumentsByType.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.cbSortDocumentsByType.AutoSize = true;
    this.cbSortDocumentsByType.Location = new Point(33, 339);
    this.cbSortDocumentsByType.Name = "cbSortDocumentsByType";
    this.cbSortDocumentsByType.Size = new Size(199, 17);
    this.cbSortDocumentsByType.TabIndex = 39;
    this.cbSortDocumentsByType.Text = "Сортировать документы по типам\r\n";
    this.cbSortDocumentsByType.UseVisualStyleBackColor = true;
    this.cbSortDocumentsByType.CheckedChanged += new EventHandler(this.cbSortDocumentsByType_CheckedChanged);
    this.Controls.Add((Control) this.cbSortDocumentsByType);
    this.Controls.Add((Control) this.cbSortPartForPodborAfterBasePart);
    this.Controls.Add((Control) this._editAttribute);
    this.Controls.Add((Control) this.label8);
    this.Controls.Add((Control) this._BtnReset);
    this.Controls.Add((Control) this._btnMoveDown);
    this.Controls.Add((Control) this._btnMoveUp);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this._treeListSortSchema);
    this.Controls.Add((Control) this._comboBoxListSource);
    this.Controls.Add((Control) this._btnDelUslov);
    this.Controls.Add((Control) this._btnAddUslov);
    this.Controls.Add((Control) this._label1);
    this.Controls.Add((Control) this._GroupBoxFinishSubstr);
    this.Controls.Add((Control) this._GroupBoxStartSubstr);
    this.Controls.Add((Control) this.label3);
    this.MinimumSize = new Size(655, 320);
    this.Name = nameof (UserControlSortingSetup);
    this.Size = new Size(704, 389);
    this.Load += new EventHandler(this.UserControlSortingSetup_Load);
    this._GroupBoxFinishSubstr.ResumeLayout(false);
    this._comboBoxSubstrFinishSymbol.Properties.EndInit();
    this._upDownSubstrFinishNumber.Properties.EndInit();
    this._comboBoxSubstrFinishAt.Properties.EndInit();
    this._GroupBoxStartSubstr.ResumeLayout(false);
    this._comboBoxSubstrStartSymbol.Properties.EndInit();
    this._upDownSubstrStartNumber.Properties.EndInit();
    this._comboBoxSubstrStartFrom.Properties.EndInit();
    this._comboBoxListSource.Properties.EndInit();
    this._treeListSortSchema.EndInit();
    this.groupBox1.ResumeLayout(false);
    this._comboBoxAlign.Properties.EndInit();
    this._comboBoxEmptyRecord.Properties.EndInit();
    this._comboBoxCompareType.Properties.EndInit();
    this._editAttribute.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary> Обновить сфокусированную ветку дерева </summary>
  private void RefreshFocusedTreeListItem()
  {
    TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
    if (focusedNode == null)
      return;
    (focusedNode.Tag is UserControlSortingSetup.NodeDescriptor tag ? tag : (UserControlSortingSetup.NodeDescriptor) null)?.RefreshNode();
  }

  /// <summary> Получить ветку дерева по идентификатору раздела спецификации. Может вернуть null !!! </summary>
  /// <param name="selectedSectionID"> Идентификатор раздела спецификации </param>
  /// <returns> Ветка дерева. Может вернуть null !!! </returns>
  private TreeListNode GetSectionNode(long selectedSectionID)
  {
    foreach (UserControlSortingSetup.NodeDescriptor nodeDescriptor in this._nodeDescriptors)
    {
      if (nodeDescriptor.SectionDescriptor != null && nodeDescriptor.SectionDescriptor.SectionID == selectedSectionID)
        return nodeDescriptor.Node;
    }
    return (TreeListNode) null;
  }

  /// <summary> Получить ветку дерева по ссылке на заголовок ведомости. Может вернуть null !!! </summary>
  /// <param name="triple"> Ссылка на раздел ведомости </param>
  /// <returns> Ветка дерева. Может вернуть null !!! </returns>
  private TreeListNode GetSectionNode(Triple triple)
  {
    foreach (UserControlSortingSetup.NodeDescriptor nodeDescriptor in this._nodeDescriptors)
    {
      if (nodeDescriptor.Triple != null && nodeDescriptor.Triple.From == triple.From && nodeDescriptor.Triple.To == triple.To && nodeDescriptor.Triple.Result == triple.Result)
        return nodeDescriptor.Node;
    }
    return (TreeListNode) null;
  }

  /// <summary> Получить ветку дерева по идентификатору атрибута. Может вернуть null !!! </summary>
  /// <param name="selectedAttributeShemeGuid"> Атрибут </param>
  /// <returns> Ветка дерева. Может вернуть null !!! </returns>
  private TreeListNode GetAttributeNode(Guid selectedAttributeShemeGuid)
  {
    foreach (UserControlSortingSetup.NodeDescriptor nodeDescriptor in this._nodeDescriptors)
    {
      if (nodeDescriptor.AttributeSortSchema != null && nodeDescriptor.AttributeSortSchema.SchemeGuid == selectedAttributeShemeGuid)
        return nodeDescriptor.Node;
    }
    return (TreeListNode) null;
  }

  /// <summary> Определение индекса текстового представления символа в ComboBox-е </summary>
  /// <param name="character"> Символ </param>
  /// <returns> индекс </returns>
  private static int GetComboBoxSymbolElementIndex(char character)
  {
    return Array.IndexOf<char>(UserControlSortingSetup._predefinedSymbols, character);
  }

  /// <summary> Преобразует строку в символ </summary>
  /// <param name="str"> строка </param>
  /// <returns> символ </returns>
  private static char StringToChar(string str)
  {
    if (str == "(пробел)")
      return ' ';
    char[] charArray = str.ToCharArray();
    return charArray.Length == 0 ? ' ' : charArray[0];
  }

  /// <summary> Фокусирует первый нод, если ни один из нодов не сфокусирован </summary>
  private void CheckFocused()
  {
    if (this._treeListSortSchema.Nodes.Count <= 0)
      return;
    TreeListNode treeListNode = (TreeListNode) null;
    foreach (UserControlSortingSetup.NodeDescriptor nodeDescriptor in this._nodeDescriptors)
    {
      if (nodeDescriptor.AttributeSortSchema != null)
      {
        treeListNode = nodeDescriptor.Node;
        break;
      }
    }
    if (treeListNode == null)
    {
      foreach (UserControlSortingSetup.NodeDescriptor nodeDescriptor in this._nodeDescriptors)
      {
        if (nodeDescriptor.SectionDescriptor == null && nodeDescriptor.AttributeSortSchema == null && nodeDescriptor.Triple == null)
        {
          treeListNode = nodeDescriptor.Node;
          break;
        }
      }
    }
    if (treeListNode == null)
    {
      foreach (UserControlSortingSetup.NodeDescriptor nodeDescriptor in this._nodeDescriptors)
      {
        if (nodeDescriptor.SectionDescriptor != null || nodeDescriptor.Triple != null)
        {
          treeListNode = nodeDescriptor.Node;
          break;
        }
      }
    }
    this._treeListSortSchema.FocusedNode = treeListNode;
    if (treeListNode == null)
      return;
    this._treeListSortSchema.TopVisibleNodeIndex = 0;
  }

  /// <summary> Сервис работы с схемами колонок </summary>
  public static IColumnSchemes IColumnSchemes
  {
    [DebuggerStepThrough] get
    {
      return (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    }
  }

  public void AddCustomAttributes(List<AVSColumnScheme> customColumnSchemes)
  {
    this._customColumnSchemes = customColumnSchemes;
  }

  /// <summary> Вызвать диалог выбора атрибута для сфокусированной ветки  дерева правил сортировки </summary>
  /// <returns> True, если был выбран другой атрибут </returns>
  private bool SelectAttribute()
  {
    if (this.ObjTypes == null)
    {
      this.ObjTypes = new List<int>();
      if (this.ActiveSectionID != 0L)
        this.ObjTypes.AddRange((IEnumerable<int>) SpecificationSectionInfo.FindSectionById(this.ActiveSectionID).PartTypes);
      else
        this.ObjTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenID(AvsIDCache.ObjType_Product));
    }
    NodeColumnCollection columnCollection1 = new NodeColumnCollection();
    NodeColumnCollection columnCollection2 = new NodeColumnCollection();
    List<AVSColumnScheme> schemesList = new List<AVSColumnScheme>(4);
    if (!this._customColumnSchemes.IsNullOrEmpty<AVSColumnScheme>())
      schemesList.AddRange((IEnumerable<AVSColumnScheme>) this._customColumnSchemes);
    RelationColumnsScheme relationColumnsScheme = new RelationColumnsScheme();
    relationColumnsScheme.AddRelationTypes((IList<int>) this.RelTypes);
    schemesList.Add((AVSColumnScheme) relationColumnsScheme);
    ObjectColumnsScheme objectColumnsScheme = new ObjectColumnsScheme();
    objectColumnsScheme.AddObjectTypes((IList<int>) this.RelTypes);
    schemesList.Add((AVSColumnScheme) objectColumnsScheme);
    foreach (AVSColumnScheme scheme in schemesList)
    {
      UserControlSortingSetup.IColumnSchemes.Register(scheme.SchemeGuid, (INodeColumnScheme) scheme);
      foreach (object possibleAttributesId in scheme.PossibleAttributesIDs)
        columnCollection1.Add(scheme.CreateColumn(scheme.SchemeGuid, possibleAttributesId));
    }
    Intermech.Navigator.DBObjects.Helper.AddAllColumns(columnCollection1);
    Intermech.Navigator.DBObjects.Helper.AddAllColumnsRelation(columnCollection1);
    AvsRowAttributeInfo info = (AvsRowAttributeInfo) null;
    try
    {
      SelectAttributeDlg selectAttributeDlg = new SelectAttributeDlg(columnCollection1, schemesList);
      if (selectAttributeDlg.ShowDialog() == DialogResult.OK)
      {
        NodeColumn selectedNodeColumn = selectAttributeDlg.SelectedNodeColumn;
        if (selectedNodeColumn.Source is AttributeInfo source)
          info = new AvsRowAttributeInfo(source);
        if (info == null)
        {
          INodeColumnScheme selectedScheme = selectAttributeDlg.SelectedScheme;
          AttributeInfo columnAttributeInfo = selectedScheme != null ? selectedScheme.FindColumnAttributeInfo(selectedNodeColumn) : (AttributeInfo) null;
          if (columnAttributeInfo != null)
          {
            info = new AvsRowAttributeInfo(columnAttributeInfo);
            selectedNodeColumn.Source = (INodeColumnSource) info;
          }
        }
      }
    }
    finally
    {
      foreach (AVSColumnScheme avsColumnScheme in schemesList)
        UserControlSortingSetup.IColumnSchemes.Unregister(avsColumnScheme.SchemeGuid);
    }
    if (info == null)
      return false;
    TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
    UserControlSortingSetup.NodeDescriptor nodeDescriptor1 = focusedNode?.Tag is UserControlSortingSetup.NodeDescriptor tag1 ? tag1 : (UserControlSortingSetup.NodeDescriptor) null;
    UserControlSortingSetup.SectionDescriptor sectionDescriptor = (UserControlSortingSetup.SectionDescriptor) null;
    Triple triple = (Triple) null;
    SectionSortSchema newItem1 = (SectionSortSchema) null;
    if (focusedNode != null)
    {
      if (focusedNode.ParentNode != null)
      {
        UserControlSortingSetup.NodeDescriptor nodeDescriptor2 = focusedNode.ParentNode.Tag is UserControlSortingSetup.NodeDescriptor tag2 ? tag2 : (UserControlSortingSetup.NodeDescriptor) null;
        sectionDescriptor = nodeDescriptor2?.SectionDescriptor;
        triple = nodeDescriptor2?.Triple;
        newItem1 = nodeDescriptor2?.SectionSortSchema;
      }
      else
      {
        sectionDescriptor = nodeDescriptor1?.SectionDescriptor;
        triple = nodeDescriptor1?.Triple;
        newItem1 = nodeDescriptor1?.SectionSortSchema;
      }
    }
    AttributeSortSchema newItem2 = nodeDescriptor1?.AttributeSortSchema;
    if (newItem2 != null && info.AttributeId == newItem2.AttributeID)
      return false;
    if (newItem1 == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sectionDescriptor != null)
        {
          IUserSession session = sessionKeeper.Session;
          SpecificationSectionInfo sectionById = SpecificationSectionInfo.FindSectionById(sectionDescriptor.SectionID);
          Guid sectionGuid = sectionById != null ? sectionById.SectionGuid : Guid.Empty;
          newItem1 = new SectionSortSchema(session, sectionGuid);
        }
        else if (triple != null)
          newItem1 = new SectionSortSchema(triple.Result);
      }
      if (newItem1 != null)
      {
        this._sortSchema.SectionSortSchemas = (SectionSortSchema[]) ArrayEditHelper.AddItemToArray((Array) this._sortSchema.SectionSortSchemas, (object) newItem1);
        ((UserControlSortingSetup.NodeDescriptor) this._treeListSortSchema.FocusedNode.ParentNode.Tag).SectionSortSchema = newItem1;
      }
    }
    if (newItem1 == null)
      return false;
    if (newItem2 == null)
    {
      newItem2 = new AttributeSortSchema();
      newItem1.AttributeSortSchemas = (AttributeSortSchema[]) ArrayEditHelper.AddItemToArray((Array) newItem1.AttributeSortSchemas, (object) newItem2);
      ((UserControlSortingSetup.NodeDescriptor) this._treeListSortSchema.FocusedNode.Tag).AttributeSortSchema = newItem2;
    }
    using (new SessionKeeper())
      newItem2.SetInfo(info);
    ((UserControlSortingSetup.NodeDescriptor) this._treeListSortSchema.FocusedNode.Tag).RefreshNode();
    this.UpdateControls(false);
    return true;
  }

  /// <summary> Вызов диалога редактирования сфокусированного правила сортировки </summary>
  private void EditParam()
  {
    bool wasUpdated = false;
    long activeSectionId = this.ActiveSectionID;
    Guid attributeSchemaGuid = this.FocusedAttributeSchemaGuid;
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && (this._comboBoxListSource.SelectedIndex != 1 || activeSectionId != this.ActiveSectionID || this.FocusedAttributeSchemaGuid != attributeSchemaGuid))
      return;
    TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
    UserControlSortingSetup.NodeDescriptor tag = focusedNode == null || focusedNode.Tag == null || !(focusedNode.Tag is UserControlSortingSetup.NodeDescriptor) ? (UserControlSortingSetup.NodeDescriptor) null : (UserControlSortingSetup.NodeDescriptor) focusedNode.Tag;
    if (tag != null)
    {
      AttributeSortSchema attributeSortSchema = tag.AttributeSortSchema;
    }
    try
    {
      if (!this.SelectAttribute())
        return;
      this.Changed = true;
    }
    finally
    {
      this.UpdateControls(false);
    }
  }

  /// <summary> Проверка, может ли быть изменено значение выбора элемента ComboBox-а </summary>
  /// <param name="comboBox"></param>
  /// <returns></returns>
  private bool CheckCanComboBoxChangeSelectedIndex(ComboBoxEdit comboBox)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
      return true;
    long activeSectionId = this.ActiveSectionID;
    Guid attributeSchemaGuid = this.FocusedAttributeSchemaGuid;
    int selectedIndex = comboBox.SelectedIndex;
    return this._sortSchema != null && this.FocusedAttributeSchemaGuid != Guid.Empty && !this.ControlsAreUpdating && this._comboBoxListSource.SelectedIndex == 1 && this.CheckCanEdit(ref wasUpdated) && (!wasUpdated || !this.ReadOnly && this._comboBoxListSource.SelectedIndex == 1 && selectedIndex == comboBox.SelectedIndex && !comboBox.Properties.ReadOnly && activeSectionId == this.ActiveSectionID && !(attributeSchemaGuid != this.FocusedAttributeSchemaGuid));
  }

  /// <summary> Проверка, может ли быть изменено значение ComboBox-а </summary>
  /// <param name="comboBox"></param>
  /// <param name="e"></param>
  /// <returns></returns>
  private bool CheckCanComboBoxEditValue(ComboBoxEdit comboBox, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
      return true;
    long activeSectionId = this.ActiveSectionID;
    Guid attributeSchemaGuid = this.FocusedAttributeSchemaGuid;
    char ch = e.OldValue == null || e.OldValue.GetType() != typeof (string) ? char.MinValue : UserControlSortingSetup.StringToChar((string) e.OldValue);
    return this.FocusedAttributeSortSchema != null && this._comboBoxListSource.SelectedIndex == 1 && this.CheckCanEdit(ref wasUpdated) && (!wasUpdated || !this.ReadOnly && this._comboBoxListSource.SelectedIndex == 1 && (int) ch == (int) UserControlSortingSetup.StringToChar(comboBox.Text) && !comboBox.Properties.ReadOnly && activeSectionId == this.ActiveSectionID && !(attributeSchemaGuid != this.FocusedAttributeSchemaGuid));
  }

  /// <summary> Проверка, может ли быть изменено значение выбора элемента SpinEdit-а </summary>
  /// <param name="spinEdit"></param>
  /// <param name="e"></param>
  /// <returns></returns>
  private bool CheckCanSpinEditEdited(SpinEdit spinEdit, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
      return true;
    long activeSectionId = this.ActiveSectionID;
    Guid attributeSchemaGuid = this.FocusedAttributeSchemaGuid;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    return this.FocusedAttributeSortSchema != null && this._comboBoxListSource.SelectedIndex == 1 && this.CheckCanEdit(ref wasUpdated) && (!wasUpdated || !this.ReadOnly && this._comboBoxListSource.SelectedIndex == 1 && oldValue == Decimal.ToInt32(spinEdit.Value) && !spinEdit.Properties.ReadOnly && activeSectionId == this.ActiveSectionID && !(attributeSchemaGuid != this.FocusedAttributeSchemaGuid));
  }

  private bool BeginControlChanges(Control control)
  {
    if (this._sortSchema == null || this.ControlsAreUpdating || this.FocusedAttributeSortSchema == null)
      return false;
    this._changedControl = control;
    return true;
  }

  private void FinishContolChanges()
  {
    this._sortSchema.Changed = true;
    this.Changed = true;
    this.UpdateControls(false);
    this._changedControl = (Control) null;
    this.RefreshFocusedTreeListItem();
  }

  public List<long> GetSections()
  {
    long templateId = this.SpecificationTemplateObjectId.IsUndefinedId() ? this.SettingsHolderObjectId : this.SpecificationTemplateObjectId;
    List<SpecificationSectionInfo> specificationSectionInfoList = SpecificationSectionInfo.GetAllowableSpecSections(templateId);
    if (specificationSectionInfoList == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        specificationSectionInfoList = SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session, templateId, new AVSDocumentType?());
    }
    List<long> sections = new List<long>();
    foreach (SpecificationSectionInfo specificationSectionInfo in specificationSectionInfoList)
      sections.Add(specificationSectionInfo.SectionID);
    return sections;
  }

  private void UpdateSection(TreeListNode sectionNode, SectionSortSchema sectionSortSchema)
  {
    this._treeListSortSchema.BeginUpdate();
    try
    {
      sectionNode.Nodes.Clear();
      foreach (AttributeSortSchema attributeSortSchema in sectionSortSchema.AttributeSortSchemas)
      {
        if (attributeSortSchema != null)
        {
          UserControlSortingSetup.NodeDescriptor nodeDescriptor = new UserControlSortingSetup.NodeDescriptor(this, this._treeListSortSchema.AppendNode((object) new object[5]
          {
            (object) string.Empty,
            (object) string.Empty,
            (object) string.Empty,
            (object) string.Empty,
            (object) string.Empty
          }, sectionNode), attributeSortSchema);
        }
      }
    }
    finally
    {
      this._treeListSortSchema.EndUpdate();
    }
  }

  /// <summary> Обновление содержимого визульного дерева настроек </summary>
  public void ReloadSchemaTree()
  {
    if (this._sortSchema == null)
      return;
    Guid empty = Guid.Empty;
    int num1 = this._treeListSortSchema.TopVisibleNodeIndex;
    TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
    UserControlSortingSetup.NodeDescriptor tag1 = focusedNode == null || focusedNode.Tag == null || !(focusedNode.Tag is UserControlSortingSetup.NodeDescriptor) ? (UserControlSortingSetup.NodeDescriptor) null : (UserControlSortingSetup.NodeDescriptor) focusedNode.Tag;
    UserControlSortingSetup.SectionDescriptor sectionDescriptor1 = tag1?.SectionDescriptor;
    Triple triple1 = tag1?.Triple;
    AttributeSortSchema attributeSortSchema1 = tag1?.AttributeSortSchema;
    bool flag = false;
    long sectionId = sectionDescriptor1 != null ? sectionDescriptor1.SectionID : 0L;
    Guid selectedAttributeShemeGuid = attributeSortSchema1 != null ? attributeSortSchema1.SchemeGuid : Guid.Empty;
    if (tag1 != null && sectionDescriptor1 == null && triple1 == null && attributeSortSchema1 == null)
    {
      TreeListNode parentNode = focusedNode.ParentNode;
      UserControlSortingSetup.NodeDescriptor tag2 = parentNode == null || parentNode.Tag == null || !(parentNode.Tag is UserControlSortingSetup.NodeDescriptor) ? (UserControlSortingSetup.NodeDescriptor) null : (UserControlSortingSetup.NodeDescriptor) parentNode.Tag;
      UserControlSortingSetup.SectionDescriptor sectionDescriptor2 = tag2?.SectionDescriptor;
      Triple triple2 = tag2?.Triple;
      if (sectionDescriptor2 != null)
      {
        flag = true;
        sectionId = sectionDescriptor2.SectionID;
      }
      else if (triple2 != null)
        flag = true;
    }
    this.LockControls();
    try
    {
      Application.DoEvents();
      this._nodeDescriptors = (ArrayList) null;
      this._treeListSortSchema.SelectImageList = (ImageList) null;
      if (this._treeListSortSchema.Nodes.Count > 0)
      {
        for (int index = this._treeListSortSchema.Nodes.Count - 1; index >= 0; --index)
          this._treeListSortSchema.Nodes.RemoveAt(index);
      }
      this._nodeDescriptors = new ArrayList();
      this._treeListSortSchema.SelectImageList = this._imageList;
      TreeListNode treeListNode1 = (TreeListNode) null;
      if (this.TripleList != null)
      {
        List<SectionSortSchema> sectionSortSchemaList = new List<SectionSortSchema>(this.TripleList.Count);
        foreach (Triple triple3 in this.TripleList)
        {
          SectionSortSchema sectionSortSchema = this.SortSchema != null ? this.SortSchema.GetSectionSchemaByTripleName(triple3.Result) : (SectionSortSchema) null;
          treeListNode1 = (TreeListNode) null;
          if (sectionSortSchema == null)
          {
            sectionSortSchema = new SectionSortSchema(triple3.Result);
            sectionSortSchemaList.Add(sectionSortSchema);
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              sectionSortSchema.LoadDefaultVedomostiSchema(sessionKeeper.Session);
          }
          TreeListNode treeListNode2 = this._treeListSortSchema.AppendNode((object) new object[3]
          {
            (object) string.Empty,
            (object) string.Empty,
            (object) string.Empty
          }, (TreeListNode) null);
          UserControlSortingSetup.NodeDescriptor nodeDescriptor1 = new UserControlSortingSetup.NodeDescriptor(this, treeListNode2, triple3, sectionSortSchema);
          foreach (AttributeSortSchema attributeSortSchema2 in sectionSortSchema.AttributeSortSchemas)
          {
            if (attributeSortSchema2 != null)
            {
              UserControlSortingSetup.NodeDescriptor nodeDescriptor2 = new UserControlSortingSetup.NodeDescriptor(this, this._treeListSortSchema.AppendNode((object) new object[5]
              {
                (object) string.Empty,
                (object) string.Empty,
                (object) string.Empty,
                (object) string.Empty,
                (object) string.Empty
              }, treeListNode2), attributeSortSchema2);
            }
          }
          if (treeListNode2 != null)
          {
            if (!treeListNode2.HasChildren)
            {
              UserControlSortingSetup.NodeDescriptor nodeDescriptor3 = new UserControlSortingSetup.NodeDescriptor(this, this._treeListSortSchema.AppendNode((object) new object[5]
              {
                (object) string.Empty,
                (object) string.Empty,
                (object) string.Empty,
                (object) string.Empty,
                (object) string.Empty
              }, treeListNode2));
            }
            treeListNode2.Expanded = true;
          }
        }
        if (sectionSortSchemaList.Count > 0)
        {
          SectionSortSchema[] sectionSortSchemaArray = new SectionSortSchema[this._sortSchema._sectionSortSchemas.Length + sectionSortSchemaList.Count];
          this._sortSchema._sectionSortSchemas.CopyTo((Array) sectionSortSchemaArray, 0);
          int num2 = this._sortSchema._sectionSortSchemas.Length + 1;
          foreach (SectionSortSchema sectionSortSchema in sectionSortSchemaList)
          {
            sectionSortSchemaArray[num2 - 1] = sectionSortSchema;
            ++num2;
          }
          this._sortSchema._sectionSortSchemas = sectionSortSchemaArray;
        }
      }
      else
      {
        List<ShortObjectDecription> objectDecriptionList = new List<ShortObjectDecription>();
        if (this.SortSchema.SectionSortSchemas.Length == 1 && this.SortSchema.SectionSortSchemas[0].SectionGuid == AvsIDCache.ObjIdElementListSortChapterGuid)
        {
          objectDecriptionList.Add(new ShortObjectDecription(0L, this.SortSchema.SectionSortSchemas[0].SectionName));
        }
        else
        {
          long templateId = this.SpecificationTemplateObjectId.IsUndefinedId() ? this.SettingsHolderObjectId : this.SpecificationTemplateObjectId;
          List<SpecificationSectionInfo> source = SpecificationSectionInfo.GetAllowableSpecSections(templateId);
          if (source == null)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              source = SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session, templateId, new AVSDocumentType?());
          }
          objectDecriptionList = source.Select<SpecificationSectionInfo, ShortObjectDecription>((Func<SpecificationSectionInfo, ShortObjectDecription>) (s => new ShortObjectDecription(s.SectionID, s.Caption))).ToList<ShortObjectDecription>();
        }
        List<long> sections = this.GetSections();
        foreach (ShortObjectDecription objectDecription in objectDecriptionList)
        {
          if (sections.Contains(objectDecription.ObjID) || objectDecription.ObjID == 0L)
          {
            SectionSortSchema sectionSortSchema1;
            if (objectDecription.ObjID == 0L)
            {
              sectionSortSchema1 = this.SortSchema?.SectionSortSchemas[0];
            }
            else
            {
              SortSchema sortSchema = this.SortSchema;
              if (sortSchema == null)
              {
                sectionSortSchema1 = (SectionSortSchema) null;
              }
              else
              {
                SpecificationSectionInfo sectionById = SpecificationSectionInfo.FindSectionById(objectDecription.ObjID);
                sectionSortSchema1 = sortSchema.GetSectionSchemaBySectionGuid(sectionById != null ? sectionById.SectionGuid : Guid.Empty);
              }
            }
            SectionSortSchema sectionSortSchema2 = sectionSortSchema1;
            TreeListNode treeListNode3 = this._treeListSortSchema.AppendNode((object) new object[3]
            {
              (object) string.Empty,
              (object) string.Empty,
              (object) string.Empty
            }, (TreeListNode) null);
            UserControlSortingSetup.SectionDescriptor sectionDescriptor3 = new UserControlSortingSetup.SectionDescriptor(objectDecription.ObjID, objectDecription.ObjCaption);
            UserControlSortingSetup.NodeDescriptor nodeDescriptor4 = new UserControlSortingSetup.NodeDescriptor(this, treeListNode3, sectionDescriptor3, sectionSortSchema2);
            if (sectionSortSchema2 != null)
            {
              foreach (AttributeSortSchema attributeSortSchema3 in sectionSortSchema2.AttributeSortSchemas)
              {
                if (attributeSortSchema3 != null)
                {
                  UserControlSortingSetup.NodeDescriptor nodeDescriptor5 = new UserControlSortingSetup.NodeDescriptor(this, this._treeListSortSchema.AppendNode((object) new object[5]
                  {
                    (object) string.Empty,
                    (object) string.Empty,
                    (object) string.Empty,
                    (object) string.Empty,
                    (object) string.Empty
                  }, treeListNode3), attributeSortSchema3);
                }
              }
            }
            if (treeListNode3 != null)
            {
              if (!treeListNode3.HasChildren)
              {
                UserControlSortingSetup.NodeDescriptor nodeDescriptor6 = new UserControlSortingSetup.NodeDescriptor(this, this._treeListSortSchema.AppendNode((object) new object[5]
                {
                  (object) string.Empty,
                  (object) string.Empty,
                  (object) string.Empty,
                  (object) string.Empty,
                  (object) string.Empty
                }, treeListNode3));
              }
              treeListNode3.Expanded = true;
            }
          }
        }
      }
      if (sectionId == 0L && selectedAttributeShemeGuid == Guid.Empty)
      {
        this.CheckFocused();
      }
      else
      {
        TreeListNode treeListNode4 = (TreeListNode) null;
        if (sectionId != 0L)
        {
          treeListNode4 = this.GetSectionNode(sectionId);
          if (treeListNode4 != null & flag && treeListNode4.HasChildren && treeListNode4.Nodes.Count > 0)
            treeListNode4 = treeListNode4.Nodes[0];
        }
        else if (selectedAttributeShemeGuid != Guid.Empty)
          treeListNode4 = this.GetAttributeNode(selectedAttributeShemeGuid);
        if (treeListNode4 != null)
          this._treeListSortSchema.FocusedNode = treeListNode4;
      }
      if (this._treeListSortSchema.FocusedNode == null)
        this.CheckFocused();
      if (num1 < 0)
        return;
      if (num1 > this._treeListSortSchema.Nodes.Count - this._treeListSortSchema.VisibleNodesCount)
        num1 = this._treeListSortSchema.Nodes.Count - this._treeListSortSchema.VisibleNodesCount;
      if (num1 <= 0 || num1 > this._treeListSortSchema.Nodes.Count - this._treeListSortSchema.VisibleNodesCount)
        return;
      this._treeListSortSchema.TopVisibleNodeIndex = num1;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary> Сервисная функция. Добавляет созданный дескриптор в кэш </summary>
  /// <param name="nodeDescriptor"> Дескриптор ветки настроек </param>
  public void AfterAddNewNodeDescriptor(
    UserControlSortingSetup.NodeDescriptor nodeDescriptor)
  {
    if (this._nodeDescriptors == null)
      return;
    this._nodeDescriptors.Add((object) nodeDescriptor);
  }

  /// <summary> Сервисная функция. Удаляет дескриптор из кэша </summary>
  /// <param name="nodeDescriptor"> Дескриптор ветки настроек </param>
  public void AfterDisposeNodeDescriptor(
    UserControlSortingSetup.NodeDescriptor nodeDescriptor)
  {
    if (this._nodeDescriptors == null)
      return;
    this._nodeDescriptors.Remove((object) nodeDescriptor);
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    if (this._editModeToolTip != null)
    {
      if (this.ReadOnly)
      {
        if (this._editModeToolTip.Active)
        {
          this._editModeToolTip.Active = false;
          this._readModeToolTip.Active = true;
        }
      }
      else if (this._readModeToolTip.Active)
      {
        this._editModeToolTip.Active = true;
        this._readModeToolTip.Active = false;
      }
    }
    int num = this._sortSchema == null ? 0 : (this._sortSchema.Changed ? 1 : (this._sortSchema.ParentLevel != null ? 1 : 0));
    this._comboBoxListSource.Visible = this._sortSchema != null && this._sortSchema.ParentLevel != null && this.SettingsStructure != null && this.SettingsStructure.AllLevels != null && this.SettingsStructure.AllLevels.Length > 1;
    this.label3.Visible = this._comboBoxListSource.Visible;
    if (this._comboBoxListSource.Visible)
    {
      this._comboBoxListSource.Properties.ReadOnly = this.ReadOnly;
      this._comboBoxListSource.BackColor = this._comboBoxListSource.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
      this._comboBoxListSource.Properties.Buttons[0].Visible = !this._comboBoxListSource.Properties.ReadOnly;
      this._comboBoxListSource.SelectedIndex = this._sortSchema.Changed ? 1 : 0;
    }
    else
      this._comboBoxListSource.SelectedIndex = 1;
    this._BtnReset.Visible = !this._comboBoxListSource.Visible;
    this._BtnReset.Enabled = !this.ReadOnly && this._BtnReset.Visible;
    Color color = this._comboBoxListSource.SelectedIndex != 1 || this.ReadOnly ? Color.WhiteSmoke : SystemColors.Window;
    if (color != this._treeListSortSchema.BackColor)
    {
      this._treeListSortSchema.ViewStylesInfo.Empty.BackColor = color;
      this._treeListSortSchema.ViewStylesInfo.Row.BackColor = color;
      this._treeListSortSchema.BackColor = color;
      this._treeListSortSchema.Styles["Style1"].BackColor = color;
      this._treeListSortSchema.Styles["Style2"].BackColor = color;
    }
    TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
    UserControlSortingSetup.NodeDescriptor tag = focusedNode == null || focusedNode.Tag == null || !(focusedNode.Tag is UserControlSortingSetup.NodeDescriptor) ? (UserControlSortingSetup.NodeDescriptor) null : (UserControlSortingSetup.NodeDescriptor) focusedNode.Tag;
    UserControlSortingSetup.SectionDescriptor sectionDescriptor = tag?.SectionDescriptor;
    Triple triple = tag?.Triple;
    if (tag != null)
    {
      SectionSortSchema sectionSortSchema = tag.SectionSortSchema;
    }
    AttributeSortSchema attributeSortSchema = tag?.AttributeSortSchema;
    bool flag1 = this._comboBoxListSource.SelectedIndex == 1 && !this.ReadOnly && tag != null;
    bool flag2 = flag1 && attributeSortSchema != null;
    this._btnAddUslov.Enabled = flag1;
    this._btnDelUslov.Enabled = flag2;
    this._btnMoveUp.Enabled = flag2 && focusedNode.ParentNode.Nodes.IndexOf(focusedNode) > 0;
    this._btnMoveDown.Enabled = flag2 && focusedNode.ParentNode.Nodes.IndexOf(focusedNode) < focusedNode.ParentNode.Nodes.Count - 1;
    this._comboBoxSubstrStartFrom.Properties.ReadOnly = !flag2;
    this._comboBoxSubstrFinishAt.Properties.ReadOnly = !flag2;
    this._comboBoxCompareType.Properties.ReadOnly = !flag2;
    this._comboBoxEmptyRecord.Properties.ReadOnly = !flag2;
    this._comboBoxAlign.Properties.ReadOnly = !flag2;
    this._upDownSubstrStartNumber.Properties.ReadOnly = !flag2 || attributeSortSchema == null || attributeSortSchema.SubstringStartType != SubstringStartFinishType.FromEndFoundNSubstring && attributeSortSchema.SubstringStartType != SubstringStartFinishType.FromNFoundSubstring && attributeSortSchema.SubstringStartType != SubstringStartFinishType.FromNPosition;
    this._upDownSubstrFinishNumber.Properties.ReadOnly = !flag2 || attributeSortSchema == null || attributeSortSchema.SubstringEndType != SubstringStartFinishType.FromEndFoundNSubstring && attributeSortSchema.SubstringEndType != SubstringStartFinishType.FromNFoundSubstring && attributeSortSchema.SubstringEndType != SubstringStartFinishType.FromNPosition;
    this._comboBoxSubstrStartSymbol.Properties.ReadOnly = !flag2 || attributeSortSchema == null || attributeSortSchema.SubstringStartType != SubstringStartFinishType.FromEndFoundNSubstring && attributeSortSchema.SubstringStartType != SubstringStartFinishType.FromNFoundSubstring;
    this._comboBoxSubstrFinishSymbol.Properties.ReadOnly = !flag2 || attributeSortSchema == null || attributeSortSchema.SubstringEndType != SubstringStartFinishType.FromEndFoundNSubstring && attributeSortSchema.SubstringEndType != SubstringStartFinishType.FromNFoundSubstring;
    this._editAttribute.Properties.Buttons[0].Visible = flag1 && (attributeSortSchema != null || tag != null && sectionDescriptor == null && triple == null);
    this._comboBoxSubstrStartFrom.Properties.Buttons[0].Visible = !this._comboBoxSubstrStartFrom.Properties.ReadOnly;
    this._comboBoxSubstrFinishAt.Properties.Buttons[0].Visible = !this._comboBoxSubstrFinishAt.Properties.ReadOnly;
    this._comboBoxCompareType.Properties.Buttons[0].Visible = !this._comboBoxCompareType.Properties.ReadOnly;
    this._comboBoxEmptyRecord.Properties.Buttons[0].Visible = !this._comboBoxEmptyRecord.Properties.ReadOnly;
    this._comboBoxAlign.Properties.Buttons[0].Visible = !this._comboBoxAlign.Properties.ReadOnly;
    this._upDownSubstrStartNumber.Properties.Buttons[0].Visible = !this._upDownSubstrStartNumber.Properties.ReadOnly;
    this._upDownSubstrFinishNumber.Properties.Buttons[0].Visible = !this._upDownSubstrFinishNumber.Properties.ReadOnly;
    this._comboBoxSubstrStartSymbol.Properties.Buttons[0].Visible = !this._comboBoxSubstrStartSymbol.Properties.ReadOnly;
    this._comboBoxSubstrFinishSymbol.Properties.Buttons[0].Visible = !this._comboBoxSubstrFinishSymbol.Properties.ReadOnly;
    this._editAttribute.BackColor = !this._editAttribute.Properties.Buttons[0].Visible ? SystemColors.Control : SystemColors.Window;
    this._comboBoxSubstrStartFrom.BackColor = this._comboBoxSubstrStartFrom.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._comboBoxSubstrFinishAt.BackColor = this._comboBoxSubstrFinishAt.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._comboBoxCompareType.BackColor = this._comboBoxCompareType.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._comboBoxEmptyRecord.BackColor = this._comboBoxEmptyRecord.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._comboBoxAlign.BackColor = this._comboBoxAlign.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownSubstrStartNumber.BackColor = this._upDownSubstrStartNumber.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._upDownSubstrFinishNumber.BackColor = this._upDownSubstrFinishNumber.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._comboBoxSubstrStartSymbol.BackColor = this._comboBoxSubstrStartSymbol.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._comboBoxSubstrFinishSymbol.BackColor = this._comboBoxSubstrFinishSymbol.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    this._comboBoxListSource.BackColor = this._comboBoxListSource.Properties.ReadOnly ? SystemColors.Control : SystemColors.Window;
    if (this._changedControl != this._editAttribute)
    {
      if (attributeSortSchema == null)
      {
        if (tag != null && sectionDescriptor == null && triple == null)
        {
          if (this._comboBoxListSource.SelectedIndex != 1 || this.ReadOnly)
            this._editAttribute.Text = "{ Атрибут не выбран }";
          else
            this._editAttribute.Text = "{ Выберите атрибут }";
        }
        else
          this._editAttribute.Text = string.Empty;
      }
      else
        this._editAttribute.Text = attributeSortSchema.AttributeNameAndSource;
    }
    if (this._changedControl != this._comboBoxSubstrStartFrom)
    {
      if (attributeSortSchema == null || attributeSortSchema.SubstringStartType == SubstringStartFinishType.Unknow)
        this._comboBoxSubstrStartFrom.Text = string.Empty;
      else
        this._comboBoxSubstrStartFrom.SelectedIndex = (int) (attributeSortSchema.SubstringStartType - 1);
    }
    if (this._changedControl != this._comboBoxSubstrFinishAt)
    {
      if (attributeSortSchema == null || attributeSortSchema.SubstringEndType == SubstringStartFinishType.Unknow)
        this._comboBoxSubstrFinishAt.Text = string.Empty;
      else
        this._comboBoxSubstrFinishAt.SelectedIndex = (int) (attributeSortSchema.SubstringEndType - 1);
    }
    if (this._changedControl != this._comboBoxCompareType)
    {
      if (attributeSortSchema == null || attributeSortSchema.CompareType == CompareType.Unknow)
        this._comboBoxCompareType.Text = string.Empty;
      else
        this._comboBoxCompareType.SelectedIndex = (int) (attributeSortSchema.CompareType - 1);
    }
    if (this._changedControl != this._comboBoxEmptyRecord)
    {
      if (attributeSortSchema == null || attributeSortSchema.EmptyOrder == EmptyOrder.Unknow)
        this._comboBoxEmptyRecord.Text = string.Empty;
      else
        this._comboBoxEmptyRecord.SelectedIndex = (int) (attributeSortSchema.EmptyOrder - 1);
    }
    if (this._changedControl != this._comboBoxAlign)
    {
      if (attributeSortSchema == null || attributeSortSchema.SortOrder == Intermech.Interfaces.AVS.SortOrder.Unknow)
        this._comboBoxAlign.Text = string.Empty;
      else
        this._comboBoxAlign.SelectedIndex = (int) (attributeSortSchema.SortOrder - 1);
    }
    if (this._changedControl != this._upDownSubstrStartNumber)
    {
      if (attributeSortSchema == null || attributeSortSchema.SubstringStartType != SubstringStartFinishType.FromEndFoundNSubstring && attributeSortSchema.SubstringStartType != SubstringStartFinishType.FromNFoundSubstring && attributeSortSchema.SubstringStartType != SubstringStartFinishType.FromNPosition)
        this._upDownSubstrStartNumber.Text = string.Empty;
      else
        this._upDownSubstrStartNumber.Value = (Decimal) attributeSortSchema.StartPosition;
    }
    if (this._changedControl != this._upDownSubstrFinishNumber)
    {
      if (attributeSortSchema == null || attributeSortSchema.SubstringEndType != SubstringStartFinishType.FromEndFoundNSubstring && attributeSortSchema.SubstringEndType != SubstringStartFinishType.FromNFoundSubstring && attributeSortSchema.SubstringEndType != SubstringStartFinishType.FromNPosition)
        this._upDownSubstrFinishNumber.Text = string.Empty;
      else
        this._upDownSubstrFinishNumber.Value = (Decimal) attributeSortSchema.EndPosition;
    }
    if (this._changedControl != this._comboBoxSubstrStartSymbol)
    {
      if (attributeSortSchema == null || attributeSortSchema.SubstringStartType != SubstringStartFinishType.FromEndFoundNSubstring && attributeSortSchema.SubstringStartType != SubstringStartFinishType.FromNFoundSubstring)
      {
        this._comboBoxSubstrStartSymbol.Text = string.Empty;
      }
      else
      {
        char character = UserControlSortingSetup.StringToChar(attributeSortSchema.StartSubstring);
        int symbolElementIndex = UserControlSortingSetup.GetComboBoxSymbolElementIndex(character);
        if (symbolElementIndex != -1)
          this._comboBoxSubstrStartSymbol.SelectedIndex = symbolElementIndex;
        else
          this._comboBoxSubstrStartSymbol.EditValue = (object) character.ToString();
      }
    }
    if (this._changedControl != this._comboBoxSubstrFinishSymbol)
    {
      if (attributeSortSchema == null || attributeSortSchema.SubstringEndType != SubstringStartFinishType.FromEndFoundNSubstring && attributeSortSchema.SubstringEndType != SubstringStartFinishType.FromNFoundSubstring)
      {
        this._comboBoxSubstrFinishSymbol.Text = string.Empty;
      }
      else
      {
        char character = UserControlSortingSetup.StringToChar(attributeSortSchema.EndSubstring);
        int symbolElementIndex = UserControlSortingSetup.GetComboBoxSymbolElementIndex(character);
        if (symbolElementIndex != -1)
          this._comboBoxSubstrFinishSymbol.SelectedIndex = symbolElementIndex;
        else
          this._comboBoxSubstrFinishSymbol.EditValue = (object) character.ToString();
      }
    }
    if (this._sortSchema == null)
      return;
    this.cbSortPartForPodborAfterBasePart.Checked = this._sortSchema.SortPartForPodborAfterBasePart;
    this.cbSortDocumentsByType.Checked = this._sortSchema.SortDocumentsByType;
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  public override bool GetIsReadOnly() => this._sortSchema == null || this._sortSchema.ReadOnly;

  public void CalcNodeHeight(object obj, CalcNodeHeightEventArgs args)
  {
    if (args == null || args.Node == null || args.Node.Tag == null || !(args.Node.Tag is UserControlSortingSetup.NodeDescriptor))
      return;
    UserControlSortingSetup.NodeDescriptor tag = (UserControlSortingSetup.NodeDescriptor) args.Node.Tag;
    if (tag.SectionDescriptor == null && tag.Triple == null)
      return;
    args.NodeHeight = this._treeListSortSchema.RowHeight + 15;
  }

  private void UserControlSortingSetup_Load(object sender, EventArgs e)
  {
    this._comboBoxListSource.Visible = this.SettingsStructure != null && this.SettingsStructure.AllLevels != null && this.SettingsStructure.AllLevels.Length > 1;
    this.label3.Visible = this._comboBoxListSource.Visible;
  }

  private void _treeListSortSchema_CustomDrawNodeCell(
    object sender,
    CustomDrawNodeCellEventArgs args)
  {
    if (args == null || args.Node == null || args.Node.Tag == null || !(args.Node.Tag is UserControlSortingSetup.NodeDescriptor))
      return;
    UserControlSortingSetup.NodeDescriptor tag = (UserControlSortingSetup.NodeDescriptor) args.Node.Tag;
    if (tag.SectionDescriptor == null && tag.Triple == null)
      return;
    args.Style = this._treeListSortSchema.FocusedNode == args.Node ? this._treeListSortSchema.Styles["Style3"] : this._treeListSortSchema.Styles["SectionStyle"];
  }

  private void _treeListSortSchema_BeforeCollapse(object sender, BeforeCollapseEventArgs e)
  {
    e.CanCollapse = false;
  }

  private void _treeListSortSchema_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    this.UpdateControls(false);
  }

  /// <summary> Была нажата нажата кнопка "Добавить" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _btnAddUslov_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    long activeSectionId = this.ActiveSectionID;
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && (this.ReadOnly || this._comboBoxListSource.SelectedIndex != 1 || activeSectionId != this.ActiveSectionID))
      return;
    bool flag1 = false;
    UserControlSortingSetup.NodeDescriptor nodeDescriptor1 = (UserControlSortingSetup.NodeDescriptor) null;
    TreeListNode node = (TreeListNode) null;
    TreeListNode parentNode1 = (TreeListNode) null;
    TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
    UserControlSortingSetup.NodeDescriptor nodeDescriptor2 = focusedNode?.Tag is UserControlSortingSetup.NodeDescriptor tag1 ? tag1 : (UserControlSortingSetup.NodeDescriptor) null;
    UserControlSortingSetup.SectionDescriptor sectionDescriptor = (UserControlSortingSetup.SectionDescriptor) null;
    Triple triple = (Triple) null;
    bool flag2 = false;
    if (focusedNode != null)
    {
      if (focusedNode.ParentNode != null)
      {
        TreeListNode parentNode2 = focusedNode.ParentNode;
        UserControlSortingSetup.NodeDescriptor nodeDescriptor3 = parentNode2.Tag is UserControlSortingSetup.NodeDescriptor tag2 ? tag2 : (UserControlSortingSetup.NodeDescriptor) null;
        sectionDescriptor = nodeDescriptor3?.SectionDescriptor;
        triple = nodeDescriptor3?.Triple;
        if (parentNode2.Nodes.Count == 1 && parentNode2.Nodes[0].Tag != null && parentNode2.Nodes[0].Tag is UserControlSortingSetup.NodeDescriptor && ((UserControlSortingSetup.NodeDescriptor) parentNode2.Nodes[0].Tag).AttributeSortSchema == null)
        {
          if (this._treeListSortSchema.FocusedNode != parentNode2.Nodes[0])
          {
            this._treeListSortSchema.FocusedNode = parentNode2.Nodes[0];
            Application.DoEvents();
          }
          flag2 = true;
        }
      }
      else
      {
        sectionDescriptor = nodeDescriptor2?.SectionDescriptor;
        triple = nodeDescriptor2?.Triple;
        if (focusedNode.Nodes.Count == 1 && focusedNode.Nodes[0].Tag != null && focusedNode.Nodes[0].Tag is UserControlSortingSetup.NodeDescriptor && ((UserControlSortingSetup.NodeDescriptor) focusedNode.Nodes[0].Tag).AttributeSortSchema == null)
        {
          this._treeListSortSchema.FocusedNode = focusedNode.Nodes[0];
          Application.DoEvents();
          nodeDescriptor2 = focusedNode.Tag is UserControlSortingSetup.NodeDescriptor tag3 ? tag3 : (UserControlSortingSetup.NodeDescriptor) null;
          flag2 = true;
        }
      }
    }
    if (nodeDescriptor2?.AttributeSortSchema != null)
      parentNode1 = focusedNode?.ParentNode;
    else if (sectionDescriptor != null || triple != null)
      parentNode1 = focusedNode;
    if (parentNode1 == null)
      return;
    bool flag3 = false;
    try
    {
      if (!flag2)
      {
        node = this._treeListSortSchema.AppendNode((object) new object[5]
        {
          (object) "",
          (object) "",
          (object) "",
          (object) "",
          (object) ""
        }, parentNode1);
        flag3 = true;
      }
      else
        node = this._treeListSortSchema.FocusedNode;
      nodeDescriptor1 = new UserControlSortingSetup.NodeDescriptor(this, node);
      if (this._treeListSortSchema.FocusedNode != node)
      {
        Application.DoEvents();
        this._treeListSortSchema.FocusedNode = node;
        Application.DoEvents();
      }
      if (!this.SelectAttribute())
        return;
      flag1 = true;
      this.Changed = true;
    }
    finally
    {
      if (!flag1 & flag3)
      {
        if (focusedNode != null)
          this._treeListSortSchema.FocusedNode = focusedNode;
        nodeDescriptor1?.Dispose();
        if (node != null)
        {
          if (node.ParentNode.Nodes.Count > 1)
          {
            node.ParentNode.Nodes.Remove(node);
          }
          else
          {
            UserControlSortingSetup.NodeDescriptor nodeDescriptor4 = new UserControlSortingSetup.NodeDescriptor(this, node);
          }
        }
      }
      this.UpdateControls(false);
    }
  }

  /// <summary> Была нажата кнопка "изменить атрибут" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _editAttribute_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (e.Button != this._editAttribute.Properties.Buttons[0])
      return;
    this.EditParam();
  }

  /// <summary> Дважды кникнули по дереву настроек </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _treeListSortSchema_DoubleClick(object sender, EventArgs e)
  {
    if (!this._editAttribute.Properties.Buttons[0].Visible || this._treeListSortSchema.GetHitInfo(this._treeListSortSchema.PointToClient(Control.MousePosition)).Node == null)
      return;
    this.EditParam();
  }

  /// <summary> Была нажата кнопка "удалить условие сортировки" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _btnDelUslov_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    long activeSectionId = this.ActiveSectionID;
    Guid attributeSchemaGuid = this.FocusedAttributeSchemaGuid;
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && (this._comboBoxListSource.SelectedIndex != 1 || activeSectionId != this.ActiveSectionID || this.FocusedAttributeSchemaGuid != attributeSchemaGuid))
      return;
    TreeListNode focusedNode = this._treeListSortSchema.FocusedNode;
    UserControlSortingSetup.NodeDescriptor nodeDescriptor1 = focusedNode?.Tag is UserControlSortingSetup.NodeDescriptor tag1 ? tag1 : (UserControlSortingSetup.NodeDescriptor) null;
    SectionSortSchema sectionSortSchema = (SectionSortSchema) null;
    TreeListNode treeListNode1 = (TreeListNode) null;
    UserControlSortingSetup.NodeDescriptor nodeDescriptor2 = (UserControlSortingSetup.NodeDescriptor) null;
    if (focusedNode?.ParentNode != null)
    {
      treeListNode1 = focusedNode.ParentNode;
      nodeDescriptor2 = treeListNode1.Tag is UserControlSortingSetup.NodeDescriptor tag2 ? tag2 : (UserControlSortingSetup.NodeDescriptor) null;
      sectionSortSchema = nodeDescriptor2?.SectionSortSchema;
    }
    AttributeSortSchema attributeSortSchema = nodeDescriptor1?.AttributeSortSchema;
    if (attributeSortSchema != null)
    {
      if (sectionSortSchema != null)
      {
        try
        {
          if (MessageBox.Show("Удалить выбранное правило сортировки?", "Удаление правила сортировки", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return;
          int index1 = Array.IndexOf<AttributeSortSchema>(sectionSortSchema.AttributeSortSchemas, attributeSortSchema);
          if (index1 != -1)
            sectionSortSchema.AttributeSortSchemas = (AttributeSortSchema[]) ArrayEditHelper.RemoveItemAt((Array) sectionSortSchema.AttributeSortSchemas, index1);
          if (sectionSortSchema.AttributeSortSchemas.Length == 0)
          {
            int index2 = Array.IndexOf<SectionSortSchema>(this._sortSchema.SectionSortSchemas, sectionSortSchema);
            if (index2 != -1)
              this._sortSchema.SectionSortSchemas = (SectionSortSchema[]) ArrayEditHelper.RemoveItemAt((Array) this._sortSchema.SectionSortSchemas, index2);
            if (nodeDescriptor2 != null)
              nodeDescriptor2.SectionSortSchema = (SectionSortSchema) null;
          }
          if (nodeDescriptor1 != null)
            nodeDescriptor1.AttributeSortSchema = (AttributeSortSchema) null;
          this.Changed = true;
          if (sectionSortSchema.AttributeSortSchemas.Length == 0)
            return;
          TreeListNode treeListNode2 = treeListNode1;
          if (treeListNode1 == null)
            return;
          int num = treeListNode1.Nodes.IndexOf(focusedNode);
          if (num < treeListNode1.Nodes.Count - 1)
            treeListNode2 = treeListNode1.Nodes[num + 1];
          else if (num > 0)
            treeListNode2 = treeListNode1.Nodes[num - 1];
          this._treeListSortSchema.FocusedNode = treeListNode2;
          treeListNode1.Nodes.Remove(focusedNode);
          return;
        }
        finally
        {
          this.UpdateControls(false);
        }
      }
    }
    this.UpdateControls(false);
  }

  /// <summary> Был изменено свойство наследования </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxListSource_SelectedIndexChanged(object sender, EventArgs e)
  {
    int selectedIndex = this._comboBoxListSource.SelectedIndex;
    bool wasUpdated = false;
    if (this.ControlsAreUpdating || this.ControlsAreUpdating || this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && this.ReadOnly)
      return;
    if ((selectedIndex == 1 || !this._sortSchema.Changed ? 6 : (int) MessageBox.Show("Сбросить изменения в настройках сортировки?", "Настройки сортировки", MessageBoxButtons.YesNo)) == 6)
    {
      this._sortSchema.Changed = selectedIndex == 1;
      this.Changed = true;
      if (!this._sortSchema.Changed)
      {
        this.LockControls();
        try
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            this._sortSchema.LoadDefaultSchema(sessionKeeper.Session);
          this.SortSchema = this._sortSchema;
          this.UpdateControls(false);
          this.Changed = true;
        }
        finally
        {
          this.UnlockControls();
        }
      }
      else
        this.UpdateControls(false);
    }
    else
      this.UpdateControls(false);
  }

  /// <summary> Был закрыт список выбора вариантов поиска начала подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartFrom_CloseUp(object sender, CloseUpEventArgs e)
  {
    e.AcceptValue = this.CheckCanComboBoxChangeSelectedIndex((ComboBoxEdit) this._comboBoxSubstrStartFrom);
  }

  /// <summary> Было изменено правило выбора начала подстроки выдираемой из обозначения </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartFrom_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.BeginControlChanges((Control) this._comboBoxSubstrStartFrom))
      return;
    this.FocusedAttributeSortSchema.SubstringStartType = (SubstringStartFinishType) (this._comboBoxSubstrStartFrom.SelectedIndex + 1);
    this.FinishContolChanges();
  }

  /// <summary> Был закрыт список выбора вариантов поиска окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrFinishAt_CloseUp(object sender, CloseUpEventArgs e)
  {
    e.AcceptValue = this.CheckCanComboBoxChangeSelectedIndex((ComboBoxEdit) this._comboBoxSubstrFinishAt);
  }

  /// <summary> Было изменено правило определения окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrFinishAt_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.BeginControlChanges((Control) this._comboBoxSubstrFinishAt))
      return;
    this.FocusedAttributeSortSchema.SubstringEndType = (SubstringStartFinishType) (this._comboBoxSubstrFinishAt.SelectedIndex + 1);
    this.FinishContolChanges();
  }

  /// <summary>Был закрыт список выбора правила сравнения получившихся подстрок </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxCompareType_CloseUp(object sender, CloseUpEventArgs e)
  {
    e.AcceptValue = this.CheckCanComboBoxChangeSelectedIndex((ComboBoxEdit) this._comboBoxCompareType);
  }

  /// <summary> Было изменено правило сравнения получившихся подстрок </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxCompareType_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.BeginControlChanges((Control) this._comboBoxCompareType))
      return;
    this.FocusedAttributeSortSchema.CompareType = (CompareType) (this._comboBoxCompareType.SelectedIndex + 1);
    this.FinishContolChanges();
  }

  /// <summary> Был закрыт список выбора правила сортировки пустых результатов </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxEmptyRecord_CloseUp(object sender, CloseUpEventArgs e)
  {
    e.AcceptValue = this.CheckCanComboBoxChangeSelectedIndex((ComboBoxEdit) this._comboBoxEmptyRecord);
  }

  /// <summary> Было изменено правило сортировки пустых результатов </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxEmptyRecord_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.BeginControlChanges((Control) this._comboBoxEmptyRecord))
      return;
    this.FocusedAttributeSortSchema.EmptyOrder = (EmptyOrder) (this._comboBoxEmptyRecord.SelectedIndex + 1);
    this.FinishContolChanges();
  }

  /// <summary> Был закрыт список выбора направления сортировки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxAlign_CloseUp(object sender, CloseUpEventArgs e)
  {
    e.AcceptValue = this.CheckCanComboBoxChangeSelectedIndex((ComboBoxEdit) this._comboBoxAlign);
  }

  /// <summary> Было изменено направление сортировки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxAlign_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.BeginControlChanges((Control) this._comboBoxAlign))
      return;
    this.FocusedAttributeSortSchema.SortOrder = (Intermech.Interfaces.AVS.SortOrder) (this._comboBoxAlign.SelectedIndex + 1);
    this.FinishContolChanges();
  }

  /// <summary> Попытка изменения номера символа начала строки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _upDownSubstrStartNumber_EditValueChanging(object sender, ChangingEventArgs e)
  {
    e.Cancel = !this.CheckCanSpinEditEdited(this._upDownSubstrStartNumber, e);
  }

  /// <summary> Был потерян фокус контролом выбора символа окончания строки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartSymbol_Leave(object sender, EventArgs e)
  {
    if (this.FocusedAttributeSortSchema == null || this.ControlsAreUpdating)
      return;
    int symbolElementIndex = UserControlSortingSetup.GetComboBoxSymbolElementIndex(UserControlSortingSetup.StringToChar(this._comboBoxSubstrStartSymbol.Text));
    if (symbolElementIndex == -1)
      return;
    this._comboBoxSubstrStartSymbol.SelectedIndex = symbolElementIndex;
  }

  /// <summary> Было изменен номер символа начала строки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _upDownSubstrStartNumber_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeginControlChanges((Control) this._upDownSubstrStartNumber))
      return;
    this.FocusedAttributeSortSchema.StartPosition = (int) this._upDownSubstrStartNumber.Value;
    this.FinishContolChanges();
  }

  /// <summary> Попытка изменения номера символа окончания строки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _upDownSubstrFinishNumber_EditValueChanging(object sender, ChangingEventArgs e)
  {
    e.Cancel = !this.CheckCanSpinEditEdited(this._upDownSubstrFinishNumber, e);
  }

  /// <summary> Было изменен номер символа начала строки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _upDownSubstrFinishNumber_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeginControlChanges((Control) this._upDownSubstrFinishNumber))
      return;
    this.FocusedAttributeSortSchema.EndPosition = (int) this._upDownSubstrFinishNumber.Value;
    this.FinishContolChanges();
  }

  /// <summary> Был закрыт список выбора символа начала подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartSymbol_SelectedIndexChanged(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
      return;
    long activeSectionId = this.ActiveSectionID;
    Guid attributeSchemaGuid = this.FocusedAttributeSchemaGuid;
    if (this.ControlsAreUpdating || this.FocusedAttributeSortSchema == null || this._comboBoxListSource.SelectedIndex != 1 || !this.CheckCanEdit(ref wasUpdated))
      return;
    char ch = UserControlSortingSetup.StringToChar(this.FocusedAttributeSortSchema.EndSubstring);
    if (wasUpdated && (this.ReadOnly || this._comboBoxSubstrStartSymbol.SelectedIndex != 1 || (int) ch != (int) UserControlSortingSetup.StringToChar(this.FocusedAttributeSortSchema.EndSubstring) || activeSectionId != this.ActiveSectionID || attributeSchemaGuid != this.FocusedAttributeSchemaGuid || this._comboBoxSubstrStartSymbol.Properties.ReadOnly))
      return;
    this._changedControl = (Control) this._comboBoxSubstrStartSymbol;
    try
    {
      if (this._comboBoxSubstrStartSymbol.SelectedIndex != -1)
        this.FocusedAttributeSortSchema.EndSubstring = UserControlSortingSetup._predefinedSymbols[this._comboBoxSubstrStartSymbol.SelectedIndex].ToString();
      this._sortSchema.Changed = true;
      this.Changed = true;
      this.UpdateControls(false);
    }
    finally
    {
      this._changedControl = (Control) null;
    }
    this.RefreshFocusedTreeListItem();
  }

  /// <summary> Была предпринята попытка редактирования символа начала подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartSymbol_EditValueChanging(object sender, ChangingEventArgs e)
  {
    e.Cancel = !this.CheckCanComboBoxEditValue(this._comboBoxSubstrStartSymbol, e);
  }

  /// <summary> Было изменен символ начала подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrStartSymbol_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeginControlChanges((Control) this._comboBoxSubstrStartSymbol))
      return;
    this.FocusedAttributeSortSchema.StartSubstring = UserControlSortingSetup.StringToChar(this._comboBoxSubstrStartSymbol.Text).ToString();
    this.FinishContolChanges();
  }

  /// <summary> Был закрыт список выбора символа окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrFinishSymbol_SelectedIndexChanged(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
      return;
    long activeSectionId = this.ActiveSectionID;
    Guid attributeSchemaGuid = this.FocusedAttributeSchemaGuid;
    if (this.ControlsAreUpdating || this.FocusedAttributeSortSchema == null || this._comboBoxListSource.SelectedIndex != 1 || !this.CheckCanEdit(ref wasUpdated))
      return;
    char ch = UserControlSortingSetup.StringToChar(this.FocusedAttributeSortSchema.EndSubstring);
    if (wasUpdated && (this.ReadOnly || this._comboBoxSubstrFinishSymbol.SelectedIndex != 1 || (int) ch != (int) UserControlSortingSetup.StringToChar(this.FocusedAttributeSortSchema.EndSubstring) || activeSectionId != this.ActiveSectionID || attributeSchemaGuid != this.FocusedAttributeSchemaGuid || this._comboBoxSubstrFinishSymbol.Properties.ReadOnly))
      return;
    this._changedControl = (Control) this._comboBoxSubstrFinishSymbol;
    try
    {
      if (this._comboBoxSubstrFinishSymbol.SelectedIndex != -1)
        this.FocusedAttributeSortSchema.EndSubstring = UserControlSortingSetup._predefinedSymbols[this._comboBoxSubstrFinishSymbol.SelectedIndex].ToString();
      this._sortSchema.Changed = true;
      this.Changed = true;
      this.UpdateControls(false);
    }
    finally
    {
      this._changedControl = (Control) null;
    }
    this.RefreshFocusedTreeListItem();
  }

  /// <summary> Была предпринята попытка редактирования символа окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrFinishSymbol_EditValueChanging(object sender, ChangingEventArgs e)
  {
    e.Cancel = !this.CheckCanComboBoxEditValue(this._comboBoxSubstrFinishSymbol, e);
  }

  /// <summary> Было изменен символ окончания подстроки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrFinishSymbol_EditValueChanged(object sender, EventArgs e)
  {
    if (!this.BeginControlChanges((Control) this._comboBoxSubstrFinishSymbol))
      return;
    this.FocusedAttributeSortSchema.EndSubstring = UserControlSortingSetup.StringToChar(this._comboBoxSubstrFinishSymbol.Text).ToString();
    this.FinishContolChanges();
  }

  /// <summary> Был потерян фокус контролом выбора символа окончания строки </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxSubstrFinishSymbol_Leave(object sender, EventArgs e)
  {
    if (this.FocusedAttributeSortSchema == null || this.ControlsAreUpdating)
      return;
    int symbolElementIndex = UserControlSortingSetup.GetComboBoxSymbolElementIndex(UserControlSortingSetup.StringToChar(this._comboBoxSubstrFinishSymbol.Text));
    if (symbolElementIndex == -1)
      return;
    this._comboBoxSubstrFinishSymbol.SelectedIndex = symbolElementIndex;
  }

  /// <summary> Была нажата кнопка "переместить активное правло сортировки вверх" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _btnMoveUp_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
      return;
    long activeSectionId = this.ActiveSectionID;
    Guid attributeSchemaGuid1 = this.FocusedAttributeSchemaGuid;
    if (attributeSchemaGuid1 == Guid.Empty)
      return;
    int focusedNodeIndex = this.RelativeFocusedNodeIndex;
    if (this.ControlsAreUpdating || this.FocusedAttributeSortSchema == null || this._comboBoxListSource.SelectedIndex != 1 || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && (this.ReadOnly || this._comboBoxSubstrFinishSymbol.SelectedIndex != 1 || activeSectionId != this.ActiveSectionID || attributeSchemaGuid1 != this.FocusedAttributeSchemaGuid || focusedNodeIndex != this.RelativeFocusedNodeIndex || !this._btnMoveUp.Enabled))
      return;
    AttributeSortSchema attributeSortSchema = this.FocusedAttributeSortSchema;
    SectionSortSchema sectionSortSchema = this.ActiveSectionSortSchema;
    if (attributeSortSchema == null || sectionSortSchema == null)
      return;
    int index1 = Array.IndexOf<AttributeSortSchema>(sectionSortSchema.AttributeSortSchemas, attributeSortSchema);
    if (index1 <= 0)
      return;
    Array array = ArrayEditHelper.RemoveItemAt((Array) sectionSortSchema.AttributeSortSchemas, index1);
    if (array == null)
      return;
    int index2 = index1 - 1;
    sectionSortSchema.AttributeSortSchemas = (AttributeSortSchema[]) ArrayEditHelper.InsertItemToArray(array, (object) attributeSortSchema, index2);
    TreeListNode parentNode = this._treeListSortSchema.FocusedNode.ParentNode;
    Guid attributeSchemaGuid2 = this.FocusedAttributeSchemaGuid;
    this._treeListSortSchema.FocusedNode = parentNode;
    this.UpdateSection(parentNode, sectionSortSchema);
    foreach (TreeListNode node in parentNode.Nodes)
    {
      if (node.Tag != null && node.Tag is UserControlSortingSetup.NodeDescriptor && ((UserControlSortingSetup.NodeDescriptor) node.Tag).AttributeSortSchema != null && ((UserControlSortingSetup.NodeDescriptor) node.Tag).AttributeSortSchema.SchemeGuid == attributeSchemaGuid2)
      {
        this._treeListSortSchema.FocusedNode = node;
        break;
      }
    }
    this._sortSchema.Changed = true;
    this.Changed = true;
    this.UpdateControls(false);
  }

  private void _btnMoveDown_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating)
      return;
    long activeSectionId = this.ActiveSectionID;
    Guid attributeSchemaGuid1 = this.FocusedAttributeSchemaGuid;
    if (attributeSchemaGuid1 == Guid.Empty)
      return;
    int focusedNodeIndex = this.RelativeFocusedNodeIndex;
    if (this.ControlsAreUpdating || this.FocusedAttributeSortSchema == null || this._comboBoxListSource.SelectedIndex != 1 || !this.CheckCanEdit(ref wasUpdated) || wasUpdated && (this.ReadOnly || this._comboBoxSubstrFinishSymbol.SelectedIndex != 1 || activeSectionId != this.ActiveSectionID || attributeSchemaGuid1 != this.FocusedAttributeSchemaGuid || focusedNodeIndex != this.RelativeFocusedNodeIndex || !this._btnMoveDown.Enabled))
      return;
    AttributeSortSchema attributeSortSchema = this.FocusedAttributeSortSchema;
    SectionSortSchema sectionSortSchema = this.ActiveSectionSortSchema;
    if (attributeSortSchema == null || sectionSortSchema == null)
      return;
    int index1 = Array.IndexOf<AttributeSortSchema>(sectionSortSchema.AttributeSortSchemas, attributeSortSchema);
    if (index1 < 0 || index1 >= sectionSortSchema.AttributeSortSchemas.Length - 1)
      return;
    Array array = ArrayEditHelper.RemoveItemAt((Array) sectionSortSchema.AttributeSortSchemas, index1);
    if (array == null)
      return;
    int index2 = index1 + 1;
    sectionSortSchema.AttributeSortSchemas = (AttributeSortSchema[]) ArrayEditHelper.InsertItemToArray(array, (object) attributeSortSchema, index2);
    TreeListNode parentNode = this._treeListSortSchema.FocusedNode.ParentNode;
    Guid attributeSchemaGuid2 = this.FocusedAttributeSchemaGuid;
    this._treeListSortSchema.FocusedNode = parentNode;
    this.UpdateSection(parentNode, sectionSortSchema);
    foreach (TreeListNode node in parentNode.Nodes)
    {
      if (node.Tag != null && node.Tag is UserControlSortingSetup.NodeDescriptor && ((UserControlSortingSetup.NodeDescriptor) node.Tag).AttributeSortSchema != null && ((UserControlSortingSetup.NodeDescriptor) node.Tag).AttributeSortSchema.SchemeGuid == attributeSchemaGuid2)
      {
        this._treeListSortSchema.FocusedNode = node;
        break;
      }
    }
    this._sortSchema.Changed = true;
    this.Changed = true;
    this.UpdateControls(false);
  }

  /// <summary> Была нажата кнопка "по умолчанию" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _BtnReset_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ControlsAreUpdating || this.ControlsAreUpdating || this.ReadOnly || !this.CheckCanEdit(ref wasUpdated))
      return;
    if ((this._sortSchema.Changed ? (int) MessageBox.Show("Сбросить изменения в настройках сортировки?", "Настройки сортировки", MessageBoxButtons.YesNo) : 6) == 6)
    {
      this._sortSchema.Changed = true;
      this.LockControls();
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this._sortSchema.LoadDefaultSchema(sessionKeeper.Session);
        this.SortSchema = this._sortSchema;
        this.UpdateControls(false);
        this.Changed = true;
      }
      finally
      {
        this.UnlockControls();
      }
    }
    else
      this.UpdateControls(false);
  }

  /// <summary>вернуть id раздела в хелпе для данной страницы</summary>
  public string HelpTopicID => throw new Exception("Нет раздела справки");

  event EventHandler IPropertyPage.Changed
  {
    add => this.OnChangedEvent += value;
    remove => this.OnChangedEvent -= value;
  }

  PropertyPageType IPropertyPage.Type => PropertyPageType.Control;

  object IPropertyPage.Control => (object) this;

  string IPropertyPage.PageName => "Настройки ведомостей";

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  string IPropertyPage.HeaderText
  {
    [DebuggerStepThrough] get => ((IPropertyPage) this).PageName;
  }

  public long SettingsHolderObjectId { get; set; } = -1;

  void IPropertyPage.Apply() => throw new Exception("The method or operation is not implemented.");

  void IPropertyPage.Cancel() => throw new Exception("The method or operation is not implemented.");

  private void cbSortPartForPodborAfterBasePart_CheckedChanged(object sender, EventArgs e)
  {
    if (this.IsDesignerHosted())
      return;
    Control control = sender as Control;
    if (control == this._changedControl || !this.BeginControlChanges(control))
      return;
    bool wasUpdated = false;
    if (!this.ReadOnly && this.CheckCanEdit(ref wasUpdated))
      this._sortSchema.SortPartForPodborAfterBasePart = this.cbSortPartForPodborAfterBasePart.Checked;
    else
      this.cbSortPartForPodborAfterBasePart.Checked = this._sortSchema.SortPartForPodborAfterBasePart;
    this.FinishContolChanges();
  }

  private void cbSortDocumentsByType_CheckedChanged(object sender, EventArgs e)
  {
    if (this.IsDesignerHosted())
      return;
    Control control = sender as Control;
    if (control == this._changedControl || !this.BeginControlChanges(control))
      return;
    bool wasUpdated = false;
    if (!this.ReadOnly && this.CheckCanEdit(ref wasUpdated))
      this._sortSchema.SortDocumentsByType = this.cbSortDocumentsByType.Checked;
    else
      this.cbSortDocumentsByType.Checked = this._sortSchema.SortDocumentsByType;
    this.FinishContolChanges();
  }

  public class SectionDescriptor
  {
    private long _sectionID = -1;
    private string _sectionName = string.Empty;

    public SectionDescriptor(long sectionID, string sectionName)
    {
      this._sectionID = sectionID;
      this._sectionName = sectionName;
    }

    /// <summary> Идентификатор раздела спецификации </summary>
    public long SectionID => this._sectionID;

    /// <summary> Заголовок раздела спецификации </summary>
    public string SectionName => this._sectionName;
  }

  /// <summary> Декриптор ноды дерева настроек </summary>
  public class NodeDescriptor : IDisposable
  {
    private UserControlSortingSetup _userControlSortingSetup;
    private TreeListNode _node;
    private UserControlSortingSetup.SectionDescriptor _sectionDescriptor;
    private Triple _triple;
    private SectionSortSchema _sectionSortSchema;
    private AttributeSortSchema _attributeSortSchema;

    public NodeDescriptor(
      UserControlSortingSetup userControlSortingSetup,
      TreeListNode node,
      UserControlSortingSetup.SectionDescriptor sectionDescriptor,
      SectionSortSchema sectionSortSchema)
    {
      this._node = node;
      this._sectionDescriptor = sectionDescriptor;
      this._sectionSortSchema = sectionSortSchema;
      this._userControlSortingSetup = userControlSortingSetup;
      this.RefreshNode();
      userControlSortingSetup.AfterAddNewNodeDescriptor(this);
    }

    public NodeDescriptor(
      UserControlSortingSetup userControlSortingSetup,
      TreeListNode node,
      Triple triple,
      SectionSortSchema sectionSortSchema)
    {
      this._node = node;
      this._triple = triple;
      this._sectionSortSchema = sectionSortSchema;
      this._userControlSortingSetup = userControlSortingSetup;
      this.RefreshNode();
      userControlSortingSetup.AfterAddNewNodeDescriptor(this);
    }

    public NodeDescriptor(
      UserControlSortingSetup userControlSortingSetup,
      TreeListNode node,
      UserControlSortingSetup.SectionDescriptor sectionDescriptor)
    {
      this._node = node;
      this._sectionDescriptor = sectionDescriptor;
      this._userControlSortingSetup = userControlSortingSetup;
      this.RefreshNode();
      userControlSortingSetup.AfterAddNewNodeDescriptor(this);
    }

    public NodeDescriptor(
      UserControlSortingSetup userControlSortingSetup,
      TreeListNode node,
      Triple triple)
    {
      this._node = node;
      this._triple = triple;
      this._userControlSortingSetup = userControlSortingSetup;
      this.RefreshNode();
      userControlSortingSetup.AfterAddNewNodeDescriptor(this);
    }

    public NodeDescriptor(
      UserControlSortingSetup userControlSortingSetup,
      TreeListNode node,
      AttributeSortSchema attributeSortSchema)
    {
      this._node = node;
      this._attributeSortSchema = attributeSortSchema;
      this._userControlSortingSetup = userControlSortingSetup;
      this.RefreshNode();
      userControlSortingSetup.AfterAddNewNodeDescriptor(this);
    }

    public NodeDescriptor(UserControlSortingSetup userControlSortingSetup, TreeListNode node)
    {
      this._userControlSortingSetup = userControlSortingSetup;
      this._node = node;
      this.RefreshNode();
      userControlSortingSetup.AfterAddNewNodeDescriptor(this);
    }

    /// <summary> Ссылка на ноду дерева настроек </summary>
    public TreeListNode Node => this._node;

    /// <summary> Дескриптор раздела спецификации  </summary>
    public UserControlSortingSetup.SectionDescriptor SectionDescriptor => this._sectionDescriptor;

    /// <summary> Ссылка на заголовок ведомости (если схема сортировки для ведомостей)  </summary>
    public Triple Triple => this._triple;

    /// <summary> Настройки сортировки раздела спецификации  </summary>
    public SectionSortSchema SectionSortSchema
    {
      get => this._sectionSortSchema;
      set
      {
        this._sectionSortSchema = value;
        this.RefreshNode();
      }
    }

    /// <summary> Настройки сортировки нижнего уровня </summary>
    public AttributeSortSchema AttributeSortSchema
    {
      get => this._attributeSortSchema;
      set
      {
        this._attributeSortSchema = value;
        this.RefreshNode();
      }
    }

    /// <summary> Обновить ноду в дереве </summary>
    public void RefreshNode()
    {
      if (this._node == null)
        return;
      if (this._attributeSortSchema != null)
      {
        this._node.SetValue((object) 0, (object) this._attributeSortSchema.AttributeNameAndSource);
        this._node.SetValue((object) 1, (object) this._attributeSortSchema.FromStr);
        this._node.SetValue((object) 2, (object) this._attributeSortSchema.ToStr);
        this._node.SetValue((object) 3, (object) this._attributeSortSchema.CompareTypeStr);
        this._node.SetValue((object) 4, (object) this._attributeSortSchema.EmptyValueStr);
        switch (this._attributeSortSchema.SortOrder)
        {
          case Intermech.Interfaces.AVS.SortOrder.Ascending:
            this._node.ImageIndex = 1;
            break;
          case Intermech.Interfaces.AVS.SortOrder.Descending:
            this._node.ImageIndex = 2;
            break;
          default:
            this._node.ImageIndex = 0;
            break;
        }
      }
      else if (this._sectionDescriptor != null)
      {
        this._node.SetValue((object) 0, (object) string.Empty);
        this._node.SetValue((object) 1, (object) string.Empty);
        this._node.SetValue((object) 2, (object) this._sectionDescriptor.SectionName);
        this._node.ImageIndex = -1;
      }
      else if (this._triple != null)
      {
        this._node.SetValue((object) 0, (object) string.Empty);
        this._node.SetValue((object) 1, (object) string.Empty);
        this._node.SetValue((object) 2, (object) this._triple.Result);
        this._node.ImageIndex = -1;
      }
      else
      {
        this._node.SetValue((object) 0, (object) string.Empty);
        this._node.SetValue((object) 1, (object) string.Empty);
        this._node.SetValue((object) 2, (object) string.Empty);
        this._node.SetValue((object) 3, (object) string.Empty);
        this._node.SetValue((object) 4, (object) string.Empty);
        this._node.ImageIndex = -1;
      }
      this._node.SelectImageIndex = this._node.ImageIndex;
      this._node.Tag = (object) this;
    }

    public void Dispose()
    {
      if (this._userControlSortingSetup == null)
        return;
      this._userControlSortingSetup.AfterDisposeNodeDescriptor(this);
    }
  }
}
