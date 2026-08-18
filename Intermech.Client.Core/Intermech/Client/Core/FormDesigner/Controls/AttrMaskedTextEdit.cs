
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrMaskedTextEdit
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.History;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Контрол-редактор для изменения текстовых значений по маске с возможностью просмотра истории. Адаптирован для работы с экспертной системой.
/// </summary>
[Designer(typeof (AttrMaskedTextEditControlDesigner))]
[RefreshProperties(RefreshProperties.All)]
public class AttrMaskedTextEdit : AttrsControl, IExpertSystemCtrl
{
  private IAttributePropertyDescriber _describer;
  /// <summary>Использование атрибута в экспертной системе</summary>
  private bool _useInExpertSystem;
  /// <summary>
  /// Необходимость проверки текста на соответствие типу данных
  /// </summary>
  private bool _needCheck = true;
  private bool _disableManualEdit;
  private bool _hasHistory;
  private ControlButton _btnDots;
  private ControlButton _btnCalc;
  private ControlButton _btnReCalc;
  private bool _textChanged;
  private bool _textChangedFromDlg;
  /// <summary>Значение атрибута</summary>
  private object _viewValue;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private MaskedTextBox _mtb;

  /// <summary>Цвет фона элемента управления.</summary>
  [DefaultValue(typeof (Color), "Window")]
  public new Color BackColor
  {
    get => this._mtb.BackColor;
    set => this._mtb.BackColor = value;
  }

