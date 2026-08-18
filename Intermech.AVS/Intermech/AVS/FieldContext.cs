// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.FieldContext
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

internal class FieldContext
{
  public TableData DocRow;
  public TextData DocCell;
  public int RelationIndex = -1;
  public int ProductIndex = -1;
  public List<RelationAttributeValuesCache> RelationList;

  public RelationAttributeValuesCache Relation
  {
    get
    {
      return this.RelationIndex == -1 || this.RelationList.IsEmpty<RelationAttributeValuesCache>() ? (RelationAttributeValuesCache) null : this.RelationList[this.RelationIndex];
    }
  }

  private FieldContext()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="avsRow">Запись владеющая полем</param>
  /// <param name="relationIndex">Индекс связи в relationList. Если -1, то ищется по productIndex.
  /// Если и productIndex -1, то в GetFieldValue берётся первая связь, а в SetFieldValue заносится во все связи</param>
  /// <param name="productIndex">Индекс исполнения в списке исполнений avsDocument.
  /// Если -1, то ищется по relationIndex.
  /// Если и relationIndex -1, то в GetFieldValue берётся первая связь, а в SetFieldValue заносится во все связи</param>
  /// <param name="relationList"></param>
  public FieldContext(
    AVSRow avsRow,
    int relationIndex,
    int productIndex,
    List<RelationAttributeValuesCache> relationList)
  {
    if (avsRow == null)
      throw new ArgumentNullException(nameof (avsRow));
    this.RelationList = relationList ?? avsRow.Relations;
    this.RelationIndex = relationIndex;
    this.ProductIndex = productIndex;
    if (this.RelationIndex == -1)
    {
      if (this.ProductIndex == -1 || this.ProductIndex >= avsRow.avsDocument.ProductsInfo.Count)
        return;
      this.RelationIndex = avsRow.GetRelationIndexForProduct(avsRow.avsDocument.FindProductByIndex(this.ProductIndex).Id, this.RelationList);
    }
    else
    {
      if (this.ProductIndex != -1)
        return;
      this.ProductIndex = avsRow.GetProductIndexForRelation(this.RelationIndex, this.RelationList);
    }
  }
}
