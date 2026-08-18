// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ColumnsAutoSettingsView
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Админская вкладка для архивов с настройками колонок на роли
/// Выделена может быть только одна роль, иначе непонятно, что показывать в колонках
/// </summary>
[ViewDescriptionProvider(typeof (ColumnsAutoSettingsView.ColumnsAutoSettingsViewDescriptionProvider))]
public class ColumnsAutoSettingsView : UserControl, IView
{
  private int _imageIndex = -1;
  private long _archiveID;
  private bool _isFirstTimeOpened;
  private bool _isModified;
  private IArchiveColumnsSettingsCacheService _archiveColumnsSettingsCache;
  /// <summary>Модель данных, с которой работаем</summary>
  private List<RolesColumnsSettings> _rolesColumnsSettingsModel;
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService _attrTypesIcons;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel pnlButtom;
  private Panel _buttons;
  private Button btnCancel;
  private Button btnApply;
  private SplitContainer splitContainer;
  private ListView lvRoles;
  private ColumnHeader roles;
  private Intermech.Bars.ToolBar toolBarRoles;
  private ButtonItem btnAddRole;
  private ButtonItem btnDeleteRole;
  private ListView lvColumns;
  private ColumnHeader columns;
  private Intermech.Bars.ToolBar toolBarForUsers;
  private ButtonItem btnColumnSettings;
  private ButtonItem btnAddDefaultRole;

  private event EventHandler OnModified;

  public ColumnsAutoSettingsView() => this.InitializeComponent();

  /// <summary>
  /// Выполняет инициализацию закладки после ее создания. Реализация
  /// этого метода должна работать быстро, т.е. все длительные операции
  /// желательно выполнять при первом вызове метода Activate.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service)
      this._imageIndex = service.ImageIndex("imgViewSettings");
    this._archiveID = items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData ? itemData.Value : throw new Exception(string.Format(ServiceHolder.rm.GetString("Archives_159")));
    this._isFirstTimeOpened = true;
    this._archiveColumnsSettingsCache = ApplicationServices.Container.GetService(typeof (IArchiveColumnsSettingsCacheService)) as IArchiveColumnsSettingsCacheService;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране. Этот метод вызывается при
  /// первом показе закладки, а также при переключении на нее с другой закладки.
  /// </summary>
  /// <param name="previousView">Закладка, с которой осуществляется переключение. Может быть null для самой первой
  /// показываемой на экране закладки.</param>
  public void Activate(IView previousView)
  {
    if (this._isFirstTimeOpened)
    {
      this.ReloadView();
      this._isFirstTimeOpened = false;
    }
    this.OnModified += new EventHandler(this.SetOkCancelButtonsAvailability);
  }

  /// <summary>Перезагрузим данные на вьюшке</summary>
  private void ReloadView()
  {
    this._isModified = this.btnApply.Enabled = this.btnCancel.Enabled = this.btnDeleteRole.Enabled = false;
    this.btnAddDefaultRole.Enabled = true;
    this._rolesColumnsSettingsModel = new List<RolesColumnsSettings>((IEnumerable<RolesColumnsSettings>) this._archiveColumnsSettingsCache.GetArchiveColumnsSettings(this._archiveID).RolesColumnSettings);
    this.lvColumns.Items.Clear();
    this.FillRoles();
    if (this.lvRoles.Items.Count > 0)
      this.lvRoles.Items[0].Selected = true;
    this.lvColumns.LargeImageList = Statics.IconSrv.BigImageList;
    this.lvColumns.SmallImageList = Statics.IconSrv.ImageList;
  }

