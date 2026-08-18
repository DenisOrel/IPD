
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrCheckedListBox
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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Контрол-редактор списка значение из списка разрешенных.
/// </summary>
public class AttrCheckedListBox : 
  CheckedListBox,
  IAttributeEditor,
  IBaseDesForm,
  IAttributeEditorModified,
  IExtendedParent4Control,
  IParent4Control,
  IIMControlEnabled,
  ILockModify
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ErrorProvider _err;
  private DesForm _parentForm;
  /// <summary>Замена Guid'а атрибута</summary>
  private AttributeInfo _attrInfo;
  /// <summary>
  /// Класс, содержащий идентификатор(ы) атрибута + его значение(я)
  /// </summary>
  private AttributeValues _attrValues;
  /// <summary>Возможность атрибута иметь пустое значение</summary>
  private bool _disableNulls;
  private string _errMsg_NullValue = string.Empty;
  private bool _enabled = true;
  /// <summary>
  /// Флаг, о рассылке уведомления об изменениии значений в контроле
  /// </summary>
  private bool _needNotify;

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
    this.HorizontalScrollbar = true;
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

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(true)]
  public new bool CheckOnClick
  {
    get => base.CheckOnClick;
    set => base.CheckOnClick = value;
  }

  /// <summary>
  /// Перекрыто для того, чтобы не сериализовать это свойство.
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new bool HorizontalScrollbar
  {
    get => base.HorizontalScrollbar;
    set => base.HorizontalScrollbar = value;
  }

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
      if (this.CheckedItems.Count != 0)
        return (object[]) this.CheckedItems.Cast<AttrCheckedListBox.CheckedListBoxItem>().Select<AttrCheckedListBox.CheckedListBoxItem, string>((System.Func<AttrCheckedListBox.CheckedListBoxItem, string>) (x => x.Value)).ToArray<string>();
      return new object[1]{ (object) DBNull.Value };
    }
  }

  /// <summary>Конструктор.</summary>
  public AttrCheckedListBox()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this.CanAddAttribute = true;
    this.ParentPoint = AttributeDestinationPoint.Default;
    this.CheckOnClick = true;
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
      string str1 = this._attrInfo != null ? MetaDataHelper.GetAttributeTypeName(this._attrInfo.AttributeGuid) : string.Empty;
      this.Items.Clear();
      string str2 = !string.IsNullOrEmpty(str1) ? str1 : this.Name;
      this.Items.Add((object) new AttrCheckedListBox.CheckedListBoxItem()
      {
        Text = str2
      });
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
      if (value != null)
      {
        List<string> list = ((IEnumerable<object>) value.Values).Where<object>((System.Func<object, bool>) (x => x != null && x != DBNull.Value)).Select<object, string>((System.Func<object, string>) (x => Convert.ToString(x))).ToList<string>();
        for (int index = 0; index < this.Items.Count; ++index)
          this.SetItemChecked(index, list.Contains((this.Items[index] as AttrCheckedListBox.CheckedListBoxItem).Value));
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          options = this.GetAttributeOptions(value.AttributeID, sessionKeeper.Session);
          this._disableNulls = (options & AttributeOptions.DisableNulls) != 0;
          if (this._disableNulls)
          {
            string attributeTypeName = MetaDataHelper.GetAttributeTypeName(value.AttributeGuid);
            this._errMsg_NullValue = string.Format(LocalizationHolder.rm.GetString("Controls_NullValue_ErrorMessage"), (object) attributeTypeName);
          }
        }
      }
      else
        this.Items.Clear();
      this.EnabledCtrl = this.IsEnabled(value, options);
      this._err.SetError((Control) this, !this._disableNulls || !this.EnabledCtrl || this.CheckedItems.Count != 0 ? string.Empty : this._errMsg_NullValue);
    }
  }

  /// <summary>Установить возможные значения.</summary>
  /// <param name="data"></param>
  /// <param name="possibleValueFieldName"></param>
  /// <param name="descriptionFieldName"></param>
  public void SetPossibleValues(
    DataTable data,
    string possibleValueFieldName,
    string descriptionFieldName)
  {
    this.BeginUpdate();
    try
    {
      this.Items.Clear();
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      foreach (DataRow row in (InternalDataCollectionBase) data.Rows)
      {
        string str1 = Convert.ToString(row[possibleValueFieldName]);
        if (!string.IsNullOrEmpty(str1))
        {
          string str2 = Convert.ToString(row[descriptionFieldName]);
          string str3 = string.IsNullOrEmpty(str2) ? str1 : str2;
          this.Items.Add((object) new AttrCheckedListBox.CheckedListBoxItem()
          {
            Text = str3,
            Value = str1
          });
        }
      }
    }
    finally
    {
      this.EndUpdate();
    }
  }

  /// <summary>Устанавливает родительскую форму.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
  public IElementInfo ParentInfo { get; set; }

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
    set
    {
      if (this.Site != null && this.Site.DesignMode)
        return;
      this._enabled = value;
      if (!value)
      {
        Color color = this.BackColor;
        int argb1 = color.ToArgb();
        color = Color.White;
        int argb2 = color.ToArgb();
        if (argb1 != argb2)
          return;
        this.BackColor = SystemColors.Control;
      }
      else
      {
        if (!(this.BackColor == SystemColors.Control))
          return;
        this.BackColor = Color.White;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ice"></param>
  protected override void OnItemCheck(ItemCheckEventArgs ice)
  {
    base.OnItemCheck(ice);
    if (this.LockModify)
      return;
    if (!this._enabled)
    {
      ice.NewValue = ice.CurrentValue;
    }
    else
    {
      this.Modified = true;
      this._needNotify = true;
      if (this.ModifiedEvent != null)
        this.ModifiedEvent((object) this, EventArgs.Empty);
      if (!this._disableNulls)
        return;
      this._err.SetError((Control) this, this.CheckedItems.Count != 1 || ice.NewValue != CheckState.Unchecked ? string.Empty : this._errMsg_NullValue);
    }
  }

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

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont()
  {
    return this.Parent != null && !this.Parent.Font.Equals((object) this.Font);
  }

  /// <summary>
  /// 
  /// </summary>
  private class CheckedListBoxItem
  {
    /// <summary>
    /// 
    /// </summary>
    internal string Text { get; set; }

    /// <summary>
    /// 
    /// </summary>
    internal string Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.Text;
  }
}
