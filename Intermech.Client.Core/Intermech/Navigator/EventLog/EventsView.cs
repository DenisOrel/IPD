
// Type: Intermech.Navigator.EventLog.EventsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Views;
using Intermech.Search;
using Intermech.Search.EventLog;
using Intermech.Search.EventLogFilters;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.EventLog;

/// <summary>
/// Реализует закладку навигатора, предназначенную для промотра событий из журнала системы.
/// </summary>
[ViewDescriptionProvider(typeof (EventsView.EventsViewDescriptionProvider))]
public class EventsView : ChildrenView
{
  /// <summary>Индекс значка вьюшки в коллекции именованных значков</summary>
  public static int ViewIconIndex = -1;
  private LabelItem labelItem1;
  private ComboBoxItem _eventLogFiltersComboBoxItem;
  private ButtonItem _createFilterButtonItem;
  private ButtonItem _editFilterButtonItem;
  private ButtonItem _removeFilterButtonItem;
  private LabelItem labelItem2;
  private ComboBoxItem _eventLogComboBoxItem;
  private ButtonItem _setArchiveFromDateButtonItem;
  private ButtonItem _openXmlButtonItem;
  private ButtonItem _exportToExelButtonItem;
  /// <summary>Название потока для сохранения настроек вида</summary>
  public static string EventsViewStatesName = "EventsView_{7632AEBF-D5C3-4ED7-BFD5-D63839D1D688}";
  public EventsView.AddListItem myDelegate;
  private Guid EventLogPartGuid = new Guid("65D31552-5F7E-4d90-BB63-34CB72A68BA7");

  protected virtual bool ShowFiltersComboBox => true;

  public EventsView()
  {
    this.InitializeComponent();
    this._grid.GroupBox.Text = LocalizationHolder.rm.GetString("Client.Core_1340");
    if (EventsView.ViewIconIndex == -1 && ChildrenView._namedImageList != null)
    {
      using (MemoryStream memoryStream = EventsView.ClientCoreResourcesAccess.LoadResource("EventLog.ico"))
      {
        using (Icon icon = new Icon((Stream) memoryStream))
        {
          ChildrenView._namedImageList.Add(icon, "imgEventLogIcon");
          EventsView.ViewIconIndex = ChildrenView._namedImageList.ImageIndex("imgEventLogIcon");
        }
      }
    }
    this.AllowEditing = false;
    this.DisableParentSelectedItems = true;
    this.InitializeEventLogComboBox();
    if (this.ShowFiltersComboBox)
      return;
    this.labelItem1.Visible = false;
    this._eventLogFiltersComboBoxItem.Visible = false;
    this._createFilterButtonItem.Visible = false;
    this._editFilterButtonItem.Visible = false;
    this._removeFilterButtonItem.Visible = false;
  }

  protected override IServiceContainer GetMenuServiceContainer()
  {
    IServiceContainer serviceContainer = base.GetMenuServiceContainer();
    if (serviceContainer != null && serviceContainer.GetService(typeof (IEventLogProvider)) == null)
      serviceContainer.AddService(typeof (IEventLogProvider), (object) new EventsView.EventLogProvider(this));
    return serviceContainer;
  }

  /// <summary>
  /// Можно ли искать унаследованные настройки отображения "Навигатора" для закладки
  /// </summary>
  protected override bool UseInheritedNavViews
  {
    [DebuggerStepThrough] get => false;
    set => base.UseInheritedNavViews = false;
  }

  public override string StateStreamPrefix => EventsView.EventsViewStatesName;

  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_610");

  public override int ImageIndex => EventsView.ViewIconIndex;

  protected virtual bool ThumbnailCallback() => false;

