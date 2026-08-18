
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrComboBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Контрол-редактор выбор одного значения из списка.</summary>
public class AttrComboBox : 
  ComboBox,
  IAttributeEditor,
  IBaseDesForm,
  IAttributeEditorModified,
  ICompletionOfEditing,
  IExtendedParent4Control,
  IParent4Control,
  IIMControlEnabled,
  ILockModify
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ErrorProvider _err;
  private DesForm _parentForm;
  /// <summary>Данные об объекте/связи, которому принадлежит атрибут</summary>
  private IElementInfo _elementInfo;
  /// <summary>Замена Guid'а атрибута</summary>
  private AttributeInfo _attrInfo;
  /// <summary>
  /// Класс, содержащий идентификатор(ы) атрибута + его значение(я)
  /// </summary>
  private AttributeValues _attrValues;
  /// <summary>Таблица с данными</summary>
  private DataTable _possibleValues;
  /// <summary>Возможность атрибута иметь пустое значение</summary>
  private bool _disableNulls;
  /// <summary>
  /// Флаг, о рассылке уведомления об изменениии значений в контроле
  /// </summary>
  private bool _needNotify;
  private const int WS_HSCROLL = 1048576 /*0x100000*/;
  private const int CB_SETHORIZONTALEXTENT = 350;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._parentForm = (DesForm) null;
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
    this._err = new ErrorProvider(this.components);
    ((ISupportInitialize) this._err).BeginInit();
    this.SuspendLayout();
    this._err.BlinkStyle = ErrorBlinkStyle.NeverBlink;
    this._err.SetIconAlignment((Control) this, ErrorIconAlignment.TopLeft);
    this._err.SetIconPadding((Control) this, -16);
    ((ISupportInitialize) this._err).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Цвет фона элемента управления.</summary>
  [DefaultValue(typeof (Color), "Window")]
  public new Color BackColor
  {
    get => base.BackColor;
    set => base.BackColor = value;
  }

  /// <summary>
  /// Перекрыто для того, чтобы не сериализовать это свойство.
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new ComboBoxStyle DropDownStyle
  {
    get => base.DropDownStyle;
    set => base.DropDownStyle = value;
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => base.Font;
    set => base.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [DefaultValue(typeof (Color), "WindowText")]
  public new Color ForeColor
  {
    get => base.ForeColor;
    set => base.ForeColor = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  public string Hint { get; set; }

  /// <summary>Доступен, только если назначен атрибут.</summary>
  [Browsable(false)]
  [DefaultValue(false)]
  public bool EnableWithoutAttribute { get; set; }

  /// <summary>
  /// Перекрыто для того, чтобы не сериализовать это свойство.
  /// </summary>
  /// <remarks>У новых контролов не сериализуется, т.к. свойство не меняется, видимо раньше свойство изменялось, что приводило к сериализации</remarks>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new bool FormattingEnabled
  {
    get => base.FormattingEnabled;
    set => base.FormattingEnabled = value;
  }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  private object[] GetValues
  {
    get
    {
      object selectedValue = this.SelectedValue;
      TypeConverter singleValueConverter = this._parentForm != null ? this._parentForm.Processor.GetSingleValueConverter(this._attrValues.AttributeID) : (TypeConverter) null;
      return new object[1]
      {
        singleValueConverter == null || selectedValue == null || !singleValueConverter.CanConvertFrom(selectedValue.GetType()) ? selectedValue : singleValueConverter.ConvertFrom(selectedValue)
      };
    }
  }

  /// <summary>Конструктор.</summary>
  public AttrComboBox()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this.Sorted = false;
    this.DropDownStyle = ComboBoxStyle.DropDownList;
    this.CanAddAttribute = true;
    this.ParentPoint = AttributeDestinationPoint.Default;
  }

  /// <summary>Guid атрибута и типа объекта/связи.</summary>
  [Browsable(false)]
  [DefaultValue(null)]
  public AttributeInfo AttributeInfo
  {
    get => this._attrInfo;
    set
    {
      this._attrInfo = value == null || !MetaDataHelper.ExistsAttributeType(value.AttributeGuid) ? (AttributeInfo) null : value;
      if (this.Site == null || !this.Site.DesignMode)
        return;
      string str = this._attrInfo != null ? MetaDataHelper.GetAttributeTypeName(this._attrInfo.AttributeGuid) : string.Empty;
      this.Items.Clear();
      if (string.IsNullOrEmpty(str))
        return;
      this.Items.Add((object) str);
      this.LockModify = true;
      try
      {
        this.SelectedIndex = 0;
      }
      finally
      {
        this.LockModify = false;
      }
    }
  }

  /// <summary>
  /// Возможность добавления атрибута в случае если он отсутствует у объекта.
  /// </summary>
  [Browsable(false)]
  [DefaultValue(true)]
  public bool CanAddAttribute { get; set; }

  /// <summary>Значение атрибута.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public AttributeValues Values
  {
    get
    {
      if (this._attrValues != null)
        this._attrValues.Values = this.GetValues;
      return this._attrValues;
    }
    set
    {
      this._attrValues = value;
      AttributeOptions options = AttributeOptions.None;
      string str = string.Empty;
      if (value != null)
      {
        try
        {
          this.SelectedValue = value.Values[0] == DBNull.Value ? value.Values[0] : (object) Convert.ToString(value.Values[0]);
        }
        catch (Exception ex)
        {
        }
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          options = this.GetAttributeOptions(value.AttributeID, sessionKeeper.Session);
          this._disableNulls = (options & AttributeOptions.DisableNulls) != 0;
          if (this._disableNulls)
          {
            string attributeTypeName = MetaDataHelper.GetAttributeTypeName(value.AttributeGuid);
            str = string.Format(LocalizationHolder.rm.GetString("Controls_NullValue_ErrorMessage"), (object) attributeTypeName);
          }
        }
      }
      else
        this.DataSource = (object) null;
      this.EnabledCtrl = this.IsEnabled(value, options);
      this._err.SetError((Control) this, !this._disableNulls || !this.EnabledCtrl || this.SelectedValue != null && this.SelectedValue != DBNull.Value ? string.Empty : str);
    }
  }

  /// <summary>Установка допустимых значений.</summary>
  /// <param name="data"></param>
  /// <param name="possibleValueFieldName"></param>
  /// <param name="descriptionFieldName"></param>
  public void SetPossibleValues(
    DataTable data,
    string possibleValueFieldName,
    string descriptionFieldName)
  {
    if (data == null || data.Rows.Count <= 0 || string.IsNullOrEmpty(possibleValueFieldName) || string.IsNullOrEmpty(descriptionFieldName))
      return;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    foreach (DataRow row in (InternalDataCollectionBase) data.Rows)
    {
      string str1 = Convert.ToString(row[possibleValueFieldName]);
      string str2 = Convert.ToString(row[descriptionFieldName]);
      row[descriptionFieldName] = string.IsNullOrEmpty(str2) ? (object) str1 : (object) str2;
    }
    this._possibleValues = data;
    this._possibleValues.CaseSensitive = true;
    this.DataSource = (object) this._possibleValues;
    this.DisplayMember = descriptionFieldName;
    this.ValueMember = possibleValueFieldName;
  }

  /// <summary>Устанавливает родительскую форму.</summary>
  [Browsable(false)]
  public DesForm DesForm
  {
    set
    {
      this._parentForm = value;
      if (this._parentForm == null)
        return;
      this._parentForm.ToolTip.SetToolTip((Control) this, this.Hint);
    }
  }

  /// <summary>Изменение данных.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Modified { get; set; }

  /// <summary>Событие, возникающее при изменении данных.</summary>
  public event EventHandler ModifiedEvent;

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool LockModify { get; set; }

  /// <summary>Устанавливает родителя для атрибута.</summary>
  [Browsable(false)]
  public IElementInfo ParentInfo
  {
    get => this._elementInfo;
    set
    {
      this._elementInfo = value;
      if (value != null)
        return;
      this.SetPossibleValuesWithoutParentInfo();
    }
  }

  /// <summary>Для чего нужен контрол.</summary>
  [Browsable(false)]
  [DefaultValue(AttributeDestinationPoint.Default)]
  public AttributeDestinationPoint ParentPoint { get; set; }

  /// <summary>Идентификатор типа объекта/связи.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int ParentTypeID { get; set; }

  /// <summary>Запретить редактирование данных.</summary>
  [Browsable(false)]
  [DefaultValue(false)]
  public bool DisabledInDesign { get; set; }

  /// <summary>Доступность контрола.</summary>
  [Browsable(false)]
  [DefaultValue(true)]
  public bool EnabledCtrl
  {
    get => this.Enabled;
    set => this.Enabled = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler CompletionOfEditingEvent;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLeave(EventArgs e)
  {
    base.OnLeave(e);
    if (!this._needNotify || this._parentForm == null)
      return;
    this._parentForm.AttributeChanging(this._attrValues.AttributeID, this._attrValues.Values, this.GetValues, this.ParentPoint == AttributeDestinationPoint.Default);
    this._needNotify = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnSelectedIndexChanged(EventArgs e)
  {
    base.OnSelectedIndexChanged(e);
    if (this._disableNulls)
      this._err.SetError((Control) this, string.Empty);
    if (!this.LockModify && this._attrInfo != null)
    {
      this.Modified = true;
      this._needNotify = true;
      if (this.ModifiedEvent != null)
        this.ModifiedEvent((object) this, EventArgs.Empty);
    }
    if (this.LockModify || this.CompletionOfEditingEvent == null)
      return;
    this.CompletionOfEditingEvent((object) this, EventArgs.Empty);
  }

  /// <summary>Проверка возможности редактирования атрибута.</summary>
  /// <param name="av">Значение атрибута</param>
  /// <param name="options">Опции</param>
  /// <returns>Результат проверки</returns>
  private bool IsEnabled(AttributeValues av, AttributeOptions options)
  {
    bool flag = av != null && !av.ReadOnly;
    if (flag)
      flag = !this.DisabledInDesign && (options & AttributeOptions.DisableManualEdit) != AttributeOptions.DisableManualEdit;
    return flag;
  }

  /// <summary>
  /// Метод используется как заплатка.
  /// Лучше список значений задавать через ParentInfo.
  /// Но если уже выхода нет, например в случае, когда нужно атрибут присвоить нескольким объектам разного типа, то можно использовать этот метод (для этого впринципе и написан).
  /// По хорошему лучше подумать как от этого избавиться.
  /// </summary>
  private void SetPossibleValuesWithoutParentInfo()
  {
    if (this._attrInfo == null)
      return;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this._attrInfo.AttributeGuid);
    if (this._possibleValues == null)
    {
      this._possibleValues = new DataTable();
      this._possibleValues.Columns.Add(attributeType.PossibleValueFieldName);
      this._possibleValues.Columns.Add("F_DESCRIPTION");
    }
    DataRow row = this._possibleValues.NewRow();
    row[attributeType.PossibleValueFieldName] = (object) DBNull.Value;
    row["F_DESCRIPTION"] = (object) string.Empty;
    this._possibleValues.Rows.Add(row);
    this._possibleValues.CaseSensitive = true;
    this.DataSource = (object) this._possibleValues;
    this.DisplayMember = "F_DESCRIPTION";
    this.ValueMember = attributeType.PossibleValueFieldName;
  }

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont()
  {
    return this.Parent != null && !this.Parent.Font.Equals((object) this.Font);
  }

  protected override void OnDropDown(EventArgs e)
  {
    this.SetHorizontalExtent();
    base.OnDropDown(e);
  }

  [DllImport("user32.dll")]
  private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

  protected override CreateParams CreateParams
  {
    get
    {
      CreateParams createParams = base.CreateParams;
      createParams.Style |= 1048576 /*0x100000*/;
      return createParams;
    }
  }

  protected void SetHorizontalExtent()
  {
    int width = 0;
    foreach (object obj in this.Items)
    {
      Size size = TextRenderer.MeasureText(obj is DataRowView ? ((DataRowView) obj).Row[this.DisplayMember].ToString() : obj.ToString(), this.Font);
      if (size.Width > width)
        width = size.Width;
    }
    this.SetHorizontalExtent(width);
  }

  protected void SetHorizontalExtent(int width)
  {
    AttrComboBox.SendMessage(this.Handle, 350U, new IntPtr(width), IntPtr.Zero);
  }
}
