
// Type: Intermech.Navigator.EventLog.StatisticsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace Intermech.Navigator.EventLog;

public class StatisticsView : UserControl, IView
{
  private bool _firstEnter;
  private DataTable _allEvents;
  private Dictionary<string, DataRow[]> _resDictionary = new Dictionary<string, DataRow[]>();
  private int maxItems;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Chart chart1;
  private GroupBox groupBox1;
  private Label label2;
  private ComboBox cbTimeStep;
  private DateTimePicker dtpFromDate;
  private Label label1;
  private DateTimePicker dtpFromTime;
  private DateTimePicker dtpToTime;
  private DateTimePicker dtpToDate;
  private Label label3;
  private Label label6;
  private Label label5;
  private Label label4;
  private CheckedComboBox ccbEventLogRecordType;
  private CheckedComboBox ccbActionType;
  private CheckedComboBox ccbUsers;
  private Button btnShowStatistics;
  private BackgroundWorker drawChartWorker;
  private BackgroundWorker drawUsersChartWorker;
  private GroupBox groupBox2;
  private RadioButton rbArea;
  private RadioButton rbColumn;
  private RadioButton rbLine;
  private TableLayoutPanel tableLayoutPanel1;

  public StatisticsView()
  {
    this.InitializeComponent();
    this.dtpFromDate.MaxDate = DateTime.Now;
    this.dtpToDate.MaxDate = DateTime.Now;
    this.dtpFromDate.Value = DateTime.Now.AddDays(-1.0);
    this.dtpFromTime.Value = DateTime.Now.AddDays(-1.0);
    this.dtpToDate.Value = DateTime.Now;
    this.dtpToTime.Value = DateTime.Now;
    this.cbTimeStep.Items.AddRange((object[]) new List<Timestep>()
    {
      new Timestep("минуты", "min"),
      new Timestep("часы", "h"),
      new Timestep("дни", "d"),
      new Timestep("недели", "w"),
      new Timestep("месяцы", "m")
    }.ToArray());
    this.cbTimeStep.SelectedIndex = 1;
    this.ccbUsers.Items.Add((object) new CCBoxItem("Все", -1));
    this.ccbUsers.SetItemChecked(0, true);
    this.ccbUsers.ItemCheck += new ItemCheckEventHandler(this.ccbUsers_ItemCheck);
    this.ccbActionType.Items.Add((object) new CCBoxItem("Все", -1));
    this.ccbActionType.SetItemChecked(0, true);
    this.ccbActionType.ItemCheck += new ItemCheckEventHandler(this.ccbActionType_ItemCheck);
    this.ccbEventLogRecordType.Items.Add((object) new CCBoxItem("Все", -1));
    this.ccbEventLogRecordType.SetItemChecked(0, true);
    this.ccbEventLogRecordType.ItemCheck += new ItemCheckEventHandler(this.ccbEventLogRecordType_ItemCheck);
  }

