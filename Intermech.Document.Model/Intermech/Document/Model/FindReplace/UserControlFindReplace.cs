// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.FindReplace.UserControlFindReplace
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.XtraEditors.Controls;
using Intermech.Docking;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Document.Model.FindReplace;

public class UserControlFindReplace : UserControl
{
  private int _replaceTextHeight = -1;
  private bool _isReplaceMode = true;
  private bool _isExpanded = true;
  private int _minimunHeight = -1;
  protected int _oldBootomDif = -1;
  protected int _bottomGroupsHeight = -1;
  private string[] _findWhatHistroy = new string[50];
  private int _findWhatHistroyIndex;
  private string[] _replaceWhatHistroy = new string[50];
  private int _replaceWhatHistroyIndex;
  private readonly string constDropDownString = LocalizationHolder.rm.GetString("Document.Model_614");
  private readonly string constDropUpString = LocalizationHolder.rm.GetString("Document.Model_615");
  internal FindReplaceManager FindReplaceManager;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Button _btnReplaceAll;
  private Label _labelBoxReplaceWith;
  protected GroupBox _groupBoxFindOptions;
  protected CheckBox _checkBoxWholeWord;
  protected Label _labelComboBoxWhereToFind;
  protected CheckBox _checkBoxMathCase;
  protected Button _btnShowMore;
  private Label _labelFindWhere;
  private Label _labelFindWhat;
  protected Button _btnReplace;
  private ImageList _imageList;
  public Button _btnClose;
  internal Button _btnFindNext;
  private ComboBox _comboBoxReplaceWith;
  private ComboBox _comboBoxFindWhere;
  private ComboBox _comboBoxSearchDirrection;
  internal ComboBox _comboBoxFindText;

  public UserControlFindReplace()
  {
    this.InitializeComponent();
    this._comboBoxSearchDirrection.SelectedIndex = 2;
  }

  /// <summary> Если true, то производиться поиск с заменой, если false, то производиться простой поиск текста </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public bool IsReplaceMode
  {
    get => this._isReplaceMode;
    set
    {
      if (this._replaceTextHeight == -1)
        this._replaceTextHeight = this._labelBoxReplaceWith.Location.Y - this._labelFindWhat.Location.Y;
      Control parentForm = (Control) this.ParentForm;
      if (value == this._isReplaceMode || parentForm == null)
        return;
      if (this._minimunHeight == -1)
        this._minimunHeight = this._btnFindNext.Bottom - this._replaceTextHeight + 4;
      if (this.IsExpanded)
        this._bottomGroupsHeight = this._groupBoxFindOptions.Height + 8;
      if (this._oldBootomDif == -1)
        this._oldBootomDif = parentForm.Height - this.Height;
      if (value)
      {
        parentForm.SuspendLayout();
        this.SuspendLayout();
        try
        {
          this.Height = this._minimunHeight + this._replaceTextHeight + (this.IsExpanded ? this._bottomGroupsHeight : 0);
          parentForm.Height = this.Height + this._oldBootomDif;
        }
        finally
        {
          parentForm.ResumeLayout(true);
          this.Height = this._minimunHeight + this._replaceTextHeight + (this.IsExpanded ? this._bottomGroupsHeight : 0);
          this.ResumeLayout(false);
        }
        this.MoveBottomComponentsBy(this._replaceTextHeight);
        this._comboBoxReplaceWith.Visible = true;
        this._labelBoxReplaceWith.Visible = true;
        this._btnShowMore.Left = this._btnReplace.Left - this._btnShowMore.Width - 5;
        this._btnReplace.Enabled = true;
        this._btnReplace.Visible = true;
        this._btnReplaceAll.Enabled = true;
        this._btnReplaceAll.Visible = true;
      }
      else
      {
        this.SuspendLayout();
        try
        {
          this.Height = this._minimunHeight + (this.IsExpanded ? this._bottomGroupsHeight : 0);
          parentForm.Height = this.Height + this._oldBootomDif;
        }
        finally
        {
          parentForm.ResumeLayout(true);
          this.Height = this._minimunHeight + (this.IsExpanded ? this._bottomGroupsHeight : 0);
          this.ResumeLayout(false);
        }
        this._btnShowMore.Left = this._btnFindNext.Left - this._btnShowMore.Width - 5;
        this._btnReplace.Enabled = false;
        this._btnReplace.Visible = false;
        this._btnReplaceAll.Enabled = false;
        this._btnReplaceAll.Visible = false;
        this._comboBoxReplaceWith.Visible = false;
        this._labelBoxReplaceWith.Visible = false;
        this.MoveBottomComponentsBy(-this._replaceTextHeight);
      }
      this._isReplaceMode = value;
    }
  }

