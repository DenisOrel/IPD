
// Type: Intermech.PropertyEditors.DataTableConvertor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Views.Grid;
using System;
using System.Data;


namespace Intermech.PropertyEditors;

/// <summary>
/// получает на входе DataTable с сырыми данными, на выходе DataTable для назначения в GridControl
/// </summary>
public class DataTableConvertor
{
  public static DataTable ConvertDataTable(DataTable data, int categoryID, int objectTypeID)
  {
    if (data == null)
      return (DataTable) null;
    DataTable dataTable = data.Clone();
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
          case "F_DEFAULT_RELATION":
          case "F_MULTIPLE_VALUED":
          case "F_PUBLIC_LC":
          case "F_RELATION_KIND":
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
              case 8:
              case 9:
                type = typeof (string);
                continue;
              default:
                flag2 = true;
                continue;
            }
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
          dataTable.Columns.Remove(column.ColumnName);
      }
    }
    dataTable.AcceptChanges();
    foreach (DataRow row1 in (InternalDataCollectionBase) data.Rows)
    {
      DataRow row2 = dataTable.NewRow();
      for (int index = 0; index < dataTable.Columns.Count; ++index)
      {
        object obj = row1[dataTable.Columns[index].ColumnName];
        if (obj != DBNull.Value)
        {
          bool flag = false;
          try
          {
            switch (dataTable.Columns[index].ColumnName)
            {
              case "F_ANY_ATTRIBUTES":
              case "F_CHKOUTFILE":
              case "F_SAVE_HISTORY":
                row2[index] = (object) BoolSrv.YesNoConvert(Convert.ToInt16(obj) == (short) 1);
                continue;
              case "F_AREA_ID":
                if (categoryID != 11)
                {
                  row2[index] = (object) new SubjectAreaPropertyClass(obj.ToString()).ToString();
                  continue;
                }
                flag = true;
                continue;
              case "F_ATTRIBUTE_TYPE":
                row2[index] = (object) new FieldTypePropertyClass((FieldTypes) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_CAPTION_ATTRIBUTE":
                row2[index] = (object) new AttributePropertyClass(Convert.ToInt32(obj)).ToString();
                continue;
              case "F_COMPUTED":
                row2[index] = (object) new ComputeValueModePropertyClass((ComputeValueModes) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_DEFAULT":
                switch (categoryID)
                {
                  case 8:
                  case 9:
                    row2[index] = (object) BoolSrv.YesNoConvert(Convert.ToInt16(obj) == (short) 1);
                    continue;
                  default:
                    flag = true;
                    continue;
                }
              case "F_DEFAULT_RELATION":
                row2[index] = (object) new RelationTypePropertyClass(Convert.ToInt32(obj)).ToString();
                continue;
              case "F_LANGUAGE_ID":
                if (categoryID != 9)
                {
                  row2[index] = (object) new LanguagePropertyClass(obj.ToString()).ToString();
                  continue;
                }
                flag = true;
                continue;
              case "F_LEVEL_ID":
                if (categoryID != 8)
                {
                  row2[index] = (object) new LevelPropertyClass(Convert.ToInt32(obj)).ToString();
                  continue;
                }
                flag = true;
                continue;
              case "F_MULTIPLE_VALUED":
                row2[index] = (object) new MultiValueModePropertyClass((MultiValueModes) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_PUBLIC_LC":
                row2[index] = (object) new InheritModePropertyClass((InheritModes) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_RELATION_KIND":
                row2[index] = (object) new RelationKindPropertyClass((RelationKinds) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_TYPE_DESCRIPTION":
                row2[index] = (object) obj.ToString();
                continue;
              case "F_UNIQUE":
                row2[index] = (object) new UniqueValueModePropertyClass((UniqueValueModes) Convert.ToInt16(obj)).ToString();
                continue;
              case "F_VERSIONABLE":
                row2[index] = (object) new ObjectVersionModePropertyClass((ObjectVersionModes) Convert.ToInt16(obj)).ToString();
                continue;
              default:
                flag = true;
                continue;
            }
          }
          finally
          {
            if (flag)
              row2[index] = row1[dataTable.Columns[index].ColumnName];
          }
        }
      }
      dataTable.Rows.Add(row2);
    }
    return dataTable;
  }

  public static void ApplyToGridControl(
    DataTable data,
    GridControl grid,
    GridView view,
    int categoryID,
    int objectTypeID)
  {
    grid.DataSource = (object) data;
    view.PopulateColumns();
  }
}
