
// Type: Intermech.Navigator.SelectionListWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>
/// Форма, позволяющая управлять списком элементов, например, списком типов объектов
/// </summary>
public class SelectionListWindow : Form
{
  /// <summary>Редактируемый список элементов</summary>
  private List<int> objectTypes;
  /// <summary>Контейнер сервисов</summary>
  private System.IServiceProvider services;
  /// <summary>Если true, то идёт работа внутри обработчика событий</summary>
  private bool inEvent;
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService objtypesIcons;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelLeft;
  private Button btnRemove;
  private Button btnAdd;
  private Panel panel3;
  private Button btnCancel;
  private Button btnOK;
  private ListView lvObjectTypes;
  private ColumnHeader colName;

  /// <summary>Создавать экземпляр класса</summary>
  public SelectionListWindow() => this.InitializeComponent();

  /// <summary>Создавать экземпляр класса</summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="objTypes">Редактируемый список типов объектов</param>
  public SelectionListWindow(System.IServiceProvider services, List<int> objTypes)
    : this()
  {
    this.services = services;
    this.objectTypes = objTypes != null ? new List<int>((IEnumerable<int>) objTypes) : new List<int>(8);
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Size = new Size(primaryWorkingArea.Width / 100 * 70, primaryWorkingArea.Height / 100 * 60);
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this.UpdateControls();
  }

  /// <summary>Вызвать форму для управления списком элементов</summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="objTypes">Редактируемый список типов объектов</param>
  /// <returns>Результат вызова формы как модального окна</returns>
  [STAThread]
  public static DialogResult Execute(System.IServiceProvider services, List<int> objTypes)
  {
    using (SelectionListWindow selectionListWindow = new SelectionListWindow(services, objTypes))
    {
      DialogResult dialogResult = selectionListWindow.ShowDialog();
      if (dialogResult != DialogResult.OK)
        return dialogResult;
      objTypes.Clear();
      objTypes.AddRange((IEnumerable<int>) selectionListWindow.CaptureChanges());
      return dialogResult;
    }
  }

  /// <summary>Инициализировать сервисы</summary>
  private void InitServices()
  {
    this.objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
  }

  /// <summary>Установить статус всех контролов формы</summary>
  internal void UpdateControls()
  {
    bool flag = this.lvObjectTypes.Items.Count > 0 && this.lvObjectTypes.SelectedItems.Count > 0;
    this.btnAdd.Enabled = true;
    this.btnRemove.Enabled = flag;
    this.btnOK.Enabled = true;
    this.btnCancel.Enabled = true;
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void SelectionListWindow_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void SelectionListWindow_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Изменился размер клиентской области списка</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void lvObjectTypes_Resize(object sender, EventArgs e)
  {
    this.lvObjectTypes.Columns[0].Width = this.lvObjectTypes.ClientSize.Width - 30;
  }

  /// <summary>Изменилась выделенная запись в списке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void lvObjectTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.inEvent)
      return;
    this.UpdateControls();
  }

  /// <summary>Форма отображается первый раз</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SelectionListWindow_Shown(object sender, EventArgs e)
  {
    this.InitServices();
    this.FillList(this.objectTypes);
  }