  /// <summary>Вид обрамления для элемента управления.</summary>
  [DefaultValue(BorderStyle.Fixed3D)]
  public new BorderStyle BorderStyle
  {
    get => this._mtb.BorderStyle;
    set => this._mtb.BorderStyle = value;
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => this._mtb.Font;
    set => this._mtb.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [DefaultValue(typeof (Color), "WindowText")]
  public new Color ForeColor
  {
    get => this._mtb.ForeColor;
    set => this._mtb.ForeColor = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this._toolTip.GetToolTip((Control) this._mtb);
    set => this._toolTip.SetToolTip((Control) this._mtb, value);
  }

  /// <summary>
  /// Получает или задает символ, представляющий отсутствие данных, введенных пользователем в элемент управления AttrMaskedTextEdit.
  /// </summary>
  [DefaultValue('_')]
  public char PromptChar
  {
    get => this._mtb.PromptChar;
    set => this._mtb.PromptChar = value;
  }

  /// <summary>Текст, связанный с элементом управления.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public override string Text
  {
    get => this._mtb.Text;
    set
    {
      this._mtb.Text = string.IsNullOrEmpty(this._designText) || !string.IsNullOrEmpty(value) ? value : this._designText;
    }
  }

  /// <summary>Выравнивание текста в элементе управления.</summary>
  [DefaultValue(HorizontalAlignment.Left)]
  public HorizontalAlignment TextAlign
  {
    get => this._mtb.TextAlign;
    set => this._mtb.TextAlign = value;
  }

  /// <summary>
  /// Получает или задает значение, показывающее, может ли символ PromptChar вводиться пользователем в качестве допустимых данных.
  /// </summary>
  [DefaultValue(true)]
  public bool AllowPromptAsInput
  {
    get => this._mtb.AllowPromptAsInput;
    set => this._mtb.AllowPromptAsInput = value;
  }

  /// <summary>
  /// Получает или задает значение, показывающее, подает ли элемент управления "Текстовое поле с маской" звуковой сигнал для каждого отклоненного нажатия клавиши.
  /// </summary>
  [DefaultValue(false)]
  public bool BeepOnError
  {
    get => this._mtb.BeepOnError;
    set => this._mtb.BeepOnError = value;
  }

  /// <summary>
  /// Получает или задает значение, определяющее, копируются ли литералы и символы приглашения в буфер обмена.
  /// </summary>
  [DefaultValue(typeof (MaskFormat), "IncludeLiterals")]
  public MaskFormat CutCopyMaskFormat
  {
    get => this._mtb.CutCopyMaskFormat;
    set => this._mtb.CutCopyMaskFormat = value;
  }

  /// <summary>
  /// Получает или задает значение, показывающее, скрываются ли символы приглашения в маске ввода, когда фокус покидает элемент управления "Текстовое поле с маской".
  /// </summary>
  [DefaultValue(false)]
  public bool HidePromptOnLeave
  {
    get => this._mtb.HidePromptOnLeave;
    set => this._mtb.HidePromptOnLeave = value;
  }

  /// <summary>
  /// Получает или задает маску ввода для использования во время выполнения.
  /// </summary>
  [Browsable(false)]
  [DefaultValue("")]
  public string Mask
  {
    get => this._mtb.Mask;
    set => this._mtb.Mask = value;
  }

  /// <summary>
  /// Получает или задает значение, определяющее способ обработки введенного знака, совпадающего со знаком приглашения.
  /// </summary>
  [DefaultValue(true)]
  public bool ResetOnPrompt
  {
    get => this._mtb.ResetOnPrompt;
    set => this._mtb.ResetOnPrompt = value;
  }

  /// <summary>
  /// Получает или задает значение, определяющее способ обработки введенный знак пробела.
  /// </summary>
  [DefaultValue(true)]
  public bool ResetOnSpace
  {
    get => this._mtb.ResetOnSpace;
    set => this._mtb.ResetOnSpace = value;
  }

  /// <summary>
  /// Получает или задает значение, указывающее, разрешено ли пользователю повторно вводить литеральные значения.
  /// </summary>
  [DefaultValue(true)]
  public bool SkipLiterals
  {
    get => this._mtb.SkipLiterals;
    set => this._mtb.SkipLiterals = value;
  }

  /// <summary>
  /// Получает или задает значение, определяющее, включаются ли литералы и символы приглашения в форматированную строку.
  /// </summary>
  [DefaultValue(typeof (MaskFormat), "IncludeLiterals")]
  public MaskFormat TextMaskFormat
  {
    get => this._mtb.TextMaskFormat;
    set => this._mtb.TextMaskFormat = value;
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
      object[] getValues;
      if (this._describer != null)
      {
        object[] objArray;
        if (this._viewValue != DBNull.Value && this._viewValue != null)
          objArray = new object[1]{ this._viewValue };
        else
          objArray = new object[1]{ (object) DBNull.Value };
        getValues = objArray;
      }
      else
      {
        MaskFormat textMaskFormat = this.TextMaskFormat;
        this.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        string text = this.Text;
        this.TextMaskFormat = textMaskFormat;
        object[] objArray;
        if (!string.IsNullOrEmpty(text))
          objArray = new object[1]{ (object) this.Text };
        else
          objArray = new object[1]{ (object) DBNull.Value };
        getValues = objArray;
      }
      return getValues;
    }
  }

  /// <summary>Наличие Descriptor'а у атрибута.</summary>
  /// <remark>Необходимость в свойстве появилась в следующем случае:
  /// При связывании атрибута с контролом необходимо выставить доступнонсть редактирования атрибута.
  /// Если у атрибута свойство "Запрет редактирования в ручную" = "Да", необходимо запретить редактирование атрибута с помощью контрола.
  /// НО!!! Если значение можно не ввести с клавиатуры, а выбрать из списка, то необходимо разрешить модификацию атрибута,
  /// несмотря на запрет.
  /// С помощью Descriptor'а можно значение выбирать из списка, следовательно перед тем как присваивать значение свойству Enabled,
  /// необходимо проверить наличие Descriptor'а</remark>
  internal bool HasDescriptor => this._describer != null;

