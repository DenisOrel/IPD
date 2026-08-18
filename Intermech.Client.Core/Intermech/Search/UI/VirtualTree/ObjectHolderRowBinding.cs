
// Type: Intermech.Search.UI.VirtualTree.ObjectHolderRowBinding
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.UI.VirtualTree;

public abstract class ObjectHolderRowBinding : ObjectRowBinding
{
  private LazyService<ICategoryTypeIconService> _categoryTypeIconService = new LazyService<ICategoryTypeIconService>();
  private LazyService<ICurrentUserAndRole> _currentUserAndRole = new LazyService<ICurrentUserAndRole>();
  private LazyService<INavGraphicsCache> _navGraphicsCache = new LazyService<INavGraphicsCache>();
  private LazyService<IColumnSchemes> _columnSchemes = new LazyService<IColumnSchemes>();
  private ObjectHolderRowBinding.SpecifyRecordAdapter _specifyRecordAdapter = new ObjectHolderRowBinding.SpecifyRecordAdapter();

  public ObjectHolderRowBinding(Type type)
  {
    if (type == (Type) null)
      throw new ArgumentNullException(nameof (type));
    this.Type = typeof (IObjectHolder).IsAssignableFrom(type) ? type : throw new ArgumentException();
  }

  public override void GetCellData(Row row, Column column, CellData cellData)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    if (column == null)
      throw new ArgumentNullException(nameof (column));
    if (cellData == null)
      throw new ArgumentNullException(nameof (cellData));
    base.GetCellData(row, column, cellData);
    IObjectHolder objectHolder = (IObjectHolder) row.Item;
    if (objectHolder.Object == null)
      return;
    if (!string.IsNullOrEmpty(column.DataField))
    {
      int result = 0;
      int.TryParse(column.DataField, out result);
      if (!AttributeTypeHelper.IsUnknownAttributeTypeID(result))
      {
        object attributeValue = objectHolder.Object.Attributes.GetAttributeValue(result);
        object obj = attributeValue;
        if (attributeValue != null)
        {
          INodeColumnTransform nodeColumnTransform = !AttributeTypeHelper.IsSystemAttributeTypeID(result) ? this._columnSchemes.Value.GetDefaultTransform(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (object) result) : this._columnSchemes.Value.GetDefaultTransform(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) result);
          if (nodeColumnTransform != null)
          {
            int[] array1 = objectHolder.Object.Attributes.Select<_Attribute, int>((Func<_Attribute, int>) (o => o.TypeID)).ToArray<int>();
            object[] array2 = objectHolder.Object.Attributes.Select<_Attribute, object>((Func<_Attribute, object>) (o => o.Value)).ToArray<object>();
            this._specifyRecordAdapter.Initialize(array1, array2);
            NodeColumn column1 = !(column is ExtendedColumn) || !(((ColumnBase) column).Tag is NodeColumn) ? new NodeColumn(!AttributeTypeHelper.IsSystemAttributeTypeID(result) ? Intermech.Navigator.Consts.ObjectColumnSchemeGuid : Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) result, attributeValue.GetType(), FieldTypes.ftString, string.Empty) : (NodeColumn) ((ColumnBase) column).Tag;
            obj = nodeColumnTransform.Apply(attributeValue, column1, (object) this._specifyRecordAdapter, array2);
          }
        }
        cellData.Value = obj is string ? (object) ((string) obj).Replace("\r", string.Empty).Replace("\n", string.Empty) : obj;
      }
    }
    UIColorsScheme currentColorsScheme = this._navGraphicsCache.Value.CurrentColorsScheme;
    if (currentColorsScheme == null || ObjectHelper.IsUnknownObjectVersionID(objectHolder.Object.CheckOutByVersionID))
      return;
    if (objectHolder.Object.CheckOutByVersionID == this._currentUserAndRole.Value.UserID)
    {
      StyleDelta delta = new StyleDelta()
      {
        BackColor = currentColorsScheme.CheckedOutBkStartColor,
        GradientColor = currentColorsScheme.CheckedOutBkEndColor,
        GradientMode = currentColorsScheme.CheckedOutGradientMode
      };
      cellData.EvenStyle = new Style(cellData.EvenStyle, delta);
      cellData.OddStyle = new Style(cellData.OddStyle, delta);
    }
    else
    {
      StyleDelta delta = new StyleDelta()
      {
        BackColor = currentColorsScheme.CheckedOutOtherBkStartColor,
        GradientColor = currentColorsScheme.CheckedOutOtherBkEndColor,
        GradientMode = currentColorsScheme.CheckedOutOtherGradientMode
      };
      cellData.EvenStyle = new Style(cellData.EvenStyle, delta);
      cellData.OddStyle = new Style(cellData.OddStyle, delta);
    }
  }

  public override void GetRowData(Row row, RowData rowData)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    if (rowData == null)
      throw new ArgumentNullException(nameof (rowData));
    base.GetRowData(row, rowData);
    IObjectHolder objectHolder = (IObjectHolder) row.Item;
    if (objectHolder.Object == null || objectHolder.Object.TypeID == -1)
      return;
    rowData.ImageList = this._categoryTypeIconService.Value.ImageList;
    rowData.ImageIndex = this._categoryTypeIconService.Value.IndexOf(4, objectHolder.Object.TypeID);
  }

  private sealed class SpecifyRecordAdapter : RecordAdapter
  {
    private int[] _attributeTypeIds;
    private object[] _attributeValues;

    public void Initialize(int[] attributeTypeIds, object[] attributeValues)
    {
      if (attributeTypeIds == null)
        throw new ArgumentNullException(nameof (attributeTypeIds));
      if (attributeValues == null)
        throw new ArgumentNullException(nameof (attributeValues));
      if (attributeTypeIds.Length != attributeValues.Length)
        throw new ArgumentException();
      this._attributeTypeIds = attributeTypeIds;
      this._attributeValues = attributeValues;
    }

    public override int GetFieldIndex(object field)
    {
      if (field == null)
        throw new ArgumentNullException(nameof (field));
      return Array.IndexOf<int>(this._attributeTypeIds, !(field is NodeColumnID) ? AttributeTypeHelper.ConvertToAttributeTypeID(field) : AttributeTypeHelper.ConvertToAttributeTypeID(((NodeColumnID) field).ID));
    }

    public override object[] GetRawRecordValues(object[] fieldValues)
    {
      List<object> objectList = new List<object>();
      foreach (object fieldValue in fieldValues)
      {
        object attributeValue = this._attributeValues[this.GetFieldIndex(fieldValue)];
        objectList.Add(attributeValue);
      }
      return objectList.ToArray();
    }

    public override object[] GetRecordValues(object[] fieldValues)
    {
      throw new NotSupportedException();
    }
  }
}
