
// Type: Intermech.Client.Core.Organizer.OrganizerReminderForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Форма напоминания пользователям о запланированных задачах.
/// </summary>
public class OrganizerReminderForm : Form
{
  private Dictionary<long, ListViewItem> _dictItemsIDs = new Dictionary<long, ListViewItem>();
  private Dictionary<ListViewItem, OrganizerReminderForm.ItemsInfo> _dictItemsInfo = new Dictionary<ListViewItem, OrganizerReminderForm.ItemsInfo>();
  private string _captionForm = LocalizationHolder.rm.GetString("Organizer_Reminder_Form_Caption");
  private string _captionDateStart = LocalizationHolder.rm.GetString("Organizer_Reminder_Form_DateStart");
  private string _captionDateFinish = LocalizationHolder.rm.GetString("Organizer_Reminder_Form_DateFinish");
  private string _captionSelectedCount = LocalizationHolder.rm.GetString("Organizer_Reminder_Form_SelectedCount");
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnStopAll;
  private Button _btnOpen;
  private Button _btnStop;
  private Label _lbTaskName;
  private Label _lbDateStart;
  private ListView _lvTasks;
  private ColumnHeader _colSubject;
  private PictureBox _pict;
  private Label _lbDefinition;
  private Label _lbDateFinish;
  private Panel _pnlBottom;
  private ToolTip _tt;
  private Panel _pnlDelay;
  private ComboBox _cmbUnit;
  private ComboBox _cmbInterval;
  private Label _lbDelayMsg;
  private Button _btnDelay;
  private GroupBox _grb;
  private ColumnHeader _colText;

  /// <summary>Конструктор.</summary>
  public OrganizerReminderForm()
  {
    this.InitializeComponent();
    if (Statics.IconSrv == null)
      return;
    this._lvTasks.SmallImageList = Statics.IconSrv.ImageList;
    this._cmbUnit.DataSource = (object) new DataTable()
    {
      Columns = {
        {
          "Key",
          typeof (OrganizerReminderForm.TimeUnits)
        },
        {
          "Value",
          typeof (string)
        }
      },
      Rows = {
        new object[2]
        {
          (object) OrganizerReminderForm.TimeUnits.Min,
          (object) LocalizationHolder.rm.GetString("Organizer_Reminder_TimeUnits_Min")
        },
        new object[2]
        {
          (object) OrganizerReminderForm.TimeUnits.Hour,
          (object) LocalizationHolder.rm.GetString("Organizer_Reminder_TimeUnits_Hour")
        },
        new object[2]
        {
          (object) OrganizerReminderForm.TimeUnits.Day,
          (object) LocalizationHolder.rm.GetString("Organizer_Reminder_TimeUnits_Day")
        },
        new object[2]
        {
          (object) OrganizerReminderForm.TimeUnits.Week,
          (object) LocalizationHolder.rm.GetString("Organizer_Reminder_TimeUnits_Week")
        }
      }
    };
    this._cmbUnit.DisplayMember = "Value";
    this._cmbUnit.ValueMember = "Key";
    this._cmbUnit.SelectedIndexChanged += new EventHandler(this.On_cmbUnit_SelectedIndexChanged);
    this.On_cmbUnit_SelectedIndexChanged((object) this._cmbUnit, new EventArgs());
  }

  /// <summary>
  /// Событие, для переноса напоминания для выбранного объекта.
  /// </summary>
  public event DelayReminderForObjectsHandler DelayReminderForObjects;

  /// <summary>
  /// Событие, для выключения напоминания для выбранного объекта.
  /// </summary>
  public event StopReminderForObjectsHandler StopReminderForObjects;

