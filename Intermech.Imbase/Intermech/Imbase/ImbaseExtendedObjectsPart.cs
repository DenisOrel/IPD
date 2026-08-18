// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseExtendedObjectsPart
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;

#nullable disable
namespace Intermech.Imbase;

public class ImbaseExtendedObjectsPart(int objTypeID, IServiceProvider services) : ObjectsPart(objTypeID, services)
{
  public override INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int int32_1 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_TYPE)]);
    long int64_1 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_ID)]);
    long int64_2 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ID)]);
    long int64_3 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_CHKOUT_BY)]);
    int int32_2 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_LC_STEP)]);
    string str1 = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncCAPTION)]);
    long int64_4 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncOWNER)]);
    long int64_5 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncVERSION)]);
    long int64_6 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncBASE_VERSION)]);
    string str2 = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID)]);
    long int64_7 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncMODIFICATION_ID)]);
    byte[] fieldValue = adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ELEMENT_STATUSES) <= -1 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ELEMENT_STATUSES)] == DBNull.Value ? (byte[]) null : fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ELEMENT_STATUSES)] as byte[];
    ObjectFiltrationState objectFiltrationState = ObjectFiltrationState.fsNotRequired;
    if (fieldValue != null)
      objectFiltrationState = (ObjectFiltrationState) (ServicesManager.GetService(typeof (IElementStatusesClientService)) as IElementStatusesClientService).GetElementStatuses32("cad005f2-306c-11d8-b4e9-00304f19f545", fieldValue);
    long objId = int64_1;
    long id = int64_2;
    long checkedOutBy = int64_3;
    int lcStepID = int32_2;
    string caption = str1;
    long owner = int64_4;
    int state = (int) objectFiltrationState;
    long version = int64_5;
    long baseVersion = int64_6;
    string siteID = str2;
    Guid empty = Guid.Empty;
    long modificationID = int64_7;
    return (INodeID) new ImbaseExtendedNodeID(new CreateObjectNodeParams(int32_1, objId, id, checkedOutBy, -1L, lcStepID, caption, -1, owner, 0L, (ObjectFiltrationState) state, version, baseVersion, siteID, 0L, empty, modificationID));
  }
}
