
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrTextEdit
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.History;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Контрол-редактор для изменения текстовых значений с возможностью просмотра истории адаптирован для работы с экспертной системой.
/// </summary>
[Designer(typeof (AttrTextEditControlDesigner))]
[RefreshProperties(RefreshProperties.All)]
public class AttrTextEdit : AttrsControl, IExpertSystemCtrl
{
  /// <summary>Тип атрибута</summary>
  private FieldTypes _attrFldType = FieldTypes.ftString;
  private IAttributePropertyDescriber _describer;
  /// <summary>Значение атрибута</summary>
  private object _viewValue;
  /// <summary>Использование атрибута в экспертной системе</summary>
  private bool _useInExpertSystem;
  private bool _lockTextChanged;
  private bool _textChanged;
  private bool _textChangedFromDlg;
  /// <summary>
  /// Необходимость проверки текста на соответствие типу данных
  /// </summary>
  private bool _needCheck = true;
  /// <summary>
  /// Признак, что был клик правой клавишей перед открытием контекстного меню
  /// </summary>
  private bool _rightButtonClick;
  private bool _disableManualEdit;
  private bool _hasHistory;
  /// <summary>Позиция клика правой клавишей мышки</summary>
  private int _position = -1;
  private ControlButton _btnDots;
  private ControlButton _btnCalc;
  private ControlButton _btnReCalc;
  /// <summary>Переменная хранит значение текста</summary>
  private string _currText = string.Empty;
  private string _description = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox _txt;
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

  /// <summary>Выравнивание текста в элементе управления.</summary>
  [DefaultValue(HorizontalAlignment.Left)]
  public HorizontalAlignment TextAlign
  {
    get => this._txt.TextAlign;
    set => this._txt.TextAlign = value;
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

  /// <summary>Наличие Descriptor'а у атрибута.</summary>
  /// <remark>
  /// Необходимость в свойстве появилась в следующем случае:
  /// При связывании атрибута с контролом необходимо выставить доступнонсть редактирования атрибута.
  /// Если у атрибута свойство "Запрет редактирования в ручную" = "Да", необходимо запретить редактирование атрибута с помощью контрола.
  /// НО!!! Если значение можно не ввести с клавиатуры, а выбрать из списка, то необходимо разрешить модификацию атрибута, несмотря на запрет.
  /// С помощью Descriptor'а значение можно выбирать из списка, поэтому перед тем как присваивать значение свойству Enabled, необходимо проверить наличие Descriptor'а.
  /// </remark>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal bool HasDescriptor => this._describer != null;

  /// <summary>Получение текущего набора значений атрибута.</summary>
  protected override object[] GetValues
  {
    get
    {
      object[] getValues = (object[]) null;
      if (this._describer != null)
      {
        if (this._viewValue == DBNull.Value || this._viewValue == null || this._attrFldType == FieldTypes.ftGuid && Guid.Empty.Equals(new Guid(this._viewValue.ToString())))
          getValues = new object[1]{ (object) DBNull.Value };
        else
          getValues = new object[1]{ this._viewValue };
      }
      else
      {
        string s = this._bNeedDescription ? Convert.ToString(this._viewValue) : this._txt.Text;
        if (string.IsNullOrEmpty(s))
        {
          getValues = new object[1]{ (object) DBNull.Value };
        }
        else
        {
          switch (this._attrFldType)
          {
            case FieldTypes.ftInteger:
              long result1 = 0;
              long.TryParse(s, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result1);
              getValues = new object[1]{ (object) result1 };
              break;
            case FieldTypes.ftDouble:
              double result2 = 0.0;
              switch (CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)
              {
                case ".":
                  s = s.Replace(',', '.');
                  break;
                case ",":
                  s = s.Replace('.', ',');
                  break;
              }
              double.TryParse(s, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result2);
              getValues = new object[1]{ (object) result2 };
              break;
            case FieldTypes.ftSystem:
              if (this._attrValues != null && this._attrValues.AttributeID == -7)
              {
                getValues = new object[1]{ this._viewValue };
                break;
              }
              break;
            case FieldTypes.ftGuid:
              try
              {
                getValues = new object[1]{ (object) s };
                break;
              }
              catch (Exception ex)
              {
                throw ex;
              }
            default:
              getValues = new object[1]{ (object) s };
              break;
          }
        }
      }
      return getValues;
    }
  }

  /// <summary>Конструктор.</summary>
  public AttrTextEdit()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this._txt.GotFocus += new EventHandler(this.On_txt_GotFocus);
    this._txt.LostFocus += new EventHandler(this.On_txt_LostFocus);
  }

