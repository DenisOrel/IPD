
// Type: Intermech.Client.Core.Controls.ContextFiltrationPanelControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Search.EditingContexts;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;


namespace Intermech.Client.Core.Controls;

/// <summary>
/// "Обёртка" над элементом Intermech.Bars.DropDownMenuItem, позволяющая
/// формировать список, связанный с контекстами редактирования
/// </summary>
public sealed class ContextFiltrationPanelControl : ObjectsDropDownControl
{
  /// <summary>Панель контекста</summary>
  private IContextFiltrationPanel panel;
  /// <summary>Идентификатор типа объекта "Контексты редактирования"</summary>
  private int contextTypeID;
  /// <summary>
  /// Список идентификаторов версий контекстов, которые были выбраны пользователем
  /// </summary>
  public List<long> History = new List<long>();
  /// <summary>
  /// Максимальное количество контекстов, запоминаемое в истории
  /// </summary>
  public int HistoryLimit = 25;

  /// <summary>Выделенный в списке элемент</summary>
  public override long SelectedItem
  {
    [DebuggerStepThrough] get => base.SelectedItem;
    set => base.SelectedItem = value;
  }

  /// <summary>
  /// Создать "обёртку" для указанного меню, инициализировать меню
  /// </summary>
  /// <param name="panel">Панель контекста</param>
  /// <param name="menu">Меню, для которого требуется создать "обёртку"</param>
  /// <param name="options">Опции</param>
  /// <param name="caption">Заголовок кнопки</param>
  /// <param name="image">Изображение кнопки</param>
  /// <param name="objectIDs">Список идентификаторов версий объектов</param>
  /// <param name="monitoredTypes">Список отслеживаемых типов объектов (или null)</param>
  /// <param name="selectedItem">Выделенный в списке элемент</param>
  /// <param name="groupItem">Группирующий элемент (null, если не задана опция WithGroupItem)</param>
  public ContextFiltrationPanelControl(
    IContextFiltrationPanel panel,
    DropDownMenuItem menu,
    Image image,
    IList<long> objectIDs,
    long selectedItem)
    : base(menu, ObjectsDropDownOptions.Default, "Текущий контекст редактирования", image, new MyObjectElement(0L, "Контекст редактирования не выбран", (object) null, MetaDataHelper.GetObjectTypeID("cad0146b-306c-11d8-b4e9-00304f19f545")), objectIDs, (IList<int>) MetaDataHelper.GetSpecialGroupingIDs(), selectedItem)
  {
    this.panel = panel;
    this.PrepareControls();
    this.UpdateControls();
  }

  /// <summary>Обновить состояние элементов</summary>
  protected override void UpdateControls()
  {
    base.UpdateControls();
    if (this.panel == null)
      return;
    MyObjectElement tag = this.menu.Tag as MyObjectElement;
    this.panel.ButtonEditingContextsEdit.Enabled = tag != null && tag.ObjectID != 0L;
    this.panel.MenuEditingContextMode.Enabled = this.panel.ButtonEditingContextsEdit.Enabled;
    this.panel.ButtonEditingContextsRefresh.Enabled = false;
    this.panel.ButtonEditingContextsRefresh.Visible = false;
    this.panel.ButtonEditingContextsRefresh.Locked = true;
    this.CorrectEditorMode();
  }

