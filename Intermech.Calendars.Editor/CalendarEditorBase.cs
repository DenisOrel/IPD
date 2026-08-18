
// Type: Intermech.Calendars.Editor.CalendarEditorBase
// Assembly: Intermech.Calendars.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0D5478F2-D4B6-4EDD-A444-F5E197647782
:\IPS\Client\Intermech.Calendars.Editor.dll

using Intermech.Bars;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Pabo.Calendar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Calendars.Editor;

[ViewDescriptionProvider(typeof (CalendarEditorBase.Description))]
public class CalendarEditorBase : UserControl, ICommandTarget, ICommandTarget2, IView
{
  private INotificationService _iNotificationService;
  private DayBase _selectedDay;
  private DayBase _lastSelectedDay;
  private bool _loaded;
  private DateTime _focusedDate;
  private DateTime _minSelectedDate;
  private DateTime _maxSelectedDate;
  private int _activeEditNumber;
  private int _updateSelectedDayLockCounter;
  [NotNull]
  private readonly CalendarEditorBase.TimeEditPair[] _timeEditPairs;
  [NotNull]
  [ItemNotNull]
  private readonly RadioButton[] _rbRepeatArray;
  [NotNull]
  private readonly Dictionary<SpecialDay, List<DateItem>> _spDayInfoToDateItemsList = new Dictionary<SpecialDay, List<DateItem>>();
  private int _lockUIEventsCounter;
  private bool _changed;
  [CanBeNull]
  private CalendarBase _calendar;
  private static int _calendarTypeID;
  private static int _userTypeID;
  private readonly int _uiLockEditTextCounter;
  private IContainer components;
  private GroupBox _groupBox1;
  private Panel _panel1;
  private Label _label3;
  private Panel _panel3;
  private Label _label2;
  private Panel _panel2;
  private Label _label1;
  private Button _btnCancel;
  private Button _btnSave;
  private Button _btnParams;
  private Label _label4;
  private RadioButton _rbStandardWorkDay;
  private RadioButton _rbHoliday;
  private RadioButton _rbNotStandardWorkDay;
  private MaskedEdit _timeEditFrom1;
  private MaskedEdit _timeEditTo1;
  private MaskedEdit _timeEditTo2;
  private MaskedEdit _timeEditFrom2;
  private MaskedEdit _timeEditTo3;
  private MaskedEdit _timeEditFrom3;
  private MaskedEdit _timeEditTo4;
  private MaskedEdit _timeEditFrom4;
  private MaskedEdit _timeEditTo5;
  private MaskedEdit _timeEditFrom5;
  private Label _label5;
  private Label _label6;
  private Label _label7;
  private Pabo.Calendar.MonthCalendar _calendarUI;
  private GroupBox _groupBox2;
  private RadioButton _rbRepeat4;
  private RadioButton _rbRepeat3;
  private RadioButton _rbRepeat2;
  private RadioButton _rbRepeat1;
  private GroupBox _groupBox3;
  private Button _btnDelete;

  public bool Changed
  {
    get => this._changed;
    set => this._changed = value;
  }