  /// <summary>Нажата кнопка "Добавить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnAdd_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), this.Text, typeof (ObjectTypeFolder), true);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    this.objectTypes = this.CaptureChanges();
    try
    {
      this.lvObjectTypes.BeginUpdate();
      for (int index = 0; index < selectorForm.IDList.Count; ++index)
      {
        int id = (int) selectorForm.IDList[index];
        if (this.objectTypes.IndexOf(id) < 0)
        {
          ListViewItem listItem = this.CreateListItem(MetaDataHelper.GetObjectType(id));
          if (listItem != null)
          {
            this.lvObjectTypes.Items.Add(listItem);
            this.objectTypes.Add(id);
          }
        }
      }
    }
    finally
    {
      this.lvObjectTypes.EndUpdate();
      this.UpdateControls();
    }
  }

  /// <summary>Нажата кнопка "Удалить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnRemove_Click(object sender, EventArgs e)
  {
    if (this.lvObjectTypes.SelectedItems.Count == 0)
      return;
    int selectedIndex = this.lvObjectTypes.SelectedIndices[0];
    try
    {
      this.lvObjectTypes.BeginUpdate();
      this.lvObjectTypes.Items.RemoveAt(selectedIndex);
      if (this.lvObjectTypes.Items.Count > 0)
      {
        if (selectedIndex > 0 && selectedIndex == this.lvObjectTypes.Items.Count)
          --selectedIndex;
        this.lvObjectTypes.Items[selectedIndex].Selected = true;
      }
      this.lvObjectTypes.Focus();
    }
    finally
    {
      this.lvObjectTypes.EndUpdate();
      this.UpdateControls();
    }
  }

  /// <summary>Двойной клик в списке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void lvObjectTypes_DoubleClick(object sender, EventArgs e)
  {
    this.UpdateControls();
    if (!this.btnRemove.Enabled)
      return;
    this.btnRemove_Click(sender, e);
  }

  /// <summary>Отпущена клавиша</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void SelectionListWindow_KeyUp(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Insert && e.KeyCode != Keys.Delete)
      return;
    this.UpdateControls();
    if (e.KeyCode == Keys.Insert && this.btnAdd.Enabled)
    {
      this.btnAdd_Click(sender, (EventArgs) e);
      e.Handled = true;
    }
    else
    {
      if (e.KeyCode != Keys.Delete || !this.btnRemove.Enabled)
        return;
      this.btnRemove_Click(sender, (EventArgs) e);
      e.Handled = true;
    }
  }

  /// <summary>Добавить запись в список для указанного типа объекта</summary>
  /// <param name="objType">Описание типа объекта</param>
  /// <returns>Запись для списка или null</returns>
  private ListViewItem CreateListItem(IMSObjectType objType)
  {
    if (objType == null)
      return (ListViewItem) null;
    return new ListViewItem(objType.ObjectTypeName)
    {
      Tag = (object) objType,
      ImageIndex = this.objtypesIcons != null ? this.objtypesIcons.IndexOf(4, objType.ObjectTypeID) : -1
    };
  }

  /// <summary>Заполнить список информацией</summary>
  /// <param name="objectTypes">Идентификаторы типов объектов</param>
  private void FillList(List<int> objectTypes)
  {
    try
    {
      this.lvObjectTypes.BeginUpdate();
      this.inEvent = true;
      this.lvObjectTypes.Items.Clear();
      this.lvObjectTypes.SmallImageList = this.objtypesIcons != null ? this.objtypesIcons.ImageList : (ImageList) null;
      if (objectTypes != null)
      {
        objectTypes.Sort((IComparer<int>) new SelectionListWindow.SortObjectTypes());
        for (int index = 0; index < objectTypes.Count; ++index)
        {
          ListViewItem listItem = this.CreateListItem(MetaDataHelper.GetObjectType(objectTypes[index]));
          if (listItem != null)
            this.lvObjectTypes.Items.Add(listItem);
        }
      }
      if (this.lvObjectTypes.Items.Count <= 0)
        return;
      this.lvObjectTypes.Items[0].Selected = true;
    }
    finally
    {
      this.lvObjectTypes.EndUpdate();
      this.inEvent = false;
      this.UpdateControls();
    }
  }

  /// <summary>Собрать список типов объектов из списка</summary>
  /// <returns>Список-результат</returns>
  private List<int> CaptureChanges()
  {
    List<int> intList = new List<int>(this.lvObjectTypes.Items.Count);
    for (int index = 0; index < this.lvObjectTypes.Items.Count; ++index)
    {
      if (this.lvObjectTypes.Items[index].Tag is IMSObjectType tag && intList.IndexOf(tag.ObjectTypeID) < 0)
        intList.Add(tag.ObjectTypeID);
    }
    intList.Sort((IComparer<int>) new SelectionListWindow.SortObjectTypes());
    return intList;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectionListWindow));
    this.panelLeft = new Panel();
    this.btnRemove = new Button();
    this.btnAdd = new Button();
    this.panel3 = new Panel();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.lvObjectTypes = new ListView();
    this.colName = new ColumnHeader();
    this.panelLeft.SuspendLayout();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    this.panelLeft.Controls.Add((Control) this.btnRemove);
    this.panelLeft.Controls.Add((Control) this.btnAdd);
    componentResourceManager.ApplyResources((object) this.panelLeft, "panelLeft");
    this.panelLeft.Name = "panelLeft";
    componentResourceManager.ApplyResources((object) this.btnRemove, "btnRemove");
    this.btnRemove.Name = "btnRemove";
    this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.panel3.Controls.Add((Control) this.btnCancel);
    this.panel3.Controls.Add((Control) this.btnOK);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Hand;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Cursor = Cursors.Hand;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.lvObjectTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.colName
    });
    componentResourceManager.ApplyResources((object) this.lvObjectTypes, "lvObjectTypes");
    this.lvObjectTypes.FullRowSelect = true;
    this.lvObjectTypes.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvObjectTypes.HideSelection = false;
    this.lvObjectTypes.MultiSelect = false;
    this.lvObjectTypes.Name = "lvObjectTypes";
    this.lvObjectTypes.UseCompatibleStateImageBehavior = false;
    this.lvObjectTypes.View = View.Details;
    this.lvObjectTypes.SelectedIndexChanged += new EventHandler(this.lvObjectTypes_SelectedIndexChanged);
    this.lvObjectTypes.DoubleClick += new EventHandler(this.lvObjectTypes_DoubleClick);
    this.lvObjectTypes.Resize += new EventHandler(this.lvObjectTypes_Resize);
    componentResourceManager.ApplyResources((object) this.colName, "colName");
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.lvObjectTypes);
    this.Controls.Add((Control) this.panelLeft);
    this.Controls.Add((Control) this.panel3);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectionListWindow);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.SelectionListWindow_FormClosed);
    this.Load += new EventHandler(this.SelectionListWindow_Load);
    this.Shown += new EventHandler(this.SelectionListWindow_Shown);
    this.KeyUp += new KeyEventHandler(this.SelectionListWindow_KeyUp);
    this.panelLeft.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Класс, позволяющий отсортировать список типов объектов по их уровням вложенности и названиям
  /// </summary>
  private class SortObjectTypes : IComparer<int>
  {
    /// <summary>Сравнить два типа объектов</summary>
    /// <param name="x">Первый тип объекта</param>
    /// <param name="y">Второй тип объекта</param>
    /// <returns>-1, 0, 1</returns>
    public int Compare(int x, int y)
    {
      return string.Compare(MetaDataHelper.GetObjectTypeFullName(x), MetaDataHelper.GetObjectTypeFullName(y), StringComparison.CurrentCultureIgnoreCase);
    }
  }
}
