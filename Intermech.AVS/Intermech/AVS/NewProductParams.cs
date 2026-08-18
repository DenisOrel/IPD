// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.NewProductParams
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS;

/// <summary>Внутренний класс. Содержит параметры для добавления новых исполнений</summary>
public class NewProductParams
{
  public long ProductID = -1;
  public int SrcProductIndex = -1;
  public string ProductDesignation;
  public string ProductNumber;
  public int ProductIndex;

  /// <summary>Конструктор</summary>
  /// <param name="productID">Идентификатор исполнения. -1 - если он не создано ранее</param>
  /// <param name="srcProductID">Идентификатор прототипа исполнения. Если оно не создано ранее, иначе не имеет значения</param>
  /// <param name="productDesignation">Обозначение исполнения</param>
  /// <param name="productNumber">Номер исполнения</param>
  /// <param name="productIndexList">Индекс, который должен быть у исполнения в списке</param>
  public NewProductParams(
    long productID,
    int srcProductIndex,
    string productDesignation,
    string productNumber,
    int productIndex)
  {
    this.ProductID = productID;
    this.SrcProductIndex = srcProductIndex;
    this.ProductDesignation = productDesignation;
    this.ProductNumber = productNumber;
    this.ProductIndex = productIndex;
  }
}
