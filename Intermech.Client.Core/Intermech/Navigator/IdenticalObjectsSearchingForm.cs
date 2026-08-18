
// Type: Intermech.Navigator.IdenticalObjectsSearchingForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator;

/// <summary>
/// Форма, для удаления объектов с одинаковым значением выбранного атрибута
/// </summary>
public class IdenticalObjectsSearchingForm : Form
{
  /// <summary>ID выбранного типа атрибута</summary>
  private readonly int selectedAttrID;
  /// <summary>Таблица с информацией об объектах выбранного типа</summary>
  private readonly DataTable table;
  /// <summary>Текущий индекс для чтения из таблицы</summary>
  private int currentTableIndex;
  /// <summary>Полная версия объектов для отображения</summary>
  private readonly Dictionary<int, List<long>> extendedTypedIDs;
  /// <summary>Сгруппированная версия объектов для отображения</summary>
  private readonly Dictionary<int, List<long>> groupedTypedIDs;
  /// <summary>Полная версия списка идентификаторов объектов</summary>
  private readonly List<long> extendedObjectIDs;
  /// <summary>Сгруппированный список идентификаторов объектов</summary>
  private readonly Dictionary<long, List<long>> groupedObjectIDs;
  /// <summary>
  /// Определяет, обновляется ли вьюшка при перегруппировке объектов
  /// </summary>
  private bool isTypedIDsNeedToBeChanged;
  /// <summary>True - если текущая загрузка вьюшки - первая</summary>
  private bool isFirstLoading;
  /// <summary>Отображаемые колонки</summary>
  private NodeColumnCollection columns;
  /// <summary>
  /// Определяет, надо ли вообще показывать эту форму. false - если не найдено подходящих объектов для отображения.
  /// </summary>
  public static bool IsNeedToBeShown;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnDelete;
  private Button btnSkip;
  private Button btnCancel;
  private Button btnSelectAll;
  private Button btnDeselectAll;
  private Panel panel1;
  private Panel panel2;
  private CheckBox cbGrouping;
  private Panel panel3;
  private ObjectsViewBase objectsViewBase1;

  public sealed override string Text
  {
    get => base.Text;
    set => base.Text = value;
  }

