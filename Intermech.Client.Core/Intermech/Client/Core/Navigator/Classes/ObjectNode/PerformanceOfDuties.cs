
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.PerformanceOfDuties
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.CustomServices;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

[ViewDescriptionProvider(typeof (PerformanceOfDuties.PerformanceOfDutiesViewDescriptionProvider))]
public class PerformanceOfDuties : UserControl, IView
{
  /// <summary>Контейнер сервисов</summary>
  private System.IServiceProvider _services;
  /// <summary>
  /// Пользователь, для которого показана вкладка исполнения обязанностей
  /// </summary>
  private long _userId;
  /// <summary>
  /// Наименование пользователя, для которого показана вкладка исполнения обязанностей
  /// </summary>
  private string _userCaption;
  /// <summary>Индекс изображения</summary>
  private int _imageIndex;
  /// <summary>Выполнена ли инициализация некоторых полей</summary>
  private bool _firstInitialized;
  /// <summary>Требуется ли инициализация закладки</summary>
  private bool _reinitialize;
  /// <summary>
  /// Загружен ли грид со списком тех, кто исполняет обязанности пользователя.
  /// </summary>
  private bool _isLoadedWhoOfficiatesUser;
  /// <summary>
  /// Загружен ли грид со списком тех, чьи обязанности исполняет пользователь.
  /// </summary>
  private bool _isLoadedWhomUserOfficiates;
  /// <summary>Список настроек исполнения обязанностей</summary>
  private List<ObjectIOSettings> _newObjectIOSettings;
  private List<ObjectIOSettings> _oldObjectIoSettings;
  private List<ObjectIOSettings> _deleteObjectIoSettings;
  private IRolesService _rolesService;
  private IUserSubstituteService _userSubstituteService;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem tsmiAddRow;
  private ToolStripMenuItem tsmiDeleteRow;
  private Panel panel1;
  private Button btnCancel;
  private Button btnCreate;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextWithButtonColumn dataGridViewTextWithButtonColumn1;
  private DataGridViewCalendarColumn dataGridViewCalendarColumn1;
  private DataGridViewCalendarColumn dataGridViewCalendarColumn2;
  private Label lblFired;
  private TabControl tabControl1;
  private TabPage tabPage1;
  private DataGridView dataGridView1;
  private DataGridViewTextBoxColumn cID;
  private DataGridViewTextWithButtonColumn cOfficiate;
  private DataGridViewCalendarColumn cBeginData;
  private DataGridViewCalendarColumn cEndData;
  private DataGridViewComboBoxColumn cRole;
  private TabPage tabPage2;
  private DataGridView dataGridView2;
  private DataGridViewTextBoxColumn cID2;
  private DataGridViewTextBoxColumn cOfficiety2;
  private DataGridViewCalendarColumn cBeginData2;
  private DataGridViewCalendarColumn cEndData2;
  private DataGridViewTextBoxColumn cRole2;

  public PerformanceOfDuties()
  {
    this.InitializeComponent();
    this.dataGridView1.Dock = DockStyle.Fill;
    this.dataGridView1.Rows.Clear();
    this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dataGridView1.RowTemplate.ContextMenuStrip = this.contextMenuStrip1;
    this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
    this.btnCreate.Enabled = false;
    this.btnCancel.Enabled = false;
    (this.dataGridView1.Columns[this.cOfficiate.Index] as DataGridViewTextWithButtonColumn).TextReadOnly = true;
    (this.dataGridView1.Columns[this.cOfficiate.Index] as DataGridViewTextWithButtonColumn).ButtonClick += new EventHandler(this.OnTextWithButtonColumn_ButtonClick);
    (this.dataGridView1.Columns[this.cOfficiate.Index] as DataGridViewTextWithButtonColumn).KeyDown += new EventHandler(this.OnTextWithButtonColumn_KeyDown);
    (this.dataGridView1.Columns[this.cBeginData.Index] as DataGridViewCalendarColumn).ClouseUp += new EventHandler(this.OnCalendaColumn_BeginData_ClouseUp);
    (this.dataGridView1.Columns[this.cEndData.Index] as DataGridViewCalendarColumn).ClouseUp += new EventHandler(this.OnCalendaColumn_EndData_ClouseUp);
    this.dataGridView2.Rows.Clear();
    this.InitResources();
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider service)
  {
    this._services = service;
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    this._userId = itemData.ObjectID;
    this._userCaption = itemData.Caption;
    this._firstInitialized = true;
    this._reinitialize = true;
  }

