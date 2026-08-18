// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.TableEditView
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.DataFormats;
using Intermech.Expert.Table;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Вьюшка для редактирования таблиц</summary>
[ViewDescriptionProvider(typeof (TableEditView.TableEditViewDescriptionProvider))]
public class TableEditView : UserControl, IView
{
  private INotificationService _notificationService;
  private long _objectID = -1;
  private string _caption = string.Empty;
  private bool _firstRun;
  private bool _activeView;
  private TableEditControl _control;

  public TableEditView()
  {
    this._notificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this._notificationService.Subscribe(new NotificationEventHandler(this.NotifyEvent));
  }

  /// <summary>Индекс изображения</summary>
  public int ImageIndex => -1;

  /// <summary>Порядок вьшки</summary>
  public int OrderID => 0;

  /// <summary>Заголовок</summary>
  public string Caption => LocalizationHolder.rm.GetString("Expert.Editor_27");

  /// <summary>Инициализация вьюшки</summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    this._firstRun = true;
  }

  /// <summary>Деактивация вьюшки</summary>
  /// <param name="nextView"></param>
  public void Deactivate(IView nextView)
  {
    if (this._control == null)
      return;
    this._control.Deactivate();
    this._activeView = false;
    if (!this._control.Modified)
      return;
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_28"), LocalizationHolder.rm.GetString("Expert.Editor_29"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals((object) DialogResult.Yes))
    {
      this._control.ApplyChanges();
      this._control_OnApplyChanges((object) this, EventArgs.Empty);
    }
    else
      this._control.RollbackChanges();
  }

  /// <summary>Активация вьюшки</summary>
  /// <param name="previousView"></param>
  public void Activate(IView previousView)
  {
    if (this._firstRun)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IExpertTable expertTable = sessionKeeper.Session.GetObject(this._objectID) as IExpertTable;
        expertTable.Load();
        this._caption = expertTable.Caption;
        eTableCollection eTableCollection = expertTable.LoadTableData();
        eTable[] tables = (eTable[]) null;
        if (eTableCollection != null)
        {
          tables = eTableCollection.Tables;
          string esName = expertTable.esName;
          foreach (eTable eTable in tables)
            eTable.Name = esName;
        }
        TempFormula cond = expertTable.Cond;
        if (this._control != null)
        {
          this._control.Parent = (Control) null;
          this._control.Dispose();
          this._control = (TableEditControl) null;
        }
        this._control = new TableEditControl(this._caption, tables, cond);
        this._control.ReadOnly = expertTable.ReadOnly;
        this._control.OnApplyChanges += new EventHandler(this._control_OnApplyChanges);
        this._control.Dock = DockStyle.Fill;
        this._control.Parent = (Control) this;
      }
      this._firstRun = false;
    }
    this._activeView = true;
    this._control.Activate();
  }

  private void _control_OnApplyChanges(object sender, EventArgs e)
  {
    TableEditView.Save(this._objectID, this._control.Tables, this._control.Formula);
    if (sender == null)
      return;
    (sender is TableEditControl ? (TableEditControl) sender : this._control).SaveOrigCopy(this._control.Tables);
  }

  /// <summary>Сохранить данные</summary>
  /// <param name="objectID">идентификатор объекта для сохранения</param>
  /// <param name="tables">список таблиц</param>
  /// <param name="formula">Условие на таблицу</param>
  public static void Save(long objectID, eTable[] tables, TempFormula formula)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IExpertTable expertTable = session.GetObject(objectID) as IExpertTable;
      if (tables == null || tables.Length == 0)
        return;
      eTable table1 = tables[0];
      expertTable.EntrysCount = (int) table1.TableType;
      eTableCollection tableCollection = new eTableCollection(tables);
      expertTable.SaveTableData(tableCollection);
      expertTable.Cond = formula;
      expertTable.SaveCondition();
      expertTable.esName = table1.Name;
      expertTable.LayersCount = tables.Length;
      expertTable.ColumnsCount = table1.ValuesTable.ColumnsCount;
      expertTable.RowsCount = table1.ValuesTable.RowsCount;
      ArrayList roles = new ArrayList();
      ArrayList comms = new ArrayList();
      foreach (eRow fixedRow in (IEnumerable<eRow>) table1.FixedRows)
      {
        if (fixedRow.Header != null)
          TableEditView.AddCellToList(table1, fixedRow.Header, comms, roles, AttributeRoles.argHorz);
        foreach (eCell cell in fixedRow)
          TableEditView.AddCellToList(table1, cell, comms, roles, AttributeRoles.argHorz);
      }
      foreach (eColumn fixedColumn in (IEnumerable<eColumn>) table1.FixedColumns)
      {
        if (fixedColumn.Header != null)
          TableEditView.AddCellToList(table1, fixedColumn.Header, comms, roles, AttributeRoles.argVert);
        foreach (eCell cell in fixedColumn)
          TableEditView.AddCellToList(table1, cell, comms, roles, AttributeRoles.argVert);
      }
      if (table1.TableType.Equals((object) eTableType.DoubleEntry))
      {
        foreach (eTable table2 in tables)
        {
          foreach (CommonTypeHolder commonTypeHolder in table2.Result)
          {
            comms.Add((object) commonTypeHolder);
            roles.Add((object) EnumTypeHelper.GetCaption((Enum) AttributeRoles.Result));
          }
        }
      }
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      foreach (CommonTypeHolder commonTypeHolder in comms)
      {
        arrayList1.Add((object) commonTypeHolder.AttributeType.Guid);
        arrayList2.Add((object) commonTypeHolder.ObjectType.Guid);
      }
      expertTable.Roles = (IList) roles;
      expertTable.AttributesList = (IList) arrayList1;
      expertTable.ObjectTypesList = (IList) arrayList2;
      ArrayList links = new ArrayList();
      foreach (eTable table3 in tables)
      {
        foreach (eRow fixedRow in (IEnumerable<eRow>) table3.FixedRows)
        {
          foreach (eCell eCell in fixedRow)
            TableEditView.AddLinkToList(eCell.CellValue, links);
        }
        foreach (eColumn fixedColumn in (IEnumerable<eColumn>) table3.FixedColumns)
        {
          foreach (eCell eCell in fixedColumn)
            TableEditView.AddLinkToList(eCell.CellValue, links);
        }
        for (int row = 0; row < table3.ValuesTable.RowsCount; ++row)
        {
          for (int column = 0; column < table3.ValuesTable.ColumnsCount; ++column)
            TableEditView.AddLinkToList(table3.ValuesTable[row, column].CellValue, links);
        }
      }
      expertTable.ObjectLinksList = (IList) links;
      IExpertServer customService = session.GetCustomService(typeof (IExpertServer)) as IExpertServer;
      byte[] traceInfo = (byte[]) null;
      bool flag = false;
      if (customService != null)
        flag = customService.ReflectObjUpdate(session.SessionGUID, expertTable.ObjectID, ExpertTraceFlags.None, (TempFormula) null, out traceInfo);
      if (!flag)
        return;
      using (RuleUpdateReport ruleUpdateReport = new RuleUpdateReport())
        ruleUpdateReport.Execute(traceInfo);
    }
  }

  private static void AddCellToList(
    eTable table,
    eCell cell,
    ArrayList comms,
    ArrayList roles,
    AttributeRoles defRole)
  {
    if (cell == null || cell.CommonType == null || comms.Contains((object) cell.CommonType))
      return;
    switch (cell.CellDestination)
    {
      case eCellDestination.Header:
        comms.Add((object) cell.CommonType);
        roles.Add((object) EnumTypeHelper.GetCaption((Enum) defRole));
        break;
      case eCellDestination.Result:
        comms.Add((object) cell.CommonType);
        if (table.TableType.Equals((object) eTableType.NoEntry))
        {
          roles.Add((object) EnumTypeHelper.GetCaption((Enum) AttributeRoles.argResult));
          break;
        }
        roles.Add((object) EnumTypeHelper.GetCaption((Enum) AttributeRoles.Result));
        break;
    }
  }

  private static void AddLinkToList(ExpertValue expValue, ArrayList links)
  {
    if (expValue == null)
      return;
    switch (expValue.ValueType)
    {
      case DataType.ObjectLink:
        long int64 = Convert.ToInt64(expValue.Value);
        if (links.Contains((object) int64))
          break;
        links.Add((object) int64);
        break;
      case DataType.Packet:
        PacketValue packetValue = expValue.Value as PacketValue;
        for (int index = 0; index < packetValue.Count; ++index)
          TableEditView.AddLinkToList(packetValue[index], links);
        break;
    }
  }

  private void NotifyEvent(object sender, NotificationEventArgs e)
  {
    if (sender != null && this._control != null && sender.Equals((object) this._control))
      return;
    DBObjectsEventArgs objectsEventArgs = e as DBObjectsEventArgs;
    DBObjectsCheckOutEventArgs checkOutEventArgs = e as DBObjectsCheckOutEventArgs;
    switch (e.EventName)
    {
      case "ObjectsChangesCancelled":
      case "ObjectsCheckedIn":
        if (objectsEventArgs == null || !objectsEventArgs.ObjectIDs.Contains(this._objectID))
          break;
        this._objectID = Math.Abs(this._objectID);
        this._firstRun = true;
        if (!this._activeView)
          break;
        if (this._control != null)
          this._control.Deactivate();
        this.Activate((IView) null);
        break;
      case "ObjectsCheckedOut":
        if (checkOutEventArgs == null || !checkOutEventArgs.ObjectIDs.Contains(this._objectID))
          break;
        int index = checkOutEventArgs.ObjectIDs.IndexOf(this._objectID);
        this._objectID = checkOutEventArgs.NewObjectIDs[index];
        this._firstRun = true;
        if (!this._activeView)
          break;
        if (this._control != null)
          this._control.Deactivate();
        this.Activate((IView) null);
        break;
    }
  }

  /// <summary>Dispose</summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this._control != null)
    {
      this._control.Parent = (Control) null;
      this._control.Dispose();
      this._control = (TableEditControl) null;
      this._notificationService.Unsubscribe(new NotificationEventHandler(this.NotifyEvent));
    }
    base.Dispose(disposing);
  }

  private sealed class TableEditViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Expert.Editor_27"),
        ImageIndex = -1,
        OrderID = 0
      };
    }
  }
}
