
// Type: Intermech.PropertyEditors.DataTableConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Views.Grid;
using Intermech.Holders;
using Intermech.Interfaces.LifeCycles;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace Intermech.PropertyEditors;

/// <summary>
/// получает на входе DataTable с сырыми данными, на выходе DataTable для назначения в GridControl
/// </summary>
public class DataTableConverter
{
  public static DataTable ConvertDataTable(DataTable data, int categoryID)
  {
    if (data == null)
      return (DataTable) null;
    DataTable dataTable = data.Clone();
    List<string> stringList = new List<string>();
    foreach (DataColumn column in (InternalDataCollectionBase) data.Columns)
    {
      bool flag1 = true;
      Type type = column.DataType;
      bool flag2 = false;
      try
      {
        switch (column.ColumnName)
        {
          case "F_ANY_ATTRIBUTES":
          case "F_ATTRIBUTE_TYPE":
          case "F_CAPTION_ATTRIBUTE":
          case "F_CHKOUTFILE":
          case "F_COMPUTED":
          case "F_CONTENT":
          case "F_DEFAULT_DESCRIPT":
          case "F_DEFAULT_RELATION":
          case "F_INVIEW":
          case "F_MASTER_ID":
          case "F_MULTIPLE_VALUED":
          case "F_OPTIONS":
          case "F_PUBLIC":
          case "F_PUBLIC_LC":
          case "F_RELATION_KIND":
          case "F_SITE_ID":
          case "F_SOURCE_ID":
          case "F_STORAGE_ID":
          case "F_TYPE_DESCRIPTION":
          case "F_UNIQUE":
          case "F_VERSIONABLE":
            type = typeof (string);
            continue;
          case "F_AREA_ID":
            if (categoryID != 11)
            {
              type = typeof (string);
              continue;
            }
            flag2 = true;
            continue;
          case "F_DEFAULT":
            switch (categoryID)
            {
              case 3:
                flag1 = false;
                continue;
              case 8:
              case 9:
              case 16 /*0x10*/:
                type = typeof (string);
                continue;
              default:
                flag2 = true;
                continue;
            }
          case "F_DRAW_DATA":
            flag1 = false;
            continue;
          case "F_ICON":
          case "F_SAVE_HISTORY":
          case "F_SIZE_TYPE":
            flag1 = false;
            continue;
          case "F_LANGUAGE_ID":
            if (categoryID != 9)
            {
              type = typeof (string);
              continue;
            }
            flag2 = true;
            continue;
          case "F_LEVEL_ID":
            if (categoryID != 8)
            {
              type = typeof (string);
              continue;
            }
            flag2 = true;
            continue;
          case "F_OPTIMIZED":
            type = typeof (string);
            continue;
          case "F_PARENT_ID":
            if (categoryID == 12)
            {
              flag1 = false;
              continue;
            }
            flag2 = true;
            continue;
          case "F_READ_DURATION":
          case "F_SEEK_DURATION":
          case "F_WRITE_DURATION":
            type = typeof (long);
            continue;
          default:
            flag2 = true;
            continue;
        }
      }
      finally
      {
        int num = flag2 ? 1 : 0;
        if (flag1)
          dataTable.Columns[column.ColumnName].DataType = type;
        else
          stringList.Add(column.ColumnName);
      }
    }
    dataTable.AcceptChanges();
    List<int> intList = new List<int>();
    List<int> collection = new List<int>();
    for (int index = 0; index < dataTable.Columns.Count; ++index)
    {
      if (stringList.IndexOf(dataTable.Columns[index].ColumnName) == -1)
      {
        if (dataTable.Columns[index].ColumnName == "F_ATTRIBUTE_TYPE")
          collection.Add(index);
        else
          intList.Add(index);
      }
    }
    intList.AddRange((IEnumerable<int>) collection);
    foreach (DataRow row1 in (InternalDataCollectionBase) data.Rows)
    {
      DataRow row2 = dataTable.NewRow();
      for (int index = 0; index < intList.Count; ++index)
      {
        int num = intList[index];
        object obj = row1[dataTable.Columns[num].ColumnName];
        if (obj != DBNull.Value)
        {
          bool flag = false;
          try
          {
            switch (dataTable.Columns[num].ColumnName)
            {
              case "F_ANY_ATTRIBUTES":
              case "F_CHKOUTFILE":
              case "F_CONTENT":
              case "F_SAVE_HISTORY":
                row2[num] = (object) BoolSrv.YesNoConvert(Convert.ToInt16(obj) == (short) 1);
                continue;
              case "F_AREA_ID":
                if (categoryID != 11)
                {
                  row2[num] = (object) new SubjectAreaPropertyClass(obj.ToString()).ToString();
                  continue;
                }
                flag = true;
                continue;
              case "F_ATTRIBUTE_TYPE":
                row2[num] = (object) new FieldTypePropertyClass((FieldTypes) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_CAPTION_ATTRIBUTE":
                row2[num] = (object) new AttributePropertyClass(Convert.ToInt32(obj)).ToString();
                continue;
              case "F_COMPUTED":
                row2[num] = (object) new ComputeValueModePropertyClass((ComputeValueModes) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_DEFAULT":
                switch (categoryID)
                {
                  case 8:
                  case 9:
                  case 16 /*0x10*/:
                    row2[num] = (object) BoolSrv.YesNoConvert(Convert.ToInt16(obj) == (short) 1);
                    continue;
                  default:
                    flag = true;
                    continue;
                }
              case "F_DEFAULT_DESCRIPT":
                row2[num] = (object) obj.ToString();
                continue;
              case "F_DEFAULT_RELATION":
                row2[num] = (object) new RelationTypePropertyClass(Convert.ToInt32(obj)).ToString();
                continue;
              case "F_INVIEW":
                row2[num] = (object) new OptimizationModePropertyClass((OptimizationModes) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_LANGUAGE_ID":
                if (categoryID != 9)
                {
                  row2[num] = (object) new LanguagePropertyClass(obj.ToString()).ToString();
                  continue;
                }
                flag = true;
                continue;
              case "F_LEVEL_ID":
                if (categoryID != 8)
                {
                  row2[num] = (object) new LevelPropertyClass(Convert.ToInt32(obj)).ToString();
                  continue;
                }
                flag = true;
                continue;
              case "F_MASTER_ID":
              case "F_SOURCE_ID":
                row2[num] = (object) new AttributePropertyClass(Convert.ToInt32(obj));
                continue;
              case "F_MULTIPLE_VALUED":
                row2[num] = (object) new MultiValueModePropertyClass((MultiValueModes) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_OPTIMIZED":
                row2[num] = (object) OptimizationModesHelper.GetCaption((OptimizationModes) Convert.ToInt32(obj));
                continue;
              case "F_OPTIONS":
                switch (categoryID)
                {
                  case 3:
                    row2[num] = (object) AttributeOptionsHelper.GetCaptions((AttributeOptions) Convert.ToInt32(obj));
                    continue;
                  case 4:
                    row2[num] = (object) ObjectTypeOptionsHelper.GetCaptions((ObjectTypeOptions) Convert.ToInt32(obj));
                    continue;
                  case 16 /*0x10*/:
                    row2[num] = (object) LCSchemaOptionsHelper.GetCaptions((LCSchemaOptions) Convert.ToInt32(obj));
                    continue;
                  default:
                    flag = true;
                    continue;
                }
              case "F_PUBLIC":
              case "F_PUBLIC_LC":
                row2[num] = (object) new InheritModePropertyClass((InheritModes) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_READ_DURATION":
              case "F_SEEK_DURATION":
              case "F_WRITE_DURATION":
                row2[num] = (object) Convert.ToInt64(obj);
                continue;
              case "F_RELATION_KIND":
                row2[num] = (object) new RelationKindPropertyClass((RelationKinds) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_SITE_ID":
                row2[num] = (object) new SiteIDPropertyClass(Convert.ToString(obj)).ToString();
                continue;
              case "F_STORAGE_ID":
                row2[num] = (object) new StoragePropertyClass(Convert.ToInt64(obj));
                continue;
              case "F_TYPE_DESCRIPTION":
                if (categoryID != 3)
                {
                  row2[num] = (object) obj.ToString();
                  continue;
                }
                switch ((FieldTypes) Convert.ToInt16(row1["F_ATTRIBUTE_TYPE"]))
                {
                  case FieldTypes.ftObjectLink:
                    row2[num] = (object) string.Format(LocalizationHolder.rm.GetString("ObjectLink"), (object) obj.ToString());
                    continue;
                  case FieldTypes.ftMeasured:
                    object physValue = PhysValueHolder.PhysValues[(object) Convert.ToInt64(obj)];
                    if (physValue != null)
                    {
                      row2[num] = (object) string.Format(LocalizationHolder.rm.GetString("PhysValue"), (object) physValue.ToString());
                      continue;
                    }
                    continue;
                  case FieldTypes.ftObjectLinkByID:
                    row2[num] = (object) string.Format(LocalizationHolder.rm.GetString("ObjectLinkByID"), (object) obj.ToString());
                    continue;
                  default:
                    row2[num] = (object) obj.ToString();
                    continue;
                }
              case "F_UNIQUE":
                row2[num] = (object) new UniqueValueModePropertyClass((UniqueValueModes) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_VERSIONABLE":
                row2[num] = (object) new ObjectVersionModePropertyClass((ObjectVersionModes) Convert.ToInt16(obj)).ToString();
                continue;
              default:
                flag = true;
                continue;
            }
          }
          finally
          {
            if (flag)
              row2[num] = row1[dataTable.Columns[num].ColumnName];
          }
        }
      }
      dataTable.Rows.Add(row2);
    }
    for (int index = 0; index < stringList.Count; ++index)
      dataTable.Columns.Remove(stringList[index]);
    dataTable.AcceptChanges();
    return dataTable;
  }

  public static void ApplyToGridControl(DataTable data, GridControl grid, MemoryStream ms)
  {
    GridView mainView = grid.MainView as GridView;
    mainView.ClearGrouping();
    mainView.ClearSorting();
    mainView.ClearColumnsFilter();
    grid.DataSource = (object) null;
    grid.DataSource = (object) data;
    if (ms != null && ms.Length > 0L)
    {
      ms.Position = 0L;
      mainView.RestoreLayoutFromStream((Stream) ms);
      if (mainView.Columns == null || mainView.Columns.Count == 0)
      {
        mainView.PopulateColumns();
      }
      else
      {
        for (int index = 0; index < mainView.Columns.Count; ++index)
        {
          if (mainView.Columns[index].FieldName == "F_SAVE_HISTORY")
          {
            mainView.Columns.RemoveAt(index);
            break;
          }
          if (mainView.Columns[index].FieldName == "F_TYPE_DESCRIPTION")
          {
            string str = "Размер/тип";
            if (!mainView.Columns[index].Caption.Equals(str))
              mainView.Columns[index].Caption = str;
          }
          if (mainView.Columns[index].FieldName == "F_DEFAULT_DESCRIPT")
          {
            string str = "Значение по умолчанию";
            if (!mainView.Columns[index].Caption.Equals(str))
              mainView.Columns[index].Caption = str;
          }
        }
      }
      if (mainView.Columns == null || mainView.Columns["F_DRAW_DATA"] == null || mainView.Columns["F_DRAW_DATA"].VisibleIndex == -1)
        return;
      mainView.Columns["F_DRAW_DATA"].VisibleIndex = -1;
    }
    else
      mainView.PopulateColumns();
  }
}
