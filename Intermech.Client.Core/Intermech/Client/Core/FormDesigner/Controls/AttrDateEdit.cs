
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrDateEdit
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
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public class AttrDateEdit : 
  IMDateTimeCtrl,
  IAttributeEditor,
  IBaseDesForm,
  IAttributeEditorModified,
  ICompletionOfEditing,
  IExtendedParent4Control,
  IParent4Control,
  IIMControlEnabled,
  ILockModify
{
  private const string _onlyDateFormat = "dd.MM.yyyy";
  private DesForm _parentForm;
  /// <summary>Замена Guid'а атрибута</summary>
  private AttributeInfo _attrInfo;
  private IElementInfo _parentInfo;
  /// <summary>
  /// Класс, содержащий идентификатор(ы) атрибута + его значение(я)
  /// </summary>
  private AttributeValues _attrValues;
  private AttributeOptions _options;
  /// <summary>
  /// Маска ввода, назначенная типу атрибута или атрибуту типа объектов
  /// </summary>
  private string _attributeFormat = string.Empty;
  private bool _onlyDateCheck;
  /// <summary>Возможность атрибута иметь пустое значение</summary>
  private bool _disableNulls;
  /// <summary>Сообщение о недопустимости пустого значения</summary>
  private string _errMsg_NullValue = string.Empty;
  /// <summary>
  /// Флаг, о рассылке уведомления об изменениии значений в контроле
  /// </summary>
  private bool _needNotify;

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Font CalendarFont { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Color CalendarForeColor { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Color CalendarMonthBackground { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Color CalendarTitleBackColor { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Color CalendarTitleForeColor { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Color CalendarTrailingForeColor { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Checked { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime MaxDate { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime MinDate { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool RightToLeftLayout { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ShowCheckBox { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ShowUpDown { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime Value { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public override Color BackColor
  {
    get => base.BackColor;
    set => base.BackColor = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public new Padding Padding
  {
    get => base.Padding;
    set => base.Padding = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  public string Hint { get; set; }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  private object[] GetValues
  {
    get
    {
      object obj = (object) DBNull.Value;
      DateTime dt = DateTime.MinValue;
      if (this.ConvertFromStringToDateTime(this.TextValue, out dt) && dt != DateTime.MinValue)
        obj = (object) dt;
      return new object[1]{ obj };
    }
  }

  /// <summary>Конструктор.</summary>
  public AttrDateEdit()
  {
    this.Name = string.Empty;
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
      IMSAttributeType attributeType = value != null ? MetaDataHelper.GetAttributeType(value.AttributeGuid) : (IMSAttributeType) null;
      if (attributeType != null)
      {
        this._attrInfo = value;
        if (attributeType.Mask == Consts.OnlyDateFunction)
        {
          this._onlyDateCheck = true;
          this._attributeFormat = "dd.MM.yyyy";
        }
        else
        {
          this._onlyDateCheck = false;
          this._attributeFormat = attributeType.Mask;
        }
        this._options = attributeType.Options;
        this._options &= ~AttributeOptions.DisableNulls;
      }
      else
      {
        this._attrInfo = (AttributeInfo) null;
        this._onlyDateCheck = false;
        this._attributeFormat = string.Empty;
      }
      this.UpdateCurrentFormat();
      if (!this.IsDesignMode)
        return;
      this.LockModify = true;
      try
      {
        this.DateTimeValue = DateTime.Now;
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
      if (this._disableNulls)
        this._errMsg_NullValue = string.Format(LocalizationHolder.rm.GetString("Controls_NullValue_ErrorMessage"), (object) MetaDataHelper.GetAttributeTypeName(this._attrValues.AttributeID));
      this.EnabledCtrl = this.IsEnabled(value, this._options);
      DateTime result = DateTime.MinValue;
      if (value != null)
        DateTime.TryParse(Convert.ToString(value.Values[0]), out result);
      this.DateTimeValue = result;
      this.CheckTextValue();
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
    get => this._parentInfo;
    set
    {
      this._parentInfo = value;
      if (value == null || this._attrInfo == null)
        return;
      IMSAttribute4 imsAttribute4 = (IMSAttribute4) null;
      if (value.ElementKind == AttributableElements.Object)
      {
        if (value.ElementIdentifier != 0L)
        {
          QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(value.ElementIdentifier);
          if (!objectInfo.Empty)
          {
            this.ParentTypeID = objectInfo.ObjectTypeID;
            imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4ObjectType(MetaDataHelper.GetObjectTypeGuid(this.ParentTypeID), this._attrInfo.AttributeGuid);
          }
          else
          {
            this.ParentTypeID = -1;
            imsAttribute4 = (IMSAttribute4) null;
          }
        }
      }
      else if (value.ElementKind == AttributableElements.Relation && value.ElementIdentifier != 0L)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(value.ElementIdentifier, false);
          if (relation != null)
          {
            this.ParentTypeID = relation.RelationType;
            imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4RelationType(MetaDataHelper.GetRelationTypeGuid(this.ParentTypeID), this._attrInfo.AttributeGuid);
          }
          else
          {
            this.ParentTypeID = -1;
            imsAttribute4 = (IMSAttribute4) null;
          }
        }
      }
      if (imsAttribute4 != null)
      {
        if (imsAttribute4.Mask == Consts.OnlyDateFunction)
        {
          this._onlyDateCheck = true;
          this._attributeFormat = "dd.MM.yyyy";
        }
        else
        {
          this._onlyDateCheck = false;
          this._attributeFormat = imsAttribute4.Mask;
        }
        this._options = imsAttribute4.Options;
        if (imsAttribute4.Required == RequiredModes.Manual)
          this._options &= ~AttributeOptions.DisableNulls;
      }
      this._disableNulls = (this._options & AttributeOptions.DisableNulls) != 0;
      this.UpdateCurrentFormat();
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
  /// <remarks>
  /// Нужно для случая, когда контрол не связан с атрибутом.
  /// В таком случае, при открытии формы, ничего в контроле не должно отображаться.
  /// </remarks>
  protected override void OnHandleCreated(EventArgs e)
  {
    base.OnHandleCreated(e);
    if (!this.IsDesignMode)
      return;
    this.DateTimeValue = DateTime.Now;
  }

  /// <summary>Проверка на наличие ошибок.</summary>
  /// <returns>Строка с текстом сообщения об ошибке</returns>
  protected override string CheckError()
  {
    return !string.IsNullOrEmpty(this.TextValue) || !this._disableNulls || !this.Enabled ? string.Empty : this._errMsg_NullValue;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void OnLeave()
  {
    base.OnLeave();
    if (!this._needNotify || this._parentForm == null)
      return;
    if (!this.IsDataFormatError && this._attrValues != null)
      this._parentForm.AttributeChanging(this._attrValues.AttributeID, this._attrValues.Values, this.GetValues, this.ParentPoint == AttributeDestinationPoint.Default);
    this._needNotify = false;
  }

  /// <summary>Изменение текста.</summary>
  protected override void OnTextChanged()
  {
    base.OnTextChanged();
    if (!this.LockModify)
    {
      this.Modified = true;
      this._needNotify = true;
      this.OnModifiedEvent(EventArgs.Empty);
    }
    this.OnCompletionOfEditingEvent(EventArgs.Empty);
  }

  /// <summary>Обновление текущего формата данных.</summary>
  protected override void UpdateCurrentFormat()
  {
    base.UpdateCurrentFormat();
    if (this.Format != DateTimePickerFormat.Custom)
      return;
    this.CurrentFormat = this._attributeFormat;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this._parentForm = (DesForm) null;
    base.Dispose(disposing);
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
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected virtual void OnModifiedEvent(EventArgs e)
  {
    if (this.ModifiedEvent == null)
      return;
    this.ModifiedEvent((object) this, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected virtual void OnCompletionOfEditingEvent(EventArgs e)
  {
    if (this.CompletionOfEditingEvent == null)
      return;
    this.CompletionOfEditingEvent((object) this, e);
  }

  /// <summary>Необходимость сериализации свойства BackColor.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeBackColor() => false;

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont()
  {
    return this.Parent != null && !this.Parent.Font.Equals((object) this.Font);
  }

  /// <summary>Необходимость сериализации свойства Padding.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializePadding() => false;
}
