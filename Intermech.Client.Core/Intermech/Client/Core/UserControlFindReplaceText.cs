
// Type: Intermech.Client.Core.UserControlFindReplaceText
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraEditors.Controls;
using Intermech.Bars;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Client.Core;

/// <summary>
/// UserControl для настройки поиска или поиска с заменой текста
/// </summary>
public class UserControlFindReplaceText : 
  UserControl,
  IFindOrReplaceController,
  IFindOrReplaceTextController,
  IPlaceable
{
  private readonly string constDropDownString = LocalizationHolder.rm.GetString("Client.Core_210");
  private readonly string constDropUpString = LocalizationHolder.rm.GetString("Client.Core_211");
  private static UserControlFindReplaceText.RegularDescription[] _regularDescriptions = new UserControlFindReplaceText.RegularDescription[16 /*0x10*/]
  {
    new UserControlFindReplaceText.RegularDescription(".", 1, string.Empty, string.Empty),
    new UserControlFindReplaceText.RegularDescription("*", 1, "(", ")*"),
    new UserControlFindReplaceText.RegularDescription("+", 1, "(", ")+"),
    new UserControlFindReplaceText.RegularDescription("^", 1, string.Empty, string.Empty),
    new UserControlFindReplaceText.RegularDescription("$", 1, string.Empty, string.Empty),
    new UserControlFindReplaceText.RegularDescription("<", 1, string.Empty, string.Empty),
    new UserControlFindReplaceText.RegularDescription(">", 1, string.Empty, string.Empty),
    new UserControlFindReplaceText.RegularDescription("\\n", 2, string.Empty, string.Empty),
    new UserControlFindReplaceText.RegularDescription("[]", 1, "[", "]"),
    new UserControlFindReplaceText.RegularDescription("[^]", 2, "[^", "]"),
    new UserControlFindReplaceText.RegularDescription("|", 1, string.Empty, string.Empty),
    new UserControlFindReplaceText.RegularDescription("\\", 1, string.Empty, string.Empty),
    new UserControlFindReplaceText.RegularDescription("{}", 1, "{", "}"),
    new UserControlFindReplaceText.RegularDescription(":q", 2, string.Empty, string.Empty),
    new UserControlFindReplaceText.RegularDescription(":t ", 2, string.Empty, string.Empty),
    new UserControlFindReplaceText.RegularDescription(":z", 2, string.Empty, string.Empty)
  };
  protected int _oldBootomDif = -1;
  protected int _bottomGroupsHeight = -1;
  private bool _isReplaceModeLoaded;
  private bool _isExpandedLoaded;
  private bool _isReplaceMode = true;
  private bool _isExpanded = true;
  private int _minimunHeight = -1;
  private int _replaceTextHeight = -1;
  private string[] _findWhatHistroy = new string[50];
  private int _findWhatHistroyIndex;
  private string[] _replaceWhatHistroy = new string[50];
  private int _replaceWhatHistroyIndex;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private MenuBar _contextMenuBar;
  private ContextMenuBarItem _contextMenuBarItem;
  private MenuButtonItem _menuButtonItem1;
  private MenuButtonItem _menuButtonItem2;
  private MenuButtonItem _menuButtonItem3;
  private MenuButtonItem _menuButtonItem4;
  private MenuButtonItem _menuButtonItem5;
  private MenuButtonItem _menuButtonItem6;
  private MenuButtonItem _menuButtonItem7;
  private MenuButtonItem _menuButtonItem8;
  private MenuButtonItem _menuButtonItem9;
  private MenuButtonItem _menuButtonItem10;
  private MenuButtonItem _menuButtonItem11;
  private MenuButtonItem _menuButtonItem12;
  private MenuButtonItem _menuButtonItem13;
  private MenuButtonItem _menuButtonItem14;
  private MenuButtonItem _menuButtonItem15;
  private MenuButtonItem _menuButtonItem16;
  private MenuButtonItem _menuButtonItem17;
  private Label _labelFindWhere;
  private Label _labelFindWhat;
  private Label _labelBoxReplaceWith;
  private ImageList _imageList;
  protected GroupBox _groupBoxFindOptions;
  protected CheckBox _checkBoxRegularExpressions;
  protected CheckBox _checkBoxWholeWord;
  protected Label _labelComboBoxWhereToFind;
  protected CheckBox _checkBoxMathCase;
  protected Button _btnFindNext;
  protected Button _btnClose;
  protected Button _btnShowMore;
  protected Button _btnSelectExpression;
  protected Button _btnReplaceAll;
  protected Button _btnReplace;
  internal ComboBox _comboBoxFindText;
  internal ComboBox _comboBoxFindWhere;
  internal ComboBox _comboBoxReplaceWith;
  internal ComboBox _comboBoxSearchDirrection;

  public UserControlFindReplaceText()
  {
    this.InitializeComponent();
    this.UpdatePositions();
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
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

  /// <summary> Добавить параметр "Заменить на" в историю соотв. контрола </summary>
  private void AddToFindWhatHistory()
  {
    if (Array.IndexOf<string>(this._findWhatHistroy, this.FindWhat) != -1)
      return;
    this._findWhatHistroy[this._findWhatHistroyIndex] = this.FindWhat;
    ++this._findWhatHistroyIndex;
    if (this._findWhatHistroyIndex >= 50)
      this._findWhatHistroyIndex = 0;
    this._comboBoxFindText.Items.Insert(0, (object) this.FindWhat);
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
    this._comboBoxReplaceWith.Items.Insert(0, (object) this.ReplaceWith);
    if (this._comboBoxReplaceWith.Items.Count <= 50)
      return;
    this._comboBoxReplaceWith.Items.RemoveAt(this._comboBoxReplaceWith.Items.Count - 1);
  }

  /// <summary> Востановление истории </summary>
  private void RestoreHistory()
  {
    int index1 = this._findWhatHistroyIndex - 1;
    if (index1 < 0)
      index1 = 49;
    int num1 = index1;
    this._comboBoxFindText.Items.Clear();
    while (this._findWhatHistroy[index1] != null)
    {
      this._comboBoxFindText.Items.Add((object) this._findWhatHistroy[index1]);
      --index1;
      if (index1 < 0)
        index1 = 49;
      if (index1 == num1)
        break;
    }
    int index2 = this._replaceWhatHistroyIndex - 1;
    if (index2 < 0)
      index2 = 49;
    int num2 = index2;
    this._comboBoxReplaceWith.Items.Clear();
    while (this._replaceWhatHistroy[index2] != null)
    {
      this._comboBoxReplaceWith.Items.Add((object) this._replaceWhatHistroy[index2]);
      --index2;
      if (index2 < 0)
        index2 = 49;
      if (index2 == num2)
        break;
    }
  }

  /// <summary> После того, как свойство Parent было изменено </summary>
  protected override void OnParentChanged(EventArgs e)
  {
    if (this.ParentForm == null || this.DesignMode)
      return;
    if (this._isExpandedLoaded != this._isExpanded)
      this.IsExpanded = this._isExpandedLoaded;
    if (this._isReplaceModeLoaded == this._isReplaceMode)
      return;
    this.IsReplaceMode = this._isReplaceModeLoaded;
  }

  protected override void OnVisibleChanged(EventArgs e)
  {
    base.OnVisibleChanged(e);
    if (!this.Visible)
      return;
    this._comboBoxFindText.Focus();
    if (this.ParentForm == null)
      return;
    this.ParentForm.CancelButton = (IButtonControl) this._btnClose;
    this.ParentForm.AcceptButton = (IButtonControl) this._btnFindNext;
    this.ParentForm.ActiveControl = (Control) this._comboBoxFindText;
  }

  protected override void OnPaint(PaintEventArgs e)
  {
  }

  /// <summary> Признака видимости GroupBox-ов </summary>
  /// <param name="visible"> Признак видимости GroupBox-ов </param>
  protected virtual void SetGroupBoxesVisible(bool visible)
  {
    this._groupBoxFindOptions.Enabled = visible;
    this._groupBoxFindOptions.Anchor = visible ? AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left : AnchorStyles.Top | AnchorStyles.Left;
    this._groupBoxFindOptions.Visible = visible;
  }

  /// <summary> Установка высоты GroupBox-ов </summary>
  /// <param name="bottomGroupsHeight"> Высота GroupBox-ов </param>
  protected virtual void SetGroupBoxesHeight(int bottomGroupsHeight)
  {
    this._groupBoxFindOptions.Height = bottomGroupsHeight;
  }

  /// <summary> Сохранить выбранные пользователем настройки поиска для последующего востановления </summary>
  /// <param name="iConfiguration"> Интерфейс позволяющий сохранять / читать конфигурацию </param>
  public virtual void SaveConfiguration(IConfiguration iConfiguration)
  {
    iConfiguration.SetProperty("FindWhat", this.FindWhat);
    iConfiguration.SetProperty("ReplaceWith", this.ReplaceWith);
    iConfiguration.SetProperty("SelectedSearchPlace", this.SelectedSearchPlace.ToString());
    iConfiguration.SetProperty("IsExpanded", this.IsExpanded.ToString());
    iConfiguration.SetProperty("SearchDirrection", ((int) this.SearchDirrection).ToString());
    iConfiguration.SetProperty("MatchCase", this.MatchCase.ToString());
    iConfiguration.SetProperty("MatchWholeWord", this.MatchWholeWord.ToString());
    iConfiguration.SetProperty("UseRegularExpressions", this.UseRegularExpressions.ToString());
    string empty = string.Empty;
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) this._findWhatHistroy);
      string base64String = Convert.ToBase64String(serializationStream.ToArray());
      iConfiguration.SetProperty("FindWhatHistroy", base64String);
    }
    iConfiguration.SetProperty("FindWhatHistroyIndex", this._findWhatHistroyIndex.ToString());
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) this._replaceWhatHistroy);
      string base64String = Convert.ToBase64String(serializationStream.ToArray());
      iConfiguration.SetProperty("ReplaceWhatHistroy", base64String);
    }
    iConfiguration.SetProperty("ReplaceWhatHistroyIndex", this._replaceWhatHistroyIndex.ToString());
  }

  /// <summary> Востановление настроек поиска из ранее сохнанённых </summary>
  /// <param name="iConfiguration"> Интерфейс позволяющий сохранять / читать конфигурацию </param>
  public virtual void LoadConfiguration(IConfiguration iConfiguration)
  {
    string empty1 = string.Empty;
    if (iConfiguration.HasProperty("FindWhat"))
    {
      string property = iConfiguration.GetProperty("FindWhat");
      if (property != string.Empty)
        this.FindWhat = property;
    }
    if (iConfiguration.HasProperty("ReplaceWith"))
    {
      string property = iConfiguration.GetProperty("ReplaceWith");
      if (property != string.Empty)
        this.ReplaceWith = property;
    }
    if (iConfiguration.HasProperty("SelectedSearchPlace"))
    {
      string property = iConfiguration.GetProperty("SelectedSearchPlace");
      if (property != string.Empty)
        this.SelectedSearchPlace = Convert.ToInt32(property);
    }
    if (iConfiguration.HasProperty("IsExpanded"))
    {
      string property = iConfiguration.GetProperty("IsExpanded");
      if (property != string.Empty)
        this.IsExpanded = Convert.ToBoolean(property);
    }
    if (iConfiguration.HasProperty("SearchDirrection"))
    {
      string property = iConfiguration.GetProperty("SearchDirrection");
      if (property != string.Empty)
        this.SearchDirrection = (SearchDirrection) Convert.ToInt32(property);
    }
    if (iConfiguration.HasProperty("MatchCase"))
    {
      string property = iConfiguration.GetProperty("MatchCase");
      if (property != string.Empty)
        this.MatchCase = Convert.ToBoolean(property);
    }
    if (iConfiguration.HasProperty("MatchWholeWord"))
    {
      string property = iConfiguration.GetProperty("MatchWholeWord");
      if (property != string.Empty)
        this.MatchWholeWord = Convert.ToBoolean(property);
    }
    if (iConfiguration.HasProperty("UseRegularExpressions"))
    {
      string property = iConfiguration.GetProperty("UseRegularExpressions");
      if (property != string.Empty)
        this.UseRegularExpressions = Convert.ToBoolean(property);
    }
    string empty2 = string.Empty;
    if (iConfiguration.HasProperty("FindWhatHistroy"))
    {
      using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(iConfiguration.GetProperty("FindWhatHistroy"))))
      {
        object obj = new BinaryFormatter().Deserialize((Stream) serializationStream);
        if (obj is Array)
          this._findWhatHistroy = (string[]) (obj as Array);
      }
    }
    if (iConfiguration.HasProperty("FindWhatHistroyIndex"))
    {
      string property = iConfiguration.GetProperty("FindWhatHistroyIndex");
      if (property != string.Empty)
        this._findWhatHistroyIndex = Convert.ToInt32(property);
    }
    if (iConfiguration.HasProperty("ReplaceWhatHistroy"))
    {
      using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(iConfiguration.GetProperty("ReplaceWhatHistroy"))))
      {
        object obj = new BinaryFormatter().Deserialize((Stream) serializationStream);
        if (obj is Array)
          this._replaceWhatHistroy = (string[]) (obj as Array);
      }
    }
    if (iConfiguration.HasProperty("ReplaceWhatHistroyIndex"))
    {
      string property = iConfiguration.GetProperty("ReplaceWhatHistroyIndex");
      if (property != string.Empty)
        this._replaceWhatHistroyIndex = Convert.ToInt32(property);
    }
    this.RestoreHistory();
  }

  /// <summary> Если true, то производиться поиск с заменой, если false, то производиться простой поиск текста </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [CustomDescription("Attribute.Client.Core_29")]
  public bool IsReplaceMode
  {
    get => this._isReplaceMode;
    set
    {
      if (value != this._isReplaceMode && this.ParentForm != null)
      {
        this._isReplaceMode = value;
        this.UpdatePositions();
      }
      this._isReplaceModeLoaded = value;
    }
  }

  public virtual void UpdatePositions()
  {
    int num1 = 16 /*0x10*/;
    if (this.ParentForm == null)
    {
      this._groupBoxFindOptions.Height = this.Height - num1 - this._groupBoxFindOptions.Top;
    }
    else
    {
      if (this._replaceTextHeight == -1)
      {
        Point location = this._labelBoxReplaceWith.Location;
        int y1 = location.Y;
        location = this._labelFindWhat.Location;
        int y2 = location.Y;
        this._replaceTextHeight = y1 - y2;
      }
      if (this._minimunHeight == -1)
        this._minimunHeight = this._btnFindNext.Bottom - this._replaceTextHeight + 4;
      if (this._oldBootomDif == -1)
        this._oldBootomDif = this.ParentForm.Height - this.Height;
      if (this._bottomGroupsHeight == -1)
        this._bottomGroupsHeight = this.Height - num1 * 2 - this._groupBoxFindOptions.Top;
      this._labelFindWhat.Top = num1;
      this._comboBoxFindText.Top = this._labelFindWhat.Bottom + num1;
      this._comboBoxFindText.Left = num1;
      this._labelBoxReplaceWith.Top = this._comboBoxFindText.Bottom + num1;
      this._comboBoxReplaceWith.Top = this._labelBoxReplaceWith.Bottom + num1;
      this._comboBoxReplaceWith.Left = num1;
      this._comboBoxReplaceWith.Width = this.Width - num1;
      if (this.IsReplaceMode)
      {
        this._labelBoxReplaceWith.Visible = true;
        this._comboBoxReplaceWith.Visible = true;
        this._btnReplace.Enabled = true;
        this._btnReplace.Visible = true;
        this._btnReplaceAll.Enabled = true;
        this._btnReplaceAll.Visible = true;
        this._labelFindWhere.Top = this._comboBoxReplaceWith.Bottom + num1;
      }
      else
      {
        this._labelBoxReplaceWith.Visible = false;
        this._comboBoxReplaceWith.Visible = false;
        this._labelFindWhere.Top = this._comboBoxFindText.Bottom + num1;
        this._btnReplace.Enabled = false;
        this._btnReplace.Visible = false;
        this._btnReplaceAll.Enabled = false;
        this._btnReplaceAll.Visible = false;
      }
      this._groupBoxFindOptions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      if (this.IsExpanded)
        this._groupBoxFindOptions.Visible = true;
      else
        this._groupBoxFindOptions.Visible = false;
      this._comboBoxFindWhere.Left = num1;
      this._comboBoxFindWhere.Top = this._labelFindWhere.Bottom + num1;
      this._comboBoxFindWhere.Width = this.Width - num1;
      this._btnClose.Left = this.Width - this._btnClose.Width - num1;
      this._btnFindNext.Left = this._btnClose.Left - this._btnFindNext.Width - num1;
      this._btnReplaceAll.Left = this._btnFindNext.Left - this._btnReplaceAll.Width - num1;
      this._btnReplace.Left = this._btnReplaceAll.Left - this._btnReplace.Width - num1;
      this._btnFindNext.Top = this._comboBoxFindWhere.Bottom + num1;
      this._btnClose.Top = this._btnFindNext.Top;
      this._btnReplace.Top = this._btnFindNext.Top;
      this._btnReplaceAll.Top = this._btnFindNext.Top;
      this._btnShowMore.Top = this._btnFindNext.Top;
      if (this.IsReplaceMode)
        this._btnShowMore.Left = this._btnReplace.Left - this._btnShowMore.Width - 5;
      else
        this._btnShowMore.Left = this._btnFindNext.Left - this._btnShowMore.Width - 5;
      this._groupBoxFindOptions.Top = this._btnShowMore.Bottom + num1;
      this._groupBoxFindOptions.Height = this._bottomGroupsHeight;
      this._btnSelectExpression.Top = this._comboBoxFindText.Top;
      this._btnSelectExpression.Left = this._comboBoxReplaceWith.Right - this._btnSelectExpression.Width;
      this._comboBoxFindText.Width = this._btnSelectExpression.Left - num1 - this._comboBoxFindText.Left;
      int num2;
      if (this.IsExpanded)
      {
        this._groupBoxFindOptions.Height = this._bottomGroupsHeight;
        num2 = this._groupBoxFindOptions.Bottom + num1;
      }
      else
        num2 = this._btnShowMore.Bottom + num1;
      float num3 = 0.0f;
      using (Graphics graphics = Graphics.FromHwnd(this.Handle))
        num3 = graphics.DpiX;
      if ((double) num3 > 100.0)
        this.ParentForm.Height = num2 + this._oldBootomDif * 2 + num1 * 2;
      else
        this.ParentForm.Height = num2 + this._oldBootomDif + num1 * 2;
      this._groupBoxFindOptions.Height = this.Height - num1 * 2 - this._groupBoxFindOptions.Top;
    }
  }

  protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

  /// <summary> Признак того, что используется расширеная форма настройки поиска (с доп. параметрами поиска) </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [CustomDescription("Attribute.Client.Core_30")]
  public bool IsExpanded
  {
    get => this._isExpanded;
    set
    {
      if (value != this.IsExpanded && this.ParentForm != null)
      {
        this._btnShowMore.Text = value ? this.constDropUpString : this.constDropDownString;
        this._btnShowMore.ImageIndex = value ? 1 : 0;
        this._isExpanded = value;
        this.UpdatePositions();
      }
      this._isExpandedLoaded = value;
    }
  }

  /// <summary> Строка поиска </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [CustomDescription("Attribute.Client.Core_31")]
  public string FindWhat
  {
    get => this._comboBoxFindText.Text;
    set => this._comboBoxFindText.Text = value;
  }

  /// <summary> На что требуется заменять найденый текст </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [CustomDescription("Attribute.Client.Core_32")]
  public string ReplaceWith
  {
    get => !this.IsReplaceMode ? string.Empty : this._comboBoxReplaceWith.Text;
    set => this._comboBoxReplaceWith.Text = value;
  }

  /// <summary> Список доступных мест для поиска текста (например, поиск в [текущем документе], [на текущей странице] и т.п.) </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [CustomDescription("Attribute.Client.Core_33")]
  public string[] PossibleSearchPlaces
  {
    get
    {
      string[] possibleSearchPlaces = new string[this._comboBoxFindWhere.Items.Count];
      int index = 0;
      this._comboBoxFindWhere.SelectedIndex = -1;
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

  /// <summary> Индекс выбраного места для поиска в PossibleSearchPlaces </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [CustomDescription("Attribute.Client.Core_34")]
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
  [CustomDescription("Attribute.Client.Core_35")]
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
  [CustomDescription("Attribute.Client.Core_36")]
  public bool MatchCase
  {
    get => this._checkBoxMathCase.Checked;
    set => this._checkBoxMathCase.Checked = value;
  }

  /// <summary> Признак того, что ищется слово "целиком" </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [CustomDescription("Attribute.Client.Core_37")]
  public bool MatchWholeWord
  {
    get => this._checkBoxWholeWord.Checked;
    set => this._checkBoxWholeWord.Checked = value;
  }

  /// <summary> Признак того, что при поиске должны быть использованы регулярные выражения </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [CustomDescription("Attribute.Client.Core_38")]
  public bool UseRegularExpressions
  {
    get => this._checkBoxRegularExpressions.Checked;
    set => this._checkBoxRegularExpressions.Checked = value;
  }

  public virtual void PlaceControls()
  {
    if (this.ParentForm == null || this.DesignMode)
      return;
    if (this._isExpandedLoaded != this._isExpanded)
      this.IsExpanded = this._isExpandedLoaded;
    if (this._isReplaceModeLoaded == this._isReplaceMode)
      return;
    this.IsReplaceMode = this._isReplaceModeLoaded;
  }

  /// <summary> Был переключён режим развёрнутого / свёрнутого вида диалога </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _btnShowMore_Click(object sender, EventArgs e) => this.IsExpanded = !this.IsExpanded;

  /// <summary> Был включён / выключен режим использования регулярных выражений </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _checkBoxRegularExpressions_CheckedChanged(object sender, EventArgs e)
  {
    this._btnSelectExpression.Enabled = this._checkBoxRegularExpressions.Checked;
  }

  /// <summary> Была нажата кнопка вызова меню регулярных выражений </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _btnSelectExpression_Click(object sender, EventArgs e)
  {
    Point mousePosition = Control.MousePosition;
    this._contextMenuBarItem.Show((Control) this._btnSelectExpression, new Point(this._btnSelectExpression.Width, 0));
  }

  /// <summary> Было выбрано регулярное выражение из списка </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _menuButtonItemRegularExpression_Click(object sender, EventArgs e)
  {
    if (sender == null || !(sender is MenuButtonItem))
      return;
    MenuButtonItem menuButtonItem = (MenuButtonItem) sender;
    if (menuButtonItem.Index < 0 || menuButtonItem.Index >= UserControlFindReplaceText._regularDescriptions.Length)
      return;
    UserControlFindReplaceText.RegularDescription regularDescription = UserControlFindReplaceText._regularDescriptions[menuButtonItem.Index];
    string text = this._comboBoxFindText.Text;
    int selectionStart = this._comboBoxFindText.SelectionStart;
    string str1 = selectionStart == 0 ? string.Empty : text.Substring(0, selectionStart);
    string str2 = selectionStart + this._comboBoxFindText.SelectionLength >= text.Length ? string.Empty : text.Substring(selectionStart + this._comboBoxFindText.SelectionLength, text.Length - (selectionStart + this._comboBoxFindText.SelectionLength));
    string str3 = regularDescription.SimpleString;
    if (this._comboBoxFindText.SelectionLength > 0 && regularDescription.SelectedTextBeforeStr != string.Empty)
    {
      string str4 = this._comboBoxFindText.SelectionLength == 0 ? string.Empty : text.Substring(selectionStart, this._comboBoxFindText.SelectionLength);
      str3 = regularDescription.SelectedTextBeforeStr + str4 + regularDescription.SelectedTextAfterStr;
    }
    string str5 = str3;
    string str6 = str2;
    string str7 = str1 + str5 + str6;
    this._comboBoxFindText.SelectionLength = 0;
    this._comboBoxFindText.Text = str7;
    this._comboBoxFindText.SelectionStart = selectionStart + regularDescription.SimpleCaretPos;
    this.ActiveControl = (Control) this._comboBoxFindText;
    this._comboBoxFindText.SelectionStart = selectionStart + regularDescription.SimpleCaretPos;
    this._comboBoxFindText.SelectionLength = 0;
    this._comboBoxFindText.SelectionStart = selectionStart + regularDescription.SimpleCaretPos;
  }

  /// <summary> Была нажата кнопка "Закрыть" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _btnClose_Click(object sender, EventArgs e)
  {
    if (this.ParentForm == null)
      return;
    FindOrReplaceService.IsFindWindowVisible = false;
    if (this.ParentForm == null)
      return;
    this.ParentForm.Close();
  }

  /// <summary> Была нажата кнопка "Найти далее" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _btnFindNext_Click(object sender, EventArgs e)
  {
    this.AddToFindWhatHistory();
    FindOrReplaceService.CallFindNext();
  }

  /// <summary> Была нажата кнопка "Заменть" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _btnReplace_Click(object sender, EventArgs e)
  {
    this.AddToFindReplaceHistory();
    FindOrReplaceService.CallReplace();
  }

  /// <summary> Была нажата кнопка "Заменть все" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _btnReplaceAll_Click(object sender, EventArgs e)
  {
    this.AddToFindReplaceHistory();
    FindOrReplaceService.CallReplaceAll();
  }

  private void _comboBoxFindWhere_SelectedIndexChanged(object sender, EventArgs e)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlFindReplaceText));
    this._contextMenuBar = new MenuBar();
    this._contextMenuBarItem = new ContextMenuBarItem();
    this._menuButtonItem1 = new MenuButtonItem();
    this._menuButtonItem2 = new MenuButtonItem();
    this._menuButtonItem3 = new MenuButtonItem();
    this._menuButtonItem4 = new MenuButtonItem();
    this._menuButtonItem5 = new MenuButtonItem();
    this._menuButtonItem6 = new MenuButtonItem();
    this._menuButtonItem7 = new MenuButtonItem();
    this._menuButtonItem8 = new MenuButtonItem();
    this._menuButtonItem9 = new MenuButtonItem();
    this._menuButtonItem10 = new MenuButtonItem();
    this._menuButtonItem11 = new MenuButtonItem();
    this._menuButtonItem12 = new MenuButtonItem();
    this._menuButtonItem13 = new MenuButtonItem();
    this._menuButtonItem14 = new MenuButtonItem();
    this._menuButtonItem15 = new MenuButtonItem();
    this._menuButtonItem16 = new MenuButtonItem();
    this._menuButtonItem17 = new MenuButtonItem();
    this._groupBoxFindOptions = new GroupBox();
    this._comboBoxSearchDirrection = new ComboBox();
    this._checkBoxRegularExpressions = new CheckBox();
    this._checkBoxWholeWord = new CheckBox();
    this._labelComboBoxWhereToFind = new Label();
    this._checkBoxMathCase = new CheckBox();
    this._btnFindNext = new Button();
    this._btnClose = new Button();
    this._btnShowMore = new Button();
    this._imageList = new ImageList();
    this._labelFindWhere = new Label();
    this._btnSelectExpression = new Button();
    this._labelFindWhat = new Label();
    this._labelBoxReplaceWith = new Label();
    this._btnReplaceAll = new Button();
    this._btnReplace = new Button();
    this._comboBoxFindWhere = new ComboBox();
    this._comboBoxReplaceWith = new ComboBox();
    this._comboBoxFindText = new ComboBox();
    this._groupBoxFindOptions.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._contextMenuBar, "_contextMenuBar");
    this._contextMenuBar.Guid = new Guid("5a561fc6-ae3a-4e84-8db4-1f56071bfffb");
    this._contextMenuBar.Hidden = false;
    this._contextMenuBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this._contextMenuBarItem
    });
    this._contextMenuBar.Name = "_contextMenuBar";
    this._contextMenuBar.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this._contextMenuBarItem, "_contextMenuBarItem");
    this._contextMenuBarItem.Items.AddRange(new ToolbarItemBase[17]
    {
      (ToolbarItemBase) this._menuButtonItem1,
      (ToolbarItemBase) this._menuButtonItem2,
      (ToolbarItemBase) this._menuButtonItem3,
      (ToolbarItemBase) this._menuButtonItem4,
      (ToolbarItemBase) this._menuButtonItem5,
      (ToolbarItemBase) this._menuButtonItem6,
      (ToolbarItemBase) this._menuButtonItem7,
      (ToolbarItemBase) this._menuButtonItem8,
      (ToolbarItemBase) this._menuButtonItem9,
      (ToolbarItemBase) this._menuButtonItem10,
      (ToolbarItemBase) this._menuButtonItem11,
      (ToolbarItemBase) this._menuButtonItem12,
      (ToolbarItemBase) this._menuButtonItem13,
      (ToolbarItemBase) this._menuButtonItem14,
      (ToolbarItemBase) this._menuButtonItem15,
      (ToolbarItemBase) this._menuButtonItem16,
      (ToolbarItemBase) this._menuButtonItem17
    });
    this._contextMenuBarItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this._menuButtonItem1, "_menuButtonItem1");
    this._menuButtonItem1.ShowText = true;
    this._menuButtonItem1.Tag = (object) ".";
    this._menuButtonItem1.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem2, "_menuButtonItem2");
    this._menuButtonItem2.ShowText = true;
    this._menuButtonItem2.Tag = (object) "*";
    this._menuButtonItem2.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem3, "_menuButtonItem3");
    this._menuButtonItem3.ShowText = true;
    this._menuButtonItem3.Tag = (object) "+";
    this._menuButtonItem3.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    this._menuButtonItem4.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._menuButtonItem4, "_menuButtonItem4");
    this._menuButtonItem4.ShowText = true;
    this._menuButtonItem4.Tag = (object) "^";
    this._menuButtonItem4.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem5, "_menuButtonItem5");
    this._menuButtonItem5.ShowText = true;
    this._menuButtonItem5.Tag = (object) "$";
    this._menuButtonItem5.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem6, "_menuButtonItem6");
    this._menuButtonItem6.ShowText = true;
    this._menuButtonItem6.Tag = (object) "<";
    this._menuButtonItem6.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem7, "_menuButtonItem7");
    this._menuButtonItem7.ShowText = true;
    this._menuButtonItem7.Tag = (object) ">";
    this._menuButtonItem7.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem8, "_menuButtonItem8");
    this._menuButtonItem8.ShowText = true;
    this._menuButtonItem8.Tag = (object) "\\n";
    this._menuButtonItem8.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    this._menuButtonItem9.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._menuButtonItem9, "_menuButtonItem9");
    this._menuButtonItem9.ShowText = true;
    this._menuButtonItem9.Tag = (object) "[<caret/>]";
    this._menuButtonItem9.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem10, "_menuButtonItem10");
    this._menuButtonItem10.ShowText = true;
    this._menuButtonItem10.Tag = (object) "[^<caret/>]";
    this._menuButtonItem10.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem11, "_menuButtonItem11");
    this._menuButtonItem11.ShowText = true;
    this._menuButtonItem11.Tag = (object) "|";
    this._menuButtonItem11.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem12, "_menuButtonItem12");
    this._menuButtonItem12.ShowText = true;
    this._menuButtonItem12.Tag = (object) "\\";
    this._menuButtonItem12.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem13, "_menuButtonItem13");
    this._menuButtonItem13.ShowText = true;
    this._menuButtonItem13.Tag = (object) "{<caret/>}";
    this._menuButtonItem13.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    this._menuButtonItem14.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._menuButtonItem14, "_menuButtonItem14");
    this._menuButtonItem14.ShowText = true;
    this._menuButtonItem14.Tag = (object) ":q";
    this._menuButtonItem14.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem15, "_menuButtonItem15");
    this._menuButtonItem15.ShowText = true;
    this._menuButtonItem15.Tag = (object) ":t";
    this._menuButtonItem15.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    componentResourceManager.ApplyResources((object) this._menuButtonItem16, "_menuButtonItem16");
    this._menuButtonItem16.ShowText = true;
    this._menuButtonItem16.Tag = (object) ":z";
    this._menuButtonItem16.Click += new EventHandler(this._menuButtonItemRegularExpression_Click);
    this._menuButtonItem17.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._menuButtonItem17, "_menuButtonItem17");
    this._menuButtonItem17.ShowText = true;
    componentResourceManager.ApplyResources((object) this._groupBoxFindOptions, "_groupBoxFindOptions");
    this._groupBoxFindOptions.Controls.Add((Control) this._comboBoxSearchDirrection);
    this._groupBoxFindOptions.Controls.Add((Control) this._checkBoxRegularExpressions);
    this._groupBoxFindOptions.Controls.Add((Control) this._checkBoxWholeWord);
    this._groupBoxFindOptions.Controls.Add((Control) this._contextMenuBar);
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
    componentResourceManager.ApplyResources((object) this._checkBoxRegularExpressions, "_checkBoxRegularExpressions");
    this._checkBoxRegularExpressions.Name = "_checkBoxRegularExpressions";
    this._checkBoxRegularExpressions.UseVisualStyleBackColor = true;
    this._checkBoxRegularExpressions.CheckedChanged += new EventHandler(this._checkBoxRegularExpressions_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._checkBoxWholeWord, "_checkBoxWholeWord");
    this._checkBoxWholeWord.Name = "_checkBoxWholeWord";
    this._checkBoxWholeWord.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._labelComboBoxWhereToFind, "_labelComboBoxWhereToFind");
    this._labelComboBoxWhereToFind.FlatStyle = FlatStyle.System;
    this._labelComboBoxWhereToFind.Name = "_labelComboBoxWhereToFind";
    componentResourceManager.ApplyResources((object) this._checkBoxMathCase, "_checkBoxMathCase");
    this._checkBoxMathCase.Name = "_checkBoxMathCase";
    this._checkBoxMathCase.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnFindNext, "_btnFindNext");
    this._btnFindNext.Name = "_btnFindNext";
    this._btnFindNext.UseVisualStyleBackColor = true;
    this._btnFindNext.Click += new EventHandler(this._btnFindNext_Click);
    componentResourceManager.ApplyResources((object) this._btnClose, "_btnClose");
    this._btnClose.DialogResult = DialogResult.Cancel;
    this._btnClose.Name = "_btnClose";
    this._btnClose.UseVisualStyleBackColor = true;
    this._btnClose.Click += new EventHandler(this._btnClose_Click);
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
    this._labelFindWhere.FlatStyle = FlatStyle.System;
    this._labelFindWhere.Name = "_labelFindWhere";
    componentResourceManager.ApplyResources((object) this._btnSelectExpression, "_btnSelectExpression");
    this._btnSelectExpression.Name = "_btnSelectExpression";
    this._btnSelectExpression.UseVisualStyleBackColor = true;
    this._btnSelectExpression.Click += new EventHandler(this._btnSelectExpression_Click);
    componentResourceManager.ApplyResources((object) this._labelFindWhat, "_labelFindWhat");
    this._labelFindWhat.FlatStyle = FlatStyle.System;
    this._labelFindWhat.Name = "_labelFindWhat";
    componentResourceManager.ApplyResources((object) this._labelBoxReplaceWith, "_labelBoxReplaceWith");
    this._labelBoxReplaceWith.FlatStyle = FlatStyle.System;
    this._labelBoxReplaceWith.Name = "_labelBoxReplaceWith";
    componentResourceManager.ApplyResources((object) this._btnReplaceAll, "_btnReplaceAll");
    this._btnReplaceAll.Name = "_btnReplaceAll";
    this._btnReplaceAll.UseVisualStyleBackColor = true;
    this._btnReplaceAll.Click += new EventHandler(this._btnReplaceAll_Click);
    componentResourceManager.ApplyResources((object) this._btnReplace, "_btnReplace");
    this._btnReplace.Name = "_btnReplace";
    this._btnReplace.UseVisualStyleBackColor = true;
    this._btnReplace.Click += new EventHandler(this._btnReplace_Click);
    componentResourceManager.ApplyResources((object) this._comboBoxFindWhere, "_comboBoxFindWhere");
    this._comboBoxFindWhere.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxFindWhere.FormattingEnabled = true;
    this._comboBoxFindWhere.Name = "_comboBoxFindWhere";
    componentResourceManager.ApplyResources((object) this._comboBoxReplaceWith, "_comboBoxReplaceWith");
    this._comboBoxReplaceWith.FormattingEnabled = true;
    this._comboBoxReplaceWith.Name = "_comboBoxReplaceWith";
    componentResourceManager.ApplyResources((object) this._comboBoxFindText, "_comboBoxFindText");
    this._comboBoxFindText.FormattingEnabled = true;
    this._comboBoxFindText.Name = "_comboBoxFindText";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Window;
    this.Controls.Add((Control) this._comboBoxFindText);
    this.Controls.Add((Control) this._comboBoxReplaceWith);
    this.Controls.Add((Control) this._comboBoxFindWhere);
    this.Controls.Add((Control) this._btnReplace);
    this.Controls.Add((Control) this._btnReplaceAll);
    this.Controls.Add((Control) this._labelBoxReplaceWith);
    this.Controls.Add((Control) this._groupBoxFindOptions);
    this.Controls.Add((Control) this._btnFindNext);
    this.Controls.Add((Control) this._btnClose);
    this.Controls.Add((Control) this._btnShowMore);
    this.Controls.Add((Control) this._labelFindWhere);
    this.Controls.Add((Control) this._btnSelectExpression);
    this.Controls.Add((Control) this._labelFindWhat);
    this.Name = nameof (UserControlFindReplaceText);
    this.Tag = (object) "  ";
    this._groupBoxFindOptions.ResumeLayout(false);
    this._groupBoxFindOptions.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private class RegularDescription
  {
    public string SimpleString = string.Empty;
    public int SimpleCaretPos;
    public string SelectedTextBeforeStr = string.Empty;
    public string SelectedTextAfterStr = string.Empty;

    public RegularDescription(
      string simpleString,
      int simpleCaretPos,
      string selectedTextBeforeStr,
      string selectedTextAfterStr)
    {
      this.SimpleString = simpleString;
      this.SimpleCaretPos = simpleCaretPos;
      this.SelectedTextBeforeStr = selectedTextBeforeStr;
      this.SelectedTextAfterStr = selectedTextAfterStr;
    }
  }
}
