// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.Email.EMailMessageColumnScheme
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow.Client.Email;

internal class EMailMessageColumnScheme : INodeColumnScheme
{
  public string Name => LocalizationHolder.rm.GetString("Workflow.Client_69");

  public string ColumnIDToPersistName(object columnID)
  {
    switch (columnID)
    {
      case int attrTypeID:
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
        if (attributeType != null)
          return attributeType.AttributeGuid.ToString();
        break;
      case Guid guid:
        return guid.ToString();
    }
    return string.Empty;
  }

  public object PersistNameToColumnID(string persistName) => (object) new Guid(persistName);

  public NodeColumn CreateColumn(Guid schemeGuid, object columnID)
  {
    NodeColumn column = this.CreateColumn(schemeGuid, columnID, NodeColumnSortOrder.None, -1);
    column.Priority = SchemeColumnPriority.High;
    return column;
  }

  public NodeColumn CreateColumn(
    Guid schemeGuid,
    object columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    if (columnID is Guid attrTypeGuid)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeGuid);
      if (attributeType != null)
      {
        NodeColumn column = this.CreateColumn(schemeGuid, attributeType, sortOrder, sortIndex);
        column.Priority = SchemeColumnPriority.Highest;
        return column;
      }
    }
    return (NodeColumn) null;
  }

  public INodeColumnTransform GetDefaultTransform(object columnID) => (INodeColumnTransform) null;

  private NodeColumn CreateColumn(
    Guid schemeGuid,
    IMSAttributeType attrType,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    return new NodeColumn(schemeGuid, (object) attrType.AttributeID, Intermech.Navigator.DBObjects.Helper.ConvertType(attrType.RealFieldType), attrType.RealFieldType, attrType.Name, sortOrder, sortIndex, attrType.ShortName, attrType.Name, (attrType.Options & AttributeOptions.Internal) != 0)
    {
      Priority = SchemeColumnPriority.High
    };
  }
}