  /// <summary>Заполнить ListView ролями</summary>
  private void FillRoles()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.lvRoles.Items.Clear();
      foreach (RolesColumnsSettings rolesColumnsSettings in this._rolesColumnsSettingsModel)
      {
        if (rolesColumnsSettings.RoleID == Consts.DefaultRoleId)
        {
          this.lvRoles.Items.Add(new ListViewItem()
          {
            Text = ServiceHolder.rm.GetString("Archives_216"),
            Tag = (object) rolesColumnsSettings.RoleID,
            Name = ServiceHolder.rm.GetString("Archives_216")
          });
          this.btnAddDefaultRole.Enabled = false;
        }
        else
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(rolesColumnsSettings.RoleID);
          if (!objectInfo.Empty)
            this.lvRoles.Items.Add(new ListViewItem()
            {
              Text = objectInfo.Caption,
              Tag = (object) rolesColumnsSettings.RoleID,
              Name = objectInfo.Caption
            });
        }
      }
    }
  }

  private void SetOkCancelButtonsAvailability(object sender, EventArgs e)
  {
    this.btnApply.Enabled = this.btnCancel.Enabled = true;
  }

  /// <summary>На форме произошли изменения.</summary>
  private void Modify()
  {
    this._isModified = true;
    EventHandler onModified = this.OnModified;
    if (onModified == null)
      return;
    onModified((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране. Этот метод
  /// вызывается при переключении на другую закладку, а также удалении всех закладок.
  /// </summary>
  /// <param name="nextView">Закладка, на которую осуществляется переключение. Может быть null, если выполяется
  /// не переключение, а удаление закладок.</param>
  public void Deactivate(IView nextView)
  {
    if (this._isModified)
    {
      if (MessageBox.Show(ServiceHolder.rm.GetString("Archives_156"), ServiceHolder.rm.GetString("Archives_155"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals((object) DialogResult.Yes))
        this.btnApply_Click((object) null, (EventArgs) null);
      else
        this.btnCancel_Click((object) null, (EventArgs) null);
    }
    this.OnModified -= new EventHandler(this.SetOkCancelButtonsAvailability);
  }

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  /// <value></value>
  public string Caption => ServiceHolder.rm.GetString("Archives_213");

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет проинициализирована в методе Initialize.
  /// </summary>
  /// <value></value>
  public int ImageIndex => this._imageIndex;

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  /// <value></value>
  public int OrderID => 29;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnApply_Click(object sender, EventArgs e)
  {
    for (int index = this._rolesColumnsSettingsModel.Count - 1; index >= 0; --index)
    {
      if (this._rolesColumnsSettingsModel[index].Columns.Count == 0)
        this._rolesColumnsSettingsModel.Remove(this._rolesColumnsSettingsModel[index]);
    }
    this._archiveColumnsSettingsCache.SaveSettingsToCacheAndBase(new ArchiveColumnsSettings()
    {
      ArchiveID = this._archiveID,
      RolesColumnSettings = this._rolesColumnsSettingsModel
    });
    this.ReloadView();
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.ReloadView();
    this._isModified = false;
  }

  /// <summary>Удалить роль</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnDeleteRole_Click(object sender, EventArgs e)
  {
    this.lvRoles.BeginUpdate();
    int count = this.lvRoles.SelectedItems.Count;
    for (int i = 0; i < count; i++)
    {
      this._rolesColumnsSettingsModel.Remove(this._rolesColumnsSettingsModel.FirstOrDefault<RolesColumnsSettings>((Func<RolesColumnsSettings, bool>) (x => x.RoleID == Convert.ToInt64(this.lvRoles.SelectedItems[i].Tag))));
      if ((long) this.lvRoles.SelectedItems[i].Tag == Consts.DefaultRoleId)
        this.btnAddDefaultRole.Enabled = true;
      this.lvRoles.Items.Remove(this.lvRoles.SelectedItems[i]);
    }
    this.SortRoles();
    this.lvRoles.EndUpdate();
    this.Modify();
  }

  /// <summary>
  /// Добавить настройки для ролей по умолчанию
  /// Кнопка работает только если этой настройки еще нет в списке ролей
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAddDefaultRole_Click(object sender, EventArgs e)
  {
    this.lvRoles.BeginUpdate();
    this.lvRoles.Items.Add(new ListViewItem()
    {
      Text = ServiceHolder.rm.GetString("Archives_216"),
      Tag = (object) Consts.DefaultRoleId,
      Name = ServiceHolder.rm.GetString("Archives_216")
    });
    this.SortRoles();
    this.lvRoles.Items[0].Selected = true;
    this.btnAddDefaultRole.Enabled = false;
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.AddObligatoryColumns(columns, true, false);
    this._rolesColumnsSettingsModel.Add(new RolesColumnsSettings()
    {
      RoleID = Consts.DefaultRoleId,
      Columns = columns
    });
    this.lvRoles.EndUpdate();
    this.lvRoles.Items[0].Selected = true;
    this.Modify();
  }

  /// <summary>
  /// Сортировка ListView с ролями таким образом, что сначала всегда стоит значение по умолчанию,
  /// а потом остальные отсортированные роли
  /// </summary>
  private void SortRoles()
  {
    if (this.lvRoles.Items.Count <= 1)
      return;
    ListViewItem[] listViewItemArray = new ListViewItem[this.lvRoles.Items.Count];
    ListViewItem listViewItem = new ListViewItem()
    {
      Text = ServiceHolder.rm.GetString("Archives_216"),
      Tag = (object) Consts.DefaultRoleId,
      Name = ServiceHolder.rm.GetString("Archives_216")
    };
    if (this.lvRoles.Items.ContainsKey(ServiceHolder.rm.GetString("Archives_216")))
    {
      this.lvRoles.Items.RemoveByKey(ServiceHolder.rm.GetString("Archives_216"));
      this.lvRoles.Sorting = SortOrder.Ascending;
      this.lvRoles.Sort();
      this.lvRoles.Sorting = SortOrder.None;
      listViewItemArray[0] = listViewItem;
      this.lvRoles.Items.CopyTo((Array) listViewItemArray, 1);
      this.lvRoles.Items.Clear();
      this.lvRoles.Items.AddRange(listViewItemArray);
    }
    else
    {
      this.lvRoles.Sorting = SortOrder.Ascending;
      this.lvRoles.Sort();
    }
  }

  /// <summary>Добавляем роли</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAddRole_Click(object sender, EventArgs e)
  {
    IReadOnlyList<IDBObjectID> dbObjectIdList = SelectDialog.Objects((IReadOnlyCollection<int>) new List<int>()
    {
      MetaDataHelper.GetObjectTypeID("cad00007-306c-11d8-b4e9-00304f19f545")
    }, "Выберите роли", operationName: "SelectPLCommand", disableGlobalContextMenuCommands: true);
    if (dbObjectIdList == null || dbObjectIdList.Count <= 0)
      return;
    this.lvRoles.BeginUpdate();
    foreach (IDBObjectID dbObjectId in (IEnumerable<IDBObjectID>) dbObjectIdList)
    {
      this.lvRoles.Items.Add(new ListViewItem()
      {
        Text = dbObjectId.Caption,
        Tag = (object) Convert.ToInt64(dbObjectId.Value),
        Name = dbObjectId.Caption
      });
      NodeColumnCollection columns = new NodeColumnCollection();
      Helper.AddObligatoryColumns(columns, true, false);
      this._rolesColumnsSettingsModel.Add(new RolesColumnsSettings()
      {
        RoleID = Convert.ToInt64(dbObjectId.Value),
        Columns = columns
      });
      this.lvRoles.Items[this.lvRoles.Items.Count - 1].Selected = true;
    }
    this.SortRoles();
    this.lvRoles.EndUpdate();
    this.Modify();
  }

  /// <summary>Изменение выделенной роли</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void lvRoles_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvRoles.SelectedItems.Count == 0)
    {
      this.btnDeleteRole.Enabled = false;
      this.btnColumnSettings.Enabled = false;
      this.lvColumns.Items.Clear();
    }
    else
    {
      this.btnDeleteRole.Enabled = true;
      this.btnColumnSettings.Enabled = true;
      this.LoadColumnsForRole();
    }
  }

  /// <summary>Загружаем в листвью отображаемые колонки для роли</summary>
  private void LoadColumnsForRole()
  {
    NodeColumnCollection columnCollection = this.GetSelectedRoleNodeColumnCollection();
    this.lvColumns.BeginUpdate();
    this.lvColumns.Items.Clear();
    foreach (NodeColumn nodeColumn in (List<NodeColumn>) columnCollection)
    {
      int num = nodeColumn.Attribute != null ? this.GetTypeImageIndex(nodeColumn.Attribute.FieldType) : -1;
      this.lvColumns.Items.Add(new ListViewItem()
      {
        Text = nodeColumn.Caption,
        ImageIndex = num,
        Name = nodeColumn.Caption
      });
    }
    this.lvColumns.EndUpdate();
  }

  /// <summary>Коллекция колонк для выделенной роли</summary>
  /// <returns></returns>
  private NodeColumnCollection GetSelectedRoleNodeColumnCollection()
  {
    if (this.lvRoles.SelectedItems.Count != 1)
      return new NodeColumnCollection();
    long currentRoleId = Convert.ToInt64(this.lvRoles.SelectedItems[0].Tag);
    RolesColumnsSettings rolesColumnsSettings = this._rolesColumnsSettingsModel.FirstOrDefault<RolesColumnsSettings>((Func<RolesColumnsSettings, bool>) (x => x.RoleID == currentRoleId));
    return rolesColumnsSettings == null ? new NodeColumnCollection() : rolesColumnsSettings.Columns;
  }

  /// <summary>Вызываем настройку колонок</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnColumnSettings_Click(object sender, EventArgs e)
  {
    NodeColumnCollection columnCollection1 = new NodeColumnCollection();
    Helper.AddObligatoryColumns(columnCollection1, true, true);
    Helper.AddObligatoryColumnsAdv(columnCollection1);
    Helper.AddObjectTypeColumns(columnCollection1, MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545"));
    Helper.AddAllColumns(columnCollection1);
    NodeColumnCollection columnCollection2 = this.GetSelectedRoleNodeColumnCollection();
    if (AppearanceTuningForm.Execute((INode) null, ContentType.None, columnCollection1, columnCollection2) != DialogResult.OK)
      return;
    this.SetColumnsForSelectedRole(columnCollection2);
    this.LoadColumnsForRole();
  }

  /// <summary>Работаем с контекстным меню</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void lvRoles_MouseUp(object sender, MouseEventArgs e)
  {
  }

  private void lvRoles_MouseClick(object sender, MouseEventArgs e)
  {
    if (e.Button == MouseButtons.Left || e.Button != MouseButtons.Right || this.lvRoles.SelectedItems.Count == 0 || !this.lvRoles.FocusedItem.Bounds.Contains(e.Location) || this.lvRoles.Items.Count < 2)
      return;
    List<MenuItem> enumerable = new List<MenuItem>();
    if (!this.btnAddDefaultRole.Enabled && this.lvRoles.SelectedItems[0].Name != ServiceHolder.rm.GetString("Archives_216"))
    {
      MenuItem menuItem = new MenuItem("Скопировать колонки у настройки по умолчанию", new EventHandler(this.CopyFromRole_Click));
      enumerable.Add(menuItem);
    }
    List<string> stringList = new List<string>();
    foreach (ListViewItem listViewItem in this.lvRoles.Items)
      stringList.Add(listViewItem.Text);
    stringList.Remove(ServiceHolder.rm.GetString("Archives_216"));
    stringList.Remove(this.lvRoles.SelectedItems[0].Text);
    if (stringList.Count > 0)
    {
      MenuItem menuItem1 = new MenuItem("Скопировать настройки у роли");
      foreach (string text in stringList)
      {
        MenuItem menuItem2 = new MenuItem(text, new EventHandler(this.CopyFromRole_Click));
        menuItem1.MenuItems.Add(menuItem2);
      }
      enumerable.Add(menuItem1);
    }
    this.lvRoles.ContextMenu = new ContextMenu(enumerable.AsArray<MenuItem>());
    this.lvRoles.ContextMenu.Show((Control) this.lvRoles, new Point(e.X, e.Y));
  }

  /// <summary>Обработчик нажатия команды контекстного меню для роли</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CopyFromRole_Click(object sender, EventArgs e)
  {
    if (!(sender is MenuItem menuItem))
      return;
    string text = menuItem.Text;
    if (text == "Скопировать колонки у настройки по умолчанию")
    {
      RolesColumnsSettings rolesColumnsSettings = this._rolesColumnsSettingsModel.FirstOrDefault<RolesColumnsSettings>((Func<RolesColumnsSettings, bool>) (x => x.RoleID == Consts.DefaultRoleId));
      if (rolesColumnsSettings != null)
      {
        this.SetColumnsForSelectedRole(rolesColumnsSettings.Columns);
        this.Modify();
        this.LoadColumnsForRole();
      }
    }
    else
    {
      foreach (ListViewItem listViewItem in this.lvRoles.Items)
      {
        ListViewItem item = listViewItem;
        if (item.Name == text)
        {
          RolesColumnsSettings rolesColumnsSettings = this._rolesColumnsSettingsModel.FirstOrDefault<RolesColumnsSettings>((Func<RolesColumnsSettings, bool>) (x => x.RoleID == Convert.ToInt64(item.Tag)));
          if (rolesColumnsSettings != null)
          {
            this.SetColumnsForSelectedRole(rolesColumnsSettings.Columns);
            this.Modify();
            this.LoadColumnsForRole();
          }
        }
      }
    }
    this.lvRoles.ContextMenu.Dispose();
  }

  /// <summary>Вернуть номер значка для указанного атрибута</summary>
  /// <param name="attrType">Тип данных атрибута</param>
  /// <returns>Номер значка для указанного атрибута</returns>
  protected int GetTypeImageIndex(FieldTypes attrType)
  {
    return Statics.IconSrv == null ? -1 : Statics.IconSrv.IndexOf(3, -1, (object) attrType);
  }

  /// <summary>Назначить подаваемые колонки выбранной роли</summary>
  /// <param name="newRoleColumns"></param>
  private void SetColumnsForSelectedRole(NodeColumnCollection newRoleColumns)
  {
    long currentRoleId = Convert.ToInt64(this.lvRoles.SelectedItems[0].Tag);
    RolesColumnsSettings rolesColumnsSettings = this._rolesColumnsSettingsModel.FirstOrDefault<RolesColumnsSettings>((Func<RolesColumnsSettings, bool>) (x => x.RoleID == currentRoleId));
    if (rolesColumnsSettings == null)
      return;
    rolesColumnsSettings.Columns = (NodeColumnCollection) newRoleColumns.Clone();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ColumnsAutoSettingsView));
    this.pnlButtom = new Panel();
    this._buttons = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.splitContainer = new SplitContainer();
    this.lvRoles = new ListView();
    this.roles = new ColumnHeader();
    this.toolBarRoles = new Intermech.Bars.ToolBar();
    this.btnAddRole = new ButtonItem();
    this.btnDeleteRole = new ButtonItem();
    this.btnAddDefaultRole = new ButtonItem();
    this.lvColumns = new ListView();
    this.columns = new ColumnHeader();
    this.toolBarForUsers = new Intermech.Bars.ToolBar();
    this.btnColumnSettings = new ButtonItem();
    this.pnlButtom.SuspendLayout();
    this._buttons.SuspendLayout();
    this.splitContainer.BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.SuspendLayout();
    this.pnlButtom.Controls.Add((Control) this._buttons);
    this.pnlButtom.Dock = DockStyle.Bottom;
    this.pnlButtom.Location = new Point(0, 413);
    this.pnlButtom.Name = "pnlButtom";
    this.pnlButtom.Size = new Size(748, 40);
    this.pnlButtom.TabIndex = 4;
    this._buttons.Controls.Add((Control) this.btnCancel);
    this._buttons.Controls.Add((Control) this.btnApply);
    this._buttons.Dock = DockStyle.Right;
    this._buttons.Location = new Point(481, 0);
    this._buttons.Name = "_buttons";
    this._buttons.Size = new Size(267, 40);
    this._buttons.TabIndex = 0;
    this.btnCancel.Enabled = false;
    this.btnCancel.FlatStyle = FlatStyle.System;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(146, 6);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 2;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnApply.Enabled = false;
    this.btnApply.FlatStyle = FlatStyle.System;
    this.btnApply.ImeMode = ImeMode.NoControl;
    this.btnApply.Location = new Point(19, 6);
    this.btnApply.Name = "btnApply";
    this.btnApply.Size = new Size(121, 27);
    this.btnApply.TabIndex = 1;
    this.btnApply.Text = "Применить";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.splitContainer.BackColor = SystemColors.ControlLight;
    this.splitContainer.Dock = DockStyle.Fill;
    this.splitContainer.Location = new Point(0, 0);
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Panel1.AutoScroll = true;
    this.splitContainer.Panel1.BackColor = SystemColors.Control;
    this.splitContainer.Panel1.Controls.Add((Control) this.lvRoles);
    this.splitContainer.Panel1.Controls.Add((Control) this.toolBarRoles);
    this.splitContainer.Panel2.AutoScroll = true;
    this.splitContainer.Panel2.BackColor = SystemColors.Control;
    this.splitContainer.Panel2.Controls.Add((Control) this.lvColumns);
    this.splitContainer.Panel2.Controls.Add((Control) this.toolBarForUsers);
    this.splitContainer.Size = new Size(748, 413);
    this.splitContainer.SplitterDistance = 326;
    this.splitContainer.TabIndex = 5;
    this.lvRoles.Columns.AddRange(new ColumnHeader[1]
    {
      this.roles
    });
    this.lvRoles.Dock = DockStyle.Fill;
    this.lvRoles.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvRoles.HideSelection = false;
    this.lvRoles.Location = new Point(0, 24);
    this.lvRoles.MultiSelect = false;
    this.lvRoles.Name = "lvRoles";
    this.lvRoles.Size = new Size(326, 389);
    this.lvRoles.TabIndex = 3;
    this.lvRoles.UseCompatibleStateImageBehavior = false;
    this.lvRoles.View = View.Details;
    this.lvRoles.SelectedIndexChanged += new EventHandler(this.lvRoles_SelectedIndexChanged);
    this.lvRoles.MouseClick += new MouseEventHandler(this.lvRoles_MouseClick);
    this.lvRoles.MouseUp += new MouseEventHandler(this.lvRoles_MouseUp);
    this.roles.Text = "Роли";
    this.roles.Width = 327;
    this.toolBarRoles.FullMenus = true;
    this.toolBarRoles.Guid = new Guid("93a280a9-f4db-4ac4-9ee6-5c5e3ba516b4");
    this.toolBarRoles.Hidden = false;
    this.toolBarRoles.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.btnAddRole,
      (ToolbarItemBase) this.btnDeleteRole,
      (ToolbarItemBase) this.btnAddDefaultRole
    });
    this.toolBarRoles.Location = new Point(0, 0);
    this.toolBarRoles.Name = "toolBarRoles";
    this.toolBarRoles.Size = new Size(326, 24);
    this.toolBarRoles.TabIndex = 2;
    this.toolBarRoles.Text = "toolBar1";
    this.btnAddRole.BeginGroup = true;
    this.btnAddRole.CommandName = "btnAddRole";
    this.btnAddRole.Image = (Image) componentResourceManager.GetObject("btnAddRole.Image");
    this.btnAddRole.ImageIndex = 0;
    this.btnAddRole.ToolTipText = "Добавить роль";
    this.btnAddRole.Click += new EventHandler(this.btnAddRole_Click);
    this.btnDeleteRole.BeginGroup = true;
    this.btnDeleteRole.CommandName = "btnDeleteRole";
    this.btnDeleteRole.Enabled = false;
    this.btnDeleteRole.Image = (Image) componentResourceManager.GetObject("btnDeleteRole.Image");
    this.btnDeleteRole.ToolTipText = "Удалить роль";
    this.btnDeleteRole.Click += new EventHandler(this.btnDeleteRole_Click);
    this.btnAddDefaultRole.BeginGroup = true;
    this.btnAddDefaultRole.CommandName = "btnAddRole";
    this.btnAddDefaultRole.Image = (Image) componentResourceManager.GetObject("btnAddDefaultRole.Image");
    this.btnAddDefaultRole.ImageIndex = 0;
    this.btnAddDefaultRole.ShowText = true;
    this.btnAddDefaultRole.Text = "Настройка по умолчанию";
    this.btnAddDefaultRole.ToolTipText = "Добавить настройки по умолчанию  для всех неуказанных ролей";
    this.btnAddDefaultRole.Click += new EventHandler(this.btnAddDefaultRole_Click);
    this.lvColumns.Columns.AddRange(new ColumnHeader[1]
    {
      this.columns
    });
    this.lvColumns.Dock = DockStyle.Fill;
    this.lvColumns.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvColumns.HideSelection = false;
    this.lvColumns.Location = new Point(0, 24);
    this.lvColumns.Name = "lvColumns";
    this.lvColumns.Size = new Size(418, 389);
    this.lvColumns.TabIndex = 3;
    this.lvColumns.UseCompatibleStateImageBehavior = false;
    this.lvColumns.View = View.Details;
    this.columns.Text = "Колонки";
    this.columns.Width = 339;
    this.toolBarForUsers.FullMenus = true;
    this.toolBarForUsers.Guid = new Guid("789fe93a-3b81-425b-a128-f7c0eb7be0ad");
    this.toolBarForUsers.Hidden = false;
    this.toolBarForUsers.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.btnColumnSettings
    });
    this.toolBarForUsers.Location = new Point(0, 0);
    this.toolBarForUsers.Name = "toolBarForUsers";
    this.toolBarForUsers.Size = new Size(418, 24);
    this.toolBarForUsers.TabIndex = 2;
    this.toolBarForUsers.Text = "toolBar1";
    this.btnColumnSettings.BeginGroup = true;
    this.btnColumnSettings.CommandName = "btnColumnSettings";
    this.btnColumnSettings.Enabled = false;
    this.btnColumnSettings.Icon = (Icon) componentResourceManager.GetObject("btnColumnSettings.Icon");
    this.btnColumnSettings.ImageIndex = 0;
    this.btnColumnSettings.ShowText = true;
    this.btnColumnSettings.Text = "Настроить отображаемые  колонки";
    this.btnColumnSettings.ToolTipText = "Настроить отображаемые  колонки";
    this.btnColumnSettings.Click += new EventHandler(this.btnColumnSettings_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer);
    this.Controls.Add((Control) this.pnlButtom);
    this.Name = nameof (ColumnsAutoSettingsView);
    this.Size = new Size(748, 453);
    this.pnlButtom.ResumeLayout(false);
    this._buttons.ResumeLayout(false);
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.EndInit();
    this.splitContainer.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class ColumnsAutoSettingsViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList service))
        service = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
      INamedImageList namedImageList = service;
      return new ViewDescription()
      {
        Caption = ServiceHolder.rm.GetString("Archives_213"),
        ImageIndex = namedImageList != null ? namedImageList.ImageIndex("imgViewSettings") : -1,
        OrderID = 29
      };
    }
  }
}