  public string Caption => LocalizationHolder.rm.GetString("Client.Core_Statistics");

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._firstEnter = true;
  }

  public void Activate(IView previousView)
  {
    if (!this._firstEnter)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.CAPTION
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.CAPTION
      }, new SortOrders[1]{ SortOrders.ASC }, 0L, (object) null, -1, true, "");
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.ObjectsSelect(new Guid("cad00002-306c-11d8-b4e9-00304f19f545"), dbRecordSetParams).Rows)
      {
        if (Convert.ToInt32(row.ItemArray[0]) != 2)
          this.ccbUsers.Items.Add((object) new CCBoxItem(row.ItemArray[1].ToString(), Convert.ToInt32(row.ItemArray[0])));
      }
      foreach (ActionType actionType in Enum.GetValues(typeof (ActionType)))
        this.ccbActionType.Items.Add((object) new CCBoxItem(ActionTypeHelper.GetCaption(actionType), (int) actionType));
      foreach (EventlogRecordType eventlogRecordType in Enum.GetValues(typeof (EventlogRecordType)))
        this.ccbEventLogRecordType.Items.Add((object) new CCBoxItem(EventlogRecordTypeHelper.GetCaption(eventlogRecordType), (int) eventlogRecordType));
      ConditionStructure[] conditions = new ConditionStructure[2]
      {
        new ConditionStructure(new Guid("cad00042-306c-11d8-b4e9-00304f19f545"), RelationalOperators.GreaterOrEqual, (object) this.dtpFromDate.Value, LogicalOperators.AND, 0),
        new ConditionStructure(new Guid("cad00042-306c-11d8-b4e9-00304f19f545"), RelationalOperators.LessOrEqual, (object) this.dtpToDate.Value, LogicalOperators.NONE, 0)
      };
      this._allEvents = sessionKeeper.Session.EventLog.Select(new DBRecordSetParams(conditions), true);
    }
    this._resDictionary.Clear();
    DateTime dateTime1;
    for (int index = 0; index < 24; ++index)
    {
      dateTime1 = this.dtpFromDate.Value;
      dateTime1 = dateTime1.AddHours((double) index);
      string str1 = dateTime1.ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo);
      dateTime1 = this.dtpFromDate.Value;
      dateTime1 = dateTime1.AddHours((double) (index + 1));
      string str2 = dateTime1.ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo);
      string columnName = this._allEvents.Columns[1].ColumnName;
      DataRow[] dataRowArray1 = this._allEvents.Select(string.Format("[{2}] <= #{1}# AND [{2}] >=#{0}#", (object) str1, (object) str2, (object) columnName));
      Dictionary<string, DataRow[]> resDictionary = this._resDictionary;
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (System.ValueType) index;
      dateTime1 = this.dtpFromDate.Value;
      dateTime1 = dateTime1.AddHours((double) index);
      string str3 = dateTime1.ToString("dd.MM HH:mm");
      string key = $"{local}_{str3}";
      DataRow[] dataRowArray2 = dataRowArray1;
      resDictionary.Add(key, dataRowArray2);
    }
    List<string> stringList = new List<string>();
    int num1 = 0;
    this.chart1.ChartAreas["caStat"].AxisX.Minimum = 0.0;
    this.chart1.ChartAreas["caStat"].AxisY.Minimum = 0.0;
    this.chart1.Series["allUsers"].Points.AddXY((double) num1, 0.0);
    dateTime1 = this.dtpFromDate.Value;
    DateTime date1 = dateTime1.Date;
    dateTime1 = this.dtpFromTime.Value;
    TimeSpan timeOfDay1 = dateTime1.TimeOfDay;
    dateTime1 = new DateTime(date1.Year, date1.Month, date1.Day, timeOfDay1.Hours, timeOfDay1.Minutes, timeOfDay1.Seconds);
    DateTime dateTime2 = dateTime1.AddMinutes(-1.0);
    this.chart1.Series["allUsers"].Points[num1].AxisLabel = dateTime2.ToString("dd.MM HH:mm");
    this.chart1.Series["allUsers"].Points[num1].IsValueShownAsLabel = false;
    int num2 = num1 + 1;
    foreach (KeyValuePair<string, DataRow[]> res in this._resDictionary)
    {
      stringList.Clear();
      int yValue = 0;
      string[] strArray = res.Key.Split(new string[1]{ "_" }, StringSplitOptions.RemoveEmptyEntries);
      foreach (DataRow dataRow in res.Value)
      {
        string str = dataRow.ItemArray[5].ToString();
        if (string.IsNullOrEmpty(str) && dataRow.ItemArray[3].Equals((object) ActionTypeHelper.GetCaption(ActionType.Login)) && !dataRow.ItemArray[0].Equals((object) EventlogRecordTypeHelper.GetCaption(EventlogRecordType.AccessDenied)))
          str = dataRow.ItemArray[4].ToString();
        if (!string.IsNullOrEmpty(str) && !stringList.Contains(str))
        {
          stringList.Add(str);
          ++yValue;
        }
      }
      this.chart1.Series["allUsers"].Points.AddXY((double) num2, (double) yValue);
      this.chart1.Series["allUsers"].Points[num2].AxisLabel = strArray[1];
      this.chart1.Series["allUsers"].Points[num2].IsValueShownAsLabel = true;
      ++num2;
    }
    this.chart1.Series["allUsers"].Points.AddXY((double) num2, 0.0);
    DateTime date2 = this.dtpToDate.Value.Date;
    TimeSpan timeOfDay2 = this.dtpToTime.Value.TimeOfDay;
    DateTime dateTime3 = new DateTime(date2.Year, date2.Month, date2.Day, timeOfDay2.Hours, timeOfDay2.Minutes, timeOfDay2.Seconds).AddMinutes(1.0);
    this.chart1.Series["allUsers"].Points[num2].AxisLabel = dateTime3.ToString("dd.MM HH:mm");
    this.chart1.Series["allUsers"].Points[num2].IsValueShownAsLabel = false;
    this.chart1.ChartAreas["caStat"].AxisX.ScaleView.Zoom(0.0, (double) (this._resDictionary.Count + 1));
    this._firstEnter = false;
  }

  public void Deactivate(IView nextView)
  {
  }

  public int ImageIndex => -1;

  public int OrderID => 20;

  private void ccbUsers_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (!(sender is CheckedListBox checkedListBox) || !(checkedListBox.SelectedItem is CCBoxItem selectedItem))
      return;
    this.ccbUsers.ItemCheck -= new ItemCheckEventHandler(this.ccbUsers_ItemCheck);
    this.CkeckedItems(e, selectedItem, this.ccbUsers);
    this.ccbUsers.ItemCheck += new ItemCheckEventHandler(this.ccbUsers_ItemCheck);
  }

  private void ccbEventLogRecordType_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (!(sender is CheckedListBox checkedListBox) || !(checkedListBox.SelectedItem is CCBoxItem selectedItem))
      return;
    this.ccbEventLogRecordType.ItemCheck -= new ItemCheckEventHandler(this.ccbEventLogRecordType_ItemCheck);
    this.CkeckedItems(e, selectedItem, this.ccbEventLogRecordType);
    this.ccbEventLogRecordType.ItemCheck += new ItemCheckEventHandler(this.ccbEventLogRecordType_ItemCheck);
  }

  private void ccbActionType_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (!(sender is CheckedListBox checkedListBox) || !(checkedListBox.SelectedItem is CCBoxItem selectedItem))
      return;
    this.ccbActionType.ItemCheck -= new ItemCheckEventHandler(this.ccbActionType_ItemCheck);
    this.CkeckedItems(e, selectedItem, this.ccbActionType);
    this.ccbActionType.ItemCheck += new ItemCheckEventHandler(this.ccbActionType_ItemCheck);
  }

  /// <summary>
  /// Если отмечаем элемент все, то нужно снять отметки со всех остальных элементов, либо если отмечаем какой-либо нужно снять отметку с "все"
  /// </summary>
  /// <param name="e">аргументы по текущему и следующему состоянию отметки</param>
  /// <param name="selected">изменяемый элемент</param>
  /// <param name="checkedBox">контрл в котором происходит изменение</param>
  private void CkeckedItems(ItemCheckEventArgs e, CCBoxItem selected, CheckedComboBox checkedBox)
  {
    if (selected.Value == -1 && e.NewValue == CheckState.Checked)
    {
      for (int index = 1; index < checkedBox.Items.Count; ++index)
        checkedBox.SetItemChecked(index, false);
    }
    else if (e.NewValue == CheckState.Unchecked && checkedBox.CheckedItems.Count == 1)
    {
      for (int index = 0; index < checkedBox.Items.Count; ++index)
      {
        if (((CCBoxItem) checkedBox.Items[index]).Value == -1)
          checkedBox.SetItemChecked(index, true);
      }
    }
    else
    {
      for (int index = 0; index < checkedBox.Items.Count; ++index)
      {
        if (((CCBoxItem) checkedBox.Items[index]).Value == -1)
          checkedBox.SetItemChecked(index, false);
      }
    }
  }

  private void btnShowStatistics_Click(object sender, EventArgs e)
  {
    DateTime date1 = this.dtpFromDate.Value.Date;
    TimeSpan timeOfDay1 = this.dtpFromTime.Value.TimeOfDay;
    DateTime conditionValue1 = new DateTime(date1.Year, date1.Month, date1.Day, timeOfDay1.Hours, timeOfDay1.Minutes, timeOfDay1.Seconds);
    DateTime date2 = this.dtpToDate.Value.Date;
    TimeSpan timeOfDay2 = this.dtpToTime.Value.TimeOfDay;
    DateTime conditionValue2 = new DateTime(date2.Year, date2.Month, date2.Day, timeOfDay2.Hours, timeOfDay2.Minutes, timeOfDay2.Seconds);
    if (conditionValue1 > conditionValue2)
      throw new KernelException("Дата начала отсчётного периода не может быть больше даты окончания");
    this.btnShowStatistics.Enabled = false;
    List<ConditionStructure> collection1 = new List<ConditionStructure>();
    List<ConditionStructure> collection2 = new List<ConditionStructure>();
    List<ConditionStructure> collection3 = new List<ConditionStructure>();
    if (((CCBoxItem) this.ccbActionType.CheckedItems[0]).Value != -1)
    {
      object[] conditionValue3 = new object[this.ccbActionType.CheckedItems.Count];
      for (int index = 0; index < this.ccbActionType.CheckedItems.Count; ++index)
        conditionValue3[index] = (object) ((CCBoxItem) this.ccbActionType.CheckedItems[index]).Value;
      collection1.Add(new ConditionStructure(new Guid("cad00041-306c-11d8-b4e9-00304f19f545"), RelationalOperators.In, (object) conditionValue3, LogicalOperators.AND, 0));
    }
    if (((CCBoxItem) this.ccbEventLogRecordType.CheckedItems[0]).Value != -1)
    {
      object[] conditionValue4 = new object[this.ccbEventLogRecordType.CheckedItems.Count];
      for (int index = 0; index < this.ccbEventLogRecordType.CheckedItems.Count; ++index)
        conditionValue4[index] = (object) ((CCBoxItem) this.ccbEventLogRecordType.CheckedItems[index]).Value;
      collection2.Add(new ConditionStructure(new Guid("cad00044-306c-11d8-b4e9-00304f19f545"), RelationalOperators.In, (object) conditionValue4, LogicalOperators.AND, 0));
    }
    if (collection1.Count > 0)
      collection3.AddRange((IEnumerable<ConditionStructure>) collection1);
    if (collection2.Count > 0)
      collection3.AddRange((IEnumerable<ConditionStructure>) collection2);
    ConditionStructure[] source1 = new ConditionStructure[2]
    {
      new ConditionStructure(new Guid("cad00042-306c-11d8-b4e9-00304f19f545"), RelationalOperators.GreaterOrEqual, (object) conditionValue1, LogicalOperators.AND, 0),
      new ConditionStructure(new Guid("cad00042-306c-11d8-b4e9-00304f19f545"), RelationalOperators.LessOrEqual, (object) conditionValue2, LogicalOperators.NONE, 0)
    };
    try
    {
      if (((CCBoxItem) this.ccbUsers.CheckedItems[0]).Value == -1)
      {
        collection3.AddRange((IEnumerable<ConditionStructure>) ((IEnumerable<ConditionStructure>) source1).ToList<ConditionStructure>());
        DataTable dataTable;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          dataTable = sessionKeeper.Session.EventLog.Select(new DBRecordSetParams(collection3.ToArray()), true);
        if (dataTable.Rows.Count == 0)
          throw new KernelException("Статистика по выбранным действиям и событиям для всех пользователей отсутствует");
        this.chart1.Series["allUsers"].Enabled = true;
        this.chart1.Series["allUsers"].Points.Clear();
        this.chart1.Legends.Clear();
        IEnumerable<Series> source2 = this.chart1.Series.Where<Series>((System.Func<Series, bool>) (x => x.Name != "allUsers"));
        if (!(source2 is Series[] seriesArray))
          seriesArray = source2.ToArray<Series>();
        Series[] source3 = seriesArray;
        for (int index = 0; index < ((IEnumerable<Series>) source3).Count<Series>(); ++index)
          this.chart1.Series.Remove(source3[index]);
        this.chart1.ChartAreas["caStat"].AxisY.Title = "Количество пользователей";
        this.chart1.ChartAreas["caStat"].AxisX.Minimum = 0.0;
        this.chart1.ChartAreas["caStat"].AxisY.Minimum = 0.0;
        object[] objArray = new object[6]
        {
          (object) dataTable,
          (object) ((Timestep) this.cbTimeStep.SelectedItem).Value,
          (object) this.dtpFromDate.Value,
          (object) this.dtpFromTime.Value,
          (object) this.dtpToDate.Value,
          (object) this.dtpToTime.Value
        };
        if (this.drawChartWorker.IsBusy)
          return;
        this.drawChartWorker.RunWorkerAsync((object) objArray);
      }
      else
      {
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
        Dictionary<string, DataTable> source4 = new Dictionary<string, DataTable>();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IEventLog eventLog = sessionKeeper.Session.EventLog;
          for (int index = 0; index < this.ccbUsers.CheckedItems.Count; ++index)
          {
            int conditionValue5 = ((CCBoxItem) this.ccbUsers.CheckedItems[index]).Value;
            conditionStructureList.Clear();
            conditionStructureList.Add(new ConditionStructure(new Guid("cad0003e-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) conditionValue5, LogicalOperators.AND, 0));
            collection3.AddRange((IEnumerable<ConditionStructure>) conditionStructureList);
            collection3.AddRange((IEnumerable<ConditionStructure>) ((IEnumerable<ConditionStructure>) source1).ToList<ConditionStructure>());
            DataTable dataTable = eventLog.Select(new DBRecordSetParams(collection3.ToArray()), true);
            source4.Add(((CCBoxItem) this.ccbUsers.CheckedItems[index]).Name, dataTable);
            collection3.RemoveRange<ConditionStructure>((IEnumerable<ConditionStructure>) ((IEnumerable<ConditionStructure>) source1).ToList<ConditionStructure>());
            collection3.RemoveRange<ConditionStructure>((IEnumerable<ConditionStructure>) conditionStructureList);
          }
        }
        if (source4.Count == 1)
        {
          if (source4.ElementAt<KeyValuePair<string, DataTable>>(0).Value.Rows.Count == 0)
            throw new KernelException("Статистика для выбранного пользователя отсутствует");
        }
        else if (source4.Count<KeyValuePair<string, DataTable>>((System.Func<KeyValuePair<string, DataTable>, bool>) (events => events.Value.Rows.Count == 0)) == source4.Count)
          throw new KernelException("Статистика для выбранных пользователей отсутствует");
        this.chart1.Series["allUsers"].Enabled = false;
        this.chart1.Legends.Clear();
        IEnumerable<Series> source5 = this.chart1.Series.Where<Series>((System.Func<Series, bool>) (x => x.Name != "allUsers"));
        if (!(source5 is Series[] seriesArray))
          seriesArray = source5.ToArray<Series>();
        Series[] source6 = seriesArray;
        for (int index = 0; index < ((IEnumerable<Series>) source6).Count<Series>(); ++index)
          this.chart1.Series.Remove(source6[index]);
        this.chart1.ChartAreas["caStat"].AxisY.Title = "Количество записей в журнале";
        this.chart1.ChartAreas["caStat"].AxisX.Minimum = 0.0;
        this.chart1.ChartAreas["caStat"].AxisY.Minimum = 0.0;
        object[] objArray = new object[6]
        {
          (object) source4,
          (object) ((Timestep) this.cbTimeStep.SelectedItem).Value,
          (object) this.dtpFromDate.Value,
          (object) this.dtpFromTime.Value,
          (object) this.dtpToDate.Value,
          (object) this.dtpToTime.Value
        };
        if (this.drawUsersChartWorker.IsBusy)
          return;
        this.drawUsersChartWorker.RunWorkerAsync((object) objArray);
      }
    }
    catch (KernelException ex)
    {
      this.btnShowStatistics.Enabled = true;
      int num = (int) MessageBox.Show(ex.Message, "Внимание");
    }
  }

  private bool DrawChartsForMin(
    int minutes,
    DateTime resultFrom,
    DataTable events,
    Dictionary<string, DataRow[]> resultDictionary,
    bool allUsers = true)
  {
    if (minutes > 300)
    {
      if (allUsers)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_Statistics.MinAsk"));
      }
      return this.DrawChartsForHour(minutes / 60, resultFrom, events, resultDictionary);
    }
    if (allUsers)
    {
      if (!this.InvokeRequired)
        return false;
      this.BeginInvoke((Delegate) new Action<string>(this.ClearCharts), (object) nameof (allUsers));
    }
    for (int index = 0; index < minutes; ++index)
    {
      DateTime dateTime = resultFrom.AddMinutes((double) index);
      string filterExpression = string.Format("[{2}] <= #{1}# AND [{2}] >=#{0}#", (object) dateTime.ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo), (object) dateTime.AddMinutes(1.0).ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo), (object) events.Columns[1].ColumnName);
      DataRow[] dataRowArray = events.Select(filterExpression);
      resultDictionary.Add($"{index}_{dateTime.ToString("HH:mm")}", dataRowArray);
    }
    return true;
  }

  /// <summary>Рисуем график по полученным данным с шагом в 1 час</summary>
  private bool DrawChartsForHour(
    int hourCount,
    DateTime resultFrom,
    DataTable events,
    Dictionary<string, DataRow[]> resultDictionary,
    bool allUsers = true)
  {
    if (hourCount > 168)
    {
      if (allUsers)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_Statistics.HourAsk"));
      }
      return this.DrawChartsForDay(hourCount / 24, resultFrom, events, resultDictionary);
    }
    if (allUsers)
    {
      if (!this.InvokeRequired)
        return false;
      this.BeginInvoke((Delegate) new Action<string>(this.ClearCharts), (object) nameof (allUsers));
    }
    for (int index = 0; index < hourCount; ++index)
    {
      DateTime dateTime = resultFrom.AddHours((double) index);
      string filterExpression = string.Format("[{2}] <= #{1}# AND [{2}] >=#{0}#", (object) dateTime.ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo), (object) dateTime.AddHours(1.0).ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo), (object) events.Columns[1].ColumnName);
      DataRow[] dataRowArray = events.Select(filterExpression);
      resultDictionary.Add($"{index}_{dateTime.ToString("dd.MM HH:mm")}", dataRowArray);
    }
    return true;
  }

  private bool DrawChartsForDay(
    int dayCount,
    DateTime resultFrom,
    DataTable events,
    Dictionary<string, DataRow[]> resultDictionary,
    bool allUsers = true)
  {
    if (dayCount > 183)
    {
      if (allUsers)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_Statistics.DayAsk"));
      }
      return this.DrawChartsForWeek(dayCount, resultFrom, events, resultDictionary);
    }
    if (allUsers)
    {
      if (!this.InvokeRequired)
        return false;
      this.BeginInvoke((Delegate) new Action<string>(this.ClearCharts), (object) nameof (allUsers));
    }
    for (int index = 0; index < dayCount; ++index)
    {
      DateTime dateTime = resultFrom.AddDays((double) index);
      string filterExpression = string.Format("[{2}] <= #{1}# AND [{2}] >=#{0}#", (object) dateTime.ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo), (object) dateTime.AddDays(1.0).ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo), (object) events.Columns[1].ColumnName);
      DataRow[] dataRowArray = events.Select(filterExpression);
      resultDictionary.Add($"{index}_{dateTime.ToString("dd.MM.yyyy")}", dataRowArray);
    }
    return true;
  }

  private bool DrawChartsForWeek(
    int dayCount,
    DateTime resultFrom,
    DataTable events,
    Dictionary<string, DataRow[]> resultDictionary,
    bool allUsers = true)
  {
    if (dayCount / 7 > 200)
    {
      if (allUsers)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_Statistics.WeekAsk"));
      }
      return this.DrawChartsForMonth(dayCount, resultFrom, events, resultDictionary);
    }
    if (allUsers)
    {
      if (!this.InvokeRequired)
        return false;
      this.BeginInvoke((Delegate) new Action<string>(this.ClearCharts), (object) nameof (allUsers));
    }
    for (int index = 0; index < dayCount; index += 7)
    {
      DateTime dateTime = resultFrom.AddDays((double) index);
      string filterExpression = string.Format("[{2}] <= #{1}# AND [{2}] >=#{0}#", (object) dateTime.ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo), (object) dateTime.AddDays(7.0).ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo), (object) events.Columns[1].ColumnName);
      DataRow[] dataRowArray = events.Select(filterExpression);
      resultDictionary.Add($"{index}_{dateTime.ToString("dd.MM.yyyy")}", dataRowArray);
    }
    return true;
  }

  private bool DrawChartsForMonth(
    int dayCount,
    DateTime resultFrom,
    DataTable events,
    Dictionary<string, DataRow[]> resultDictionary,
    bool allUsers = true)
  {
    int num = dayCount / 30;
    if (allUsers)
    {
      if (!this.InvokeRequired)
        return false;
      this.BeginInvoke((Delegate) new Action<string>(this.ClearCharts), (object) nameof (allUsers));
    }
    for (int months = 0; months < num; ++months)
    {
      DateTime dateTime = resultFrom.AddMonths(months);
      string filterExpression = string.Format("[{2}] <= #{1}# AND [{2}] >=#{0}#", (object) dateTime.ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo), (object) dateTime.AddMonths(1).ToString((IFormatProvider) DateTimeFormatInfo.InvariantInfo), (object) events.Columns[1].ColumnName);
      DataRow[] dataRowArray = events.Select(filterExpression);
      resultDictionary.Add($"{months}_{dateTime.ToString("MM.yyyy")}", dataRowArray);
    }
    return true;
  }

  public void ClearCharts(string seriesName) => this.chart1.Series[seriesName].Points.Clear();

  private void drawChartWorker_DoWork(object sender, DoWorkEventArgs e)
  {
    this.maxItems = 0;
    BackgroundWorker backgroundWorker = (BackgroundWorker) sender;
    object[] objArray = (object[]) e.Argument;
    DataTable events = (DataTable) objArray[0];
    string str1 = (string) objArray[1];
    DateTime dateTime1 = (DateTime) objArray[2];
    DateTime dateTime2 = (DateTime) objArray[3];
    DateTime dateTime3 = (DateTime) objArray[4];
    DateTime dateTime4 = (DateTime) objArray[5];
    Dictionary<string, DataRow[]> resultDictionary = new Dictionary<string, DataRow[]>();
    DateTime date1 = dateTime1.Date;
    TimeSpan timeOfDay1 = dateTime2.TimeOfDay;
    DateTime resultFrom = new DateTime(date1.Year, date1.Month, date1.Day, timeOfDay1.Hours, timeOfDay1.Minutes, timeOfDay1.Seconds);
    DateTime date2 = dateTime3.Date;
    TimeSpan timeOfDay2 = dateTime4.TimeOfDay;
    TimeSpan timeSpan = new DateTime(date2.Year, date2.Month, date2.Day, timeOfDay2.Hours, timeOfDay2.Minutes, timeOfDay2.Seconds) - resultFrom;
    int hourCount = timeSpan.Days * 24 + timeSpan.Hours;
    switch (str1)
    {
      case "min":
        if (!this.DrawChartsForMin(hourCount * 60 + timeSpan.Minutes, resultFrom, events, resultDictionary))
          return;
        break;
      case "h":
        if (!this.DrawChartsForHour(hourCount, resultFrom, events, resultDictionary))
          return;
        break;
      case "d":
        if (timeSpan.Hours != 0)
        {
          if (!this.DrawChartsForDay(timeSpan.Days + 1, resultFrom, events, resultDictionary))
            return;
          break;
        }
        if (!this.DrawChartsForDay(timeSpan.Days, resultFrom, events, resultDictionary))
          return;
        break;
      case "w":
        if (!this.DrawChartsForWeek(timeSpan.Days, resultFrom, events, resultDictionary))
          return;
        break;
      case "m":
        if (!this.DrawChartsForMonth(timeSpan.Days, resultFrom, events, resultDictionary))
          return;
        break;
      default:
        if (!this.DrawChartsForHour(hourCount, resultFrom, events, resultDictionary))
          return;
        break;
    }
    this.maxItems = resultDictionary.Count;
    List<string> stringList = new List<string>();
    int percentProgress1 = 0;
    string userState1 = $"{0}|0,001|{string.Empty}";
    backgroundWorker.ReportProgress(percentProgress1, (object) userState1);
    int percentProgress2 = percentProgress1 + 1;
    foreach (KeyValuePair<string, DataRow[]> keyValuePair in resultDictionary)
    {
      stringList.Clear();
      int num = 0;
      string[] strArray = keyValuePair.Key.Split(new string[1]
      {
        "_"
      }, StringSplitOptions.RemoveEmptyEntries);
      foreach (DataRow dataRow in keyValuePair.Value)
      {
        string str2 = dataRow.ItemArray[5].ToString();
        if (string.IsNullOrEmpty(str2) && dataRow.ItemArray[3].Equals((object) ActionTypeHelper.GetCaption(ActionType.Login)) && !dataRow.ItemArray[0].Equals((object) EventlogRecordTypeHelper.GetCaption(EventlogRecordType.AccessDenied)))
          str2 = dataRow.ItemArray[4].ToString();
        if (!string.IsNullOrEmpty(str2) && !stringList.Contains(str2))
        {
          stringList.Add(str2);
          ++num;
        }
      }
      string userState2 = $"{percentProgress2}|{num}|{strArray[1]}";
      backgroundWorker.ReportProgress(percentProgress2, (object) userState2);
      ++percentProgress2;
    }
    string userState3 = $"{percentProgress2}|0,01|{string.Empty}";
    backgroundWorker.ReportProgress(percentProgress2, (object) userState3);
  }

  private void drawChartWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    RadioButton radioButton = this.groupBox2.Controls.OfType<RadioButton>().FirstOrDefault<RadioButton>((System.Func<RadioButton, bool>) (r => r.Checked));
    if (radioButton == null)
    {
      this.EditSeriesChartType(SeriesChartType.Column);
    }
    else
    {
      switch (radioButton.Name)
      {
        case "rbLine":
          this.EditSeriesChartType(SeriesChartType.Line);
          break;
        case "rbColumn":
          this.EditSeriesChartType(SeriesChartType.Column);
          break;
        case "rbArea":
          this.EditSeriesChartType(SeriesChartType.Area);
          break;
      }
      this.chart1.ChartAreas["caStat"].Position.Width = this.chart1.Legends.Count > 0 ? 80f : 100f;
      this.chart1.ChartAreas["caStat"].AxisX.Minimum = 0.0;
      this.chart1.ChartAreas["caStat"].AxisY.Minimum = 0.0;
      this.chart1.ChartAreas["caStat"].AxisX.ScaleView.Zoom(0.0, (double) (this.maxItems + 1));
      this.btnShowStatistics.Enabled = true;
    }
  }

  private void drawChartWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    this.chart1.Legends.Clear();
    string[] strArray = ((string) e.UserState).Split(new string[1]
    {
      "|"
    }, StringSplitOptions.RemoveEmptyEntries);
    if (Convert.ToDouble(strArray[1]).Equals(0.001))
    {
      this.chart1.Series["allUsers"].Points.AddXY(0.0, 0.0);
      this.chart1.Series["allUsers"].Points[this.chart1.Series["allUsers"].Points.Count - 1].IsValueShownAsLabel = false;
      DateTime date = this.dtpFromDate.Value.Date;
      TimeSpan timeOfDay = this.dtpFromTime.Value.TimeOfDay;
      this.chart1.Series["allUsers"].Points[this.chart1.Series["allUsers"].Points.Count - 1].AxisLabel = new DateTime(date.Year, date.Month, date.Day, timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds).AddMinutes(-1.0).ToString("dd.MM.yyyy HH:mm");
    }
    else if (Convert.ToDouble(strArray[1]).Equals(0.01))
    {
      this.chart1.Series["allUsers"].Points.AddXY(Convert.ToDouble(strArray[0]), 0.0);
      this.chart1.Series["allUsers"].Points[this.chart1.Series["allUsers"].Points.Count - 1].IsValueShownAsLabel = false;
      DateTime dateTime = this.dtpToDate.Value;
      DateTime date = dateTime.Date;
      dateTime = this.dtpToTime.Value;
      TimeSpan timeOfDay = dateTime.TimeOfDay;
      this.chart1.Series["allUsers"].Points[this.chart1.Series["allUsers"].Points.Count - 1].AxisLabel = new DateTime(date.Year, date.Month, date.Day, timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds).AddMinutes(1.0).ToString("dd.MM.yyyy HH:mm");
    }
    else
    {
      this.chart1.Series["allUsers"].Points.AddXY(Convert.ToDouble(strArray[0]), Convert.ToDouble(strArray[1]));
      this.chart1.Series["allUsers"].Points[Convert.ToInt32(strArray[0])].AxisLabel = strArray[2];
      this.chart1.Series["allUsers"].Points[Convert.ToInt32(strArray[0])].IsValueShownAsLabel = true;
    }
  }

  private void drawUsersChartWorker_DoWork(object sender, DoWorkEventArgs e)
  {
    this.maxItems = 0;
    BackgroundWorker sendingWorker = (BackgroundWorker) sender;
    object[] objArray = (object[]) e.Argument;
    Dictionary<string, DataTable> dictionary = (Dictionary<string, DataTable>) objArray[0];
    string str = (string) objArray[1];
    DateTime dateTime1 = (DateTime) objArray[2];
    DateTime dateTime2 = (DateTime) objArray[3];
    DateTime dateTime3 = (DateTime) objArray[4];
    DateTime dateTime4 = (DateTime) objArray[5];
    Dictionary<string, DataRow[]> resultDictionary = new Dictionary<string, DataRow[]>();
    DateTime date1 = dateTime1.Date;
    TimeSpan timeOfDay1 = dateTime2.TimeOfDay;
    DateTime resultFrom = new DateTime(date1.Year, date1.Month, date1.Day, timeOfDay1.Hours, timeOfDay1.Minutes, timeOfDay1.Seconds);
    DateTime date2 = dateTime3.Date;
    TimeSpan timeOfDay2 = dateTime4.TimeOfDay;
    TimeSpan timeSpan = new DateTime(date2.Year, date2.Month, date2.Day, timeOfDay2.Hours, timeOfDay2.Minutes, timeOfDay2.Seconds) - resultFrom;
    int hourCount = timeSpan.Days * 24 + timeSpan.Hours;
    switch (str)
    {
      case "min":
        if (hourCount * 60 + timeSpan.Minutes > 300)
        {
          int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_Statistics.MinAsk"));
        }
        using (Dictionary<string, DataTable>.Enumerator enumerator = dictionary.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, DataTable> current = enumerator.Current;
            resultDictionary.Clear();
            sendingWorker.ReportProgress(1, (object) current.Key);
            if (!this.DrawChartsForMin(hourCount * 60 + timeSpan.Minutes, resultFrom, current.Value, resultDictionary, false))
              return;
            StatisticsView.GenerateCharts(resultDictionary, current.Key, sendingWorker);
          }
          break;
        }
      case "h":
        if (hourCount > 168)
        {
          int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_Statistics.HourAsk"));
        }
        using (Dictionary<string, DataTable>.Enumerator enumerator = dictionary.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, DataTable> current = enumerator.Current;
            resultDictionary.Clear();
            sendingWorker.ReportProgress(1, (object) current.Key);
            if (!this.DrawChartsForHour(hourCount, resultFrom, current.Value, resultDictionary, false))
              return;
            StatisticsView.GenerateCharts(resultDictionary, current.Key, sendingWorker);
          }
          break;
        }
      case "d":
        if (timeSpan.Days > 183)
        {
          int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_Statistics.DayAsk"));
        }
        using (Dictionary<string, DataTable>.Enumerator enumerator = dictionary.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, DataTable> current = enumerator.Current;
            resultDictionary.Clear();
            sendingWorker.ReportProgress(1, (object) current.Key);
            if (timeSpan.Hours != 0)
            {
              if (!this.DrawChartsForDay(timeSpan.Days + 1, resultFrom, current.Value, resultDictionary, false))
                return;
              StatisticsView.GenerateCharts(resultDictionary, current.Key, sendingWorker);
            }
            else
            {
              if (!this.DrawChartsForDay(timeSpan.Days, resultFrom, current.Value, resultDictionary, false))
                return;
              StatisticsView.GenerateCharts(resultDictionary, current.Key, sendingWorker);
            }
          }
          break;
        }
      case "w":
        if (timeSpan.Days / 7 > 200)
        {
          int num4 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_Statistics.WeekAsk"));
        }
        using (Dictionary<string, DataTable>.Enumerator enumerator = dictionary.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, DataTable> current = enumerator.Current;
            resultDictionary.Clear();
            sendingWorker.ReportProgress(1, (object) current.Key);
            if (!this.DrawChartsForWeek(timeSpan.Days, resultFrom, current.Value, resultDictionary, false))
              return;
            StatisticsView.GenerateCharts(resultDictionary, current.Key, sendingWorker);
          }
          break;
        }
      case "m":
        using (Dictionary<string, DataTable>.Enumerator enumerator = dictionary.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, DataTable> current = enumerator.Current;
            resultDictionary.Clear();
            sendingWorker.ReportProgress(1, (object) current.Key);
            if (!this.DrawChartsForMonth(timeSpan.Days, resultFrom, current.Value, resultDictionary, false))
              return;
            StatisticsView.GenerateCharts(resultDictionary, current.Key, sendingWorker);
          }
          break;
        }
      default:
        if (hourCount > 168)
        {
          int num5 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_Statistics.HourAsk"));
        }
        using (Dictionary<string, DataTable>.Enumerator enumerator = dictionary.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, DataTable> current = enumerator.Current;
            resultDictionary.Clear();
            sendingWorker.ReportProgress(1, (object) current.Key);
            if (!this.DrawChartsForHour(hourCount, resultFrom, current.Value, resultDictionary, false))
              return;
            StatisticsView.GenerateCharts(resultDictionary, current.Key, sendingWorker);
          }
          break;
        }
    }
    this.maxItems = resultDictionary.Count;
    sendingWorker.ReportProgress(3);
  }

  private static void GenerateCharts(
    Dictionary<string, DataRow[]> resultDictionary,
    string userName,
    BackgroundWorker sendingWorker)
  {
    int num1 = 0;
    string userState1 = $"{0}|0,001|{string.Empty}|{userName}";
    sendingWorker.ReportProgress(2, (object) userState1);
    int num2 = num1 + 1;
    foreach (KeyValuePair<string, DataRow[]> result in resultDictionary)
    {
      string[] strArray = result.Key.Split(new string[1]
      {
        "_"
      }, StringSplitOptions.RemoveEmptyEntries);
      string userState2 = $"{num2}|{((IEnumerable<DataRow>) result.Value).Count<DataRow>()}|{strArray[1]}|{userName}";
      sendingWorker.ReportProgress(2, (object) userState2);
      ++num2;
    }
    string userState3 = $"{num2}|0,01|{string.Empty}|{userName}";
    sendingWorker.ReportProgress(2, (object) userState3);
  }

  private void drawUsersChartWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    if (e.ProgressPercentage == 1)
    {
      this.chart1.Series.Add(e.UserState.ToString());
      this.chart1.Series[e.UserState.ToString()].IsValueShownAsLabel = false;
      RadioButton radioButton = this.groupBox2.Controls.OfType<RadioButton>().FirstOrDefault<RadioButton>((System.Func<RadioButton, bool>) (r => r.Checked));
      if (radioButton == null)
      {
        this.EditSeriesChartType(SeriesChartType.Column);
        return;
      }
      switch (radioButton.Name)
      {
        case "rbLine":
          this.EditSeriesChartType(SeriesChartType.Line);
          break;
        case "rbColumn":
          this.EditSeriesChartType(SeriesChartType.Column);
          break;
        case "rbArea":
          this.EditSeriesChartType(SeriesChartType.Area);
          break;
      }
    }
    if (e.ProgressPercentage == 2)
    {
      string[] strArray = ((string) e.UserState).Split(new string[1]
      {
        "|"
      }, StringSplitOptions.None);
      if (Convert.ToDouble(strArray[1]).Equals(0.001))
      {
        this.chart1.Series[strArray[3]].Points.AddXY(0.0, 0.0);
        this.chart1.Series[strArray[3]].Points[this.chart1.Series[strArray[3]].Points.Count - 1].IsValueShownAsLabel = false;
        DateTime date = this.dtpFromDate.Value.Date;
        TimeSpan timeOfDay = this.dtpFromTime.Value.TimeOfDay;
        DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds);
        this.chart1.Series[strArray[3]].Points[this.chart1.Series[strArray[3]].Points.Count - 1].AxisLabel = dateTime.AddMinutes(-1.0).ToString("dd.MM.yyyy HH:mm");
      }
      else if (Convert.ToDouble(strArray[1]).Equals(0.01))
      {
        double xValue = Convert.ToDouble(strArray[0]);
        this.chart1.Series[strArray[3]].Points.AddXY(xValue, 0.0);
        this.chart1.Series[strArray[3]].Points[this.chart1.Series[strArray[3]].Points.Count - 1].IsValueShownAsLabel = false;
        DateTime date = this.dtpToDate.Value.Date;
        TimeSpan timeOfDay = this.dtpToTime.Value.TimeOfDay;
        DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds);
        this.chart1.Series[strArray[3]].Points[this.chart1.Series[strArray[3]].Points.Count - 1].AxisLabel = dateTime.AddMinutes(1.0).ToString("dd.MM.yyyy HH:mm");
      }
      else
      {
        double xValue = Convert.ToDouble(strArray[0]);
        double yValue = Convert.ToDouble(strArray[1]);
        this.chart1.Series[strArray[3]].Points.AddXY(xValue, yValue);
        this.chart1.Series[strArray[3]].Points[this.chart1.Series[strArray[3]].Points.Count - 1].IsValueShownAsLabel = true;
        this.chart1.Series[strArray[3]].Points[this.chart1.Series[strArray[3]].Points.Count - 1].AxisLabel = strArray[2];
      }
    }
    if (e.ProgressPercentage != 3)
      return;
    this.chart1.Legends.Add("Рассматриваемые пользователи");
    for (int index = 0; index < this.chart1.Series.Count; ++index)
      this.chart1.Series[index].Legend = "Рассматриваемые пользователи";
  }

  private void rbArea_CheckedChanged(object sender, EventArgs e)
  {
    RadioButton radioButton = sender as RadioButton;
    if (!radioButton.Checked)
      return;
    switch (radioButton.Name)
    {
      case "rbLine":
        this.EditSeriesChartType(SeriesChartType.Line);
        break;
      case "rbColumn":
        this.EditSeriesChartType(SeriesChartType.Column);
        break;
      case "rbArea":
        this.EditSeriesChartType(SeriesChartType.Area);
        break;
    }
  }

  private void EditSeriesChartType(SeriesChartType type)
  {
    foreach (Series series in (Collection<Series>) this.chart1.Series)
      series.ChartType = type;
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
    ChartArea chartArea = new ChartArea();
    Series series = new Series();
    this.chart1 = new Chart();
    this.groupBox1 = new GroupBox();
    this.groupBox2 = new GroupBox();
    this.rbArea = new RadioButton();
    this.rbColumn = new RadioButton();
    this.rbLine = new RadioButton();
    this.btnShowStatistics = new Button();
    this.label6 = new Label();
    this.label5 = new Label();
    this.label4 = new Label();
    this.dtpToTime = new DateTimePicker();
    this.dtpFromTime = new DateTimePicker();
    this.label2 = new Label();
    this.cbTimeStep = new ComboBox();
    this.dtpToDate = new DateTimePicker();
    this.dtpFromDate = new DateTimePicker();
    this.label3 = new Label();
    this.label1 = new Label();
    this.drawChartWorker = new BackgroundWorker();
    this.drawUsersChartWorker = new BackgroundWorker();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.ccbEventLogRecordType = new CheckedComboBox();
    this.ccbActionType = new CheckedComboBox();
    this.ccbUsers = new CheckedComboBox();
    this.chart1.BeginInit();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    chartArea.AlignmentOrientation = AreaAlignmentOrientations.All;
    chartArea.AxisX.Interval = 1.0;
    chartArea.AxisX.MajorGrid.Enabled = false;
    chartArea.AxisX.MaximumAutoSize = 50f;
    chartArea.AxisX.Minimum = 0.0;
    chartArea.AxisX.ScaleView.MinSize = 10.0;
    chartArea.AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Number;
    chartArea.AxisX.ScaleView.Size = 12.0;
    chartArea.AxisX.ScaleView.SizeType = DateTimeIntervalType.Number;
    chartArea.AxisX.ScrollBar.ButtonColor = SystemColors.ScrollBar;
    chartArea.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
    chartArea.AxisX.ScrollBar.LineColor = Color.White;
    chartArea.AxisX.Title = "Интервал времени";
    chartArea.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
    chartArea.AxisY.MajorGrid.Enabled = false;
    chartArea.AxisY.Minimum = 0.0;
    chartArea.AxisY.Title = "Количество пользователей";
    chartArea.CursorX.AutoScroll = false;
    chartArea.CursorX.IsUserEnabled = true;
    chartArea.CursorX.IsUserSelectionEnabled = true;
    chartArea.CursorX.LineColor = Color.Transparent;
    chartArea.CursorX.SelectionColor = Color.FromArgb(224 /*0xE0*/, 224 /*0xE0*/, 224 /*0xE0*/);
    chartArea.Name = "caStat";
    chartArea.Position.Auto = false;
    chartArea.Position.Height = 94f;
    chartArea.Position.Width = 80f;
    chartArea.Position.X = 3f;
    chartArea.Position.Y = 3f;
    this.chart1.ChartAreas.Add(chartArea);
    this.chart1.Dock = DockStyle.Fill;
    this.chart1.Location = new Point(4, 4);
    this.chart1.Margin = new Padding(4, 4, 4, 4);
    this.chart1.Name = "chart1";
    series.ChartArea = "caStat";
    series.ChartType = SeriesChartType.Line;
    series.Name = "allUsers";
    this.chart1.Series.Add(series);
    this.chart1.Size = new Size(1003, 300);
    this.chart1.TabIndex = 1;
    this.chart1.Text = "Статистика";
    this.groupBox1.Controls.Add((Control) this.groupBox2);
    this.groupBox1.Controls.Add((Control) this.btnShowStatistics);
    this.groupBox1.Controls.Add((Control) this.label6);
    this.groupBox1.Controls.Add((Control) this.label5);
    this.groupBox1.Controls.Add((Control) this.label4);
    this.groupBox1.Controls.Add((Control) this.ccbEventLogRecordType);
    this.groupBox1.Controls.Add((Control) this.ccbActionType);
    this.groupBox1.Controls.Add((Control) this.ccbUsers);
    this.groupBox1.Controls.Add((Control) this.dtpToTime);
    this.groupBox1.Controls.Add((Control) this.dtpFromTime);
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Controls.Add((Control) this.cbTimeStep);
    this.groupBox1.Controls.Add((Control) this.dtpToDate);
    this.groupBox1.Controls.Add((Control) this.dtpFromDate);
    this.groupBox1.Controls.Add((Control) this.label3);
    this.groupBox1.Controls.Add((Control) this.label1);
    this.groupBox1.Dock = DockStyle.Fill;
    this.groupBox1.Location = new Point(4, 312);
    this.groupBox1.Margin = new Padding(4, 4, 4, 4);
    this.groupBox1.MinimumSize = new Size(500, 190);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Padding = new Padding(4, 4, 4, 4);
    this.groupBox1.Size = new Size(1003, 192 /*0xC0*/);
    this.groupBox1.TabIndex = 2;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Настройки";
    this.groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.groupBox2.Controls.Add((Control) this.rbArea);
    this.groupBox2.Controls.Add((Control) this.rbColumn);
    this.groupBox2.Controls.Add((Control) this.rbLine);
    this.groupBox2.Location = new Point(872, 21);
    this.groupBox2.Margin = new Padding(4, 4, 4, 4);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Padding = new Padding(4, 4, 4, 4);
    this.groupBox2.Size = new Size(123, (int) sbyte.MaxValue);
    this.groupBox2.TabIndex = 8;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Тип графика";
    this.rbArea.AutoSize = true;
    this.rbArea.Location = new Point(8, 91);
    this.rbArea.Margin = new Padding(4, 4, 4, 4);
    this.rbArea.Name = "rbArea";
    this.rbArea.Size = new Size(62, 21);
    this.rbArea.TabIndex = 0;
    this.rbArea.Text = "Зона";
    this.rbArea.UseVisualStyleBackColor = true;
    this.rbArea.CheckedChanged += new EventHandler(this.rbArea_CheckedChanged);
    this.rbColumn.AutoSize = true;
    this.rbColumn.Location = new Point(8, 63 /*0x3F*/);
    this.rbColumn.Margin = new Padding(4, 4, 4, 4);
    this.rbColumn.Name = "rbColumn";
    this.rbColumn.Size = new Size(87, 21);
    this.rbColumn.TabIndex = 0;
    this.rbColumn.Text = "Столбцы";
    this.rbColumn.UseVisualStyleBackColor = true;
    this.rbColumn.CheckedChanged += new EventHandler(this.rbArea_CheckedChanged);
    this.rbLine.AutoSize = true;
    this.rbLine.Checked = true;
    this.rbLine.Location = new Point(8, 34);
    this.rbLine.Margin = new Padding(4, 4, 4, 4);
    this.rbLine.Name = "rbLine";
    this.rbLine.Size = new Size(71, 21);
    this.rbLine.TabIndex = 0;
    this.rbLine.TabStop = true;
    this.rbLine.Text = "Линии";
    this.rbLine.UseVisualStyleBackColor = true;
    this.rbLine.CheckedChanged += new EventHandler(this.rbArea_CheckedChanged);
    this.btnShowStatistics.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnShowStatistics.Location = new Point(880, 150);
    this.btnShowStatistics.Margin = new Padding(4, 4, 4, 4);
    this.btnShowStatistics.Name = "btnShowStatistics";
    this.btnShowStatistics.Size = new Size(111, 28);
    this.btnShowStatistics.TabIndex = 7;
    this.btnShowStatistics.Text = "Посмотреть";
    this.btnShowStatistics.UseVisualStyleBackColor = true;
    this.btnShowStatistics.Click += new EventHandler(this.btnShowStatistics_Click);
    this.label6.AutoSize = true;
    this.label6.Location = new Point(8, 156);
    this.label6.Margin = new Padding(4, 0, 4, 0);
    this.label6.Name = "label6";
    this.label6.Size = new Size(97, 17);
    this.label6.TabIndex = 6;
    this.label6.Text = "Тип события:";
    this.label5.AutoSize = true;
    this.label5.Location = new Point(8, 126);
    this.label5.Margin = new Padding(4, 0, 4, 0);
    this.label5.Name = "label5";
    this.label5.Size = new Size(102, 17);
    this.label5.TabIndex = 6;
    this.label5.Text = "Вид действия:";
    this.label4.AutoSize = true;
    this.label4.Location = new Point(8, 92);
    this.label4.Margin = new Padding(4, 0, 4, 0);
    this.label4.Name = "label4";
    this.label4.Size = new Size(106, 17);
    this.label4.TabIndex = 6;
    this.label4.Text = "Пользователи:";
    this.dtpToTime.Format = DateTimePickerFormat.Time;
    this.dtpToTime.Location = new Point(768 /*0x0300*/, 21);
    this.dtpToTime.Margin = new Padding(4, 4, 4, 4);
    this.dtpToTime.Name = "dtpToTime";
    this.dtpToTime.ShowUpDown = true;
    this.dtpToTime.Size = new Size(96 /*0x60*/, 22);
    this.dtpToTime.TabIndex = 4;
    this.dtpFromTime.Format = DateTimePickerFormat.Time;
    this.dtpFromTime.Location = new Point(339, 21);
    this.dtpFromTime.Margin = new Padding(4, 4, 4, 4);
    this.dtpFromTime.Name = "dtpFromTime";
    this.dtpFromTime.ShowUpDown = true;
    this.dtpFromTime.Size = new Size(96 /*0x60*/, 22);
    this.dtpFromTime.TabIndex = 4;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(8, 58);
    this.label2.Margin = new Padding(4, 0, 4, 0);
    this.label2.Name = "label2";
    this.label2.Size = new Size(166, 17);
    this.label2.TabIndex = 3;
    this.label2.Text = "Шаг времени на шкале: ";
    this.cbTimeStep.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbTimeStep.FormattingEnabled = true;
    this.cbTimeStep.Location = new Point(189, 54);
    this.cbTimeStep.Margin = new Padding(4, 4, 4, 4);
    this.cbTimeStep.Name = "cbTimeStep";
    this.cbTimeStep.Size = new Size(675, 24);
    this.cbTimeStep.TabIndex = 2;
    this.dtpToDate.CustomFormat = "dd.MM.yyyy";
    this.dtpToDate.Format = DateTimePickerFormat.Custom;
    this.dtpToDate.Location = new Point(645, 21);
    this.dtpToDate.Margin = new Padding(4, 4, 4, 4);
    this.dtpToDate.MinDate = new DateTime(1980, 12, 1, 0, 0, 0, 0);
    this.dtpToDate.Name = "dtpToDate";
    this.dtpToDate.Size = new Size(121, 22);
    this.dtpToDate.TabIndex = 1;
    this.dtpToDate.Value = new DateTime(2015, 7, 14, 0, 0, 0, 0);
    this.dtpFromDate.CustomFormat = "dd.MM.yyyy";
    this.dtpFromDate.Format = DateTimePickerFormat.Custom;
    this.dtpFromDate.Location = new Point(216, 21);
    this.dtpFromDate.Margin = new Padding(4, 4, 4, 4);
    this.dtpFromDate.MinDate = new DateTime(1980, 12, 1, 0, 0, 0, 0);
    this.dtpFromDate.Name = "dtpFromDate";
    this.dtpFromDate.Size = new Size(121, 22);
    this.dtpFromDate.TabIndex = 1;
    this.dtpFromDate.Value = new DateTime(2015, 7, 14, 0, 0, 0, 0);
    this.label3.AutoSize = true;
    this.label3.Location = new Point(444, 23);
    this.label3.Margin = new Padding(4, 0, 4, 0);
    this.label3.Name = "label3";
    this.label3.Size = new Size(191, 17);
    this.label3.TabIndex = 0;
    this.label3.Text = "Конец отсчётного периода:";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(8, 23);
    this.label1.Margin = new Padding(4, 0, 4, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(204, 17);
    this.label1.TabIndex = 0;
    this.label1.Text = "Начало отсчётного периода: ";
    this.drawChartWorker.WorkerReportsProgress = true;
    this.drawChartWorker.DoWork += new DoWorkEventHandler(this.drawChartWorker_DoWork);
    this.drawChartWorker.ProgressChanged += new ProgressChangedEventHandler(this.drawChartWorker_ProgressChanged);
    this.drawChartWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.drawChartWorker_RunWorkerCompleted);
    this.drawUsersChartWorker.WorkerReportsProgress = true;
    this.drawUsersChartWorker.DoWork += new DoWorkEventHandler(this.drawUsersChartWorker_DoWork);
    this.drawUsersChartWorker.ProgressChanged += new ProgressChangedEventHandler(this.drawUsersChartWorker_ProgressChanged);
    this.drawUsersChartWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.drawChartWorker_RunWorkerCompleted);
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.chart1, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.groupBox1, 0, 1);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 200f));
    this.tableLayoutPanel1.Size = new Size(1011, 508);
    this.tableLayoutPanel1.TabIndex = 3;
    this.ccbEventLogRecordType.CheckOnClick = true;
    this.ccbEventLogRecordType.DrawMode = DrawMode.OwnerDrawVariable;
    this.ccbEventLogRecordType.DropDownHeight = 1;
    this.ccbEventLogRecordType.FormattingEnabled = true;
    this.ccbEventLogRecordType.IntegralHeight = false;
    this.ccbEventLogRecordType.Location = new Point((int) sbyte.MaxValue, 153);
    this.ccbEventLogRecordType.Margin = new Padding(4);
    this.ccbEventLogRecordType.Name = "ccbEventLogRecordType";
    this.ccbEventLogRecordType.Size = new Size(737, 23);
    this.ccbEventLogRecordType.TabIndex = 5;
    this.ccbEventLogRecordType.ValueSeparator = ", ";
    this.ccbActionType.CheckOnClick = true;
    this.ccbActionType.DrawMode = DrawMode.OwnerDrawVariable;
    this.ccbActionType.DropDownHeight = 1;
    this.ccbActionType.FormattingEnabled = true;
    this.ccbActionType.IntegralHeight = false;
    this.ccbActionType.Location = new Point((int) sbyte.MaxValue, 122);
    this.ccbActionType.Margin = new Padding(4);
    this.ccbActionType.Name = "ccbActionType";
    this.ccbActionType.Size = new Size(737, 23);
    this.ccbActionType.TabIndex = 5;
    this.ccbActionType.ValueSeparator = ", ";
    this.ccbUsers.CheckOnClick = true;
    this.ccbUsers.DrawMode = DrawMode.OwnerDrawVariable;
    this.ccbUsers.DropDownHeight = 1;
    this.ccbUsers.FormattingEnabled = true;
    this.ccbUsers.IntegralHeight = false;
    this.ccbUsers.Location = new Point((int) sbyte.MaxValue, 89);
    this.ccbUsers.Margin = new Padding(4);
    this.ccbUsers.Name = "ccbUsers";
    this.ccbUsers.Size = new Size(737, 23);
    this.ccbUsers.TabIndex = 5;
    this.ccbUsers.ValueSeparator = ", ";
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Margin = new Padding(4, 4, 4, 4);
    this.MinimumSize = new Size(1011, 0);
    this.Name = nameof (StatisticsView);
    this.Size = new Size(1011, 508);
    this.chart1.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.tableLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