  [CanBeNull]
  protected CalendarBase Calendar
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._calendar;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._calendar = value;
  }

  [Conditional("DEBUG")]
  protected virtual void CheckCalendarType([NotNull] CalendarBase calendar)
  {
    throw new NotImplementedException(nameof (CheckCalendarType));
  }

  protected virtual CalendarOwnerType CalendarOwnerType
  {
    get => throw new NotImplementedException(nameof (CalendarOwnerType));
  }

  [NotEmpty]
  protected virtual long CalendarOwnerID
  {
    get => throw new NotImplementedException(nameof (CalendarOwnerID));
    set => throw new NotImplementedException(nameof (CalendarOwnerID));
  }

  protected CalendarEditorBase()
  {
    this.InitializeComponent();
    this.CalendarUI.ActiveMonth.Year = DateTime.Today.Year;
    this.CalendarUI.ActiveMonth.Month = DateTime.Today.Month;
    this._timeEditPairs = new CalendarEditorBase.TimeEditPair[5]
    {
      (CalendarEditorBase.TimeEditPair) (0, (MaskedTextBox) this.TimeEditFrom1, (MaskedTextBox) this.TimeEditTo1),
      (CalendarEditorBase.TimeEditPair) (1, (MaskedTextBox) this.TimeEditFrom2, (MaskedTextBox) this.TimeEditTo2),
      (CalendarEditorBase.TimeEditPair) (2, (MaskedTextBox) this.TimeEditFrom3, (MaskedTextBox) this.TimeEditTo3),
      (CalendarEditorBase.TimeEditPair) (3, (MaskedTextBox) this.TimeEditFrom4, (MaskedTextBox) this.TimeEditTo4),
      (CalendarEditorBase.TimeEditPair) (4, (MaskedTextBox) this.TimeEditFrom5, (MaskedTextBox) this.TimeEditTo5)
    };
    this._rbRepeatArray = new RadioButton[4]
    {
      this.RbRepeat1,
      this.RbRepeat2,
      this.RbRepeat3,
      this.RbRepeat4
    };
  }

  public bool Execute([NotNull] ICommandState commandState) => false;

  public bool QueryStatus([NotNull] ICommandState commandState) => false;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected void FireChanged(bool firePrePostEvents = false)
  {
    Intermech.Diagnostics.Check.NotNull<CalendarBase>(this.Calendar, "Calendar");
    Intermech.Check.ObjectIdNotEmpty(this.CalendarOwnerID, "CalendarOwnerID");
    if (this._iNotificationService == null)
      return;
    this._iNotificationService.FireEvent((object) "CalendarChanged", (NotificationEventArgs) new CalendarEvents.ChangedArgs(this.CalendarOwnerType, this.CalendarOwnerID, firePrePostEvents));
  }

  public void BeginQuery()
  {
  }

  public void EndQuery()
  {
  }

  private static int CalendarTypeID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return CalendarEditorBase._calendarTypeID != 0 ? CalendarEditorBase._calendarTypeID : (CalendarEditorBase._calendarTypeID = MetaDataHelper.GetObjectTypeID(new Guid("cad00d87-306c-11d8-b4e9-00304f19f545")));
    }
  }

  private static int UserTypeID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return CalendarEditorBase._userTypeID != 0 ? CalendarEditorBase._userTypeID : (CalendarEditorBase._userTypeID = MetaDataHelper.GetObjectTypeID(new Guid("cad00002-306c-11d8-b4e9-00304f19f545")));
    }
  }

  public void Initialize([NotNull] ISelectedItems items, System.IServiceProvider provider)
  {
    if (items.Count != 1)
      return;
    if (this._iNotificationService == null)
    {
      this._iNotificationService = (INotificationService) provider.GetService(typeof (INotificationService));
      if (this._iNotificationService != null)
      {
        INotificationService notificationService = this._iNotificationService;
        notificationService.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.ObjectWasCheckedOut));
        notificationService.Subscribe("ObjectsCheckedIn", new NotificationEventHandler(this.ObjectWasCheckedIn));
        notificationService.Subscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.ObjectChangesWasCanceled));
      }
    }
    this.CalendarOwnerID = ((IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID))).ObjectID;
    Intermech.Check.ObjectIdNotEmpty(this.CalendarOwnerID, "CalendarOwnerID");
    this._loaded = false;
  }

  public void Activate(IView previousView)
  {
    if (this._loaded)
      return;
    this.InitCalendarData();
    this._loaded = true;
  }

  public void Deactivate(IView nextView)
  {
    if (!this.Changed || MessageBox.Show(Localization.GetString("Save_changes"), Localization.GetString("Calendar"), MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.SaveCalendarData();
    this.BtnSave.Enabled = false;
    this.BtnCancel.Enabled = false;
    this._changed = false;
  }

  [Obsolete("см. BB 1528729")]
  public string Caption => string.Empty;

  [Obsolete("см. BB 1528729")]
  public int ImageIndex => -1;

  [Obsolete("см. BB 1528729")]
  public int OrderID => 0;

  public void ObjectWasCheckedOut([CanBeNull] object sender, [NotNull] NotificationEventArgs e)
  {
    int index;
    if (this.CalendarOwnerID == 0L || !(e is DBObjectsCheckOutEventArgs checkOutEventArgs) || checkOutEventArgs.ObjectIDs == null || checkOutEventArgs.NewObjectIDs == null || checkOutEventArgs.ObjectIDs.Count <= 0 || !checkOutEventArgs.ObjectIDs.TryGetIndex<long>(this.CalendarOwnerID, out index) || checkOutEventArgs.NewObjectIDs.Count <= index)
      return;
    long newObjectId = checkOutEventArgs.NewObjectIDs[index];
    Intermech.Check.ObjectIdNotEmpty(newObjectId, "newCalendarOwnerID");
    if (this.CalendarOwnerID == newObjectId)
      return;
    this.CalendarOwnerID = newObjectId;
    this.InitCalendarData(false);
  }

  public void ObjectWasCheckedIn([CanBeNull] object sender, [NotNull] NotificationEventArgs e)
  {
  }

  public void ObjectChangesWasCanceled([CanBeNull] object sender, [NotNull] NotificationEventArgs e)
  {
  }

  [NotNull]
  public virtual CalendarBase GetCalendar([NotNull] IUserSession session)
  {
    throw new NotImplementedException(nameof (GetCalendar));
  }

  protected void InitCalendarData(bool selectToday = true)
  {
    this.Calendar = Session.Invoke<CalendarBase>(new Session.SessionHandler<CalendarBase>(this.GetCalendar));
    this.InitUI();
    if (selectToday)
    {
      this.CalendarUI.SelectDate(DateTime.Now);
      this.UpdateSelectedDateInfo();
    }
    this.UpdateSelectedDay();
  }

  [NotNull]
  public virtual IBlobWriter GetCalendarWriter([NotNull] IUserSession userSession)
  {
    throw new NotImplementedException(nameof (GetCalendarWriter));
  }

  protected virtual void SaveCalendarData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IBlobWriter calendarWriter = this.GetCalendarWriter(sessionKeeper.Session);
      Intermech.Diagnostics.Check.NotNull<IBlobWriter>(calendarWriter, "calendarWriter");
      Intermech.Diagnostics.Check.NotNull<CalendarBase>(this.Calendar, "Calendar");
      this.Calendar.SaveParams(calendarWriter);
      this.FireChanged(true);
    }
  }

  private void InitUI()
  {
    this.BtnSave.Enabled = false;
    this.BtnCancel.Enabled = false;
    this._selectedDay = (DayBase) null;
    this._changed = false;
    this.CalendarUI.Dates.Clear();
    foreach (CalendarEditorBase.TimeEditPair timeEditPair in this._timeEditPairs)
      timeEditPair.Clear();
    this.RbStandardWorkDay.Checked = true;
    this.CalendarUI.SelectedDates.Clear();
    if (this.Calendar == null)
      return;
    if ((WeekDay) this.CalendarUI.FirstDayOfWeek != this.Calendar.WeekStartDay - 1)
      this.CalendarUI.FirstDayOfWeek = (int) (this.Calendar.WeekStartDay - 1);
    foreach (CalendarWeekDay weekDay in (IEnumerable<CalendarWeekDay>) this.Calendar.StandardWeek.WeekDays)
    {
      if (weekDay.DayType == DayType.Holiday)
        this.CalendarUI.Dates.Add(new DateItem()
        {
          BackColor1 = Color.LightGray,
          BackColor2 = Color.LightGray,
          Pattern = mcDayInfoRecurrence.Weekly,
          Date = new DateTime(1980, 9, (int) weekDay.WeekDay),
          Range = new DateTime(2980, 1, 1),
          Tag = (object) 0,
          GradientMode = mcGradientMode.Vertical
        });
    }
    List<SpecialDay> specialDaysInPeriod = this.Calendar.GetSpecialDaysInPeriod(new DateTime(this.CalendarUI.ActiveMonth.Year, this.CalendarUI.ActiveMonth.Month, 1), new DateTime(this.CalendarUI.ActiveMonth.Year, this.CalendarUI.ActiveMonth.Month, DateTime.DaysInMonth(this.CalendarUI.ActiveMonth.Year, this.CalendarUI.ActiveMonth.Month)));
    this._spDayInfoToDateItemsList.Clear();
    foreach (SpecialDay specialDay in specialDaysInPeriod)
    {
      List<DateItem> dateItemsForDate = this.CreateDateItemsForDate(specialDay);
      this._spDayInfoToDateItemsList[specialDay] = dateItemsForDate;
    }
    this.CalendarUI.SelectedDates.Add(DateTime.Today);
    this.UpdateSelectedDateInfo();
  }

  private static void AssignColors([NotNull] DateItem dateItem, [NotNull] SpecialDay specialCalendarDayInfo)
  {
    switch (specialCalendarDayInfo.DayType)
    {
      case DayType.StandardWork:
        dateItem.BackColor1 = Color.White;
        dateItem.BackColor2 = Color.White;
        break;
      case DayType.Holiday:
        dateItem.BackColor1 = Color.LightGray;
        dateItem.BackColor2 = Color.LightGray;
        break;
      case DayType.NonStandardWork:
        dateItem.BackColor1 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192 /*0xC0*/);
        dateItem.BackColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192 /*0xC0*/);
        break;
    }
  }

  [CanBeNull]
  private List<DateItem> CreateDateItemsForDate([NotNull] SpecialDay specialCalendarDayInfo)
  {
    List<DateItem> dateItemsForDate = (List<DateItem>) null;
    switch (specialCalendarDayInfo.DateRepeatRate)
    {
      case DateRepeatRate.Once:
        DateItem dateItem1 = new DateItem();
        dateItem1.Pattern = mcDayInfoRecurrence.Daily;
        dateItem1.Date = specialCalendarDayInfo.PeriodStartDate;
        dateItem1.Range = specialCalendarDayInfo.PeriodFinishDate;
        CalendarEditorBase.AssignColors(dateItem1, specialCalendarDayInfo);
        dateItem1.GradientMode = mcGradientMode.Vertical;
        this.CalendarUI.Dates.Add(dateItem1);
        dateItemsForDate = new List<DateItem>(1);
        dateItemsForDate.Add(dateItem1);
        break;
      case DateRepeatRate.EveryWeek:
        if (specialCalendarDayInfo.PeriodFinishDate.DayOfWeek >= specialCalendarDayInfo.PeriodStartDate.DayOfWeek)
        {
          dateItemsForDate = new List<DateItem>(specialCalendarDayInfo.PeriodFinishDate.DayOfWeek - specialCalendarDayInfo.PeriodStartDate.DayOfWeek + 1);
          for (DayOfWeek dayOfWeek = specialCalendarDayInfo.PeriodStartDate.DayOfWeek; dayOfWeek <= specialCalendarDayInfo.PeriodFinishDate.DayOfWeek; ++dayOfWeek)
          {
            DateItem dateItem2 = new DateItem();
            dateItem2.Pattern = mcDayInfoRecurrence.Weekly;
            dateItem2.Date = new DateTime(1980, 9, (int) dayOfWeek);
            dateItem2.Range = new DateTime(2980, 1, 1);
            CalendarEditorBase.AssignColors(dateItem2, specialCalendarDayInfo);
            dateItem2.GradientMode = mcGradientMode.Vertical;
            this.CalendarUI.Dates.Add(dateItem2);
            dateItemsForDate.Add(dateItem2);
          }
          break;
        }
        dateItemsForDate = new List<DateItem>((int) (specialCalendarDayInfo.PeriodFinishDate.DayOfWeek + 1 + (int) (7 - CalendarsService.DayOfWeekToWeekDay(specialCalendarDayInfo.PeriodStartDate.DayOfWeek))));
        DayOfWeek dayOfWeek1 = DayOfWeek.Sunday;
        DateTime dateTime;
        while (true)
        {
          int num = (int) dayOfWeek1;
          dateTime = specialCalendarDayInfo.PeriodFinishDate;
          int dayOfWeek2 = (int) dateTime.DayOfWeek;
          if (num <= dayOfWeek2)
          {
            DateItem dateItem3 = new DateItem();
            dateItem3.Pattern = mcDayInfoRecurrence.Weekly;
            dateItem3.Date = new DateTime(1980, 9, (int) CalendarsService.DayOfWeekToWeekDay(dayOfWeek1));
            dateItem3.Range = new DateTime(2980, 1, 1);
            CalendarEditorBase.AssignColors(dateItem3, specialCalendarDayInfo);
            dateItem3.GradientMode = mcGradientMode.Vertical;
            this.CalendarUI.Dates.Add(dateItem3);
            dateItemsForDate.Add(dateItem3);
            ++dayOfWeek1;
          }
          else
            break;
        }
        dateTime = specialCalendarDayInfo.PeriodStartDate;
        for (DayOfWeek dayOfWeek3 = dateTime.DayOfWeek; dayOfWeek3 <= DayOfWeek.Saturday; ++dayOfWeek3)
        {
          DateItem dateItem4 = new DateItem();
          dateItem4.Pattern = mcDayInfoRecurrence.Weekly;
          dateItem4.Date = new DateTime(1980, 9, (int) CalendarsService.DayOfWeekToWeekDay(dayOfWeek3));
          dateItem4.Range = new DateTime(2980, 1, 1);
          CalendarEditorBase.AssignColors(dateItem4, specialCalendarDayInfo);
          dateItem4.GradientMode = mcGradientMode.Vertical;
          this.CalendarUI.Dates.Add(dateItem4);
          dateItemsForDate.Add(dateItem4);
        }
        break;
      case DateRepeatRate.EveryMonth:
        if (specialCalendarDayInfo.PeriodFinishDate.Day >= specialCalendarDayInfo.PeriodStartDate.Day)
        {
          DateItem dateItem5 = new DateItem();
          dateItem5.Pattern = mcDayInfoRecurrence.Daily;
          dateItem5.Date = new DateTime(this.CalendarUI.ActiveMonth.Year, this.CalendarUI.ActiveMonth.Month, specialCalendarDayInfo.PeriodStartDate.Day);
          dateItem5.Range = new DateTime(this.CalendarUI.ActiveMonth.Year, this.CalendarUI.ActiveMonth.Month, specialCalendarDayInfo.PeriodFinishDate.Day);
          dateItem5.GradientMode = mcGradientMode.Vertical;
          CalendarEditorBase.AssignColors(dateItem5, specialCalendarDayInfo);
          this.CalendarUI.Dates.Add(dateItem5);
          dateItemsForDate = new List<DateItem>(1);
          dateItemsForDate.Add(dateItem5);
          break;
        }
        DateItem dateItem6 = new DateItem();
        dateItem6.Pattern = mcDayInfoRecurrence.Daily;
        dateItem6.Date = new DateTime(this.CalendarUI.ActiveMonth.Year, this.CalendarUI.ActiveMonth.Month, 1);
        dateItem6.Range = new DateTime(this.CalendarUI.ActiveMonth.Year, this.CalendarUI.ActiveMonth.Month, specialCalendarDayInfo.PeriodFinishDate.Day);
        dateItem6.GradientMode = mcGradientMode.Vertical;
        CalendarEditorBase.AssignColors(dateItem6, specialCalendarDayInfo);
        this.CalendarUI.Dates.Add(dateItem6);
        dateItemsForDate = new List<DateItem>(2);
        dateItemsForDate.Add(dateItem6);
        dateItem6.Pattern = mcDayInfoRecurrence.Daily;
        dateItem6.Date = new DateTime(this.CalendarUI.ActiveMonth.Year, this.CalendarUI.ActiveMonth.Month, specialCalendarDayInfo.PeriodStartDate.Day);
        dateItem6.Range = new DateTime(this.CalendarUI.ActiveMonth.Year, this.CalendarUI.ActiveMonth.Month, DateTime.DaysInMonth(this.CalendarUI.ActiveMonth.Year, this.CalendarUI.ActiveMonth.Month));
        dateItem6.GradientMode = mcGradientMode.Vertical;
        CalendarEditorBase.AssignColors(dateItem6, specialCalendarDayInfo);
        this.CalendarUI.Dates.Add(dateItem6);
        dateItemsForDate.Add(dateItem6);
        break;
      case DateRepeatRate.EveryYear:
        if (specialCalendarDayInfo.PeriodFinishDate.Day >= specialCalendarDayInfo.PeriodStartDate.Day)
        {
          DateItem dateItem7 = new DateItem();
          dateItem7.Pattern = mcDayInfoRecurrence.Daily;
          dateItem7.Date = new DateTime(this.CalendarUI.ActiveMonth.Year, specialCalendarDayInfo.PeriodStartDate.Month, specialCalendarDayInfo.PeriodStartDate.Day);
          dateItem7.Range = new DateTime(this.CalendarUI.ActiveMonth.Year, specialCalendarDayInfo.PeriodFinishDate.Month, specialCalendarDayInfo.PeriodFinishDate.Day);
          dateItem7.GradientMode = mcGradientMode.Vertical;
          CalendarEditorBase.AssignColors(dateItem7, specialCalendarDayInfo);
          this.CalendarUI.Dates.Add(dateItem7);
          dateItemsForDate = new List<DateItem>(1);
          dateItemsForDate.Add(dateItem7);
          break;
        }
        DateItem dateItem8 = new DateItem();
        dateItem8.Pattern = mcDayInfoRecurrence.Daily;
        dateItem8.Date = new DateTime(this.CalendarUI.ActiveMonth.Year, 1, 1);
        dateItem8.Range = new DateTime(this.CalendarUI.ActiveMonth.Year, specialCalendarDayInfo.PeriodFinishDate.Month, specialCalendarDayInfo.PeriodFinishDate.Day);
        dateItem8.GradientMode = mcGradientMode.Vertical;
        CalendarEditorBase.AssignColors(dateItem8, specialCalendarDayInfo);
        this.CalendarUI.Dates.Add(dateItem8);
        dateItemsForDate = new List<DateItem>(2);
        dateItemsForDate.Add(dateItem8);
        dateItem8.Pattern = mcDayInfoRecurrence.Daily;
        dateItem8.Date = new DateTime(this.CalendarUI.ActiveMonth.Year, specialCalendarDayInfo.PeriodStartDate.Month, specialCalendarDayInfo.PeriodStartDate.Day);
        dateItem8.Range = new DateTime(this.CalendarUI.ActiveMonth.Year, 12, 31 /*0x1F*/);
        dateItem8.GradientMode = mcGradientMode.Vertical;
        CalendarEditorBase.AssignColors(dateItem8, specialCalendarDayInfo);
        this.CalendarUI.Dates.Add(dateItem8);
        dateItemsForDate.Add(dateItem8);
        break;
    }
    return dateItemsForDate;
  }

  private void LockUpdateSelectedDay() => ++this._updateSelectedDayLockCounter;

  private void UnlockUpdateSelectedDay()
  {
    if (this._updateSelectedDayLockCounter <= 0)
      return;
    --this._updateSelectedDayLockCounter;
  }

  private void UpdateSelectedDay()
  {
    if (this._updateSelectedDayLockCounter > 0)
      return;
    if (this._activeEditNumber != 0)
    {
      this.LockUpdateSelectedDay();
      try
      {
        this.RecalcTime(this._activeEditNumber);
      }
      finally
      {
        this.UnlockUpdateSelectedDay();
      }
    }
    this.CalendarUI.Month.RecalcSelected();
    this._minSelectedDate = this.CalendarUI.SelectStart < this.CalendarUI.SelectEnd ? this.CalendarUI.SelectStart : this.CalendarUI.SelectEnd;
    this._maxSelectedDate = this.CalendarUI.SelectStart > this.CalendarUI.SelectEnd ? this.CalendarUI.SelectStart : this.CalendarUI.SelectEnd;
    if (this._minSelectedDate != DateTime.MaxValue && this._minSelectedDate != DateTime.MinValue)
      this._focusedDate = this._minSelectedDate;
    else if (this._maxSelectedDate != DateTime.MaxValue && this._maxSelectedDate != DateTime.MinValue)
      this._focusedDate = DateTime.MinValue;
    this._selectedDay = this._focusedDate != DateTime.MinValue ? this.Calendar?.GetDayByDate(this._focusedDate, (DayBase) null) : (DayBase) null;
    this.BtnDelete.Enabled = this._selectedDay != null && !(this._selectedDay is CalendarWeekDay);
  }

  private void EnableTimesEdit()
  {
    if (this._timeEditPairs.Length == 0 || !this._timeEditPairs[0].FromEdit.ReadOnly)
      return;
    foreach (CalendarEditorBase.TimeEditPair timeEditPair in this._timeEditPairs)
      timeEditPair.Enable();
  }

  private void DisableTimesEdit()
  {
    if (this._timeEditPairs.Length == 0 || this._timeEditPairs[0].FromEdit.ReadOnly)
      return;
    foreach (CalendarEditorBase.TimeEditPair timeEditPair in this._timeEditPairs)
      timeEditPair.Disable();
  }

  public void UpdateSelectedDateInfo()
  {
    this.LockUIEvents();
    try
    {
      this.UpdateSelectedDay();
      if (this._selectedDay == null)
        return;
      switch (this._selectedDay.DayType)
      {
        case DayType.StandardWork:
          this.RbStandardWorkDay.Checked = true;
          this.EnableTimesEdit();
          Intermech.Diagnostics.Check.NotNull<CalendarBase>(this.Calendar, "Calendar");
          this.LoadWorkPeriods((IReadOnlyCollection<WorkTime>) this.Calendar.StandardWorkPeriods);
          break;
        case DayType.Holiday:
          this.RbHoliday.Checked = true;
          this.DisableTimesEdit();
          break;
        case DayType.NonStandardWork:
          this.RbNotStandardWorkDay.Checked = true;
          this.EnableTimesEdit();
          Intermech.Diagnostics.Check.NotNull<DayBase>(this._selectedDay, "_selectedDay");
          this.LoadWorkPeriods((IReadOnlyCollection<WorkTime>) this._selectedDay.WorkTimePeriods);
          break;
      }
      if (this._selectedDay is SpecialDay selectedDay)
        this._rbRepeatArray[(int) selectedDay.DateRepeatRate].Checked = true;
      else
        this.RbRepeat1.Checked = true;
    }
    finally
    {
      this.UnlockUIEvents();
    }
  }

  public void LoadWorkPeriods([NotNull, ItemNotNull] IReadOnlyCollection<WorkTime> workPeriods)
  {
    if (this._uiLockEditTextCounter != 0)
      return;
    bool flag = this._lastSelectedDay != this._selectedDay || this._selectedDay == null;
    this._lastSelectedDay = this._selectedDay;
    char[] chArray = new char[2]{ ' ', ':' };
    foreach (CalendarEditorBase.TimeEditPair timeEditPair in this._timeEditPairs)
    {
      if (flag)
        timeEditPair.Clear();
      else if (timeEditPair.Index >= this._selectedDay.WorkTimePeriods.Count && !string.IsNullOrEmpty(timeEditPair.FromEdit.Text) && !string.IsNullOrEmpty(timeEditPair.ToEdit.Text))
        timeEditPair.Clear();
    }
    int index = 0;
    foreach (WorkTime workPeriod in (IEnumerable<WorkTime>) workPeriods)
    {
      if (index > 4)
        break;
      CalendarEditorBase.TimeEditPair timeEditPair = this._timeEditPairs[index];
      MaskedTextBox fromEdit = timeEditPair.FromEdit;
      MaskedTextBox toEdit = timeEditPair.ToEdit;
      ++index;
      string str1 = fromEdit.Text.Trim(chArray);
      string str2 = toEdit.Text.Trim(chArray);
      if (str1 != string.Empty && str2 != string.Empty || str1 == string.Empty && str2 == string.Empty)
      {
        int num;
        string str3;
        if (workPeriod.StartHours >= 10)
        {
          num = workPeriod.StartHours;
          str3 = num.ToString();
        }
        else
          str3 = " " + (object) workPeriod.StartHours;
        string str4;
        if (workPeriod.StartMinutes >= 10)
        {
          num = workPeriod.StartMinutes;
          str4 = num.ToString();
        }
        else
          str4 = "0" + (object) workPeriod.StartMinutes;
        string str5 = $"{str3}:{str4}";
        if (this._uiLockEditTextCounter == 0)
          fromEdit.Text = str5;
        string str6;
        if (workPeriod.FinishHours >= 10)
        {
          num = workPeriod.FinishHours;
          str6 = num.ToString();
        }
        else
          str6 = " " + (object) workPeriod.FinishHours;
        string str7;
        if (workPeriod.FinishMinutes >= 10)
        {
          num = workPeriod.FinishMinutes;
          str7 = num.ToString();
        }
        else
          str7 = "0" + (object) workPeriod.FinishMinutes;
        string str8 = $"{str6}:{str7}";
        if (this._uiLockEditTextCounter == 0)
          toEdit.Text = str8;
      }
    }
  }

  private DayType UI_DayType
  {
    get
    {
      if (this.RbStandardWorkDay.Checked)
        return DayType.StandardWork;
      return !this.RbHoliday.Checked ? DayType.NonStandardWork : DayType.Holiday;
    }
  }

  [NotNull]
  [ItemNotNull]
  private IEnumerable<WorkTime> UI_WorkTimePeriodsEnumeration()
  {
    CalendarEditorBase.TimeEditPair[] timeEditPairArray = this._timeEditPairs;
    for (int index = 0; index < timeEditPairArray.Length; ++index)
    {
      WorkTime period;
      if (timeEditPairArray[index].TryGetPeriod(out period))
        yield return period;
    }
    timeEditPairArray = (CalendarEditorBase.TimeEditPair[]) null;
  }

  [NotNull]
  private WorkTime[] UI_WorkTimePeriods
  {
    get => this.UI_WorkTimePeriodsEnumeration().ToArray<WorkTime>(5);
  }

  public void CheckDateIsSpecialFromUI()
  {
    CalendarBase calendar = Intermech.Diagnostics.Check.NotNull<CalendarBase>(this.Calendar, "Calendar");
    this.BtnSave.Enabled = true;
    this.BtnCancel.Enabled = true;
    if (this._selectedDay is CalendarWeekDay selectedDay1)
    {
      if (selectedDay1.DayType == this.UI_DayType && (this.UI_DayType != DayType.StandardWork || calendar.StandardWorkPeriods.Equals((ICollection<WorkTime>) this.UI_WorkTimePeriods)) && this.UI_DayType != DayType.Holiday)
        return;
      this.UpdateSelectedDay();
      SpecialDay specialDay = new SpecialDay(calendar, this._minSelectedDate, this._maxSelectedDate, this.UI_DayType);
      calendar.AddSpecialDay(specialDay);
      List<DateItem> dateItemsForDate = this.CreateDateItemsForDate(specialDay);
      this._spDayInfoToDateItemsList[specialDay] = dateItemsForDate;
      this._changed = true;
      this._selectedDay = (DayBase) specialDay;
      this.CalendarUI.Refresh();
      this.CalendarUI.Invalidate();
    }
    else
    {
      SpecialDay selectedDay = (SpecialDay) this._selectedDay;
      if (selectedDay.DateRepeatRate != DateRepeatRate.Once)
        return;
      bool flag = false;
      for (DateTime day = selectedDay.PeriodStartDate; day <= selectedDay.PeriodFinishDate; day = day.AddDays(1.0))
      {
        DayBase dayByDate = selectedDay.Calendar.GetDayByDate(day, (DayBase) selectedDay);
        Intermech.Diagnostics.Check.NotNull<DayBase>(dayByDate, "underlineDay is null");
        if ((this.UI_DayType ^ dayByDate.DayType & DayType.Holiday) == DayType.Holiday || this.UI_DayType == DayType.NonStandardWork && !dayByDate.WorkTimePeriods.Equals((object) this.UI_WorkTimePeriods))
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      this.DeleteSelectedDay();
    }
  }

  public void LockUIEvents() => ++this._lockUIEventsCounter;

  public void UnlockUIEvents()
  {
    if (this._lockUIEventsCounter <= 0)
      return;
    --this._lockUIEventsCounter;
  }

  private void calendarUI_DayClick([CanBeNull] object sender, [NotNull] DayClickEventArgs e)
  {
    this.UpdateSelectedDay();
    if (this._selectedDay == null || !this._loaded || this._lockUIEventsCounter != 0)
      return;
    this.UpdateSelectedDateInfo();
  }

  private void UpdateSelectedDayCalendarColors()
  {
    if (!(this._selectedDay is SpecialDay selectedDay))
      return;
    List<DateItem> dayInfoToDateItems = this._spDayInfoToDateItemsList[selectedDay];
    if (dayInfoToDateItems == null)
      return;
    foreach (DateItem dateItem in dayInfoToDateItems)
      CalendarEditorBase.AssignColors(dateItem, selectedDay);
  }

  private void rbStandardWorkDay_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateSelectedDay();
    if (this._selectedDay == null || !this._loaded || this._lockUIEventsCounter != 0)
      return;
    this.LockUIEvents();
    try
    {
      this.CheckDateIsSpecialFromUI();
      Intermech.Diagnostics.Check.NotNull<DayBase>(this._selectedDay, "_selectedDay");
      this._selectedDay.DayType = DayType.StandardWork;
      this.UpdateSelectedDayCalendarColors();
      Intermech.Diagnostics.Check.NotNull<CalendarBase>(this.Calendar, "Calendar");
      this.LoadWorkPeriods((IReadOnlyCollection<WorkTime>) this.Calendar.StandardWorkPeriods);
      this.CalendarUI.Refresh();
      this.CalendarUI.Invalidate();
      this.UpdateSelectedDateInfo();
    }
    finally
    {
      this.UnlockUIEvents();
    }
    this.UpdateSelectedDay();
  }

  private void rbHoliday_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateSelectedDay();
    if (this._selectedDay == null || !this._loaded || this._lockUIEventsCounter != 0)
      return;
    this.LockUIEvents();
    try
    {
      this.CheckDateIsSpecialFromUI();
      Intermech.Diagnostics.Check.NotNull<DayBase>(this._selectedDay, "_selectedDay");
      this._selectedDay.DayType = DayType.Holiday;
      this.UpdateSelectedDayCalendarColors();
      Intermech.Diagnostics.Check.NotNull<CalendarBase>(this.Calendar, "Calendar");
      this.LoadWorkPeriods((IReadOnlyCollection<WorkTime>) this.Calendar.StandardWorkPeriods);
      this.CalendarUI.Refresh();
      this.CalendarUI.Invalidate();
      this.UpdateSelectedDateInfo();
    }
    finally
    {
      this.UnlockUIEvents();
    }
    this.UpdateSelectedDay();
  }

  private void btNotStandardWorkDay_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateSelectedDay();
    if (this._selectedDay == null || !this._loaded || this._lockUIEventsCounter != 0)
      return;
    this.LockUIEvents();
    try
    {
      this.CheckDateIsSpecialFromUI();
      Intermech.Diagnostics.Check.NotNull<DayBase>(this._selectedDay, "_selectedDay");
      this._selectedDay.DayType = DayType.NonStandardWork;
      this.UpdateSelectedDayCalendarColors();
      Intermech.Diagnostics.Check.NotNull<DayBase>(this._selectedDay, "_selectedDay");
      this.LoadWorkPeriods((IReadOnlyCollection<WorkTime>) this._selectedDay.WorkTimePeriods);
      this.CalendarUI.Refresh();
      this.CalendarUI.Invalidate();
      this.UpdateSelectedDateInfo();
    }
    finally
    {
      this.UnlockUIEvents();
    }
    this.UpdateSelectedDay();
  }

  private void rbRepeat1_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(sender is RadioButton radioButton))
      return;
    this.UpdateSelectedDay();
    if (this._selectedDay == null || !this._loaded || this._lockUIEventsCounter != 0)
      return;
    this.LockUIEvents();
    try
    {
      this.CheckDateIsSpecialFromUI();
      if (this._selectedDay is SpecialDay selectedDay1)
      {
        if (this.RbStandardWorkDay.Checked)
          this._selectedDay.DayType = DayType.StandardWork;
        else if (this.RbHoliday.Checked)
          this._selectedDay.DayType = DayType.Holiday;
        else if (this.RbNotStandardWorkDay.Checked)
          this._selectedDay.DayType = DayType.NonStandardWork;
        selectedDay1.DateRepeatRate = (DateRepeatRate) Convert.ToInt32(radioButton.Tag);
      }
      if (this._selectedDay is SpecialDay selectedDay2)
      {
        List<DateItem> dateItemsForDate = this.CreateDateItemsForDate(selectedDay2);
        this._spDayInfoToDateItemsList[selectedDay2] = dateItemsForDate;
        this.CalendarUI.Refresh();
        this.CalendarUI.Invalidate();
      }
      Intermech.Diagnostics.Check.NotNull<DayBase>(this._selectedDay, "_selectedDay");
      this.LoadWorkPeriods((IReadOnlyCollection<WorkTime>) this._selectedDay.WorkTimePeriods);
    }
    finally
    {
      this.UnlockUIEvents();
    }
    this.UpdateSelectedDateInfo();
  }

  private void btnSave_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!this.Changed || MessageBox.Show(Localization.GetString("Save_changes"), Localization.GetString("Calendar"), MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.LockUIEvents();
    try
    {
      this.SaveCalendarData();
      this.BtnSave.Enabled = false;
      this.BtnCancel.Enabled = false;
      this._changed = false;
    }
    finally
    {
      this.UnlockUIEvents();
    }
  }

  private void btnDelete_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateSelectedDay();
    if (!this.BtnDelete.Enabled)
      return;
    if (MessageBox.Show(Localization.GetString("Delete_Period"), Localization.GetString("Calendar"), MessageBoxButtons.YesNo) == DialogResult.Yes)
      this.DeleteSelectedDay();
    this.UpdateSelectedDateInfo();
  }

  private void DeleteSelectedDay()
  {
    this.LockUIEvents();
    try
    {
      if (this._selectedDay is SpecialDay selectedDay)
      {
        if (this._spDayInfoToDateItemsList.ContainsKey(selectedDay))
        {
          List<DateItem> dayInfoToDateItems = this._spDayInfoToDateItemsList[selectedDay];
          this._spDayInfoToDateItemsList.Remove(selectedDay);
          if (dayInfoToDateItems != null)
          {
            foreach (DateItem dateItem in dayInfoToDateItems)
              this.CalendarUI.Dates.Remove(dateItem);
          }
        }
        Intermech.Diagnostics.Check.NotNull<CalendarBase>(this.Calendar, "Calendar");
        this.Calendar.SpecialCalendarDays.Remove(selectedDay);
      }
      this.BtnSave.Enabled = true;
      this.BtnCancel.Enabled = true;
      this._changed = true;
      this.CalendarUI.Refresh();
      this.CalendarUI.Invalidate();
    }
    finally
    {
      this.UnlockUIEvents();
    }
    this.UpdateSelectedDateInfo();
  }

  private void OnTimeEditUpdated([CanBeNull] object sender)
  {
    if (sender is MaskedTextBox maskedTextBox && maskedTextBox.Tag != null)
      this.RecalcTime(Convert.ToInt32(maskedTextBox.Tag));
    this._activeEditNumber = 0;
  }

  private void timeEdit_Leave([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.OnTimeEditUpdated(sender);
  }

  private void RecalcTime(int editNumber)
  {
    this.UpdateSelectedDay();
    if (this._selectedDay == null || !this._loaded || this._lockUIEventsCounter != 0 || this._selectedDay.DayType == DayType.Holiday)
      return;
    this.LockUIEvents();
    try
    {
      int index1 = editNumber >= 5 ? editNumber - 5 : editNumber;
      MaskedTextBox maskedTextBox = editNumber >= 5 ? this._timeEditPairs[index1].ToEdit : this._timeEditPairs[index1].FromEdit;
      string[] strArray1 = maskedTextBox.Text.Split(':');
      if (strArray1.Length != 0)
      {
        bool flag1 = true;
        for (int index2 = strArray1.Length - 1; index2 >= 0; --index2)
        {
          strArray1[index2] = strArray1[index2].Trim();
          if (flag1 && !string.IsNullOrWhiteSpace(strArray1[index2]))
            flag1 = false;
        }
        if (flag1)
        {
          Intermech.Diagnostics.Check.NotNull<DayBase>(this._selectedDay, "_selectedDay");
          if (index1 < this._selectedDay.WorkTimePeriods.Count)
          {
            string[] strArray2 = (editNumber >= 5 ? (Control) this._timeEditPairs[index1].FromEdit : (Control) this._timeEditPairs[index1].ToEdit).Text.Split(':');
            if (strArray2.Length != 0)
            {
              for (int index3 = strArray2.Length - 1; index3 >= 0; --index3)
              {
                strArray2[index3] = strArray2[index3].Trim();
                if (flag1 && !string.IsNullOrWhiteSpace(strArray2[index3]))
                  flag1 = false;
              }
            }
            if (flag1)
            {
              if (this._selectedDay.DayType == DayType.StandardWork)
              {
                this.LockUIEvents();
                try
                {
                  this.CheckDateIsSpecialFromUI();
                  if (this._selectedDay is SpecialDay selectedDay)
                  {
                    selectedDay.DayType = DayType.NonStandardWork;
                    selectedDay.RemoveWorkPeriod(index1);
                    List<DateItem> dateItemsForDate = this.CreateDateItemsForDate(selectedDay);
                    this._spDayInfoToDateItemsList[selectedDay] = dateItemsForDate;
                    this.LoadWorkPeriods((IReadOnlyCollection<WorkTime>) selectedDay.WorkTimePeriods);
                  }
                  this.CalendarUI.Refresh();
                  this.CalendarUI.Invalidate();
                }
                finally
                {
                  this.UnlockUIEvents();
                }
              }
              else
                this._selectedDay.RemoveWorkPeriod(index1);
            }
          }
        }
        else
        {
          bool flag2 = true;
          Intermech.Diagnostics.Check.NotNull<DayBase>(this._selectedDay, "_selectedDay");
          if (index1 < this._selectedDay.WorkTimePeriods.Count)
          {
            string source = maskedTextBox.Text.Trim();
            if (source.Last<char>() == ':')
              source += "0";
            DateTime dateTime1 = Convert.ToDateTime(source);
            DateTime dateTime2 = editNumber < 5 ? new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day, this._selectedDay.WorkTimePeriods[index1].StartHours, this._selectedDay.WorkTimePeriods[index1].StartMinutes, 0) : new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day, this._selectedDay.WorkTimePeriods[index1].FinishHours, this._selectedDay.WorkTimePeriods[index1].FinishMinutes, 0);
            flag2 = (dateTime1 > dateTime2 ? dateTime1 - dateTime2 : dateTime2 - dateTime1) > new TimeSpan(0, 0, 50);
          }
          if (flag2)
          {
            bool flag3 = false;
            string[] strArray3 = (editNumber >= 5 ? (Control) this._timeEditPairs[index1].FromEdit : (Control) this._timeEditPairs[index1].ToEdit).Text.Split(':');
            if (strArray3.Length != 0)
            {
              for (int index4 = strArray3.Length - 1; index4 >= 0; --index4)
              {
                strArray3[index4] = strArray3[index4].Trim();
                if (!flag3 && !string.IsNullOrWhiteSpace(strArray3[index4]))
                  flag3 = true;
              }
            }
            if (flag3)
            {
              this.LockUIEvents();
              try
              {
                this.CheckDateIsSpecialFromUI();
                Intermech.Diagnostics.Check.NotNull<DayBase>(this._selectedDay, "_selectedDay");
                if (this._selectedDay.DayType == DayType.StandardWork)
                {
                  if (this._selectedDay is SpecialDay selectedDay)
                  {
                    selectedDay.DayType = DayType.NonStandardWork;
                    WorkTime workTimePeriod;
                    if (index1 >= selectedDay.WorkTimePeriods.Count)
                    {
                      workTimePeriod = new WorkTime();
                      selectedDay.AddWorkPeriod(workTimePeriod);
                    }
                    else
                      workTimePeriod = selectedDay.WorkTimePeriods[index1];
                    string[] strArray4 = editNumber >= 5 ? strArray3 : strArray1;
                    string[] strArray5 = editNumber >= 5 ? strArray1 : strArray3;
                    workTimePeriod.LockCorrection();
                    try
                    {
                      int result;
                      if (int.TryParse(strArray4[0], out result))
                      {
                        workTimePeriod.StartHours = result;
                        workTimePeriod.StartMinutes = strArray4.Length == 0 ? 0 : (int.TryParse(strArray4[1], out result) ? result : 0);
                        if (int.TryParse(strArray5[0], out result))
                        {
                          workTimePeriod.FinishHours = result;
                          workTimePeriod.FinishMinutes = strArray5.Length == 0 ? 0 : (int.TryParse(strArray5[1], out result) ? result : 0);
                        }
                      }
                    }
                    finally
                    {
                      workTimePeriod.UnlockCorrection();
                    }
                    List<DateItem> dateItemsForDate = this.CreateDateItemsForDate(selectedDay);
                    this._spDayInfoToDateItemsList[selectedDay] = dateItemsForDate;
                    this.LoadWorkPeriods((IReadOnlyCollection<WorkTime>) selectedDay.WorkTimePeriods);
                  }
                  this.CalendarUI.Refresh();
                  this.CalendarUI.Invalidate();
                }
                else
                {
                  WorkTime workTimePeriod;
                  if (index1 >= this._selectedDay.WorkTimePeriods.Count)
                  {
                    workTimePeriod = new WorkTime();
                    this._selectedDay.AddWorkPeriod(workTimePeriod);
                  }
                  else
                    workTimePeriod = this._selectedDay.WorkTimePeriods[index1];
                  string[] strArray6 = editNumber >= 5 ? strArray3 : strArray1;
                  string[] strArray7 = editNumber >= 5 ? strArray1 : strArray3;
                  workTimePeriod.LockCorrection();
                  try
                  {
                    int result;
                    if (int.TryParse(strArray6[0], out result))
                    {
                      workTimePeriod.StartHours = result;
                      workTimePeriod.StartMinutes = strArray6.Length == 0 ? 0 : (int.TryParse(strArray6[1], out result) ? result : 0);
                      if (int.TryParse(strArray7[0], out result))
                      {
                        workTimePeriod.FinishHours = result;
                        workTimePeriod.FinishMinutes = strArray7.Length == 0 ? 0 : (int.TryParse(strArray7[1], out result) ? result : 0);
                      }
                    }
                  }
                  finally
                  {
                    workTimePeriod.UnlockCorrection();
                  }
                }
              }
              finally
              {
                this.UnlockUIEvents();
              }
              if (this._uiLockEditTextCounter == 0)
                this._timeEditPairs[index1].Clear();
            }
          }
        }
      }
    }
    finally
    {
      this.UnlockUIEvents();
    }
    this.UpdateSelectedDateInfo();
  }

  private void btnParams_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    Intermech.Diagnostics.Check.NotNull<CalendarBase>(this.Calendar, "Calendar");
    if (new FormStandardCalendarParams(this.Calendar).ShowDialog() != DialogResult.OK)
      return;
    this.InitUI();
    this.UpdateSelectedDay();
    this.BtnSave.Enabled = true;
    this.BtnCancel.Enabled = true;
    this._changed = true;
  }

  private void CalendarEditorUserControl_Load([CanBeNull] object sender, [NotNull] EventArgs e)
  {
  }

  private void calendarUI_MonthChanged([CanBeNull] object sender, [NotNull] MonthChangedEventArgs e)
  {
    if (!this._loaded)
      return;
    this.InitUI();
    int month1 = this.CalendarUI.ActiveMonth.Month;
    DateTime today = DateTime.Today;
    int month2 = today.Month;
    int day;
    if (month1 != month2)
    {
      day = 1;
    }
    else
    {
      today = DateTime.Today;
      day = today.Day;
    }
    this.CalendarUI.SelectDate(new DateTime(this.CalendarUI.ActiveMonth.Year, this.CalendarUI.ActiveMonth.Month, day));
    this.UpdateSelectedDay();
  }

  private void btnCancel_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!this.Changed || MessageBox.Show(Localization.GetString("Undo_Changes"), Localization.GetString("Calendar"), MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.LockUIEvents();
    try
    {
      this.InitCalendarData();
      this.BtnSave.Enabled = false;
      this.BtnCancel.Enabled = false;
      this._changed = false;
    }
    finally
    {
      this.UnlockUIEvents();
    }
  }

  private void timeEdit_Enter([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(sender is MaskedTextBox maskedTextBox) || maskedTextBox.Tag == null)
      return;
    this._activeEditNumber = Convert.ToInt32(maskedTextBox.Tag);
  }

  private void timeEdit_EnterPressed([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.OnTimeEditUpdated(sender);
  }

  private void timeEdit_MouseClick([CanBeNull] object sender, [NotNull] MouseEventArgs e)
  {
    if (!(sender is MaskedTextBox maskedTextBox) || maskedTextBox.Tag == null)
      return;
    maskedTextBox.Select(0, 0);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CalendarEditorBase));
    this._groupBox1 = new GroupBox();
    this._label3 = new Label();
    this._panel3 = new Panel();
    this._label2 = new Label();
    this._panel2 = new Panel();
    this._label1 = new Label();
    this._panel1 = new Panel();
    this._btnCancel = new Button();
    this._btnSave = new Button();
    this._btnParams = new Button();
    this._label4 = new Label();
    this._rbStandardWorkDay = new RadioButton();
    this._rbHoliday = new RadioButton();
    this._rbNotStandardWorkDay = new RadioButton();
    this._timeEditFrom1 = new MaskedEdit();
    this._timeEditTo1 = new MaskedEdit();
    this._timeEditTo2 = new MaskedEdit();
    this._timeEditFrom2 = new MaskedEdit();
    this._timeEditTo3 = new MaskedEdit();
    this._timeEditFrom3 = new MaskedEdit();
    this._timeEditTo4 = new MaskedEdit();
    this._timeEditFrom4 = new MaskedEdit();
    this._timeEditTo5 = new MaskedEdit();
    this._timeEditFrom5 = new MaskedEdit();
    this._label5 = new Label();
    this._label6 = new Label();
    this._label7 = new Label();
    this._groupBox2 = new GroupBox();
    this._btnDelete = new Button();
    this._rbRepeat4 = new RadioButton();
    this._rbRepeat3 = new RadioButton();
    this._rbRepeat2 = new RadioButton();
    this._rbRepeat1 = new RadioButton();
    this._groupBox3 = new GroupBox();
    this._calendarUI = new Pabo.Calendar.MonthCalendar();
    this._groupBox1.SuspendLayout();
    this._groupBox2.SuspendLayout();
    this._groupBox3.SuspendLayout();
    this.SuspendLayout();
    this._groupBox1.Controls.Add((Control) this._label3);
    this._groupBox1.Controls.Add((Control) this._panel3);
    this._groupBox1.Controls.Add((Control) this._label2);
    this._groupBox1.Controls.Add((Control) this._panel2);
    this._groupBox1.Controls.Add((Control) this._label1);
    this._groupBox1.Controls.Add((Control) this._panel1);
    componentResourceManager.ApplyResources((object) this._groupBox1, "_groupBox1");
    this._groupBox1.Name = "_groupBox1";
    this._groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this._label3, "_label3");
    this._label3.Name = "_label3";
    componentResourceManager.ApplyResources((object) this._panel3, "_panel3");
    this._panel3.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192 /*0xC0*/);
    this._panel3.BorderStyle = BorderStyle.FixedSingle;
    this._panel3.Name = "_panel3";
    componentResourceManager.ApplyResources((object) this._label2, "_label2");
    this._label2.Name = "_label2";
    componentResourceManager.ApplyResources((object) this._panel2, "_panel2");
    this._panel2.BackColor = Color.LightGray;
    this._panel2.BorderStyle = BorderStyle.FixedSingle;
    this._panel2.Name = "_panel2";
    componentResourceManager.ApplyResources((object) this._label1, "_label1");
    this._label1.Name = "_label1";
    componentResourceManager.ApplyResources((object) this._panel1, "_panel1");
    this._panel1.BackColor = Color.White;
    this._panel1.BorderStyle = BorderStyle.FixedSingle;
    this._panel1.Name = "_panel1";
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this._btnSave, "_btnSave");
    this._btnSave.Name = "_btnSave";
    this._btnSave.UseVisualStyleBackColor = true;
    this._btnSave.Click += new EventHandler(this.btnSave_Click);
    componentResourceManager.ApplyResources((object) this._btnParams, "_btnParams");
    this._btnParams.Name = "_btnParams";
    this._btnParams.UseVisualStyleBackColor = true;
    this._btnParams.Click += new EventHandler(this.btnParams_Click);
    componentResourceManager.ApplyResources((object) this._label4, "_label4");
    this._label4.Name = "_label4";
    componentResourceManager.ApplyResources((object) this._rbStandardWorkDay, "_rbStandardWorkDay");
    this._rbStandardWorkDay.Checked = true;
    this._rbStandardWorkDay.Name = "_rbStandardWorkDay";
    this._rbStandardWorkDay.TabStop = true;
    this._rbStandardWorkDay.UseVisualStyleBackColor = true;
    this._rbStandardWorkDay.Click += new EventHandler(this.rbStandardWorkDay_Click);
    componentResourceManager.ApplyResources((object) this._rbHoliday, "_rbHoliday");
    this._rbHoliday.Name = "_rbHoliday";
    this._rbHoliday.UseVisualStyleBackColor = true;
    this._rbHoliday.Click += new EventHandler(this.rbHoliday_Click);
    componentResourceManager.ApplyResources((object) this._rbNotStandardWorkDay, "_rbNotStandardWorkDay");
    this._rbNotStandardWorkDay.Name = "_rbNotStandardWorkDay";
    this._rbNotStandardWorkDay.UseVisualStyleBackColor = true;
    this._rbNotStandardWorkDay.Click += new EventHandler(this.btNotStandardWorkDay_Click);
    componentResourceManager.ApplyResources((object) this._timeEditFrom1, "_timeEditFrom1");
    this._timeEditFrom1.Name = "_timeEditFrom1";
    this._timeEditFrom1.Tag = (object) "0";
    this._timeEditFrom1.ValidatingType = typeof (DateTime);
    this._timeEditFrom1.EnterPressed += new EventHandler(this.timeEdit_EnterPressed);
    this._timeEditFrom1.Enter += new EventHandler(this.timeEdit_Enter);
    this._timeEditFrom1.Leave += new EventHandler(this.timeEdit_Leave);
    this._timeEditFrom1.MouseClick += new MouseEventHandler(this.timeEdit_MouseClick);
    componentResourceManager.ApplyResources((object) this._timeEditTo1, "_timeEditTo1");
    this._timeEditTo1.Name = "_timeEditTo1";
    this._timeEditTo1.Tag = (object) "5";
    this._timeEditTo1.ValidatingType = typeof (DateTime);
    this._timeEditTo1.EnterPressed += new EventHandler(this.timeEdit_EnterPressed);
    this._timeEditTo1.Enter += new EventHandler(this.timeEdit_Enter);
    this._timeEditTo1.Leave += new EventHandler(this.timeEdit_Leave);
    this._timeEditTo1.MouseClick += new MouseEventHandler(this.timeEdit_MouseClick);
    componentResourceManager.ApplyResources((object) this._timeEditTo2, "_timeEditTo2");
    this._timeEditTo2.Name = "_timeEditTo2";
    this._timeEditTo2.Tag = (object) "6";
    this._timeEditTo2.ValidatingType = typeof (DateTime);
    this._timeEditTo2.EnterPressed += new EventHandler(this.timeEdit_EnterPressed);
    this._timeEditTo2.Enter += new EventHandler(this.timeEdit_Enter);
    this._timeEditTo2.Leave += new EventHandler(this.timeEdit_Leave);
    this._timeEditTo2.MouseClick += new MouseEventHandler(this.timeEdit_MouseClick);
    componentResourceManager.ApplyResources((object) this._timeEditFrom2, "_timeEditFrom2");
    this._timeEditFrom2.Name = "_timeEditFrom2";
    this._timeEditFrom2.Tag = (object) "1";
    this._timeEditFrom2.ValidatingType = typeof (DateTime);
    this._timeEditFrom2.EnterPressed += new EventHandler(this.timeEdit_EnterPressed);
    this._timeEditFrom2.Enter += new EventHandler(this.timeEdit_Enter);
    this._timeEditFrom2.Leave += new EventHandler(this.timeEdit_Leave);
    this._timeEditFrom2.MouseClick += new MouseEventHandler(this.timeEdit_MouseClick);
    componentResourceManager.ApplyResources((object) this._timeEditTo3, "_timeEditTo3");
    this._timeEditTo3.Name = "_timeEditTo3";
    this._timeEditTo3.Tag = (object) "7";
    this._timeEditTo3.ValidatingType = typeof (DateTime);
    this._timeEditTo3.EnterPressed += new EventHandler(this.timeEdit_EnterPressed);
    this._timeEditTo3.Enter += new EventHandler(this.timeEdit_Enter);
    this._timeEditTo3.Leave += new EventHandler(this.timeEdit_Leave);
    this._timeEditTo3.MouseClick += new MouseEventHandler(this.timeEdit_MouseClick);
    componentResourceManager.ApplyResources((object) this._timeEditFrom3, "_timeEditFrom3");
    this._timeEditFrom3.Name = "_timeEditFrom3";
    this._timeEditFrom3.Tag = (object) "2";
    this._timeEditFrom3.ValidatingType = typeof (DateTime);
    this._timeEditFrom3.EnterPressed += new EventHandler(this.timeEdit_EnterPressed);
    this._timeEditFrom3.Enter += new EventHandler(this.timeEdit_Enter);
    this._timeEditFrom3.Leave += new EventHandler(this.timeEdit_Leave);
    this._timeEditFrom3.MouseClick += new MouseEventHandler(this.timeEdit_MouseClick);
    componentResourceManager.ApplyResources((object) this._timeEditTo4, "_timeEditTo4");
    this._timeEditTo4.Name = "_timeEditTo4";
    this._timeEditTo4.Tag = (object) "8";
    this._timeEditTo4.ValidatingType = typeof (DateTime);
    this._timeEditTo4.EnterPressed += new EventHandler(this.timeEdit_EnterPressed);
    this._timeEditTo4.Enter += new EventHandler(this.timeEdit_Enter);
    this._timeEditTo4.Leave += new EventHandler(this.timeEdit_Leave);
    this._timeEditTo4.MouseClick += new MouseEventHandler(this.timeEdit_MouseClick);
    componentResourceManager.ApplyResources((object) this._timeEditFrom4, "_timeEditFrom4");
    this._timeEditFrom4.Name = "_timeEditFrom4";
    this._timeEditFrom4.Tag = (object) "3";
    this._timeEditFrom4.ValidatingType = typeof (DateTime);
    this._timeEditFrom4.EnterPressed += new EventHandler(this.timeEdit_EnterPressed);
    this._timeEditFrom4.Enter += new EventHandler(this.timeEdit_Enter);
    this._timeEditFrom4.Leave += new EventHandler(this.timeEdit_Leave);
    this._timeEditFrom4.MouseClick += new MouseEventHandler(this.timeEdit_MouseClick);
    componentResourceManager.ApplyResources((object) this._timeEditTo5, "_timeEditTo5");
    this._timeEditTo5.Name = "_timeEditTo5";
    this._timeEditTo5.Tag = (object) "9";
    this._timeEditTo5.ValidatingType = typeof (DateTime);
    this._timeEditTo5.EnterPressed += new EventHandler(this.timeEdit_EnterPressed);
    this._timeEditTo5.Enter += new EventHandler(this.timeEdit_Enter);
    this._timeEditTo5.Leave += new EventHandler(this.timeEdit_Leave);
    this._timeEditTo5.MouseClick += new MouseEventHandler(this.timeEdit_MouseClick);
    componentResourceManager.ApplyResources((object) this._timeEditFrom5, "_timeEditFrom5");
    this._timeEditFrom5.Name = "_timeEditFrom5";
    this._timeEditFrom5.Tag = (object) "4";
    this._timeEditFrom5.ValidatingType = typeof (DateTime);
    this._timeEditFrom5.EnterPressed += new EventHandler(this.timeEdit_EnterPressed);
    this._timeEditFrom5.Enter += new EventHandler(this.timeEdit_Enter);
    this._timeEditFrom5.Leave += new EventHandler(this.timeEdit_Leave);
    this._timeEditFrom5.MouseClick += new MouseEventHandler(this.timeEdit_MouseClick);
    componentResourceManager.ApplyResources((object) this._label5, "_label5");
    this._label5.Name = "_label5";
    componentResourceManager.ApplyResources((object) this._label6, "_label6");
    this._label6.Name = "_label6";
    componentResourceManager.ApplyResources((object) this._label7, "_label7");
    this._label7.Name = "_label7";
    this._groupBox2.Controls.Add((Control) this._btnDelete);
    this._groupBox2.Controls.Add((Control) this._rbRepeat4);
    this._groupBox2.Controls.Add((Control) this._rbRepeat3);
    this._groupBox2.Controls.Add((Control) this._rbRepeat2);
    this._groupBox2.Controls.Add((Control) this._rbRepeat1);
    componentResourceManager.ApplyResources((object) this._groupBox2, "_groupBox2");
    this._groupBox2.Name = "_groupBox2";
    this._groupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this._btnDelete, "_btnDelete");
    this._btnDelete.Name = "_btnDelete";
    this._btnDelete.UseVisualStyleBackColor = true;
    this._btnDelete.Click += new EventHandler(this.btnDelete_Click);
    componentResourceManager.ApplyResources((object) this._rbRepeat4, "_rbRepeat4");
    this._rbRepeat4.Name = "_rbRepeat4";
    this._rbRepeat4.Tag = (object) "3";
    this._rbRepeat4.UseVisualStyleBackColor = true;
    this._rbRepeat4.Click += new EventHandler(this.rbRepeat1_Click);
    componentResourceManager.ApplyResources((object) this._rbRepeat3, "_rbRepeat3");
    this._rbRepeat3.Name = "_rbRepeat3";
    this._rbRepeat3.Tag = (object) "2";
    this._rbRepeat3.UseVisualStyleBackColor = true;
    this._rbRepeat3.Click += new EventHandler(this.rbRepeat1_Click);
    componentResourceManager.ApplyResources((object) this._rbRepeat2, "_rbRepeat2");
    this._rbRepeat2.Name = "_rbRepeat2";
    this._rbRepeat2.Tag = (object) "1";
    this._rbRepeat2.UseVisualStyleBackColor = true;
    this._rbRepeat2.Click += new EventHandler(this.rbRepeat1_Click);
    componentResourceManager.ApplyResources((object) this._rbRepeat1, "_rbRepeat1");
    this._rbRepeat1.Checked = true;
    this._rbRepeat1.Name = "_rbRepeat1";
    this._rbRepeat1.TabStop = true;
    this._rbRepeat1.Tag = (object) "0";
    this._rbRepeat1.UseVisualStyleBackColor = true;
    this._rbRepeat1.Click += new EventHandler(this.rbRepeat1_Click);
    this._groupBox3.Controls.Add((Control) this._rbStandardWorkDay);
    this._groupBox3.Controls.Add((Control) this._rbHoliday);
    this._groupBox3.Controls.Add((Control) this._rbNotStandardWorkDay);
    this._groupBox3.Controls.Add((Control) this._timeEditFrom1);
    this._groupBox3.Controls.Add((Control) this._timeEditTo1);
    this._groupBox3.Controls.Add((Control) this._timeEditFrom2);
    this._groupBox3.Controls.Add((Control) this._timeEditTo2);
    this._groupBox3.Controls.Add((Control) this._timeEditFrom3);
    this._groupBox3.Controls.Add((Control) this._timeEditTo3);
    this._groupBox3.Controls.Add((Control) this._timeEditFrom4);
    this._groupBox3.Controls.Add((Control) this._timeEditTo4);
    this._groupBox3.Controls.Add((Control) this._timeEditFrom5);
    this._groupBox3.Controls.Add((Control) this._timeEditTo5);
    componentResourceManager.ApplyResources((object) this._groupBox3, "_groupBox3");
    this._groupBox3.Name = "_groupBox3";
    this._groupBox3.TabStop = false;
    this._calendarUI.ActiveMonth.Month = 8;
    this._calendarUI.ActiveMonth.Year = 2009;
    this._calendarUI.BorderColor = Color.FromArgb(197, 198, 214);
    this._calendarUI.Culture = new CultureInfo("ru-RU");
    this._calendarUI.Footer.BackColor2 = SystemColors.ButtonFace;
    this._calendarUI.Footer.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
    this._calendarUI.Footer.GradientMode = mcGradientMode.Vertical;
    this._calendarUI.Header.BackColor1 = Color.FromArgb(177, 179, 200);
    this._calendarUI.Header.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold);
    this._calendarUI.Header.GradientMode = mcGradientMode.Vertical;
    this._calendarUI.Header.TextColor = SystemColors.ActiveCaptionText;
    this._calendarUI.ImageList = (ImageList) null;
    componentResourceManager.ApplyResources((object) this._calendarUI, "_calendarUI");
    this._calendarUI.MaxDate = new DateTime(2999, 8, 16 /*0x10*/, 0, 0, 0, 0);
    this._calendarUI.MinDate = new DateTime(1999, 8, 16 /*0x10*/, 16 /*0x10*/, 51, 6, 789);
    this._calendarUI.Month.BackgroundImage = (Image) null;
    this._calendarUI.Month.BorderStyles.Selected = ButtonBorderStyle.Dotted;
    this._calendarUI.Month.Colors.Focus.BackColor = Color.FromArgb(211, 213, 224 /*0xE0*/);
    this._calendarUI.Month.Colors.Focus.Border = Color.FromArgb(197, 198, 214);
    this._calendarUI.Month.Colors.Selected.BackColor = Color.FromArgb(197, 198, 214);
    this._calendarUI.Month.Colors.Selected.Border = Color.FromArgb(95, 97, 135);
    this._calendarUI.Month.Colors.Weekend.BackColor1 = Color.Gray;
    this._calendarUI.Month.Colors.Weekend.BackColor2 = Color.Gray;
    this._calendarUI.Month.DateFont = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold);
    this._calendarUI.Month.TextFont = new Font("Microsoft Sans Serif", 8.25f);
    this._calendarUI.Month.Transparency.Background = 0;
    this._calendarUI.Month.Transparency.Text = (int) byte.MaxValue;
    this._calendarUI.Name = "_calendarUI";
    this._calendarUI.SelectEnd = new DateTime(0L);
    this._calendarUI.SelectStart = new DateTime(9999, 12, 31 /*0x1F*/, 23, 59, 59, 999);
    this._calendarUI.ShowFooter = false;
    this._calendarUI.Theme = true;
    this._calendarUI.Weekdays.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
    this._calendarUI.Weekdays.TextColor = Color.FromArgb(166, 164, 186);
    this._calendarUI.Weeknumbers.Font = new Font("Microsoft Sans Serif", 8.25f);
    this._calendarUI.Weeknumbers.TextColor = Color.FromArgb(166, 164, 186);
    this._calendarUI.MonthChanged += new MonthChangedEventHandler(this.calendarUI_MonthChanged);
    this._calendarUI.DayClick += new DayClickEventHandler(this.calendarUI_DayClick);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._groupBox2);
    this.Controls.Add((Control) this._calendarUI);
    this.Controls.Add((Control) this._label7);
    this.Controls.Add((Control) this._label6);
    this.Controls.Add((Control) this._label5);
    this.Controls.Add((Control) this._label4);
    this.Controls.Add((Control) this._btnParams);
    this.Controls.Add((Control) this._btnSave);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._groupBox1);
    this.Controls.Add((Control) this._groupBox3);
    this.Name = nameof (CalendarEditorBase);
    this.Load += new EventHandler(this.CalendarEditorUserControl_Load);
    this._groupBox1.ResumeLayout(false);
    this._groupBox1.PerformLayout();
    this._groupBox2.ResumeLayout(false);
    this._groupBox2.PerformLayout();
    this._groupBox3.ResumeLayout(false);
    this._groupBox3.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
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
  protected Button BtnSave
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnSave.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Button BtnParams
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnParams.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected RadioButton RbStandardWorkDay
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rbStandardWorkDay.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected RadioButton RbHoliday
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rbHoliday.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected RadioButton RbNotStandardWorkDay
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rbNotStandardWorkDay.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedEdit TimeEditFrom1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditFrom1.CheckInitializedIn<MaskedEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedEdit TimeEditTo1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditTo1.CheckInitializedIn<MaskedEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedEdit TimeEditTo2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditTo2.CheckInitializedIn<MaskedEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedEdit TimeEditFrom2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditFrom2.CheckInitializedIn<MaskedEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedEdit TimeEditTo3
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditTo3.CheckInitializedIn<MaskedEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedEdit TimeEditFrom3
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditFrom3.CheckInitializedIn<MaskedEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedEdit TimeEditTo4
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditTo4.CheckInitializedIn<MaskedEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedEdit TimeEditFrom4
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditFrom4.CheckInitializedIn<MaskedEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedEdit TimeEditTo5
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditTo5.CheckInitializedIn<MaskedEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected MaskedEdit TimeEditFrom5
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timeEditFrom5.CheckInitializedIn<MaskedEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Pabo.Calendar.MonthCalendar CalendarUI
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._calendarUI.CheckInitializedIn<Pabo.Calendar.MonthCalendar>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected RadioButton RbRepeat4
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rbRepeat4.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected RadioButton RbRepeat3
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rbRepeat3.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected RadioButton RbRepeat2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rbRepeat2.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected RadioButton RbRepeat1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rbRepeat1.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Button BtnDelete
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnDelete.CheckInitializedIn<Button>((object) this);
    }
  }

  protected class Description : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList))
        ServicesManager.GetService(typeof (INamedImageList));
      return new ViewDescription()
      {
        Caption = Localization.GetString("Calendar_Editor"),
        ImageIndex = -1,
        OrderID = 0
      };
    }
  }

  private readonly struct TimeEditPair : IEquatable<CalendarEditorBase.TimeEditPair>
  {
    [NotNull]
    [UsedImplicitly]
    public MaskedTextBox FromEdit { get; }

    [NotNull]
    [UsedImplicitly]
    public MaskedTextBox ToEdit { get; }

    [UsedImplicitly]
    public int Index { get; }

    private TimeEditPair(int index, [NotNull] MaskedTextBox fromEdit, [NotNull] MaskedTextBox toEdit)
    {
      this.FromEdit = fromEdit;
      this.ToEdit = toEdit;
      this.Index = index;
    }

    public static implicit operator CalendarEditorBase.TimeEditPair(
      (int, MaskedTextBox, MaskedTextBox) tuple)
    {
      return new CalendarEditorBase.TimeEditPair(tuple.Item1, tuple.Item2, tuple.Item3);
    }

    private bool TryGetDateTime(out DateTime fromTime, out DateTime toTime)
    {
      toTime = DateTime.MinValue;
      string str1 = this.FromEdit.Text.Trim();
      if (str1.Last<char>() == ':')
        str1 += "0";
      string str2 = this.ToEdit.Text.Trim();
      if (str2.Last<char>() == ':')
        str2 += "0";
      int num = !DateTime.TryParseExact(str1, "H:m", (IFormatProvider) CultureInfo.CurrentUICulture, DateTimeStyles.None, out fromTime) ? 0 : (DateTime.TryParseExact(str2, "H:m", (IFormatProvider) CultureInfo.CurrentUICulture, DateTimeStyles.None, out toTime) ? 1 : 0);
      if (num == 0)
        return num != 0;
      if (!(fromTime > toTime))
        return num != 0;
      toTime = fromTime;
      return num != 0;
    }

    [ContractAnnotation("=> true, period: notnull; => false, period: null")]
    public bool TryGetPeriod([CanBeNull] out WorkTime period)
    {
      period = (WorkTime) null;
      DateTime fromTime;
      DateTime toTime;
      if (!this.TryGetDateTime(out fromTime, out toTime))
        return false;
      period = new WorkTime(fromTime.Hour, fromTime.Minute, toTime.Hour, toTime.Minute);
      return true;
    }

    public bool Equals(CalendarEditorBase.TimeEditPair other)
    {
      return this.Index == other.Index && this.FromEdit == other.FromEdit && this.ToEdit == other.ToEdit;
    }

    public override bool Equals(object obj)
    {
      return obj is CalendarEditorBase.TimeEditPair other && this.Equals(other);
    }

    public override int GetHashCode() => (this.Index, this.FromEdit, this.ToEdit).GetHashCode();

    public static bool operator ==(
      CalendarEditorBase.TimeEditPair left,
      CalendarEditorBase.TimeEditPair right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(
      CalendarEditorBase.TimeEditPair left,
      CalendarEditorBase.TimeEditPair right)
    {
      return !left.Equals(right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
      this.FromEdit.Clear();
      this.ToEdit.Clear();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Enable()
    {
      this.FromEdit.Enable();
      this.ToEdit.Enable();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Disable()
    {
      this.FromEdit.Disable();
      this.ToEdit.Disable();
    }
  }
}
