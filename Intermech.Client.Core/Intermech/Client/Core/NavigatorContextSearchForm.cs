
// Type: Intermech.Client.Core.NavigatorContextSearchForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Форма "Найти текст" (контекстный поиск в элементах Навигатора)
/// </summary>
public sealed class NavigatorContextSearchForm : Form
{
  private string NavigatorContextSearchFormMementoKey = "NavigatorContextSearchFormMemento";
  private INavigatorContextSearch _navigatorContextSearch;
  private List<string> _searchHistory = new List<string>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel panelBottom;
  protected ComboBox _findWhatComboBox;
  protected Label labelSearch;
  private GroupBox gbOptions;
  private CheckBox _matchCaseChekBox;
  private CheckBox _useRegexCheckBox;
  private CheckBox _searchInCurrentColumnOnlyCheckBox;
  private GroupBox gbFind;
  private RadioButton _searchFromCurrentPositionRadioButton;
  private RadioButton _searchFromBeginningRadioButton;
  private GroupBox gbDirection;
  private RadioButton _searchBackwardRadioButton;
  private RadioButton _searchForwardRadioButton;
  protected Button _clearSearchHistoryButton;
  private ImageList imageList;
  private ToolTip toolTips;
  private Label _currentColumnTextLabel;
  private CheckBox _useSearchMaskCheckBox;
  private CheckBox _matchWholeWordCheckBox;
  private Button _cancelButton;
  private Button _findButton;
  private Button _findAllButton;

  public NavigatorContextSearchForm()
  {
    this.InitializeComponent();
    this.UpdateControls();
  }

  public INavigatorContextSearch NavigatorContextSearch
  {
    get => this._navigatorContextSearch;
    set
    {
      if (this._navigatorContextSearch == value)
        return;
      if (this._navigatorContextSearch != null)
        this._navigatorContextSearch.CurrentColumnChanged -= new EventHandler(this.NavigatorContextSearch_CurrentColumnChanged);
      this._navigatorContextSearch = value;
      if (this._navigatorContextSearch != null)
        this._navigatorContextSearch.CurrentColumnChanged += new EventHandler(this.NavigatorContextSearch_CurrentColumnChanged);
      this.UpdateControls();
    }
  }

  public string FindWhat { get; private set; }

  public void FindNext()
  {
    this.AddComboBoxTextToHistory();
    Tuple<int, int, string> tuple = this.FindAll().FirstOrDefault<Tuple<int, int, string>>();
    if (tuple != null)
      this.SelectCellValues(new Tuple<int, int, string>[1]
      {
        tuple
      });
    else
      this.ShowNotFoundMessage();
  }

  public NavigatorContextSearchForm.NavigatorContextSearchFormMemento GetMemento()
  {
    return new NavigatorContextSearchForm.NavigatorContextSearchFormMemento()
    {
      FindWhat = this._findWhatComboBox.Text,
      SearchHistory = this._searchHistory.ToArray(),
      MatchCase = this._matchCaseChekBox.Checked,
      MatchWholeWord = this._matchWholeWordCheckBox.Checked,
      UseSearchMask = this._useSearchMaskCheckBox.Checked,
      UseRegex = this._useRegexCheckBox.Checked,
      SearchInCurrentColumnOnly = this._searchInCurrentColumnOnlyCheckBox.Checked,
      SearchFromCurrentPosition = this._searchFromCurrentPositionRadioButton.Checked,
      SearchForward = this._searchForwardRadioButton.Checked
    };
  }

  public void SetMemento(
    NavigatorContextSearchForm.NavigatorContextSearchFormMemento memento)
  {
    this._findWhatComboBox.Text = memento != null ? memento.FindWhat : throw new ArgumentNullException(nameof (memento));
    if (memento.SearchHistory != null)
    {
      this._searchHistory.Clear();
      this._searchHistory.AddRange((IEnumerable<string>) memento.SearchHistory);
      this.FillHistoryComboBox();
    }
    this._matchCaseChekBox.Checked = memento.MatchCase;
    this._matchWholeWordCheckBox.Checked = memento.MatchWholeWord;
    this._useSearchMaskCheckBox.Checked = memento.UseSearchMask;
    this._useRegexCheckBox.Checked = memento.UseRegex;
    this._searchInCurrentColumnOnlyCheckBox.Checked = memento.SearchInCurrentColumnOnly;
    this._searchFromCurrentPositionRadioButton.Checked = memento.SearchFromCurrentPosition;
    this._searchFromBeginningRadioButton.Checked = !memento.SearchFromCurrentPosition;
    this._searchForwardRadioButton.Checked = memento.SearchForward;
    this._searchBackwardRadioButton.Checked = !memento.SearchForward;
    this.UpdateControls();
  }

