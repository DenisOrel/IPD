
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrMemoEdit
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls.SpellCheck;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Контрол для редактирования текста.</summary>
public class AttrMemoEdit : AttrsControl, IExpertSystemCtrl
{
  /// <summary>Использование атрибута в экспертной системе</summary>
  private bool _useInExpertSystem;
  private bool _lockTextChanged;
  private bool _textChanged;
  /// <summary>
  /// Признак, что был клик правой клавишей перед открытием контекстного меню
  /// </summary>
  private bool _rightButtonClick;
  /// <summary>Позиция клика правой клавишей мыши</summary>
  private int _position = -1;
  private ControlButton _btnCalc;
  private ControlButton _btnReCalc;
  /// <summary>Переменная хранит значение текста</summary>
  private string _currText = string.Empty;
  /// <summary>Значение атрибута</summary>
  private string _viewValue = string.Empty;
  private int oldCursorPos = -1;
  /// <summary>Описание атрибута</summary>
  private string _description = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private AnyLinkRichTextBox _txt;
  private ContextMenuStrip _cm;
  private ToolStripMenuItem _cmiUndo;
  private ToolStripSeparator toolStripSeparator3;
  private ToolStripMenuItem _cmiCut;
  private ToolStripMenuItem _cmiCopy;
  private ToolStripMenuItem _cmiPaste;
  private ToolStripMenuItem _cmiDelete;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripMenuItem _cmiSelectAll;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripMenuItem _cmiPasteSymbol;
  private ToolStripMenuItem _cmiEditSymbol;
  private ToolStripMenuItem _cmiDelSymbol;
  private ToolStripMenuItem _cmiAddToDictionary;

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int LockWindowUpdate(IntPtr hWndLock);

  /// <summary>Цвет фона элемента управления.</summary>
  [DefaultValue(typeof (Color), "Window")]
  public new Color BackColor
  {
    get => this._txt.BackColor;
    set => this._txt.BackColor = value;
  }