  /// <summary>Отложить оповещение.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnDelay_Click(object sender, EventArgs e)
  {
    if (this._lvTasks.SelectedItems.Count <= 0)
      return;
    double result1 = 0.0;
    string empty = string.Empty;
    string s = string.Empty;
    switch (CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator)
    {
      case ".":
        s = this._cmbInterval.Text.Replace(',', '.');
        break;
      case ",":
        s = this._cmbInterval.Text.Replace('.', ',');
        break;
    }
    if (string.IsNullOrEmpty(s) || !double.TryParse(s, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
    {
      int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Organizer_Reminder_InvalidDelayTime"), (object) s), LocalizationHolder.rm.GetString("Organizer_Name"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      this._cmbInterval.Focus();
    }
    else
    {
      Dictionary<int, Dictionary<long, DateTime>> dict = new Dictionary<int, Dictionary<long, DateTime>>(1);
      DateTime dateTime = DateTime.Now;
      int result2 = 0;
      int.TryParse(this._cmbUnit.SelectedValue.ToString(), out result2);
      switch (result2)
      {
        case 0:
          dateTime = DateTime.Now.AddMinutes(result1);
          break;
        case 1:
          dateTime = DateTime.Now.AddHours(result1);
          break;
        case 2:
          dateTime = DateTime.Now.AddDays(result1);
          break;
        case 3:
          dateTime = DateTime.Now.AddDays(result1 * 7.0);
          break;
      }
      foreach (ListViewItem selectedItem in this._lvTasks.SelectedItems)
      {
        OrganizerReminderForm.ItemsInfo itemsInfo = this._dictItemsInfo[selectedItem];
        if (!dict.ContainsKey(itemsInfo.ObjectTypeID))
          dict.Add(itemsInfo.ObjectTypeID, new Dictionary<long, DateTime>(this._lvTasks.SelectedItems.Count));
        dict[itemsInfo.ObjectTypeID].Add(itemsInfo.ObjectID, dateTime);
      }
      this.OnDelayReminder(dict);
      ListViewItem[] dest = new ListViewItem[this._lvTasks.SelectedItems.Count];
      this._lvTasks.SelectedItems.CopyTo((Array) dest, 0);
      for (int index = 0; index < dest.Length; ++index)
        this.Remove(this._dictItemsInfo[dest[index]].ObjectID);
      if (this._lvTasks.Items.Count != 0)
        return;
      this.Close();
    }
  }

  /// <summary>Открытие карточки объекта.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnOpen_Click(object sender, EventArgs e)
  {
    if (this._lvTasks.SelectedItems.Count == 0)
      return;
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, this._dictItemsInfo[this._lvTasks.SelectedItems[0]].ObjectID, false);
  }

  /// <summary>Прекращение напоминания о выделенных объектах.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnStop_Click(object sender, EventArgs e)
  {
    if (this._lvTasks.SelectedItems.Count <= 0)
      return;
    Dictionary<int, List<long>> dict = new Dictionary<int, List<long>>(1);
    foreach (ListViewItem selectedItem in this._lvTasks.SelectedItems)
    {
      OrganizerReminderForm.ItemsInfo itemsInfo = this._dictItemsInfo[selectedItem];
      if (!dict.ContainsKey(itemsInfo.ObjectTypeID))
        dict.Add(itemsInfo.ObjectTypeID, new List<long>(this._lvTasks.SelectedItems.Count));
      dict[itemsInfo.ObjectTypeID].Add(itemsInfo.ObjectID);
    }
    this.OnStopReminder(dict);
    ListViewItem[] dest = new ListViewItem[this._lvTasks.SelectedItems.Count];
    this._lvTasks.SelectedItems.CopyTo((Array) dest, 0);
    for (int index = 0; index < dest.Length; ++index)
      this.Remove(this._dictItemsInfo[dest[index]].ObjectID);
    if (this._lvTasks.Items.Count != 0)
      return;
    this.Close();
  }

  /// <summary>Прекращение напоминания о всех объектах.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnStopAll_Click(object sender, EventArgs e)
  {
    Dictionary<int, List<long>> dict = new Dictionary<int, List<long>>();
    foreach (ListViewItem key in this._lvTasks.Items)
    {
      OrganizerReminderForm.ItemsInfo itemsInfo = this._dictItemsInfo[key];
      if (!dict.ContainsKey(itemsInfo.ObjectTypeID))
        dict.Add(itemsInfo.ObjectTypeID, new List<long>(this._lvTasks.Items.Count));
      dict[itemsInfo.ObjectTypeID].Add(itemsInfo.ObjectID);
    }
    this.OnStopReminder(dict);
    ListViewItem[] dest = new ListViewItem[this._lvTasks.Items.Count];
    this._lvTasks.Items.CopyTo((Array) dest, 0);
    for (int index = 0; index < dest.Length; ++index)
      this.Remove(this._dictItemsInfo[dest[index]].ObjectID);
    if (this._lvTasks.Items.Count != 0)
      return;
    this.Close();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
  {
    int result = 0;
    int.TryParse(this._cmbUnit.SelectedValue.ToString(), out result);
    this._cmbInterval.Items.Clear();
    switch (result)
    {
      case 0:
        this._cmbInterval.Items.AddRange(new object[4]
        {
          (object) 5,
          (object) 10,
          (object) 15,
          (object) 30
        });
        break;
      case 1:
        this._cmbInterval.Items.AddRange(new object[4]
        {
          (object) 1,
          (object) 2,
          (object) 4,
          (object) 8
        });
        break;
      case 2:
        this._cmbInterval.Items.AddRange(new object[5]
        {
          (object) 0.5,
          (object) 1,
          (object) 2,
          (object) 3,
          (object) 4
        });
        break;
      case 3:
        this._cmbInterval.Items.AddRange(new object[3]
        {
          (object) 1,
          (object) 2,
          (object) 3
        });
        break;
    }
    this._cmbInterval.SelectedIndex = 0;
  }

  /// <summary>Изменение выделенных объектов.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lvTasks_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._lvTasks.SelectedItems.Count == 1)
    {
      this._pict.Visible = this._lbTaskName.Visible = this._lbDateFinish.Visible = true;
      this._btnOpen.Enabled = true;
      ListViewItem selectedItem = this._lvTasks.SelectedItems[0];
      this._pict.Image = selectedItem.ImageList.Images[selectedItem.ImageIndex];
      this._lbTaskName.Text = selectedItem.Text;
      this._tt.SetToolTip((Control) this._lbTaskName, this._lbTaskName.Text);
      OrganizerReminderForm.ItemsInfo itemsInfo = this._dictItemsInfo[selectedItem];
      this._lbDateStart.Text = itemsInfo.DateStart != DateTime.MinValue ? $"{this._captionDateStart} {itemsInfo.DateStart}" : this._captionDateStart;
      this._lbDateFinish.Text = itemsInfo.DateFinish != DateTime.MinValue ? $"{this._captionDateFinish} {itemsInfo.DateFinish}" : this._captionDateFinish;
    }
    else
    {
      this._pict.Visible = this._lbTaskName.Visible = this._lbDateFinish.Visible = false;
      this._lbDateStart.Text = $"{this._captionSelectedCount} {this._lvTasks.SelectedItems.Count}";
      if (this._lvTasks.SelectedItems.Count == 0)
      {
        this._pnlBottom.Enabled = this._pnlDelay.Enabled = false;
        return;
      }
      this._btnOpen.Enabled = false;
    }
    this._pnlBottom.Enabled = this._pnlDelay.Enabled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Проверка наличия просроченных задач.</summary>
  private void CheckExpiredItems()
  {
    foreach (ListViewItem key in this._lvTasks.Items)
    {
      if (this._dictItemsInfo.ContainsKey(key))
      {
        OrganizerReminderForm.ItemsInfo itemsInfo = this._dictItemsInfo[key];
        if (!(itemsInfo.DateFinish == DateTime.MinValue) && !(itemsInfo.DateFinish > DateTime.Now))
        {
          this._lbDefinition.Visible = true;
          return;
        }
      }
    }
    this._lbDefinition.Visible = false;
  }

  /// <summary>Перенос напоминания.</summary>
  /// <param name="dict">Коллекция объектов
  /// int - идентификатор типа объектов
  /// Int64 - идентификатор объекта
  /// DateTime - Новая дата и время напоминания</param>
  private void OnDelayReminder(Dictionary<int, Dictionary<long, DateTime>> dict)
  {
    if (this.DelayReminderForObjects == null)
      return;
    this.DelayReminderForObjects((object) this, dict);
  }

  /// <summary>Прекращение напоминания.</summary>
  /// <param name="dict">Коллекция объектов
  /// int - идентификатор типа объектов
  /// Int64 - идентификатор объекта</param>
  private void OnStopReminder(Dictionary<int, List<long>> dict)
  {
    if (this.StopReminderForObjects == null)
      return;
    this.StopReminderForObjects((object) this, dict);
  }

  /// <summary>Удаление элемента.</summary>
  /// <param name="objID">Идентификатор элемента</param>
  private void Remove(long objID)
  {
    if (this._dictItemsIDs.ContainsKey(objID))
    {
      this._lvTasks.Items.Remove(this._dictItemsIDs[objID]);
      this._dictItemsInfo.Remove(this._dictItemsIDs[objID]);
      this._dictItemsIDs.Remove(objID);
    }
    if (this._lvTasks.Items.Count > 0)
    {
      if (this._lvTasks.SelectedItems.Count == 0)
        this._lvTasks.Items[0].Selected = true;
      this.Text = $"{this._captionForm} {this._lvTasks.Items.Count}";
    }
    this.CheckExpiredItems();
  }

  /// <summary>Обновление информации.</summary>
  /// <param name="objsInfo">Информация об объектах
  /// int - идентификатор типа объектов
  /// DataRow[] - массив информации об объектах</param>
  public void Refresh(Dictionary<int, DataRow[]> objsInfo)
  {
    List<string> stringList = new List<string>(this._lvTasks.SelectedItems.Count);
    foreach (ListViewItem selectedItem in this._lvTasks.SelectedItems)
      stringList.Add(selectedItem.Name);
    this._dictItemsIDs.Clear();
    this._dictItemsInfo.Clear();
    this._lvTasks.Items.Clear();
    this._lbDefinition.Visible = false;
    string empty = string.Empty;
    foreach (KeyValuePair<int, DataRow[]> keyValuePair in objsInfo)
    {
      int imageIndex = Statics.IconSrv.IndexOf(4, keyValuePair.Key);
      foreach (DataRow row in keyValuePair.Value)
      {
        OrganizerReminderForm.ItemsInfo itemsInfo = new OrganizerReminderForm.ItemsInfo();
        itemsInfo.Refresh(row);
        ListViewItem listViewItem = new ListViewItem(Convert.ToString(row["CAPTION"]), imageIndex)
        {
          Name = Convert.ToString(row["OBJECT_ID"])
        };
        string text = Convert.ToString(row["OBJECT_TEXT"]);
        if (text.Length > 100)
          text = $"{text.Substring(0, 100)}...";
        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, text));
        if (itemsInfo.DateFinish != DateTime.MinValue && itemsInfo.DateFinish < DateTime.Now)
        {
          listViewItem.ForeColor = Color.Red;
          this._lbDefinition.Visible = true;
        }
        this._lvTasks.Items.Add(listViewItem);
        this._dictItemsIDs.Add(itemsInfo.ObjectID, listViewItem);
        this._dictItemsInfo.Add(listViewItem, itemsInfo);
        listViewItem.Selected = stringList.Contains(listViewItem.Name);
      }
    }
    this.Text = $"{this._captionForm} {this._lvTasks.Items.Count}";
    if (this._lvTasks.SelectedItems.Count != 0)
      return;
    if (this._lvTasks.Items.Count == 0)
      this.Close();
    else
      this._lvTasks.Items[0].Selected = true;
  }

