namespace Intermech.Interfaces.Sales;

public static class ProductsProcessor
{
  /// <summary>глобальный список комплектов продуктов</summary>
  public static ProductCompositionList FullProductCompositionList = new ProductCompositionList();
  /// <summary>глобальный список продуктов</summary>
  public static ProductList FullProductList = new ProductList();

  public static void LoadInfo(IUserSession session)
  {
    ProductsProcessor.FullProductList.FillData(session);
    ProductsProcessor.FullProductCompositionList.FillData(session, ProductsProcessor.FullProductList);
  }
}
