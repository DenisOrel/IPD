
// Type: Intermech.Client.Core.Organizer.OrganizerTaskCtrl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Форма для создания объектов типа "Задачи органайзера".
/// Если данная форма будет создана с помощью средств разработки IPS то эту форму можно будет удалить.
/// </summary>
[ToolboxItem(false)]
public class OrganizerTaskCtrl : ObjectCreatorControl
{
  private long _objID;
  private long _userID;
  private int _relTypeID = -1;
  private string _nullFormat = string.Empty;
  private string _customFormat = "  dd MMMM yyyy   H:mm";
  private string _setDateMsg = string.Empty;
  private string _setDateCaption = string.Empty;
  private bool _isChanged;
  /// <summary>
  /// При обнулении значений контролов возникает событие на изменение значения.
  /// В связи с этим возникает моргание кнопок "Применить" и "Отмена".
  /// Поэтому на время обнуления заблокируем установку флага на изменение.
  /// </summary>
  private bool _lockChanges;
  private bool _enabledControls;
  private object _categoryDefaultValue;
  private object _relevanceDefaultValue;
  private object _repetitionDefaultValue;
  private object _stateDefaultValue;
  private object _repetitionCurrentValue;
  private DateTime _dateStartCurrentValue = DateTime.MinValue;
  private DateTime _dateFinishCurrentValue = DateTime.MinValue;
  private string _strDaily = LocalizationHolder.rm.GetString("Client_Core_Daily");
  private string _strMonthly = LocalizationHolder.rm.GetString("Client_Core_Monthly");
  private string _strWeekly = LocalizationHolder.rm.GetString("Client_Core_Weekly");
  private string _strYearly = LocalizationHolder.rm.GetString("Client_Core_Yearly");
  private Guid _addresseeNoticeGuid = new Guid("cad00628-306c-11d8-b4e9-00304f19f545");
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label _lbSubject;
  private Panel _pnlSubject;
  private TextBox _txtSubject;
  private GroupBox _gbSeparator1;
  private GroupBox _gbSeparator2;
  private Panel _pnlRecipient;
  private Label _lbRecipient;
  private ToolTip _tt;
  private Label _lbState;
  private Panel _pnlReminder;
  private DateTimePicker _dtReminder;
  private CheckBox _chbReminder;
  private ComboBox _cmbState;
  private Label _lbDateStart;
  private Label _lbDateFinish;
  private DateTimePicker _dtDateStart;
  private DateTimePicker _dtDateFinish;
  private Label _lbRelevance;
  private ComboBox _cmbRelevance;
  private Panel _pnlDate;
  private RichTextBox _rtbText;
  private ComboBox _cmbCategory;
  private ComboBox _cmbRepetition;
  private Label _lbCategory;
  private Label _lbRepetition;
  private Label _lbRepetitionMsg;
  private AttrListBoxBtn _lstRecipients;
  private GroupBox _gbSeparator3;

  /// <summary>Дата напоминания о задаче органайзера.</summary>
  /// <remarks>Т.к. атрибут может содержать пустое значение, то тип возвращаемого значения object</remarks>
  public object DateReminder
  {
    get
    {
      object dateReminder = (object) DBNull.Value;
      if (this._dtReminder.CustomFormat != this._nullFormat)
        dateReminder = (object) this._dtReminder.Value.Subtract(new TimeSpan(0, 0, this._dtReminder.Value.Second));
      return dateReminder;
    }
  }

  /// <summary>Наличие изменений.</summary>
  public bool IsChanged
  {
    get => this._isChanged;
    private set
    {
      this._isChanged = value;
      if (!value || this.Modified == null)
        return;
      this.Modified((object) this, (EventArgs) null);
    }
  }

  /// <summary>Напоминание о задаче органайзера.</summary>
  public bool Reminder => this._chbReminder.Checked;

