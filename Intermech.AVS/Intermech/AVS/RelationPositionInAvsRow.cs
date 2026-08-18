// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RelationPositionInAvsRow
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

internal class RelationPositionInAvsRow
{
  public List<RelationAttributeValuesCache> RelationList;
  public bool IsHiddenRelation;
  public int RelationIndex;
  public AVSRow Owner;

  public RelationPositionInAvsRow(
    AVSRow owner,
    List<RelationAttributeValuesCache> relationList,
    bool isHiddenRelation,
    int relationIndex)
  {
    this.Owner = owner;
    this.RelationList = relationList;
    this.IsHiddenRelation = isHiddenRelation;
    this.RelationIndex = relationIndex;
  }

  public RelationPositionInAvsRow(AVSRow owner, RelationAttributeValuesCache relation)
  {
    if (owner == null)
      throw new ArgumentNullException(nameof (owner));
    if (relation == null)
      throw new ArgumentNullException(nameof (relation));
    this.Owner = owner;
    this.RelationList = (List<RelationAttributeValuesCache>) null;
    this.IsHiddenRelation = false;
    this.RelationIndex = -1;
    if (owner.HasRelation)
      this.RelationIndex = this.Owner.Relations.IndexOf(relation);
    if (this.RelationIndex != -1)
    {
      this.RelationList = this.Owner.Relations;
      this.IsHiddenRelation = false;
    }
    else
    {
      if (this.Owner.HasHiddenRelation)
        this.RelationIndex = this.Owner.HiddenRelations.IndexOf(relation);
      if (this.RelationIndex == -1)
        return;
      this.RelationList = this.Owner.HiddenRelations;
      this.IsHiddenRelation = true;
    }
  }
}
