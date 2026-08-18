
// Type: Intermech.Navigator.DBObjects.ObjectLinkColumnTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjects;

public sealed class ObjectLinkColumnTransform : INodeColumnTransform
{
  public Type DataType => typeof (string);

  public object Apply(object sourceValue, NodeColumn column, object adapter, object[] allValues)
  {
    switch (sourceValue)
    {
      case long objectID:
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
          return dbObject != null ? (object) dbObject.Caption : (object) string.Empty;
        }
      case string _:
        return sourceValue;
      default:
        return (object) string.Empty;
    }
  }
}