  private void MoveBottomComponentsBy(int moveBy)
  {
    if (this.Controls == null)
      return;
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (control != null && control != this._comboBoxReplaceWith && control != this._labelBoxReplaceWith && (moveBy > 0 ? (control.Location.Y >= this._labelBoxReplaceWith.Location.Y ? 1 : 0) : (control.Location.Y > this._comboBoxReplaceWith.Location.Y ? 1 : 0)) != 0)
        control.Location = new Point(control.Location.X, control.Location.Y + moveBy);
    }
  }

  /// <summary> Признак того, что используется расширеная форма настройки поиска (с доп. параметрами поиска) </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public bool IsExpanded
  {
    get => this._isExpanded;
    set
    {
      if (this._replaceTextHeight == -1)
        this._replaceTextHeight = this._labelBoxReplaceWith.Location.Y - this._labelFindWhat.Location.Y;
      if (value == this.IsExpanded || this.ParentForm == null)
        return;
      if (this._minimunHeight == -1)
        this._minimunHeight = this._btnFindNext.Bottom - this._replaceTextHeight + 4;
      if (this._oldBootomDif == -1)
        this._oldBootomDif = this.ParentForm.Height - this.Height;
      if (value)
      {
        this.ParentForm.SuspendLayout();
        this.SuspendLayout();
        try
        {
          this.Height = this._minimunHeight + (this.IsReplaceMode ? this._replaceTextHeight : 0) + this._bottomGroupsHeight;
          this.ParentForm.Height = this.Height + this._oldBootomDif;
        }
        finally
        {
          this.ParentForm.ResumeLayout(true);
          this.Height = this._minimunHeight + (this.IsReplaceMode ? this._replaceTextHeight : 0) + this._bottomGroupsHeight;
          this.ResumeLayout(false);
        }
      }
      else
        this._bottomGroupsHeight = this._groupBoxFindOptions.Height + 8;
      this.SetGroupBoxesVisible(value);
      if (!value)
      {
        this.ParentForm.SuspendLayout();
        this.SuspendLayout();
        try
        {
          this.Height = this._minimunHeight + (this.IsReplaceMode ? this._replaceTextHeight : 0);
          this.ParentForm.Height = this.Height + this._oldBootomDif;
        }
        finally
        {
          this.ParentForm.ResumeLayout(true);
          this.Height = this._minimunHeight + (this.IsReplaceMode ? this._replaceTextHeight : 0);
          this.ResumeLayout(false);
        }
      }
      this._btnShowMore.Text = value ? this.constDropUpString : this.constDropDownString;
      this._btnShowMore.ImageIndex = value ? 1 : 0;
      this._isExpanded = value;
    }
  }

  /// <summary> Признака видимости GroupBox-ов </summary>
  /// <param name="visible"> Признак видимости GroupBox-ов </param>
  protected virtual void SetGroupBoxesVisible(bool visible)
  {
    this._groupBoxFindOptions.Enabled = visible;
    this._groupBoxFindOptions.Anchor = visible ? AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left : AnchorStyles.Top | AnchorStyles.Left;
    this._groupBoxFindOptions.Visible = visible;
  }

  /// <summary> Строка поиска </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string FindWhat
  {
    get => this._comboBoxFindText.Text;
    set => this._comboBoxFindText.Text = value;
  }

  /// <summary> На что требуется заменять найденый текст </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string ReplaceWith
  {
    get => this._comboBoxReplaceWith.Text;
    set => this._comboBoxReplaceWith.Text = value;
  }

  /// <summary> Список доступных мест для поиска текста (например, поиск в [текущем документе], [на текущей странице] и т.п.) </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public string[] PossibleSearchPlaces
  {
    get
    {
      string[] possibleSearchPlaces = new string[this._comboBoxFindWhere.Items.Count];
      int index = 0;
      foreach (ImageComboBoxItem imageComboBoxItem in this._comboBoxFindWhere.Items)
      {
        possibleSearchPlaces.SetValue((object) imageComboBoxItem.Description, index);
        ++index;
      }
      return possibleSearchPlaces;
    }
    set
    {
      this._comboBoxFindWhere.Items.Clear();
      foreach (string _description in value)
      {
        if (_description != null)
        {
          ImageComboBoxItem imageComboBoxItem = new ImageComboBoxItem(_description);
          imageComboBoxItem.Value = (object) _description;
          this._comboBoxFindWhere.Items.Add((object) imageComboBoxItem);
        }
      }
      if (this._comboBoxFindWhere.Items.Count <= 0)
        return;
      this._comboBoxFindWhere.SelectedIndex = 0;
    }
  }

  /// <summary> Добавить параметр "Заменить на" в историю соотв. контрола </summary>
  private void AddToFindWhatHistory()
  {
    if (Array.IndexOf<string>(this._findWhatHistroy, this.FindWhat) != -1)
      return;
    this._findWhatHistroy[this._findWhatHistroyIndex] = this.FindWhat;
    ++this._findWhatHistroyIndex;
    if (this._findWhatHistroyIndex >= 50)
      this._findWhatHistroyIndex = 0;
    this._comboBoxFindText.Items.Insert(0, (object) new ComboBoxItem((object) this.FindWhat));
    if (this._comboBoxFindText.Items.Count <= 50)
      return;
    this._comboBoxFindText.Items.RemoveAt(this._comboBoxFindText.Items.Count - 1);
  }

  /// <summary> Добавить параметр "Заменить на" в историю соотв. контрола </summary>
  private void AddToFindReplaceHistory()
  {
    if (Array.IndexOf<string>(this._replaceWhatHistroy, this.ReplaceWith) != -1)
      return;
    this._replaceWhatHistroy[this._replaceWhatHistroyIndex] = this.ReplaceWith;
    ++this._replaceWhatHistroyIndex;
    if (this._replaceWhatHistroyIndex >= 50)
      this._replaceWhatHistroyIndex = 0;
    this._comboBoxReplaceWith.Items.Insert(0, (object) new ComboBoxItem((object) this.ReplaceWith));
    if (this._comboBoxReplaceWith.Items.Count <= 50)
      return;
    this._comboBoxReplaceWith.Items.RemoveAt(this._comboBoxReplaceWith.Items.Count - 1);
  }

  /// <summary> Индекс выбраного места для поиска в PossibleSearchPlaces </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public int SelectedSearchPlace
  {
    get => this._comboBoxFindWhere.SelectedIndex;
    set
    {
      if (value < 0 || value >= this._comboBoxFindWhere.Items.Count)
        return;
      this._comboBoxFindWhere.SelectedIndex = value;
    }
  }

  /// <summary> Направление сортировки </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public SearchDirrection SearchDirrection
  {
    get
    {
      return this._comboBoxSearchDirrection.SelectedIndex >= 0 ? (SearchDirrection) this._comboBoxSearchDirrection.SelectedIndex : SearchDirrection.EntireDocSearch;
    }
    set
    {
      int num = (int) value;
      if (num < 0 || num >= this._comboBoxSearchDirrection.Items.Count)
        return;
      this._comboBoxSearchDirrection.SelectedIndex = num;
    }
  }

  /// <summary> Признак того, что поиск должен вестись с учётом регистра </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public bool MatchCase
  {
    get => this._checkBoxMathCase.Checked;
    set => this._checkBoxMathCase.Checked = value;
  }

  /// <summary> Признак того, что ищется слово "целиком" </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public bool MatchWholeWord
  {
    get => this._checkBoxWholeWord.Checked;
    set => this._checkBoxWholeWord.Checked = value;
  }

  internal void FromManager()
  {
    this._comboBoxFindText.Items.Clear();
    this._comboBoxFindText.Items.AddRange((object[]) FindReplaceManager.HistoryFind.ToArray());
    this._comboBoxReplaceWith.Items.Clear();
    this._comboBoxReplaceWith.Items.AddRange((object[]) FindReplaceManager.HistoryReplace.ToArray());
    this.FindWhat = this.FindReplaceManager.FindWhat;
    this.MatchCase = this.FindReplaceManager.MatchCase;
    this.MatchWholeWord = this.FindReplaceManager.MatchWholeWord;
    this.PossibleSearchPlaces = this.FindReplaceManager.PossibleSearchPlaces;
    this.ReplaceWith = this.FindReplaceManager.ReplaceWith;
    this.SearchDirrection = this.FindReplaceManager.SearchDirrection;
    this.SelectedSearchPlace = this.FindReplaceManager.SelectedSearchPlace;
  }

  internal void ToManager()
  {
    this.FindReplaceManager.Initialized = true;
    this.FindReplaceManager.FindWhat = this.FindWhat;
    this.FindReplaceManager.MatchCase = this.MatchCase;
    this.FindReplaceManager.MatchWholeWord = this.MatchWholeWord;
    this.FindReplaceManager.ReplaceWith = this.ReplaceWith;
    this.FindReplaceManager.SearchDirrection = this.SearchDirrection;
    this.FindReplaceManager.SelectedSearchPlace = this.SelectedSearchPlace;
  }

  private void _btnShowMore_Click(object sender, EventArgs e) => this.IsExpanded = !this.IsExpanded;

  private void _btnReplace_Click(object sender, EventArgs e)
  {
    this.ToManager();
    this.FindReplaceManager.Replace();
    this.FromManager();
  }

  private void _btnReplaceAll_Click(object sender, EventArgs e)
  {
    this.ToManager();
    this.FindReplaceManager.ReplaceAll();
    this.FromManager();
  }

  private void _btnFindNext_Click(object sender, EventArgs e) => this.FindNext();

  private DockControl DockControl
  {
    get
    {
      for (Control dockControl = (Control) this; dockControl != null; dockControl = dockControl.Parent)
      {
        if (dockControl is DockControl)
          return dockControl as DockControl;
      }
      return (DockControl) null;
    }
  }

  private void _btnClose_Click(object sender, EventArgs e)
  {
    if (this.DockControl != null)
      this.DockControl.Close();
    else
      this.ParentForm.Close();
  }

  public void FindNext()
  {
    this.ToManager();
    this.FindReplaceManager.Find();
    this.FromManager();
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlFindReplace));
    this._btnReplaceAll = new Button();
    this._labelBoxReplaceWith = new Label();
    this._groupBoxFindOptions = new GroupBox();
    this._comboBoxSearchDirrection = new ComboBox();
    this._checkBoxWholeWord = new CheckBox();
    this._labelComboBoxWhereToFind = new Label();
    this._checkBoxMathCase = new CheckBox();
    this._btnClose = new Button();
    this._btnFindNext = new Button();
    this._btnShowMore = new Button();
    this._imageList = new ImageList(this.components);
    this._labelFindWhere = new Label();
    this._labelFindWhat = new Label();
    this._btnReplace = new Button();
    this._comboBoxFindText = new ComboBox();
    this._comboBoxReplaceWith = new ComboBox();
    this._comboBoxFindWhere = new ComboBox();
    this._groupBoxFindOptions.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnReplaceAll, "_btnReplaceAll");
    this._btnReplaceAll.Name = "_btnReplaceAll";
    this._btnReplaceAll.UseVisualStyleBackColor = true;
    this._btnReplaceAll.Click += new EventHandler(this._btnReplaceAll_Click);
    componentResourceManager.ApplyResources((object) this._labelBoxReplaceWith, "_labelBoxReplaceWith");
    this._labelBoxReplaceWith.BackColor = SystemColors.Window;
    this._labelBoxReplaceWith.FlatStyle = FlatStyle.System;
    this._labelBoxReplaceWith.Name = "_labelBoxReplaceWith";
    componentResourceManager.ApplyResources((object) this._groupBoxFindOptions, "_groupBoxFindOptions");
    this._groupBoxFindOptions.BackColor = SystemColors.Window;
    this._groupBoxFindOptions.Controls.Add((Control) this._comboBoxSearchDirrection);
    this._groupBoxFindOptions.Controls.Add((Control) this._checkBoxWholeWord);
    this._groupBoxFindOptions.Controls.Add((Control) this._labelComboBoxWhereToFind);
    this._groupBoxFindOptions.Controls.Add((Control) this._checkBoxMathCase);
    this._groupBoxFindOptions.FlatStyle = FlatStyle.System;
    this._groupBoxFindOptions.Name = "_groupBoxFindOptions";
    this._groupBoxFindOptions.TabStop = false;
    this._comboBoxSearchDirrection.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxSearchDirrection.FormattingEnabled = true;
    this._comboBoxSearchDirrection.Items.AddRange(new object[3]
    {
      (object) componentResourceManager.GetString("_comboBoxSearchDirrection.Items"),
      (object) componentResourceManager.GetString("_comboBoxSearchDirrection.Items1"),
      (object) componentResourceManager.GetString("_comboBoxSearchDirrection.Items2")
    });
    componentResourceManager.ApplyResources((object) this._comboBoxSearchDirrection, "_comboBoxSearchDirrection");
    this._comboBoxSearchDirrection.Name = "_comboBoxSearchDirrection";
    componentResourceManager.ApplyResources((object) this._checkBoxWholeWord, "_checkBoxWholeWord");
    this._checkBoxWholeWord.Name = "_checkBoxWholeWord";
    this._checkBoxWholeWord.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._labelComboBoxWhereToFind, "_labelComboBoxWhereToFind");
    this._labelComboBoxWhereToFind.FlatStyle = FlatStyle.System;
    this._labelComboBoxWhereToFind.Name = "_labelComboBoxWhereToFind";
    componentResourceManager.ApplyResources((object) this._checkBoxMathCase, "_checkBoxMathCase");
    this._checkBoxMathCase.Name = "_checkBoxMathCase";
    this._checkBoxMathCase.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnClose, "_btnClose");
    this._btnClose.DialogResult = DialogResult.Cancel;
    this._btnClose.Name = "_btnClose";
    this._btnClose.UseVisualStyleBackColor = true;
    this._btnClose.Click += new EventHandler(this._btnClose_Click);
    componentResourceManager.ApplyResources((object) this._btnFindNext, "_btnFindNext");
    this._btnFindNext.Name = "_btnFindNext";
    this._btnFindNext.UseVisualStyleBackColor = true;
    this._btnFindNext.Click += new EventHandler(this._btnFindNext_Click);
    componentResourceManager.ApplyResources((object) this._btnShowMore, "_btnShowMore");
    this._btnShowMore.ImageList = this._imageList;
    this._btnShowMore.Name = "_btnShowMore";
    this._btnShowMore.UseVisualStyleBackColor = true;
    this._btnShowMore.Click += new EventHandler(this._btnShowMore_Click);
    this._imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imageList.ImageStream");
    this._imageList.TransparentColor = Color.Transparent;
    this._imageList.Images.SetKeyName(0, "DropDownParams2.gif");
    this._imageList.Images.SetKeyName(1, "DropUpParams2.gif");
    componentResourceManager.ApplyResources((object) this._labelFindWhere, "_labelFindWhere");
    this._labelFindWhere.BackColor = SystemColors.Window;
    this._labelFindWhere.FlatStyle = FlatStyle.System;
    this._labelFindWhere.Name = "_labelFindWhere";
    componentResourceManager.ApplyResources((object) this._labelFindWhat, "_labelFindWhat");
    this._labelFindWhat.BackColor = SystemColors.Window;
    this._labelFindWhat.FlatStyle = FlatStyle.System;
    this._labelFindWhat.Name = "_labelFindWhat";
    componentResourceManager.ApplyResources((object) this._btnReplace, "_btnReplace");
    this._btnReplace.Name = "_btnReplace";
    this._btnReplace.UseVisualStyleBackColor = true;
    this._btnReplace.Click += new EventHandler(this._btnReplace_Click);
    componentResourceManager.ApplyResources((object) this._comboBoxFindText, "_comboBoxFindText");
    this._comboBoxFindText.FormattingEnabled = true;
    this._comboBoxFindText.Name = "_comboBoxFindText";
    componentResourceManager.ApplyResources((object) this._comboBoxReplaceWith, "_comboBoxReplaceWith");
    this._comboBoxReplaceWith.FormattingEnabled = true;
    this._comboBoxReplaceWith.Name = "_comboBoxReplaceWith";
    componentResourceManager.ApplyResources((object) this._comboBoxFindWhere, "_comboBoxFindWhere");
    this._comboBoxFindWhere.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxFindWhere.FormattingEnabled = true;
    this._comboBoxFindWhere.Name = "_comboBoxFindWhere";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Window;
    this.Controls.Add((Control) this._groupBoxFindOptions);
    this.Controls.Add((Control) this._comboBoxFindWhere);
    this.Controls.Add((Control) this._comboBoxReplaceWith);
    this.Controls.Add((Control) this._comboBoxFindText);
    this.Controls.Add((Control) this._btnReplaceAll);
    this.Controls.Add((Control) this._labelBoxReplaceWith);
    this.Controls.Add((Control) this._btnClose);
    this.Controls.Add((Control) this._btnFindNext);
    this.Controls.Add((Control) this._btnShowMore);
    this.Controls.Add((Control) this._labelFindWhere);
    this.Controls.Add((Control) this._labelFindWhat);
    this.Controls.Add((Control) this._btnReplace);
    this.Name = nameof (UserControlFindReplace);
    this._groupBoxFindOptions.ResumeLayout(false);
    this._groupBoxFindOptions.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