  /// <summary>Конструктор.</summary>
  /// <param name="objID">Идентификатор объекта</param>
  public OrganizerTaskCtrl(long objID)
  {
    this.InitializeComponent();
    this._objID = objID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(sessionKeeper.Session.UserID);
      if (!objectInfo.Empty)
        this._userID = objectInfo.ID;
    }
    this._relTypeID = MetaDataHelper.GetRelationTypeID("cadd938e-306c-11d8-b4e9-00304f19f545");
    this._lstRecipients.AttributeInfo = new AttributeInfo(this._addresseeNoticeGuid, Guid.Empty);
    this._dtDateFinish.CustomFormat = this._dtDateStart.CustomFormat = this._nullFormat = LocalizationHolder.rm.GetString("Organizer_NullDateFormat");
    this._setDateMsg = LocalizationHolder.rm.GetString("Organizer_FinishDate_LessStartDateMessage");
    this._setDateCaption = LocalizationHolder.rm.GetString("Organizer_OrganizerTask");
    this.LoadPossibleValues();
  }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler Modified;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lstRecipients_ModifiedEvent(object sender, EventArgs e)
  {
    if (!(sender is IAttributeEditorModified))
      return;
    this.IsChanged = (sender as IAttributeEditorModified).Modified;
    if (!this.IsChanged)
      return;
    this.On_ValueChanged((object) this._lstRecipients, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_chbReminder_CheckStateChanged(object sender, EventArgs e)
  {
    if (this._dtReminder.Enabled = (sender as CheckBox).Checked)
    {
      OrganizerService service = ServicesManager.GetService(typeof (IOrganizerService)) as OrganizerService;
      this._dtReminder.Value = this._dtDateStart.Value.AddMinutes((double) -(service != null ? service.TimeBeforeReminder : 30));
      this._dtReminder.CustomFormat = this._customFormat;
    }
    else
      this._dtReminder.CustomFormat = this._nullFormat;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmbRepetition_SelectedValueChanged(object sender, EventArgs e)
  {
    if (this._lockChanges)
      return;
    short int16 = Convert.ToInt16(this._cmbRepetition.SelectedValue);
    string str1 = string.Empty;
    if ((int) int16 == (int) Convert.ToInt16((object) Repetition.Once))
    {
      this._lbRepetitionMsg.Visible = false;
      this._repetitionCurrentValue = (object) Convert.ToInt16((object) Repetition.Once);
      (sender as Control).Tag = (object) 1;
      this.IsChanged = true;
    }
    else
    {
      if ((int) int16 == (int) Convert.ToInt16((object) Repetition.Daily))
        str1 = this._strDaily;
      else if ((int) int16 == (int) Convert.ToInt16((object) Repetition.Weekly))
        str1 = this._strWeekly;
      else if ((int) int16 == (int) Convert.ToInt16((object) Repetition.Monthly))
        str1 = this._strMonthly;
      else if ((int) int16 == (int) Convert.ToInt16((object) Repetition.Yearly))
        str1 = this._strYearly;
      if (!this.ValidationRepetitionValue(this._dtDateStart.Value, this._dtDateFinish.Value, (int) int16))
      {
        this._lockChanges = true;
        this._cmbRepetition.SelectedValue = this._repetitionCurrentValue;
        this._lockChanges = false;
      }
      else
      {
        this._repetitionCurrentValue = (object) int16;
        string str2 = this._dtDateStart.Value.ToString(this._customFormat);
        this._lbRepetitionMsg.Text = string.Format(LocalizationHolder.rm.GetString("Organizer_Repetition_RangeMsg"), (object) str1, (object) str2);
        this._lbRepetitionMsg.Visible = true;
        (sender as Control).Tag = (object) 1;
        this.IsChanged = true;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_dt_CloseUp(object sender, EventArgs e)
  {
    DateTimePicker ctrl = sender as DateTimePicker;
    string customFormat1 = ctrl.CustomFormat;
    this.SetDate(ctrl, (object) ctrl.Value);
    if (this._lbRepetitionMsg.Visible)
      this.On_cmbRepetition_SelectedValueChanged((object) this._cmbRepetition, new EventArgs());
    string customFormat2 = ctrl.CustomFormat;
    if (customFormat1 == customFormat2)
      return;
    this.IsChanged = true;
  }

  /// <summary>Нажатие клавиши.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_dt_KeyUp(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete)
      return;
    this.SetDate(sender as DateTimePicker, (object) null);
    if (sender as DateTimePicker == this._dtDateFinish)
      this._dtDateStart.Tag = (object) 1;
    this.On_ValueChanged(sender, (EventArgs) e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_dt_Leave(object sender, EventArgs e)
  {
    DateTimePicker ctrl = sender as DateTimePicker;
    if (!this.IsChanged || string.Compare(ctrl.CustomFormat, this._nullFormat) == 0)
      return;
    string customFormat1 = ctrl.CustomFormat;
    this.SetDate(ctrl, (object) ctrl.Value);
    if (this._lbRepetitionMsg.Visible)
      this.On_cmbRepetition_SelectedValueChanged((object) this._cmbRepetition, new EventArgs());
    string customFormat2 = ctrl.CustomFormat;
    if (customFormat1 == customFormat2)
      return;
    this.IsChanged = true;
  }

  /// <summary>Изменение значений.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_ValueChanged(object sender, EventArgs e)
  {
    if (this._lockChanges)
      return;
    (sender as Control).Tag = (object) 1;
    this.IsChanged = true;
  }

  /// <summary>Обновление данных объекта.</summary>
  public override void Refresh()
  {
    base.Refresh();
    this.ResetChanges();
    this._lockChanges = true;
    this.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._objID, false);
      IDBRelation relation = sessionKeeper.Session.GetRelation(this._objID, this._userID, this._relTypeID);
      long userId = sessionKeeper.Session.UserID;
      if (objectActualCopy != null)
      {
        this._enabledControls = objectActualCopy.OwnerID == userId;
        this._txtSubject.Enabled = this._enabledControls;
        this._dtDateStart.Enabled = this._dtDateFinish.Enabled = this._enabledControls;
        this._cmbState.Enabled = this._cmbRelevance.Enabled = this._cmbCategory.Enabled = this._cmbRepetition.Enabled = this._enabledControls;
        this._lstRecipients.EnabledCtrl = this._enabledControls;
        this._rtbText.ReadOnly = !this._enabledControls;
        this._rtbText.BackColor = this._rtbText.ReadOnly ? SystemColors.Control : Color.White;
        this._txtSubject.Text = objectActualCopy.Caption;
        IDBAttribute attributeByGuid1 = objectActualCopy.GetAttributeByGuid(SystemGUIDs.attributeStart);
        IDBAttribute attributeByGuid2 = objectActualCopy.GetAttributeByGuid(SystemGUIDs.attributeDueDate);
        IDBAttribute attributeByGuid3 = objectActualCopy.GetAttributeByGuid(new Guid("cad015d7-306c-11d8-b4e9-00304f19f545"));
        IDBAttribute attributeByGuid4 = objectActualCopy.GetAttributeByGuid(new Guid("cad015d6-306c-11d8-b4e9-00304f19f545"));
        IDBAttribute attributeByGuid5 = objectActualCopy.GetAttributeByGuid(new Guid("cad015d3-306c-11d8-b4e9-00304f19f545"));
        IDBAttribute attributeByGuid6 = objectActualCopy.GetAttributeByGuid(new Guid("cad015d2-306c-11d8-b4e9-00304f19f545"));
        IDBAttribute attributeByGuid7 = objectActualCopy.GetAttributeByGuid(new Guid("cad015d8-306c-11d8-b4e9-00304f19f545"));
        IDBAttribute dbAttribute1 = (IDBAttribute) null;
        IDBAttribute dbAttribute2 = (IDBAttribute) null;
        if (relation != null)
        {
          dbAttribute1 = relation.GetAttributeByGuid(new Guid("cad015d5-306c-11d8-b4e9-00304f19f545"));
          dbAttribute2 = relation.GetAttributeByGuid(new Guid("cad015d4-306c-11d8-b4e9-00304f19f545"));
        }
        if (attributeByGuid2 != null && attributeByGuid2.Value != null && attributeByGuid2.Value != DBNull.Value)
        {
          this.SetDate(this._dtDateStart, attributeByGuid1?.Value);
          this.SetDate(this._dtDateFinish, attributeByGuid2.Value);
        }
        else
          this.SetDate(this._dtDateFinish, (object) null);
        GetAttributeValuesModes modes = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility | GetAttributeValuesModes.IncludeCaption;
        AttributeValues[] attributesValues = objectActualCopy.GetAttributesValues(modes);
        if (attributesValues != null)
        {
          for (int index = 0; index < attributesValues.Length; ++index)
          {
            if (!(attributesValues[index].AttributeGuid != this._addresseeNoticeGuid))
            {
              this._lstRecipients.Values = attributesValues[index];
              this._lstRecipients.Modified = false;
              break;
            }
          }
        }
        this._cmbState.SelectedValue = attributeByGuid7 == null || this._cmbState.DataSource == null ? this._cmbState.SelectedValue : attributeByGuid7.Value;
        this._cmbRelevance.SelectedValue = attributeByGuid3 == null || this._cmbRelevance.DataSource == null ? this._cmbRelevance.SelectedValue : attributeByGuid3.Value;
        this._rtbText.Text = attributeByGuid6 == null || attributeByGuid6.Value == null ? string.Empty : attributeByGuid6.Value.ToString();
        if (attributeByGuid4 != null)
        {
          object obj = attributeByGuid4.Value;
          this._cmbCategory.SelectedValue = obj == null || obj == DBNull.Value ? (object) -1 : obj;
        }
        this._lockChanges = false;
        this._cmbRepetition.SelectedValue = attributeByGuid5 == null || this._cmbRepetition.DataSource == null ? this._cmbRepetition.SelectedValue : attributeByGuid5.Value;
        if ((int) Convert.ToInt16(this._cmbRepetition.SelectedValue) == (int) Convert.ToInt16((object) Repetition.Once))
        {
          this._lbRepetitionMsg.Visible = false;
          this._repetitionCurrentValue = (object) Convert.ToInt16((object) Repetition.Once);
        }
        this._cmbRepetition.Tag = (object) 0;
        this.IsChanged = false;
        this._lockChanges = true;
        this._chbReminder.Checked = dbAttribute1 != null && (bool) dbAttribute1.Value;
        if (dbAttribute2 != null && this._chbReminder.Checked)
        {
          object obj = dbAttribute2.Value;
          if (obj != null && obj != DBNull.Value)
          {
            DateTime result = DateTime.Now;
            if (DateTime.TryParse(obj.ToString(), out result))
              this._dtReminder.Value = result;
          }
          else
            this._dtReminder.CustomFormat = this._nullFormat;
        }
        else
          this._dtReminder.CustomFormat = this._nullFormat;
      }
    }
    this._lockChanges = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="args"></param>
  /// <returns></returns>
  public override bool Refresh(PageRefreshArgs args)
  {
    this.Refresh();
    return base.Refresh(args);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="args"></param>
  /// <returns></returns>
  public override bool Save(PageSaveArgs args)
  {
    this.Save();
    return base.Save(args);
  }

  /// <summary>Обнулить значения контролов.</summary>
  private void Clear()
  {
    this._txtSubject.Text = string.Empty;
    this._dtDateStart.Value = this._dtDateFinish.Value = this._dtReminder.Value = DateTime.Now;
    this._dtDateStart.CustomFormat = this._dtDateFinish.CustomFormat = this._dtReminder.CustomFormat = this._nullFormat;
    this._lstRecipients.Modified = false;
    this._chbReminder.Checked = false;
    this._rtbText.Text = string.Empty;
    this._cmbCategory.SelectedValue = this._categoryDefaultValue;
    this._cmbRelevance.SelectedValue = this._relevanceDefaultValue;
    this._cmbRepetition.SelectedValue = this._repetitionDefaultValue;
    this._cmbState.SelectedValue = this._stateDefaultValue;
    this._cmbRepetition.Enabled = false;
    this._txtSubject.Tag = (object) null;
    this._dtDateStart.Tag = this._dtDateFinish.Tag = this._cmbState.Tag = this._cmbRelevance.Tag = this._cmbCategory.Tag = this._cmbRepetition.Tag = (object) null;
    this._lstRecipients.Tag = (object) null;
    this._chbReminder.Tag = this._dtReminder.Tag = (object) null;
    this._rtbText.Tag = (object) null;
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadPossibleValues()
  {
    this._lockChanges = true;
    Dictionary<Guid, ComboBox> dictionary = new Dictionary<Guid, ComboBox>(3);
    dictionary.Add(new Guid("cad015d6-306c-11d8-b4e9-00304f19f545"), this._cmbCategory);
    dictionary.Add(new Guid("cad015d7-306c-11d8-b4e9-00304f19f545"), this._cmbRelevance);
    dictionary.Add(new Guid("cad015d3-306c-11d8-b4e9-00304f19f545"), this._cmbRepetition);
    dictionary.Add(new Guid("cad015d8-306c-11d8-b4e9-00304f19f545"), this._cmbState);
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    foreach (KeyValuePair<Guid, ComboBox> keyValuePair in dictionary)
    {
      IDBAttributeTypeInfo attributeType = service.GetAttributeType(keyValuePair.Key, false);
      if (attributeType != null)
      {
        DataTable possibleValues = attributeType.GetPossibleValues();
        if (possibleValues != null)
        {
          if ((attributeType.Options & AttributeOptions.DisableNulls) != AttributeOptions.DisableNulls)
          {
            DataRow row = possibleValues.NewRow();
            row["F_STRING_VALUE"] = (object) -1;
            row["F_DESCRIPTION"] = (object) string.Empty;
            possibleValues.Rows.InsertAt(row, 0);
          }
          keyValuePair.Value.DataSource = (object) possibleValues;
          keyValuePair.Value.ValueMember = "F_STRING_VALUE";
          keyValuePair.Value.DisplayMember = "F_DESCRIPTION";
          keyValuePair.Value.SelectedValue = attributeType.DefaultValue == null || attributeType.DefaultValue == DBNull.Value ? (object) -1 : attributeType.DefaultValue;
          if (keyValuePair.Value.SelectedValue == null && possibleValues.Rows.Count != 0)
            keyValuePair.Value.SelectedValue = possibleValues.Rows[0][1];
        }
      }
    }
    this._categoryDefaultValue = this._cmbCategory.SelectedValue;
    this._relevanceDefaultValue = this._cmbRelevance.SelectedValue;
    this._repetitionCurrentValue = this._repetitionDefaultValue = this._cmbRepetition.SelectedValue;
    this._stateDefaultValue = this._cmbState.SelectedValue;
    this._lockChanges = false;
  }

  /// <summary>Установка значения даты.</summary>
  /// <param name="ctrl">Контрол, для которого необходимо установить значение</param>
  /// <param name="value">Значение</param>
  private void SetDate(DateTimePicker ctrl, object value)
  {
    DateTime result = ctrl.MinDate;
    if (value != null && DateTime.TryParse(value.ToString(), out result))
    {
      if (ctrl == this._dtDateStart)
      {
        if (this._lbRepetitionMsg.Visible && !this.ValidationRepetitionValue(result, this._dtDateFinish.Value, (int) Convert.ToInt16(this._cmbRepetition.SelectedValue)))
        {
          this._lockChanges = true;
          ctrl.Value = this._dateStartCurrentValue;
          this._lockChanges = false;
          return;
        }
        ctrl.Value = result;
        if (this._dtDateFinish.CustomFormat == this._nullFormat || result > this._dtDateFinish.Value)
          this._dtDateFinish.Value = result.AddMinutes(result.Minute < 30 ? (double) (30 - result.Minute) : (double) (60 - result.Minute));
        this._dtDateFinish.CustomFormat = this._customFormat;
        this._cmbRepetition.Enabled = this._enabledControls;
      }
      else if (ctrl == this._dtDateFinish)
      {
        if (this._lbRepetitionMsg.Visible && !this.ValidationRepetitionValue(this._dtDateStart.Value, result, (int) Convert.ToInt16(this._cmbRepetition.SelectedValue)))
        {
          this._lockChanges = true;
          ctrl.Value = this._dateFinishCurrentValue;
          this._lockChanges = false;
          return;
        }
        DateTime dateTime1;
        if (this._dtDateStart.CustomFormat == this._nullFormat)
        {
          DateTimePicker dtDateStart = this._dtDateStart;
          ref DateTime local = ref result;
          int num1;
          if (result.Minute >= 30)
          {
            dateTime1 = this._dtDateStart.Value;
            num1 = 30 - dateTime1.Minute;
          }
          else
          {
            dateTime1 = this._dtDateStart.Value;
            num1 = -dateTime1.Minute;
          }
          double num2 = (double) num1;
          DateTime dateTime2 = local.AddMinutes(num2);
          dtDateStart.Value = dateTime2;
        }
        if (this._dtDateStart.Value > result)
        {
          int num3 = (int) MessageBox.Show(this._setDateMsg, this._setDateCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          DateTimePicker dateTimePicker = ctrl;
          dateTime1 = this._dtDateStart.Value;
          ref DateTime local = ref dateTime1;
          DateTime dateTime3 = this._dtDateStart.Value;
          int num4;
          if (dateTime3.Minute >= 30)
          {
            dateTime3 = this._dtDateStart.Value;
            num4 = 60 - dateTime3.Minute;
          }
          else
          {
            dateTime3 = this._dtDateStart.Value;
            num4 = 30 - dateTime3.Minute;
          }
          double num5 = (double) num4;
          DateTime dateTime4 = local.AddMinutes(num5);
          dateTimePicker.Value = dateTime4;
        }
        else
          ctrl.Value = result;
        this._cmbRepetition.Enabled = this._dtDateStart.CustomFormat != this._nullFormat && this._dtDateFinish.CustomFormat != this._nullFormat && this._enabledControls;
      }
      else
        ctrl.Value = result;
      ctrl.CustomFormat = this._customFormat;
    }
    else
    {
      ctrl.CustomFormat = this._nullFormat;
      if (ctrl == this._dtDateStart)
      {
        this._cmbRepetition.SelectedValue = (object) Convert.ToInt16((object) Repetition.Once);
        this._cmbRepetition.Enabled = false;
      }
      else if (ctrl == this._dtDateFinish)
      {
        this._dtDateStart.CustomFormat = this._nullFormat;
        this._cmbRepetition.SelectedValue = (object) Convert.ToInt16((object) Repetition.Once);
        this._cmbRepetition.Enabled = false;
      }
      else
        this._chbReminder.Checked = false;
    }
    this._dateStartCurrentValue = this._dtDateStart.Value;
    this._dateFinishCurrentValue = this._dtDateFinish.Value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="valueFinish"></param>
  /// <param name="valueStart"></param>
  /// <param name="nRepetition"></param>
  /// <returns></returns>
  private bool ValidationRepetitionValue(
    DateTime valueStart,
    DateTime valueFinish,
    int nRepetition)
  {
    bool flag = true;
    if (nRepetition != (int) Convert.ToInt16((object) Repetition.Once))
    {
      if (nRepetition == (int) Convert.ToInt16((object) Repetition.Daily))
        valueStart = valueStart.AddDays(1.0);
      else if (nRepetition == (int) Convert.ToInt16((object) Repetition.Weekly))
        valueStart = valueStart.AddDays(7.0);
      else if (nRepetition == (int) Convert.ToInt16((object) Repetition.Monthly))
        valueStart = valueStart.AddMonths(1);
      else if (nRepetition == (int) Convert.ToInt16((object) Repetition.Yearly))
        valueStart = valueStart.AddYears(1);
      if (valueStart < valueFinish)
      {
        string caption = LocalizationHolder.rm.GetString("Organizer_Name");
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Organizer_Repetition_InvalidRange"), caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        flag = false;
      }
    }
    return flag;
  }

  /// <summary>Обновление данных.</summary>
  /// <param name="objID">Идентификатор обноляемого объекта</param>
  public void Refresh(long objID)
  {
    this._objID = objID;
    this.Refresh();
  }

  /// <summary>Сбросить пометку о сделанных изменениях.</summary>
  public void ResetChanges() => this.IsChanged = false;

  /// <summary>
  /// Сохранение данных объекта.
  /// Если данные не изменились, то выполнение прерывается.
  /// </summary>
  public void Save()
  {
    if (!this.IsChanged)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._objID, false);
      bool isCreationMode = objectActualCopy.IsCreationMode;
      if (objectActualCopy == null)
        return;
      objectActualCopy.Caption = this._txtSubject.Text;
      DateTime dateTime;
      if (this._dtDateStart.Tag != null)
      {
        IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(SystemGUIDs.attributeStart);
        if (this._dtDateStart.CustomFormat != this._nullFormat)
        {
          IDBAttribute dbAttribute = attributeByGuid;
          dateTime = this._dtDateStart.Value;
          // ISSUE: variable of a boxed type
          __Boxed<DateTime> local = (System.ValueType) dateTime.Subtract(new TimeSpan(0, 0, this._dtDateStart.Value.Second));
          dbAttribute.Value = (object) local;
        }
        else
          attributeByGuid.Value = (object) DBNull.Value;
      }
      if (this._dtDateFinish.Tag != null)
      {
        IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(SystemGUIDs.attributeDueDate);
        if (this._dtDateFinish.CustomFormat != this._nullFormat)
        {
          IDBAttribute dbAttribute = attributeByGuid;
          dateTime = this._dtDateFinish.Value;
          // ISSUE: variable of a boxed type
          __Boxed<DateTime> local = (System.ValueType) dateTime.Subtract(new TimeSpan(0, 0, this._dtDateFinish.Value.Second));
          dbAttribute.Value = (object) local;
        }
        else
          attributeByGuid.Value = (object) DBNull.Value;
      }
      if (this._lstRecipients.Tag != null)
      {
        if (!isCreationMode)
        {
          IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(new Guid("cad00628-306c-11d8-b4e9-00304f19f545"));
          List<long> longList = (List<long>) null;
          if (attributeByGuid.Values != null && attributeByGuid.Values.Length != 0)
          {
            longList = new List<long>(attributeByGuid.Values.Length);
            foreach (object obj in attributeByGuid.Values)
            {
              if (obj != null && obj != DBNull.Value)
              {
                long int64 = Convert.ToInt64(obj);
                if (!longList.Contains(int64))
                  longList.Add(int64);
              }
            }
          }
          object[] values = this._lstRecipients.Values.Values;
          if (values != null && values.Length != 0)
          {
            IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(this._relTypeID);
            foreach (object obj in values)
            {
              try
              {
                if (obj != null)
                {
                  if (obj != DBNull.Value)
                  {
                    long int64 = Convert.ToInt64(obj);
                    if (longList.Contains(int64))
                      longList.Remove(int64);
                    else
                      relationCollection.Create(this._objID, int64);
                  }
                }
              }
              catch (Exception ex)
              {
                ExceptionHelper.ExceptionService.ShowException(ex);
              }
            }
          }
          if (longList != null)
          {
            foreach (long objectID in longList)
            {
              try
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
                sessionKeeper.Session.GetRelation(this._objID, objectInfo.ID, this._relTypeID)?.Delete(0L);
              }
              catch (Exception ex)
              {
                ExceptionHelper.ExceptionService.ShowException(ex);
              }
            }
          }
        }
        objectActualCopy.SetAttributesValues(new AttributeValues[1]
        {
          this._lstRecipients.Values
        });
      }
      if (this._cmbState.Tag != null)
        objectActualCopy.GetAttributeByGuid(new Guid("cad015d8-306c-11d8-b4e9-00304f19f545")).Value = this._cmbState.SelectedValue;
      if (this._cmbRelevance.Tag != null)
        objectActualCopy.GetAttributeByGuid(new Guid("cad015d7-306c-11d8-b4e9-00304f19f545")).Value = this._cmbRelevance.SelectedValue;
      if (this._cmbCategory.Tag != null)
        objectActualCopy.GetAttributeByGuid(new Guid("cad015d6-306c-11d8-b4e9-00304f19f545")).Value = Convert.ToInt16(this._cmbCategory.SelectedValue) == (short) -1 ? (object) DBNull.Value : this._cmbCategory.SelectedValue;
      if (this._cmbRepetition.Tag != null)
        objectActualCopy.GetAttributeByGuid(new Guid("cad015d3-306c-11d8-b4e9-00304f19f545")).Value = this._cmbRepetition.SelectedValue;
      if (this._rtbText.Tag != null)
        objectActualCopy.GetAttributeByGuid(new Guid("cad015d2-306c-11d8-b4e9-00304f19f545")).Value = (object) this._rtbText.Text;
      if (this._chbReminder.Tag == null)
      {
        if (this._dtReminder.Tag == null)
          goto label_63;
      }
      if (!isCreationMode)
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(this._objID, this._userID, this._relTypeID);
        if (relation != null)
        {
          relation.GetAttributeByGuid(new Guid("cad015d5-306c-11d8-b4e9-00304f19f545")).Value = (object) this._chbReminder.Checked;
          IDBAttribute attributeByGuid = relation.GetAttributeByGuid(new Guid("cad015d4-306c-11d8-b4e9-00304f19f545"));
          if (this._chbReminder.Checked)
          {
            IDBAttribute dbAttribute = attributeByGuid;
            dateTime = this._dtReminder.Value;
            // ISSUE: variable of a boxed type
            __Boxed<DateTime> local = (System.ValueType) dateTime.Subtract(new TimeSpan(0, 0, this._dtReminder.Value.Second));
            dbAttribute.Value = (object) local;
          }
          else
            attributeByGuid.Value = (object) DBNull.Value;
        }
      }
    }
label_63:
    this.ResetChanges();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OrganizerTaskCtrl));
    this._lbSubject = new Label();
    this._pnlSubject = new Panel();
    this._txtSubject = new TextBox();
    this._gbSeparator1 = new GroupBox();
    this._cmbState = new ComboBox();
    this._lbState = new Label();
    this._gbSeparator2 = new GroupBox();
    this._pnlRecipient = new Panel();
    this._lstRecipients = new AttrListBoxBtn();
    this._lbRecipient = new Label();
    this._lbCategory = new Label();
    this._cmbCategory = new ComboBox();
    this._tt = new ToolTip(this.components);
    this._chbReminder = new CheckBox();
    this._dtDateStart = new DateTimePicker();
    this._dtDateFinish = new DateTimePicker();
    this._cmbRelevance = new ComboBox();
    this._dtReminder = new DateTimePicker();
    this._pnlReminder = new Panel();
    this._lbRepetition = new Label();
    this._lbRepetitionMsg = new Label();
    this._cmbRepetition = new ComboBox();
    this._lbDateStart = new Label();
    this._lbDateFinish = new Label();
    this._lbRelevance = new Label();
    this._pnlDate = new Panel();
    this._rtbText = new RichTextBox();
    this._gbSeparator3 = new GroupBox();
    this._pnlSubject.SuspendLayout();
    this._pnlRecipient.SuspendLayout();
    this._pnlReminder.SuspendLayout();
    this._pnlDate.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._lbSubject, "_lbSubject");
    this._lbSubject.Name = "_lbSubject";
    this._tt.SetToolTip((Control) this._lbSubject, componentResourceManager.GetString("_lbSubject.ToolTip"));
    componentResourceManager.ApplyResources((object) this._pnlSubject, "_pnlSubject");
    this._pnlSubject.Controls.Add((Control) this._txtSubject);
    this._pnlSubject.Controls.Add((Control) this._lbSubject);
    this._pnlSubject.Name = "_pnlSubject";
    this._tt.SetToolTip((Control) this._pnlSubject, componentResourceManager.GetString("_pnlSubject.ToolTip"));
    componentResourceManager.ApplyResources((object) this._txtSubject, "_txtSubject");
    this._txtSubject.Name = "_txtSubject";
    this._tt.SetToolTip((Control) this._txtSubject, componentResourceManager.GetString("_txtSubject.ToolTip"));
    this._txtSubject.TextChanged += new EventHandler(this.On_ValueChanged);
    componentResourceManager.ApplyResources((object) this._gbSeparator1, "_gbSeparator1");
    this._gbSeparator1.Name = "_gbSeparator1";
    this._gbSeparator1.TabStop = false;
    this._tt.SetToolTip((Control) this._gbSeparator1, componentResourceManager.GetString("_gbSeparator1.ToolTip"));
    componentResourceManager.ApplyResources((object) this._cmbState, "_cmbState");
    this._cmbState.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbState.Name = "_cmbState";
    this._tt.SetToolTip((Control) this._cmbState, componentResourceManager.GetString("_cmbState.ToolTip"));
    this._cmbState.SelectedValueChanged += new EventHandler(this.On_ValueChanged);
    componentResourceManager.ApplyResources((object) this._lbState, "_lbState");
    this._lbState.Name = "_lbState";
    this._tt.SetToolTip((Control) this._lbState, componentResourceManager.GetString("_lbState.ToolTip"));
    componentResourceManager.ApplyResources((object) this._gbSeparator2, "_gbSeparator2");
    this._gbSeparator2.Name = "_gbSeparator2";
    this._gbSeparator2.TabStop = false;
    this._tt.SetToolTip((Control) this._gbSeparator2, componentResourceManager.GetString("_gbSeparator2.ToolTip"));
    componentResourceManager.ApplyResources((object) this._pnlRecipient, "_pnlRecipient");
    this._pnlRecipient.Controls.Add((Control) this._lstRecipients);
    this._pnlRecipient.Controls.Add((Control) this._lbRecipient);
    this._pnlRecipient.Name = "_pnlRecipient";
    this._tt.SetToolTip((Control) this._pnlRecipient, componentResourceManager.GetString("_pnlRecipient.ToolTip"));
    componentResourceManager.ApplyResources((object) this._lstRecipients, "_lstRecipients");
    this._lstRecipients.AttributeInfo = (AttributeInfo) null;
    this._lstRecipients.MinimumSize = new Size(90, 60);
    this._lstRecipients.Name = "_lstRecipients";
    this._tt.SetToolTip((Control) this._lstRecipients, componentResourceManager.GetString("_lstRecipients.ToolTip"));
    this._lstRecipients.ModifiedEvent += new EventHandler(this.On_lstRecipients_ModifiedEvent);
    componentResourceManager.ApplyResources((object) this._lbRecipient, "_lbRecipient");
    this._lbRecipient.Name = "_lbRecipient";
    this._tt.SetToolTip((Control) this._lbRecipient, componentResourceManager.GetString("_lbRecipient.ToolTip"));
    componentResourceManager.ApplyResources((object) this._lbCategory, "_lbCategory");
    this._lbCategory.Name = "_lbCategory";
    this._tt.SetToolTip((Control) this._lbCategory, componentResourceManager.GetString("_lbCategory.ToolTip"));
    componentResourceManager.ApplyResources((object) this._cmbCategory, "_cmbCategory");
    this._cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbCategory.Name = "_cmbCategory";
    this._tt.SetToolTip((Control) this._cmbCategory, componentResourceManager.GetString("_cmbCategory.ToolTip"));
    this._cmbCategory.SelectedValueChanged += new EventHandler(this.On_ValueChanged);
    componentResourceManager.ApplyResources((object) this._chbReminder, "_chbReminder");
    this._chbReminder.Name = "_chbReminder";
    this._tt.SetToolTip((Control) this._chbReminder, componentResourceManager.GetString("_chbReminder.ToolTip"));
    this._chbReminder.UseVisualStyleBackColor = true;
    this._chbReminder.CheckedChanged += new EventHandler(this.On_ValueChanged);
    this._chbReminder.CheckStateChanged += new EventHandler(this.On_chbReminder_CheckStateChanged);
    componentResourceManager.ApplyResources((object) this._dtDateStart, "_dtDateStart");
    this._dtDateStart.Format = DateTimePickerFormat.Custom;
    this._dtDateStart.Name = "_dtDateStart";
    this._tt.SetToolTip((Control) this._dtDateStart, componentResourceManager.GetString("_dtDateStart.ToolTip"));
    this._dtDateStart.CloseUp += new EventHandler(this.On_dt_CloseUp);
    this._dtDateStart.ValueChanged += new EventHandler(this.On_ValueChanged);
    this._dtDateStart.KeyUp += new KeyEventHandler(this.On_dt_KeyUp);
    this._dtDateStart.Leave += new EventHandler(this.On_dt_Leave);
    componentResourceManager.ApplyResources((object) this._dtDateFinish, "_dtDateFinish");
    this._dtDateFinish.Format = DateTimePickerFormat.Custom;
    this._dtDateFinish.Name = "_dtDateFinish";
    this._tt.SetToolTip((Control) this._dtDateFinish, componentResourceManager.GetString("_dtDateFinish.ToolTip"));
    this._dtDateFinish.CloseUp += new EventHandler(this.On_dt_CloseUp);
    this._dtDateFinish.ValueChanged += new EventHandler(this.On_ValueChanged);
    this._dtDateFinish.KeyUp += new KeyEventHandler(this.On_dt_KeyUp);
    this._dtDateFinish.Leave += new EventHandler(this.On_dt_Leave);
    componentResourceManager.ApplyResources((object) this._cmbRelevance, "_cmbRelevance");
    this._cmbRelevance.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbRelevance.FormattingEnabled = true;
    this._cmbRelevance.Name = "_cmbRelevance";
    this._tt.SetToolTip((Control) this._cmbRelevance, componentResourceManager.GetString("_cmbRelevance.ToolTip"));
    this._cmbRelevance.SelectedValueChanged += new EventHandler(this.On_ValueChanged);
    componentResourceManager.ApplyResources((object) this._dtReminder, "_dtReminder");
    this._dtReminder.Format = DateTimePickerFormat.Custom;
    this._dtReminder.Name = "_dtReminder";
    this._tt.SetToolTip((Control) this._dtReminder, componentResourceManager.GetString("_dtReminder.ToolTip"));
    this._dtReminder.CloseUp += new EventHandler(this.On_dt_CloseUp);
    this._dtReminder.ValueChanged += new EventHandler(this.On_ValueChanged);
    this._dtReminder.KeyUp += new KeyEventHandler(this.On_dt_KeyUp);
    this._dtReminder.Leave += new EventHandler(this.On_dt_Leave);
    componentResourceManager.ApplyResources((object) this._pnlReminder, "_pnlReminder");
    this._pnlReminder.Controls.Add((Control) this._dtReminder);
    this._pnlReminder.Controls.Add((Control) this._chbReminder);
    this._pnlReminder.Name = "_pnlReminder";
    this._tt.SetToolTip((Control) this._pnlReminder, componentResourceManager.GetString("_pnlReminder.ToolTip"));
    componentResourceManager.ApplyResources((object) this._lbRepetition, "_lbRepetition");
    this._lbRepetition.Name = "_lbRepetition";
    this._tt.SetToolTip((Control) this._lbRepetition, componentResourceManager.GetString("_lbRepetition.ToolTip"));
    componentResourceManager.ApplyResources((object) this._lbRepetitionMsg, "_lbRepetitionMsg");
    this._lbRepetitionMsg.Name = "_lbRepetitionMsg";
    this._tt.SetToolTip((Control) this._lbRepetitionMsg, componentResourceManager.GetString("_lbRepetitionMsg.ToolTip"));
    componentResourceManager.ApplyResources((object) this._cmbRepetition, "_cmbRepetition");
    this._cmbRepetition.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbRepetition.Name = "_cmbRepetition";
    this._tt.SetToolTip((Control) this._cmbRepetition, componentResourceManager.GetString("_cmbRepetition.ToolTip"));
    this._cmbRepetition.SelectedValueChanged += new EventHandler(this.On_cmbRepetition_SelectedValueChanged);
    componentResourceManager.ApplyResources((object) this._lbDateStart, "_lbDateStart");
    this._lbDateStart.Name = "_lbDateStart";
    this._tt.SetToolTip((Control) this._lbDateStart, componentResourceManager.GetString("_lbDateStart.ToolTip"));
    componentResourceManager.ApplyResources((object) this._lbDateFinish, "_lbDateFinish");
    this._lbDateFinish.Name = "_lbDateFinish";
    this._tt.SetToolTip((Control) this._lbDateFinish, componentResourceManager.GetString("_lbDateFinish.ToolTip"));
    componentResourceManager.ApplyResources((object) this._lbRelevance, "_lbRelevance");
    this._lbRelevance.Name = "_lbRelevance";
    this._tt.SetToolTip((Control) this._lbRelevance, componentResourceManager.GetString("_lbRelevance.ToolTip"));
    componentResourceManager.ApplyResources((object) this._pnlDate, "_pnlDate");
    this._pnlDate.Controls.Add((Control) this._lbRepetitionMsg);
    this._pnlDate.Controls.Add((Control) this._lbRepetition);
    this._pnlDate.Controls.Add((Control) this._lbCategory);
    this._pnlDate.Controls.Add((Control) this._cmbCategory);
    this._pnlDate.Controls.Add((Control) this._cmbRepetition);
    this._pnlDate.Controls.Add((Control) this._lbDateStart);
    this._pnlDate.Controls.Add((Control) this._cmbRelevance);
    this._pnlDate.Controls.Add((Control) this._lbDateFinish);
    this._pnlDate.Controls.Add((Control) this._dtDateStart);
    this._pnlDate.Controls.Add((Control) this._dtDateFinish);
    this._pnlDate.Controls.Add((Control) this._cmbState);
    this._pnlDate.Controls.Add((Control) this._lbState);
    this._pnlDate.Controls.Add((Control) this._lbRelevance);
    this._pnlDate.Name = "_pnlDate";
    this._tt.SetToolTip((Control) this._pnlDate, componentResourceManager.GetString("_pnlDate.ToolTip"));
    componentResourceManager.ApplyResources((object) this._rtbText, "_rtbText");
    this._rtbText.Name = "_rtbText";
    this._tt.SetToolTip((Control) this._rtbText, componentResourceManager.GetString("_rtbText.ToolTip"));
    this._rtbText.TextChanged += new EventHandler(this.On_ValueChanged);
    componentResourceManager.ApplyResources((object) this._gbSeparator3, "_gbSeparator3");
    this._gbSeparator3.Name = "_gbSeparator3";
    this._gbSeparator3.TabStop = false;
    this._tt.SetToolTip((Control) this._gbSeparator3, componentResourceManager.GetString("_gbSeparator3.ToolTip"));
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._rtbText);
    this.Controls.Add((Control) this._pnlReminder);
    this.Controls.Add((Control) this._gbSeparator3);
    this.Controls.Add((Control) this._pnlRecipient);
    this.Controls.Add((Control) this._gbSeparator2);
    this.Controls.Add((Control) this._pnlDate);
    this.Controls.Add((Control) this._gbSeparator1);
    this.Controls.Add((Control) this._pnlSubject);
    this.DoubleBuffered = true;
    this.MinimumSize = new Size(722, 300);
    this.Name = nameof (OrganizerTaskCtrl);
    this._tt.SetToolTip((Control) this, componentResourceManager.GetString("$this.ToolTip"));
    this._pnlSubject.ResumeLayout(false);
    this._pnlSubject.PerformLayout();
    this._pnlRecipient.ResumeLayout(false);
    this._pnlReminder.ResumeLayout(false);
    this._pnlReminder.PerformLayout();
    this._pnlDate.ResumeLayout(false);
    this._pnlDate.PerformLayout();
    this.ResumeLayout(false);
  }
}
