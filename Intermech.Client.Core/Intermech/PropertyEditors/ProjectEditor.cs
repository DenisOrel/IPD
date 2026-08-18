
// Type: Intermech.PropertyEditors.ProjectEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Projects;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>Редактор юзеров.</summary>
public class ProjectEditor : UITypeEditor
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    if (context == null)
      return base.GetEditStyle(context);
    return !context.PropertyDescriptor.IsReadOnly ? UITypeEditorEditStyle.Modal : UITypeEditorEditStyle.None;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="sp"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider sp,
    object value)
  {
    List<long> objectIDs = (List<long>) null;
    int objectType = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ProjectFiltrationModes projectFiltrationMode = sessionKeeper.Session.ProjectFiltrationMode;
      try
      {
        sessionKeeper.Session.ProjectFiltrationMode = ProjectFiltrationModes.UserProjects;
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
        {
          (object) -2
        });
        DataTable dataTable = sessionKeeper.Session.ObjectsSelect(new Guid("cad00812-306c-11d8-b4e9-00304f19f545"), dbRecordSetParams);
        objectIDs = new List<long>(dataTable.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row[0] != null && row[0] != DBNull.Value)
            objectIDs.Add(Convert.ToInt64(row[0]));
        }
        objectType = sessionKeeper.Session.IdentHelper.ProjectsTypeID;
      }
      finally
      {
        sessionKeeper.Session.ProjectFiltrationMode = projectFiltrationMode;
      }
    }
    Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(objectType, true), true);
    ListDescriptor rootDescriptor = new ListDescriptor(Intermech.Navigator.Consts.CategoryCurrentProjectNode, -1, LocalizationHolder.rm.GetString("Client_Core_ObjectsType_Projects"), (IList) objectIDs);
    return !(Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_704"), (IDescriptor) rootDescriptor, typeof (IDBObjectID), SelectionOptions.Default | SelectionOptions.DisableMultiselect) is IDBObjectID[] dbObjectIdArray) || dbObjectIdArray.Length == 0 ? value : (object) new ProjectPropertyClass(dbObjectIdArray[0].Value, (string) null);
  }
}
