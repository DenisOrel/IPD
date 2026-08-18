
// Type: Intermech.Search.GroupAttributesChanging.GroupAttributesChangingSettingsControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Intermech.Search.GroupAttributesChanging;

public sealed class GroupAttributesChangingSettingsControl : UserControl
{
  private int[] _attributeTypeIds = new int[0];
  private int[] _replacementAttributeTypeIds = new int[0];
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private GroupBox groupBox1;
  private TableLayoutPanel tableLayoutPanel4;
  private ComboBox _attributeComboBox;
  private GroupBox groupBox2;
  private TableLayoutPanel tableLayoutPanel3;
  private CheckBox _matchCaseCheckBox;
  private CheckBox _matchCirillicLatinSimilarityCheckBox;
  private SpecialCharacterInputControl _findWhatSpecialCharacterInputControl;
  private GroupBox groupBox3;
  private RadioButton _replacementTextRadioButton;
  private RadioButton _replacementAttributeValueRadioButton;
  private ComboBox _replacementAttributeComboBox;
  private SpecialCharacterInputControl _replacementSpecialCharacterInputControl;
  private TableLayoutPanel tableLayoutPanel2;
  private TableLayoutPanel tableLayoutPanel8;
  private ComboBox _replacementCharacterCaseTransformationComboBox;
  private Label _replacementCharacterCaseTransformationLabel;
  private TableLayoutPanel tableLayoutPanel7;
  private TableLayoutPanel tableLayoutPanel6;

  public GroupAttributesChangingSettingsControl()
  {
    this.InitializeComponent();
    this.InitializeFindWhatSpecialCharacterInputControl();
    this.InitializeReplacementSpecialCharacterInputControl();
    this.InitializeReplacementCharacterCaseTransformationComboBox();
    this.UpdateControl();
  }