  public IdenticalObjectsSearchingForm(DataTable table, int selectedAttrID)
  {
    this.InitializeComponent();
    this.objectsViewBase1.DisableFiltration = true;
    this.objectsViewBase1.DisableDoubleClicks = true;
    this.objectsViewBase1.DisableIMContextMenu = true;
    this.objectsViewBase1.ViewContentType = ContentType.NonFolders;
    this.Text += MetaDataHelper.GetAttributeTypeName(selectedAttrID);
    this.cbGrouping.CheckState = CheckState.Checked;
    this.table = table;
    this.selectedAttrID = selectedAttrID;
    this.currentTableIndex = 0;
    this.extendedTypedIDs = new Dictionary<int, List<long>>();
    this.groupedTypedIDs = new Dictionary<int, List<long>>();
    this.extendedObjectIDs = new List<long>();
    this.groupedObjectIDs = new Dictionary<long, List<long>>();
    this.isTypedIDsNeedToBeChanged = true;
    this.isFirstLoading = true;
    this.objectsViewBase1_Load();
    INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
    if (service == null)
      return;
    service.Subscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.NotificationEventFired));
    service.Subscribe("ObjectsCheckedIn", new NotificationEventHandler(this.NotificationEventFired));
    service.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.NotificationEventFired));
  }

  /// <summary>Загрузка вьюшки</summary>
  private void objectsViewBase1_Load()
  {
    if (this.isTypedIDsNeedToBeChanged)
      this.SetObjectsToDisplay();
    if (this.extendedTypedIDs.Count > 0)
    {
      this.objectsViewBase1.Initialize(this.cbGrouping.CheckState != CheckState.Checked ? (IDescriptor) new DictDescriptor(Consts.CategoryAllObjectTypes, 0, string.Empty, this.extendedTypedIDs) : (IDescriptor) new DictDescriptor(Consts.CategoryAllObjectTypes, 0, string.Empty, this.groupedTypedIDs), (System.IServiceProvider) this.objectsViewBase1.Services);
      this.objectsViewBase1.Activate((IView) null);
      this.SetIDColumn();
      this.isTypedIDsNeedToBeChanged = false;
      IdenticalObjectsSearchingForm.IsNeedToBeShown = true;
      this.isFirstLoading = false;
    }
    else
    {
      IdenticalObjectsSearchingForm.IsNeedToBeShown = false;
      this.objectsViewBase1.Deactivate((IView) null);
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1628"), LocalizationHolder.rm.GetString("Client.Core_1261"), MessageBoxButtons.OK);
      this.Close();
    }
  }

  /// <summary>Изменение состояния чекбокса "Группировать объекты"</summary>
  private void cbGrouping_CheckStateChanged(object sender, EventArgs e) => this.ReloadView();

  /// <summary>Изменились выделенные элементы в гриде</summary>
  private void objectsViewBase1_SelectedItemsChanged(object sender, EventArgs e)
  {
    if (!this.btnDelete.Enabled && this.objectsViewBase1._gridSelectedItems.Count != 0)
      this.btnDelete.Enabled = true;
    if (this.objectsViewBase1._gridSelectedItems.Count != 0)
      return;
    this.btnDelete.Enabled = false;
  }

  /// <summary>Обработка уведомлений</summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The <see cref="T:Intermech.Interfaces.Client.NotificationEventArgs" /> instance containing the event data.</param>
  public void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    switch (e)
    {
      case DBObjectsCheckOutEventArgs _ when e.EventName == "ObjectsCheckedOut":
        DBObjectsCheckOutEventArgs checkOutEventArgs = (DBObjectsCheckOutEventArgs) e;
        this.UpdateObjectIDsInView(checkOutEventArgs.ObjectIDs, checkOutEventArgs.NewObjectIDs);
        break;
      case DBObjectsEventArgs _ when e.EventName == "ObjectsCheckedIn" || e.EventName == "ObjectsChangesCancelled":
        this.UpdateObjectIDsInView(((DBObjectsEventArgs) e).ObjectIDs, (IList<long>) null);
        break;
    }
  }

  /// <summary>Нажатие кнопки "Отметить все объекты"</summary>
  private void btnSelectAll_Click(object sender, EventArgs e)
  {
    this.objectsViewBase1._grid.PerformAction(iGActions.SelectAll);
    this.objectsViewBase1._grid.Focus();
  }

  /// <summary>Нажатие кнопки "Снять все отметки"</summary>
  private void btnDeselectAll_Click(object sender, EventArgs e)
  {
    this.objectsViewBase1._grid.PerformAction(iGActions.DeselectAll);
    if (this.objectsViewBase1._grid.Rows.Count <= 1)
      return;
    this.btnDelete.Enabled = false;
  }

  /// <summary>Нажатие кнопки "Удалить"</summary>
  private void btnDelete_Click(object sender, EventArgs e)
  {
    if (this.objectsViewBase1._gridSelectedItems.Count == this.objectsViewBase1._grid.Rows.Count)
      this.DeleteAllObjects();
    else
      this.DeleteSelectedObjects();
    this.isTypedIDsNeedToBeChanged = true;
    this.ReloadView();
  }

  /// <summary>Нажатие кнопки "Пропустить"</summary>
  private void btnSkip_Click(object sender, EventArgs e)
  {
    this.isTypedIDsNeedToBeChanged = true;
    this.ReloadView();
  }

  /// <summary>Нажатие кнопки "Отмена"</summary>
  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.objectsViewBase1.Deactivate((IView) null);
    this.Close();
  }

  /// <summary>Перезагрузить вьюшку</summary>
  private void ReloadView()
  {
    this.columns = this.objectsViewBase1.GetNodeColumns();
    this.objectsViewBase1.Deactivate((IView) null);
    this.objectsViewBase1_Load();
  }

  /// <summary>Располагает в гриде колонку с F_ID</summary>
  private void SetIDColumn()
  {
    this.columns = this.objectsViewBase1.GetNodeColumns();
    if (this.isFirstLoading && !this.columns.ColumnIDExists((object) ObligatoryObjectAttributes.F_ID))
    {
      IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
      if (service != null)
      {
        NodeColumn column = service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_ID);
        int index = this.columns.FindIndex((Predicate<NodeColumn>) (item => (int) item.ID == -2));
        if (index != -1)
          this.columns.Insert(index + 1, column);
        else
          this.columns.Add(column);
      }
    }
    this.objectsViewBase1.SetColumns(this.columns, true);
  }

  /// <summary>Находит объект, в который происходит объединение</summary>
  /// <param name="objectsIDs">ObjectID/ID выделенных в гриде объектов.</param>
  /// <returns>Объект, в который происходит объединение</returns>
  private long FindToObjectID(Dictionary<long, long> selectedObjects)
  {
    long toObjectId = -1;
    List<long> source = this.cbGrouping.CheckState != CheckState.Unchecked ? this.GetShowingObjectIds().Except<long>((IEnumerable<long>) selectedObjects.Keys).ToList<long>() : this.extendedObjectIDs.Except<long>((IEnumerable<long>) selectedObjects.Keys).ToList<long>();
    if (source.Count != 0)
      toObjectId = source.First<long>();
    return toObjectId;
  }

  /// <summary>Собирает ИД отображаемых в гриде объектов</summary>
  /// <returns>ИД отображаемых в гриде объектов</returns>
  private List<long> GetShowingObjectIds()
  {
    List<long> showingObjectIds = new List<long>();
    foreach (KeyValuePair<int, List<long>> groupedTypedId in this.groupedTypedIDs)
      showingObjectIds.AddRange((IEnumerable<long>) groupedTypedId.Value);
    return showingObjectIds;
  }

  /// <summary>Собирает ObjectID/ID выделенных в гриде элементов</summary>
  /// <returns>ObjectID/ID выделенных в гриде элементов</returns>
  private Dictionary<long, long> GetSelectedObjects()
  {
    Dictionary<long, long> selectedObjects = new Dictionary<long, long>();
    ChildrenViewSelectedItems gridSelectedItems = this.objectsViewBase1._gridSelectedItems;
    for (int index = 0; index < gridSelectedItems.Count; ++index)
    {
      if (gridSelectedItems.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        selectedObjects.Add(itemData.ObjectID, itemData.ID);
    }
    return selectedObjects;
  }

  /// <summary>
  /// Удаляет все отображенные в гриде объекты (входящие в состав сгруппированных тоже)
  /// </summary>
  private void DeleteAllObjects()
  {
    if (this.cbGrouping.CheckState == CheckState.Checked)
    {
      this.objectsViewBase1.Deactivate((IView) null);
      this.objectsViewBase1.Initialize((IDescriptor) new DictDescriptor(Consts.CategoryAllObjectTypes, 0, string.Empty, this.extendedTypedIDs), (System.IServiceProvider) this.objectsViewBase1.Services);
      this.objectsViewBase1.Activate((IView) null);
      this.objectsViewBase1._grid.PerformAction(iGActions.SelectAll);
    }
    ObjectCommands.DeleteCommand((ISelectedItems) this.objectsViewBase1._gridSelectedItems, (System.IServiceProvider) this.objectsViewBase1.Services, (object) null);
  }

  /// <summary>Удаляет выбранные объекты</summary>
  private void DeleteSelectedObjects()
  {
    Dictionary<long, long> selectedObjects = this.GetSelectedObjects();
    long toObjectId = this.FindToObjectID(selectedObjects);
    if (toObjectId == -1L)
      return;
    this.CombineObjects(selectedObjects, toObjectId);
  }

  /// <summary>Объединяет выбранные объекты в одну версию toObjectID</summary>
  /// <param name="selectedObjectIDs">Выделенные в гриде версии объектов.</param>
  /// <param name="toObjectID">ID версии объекта, в которую нужно перекинуть все ссылки и связи указанных объектов</param>
  private void CombineObjects(Dictionary<long, long> selectedObjects, long toObjectID)
  {
    List<long> deletingObjectIds = this.GetDeletingObjectIDs(selectedObjects);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        return;
      customService.CombineObjects(sessionKeeper.Session.SessionGUID, deletingObjectIds.ToArray(), toObjectID);
      if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
        return;
      service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) deletingObjectIds));
    }
  }

  /// <summary>Формирует список объектов для удаления</summary>
  /// <param name="selectedObjectIDs">ObjectID/ID выделенных в гриде объектов</param>
  /// <returns>Список ObjectID объектов для удаления</returns>
  private List<long> GetDeletingObjectIDs(Dictionary<long, long> selectedObjects)
  {
    if (this.cbGrouping.CheckState == CheckState.Unchecked)
      return selectedObjects.Keys.ToList<long>();
    List<long> first = new List<long>();
    foreach (KeyValuePair<long, long> selectedObject in selectedObjects)
    {
      List<long> second;
      if (this.groupedObjectIDs.TryGetValue(selectedObject.Value, out second))
        first = first.Union<long>((IEnumerable<long>) second).ToList<long>();
    }
    return first;
  }

  /// <summary>Формирует словарики с объектами для отображения.</summary>
  /// <returns></returns>
  private void SetObjectsToDisplay()
  {
    this.extendedTypedIDs.Clear();
    this.groupedTypedIDs.Clear();
    this.extendedObjectIDs.Clear();
    this.groupedObjectIDs.Clear();
    if (this.table.Rows.Count == 0 || this.currentTableIndex == this.table.Rows.Count)
      return;
    string str = Convert.ToString(this.table.Rows[this.currentTableIndex][this.selectedAttrID.ToString()]);
    long int64_1 = Convert.ToInt64(this.table.Rows[this.currentTableIndex][-3.ToString()]);
    int int32_1 = Convert.ToInt32(this.table.Rows[this.currentTableIndex][-7.ToString()]);
    Dictionary<long, int> dictionary = new Dictionary<long, int>();
    int num = 0;
    for (int currentTableIndex = this.currentTableIndex; currentTableIndex < this.table.Rows.Count; ++currentTableIndex)
    {
      int int32_2 = Convert.ToInt32(this.table.Rows[currentTableIndex][-3.ToString()]);
      int int32_3 = Convert.ToInt32(this.table.Rows[currentTableIndex][-7.ToString()]);
      if (str.Equals(Convert.ToString(this.table.Rows[currentTableIndex][this.selectedAttrID.ToString()])))
      {
        List<long> longList1;
        if (!this.extendedTypedIDs.TryGetValue(int32_3, out longList1))
        {
          longList1 = new List<long>();
          this.extendedTypedIDs.Add(int32_3, longList1);
        }
        long int64_2 = Convert.ToInt64(this.table.Rows[currentTableIndex][-2.ToString()]);
        longList1.Add(int64_2);
        this.extendedObjectIDs.Add(int64_2);
        if (int32_3 != int32_1 || (long) int32_2 != int64_1)
        {
          long versionToDisplay = this.FindObjectVersionToDisplay(dictionary, int64_1);
          List<long> longList2;
          if (!this.groupedTypedIDs.TryGetValue(int32_1, out longList2))
          {
            longList2 = new List<long>();
            this.groupedTypedIDs.Add(int32_1, longList2);
          }
          longList2.Add(versionToDisplay);
          this.groupedObjectIDs.Add(int64_1, dictionary.Keys.ToList<long>());
          ++num;
          int64_1 = Convert.ToInt64(this.table.Rows[this.currentTableIndex][-3.ToString()]);
          int32_1 = Convert.ToInt32(this.table.Rows[this.currentTableIndex][-7.ToString()]);
          dictionary.Clear();
        }
        dictionary.Add(int64_2, Convert.ToInt32(this.table.Rows[currentTableIndex][-16.ToString()]));
        if (currentTableIndex + 1 == this.table.Rows.Count)
        {
          long versionToDisplay = this.FindObjectVersionToDisplay(dictionary, (long) int32_2);
          List<long> longList3;
          if (!this.groupedTypedIDs.TryGetValue(int32_3, out longList3))
          {
            longList3 = new List<long>();
            this.groupedTypedIDs.Add(int32_1, longList3);
          }
          longList3.Add(versionToDisplay);
          this.groupedObjectIDs.Add((long) int32_2, dictionary.Keys.ToList<long>());
          ++num;
          if (num == 1)
          {
            this.extendedTypedIDs.Clear();
            this.groupedTypedIDs.Clear();
            this.extendedObjectIDs.Clear();
            this.groupedObjectIDs.Clear();
          }
        }
        ++this.currentTableIndex;
      }
      else
      {
        if (dictionary.Any<KeyValuePair<long, int>>())
        {
          long versionToDisplay = this.FindObjectVersionToDisplay(dictionary, int64_1);
          List<long> longList;
          if (!this.groupedTypedIDs.TryGetValue(int32_1, out longList))
          {
            longList = new List<long>();
            this.groupedTypedIDs.Add(int32_1, longList);
          }
          longList.Add(versionToDisplay);
          this.groupedObjectIDs.Add(int64_1, dictionary.Keys.ToList<long>());
          ++num;
        }
        if (num > 1)
          break;
        this.extendedTypedIDs.Clear();
        this.groupedTypedIDs.Clear();
        this.extendedObjectIDs.Clear();
        this.groupedObjectIDs.Clear();
        str = Convert.ToString(this.table.Rows[this.currentTableIndex][this.selectedAttrID.ToString()]);
        int64_1 = Convert.ToInt64(this.table.Rows[this.currentTableIndex][-3.ToString()]);
        int32_1 = Convert.ToInt32(this.table.Rows[this.currentTableIndex][-7.ToString()]);
        dictionary.Clear();
        num = 0;
        --currentTableIndex;
      }
    }
  }

  /// <summary>
  /// Отбирает из версий объекта версию для отображения.
  /// По правилам подбора, базовую или первую попавшуюся.
  /// </summary>
  /// <param name="objectVersions">Версии объекта с признаком базовости</param>
  /// <returns>Версия объекта для отображения.</returns>
  private long FindObjectVersionToDisplay(Dictionary<long, int> objectVersions, long ID)
  {
    long objectByVersionRule = IdenticalObjectsSearchingForm.FindObjectByVersionRule(ID);
    if (objectByVersionRule != 0L && objectVersions.ContainsKey(objectByVersionRule))
      return objectByVersionRule;
    long baseVersion = IdenticalObjectsSearchingForm.FindBaseVersion(objectVersions);
    return baseVersion != 0L ? baseVersion : objectVersions.Keys.First<long>();
  }

  /// <summary>
  /// Находит версию, подобранную по текущим правилам подбора
  /// </summary>
  /// <param name="objectVersions">Версии объекта с признаком базовости</param>
  /// <returns>ИД версии по текущим правилам подбора или 0, если таковой нет в словаре</returns>
  private static long FindObjectByVersionRule(long ID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IFiltrationService service = (IFiltrationService) ServicesManager.GetService(typeof (IFiltrationService));
      if (service == null)
        return 0;
      IDBObject objectByVersionsRule = sessionKeeper.Session.GetObjectByVersionsRule(ID, service.Filtration.OwnerID, false);
      return objectByVersionsRule == null ? 0L : objectByVersionsRule.ObjectID;
    }
  }

  /// <summary>Находит базовую версию в списке версий</summary>
  /// <param name="versionsList">Версии объекта с признаком базовости</param>
  /// <returns>ИД базовой версии, или 0, если базовой версии нет в словаре.</returns>
  private static long FindBaseVersion(Dictionary<long, int> objectVersions)
  {
    foreach (KeyValuePair<long, int> objectVersion in objectVersions)
    {
      if (objectVersion.Value == 1)
        return objectVersion.Key;
    }
    return 0;
  }

  /// <summary>Обновляет вьюшку с учетом изменившихся ИД объектов</summary>
  /// <param name="oldIDs">Список старых ИД.</param>
  /// <param name="newIDs">Список новых ИД.</param>
  private void UpdateObjectIDsInView(IList<long> oldIDs, IList<long> newIDs)
  {
    bool flag = false;
    for (int index1 = 0; index1 < this.extendedObjectIDs.Count; ++index1)
    {
      int index2 = oldIDs.IndexOf(this.extendedObjectIDs[index1]);
      if (index2 != -1)
      {
        if (newIDs != null)
        {
          this.SwapIDsInDictionary(this.extendedObjectIDs[index1], newIDs[index2], this.extendedTypedIDs);
          this.SwapIDsInDictionary(this.extendedObjectIDs[index1], newIDs[index2], this.groupedTypedIDs);
          this.SwapIDsInDictionary(this.extendedObjectIDs[index1], newIDs[index2], this.groupedObjectIDs);
          this.extendedObjectIDs[index1] = newIDs[index2];
        }
        else
        {
          this.SwapIDsInDictionary(this.extendedObjectIDs[index1], -this.extendedObjectIDs[index1], this.extendedTypedIDs);
          this.SwapIDsInDictionary(this.extendedObjectIDs[index1], -this.extendedObjectIDs[index1], this.groupedTypedIDs);
          this.SwapIDsInDictionary(this.extendedObjectIDs[index1], -this.extendedObjectIDs[index1], this.groupedObjectIDs);
          this.extendedObjectIDs[index1] = -this.extendedObjectIDs[index1];
        }
        flag = true;
      }
    }
    if (!flag)
      return;
    this.isTypedIDsNeedToBeChanged = false;
    this.ReloadView();
  }

  /// <summary>
  /// Меняет в словаре старый идентификатор объекта на новый
  /// </summary>
  /// <param name="oldID">Старый ID.</param>
  /// <param name="newID">Новый ID.</param>
  private void SwapIDsInDictionary(long oldID, long newID, Dictionary<int, List<long>> dictionary)
  {
    foreach (KeyValuePair<int, List<long>> keyValuePair in dictionary)
    {
      List<long> longList = keyValuePair.Value;
      int index = longList.IndexOf(oldID);
      if (index != -1)
        longList[index] = newID;
    }
  }

  /// <summary>
  /// Меняет в словаре старый идентификатор объекта на новый
  /// </summary>
  /// <param name="oldID">Старый ID.</param>
  /// <param name="newID">Новый ID.</param>
  private void SwapIDsInDictionary(long oldID, long newID, Dictionary<long, List<long>> dictionary)
  {
    foreach (KeyValuePair<long, List<long>> keyValuePair in dictionary)
    {
      List<long> longList = keyValuePair.Value;
      int index = longList.IndexOf(oldID);
      if (index != -1)
        longList[index] = newID;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
    if (service != null)
    {
      service.Unsubscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.NotificationEventFired));
      service.Unsubscribe("ObjectsCheckedIn", new NotificationEventHandler(this.NotificationEventFired));
      service.Unsubscribe("ObjectsCheckedOut", new NotificationEventHandler(this.NotificationEventFired));
    }
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
    this.btnDelete = new Button();
    this.btnSkip = new Button();
    this.btnCancel = new Button();
    this.btnSelectAll = new Button();
    this.btnDeselectAll = new Button();
    this.panel1 = new Panel();
    this.panel2 = new Panel();
    this.cbGrouping = new CheckBox();
    this.panel3 = new Panel();
    this.objectsViewBase1 = new ObjectsViewBase();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    this.btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnDelete.Location = new Point(323, 43);
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Size = new Size(150, 34);
    this.btnDelete.TabIndex = 1;
    this.btnDelete.Text = "Удалить";
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.btnSkip.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnSkip.Location = new Point(488, 43);
    this.btnSkip.Name = "btnSkip";
    this.btnSkip.Size = new Size(133, 34);
    this.btnSkip.TabIndex = 2;
    this.btnSkip.Text = "Пропустить";
    this.btnSkip.UseVisualStyleBackColor = true;
    this.btnSkip.Click += new EventHandler(this.btnSkip_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(636, 43);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(122, 34);
    this.btnCancel.TabIndex = 3;
    this.btnCancel.Text = "Прервать";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnSelectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btnSelectAll.Location = new Point(12, 43);
    this.btnSelectAll.Name = "btnSelectAll";
    this.btnSelectAll.Size = new Size(110, 34);
    this.btnSelectAll.TabIndex = 4;
    this.btnSelectAll.Text = "Отметить все объекты";
    this.btnSelectAll.UseVisualStyleBackColor = true;
    this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
    this.btnDeselectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btnDeselectAll.Location = new Point(140, 43);
    this.btnDeselectAll.Name = "btnDeselectAll";
    this.btnDeselectAll.Size = new Size(109, 34);
    this.btnDeselectAll.TabIndex = 5;
    this.btnDeselectAll.Text = "Снять все отметки";
    this.btnDeselectAll.UseVisualStyleBackColor = true;
    this.btnDeselectAll.Click += new EventHandler(this.btnDeselectAll_Click);
    this.panel1.Controls.Add((Control) this.panel2);
    this.panel1.Controls.Add((Control) this.btnDelete);
    this.panel1.Controls.Add((Control) this.btnSkip);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 391);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(770, 86);
    this.panel1.TabIndex = 7;
    this.panel2.Controls.Add((Control) this.cbGrouping);
    this.panel2.Controls.Add((Control) this.btnDeselectAll);
    this.panel2.Controls.Add((Control) this.btnSelectAll);
    this.panel2.Dock = DockStyle.Left;
    this.panel2.Location = new Point(0, 0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(294, 86);
    this.panel2.TabIndex = 8;
    this.cbGrouping.AutoSize = true;
    this.cbGrouping.Checked = true;
    this.cbGrouping.CheckState = CheckState.Checked;
    this.cbGrouping.Location = new Point(12, 3);
    this.cbGrouping.Name = "cbGrouping";
    this.cbGrouping.Size = new Size(158, 17);
    this.cbGrouping.TabIndex = 7;
    this.cbGrouping.Text = "Группировать по версиям";
    this.cbGrouping.UseVisualStyleBackColor = true;
    this.cbGrouping.CheckStateChanged += new EventHandler(this.cbGrouping_CheckStateChanged);
    this.panel3.Controls.Add((Control) this.objectsViewBase1);
    this.panel3.Dock = DockStyle.Fill;
    this.panel3.Location = new Point(0, 0);
    this.panel3.Name = "panel3";
    this.panel3.Size = new Size(770, 391);
    this.panel3.TabIndex = 9;
    this.objectsViewBase1.AllowCustomGroupValues = true;
    this.objectsViewBase1.AllowEditing = true;
    this.objectsViewBase1.AutoScroll = true;
    this.objectsViewBase1.Control = (object) this.objectsViewBase1;
    this.objectsViewBase1.DisableKeyDownEvents = false;
    this.objectsViewBase1.Dock = DockStyle.Fill;
    this.objectsViewBase1.EditingMode = false;
    this.objectsViewBase1.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.objectsViewBase1.Font = new Font("Tahoma", 8.25f);
    this.objectsViewBase1.Location = new Point(0, 0);
    this.objectsViewBase1.Name = "objectsViewBase1";
    this.objectsViewBase1.Size = new Size(770, 391);
    this.objectsViewBase1.TabIndex = 9;
    this.objectsViewBase1.ViewContentType = ContentType.NonFolders;
    this.objectsViewBase1.SelectedItemsChanged += new EventHandler(this.objectsViewBase1_SelectedItemsChanged);
    this.AcceptButton = (IButtonControl) this.btnDelete;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(770, 477);
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel1);
    this.MinimumSize = new Size(760, 303);
    this.Name = nameof (IdenticalObjectsSearchingForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Объекты с одинаковым значением атрибута ";
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
