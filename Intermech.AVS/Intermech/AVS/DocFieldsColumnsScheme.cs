// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DocFieldsColumnsScheme
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Attributes;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

public class DocFieldsColumnsScheme : AVSColumnScheme
{
  private Dictionary<int, INodeColumnSource> docRowFields = new Dictionary<int, INodeColumnSource>();

  public DocFieldsColumnsScheme() => this._schemeGuid = Guid.NewGuid();

  public DocFieldsColumnsScheme(IEnumerable<AttributeInfo> fields)
    : this()
  {
    this.AddFields(fields);
  }

  public override string Name => "Графы записи документа";

  public override bool IsRelationColumn(NodeColumn nc)
  {
    return nc.ID is int && this.FindAttribute((int) nc.ID) is AttributeInfo attribute && attribute.IsRelationAttribute;
  }

  public void AddFields(IEnumerable<AttributeInfo> docRowFields)
  {
    this._possibleAttributesIDs.Clear();
    int num = -20000;
    foreach (AttributeInfo docRowField in docRowFields)
    {
      int key = docRowField.AttributeId;
      if (key == -1)
      {
        key = num;
        --num;
      }
      if (!this._possibleAttributesIDs.Contains((object) key))
      {
        this._possibleAttributesIDs.Add((object) key);
        this.docRowFields.Add(key, (INodeColumnSource) docRowField);
      }
    }
    this._possibleAttributesIDs.Sort(this as IComparer<object>);
  }

  public INodeColumnSource FindAttribute(int id)
  {
    return this.docRowFields.ContainsKey(id) ? this.docRowFields[id] : (INodeColumnSource) null;
  }

  private int GetAttributeId(INodeColumnSource info)
  {
    foreach (KeyValuePair<int, INodeColumnSource> docRowField in this.docRowFields)
    {
      if (docRowField.Value is AttributeInfo attributeInfo && info is AttributeInfo attrInfo && attributeInfo.Equals(attrInfo))
        return docRowField.Key;
    }
    return -1;
  }

  public NodeColumn GetColumn(INodeColumnSource info)
  {
    int attributeId = this.GetAttributeId(info);
    return attributeId != -1 ? this.GetColumnByAttributeID((object) attributeId) : (NodeColumn) null;
  }

  protected override NodeColumn CreateColumn(
    Guid schemeGuid,
    int columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    if (!(this.FindAttribute(columnID) is AttributeInfo attribute))
      return (NodeColumn) null;
    NodeColumn column = new NodeColumn(schemeGuid, (object) columnID, typeof (AttributeInfo), FieldTypes.ftString, attribute.Name, sortOrder, sortIndex);
    column.Priority = SchemeColumnPriority.Highest;
    column.Source = (INodeColumnSource) attribute;
    this._createdColumns[(object) columnID] = column;
    return column;
  }

  public override AttributeInfo FindAttributeInfo(NodeColumn nodeColumn)
  {
    AttributeInfo attributeInfo = (AttributeInfo) null;
    if (nodeColumn == null)
      return attributeInfo;
    return this.FindAttribute((int) nodeColumn.ID) as AttributeInfo;
  }
}