  /// <summary>Вид обрамления для элемента управления.</summary>
  [DefaultValue(BorderStyle.Fixed3D)]
  public new BorderStyle BorderStyle
  {
    get => this._txt.BorderStyle;
    set => this._txt.BorderStyle = value;
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => this._txt.Font;
    set => this._txt.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [DefaultValue(typeof (Color), "WindowText")]
  public new Color ForeColor
  {
    get => this._txt.ForeColor;
    set => this._txt.ForeColor = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this._toolTip.GetToolTip((Control) this._txt);
    set => this._toolTip.SetToolTip((Control) this._txt, value);
  }

  /// <summary>Текст, связанный с элементом управления.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public override string Text
  {
    get => this._txt.Text;
    set
    {
      this._txt.Text = string.IsNullOrEmpty(this._designText) || !string.IsNullOrEmpty(value) ? value : this._designText;
    }
  }

  /// <summary>Детектировать ссылки</summary>
  [DefaultValue(false)]
  public bool DetectUrls
  {
    get => this._txt.DetectUrls;
    set => this._txt.DetectUrls = value;
  }

  /// <summary>Отступы от краев в элементе управления.</summary>
  /// <remarks>Здесь нужно только для того, чтобы запреить сериализацию</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Padding Padding
  {
    get => base.Padding;
    private set => base.Padding = value;
  }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  protected override object[] GetValues
  {
    get
    {
      string p = this._bNeedDescription ? this._viewValue : this._txt.Text;
      return !string.IsNullOrEmpty(p) ? new object[1]
      {
        (object) this.DropHiddenSymbols(p)
      } : new object[1]{ (object) DBNull.Value };
    }
  }

  /// <summary>Позиция курсора.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int CursorPosition
  {
    get => this._txt.SelectionStart;
    set
    {
      if (value <= -1)
        return;
      this._txt.SelectionStart = value;
    }
  }

  /// <summary>Конструктор.</summary>
  public AttrMemoEdit()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this._txt.GotFocus += new EventHandler(this.On_txt_GotFocus);
    this._txt.LostFocus += new EventHandler(this.On_txt_LostFocus);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cm_Opening(object sender, CancelEventArgs e)
  {
    if (this._enabled)
    {
      this._cmiUndo.Enabled = this._txt.Text != this._currText;
      this._cmiCut.Enabled = this._cmiCopy.Enabled = this._cmiDelete.Enabled = this._txt.SelectionLength > 0;
      this._cmiPaste.Enabled = Clipboard.ContainsText();
      int startIndex = -1;
      int finishIndex = -1;
      int nPos = this._txt.SelectionStart;
      if (this._rightButtonClick)
      {
        this._rightButtonClick = false;
        nPos = this._position;
      }
      else
        this._position = nPos;
      this._cmiEditSymbol.Enabled = this._cmiDelSymbol.Enabled = !string.IsNullOrEmpty(this.GetSymbol(nPos, ref startIndex, ref finishIndex));
      this._cmiPasteSymbol.Enabled = string.IsNullOrEmpty(this.GetSymbol(nPos, ref startIndex, ref finishIndex));
      this._cmiAddToDictionary.Visible = this._txt.SelectionColor == Color.Red;
    }
    else
    {
      this._cmiUndo.Enabled = false;
      this._cmiCut.Enabled = this._cmiDelete.Enabled = this._cmiPaste.Enabled = false;
      this._cmiPasteSymbol.Enabled = this._cmiEditSymbol.Enabled = this._cmiDelSymbol.Enabled = false;
      this._cmiCopy.Enabled = !string.IsNullOrEmpty(this._txt.SelectedText);
      this._cmiAddToDictionary.Visible = false;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cm_MenuItem_Click(object sender, EventArgs e)
  {
    string str1;
    switch (Convert.ToInt16((sender as ToolStripMenuItem).Tag))
    {
      case 0:
        this._txt.Text = this._currText;
        this._txt.SelectionStart = this._txt.Text.Length;
        break;
      case 1:
        this._currText = this._txt.Text;
        Clipboard.SetText(this._txt.SelectedText);
        int selectionStart1 = this._txt.SelectionStart;
        this._txt.Text = this._txt.Text.Remove(selectionStart1, this._txt.SelectionLength);
        this._txt.SelectionStart = selectionStart1;
        break;
      case 2:
        Clipboard.SetText(this._txt.SelectedText);
        break;
      case 3:
        this._currText = this._txt.Text;
        string text1 = Clipboard.GetText();
        int selectionStart2 = this._txt.SelectionStart;
        this._txt.Text = this._txt.SelectionLength > 0 ? this._txt.Text.Replace(this._txt.SelectedText, text1) : this._txt.Text.Insert(selectionStart2, text1);
        this._txt.SelectionStart = selectionStart2 + text1.Length;
        break;
      case 4:
        this._currText = this._txt.Text;
        int selectionStart3 = this._txt.SelectionStart;
        this._txt.Text = this._txt.Text.Remove(selectionStart3, this._txt.SelectionLength);
        this._txt.SelectionStart = selectionStart3;
        break;
      case 5:
        this._txt.SelectionStart = 0;
        this._txt.SelectionLength = this._txt.Text.Length;
        break;
      case 6:
      case 7:
        this._currText = this._txt.Text;
        int startIndex1 = -1;
        int finishIndex1 = -1;
        str1 = string.Empty;
        if (!(ServicesManager.GetService(typeof (IIMDocumentEditorService)) is IIMDocumentEditorService service))
          break;
        string symbol = this.GetSymbol(this._position, ref startIndex1, ref finishIndex1);
        if (!service.CallDocumentFormulaEditor(ref symbol) || string.IsNullOrEmpty(symbol))
          break;
        string text2 = this._txt.Text;
        int startIndex2;
        string str2;
        if (startIndex1 > -1 && startIndex1 < finishIndex1)
        {
          string str3 = text2.Remove(startIndex1, finishIndex1 - startIndex1);
          startIndex2 = startIndex1 < this._txt.Text.Length ? startIndex1 : (this._txt.Text.Length > 0 ? this._txt.Text.Length - 1 : 0);
          str2 = str3.Insert(startIndex2, symbol);
        }
        else
        {
          startIndex2 = this._txt.SelectionStart;
          str2 = !string.IsNullOrEmpty(this._txt.SelectedText) ? text2.Replace(this._txt.SelectedText, symbol) : text2.Insert(startIndex2, symbol);
        }
        this._txt.Text = str2;
        this._txt.SelectionStart = startIndex2 + symbol.Length;
        break;
      case 8:
        int startIndex3 = -1;
        int finishIndex2 = -1;
        str1 = this.GetSymbol(this._position, ref startIndex3, ref finishIndex2);
        if (startIndex3 <= -1 || startIndex3 >= finishIndex2)
          break;
        this._txt.Text = this._txt.Text.Remove(startIndex3, finishIndex2 - startIndex3);
        this._txt.SelectionStart = startIndex3 < this._txt.Text.Length ? startIndex3 : (this._txt.Text.Length > 0 ? this._txt.Text.Length - 1 : 0);
        break;
      case 9:
        this.AddToDictionary();
        break;
    }
  }

  private void AddToDictionary()
  {
    int selectionStart = this._txt.SelectionStart;
    int selectionLength = this._txt.SelectionLength;
    this._lockTextChanged = true;
    try
    {
      int num1 = selectionStart;
      while (this._txt.SelectionColor == Color.Red)
      {
        --num1;
        if (num1 >= 0)
        {
          this._txt.SelectionStart = num1;
          this._txt.SelectionLength = selectionStart - num1;
        }
        else
          break;
      }
      int num2 = num1 + 1;
      this._txt.SelectionStart = num2;
      int num3 = selectionStart - num2;
      while (this._txt.SelectionColor == Color.Red)
      {
        ++num3;
        if (num2 + num3 <= this._txt.TextLength)
          this._txt.SelectionLength = num3;
        else
          break;
      }
      this._txt.SelectionLength = num3 - 1;
      SpellChecker.Instance.Dict.UserFileAdd(this._txt.SelectedText);
    }
    finally
    {
      this._txt.SelectionStart = selectionStart;
      this._txt.SelectionLength = selectionLength;
      this._txt.Focus();
      this._lockTextChanged = false;
      this.SpellCheck(false);
    }
  }

  /// <summary>
  /// Режем невидимые символы форматирования, которые могут влиять на внешний вид текста далее при выводе в документы N1509028
  /// </summary>
  /// <param name="p"></param>
  /// <returns></returns>
  private string DropHiddenSymbols(string p)
  {
    p = p.Replace('\u00AD'.ToString(), string.Empty);
    return p;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_expBtn_CalcClick(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this.ParentInfo == null)
      return;
    ExpertSystem.Calculate(this.ParentInfo, this.AttributeInfo.AttributeGuid, this.DesForm);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_expBtn_ReCalcClick(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this.ParentInfo == null)
      return;
    ExpertSystem.ReCalculate(this.ParentInfo.ElementIdentifier, this.AttributeInfo.AttributeGuid, this.DesForm);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_Enter(object sender, EventArgs e)
  {
    if (this._bNeedDescription)
    {
      this._lockTextChanged = true;
      try
      {
        this._txt.Text = this._viewValue;
      }
      finally
      {
        this._lockTextChanged = false;
      }
    }
    this._textChanged = false;
    this.SpellCheck(true);
  }

  /// <summary>Фокусирование текстового контрола.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_GotFocus(object sender, EventArgs e) => this.Error = string.Empty;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_KeyDown(object sender, KeyEventArgs e)
  {
    if (!this.EnabledCtrl)
      return;
    if (e.Control && e.KeyCode == Keys.V && !Clipboard.ContainsText())
    {
      e.Handled = true;
    }
    else
    {
      this._currText = this._txt.Text;
      this.TestEndEnter();
    }
  }

  /// <summary>для проверки окончания ввода текста</summary>
  protected virtual void TestEndEnter()
  {
  }

  /// <summary></summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_LostFocus(object sender, EventArgs e)
  {
    this.Error = !this._disableNulls || !this.EnabledCtrl || !string.IsNullOrEmpty(this._txt.Text) ? string.Empty : this._errMsg_NullValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this._rightButtonClick = true;
    Point location = e.Location;
    int indexFromPosition = this._txt.GetCharIndexFromPosition(location);
    Point positionFromCharIndex = this._txt.GetPositionFromCharIndex(indexFromPosition);
    int num1;
    if (location.X <= positionFromCharIndex.X)
    {
      num1 = indexFromPosition;
    }
    else
    {
      int num2 = num1 = indexFromPosition + 1;
    }
    this._position = num1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_TextChanged(object sender, EventArgs e)
  {
    if (!this.IsDesignMode && !this._lockTextChanged && this.AttributeInfo != null)
    {
      if (this._txt.Focused)
      {
        if (OptimizationSettings.SpellCheck)
          this.SpellCheck(false);
      }
      else if (this._disableNulls && this.EnabledCtrl)
        this.Error = string.IsNullOrEmpty(this._txt.Text) ? this._errMsg_NullValue : string.Empty;
      int num = !string.IsNullOrEmpty(this._viewValue) || !(this._viewValue != this._description) ? (this._viewValue == this._txt.Text ? 1 : 0) : 1;
      this._viewValue = this._txt.Text;
      this.oldCursorPos = this._txt.SelectionStart + this._txt.SelectionLength;
      if (num == 0)
      {
        this.Modified = true;
        if (this.Modified)
          this._textChanged = true;
      }
    }
    if (this.IsDisposed || this.Disposing)
      return;
    this.OnTextChanged(new EventArgs());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _txt_KeyUp(object sender, KeyEventArgs e)
  {
    if (this.IsDisposed || this.Disposing)
      return;
    this.OnKeyUp(e);
  }

  private void On_txt_LinkClicked(object sender, LinkClickedEventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  public override AttributeValues Values
  {
    get => base.Values;
    set
    {
      this._viewValue = this._description = this._currText = string.Empty;
      base.Values = value;
      this._lockTextChanged = true;
      try
      {
        if (this._attrValues != null)
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(value.AttributeGuid);
          if (attributeType != null)
            this._txt.MaxLength = Convert.ToInt32(attributeType.SizeType);
          if (this._bNeedDescription && value.Descriptions != null && value.Descriptions.Length != 0)
            this._description = Convert.ToString(value.Descriptions[0]);
          this._currText = this._viewValue = Convert.ToString(value.Values[0]);
          this.Error = !this._disableNulls || !this.EnabledCtrl || !string.IsNullOrEmpty(this._viewValue) ? string.Empty : this._errMsg_NullValue;
          this._txt.Text = !this._bNeedDescription || this._txt.Focused ? this._viewValue : this._description;
        }
        else
          this._txt.Text = string.Empty;
      }
      finally
      {
        this._lockTextChanged = false;
      }
    }
  }

  /// <summary>Доступность контрола.</summary>
  /// <remarks>Для многострочного поля нужно использовать свойство "ReadOnly", чтобы можно было использовать скроллинг</remarks>
  [DefaultValue(true)]
  public override bool EnabledCtrl
  {
    get => this._enabled;
    set
    {
      this._enabled = value;
      if (!this.IsDesignMode)
      {
        if (this._enabled)
          this._enabled = this._attrValues == null || !(this._attrValues.AttributeGuid == Guid.Empty);
        this._txt.ReadOnly = !this._enabled;
        if (!this._enabled)
        {
          if (this._txt.BackColor.ToArgb() == SystemColors.Window.ToArgb())
            this._txt.BackColor = SystemColors.Control;
        }
        else if (this._txt.BackColor == SystemColors.Control || this._txt.BackColor.ToArgb() == SystemColors.Window.ToArgb())
          this._txt.BackColor = SystemColors.Window;
      }
      this.CheckAccessibilityButtons();
    }
  }

  /// <summary>
  /// Возможность использовать атрибут в экспертной системе.
  /// </summary>
  [DefaultValue(false)]
  public bool UseInExpertSystem
  {
    get => this._useInExpertSystem;
    set
    {
      this._useInExpertSystem = value && ExpertSystem.IsExpertSystemExists();
      if (this._useInExpertSystem)
      {
        if (this._btnCalc == null)
        {
          this._btnCalc = new ControlButton("Calc", 1);
          this._btnReCalc = new ControlButton("ReCalc", 2);
          if (!this.IsDesignMode)
          {
            this._btnCalc.Click += new EventHandler(this.On_expBtn_CalcClick);
            this._btnReCalc.Click += new EventHandler(this.On_expBtn_ReCalcClick);
          }
        }
        this.AddRightButtons(new List<ControlButton>()
        {
          this._btnCalc,
          this._btnReCalc
        });
      }
      else if (this._btnCalc != null)
        this.RemoveRightButtons(new List<ControlButton>()
        {
          this._btnCalc,
          this._btnReCalc
        });
      this.CheckAccessibilityButtons();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLeaveControl(EventArgs e)
  {
    int num1 = AttrMemoEdit.LockWindowUpdate(this._txt.Handle);
    int num2 = 0;
    this._lockTextChanged = true;
    try
    {
      if (this._bNeedDescription)
        this._txt.Text = this._description;
      num2 = this._txt.SelectionStart;
      this._txt.SelectAll();
      this._txt.SelectionColor = this._txt.ForeColor;
    }
    finally
    {
      this._txt.SelectionStart = num2;
      this._txt.SelectionLength = 0;
      if (num1 != 0)
        AttrMemoEdit.LockWindowUpdate(IntPtr.Zero);
      this._lockTextChanged = false;
    }
    base.OnLeaveControl(e);
    if (!this._textChanged)
      return;
    this.OnCompletionOfEditing();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  protected override void SetDesignText(string text)
  {
    base.SetDesignText(text);
    if (this._txt.Text == this._designText)
      this._txt.Text = text;
    this._designText = text;
  }

  /// <summary>
  /// Переопределим данное свойство, т.к. при вызове метода Focus()
  /// фокус на себя забирает _txt и Focused возвращает false.
  /// </summary>
  public override bool Focused
  {
    get
    {
      AnyLinkRichTextBox txt = this._txt;
      return txt == null ? base.Focused : txt.Focused;
    }
  }

  /// <summary>Проверка доступности кнопок.</summary>
  private void CheckAccessibilityButtons()
  {
    if (this._btnCalc == null)
      return;
    if (this.IsDesignMode)
      this._btnCalc.Enabled = this._btnReCalc.Enabled = this.AttributeInfo != null;
    else
      this._btnCalc.Enabled = this._btnReCalc.Enabled = this._attrValues != null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nPos"></param>
  /// <param name="startIndex"></param>
  /// <param name="finishIndex"></param>
  /// <returns></returns>
  private string GetSymbol(int nPos, ref int startIndex, ref int finishIndex)
  {
    string symbol = string.Empty;
    if (!string.IsNullOrEmpty(this._txt.Text))
    {
      startIndex = finishIndex = -1;
      string text = this._txt.Text;
      int startIndex1 = text.LastIndexOf("<<", nPos, nPos + 1);
      if (startIndex1 > -1 && text.LastIndexOf(">>", nPos, nPos) < startIndex1)
      {
        int num1 = text.IndexOf(">>", nPos);
        if (num1 > -1)
        {
          int num2 = text.IndexOf("<<", nPos);
          if (num2 == -1 || num2 > num1)
          {
            int num3 = num1 + 2;
            symbol = text.Substring(startIndex1, num3 - startIndex1);
            startIndex = startIndex1;
            finishIndex = num3;
          }
        }
      }
    }
    return symbol;
  }

  /// <summary>Проверка правописания.</summary>
  private void SpellCheck(bool all)
  {
    if (!OptimizationSettings.SpellCheck || this._txt.Text.Length <= 0)
      return;
    SpellChecker.Instance.WorkInThread = true;
    SpellChecker.Instance.GerErrors(this._txt.Text, this._viewValue, all ? -1 : this._txt.SelectionStart + this._txt.SelectionLength, this.oldCursorPos, new SpellChecker.SetErrorsDelegate(this.ShowErrors));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="errors"></param>
  public void ShowErrors(List<ErrorStruct> errors, int startIndex, int length)
  {
    if (this.ParentForm == null || this.IsDisposed || this.Disposing)
      return;
    if (!this.InvokeRequired)
    {
      int selectionStart = this._txt.SelectionStart;
      this._lockTextChanged = true;
      try
      {
        this._txt.SelectionStart = startIndex;
        this._txt.SelectionLength = length;
        this._txt.SelectionColor = this._txt.ForeColor;
        foreach (ErrorStruct error in errors)
        {
          this._txt.SelectionStart = error.Start;
          this._txt.SelectionLength = error.End - error.Start + 1;
          this._txt.SelectionColor = Color.Red;
        }
      }
      finally
      {
        this._txt.SelectionStart = selectionStart;
        this._txt.SelectionLength = 0;
        this._txt.Focus();
        this._lockTextChanged = false;
      }
    }
    else
    {
      if (this.IsDisposed)
        return;
      this.BeginInvoke((Delegate) new SpellChecker.SetErrorsDelegate(this.ShowErrors), (object) errors, (object) startIndex, (object) length);
    }
  }

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont() => !base.Font.Equals((object) this._txt.Font);

  /// <summary>Необходимость сериализации свойства Text.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeText()
  {
    return !string.IsNullOrEmpty(this._designText) ? this._txt.Text != this._designText : !string.IsNullOrEmpty(this._txt.Text);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._txt.TextChanged -= new EventHandler(this.On_txt_TextChanged);
      this._txt.Enter -= new EventHandler(this.On_txt_Enter);
      this._txt.KeyDown -= new KeyEventHandler(this.On_txt_KeyDown);
      this._txt.MouseDown -= new MouseEventHandler(this.On_txt_MouseDown);
      this._txt.GotFocus -= new EventHandler(this.On_txt_GotFocus);
      this._txt.LostFocus -= new EventHandler(this.On_txt_LostFocus);
      this._cm.Opening -= new CancelEventHandler(this.On_cm_Opening);
      this._cmiUndo.Click -= new EventHandler(this.On_cm_MenuItem_Click);
      this._cmiCut.Click -= new EventHandler(this.On_cm_MenuItem_Click);
      this._cmiCopy.Click -= new EventHandler(this.On_cm_MenuItem_Click);
      this._cmiPaste.Click -= new EventHandler(this.On_cm_MenuItem_Click);
      this._cmiDelete.Click -= new EventHandler(this.On_cm_MenuItem_Click);
      this._cmiSelectAll.Click -= new EventHandler(this.On_cm_MenuItem_Click);
      this._cmiPasteSymbol.Click -= new EventHandler(this.On_cm_MenuItem_Click);
      this._cmiEditSymbol.Click -= new EventHandler(this.On_cm_MenuItem_Click);
      this._cmiDelSymbol.Click -= new EventHandler(this.On_cm_MenuItem_Click);
      this._cmiAddToDictionary.Click -= new EventHandler(this.On_cm_MenuItem_Click);
      if (this._btnCalc != null && !this.IsDesignMode)
      {
        this._btnCalc.Click -= new EventHandler(this.On_expBtn_CalcClick);
        this._btnReCalc.Click -= new EventHandler(this.On_expBtn_ReCalcClick);
      }
      if (this.components != null)
        this.components.Dispose();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrMemoEdit));
    this._txt = new AnyLinkRichTextBox();
    this._cm = new ContextMenuStrip(this.components);
    this._cmiAddToDictionary = new ToolStripMenuItem();
    this._cmiUndo = new ToolStripMenuItem();
    this.toolStripSeparator3 = new ToolStripSeparator();
    this._cmiCut = new ToolStripMenuItem();
    this._cmiCopy = new ToolStripMenuItem();
    this._cmiPaste = new ToolStripMenuItem();
    this._cmiDelete = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this._cmiSelectAll = new ToolStripMenuItem();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this._cmiPasteSymbol = new ToolStripMenuItem();
    this._cmiEditSymbol = new ToolStripMenuItem();
    this._cmiDelSymbol = new ToolStripMenuItem();
    ((ISupportInitialize) this._err).BeginInit();
    this._cm.SuspendLayout();
    this.SuspendLayout();
    this._txt.AcceptsTab = true;
    this._txt.ContextMenuStrip = this._cm;
    componentResourceManager.ApplyResources((object) this._txt, "_txt");
    this._txt.HideSelection = false;
    this._txt.Name = "_txt";
    this._txt.LinkClicked += new LinkClickedEventHandler(this.On_txt_LinkClicked);
    this._txt.TextChanged += new EventHandler(this.On_txt_TextChanged);
    this._txt.Enter += new EventHandler(this.On_txt_Enter);
    this._txt.KeyDown += new KeyEventHandler(this.On_txt_KeyDown);
    this._txt.KeyUp += new KeyEventHandler(this._txt_KeyUp);
    this._txt.MouseDown += new MouseEventHandler(this.On_txt_MouseDown);
    this._cm.Items.AddRange(new ToolStripItem[13]
    {
      (ToolStripItem) this._cmiAddToDictionary,
      (ToolStripItem) this._cmiUndo,
      (ToolStripItem) this.toolStripSeparator3,
      (ToolStripItem) this._cmiCut,
      (ToolStripItem) this._cmiCopy,
      (ToolStripItem) this._cmiPaste,
      (ToolStripItem) this._cmiDelete,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this._cmiSelectAll,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this._cmiPasteSymbol,
      (ToolStripItem) this._cmiEditSymbol,
      (ToolStripItem) this._cmiDelSymbol
    });
    this._cm.Name = "_cm";
    componentResourceManager.ApplyResources((object) this._cm, "_cm");
    this._cm.Opening += new CancelEventHandler(this.On_cm_Opening);
    this._cmiAddToDictionary.Name = "_cmiAddToDictionary";
    componentResourceManager.ApplyResources((object) this._cmiAddToDictionary, "_cmiAddToDictionary");
    this._cmiAddToDictionary.Tag = (object) "9";
    this._cmiAddToDictionary.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiUndo.Name = "_cmiUndo";
    componentResourceManager.ApplyResources((object) this._cmiUndo, "_cmiUndo");
    this._cmiUndo.Tag = (object) "0";
    this._cmiUndo.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this.toolStripSeparator3.Name = "toolStripSeparator3";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator3, "toolStripSeparator3");
    this._cmiCut.Name = "_cmiCut";
    componentResourceManager.ApplyResources((object) this._cmiCut, "_cmiCut");
    this._cmiCut.Tag = (object) "1";
    this._cmiCut.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiCopy.Name = "_cmiCopy";
    componentResourceManager.ApplyResources((object) this._cmiCopy, "_cmiCopy");
    this._cmiCopy.Tag = (object) "2";
    this._cmiCopy.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiPaste.Name = "_cmiPaste";
    componentResourceManager.ApplyResources((object) this._cmiPaste, "_cmiPaste");
    this._cmiPaste.Tag = (object) "3";
    this._cmiPaste.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiDelete.Name = "_cmiDelete";
    componentResourceManager.ApplyResources((object) this._cmiDelete, "_cmiDelete");
    this._cmiDelete.Tag = (object) "4";
    this._cmiDelete.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this._cmiSelectAll.Name = "_cmiSelectAll";
    componentResourceManager.ApplyResources((object) this._cmiSelectAll, "_cmiSelectAll");
    this._cmiSelectAll.Tag = (object) "5";
    this._cmiSelectAll.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator2, "toolStripSeparator2");
    this._cmiPasteSymbol.Name = "_cmiPasteSymbol";
    componentResourceManager.ApplyResources((object) this._cmiPasteSymbol, "_cmiPasteSymbol");
    this._cmiPasteSymbol.Tag = (object) "6";
    this._cmiPasteSymbol.Click += new EventHandler(this.On_cm_MenuItem_Click);
    componentResourceManager.ApplyResources((object) this._cmiEditSymbol, "_cmiEditSymbol");
    this._cmiEditSymbol.Name = "_cmiEditSymbol";
    this._cmiEditSymbol.Tag = (object) "7";
    this._cmiEditSymbol.Click += new EventHandler(this.On_cm_MenuItem_Click);
    componentResourceManager.ApplyResources((object) this._cmiDelSymbol, "_cmiDelSymbol");
    this._cmiDelSymbol.Name = "_cmiDelSymbol";
    this._cmiDelSymbol.Tag = (object) "8";
    this._cmiDelSymbol.Click += new EventHandler(this.On_cm_MenuItem_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ContextMenuStrip = this._cm;
    this.Controls.Add((Control) this._txt);
    this._err.SetIconAlignment((Control) this, (ErrorIconAlignment) componentResourceManager.GetObject("$this.IconAlignment"));
    this._err.SetIconPadding((Control) this, (int) componentResourceManager.GetObject("$this.IconPadding"));
    this.Name = nameof (AttrMemoEdit);
    ((ISupportInitialize) this._err).EndInit();
    this._cm.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