  /// <summary>Обновление элемента.</summary>
  /// <param name="objID">Идентификатор элемента</param>
  /// <param name="row">Строка с данными</param>
  public void RefreshElement(long objID, DataRow row)
  {
    if (!this._dictItemsIDs.ContainsKey(objID))
      return;
    ListViewItem dictItemsId = this._dictItemsIDs[objID];
    object obj = row["CAPTION"];
    if (obj != null && obj != DBNull.Value)
      dictItemsId.Text = obj.ToString();
    OrganizerReminderForm.ItemsInfo itemsInfo = this._dictItemsInfo[dictItemsId];
    itemsInfo.Refresh(row);
    dictItemsId.ForeColor = !(itemsInfo.DateFinish != DateTime.MinValue) || !(itemsInfo.DateFinish < DateTime.Now) ? Color.Black : Color.Red;
    if (!dictItemsId.Selected || this._lvTasks.SelectedItems.Count != 1)
      return;
    if (itemsInfo.DateFinish != DateTime.MinValue)
      this._lbDateFinish.Text = $"{this._captionDateFinish} {itemsInfo.DateFinish}";
    else
      this._lbDateStart.Text = this._captionDateFinish;
    this._lbTaskName.Text = dictItemsId.Text;
    this._tt.SetToolTip((Control) this._lbTaskName, this._lbTaskName.Text);
    this._lbDateStart.Text = itemsInfo.DateStart != DateTime.MinValue ? $"{this._captionDateStart} {itemsInfo.DateStart}" : this._captionDateStart;
    this.CheckExpiredItems();
  }

