// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RelationColumnsScheme
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

public class RelationColumnsScheme : AVSColumnScheme
{
  public RelationColumnsScheme() => this._schemeGuid = Guid.NewGuid();

  public override string Name => "Атрибуты связи";

  public override bool IsRelationColumn(NodeColumn nc) => true;

  public void AddRelationTypes(IList<int> relationTypeIDs)
  {
    this._possibleAttributesIDs.Clear();
    foreach (int relationTypeId in (IEnumerable<int>) relationTypeIDs)
      this.LoadPossibleAttributes(new List<IMSAttribute4>((IEnumerable<IMSAttribute4>) MetaDataHelper.GetAttribute4RelationTypeList(relationTypeId)));
    this._possibleAttributesIDs.Sort(this as IComparer<object>);
  }

  public override AttributeInfo FindAttributeInfo(NodeColumn nodeColumn)
  {
    AttributeInfo attributeInfo = (AttributeInfo) null;
    if (nodeColumn != null)
    {
      int id = (int) nodeColumn.ID;
      Guid attributeGuidById = DBHelper.GetAttributeGuidByID(id);
      if (attributeGuidById != Guid.Empty)
        attributeInfo = new AttributeInfo(FieldSource.Relation, attributeGuidById, id, nodeColumn.Caption);
    }
    return attributeInfo;
  }
}
