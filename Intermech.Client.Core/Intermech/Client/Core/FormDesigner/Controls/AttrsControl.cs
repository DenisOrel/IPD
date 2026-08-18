
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrsControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Базовый класс для контролов с атрибутом.</summary>
public class AttrsControl : 
  UserControl,
  IAttributeEditor,
  IBaseDesForm,
  IAttributeEditorModified,
  ICompletionOfEditing,
  IDataFormatError,
  IExtendedParent4Control,
  IParent4Control,
  IIMControlEnabled,
  ILockModify
{
  /// <summary>Панель, отображающая кнопки</summary>
  protected PanelForControlButtons _buttons = new PanelForControlButtons();
  private AttrsControlButtonRenderer _btnRenderer = new AttrsControlButtonRenderer();
  /// <summary>Замена Guid'а атрибута</summary>
  protected AttributeInfo _attrInfo;
  /// <summary>
  /// Класс, содержащий идентификатор(ы) атрибута + его значение(я)
  /// </summary>
  protected AttributeValues _attrValues;
  /// <summary>Возможность атрибута иметь пустое значение</summary>
  protected bool _disableNulls;
  /// <summary>Наличие изменений в контроле</summary>
  protected bool _modified;
  protected bool _enabled = true;
  /// <summary>Флаг необходимости расшифровки значения атрибута</summary>
  protected bool _bNeedDescription;
  /// <summary>Возможные значения атрибута</summary>
  protected DataTable _possibleValues;
  protected string _colKey = "Key";
  protected string _colDesc = "Description";
  /// <summary>Атрибут '{0}' не может содержать пустые значения.</summary>
  protected string _errMsg_NullValue = string.Empty;
  protected string _designText = string.Empty;
  /// <summary>
  /// Флаг, о рассылке уведомления об изменениии значений в контроле
  /// </summary>
  protected bool _needNotify;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected ToolTip _toolTip;
  protected ErrorProvider _err;

  /// <summary>Установить сообщение об ошибке.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected string Error
  {
    get => this._err.GetError((Control) this);
    set => this._err.SetError((Control) this, value);
  }

  /// <summary>Режим разработки.</summary>
  protected bool IsDesignMode => this.Site != null && this.Site.DesignMode;

  /// <summary>
  /// 
  /// </summary>
  protected virtual bool ValueIsEmpty
  {
    get
    {
      return this._attrValues == null || this._attrValues.Values == null || this._attrValues.Values.Length == 0 || this._attrValues.Values[0] == null || this._attrValues.Values[0] == DBNull.Value;
    }
  }

  /// <summary>Конструктор.</summary>
  public AttrsControl()
  {
    this.InitializeComponent();
    this.CanAddAttribute = true;
    this.ParentPoint = AttributeDestinationPoint.Default;
    this._err.SetError((Control) this, string.Empty);
  }

  /// <summary>Guid атрибута и типа объекта/связи.</summary>
  [Browsable(false)]
  [DefaultValue(null)]
  public virtual AttributeInfo AttributeInfo
  {
    get => this._attrInfo;
    set
    {
      this.ClearAttributeInfoCachedOptions();
      this._attrInfo = value == null || !(value.AttributeGuid != Guid.Empty) || !MetaDataHelper.ExistsAttributeType(value.AttributeGuid) ? (AttributeInfo) null : value;
      if (!this.IsDesignMode)
        return;
      this.EnabledCtrl = this._attrInfo != null;
      this.SetDesignText(this._attrInfo != null ? MetaDataHelper.GetAttributeTypeName(this._attrInfo.AttributeGuid) : string.Empty);
      this.Invalidate();
    }
  }

  /// <summary>
  /// Очистка закэшированных опций атрибута применительно к типу объекта/связи,
  /// которые не тягаются вместе с AttributeInfo, но могут быть дополнительно (и однократно) зачитаны в процессе работы контрола
  /// </summary>
  protected virtual void ClearAttributeInfoCachedOptions()
  {
  }

  /// <summary>
  /// Устанавливает и возвращает возможность добавления атрибута к объекту.
  /// </summary>
  [Browsable(false)]
  [DefaultValue(true)]
  public virtual bool CanAddAttribute { get; set; }

  /// <summary>Значение атрибута.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public virtual AttributeValues Values
  {
    [DebuggerStepThrough] get
    {
      if (this._attrValues != null)
        this._attrValues.Values = this.GetValues;
      return this._attrValues;
    }
    set
    {
      this._attrValues = value;
      AttributeOptions options = AttributeOptions.None;
      if (value != null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          options = this.GetAttributeOptions(value.AttributeID, sessionKeeper.Session);
          this._bNeedDescription = (options & AttributeOptions.GetDescriptionEvent) != 0;
          this._disableNulls = (options & AttributeOptions.DisableNulls) != 0;
          if (this._disableNulls)
          {
            string attributeTypeName = MetaDataHelper.GetAttributeTypeName(value.AttributeGuid);
            this._errMsg_NullValue = string.Format(LocalizationHolder.rm.GetString("Controls_NullValue_ErrorMessage"), (object) attributeTypeName);
          }
        }
      }
      this.EnabledCtrl = this.IsEnabled(value, options);
      this.Error = !this._disableNulls || !this.EnabledCtrl || !this.ValueIsEmpty ? string.Empty : this._errMsg_NullValue;
    }
  }

  /// <summary>Проверка возможности редактирования атрибута.</summary>
  /// <param name="av">Значение атрибута</param>
  /// <param name="options">Опции</param>
  /// <returns>Результат проверки</returns>
  private bool IsEnabled(AttributeValues av, AttributeOptions options)
  {
    bool flag = av != null && !av.ReadOnly;
    if (flag)
      flag = !this.DisabledInDesign && (options & AttributeOptions.DisableManualEdit) == AttributeOptions.None;
    return flag;
  }

  /// <summary>Установка допустимых значений.</summary>
  /// <param name="data">DataTable со значениями</param>
  /// <param name="possibleValueFieldName"></param>
  /// <param name="descriptionFieldName"></param>
  public virtual void SetPossibleValues(
    DataTable data,
    string possibleValueFieldName,
    string descriptionFieldName)
  {
    if (data == null || data.Rows.Count <= 0 || string.IsNullOrEmpty(possibleValueFieldName) || string.IsNullOrEmpty(descriptionFieldName))
      return;
    string empty = string.Empty;
    foreach (DataRow row in (InternalDataCollectionBase) data.Rows)
    {
      string str = Convert.ToString(row[descriptionFieldName]);
      row[descriptionFieldName] = string.IsNullOrEmpty(str) ? (object) Convert.ToString(row[possibleValueFieldName]) : (object) str;
    }
    data.DefaultView.Sort = descriptionFieldName;
    data.Columns[possibleValueFieldName].ColumnName = this._colKey;
    data.Columns[descriptionFieldName].ColumnName = this._colDesc;
    this._possibleValues = data;
  }

  /// <summary>
  /// Устанавливает и возвращает произошло ли изменение данных.
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool Modified
  {
    get => this._modified;
    set
    {
      if (this.LockModify || this._attrValues == null)
        return;
      this._modified = value;
      this._needNotify = true;
      if (!this._modified)
        return;
      this.OnModified();
    }
  }

  /// <summary>Событие на изменение данных в контроле.</summary>
  public event EventHandler ModifiedEvent;

  /// <summary>
  /// 
  /// </summary>
  private void OnModified()
  {
    if (this.ModifiedEvent == null)
      return;
    this.ModifiedEvent((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool LockModify { get; set; }

  /// <summary>Устанавливает родительскую форму.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual DesForm DesForm { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler CompletionOfEditingEvent;

  /// <summary>
  /// 
  /// </summary>
  protected virtual void OnCompletionOfEditing()
  {
    if (this.CompletionOfEditingEvent == null)
      return;
    this.CompletionOfEditingEvent((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsDataFormatError => !string.IsNullOrEmpty(this.Error);

  /// <summary>Запретить редактирование данных.</summary>
  [DefaultValue(false)]
  public bool DisabledInDesign { get; set; }

  /// <summary>Доступность контрола.</summary>
  [Browsable(false)]
  [DefaultValue(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool EnabledCtrl
  {
    get => this._enabled;
    set => this.Enabled = this._enabled = value;
  }

  /// <summary>Устанавливает родителя для атрибута.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual IElementInfo ParentInfo { get; set; }

  /// <summary>Для чего нужен контрол.</summary>
  [Browsable(false)]
  [DefaultValue(AttributeDestinationPoint.Default)]
  public virtual AttributeDestinationPoint ParentPoint { get; set; }

  /// <summary>Идентификатор типа объекта/связи.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int ParentTypeID { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLeave(EventArgs e)
  {
    base.OnLeave(e);
    this.OnLeaveControl(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    if (this.IsDesignMode)
      return;
    this._buttons.MouseDown(e);
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    if (this.IsDesignMode)
      return;
    this._buttons.MouseLeave(e);
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (this.IsDesignMode)
      return;
    this._buttons.MouseMove(e);
    this._toolTip.SetToolTip((Control) this, this._buttons.Hint);
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    this._buttons.Bounds = new Rectangle(this._buttons.RightButtons ? this.Width - this.Padding.Right : this.Padding.Left, 0, this._buttons.Width, this._buttons.Height);
    this._btnRenderer.Draw(e.Graphics, this._buttons.Bounds.Location, (List<ControlButton>) this._buttons);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnParentChanged(EventArgs e)
  {
    base.OnParentChanged(e);
    this.DesForm = this.Parent as DesForm;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnSizeChanged(EventArgs e)
  {
    base.OnSizeChanged(e);
    this.Invalidate();
  }

  /// <summary>
  /// Добавить кнопку на панель (справа от основного элемента).
  /// </summary>
  /// <param name="button">Кнопка</param>
  /// <param name="needSort">Необходимость сортировки массива после вставки элемента</param>
  protected void AddRightButton(ControlButton button, bool needSort = false)
  {
    this._buttons.AddButton(button, needSort);
    this._buttons.RightButtons = true;
    Padding padding = this.Padding;
    int left = padding.Left;
    padding = this.Padding;
    int top = padding.Top;
    int width = this._buttons.Width;
    padding = this.Padding;
    int bottom = padding.Bottom;
    this.Padding = new Padding(left, top, width, bottom);
    this.Invalidate();
  }

  /// <summary>
  /// Добавить кнопки на панель (справа от основного элемента).
  /// </summary>
  /// <param name="buttons">Список кнопок</param>
  /// <param name="needSort">Необходимость сортировки массива после вставки элемента</param>
  protected void AddRightButtons(List<ControlButton> buttons, bool needSort = false)
  {
    this._buttons.AddButtons(buttons, needSort);
    this._buttons.RightButtons = true;
    Padding padding = this.Padding;
    int left = padding.Left;
    padding = this.Padding;
    int top = padding.Top;
    int width = this._buttons.Width;
    padding = this.Padding;
    int bottom = padding.Bottom;
    this.Padding = new Padding(left, top, width, bottom);
    this.Invalidate();
  }

  /// <summary>
  /// Добавить кнопку на панель (сверху от основного элемента).
  /// </summary>
  /// <param name="button"></param>
  protected void AddTopButton(ControlButton button)
  {
    this._buttons.AddButton(button);
    this._buttons.RightButtons = false;
    Padding padding = this.Padding;
    int left = padding.Left;
    int height = this._buttons.Height;
    padding = this.Padding;
    int right = padding.Right;
    padding = this.Padding;
    int bottom = padding.Bottom;
    this.Padding = new Padding(left, height, right, bottom);
    this.Invalidate();
  }

  /// <summary>
  /// Добавить кнопки на панель (сверху от основного элемента).
  /// </summary>
  /// <param name="buttons">Список кнопок</param>
  protected void AddTopButtons(List<ControlButton> buttons)
  {
    this._buttons.AddButtons(buttons);
    this._buttons.RightButtons = false;
    Padding padding = this.Padding;
    int left = padding.Left;
    int height = this._buttons.Height;
    padding = this.Padding;
    int right = padding.Right;
    padding = this.Padding;
    int bottom = padding.Bottom;
    this.Padding = new Padding(left, height, right, bottom);
    this.Invalidate();
  }

  /// <summary>Удалить кнопку с панели.</summary>
  /// <param name="button">Кнопка</param>
  protected void RemoveRightButton(ControlButton button)
  {
    this._buttons.Remove(button);
    Padding padding = this.Padding;
    int left = padding.Left;
    padding = this.Padding;
    int top = padding.Top;
    int width = this._buttons.Width;
    padding = this.Padding;
    int bottom = padding.Bottom;
    this.Padding = new Padding(left, top, width, bottom);
    this.Invalidate();
  }

  /// <summary>Удалить кнопки с панели.</summary>
  /// <param name="buttons">Список кнопок</param>
  protected void RemoveRightButtons(List<ControlButton> buttons)
  {
    this._buttons.RemoveButtons(buttons);
    Padding padding = this.Padding;
    int left = padding.Left;
    padding = this.Padding;
    int top = padding.Top;
    int width = this._buttons.Width;
    padding = this.Padding;
    int bottom = padding.Bottom;
    this.Padding = new Padding(left, top, width, bottom);
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  protected virtual object[] GetValues
  {
    get => new object[1]{ (object) DBNull.Value };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected virtual void OnLeaveControl(EventArgs e)
  {
    if (!this._needNotify || this.DesForm == null || this._attrValues == null)
      return;
    this.DesForm.AttributeChanging(this._attrValues.AttributeID, this._attrValues.Values, this.GetValues, this.ParentPoint == AttributeDestinationPoint.Default);
    this._needNotify = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  protected virtual void SetDesignText(string text)
  {
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.DesForm = (DesForm) null;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrsControl));
    this._err = new ErrorProvider(this.components);
    this._toolTip = new ToolTip(this.components);
    ((ISupportInitialize) this._err).BeginInit();
    this.SuspendLayout();
    this._err.BlinkStyle = ErrorBlinkStyle.NeverBlink;
    this._err.ContainerControl = (ContainerControl) this;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.DoubleBuffered = true;
    this._err.SetError((Control) this, componentResourceManager.GetString("$this.Error"));
    this._err.SetIconAlignment((Control) this, (ErrorIconAlignment) componentResourceManager.GetObject("$this.IconAlignment"));
    this._err.SetIconPadding((Control) this, (int) componentResourceManager.GetObject("$this.IconPadding"));
    this.Name = nameof (AttrsControl);
    ((ISupportInitialize) this._err).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>
  /// 
  /// </summary>
  protected enum ButtonOrderIndex
  {
    Dots,
    Calc,
    ReCalc,
    Add,
    Del,
    Edit,
    Clean,
    Form,
  }
}
