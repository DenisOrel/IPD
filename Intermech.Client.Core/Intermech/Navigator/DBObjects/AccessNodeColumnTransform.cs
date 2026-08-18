
// Type: Intermech.Navigator.DBObjects.AccessNodeColumnTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using System;


namespace Intermech.Navigator.DBObjects;

public sealed class AccessNodeColumnTransform : INodeColumnTransform
{
  public Type DataType => typeof (string);

  public object Apply(object sourceValue, NodeColumn column, object adapter, object[] allValues)
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Constants.SecurityLevelAttributeTypeID);
    if (sourceValue == null)
      return (object) string.Empty;
    int index = attributeType.PossibleValues.IndexOf((object) Convert.ToInt64(sourceValue));
    return index < 0 || index >= attributeType.PossibleValuesDescriptions.Count ? (object) string.Empty : attributeType.PossibleValuesDescriptions[index];
  }
}
