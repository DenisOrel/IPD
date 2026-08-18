using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.Sales;

/// <summary>список комплектов продуктов</summary>
public class ProductCompositionList : List<ProductCompositionObject>
{
  public bool Loaded;

  public void FillData(IUserSession session, ProductList productList)
  {
    this.Clear();
    if (!productList.Loaded)
      productList.FillData(session);
    foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(new Guid("cad01511-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[0], new ColumnDescriptor[6]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) new Guid("cad0001c-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) new Guid("cad0150b-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) new Guid("cad01554-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Guid, SortOrders.NONE, 0)
    })).Rows)
    {
      ProductCompositionObject compositionObject = new ProductCompositionObject();
      compositionObject.ProductCompositionDataInit(Convert.ToInt64(row[0]), Convert.ToString(row[1]), Convert.ToString(row[2]), Convert.ToString(row[3]), Convert.ToString(row[4]), Convert.ToString(row[5]));
      this.Add(compositionObject);
    }
    for (int index = 0; index < this.Count; ++index)
      this[index].FillData(session, productList);
    this.Loaded = true;
  }
}
