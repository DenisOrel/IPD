using Intermech.Kernel.Search;
using System;
using System.Data;


namespace Intermech.Interfaces.Sales;

/// <summary>объект описывающий комплект продуктов</summary>
public class ProductCompositionObject : CustomProductClass
{
  public ProductList productList = new ProductList();
  public string CodeAndVersion;
  public string Name;
  public string FullName;

  public void ProductCompositionDataInit(
    long objectId,
    string caption,
    string description,
    string codeAndVersion,
    string name,
    string fullName)
  {
    this.ObjectId = objectId;
    this.Caption = caption;
    this.Description = description;
    this.CodeAndVersion = codeAndVersion;
    this.Name = name;
    this.FullName = fullName;
  }

  public void FillData(IUserSession session, ProductList aProductList)
  {
    this.productList.Clear();
    foreach (DataRow row in (InternalDataCollectionBase) session.GetRelationCollection(MetaDataHelper.GetRelationTypeID(new Guid("cad01557-306c-11d8-b4e9-00304f19f545"))).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }), this.ObjectId).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      int productById = aProductList.FindProductByID(int64);
      if (productById != -1)
        this.productList.Add(aProductList[productById]);
    }
  }
}
