// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.EditVarForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for EditVarForm.</summary>
public class EditVarForm : Form
{
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  private ComboBoxEx VarsCombo;
  private Label label1;
  private Label label2;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private ComboBoxEx TypesBox;
  public string VarName = "";
  public VarType VarType;
  public int VarTypeID;
  public string DefValue = "";
  public object[] AddInfo;
  private TextBox ValueBox;
  private Button ValListButton;
  private Variable _variable;
  private Label CurValueLabel;
  private Label label3;
  private ParticipantList _participants;
  private System.Windows.Forms.ComboBox ValuesCombo;
  private System.Windows.Forms.ComboBox BoolCombo;
  private NumericUpDown UpDown;
  private ButtonEdit ArcIDEdit;
  private NumericUpDown UpDownFloat;
  private DateTimeWithNull DatePicker;
  private CheckBox isGlobalVariableCheckBox;
  private bool _readonly;
  private long _processID;
  private VarKind _varKind;
  private bool _isGlobalVariable;
  private StringList _valuesList;
  private string _textValue = string.Empty;
  private VarCustomEditors Editors = new VarCustomEditors();

  public EditVarForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1307);
    this.BoolCombo.Items.Add((object) LocalizationHolder.GetString("BoolFalse"));
    this.BoolCombo.Items.Add((object) LocalizationHolder.GetString("BoolTrue"));
    this.BoolCombo.SelectedIndex = 0;
    this.InitializeCustomEditors();
  }

  public bool ReadOnly
  {
    get => this._readonly;
    set
    {
      this._readonly = value;
      this.VarsCombo.Enabled = !value;
      this.TypesBox.Enabled = !value;
      this.Editors.Enabled = !value;
    }
  }

  public VarKind VarKind
  {
    get => this._varKind;
    set => this._varKind = value;
  }

  /// <summary>Указывает является ли переменная глобальной</summary>
  public bool IsGlobalVariable => this._isGlobalVariable;

  public EditVarForm(VariablesForm owner)
    : this()
  {
    this._processID = owner.ObjectID;
    this.TypesBox.ImageList = BaseHolder.IconService.ImageList;
    this.VarsCombo.ImageList = this.TypesBox.ImageList;
    this.FillVarList();
    this.TypesBox_SelectionChangeCommitted((object) null, (EventArgs) null);
  }

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditVarForm));
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.label1 = new Label();
    this.label2 = new Label();
    this.ValueBox = new TextBox();
    this.CurValueLabel = new Label();
    this.ValListButton = new Button();
    this.label3 = new Label();
    this.ValuesCombo = new System.Windows.Forms.ComboBox();
    this.BoolCombo = new System.Windows.Forms.ComboBox();
    this.UpDown = new NumericUpDown();
    this.ArcIDEdit = new ButtonEdit();
    this.TypesBox = new ComboBoxEx();
    this.VarsCombo = new ComboBoxEx();
    this.UpDownFloat = new NumericUpDown();
    this.DatePicker = new DateTimeWithNull();
    this.isGlobalVariableCheckBox = new CheckBox();
    this.Panel2.SuspendLayout();
    this.UpDown.BeginInit();
    this.ArcIDEdit.Properties.BeginInit();
    this.UpDownFloat.BeginInit();
    this.SuspendLayout();
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
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.ValueBox, "ValueBox");
    this.ValueBox.Name = "ValueBox";
    componentResourceManager.ApplyResources((object) this.CurValueLabel, "CurValueLabel");
    this.CurValueLabel.Name = "CurValueLabel";
    componentResourceManager.ApplyResources((object) this.ValListButton, "ValListButton");
    this.ValListButton.Name = "ValListButton";
    this.ValListButton.Click += new EventHandler(this.ValListButton_Click);
    this.label3.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    this.ValuesCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.ValuesCombo.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.ValuesCombo, "ValuesCombo");
    this.ValuesCombo.Name = "ValuesCombo";
    this.ValuesCombo.DropDown += new EventHandler(this.ValuesCombo_DropDown);
    this.BoolCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.BoolCombo.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.BoolCombo, "BoolCombo");
    this.BoolCombo.Name = "BoolCombo";
    componentResourceManager.ApplyResources((object) this.UpDown, "UpDown");
    this.UpDown.Maximum = new Decimal(new int[4]
    {
      999999999,
      0,
      0,
      0
    });
    this.UpDown.Minimum = new Decimal(new int[4]
    {
      999999999,
      0,
      0,
      int.MinValue
    });
    this.UpDown.Name = "UpDown";
    componentResourceManager.ApplyResources((object) this.ArcIDEdit, "ArcIDEdit");
    this.ArcIDEdit.Name = "ArcIDEdit";
    this.ArcIDEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "", 15, true, true, false, HorzAlignment.Center, (Image) null)
    });
    this.ArcIDEdit.Properties.ReadOnly = true;
    this.ArcIDEdit.ButtonClick += new ButtonPressedEventHandler(this.DocArcIDEdit_ButtonClick);
    componentResourceManager.ApplyResources((object) this.TypesBox, "TypesBox");
    this.TypesBox.DrawMode = DrawMode.OwnerDrawFixed;
    this.TypesBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.TypesBox.ImageList = (ImageList) null;
    this.TypesBox.Name = "TypesBox";
    this.TypesBox.SelectionChangeCommitted += new EventHandler(this.TypesBox_SelectionChangeCommitted);
    componentResourceManager.ApplyResources((object) this.VarsCombo, "VarsCombo");
    this.VarsCombo.AutoCompleteMode = AutoCompleteMode.Suggest;
    this.VarsCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
    this.VarsCombo.DrawMode = DrawMode.OwnerDrawFixed;
    this.VarsCombo.ImageList = (ImageList) null;
    this.VarsCombo.Name = "VarsCombo";
    this.VarsCombo.Sorted = true;
    this.VarsCombo.SelectionChangeCommitted += new EventHandler(this.VarsCombo_SelectionChangeCommitted);
    this.VarsCombo.SelectedValueChanged += new EventHandler(this.VarsCombo_SelectedValueChanged);
    this.VarsCombo.TextChanged += new EventHandler(this.VarsCombo_TextChanged);
    this.UpDownFloat.DecimalPlaces = 3;
    componentResourceManager.ApplyResources((object) this.UpDownFloat, "UpDownFloat");
    this.UpDownFloat.Maximum = new Decimal(new int[4]
    {
      999999999,
      0,
      0,
      0
    });
    this.UpDownFloat.Minimum = new Decimal(new int[4]
    {
      999999999,
      0,
      0,
      int.MinValue
    });
    this.UpDownFloat.Name = "UpDownFloat";
    this.DatePicker.DateTime = "13.02.2019 11:25:56";
    componentResourceManager.ApplyResources((object) this.DatePicker, "DatePicker");
    this.DatePicker.Name = "DatePicker";
    componentResourceManager.ApplyResources((object) this.isGlobalVariableCheckBox, "isGlobalVariableCheckBox");
    this.isGlobalVariableCheckBox.Name = "isGlobalVariableCheckBox";
    this.isGlobalVariableCheckBox.UseVisualStyleBackColor = true;
    this.isGlobalVariableCheckBox.CheckedChanged += new EventHandler(this.isGlobalVariableCheckBox_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.isGlobalVariableCheckBox);
    this.Controls.Add((Control) this.DatePicker);
    this.Controls.Add((Control) this.UpDownFloat);
    this.Controls.Add((Control) this.ArcIDEdit);
    this.Controls.Add((Control) this.UpDown);
    this.Controls.Add((Control) this.BoolCombo);
    this.Controls.Add((Control) this.ValuesCombo);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.ValListButton);
    this.Controls.Add((Control) this.CurValueLabel);
    this.Controls.Add((Control) this.ValueBox);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.TypesBox);
    this.Controls.Add((Control) this.VarsCombo);
    this.Controls.Add((Control) this.Panel2);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditVarForm);
    this.ShowInTaskbar = false;
    this.Activated += new EventHandler(this.EditVarForm_Activated);
    this.Closing += new CancelEventHandler(this.EditVarForm_Closing);
    this.KeyDown += new KeyEventHandler(this.EditVarForm_KeyDown);
    this.Panel2.ResumeLayout(false);
    this.UpDown.EndInit();
    this.ArcIDEdit.Properties.EndInit();
    this.UpDownFloat.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  protected void FillVarList()
  {
    foreach (VarType vt in Enum.GetValues(typeof (VarType)))
    {
      if (vt != VarType.Unknown)
        this.TypesBox.Items.Add((object) new ComboBoxExItem(MiscFunx.VarTypeToString(vt), Holder.VarTypeImageIndex[vt]));
    }
    this.TypesBox.SelectedIndex = 0;
    if (!(ApplicationServices.Container.GetService(typeof (IClientMetadataCache)) is IClientMetadataCache service))
      return;
    IDBAttributeTypeInfoCollection attributeTypeCollection1 = service.GetAttributeTypeCollection(wfConsts.WorkflowVarsGroupID, false);
    IDBAttributeTypeInfoCollection attributeTypeCollection2 = service.GetAttributeTypeCollection(wfConsts.GlobalVariablesGroupID, false);
    DataTable tbl1 = attributeTypeCollection1.Select("", (object[]) null);
    DataTable tbl2 = attributeTypeCollection2.Select("", (object[]) null);
    this.FillVarsCombo(tbl1, false);
    this.FillVarsCombo(tbl2, true);
  }

  private void FillVarsCombo(DataTable tbl, bool isGlobalVar)
  {
    foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
    {
      VarType varType = MiscFunx.DetermineVarType(row);
      if (varType != VarType.Unknown)
        this.VarsCombo.Items.Add((object) new VarComboItem(row["F_NAME"].ToString(), Convert.ToInt64(row["F_ATTRIBUTE_ID"]), Holder.VarTypeImageIndex[varType], isGlobalVar)
        {
          Type = varType
        });
    }
  }

  private void VarsCombo_SelectionChangeCommitted(object sender, EventArgs e)
  {
    VarComboItem selectedItem = this.VarsCombo.SelectedItem as VarComboItem;
    this.TypesBox.Enabled = selectedItem == null;
    if (selectedItem != null)
    {
      this.SelectedVarType = selectedItem.Type;
      this.isGlobalVariableCheckBox.Checked = selectedItem.VarKind == VarKind.Global;
      if (1.Equals(sender) || selectedItem.Type != VarType.StringList)
        return;
      this.ValuesCombo.Items.Clear();
      this._valuesList = new StringList();
      VarList.FillPossibleValues(MetaDataHelper.GetAttributeType((int) selectedItem.ID), this._valuesList);
    }
    else
    {
      if (this.SelectedVarType != VarType.StringList)
        return;
      this._valuesList = (StringList) null;
    }
  }

  private int VarsComboExistedVarIndex
  {
    get => this.VarsCombo.Items.IndexOf((object) this.VarsCombo.Text);
  }

  private void VarsCombo_TextChanged(object sender, EventArgs e)
  {
    if (this._readonly || this._variable != null && this._variable.Kind == VarKind.System && this.TypesBox.Enabled)
    {
      this.TypesBox.Enabled = false;
    }
    else
    {
      this.RemoveEmptyVars();
      int selectionStart = this.VarsCombo.SelectionStart;
      this.VarsCombo.SelectedItem = (object) this.VarsCombo.Text;
      if (this.VarsCombo.SelectedItem == null)
      {
        this.VarsCombo.Items.Insert(0, (object) new EmptyVarComboItem(this.VarsCombo.Text));
        this.VarsCombo.SelectedItem = (object) this.VarsCombo.Text;
      }
      this.VarsCombo.SelectionStart = selectionStart;
      this.VarsCombo_SelectionChangeCommitted((object) null, (EventArgs) null);
    }
  }

  private void RemoveEmptyVars()
  {
    List<(object, int)> list = this.VarsCombo.Items.Cast<object>().Select<object, (object, int)>((Func<object, int, (object, int)>) ((item, index) => (item, index))).Where<(object, int)>((System.Func<(object, int), bool>) (x => x.item is EmptyVarComboItem)).ToList<(object, int)>();
    if (!list.Any<(object, int)>())
      return;
    for (int index = list.Count - 1; index >= 0; --index)
      this.VarsCombo.Items.RemoveAt(list[index].Item2);
  }

  private void VarsCombo_SelectedValueChanged(object sender, EventArgs e)
  {
  }

  private VarType SelectedVarType
  {
    get => (VarType) this.TypesBox.SelectedIndex;
    set
    {
      if (value == VarType.Unknown)
        return;
      this.TypesBox.SelectedIndex = (int) value;
      this.TypesBox_SelectionChangeCommitted((object) null, (EventArgs) null);
    }
  }

  private void EditVarForm_Closing(object sender, CancelEventArgs e)
  {
    if (this.DialogResult != DialogResult.OK || this.ReadOnly)
      return;
    this.VarName = this.VarsCombo.Text;
    if (this.VarName.Trim() == "")
    {
      wfFunx.SayError(LocalizationHolder.rm.GetString("Workflow.Design_39"));
      e.Cancel = true;
    }
    int comboExistedVarIndex = this.VarsComboExistedVarIndex;
    if (comboExistedVarIndex != -sc_21802.ssp_workflow_21803(105689735))
    {
      VarComboItem varComboItem = this.VarsCombo.Items[comboExistedVarIndex] as VarComboItem;
      this.VarType = varComboItem.Type;
      this.VarTypeID = Convert.ToInt32(varComboItem.ID);
    }
    else
    {
      this.VarTypeID = 0;
      this.VarType = this.SelectedVarType;
    }
    switch (this.VarType)
    {
      case VarType.StringList:
        if (this.ValuesList.Count == 0)
        {
          wfFunx.SayError("Список значений строк не может быть пустым!");
          e.Cancel = true;
          break;
        }
        int num = 0;
        foreach (string values in (List<string>) this.ValuesList)
        {
          if (string.IsNullOrEmpty(values) || string.IsNullOrWhiteSpace(values))
            ++num;
        }
        if (num == this.ValuesList.Count)
        {
          wfFunx.SayError("Список значений строк не может быть составлен исключительно из пустых значений!");
          e.Cancel = true;
          break;
        }
        StringList stringList = new StringList(this.ValuesList);
        if (stringList.Count > 0)
          stringList.Insert(0, this.Editors.Value);
        this.AddInfo = new object[1]
        {
          (object) stringList.Text
        };
        break;
      case VarType.ParticipantList:
        this.AddInfo = new object[1]
        {
          (object) this.AsParticipants.AsString
        };
        break;
      case VarType.Text:
        this.AddInfo = new object[1]
        {
          (object) this._textValue
        };
        break;
      default:
        this.AddInfo = new object[1]
        {
          (object) this.Editors.Value
        };
        break;
    }
    if (this._variable == null)
      return;
    if (this._variable.New)
    {
      this._variable.Name = this.VarName;
      this._variable.VarType = this.VarType;
    }
    this._variable.AddInfo = this.AddInfo;
  }

  private bool ValueEditorVisible
  {
    get => this.CurValueLabel.Visible;
    set
    {
      this.CurValueLabel.Visible = value;
      this.Editors.Visible = value;
    }
  }

  private void TypesBox_SelectionChangeCommitted(object sender, EventArgs e)
  {
    int selectedIndex = this.TypesBox.SelectedIndex;
    this._valuesList = (StringList) null;
    this.ValListButton.Enabled = selectedIndex == 5 || selectedIndex == 4 || selectedIndex == 8;
    this.ValueEditorVisible = selectedIndex != 5;
    this.Editors.VarType = (VarType) selectedIndex;
  }

  private ParticipantList AsParticipants
  {
    get => this._participants ?? (this._participants = new ParticipantList());
  }

  private StringList ValuesList
  {
    get
    {
      if (this._valuesList == null)
        this._valuesList = new StringList();
      return this._valuesList;
    }
  }

  private void ValListButton_Click(object sender, EventArgs e)
  {
    switch (this.SelectedVarType)
    {
      case VarType.StringList:
        int VarAttrTypeID = sc_21802.ssp_workflow_21804(312115757);
        string str1;
        if (this._variable != null)
        {
          str1 = this._variable.Name;
          VarAttrTypeID = this._variable.AttrTypeID;
        }
        else
        {
          str1 = this.VarsCombo.Text;
          int comboExistedVarIndex = this.VarsComboExistedVarIndex;
          if (comboExistedVarIndex != -1)
            VarAttrTypeID = Convert.ToInt32((this.VarsCombo.Items[comboExistedVarIndex] as VarComboItem).ID);
        }
        if (!this._readonly && VarAttrTypeID != 0)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            string[] applicabilityStrings = MiscFunx.GetVariableApplicabilityStrings(sessionKeeper.Session, VarAttrTypeID, this._processID);
            using (CurrentVariableUsedAnotherSchemes usedAnotherSchemes = new CurrentVariableUsedAnotherSchemes($"Помимо текущего шаблона, переменная \"{str1}\" возможно используется в:", applicabilityStrings))
            {
              if (applicabilityStrings.Length != 0)
              {
                if (usedAnotherSchemes.ShowDialog() == DialogResult.No)
                  break;
              }
            }
          }
        }
        using (StringsEditorForm stringsEditorForm = new StringsEditorForm())
        {
          StringList stringList = new StringList(this.ValuesList);
          stringsEditorForm.ValuesBox.Text = stringList.Text;
          if (stringsEditorForm.ShowDialog() != DialogResult.OK)
            break;
          stringList.Text = stringsEditorForm.ValuesBox.Text;
          string str2 = this.Editors.Value;
          this._valuesList = stringList;
          this.FillValuesCombo();
          this.Editors.Value = str2;
          if (this.ValuesCombo.SelectedIndex != -1 || this.ValuesCombo.Items.Count <= 0)
            break;
          this.ValuesCombo.SelectedIndex = 0;
          break;
        }
      case VarType.ParticipantList:
        using (DefineUsersForm defineUsersForm = new DefineUsersForm(this._processID))
        {
          defineUsersForm.ReadOnly = this.ReadOnly;
          defineUsersForm.Participants = this.AsParticipants;
          if (defineUsersForm.ShowDialog() != DialogResult.OK)
            break;
          this._participants.Assign(defineUsersForm.Participants);
          break;
        }
      case VarType.Text:
        string empty = string.Empty;
        if (this._variable != null)
          empty = this._variable.Value;
        using (TextVariableEditors textVariableEditors = new TextVariableEditors(empty))
        {
          if (textVariableEditors.ShowDialog() != DialogResult.OK)
            break;
          this._textValue = textVariableEditors.textValue.Text;
          break;
        }
    }
  }

  private void EditVarForm_Activated(object sender, EventArgs e)
  {
    this.ValueEditorVisible = this.ValueEditorVisible;
  }

  public Variable Variable
  {
    set
    {
      this._variable = value;
      if (value.AttrTypeID != 0)
      {
        foreach (VarComboItem varComboItem in this.VarsCombo.Items)
        {
          if (varComboItem.ID == (long) value.AttrTypeID)
          {
            this.VarsCombo.SelectedItem = (object) varComboItem;
            this.VarsCombo_SelectionChangeCommitted((object) 1, (EventArgs) null);
            break;
          }
        }
      }
      if (this.VarsCombo.Text == "")
        this.VarsCombo.Text = this._variable.Name;
      this.VarsCombo.Enabled = !this._readonly && value.New;
      if (this._variable.New || !this.VarsCombo.Enabled)
        this.VarsCombo.DropDownStyle = ComboBoxStyle.Simple;
      this.SelectedVarType = value.VarType;
      if (value.VarType == VarType.ParticipantList)
        this._participants = value.AsParticipants;
      else if (value.VarType == VarType.StringList)
      {
        this._valuesList = value.ValuesList;
        this.FillValuesCombo();
      }
      this.Editors.Value = value.Value;
      this.isGlobalVariableCheckBox.Checked = value.Kind == VarKind.Global;
      this.isGlobalVariableCheckBox.Visible = false;
    }
  }

  private void FillValuesCombo()
  {
    object selectedItem = this.ValuesCombo.SelectedItem;
    this.ValuesCombo.Items.Clear();
    this.ValuesCombo.Items.AddRange((object[]) this._valuesList.ToArray());
    this.ValuesCombo.SelectedItem = selectedItem;
  }

  private void InitializeCustomEditors()
  {
    this.ValuesCombo.Bounds = this.ValueBox.Bounds;
    this.BoolCombo.Bounds = this.ValueBox.Bounds;
    this.UpDown.Bounds = this.ValueBox.Bounds;
    this.UpDownFloat.Bounds = this.ValueBox.Bounds;
    this.DatePicker.Bounds = this.ValueBox.Bounds;
    this.ArcIDEdit.Bounds = this.ValueBox.Bounds;
    this.Editors.Add(new VarCustomEditor(VarType.StringList, (Control) this.ValuesCombo));
    this.Editors.Add(new VarCustomEditor(VarType.Boolean, (Control) this.BoolCombo));
    this.Editors.Add(new VarCustomEditor(VarType.ParticipantList, (Control) null));
    this.Editors.Add(new VarCustomEditor(VarType.Integer, (Control) this.UpDown));
    this.Editors.Add(new VarCustomEditor(VarType.Float, (Control) this.UpDownFloat));
    this.Editors.Add(new VarCustomEditor(VarType.DateTime, (Control) this.DatePicker));
    this.Editors.Add(new VarCustomEditor(VarType.Archive, (Control) this.ArcIDEdit));
    this.Editors.Add(new VarCustomEditor(VarType.Text, (Control) null));
    this.Editors.Add(new VarCustomEditor(VarType.Unknown, (Control) this.ValueBox));
  }

  private void DocArcIDEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Workflow.Design_42"), "", wfConsts.ArchivesTypeID, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    this.ArcIDEdit.Tag = (object) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ObjectSystemPropertiesEx systemPropertiesEx = sessionKeeper.Session.GetObjectSystemPropertiesEx(numArray[0], false);
      if (systemPropertiesEx != null)
      {
        this.ArcIDEdit.Text = systemPropertiesEx.Caption;
        this.ArcIDEdit.Tag = (object) systemPropertiesEx.VersionGuid;
      }
      else
        this.ArcIDEdit.Text = "???";
    }
  }

  private void EditVarForm_KeyDown(object sender, KeyEventArgs e)
  {
    if (this._readonly || !this.ArcIDEdit.EditorContainsFocus || e.KeyCode != Keys.Delete)
      return;
    this.ArcIDEdit.Tag = (object) null;
    this.ArcIDEdit.Text = "???";
  }

  private void ValuesCombo_DropDown(object sender, EventArgs e)
  {
    if (this._valuesList == null)
      return;
    this.FillValuesCombo();
  }

  private void isGlobalVariableCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._isGlobalVariable = this.isGlobalVariableCheckBox.Checked;
  }
}
