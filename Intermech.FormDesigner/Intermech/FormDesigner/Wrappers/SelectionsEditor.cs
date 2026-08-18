// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.SelectionsEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
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

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Едитор для свойства "Выборка".</summary>
public class SelectionsEditor : UITypeEditor
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    object obj = value;
    List<long> objectIDs = (List<long>) null;
    int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid("cad00156-306c-11d8-b4e9-00304f19f545"));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(new Guid("cad00156-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(Intermech.Navigator.Selections.Consts.KindSelectionAttrID, RelationalOperators.Equal, (object) 5, LogicalOperators.NONE, 0, false)
      }, new object[1]{ (object) -2 }));
      objectIDs = new List<long>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row[0] != null && row[0] != DBNull.Value)
          objectIDs.Add(Convert.ToInt64(row[0]));
      }
    }
    if (objectTypeId != -1)
      Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(objectTypeId, true), true);
    ListDescriptor rootDescriptor = new ListDescriptor(Intermech.Navigator.Consts.CategoryContextSelectionsNodeID, -1, LocalizationHolder.rm.GetString("FormDesigner_ContextSelections"), (IList) objectIDs);
    SelectionOptions options = SelectionOptions.Default | SelectionOptions.HideViews | SelectionOptions.DisableMultiselect;
    if (Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("FormDesigner_CheckSelection"), (IDescriptor) rootDescriptor, typeof (IDBObjectID), options) is IDBObjectID[] dbObjectIdArray)
    {
      QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(dbObjectIdArray[0].Value);
      obj = (object) (objectInfo.Empty ? Guid.Empty : objectInfo.VersionGuid);
    }
    return obj;
  }
}