  public void Activate(IView previousView)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._rolesService = sessionKeeper.Session.GetCustomService(typeof (IRolesService)) as IRolesService;
      if (this._rolesService == null)
        throw new KernelException("Не найдена служба для получения информации о ролях пользователей.");
      this._userSubstituteService = sessionKeeper.Session.GetCustomService(typeof (IUserSubstituteService)) as IUserSubstituteService;
      if (this._userSubstituteService == null)
        throw new KernelException("Не найдена служба для работы с исполняющими обязанности пользователей.");
      if (!this._reinitialize)
        return;
      this._isLoadedWhoOfficiatesUser = false;
      this._isLoadedWhomUserOfficiates = false;
      if (this._newObjectIOSettings != null)
        this._newObjectIOSettings.Clear();
      this.dataGridView1.Rows.Clear();
      this.dataGridView2.Rows.Clear();
      if (sessionKeeper.Session.GetObject(this._userId).LCStep == MetaDataHelper.GetLCStepID(new Guid("cadd9504-306c-11d8-b4e9-00304f19f545")))
      {
        this.dataGridView1.ReadOnly = true;
        this.dataGridView1.RowTemplate.ContextMenuStrip = (ContextMenuStrip) null;
        this.dataGridView1.ContextMenuStrip = (ContextMenuStrip) null;
        this.lblFired.Visible = true;
      }
      else
      {
        this.dataGridView1.ReadOnly = false;
        this.dataGridView1.RowTemplate.ContextMenuStrip = this.contextMenuStrip1;
        this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
        this.lblFired.Visible = false;
      }
      if (this.tabControl1.SelectedTab.TabIndex == 0)
        this.LoadWhomUserOfficiates(sessionKeeper);
      else if (this.tabControl1.SelectedTab.TabIndex == 1)
        this.LoadWhoOfficiatesUser(sessionKeeper);
      this._reinitialize = false;
    }
  }

  public void Deactivate(IView nextView)
  {
    this._reinitialize = true;
    if (this._newObjectIOSettings.FindAll((Predicate<ObjectIOSettings>) (q => q.Reload)).Count > 0 && this._newObjectIOSettings.Find((Predicate<ObjectIOSettings>) (q => q.IoList.Count > 0)) != null)
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1649"), LocalizationHolder.rm.GetString("Client.Core_1650"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
        this.Savedata();
      this._newObjectIOSettings.Clear();
    }
    if (this._oldObjectIoSettings != null)
      this._oldObjectIoSettings.Clear();
    this.btnCancel.Enabled = false;
    this.btnCreate.Enabled = false;
  }

  /// <summary>Вернуть заголовок вьюшки</summary>
  public string Caption => LocalizationHolder.rm.GetString("Client.Core_1651");

  /// <summary>
  /// Вернуть номер изображения вьюшки из глобального списка
  /// </summary>
  public int ImageIndex => this._imageIndex;

  /// <summary>Вернуть порядковый номер вьюшки в списке всех вьюшек</summary>
  public int OrderID => 17;

  /// <summary>Инициализировать ресурсы закладки</summary>
  private void InitResources()
  {
    this._imageIndex = -1;
    this.Name = "PerformanceOfDuities";
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Загрузка данных на вкладку Кто исполняет обязанности пользователя.
  /// </summary>
  private void LoadWhoOfficiatesUser(SessionKeeper sk)
  {
    foreach (UserSubstitute userSubstitute in this._userSubstituteService.GetUserSubstitutes(sk.Session.SessionGUID, this._userId))
      this.dataGridView2.Rows.Add((object) userSubstitute.SettingsId, (object) userSubstitute.SubstituteUserName, (object) userSubstitute.BeginDate, (object) userSubstitute.EndDate, (object) userSubstitute.RoleCaption);
    this._isLoadedWhoOfficiatesUser = true;
  }

  /// <summary>
  /// Загрузка вкладки Чьи обязанности исполняет пользоваель
  /// </summary>
  /// <param name="selUserObject">Пользователь.</param>
  /// <param name="keeper">Хранитель сессии.</param>
  private void LoadWhomUserOfficiates(SessionKeeper keeper)
  {
    if (MetaDataHelper.GetObjectTypeID("cadd94e2-306c-11d8-b4e9-00304f19f545") == -1)
      return;
    List<ObjectIOSettings> usersIoSettings = this._userSubstituteService.GetUsersIOSettings(keeper.Session.SessionGUID, this._userCaption);
    if (usersIoSettings.Count == 0)
    {
      this.CreateEmptySetting();
    }
    else
    {
      foreach (ObjectIOSettings objectIoSettings in usersIoSettings)
      {
        this.cRole.Items.Clear();
        DateTime dateTime1 = objectIoSettings.BeginDate == "" ? DateTime.MinValue : Convert.ToDateTime(objectIoSettings.BeginDate);
        DateTime dateTime2 = objectIoSettings.EndDate == "" ? DateTime.MinValue : Convert.ToDateTime(objectIoSettings.EndDate);
        this.dataGridView1.Rows.Add((object) objectIoSettings.ID, (object) objectIoSettings.IOCaptions(), (object) dateTime1, (object) dateTime2, (object) objectIoSettings.CurrentRole.RoleName);
        DataGridViewComboBoxCell viewComboBoxCell = new DataGridViewComboBoxCell();
        viewComboBoxCell.Items.Add((object) "");
        List<string> list = this.GetCommonUsersRoles(objectIoSettings.IoList.Select<MyElement, long>((Func<MyElement, long>) (x => (long) x.Value)).ToList<long>()).Select<RoleProperties, string>((Func<RoleProperties, string>) (x => x.RoleName)).ToList<string>();
        viewComboBoxCell.Items.AddRange((object[]) list.ToArray());
        if (objectIoSettings.CurrentRole.RoleID == 0L || !list.Contains(objectIoSettings.CurrentRole.RoleName))
          viewComboBoxCell.Value = (object) "";
        else
          viewComboBoxCell.Value = (object) objectIoSettings.CurrentRole.RoleName;
        this.dataGridView1.Rows[this.dataGridView1.RowCount - 1].Cells[this.cRole.Index] = (DataGridViewCell) viewComboBoxCell;
        if (this._oldObjectIoSettings == null)
        {
          this._oldObjectIoSettings = new List<ObjectIOSettings>();
          this._newObjectIOSettings = new List<ObjectIOSettings>();
        }
        if (!this._oldObjectIoSettings.Contains(objectIoSettings))
        {
          this._oldObjectIoSettings.Add(objectIoSettings);
          this._newObjectIOSettings.Add(objectIoSettings);
        }
      }
    }
    this._isLoadedWhomUserOfficiates = true;
  }

  private void CreateEmptySetting()
  {
    this.dataGridView1.Rows.Add((object) -1, (object) "", (object) DateTime.MinValue, (object) DateTime.MinValue, (object) "");
    if (this._newObjectIOSettings == null)
      this._newObjectIOSettings = new List<ObjectIOSettings>();
    this._newObjectIOSettings.Add(new ObjectIOSettings());
  }

  /// <summary>Получить общие для указанных пользователей роли</summary>
  /// <param name="userIds">Ид пользователей</param>
  /// <returns>Общие роли</returns>
  private List<RoleProperties> GetCommonUsersRoles(List<long> userIds)
  {
    List<RoleProperties> list = ((IEnumerable<RoleProperties>) this._rolesService.GetRolesList(userIds[0])).ToList<RoleProperties>();
    for (int index = 1; index < userIds.Count; ++index)
    {
      RoleProperties[] rolesList = this._rolesService.GetRolesList(userIds[index]);
      list = list.Intersect<RoleProperties>((IEnumerable<RoleProperties>) rolesList, (IEqualityComparer<RoleProperties>) new RolePropertiesComparer()).ToList<RoleProperties>();
    }
    return list;
  }

  /// <summary>Проверка данных перед сохранением</summary>
  /// <param name="newObjectIoSetting"></param>
  /// <returns></returns>
  private bool DataValidation(ObjectIOSettings newObjectIoSetting)
  {
    if (newObjectIoSetting.IoList.Count == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1652"), LocalizationHolder.rm.GetString("Client.Core_1650"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.dataGridView1.ClearSelection();
      for (int rowIndex = 0; rowIndex < this.dataGridView1.RowCount; ++rowIndex)
      {
        if (this.dataGridView1[0, rowIndex].Value.ToString() == newObjectIoSetting.ID.ToString())
        {
          this.dataGridView1.CurrentCell = this.dataGridView1[1, rowIndex];
          this.dataGridView1[1, rowIndex].Selected = true;
        }
      }
      return false;
    }
    if (!(newObjectIoSetting.BeginDate != "") || !(newObjectIoSetting.EndDate != "") || !(Convert.ToDateTime(newObjectIoSetting.BeginDate) > Convert.ToDateTime(newObjectIoSetting.EndDate)))
      return true;
    int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1653"), LocalizationHolder.rm.GetString("Client.Core_1650"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    newObjectIoSetting.BeginDate = "";
    newObjectIoSetting.EndDate = "";
    this.dataGridView1.ClearSelection();
    for (int rowIndex = 0; rowIndex < this.dataGridView1.RowCount; ++rowIndex)
    {
      if (this.dataGridView1[0, rowIndex].Value.ToString() == newObjectIoSetting.ID.ToString())
      {
        this.dataGridView1.CurrentCell = this.dataGridView1[2, rowIndex];
        this.dataGridView1[2, rowIndex].Selected = true;
        this.dataGridView1[2, rowIndex].Value = (object) DateTime.MinValue;
        this.dataGridView1[3, rowIndex].Value = (object) DateTime.MinValue;
      }
    }
    return false;
  }

  /// <summary>Сохранение объекта настройки исполнения</summary>
  /// <param name="objectIoSettings"> Объект настройки</param>
  private void Savedata()
  {
    if (this._newObjectIOSettings == null)
      return;
    List<ObjectIOSettings> ioSettings = new List<ObjectIOSettings>();
    foreach (ObjectIOSettings newObjectIoSetting in this._newObjectIOSettings)
    {
      if (newObjectIoSetting.IsValid() && newObjectIoSetting.Reload)
        ioSettings.Add(newObjectIoSetting);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._userSubstituteService.SaveIoSettings(sessionKeeper.Session.SessionGUID, ioSettings, this._userId);
  }

  private void OnTextWithButtonColumn_ButtonClick(object sender, EventArgs e)
  {
    DataGridViewCellCollection cells = this.dataGridView1.CurrentRow.Cells;
    ObjectIOSettings objectIoSettings = this._newObjectIOSettings.Find((Predicate<ObjectIOSettings>) (q => q.ID == Convert.ToInt64(cells[this.cID.Index].Value)));
    Dictionary<long, string> dictionary = AddOrRemoveUsersForm.Execute(objectIoSettings.IoList, this._userId);
    if (dictionary == null || dictionary.Count <= 0)
      return;
    this.dataGridView1.BeginEdit(true);
    objectIoSettings.IoList.Clear();
    objectIoSettings.Reload = true;
    foreach (KeyValuePair<long, string> keyValuePair in dictionary)
      objectIoSettings.IoList.Add(new MyElement((object) keyValuePair.Key, keyValuePair.Value, (object) null));
    (this.dataGridView1.Columns[this.cOfficiate.Index] as DataGridViewTextWithButtonColumn).TextReadOnly = false;
    cells[this.cOfficiate.Index].Value = (object) objectIoSettings.IOCaptions();
    (this.dataGridView1.Columns[this.cOfficiate.Index] as DataGridViewTextWithButtonColumn).TextReadOnly = true;
    DataGridViewComboBoxCell viewComboBoxCell = new DataGridViewComboBoxCell();
    viewComboBoxCell.Items.Add((object) "");
    List<RoleProperties> commonUsersRoles = this.GetCommonUsersRoles(objectIoSettings.IoList.Select<MyElement, long>((Func<MyElement, long>) (x => (long) x.Value)).ToList<long>());
    List<string> list = commonUsersRoles.Select<RoleProperties, string>((Func<RoleProperties, string>) (x => x.RoleName)).ToList<string>();
    viewComboBoxCell.Items.AddRange((object[]) list.ToArray());
    objectIoSettings.CommonUsersRoles.Clear();
    objectIoSettings.CommonUsersRoles = commonUsersRoles;
    if (objectIoSettings.CurrentRole.RoleID != 0L)
    {
      string roleName = objectIoSettings.CurrentRole.RoleName;
      if (viewComboBoxCell.Items.Contains((object) roleName))
      {
        viewComboBoxCell.Value = (object) roleName;
      }
      else
      {
        viewComboBoxCell.Value = (object) "";
        objectIoSettings.CurrentRole.RoleID = 0L;
      }
    }
    else
      viewComboBoxCell.Value = (object) "";
    cells[this.cRole.Index] = (DataGridViewCell) viewComboBoxCell;
    this.dataGridView1.EndEdit();
    if (this._newObjectIOSettings.Find((Predicate<ObjectIOSettings>) (q => q.IoList.Count <= 0)) != null)
      return;
    this.btnCreate.Enabled = true;
    this.btnCancel.Enabled = true;
  }

  private void OnTextWithButtonColumn_KeyDown(object sender, EventArgs e)
  {
    ObjectIOSettings objectIoSettings = this._newObjectIOSettings.Find((Predicate<ObjectIOSettings>) (q => q.ID.ToString() == this.dataGridView1.CurrentRow.Cells[this.cID.Index].Value.ToString()));
    objectIoSettings.IoList.Clear();
    objectIoSettings.Reload = true;
    this.btnCreate.Enabled = false;
    this.btnCancel.Enabled = true;
  }

  private void OnCalendaColumn_BeginData_ClouseUp(object sender, EventArgs e)
  {
    long IOId = Convert.ToInt64(this.dataGridView1.CurrentRow.Cells[this.cID.Index].Value);
    ObjectIOSettings objectIoSettings = this._newObjectIOSettings.Find((Predicate<ObjectIOSettings>) (q => q.ID == IOId));
    objectIoSettings.BeginDate = Convert.ToDateTime(this.dataGridView1.CurrentRow.Cells[this.cBeginData.Index].EditedFormattedValue).Date.ToString();
    objectIoSettings.Reload = true;
    this.btnCancel.Enabled = true;
    this.btnCreate.Enabled = true;
  }

  private void OnCalendaColumn_EndData_ClouseUp(object sender, EventArgs e)
  {
    long ioId = Convert.ToInt64(this.dataGridView1.CurrentRow.Cells[this.cID.Index].Value);
    ObjectIOSettings objectIoSettings = this._newObjectIOSettings.Find((Predicate<ObjectIOSettings>) (q => q.ID == ioId));
    objectIoSettings.EndDate = Convert.ToDateTime(this.dataGridView1.CurrentRow.Cells[this.cEndData.Index].EditedFormattedValue).Date.ToString();
    objectIoSettings.Reload = true;
    this.btnCancel.Enabled = true;
    this.btnCreate.Enabled = true;
  }

  private void tsmiAddRow_Click(object sender, EventArgs e)
  {
    this.dataGridView1.Rows.Add();
    this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[this.cBeginData.Index].Value = (object) DateTime.MinValue;
    this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[this.cEndData.Index].Value = (object) DateTime.MinValue;
    this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[this.cID.Index].Value = (object) (-1 * this.dataGridView1.Rows.Count);
    this._newObjectIOSettings.Add(new ObjectIOSettings());
    this._newObjectIOSettings.Last<ObjectIOSettings>().ID = (long) (-1 * this.dataGridView1.Rows.Count);
    this.btnCreate.Enabled = false;
    this.btnCancel.Enabled = true;
  }

  private void tsmiDeleteRow_Click(object sender, EventArgs e)
  {
    if (this.dataGridView1.CurrentRow != null)
    {
      if (this._oldObjectIoSettings != null)
      {
        ObjectIOSettings objectIoSettings1 = this._oldObjectIoSettings.Find((Predicate<ObjectIOSettings>) (q => q.ID.ToString() == this.dataGridView1.CurrentRow.Cells[this.cID.Index].Value.ToString()));
        if (objectIoSettings1 != null)
        {
          this._newObjectIOSettings.Remove(objectIoSettings1);
          if (this._deleteObjectIoSettings == null)
            this._deleteObjectIoSettings = new List<ObjectIOSettings>();
          this._deleteObjectIoSettings.Add(objectIoSettings1);
        }
        else
        {
          ObjectIOSettings objectIoSettings2 = this._newObjectIOSettings.Find((Predicate<ObjectIOSettings>) (q => q.ID.ToString() == this.dataGridView1.CurrentRow.Cells[this.cID.Index].Value.ToString()));
          this._newObjectIOSettings.Remove(objectIoSettings2);
          if (this._deleteObjectIoSettings == null)
            this._deleteObjectIoSettings = new List<ObjectIOSettings>();
          this._deleteObjectIoSettings.Add(objectIoSettings2);
        }
      }
      else
        this._newObjectIOSettings.Remove(this._newObjectIOSettings.Find((Predicate<ObjectIOSettings>) (q => q.ID.ToString() == this.dataGridView1.CurrentRow.Cells[this.cID.Index].Value.ToString())));
      if (this._newObjectIOSettings.Find((Predicate<ObjectIOSettings>) (q => q.IoList.Count <= 0)) == null)
        this.btnCreate.Enabled = true;
      this.btnCancel.Enabled = true;
      this.dataGridView1.Rows.RemoveAt(this.dataGridView1.CurrentRow.Index);
    }
    else
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1642"), LocalizationHolder.rm.GetString("Client.Core_1650"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  private void btnCreate_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this._newObjectIOSettings != null)
      {
        foreach (ObjectIOSettings newObjectIoSetting in this._newObjectIOSettings)
        {
          if (newObjectIoSetting.Reload)
          {
            if (!this.DataValidation(newObjectIoSetting))
            {
              this.btnCreate.Enabled = false;
              return;
            }
            long id = newObjectIoSetting.ID;
            IUserSubstituteService substituteService = this._userSubstituteService;
            Guid sessionGuid = sessionKeeper.Session.SessionGUID;
            List<ObjectIOSettings> ioSettings = new List<ObjectIOSettings>();
            ioSettings.Add(newObjectIoSetting);
            long userId = this._userId;
            long saveIoSetting = substituteService.SaveIoSettings(sessionGuid, ioSettings, userId)[0];
            foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
            {
              if (Convert.ToInt64(row.Cells[this.cID.Index].Value) == id)
                row.Cells[this.cID.Index].Value = (object) saveIoSetting;
            }
            newObjectIoSetting.ID = saveIoSetting;
            newObjectIoSetting.Reload = false;
          }
        }
      }
      if (this._deleteObjectIoSettings != null)
      {
        foreach (ObjectIOSettings deleteObjectIoSetting in this._deleteObjectIoSettings)
          sessionKeeper.Session.GetObject(deleteObjectIoSetting.ID, false)?.Delete(0L);
        this._deleteObjectIoSettings.Clear();
      }
    }
    this.dataGridView1.Update();
    this.btnCreate.Enabled = false;
    this.btnCancel.Enabled = false;
  }

  private void dataGridView1_EditingControlShowing(
    object sender,
    DataGridViewEditingControlShowingEventArgs e)
  {
    if ((this.dataGridView1.CurrentCell.ColumnIndex == this.cBeginData.Index || this.dataGridView1.CurrentCell.ColumnIndex == this.cEndData.Index) && e.Control is DateTimePicker control1)
    {
      control1.KeyDown -= new KeyEventHandler(this.innerCalendar_KeyDown);
      control1.KeyDown += new KeyEventHandler(this.innerCalendar_KeyDown);
    }
    if (this.dataGridView1.CurrentCell.ColumnIndex != this.cRole.Index || !(e.Control is ComboBox control2))
      return;
    control2.SelectedIndexChanged += new EventHandler(this.comboBox_SelectedIndexChanged);
  }

  private void innerCalendar_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete)
      return;
    this.dataGridView1.BeginEdit(true);
    this.dataGridView1.CurrentCell.Value = (object) DateTime.MinValue;
    this.dataGridView1.EndEdit();
    ObjectIOSettings objectIoSettings = this._newObjectIOSettings.Find((Predicate<ObjectIOSettings>) (q => q.ID.ToString() == this.dataGridView1.CurrentRow.Cells[this.cID.Index].Value.ToString()));
    if (this.dataGridView1.CurrentCell.ColumnIndex == this.cBeginData.Index)
      objectIoSettings.BeginDate = "";
    else
      objectIoSettings.EndDate = "";
    objectIoSettings.Reload = true;
    this.btnCreate.Enabled = true;
    this.btnCancel.Enabled = true;
  }

  private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    ComboBox comboBox = (ComboBox) sender;
    ObjectIOSettings objectIoSettings = this._newObjectIOSettings.Find((Predicate<ObjectIOSettings>) (q => q.ID.ToString() == this.dataGridView1.CurrentRow.Cells[this.cID.Index].Value.ToString()));
    if (objectIoSettings == null)
      return;
    if ((string) comboBox.SelectedItem != "")
    {
      if (objectIoSettings.CurrentRole.RoleID == objectIoSettings.CommonUsersRoles.First<RoleProperties>((Func<RoleProperties, bool>) (q => q.RoleName == (string) comboBox.SelectedItem)).RoleID)
        return;
      objectIoSettings.CurrentRole.RoleID = objectIoSettings.CommonUsersRoles.First<RoleProperties>((Func<RoleProperties, bool>) (q => q.RoleName == (string) comboBox.SelectedItem)).RoleID;
      objectIoSettings.Reload = true;
      this.btnCreate.Enabled = true;
      this.btnCancel.Enabled = true;
    }
    else
    {
      if (objectIoSettings.CurrentRole.RoleID == 0L)
        return;
      objectIoSettings.CurrentRole.RoleID = 0L;
      objectIoSettings.Reload = true;
      this.btnCreate.Enabled = true;
      this.btnCancel.Enabled = true;
    }
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    if (this.dataGridView1.CurrentRow != null)
    {
      this.dataGridView1.BeginEdit(true);
      this.dataGridView1.Rows.Clear();
      this.dataGridView1.EndEdit();
    }
    if (this._newObjectIOSettings != null)
      this._newObjectIOSettings.Clear();
    if (this._oldObjectIoSettings != null)
      this._oldObjectIoSettings.Clear();
    if (this._deleteObjectIoSettings != null)
      this._deleteObjectIoSettings.Clear();
    using (SessionKeeper keeper = new SessionKeeper())
      this.LoadWhomUserOfficiates(keeper);
    this.btnCreate.Enabled = false;
    this.btnCancel.Enabled = false;
  }

  private void tabControl1_Selected(object sender, TabControlEventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (e.TabPageIndex == 1)
      {
        if (this._isLoadedWhoOfficiatesUser)
          return;
        this.LoadWhoOfficiatesUser(sessionKeeper);
      }
      else
      {
        if (e.TabPageIndex != 0 || this._isLoadedWhomUserOfficiates)
          return;
        this.LoadWhomUserOfficiates(sessionKeeper);
      }
    }
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
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.tsmiAddRow = new ToolStripMenuItem();
    this.tsmiDeleteRow = new ToolStripMenuItem();
    this.panel1 = new Panel();
    this.lblFired = new Label();
    this.btnCancel = new Button();
    this.btnCreate = new Button();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextWithButtonColumn1 = new DataGridViewTextWithButtonColumn();
    this.dataGridViewCalendarColumn1 = new DataGridViewCalendarColumn();
    this.dataGridViewCalendarColumn2 = new DataGridViewCalendarColumn();
    this.tabControl1 = new TabControl();
    this.tabPage1 = new TabPage();
    this.dataGridView1 = new DataGridView();
    this.cID = new DataGridViewTextBoxColumn();
    this.cOfficiate = new DataGridViewTextWithButtonColumn();
    this.cBeginData = new DataGridViewCalendarColumn();
    this.cEndData = new DataGridViewCalendarColumn();
    this.cRole = new DataGridViewComboBoxColumn();
    this.tabPage2 = new TabPage();
    this.dataGridView2 = new DataGridView();
    this.cID2 = new DataGridViewTextBoxColumn();
    this.cOfficiety2 = new DataGridViewTextBoxColumn();
    this.cBeginData2 = new DataGridViewCalendarColumn();
    this.cEndData2 = new DataGridViewCalendarColumn();
    this.cRole2 = new DataGridViewTextBoxColumn();
    this.contextMenuStrip1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.tabControl1.SuspendLayout();
    this.tabPage1.SuspendLayout();
    ((ISupportInitialize) this.dataGridView1).BeginInit();
    this.tabPage2.SuspendLayout();
    ((ISupportInitialize) this.dataGridView2).BeginInit();
    this.SuspendLayout();
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiAddRow,
      (ToolStripItem) this.tsmiDeleteRow
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.Size = new Size((int) sbyte.MaxValue, 48 /*0x30*/);
    this.tsmiAddRow.Name = "tsmiAddRow";
    this.tsmiAddRow.Size = new Size(126, 22);
    this.tsmiAddRow.Text = "Добавить";
    this.tsmiAddRow.Click += new EventHandler(this.tsmiAddRow_Click);
    this.tsmiDeleteRow.Name = "tsmiDeleteRow";
    this.tsmiDeleteRow.Size = new Size(126, 22);
    this.tsmiDeleteRow.Text = "Удалить";
    this.tsmiDeleteRow.Click += new EventHandler(this.tsmiDeleteRow_Click);
    this.panel1.Controls.Add((Control) this.lblFired);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Controls.Add((Control) this.btnCreate);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 352);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(721, 56);
    this.panel1.TabIndex = 2;
    this.lblFired.AutoSize = true;
    this.lblFired.Location = new Point(15, 16 /*0x10*/);
    this.lblFired.Name = "lblFired";
    this.lblFired.Size = new Size(324, 13);
    this.lblFired.TabIndex = 2;
    this.lblFired.Text = "Пользователь уволен.  Редактирование настроек запрещено.";
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.Location = new Point(587, 16 /*0x10*/);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCreate.Location = new Point(460, 16 /*0x10*/);
    this.btnCreate.Name = "btnCreate";
    this.btnCreate.Size = new Size(121, 27);
    this.btnCreate.TabIndex = 0;
    this.btnCreate.Text = "Применить";
    this.btnCreate.UseVisualStyleBackColor = true;
    this.btnCreate.Click += new EventHandler(this.btnCreate_Click);
    this.dataGridViewTextBoxColumn1.FillWeight = 12.69035f;
    this.dataGridViewTextBoxColumn1.HeaderText = "Идентификатор";
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.Visible = false;
    this.dataGridViewTextWithButtonColumn1.FillWeight = 121.8274f;
    this.dataGridViewTextWithButtonColumn1.HeaderText = "Исполняет обязанности";
    this.dataGridViewTextWithButtonColumn1.Name = "dataGridViewTextWithButtonColumn1";
    this.dataGridViewTextWithButtonColumn1.Resizable = DataGridViewTriState.True;
    this.dataGridViewTextWithButtonColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
    this.dataGridViewTextWithButtonColumn1.TextReadOnly = false;
    this.dataGridViewTextWithButtonColumn1.Width = 180;
    this.dataGridViewCalendarColumn1.FillWeight = 121.8274f;
    this.dataGridViewCalendarColumn1.HeaderText = "Начальная дата";
    this.dataGridViewCalendarColumn1.Name = "dataGridViewCalendarColumn1";
    this.dataGridViewCalendarColumn1.Width = 179;
    this.dataGridViewCalendarColumn2.FillWeight = 121.8274f;
    this.dataGridViewCalendarColumn2.HeaderText = "Конечная дата";
    this.dataGridViewCalendarColumn2.Name = "dataGridViewCalendarColumn2";
    this.dataGridViewCalendarColumn2.Width = 180;
    this.tabControl1.Controls.Add((Control) this.tabPage1);
    this.tabControl1.Controls.Add((Control) this.tabPage2);
    this.tabControl1.Dock = DockStyle.Fill;
    this.tabControl1.Location = new Point(0, 0);
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    this.tabControl1.Size = new Size(721, 352);
    this.tabControl1.TabIndex = 3;
    this.tabControl1.Selected += new TabControlEventHandler(this.tabControl1_Selected);
    this.tabPage1.Controls.Add((Control) this.dataGridView1);
    this.tabPage1.Location = new Point(4, 22);
    this.tabPage1.Name = "tabPage1";
    this.tabPage1.Padding = new Padding(3);
    this.tabPage1.Size = new Size(713, 326);
    this.tabPage1.TabIndex = 0;
    this.tabPage1.Text = "Чьи обязанности исполняет пользователь";
    this.tabPage1.UseVisualStyleBackColor = true;
    this.dataGridView1.AllowUserToAddRows = false;
    this.dataGridView1.AllowUserToDeleteRows = false;
    this.dataGridView1.AllowUserToResizeColumns = false;
    this.dataGridView1.AllowUserToResizeRows = false;
    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.cID, (DataGridViewColumn) this.cOfficiate, (DataGridViewColumn) this.cBeginData, (DataGridViewColumn) this.cEndData, (DataGridViewColumn) this.cRole);
    this.dataGridView1.Dock = DockStyle.Fill;
    this.dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;
    this.dataGridView1.Location = new Point(3, 3);
    this.dataGridView1.Name = "dataGridView1";
    this.dataGridView1.ReadOnly = true;
    this.dataGridView1.RowHeadersVisible = false;
    this.dataGridView1.Size = new Size(707, 320);
    this.dataGridView1.TabIndex = 1;
    this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
    this.cID.FillWeight = 12.69035f;
    this.cID.HeaderText = "Идентификатор";
    this.cID.Name = "cID";
    this.cID.ReadOnly = true;
    this.cID.Visible = false;
    this.cOfficiate.FillWeight = 121.8274f;
    this.cOfficiate.HeaderText = "Исполняет обязанности";
    this.cOfficiate.Name = "cOfficiate";
    this.cOfficiate.ReadOnly = true;
    this.cOfficiate.Resizable = DataGridViewTriState.True;
    this.cOfficiate.SortMode = DataGridViewColumnSortMode.Automatic;
    this.cOfficiate.TextReadOnly = false;
    this.cBeginData.FillWeight = 121.8274f;
    this.cBeginData.HeaderText = "Начальная дата";
    this.cBeginData.Name = "cBeginData";
    this.cBeginData.ReadOnly = true;
    this.cBeginData.SortMode = DataGridViewColumnSortMode.Automatic;
    this.cEndData.FillWeight = 121.8274f;
    this.cEndData.HeaderText = "Конечная дата";
    this.cEndData.Name = "cEndData";
    this.cEndData.ReadOnly = true;
    this.cEndData.SortMode = DataGridViewColumnSortMode.Automatic;
    this.cRole.FillWeight = 121.8274f;
    this.cRole.HeaderText = "Роль";
    this.cRole.Name = "cRole";
    this.cRole.ReadOnly = true;
    this.cRole.SortMode = DataGridViewColumnSortMode.Automatic;
    this.tabPage2.Controls.Add((Control) this.dataGridView2);
    this.tabPage2.Location = new Point(4, 22);
    this.tabPage2.Name = "tabPage2";
    this.tabPage2.Padding = new Padding(3);
    this.tabPage2.Size = new Size(713, 326);
    this.tabPage2.TabIndex = 1;
    this.tabPage2.Text = "Кто исполняет обязанности пользователя";
    this.tabPage2.UseVisualStyleBackColor = true;
    this.dataGridView2.AllowUserToAddRows = false;
    this.dataGridView2.AllowUserToDeleteRows = false;
    this.dataGridView2.AllowUserToResizeRows = false;
    this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView2.Columns.AddRange((DataGridViewColumn) this.cID2, (DataGridViewColumn) this.cOfficiety2, (DataGridViewColumn) this.cBeginData2, (DataGridViewColumn) this.cEndData2, (DataGridViewColumn) this.cRole2);
    this.dataGridView2.Dock = DockStyle.Fill;
    this.dataGridView2.EditMode = DataGridViewEditMode.EditOnKeystroke;
    this.dataGridView2.Location = new Point(3, 3);
    this.dataGridView2.Name = "dataGridView2";
    this.dataGridView2.ReadOnly = true;
    this.dataGridView2.RowHeadersVisible = false;
    this.dataGridView2.Size = new Size(707, 320);
    this.dataGridView2.TabIndex = 2;
    this.cID2.FillWeight = 12.69035f;
    this.cID2.HeaderText = "Идентификатор";
    this.cID2.Name = "cID2";
    this.cID2.ReadOnly = true;
    this.cID2.Visible = false;
    this.cOfficiety2.FillWeight = 121.8274f;
    this.cOfficiety2.HeaderText = "Исполняет обязанности";
    this.cOfficiety2.Name = "cOfficiety2";
    this.cOfficiety2.ReadOnly = true;
    this.cOfficiety2.Resizable = DataGridViewTriState.True;
    this.cBeginData2.FillWeight = 121.8274f;
    this.cBeginData2.HeaderText = "Начальная дата";
    this.cBeginData2.Name = "cBeginData2";
    this.cBeginData2.ReadOnly = true;
    this.cBeginData2.SortMode = DataGridViewColumnSortMode.Automatic;
    this.cEndData2.FillWeight = 121.8274f;
    this.cEndData2.HeaderText = "Конечная дата";
    this.cEndData2.Name = "cEndData2";
    this.cEndData2.ReadOnly = true;
    this.cEndData2.SortMode = DataGridViewColumnSortMode.Automatic;
    this.cRole2.FillWeight = 121.8274f;
    this.cRole2.HeaderText = "Роль";
    this.cRole2.Name = "cRole2";
    this.cRole2.ReadOnly = true;
    this.cRole2.Resizable = DataGridViewTriState.True;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tabControl1);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (PerformanceOfDuties);
    this.Size = new Size(721, 408);
    this.contextMenuStrip1.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.tabControl1.ResumeLayout(false);
    this.tabPage1.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView1).EndInit();
    this.tabPage2.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView2).EndInit();
    this.ResumeLayout(false);
  }

  private sealed class PerformanceOfDutiesViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core_1651"),
        ImageIndex = -1,
        OrderID = 17
      };
    }
  }
}