  public void SetFocusToComboBox() => this._findWhatComboBox.Focus();

  private void NavigatorContextSearch_CurrentColumnChanged(object sender, EventArgs e)
  {
    if (!this.IsDisposed)
    {
      this.UpdateControls();
    }
    else
    {
      if (this._navigatorContextSearch == null)
        return;
      this._navigatorContextSearch.CurrentColumnChanged -= new EventHandler(this.NavigatorContextSearch_CurrentColumnChanged);
    }
  }

  private void NavigatorContextSearchForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this, (IDictionary) new Hashtable()
    {
      [(object) this.NavigatorContextSearchFormMementoKey] = (object) this.SerializeMemento(this.GetMemento())
    });
  }

  private void NavigatorContextSearchForm_Load(object sender, EventArgs e)
  {
    Hashtable hashtable = new Hashtable();
    FormStorage.LoadLayout((Control) this, (IDictionary) hashtable);
    if (!hashtable.ContainsKey((object) this.NavigatorContextSearchFormMementoKey))
      return;
    string text = hashtable[(object) this.NavigatorContextSearchFormMementoKey] as string;
    if (string.IsNullOrEmpty(text))
      return;
    NavigatorContextSearchForm.NavigatorContextSearchFormMemento memento = this.DeserializeMemento(text);
    if (memento == null)
      return;
    this.SetMemento(memento);
  }

  private void ClearSearchHistoryButton_Click(object sender, EventArgs e)
  {
    this._searchHistory.Clear();
    this.FillHistoryComboBox();
  }

  private void FindWhatСomboBox_TextChanged(object sender, EventArgs e)
  {
    this.FindWhat = !string.IsNullOrEmpty(this._findWhatComboBox.Text) ? this._findWhatComboBox.Text.Trim() : (string) null;
    this.UpdateControls();
  }

  private void MatchWholeWordCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void UseSearchMaskCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void SearchInCurrentColumnOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void FindButton_Click(object sender, EventArgs e) => this.FindNext();

  private void FindAllButton_Click(object sender, EventArgs e)
  {
    this.AddComboBoxTextToHistory();
    Tuple<int, int, string>[] array = this.FindAll().ToArray<Tuple<int, int, string>>();
    if (array.Length != 0)
      this.SelectCellValues(array);
    else
      this.ShowNotFoundMessage();
  }

  private void CancelButton_Click(object sender, EventArgs e) => this.Close();

  private void UpdateControls()
  {
    this._clearSearchHistoryButton.Enabled = this._searchHistory.Count > 0;
    this._matchWholeWordCheckBox.Enabled = !this._useSearchMaskCheckBox.Checked;
    this._useSearchMaskCheckBox.Enabled = !this._matchWholeWordCheckBox.Checked;
    this._useRegexCheckBox.Enabled = this._useSearchMaskCheckBox.Checked;
    this._currentColumnTextLabel.Enabled = this._searchInCurrentColumnOnlyCheckBox.Checked;
    this._currentColumnTextLabel.Text = this._navigatorContextSearch != null ? this._navigatorContextSearch.CurrentColumnText : string.Empty;
    this._findButton.Enabled = this._findAllButton.Enabled = this._navigatorContextSearch != null && !string.IsNullOrEmpty(this.FindWhat);
  }

  private void FillHistoryComboBox()
  {
    this._findWhatComboBox.BeginUpdate();
    try
    {
      this._findWhatComboBox.Items.Clear();
      this._findWhatComboBox.Items.AddRange((object[]) this._searchHistory.ToArray());
    }
    finally
    {
      this._findWhatComboBox.EndUpdate();
    }
    this.UpdateControls();
  }

  private string SerializeMemento(
    NavigatorContextSearchForm.NavigatorContextSearchFormMemento memento)
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) memento);
      return Convert.ToBase64String(serializationStream.GetBuffer());
    }
  }

  private NavigatorContextSearchForm.NavigatorContextSearchFormMemento DeserializeMemento(
    string text)
  {
    try
    {
      using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(text)))
        return new BinaryFormatter().Deserialize((Stream) serializationStream) as NavigatorContextSearchForm.NavigatorContextSearchFormMemento;
    }
    catch
    {
      return (NavigatorContextSearchForm.NavigatorContextSearchFormMemento) null;
    }
  }

  private void AddComboBoxTextToHistory()
  {
    if (string.IsNullOrEmpty(this._findWhatComboBox.Text))
      return;
    string str = this._findWhatComboBox.Text.Trim();
    if (string.IsNullOrEmpty(str) || this._searchHistory.Contains(str))
      return;
    this._searchHistory.Add(str);
    this.FillHistoryComboBox();
  }

  private IEnumerable<Tuple<int, int, string>> FindAll()
  {
    Regex regex = (Regex) null;
    if (this._useSearchMaskCheckBox.Checked)
      regex = !this._useRegexCheckBox.Checked ? RegexHelper.ToRegex(this._findWhatComboBox.Text, !this._matchCaseChekBox.Checked) : new Regex(this.FindWhat, this._matchCaseChekBox.Checked ? RegexOptions.Singleline : RegexOptions.IgnoreCase | RegexOptions.Singleline);
    foreach (Tuple<int, int, string> cellValue in this.GetCellValues())
    {
      if (!string.IsNullOrEmpty(cellValue.Item3) && (regex != null ? (regex.IsMatch(cellValue.Item3) ? 1 : 0) : (StringsHelper.Exists(cellValue.Item3, this.FindWhat, this._matchCaseChekBox.Checked, this._matchWholeWordCheckBox.Checked) ? 1 : 0)) != 0)
        yield return cellValue;
    }
  }

  private IEnumerable<Tuple<int, int, string>> GetCellValues()
  {
    return this._navigatorContextSearch.GetCellValues(this._searchInCurrentColumnOnlyCheckBox.Checked, this._searchFromBeginningRadioButton.Checked, this._searchBackwardRadioButton.Checked);
  }

  private void SelectCellValues(Tuple<int, int, string>[] cellValues)
  {
    this._navigatorContextSearch.SelectCells(((IEnumerable<Tuple<int, int, string>>) cellValues).Select<Tuple<int, int, string>, Tuple<int, int>>((Func<Tuple<int, int, string>, Tuple<int, int>>) (o => new Tuple<int, int>(o.Item1, o.Item2))).ToArray<Tuple<int, int>>());
  }

  private void ShowNotFoundMessage()
  {
    int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_1387"), (object) this.FindWhat), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NavigatorContextSearchForm));
    this.panelBottom = new Panel();
    this._findAllButton = new Button();
    this._cancelButton = new Button();
    this._findButton = new Button();
    this._findWhatComboBox = new ComboBox();
    this.labelSearch = new Label();
    this.gbOptions = new GroupBox();
    this._currentColumnTextLabel = new Label();
    this._searchInCurrentColumnOnlyCheckBox = new CheckBox();
    this._useRegexCheckBox = new CheckBox();
    this._matchWholeWordCheckBox = new CheckBox();
    this._useSearchMaskCheckBox = new CheckBox();
    this._matchCaseChekBox = new CheckBox();
    this.gbFind = new GroupBox();
    this._searchFromBeginningRadioButton = new RadioButton();
    this._searchFromCurrentPositionRadioButton = new RadioButton();
    this.gbDirection = new GroupBox();
    this._searchBackwardRadioButton = new RadioButton();
    this._searchForwardRadioButton = new RadioButton();
    this._clearSearchHistoryButton = new Button();
    this.imageList = new ImageList(this.components);
    this.toolTips = new ToolTip(this.components);
    this.panelBottom.SuspendLayout();
    this.gbOptions.SuspendLayout();
    this.gbFind.SuspendLayout();
    this.gbDirection.SuspendLayout();
    this.SuspendLayout();
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this._findAllButton);
    this.panelBottom.Controls.Add((Control) this._cancelButton);
    this.panelBottom.Controls.Add((Control) this._findButton);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this._findAllButton, "_findAllButton");
    this._findAllButton.Cursor = Cursors.Default;
    this._findAllButton.Name = "_findAllButton";
    this.toolTips.SetToolTip((Control) this._findAllButton, componentResourceManager.GetString("_findAllButton.ToolTip"));
    this._findAllButton.Click += new EventHandler(this.FindAllButton_Click);
    componentResourceManager.ApplyResources((object) this._cancelButton, "_cancelButton");
    this._cancelButton.Cursor = Cursors.Default;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Click += new EventHandler(this.CancelButton_Click);
    componentResourceManager.ApplyResources((object) this._findButton, "_findButton");
    this._findButton.Cursor = Cursors.Default;
    this._findButton.Name = "_findButton";
    this.toolTips.SetToolTip((Control) this._findButton, componentResourceManager.GetString("_findButton.ToolTip"));
    this._findButton.Click += new EventHandler(this.FindButton_Click);
    this._findWhatComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this._findWhatComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
    componentResourceManager.ApplyResources((object) this._findWhatComboBox, "_findWhatComboBox");
    this._findWhatComboBox.FormattingEnabled = true;
    this._findWhatComboBox.Name = "_findWhatComboBox";
    this._findWhatComboBox.Sorted = true;
    this._findWhatComboBox.TextChanged += new EventHandler(this.FindWhatСomboBox_TextChanged);
    componentResourceManager.ApplyResources((object) this.labelSearch, "labelSearch");
    this.labelSearch.Name = "labelSearch";
    this.gbOptions.Controls.Add((Control) this._currentColumnTextLabel);
    this.gbOptions.Controls.Add((Control) this._searchInCurrentColumnOnlyCheckBox);
    this.gbOptions.Controls.Add((Control) this._useRegexCheckBox);
    this.gbOptions.Controls.Add((Control) this._matchWholeWordCheckBox);
    this.gbOptions.Controls.Add((Control) this._useSearchMaskCheckBox);
    this.gbOptions.Controls.Add((Control) this._matchCaseChekBox);
    this.gbOptions.FlatStyle = FlatStyle.System;
    componentResourceManager.ApplyResources((object) this.gbOptions, "gbOptions");
    this.gbOptions.Name = "gbOptions";
    this.gbOptions.TabStop = false;
    this._currentColumnTextLabel.AutoEllipsis = true;
    componentResourceManager.ApplyResources((object) this._currentColumnTextLabel, "_currentColumnTextLabel");
    this._currentColumnTextLabel.ForeColor = Color.MediumBlue;
    this._currentColumnTextLabel.Name = "_currentColumnTextLabel";
    this.toolTips.SetToolTip((Control) this._currentColumnTextLabel, componentResourceManager.GetString("_currentColumnTextLabel.ToolTip"));
    componentResourceManager.ApplyResources((object) this._searchInCurrentColumnOnlyCheckBox, "_searchInCurrentColumnOnlyCheckBox");
    this._searchInCurrentColumnOnlyCheckBox.Name = "_searchInCurrentColumnOnlyCheckBox";
    this.toolTips.SetToolTip((Control) this._searchInCurrentColumnOnlyCheckBox, componentResourceManager.GetString("_searchInCurrentColumnOnlyCheckBox.ToolTip"));
    this._searchInCurrentColumnOnlyCheckBox.UseVisualStyleBackColor = true;
    this._searchInCurrentColumnOnlyCheckBox.CheckedChanged += new EventHandler(this.SearchInCurrentColumnOnlyCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._useRegexCheckBox, "_useRegexCheckBox");
    this._useRegexCheckBox.Name = "_useRegexCheckBox";
    this.toolTips.SetToolTip((Control) this._useRegexCheckBox, componentResourceManager.GetString("_useRegexCheckBox.ToolTip"));
    this._useRegexCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._matchWholeWordCheckBox, "_matchWholeWordCheckBox");
    this._matchWholeWordCheckBox.Name = "_matchWholeWordCheckBox";
    this.toolTips.SetToolTip((Control) this._matchWholeWordCheckBox, componentResourceManager.GetString("_matchWholeWordCheckBox.ToolTip"));
    this._matchWholeWordCheckBox.UseVisualStyleBackColor = true;
    this._matchWholeWordCheckBox.CheckedChanged += new EventHandler(this.MatchWholeWordCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._useSearchMaskCheckBox, "_useSearchMaskCheckBox");
    this._useSearchMaskCheckBox.Name = "_useSearchMaskCheckBox";
    this.toolTips.SetToolTip((Control) this._useSearchMaskCheckBox, componentResourceManager.GetString("_useSearchMaskCheckBox.ToolTip"));
    this._useSearchMaskCheckBox.UseVisualStyleBackColor = true;
    this._useSearchMaskCheckBox.CheckedChanged += new EventHandler(this.UseSearchMaskCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._matchCaseChekBox, "_matchCaseChekBox");
    this._matchCaseChekBox.Name = "_matchCaseChekBox";
    this.toolTips.SetToolTip((Control) this._matchCaseChekBox, componentResourceManager.GetString("_matchCaseChekBox.ToolTip"));
    this._matchCaseChekBox.UseVisualStyleBackColor = true;
    this.gbFind.Controls.Add((Control) this._searchFromBeginningRadioButton);
    this.gbFind.Controls.Add((Control) this._searchFromCurrentPositionRadioButton);
    this.gbFind.FlatStyle = FlatStyle.System;
    componentResourceManager.ApplyResources((object) this.gbFind, "gbFind");
    this.gbFind.Name = "gbFind";
    this.gbFind.TabStop = false;
    componentResourceManager.ApplyResources((object) this._searchFromBeginningRadioButton, "_searchFromBeginningRadioButton");
    this._searchFromBeginningRadioButton.Name = "_searchFromBeginningRadioButton";
    this.toolTips.SetToolTip((Control) this._searchFromBeginningRadioButton, componentResourceManager.GetString("_searchFromBeginningRadioButton.ToolTip"));
    this._searchFromBeginningRadioButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._searchFromCurrentPositionRadioButton, "_searchFromCurrentPositionRadioButton");
    this._searchFromCurrentPositionRadioButton.Checked = true;
    this._searchFromCurrentPositionRadioButton.Name = "_searchFromCurrentPositionRadioButton";
    this._searchFromCurrentPositionRadioButton.TabStop = true;
    this.toolTips.SetToolTip((Control) this._searchFromCurrentPositionRadioButton, componentResourceManager.GetString("_searchFromCurrentPositionRadioButton.ToolTip"));
    this._searchFromCurrentPositionRadioButton.UseVisualStyleBackColor = true;
    this.gbDirection.Controls.Add((Control) this._searchBackwardRadioButton);
    this.gbDirection.Controls.Add((Control) this._searchForwardRadioButton);
    this.gbDirection.FlatStyle = FlatStyle.System;
    componentResourceManager.ApplyResources((object) this.gbDirection, "gbDirection");
    this.gbDirection.Name = "gbDirection";
    this.gbDirection.TabStop = false;
    componentResourceManager.ApplyResources((object) this._searchBackwardRadioButton, "_searchBackwardRadioButton");
    this._searchBackwardRadioButton.Name = "_searchBackwardRadioButton";
    this.toolTips.SetToolTip((Control) this._searchBackwardRadioButton, componentResourceManager.GetString("_searchBackwardRadioButton.ToolTip"));
    this._searchBackwardRadioButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._searchForwardRadioButton, "_searchForwardRadioButton");
    this._searchForwardRadioButton.Checked = true;
    this._searchForwardRadioButton.Name = "_searchForwardRadioButton";
    this._searchForwardRadioButton.TabStop = true;
    this.toolTips.SetToolTip((Control) this._searchForwardRadioButton, componentResourceManager.GetString("_searchForwardRadioButton.ToolTip"));
    this._searchForwardRadioButton.UseVisualStyleBackColor = true;
    this._clearSearchHistoryButton.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this._clearSearchHistoryButton, "_clearSearchHistoryButton");
    this._clearSearchHistoryButton.ImageList = this.imageList;
    this._clearSearchHistoryButton.Name = "_clearSearchHistoryButton";
    this.toolTips.SetToolTip((Control) this._clearSearchHistoryButton, componentResourceManager.GetString("_clearSearchHistoryButton.ToolTip"));
    this._clearSearchHistoryButton.Click += new EventHandler(this.ClearSearchHistoryButton_Click);
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Transparent;
    this.imageList.Images.SetKeyName(0, "удалить_11.png");
    this.AcceptButton = (IButtonControl) this._findButton;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this._cancelButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._clearSearchHistoryButton);
    this.Controls.Add((Control) this.gbDirection);
    this.Controls.Add((Control) this.gbFind);
    this.Controls.Add((Control) this.gbOptions);
    this.Controls.Add((Control) this._findWhatComboBox);
    this.Controls.Add((Control) this.labelSearch);
    this.Controls.Add((Control) this.panelBottom);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (NavigatorContextSearchForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.NavigatorContextSearchForm_FormClosed);
    this.Load += new EventHandler(this.NavigatorContextSearchForm_Load);
    this.panelBottom.ResumeLayout(false);
    this.gbOptions.ResumeLayout(false);
    this.gbOptions.PerformLayout();
    this.gbFind.ResumeLayout(false);
    this.gbFind.PerformLayout();
    this.gbDirection.ResumeLayout(false);
    this.gbDirection.PerformLayout();
    this.ResumeLayout(false);
  }

  [Serializable]
  public sealed class NavigatorContextSearchFormMemento
  {
    public string FindWhat { get; set; }

    public string[] SearchHistory { get; set; }

    public bool MatchCase { get; set; }

    public bool MatchWholeWord { get; set; }

    public bool UseSearchMask { get; set; }

    public bool UseRegex { get; set; }

    public bool SearchInCurrentColumnOnly { get; set; }

    public bool SearchFromCurrentPosition { get; set; }

    public bool SearchForward { get; set; }
  }
}