  public static void EventLogExcelReport(string caption, iGrid grid)
  {
    if (grid == null)
      return;
    if (grid.SelectedCells == null || grid.SelectedCells.Count == 0)
    {
      int num1 = (int) MessageBox.Show("Не выбраны ячейки для экспорта данных", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      if (!(ServicesManager.GetService(typeof (ISimpleExcelReports)) is ISimpleExcelReports service))
        return;
      object obj1 = (object) null;
      try
      {
        obj1 = service.GetExcelInstance((object) null, caption);
        // ISSUE: reference to a compiler-generated field
        if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          EventsView.\u003C\u003Eo__26.\u003C\u003Ep__0 = CallSite<Action<CallSite, ISimpleExcelReports, object, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetVisible", (IEnumerable<System.Type>) null, typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        EventsView.\u003C\u003Eo__26.\u003C\u003Ep__0.Target((CallSite) EventsView.\u003C\u003Eo__26.\u003C\u003Ep__0, service, obj1, true);
        // ISSUE: reference to a compiler-generated field
        if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          EventsView.\u003C\u003Eo__26.\u003C\u003Ep__1 = CallSite<Func<CallSite, ISimpleExcelReports, object, string, string, string, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "CreateWorkbook", (IEnumerable<System.Type>) null, typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[6]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__1.Target((CallSite) EventsView.\u003C\u003Eo__26.\u003C\u003Ep__1, service, obj1, caption, caption, "", "");
        // ISSUE: reference to a compiler-generated field
        if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          EventsView.\u003C\u003Eo__26.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int, object> target1 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__4.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int, object>> p4 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__4;
        // ISSUE: reference to a compiler-generated field
        if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          EventsView.\u003C\u003Eo__26.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Item", typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object> target2 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object>> p3 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          EventsView.\u003C\u003Eo__26.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Worksheets", typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__2.Target((CallSite) EventsView.\u003C\u003Eo__26.\u003C\u003Ep__2, obj2);
        object obj4 = target2((CallSite) p3, obj3);
        object obj5 = target1((CallSite) p4, obj4, 1);
        int num2 = 1;
        for (int index = 0; index < grid.Cols.Count; ++index)
        {
          iGCol col = grid.Cols[index];
          if (col.Visible)
          {
            // ISSUE: reference to a compiler-generated field
            if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__7 == null)
            {
              // ISSUE: reference to a compiler-generated field
              EventsView.\u003C\u003Eo__26.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Value", typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, object, object> target3 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__7.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, object, object>> p7 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__7;
            // ISSUE: reference to a compiler-generated field
            if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__6 == null)
            {
              // ISSUE: reference to a compiler-generated field
              EventsView.\u003C\u003Eo__26.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, int, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, int, int, object> target4 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__6.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, int, int, object>> p6 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__6;
            // ISSUE: reference to a compiler-generated field
            if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__5 == null)
            {
              // ISSUE: reference to a compiler-generated field
              EventsView.\u003C\u003Eo__26.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Cells", typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj6 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__5.Target((CallSite) EventsView.\u003C\u003Eo__26.\u003C\u003Ep__5, obj5);
            int num3 = num2;
            object obj7 = target4((CallSite) p6, obj6, 1, num3);
            object text1 = col.Text;
            object obj8 = target3((CallSite) p7, obj7, text1);
            int num4 = 2;
            for (int rowIndex = 0; rowIndex < grid.Rows.Count; ++rowIndex)
            {
              iGCell cell = grid.Cells[rowIndex, index];
              if (cell.Selected)
              {
                // ISSUE: reference to a compiler-generated field
                if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__10 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  EventsView.\u003C\u003Eo__26.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Value", typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                Func<CallSite, object, string, object> target5 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__10.Target;
                // ISSUE: reference to a compiler-generated field
                CallSite<Func<CallSite, object, string, object>> p10 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__10;
                // ISSUE: reference to a compiler-generated field
                if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__9 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  EventsView.\u003C\u003Eo__26.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, int, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                Func<CallSite, object, int, int, object> target6 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__9.Target;
                // ISSUE: reference to a compiler-generated field
                CallSite<Func<CallSite, object, int, int, object>> p9 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__9;
                // ISSUE: reference to a compiler-generated field
                if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__8 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  EventsView.\u003C\u003Eo__26.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Cells", typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj9 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__8.Target((CallSite) EventsView.\u003C\u003Eo__26.\u003C\u003Ep__8, obj5);
                int num5 = num4;
                int num6 = num2;
                object obj10 = target6((CallSite) p9, obj9, num5, num6);
                string text2 = cell.Text;
                object obj11 = target5((CallSite) p10, obj10, text2);
                ++num4;
              }
            }
            // ISSUE: reference to a compiler-generated field
            if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__13 == null)
            {
              // ISSUE: reference to a compiler-generated field
              EventsView.\u003C\u003Eo__26.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ColumnWidth", typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, int, object> target7 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__13.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, int, object>> p13 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__13;
            // ISSUE: reference to a compiler-generated field
            if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__12 == null)
            {
              // ISSUE: reference to a compiler-generated field
              EventsView.\u003C\u003Eo__26.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, int, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, int, int, object> target8 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__12.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, int, int, object>> p12 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__12;
            // ISSUE: reference to a compiler-generated field
            if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__11 == null)
            {
              // ISSUE: reference to a compiler-generated field
              EventsView.\u003C\u003Eo__26.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Cells", typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj12 = EventsView.\u003C\u003Eo__26.\u003C\u003Ep__11.Target((CallSite) EventsView.\u003C\u003Eo__26.\u003C\u003Ep__11, obj5);
            int num7 = num2;
            object obj13 = target8((CallSite) p12, obj12, 1, num7);
            int num8 = col.Width / 5;
            object obj14 = target7((CallSite) p13, obj13, num8);
            ++num2;
          }
        }
      }
      finally
      {
        // ISSUE: reference to a compiler-generated field
        if (EventsView.\u003C\u003Eo__26.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          EventsView.\u003C\u003Eo__26.\u003C\u003Ep__14 = CallSite<Action<CallSite, ISimpleExcelReports, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ReleaseExcelInstance", (IEnumerable<System.Type>) null, typeof (EventsView), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        EventsView.\u003C\u003Eo__26.\u003C\u003Ep__14.Target((CallSite) EventsView.\u003C\u003Eo__26.\u003C\u003Ep__14, service, obj1);
      }
    }
  }

  public override void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    EventsViewConsts.IsFile = false;
    base.Initialize(items, services);
    this.InitServices();
  }

  public override void Initialize(
    NodeIDPath parentPath,
    INode parentNode,
    INodeID nodeId,
    System.IServiceProvider services)
  {
    base.Initialize(parentPath, parentNode, nodeId, services);
    if (this._services != null && this._services.GetService(typeof (IEventLogFilterProvider)) == null)
      this._services.AddService(typeof (IEventLogFilterProvider), (object) new EventsView.EventLogFilterProvider(this));
    if (this._services != null && this._services.GetService(typeof (IEventLogProvider)) == null)
      this._services.AddService(typeof (IEventLogProvider), (object) new EventsView.EventLogProvider(this));
    this.UpdateSetArchiveFromDateButtonItem();
    this.FillFiltersComboBox();
  }

  private void InitServices()
  {
    this.ReleaseServices();
    if (this._notificationService == null)
      return;
    this._notificationService.Subscribe("RefreshEventLog", new NotificationEventHandler(this.OnRefreshEventLog));
  }

  private void ReleaseServices()
  {
    if (this._notificationService == null)
      return;
    this._notificationService.Unsubscribe("RefreshEventLog", new NotificationEventHandler(this.OnRefreshEventLog));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.ReleaseServices();
    base.Dispose(disposing);
  }

  public override void Deactivate(IView nextView)
  {
    EventsViewConsts.IsFile = false;
    base.Deactivate(nextView);
    if (nextView != null)
      return;
    this.ReleaseServices();
  }

  public override ContentType ViewContentType => ContentType.NonFolders;

  private void OnRefreshEventLog(object sender, NotificationEventArgs e) => this.ReloadItems();

  private void GenerateNewValuesToGrid(string fileName)
  {
    try
    {
      EventsViewConsts.IsFile = true;
      this._toolBar.BeginUpdate();
      this._grid.BeginUpdate();
      this._grid.Redraw = false;
      if (this._grid.Rows.Count > 0)
        this.ClearData();
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(fileName);
      foreach (XmlNode selectNode in xmlDocument.SelectNodes("/DocumentElement/Event"))
      {
        iGRow iGrow = this._grid.Rows.Add();
        foreach (XmlNode childNode in selectNode.ChildNodes)
        {
          string str = childNode.InnerText;
          switch (childNode.Name)
          {
            case "F_EVENT_TYPE":
              ActionType result1;
              Enum.TryParse<ActionType>(childNode.InnerText, true, out result1);
              str = ActionTypeHelper.GetCaption(result1);
              break;
            case "F_CATEGORY_TYPE":
              str = Intermech.Consts.GetCategoryName(Convert.ToInt32(childNode.InnerText));
              break;
            case "F_AUDIT_TYPE":
              EventlogRecordType result2;
              Enum.TryParse<EventlogRecordType>(childNode.InnerText, true, out result2);
              str = EventlogRecordTypeHelper.GetCaption(result2);
              break;
            case "F_USER_ID":
              str = new UserNamesCache().GetUserName((long) Convert.ToInt32(childNode.InnerText));
              break;
          }
          for (int colIndex = 0; colIndex < iGrow.Cells.Count; ++colIndex)
          {
            if (iGrow.Cells[colIndex].Col.Tag is NodeColumn tag && tag.Attribute.PossibleValueFieldName == childNode.Name)
            {
              iGrow.Cells[colIndex].Value = (object) str;
              break;
            }
          }
        }
      }
      this._selectedRecordsCountToolStripStatusLabel.Text = string.Empty;
      this._readedRecordsCountToolStripStatusLabel.Text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1175"), (object) this._grid.Rows.Count.ToString());
    }
    finally
    {
      this._selectedRecordsCountToolStripStatusLabel.Text = string.Empty;
      this._readedRecordsCountToolStripStatusLabel.Text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1175"), (object) this._grid.Rows.Count.ToString());
      this._grid.Redraw = true;
      this._grid.EndUpdate();
      this._toolBar.EndUpdate();
    }
  }

  protected override void GridSetColumns(NodeColumnCollection columns, bool reloadData)
  {
    for (int index = 0; index < columns.Count; ++index)
    {
      if (columns[index].ID.Equals((object) ObligatoryObjectAttributes.F_OBJECT_NAME))
      {
        columns[index].DisableSorting = true;
        break;
      }
    }
    base.GridSetColumns(columns, reloadData);
  }

  private void EventLogComboBoxItem_SelectedValueChanged(object sender, EventArgs e)
  {
    this.UpdateSetArchiveFromDateButtonItem();
    this.ReloadItems();
  }

  private void UpdateSetArchiveFromDateButtonItem()
  {
    this._setArchiveFromDateButtonItem.Visible = this.IsInDatabaseAdministrator() && this.GetSelectedEventLog() == EventLogs.Operational;
  }

  private bool IsInDatabaseAdministrator()
  {
    return this._parentNode is CompositeNode && ((CompositeNode) this._parentNode).FolderSlots != null && ((CompositeNode) this._parentNode).FolderSlots.Count > 0 && ((CompositeNode) this._parentNode).FolderSlots[0].Object is DescriptorsPart && ((DescriptorsPart) ((CompositeNode) this._parentNode).FolderSlots[0].Object).Descriptors != null && ((DescriptorsPart) ((CompositeNode) this._parentNode).FolderSlots[0].Object).Descriptors.Slots != null && ((DescriptorsPart) ((CompositeNode) this._parentNode).FolderSlots[0].Object).Descriptors.Slots.Any<DescriptorSlot>((Func<DescriptorSlot, bool>) (o => PartGuidMapper.GetGuid(o.UniqueId) == this.EventLogPartGuid));
  }

  private void SetArchiveFromDateButtonItem_Click(object sender, EventArgs e)
  {
    using (SetArchiveFromDateForm archiveFromDateForm = new SetArchiveFromDateForm())
    {
      int num = (int) archiveFromDateForm.ShowDialog();
      this.ReloadItems();
    }
  }

  private void EventLogFiltersComboBoxItem_SelectedValueChanged(object sender, EventArgs e)
  {
    this.UpdateFiltersControls();
    this.ReloadItems();
  }

  private void CreateFilterButtonItem_Click(object sender, EventArgs e)
  {
    EventLogFilter newFilter = ServiceLocator.Get<IEventLogFiltersClientService>().CreateNewFilter();
    if (newFilter == null)
      return;
    this.FillFiltersComboBox(new Guid?(newFilter.Guid));
    this.ReloadItems();
  }

  private void EditFilterButtonItem_Click(object sender, EventArgs e)
  {
    ServiceLocator.Get<IEventLogFiltersClientService>().EditFilter(this.GetSelectedFilter().Guid);
    this.FillFiltersComboBox(new Guid?(this.GetSelectedFilter().Guid));
    this.ReloadItems();
  }

  private void RemoveFilterButtonItem_Click(object sender, EventArgs e)
  {
    if (!ServiceLocator.Get<IEventLogFiltersClientService>().RemoveFilter(this.GetSelectedFilter().Guid))
      return;
    this.FillFiltersComboBox();
    this.ReloadItems();
  }

  private void OpenXmlButtonItem_Click(object sender, EventArgs e)
  {
    this.myDelegate = new EventsView.AddListItem(this.GenerateNewValuesToGrid);
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.RestoreDirectory = true;
    openFileDialog.Filter = "xml файл журнала|*.xml";
    openFileDialog.Title = "Выберите файл для открытия";
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.Invoke((Delegate) this.myDelegate, (object) openFileDialog.FileName);
    foreach (object obj in (CollectionBase) this._toolBar.Items)
    {
      if (obj is LabelItem labelItem && labelItem.CommandName == "XMLFileName")
      {
        labelItem.Enabled = true;
        labelItem.Text = $"Открыт файл: {openFileDialog.FileName}";
        break;
      }
    }
  }

  private void ExportToExelButtonItem_Click(object sender, EventArgs e)
  {
    EventsView.EventLogExcelReport("Журнал событий", this._grid);
  }

  private void InitializeEventLogComboBox()
  {
    this._eventLogComboBoxItem.SelectedValueChanged -= new EventHandler(this.EventLogComboBoxItem_SelectedValueChanged);
    this._eventLogComboBoxItem.ComboBox.BeginUpdate();
    try
    {
      this._eventLogComboBoxItem.ComboBox.Items.Clear();
      this._eventLogComboBoxItem.ComboBox.DisplayMember = "Item2";
      this._eventLogComboBoxItem.ComboBox.ValueMember = "Item1";
      this._eventLogComboBoxItem.ComboBox.Items.AddRange((object[]) new Tuple<EventLogs, string>[2]
      {
        new Tuple<EventLogs, string>(EventLogs.Operational, EventLogs.Operational.GetDescription<EventLogs>()),
        new Tuple<EventLogs, string>(EventLogs.Archival, EventLogs.Archival.GetDescription<EventLogs>())
      });
      this._eventLogComboBoxItem.ComboBox.SelectedIndex = 0;
    }
    finally
    {
      this._eventLogComboBoxItem.ComboBox.EndUpdate();
      this._eventLogComboBoxItem.SelectedValueChanged += new EventHandler(this.EventLogComboBoxItem_SelectedValueChanged);
    }
  }

  private EventLogs GetSelectedEventLog()
  {
    return ((Tuple<EventLogs, string>) this._eventLogComboBoxItem.ComboBox.SelectedItem).Item1;
  }

  private void UpdateFiltersControls()
  {
    this._editFilterButtonItem.Enabled = this.CanEditFilter();
    this._removeFilterButtonItem.Enabled = this.CanRemoveFilter();
  }

  private bool CanEditFilter() => this.IsAllEventsFitlerSelected();

  private bool IsAllEventsFitlerSelected()
  {
    EventLogFilter selectedFilter = this.GetSelectedFilter();
    return selectedFilter != null && selectedFilter.Guid != EventLogFilter.AllEventsFilter.Guid;
  }

  private bool CanRemoveFilter() => this.IsAllEventsFitlerSelected();

  private void FillFiltersComboBox(Guid? mustBeSelectedFilterGuid = null)
  {
    this._eventLogFiltersComboBoxItem.ComboBox.BeginUpdate();
    try
    {
      Guid selectedFilterGuid = mustBeSelectedFilterGuid.HasValue ? mustBeSelectedFilterGuid.Value : (this._eventLogFiltersComboBoxItem.ComboBox.SelectedValue is Guid ? (Guid) this._eventLogFiltersComboBoxItem.ComboBox.SelectedValue : EventLogFilter.AllEventsFilter.Guid);
      this._eventLogFiltersComboBoxItem.SelectedValueChanged -= new EventHandler(this.EventLogFiltersComboBoxItem_SelectedValueChanged);
      try
      {
        this._eventLogFiltersComboBoxItem.ComboBox.Items.Clear();
        EventLogFilter[] allFilters = ServiceLocator.Get<IEventLogFiltersClientService>().GetAllFilters();
        this._eventLogFiltersComboBoxItem.ComboBox.DisplayMember = "Name";
        this._eventLogFiltersComboBoxItem.ComboBox.ValueMember = "Guid";
        foreach (object obj in allFilters)
          this._eventLogFiltersComboBoxItem.ComboBox.Items.Add(obj);
        this._eventLogFiltersComboBoxItem.ComboBox.SelectedItem = (object) (((IEnumerable<EventLogFilter>) allFilters).FirstOrDefault<EventLogFilter>((Func<EventLogFilter, bool>) (o => o.Guid == selectedFilterGuid)) ?? EventLogFilter.AllEventsFilter);
      }
      finally
      {
        this._eventLogFiltersComboBoxItem.SelectedValueChanged += new EventHandler(this.EventLogFiltersComboBoxItem_SelectedValueChanged);
      }
    }
    finally
    {
      this._eventLogFiltersComboBoxItem.ComboBox.EndUpdate();
    }
    this.UpdateFiltersControls();
  }

  private EventLogFilter GetSelectedFilter()
  {
    return this._eventLogFiltersComboBoxItem.ComboBox.SelectedItem as EventLogFilter;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EventsView));
    this._eventLogFiltersComboBoxItem = new ComboBoxItem();
    this._createFilterButtonItem = new ButtonItem();
    this._editFilterButtonItem = new ButtonItem();
    this._removeFilterButtonItem = new ButtonItem();
    this.labelItem1 = new LabelItem();
    this._eventLogComboBoxItem = new ComboBoxItem();
    this.labelItem2 = new LabelItem();
    this._setArchiveFromDateButtonItem = new ButtonItem();
    this._openXmlButtonItem = new ButtonItem();
    this._exportToExelButtonItem = new ButtonItem();
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.Items.AddRange(new ToolbarItemBase[10]
    {
      (ToolbarItemBase) this.labelItem2,
      (ToolbarItemBase) this._eventLogComboBoxItem,
      (ToolbarItemBase) this._setArchiveFromDateButtonItem,
      (ToolbarItemBase) this.labelItem1,
      (ToolbarItemBase) this._eventLogFiltersComboBoxItem,
      (ToolbarItemBase) this._createFilterButtonItem,
      (ToolbarItemBase) this._editFilterButtonItem,
      (ToolbarItemBase) this._removeFilterButtonItem,
      (ToolbarItemBase) this._openXmlButtonItem,
      (ToolbarItemBase) this._exportToExelButtonItem
    });
    this._toggleManualSortingButtonItem.Visible = false;
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("_grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("_grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    componentResourceManager.ApplyResources((object) this._grid, "_grid");
    componentResourceManager.ApplyResources((object) this._pageViewsManager, "_pageViewsManager");
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    this._filtersComboBoxItem.Visible = false;
    this._manualSortingSetupButtonItem.Visible = false;
    this._currentVersionsRuleButtonItem.Visible = false;
    this._editingModeButtonItem.Visible = false;
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this.buttonHeightSet.Click += new EventHandler(this.buttonHeightSet_Click);
    componentResourceManager.ApplyResources((object) this._eventLogFiltersComboBoxItem, "_eventLogFiltersComboBoxItem");
    this._eventLogFiltersComboBoxItem.DropDownStyle = ComboBoxStyle.DropDownList;
    this._eventLogFiltersComboBoxItem.MinimumControlWidth = 250;
    this._eventLogFiltersComboBoxItem.Padding.Bottom = 0;
    this._eventLogFiltersComboBoxItem.Padding.Left = 1;
    this._eventLogFiltersComboBoxItem.Padding.Right = 1;
    this._eventLogFiltersComboBoxItem.Padding.Top = 0;
    this._eventLogFiltersComboBoxItem.SelectedValueChanged += new EventHandler(this.EventLogFiltersComboBoxItem_SelectedValueChanged);
    componentResourceManager.ApplyResources((object) this._createFilterButtonItem, "_createFilterButtonItem");
    this._createFilterButtonItem.Image = (Image) Intermech.Client.Core.Properties.Resources.AddStandart;
    this._createFilterButtonItem.Click += new EventHandler(this.CreateFilterButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._editFilterButtonItem, "_editFilterButtonItem");
    this._editFilterButtonItem.Image = (Image) Intermech.Client.Core.Properties.Resources.EditStandart;
    this._editFilterButtonItem.Click += new EventHandler(this.EditFilterButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._removeFilterButtonItem, "_removeFilterButtonItem");
    this._removeFilterButtonItem.Image = (Image) Intermech.Client.Core.Properties.Resources.DeleteStandart;
    this._removeFilterButtonItem.Click += new EventHandler(this.RemoveFilterButtonItem_Click);
    this.labelItem1.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.labelItem1, "labelItem1");
    componentResourceManager.ApplyResources((object) this._eventLogComboBoxItem, "_eventLogComboBoxItem");
    this._eventLogComboBoxItem.DropDownStyle = ComboBoxStyle.DropDownList;
    this._eventLogComboBoxItem.MinimumControlWidth = 200;
    this._eventLogComboBoxItem.Padding.Bottom = 0;
    this._eventLogComboBoxItem.Padding.Left = 1;
    this._eventLogComboBoxItem.Padding.Right = 1;
    this._eventLogComboBoxItem.Padding.Top = 0;
    this._eventLogComboBoxItem.SelectedValueChanged += new EventHandler(this.EventLogComboBoxItem_SelectedValueChanged);
    this.labelItem2.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.labelItem2, "labelItem2");
    componentResourceManager.ApplyResources((object) this._setArchiveFromDateButtonItem, "_setArchiveFromDateButtonItem");
    this._setArchiveFromDateButtonItem.Image = (Image) Intermech.Client.Core.Properties.Resources.Calendar;
    this._setArchiveFromDateButtonItem.Click += new EventHandler(this.SetArchiveFromDateButtonItem_Click);
    this._openXmlButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._openXmlButtonItem, "_openXmlButtonItem");
    this._openXmlButtonItem.Icon = (Icon) componentResourceManager.GetObject("_openXmlButtonItem.Icon");
    this._openXmlButtonItem.Click += new EventHandler(this.OpenXmlButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._exportToExelButtonItem, "_exportToExelButtonItem");
    this._exportToExelButtonItem.Icon = (Icon) componentResourceManager.GetObject("_exportToExelButtonItem.Icon");
    this._exportToExelButtonItem.Click += new EventHandler(this.ExportToExelButtonItem_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (EventsView);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void buttonHeightSet_Click(object sender, EventArgs e)
  {
  }

  /// <summary>Чтение ресурсов</summary>
  protected static class ClientCoreResourcesAccess
  {
    /// <summary>Путь к ресурсам</summary>
    private static string ResourcePath = "Intermech.Client.Core.Navigator.Resources.";

    /// <summary>Считать ресурс в массив байт</summary>
    /// <param name="ResourceName">Имя ресурса</param>
    /// <returns>Иконка в потоке</returns>
    public static MemoryStream LoadResource(string ResourceName)
    {
      Stream stream = (Stream) null;
      try
      {
        stream = typeof (Engine).Assembly.GetManifestResourceStream(EventsView.ClientCoreResourcesAccess.ResourcePath + ResourceName);
        if (stream == null)
          return new MemoryStream();
        byte[] buffer = new byte[stream.Length];
        MemoryStream memoryStream = new MemoryStream(buffer.Length);
        stream.Read(buffer, 0, buffer.Length);
        memoryStream.Write(buffer, 0, buffer.Length);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        return memoryStream;
      }
      finally
      {
        stream?.Close();
      }
    }
  }

  public delegate void AddListItem(string myString);

  protected class EventsViewDescriptionProvider : BaseViewDescriptionProvider
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
        Caption = LocalizationHolder.rm.GetString("Client.Core_610"),
        ImageIndex = namedImageList.ImageIndex("imgEventLogIcon"),
        OrderID = 20
      };
    }
  }

  private sealed class EventLogProvider : IEventLogProvider
  {
    private EventsView _eventsView;

    public EventLogProvider(EventsView eventsView) => this._eventsView = eventsView;

    public EventLogs EventLog => this._eventsView.GetSelectedEventLog();
  }

  private sealed class EventLogFilterProvider : IEventLogFilterProvider
  {
    private EventsView _eventsView;

    public EventLogFilterProvider(EventsView eventsView)
    {
      this._eventsView = eventsView != null ? eventsView : throw new ArgumentNullException(nameof (eventsView));
    }

    public EventLogFilter Filter => this._eventsView.GetSelectedFilter();
  }
}
