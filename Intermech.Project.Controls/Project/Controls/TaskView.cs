// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.TaskView
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Metadata;
using Intermech.Workflow.Design;
using OfficePickers.ColorPicker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>Панель свойств задачи</summary>
[DefaultEvent("PropertyChanged")]
public class TaskView : UserControl, INotifyPropertyChanged
{
  [CanBeNull]
  private Task _task;
  /// <summary>Имя параметра задачи (напр. значение DataPropertyName у колонки
  /// ProjectDataGridView) =&gt; Закладка, например по-умолчанию отображаемая в диалоге
  /// для этого параметра при двойном клике на соотв. ячейке</summary>
  internal static readonly IReadOnlyDictionary<string, TaskView.Page> ParamsBinding2Page = (IReadOnlyDictionary<string, TaskView.Page>) new Dictionary<string, TaskView.Page>()
  {
    {
      "Name",
      TaskView.Page.Common
    },
    {
      "DurationString",
      TaskView.Page.Common
    },
    {
      "PriorityString",
      TaskView.Page.Common
    },
    {
      "WorkString",
      TaskView.Page.Common
    },
    {
      "StartString",
      TaskView.Page.Common
    },
    {
      "FinishString",
      TaskView.Page.Common
    },
    {
      "FactStart",
      TaskView.Page.Common
    },
    {
      "FactFinish",
      TaskView.Page.Common
    },
    {
      "StatusString",
      TaskView.Page.Common
    },
    {
      "PercentCompletedString",
      TaskView.Page.Common
    },
    {
      "DependenciesString",
      TaskView.Page.Precursors
    },
    {
      "AssignmentsString",
      TaskView.Page.Resources
    },
    {
      "ChiefString",
      TaskView.Page.Resources
    },
    {
      "StartConstraintString",
      TaskView.Page.Extra
    },
    {
      "FinishConstraintString",
      TaskView.Page.Extra
    },
    {
      "WbsCode",
      TaskView.Page.Extra
    },
    {
      "CostString",
      TaskView.Page.Extra
    },
    {
      "Milestone",
      TaskView.Page.Extra
    },
    {
      "NotesString",
      TaskView.Page.Notes
    }
  };
  [NotNull]
  private readonly Dictionary<string, DependencyType> _dependencyTypeMapper = new Dictionary<string, DependencyType>();
  private readonly TaskView.TaskInfo _noTaskComboItem = new TaskView.TaskInfo(string.Empty, string.Empty);
  public bool ReadOnly;
  private int _lastStandardTabIndex;
  private long _clonedObjectID;
  [NotNull]
  private readonly Dictionary<string, TaskView.TaskInfo> _allTasks = new Dictionary<string, TaskView.TaskInfo>();
  private Label _labelNotes;
  private Label _labelPercentCompleted;
  private Label _labelStatus;
  private Label _labelWork;
  private NumericUpDown _percentCompletedUpDown;
  private TabControl _pageControl;
  private TabPage _generalPage;
  private TabPage _notesPage;
  private TextBox _notesTextBox;
  private TabPage _miscPage;
  private TabPage _dependenciesPage;
  private TabPage _resourcesPage;
  private CheckBox _milestoneCheckBox;
  private TextBox _textBoxCost;
  private Label _labelCost;
  private TextBox _wbsCodeTextBox;
  private Label _labelWbsCode;
  private Label _label1;
  private EnhDataGridView _dependenciesView;
  private EnhDataGridView _resultsView;
  private System.Windows.Forms.ComboBox _constraintComboBox;
  private Label _label2;
  private DateTimePicker _constraintDatePicker;
  private Label _label3;
  private Label _statusLabel;
  private ChiefEdit _chiefEdit;
  private TabPage _resultsPage;
  private TabPage _srcDataPage;
  private CheckBox _verifySchemeCheckBox;
  private ButtonEdit _verifySchemeEdit;
  private CheckBox _copyResultsToSrcDataCheckBox;
  private Panel _resultSettingsPanel;
  private Panel _panel1;
  private Label _label4;
  private NumericUpDown _priorityUpDown;
  private DateTimePicker _finishPicker;
  private DateTimePicker _startPicker;
  private Label _labelFinish;
  private Label _labelStart;
  private Label _labelPriority;
  private ButtonEdit _durationTextBox;
  private Label _labelDuration;
  private TextBox _nameTextBox;
  private Label _labelTaskName;
  private Panel _factPanel;
  private ColoredDateTimePicker _factFinishPicker;
  private DateTimePicker _factStartPicker;
  private Label _factFinishLabel;
  private Label _factStartLabel;
  private Panel _panel3;
  private DataGridViewTextBoxColumn _idColumn;
  private DataGridViewComboBoxColumn _nameColumn;
  private DataGridViewComboBoxColumn _depTypeColumn;
  private DataGridViewTextBoxColumn _lagColumn;
  private DataGridViewButtonTextBoxColumn _dataGridViewButtonTextBoxColumn1;
  private DataGridViewTextBoxColumn _dataGridViewTextBoxColumn3;
  private TextBox _workTextBox;
  private ComboBoxColorPicker _colorCombo;
  private CheckBox _checkBoxSetColor;
  private CheckBox _checkBoxUseActualScheme;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelNotes
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelNotes.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelPercentCompleted
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelPercentCompleted.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelStatus
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelStatus.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelWork
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelWork.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown PercentCompletedUpDown
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._percentCompletedUpDown.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabControl PageControl
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._pageControl.CheckInitializedIn<TabControl>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage GeneralPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._generalPage.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage NotesPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._notesPage.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TextBox NotesTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._notesTextBox.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage MiscPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._miscPage.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage DependenciesPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._dependenciesPage;
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage ResourcesPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._resourcesPage;
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox MilestoneCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._milestoneCheckBox.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TextBox TextBoxCost
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBoxCost.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelCost
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelCost.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TextBox WbsCodeTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._wbsCodeTextBox.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelWbsCode
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelWbsCode.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label1.CheckInitializedIn<Label>((object) this);
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridView DependenciesView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._dependenciesView.CheckInitializedIn<EnhDataGridView>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridView ResultsView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._resultsView.CheckInitializedIn<EnhDataGridView>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal System.Windows.Forms.ComboBox ConstraintComboBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._constraintComboBox.CheckInitializedIn<System.Windows.Forms.ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label2.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DateTimePicker ConstraintDatePicker
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._constraintDatePicker.CheckInitializedIn<DateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label3
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label3.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label StatusLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._statusLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ChiefEdit ChiefEdit
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._chiefEdit.CheckInitializedIn<ChiefEdit>((object) this);
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage ResultsPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._resultsPage;
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage SrcDataPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._srcDataPage;
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox VerifySchemeCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._verifySchemeCheckBox.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ButtonEdit VerifySchemeEdit
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._verifySchemeEdit.CheckInitializedIn<ButtonEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox CheckBoxUseActualScheme
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxUseActualScheme.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox CopyResultsToSrcDataCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._copyResultsToSrcDataCheckBox.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Panel ResultSettingsPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._resultSettingsPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
  protected internal Label Label4
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label4.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown PriorityUpDown
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._priorityUpDown.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DateTimePicker FinishPicker
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._finishPicker.CheckInitializedIn<DateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DateTimePicker StartPicker
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._startPicker.CheckInitializedIn<DateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelFinish
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelFinish.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelStart
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelStart.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelPriority
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelPriority.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ButtonEdit DurationTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._durationTextBox.CheckInitializedIn<ButtonEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelDuration
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelDuration.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TextBox NameTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._nameTextBox.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelTaskName
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelTaskName.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Panel FactPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._factPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ColoredDateTimePicker FactFinishPicker
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._factFinishPicker.CheckInitializedIn<ColoredDateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DateTimePicker FactStartPicker
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._factStartPicker.CheckInitializedIn<DateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label FactFinishLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._factFinishLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label FactStartLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._factStartLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Panel Panel3
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel3.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewTextBoxColumn IdColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._idColumn.CheckInitializedIn<DataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewComboBoxColumn NameColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._nameColumn.CheckInitializedIn<DataGridViewComboBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewComboBoxColumn DepTypeColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._depTypeColumn.CheckInitializedIn<DataGridViewComboBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewTextBoxColumn LagColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._lagColumn.CheckInitializedIn<DataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewButtonTextBoxColumn DataGridViewButtonTextBoxColumn1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._dataGridViewButtonTextBoxColumn1.CheckInitializedIn<DataGridViewButtonTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn3
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._dataGridViewTextBoxColumn3.CheckInitializedIn<DataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TextBox WorkTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._workTextBox.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBoxColorPicker ColorCombo
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._colorCombo.CheckInitializedIn<ComboBoxColorPicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox CheckBoxSetColor
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxSetColor.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [Description("Occurs when a property of the control has changed")]
  [Category("Behavior")]
  public event PropertyChangedEventHandler PropertyChanged;