  /// <summary>
  /// 
  /// </summary>
  public event KeyEventHandler TxtKeyDown;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void On_btn_Click(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this._attrValues == null)
      return;
    if (this._describer != null)
    {
      if (this._describer.GetPropDescriptorEditor(this._attrValues.AttributeID) is UITypeEditor descriptorEditor)
      {
        using (ServiceContainer provider = new ServiceContainer())
        {
          using (DropDownEditorForm serviceInstance = new DropDownEditorForm())
          {
            provider.AddService(typeof (IWindowsFormsEditorService), (object) serviceInstance);
            ITypeDescriptorContext context = (ITypeDescriptorContext) new ControlsContext(this.Values, this._describer, this.ParentInfo);
            switch (descriptorEditor.GetEditStyle(context))
            {
              case UITypeEditorEditStyle.Modal:
              case UITypeEditorEditStyle.DropDown:
                object propDescriptorValue = this._describer.GetPropDescriptorValue(this.ParentInfo, this._attrValues.AttributeID, this._viewValue);
                object obj = descriptorEditor.EditValue(context, (System.IServiceProvider) provider, propDescriptorValue);
                if (!object.Equals(obj, propDescriptorValue))
                {
                  this._viewValue = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, obj);
                  this.SetText(this._viewValue);
                }
                this._txt.Focus();
                break;
            }
          }
        }
      }
    }
    else if (this._attrValues.AttributeID == -7)
    {
      using (SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_118"), new System.Type[2]
      {
        typeof (ObjectTypesFolder),
        typeof (ObjectTypeFolder)
      }, false))
      {
        selectorForm.ClearSelection();
        selectorForm.InitSelectionAsType(new ArrayList((ICollection) new int[1]
        {
          (int) this._viewValue
        }), new ArrayList((ICollection) new System.Type[1]
        {
          typeof (ObjectTypeFolder)
        }));
        if (selectorForm.ShowDialog() == DialogResult.OK)
        {
          if (selectorForm.IDList.Count > 0)
          {
            int id = (int) selectorForm.IDList[0];
            if (id != -1)
            {
              this._viewValue = (object) id;
              this.SetText(this._viewValue);
            }
          }
        }
      }
    }
    else
    {
      using (ObjectsHistory objectsHistory = new ObjectsHistory((object) this.ParentInfo.ElementIdentifier, this.ParentInfo.ElementKind, (object) this.AttributeInfo.AttributeGuid))
      {
        objectsHistory.SelectedValue = this._bNeedDescription ? this._viewValue : (object) this._txt.Text;
        if (this._txt.ReadOnly)
          objectsHistory.SetReadOnly();
        this._textChangedFromDlg = false;
        if (objectsHistory.ShowDialog() == DialogResult.OK)
        {
          this._textChangedFromDlg = true;
          this._viewValue = objectsHistory.SelectedValue;
          this.SetText(this._viewValue);
          this._txt.Focus();
        }
      }
    }
    this._txt.SelectionStart = this._txt.Text.Length;
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
    }
    else
    {
      this._cmiUndo.Enabled = false;
      this._cmiCut.Enabled = this._cmiDelete.Enabled = this._cmiPaste.Enabled = false;
      this._cmiPasteSymbol.Enabled = this._cmiEditSymbol.Enabled = this._cmiDelSymbol.Enabled = false;
      this._cmiCopy.Enabled = !string.IsNullOrEmpty(this._txt.SelectedText);
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
    }
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

  /// <summary>Фокусирование текстового контрола.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_GotFocus(object sender, EventArgs e)
  {
    this.SetNullValueError();
    if (this._bNeedDescription)
    {
      this._lockTextChanged = true;
      this.SetText(this._viewValue);
      this._lockTextChanged = false;
    }
    this._textChanged = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_KeyDown(object sender, KeyEventArgs e)
  {
    if (this._attrValues == null)
      return;
    if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
    {
      if (this.TxtKeyDown == null)
        return;
      this.TxtKeyDown((object) this, e);
    }
    else if (this._describer != null)
    {
      e.SuppressKeyPress = true;
      if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
        return;
      this._viewValue = (object) DBNull.Value;
      this.SetText(this._viewValue);
    }
    else
      this._currText = this._txt.Text;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_LostFocus(object sender, EventArgs e)
  {
    this.SetNullValueError();
    if (this._bNeedDescription)
    {
      this._lockTextChanged = true;
      this.SetText((object) this._description);
      this._lockTextChanged = false;
    }
    if (!this._textChanged && !this._textChangedFromDlg)
      return;
    this.OnCompletionOfEditing();
    this._textChanged = this._textChangedFromDlg = false;
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
  private void On_txt_SizeChanged(object sender, EventArgs e)
  {
    this.Height = this._txt == null || this._txt.Height < 20 ? 22 : this._txt.Height + 2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_TextChanged(object sender, EventArgs e)
  {
    if (this.IsDesignMode || this._lockTextChanged || this._attrValues == null)
      return;
    if (this._needCheck)
      this.ValidatingText();
    this.Modified = true;
    if (!this.Modified)
      return;
    this._textChanged = this._needCheck = true;
  }

  /// <summary>Значение атрибута.</summary>
  public override AttributeValues Values
  {
    get => base.Values;
    set
    {
      this._currText = this._description = string.Empty;
      this._attrFldType = FieldTypes.ftString;
      this._attrValues = value;
      this._viewValue = value == null || value.Values == null || value.Values.Length == 0 ? (object) DBNull.Value : value.Values[0];
      bool visible = false;
      if (value != null)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(value.AttributeID);
        if (attributeType != null)
        {
          this._attrFldType = attributeType.FieldType;
          if (attributeType.SizeType == -1L)
          {
            this._txt.MaxLength = 0;
            int num = (int) IMMessageBox.Show("Внимание!", $"Для редактирования атрибута \"{attributeType.Name}\" в форме редактирования следует использовать компонент \"Число в единицах измерения\"", new IMMessageBoxButton[1]
            {
              new IMMessageBoxButton("OK", DialogResult.OK)
            }, IMMessageBoxImage.Warning);
          }
          else
            this._txt.MaxLength = Convert.ToInt32(attributeType.SizeType);
        }
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          AttributeOptions attributeOptions = this.GetAttributeOptions(value.AttributeID, sessionKeeper.Session);
          this._hasHistory = (attributeOptions & AttributeOptions.SavePrivateHistory) > AttributeOptions.None || (attributeOptions & AttributeOptions.SaveCommonHistory) > AttributeOptions.None;
          this._disableManualEdit = (attributeOptions & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit;
          if ((this._bNeedDescription = (attributeOptions & AttributeOptions.GetDescriptionEvent) != 0) && value.Descriptions != null && value.Descriptions.Length != 0)
            this._description = Convert.ToString(value.Descriptions[0]);
          if (this._disableNulls = (attributeOptions & AttributeOptions.DisableNulls) != 0)
          {
            string attributeTypeName = MetaDataHelper.GetAttributeTypeName(value.AttributeGuid);
            this._errMsg_NullValue = string.Format(LocalizationHolder.rm.GetString("Controls_NullValue_ErrorMessage"), (object) attributeTypeName);
          }
        }
        this._describer = !(ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service) || this.Site != null && this.Site.DesignMode ? (IAttributePropertyDescriber) null : service.GetDescriber(value.AttributeID);
        this.SetText(!this._bNeedDescription || this._txt.Focused ? this._viewValue : (object) this._description);
        if (this._attrFldType == FieldTypes.ftSystem)
        {
          visible = this.DesForm == null || !this.DesForm.IsCreationMode;
        }
        else
        {
          visible = this._describer != null || this._hasHistory;
          if (this.ParentInfo == null)
            visible = false;
        }
      }
      else
        this.SetText(this._viewValue);
      this.SetVisibleButton(visible);
      this.EnabledCtrl = this.IsEnabled();
      this.SetNullValueError();
    }
  }

  /// <summary>Доступность контрола.</summary>
  [DefaultValue(true)]
  public override bool EnabledCtrl
  {
    get => this._enabled;
    set
    {
      this._enabled = value;
      if (this.Site == null || !this.Site.DesignMode)
      {
        this._txt.ReadOnly = this._attrFldType == FieldTypes.ftSystem || !this._enabled;
        if (!this._enabled)
        {
          if (this._txt.BackColor.ToArgb() == SystemColors.Window.ToArgb())
            this._txt.BackColor = SystemColors.Control;
        }
        else if (this._txt.BackColor == SystemColors.Control)
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
  /// <param name="text"></param>
  protected override void SetDesignText(string text)
  {
    base.SetDesignText(text);
    if (this._txt.Text == this._designText)
      this._txt.Text = text;
    this._designText = text;
  }

  /// <summary>
  /// 
  /// </summary>
  private void CheckAccessibilityButtons()
  {
    if (this.IsDesignMode)
    {
      if (this._btnCalc == null)
        return;
      this._btnCalc.Enabled = this._btnReCalc.Enabled = this.AttributeInfo != null;
    }
    else if (this._attrValues != null)
    {
      if (this._attrValues.AttributeGuid == Guid.Empty)
      {
        if (this._btnDots != null)
          this._btnDots.Enabled = this._hasHistory;
      }
      else if (this._btnDots != null)
        this._btnDots.Enabled = this.EnabledCtrl || (this._attrValues.ReadOnly || this.DisabledInDesign ? this._describer == null && this._hasHistory : this._disableManualEdit && (this._describer != null || this._hasHistory));
      if (this._btnCalc == null)
        return;
      this._btnCalc.Enabled = this._btnReCalc.Enabled = true;
    }
    else
      this._buttons.Enabled = false;
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

  /// <summary>Проверка возможности редактирования атрибута.</summary>
  /// <param name="av">Значение атрибута</param>
  /// <param name="options">Опции</param>
  /// <returns>Результат проверки</returns>
  private bool IsEnabled()
  {
    bool flag = this._attrValues != null && !this._attrValues.ReadOnly;
    if (flag)
    {
      flag = !this.DisabledInDesign && !this._disableManualEdit;
      if (flag)
        flag = this._attrValues != null && this._attrValues.AttributeGuid != Guid.Empty;
    }
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  private void SetNullValueError()
  {
    if (this._txt.Focused)
      this.Error = string.Empty;
    else if (this._disableNulls && this.EnabledCtrl && (this._viewValue == DBNull.Value || this._viewValue == null || this._attrFldType == FieldTypes.ftGuid && Guid.Empty.Equals(new Guid(this._viewValue.ToString()))))
    {
      this.Error = this._errMsg_NullValue;
    }
    else
    {
      if (this._attrFldType == FieldTypes.ftGuid)
      {
        try
        {
          Guid guid = new Guid(this._viewValue.ToString());
        }
        catch (Exception ex)
        {
          this.Error = ex.Message;
          return;
        }
      }
      this.Error = string.Empty;
    }
  }

  /// <summary>Устанавить значение текста.</summary>
  /// <param name="value">Значение</param>
  private void SetText(object value)
  {
    if (this._attrValues != null && this._describer != null && this.ParentInfo != null)
      this._txt.Text = Convert.ToString(this._describer.GetPropDescriptorValue(this.ParentInfo, this._attrValues.AttributeID, value));
    else if (value == null || value == DBNull.Value)
      this._txt.Text = string.Empty;
    else if (this._attrFldType == FieldTypes.ftDouble)
    {
      try
      {
        string str = Convert.ToDouble(value, (IFormatProvider) CultureInfo.InvariantCulture).ToString("#################0.#################", (IFormatProvider) CultureInfo.InvariantCulture);
        switch (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
        {
          case ".":
            this._txt.Text = str.Replace(',', '.');
            break;
          case ",":
            this._txt.Text = str.Replace('.', ',');
            break;
        }
      }
      catch
      {
        this._txt.Text = string.Empty;
      }
    }
    else if (-7 == this._attrValues.AttributeID)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType((int) value);
      this._txt.Text = objectType != null ? objectType.ObjectName : string.Empty;
    }
    else
      this._txt.Text = Convert.ToString(value);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="visible"></param>
  private void SetVisibleButton(bool visible)
  {
    if (visible)
    {
      if (this._btnDots == null)
      {
        this._btnDots = new ControlButton("Dots", 0);
        this._btnDots.Click += new EventHandler(this.On_btn_Click);
      }
      this.AddRightButton(this._btnDots, true);
    }
    else
    {
      if (this._btnDots == null)
        return;
      this.RemoveRightButton(this._btnDots);
    }
  }

  /// <summary>Проверка текста на соответствие типу данных.</summary>
  /// <returns>Результат проверки</returns>
  private void ValidatingText()
  {
    if (this._describer != null)
      return;
    if (string.IsNullOrEmpty(this._txt.Text))
    {
      this._viewValue = (object) null;
    }
    else
    {
      switch (this._attrFldType)
      {
        case FieldTypes.ftInteger:
          long result1 = 0;
          if (long.TryParse(this._txt.Text, out result1))
          {
            this._currText = this._txt.Text;
            break;
          }
          this._needCheck = false;
          this._txt.Text = this._currText;
          this._txt.SelectionStart = this._txt.Text.Length;
          break;
        case FieldTypes.ftDouble:
          string s = this._txt.Text;
          double result2 = 0.0;
          if (s.Contains(".") || s.Contains(","))
          {
            switch (CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)
            {
              case ".":
                s = s.Replace(',', '.');
                break;
              case ",":
                s = s.Replace('.', ',');
                break;
            }
          }
          if (double.TryParse(s, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result2))
          {
            int selectionStart = this._txt.SelectionStart;
            this._txt.Text = s;
            this._txt.SelectionStart = selectionStart;
            this._currText = this._txt.Text;
            break;
          }
          this._needCheck = false;
          this._txt.Text = this._currText;
          this._txt.SelectionStart = this._txt.Text.Length;
          break;
        case FieldTypes.ftSystem:
          if (this._attrValues != null && this._attrValues.AttributeID == -7)
            return;
          break;
      }
      if (this._bNeedDescription && !this._txt.Focused)
        return;
      this._viewValue = (object) this._txt.Text;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  public void SetSystemAttributeEnable(bool value)
  {
    if (value)
    {
      this.Enabled = true;
      this._txt.ReadOnly = true;
    }
    else
      this.Enabled = false;
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
      this._txt.SizeChanged -= new EventHandler(this.On_txt_SizeChanged);
      this._txt.TextChanged -= new EventHandler(this.On_txt_TextChanged);
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
      if (!this.IsDesignMode)
      {
        if (this._btnDots != null)
          this._btnDots.Click -= new EventHandler(this.On_btn_Click);
        if (this._btnCalc != null)
        {
          this._btnCalc.Click -= new EventHandler(this.On_expBtn_CalcClick);
          this._btnReCalc.Click -= new EventHandler(this.On_expBtn_ReCalcClick);
        }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrTextEdit));
    this._txt = new TextBox();
    this._cm = new ContextMenuStrip(this.components);
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
    this._txt.ContextMenuStrip = this._cm;
    componentResourceManager.ApplyResources((object) this._txt, "_txt");
    this._txt.Name = "_txt";
    this._txt.SizeChanged += new EventHandler(this.On_txt_SizeChanged);
    this._txt.TextChanged += new EventHandler(this.On_txt_TextChanged);
    this._txt.KeyDown += new KeyEventHandler(this.On_txt_KeyDown);
    this._txt.MouseDown += new MouseEventHandler(this.On_txt_MouseDown);
    this._cm.Items.AddRange(new ToolStripItem[12]
    {
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
    this.Controls.Add((Control) this._txt);
    this._err.SetIconAlignment((Control) this, (ErrorIconAlignment) componentResourceManager.GetObject("$this.IconAlignment"));
    this._err.SetIconPadding((Control) this, (int) componentResourceManager.GetObject("$this.IconPadding"));
    this.Name = nameof (AttrTextEdit);
    ((ISupportInitialize) this._err).EndInit();
    this._cm.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
