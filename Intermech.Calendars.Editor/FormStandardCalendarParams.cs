
// Type: Intermech.Calendars.Editor.FormStandardCalendarParams
// Assembly: Intermech.Calendars.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0D5478F2-D4B6-4EDD-A444-F5E197647782
:\IPS\Client\Intermech.Calendars.Editor.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Calendars.Editor;

public class FormStandardCalendarParams : Form
{
  [NotNull]
  private readonly CalendarBase _calendar;
  [NotNull]
  private readonly List<DayType> _weekDaysList = new List<DayType>();
  [NotNull]
  [ItemNotNull]
  private readonly List<WorkTime> _standardWorkPeriods = new List<WorkTime>();
  [NotNull]
  [ItemNotNull]
  private readonly MaskedTextBox[] _timeEditsFrom;
  [NotNull]
  [ItemNotNull]
  private readonly MaskedTextBox[] _timeEditsTo;
  private int _lockUiEventsCounter;
  private double _hoursInDay = 8.0;
  private double _hoursInWeek = 40.0;
  private int _daysInMonth = 20;
  private IContainer components;
  private Label _label7;
  private ComboBox _comboBoxWeekStart;
  private Button _btnOk;
  private Button _btnCancel;
  private Label _label1;
  private ComboBox _comboBoxYearStart;
  private Label _label6;
  private MaskedTextBox _timeEditTo2;
  private Label _label5;
  private MaskedTextBox _timeEditTo5;
  private MaskedTextBox _timeEditFrom5;
  private MaskedTextBox _timeEditTo4;
  private MaskedTextBox _timeEditFrom4;
  private MaskedTextBox _timeEditTo3;
  private MaskedTextBox _timeEditFrom3;
  private MaskedTextBox _timeEditFrom2;
  private MaskedTextBox _timeEditTo1;
  private MaskedTextBox _timeEditFrom1;
  private GroupBox _groupBox2;
  private GroupBox _groupBox1;
  private Label _label10;
  private Label _label9;
  private Label _label8;
  private Label _label4;
  private Label _label3;
  private ComboBox _comboBoxMonday;
  private Label _label2;
  private ComboBox _comboBoxSunday;
  private ComboBox _comboBoxSaturday;
  private ComboBox _comboBoxFriday;
  private ComboBox _comboBoxThursday;
  private ComboBox _comboBoxWednesday;
  private ComboBox _comboBoxTuesday;
  private Label _label11;
  private Label _label12;
  private Label _label13;
  private Label _label14;
  private TextBox _textBoxHoursInDay;
  private TextBox _textBoxHoursInWeek;
  private TextBox _textBoxDaysInMonth;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Button BtnOk
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnOk.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Button BtnCancel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnCancel.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ComboBox ComboBoxWeekStart
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBoxWeekStart.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ComboBox ComboBoxYearStart
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBoxYearStart.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedTextBox TimeEditTo2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditTo2.CheckInitializedIn<MaskedTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedTextBox TimeEditTo5
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditTo5.CheckInitializedIn<MaskedTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedTextBox TimeEditFrom5
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditFrom5.CheckInitializedIn<MaskedTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedTextBox TimeEditTo4
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditTo4.CheckInitializedIn<MaskedTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedTextBox TimeEditFrom4
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditFrom4.CheckInitializedIn<MaskedTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedTextBox TimeEditTo3
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditTo3.CheckInitializedIn<MaskedTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedTextBox TimeEditFrom3
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditFrom3.CheckInitializedIn<MaskedTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedTextBox TimeEditFrom2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditFrom2.CheckInitializedIn<MaskedTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedTextBox TimeEditTo1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditTo1.CheckInitializedIn<MaskedTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedTextBox TimeEditFrom1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditFrom1.CheckInitializedIn<MaskedTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ComboBox ComboBoxMonday
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBoxMonday.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ComboBox ComboBoxSunday
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBoxSunday.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ComboBox ComboBoxSaturday
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBoxSaturday.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ComboBox ComboBoxFriday
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBoxFriday.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ComboBox ComboBoxThursday
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBoxThursday.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ComboBox ComboBoxWednesday
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBoxWednesday.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ComboBox ComboBoxTuesday
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBoxTuesday.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected TextBox TextBoxHoursInDay
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBoxHoursInDay.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected TextBox TextBoxHoursInWeek
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBoxHoursInWeek.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected TextBox TextBoxDaysInMonth
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBoxDaysInMonth.CheckInitializedIn<TextBox>((object) this);
    }
  }

  public FormStandardCalendarParams([NotNull] CalendarBase calendar)
  {
    this.InitializeComponent();
    this._calendar = calendar;
    this._timeEditsFrom = new MaskedTextBox[5]
    {
      this._timeEditFrom1,
      this._timeEditFrom2,
      this._timeEditFrom3,
      this._timeEditFrom4,
      this._timeEditFrom5
    };
    this._timeEditsTo = new MaskedTextBox[5]
    {
      this._timeEditTo1,
      this._timeEditTo2,
      this._timeEditTo3,
      this._timeEditTo4,
      this._timeEditTo5
    };
    ComboBox[] comboBoxArray = new ComboBox[7]
    {
      this._comboBoxMonday,
      this._comboBoxTuesday,
      this._comboBoxWednesday,
      this._comboBoxThursday,
      this._comboBoxFriday,
      this._comboBoxSaturday,
      this._comboBoxSunday
    };
    this._standardWorkPeriods.Clear();
    foreach (WorkTime standardWorkPeriod in (List<WorkTime>) this._calendar.StandardWorkPeriods)
      this._standardWorkPeriods.Add(WorkTime.CreateCopy(standardWorkPeriod));
    this._weekDaysList.Clear();
    foreach (DayBase weekDay in (IEnumerable<CalendarWeekDay>) this._calendar.StandardWeek.WeekDays)
      this._weekDaysList.Add(weekDay.DayType == DayType.Holiday ? DayType.Holiday : DayType.StandardWork);
    this.ComboBoxWeekStart.SelectedIndex = (int) (this._calendar.WeekStartDay - 1);
    this.ComboBoxYearStart.SelectedIndex = (int) (this._calendar.YearStartMonth - 1);
    for (int index = 0; index < 7; ++index)
      comboBoxArray[index].SelectedIndex = (int) this._weekDaysList[index];
    this.UpdateWorkPeriods();
    this.RecalcWorkTimes();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2527);
  }

  private void UpdateWorkPeriods()
  {
    char[] chArray = new char[2]{ ' ', ':' };
    for (int index = 0; index < 5; ++index)
    {
      if (index >= this._standardWorkPeriods.Count && this._timeEditsFrom[index].Text.Trim(chArray) != string.Empty && this._timeEditsTo[index].Text.Trim(chArray) != string.Empty)
      {
        this._timeEditsFrom[index].Text = string.Empty;
        this._timeEditsTo[index].Text = string.Empty;
      }
    }
    int index1 = 0;
    foreach (WorkTime standardWorkPeriod in this._standardWorkPeriods)
    {
      if (index1 > 4)
        break;
      MaskedTextBox maskedTextBox1 = this._timeEditsFrom[index1];
      MaskedTextBox maskedTextBox2 = this._timeEditsTo[index1];
      ++index1;
      string str1 = maskedTextBox1.Text.Trim(chArray);
      string str2 = maskedTextBox2.Text.Trim(chArray);
      if (str1 != string.Empty && str2 != string.Empty || str1 == string.Empty && str2 == string.Empty)
      {
        string str3 = $"{(standardWorkPeriod.StartHours < 10 ? " " + (object) standardWorkPeriod.StartHours : standardWorkPeriod.StartHours.ToString())}:{(standardWorkPeriod.StartMinutes < 10 ? "0" + (object) standardWorkPeriod.StartMinutes : standardWorkPeriod.StartMinutes.ToString())}";
        maskedTextBox1.Text = str3;
        string str4 = $"{(standardWorkPeriod.FinishHours < 10 ? " " + (object) standardWorkPeriod.FinishHours : standardWorkPeriod.FinishHours.ToString())}:{(standardWorkPeriod.FinishMinutes < 10 ? "0" + (object) standardWorkPeriod.FinishMinutes : standardWorkPeriod.FinishMinutes.ToString())}";
        maskedTextBox2.Text = str4;
      }
    }
  }

  private void RecalcTime(int editNumber)
  {
    if (this._lockUiEventsCounter != 0)
      return;
    this.LockUIEvents();
    try
    {
      int index1 = editNumber >= 5 ? editNumber - 5 : editNumber;
      string[] strArray1 = (editNumber >= 5 ? (Control) this._timeEditsTo[index1] : (Control) this._timeEditsFrom[index1]).Text.Split(':');
      if (strArray1.Length != 0)
      {
        bool flag1 = true;
        for (int index2 = strArray1.Length - 1; index2 >= 0; --index2)
        {
          strArray1[index2] = strArray1[index2].Trim();
          if (flag1 && strArray1[index2] != string.Empty)
            flag1 = false;
        }
        if (flag1)
        {
          if (index1 < this._standardWorkPeriods.Count)
          {
            string[] strArray2 = (editNumber >= 5 ? (Control) this._timeEditsFrom[index1] : (Control) this._timeEditsTo[index1]).Text.Split(':');
            if (strArray2.Length != 0)
            {
              for (int index3 = strArray2.Length - 1; index3 >= 0; --index3)
              {
                strArray2[index3] = strArray2[index3].Trim();
                if (flag1 && strArray2[index3] != string.Empty)
                  flag1 = false;
              }
            }
            if (flag1)
              this._standardWorkPeriods.RemoveAt(index1);
          }
        }
        else
        {
          bool flag2 = false;
          string[] strArray3 = (editNumber >= 5 ? (Control) this._timeEditsFrom[index1] : (Control) this._timeEditsTo[index1]).Text.Split(':');
          if (strArray3.Length != 0)
          {
            for (int index4 = strArray3.Length - 1; index4 >= 0; --index4)
            {
              strArray3[index4] = strArray3[index4].Trim();
              if (!flag2 && strArray3[index4] != string.Empty)
                flag2 = true;
            }
          }
          if (flag2)
          {
            WorkTime workTime;
            if (index1 >= this._standardWorkPeriods.Count)
            {
              workTime = new WorkTime();
              this._standardWorkPeriods.Add(workTime);
            }
            else
              workTime = this._standardWorkPeriods[index1];
            string[] strArray4 = editNumber >= 5 ? strArray3 : strArray1;
            string[] strArray5 = editNumber >= 5 ? strArray1 : strArray3;
            workTime.LockCorrection();
            try
            {
              int result;
              if (int.TryParse(strArray4[0], out result))
              {
                workTime.StartHours = result;
                workTime.StartMinutes = strArray4.Length == 0 ? 0 : (int.TryParse(strArray4[1], out result) ? result : 0);
                if (int.TryParse(strArray5[0], out result))
                {
                  workTime.FinishHours = result;
                  workTime.FinishMinutes = strArray5.Length == 0 ? 0 : (int.TryParse(strArray5[1], out result) ? result : 0);
                }
              }
            }
            finally
            {
              workTime.UnlockCorrection();
            }
            this._timeEditsFrom[index1].Text = string.Empty;
            this._timeEditsTo[index1].Text = string.Empty;
          }
        }
      }
    }
    finally
    {
      this.UnlockUIEvents();
    }
    this.UpdateWorkPeriods();
    this.UpdateEnabled();
  }

  public void LockUIEvents() => ++this._lockUiEventsCounter;

  public void UnlockUIEvents()
  {
    if (this._lockUiEventsCounter <= 0)
      return;
    --this._lockUiEventsCounter;
  }

  public void RecalcWorkTimes()
  {
    TimeSpan timeSpan1 = new TimeSpan(0L);
    foreach (WorkTime standardWorkPeriod in this._standardWorkPeriods)
      timeSpan1 += standardWorkPeriod.Duration;
    this._hoursInDay = timeSpan1.TotalHours;
    TimeSpan timeSpan2 = new TimeSpan(0L);
    int num = 0;
    foreach (DayType weekDays in this._weekDaysList)
    {
      if (weekDays == DayType.StandardWork)
      {
        timeSpan2 += timeSpan1;
        ++num;
      }
    }
    this._hoursInWeek = timeSpan2.TotalHours;
    this._daysInMonth = (int) Math.Round(365.0 * (double) num / 84.0);
    this.TextBoxHoursInDay.Text = Math.Round(this._hoursInDay, 1).ToString((IFormatProvider) CultureInfo.CurrentCulture);
    this.TextBoxHoursInWeek.Text = Math.Round(this._hoursInWeek, 1).ToString((IFormatProvider) CultureInfo.CurrentCulture);
    this.TextBoxDaysInMonth.Text = this._daysInMonth.ToString();
  }

  private void UpdateEnabled()
  {
    this.RecalcWorkTimes();
    bool flag = false;
    foreach (DayType weekDays in this._weekDaysList)
    {
      if (weekDays == DayType.StandardWork)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      this.BtnOk.Enabled = false;
    else
      this.BtnOk.Enabled = this._hoursInDay > 0.0;
  }

  private void Save()
  {
    this._calendar.HoursInDay = this._hoursInDay;
    this._calendar.HoursInWeek = this._hoursInWeek;
    this._calendar.DaysInMonth = this._daysInMonth;
    this._calendar.WeekStartDay = (WeekDay) (this.ComboBoxWeekStart.SelectedIndex + 1);
    this._calendar.YearStartMonth = (Month) (this.ComboBoxYearStart.SelectedIndex + 1);
    this._calendar.StandardWorkPeriods.Clear();
    foreach (WorkTime standardWorkPeriod in this._standardWorkPeriods)
      this._calendar.StandardWorkPeriods.Add(WorkTime.CreateCopy(standardWorkPeriod));
    int num = 0;
    foreach (DayBase dayBase in this._calendar.StandardWeek)
      dayBase.DayType = this._weekDaysList[num++];
  }

  private void TimeEditTo_Leave([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(sender is MaskedTextBox maskedTextBox) || maskedTextBox.Tag == null)
      return;
    this.RecalcTime(Convert.ToInt32(maskedTextBox.Tag));
  }

  private void TimeEditTo_KeyDown([CanBeNull] object sender, [NotNull] KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return || !(sender is MaskedTextBox maskedTextBox) || maskedTextBox.Tag == null)
      return;
    this.RecalcTime(Convert.ToInt32(maskedTextBox.Tag));
  }

  private void BtnOk_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateEnabled();
    if (!this.BtnOk.Enabled)
      return;
    this.Save();
  }

  private void ComboBoxWeekStart_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
  }

  private void ComboBoxYearStart_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateEnabled();
  }

  private void ComboBoxWeekStart_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateEnabled();
  }

  private void ComboBoxWeekDay_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(sender is ComboBox comboBox) || comboBox.Tag == null)
      return;
    int int32 = Convert.ToInt32(comboBox.Tag);
    if (this._weekDaysList[int32] == (DayType) comboBox.SelectedIndex)
      return;
    this._weekDaysList[int32] = (DayType) comboBox.SelectedIndex;
    this.UpdateEnabled();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormStandardCalendarParams));
    this._label7 = new Label();
    this._comboBoxWeekStart = new ComboBox();
    this._btnOk = new Button();
    this._btnCancel = new Button();
    this._label1 = new Label();
    this._comboBoxYearStart = new ComboBox();
    this._label6 = new Label();
    this._timeEditTo2 = new MaskedTextBox();
    this._label5 = new Label();
    this._timeEditTo5 = new MaskedTextBox();
    this._timeEditFrom5 = new MaskedTextBox();
    this._timeEditTo4 = new MaskedTextBox();
    this._timeEditFrom4 = new MaskedTextBox();
    this._timeEditTo3 = new MaskedTextBox();
    this._timeEditFrom3 = new MaskedTextBox();
    this._timeEditFrom2 = new MaskedTextBox();
    this._timeEditTo1 = new MaskedTextBox();
    this._timeEditFrom1 = new MaskedTextBox();
    this._groupBox2 = new GroupBox();
    this._groupBox1 = new GroupBox();
    this._comboBoxSunday = new ComboBox();
    this._comboBoxSaturday = new ComboBox();
    this._comboBoxFriday = new ComboBox();
    this._comboBoxThursday = new ComboBox();
    this._comboBoxWednesday = new ComboBox();
    this._comboBoxTuesday = new ComboBox();
    this._label11 = new Label();
    this._label10 = new Label();
    this._label9 = new Label();
    this._label8 = new Label();
    this._label4 = new Label();
    this._label3 = new Label();
    this._comboBoxMonday = new ComboBox();
    this._label2 = new Label();
    this._label12 = new Label();
    this._label13 = new Label();
    this._label14 = new Label();
    this._textBoxHoursInDay = new TextBox();
    this._textBoxHoursInWeek = new TextBox();
    this._textBoxDaysInMonth = new TextBox();
    this._groupBox2.SuspendLayout();
    this._groupBox1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._label7, "_label7");
    this._label7.Name = "_label7";
    this._comboBoxWeekStart.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxWeekStart.FormattingEnabled = true;
    this._comboBoxWeekStart.Items.AddRange(new object[7]
    {
      (object) componentResourceManager.GetString("_comboBoxWeekStart.Items"),
      (object) componentResourceManager.GetString("_comboBoxWeekStart.Items1"),
      (object) componentResourceManager.GetString("_comboBoxWeekStart.Items2"),
      (object) componentResourceManager.GetString("_comboBoxWeekStart.Items3"),
      (object) componentResourceManager.GetString("_comboBoxWeekStart.Items4"),
      (object) componentResourceManager.GetString("_comboBoxWeekStart.Items5"),
      (object) componentResourceManager.GetString("_comboBoxWeekStart.Items6")
    });
    componentResourceManager.ApplyResources((object) this._comboBoxWeekStart, "_comboBoxWeekStart");
    this._comboBoxWeekStart.Name = "_comboBoxWeekStart";
    this._comboBoxWeekStart.SelectedIndexChanged += new EventHandler(this.ComboBoxWeekStart_SelectedIndexChanged);
    this._comboBoxWeekStart.Click += new EventHandler(this.ComboBoxWeekStart_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._btnOk, "_btnOk");
    this._btnOk.DialogResult = DialogResult.OK;
    this._btnOk.Name = "_btnOk";
    this._btnOk.UseVisualStyleBackColor = true;
    this._btnOk.Click += new EventHandler(this.BtnOk_Click);
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._label1, "_label1");
    this._label1.Name = "_label1";
    this._comboBoxYearStart.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxYearStart.FormattingEnabled = true;
    this._comboBoxYearStart.Items.AddRange(new object[12]
    {
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items"),
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items1"),
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items2"),
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items3"),
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items4"),
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items5"),
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items6"),
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items7"),
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items8"),
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items9"),
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items10"),
      (object) componentResourceManager.GetString("_comboBoxYearStart.Items11")
    });
    componentResourceManager.ApplyResources((object) this._comboBoxYearStart, "_comboBoxYearStart");
    this._comboBoxYearStart.Name = "_comboBoxYearStart";
    this._comboBoxYearStart.SelectedIndexChanged += new EventHandler(this.ComboBoxYearStart_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._label6, "_label6");
    this._label6.Name = "_label6";
    componentResourceManager.ApplyResources((object) this._timeEditTo2, "_timeEditTo2");
    this._timeEditTo2.Name = "_timeEditTo2";
    this._timeEditTo2.Tag = (object) "6";
    this._timeEditTo2.ValidatingType = typeof (DateTime);
    this._timeEditTo2.KeyDown += new KeyEventHandler(this.TimeEditTo_KeyDown);
    this._timeEditTo2.Leave += new EventHandler(this.TimeEditTo_Leave);
    componentResourceManager.ApplyResources((object) this._label5, "_label5");
    this._label5.Name = "_label5";
    componentResourceManager.ApplyResources((object) this._timeEditTo5, "_timeEditTo5");
    this._timeEditTo5.Name = "_timeEditTo5";
    this._timeEditTo5.Tag = (object) "9";
    this._timeEditTo5.ValidatingType = typeof (DateTime);
    this._timeEditTo5.KeyDown += new KeyEventHandler(this.TimeEditTo_KeyDown);
    this._timeEditTo5.Leave += new EventHandler(this.TimeEditTo_Leave);
    componentResourceManager.ApplyResources((object) this._timeEditFrom5, "_timeEditFrom5");
    this._timeEditFrom5.Name = "_timeEditFrom5";
    this._timeEditFrom5.Tag = (object) "4";
    this._timeEditFrom5.ValidatingType = typeof (DateTime);
    this._timeEditFrom5.KeyDown += new KeyEventHandler(this.TimeEditTo_KeyDown);
    this._timeEditFrom5.Leave += new EventHandler(this.TimeEditTo_Leave);
    componentResourceManager.ApplyResources((object) this._timeEditTo4, "_timeEditTo4");
    this._timeEditTo4.Name = "_timeEditTo4";
    this._timeEditTo4.Tag = (object) "8";
    this._timeEditTo4.ValidatingType = typeof (DateTime);
    this._timeEditTo4.KeyDown += new KeyEventHandler(this.TimeEditTo_KeyDown);
    this._timeEditTo4.Leave += new EventHandler(this.TimeEditTo_Leave);
    componentResourceManager.ApplyResources((object) this._timeEditFrom4, "_timeEditFrom4");
    this._timeEditFrom4.Name = "_timeEditFrom4";
    this._timeEditFrom4.Tag = (object) "3";
    this._timeEditFrom4.ValidatingType = typeof (DateTime);
    this._timeEditFrom4.KeyDown += new KeyEventHandler(this.TimeEditTo_KeyDown);
    this._timeEditFrom4.Leave += new EventHandler(this.TimeEditTo_Leave);
    componentResourceManager.ApplyResources((object) this._timeEditTo3, "_timeEditTo3");
    this._timeEditTo3.Name = "_timeEditTo3";
    this._timeEditTo3.Tag = (object) "7";
    this._timeEditTo3.ValidatingType = typeof (DateTime);
    this._timeEditTo3.KeyDown += new KeyEventHandler(this.TimeEditTo_KeyDown);
    this._timeEditTo3.Leave += new EventHandler(this.TimeEditTo_Leave);
    componentResourceManager.ApplyResources((object) this._timeEditFrom3, "_timeEditFrom3");
    this._timeEditFrom3.Name = "_timeEditFrom3";
    this._timeEditFrom3.Tag = (object) "2";
    this._timeEditFrom3.ValidatingType = typeof (DateTime);
    this._timeEditFrom3.KeyDown += new KeyEventHandler(this.TimeEditTo_KeyDown);
    this._timeEditFrom3.Leave += new EventHandler(this.TimeEditTo_Leave);
    componentResourceManager.ApplyResources((object) this._timeEditFrom2, "_timeEditFrom2");
    this._timeEditFrom2.Name = "_timeEditFrom2";
    this._timeEditFrom2.Tag = (object) "1";
    this._timeEditFrom2.ValidatingType = typeof (DateTime);
    this._timeEditFrom2.KeyDown += new KeyEventHandler(this.TimeEditTo_KeyDown);
    this._timeEditFrom2.Leave += new EventHandler(this.TimeEditTo_Leave);
    componentResourceManager.ApplyResources((object) this._timeEditTo1, "_timeEditTo1");
    this._timeEditTo1.Name = "_timeEditTo1";
    this._timeEditTo1.Tag = (object) "5";
    this._timeEditTo1.ValidatingType = typeof (DateTime);
    this._timeEditTo1.KeyDown += new KeyEventHandler(this.TimeEditTo_KeyDown);
    this._timeEditTo1.Leave += new EventHandler(this.TimeEditTo_Leave);
    componentResourceManager.ApplyResources((object) this._timeEditFrom1, "_timeEditFrom1");
    this._timeEditFrom1.Name = "_timeEditFrom1";
    this._timeEditFrom1.Tag = (object) "0";
    this._timeEditFrom1.ValidatingType = typeof (DateTime);
    this._timeEditFrom1.KeyDown += new KeyEventHandler(this.TimeEditTo_KeyDown);
    this._timeEditFrom1.Leave += new EventHandler(this.TimeEditTo_Leave);
    this._groupBox2.Controls.Add((Control) this._timeEditFrom2);
    this._groupBox2.Controls.Add((Control) this._label6);
    this._groupBox2.Controls.Add((Control) this._timeEditFrom1);
    this._groupBox2.Controls.Add((Control) this._timeEditTo2);
    this._groupBox2.Controls.Add((Control) this._timeEditTo1);
    this._groupBox2.Controls.Add((Control) this._label5);
    this._groupBox2.Controls.Add((Control) this._timeEditFrom3);
    this._groupBox2.Controls.Add((Control) this._timeEditTo5);
    this._groupBox2.Controls.Add((Control) this._timeEditTo3);
    this._groupBox2.Controls.Add((Control) this._timeEditFrom5);
    this._groupBox2.Controls.Add((Control) this._timeEditFrom4);
    this._groupBox2.Controls.Add((Control) this._timeEditTo4);
    componentResourceManager.ApplyResources((object) this._groupBox2, "_groupBox2");
    this._groupBox2.Name = "_groupBox2";
    this._groupBox2.TabStop = false;
    this._groupBox1.Controls.Add((Control) this._comboBoxSunday);
    this._groupBox1.Controls.Add((Control) this._comboBoxSaturday);
    this._groupBox1.Controls.Add((Control) this._comboBoxFriday);
    this._groupBox1.Controls.Add((Control) this._comboBoxThursday);
    this._groupBox1.Controls.Add((Control) this._comboBoxWednesday);
    this._groupBox1.Controls.Add((Control) this._comboBoxTuesday);
    this._groupBox1.Controls.Add((Control) this._label11);
    this._groupBox1.Controls.Add((Control) this._label10);
    this._groupBox1.Controls.Add((Control) this._label9);
    this._groupBox1.Controls.Add((Control) this._label8);
    this._groupBox1.Controls.Add((Control) this._label4);
    this._groupBox1.Controls.Add((Control) this._label3);
    this._groupBox1.Controls.Add((Control) this._comboBoxMonday);
    this._groupBox1.Controls.Add((Control) this._label2);
    componentResourceManager.ApplyResources((object) this._groupBox1, "_groupBox1");
    this._groupBox1.Name = "_groupBox1";
    this._groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this._comboBoxSunday, "_comboBoxSunday");
    this._comboBoxSunday.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxSunday.FormattingEnabled = true;
    this._comboBoxSunday.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("_comboBoxSunday.Items"),
      (object) componentResourceManager.GetString("_comboBoxSunday.Items1")
    });
    this._comboBoxSunday.Name = "_comboBoxSunday";
    this._comboBoxSunday.Tag = (object) "6";
    this._comboBoxSunday.SelectedIndexChanged += new EventHandler(this.ComboBoxWeekDay_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._comboBoxSaturday, "_comboBoxSaturday");
    this._comboBoxSaturday.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxSaturday.FormattingEnabled = true;
    this._comboBoxSaturday.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("_comboBoxSaturday.Items"),
      (object) componentResourceManager.GetString("_comboBoxSaturday.Items1")
    });
    this._comboBoxSaturday.Name = "_comboBoxSaturday";
    this._comboBoxSaturday.Tag = (object) "5";
    this._comboBoxSaturday.SelectedIndexChanged += new EventHandler(this.ComboBoxWeekDay_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._comboBoxFriday, "_comboBoxFriday");
    this._comboBoxFriday.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxFriday.FormattingEnabled = true;
    this._comboBoxFriday.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("_comboBoxFriday.Items"),
      (object) componentResourceManager.GetString("_comboBoxFriday.Items1")
    });
    this._comboBoxFriday.Name = "_comboBoxFriday";
    this._comboBoxFriday.Tag = (object) "4";
    this._comboBoxFriday.SelectedIndexChanged += new EventHandler(this.ComboBoxWeekDay_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._comboBoxThursday, "_comboBoxThursday");
    this._comboBoxThursday.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxThursday.FormattingEnabled = true;
    this._comboBoxThursday.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("_comboBoxThursday.Items"),
      (object) componentResourceManager.GetString("_comboBoxThursday.Items1")
    });
    this._comboBoxThursday.Name = "_comboBoxThursday";
    this._comboBoxThursday.Tag = (object) "3";
    this._comboBoxThursday.SelectedIndexChanged += new EventHandler(this.ComboBoxWeekDay_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._comboBoxWednesday, "_comboBoxWednesday");
    this._comboBoxWednesday.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxWednesday.FormattingEnabled = true;
    this._comboBoxWednesday.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("_comboBoxWednesday.Items"),
      (object) componentResourceManager.GetString("_comboBoxWednesday.Items1")
    });
    this._comboBoxWednesday.Name = "_comboBoxWednesday";
    this._comboBoxWednesday.Tag = (object) "2";
    this._comboBoxWednesday.SelectedIndexChanged += new EventHandler(this.ComboBoxWeekDay_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._comboBoxTuesday, "_comboBoxTuesday");
    this._comboBoxTuesday.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxTuesday.FormattingEnabled = true;
    this._comboBoxTuesday.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("_comboBoxTuesday.Items"),
      (object) componentResourceManager.GetString("_comboBoxTuesday.Items1")
    });
    this._comboBoxTuesday.Name = "_comboBoxTuesday";
    this._comboBoxTuesday.Tag = (object) "1";
    this._comboBoxTuesday.SelectedIndexChanged += new EventHandler(this.ComboBoxWeekDay_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._label11, "_label11");
    this._label11.Name = "_label11";
    componentResourceManager.ApplyResources((object) this._label10, "_label10");
    this._label10.Name = "_label10";
    componentResourceManager.ApplyResources((object) this._label9, "_label9");
    this._label9.Name = "_label9";
    componentResourceManager.ApplyResources((object) this._label8, "_label8");
    this._label8.Name = "_label8";
    componentResourceManager.ApplyResources((object) this._label4, "_label4");
    this._label4.Name = "_label4";
    componentResourceManager.ApplyResources((object) this._label3, "_label3");
    this._label3.Name = "_label3";
    componentResourceManager.ApplyResources((object) this._comboBoxMonday, "_comboBoxMonday");
    this._comboBoxMonday.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxMonday.FormattingEnabled = true;
    this._comboBoxMonday.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("_comboBoxMonday.Items"),
      (object) componentResourceManager.GetString("_comboBoxMonday.Items1")
    });
    this._comboBoxMonday.Name = "_comboBoxMonday";
    this._comboBoxMonday.Tag = (object) "0";
    this._comboBoxMonday.SelectedIndexChanged += new EventHandler(this.ComboBoxWeekDay_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._label2, "_label2");
    this._label2.Name = "_label2";
    componentResourceManager.ApplyResources((object) this._label12, "_label12");
    this._label12.Name = "_label12";
    componentResourceManager.ApplyResources((object) this._label13, "_label13");
    this._label13.Name = "_label13";
    componentResourceManager.ApplyResources((object) this._label14, "_label14");
    this._label14.Name = "_label14";
    this._textBoxHoursInDay.BackColor = SystemColors.ButtonFace;
    this._textBoxHoursInDay.Cursor = Cursors.Arrow;
    componentResourceManager.ApplyResources((object) this._textBoxHoursInDay, "_textBoxHoursInDay");
    this._textBoxHoursInDay.Name = "_textBoxHoursInDay";
    this._textBoxHoursInDay.ReadOnly = true;
    this._textBoxHoursInWeek.BackColor = SystemColors.ButtonFace;
    this._textBoxHoursInWeek.Cursor = Cursors.Arrow;
    componentResourceManager.ApplyResources((object) this._textBoxHoursInWeek, "_textBoxHoursInWeek");
    this._textBoxHoursInWeek.Name = "_textBoxHoursInWeek";
    this._textBoxHoursInWeek.ReadOnly = true;
    this._textBoxDaysInMonth.BackColor = SystemColors.ButtonFace;
    this._textBoxDaysInMonth.Cursor = Cursors.Arrow;
    componentResourceManager.ApplyResources((object) this._textBoxDaysInMonth, "_textBoxDaysInMonth");
    this._textBoxDaysInMonth.Name = "_textBoxDaysInMonth";
    this._textBoxDaysInMonth.ReadOnly = true;
    this.AcceptButton = (IButtonControl) this._btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._textBoxDaysInMonth);
    this.Controls.Add((Control) this._textBoxHoursInWeek);
    this.Controls.Add((Control) this._textBoxHoursInDay);
    this.Controls.Add((Control) this._label14);
    this.Controls.Add((Control) this._label13);
    this.Controls.Add((Control) this._label12);
    this.Controls.Add((Control) this._groupBox1);
    this.Controls.Add((Control) this._groupBox2);
    this.Controls.Add((Control) this._comboBoxYearStart);
    this.Controls.Add((Control) this._label1);
    this.Controls.Add((Control) this._btnOk);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._comboBoxWeekStart);
    this.Controls.Add((Control) this._label7);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormStandardCalendarParams);
    this._groupBox2.ResumeLayout(false);
    this._groupBox2.PerformLayout();
    this._groupBox1.ResumeLayout(false);
    this._groupBox1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