  public event EventHandler SettingsChanged;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int[] AttributeTypeIds
  {
    get => this._attributeTypeIds;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (AttributeTypeHelper.IsAnyUnknownAttributeTypeID((IEnumerable<int>) value))
        throw new ArgumentException();
      if (this._attributeTypeIds == value)
        return;
      this._attributeTypeIds = ((IEnumerable<int>) value).Distinct<int>().ToArray<int>();
      this.FillAttributeComboBox(this._attributeComboBox, (IEnumerable<int>) this._attributeTypeIds);
      this.OnSettingsChanged();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int SelectedAttributeTypeID
  {
    get
    {
      return this._attributeComboBox.SelectedItem == null ? 0 : ((IMSAttributeType) this._attributeComboBox.SelectedItem).AttributeID;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Regex FindWhat { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool UseReplacementText => this._replacementTextRadioButton.Checked;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string Replacement => this._replacementSpecialCharacterInputControl.Text;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public CharacterCaseTransformation ReplacementCharacterCaseTransformation
  {
    get
    {
      return ((Tuple<CharacterCaseTransformation, string>) this._replacementCharacterCaseTransformationComboBox.SelectedItem).Item1;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool UseReplacementAttribute => this._replacementAttributeValueRadioButton.Checked;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int[] ReplacementAttributeTypeIds
  {
    get => this._replacementAttributeTypeIds;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (AttributeTypeHelper.IsAnyUnknownAttributeTypeID((IEnumerable<int>) value))
        throw new ArgumentException();
      if (this._replacementAttributeTypeIds == value)
        return;
      this._replacementAttributeTypeIds = ((IEnumerable<int>) value).Distinct<int>().ToArray<int>();
      this.FillAttributeComboBox(this._replacementAttributeComboBox, (IEnumerable<int>) this._replacementAttributeTypeIds);
      this.OnSettingsChanged();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int SelectedReplacementAttributeTypeID
  {
    get
    {
      return this._replacementAttributeComboBox.SelectedItem == null ? 0 : ((IMSAttributeType) this._replacementAttributeComboBox.SelectedItem).AttributeID;
    }
  }

  private void AttributeComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.OnSettingsChanged();
  }

  private void FindWhatSpecialCharacterInputControl_Changed(object sender, EventArgs e)
  {
    this.CreateFindWhatRegex();
    this.OnSettingsChanged();
  }

  private void MatchCaseCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.CreateFindWhatRegex();
    this.OnSettingsChanged();
  }

  private void MatchCirillicLatinSimilarityCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.CreateFindWhatRegex();
    this.OnSettingsChanged();
  }

  private void ReplacementTextRadioButton_CheckedChanged(object sender, EventArgs e)
  {
    if (this._replacementTextRadioButton.Checked)
      this._replacementAttributeValueRadioButton.Checked = false;
    this.UpdateControl();
    this.OnSettingsChanged();
  }

  private void ReplacementAttributeValueRadioButton_CheckedChanged(object sender, EventArgs e)
  {
    if (this._replacementAttributeValueRadioButton.Checked)
      this._replacementTextRadioButton.Checked = false;
    this.UpdateControl();
    this.OnSettingsChanged();
  }

  private void ReplacementSpecialCharacterInputControl_Changed(object sender, EventArgs e)
  {
    this.OnSettingsChanged();
  }

  private void ReplacementAttributeComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.OnSettingsChanged();
  }

  private void ReplacementCharacterCaseTransformationComboBox_SelectedIndexChanged(
    object sender,
    EventArgs e)
  {
    this.OnSettingsChanged();
  }

  private void InitializeFindWhatSpecialCharacterInputControl()
  {
    this._findWhatSpecialCharacterInputControl.SpecialCharacters = new SpecialCharacter[4]
    {
      SpecialCharacters.AnyNumber,
      SpecialCharacters.Any,
      SpecialCharacters.AnyDigit,
      SpecialCharacters.AnyLetter
    };
  }

  private void InitializeReplacementSpecialCharacterInputControl()
  {
    this._replacementSpecialCharacterInputControl.SpecialCharacters = new SpecialCharacter[2]
    {
      SpecialCharacters.CurrentAttributeValue,
      SpecialCharacters.Counter
    };
  }

  private void InitializeReplacementCharacterCaseTransformationComboBox()
  {
    this._replacementCharacterCaseTransformationComboBox.BeginUpdate();
    try
    {
      this._replacementCharacterCaseTransformationComboBox.Items.Clear();
      this._replacementCharacterCaseTransformationComboBox.ValueMember = "Item1";
      this._replacementCharacterCaseTransformationComboBox.DisplayMember = "Item2";
      this._replacementCharacterCaseTransformationComboBox.Items.AddRange((object[]) new Tuple<CharacterCaseTransformation, string>[4]
      {
        new Tuple<CharacterCaseTransformation, string>(CharacterCaseTransformation.None, CharacterCaseTransformation.None.GetDescription<CharacterCaseTransformation>()),
        new Tuple<CharacterCaseTransformation, string>(CharacterCaseTransformation.LowerCase, CharacterCaseTransformation.LowerCase.GetDescription<CharacterCaseTransformation>()),
        new Tuple<CharacterCaseTransformation, string>(CharacterCaseTransformation.UpperCase, CharacterCaseTransformation.UpperCase.GetDescription<CharacterCaseTransformation>()),
        new Tuple<CharacterCaseTransformation, string>(CharacterCaseTransformation.StartWithCapital, CharacterCaseTransformation.StartWithCapital.GetDescription<CharacterCaseTransformation>())
      });
      this._replacementCharacterCaseTransformationComboBox.SelectedIndex = 0;
    }
    finally
    {
      this._replacementCharacterCaseTransformationComboBox.EndUpdate();
    }
  }

  private void FillAttributeComboBox(ComboBox comboBox, IEnumerable<int> attributeTypeIds)
  {
    comboBox.BeginUpdate();
    try
    {
      comboBox.Items.Clear();
      comboBox.DisplayMember = "Name";
      comboBox.ValueMember = "AttributeID";
      foreach (IMSAttributeType imsAttributeType in (IEnumerable<IMSAttributeType>) attributeTypeIds.Select<int, IMSAttributeType>((Func<int, IMSAttributeType>) (o => MetaDataHelper.GetAttributeType(o))).OrderBy<IMSAttributeType, string>((Func<IMSAttributeType, string>) (o => o.Name)))
        comboBox.Items.Add((object) imsAttributeType);
    }
    finally
    {
      comboBox.EndUpdate();
    }
    if (comboBox.Items.Count <= 0)
      return;
    comboBox.SelectedIndex = 0;
  }

  public void UpdateControl()
  {
    this._replacementSpecialCharacterInputControl.Enabled = this._replacementTextRadioButton.Checked;
    this._replacementAttributeComboBox.Enabled = this._replacementAttributeValueRadioButton.Checked;
  }

  private void CreateFindWhatRegex()
  {
    if (!string.IsNullOrEmpty(this._findWhatSpecialCharacterInputControl.Text))
      this.FindWhat = new FindWhatBuilder()
      {
        MatchCase = this._matchCaseCheckBox.Checked,
        Text = this._findWhatSpecialCharacterInputControl.Text,
        MatchCirillicLatinSimilarity = this._matchCirillicLatinSimilarityCheckBox.Checked
      }.GetResult();
    else
      this.FindWhat = (Regex) null;
  }

  private void OnSettingsChanged()
  {
    EventHandler settingsChanged = this.SettingsChanged;
    if (settingsChanged == null)
      return;
    settingsChanged((object) this, EventArgs.Empty);
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
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.groupBox3 = new GroupBox();
    this.tableLayoutPanel2 = new TableLayoutPanel();
    this.tableLayoutPanel8 = new TableLayoutPanel();
    this._replacementCharacterCaseTransformationComboBox = new ComboBox();
    this._replacementCharacterCaseTransformationLabel = new Label();
    this.tableLayoutPanel7 = new TableLayoutPanel();
    this._replacementAttributeComboBox = new ComboBox();
    this._replacementAttributeValueRadioButton = new RadioButton();
    this.tableLayoutPanel6 = new TableLayoutPanel();
    this._replacementSpecialCharacterInputControl = new SpecialCharacterInputControl();
    this._replacementTextRadioButton = new RadioButton();
    this.groupBox2 = new GroupBox();
    this.tableLayoutPanel3 = new TableLayoutPanel();
    this._matchCaseCheckBox = new CheckBox();
    this._matchCirillicLatinSimilarityCheckBox = new CheckBox();
    this._findWhatSpecialCharacterInputControl = new SpecialCharacterInputControl();
    this.groupBox1 = new GroupBox();
    this.tableLayoutPanel4 = new TableLayoutPanel();
    this._attributeComboBox = new ComboBox();
    this.tableLayoutPanel1.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.tableLayoutPanel2.SuspendLayout();
    this.tableLayoutPanel8.SuspendLayout();
    this.tableLayoutPanel7.SuspendLayout();
    this.tableLayoutPanel6.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.tableLayoutPanel3.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.tableLayoutPanel4.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.groupBox3, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.groupBox2, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.groupBox1, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 3;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(460, 551);
    this.tableLayoutPanel1.TabIndex = 0;
    this.groupBox3.AutoSize = true;
    this.groupBox3.Controls.Add((Control) this.tableLayoutPanel2);
    this.groupBox3.Dock = DockStyle.Fill;
    this.groupBox3.Location = new Point(3, 164);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Size = new Size(454, 384);
    this.groupBox3.TabIndex = 3;
    this.groupBox3.TabStop = false;
    this.groupBox3.Text = "Заменить на";
    this.tableLayoutPanel2.ColumnCount = 1;
    this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel2.Controls.Add((Control) this.tableLayoutPanel8, 0, 2);
    this.tableLayoutPanel2.Controls.Add((Control) this.tableLayoutPanel7, 0, 1);
    this.tableLayoutPanel2.Controls.Add((Control) this.tableLayoutPanel6, 0, 0);
    this.tableLayoutPanel2.Dock = DockStyle.Fill;
    this.tableLayoutPanel2.Location = new Point(3, 16 /*0x10*/);
    this.tableLayoutPanel2.Name = "tableLayoutPanel2";
    this.tableLayoutPanel2.RowCount = 4;
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel2.Size = new Size(448, 365);
    this.tableLayoutPanel2.TabIndex = 1;
    this.tableLayoutPanel8.AutoSize = true;
    this.tableLayoutPanel8.ColumnCount = 2;
    this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel8.Controls.Add((Control) this._replacementCharacterCaseTransformationComboBox, 1, 0);
    this.tableLayoutPanel8.Controls.Add((Control) this._replacementCharacterCaseTransformationLabel, 0, 0);
    this.tableLayoutPanel8.Dock = DockStyle.Fill;
    this.tableLayoutPanel8.Location = new Point(3, 126);
    this.tableLayoutPanel8.Name = "tableLayoutPanel8";
    this.tableLayoutPanel8.RowCount = 1;
    this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel8.Size = new Size(442, 27);
    this.tableLayoutPanel8.TabIndex = 2;
    this._replacementCharacterCaseTransformationComboBox.Dock = DockStyle.Fill;
    this._replacementCharacterCaseTransformationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._replacementCharacterCaseTransformationComboBox.FormattingEnabled = true;
    this._replacementCharacterCaseTransformationComboBox.Location = new Point(57, 3);
    this._replacementCharacterCaseTransformationComboBox.Name = "_replacementCharacterCaseTransformationComboBox";
    this._replacementCharacterCaseTransformationComboBox.Size = new Size(382, 21);
    this._replacementCharacterCaseTransformationComboBox.TabIndex = 1;
    this._replacementCharacterCaseTransformationComboBox.SelectedIndexChanged += new EventHandler(this.ReplacementCharacterCaseTransformationComboBox_SelectedIndexChanged);
    this._replacementCharacterCaseTransformationLabel.AutoSize = true;
    this._replacementCharacterCaseTransformationLabel.Location = new Point(3, 0);
    this._replacementCharacterCaseTransformationLabel.Name = "_replacementCharacterCaseTransformationLabel";
    this._replacementCharacterCaseTransformationLabel.Size = new Size(48 /*0x30*/, 13);
    this._replacementCharacterCaseTransformationLabel.TabIndex = 0;
    this._replacementCharacterCaseTransformationLabel.Text = "Регистр";
    this.tableLayoutPanel7.AutoSize = true;
    this.tableLayoutPanel7.ColumnCount = 1;
    this.tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel7.Controls.Add((Control) this._replacementAttributeComboBox, 0, 1);
    this.tableLayoutPanel7.Controls.Add((Control) this._replacementAttributeValueRadioButton, 0, 0);
    this.tableLayoutPanel7.Dock = DockStyle.Fill;
    this.tableLayoutPanel7.Location = new Point(3, 70);
    this.tableLayoutPanel7.Name = "tableLayoutPanel7";
    this.tableLayoutPanel7.RowCount = 2;
    this.tableLayoutPanel7.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel7.Size = new Size(442, 50);
    this.tableLayoutPanel7.TabIndex = 1;
    this._replacementAttributeComboBox.Dock = DockStyle.Fill;
    this._replacementAttributeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._replacementAttributeComboBox.FormattingEnabled = true;
    this._replacementAttributeComboBox.Location = new Point(3, 26);
    this._replacementAttributeComboBox.Name = "_replacementAttributeComboBox";
    this._replacementAttributeComboBox.Size = new Size(436, 21);
    this._replacementAttributeComboBox.TabIndex = 4;
    this._replacementAttributeComboBox.SelectedIndexChanged += new EventHandler(this.ReplacementAttributeComboBox_SelectedIndexChanged);
    this._replacementAttributeValueRadioButton.AutoSize = true;
    this._replacementAttributeValueRadioButton.Location = new Point(3, 3);
    this._replacementAttributeValueRadioButton.Name = "_replacementAttributeValueRadioButton";
    this._replacementAttributeValueRadioButton.Size = new Size(121, 17);
    this._replacementAttributeValueRadioButton.TabIndex = 1;
    this._replacementAttributeValueRadioButton.Text = "Значение атрибута";
    this._replacementAttributeValueRadioButton.UseVisualStyleBackColor = true;
    this._replacementAttributeValueRadioButton.CheckedChanged += new EventHandler(this.ReplacementAttributeValueRadioButton_CheckedChanged);
    this.tableLayoutPanel6.AutoSize = true;
    this.tableLayoutPanel6.ColumnCount = 1;
    this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel6.Controls.Add((Control) this._replacementSpecialCharacterInputControl, 0, 1);
    this.tableLayoutPanel6.Controls.Add((Control) this._replacementTextRadioButton, 0, 0);
    this.tableLayoutPanel6.Dock = DockStyle.Fill;
    this.tableLayoutPanel6.Location = new Point(3, 3);
    this.tableLayoutPanel6.Name = "tableLayoutPanel6";
    this.tableLayoutPanel6.RowCount = 2;
    this.tableLayoutPanel6.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel6.Size = new Size(442, 61);
    this.tableLayoutPanel6.TabIndex = 0;
    this._replacementSpecialCharacterInputControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this._replacementSpecialCharacterInputControl.Dock = DockStyle.Fill;
    this._replacementSpecialCharacterInputControl.Location = new Point(3, 26);
    this._replacementSpecialCharacterInputControl.Name = "_replacementSpecialCharacterInputControl";
    this._replacementSpecialCharacterInputControl.Size = new Size(436, 32 /*0x20*/);
    this._replacementSpecialCharacterInputControl.TabIndex = 6;
    this._replacementSpecialCharacterInputControl.Changed += new EventHandler(this.ReplacementSpecialCharacterInputControl_Changed);
    this._replacementTextRadioButton.AutoSize = true;
    this._replacementTextRadioButton.Checked = true;
    this._replacementTextRadioButton.Location = new Point(3, 3);
    this._replacementTextRadioButton.Name = "_replacementTextRadioButton";
    this._replacementTextRadioButton.Size = new Size(55, 17);
    this._replacementTextRadioButton.TabIndex = 1;
    this._replacementTextRadioButton.TabStop = true;
    this._replacementTextRadioButton.Text = "Текст";
    this._replacementTextRadioButton.UseVisualStyleBackColor = true;
    this._replacementTextRadioButton.CheckedChanged += new EventHandler(this.ReplacementTextRadioButton_CheckedChanged);
    this.groupBox2.AutoSize = true;
    this.groupBox2.Controls.Add((Control) this.tableLayoutPanel3);
    this.groupBox2.Dock = DockStyle.Fill;
    this.groupBox2.Location = new Point(3, 55);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(454, 103);
    this.groupBox2.TabIndex = 2;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Найти в значении атрибута";
    this.tableLayoutPanel3.AutoSize = true;
    this.tableLayoutPanel3.ColumnCount = 1;
    this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel3.Controls.Add((Control) this._matchCaseCheckBox, 0, 1);
    this.tableLayoutPanel3.Controls.Add((Control) this._matchCirillicLatinSimilarityCheckBox, 0, 2);
    this.tableLayoutPanel3.Controls.Add((Control) this._findWhatSpecialCharacterInputControl, 0, 0);
    this.tableLayoutPanel3.Dock = DockStyle.Fill;
    this.tableLayoutPanel3.Location = new Point(3, 16 /*0x10*/);
    this.tableLayoutPanel3.Name = "tableLayoutPanel3";
    this.tableLayoutPanel3.RowCount = 3;
    this.tableLayoutPanel3.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel3.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel3.Size = new Size(448, 84);
    this.tableLayoutPanel3.TabIndex = 0;
    this._matchCaseCheckBox.AutoSize = true;
    this._matchCaseCheckBox.Location = new Point(3, 41);
    this._matchCaseCheckBox.Name = "_matchCaseCheckBox";
    this._matchCaseCheckBox.Size = new Size(124, 17);
    this._matchCaseCheckBox.TabIndex = 2;
    this._matchCaseCheckBox.Text = "Учитывать регистр";
    this._matchCaseCheckBox.UseVisualStyleBackColor = true;
    this._matchCaseCheckBox.CheckedChanged += new EventHandler(this.MatchCaseCheckBox_CheckedChanged);
    this._matchCirillicLatinSimilarityCheckBox.AutoSize = true;
    this._matchCirillicLatinSimilarityCheckBox.Location = new Point(3, 64 /*0x40*/);
    this._matchCirillicLatinSimilarityCheckBox.Name = "_matchCirillicLatinSimilarityCheckBox";
    this._matchCirillicLatinSimilarityCheckBox.Size = new Size(276, 17);
    this._matchCirillicLatinSimilarityCheckBox.TabIndex = 2;
    this._matchCirillicLatinSimilarityCheckBox.Text = "Учитывать сходство букв кириллицы и латиницы";
    this._matchCirillicLatinSimilarityCheckBox.UseVisualStyleBackColor = true;
    this._matchCirillicLatinSimilarityCheckBox.CheckedChanged += new EventHandler(this.MatchCirillicLatinSimilarityCheckBox_CheckedChanged);
    this._findWhatSpecialCharacterInputControl.Dock = DockStyle.Fill;
    this._findWhatSpecialCharacterInputControl.Location = new Point(3, 3);
    this._findWhatSpecialCharacterInputControl.Name = "_findWhatSpecialCharacterInputControl";
    this._findWhatSpecialCharacterInputControl.Size = new Size(442, 32 /*0x20*/);
    this._findWhatSpecialCharacterInputControl.TabIndex = 3;
    this._findWhatSpecialCharacterInputControl.Changed += new EventHandler(this.FindWhatSpecialCharacterInputControl_Changed);
    this.groupBox1.AutoSize = true;
    this.groupBox1.Controls.Add((Control) this.tableLayoutPanel4);
    this.groupBox1.Dock = DockStyle.Fill;
    this.groupBox1.Location = new Point(3, 3);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(454, 46);
    this.groupBox1.TabIndex = 1;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Атрибут";
    this.tableLayoutPanel4.AutoSize = true;
    this.tableLayoutPanel4.ColumnCount = 1;
    this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel4.Controls.Add((Control) this._attributeComboBox, 0, 0);
    this.tableLayoutPanel4.Dock = DockStyle.Fill;
    this.tableLayoutPanel4.Location = new Point(3, 16 /*0x10*/);
    this.tableLayoutPanel4.Name = "tableLayoutPanel4";
    this.tableLayoutPanel4.RowCount = 1;
    this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel4.Size = new Size(448, 27);
    this.tableLayoutPanel4.TabIndex = 0;
    this._attributeComboBox.Dock = DockStyle.Fill;
    this._attributeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._attributeComboBox.FormattingEnabled = true;
    this._attributeComboBox.Location = new Point(3, 3);
    this._attributeComboBox.Name = "_attributeComboBox";
    this._attributeComboBox.Size = new Size(442, 21);
    this._attributeComboBox.TabIndex = 0;
    this._attributeComboBox.SelectedIndexChanged += new EventHandler(this.AttributeComboBox_SelectedIndexChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (GroupAttributesChangingSettingsControl);
    this.Size = new Size(460, 551);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.groupBox3.ResumeLayout(false);
    this.tableLayoutPanel2.ResumeLayout(false);
    this.tableLayoutPanel2.PerformLayout();
    this.tableLayoutPanel8.ResumeLayout(false);
    this.tableLayoutPanel8.PerformLayout();
    this.tableLayoutPanel7.ResumeLayout(false);
    this.tableLayoutPanel7.PerformLayout();
    this.tableLayoutPanel6.ResumeLayout(false);
    this.tableLayoutPanel6.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.tableLayoutPanel3.ResumeLayout(false);
    this.tableLayoutPanel3.PerformLayout();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.tableLayoutPanel4.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
