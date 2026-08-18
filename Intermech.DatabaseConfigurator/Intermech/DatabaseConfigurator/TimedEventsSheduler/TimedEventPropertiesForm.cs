// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.TimedEventsSheduler.TimedEventPropertiesForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.TimedEventsSheduler;

public class TimedEventPropertiesForm : Form
{
  public TimedEventProperties properties;
  public Dictionary<Guid, string> eventHandlersInfo = new Dictionary<Guid, string>();
  private FormMode mode;
  private string dateTimeFormat = $"{DateTimeFormatInfo.CurrentInfo.LongDatePattern} {DateTimeFormatInfo.CurrentInfo.ShortTimePattern}";
  private string yearlyDateTimeFormat = "ddMMMM" + DateTimeFormatInfo.CurrentInfo.ShortTimePattern;
  private string timeFormat = DateTimeFormatInfo.CurrentInfo.ShortTimePattern;
  private bool autoChangeTaskName;
  private BackgroundWorker worker;
  private IContainer components;
  private Label label1;
  private Label label2;
  private Label label3;
  private Label label4;
  private Button btnOK;
  private Button btnCancel;
  private ComboBox cbTask;
  private TextBox tbEventName;
  private CheckBox cbImmediatly;
  private Label label5;
  private ComboBox cbTaskType;
  private DateTimePicker dtSchedule;
  private Label lbDays;
  private CheckedListBox clbWeek;
  private NumericUpDown numHours;
  private Label lbDate;
  private TextBox tbDate;
  private ComboBox cbServerName;
  private Label lblStep;

  public TimedEventPropertiesForm(Dictionary<Guid, string> eventHandlersInfo)
    : this(FormMode.Create, new TimedEventProperties(), eventHandlersInfo)
  {
  }

  public TimedEventPropertiesForm(
    FormMode mode,
    TimedEventProperties properties,
    Dictionary<Guid, string> eventHandlersInfo)
  {
    this.InitializeComponent();
    this.properties = properties;
    this.mode = mode;
    this.eventHandlersInfo = eventHandlersInfo;
    this.Text = mode == FormMode.Create ? "Создание задачи" : "Редактирование задачи";
    this.cbTask.Enabled = this.autoChangeTaskName = mode == FormMode.Create;
    this.worker = new BackgroundWorker();
    this.worker.WorkerSupportsCancellation = true;
    this.worker.WorkerReportsProgress = true;
    this.worker.DoWork += new DoWorkEventHandler(this.worker_DoWork);
    this.worker.ProgressChanged += new ProgressChangedEventHandler(this.worker_ProgressChanged);
    this.LoadData();
  }

  private void LoadData()
  {
    foreach (KeyValuePair<Guid, string> keyValuePair in (IEnumerable<KeyValuePair<Guid, string>>) this.eventHandlersInfo.OrderBy<KeyValuePair<Guid, string>, string>((Func<KeyValuePair<Guid, string>, string>) (pair => pair.Value)))
      this.cbTask.Items.Add((object) new MyElement((object) keyValuePair.Key, this.eventHandlersInfo[keyValuePair.Key], (object) keyValuePair.Key));
    foreach (TimedEventKinds tag in Enum.GetValues(typeof (TimedEventKinds)))
      this.cbTaskType.Items.Add((object) new MyElement((object) tag, EnumDescConverter.GetEnumDescription((Enum) tag), (object) tag));
    foreach (DayOfWeek dayofweek in Enum.GetValues(typeof (DayOfWeek)))
      this.clbWeek.Items.Add((object) DateTimeFormatInfo.CurrentInfo.GetDayName(dayofweek));
    this.FindServers();
    if (this.mode == FormMode.Edit)
    {
      this.cbImmediatly.Checked = this.properties.EventKind != TimedEventKinds.Once && this.properties.ImmediateRun;
      this.cbTask.SelectedItem = (object) new MyElement((object) this.properties.ServiceGuid, this.eventHandlersInfo[this.properties.ServiceGuid], (object) this.properties.ServiceGuid);
      this.cbTaskType.SelectedItem = (object) new MyElement((object) this.properties.EventKind, EnumDescConverter.GetEnumDescription((Enum) this.properties.EventKind), (object) this.properties.EventKind);
      this.tbEventName.Text = this.properties.Name;
      this.cbServerName.Text = this.properties.ServerName;
      if (!this.cbServerName.Items.Contains((object) this.properties.ServerName))
      {
        this.cbServerName.Items.Add((object) this.properties.ServerName);
        this.cbServerName.SelectedIndex = this.cbServerName.Items.Count - 1;
      }
      else
        this.cbServerName.SelectedIndex = this.cbServerName.Items.IndexOf((object) this.properties.ServerName);
      this.PrepareDateTimeControl(this.properties.EventKind, this.properties.StartDate, this.properties.Schedule);
    }
    else
    {
      if (this.cbTaskType.Items.Count > 0)
        this.cbTaskType.SelectedIndex = 0;
      if (this.cbTask.Items.Count <= 0)
        return;
      this.cbTask.SelectedIndex = 0;
    }
  }

