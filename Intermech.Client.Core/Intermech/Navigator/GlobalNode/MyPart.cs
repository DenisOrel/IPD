
// Type: Intermech.Navigator.GlobalNode.MyPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.GlobalNode;

internal class MyPart(IServiceProvider services) : ObjectsPart(services)
{
  private const int AttributeId = -50;

  public override List<object> GetSpecialFields()
  {
    List<object> specialFields = base.GetSpecialFields();
    specialFields.Add((object) -50);
    return specialFields;
  }

  public override INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int int32_1 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_OBJECT_TYPE)]);
    long int64_1 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_OBJECT_ID)]);
    long int64_2 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_ID)]);
    long int64_3 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_CHKOUT_BY)]);
    int int32_2 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) -50)]);
    long objId = int64_1;
    long id = int64_2;
    long checkedOutBy = int64_3;
    int myStatus = int32_2;
    return (INodeID) new MyNodeId(int32_1, objId, id, checkedOutBy, myStatus);
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return dataFormat == typeof (IMyStatus) && nodeID is IMyStatus ? (object) (IMyStatus) nodeID : base.GetData(nodeID, dataFormat);
  }
}