  public TaskView()
  {
    this.InitializeComponent();
    this.DepTypeColumn.Items.Clear();
    foreach (DependencyType dependencyType in Enum.GetValues(typeof (DependencyType)))
    {
      string enumDescription = SimpleFuncs.GetEnumDescription((Enum) dependencyType);
      this._dependencyTypeMapper.Add(enumDescription, dependencyType);
      this.DepTypeColumn.Items.Add((object) enumDescription);
    }
    this.VerifySchemeEdit.Enabled = false;
  }

  protected override void Dispose(bool disposing)
  {
    if (this._clonedObjectID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._clonedObjectID, false);
        if (dbObject != null)
        {
          dbObject.Delete(0L);
          this._clonedObjectID = 0L;
        }
      }
    }
    base.Dispose(disposing);
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(null)]
  public Task Task
  {
    get => this._task;
    set
    {
      if (value == this.Task)
        return;
      this._task = value;
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(nameof (Task)));
    }
  }

  private bool UpdateReadOnly([NotNull] Control control, [NotNull] string propName)
  {
    if (!this.ReadOnly)
    {
      if (propName != string.Empty)
      {
        Task task = this._task;
        if ((task != null ? (!task.CanSetProperty(propName, (object) null, true) ? 1 : 0) : 1) != 0)
          goto label_3;
      }
      return false;
    }
label_3:
    switch (control)
    {
      case TextBox textBox:
        textBox.ReadOnly = true;
        break;
      case DataGridView dataGridView:
        dataGridView.ReadOnly = true;
        break;
      default:
        control.Enabled = false;
        break;
    }
    return true;
  }

  private bool UpdateReadOnly([NotNull] Control control)
  {
    return this.UpdateReadOnly(control, string.Empty);
  }

  private static bool IsEditable([NotNull] Control control)
  {
    switch (control)
    {
      case TextBox textBox:
        return !textBox.ReadOnly;
      case DataGridView dataGridView:
        return !dataGridView.ReadOnly;
      default:
        return control.Enabled;
    }
  }

  public void LoadFromTask([NotNull] Task task, bool readOnly)
  {
    this._task = task;
    if (task.EditingLocked)
      readOnly = true;
    this.ReadOnly = readOnly;
    string pickerDateFormat = ProjectDisplayOptions.ToPickerDateFormat(task.DateFormat);
    this.StartPicker.CustomFormat = pickerDateFormat;
    this.FinishPicker.CustomFormat = pickerDateFormat;
    this.ConstraintDatePicker.CustomFormat = pickerDateFormat;
    this.FactStartPicker.CustomFormat = pickerDateFormat;
    this.FactFinishPicker.CustomFormat = pickerDateFormat;
    this.NameTextBox.Text = task.Name;
    this.UpdateReadOnly((Control) this.NameTextBox, "Name");
    this.DurationTextBox.Text = task.DurationString;
    this.UpdateReadOnly((Control) this.DurationTextBox, "Work");
    this.PercentCompletedUpDown.Value = (Decimal) task.PercentCompleted;
    this.UpdateReadOnly((Control) this.PercentCompletedUpDown, "PercentCompleted");
    this.PriorityUpDown.Value = (Decimal) task.Priority;
    this.UpdateReadOnly((Control) this.PriorityUpDown, "Priority");
    try
    {
      if (task.Start >= this.StartPicker.MinDate)
      {
        if (task.Start <= this.StartPicker.MaxDate)
          this.StartPicker.Value = task.Start;
      }
    }
    catch (ArgumentOutOfRangeException ex)
    {
    }
    this.UpdateReadOnly((Control) this.StartPicker, "Start");
    try
    {
      if (task.Finish >= this.FinishPicker.MinDate)
      {
        if (task.Finish <= this.FinishPicker.MaxDate)
          this.FinishPicker.Value = task.Finish;
      }
    }
    catch (ArgumentOutOfRangeException ex)
    {
    }
    this.UpdateReadOnly((Control) this.FinishPicker, "Work");
    this.StatusLabel.Text = task.StatusString;
    if (task.FactStart != DateTime.MinValue)
    {
      this.FactStartPicker.Value = task.FactStart;
      this.FactStartPicker.Tag = (object) 1;
    }
    else
    {
      this.FactStartPicker.Visible = false;
      this.FactStartLabel.Visible = false;
    }
    if (task.FactFinish != DateTime.MinValue)
    {
      this.FactFinishPicker.Value = task.FactFinish;
      this.FactFinishPicker.Tag = (object) 1;
      if (this.FactFinishPicker.Value > this.FinishPicker.Value)
        this.FactFinishPicker.ForeColor = Color.Red;
    }
    else
    {
      this.FactFinishPicker.Visible = false;
      this.FactFinishLabel.Visible = false;
    }
    this.FactPanel.Visible = 1.Equals(this.FactStartPicker.Tag) || 1.Equals(this.FactFinishPicker.Tag);
    this.UpdateReadOnly((Control) this.ConstraintComboBox, "ConstraintType");
    this.UpdateReadOnly((Control) this.ConstraintDatePicker, "ConstraintType");
    this.UpdateReadOnly((Control) this.CheckBoxSetColor, "TaskColor");
    ControlFuncs.SetNullablePickerValue(this.ConstraintDatePicker, task.ConstraintDate);
    List<Enum> skip = new List<Enum>((IEnumerable<Enum>) new Enum[1]
    {
      (Enum) ConstraintType.Undefined
    });
    if (!(task is Intermech.Project.Project))
      skip.Add((Enum) ConstraintType.ManualPlanning);
    ControlFuncs.EnumToCombo(this.ConstraintComboBox, (Enum) task.ConstraintType, skip);
    ComboBoxColorPicker colorCombo = this.ColorCombo;
    Color? taskColor = task.TaskColor;
    Color color = taskColor ?? IMProject.DefaultTaskColor;
    colorCombo.Color = color;
    CheckBox checkBoxSetColor = this.CheckBoxSetColor;
    taskColor = task.TaskColor;
    int num = taskColor.HasValue ? 1 : 0;
    checkBoxSetColor.Checked = num != 0;
    this.CheckBoxSetColor_CheckedChanged((object) null, EventArgs.Empty);
    this.ColorCombo.Enabled = this.CheckBoxSetColor.Enabled && this.CheckBoxSetColor.Checked;
    this.ConstraintComboBox_SelectedIndexChanged((object) null, EventArgs.Empty);
    this.WbsCodeTextBox.Text = task.WbsCode;
    this.WbsCodeTextBox.ReadOnly = true;
    this.MilestoneCheckBox.Checked = task.Milestone;
    this.UpdateReadOnly((Control) this.MilestoneCheckBox);
    this.NotesTextBox.Text = task.Notes;
    this.UpdateReadOnly((Control) this.NotesTextBox);
    if (task.Partial)
      this.HidePage(ref this._dependenciesPage);
    this.UpdateReadOnly((Control) this.VerifySchemeCheckBox);
    this.VerifySchemeID = task.VerifySchemeID;
    this.UpdateReadOnly((Control) this.CopyResultsToSrcDataCheckBox);
    this.CopyResultsToSrcDataCheckBox.Checked = task.PropagateResults;
    this.UpdateReadOnly((Control) this.CheckBoxUseActualScheme);
    this.CheckBoxUseActualScheme.Checked = task.UseActualScheme;
    this._lastStandardTabIndex = this.PageControl.TabCount - 1;
    IUserSession session = task.GetSession();
    try
    {
      ICollection<FormInformation> formsForObjectType = session.GetCustomService<IFormDesignerService>("Forms service not found").GetFormsForObjectType((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task, session.SessionGUID);
      if (formsForObjectType == null || formsForObjectType.Count <= 0)
        return;
      foreach (FormInformation formInformation in (IEnumerable<FormInformation>) formsForObjectType)
      {
        TabPage tabPage = new TabPage(formInformation.Caption);
        tabPage.Tag = (object) formInformation.ID;
        this.PageControl.TabPages.Add(tabPage);
      }
    }
    finally
    {
      task.ReleaseSession();
    }
  }

  private void HidePage([CanBeNull] ref TabPage page)
  {
    if (page == null)
      return;
    this.PageControl.TabPages.Remove(page);
    page = (TabPage) null;
  }

  public void SaveToTask([NotNull] Task task)
  {
    string empty = string.Empty;
    task.Name = this.NameTextBox.Text;
    if (TaskView.IsEditable((Control) this.PercentCompletedUpDown))
      task.PercentCompleted = (double) this.PercentCompletedUpDown.Value;
    if (TaskView.IsEditable((Control) this.PriorityUpDown))
      task.Priority = (int) this.PriorityUpDown.Value;
    bool flag1 = TaskView.IsEditable((Control) this.DurationTextBox) && this.DurationTextBox.Text != task.DurationString;
    bool flag2 = TaskView.IsEditable((Control) this.StartPicker) && this.StartPicker.Value != task.Start;
    bool flag3 = TaskView.IsEditable((Control) this.FinishPicker) && this.FinishPicker.Value != task.Finish;
    KeyValuePair<Enum, string> selectedItem = (KeyValuePair<Enum, string>) this.ConstraintComboBox.SelectedItem;
    int num = !TaskView.IsEditable((Control) this.ConstraintComboBox) ? 0 : (task.ConstraintType != (ConstraintType) selectedItem.Key ? 1 : 0);
    bool flag4 = TaskView.IsEditable((Control) this.ConstraintDatePicker) && task.ConstraintDate != ControlFuncs.GetNullablePickerValue(this.ConstraintDatePicker);
    if (flag1)
      task.DurationString = this.DurationTextBox.Text;
    if (flag2)
      task.Start = this.StartPicker.Value;
    if (flag3)
      task.Finish = this.FinishPicker.Value;
    if (num != 0)
      task.ConstraintType = (ConstraintType) selectedItem.Key;
    if ((num | (flag4 ? 1 : 0)) != 0)
      task.ConstraintDate = ControlFuncs.GetNullablePickerValue(this.ConstraintDatePicker);
    task.Milestone = this.MilestoneCheckBox.Checked;
    task.Notes = this.NotesTextBox.Text;
    if (this.DependenciesPage?.Tag != null && task.CanSetProperty("Dependencies", (object) null) && this._dependenciesView != null)
    {
      List<Dependency> dependencyList = new List<Dependency>();
      foreach (DataGridViewRow row in (IEnumerable) this._dependenciesView.Rows)
      {
        if (row.Tag is Dependency tag2)
          dependencyList.Add(tag2);
        else if (row.Tag is TaskView.TaskInfo tag1 && tag1 != this._noTaskComboItem)
        {
          Dependency dependency = row.Cells[this.IdColumn.Index].Tag as Dependency;
          DependencyType dependencyType;
          if (this._dependencyTypeMapper.TryGetValue(row.Cells[this.DepTypeColumn.Index].Value.ToString(), out dependencyType))
          {
            Task byIndexString = task.Project.Tasks.FindByIndexString(tag1.ID);
            if (dependency == null)
            {
              try
              {
                dependency = new Dependency(byIndexString, dependencyType);
                dependency.Task = task;
              }
              catch (Exception ex)
              {
                dependency = (Dependency) null;
                if (empty != string.Empty)
                  empty += "\r\n";
                empty += ex.Message;
              }
            }
            else
            {
              if (byIndexString != dependency.DependentOfTask)
                dependency.DependentOfTask = byIndexString;
              if (dependency.DependencyType != dependencyType)
                dependency.DependencyType = dependencyType;
            }
          }
          if (dependency != null)
          {
            dependency.LagString = row.Cells[this.LagColumn.Index].Value != null ? row.Cells[this.LagColumn.Index].Value.ToString() : string.Empty;
            dependencyList.Add(dependency);
          }
        }
      }
      for (int index = task.Dependencies.Count - 1; index >= 0; --index)
      {
        if (!dependencyList.Contains(task.Dependencies[index]))
          task.Dependencies.RemoveAt(index);
      }
    }
    if (this.ResourcesPage.Tag != null && !task.Milestone && task.CanSetProperty("Assignments", (object) null))
    {
      List<Assignment> assignmentList = new List<Assignment>();
      for (int index = 0; index < this.ResultsView.Rows.Count; ++index)
      {
        DataGridViewRow row = this.ResultsView.Rows[index];
        if (row.Tag != null)
        {
          TaskView.ResourceInfo tag = row.Tag as TaskView.ResourceInfo;
          Assignment assignment1 = (Assignment) null;
          if (tag.OldID != 0L)
            assignment1 = task.Assignments.FindByResourceObjectID(tag.OldID, new bool?(false));
          if (assignment1 != null)
            task.Assignments.Remove(assignment1);
          Resource resource = new Resource((ISessionProvider) task, tag.NewID, row.Cells[0].Value.ToString(), tag.ObjectType);
          Assignment assignment2 = new Assignment(resource);
          task.Assignments.Add(assignment2);
          assignment2.Resource = resource;
          assignment2.UnitsString = row.Cells[1].Value.ToString();
          if (!assignmentList.Contains(assignment2))
            assignmentList.Add(assignment2);
        }
      }
      for (int index = task.Assignments.Count - 1; index >= 0; --index)
      {
        if (!task.Assignments[index].IsChief && !assignmentList.Contains(task.Assignments[index]))
          task.Assignments.RemoveAt(index);
      }
      if (this.ChiefEdit.Enabled)
        this.ChiefEdit.ToTask(task);
    }
    if (this.SrcDataPage.Tag != null && task.CanSetProperty("SrcData", (object) null) && this.SrcDataPage.Tag is AttachmentsView tag3 && tag3.Modified)
      task.SrcData = tag3.Attachments;
    task.VerifySchemeID = this.VerifySchemeID;
    task.PropagateResults = this.CopyResultsToSrcDataCheckBox.Checked;
    task.UseActualScheme = this.CheckBoxUseActualScheme.Checked;
    if (this.ResultsPage.Tag != null && !this.ResultsPage.Tag.Equals((object) 0) && task.CanSetProperty("Results", (object) null) && this.ResultsPage.Tag is AttachmentsView tag4 && tag4.Modified)
      task.Results = tag4.Attachments;
    if (this.CheckBoxSetColor.Enabled)
      task.TaskColor = !this.CheckBoxSetColor.Checked || !(this.ColorCombo.Color != Color.Empty) ? new Color?() : new Color?(this.ColorCombo.Color);
    bool flag5 = false;
    for (int index = this._lastStandardTabIndex + 1; index < this.PageControl.TabCount; ++index)
    {
      FormDesignerView control = this.PageControl.TabPages[index].Controls.Count > 0 ? this.PageControl.TabPages[index].Controls[0] as FormDesignerView : (FormDesignerView) null;
      if (control != null)
      {
        control.SaveForm();
        flag5 = true;
      }
    }
    if (flag5)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._clonedObjectID, false);
        if (dbObject != null)
        {
          IDBAttributeCollection attributes = dbObject.Attributes;
          for (int AttrIndex = 0; AttrIndex < attributes.Count; ++AttrIndex)
          {
            int attributeId = attributes[AttrIndex].AttributeID;
            if (attributeId != (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Modified && attributes[AttrIndex].Value != null)
              task.SetAttributeValue(attributeId, attributes[AttrIndex].Value);
          }
        }
      }
    }
    if (empty != string.Empty)
      throw new Exception(empty);
  }

  private void DependenciesView_CellValidating([CanBeNull] object sender, [NotNull] DataGridViewCellValidatingEventArgs e)
  {
    if (e.ColumnIndex != this.NameColumn.Index)
      return;
    int rowIndex = e.RowIndex;
  }

  private void DependenciesView_ComboSelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    DataGridViewCell currentCell = this.DependenciesView.CurrentCell;
    if (currentCell.ColumnIndex != this.NameColumn.Index)
      return;
    int rowIndex = currentCell.RowIndex;
    if (rowIndex < 0)
      return;
    DataGridViewCell cell1 = this.DependenciesView.Rows[rowIndex].Cells[this.NameColumn.Index];
    DataGridViewCell cell2 = this.DependenciesView.Rows[rowIndex].Cells[this.IdColumn.Index];
    if (Intermech.Diagnostics.Check.Is<System.Windows.Forms.ComboBox>(sender, nameof (sender)).SelectedItem is TaskView.TaskInfo selectedItem)
    {
      cell2.Value = (object) selectedItem.ID;
      this.DependenciesView.Rows[rowIndex].Cells[this.NameColumn.Index].Value = (object) selectedItem;
    }
    else
      cell2.Value = (object) "?";
    this.DependenciesView.Rows[rowIndex].Tag = (object) selectedItem;
    DataGridViewCell cell3 = this.DependenciesView.Rows[rowIndex].Cells[this.DepTypeColumn.Index];
    if (!cell3.ReadOnly)
      return;
    cell3.ReadOnly = cell1.Value == null;
    cell3.Value = (object) SimpleFuncs.GetEnumDescription((Enum) DependencyType.FinishStart);
  }

  private void DependenciesView_EditingControlShowing(
    [CanBeNull] object sender,
    [NotNull] DataGridViewEditingControlShowingEventArgs e)
  {
    if (!(e.Control is System.Windows.Forms.ComboBox control))
      return;
    control.SelectedIndexChanged -= new EventHandler(this.DependenciesView_ComboSelectedIndexChanged);
    control.SelectedIndexChanged += new EventHandler(this.DependenciesView_ComboSelectedIndexChanged);
    control.DropDown += new EventHandler(TaskView.DependenciesView_ComboDropDown);
    control.GotFocus += new EventHandler(TaskView.DependenciesView_ComboDropDown);
  }

  private static void DependenciesView_ComboDropDown([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(sender is System.Windows.Forms.ComboBox comboBox))
      return;
    comboBox.BackColor = Color.White;
  }

  private void DependenciesView_RowsAdded([CanBeNull] object sender, [NotNull] DataGridViewRowsAddedEventArgs e)
  {
    for (int index1 = 0; index1 < e.RowCount; ++index1)
    {
      int index2 = e.RowIndex + index1;
      DataGridViewCell cell = this.DependenciesView.Rows[index2].Cells[this.NameColumn.Index];
      this.DependenciesView.Rows[index2].Cells[this.DepTypeColumn.Index].ReadOnly = cell.Value == null;
      if (cell.Value == null)
        cell.Value = (object) this._noTaskComboItem;
    }
  }

  private void DependenciesView_DataError([CanBeNull] object sender, [NotNull] DataGridViewDataErrorEventArgs e)
  {
    Exception exception = e.Exception;
    if (exception != null)
    {
      string message = exception.Message;
    }
    e.Cancel = false;
  }

  private void DependenciesView_CellFormatting([CanBeNull] object sender, [NotNull] DataGridViewCellFormattingEventArgs e)
  {
    if (e.ColumnIndex != this.NameColumn.Index || e.Value != this._noTaskComboItem || this.DependenciesView == null)
      return;
    DataGridViewRow row = this.DependenciesView.Rows[e.RowIndex];
    if (!(row.Tag is Dependency))
      return;
    e.Value = (object) row.Tag.ToString();
    e.FormattingApplied = true;
  }

  private void DependenciesView_CellParsing([CanBeNull] object sender, [NotNull] DataGridViewCellParsingEventArgs e)
  {
    if (e.ColumnIndex == this.NameColumn.Index && this.DependenciesView != null)
    {
      e.Value = this.DependenciesView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
      e.ParsingApplied = true;
    }
    else
      e.ParsingApplied = false;
  }

  private void resourcesView_EditorButtonClicked([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    IDBTypedObjectID[] dbTypedObjectIdArray = ClientProject.BrowseForResources(this._task.Project);
    if (dbTypedObjectIdArray == null)
      return;
    DataGridViewRow dataGridViewRow1 = this.ResultsView.CurrentRow;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (IDBTypedObjectID dbTypedObjectId in dbTypedObjectIdArray)
      {
        long id = dbTypedObjectId.ObjectID;
        if (!this.ResultsView.Rows.Cast<DataGridViewRow>().Select<DataGridViewRow, object>((Func<DataGridViewRow, object>) (dataGridViewRow => dataGridViewRow.Tag)).OfType<TaskView.ResourceInfo>().Any<TaskView.ResourceInfo>((Func<TaskView.ResourceInfo, bool>) (resourceInfo => resourceInfo.NewID == id)))
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(id, false);
          if (dbObject != null && dataGridViewRow1 != null)
          {
            if (!(dataGridViewRow1.Tag is TaskView.ResourceInfo resourceInfo))
            {
              resourceInfo = new TaskView.ResourceInfo(0L, id, dbTypedObjectId.ObjectType);
            }
            else
            {
              resourceInfo.NewID = id;
              resourceInfo.ObjectType = dbTypedObjectId.ObjectType;
            }
            dataGridViewRow1.Tag = (object) resourceInfo;
            dataGridViewRow1.Cells[0].Value = (object) dbObject.Caption;
            if (dataGridViewRow1.Cells[1].Value == null)
              dataGridViewRow1.Cells[1].Value = (object) "100%";
            while (dataGridViewRow1.Index < this.ResultsView.RowCount - 1)
            {
              dataGridViewRow1 = this.ResultsView.Rows[dataGridViewRow1.Index + 1];
              if (dataGridViewRow1.Tag == null)
                break;
            }
          }
        }
      }
    }
  }

  private void ConstraintComboBox_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    ConstraintType constraintType = Intermech.Diagnostics.Check.Is<ConstraintType>((object) Intermech.Diagnostics.Check.Is<KeyValuePair<Enum, string>>(this.ConstraintComboBox.SelectedItem).Key);
    this.ConstraintDatePicker.Enabled = !this.ReadOnly && constraintType != ConstraintType.AsSoonAsPossible && constraintType != ConstraintType.AsLateAsPossible;
  }

  public void LoadSelectedPage()
  {
    if (this.DependenciesPage != null && this.PageControl.SelectedTab == this.DependenciesPage)
    {
      if (this.DependenciesPage.Tag != null)
        return;
      this.DependenciesPage.Tag = (object) 1;
      this.UpdateReadOnly((Control) this.DependenciesView, "Dependencies");
      this.DependenciesView.Rows.AddCopies(0, 200);
      this.NameColumn.Items.Clear();
      this.NameColumn.DisplayMember = "Name";
      this.NameColumn.ValueMember = "ID";
      List<object> objectList = new List<object>();
      objectList.Add((object) this._noTaskComboItem);
      if (this._task.Project != null)
      {
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this._task.Project.Tasks)
        {
          if (task != this._task && !task.Contains(this._task) && !this._task.Contains(task))
          {
            TaskView.TaskInfo taskInfo;
            if (!this._allTasks.TryGetValue(task.IndexString, out taskInfo))
            {
              taskInfo = new TaskView.TaskInfo(task.IndexString, task.Name);
              this._allTasks.Add(task.IndexString, taskInfo);
            }
            objectList.Add((object) taskInfo);
          }
        }
      }
      this.NameColumn.Items.AddRange(objectList.ToArray());
      int index = 0;
      foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) this._task.Dependencies)
      {
        DataGridViewRow row = this.DependenciesView.Rows[index];
        row.Cells[0].Value = (object) dependency.IndexString;
        DataGridViewCell cell = row.Cells[this.DepTypeColumn.Index];
        cell.Value = (object) SimpleFuncs.GetEnumDescription((Enum) dependency.DependencyType);
        cell.ReadOnly = false;
        row.Cells[this.IdColumn.Index].Tag = (object) dependency;
        TaskView.TaskInfo taskInfo;
        if (this._allTasks.TryGetValue(dependency.IndexString, out taskInfo))
        {
          row.Cells[1].Value = (object) taskInfo;
          row.Tag = (object) taskInfo;
        }
        else
        {
          row.ReadOnly = true;
          if (row.DefaultCellStyle != null)
            row.DefaultCellStyle.ForeColor = SystemColors.GrayText;
          row.Tag = (object) dependency;
        }
        row.Cells[this.LagColumn.Index].Value = (object) dependency.LagString;
        ++index;
      }
    }
    else if (this.ResourcesPage != null && this.PageControl.SelectedTab == this.ResourcesPage)
    {
      if (this._task.HasSubTasks || this.MilestoneCheckBox.Checked)
        this.ResultsView.Visible = false;
      else
        this.ResultsView.Visible = true;
      if (this.ResourcesPage.Tag != null)
        return;
      this.ResourcesPage.Tag = (object) 1;
      this.UpdateReadOnly((Control) this.ResultsView, "Assignments");
      this.ResultsView.Rows.Clear();
      this.ResultsView.Rows.AddCopies(0, 200);
      int index = 0;
      foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) this._task.Assignments)
      {
        if (!assignment.IsChief)
        {
          DataGridViewRow row = this.ResultsView.Rows[index];
          row.Tag = (object) new TaskView.ResourceInfo(assignment.Resource.ObjectID, assignment.Resource.ObjectType);
          row.Cells[0].Value = (object) assignment.Resource.Name;
          row.Cells[1].Value = (object) assignment.UnitsString;
          ++index;
        }
      }
      this.UpdateReadOnly((Control) this.ChiefEdit, "Assignments");
      this.ChiefEdit.FromTask(this._task);
    }
    else if (this.SrcDataPage != null && this.PageControl.SelectedTab == this.SrcDataPage && this.SrcDataPage.Tag == null)
    {
      TaskDataView taskDataView = new TaskDataView();
      taskDataView.Parent = (Control) this.SrcDataPage;
      taskDataView.Dock = DockStyle.Fill;
      taskDataView.Load(this._task.SrcData);
      this.SrcDataPage.Tag = (object) taskDataView;
      taskDataView.ReadOnly = this.ReadOnly;
    }
    else if (this.ResultsPage != null && this.PageControl.SelectedTab == this.ResultsPage && this.ResultsPage.Tag == null)
    {
      if (this._task.Status != TaskStatus.NotStarted)
      {
        TaskResultsView taskResultsView = new TaskResultsView();
        taskResultsView.Parent = (Control) this.ResultsPage;
        taskResultsView.Dock = DockStyle.Fill;
        this.ResultSettingsPanel.Dock = DockStyle.Bottom;
        taskResultsView.BringToFront();
        taskResultsView.Load(this._task.Results);
        this.ResultsPage.Tag = (object) taskResultsView;
        taskResultsView.ReadOnly = this.ReadOnly;
      }
      else
        this.ResultsPage.Tag = (object) 0;
    }
    else
    {
      if (this.PageControl.SelectedIndex <= this._lastStandardTabIndex)
        return;
      TabPage selectedTab = this.PageControl.SelectedTab;
      FormDesignerView control = selectedTab.Controls.Count > 0 ? selectedTab.Controls[0] as FormDesignerView : (FormDesignerView) null;
      if (this._clonedObjectID == 0L)
      {
        IUserSession session = this._task.GetSession();
        try
        {
          IDBObjectCollection objectCollection = session.GetObjectCollection(this._task.ObjectTypeID);
          IDBObject dbObject = this._task.ObjectID == 0L ? objectCollection.Create() : objectCollection.Create(this._task.ObjectID);
          this._clonedObjectID = dbObject.ObjectID;
          this._task.WriteCachedAttributes(dbObject, false, true);
        }
        finally
        {
          this._task.ReleaseSession();
        }
      }
      if (control != null)
        return;
      long num = Convert.ToInt64(selectedTab.Tag);
      IUserSession session1 = this._task.GetSession();
      try
      {
        IDBObject objectActualCopy = session1.GetObjectActualCopy(num, false);
        if (objectActualCopy != null)
          num = objectActualCopy.ObjectID;
      }
      finally
      {
        this._task.ReleaseSession();
      }
      FormDesignerView parent = new FormDesignerView(this._clonedObjectID, num);
      parent.Parent = (Control) selectedTab;
      parent.Dock = DockStyle.Fill;
      parent.LoadForm();
      if (!this.ReadOnly)
        return;
      ControlFuncs.SetControlsReadOnly((Control) parent, true);
    }
  }

  private void tabControlTask_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.LoadSelectedPage();
  }

  private void VerifySchemeCheckBox_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.VerifySchemeEdit.Enabled = this.VerifySchemeCheckBox.Checked && !this.ReadOnly;
    if (this.VerifySchemeID != 0L || !this.VerifySchemeEdit.Enabled)
      return;
    this.VerifySchemeEdit_ButtonClick((object) null, (ButtonPressedEventArgs) null);
    if (this.VerifySchemeID != 0L)
      return;
    this.VerifySchemeCheckBox.Checked = false;
  }

  private long VerifySchemeID
  {
    get
    {
      return this.VerifySchemeCheckBox.Checked && this.VerifySchemeEdit.Tag is long ? Convert.ToInt64(this.VerifySchemeEdit.Tag) : 0L;
    }
    set
    {
      this.VerifySchemeEdit.Tag = (object) value;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(value, false);
        if (dbObject != null)
          this.VerifySchemeEdit.Text = dbObject.Caption;
      }
      this.VerifySchemeCheckBox.Checked = value != 0L;
    }
  }

  private void VerifySchemeEdit_ButtonClick([CanBeNull] object sender, [CanBeNull] ButtonPressedEventArgs e)
  {
    long num = wfFunx.BrowseForScheme();
    if (num == -1L)
      return;
    this.VerifySchemeID = num;
  }

  private void VerifySchemeEdit_DoubleClick([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.VerifySchemeEdit_ButtonClick((object) null, (ButtonPressedEventArgs) null);
  }

  private void DurationTextBox_ButtonClick([CanBeNull] object sender, [CanBeNull] ButtonPressedEventArgs e)
  {
    this.ShowDurationForm();
  }

  private void DurationTextBox_DoubleClick([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.ShowDurationForm();
  }

  private void ShowDurationForm()
  {
    DurationForm.ShowUnder((Control) this.DurationTextBox).ValueChanged += new EventHandler(this._form_ValueChanged);
  }

  private void _form_ValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(sender is DurationForm durationForm))
      return;
    this.DurationTextBox.Text = durationForm.Value;
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Dictionary<string, object> Layout
  {
    get
    {
      Dictionary<string, object> dic = new Dictionary<string, object>();
      this.DependenciesView.SaveLayout(dic);
      this.ResultsView.SaveLayout(dic);
      return dic;
    }
    set
    {
      this.DependenciesView.LoadLayout(value);
      this.ResultsView.LoadLayout(value);
    }
  }

  internal void ActivatePage(TaskView.Page page)
  {
    if (page == TaskView.Page.None)
      return;
    this.PageControl.SelectedIndex = (int) (page - 1);
  }

  private void CheckBoxSetColor_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.ColorCombo.Enabled = this.CheckBoxSetColor.Enabled && this.CheckBoxSetColor.Checked;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TaskView));
    DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
    this._labelNotes = new Label();
    this._notesTextBox = new TextBox();
    this._labelStatus = new Label();
    this._workTextBox = new TextBox();
    this._labelWork = new Label();
    this._generalPage = new TabPage();
    this._panel3 = new Panel();
    this._labelPercentCompleted = new Label();
    this._label1 = new Label();
    this._percentCompletedUpDown = new NumericUpDown();
    this._statusLabel = new Label();
    this._factPanel = new Panel();
    this._factFinishPicker = new ColoredDateTimePicker();
    this._factStartPicker = new DateTimePicker();
    this._factFinishLabel = new Label();
    this._factStartLabel = new Label();
    this._panel1 = new Panel();
    this._label4 = new Label();
    this._priorityUpDown = new NumericUpDown();
    this._finishPicker = new DateTimePicker();
    this._startPicker = new DateTimePicker();
    this._labelFinish = new Label();
    this._labelStart = new Label();
    this._labelPriority = new Label();
    this._durationTextBox = new ButtonEdit();
    this._labelDuration = new Label();
    this._nameTextBox = new TextBox();
    this._labelTaskName = new Label();
    this._pageControl = new TabControl();
    this._dependenciesPage = new TabPage();
    this._dependenciesView = new EnhDataGridView();
    this._idColumn = new DataGridViewTextBoxColumn();
    this._nameColumn = new DataGridViewComboBoxColumn();
    this._depTypeColumn = new DataGridViewComboBoxColumn();
    this._lagColumn = new DataGridViewTextBoxColumn();
    this._resourcesPage = new TabPage();
    this._resultsView = new EnhDataGridView();
    this._dataGridViewButtonTextBoxColumn1 = new DataGridViewButtonTextBoxColumn();
    this._dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
    this._chiefEdit = new ChiefEdit();
    this._srcDataPage = new TabPage();
    this._resultsPage = new TabPage();
    this._resultSettingsPanel = new Panel();
    this._verifySchemeEdit = new ButtonEdit();
    this._verifySchemeCheckBox = new CheckBox();
    this._checkBoxUseActualScheme = new CheckBox();
    this._copyResultsToSrcDataCheckBox = new CheckBox();
    this._miscPage = new TabPage();
    this._checkBoxSetColor = new CheckBox();
    this._colorCombo = new ComboBoxColorPicker();
    this._constraintDatePicker = new DateTimePicker();
    this._label3 = new Label();
    this._constraintComboBox = new System.Windows.Forms.ComboBox();
    this._label2 = new Label();
    this._milestoneCheckBox = new CheckBox();
    this._textBoxCost = new TextBox();
    this._labelCost = new Label();
    this._wbsCodeTextBox = new TextBox();
    this._labelWbsCode = new Label();
    this._notesPage = new TabPage();
    this._generalPage.SuspendLayout();
    this._panel3.SuspendLayout();
    this._percentCompletedUpDown.BeginInit();
    this._factPanel.SuspendLayout();
    this._panel1.SuspendLayout();
    this._priorityUpDown.BeginInit();
    this._durationTextBox.Properties.BeginInit();
    this._pageControl.SuspendLayout();
    this._dependenciesPage.SuspendLayout();
    ((ISupportInitialize) this._dependenciesView).BeginInit();
    this._resourcesPage.SuspendLayout();
    ((ISupportInitialize) this._resultsView).BeginInit();
    this._resultsPage.SuspendLayout();
    this._resultSettingsPanel.SuspendLayout();
    this._verifySchemeEdit.Properties.BeginInit();
    this._miscPage.SuspendLayout();
    this._notesPage.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._labelNotes, "_labelNotes");
    this._labelNotes.Name = "_labelNotes";
    this._notesTextBox.AcceptsReturn = true;
    componentResourceManager.ApplyResources((object) this._notesTextBox, "_notesTextBox");
    this._notesTextBox.Name = "_notesTextBox";
    componentResourceManager.ApplyResources((object) this._labelStatus, "_labelStatus");
    this._labelStatus.Name = "_labelStatus";
    componentResourceManager.ApplyResources((object) this._workTextBox, "_workTextBox");
    this._workTextBox.Name = "_workTextBox";
    componentResourceManager.ApplyResources((object) this._labelWork, "_labelWork");
    this._labelWork.Name = "_labelWork";
    this._generalPage.Controls.Add((Control) this._panel3);
    this._generalPage.Controls.Add((Control) this._labelWork);
    this._generalPage.Controls.Add((Control) this._workTextBox);
    this._generalPage.Controls.Add((Control) this._factPanel);
    this._generalPage.Controls.Add((Control) this._panel1);
    componentResourceManager.ApplyResources((object) this._generalPage, "_generalPage");
    this._generalPage.Name = "_generalPage";
    this._generalPage.UseVisualStyleBackColor = true;
    this._panel3.Controls.Add((Control) this._labelPercentCompleted);
    this._panel3.Controls.Add((Control) this._label1);
    this._panel3.Controls.Add((Control) this._labelStatus);
    this._panel3.Controls.Add((Control) this._percentCompletedUpDown);
    this._panel3.Controls.Add((Control) this._statusLabel);
    componentResourceManager.ApplyResources((object) this._panel3, "_panel3");
    this._panel3.Name = "_panel3";
    componentResourceManager.ApplyResources((object) this._labelPercentCompleted, "_labelPercentCompleted");
    this._labelPercentCompleted.Name = "_labelPercentCompleted";
    componentResourceManager.ApplyResources((object) this._label1, "_label1");
    this._label1.BackColor = SystemColors.Control;
    this._label1.BorderStyle = BorderStyle.Fixed3D;
    this._label1.Name = "_label1";
    componentResourceManager.ApplyResources((object) this._percentCompletedUpDown, "_percentCompletedUpDown");
    this._percentCompletedUpDown.Increment = new Decimal(new int[4]
    {
      25,
      0,
      0,
      0
    });
    this._percentCompletedUpDown.Name = "_percentCompletedUpDown";
    this._percentCompletedUpDown.Value = new Decimal(new int[4]
    {
      100,
      0,
      0,
      0
    });
    componentResourceManager.ApplyResources((object) this._statusLabel, "_statusLabel");
    this._statusLabel.Name = "_statusLabel";
    this._factPanel.Controls.Add((Control) this._factFinishPicker);
    this._factPanel.Controls.Add((Control) this._factStartPicker);
    this._factPanel.Controls.Add((Control) this._factFinishLabel);
    this._factPanel.Controls.Add((Control) this._factStartLabel);
    componentResourceManager.ApplyResources((object) this._factPanel, "_factPanel");
    this._factPanel.Name = "_factPanel";
    componentResourceManager.ApplyResources((object) this._factFinishPicker, "_factFinishPicker");
    this._factFinishPicker.Format = DateTimePickerFormat.Custom;
    this._factFinishPicker.Name = "_factFinishPicker";
    componentResourceManager.ApplyResources((object) this._factStartPicker, "_factStartPicker");
    this._factStartPicker.Format = DateTimePickerFormat.Custom;
    this._factStartPicker.Name = "_factStartPicker";
    componentResourceManager.ApplyResources((object) this._factFinishLabel, "_factFinishLabel");
    this._factFinishLabel.Name = "_factFinishLabel";
    componentResourceManager.ApplyResources((object) this._factStartLabel, "_factStartLabel");
    this._factStartLabel.Name = "_factStartLabel";
    this._panel1.Controls.Add((Control) this._label4);
    this._panel1.Controls.Add((Control) this._priorityUpDown);
    this._panel1.Controls.Add((Control) this._finishPicker);
    this._panel1.Controls.Add((Control) this._startPicker);
    this._panel1.Controls.Add((Control) this._labelFinish);
    this._panel1.Controls.Add((Control) this._labelStart);
    this._panel1.Controls.Add((Control) this._labelPriority);
    this._panel1.Controls.Add((Control) this._durationTextBox);
    this._panel1.Controls.Add((Control) this._labelDuration);
    this._panel1.Controls.Add((Control) this._nameTextBox);
    this._panel1.Controls.Add((Control) this._labelTaskName);
    componentResourceManager.ApplyResources((object) this._panel1, "_panel1");
    this._panel1.Name = "_panel1";
    componentResourceManager.ApplyResources((object) this._label4, "_label4");
    this._label4.BackColor = SystemColors.Control;
    this._label4.BorderStyle = BorderStyle.Fixed3D;
    this._label4.Name = "_label4";
    componentResourceManager.ApplyResources((object) this._priorityUpDown, "_priorityUpDown");
    this._priorityUpDown.Increment = new Decimal(new int[4]
    {
      100,
      0,
      0,
      0
    });
    this._priorityUpDown.Maximum = new Decimal(new int[4]
    {
      1000,
      0,
      0,
      0
    });
    this._priorityUpDown.Name = "_priorityUpDown";
    this._priorityUpDown.Value = new Decimal(new int[4]
    {
      500,
      0,
      0,
      0
    });
    componentResourceManager.ApplyResources((object) this._finishPicker, "_finishPicker");
    this._finishPicker.Format = DateTimePickerFormat.Custom;
    this._finishPicker.Name = "_finishPicker";
    this._startPicker.Format = DateTimePickerFormat.Custom;
    componentResourceManager.ApplyResources((object) this._startPicker, "_startPicker");
    this._startPicker.Name = "_startPicker";
    componentResourceManager.ApplyResources((object) this._labelFinish, "_labelFinish");
    this._labelFinish.Name = "_labelFinish";
    componentResourceManager.ApplyResources((object) this._labelStart, "_labelStart");
    this._labelStart.Name = "_labelStart";
    componentResourceManager.ApplyResources((object) this._labelPriority, "_labelPriority");
    this._labelPriority.Name = "_labelPriority";
    componentResourceManager.ApplyResources((object) this._durationTextBox, "_durationTextBox");
    this._durationTextBox.Name = "_durationTextBox";
    this._durationTextBox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo, "", -1, true, true, false, HorzAlignment.Default, (Image) null, new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 7.2f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.None, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText))
    });
    this._durationTextBox.ButtonClick += new ButtonPressedEventHandler(this.DurationTextBox_ButtonClick);
    this._durationTextBox.DoubleClick += new EventHandler(this.DurationTextBox_DoubleClick);
    componentResourceManager.ApplyResources((object) this._labelDuration, "_labelDuration");
    this._labelDuration.Name = "_labelDuration";
    componentResourceManager.ApplyResources((object) this._nameTextBox, "_nameTextBox");
    this._nameTextBox.Name = "_nameTextBox";
    componentResourceManager.ApplyResources((object) this._labelTaskName, "_labelTaskName");
    this._labelTaskName.Name = "_labelTaskName";
    this._pageControl.Controls.Add((Control) this._generalPage);
    this._pageControl.Controls.Add((Control) this._dependenciesPage);
    this._pageControl.Controls.Add((Control) this._resourcesPage);
    this._pageControl.Controls.Add((Control) this._srcDataPage);
    this._pageControl.Controls.Add((Control) this._resultsPage);
    this._pageControl.Controls.Add((Control) this._miscPage);
    this._pageControl.Controls.Add((Control) this._notesPage);
    componentResourceManager.ApplyResources((object) this._pageControl, "_pageControl");
    this._pageControl.Name = "_pageControl";
    this._pageControl.SelectedIndex = 0;
    this._pageControl.TabStop = false;
    this._pageControl.SelectedIndexChanged += new EventHandler(this.tabControlTask_SelectedIndexChanged);
    this._dependenciesPage.Controls.Add((Control) this._dependenciesView);
    componentResourceManager.ApplyResources((object) this._dependenciesPage, "_dependenciesPage");
    this._dependenciesPage.Name = "_dependenciesPage";
    this._dependenciesPage.UseVisualStyleBackColor = true;
    this._dependenciesView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
    this._dependenciesView.BackgroundColor = SystemColors.Window;
    this._dependenciesView.CellBorderStyle = DataGridViewCellBorderStyle.None;
    this._dependenciesView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._dependenciesView.Columns.AddRange((DataGridViewColumn) this._idColumn, (DataGridViewColumn) this._nameColumn, (DataGridViewColumn) this._depTypeColumn, (DataGridViewColumn) this._lagColumn);
    componentResourceManager.ApplyResources((object) this._dependenciesView, "_dependenciesView");
    this._dependenciesView.EnableHeadersVisualStyles = false;
    this._dependenciesView.Name = "_dependenciesView";
    this._dependenciesView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
    this._dependenciesView.CellFormatting += new DataGridViewCellFormattingEventHandler(this.DependenciesView_CellFormatting);
    this._dependenciesView.CellParsing += new DataGridViewCellParsingEventHandler(this.DependenciesView_CellParsing);
    this._dependenciesView.CellValidating += new DataGridViewCellValidatingEventHandler(this.DependenciesView_CellValidating);
    this._dependenciesView.DataError += new DataGridViewDataErrorEventHandler(this.DependenciesView_DataError);
    this._dependenciesView.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.DependenciesView_EditingControlShowing);
    this._dependenciesView.RowsAdded += new DataGridViewRowsAddedEventHandler(this.DependenciesView_RowsAdded);
    componentResourceManager.ApplyResources((object) this._idColumn, "_idColumn");
    this._idColumn.Name = "_idColumn";
    this._idColumn.ReadOnly = true;
    this._nameColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
    this._nameColumn.DisplayStyleForCurrentCellOnly = true;
    componentResourceManager.ApplyResources((object) this._nameColumn, "_nameColumn");
    this._nameColumn.Name = "_nameColumn";
    this._nameColumn.Resizable = DataGridViewTriState.True;
    this._nameColumn.SortMode = DataGridViewColumnSortMode.Automatic;
    this._depTypeColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
    this._depTypeColumn.DisplayStyleForCurrentCellOnly = true;
    componentResourceManager.ApplyResources((object) this._depTypeColumn, "_depTypeColumn");
    this._depTypeColumn.Name = "_depTypeColumn";
    this._lagColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this._lagColumn, "_lagColumn");
    this._lagColumn.Name = "_lagColumn";
    this._resourcesPage.Controls.Add((Control) this._resultsView);
    this._resourcesPage.Controls.Add((Control) this._chiefEdit);
    componentResourceManager.ApplyResources((object) this._resourcesPage, "_resourcesPage");
    this._resourcesPage.Name = "_resourcesPage";
    this._resourcesPage.UseVisualStyleBackColor = true;
    this._resultsView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
    this._resultsView.BackgroundColor = SystemColors.Window;
    this._resultsView.CellBorderStyle = DataGridViewCellBorderStyle.None;
    this._resultsView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._resultsView.Columns.AddRange((DataGridViewColumn) this._dataGridViewButtonTextBoxColumn1, (DataGridViewColumn) this._dataGridViewTextBoxColumn3);
    componentResourceManager.ApplyResources((object) this._resultsView, "_resultsView");
    this._resultsView.EnableHeadersVisualStyles = false;
    this._resultsView.Name = "_resultsView";
    this._resultsView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
    this._resultsView.EditorButtonClicked += new EventHandler(this.resourcesView_EditorButtonClicked);
    gridViewCellStyle.Padding = new Padding(0, 0, 17, 0);
    this._dataGridViewButtonTextBoxColumn1.DefaultCellStyle = gridViewCellStyle;
    componentResourceManager.ApplyResources((object) this._dataGridViewButtonTextBoxColumn1, "_dataGridViewButtonTextBoxColumn1");
    this._dataGridViewButtonTextBoxColumn1.Name = "_dataGridViewButtonTextBoxColumn1";
    this._dataGridViewButtonTextBoxColumn1.ReadOnly = true;
    this._dataGridViewButtonTextBoxColumn1.Resizable = DataGridViewTriState.True;
    this._dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this._dataGridViewTextBoxColumn3, "_dataGridViewTextBoxColumn3");
    this._dataGridViewTextBoxColumn3.Name = "_dataGridViewTextBoxColumn3";
    this._dataGridViewTextBoxColumn3.Resizable = DataGridViewTriState.True;
    this._chiefEdit.AllowDel = true;
    componentResourceManager.ApplyResources((object) this._chiefEdit, "_chiefEdit");
    this._chiefEdit.Inherited = false;
    this._chiefEdit.Name = "_chiefEdit";
    componentResourceManager.ApplyResources((object) this._srcDataPage, "_srcDataPage");
    this._srcDataPage.Name = "_srcDataPage";
    this._srcDataPage.UseVisualStyleBackColor = true;
    this._resultsPage.Controls.Add((Control) this._resultSettingsPanel);
    componentResourceManager.ApplyResources((object) this._resultsPage, "_resultsPage");
    this._resultsPage.Name = "_resultsPage";
    this._resultsPage.UseVisualStyleBackColor = true;
    this._resultSettingsPanel.Controls.Add((Control) this._verifySchemeEdit);
    this._resultSettingsPanel.Controls.Add((Control) this._verifySchemeCheckBox);
    this._resultSettingsPanel.Controls.Add((Control) this._checkBoxUseActualScheme);
    this._resultSettingsPanel.Controls.Add((Control) this._copyResultsToSrcDataCheckBox);
    componentResourceManager.ApplyResources((object) this._resultSettingsPanel, "_resultSettingsPanel");
    this._resultSettingsPanel.Name = "_resultSettingsPanel";
    componentResourceManager.ApplyResources((object) this._verifySchemeEdit, "_verifySchemeEdit");
    this._verifySchemeEdit.Name = "_verifySchemeEdit";
    this._verifySchemeEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._verifySchemeEdit.Properties.ReadOnly = true;
    this._verifySchemeEdit.ButtonClick += new ButtonPressedEventHandler(this.VerifySchemeEdit_ButtonClick);
    this._verifySchemeEdit.DoubleClick += new EventHandler(this.VerifySchemeEdit_DoubleClick);
    componentResourceManager.ApplyResources((object) this._verifySchemeCheckBox, "_verifySchemeCheckBox");
    this._verifySchemeCheckBox.Name = "_verifySchemeCheckBox";
    this._verifySchemeCheckBox.UseVisualStyleBackColor = true;
    this._verifySchemeCheckBox.CheckedChanged += new EventHandler(this.VerifySchemeCheckBox_CheckedChanged);
    this._checkBoxUseActualScheme.Checked = true;
    this._checkBoxUseActualScheme.CheckState = CheckState.Checked;
    componentResourceManager.ApplyResources((object) this._checkBoxUseActualScheme, "_checkBoxUseActualScheme");
    this._checkBoxUseActualScheme.Name = "_checkBoxUseActualScheme";
    this._checkBoxUseActualScheme.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._copyResultsToSrcDataCheckBox, "_copyResultsToSrcDataCheckBox");
    this._copyResultsToSrcDataCheckBox.Name = "_copyResultsToSrcDataCheckBox";
    this._copyResultsToSrcDataCheckBox.UseVisualStyleBackColor = true;
    this._miscPage.Controls.Add((Control) this._checkBoxSetColor);
    this._miscPage.Controls.Add((Control) this._colorCombo);
    this._miscPage.Controls.Add((Control) this._constraintDatePicker);
    this._miscPage.Controls.Add((Control) this._label3);
    this._miscPage.Controls.Add((Control) this._constraintComboBox);
    this._miscPage.Controls.Add((Control) this._label2);
    this._miscPage.Controls.Add((Control) this._milestoneCheckBox);
    this._miscPage.Controls.Add((Control) this._textBoxCost);
    this._miscPage.Controls.Add((Control) this._labelCost);
    this._miscPage.Controls.Add((Control) this._wbsCodeTextBox);
    this._miscPage.Controls.Add((Control) this._labelWbsCode);
    componentResourceManager.ApplyResources((object) this._miscPage, "_miscPage");
    this._miscPage.Name = "_miscPage";
    this._miscPage.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._checkBoxSetColor, "_checkBoxSetColor");
    this._checkBoxSetColor.Name = "_checkBoxSetColor";
    this._checkBoxSetColor.UseVisualStyleBackColor = true;
    this._checkBoxSetColor.CheckedChanged += new EventHandler(this.CheckBoxSetColor_CheckedChanged);
    this._colorCombo.Color = Color.RoyalBlue;
    this._colorCombo.DrawMode = DrawMode.OwnerDrawFixed;
    this._colorCombo.DropDownHeight = 1;
    this._colorCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this._colorCombo.DropDownWidth = 1;
    componentResourceManager.ApplyResources((object) this._colorCombo, "_colorCombo");
    this._colorCombo.FormattingEnabled = true;
    this._colorCombo.Hatch = new HatchStyle?(HatchStyle.Percent50);
    this._colorCombo.Items.AddRange(new object[1]
    {
      (object) componentResourceManager.GetString("_colorCombo.Items")
    });
    this._colorCombo.Name = "_colorCombo";
    componentResourceManager.ApplyResources((object) this._constraintDatePicker, "_constraintDatePicker");
    this._constraintDatePicker.Format = DateTimePickerFormat.Custom;
    this._constraintDatePicker.Name = "_constraintDatePicker";
    componentResourceManager.ApplyResources((object) this._label3, "_label3");
    this._label3.Name = "_label3";
    this._constraintComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._constraintComboBox.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this._constraintComboBox, "_constraintComboBox");
    this._constraintComboBox.Name = "_constraintComboBox";
    this._constraintComboBox.SelectedIndexChanged += new EventHandler(this.ConstraintComboBox_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._label2, "_label2");
    this._label2.Name = "_label2";
    componentResourceManager.ApplyResources((object) this._milestoneCheckBox, "_milestoneCheckBox");
    this._milestoneCheckBox.Name = "_milestoneCheckBox";
    this._milestoneCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._textBoxCost, "_textBoxCost");
    this._textBoxCost.Name = "_textBoxCost";
    this._textBoxCost.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this._labelCost, "_labelCost");
    this._labelCost.Name = "_labelCost";
    componentResourceManager.ApplyResources((object) this._wbsCodeTextBox, "_wbsCodeTextBox");
    this._wbsCodeTextBox.Name = "_wbsCodeTextBox";
    componentResourceManager.ApplyResources((object) this._labelWbsCode, "_labelWbsCode");
    this._labelWbsCode.Name = "_labelWbsCode";
    this._notesPage.Controls.Add((Control) this._notesTextBox);
    this._notesPage.Controls.Add((Control) this._labelNotes);
    componentResourceManager.ApplyResources((object) this._notesPage, "_notesPage");
    this._notesPage.Name = "_notesPage";
    this._notesPage.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._pageControl);
    this.Name = nameof (TaskView);
    this.Tag = (object) "";
    this._generalPage.ResumeLayout(false);
    this._generalPage.PerformLayout();
    this._panel3.ResumeLayout(false);
    this._panel3.PerformLayout();
    this._percentCompletedUpDown.EndInit();
    this._factPanel.ResumeLayout(false);
    this._factPanel.PerformLayout();
    this._panel1.ResumeLayout(false);
    this._panel1.PerformLayout();
    this._priorityUpDown.EndInit();
    this._durationTextBox.Properties.EndInit();
    this._pageControl.ResumeLayout(false);
    this._dependenciesPage.ResumeLayout(false);
    ((ISupportInitialize) this._dependenciesView).EndInit();
    this._resourcesPage.ResumeLayout(false);
    ((ISupportInitialize) this._resultsView).EndInit();
    this._resultsPage.ResumeLayout(false);
    this._resultSettingsPanel.ResumeLayout(false);
    this._resultSettingsPanel.PerformLayout();
    this._verifySchemeEdit.Properties.EndInit();
    this._miscPage.ResumeLayout(false);
    this._miscPage.PerformLayout();
    this._notesPage.ResumeLayout(false);
    this._notesPage.PerformLayout();
    this.ResumeLayout(false);
  }

  /// <summary>Закладка</summary>
  public enum Page
  {
    /// <summary>Неопределённая закладка</summary>
    [Intermech.Project.CustomDescription("TaskViewPageNone")] None,
    /// <summary>Общие</summary>
    [Intermech.Project.CustomDescription("TaskViewPageCommon")] Common,
    /// <summary>Предшественники</summary>
    [Intermech.Project.CustomDescription("TaskViewPagePrecursors")] Precursors,
    /// <summary>Ресурсы</summary>
    [Intermech.Project.CustomDescription("TaskViewPageResources")] Resources,
    /// <summary>Исходные данные</summary>
    [Intermech.Project.CustomDescription("TaskViewPageInitialData")] InitialData,
    /// <summary>Результаты</summary>
    [Intermech.Project.CustomDescription("TaskViewPageResults")] Results,
    /// <summary>Дополнительно</summary>
    [Intermech.Project.CustomDescription("TaskViewPageExtra")] Extra,
    /// <summary>Заметки</summary>
    [Intermech.Project.CustomDescription("TaskViewPageNotes")] Notes,
  }

  private class TaskInfo
  {
    [NotNull]
    private readonly string _name;

    [NotNull]
    public string ID { get; }

    public TaskInfo([NotNull] string id, [NotNull] string name)
    {
      this.ID = id;
      this._name = name;
    }

    public override string ToString() => this._name;

    public override bool Equals(object obj)
    {
      return obj is TaskView.TaskInfo taskInfo ? taskInfo.ID == this.ID && taskInfo._name == this._name : obj != null && obj.Equals((object) this);
    }

    public override int GetHashCode() => (this.ID + this._name).GetHashCode();
  }

  private class ResourceInfo
  {
    public long OldID { get; }

    public long NewID { get; set; }

    public int ObjectType { get; set; }

    public ResourceInfo(long oldID, long newID, int objectType)
    {
      this.OldID = oldID;
      this.NewID = newID;
      this.ObjectType = objectType;
    }

    public ResourceInfo(long id, int objectType)
      : this(id, id, objectType)
    {
    }
  }
}
