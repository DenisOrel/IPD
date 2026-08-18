
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypeViewsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Selections;
using Intermech.Navigator.Views;
using Intermech.Search.Utilities;
using System;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Провайдер базовых вьюшек для элементов "Тип объекта" из пространства
/// навигации.
/// </summary>
internal sealed class ObjectTypeViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, System.IServiceProvider services)
  {
    IDBObjectTypeID objTypeID;
    if (items.Count != 1 || (objTypeID = items.GetItemData(0, typeof (IDBObjectTypeID)) as IDBObjectTypeID) == null)
      return ViewsInfo.Empty;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID.Value);
    if (objectType == null)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    ObjectTypeNodeOptions objectTypeNodeOptions = ObjectTypeNodeOptions.None;
    if (items.GetItemData(0, typeof (IObjectTypeNodeOptionsHolder)) is IObjectTypeNodeOptionsHolder itemData)
      objectTypeNodeOptions = itemData.Options;
    if ((objectTypeNodeOptions & ObjectTypeNodeOptions.OnlyTypesMode) == ObjectTypeNodeOptions.OnlyTypesMode)
    {
      views.Suppress("Thumbnails", 7);
      if ((objectTypeNodeOptions & ObjectTypeNodeOptions.ShowLCSteps) == ObjectTypeNodeOptions.ShowLCSteps)
        views.Add("ChildrenView", new ViewInfo(0, typeof (ObjectTypeLCStepsView)));
      return views;
    }
    views.Add("ChildrenView", new ViewInfo(0, typeof (TypedObjectsView)));
    if (MetaDataHelper.IsObjectTypeChildOf(objectType.Guid, new Guid("cad00070-306c-11d8-b4e9-00304f19f545")))
      views.Add("DocumentsThumbnailView", new ViewInfo(0, typeof (ThumbnailDocs)));
    if (UISettings.ShowSelectionsTabsForObjectTypes)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ObjectTypeBinding objectTypeBinding = new ObjectTypeBinding(objTypeID.Value, BindingType.Selections);
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams()
        {
          Columns = new object[2]
          {
            (object) ObligatoryObjectAttributes.F_OBJECT_ID,
            (object) ObligatoryObjectAttributes.CAPTION
          },
          Conditions = objectTypeBinding.TopConditions,
          RecordCount = -1,
          SortColumns = new object[1]
          {
            (object) ObligatoryObjectAttributes.CAPTION
          }
        };
        DataTable dataTable = sessionKeeper.Session.ObjectsSelect(new Guid("cad00156-306c-11d8-b4e9-00304f19f545"), dbRecordSetParams);
        int num = 1073741823 /*0x3FFFFFFF*/;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long selectionVersionID = DataSetProcessor.GetInt64Value(row, 0, 0L);
          string selectionCaption = DataSetProcessor.GetStringValue(row, 1, string.Empty);
          if (!ObjectHelper.IsUnknownObjectVersionID(selectionVersionID))
          {
            int currentViewOrder = num;
            views.Add($"ChildrenView_Selection_#{selectionVersionID}", new ViewInfo(-1, (ViewCreatorCallback) ((a, b, c) => (Control) new SelectionObjectsView(objTypeID.Value, selectionVersionID, selectionCaption, currentViewOrder))));
            ++num;
          }
        }
      }
    }
    return views;
  }
}