  /// <summary>
  /// Заполнить свойства элементов управления панели "Текущий контекст редактирования" определёнными значениями
  /// </summary>
  internal void PrepareControls()
  {
    if (this.History.Count > this.HistoryLimit)
      this.History.RemoveRange(this.HistoryLimit, this.History.Count - this.HistoryLimit);
    this.panel.ButtonEditingContextsRefresh.ImageIndex = this.namedImageList.ImageIndex("imgRefresh");
    this.panel.MenuEditingContextMode.ImageIndex = this.namedImageList.ImageIndex("imgEditingContextsMode");
    this.panel.ButtonEditingContextsEdit.ImageIndex = this.namedImageList.ImageIndex("imgEditingContextsEdit");
    this.panel.ButtonEditingContextsCreate.ImageIndex = this.namedImageList.ImageIndex("imgEditingContextsCreate");
    this.panel.ButtonEditingContextsBrowse.ImageIndex = this.namedImageList.ImageIndex("imgEditingContextsBrowse");
    this.panel.MenuEditingContextMode.Items.Clear();
    this.panel.MenuEditingContextMode.Tag = (object) EditingContextMode.Default;
    this.panel.ButtonProjectFilterMode.ToolTipText = EnumDescConverter.GetEnumDescription((Enum) EditingContextMode.Default);
    for (int index = 0; index < Enum.GetValues(typeof (EditingContextMode)).Length; ++index)
    {
      MenuButtonItem menuButtonItem = new MenuButtonItem(EnumDescConverter.GetEnumDescription((Enum) (EditingContextMode) Enum.GetValues(typeof (EditingContextMode)).GetValue(index)), new EventHandler(this.ContextFilterOn), index == 0 ? this.namedImageList.ImageIndex("imgEditingContextsMode") : this.namedImageList.ImageIndex("imgEditingContextsModeAuto"));
      menuButtonItem.Tag = Enum.GetValues(typeof (EditingContextMode)).GetValue(index);
      menuButtonItem.AutoToggle = AutoToggleType.Radio;
      menuButtonItem.Checked = false;
      this.panel.MenuEditingContextMode.Items.Add((ToolbarItemBase) menuButtonItem);
    }
    this.panel.ButtonEditingContextsBrowse.Click += new EventHandler(this.DoContextBrowse);
    this.panel.ButtonEditingContextsCreate.Click += new EventHandler(this.DoContextCreate);
    this.panel.ButtonEditingContextsEdit.Click += new EventHandler(this.DoContextEdit);
    this.panel.ButtonEditingContextsRefresh.Click += new EventHandler(this.DoReload);
  }

  /// <summary>Вызван группирующий элемент меню</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  protected override void OnGroupItemClick(object sender, EventArgs e)
  {
    base.OnGroupItemClick(sender, e);
    try
    {
      this.userAndRole.EditingContextID = this.groupItem.ObjectID;
      this.SelectedItem = this.userAndRole.CachedEditingContextID;
    }
    finally
    {
      this.UpdateControls();
    }
  }

  /// <summary>Вызван обычный элемент меню</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  protected override void OnItemClick(object sender, EventArgs e)
  {
    base.OnItemClick(sender, e);
    try
    {
      this.userAndRole.EditingContextID = this.selectedItem;
      this.SelectedItem = this.userAndRole.CachedEditingContextID;
    }
    finally
    {
      this.UpdateControls();
    }
  }

  /// <summary>
  /// Исправить выделенный пункт в меню с режимами работы контекстов редактирования
  /// </summary>
  internal void CorrectEditorMode()
  {
    for (int index = 0; index < this.panel.MenuEditingContextMode.Items.Count; ++index)
      this.panel.MenuEditingContextMode.Items[index].Checked = false;
    EditingContextMode cachedContextMode = this.userAndRole.CachedContextMode;
    for (int index = 0; index < this.panel.MenuEditingContextMode.Items.Count; ++index)
    {
      this.panel.MenuEditingContextMode.Items[index].Checked = (EditingContextMode) this.panel.MenuEditingContextMode.Items[index].Tag == cachedContextMode;
      if (this.panel.MenuEditingContextMode.Items[index].Checked)
      {
        this.panel.MenuEditingContextMode.Tag = this.panel.MenuEditingContextMode.Items[index].Tag;
        this.panel.MenuEditingContextMode.ToolTipText = this.panel.MenuEditingContextMode.Items[index].Text;
        this.panel.MenuEditingContextMode.ImageIndex = this.panel.MenuEditingContextMode.Items[index].ImageIndex;
      }
    }
  }

