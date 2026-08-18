
// Type: Intermech.Search.NodeColumnTransforms.NodeColumnTransformsClientHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.NodeColumnTransforms;

public static class NodeColumnTransformsClientHelper
{
  private static NodeColumnTransformsClientHelper.NavigatorCellFeatureRecordAdapter _navigatorCellFeatureRecordAdapter = new NodeColumnTransformsClientHelper.NavigatorCellFeatureRecordAdapter();

  public static object GetCellValue(object item, NodeColumn nodeColumn)
  {
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    if (nodeColumn == null)
      throw new ArgumentNullException(nameof (nodeColumn));
    _Object @object = (_Object) null;
    Relation relation = (Relation) null;
    switch (item)
    {
      case _Object _:
        @object = (_Object) item;
        break;
      case CompositionPart _:
        @object = ((RelationObjectBase) item).Object;
        relation = ((RelationObjectBase) item).Relation;
        break;
      case Relation _:
        relation = (Relation) item;
        break;
      case IObjectHolder _:
        @object = ((IObjectHolder) item).Object;
        break;
    }
    object cellValue = (object) null;
    if (nodeColumn.Attribute != null)
    {
      AttributeSourceTypes attributeSourceTypes = nodeColumn.AttrSource;
      if (attributeSourceTypes == AttributeSourceTypes.Auto && AttributeTypeHelper.IsSystemAttributeTypeID(nodeColumn.Attribute.AttributeID))
        attributeSourceTypes = ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) nodeColumn.Attribute.AttributeID);
      object sourceValue = (object) null;
      if (attributeSourceTypes == AttributeSourceTypes.Object && @object != null)
        sourceValue = @object.Attributes.GetAttributeValue(nodeColumn.Attribute.AttributeID);
      else if (attributeSourceTypes == AttributeSourceTypes.Relation && relation != null)
        sourceValue = relation.Attributes.GetAttributeValue(nodeColumn.Attribute.AttributeID);
      if (sourceValue != null)
      {
        IColumnSchemes columnSchemes = ServiceLocator.Get<IColumnSchemes>();
        INodeColumnTransform nodeColumnTransform = (INodeColumnTransform) null;
        switch (attributeSourceTypes)
        {
          case AttributeSourceTypes.Object:
            nodeColumnTransform = !AttributeTypeHelper.IsSystemAttributeTypeID(nodeColumn.Attribute.AttributeID) ? columnSchemes.GetDefaultTransform(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (object) nodeColumn.Attribute.AttributeID) : columnSchemes.GetDefaultTransform(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) nodeColumn.Attribute.AttributeID);
            break;
          case AttributeSourceTypes.Relation:
            nodeColumnTransform = !AttributeTypeHelper.IsSystemAttributeTypeID(nodeColumn.Attribute.AttributeID) ? columnSchemes.GetDefaultTransform(Intermech.Navigator.Consts.RelationColumnSchemeGuid, (object) nodeColumn.Attribute.AttributeID) : columnSchemes.GetDefaultTransform(Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid, (object) nodeColumn.Attribute.AttributeID);
            break;
        }
        if (nodeColumnTransform != null)
        {
          int[] array1 = @object.Attributes.Select<_Attribute, int>((Func<_Attribute, int>) (o => o.TypeID)).ToArray<int>();
          object[] array2 = @object.Attributes.Select<_Attribute, object>((Func<_Attribute, object>) (o => o.Value)).ToArray<object>();
          NodeColumnTransformsClientHelper._navigatorCellFeatureRecordAdapter.Initialize(array1, array2);
          cellValue = nodeColumnTransform.Apply(sourceValue, nodeColumn, (object) NodeColumnTransformsClientHelper._navigatorCellFeatureRecordAdapter, array2);
        }
        else
          cellValue = sourceValue;
      }
    }
    return cellValue;
  }

  private sealed class NavigatorCellFeatureRecordAdapter : RecordAdapter
  {
    private int[] _attributeTypeIds;
    private object[] _attributeValues;

    public void Initialize(int[] attributeTypeIds, object[] attributeValues)
    {
      if (attributeTypeIds == null)
        throw new ArgumentNullException(nameof (attributeTypeIds));
      if (attributeValues == null)
        throw new ArgumentNullException(nameof (attributeValues));
      if (attributeTypeIds.Length != attributeValues.Length)
        throw new ArgumentException();
      this._attributeTypeIds = attributeTypeIds;
      this._attributeValues = attributeValues;
    }

    public override int GetFieldIndex(object field)
    {
      if (field == null)
        throw new ArgumentNullException(nameof (field));
      return Array.IndexOf<int>(this._attributeTypeIds, !(field is NodeColumnID) ? AttributeTypeHelper.ConvertToAttributeTypeID(field) : AttributeTypeHelper.ConvertToAttributeTypeID(((NodeColumnID) field).ID));
    }

    public override object[] GetRawRecordValues(object[] fieldValues)
    {
      List<object> objectList = new List<object>();
      foreach (object fieldValue in fieldValues)
      {
        object attributeValue = this._attributeValues[this.GetFieldIndex(fieldValue)];
        objectList.Add(attributeValue);
      }
      return objectList.ToArray();
    }

    public override object[] GetRecordValues(object[] fieldValues)
    {
      throw new NotSupportedException();
    }
  }
}
