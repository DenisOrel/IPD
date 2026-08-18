// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ObjectRevisionHistoryView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Helpers;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class ObjectRevisionHistoryView : DockControl
{
  private ActivitiesDescriptor _actDescr;
  private UniversalDescriptor _procDescr;
  public static readonly Guid revGuid = new Guid("DE3E2784-9283-46ab-92DE-A0EEDC4F0028");
  public static readonly Guid procGuid = new Guid("7AE152AB-8B1D-43f1-BE16-DACFD3C4C8D6");
  protected internal long AttachmentObjectID = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ActivitiesView objectsView;

  public ObjectRevisionHistoryView()
  {
    this.InitializeComponent();
    this.Closing += new CancelEventHandler(this.ObjectRevisionHistoryView_Closing);
  }

  private void ObjectRevisionHistoryView_Closing(object sender, CancelEventArgs e)
  {
    this.objectsView.Deactivate((IView) null);
  }

  /// <summary>используется в Процессах</summary>
  /// <param name="list">Список идентификаторов процессов</param>
  private void Initialize(List<long> list)
  {
    ServiceContainer services = new ServiceContainer();
    services.AddService(typeof (IViewState), (object) new ViewStateService());
    services.AddService(typeof (INotificationService), (object) BaseHolder.NotificationService);
    this._procDescr = new UniversalDescriptor(Holder.CategoryAttachmentsID, wfConsts.ProcessesTypeID, string.Empty, (IList) list);
    this._procDescr.OnGetDefaultColumns += new UniversalDescriptor.GetDefaultColumnsHandler(this.ProcessUsage_OnGetDefaultColumns);
    this.objectsView.Initialize((IDescriptor) this._procDescr, (System.IServiceProvider) services);
    this.objectsView.ProcessesFilterChanged += new EventHandler(this.objectsView_ProcessesFilterChanged);
    this.objectsView.AddProcessesFilterButtons();
    this.objectsView.Activate((IView) null);
  }

  private NodeColumnCollection ProcessUsage_OnGetDefaultColumns()
  {
    NodeColumnCollection defaultColumns = new NodeColumnCollection();
    Guid columnSchemeGuid1 = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    Guid columnSchemeGuid2 = Intermech.Navigator.Consts.ObjectColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ApplicationServices.Container.GetService(typeof (IColumnSchemes));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid1, (object) ObligatoryObjectAttributes.F_OBJECT_ID));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid1, (object) ObligatoryObjectAttributes.CAPTION));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrActivityStatusID));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrPriorityID));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrDescriptionID));
    NodeColumn column = service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrStartedID);
    column.SortOrder = NodeColumnSortOrder.Ascending;
    column.SortIndex = 0;
    defaultColumns.Add(column);
    return defaultColumns;
  }

  private void objectsView_ProcessesFilterChanged(object sender, EventArgs e)
  {
    List<int> selectedProcessStatuses = this.objectsView.GetSelectedProcessStatuses();
    if (this.objectsView.GetII && this.AttachmentObjectID > 0L)
    {
      List<long> longList = new List<long>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        List<long> parentIis = wfFunx.GetParentIIs(sessionKeeper.Session, this.AttachmentObjectID);
        if (parentIis.Count <= 0)
          return;
        parentIis.Add(this.AttachmentObjectID);
        List<long> objectIDs = new List<long>();
        ConditionStructure[] conds = new ConditionStructure[1]
        {
          new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.NotEmpty, (object) 0, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object)
        };
        ColumnDescriptor[] columns = new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) wfConsts.AttrProcessID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0)
        };
        DataTable dataTable = new DataTable();
        foreach (long ObjectID in parentIis)
        {
          DataTable attachmentUsage = AttachmentFuncs.GetAttachmentUsage(sessionKeeper.Session, ObjectID, conds, columns);
          dataTable.Merge(attachmentUsage);
        }
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (!objectIDs.Contains(int64))
            objectIDs.Add(int64);
        }
        UniversalDescriptor rootDescriptor = new UniversalDescriptor(Holder.CategoryAttachmentsID, wfConsts.ProcessesTypeID, string.Empty, (IList) objectIDs);
        rootDescriptor.OnGetDefaultColumns += new UniversalDescriptor.GetDefaultColumnsHandler(this.ProcessUsage_OnGetDefaultColumns);
        if (selectedProcessStatuses != null)
        {
          if (selectedProcessStatuses.Count > 0)
            rootDescriptor.AdditionalConditions = new ConditionStructure[1]
            {
              new ConditionStructure(wfConsts.AttrActivityStatusID, RelationalOperators.In, (object) selectedProcessStatuses.ToArray(), LogicalOperators.NONE, 0, false)
            };
          else
            rootDescriptor.AdditionalConditions = new ConditionStructure[1]
            {
              new ConditionStructure(-2, RelationalOperators.Equal, (object) 0, LogicalOperators.NONE, 0, false)
            };
        }
        else
          rootDescriptor.AdditionalConditions = (ConditionStructure[]) null;
        this.objectsView.Initialize((IDescriptor) rootDescriptor, (System.IServiceProvider) this.objectsView.Services);
        this.objectsView.Activate((IView) null);
      }
    }
    else
    {
      if (selectedProcessStatuses != null)
      {
        if (selectedProcessStatuses.Count > 0)
          this._procDescr.AdditionalConditions = new ConditionStructure[1]
          {
            new ConditionStructure(wfConsts.AttrActivityStatusID, RelationalOperators.In, (object) selectedProcessStatuses.ToArray(), LogicalOperators.NONE, 0, false)
          };
        else
          this._procDescr.AdditionalConditions = new ConditionStructure[1]
          {
            new ConditionStructure(-2, RelationalOperators.Equal, (object) 0, LogicalOperators.NONE, 0, false)
          };
      }
      else
        this._procDescr.AdditionalConditions = (ConditionStructure[]) null;
      this.objectsView.Initialize((IDescriptor) this._procDescr, (System.IServiceProvider) this.objectsView.Services);
      this.objectsView.Activate((IView) null);
    }
  }

  private void Initialize(long objID)
  {
    ServiceContainer services = new ServiceContainer();
    services.AddService(typeof (IViewState), (object) new ViewStateService());
    services.AddService(typeof (INotificationService), (object) BaseHolder.NotificationService);
    this.PersistString = objID.ToString();
    this.AttachmentObjectID = objID;
    this._actDescr = new ActivitiesDescriptor(Holder.CategoryAttachmentsID, wfConsts.ActivitiesTypeID, string.Empty, objID);
    this.objectsView.Initialize((IDescriptor) this._actDescr, (System.IServiceProvider) services);
    this.objectsView.ActivitiesFilterChanged += new EventHandler(this.objectsView_ActivitiesFilterChanged);
    this.objectsView.AddActivitiesFilterButtons();
    this.objectsView.Activate((IView) null);
  }

  public static DockControl ShowRevisionHistory(string persistString)
  {
    try
    {
      return ObjectRevisionHistoryView.ShowRevisionHistory(Convert.ToInt64(persistString));
    }
    catch (Exception ex)
    {
      if (ApplicationServices.Container.GetService(typeof (IOutputView)) is IOutputView service)
        service.WriteString("Ошибки", "При загрузке истории утверждения объекта произошла ошибка: " + ex.Message);
      return (DockControl) null;
    }
  }

  private static string GetDisplayName(IDBObject obj)
  {
    string displayName = obj.Caption;
    if (displayName.Trim() == "")
      displayName = obj.NameInMessages;
    return displayName;
  }

  public static DockControl ShowRevisionHistory(long objID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objID, objID > 0L);
      if (dbObject == null)
      {
        objID *= -1L;
        dbObject = sessionKeeper.Session.GetObject(objID);
      }
      ObjectRevisionHistoryView revisionHistoryView = new ObjectRevisionHistoryView();
      revisionHistoryView.Text = LocalizationHolder.rm.GetString("Workflow.Design_81") + ObjectRevisionHistoryView.GetDisplayName(dbObject);
      revisionHistoryView.Guid = ObjectRevisionHistoryView.revGuid;
      revisionHistoryView.Initialize(objID);
      revisionHistoryView.Show((DockManager) ApplicationServices.Container.GetService(typeof (DockManager)));
      return (DockControl) revisionHistoryView;
    }
  }

  public static DockControl ShowProcesses(string persistString)
  {
    try
    {
      return ObjectRevisionHistoryView.ShowProcesses(Convert.ToInt64(persistString));
    }
    catch (Exception ex)
    {
      if (ApplicationServices.Container.GetService(typeof (IOutputView)) is IOutputView service)
        service.WriteString("Ошибки", "При загрузке использования в процессах объекта произошла ошибка: " + ex.Message);
      return (DockControl) null;
    }
  }

  public static DockControl ShowProcesses(long objID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objID, objID > 0L);
      if (dbObject == null)
      {
        objID *= -1L;
        dbObject = sessionKeeper.Session.GetObject(objID);
      }
      List<long> list = new List<long>();
      ConditionStructure[] conds = new ConditionStructure[1]
      {
        new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.NotEmpty, (object) 0, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object)
      };
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) wfConsts.AttrProcessID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0)
      };
      foreach (DataRow row in (InternalDataCollectionBase) AttachmentFuncs.GetAttachmentUsage(sessionKeeper.Session, objID, conds, columns).Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        if (!list.Contains(int64))
          list.Add(int64);
      }
      ObjectRevisionHistoryView revisionHistoryView = new ObjectRevisionHistoryView();
      revisionHistoryView.Text = LocalizationHolder.rm.GetString("Workflow.Design_82") + ObjectRevisionHistoryView.GetDisplayName(dbObject);
      revisionHistoryView.Guid = ObjectRevisionHistoryView.procGuid;
      revisionHistoryView.PersistString = objID.ToString();
      revisionHistoryView.AttachmentObjectID = objID;
      revisionHistoryView.Initialize(list);
      revisionHistoryView.Show((DockManager) ApplicationServices.Container.GetService(typeof (DockManager)));
      return (DockControl) revisionHistoryView;
    }
  }

  private void objectsView_ActivitiesFilterChanged(object sender, EventArgs e)
  {
    List<int> selectedActivityTypes = this.objectsView.GetSelectedActivityTypes();
    this._actDescr.ActivityTypesFilter = selectedActivityTypes;
    if (this.objectsView.GetII && this.AttachmentObjectID > 0L)
    {
      List<long> objectIDs = new List<long>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        objectIDs = wfFunx.GetParentIIs(sessionKeeper.Session, this.AttachmentObjectID);
      if (objectIDs.Count <= 0)
        return;
      objectIDs.Add(this.AttachmentObjectID);
      this.objectsView.Initialize((IDescriptor) new ActivitiesDescriptor(Holder.CategoryAttachmentsID, wfConsts.ActivitiesTypeID, string.Empty, (IList) objectIDs)
      {
        ActivityTypesFilter = selectedActivityTypes
      }, (System.IServiceProvider) this.objectsView.Services);
      this.objectsView.Activate((IView) null);
    }
    else
    {
      this.objectsView.Initialize((IDescriptor) this._actDescr, (System.IServiceProvider) this.objectsView.Services);
      this.objectsView.Activate((IView) null);
    }
  }

  /// <summary>вернуть раздел справки для контрола</summary>
  public override string HelpID => "829";

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectRevisionHistoryView));
    this.objectsView = new ActivitiesView();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.objectsView, "objectsView");
    this.objectsView.AllowCustomGroupValues = true;
    this.objectsView.Control = (object) this.objectsView;
    this.objectsView.DisableCheckedOutColumn = true;
    this.objectsView.DisableFiltration = true;
    this.objectsView.DisableKeyDownEvents = false;
    this.objectsView.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.objectsView.Name = "objectsView";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.objectsView);
    this.Name = nameof (ObjectRevisionHistoryView);
    this.ResumeLayout(false);
  }
}
