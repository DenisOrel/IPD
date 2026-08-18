// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ImportObjectsFormAdvBase
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using Intermech.Workflow.Design;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>База для формы импорта структуры объекта в структуру задач</summary>
public class ImportObjectsFormAdvBase : 
  ProjectDialogBase,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IContextAware,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2
{
  private static readonly Point _selectButtonLocation = new Point(173, 6);
  /// <summary>Коллекция изображений для разных категорий</summary>
  [CanBeNull]
  protected readonly ICategoryTypeIconService _ObjTypesIconsService;
  /// <summary>Тип контрола выбора объектов из структуры, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного формы, в этом случае контрол будет создан указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [CanBeNull]
  public static System.Type OverrideSelectObjectsInCompositionControlType;
  [NotNull]
  protected SelectObjectsForImportControl _treeViewControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel _panelTreeCaption;
  protected Panel _panelRight;
  protected Label _labelTreeCaption;
  protected CheckBox _checkBoxAsProject;
  protected GroupBox _groupBoxSettings;
  protected TextBox _editIterationName;
  protected CheckBox _checkBoxImportRoot;
  protected Label _labelScenario;
  protected ComboBox _comboScript;
  protected Button _buttonPrototype;
  protected CheckBox _checkBoxProto;
  protected Label _labelIterationName;
  protected Label _labelMaxLevels;
  protected NumericUpDown _editMaxLevels;
  protected CheckBox _checkBoxCopySummaries;
  protected CheckBox _checkBoxCreateIteration;
  protected CheckBox _checkBoxLinear;
  protected CheckBox _checkBoxMaxLevels;
  protected Panel _panelRightDown;
  protected StatusStrip _statusStrip;
  protected ToolStripStatusLabel _labelFocusedObjectType;
  protected ToolStripStatusLabel _labelFocusedObjectCaption;
  protected Panel _panel1;
  protected Button _initTaskSettings;
  protected CheckBox _checkBoxInitTaskSettings;
  protected ComboBoxEx _comboBoxObjTypes;
  protected Bevel _bevelObjTypes;
  protected Label _labelObjTypes;
  protected Button _btnAddObjType;
  protected Button _btnDelObjType;
  protected CheckBox _checkBoxAsSubTask;
  protected Label label1;
  protected ComboBox _comboFinalScript;
  protected Bevel bevel1;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Panel PanelTreeCaption
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panelTreeCaption.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Panel PanelRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panelRight.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Label LabelTreeCaption
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelTreeCaption.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxAsProject
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxAsProject.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal GroupBox GroupBoxSettings
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._groupBoxSettings.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal TextBox EditIterationName
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editIterationName.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxImportRoot
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxImportRoot.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Label LabelScenario
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelScenario.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal ComboBox ComboScript
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboScript.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal ComboBox ComboFinalScript
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboFinalScript.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Button ButtonPrototype
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonPrototype.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxProto
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxProto.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Label LabelIterationName
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelIterationName.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Label LabelMaxLevels
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelMaxLevels.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal NumericUpDown EditMaxLevels
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editMaxLevels.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxCopySummaries
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxCopySummaries.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxCreateIteration
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxCreateIteration.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxLinear
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxLinear.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxMaxLevels
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxMaxLevels.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Panel PanelRightDown
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panelRightDown.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal StatusStrip StatusStrip
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._statusStrip.CheckInitializedIn<StatusStrip>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal ToolStripStatusLabel LabelFocusedObjectType
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelFocusedObjectType.CheckInitializedIn<ToolStripStatusLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal ToolStripStatusLabel LabelFocusedObjectCaption
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelFocusedObjectCaption.CheckInitializedIn<ToolStripStatusLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Panel Panel1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel1.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Button InitTaskSettings
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._initTaskSettings.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxInitTaskSettings
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxInitTaskSettings.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal ComboBoxEx ComboBoxObjTypes
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBoxObjTypes.CheckInitializedIn<ComboBoxEx>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Bevel BevelObjTypes
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._bevelObjTypes.CheckInitializedIn<Bevel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Label LabelObjTypes
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelObjTypes.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Button BtnAddObjType
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnAddObjType.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Button BtnDelObjType
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnDelObjType.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxAsSubTask
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxAsSubTask.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  public ImportObjectsFormAdvBase()
  {
    this.InitializeComponent();
    this.CreateTreeViewControl();
    this.MoveSelectObjectsButton();
    if (!this.InDesignMode)
      this._ObjTypesIconsService = ApplicationServices.Container.GetService<ICategoryTypeIconService>();
    ImageList imageList = new ImageList();
    imageList.ImageSize = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.ComboBoxObjTypes.ImageList = imageList;
    this.components = this.components ?? (IContainer) new System.ComponentModel.Container();
    this.components.Add((IComponent) imageList);
    this.ComboBoxObjTypes.Items.Add((object) new IDComboItem("По-умолчанию для всех типов объектов", -1L, -1));
    this.ComboBoxObjTypes.SelectedIndex = 0;
  }

  public ImportObjectsFormAdvBase([CanBeNull] System.IServiceProvider ownerServices, [NotNull, NotEmpty] string contextName)
    : base(ownerServices, contextName)
  {
    this.InitializeComponent();
    this.CreateTreeViewControl();
    this.MoveSelectObjectsButton();
    this._ObjTypesIconsService = ApplicationServices.Container.GetService<ICategoryTypeIconService>();
    this.ComboBoxObjTypes.ImageList = this._ObjTypesIconsService.ImageList;
    this.ComboBoxObjTypes.Items.Add((object) new IDComboItem(Localization.GetString("DefaultForAllObjectTypes"), -1L, -1));
    this.ComboBoxObjTypes.SelectedIndex = 0;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected virtual System.Type DefaultSelectObjectsInCompositionControlType
  {
    [DebuggerStepThrough] get => typeof (SelectObjectsForImportControl);
  }

  protected virtual void CreateTreeViewControl()
  {
    System.Type compositionControlType = ImportObjectsFormAdvBase.OverrideSelectObjectsInCompositionControlType;
    if ((object) compositionControlType == null)
      compositionControlType = this.DefaultSelectObjectsInCompositionControlType;
    this._treeViewControl = (SelectObjectsForImportControl) Activator.CreateInstance(compositionControlType);
    ImportObjectsFormAdvBase.OverrideSelectObjectsInCompositionControlType = (System.Type) null;
    this.TreeViewControl.PanelSelectButtons.SuspendLayout();
    this._treeViewControl.SuspendLayout();
    this._panelTreeCaption.SuspendLayout();
    this._panelRight.SuspendLayout();
    this._groupBoxSettings.SuspendLayout();
    this._editMaxLevels.BeginInit();
    this.PanelRightDown.SuspendLayout();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this.TreeViewControl.TreeView.BeginInit();
    this.SuspendLayout();
    this._panel1.Controls.Add((Control) this._treeViewControl);
    this._treeViewControl.AllowChangeObjects = true;
    this._treeViewControl.Dock = DockStyle.Fill;
    this._treeViewControl.Location = new Point(3, 3);
    this._treeViewControl.MinimumSize = new Size(562, 204);
    this._treeViewControl.Name = "_treeViewControl";
    this._treeViewControl.BtnSelectObjects.Anchor = AnchorStyles.Top | AnchorStyles.Left;
    this._treeViewControl.BtnSelectObjects.Location = new Point(167, 6);
    this._treeViewControl.PanelSelectButtons.Controls.Add((Control) this._checkBoxAsProject);
    this._treeViewControl.PanelSelectButtons.Dock = DockStyle.Bottom;
    this._treeViewControl.PanelSelectButtons.Location = new Point(0, 439);
    this._treeViewControl.PanelSelectButtons.Name = "PanelSelectButtons";
    this._treeViewControl.PanelSelectButtons.Size = new Size(562, 61);
    this._treeViewControl.PanelSelectButtons.TabIndex = 12;
    this._treeViewControl.Size = new Size(562, 508);
    this._treeViewControl.TabIndex = 6;
    this._treeViewControl.TreeView.AllowDrop = true;
    this._treeViewControl.TreeView.AllowMultiSelect = false;
    this._treeViewControl.TreeView.AllowUserPinnedColumns = false;
    this._treeViewControl.TreeView.BackgroundImageMode = ImageDrawMode.Tile;
    this._treeViewControl.TreeView.BorderStyle = BorderStyle.Fixed3D;
    this._treeViewControl.TreeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;
    this._treeViewControl.TreeView.DisableCheckedOutColumn = true;
    this._treeViewControl.TreeView.DisableDragAndDrop = true;
    this._treeViewControl.TreeView.DisableIMContextMenu = true;
    this._treeViewControl.TreeView.Dock = DockStyle.Fill;
    this._treeViewControl.TreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._treeViewControl.TreeView.LineStyle = LineStyle.Dot;
    this._treeViewControl.TreeView.Location = new Point(0, 24);
    this._treeViewControl.TreeView.Name = "TreeView";
    this._treeViewControl.TreeView.RowEvenStyle.WordWrap = false;
    this._treeViewControl.TreeView.RowOddStyle.WordWrap = false;
    this._treeViewControl.TreeView.RowSelectedStyle.WordWrap = false;
    this._treeViewControl.TreeView.RowStyle.BorderColor = SystemColors.Control;
    this._treeViewControl.TreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this._treeViewControl.TreeView.RowStyle.BorderWidth = 1;
    this._treeViewControl.TreeView.RowStyle.WordWrap = false;
    this._treeViewControl.TreeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this._treeViewControl.TreeView.SelectBeforeEdit = true;
    this._treeViewControl.TreeView.ShowRootRow = false;
    this._treeViewControl.TreeView.Size = new Size(562, 415);
    this._treeViewControl.TreeView.SuppressErrorMessages = true;
    this._treeViewControl.TreeView.TabIndex = 0;
    this.TreeViewControl.PanelSelectButtons.ResumeLayout(false);
    this.TreeViewControl.PanelSelectButtons.PerformLayout();
    this._treeViewControl.ResumeLayout(false);
    this._panelTreeCaption.ResumeLayout(false);
    this._panelTreeCaption.PerformLayout();
    this._panelRight.ResumeLayout(false);
    this._groupBoxSettings.ResumeLayout(false);
    this._groupBoxSettings.PerformLayout();
    this._editMaxLevels.EndInit();
    this.PanelRightDown.ResumeLayout(false);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this.TreeViewControl.TreeView.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Контрол с деревом навигатора</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public SelectObjectsForImportControl TreeViewControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._treeViewControl;
    }
  }

  /// <summary>UI: Дерево состава объекта</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public ImportObjectsNavTree TreeView
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._treeViewControl.TreeView;
    }
  }

  private void MoveSelectObjectsButton()
  {
    Button btnSelectObjects = this.TreeViewControl._BtnSelectObjects;
    this.TreeViewControl.PanelSelectButtons.Controls.Remove((Control) btnSelectObjects);
    btnSelectObjects.Anchor = AnchorStyles.Top | AnchorStyles.Left;
    btnSelectObjects.Location = ImportObjectsFormAdvBase._selectButtonLocation;
    this.PanelRightDown.SuspendLayout();
    this.PanelRightDown.Controls.Add((Control) btnSelectObjects);
    this.PanelRightDown.ResumeLayout(true);
    Point point1 = btnSelectObjects.Location;
    int x1 = point1.X;
    point1 = ImportObjectsFormAdvBase._selectButtonLocation;
    int x2 = point1.X;
    if (x1 == x2)
    {
      Point point2 = btnSelectObjects.Location;
      int y1 = point2.Y;
      point2 = ImportObjectsFormAdvBase._selectButtonLocation;
      int y2 = point2.Y;
      if (y1 == y2)
        return;
    }
    btnSelectObjects.Location = ImportObjectsFormAdvBase._selectButtonLocation;
  }

  /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
  private void InitializeComponent()
  {
    this._panelTreeCaption = new Panel();
    this._labelTreeCaption = new Label();
    this._panelRightDown = new Panel();
    this._panelRight = new Panel();
    this._groupBoxSettings = new GroupBox();
    this._comboBoxObjTypes = new ComboBoxEx();
    this._initTaskSettings = new Button();
    this._checkBoxInitTaskSettings = new CheckBox();
    this._btnAddObjType = new Button();
    this._labelScenario = new Label();
    this.label1 = new Label();
    this._labelObjTypes = new Label();
    this._comboFinalScript = new ComboBox();
    this._comboScript = new ComboBox();
    this.bevel1 = new Bevel();
    this._bevelObjTypes = new Bevel();
    this._buttonPrototype = new Button();
    this._checkBoxProto = new CheckBox();
    this._editIterationName = new TextBox();
    this._btnDelObjType = new Button();
    this._checkBoxAsSubTask = new CheckBox();
    this._checkBoxImportRoot = new CheckBox();
    this._labelIterationName = new Label();
    this._labelMaxLevels = new Label();
    this._editMaxLevels = new NumericUpDown();
    this._checkBoxCopySummaries = new CheckBox();
    this._checkBoxCreateIteration = new CheckBox();
    this._checkBoxLinear = new CheckBox();
    this._checkBoxMaxLevels = new CheckBox();
    this._checkBoxAsProject = new CheckBox();
    this._statusStrip = new StatusStrip();
    this._labelFocusedObjectType = new ToolStripStatusLabel();
    this._labelFocusedObjectCaption = new ToolStripStatusLabel();
    this._panel1 = new Panel();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this._panelTreeCaption.SuspendLayout();
    this._panelRight.SuspendLayout();
    this._groupBoxSettings.SuspendLayout();
    this._editMaxLevels.BeginInit();
    this._statusStrip.SuspendLayout();
    this.SuspendLayout();
    this._pnlDialogButtons.Location = new Point(0, 579);
    this._pnlDialogButtons.Size = new Size(914, 36);
    this._okButton.DialogResult = DialogResult.None;
    this._bevelDialogButtons.Location = new Point(0, 577);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(914, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._panelBtns.Location = new Point(741, 0);
    this._panelTreeCaption.Controls.Add((Control) this._labelTreeCaption);
    this._panelTreeCaption.Dock = DockStyle.Top;
    this._panelTreeCaption.Location = new Point(0, 0);
    this._panelTreeCaption.Name = "_panelTreeCaption";
    this._panelTreeCaption.Size = new Size(567, 28);
    this._panelTreeCaption.TabIndex = 5;
    this._labelTreeCaption.AutoSize = true;
    this._labelTreeCaption.Location = new Point(3, 9);
    this._labelTreeCaption.Name = "_labelTreeCaption";
    this._labelTreeCaption.Size = new Size(366, 13);
    this._labelTreeCaption.TabIndex = 1;
    this._labelTreeCaption.Text = "Выберите объекты, по которым должны создаваться задачи проекта:";
    this._panelRightDown.Dock = DockStyle.Bottom;
    this._panelRightDown.Location = new Point(6, 536);
    this._panelRightDown.Name = "_panelRightDown";
    this._panelRightDown.Size = new Size(335, 35);
    this._panelRightDown.TabIndex = 0;
    this._panelRight.Controls.Add((Control) this._groupBoxSettings);
    this._panelRight.Controls.Add((Control) this._panelRightDown);
    this._panelRight.Dock = DockStyle.Right;
    this._panelRight.Location = new Point(567, 0);
    this._panelRight.Name = "_panelRight";
    this._panelRight.Padding = new Padding(6);
    this._panelRight.Size = new Size(347, 577);
    this._panelRight.TabIndex = 4;
    this._groupBoxSettings.Controls.Add((Control) this._comboBoxObjTypes);
    this._groupBoxSettings.Controls.Add((Control) this._initTaskSettings);
    this._groupBoxSettings.Controls.Add((Control) this._checkBoxInitTaskSettings);
    this._groupBoxSettings.Controls.Add((Control) this._btnAddObjType);
    this._groupBoxSettings.Controls.Add((Control) this._labelScenario);
    this._groupBoxSettings.Controls.Add((Control) this.label1);
    this._groupBoxSettings.Controls.Add((Control) this._labelObjTypes);
    this._groupBoxSettings.Controls.Add((Control) this._comboFinalScript);
    this._groupBoxSettings.Controls.Add((Control) this._comboScript);
    this._groupBoxSettings.Controls.Add((Control) this.bevel1);
    this._groupBoxSettings.Controls.Add((Control) this._bevelObjTypes);
    this._groupBoxSettings.Controls.Add((Control) this._buttonPrototype);
    this._groupBoxSettings.Controls.Add((Control) this._checkBoxProto);
    this._groupBoxSettings.Controls.Add((Control) this._editIterationName);
    this._groupBoxSettings.Controls.Add((Control) this._btnDelObjType);
    this._groupBoxSettings.Controls.Add((Control) this._checkBoxAsSubTask);
    this._groupBoxSettings.Controls.Add((Control) this._checkBoxImportRoot);
    this._groupBoxSettings.Controls.Add((Control) this._labelIterationName);
    this._groupBoxSettings.Controls.Add((Control) this._labelMaxLevels);
    this._groupBoxSettings.Controls.Add((Control) this._editMaxLevels);
    this._groupBoxSettings.Controls.Add((Control) this._checkBoxCopySummaries);
    this._groupBoxSettings.Controls.Add((Control) this._checkBoxCreateIteration);
    this._groupBoxSettings.Controls.Add((Control) this._checkBoxLinear);
    this._groupBoxSettings.Controls.Add((Control) this._checkBoxMaxLevels);
    this._groupBoxSettings.Dock = DockStyle.Fill;
    this._groupBoxSettings.Location = new Point(6, 6);
    this._groupBoxSettings.Margin = new Padding(3, 10, 3, 3);
    this._groupBoxSettings.Name = "_groupBoxSettings";
    this._groupBoxSettings.Size = new Size(335, 530);
    this._groupBoxSettings.TabIndex = 2;
    this._groupBoxSettings.TabStop = false;
    this._groupBoxSettings.Text = "Общие настройки";
    this._comboBoxObjTypes.DrawMode = DrawMode.OwnerDrawFixed;
    this._comboBoxObjTypes.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxObjTypes.ImageList = (ImageList) null;
    this._comboBoxObjTypes.Location = new Point(10, 290);
    this._comboBoxObjTypes.Name = "_comboBoxObjTypes";
    this._comboBoxObjTypes.Size = new Size(313, 21);
    this._comboBoxObjTypes.TabIndex = 8;
    this._initTaskSettings.Location = new Point(207, 352);
    this._initTaskSettings.Name = "_initTaskSettings";
    this._initTaskSettings.Size = new Size(116, 23);
    this._initTaskSettings.TabIndex = 12;
    this._initTaskSettings.Text = "Выбрать...";
    this._initTaskSettings.UseVisualStyleBackColor = true;
    this._checkBoxInitTaskSettings.Location = new Point(10, 352);
    this._checkBoxInitTaskSettings.Name = "_checkBoxInitTaskSettings";
    this._checkBoxInitTaskSettings.Size = new Size(191, 26);
    this._checkBoxInitTaskSettings.TabIndex = 11;
    this._checkBoxInitTaskSettings.Text = "Использовать параметры";
    this._checkBoxInitTaskSettings.UseVisualStyleBackColor = true;
    this._btnAddObjType.Location = new Point(10, 316);
    this._btnAddObjType.Name = "_btnAddObjType";
    this._btnAddObjType.Size = new Size(75, 23);
    this._btnAddObjType.TabIndex = 9;
    this._btnAddObjType.Text = "Добавить...";
    this._btnAddObjType.UseVisualStyleBackColor = true;
    this._labelScenario.AutoSize = true;
    this._labelScenario.ImeMode = ImeMode.NoControl;
    this._labelScenario.Location = new Point(7, 416);
    this._labelScenario.Name = "_labelScenario";
    this._labelScenario.Size = new Size(172, 13);
    this._labelScenario.TabIndex = 9;
    this._labelScenario.Text = "Сценарий инициализации задач:";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(10, 476);
    this.label1.Name = "label1";
    this.label1.Size = new Size(194, 13);
    this.label1.TabIndex = 9;
    this.label1.Text = "Выполнить сценарий по завершении";
    this._labelObjTypes.AutoSize = true;
    this._labelObjTypes.Location = new Point(10, 272);
    this._labelObjTypes.Name = "_labelObjTypes";
    this._labelObjTypes.Size = new Size(106, 13);
    this._labelObjTypes.TabIndex = 9;
    this._labelObjTypes.Text = "По типам объектов";
    this._comboFinalScript.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboFinalScript.FormattingEnabled = true;
    this._comboFinalScript.Items.AddRange(new object[1]
    {
      (object) "(Нет)"
    });
    this._comboFinalScript.Location = new Point(7, 495);
    this._comboFinalScript.Name = "_comboFinalScript";
    this._comboFinalScript.Size = new Size(316, 21);
    this._comboFinalScript.TabIndex = 15;
    this._comboScript.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboScript.FormattingEnabled = true;
    this._comboScript.Items.AddRange(new object[1]
    {
      (object) "(Нет)"
    });
    this._comboScript.Location = new Point(7, 434);
    this._comboScript.Name = "_comboScript";
    this._comboScript.Size = new Size(316, 21);
    this._comboScript.TabIndex = 15;
    this.bevel1.Location = new Point(1, 476);
    this.bevel1.Name = "bevel1";
    this.bevel1.Shape = BevelShape.BottomLine;
    this.bevel1.Size = new Size(322, 10);
    this.bevel1.TabIndex = 8;
    this.bevel1.Text = "bevel1";
    this._bevelObjTypes.Location = new Point(1, 272);
    this._bevelObjTypes.Name = "_bevelObjTypes";
    this._bevelObjTypes.Shape = BevelShape.BottomLine;
    this._bevelObjTypes.Size = new Size(322, 10);
    this._bevelObjTypes.TabIndex = 8;
    this._bevelObjTypes.Text = "bevel1";
    this._buttonPrototype.AutoEllipsis = true;
    this._buttonPrototype.ImeMode = ImeMode.NoControl;
    this._buttonPrototype.Location = new Point(207, 384);
    this._buttonPrototype.Name = "_buttonPrototype";
    this._buttonPrototype.Size = new Size(116, 23);
    this._buttonPrototype.TabIndex = 14;
    this._buttonPrototype.Text = "Выбрать...";
    this._buttonPrototype.UseVisualStyleBackColor = true;
    this._checkBoxProto.ImeMode = ImeMode.NoControl;
    this._checkBoxProto.Location = new Point(10, 380);
    this._checkBoxProto.Name = "_checkBoxProto";
    this._checkBoxProto.Size = new Size(187, 33);
    this._checkBoxProto.TabIndex = 13;
    this._checkBoxProto.Text = "Создавать задачи по прототипу";
    this._checkBoxProto.UseVisualStyleBackColor = true;
    this._editIterationName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._editIterationName.BackColor = SystemColors.Control;
    this._editIterationName.Enabled = false;
    this._editIterationName.Location = new Point(10, 239);
    this._editIterationName.Name = "_editIterationName";
    this._editIterationName.Size = new Size(313, 20);
    this._editIterationName.TabIndex = 7;
    this._editIterationName.WordWrap = false;
    this._btnDelObjType.Enabled = false;
    this._btnDelObjType.Location = new Point(91, 316);
    this._btnDelObjType.Name = "_btnDelObjType";
    this._btnDelObjType.Size = new Size(75, 23);
    this._btnDelObjType.TabIndex = 10;
    this._btnDelObjType.Text = "Удалить";
    this._btnDelObjType.UseVisualStyleBackColor = true;
    this._checkBoxAsSubTask.AutoSize = true;
    this._checkBoxAsSubTask.ImeMode = ImeMode.NoControl;
    this._checkBoxAsSubTask.Location = new Point(10, 23);
    this._checkBoxAsSubTask.Name = "_checkBoxAsSubTask";
    this._checkBoxAsSubTask.Size = new Size(183, 17);
    this._checkBoxAsSubTask.TabIndex = 0;
    this._checkBoxAsSubTask.Text = "Импортировать как подзадачи";
    this._checkBoxAsSubTask.UseVisualStyleBackColor = true;
    this._checkBoxImportRoot.AutoSize = true;
    this._checkBoxImportRoot.Checked = true;
    this._checkBoxImportRoot.CheckState = CheckState.Checked;
    this._checkBoxImportRoot.ImeMode = ImeMode.NoControl;
    this._checkBoxImportRoot.Location = new Point(10, 55);
    this._checkBoxImportRoot.Name = "_checkBoxImportRoot";
    this._checkBoxImportRoot.Size = new Size(206, 17);
    this._checkBoxImportRoot.TabIndex = 1;
    this._checkBoxImportRoot.Text = "Импортировать корневые объекты";
    this._checkBoxImportRoot.UseVisualStyleBackColor = true;
    this._labelIterationName.AutoSize = true;
    this._labelIterationName.Enabled = false;
    this._labelIterationName.ImeMode = ImeMode.NoControl;
    this._labelIterationName.Location = new Point(7, 223);
    this._labelIterationName.Name = "_labelIterationName";
    this._labelIterationName.Size = new Size(136, 13);
    this._labelIterationName.TabIndex = 4;
    this._labelIterationName.Text = "Наименование итерации:";
    this._labelMaxLevels.AutoSize = true;
    this._labelMaxLevels.ImeMode = ImeMode.NoControl;
    this._labelMaxLevels.Location = new Point(270, 89);
    this._labelMaxLevels.Name = "_labelMaxLevels";
    this._labelMaxLevels.Size = new Size(56, 13);
    this._labelMaxLevels.TabIndex = 4;
    this._labelMaxLevels.Text = "уровнями";
    this._editMaxLevels.BackColor = SystemColors.Control;
    this._editMaxLevels.Enabled = false;
    this._editMaxLevels.Location = new Point(228, 86);
    this._editMaxLevels.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editMaxLevels.Name = "_editMaxLevels";
    this._editMaxLevels.Size = new Size(38, 20);
    this._editMaxLevels.TabIndex = 3;
    this._editMaxLevels.Value = new Decimal(new int[4]
    {
      3,
      0,
      0,
      0
    });
    this._checkBoxCopySummaries.AutoSize = true;
    this._checkBoxCopySummaries.ImeMode = ImeMode.NoControl;
    this._checkBoxCopySummaries.Location = new Point(10, 119);
    this._checkBoxCopySummaries.Name = "_checkBoxCopySummaries";
    this._checkBoxCopySummaries.Size = new Size(267, 17);
    this._checkBoxCopySummaries.TabIndex = 4;
    this._checkBoxCopySummaries.Text = "Создавать вложенные копии суммарных задач";
    this._checkBoxCopySummaries.UseVisualStyleBackColor = true;
    this._checkBoxCreateIteration.ImeMode = ImeMode.NoControl;
    this._checkBoxCreateIteration.Location = new Point(10, 174);
    this._checkBoxCreateIteration.Name = "_checkBoxCreateIteration";
    this._checkBoxCreateIteration.Size = new Size(315, 45);
    this._checkBoxCreateIteration.TabIndex = 6;
    this._checkBoxCreateIteration.Text = "Сохранить настройки и создать итерации импортируемых объектов для последующей синхронизации изменений состава";
    this._checkBoxCreateIteration.UseVisualStyleBackColor = true;
    this._checkBoxLinear.ImeMode = ImeMode.NoControl;
    this._checkBoxLinear.Location = new Point(10, 145);
    this._checkBoxLinear.Name = "_checkBoxLinear";
    this._checkBoxLinear.Size = new Size(318, 31 /*0x1F*/);
    this._checkBoxLinear.TabIndex = 5;
    this._checkBoxLinear.Text = "Создавать задачи на одном уровне, игнорируя иерархию";
    this._checkBoxLinear.UseVisualStyleBackColor = true;
    this._checkBoxMaxLevels.ImeMode = ImeMode.NoControl;
    this._checkBoxMaxLevels.Location = new Point(10, 81);
    this._checkBoxMaxLevels.Margin = new Padding(0);
    this._checkBoxMaxLevels.Name = "_checkBoxMaxLevels";
    this._checkBoxMaxLevels.Size = new Size(219, 29);
    this._checkBoxMaxLevels.TabIndex = 2;
    this._checkBoxMaxLevels.Text = "Ограничить глубину импорта состава";
    this._checkBoxMaxLevels.UseVisualStyleBackColor = true;
    this._checkBoxAsProject.AutoSize = true;
    this._checkBoxAsProject.Enabled = false;
    this._checkBoxAsProject.Location = new Point(351, 31 /*0x1F*/);
    this._checkBoxAsProject.Name = "_checkBoxAsProject";
    this._checkBoxAsProject.Size = new Size(165, 17);
    this._checkBoxAsProject.TabIndex = 6;
    this._checkBoxAsProject.Text = "Выбранный объект как подпроект";
    this._checkBoxAsProject.UseVisualStyleBackColor = true;
    this._statusStrip.GripStyle = ToolStripGripStyle.Visible;
    this._statusStrip.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._labelFocusedObjectType,
      (ToolStripItem) this._labelFocusedObjectCaption
    });
    this._statusStrip.Location = new Point(0, 615);
    this._statusStrip.Name = "_statusStrip";
    this._statusStrip.Size = new Size(914, 22);
    this._statusStrip.TabIndex = 12;
    this._labelFocusedObjectType.BorderStyle = Border3DStyle.Sunken;
    this._labelFocusedObjectType.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this._labelFocusedObjectType.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
    this._labelFocusedObjectType.ImageAlign = ContentAlignment.MiddleLeft;
    this._labelFocusedObjectType.Margin = new Padding(6, 3, 0, 2);
    this._labelFocusedObjectType.Name = "_labelFocusedObjectType";
    this._labelFocusedObjectType.Size = new Size(0, 17);
    this._labelFocusedObjectType.TextAlign = ContentAlignment.MiddleLeft;
    this._labelFocusedObjectCaption.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this._labelFocusedObjectCaption.ImageAlign = ContentAlignment.MiddleLeft;
    this._labelFocusedObjectCaption.Name = "_labelFocusedObjectCaption";
    this._labelFocusedObjectCaption.Size = new Size(893, 17);
    this._labelFocusedObjectCaption.Spring = true;
    this._labelFocusedObjectCaption.TextAlign = ContentAlignment.MiddleLeft;
    this._panel1.Dock = DockStyle.Fill;
    this._panel1.Location = new Point(0, 28);
    this._panel1.Name = "_panel1";
    this._panel1.Padding = new Padding(3);
    this._panel1.Size = new Size(567, 549);
    this._panel1.TabIndex = 13;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(914, 637);
    this.Controls.Add((Control) this._panel1);
    this.Controls.Add((Control) this._panelTreeCaption);
    this.Controls.Add((Control) this._panelRight);
    this.Controls.Add((Control) this._statusStrip);
    this.FormBorderStyle = FormBorderStyle.Sizable;
    this.MinimumSize = new Size(930, 676);
    this.Name = nameof (ImportObjectsFormAdvBase);
    this.Text = "Импорт объектов";
    this.Controls.SetChildIndex((Control) this._statusStrip, 0);
    this.Controls.SetChildIndex((Control) this._pnlDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._bevelDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._panelRight, 0);
    this.Controls.SetChildIndex((Control) this._panelTreeCaption, 0);
    this.Controls.SetChildIndex((Control) this._panel1, 0);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this._panelTreeCaption.ResumeLayout(false);
    this._panelTreeCaption.PerformLayout();
    this._panelRight.ResumeLayout(false);
    this._groupBoxSettings.ResumeLayout(false);
    this._groupBoxSettings.PerformLayout();
    this._editMaxLevels.EndInit();
    this._statusStrip.ResumeLayout(false);
    this._statusStrip.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