  /// <summary>Удаление элемента.</summary>
  /// <param name="objID">Идентификатор элемента</param>
  public void RemoveElement(long objID)
  {
    this.Remove(objID);
    if (this._lvTasks.Items.Count != 0)
      return;
    this.Close();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OrganizerReminderForm));
    this._btnStopAll = new Button();
    this._btnOpen = new Button();
    this._btnStop = new Button();
    this._lbTaskName = new Label();
    this._lbDateStart = new Label();
    this._lvTasks = new ListView();
    this._colSubject = new ColumnHeader();
    this._pict = new PictureBox();
    this._lbDefinition = new Label();
    this._lbDateFinish = new Label();
    this._pnlBottom = new Panel();
    this._tt = new ToolTip(this.components);
    this._pnlDelay = new Panel();
    this._btnDelay = new Button();
    this._cmbUnit = new ComboBox();
    this._cmbInterval = new ComboBox();
    this._lbDelayMsg = new Label();
    this._grb = new GroupBox();
    this._colText = new ColumnHeader();
    ((ISupportInitialize) this._pict).BeginInit();
    this._pnlBottom.SuspendLayout();
    this._pnlDelay.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnStopAll, "_btnStopAll");
    this._btnStopAll.Name = "_btnStopAll";
    this._btnStopAll.UseVisualStyleBackColor = true;
    this._btnStopAll.Click += new EventHandler(this.On_btnStopAll_Click);
    componentResourceManager.ApplyResources((object) this._btnOpen, "_btnOpen");
    this._btnOpen.Name = "_btnOpen";
    this._btnOpen.UseVisualStyleBackColor = true;
    this._btnOpen.Click += new EventHandler(this.On_btnOpen_Click);
    componentResourceManager.ApplyResources((object) this._btnStop, "_btnStop");
    this._btnStop.Name = "_btnStop";
    this._btnStop.UseVisualStyleBackColor = true;
    this._btnStop.Click += new EventHandler(this.On_btnStop_Click);
    componentResourceManager.ApplyResources((object) this._lbTaskName, "_lbTaskName");
    this._lbTaskName.Name = "_lbTaskName";
    componentResourceManager.ApplyResources((object) this._lbDateStart, "_lbDateStart");
    this._lbDateStart.Name = "_lbDateStart";
    componentResourceManager.ApplyResources((object) this._lvTasks, "_lvTasks");
    this._lvTasks.Columns.AddRange(new ColumnHeader[2]
    {
      this._colSubject,
      this._colText
    });
    this._lvTasks.FullRowSelect = true;
    this._lvTasks.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this._lvTasks.HideSelection = false;
    this._lvTasks.Name = "_lvTasks";
    this._lvTasks.UseCompatibleStateImageBehavior = false;
    this._lvTasks.View = View.Details;
    this._lvTasks.SelectedIndexChanged += new EventHandler(this.On_lvTasks_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._colSubject, "_colSubject");
    componentResourceManager.ApplyResources((object) this._pict, "_pict");
    this._pict.Name = "_pict";
    this._pict.TabStop = false;
    componentResourceManager.ApplyResources((object) this._lbDefinition, "_lbDefinition");
    this._lbDefinition.ForeColor = Color.Red;
    this._lbDefinition.Name = "_lbDefinition";
    componentResourceManager.ApplyResources((object) this._lbDateFinish, "_lbDateFinish");
    this._lbDateFinish.Name = "_lbDateFinish";
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Controls.Add((Control) this._btnStop);
    this._pnlBottom.Controls.Add((Control) this._btnStopAll);
    this._pnlBottom.Controls.Add((Control) this._btnOpen);
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._pnlDelay, "_pnlDelay");
    this._pnlDelay.Controls.Add((Control) this._btnDelay);
    this._pnlDelay.Controls.Add((Control) this._cmbUnit);
    this._pnlDelay.Controls.Add((Control) this._cmbInterval);
    this._pnlDelay.Controls.Add((Control) this._lbDelayMsg);
    this._pnlDelay.Name = "_pnlDelay";
    componentResourceManager.ApplyResources((object) this._btnDelay, "_btnDelay");
    this._btnDelay.Name = "_btnDelay";
    this._btnDelay.UseVisualStyleBackColor = true;
    this._btnDelay.Click += new EventHandler(this.On_btnDelay_Click);
    componentResourceManager.ApplyResources((object) this._cmbUnit, "_cmbUnit");
    this._cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbUnit.FormattingEnabled = true;
    this._cmbUnit.Name = "_cmbUnit";
    componentResourceManager.ApplyResources((object) this._cmbInterval, "_cmbInterval");
    this._cmbInterval.FormattingEnabled = true;
    this._cmbInterval.Name = "_cmbInterval";
    componentResourceManager.ApplyResources((object) this._lbDelayMsg, "_lbDelayMsg");
    this._lbDelayMsg.Name = "_lbDelayMsg";
    componentResourceManager.ApplyResources((object) this._grb, "_grb");
    this._grb.Name = "_grb";
    this._grb.TabStop = false;
    componentResourceManager.ApplyResources((object) this._colText, "_colText");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._grb);
    this.Controls.Add((Control) this._pnlDelay);
    this.Controls.Add((Control) this._pnlBottom);
    this.Controls.Add((Control) this._lbDateFinish);
    this.Controls.Add((Control) this._lbDefinition);
    this.Controls.Add((Control) this._pict);
    this.Controls.Add((Control) this._lvTasks);
    this.Controls.Add((Control) this._lbDateStart);
    this.Controls.Add((Control) this._lbTaskName);
    this.DoubleBuffered = true;
    this.Name = nameof (OrganizerReminderForm);
    ((ISupportInitialize) this._pict).EndInit();
    this._pnlBottom.ResumeLayout(false);
    this._pnlDelay.ResumeLayout(false);
    this._pnlDelay.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Класс для хранения данных объетка.</summary>
  private class ItemsInfo
  {
    internal int ObjectTypeID = -1;
    internal long ObjectID;
    internal DateTime DateStart = DateTime.MinValue;
    internal DateTime DateFinish = DateTime.MinValue;

    /// <summary>Обновление данных объекта.</summary>
    /// <param name="row">Строка с данными</param>
    internal void Refresh(DataRow row)
    {
      this.ObjectTypeID = Convert.ToInt32(row["OBJECT_TYPE"]);
      this.ObjectID = Convert.ToInt64(row["OBJECT_ID"]);
      object obj1 = row["START_DATE"];
      if (obj1 != null && obj1 != DBNull.Value)
        DateTime.TryParse(obj1.ToString(), out this.DateStart);
      object obj2 = row["FINISH_DATE"];
      if (obj2 == null || obj2 == DBNull.Value)
        return;
      DateTime.TryParse(obj2.ToString(), out this.DateFinish);
    }
  }

  /// <summary>Единицы измерения времени.</summary>
  private enum TimeUnits
  {
    Min,
    Hour,
    Day,
    Week,
  }
}