  /// <summary>Конструктор.</summary>
  public AttrMaskedTextEdit()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this._mtb.GotFocus += new EventHandler(this.On_mtb_GotFocus);
    this._mtb.LostFocus += new EventHandler(this.On_mtb_LostFocus);
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
    if (this._attrValues == null)
      return;
    bool flag = true;
    if (this._describer != null && this._describer.GetPropDescriptorEditor(this._attrValues.AttributeID) is UITypeEditor descriptorEditor)
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
                this.Text = Convert.ToString(obj ?? this._viewValue);
              }
              this._mtb.Focus();
              break;
          }
        }
      }
      flag = false;
    }
    if (!flag)
      return;
    using (ObjectsHistory objectsHistory = new ObjectsHistory((object) this.ParentInfo.ElementIdentifier, this.ParentInfo.ElementKind, (object) this.AttributeInfo.AttributeGuid))
    {
      objectsHistory.SelectedValue = (object) this.Text;
      this._textChangedFromDlg = false;
      if (this._mtb.ReadOnly)
        objectsHistory.SetReadOnly();
      if (objectsHistory.ShowDialog() != DialogResult.OK)
        return;
      this._textChangedFromDlg = true;
      this.Text = Convert.ToString(this._viewValue = objectsHistory.SelectedValue);
      this._mtb.Focus();
      this._mtb.SelectionStart = this._mtb.Text.Length;
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
  private void On_mtb_GotFocus(object sender, EventArgs e)
  {
    this.Error = string.Empty;
    this._textChanged = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_mtb_KeyDown(object sender, KeyEventArgs e)
  {
    if (this._attrValues == null)
      return;
    if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
    {
      if (this.TxtKeyDown == null)
        return;
      this.TxtKeyDown((object) this, e);
    }
    else
    {
      if (this._describer == null)
        return;
      e.SuppressKeyPress = true;
      if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
        return;
      object propDescriptorValue = this._describer.GetPropDescriptorValue(this.ParentInfo, this._attrValues.AttributeID, (object) DBNull.Value);
      this._viewValue = propDescriptorValue == null ? (object) DBNull.Value : this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, propDescriptorValue);
      this.Text = Convert.ToString(propDescriptorValue);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_mtb_LostFocus(object sender, EventArgs e)
  {
    this.SetNullValueError();
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
  private void On_mtb_SizeChanged(object sender, EventArgs e)
  {
    this.Height = this._mtb == null || this._mtb.Height < 20 ? 22 : this._mtb.Height + 2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_mtb_TextChanged(object sender, EventArgs e)
  {
    if (this.IsDesignMode || this._attrValues == null)
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
      this._attrValues = value;
      bool visible = false;
      if (value != null)
      {
        IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(value.AttributeID);
        if (attributeType1 != null)
          this._mtb.MaxLength = Convert.ToInt32(attributeType1.SizeType);
        if (this.ParentInfo != null)
        {
          IMSAttribute4 imsAttribute4 = this.ParentInfo.ElementKind != AttributableElements.Object ? (IMSAttribute4) MetaDataHelper.GetAttribute4RelationType(this.ParentTypeID, value.AttributeID) : (IMSAttribute4) MetaDataHelper.GetAttribute4ObjectType(this.ParentTypeID, value.AttributeID);
          AttributeOptions attributeOptions;
          if (imsAttribute4 != null)
          {
            this._mtb.Mask = imsAttribute4.Mask;
            attributeOptions = imsAttribute4.Options;
            if (imsAttribute4.Required == RequiredModes.Manual)
              attributeOptions &= ~AttributeOptions.DisableNulls;
          }
          else
          {
            IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(value.AttributeID);
            this._mtb.Mask = attributeType2 != null ? attributeType2.Mask : string.Empty;
            attributeOptions = attributeType2.Options & ~AttributeOptions.DisableNulls;
          }
          this._hasHistory = (attributeOptions & AttributeOptions.SavePrivateHistory) > AttributeOptions.None || (attributeOptions & AttributeOptions.SaveCommonHistory) > AttributeOptions.None;
          this._disableManualEdit = (attributeOptions & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit;
          if (this._disableNulls = (attributeOptions & AttributeOptions.DisableNulls) != 0)
          {
            string attributeTypeName = MetaDataHelper.GetAttributeTypeName(value.AttributeGuid);
            this._errMsg_NullValue = string.Format(LocalizationHolder.rm.GetString("Controls_NullValue_ErrorMessage"), (object) attributeTypeName);
          }
        }
        this._viewValue = value.Values == null || value.Values.Length == 0 ? (object) DBNull.Value : value.Values[0];
        this._describer = ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service ? service.GetDescriber(value.AttributeID) : (IAttributePropertyDescriber) null;
        if (this._describer != null)
        {
          object propDescriptorValue = this._describer.GetPropDescriptorValue(this.ParentInfo, value.AttributeID, value.Values[0]);
          if (propDescriptorValue != null)
            this._viewValue = this._describer.GetAttributeValue(this.ParentInfo, value.AttributeID, propDescriptorValue);
          this.Text = Convert.ToString(propDescriptorValue);
        }
        else
          this.Text = Convert.ToString(this._viewValue);
        visible = this._describer != null || this._hasHistory;
      }
      else
        this.Text = string.Empty;
      this.SetVisibleButton(visible);
      this.EnabledCtrl = this.IsEnabled();
      this.SetNullValueError();
    }
  }

  /// <summary>Доступность контрола.</summary>
  public override bool EnabledCtrl
  {
    get => this._enabled;
    set
    {
      this._enabled = value;
      if (!this.IsDesignMode)
      {
        this._mtb.ReadOnly = !this._enabled;
        if (!this._enabled)
        {
          if (this._mtb.BackColor == SystemColors.Window)
            this._mtb.BackColor = SystemColors.Control;
        }
        else if (this._mtb.BackColor == SystemColors.Control)
          this._mtb.BackColor = SystemColors.Window;
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
    if (this._mtb.Text == this._designText)
      this._mtb.Text = text;
    this._designText = text;
  }

  /// <summary>
  /// 
  /// </summary>
  private void CheckAccessibilityButtons()
  {
    if (this.Site != null && this.Site.DesignMode)
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
    this.Error = this._mtb.Focused || !this._disableNulls || !this.EnabledCtrl || this._viewValue != DBNull.Value && this._viewValue != null ? string.Empty : this._errMsg_NullValue;
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
    if (string.IsNullOrEmpty(this.Text))
      this._viewValue = (object) DBNull.Value;
    else
      this._viewValue = (object) this.Text;
  }

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont() => !base.Font.Equals((object) this._mtb.Font);

  /// <summary>Необходимость сериализации свойства Text.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeText()
  {
    return !string.IsNullOrEmpty(this._designText) ? this._mtb.Text != this._designText : !string.IsNullOrEmpty(this._mtb.Text);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._mtb.SizeChanged -= new EventHandler(this.On_mtb_SizeChanged);
      this._mtb.TextChanged -= new EventHandler(this.On_mtb_TextChanged);
      this._mtb.KeyDown -= new KeyEventHandler(this.On_mtb_KeyDown);
      this._mtb.GotFocus -= new EventHandler(this.On_mtb_GotFocus);
      this._mtb.LostFocus -= new EventHandler(this.On_mtb_LostFocus);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrMaskedTextEdit));
    this._mtb = new MaskedTextBox();
    ((ISupportInitialize) this._err).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._mtb, "_mtb");
    this._mtb.Name = "_mtb";
    this._mtb.SizeChanged += new EventHandler(this.On_mtb_SizeChanged);
    this._mtb.TextChanged += new EventHandler(this.On_mtb_TextChanged);
    this._mtb.KeyDown += new KeyEventHandler(this.On_mtb_KeyDown);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._mtb);
    this._err.SetIconAlignment((Control) this, (ErrorIconAlignment) componentResourceManager.GetObject("$this.IconAlignment"));
    this._err.SetIconPadding((Control) this, (int) componentResourceManager.GetObject("$this.IconPadding"));
    this.Name = nameof (AttrMaskedTextEdit);
    ((ISupportInitialize) this._err).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
