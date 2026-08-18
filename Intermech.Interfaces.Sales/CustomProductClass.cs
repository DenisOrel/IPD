using System;

namespace Intermech.Interfaces.Sales;

public class CustomProductClass
{
  public long ObjectId = -1;
  public string Caption;
  public string Description;
  public ProductList CompatibleProducts = new ProductList();

  public void FillCompatibleProducts(IUserSession session, ProductList aFullProductList)
  {
    this.CompatibleProducts.Clear();
    if (this.ObjectId == -1L)
      return;
    IDBObject objectById = session.GetObjectByID(this.ObjectId, false);
    if (objectById == null)
      return;
    IDBAttribute attributeByGuid = objectById.GetAttributeByGuid(new Guid("cad0153d-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null)
      return;
    for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
    {
      attributeByGuid.Index = index;
      long int64 = Convert.ToInt64(attributeByGuid.Value);
      int productById = aFullProductList.FindProductByID(int64);
      if (productById != -1)
        this.CompatibleProducts.Add(aFullProductList[productById]);
    }
  }
}