  private void FindServers()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITimedEventsSheduler customService = (ITimedEventsSheduler) sessionKeeper.Session.GetCustomService(typeof (ITimedEventsSheduler));
      if (customService == null || this.worker.IsBusy)
        return;
      this.cbServerName.Items.Clear();
      string primaryServer = customService.GetPrimaryServer(sessionKeeper.Session.SessionGUID);
      if (primaryServer != string.Empty)
        this.cbServerName.Items.Add((object) string.Empty);
      this.cbServerName.Items.Add((object) primaryServer);
      this.cbServerName.SelectedIndex = this.cbServerName.Items.Count - 1;
      this.worker.RunWorkerAsync((object) primaryServer);
    }
  }

  private void PrepareDateTimeControl(
    TimedEventKinds eventKind,
    DateTime startDate,
    string schedule)
  {
    switch (eventKind)
    {
      case TimedEventKinds.Once:
        this.dtSchedule.CustomFormat = this.dateTimeFormat;
        this.dtSchedule.ShowUpDown = false;
        this.dtSchedule.Value = startDate.ToLocalTime();
        this.lblStep.Text = LocalizationHolder.rm.GetString("StartDate");
        break;
      case TimedEventKinds.Hourly:
        this.numHours.Value = !(schedule == string.Empty) ? (Decimal) Convert.ToInt32(schedule) : 1M;
        this.lblStep.Text = LocalizationHolder.rm.GetString("StepInHours");
        break;
      case TimedEventKinds.Daily:
        this.dtSchedule.CustomFormat = this.timeFormat;
        this.dtSchedule.ShowUpDown = true;
        this.dtSchedule.Value = !(schedule != string.Empty) ? DateTime.Now : DateTime.Parse(schedule, (IFormatProvider) CultureInfo.InvariantCulture).ToLocalTime();
        this.lblStep.Text = LocalizationHolder.rm.GetString("StartTime");
        break;
      case TimedEventKinds.Weekly:
        this.dtSchedule.CustomFormat = this.timeFormat;
        this.dtSchedule.ShowUpDown = true;
        if (schedule != string.Empty)
        {
          string[] strArray = schedule.Split(',');
          this.dtSchedule.Value = DateTime.Parse(strArray[0], (IFormatProvider) CultureInfo.InvariantCulture).ToLocalTime();
          this.clbWeek.ClearSelected();
          for (int index = 1; index < strArray.Length; ++index)
            this.clbWeek.SetItemCheckState(Convert.ToInt32(strArray[index]), CheckState.Checked);
        }
        else
          this.dtSchedule.Value = DateTime.Now;
        this.lblStep.Text = LocalizationHolder.rm.GetString("StartTime");
        break;
      case TimedEventKinds.Monthly:
        this.dtSchedule.CustomFormat = this.timeFormat;
        this.dtSchedule.ShowUpDown = true;
        if (schedule != string.Empty)
        {
          string[] lst2 = schedule.Split(',');
          this.dtSchedule.Value = DateTime.Parse(lst2[0], (IFormatProvider) CultureInfo.InvariantCulture).ToLocalTime();
          this.tbDate.Text = DateStringWithPeriod.ConvertToString(lst2);
        }
        else
          this.dtSchedule.Value = DateTime.Now;
        this.lblStep.Text = LocalizationHolder.rm.GetString("StartTime");
        break;
      case TimedEventKinds.Yearly:
        this.dtSchedule.CustomFormat = this.yearlyDateTimeFormat;
        this.dtSchedule.ShowUpDown = false;
        if (schedule != string.Empty)
        {
          string[] strArray = schedule.Split(',');
          this.dtSchedule.Value = new DateTime(DateTime.UtcNow.Year + 1, Convert.ToInt32(strArray[3]), Convert.ToInt32(strArray[2]), Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), 0, DateTimeKind.Utc).ToLocalTime();
        }
        else
          this.dtSchedule.Value = DateTime.Now;
        this.lblStep.Text = LocalizationHolder.rm.GetString("StartDate");
        break;
      case TimedEventKinds.Minutely:
        this.numHours.Value = !(schedule == string.Empty) ? (Decimal) Convert.ToInt32(schedule) : 60M;
        this.lblStep.Text = LocalizationHolder.rm.GetString("StepInMinutes");
        break;
    }
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITimedEventsSheduler customService = (ITimedEventsSheduler) sessionKeeper.Session.GetCustomService(typeof (ITimedEventsSheduler));
      Guid guid = (Guid) (this.cbTask.SelectedItem as MyElement).Value;
      TimedEventKinds eventKind = (TimedEventKinds) (this.cbTaskType.SelectedItem as MyElement).Value;
      this.properties.ServiceGuid = guid;
      this.properties.EventKind = eventKind;
      this.properties.ServerName = this.cbServerName.Text.ToUpper();
      this.properties.Name = this.tbEventName.Text;
      this.properties.ImmediateRun = this.cbImmediatly.Checked;
      this.properties.Schedule = this.FormShedule(eventKind);
      if (!(this.properties.Schedule != string.Empty))
        return;
      if (eventKind == TimedEventKinds.Once)
      {
        this.properties.StartDate = this.dtSchedule.Value.ToUniversalTime();
        this.properties.ImmediateRun = false;
      }
      this.properties = this.mode != FormMode.Create ? customService.EditEvent(sessionKeeper.Session.SessionGUID, this.properties) : customService.AddEvent(sessionKeeper.Session.SessionGUID, this.properties);
      this.DialogResult = DialogResult.OK;
    }
  }

  private string FormShedule(TimedEventKinds eventKind)
  {
    string str1 = string.Empty;
    switch (eventKind)
    {
      case TimedEventKinds.Once:
        DateTime universalTime1 = this.dtSchedule.Value;
        universalTime1 = universalTime1.ToUniversalTime();
        str1 = universalTime1.ToString();
        break;
      case TimedEventKinds.Hourly:
        str1 = this.numHours.Value.ToString();
        break;
      case TimedEventKinds.Daily:
        DateTime universalTime2 = this.dtSchedule.Value;
        universalTime2 = universalTime2.ToUniversalTime();
        str1 = universalTime2.ToString(this.timeFormat);
        break;
      case TimedEventKinds.Weekly:
        DateTime universalTime3 = this.dtSchedule.Value;
        universalTime3 = universalTime3.ToUniversalTime();
        str1 = universalTime3.ToString(this.timeFormat);
        for (int index = 0; index < this.clbWeek.Items.Count; ++index)
        {
          if (this.clbWeek.GetItemChecked(index))
            str1 = $"{str1},{(object) index}";
        }
        break;
      case TimedEventKinds.Monthly:
        DateTime universalTime4 = this.dtSchedule.Value;
        universalTime4 = universalTime4.ToUniversalTime();
        string str2 = universalTime4.ToString(this.timeFormat) + ",";
        string[] strArray1 = (this.tbDate.Text.Replace(" ", "") + ",").Split(new char[1]
        {
          ','
        }, StringSplitOptions.RemoveEmptyEntries);
        List<string> stringList = new List<string>(31 /*0x1F*/);
        foreach (string s in strArray1)
        {
          int result1 = 0;
          if (int.TryParse(s, out result1))
          {
            if (result1 > 31 /*0x1F*/ || result1 < 1)
            {
              stringList.Clear();
              break;
            }
            if (!stringList.Contains(s))
              stringList.Add(s);
          }
          else
          {
            string[] strArray2 = s.Split('-');
            if (strArray2.Length == 2)
            {
              int result2 = 0;
              int result3 = 0;
              if (int.TryParse(strArray2[0], out result2) && int.TryParse(strArray2[1], out result3))
              {
                for (; result2 <= result3; ++result2)
                {
                  if (!stringList.Contains(result2.ToString()))
                    stringList.Add(result2.ToString());
                }
              }
            }
            else
            {
              stringList.Clear();
              break;
            }
          }
        }
        if (stringList.Count == 0)
        {
          int num = (int) IMMessageBox.Show("Внимание", "Проверьте формат введённой строки", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Warning);
          str1 = string.Empty;
          break;
        }
        str1 = str2 + string.Join(",", stringList.ToArray());
        break;
      case TimedEventKinds.Yearly:
        str1 = this.dtSchedule.Value.ToUniversalTime().ToString("HH,mm,dd,MM");
        break;
      case TimedEventKinds.Minutely:
        str1 = this.numHours.Value.ToString();
        break;
    }
    return str1;
  }

  private void tbEventName_TextChanged(object sender, EventArgs e)
  {
    this.btnOK.Enabled = this.tbEventName.Text.Length > 0;
    if (!(this.cbTask.SelectedItem is MyElement selectedItem) || !(selectedItem.Caption != this.tbEventName.Text))
      return;
    this.autoChangeTaskName = false;
  }

  private void cbTaskType_SelectedIndexChanged(object sender, EventArgs e)
  {
    MyElement selectedItem = this.cbTaskType.SelectedItem as MyElement;
    this.cbImmediatly.Enabled = (TimedEventKinds) selectedItem.Value != 0;
    this.lbDays.Visible = this.clbWeek.Visible = (TimedEventKinds) selectedItem.Value == TimedEventKinds.Weekly;
    this.numHours.Visible = (TimedEventKinds) selectedItem.Value == TimedEventKinds.Hourly || (TimedEventKinds) selectedItem.Value == TimedEventKinds.Minutely;
    this.dtSchedule.Visible = !this.numHours.Visible;
    this.lbDate.Visible = this.tbDate.Visible = (TimedEventKinds) selectedItem.Value == TimedEventKinds.Monthly;
    this.PrepareDateTimeControl((TimedEventKinds) selectedItem.Value, DateTime.Now, string.Empty);
  }

  private void cbTask_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.autoChangeTaskName || !(this.cbTask.SelectedItem is MyElement selectedItem))
      return;
    this.tbEventName.Text = selectedItem.Caption;
  }

  private void worker_DoWork(object sender, DoWorkEventArgs e)
  {
    foreach (DirectoryEntry child1 in new DirectoryEntry("WinNT:").Children)
    {
      if (this.worker.CancellationPending)
      {
        e.Cancel = true;
        break;
      }
      child1.Children.SchemaFilter.Add("computer");
      foreach (DirectoryEntry child2 in child1.Children)
      {
        if (this.worker.CancellationPending)
        {
          e.Cancel = true;
          break;
        }
        if (child2.Name != "Schema" && e.Argument.ToString() != child2.Name)
          this.worker.ReportProgress(0, (object) child2.Name);
      }
    }
  }

  private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    string str = e.UserState.ToString();
    if (this.cbServerName.Items.Contains((object) str))
      return;
    this.cbServerName.Items.Add((object) str);
  }

  private void numHours_ValueChanged(object sender, EventArgs e)
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TimedEventPropertiesForm));
    this.label1 = new Label();
    this.label2 = new Label();
    this.label3 = new Label();
    this.label4 = new Label();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.cbTask = new ComboBox();
    this.tbEventName = new TextBox();
    this.cbImmediatly = new CheckBox();
    this.label5 = new Label();
    this.cbTaskType = new ComboBox();
    this.dtSchedule = new DateTimePicker();
    this.lbDays = new Label();
    this.clbWeek = new CheckedListBox();
    this.numHours = new NumericUpDown();
    this.lbDate = new Label();
    this.tbDate = new TextBox();
    this.cbServerName = new ComboBox();
    this.lblStep = new Label();
    this.numHours.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbTask, "cbTask");
    this.cbTask.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbTask.FormattingEnabled = true;
    this.cbTask.Name = "cbTask";
    this.cbTask.SelectedIndexChanged += new EventHandler(this.cbTask_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.tbEventName, "tbEventName");
    this.tbEventName.Name = "tbEventName";
    this.tbEventName.TextChanged += new EventHandler(this.tbEventName_TextChanged);
    componentResourceManager.ApplyResources((object) this.cbImmediatly, "cbImmediatly");
    this.cbImmediatly.Name = "cbImmediatly";
    this.cbImmediatly.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    this.cbTaskType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbTaskType.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.cbTaskType, "cbTaskType");
    this.cbTaskType.Name = "cbTaskType";
    this.cbTaskType.SelectedIndexChanged += new EventHandler(this.cbTaskType_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.dtSchedule, "dtSchedule");
    this.dtSchedule.Format = DateTimePickerFormat.Custom;
    this.dtSchedule.Name = "dtSchedule";
    this.dtSchedule.Value = new DateTime(2012, 6, 6, 0, 0, 0, 0);
    componentResourceManager.ApplyResources((object) this.lbDays, "lbDays");
    this.lbDays.Name = "lbDays";
    componentResourceManager.ApplyResources((object) this.clbWeek, "clbWeek");
    this.clbWeek.CheckOnClick = true;
    this.clbWeek.FormattingEnabled = true;
    this.clbWeek.Name = "clbWeek";
    componentResourceManager.ApplyResources((object) this.numHours, "numHours");
    this.numHours.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numHours.Name = "numHours";
    this.numHours.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numHours.ValueChanged += new EventHandler(this.numHours_ValueChanged);
    componentResourceManager.ApplyResources((object) this.lbDate, "lbDate");
    this.lbDate.Name = "lbDate";
    componentResourceManager.ApplyResources((object) this.tbDate, "tbDate");
    this.tbDate.Name = "tbDate";
    componentResourceManager.ApplyResources((object) this.cbServerName, "cbServerName");
    this.cbServerName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.cbServerName.AutoCompleteSource = AutoCompleteSource.ListItems;
    this.cbServerName.FormattingEnabled = true;
    this.cbServerName.Name = "cbServerName";
    componentResourceManager.ApplyResources((object) this.lblStep, "lblStep");
    this.lblStep.Name = "lblStep";
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.lblStep);
    this.Controls.Add((Control) this.cbServerName);
    this.Controls.Add((Control) this.tbDate);
    this.Controls.Add((Control) this.lbDate);
    this.Controls.Add((Control) this.numHours);
    this.Controls.Add((Control) this.clbWeek);
    this.Controls.Add((Control) this.lbDays);
    this.Controls.Add((Control) this.dtSchedule);
    this.Controls.Add((Control) this.cbTaskType);
    this.Controls.Add((Control) this.label5);
    this.Controls.Add((Control) this.cbImmediatly);
    this.Controls.Add((Control) this.tbEventName);
    this.Controls.Add((Control) this.cbTask);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (TimedEventPropertiesForm);
    this.ShowInTaskbar = false;
    this.numHours.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