  /// <summary>Редактировать текущий контекст</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  internal void DoContextEdit(object sender, EventArgs e)
  {
    if (this.userAndRole.CachedEditingContextID == 0L)
      return;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(Math.Abs(this.userAndRole.CachedEditingContextID), false);
        if (objectActualCopy == null)
        {
          this.userAndRole.EditingContextID = 0L;
          this.UpdateControls();
          return;
        }
        if (this.userAndRole.EditingContextID != objectActualCopy.ObjectID)
          this.userAndRole.EditingContextID = objectActualCopy.ObjectID;
      }
      using (EditingContextEditorDialog contextEditorDialog = new EditingContextEditorDialog())
      {
        contextEditorDialog.EditingContextVersionID = this.userAndRole.CachedEditingContextID;
        int num = (int) contextEditorDialog.ShowDialog();
      }
      this.SelectedItem = this.userAndRole.CachedEditingContextID;
    }
    finally
    {
      this.UpdateControls();
    }
  }

  /// <summary>Выбрать другой контекст редактирования</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  internal void DoContextBrowse(object sender, EventArgs e)
  {
    DescriptorCollection descriptors = new DescriptorCollection();
    List<int> contextTopObjectsIds = MetaDataHelper.GetEditingContextTopObjectsIDs();
    for (int index = 0; index < contextTopObjectsIds.Count; ++index)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(contextTopObjectsIds[index]));
    long[] numArray = SelectionWindow.SelectObjects("Выберите контекст редактирования", "Выберите контекст редактирования. Он будет автоматически активирован, а его правило подбора версий применено для текущего окна", (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor("Контексты редактирования ", descriptors), SelectionOptions.Default | SelectionOptions.DisableSelectAbstractTypes | SelectionOptions.DisableMultiselect);
    if (numArray == null)
      return;
    if (numArray.Length == 0)
      return;
    try
    {
      this.userAndRole.EditingContextID = numArray[0];
      this.SelectedItem = this.userAndRole.CachedEditingContextID;
    }
    finally
    {
      this.UpdateControls();
    }
  }

  /// <summary>Создать новый контекст и активировать его</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  internal void DoContextCreate(object sender, EventArgs e)
  {
    IObjectCreatorService service = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    ServicesManager.GetService(typeof (INotificationService));
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad0146b-306c-11d8-b4e9-00304f19f545");
    long objectByTypeDialog = service.CreateObjectByTypeDialog(objectTypeId);
    switch (objectByTypeDialog)
    {
      case -1:
        break;
      case 0:
        break;
      default:
        try
        {
          this.userAndRole.EditingContextID = objectByTypeDialog;
          this.SelectedItem = this.userAndRole.CachedEditingContextID;
          break;
        }
        finally
        {
          this.UpdateControls();
        }
    }
  }

  /// <summary>Перечитать контексты редактирования</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  internal void DoReload(object sender, EventArgs e)
  {
    this.Load((IList<long>) this.History, this.SelectedItem);
  }

  /// <summary>
  /// Загрузить информацию из базы данных и "привязать" её к меню
  /// </summary>
  /// <param name="caption">Заголовок кнопки</param>
  /// <param name="image">Изображение кнопки</param>
  /// <param name="objectIDs">Список идентификаторов версий объектов</param>
  /// <param name="monitoredTypes">Список отслеживаемых типов объектов (или null)</param>
  /// <param name="selectedItem">Выделенный в списке элемент</param>
  public override void Load(
    string caption,
    Image image,
    IList<long> objectIDs,
    IList<int> monitoredTypes,
    long selectedItem)
  {
    base.Load(caption, image, objectIDs, monitoredTypes, selectedItem);
    this.History = new List<long>();
    if (this.items == null)
      return;
    this.History.AddRange((IEnumerable<long>) this.items.ConvertAll<long>((Converter<MyObjectElement, long>) (item => item.ObjectID)));
  }

  /// <summary>Получено очередное событие от службы уведомлений</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  protected override void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (e.EventName == "FiltrationChanged" || e.EventName == "EditingContextChanged")
    {
      this.SelectedItem = this.userAndRole.CachedEditingContextID;
      this.AlterObject(this.userAndRole.CachedEditingContextID, true, true, true);
      this.UpdateControls();
    }
    else
      base.NotificationEventFired(sender, e);
  }

  /// <summary>Добавить в историю список контекстов</summary>
  /// <param name="contexts">Список контекстов</param>
  public void AddToHistory(List<long> contexts)
  {
    if (contexts == null || contexts.Count == 0)
      return;
    for (int index = 0; index < contexts.Count; ++index)
    {
      long context = contexts[index];
      if (context != 0L)
      {
        this.History.Remove(context);
        this.History.Remove(-context);
        this.History.Insert(0, context);
      }
    }
    if (this.History.Count > this.HistoryLimit)
      this.History.RemoveRange(this.HistoryLimit, this.History.Count - this.HistoryLimit);
    base.Load(this.caption, this.image, (IList<long>) this.History, (IList<int>) this.monitoredTypes, this.SelectedItem);
  }

  /// <summary>
  /// Изучить текущее окно "Навигатора", добавить в историю контекстов все найденные контексты редактирования
  /// </summary>
  public void CollectCurrentContextsHistory()
  {
    if (!(ServicesManager.GetService(typeof (ICurrentNavWindow)) is ICurrentNavWindow service) || service.NavWindow == null)
      return;
    List<long> versionIDs = new List<long>();
    if (service.NavWindow is ITreeListColumns navWindow && navWindow.RootDescriptor != null && navWindow.RootDescriptor is Intermech.Navigator.DBObjects.Descriptor rootDescriptor && !rootDescriptor.InvalidDescriptor && rootDescriptor.ObjectID != 0L && versionIDs.IndexOf(rootDescriptor.ObjectID) < 0)
      versionIDs.Add(rootDescriptor.ObjectID);
    if (versionIDs.Count == 0)
      return;
    List<long> contexts = (List<long>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) is IDBEditingContextsService customService))
        return;
      contexts = customService.FindObjectsContexts((object) sessionKeeper.Session.SessionGUID, versionIDs, true);
    }
    this.AddToHistory(contexts);
  }

  /// <summary>Режим работы текущего контекста редактирования</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void ContextFilterOn(object sender, EventArgs e)
  {
    MenuButtonItem menuButtonItem = sender as MenuButtonItem;
    try
    {
      this.userAndRole.EditingContextMode = (EditingContextMode) menuButtonItem.Tag;
      this.SelectedItem = this.userAndRole.CachedEditingContextID;
    }
    finally
    {
      this.UpdateControls();
    }
  }

  protected override void FillDropDownMenu()
  {
    base.FillDropDownMenu();
    this.History = new List<long>();
    if (this.items != null)
      this.History.AddRange(this.items.Where<MyObjectElement>((Func<MyObjectElement, bool>) (o => this.IsEditingContext(o))).Select<MyObjectElement, long>((Func<MyObjectElement, long>) (o => o.ObjectID)));
    Image notSelectedImage = this.GetEditingContextNotSelectedImage();
    if (this.menu.Tag is MyObjectElement tag1 && ObjectHelper.IsUnknownObjectVersionID(tag1.ObjectID))
      this.menu.Image = notSelectedImage;
    foreach (MenuItemBase menuItemBase in (CollectionBase) this.menu.Items)
    {
      MyObjectElement tag2 = menuItemBase.Tag as MyObjectElement;
      if (tag1 != null && ObjectHelper.IsUnknownObjectVersionID(tag2.ObjectID))
        menuItemBase.Image = notSelectedImage;
    }
  }

  private bool IsEditingContext(MyObjectElement myObjectElement)
  {
    return !ObjectTypeHelper.IsUnknownObjectTypeID(myObjectElement.ObjectType) && EditingContextsHelper.IsEditingContextObjectTypeID(myObjectElement.ObjectType);
  }

  private Image GetEditingContextNotSelectedImage()
  {
    using (Bitmap notSelected16x16 = Resources.EditingContextNotSelected_16x16)
    {
      Bitmap notSelectedImage = new Bitmap(32 /*0x20*/, 16 /*0x10*/);
      using (Graphics graphics = Graphics.FromImage((Image) notSelectedImage))
        graphics.DrawImage((Image) notSelected16x16, 0, 0);
      return (Image) notSelectedImage;
    }
  }
}
