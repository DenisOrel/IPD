// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AttachmentFuncs
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public class AttachmentFuncs
{
  public static DataTable GetAttachmentUsage(
    IUserSession sess,
    long ObjectID,
    ConditionStructure[] conds = null,
    ColumnDescriptor[] columns = null)
  {
    IDBRelationCollection relationCollection = sess.GetRelationCollection(wfConsts.AttachmentRelationTypeID);
    List<int> intList = MetaDataHelper.OptimizeChildObjectTypes((IEnumerable<int>) (sess.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService).GetPresentCompositionTypes((object) sess.SessionGUID, (IEnumerable<long>) new long[1]
    {
      ObjectID
    }, wfConsts.AttachmentRelationTypeID, false));
    relationCollection.ChildObjectTypes = (IList<int>) intList;
    if (columns == null)
      columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID)
      };
    DBRecordSetParams paramSet = new DBRecordSetParams(conds, columns);
    return relationCollection.EntersInVersion(paramSet, ObjectID);
  }

  public static DataTable GetAttachmentsUsage(
    IUserSession sess,
    long[] ObjectIDs,
    ConditionStructure[] conds = null,
    ColumnDescriptor[] columns = null,
    List<int> childTypes = null,
    bool expandECOs = false)
  {
    ICompositionLoadService customService = sess.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
    List<ObjInfoItem> objectInfoList1 = ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) ObjectIDs);
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objectInfoList1, sess);
    if (expandECOs)
    {
      DataTable dataTable = customService.LoadComplexCompositions((object) sess, (IEnumerable<ObjInfoItem>) objectInfoList1, (IEnumerable<int>) new List<int>((IEnumerable<int>) new int[1]
      {
        wfConsts.DocsInECORelationTypeID
      }), (IEnumerable<int>) null, (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>()
      {
        new ColumnDescriptor((object) -2)
      }, false, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, "cad001e0-306c-11d8-b4e9-00304f19f545", (Dictionary<long, HybridDictionary>) null, 1);
      if (dataTable != null)
      {
        List<long> objectIDs = new List<long>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          objectIDs.Add(Convert.ToInt64(row[0]));
        List<ObjInfoItem> objectInfoList2 = ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) objectIDs);
        ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objectInfoList2, sess);
        objectInfoList1.AddRange((IEnumerable<ObjInfoItem>) objectInfoList2);
      }
    }
    return customService.LoadComplexCompositions((object) sess, (IEnumerable<ObjInfoItem>) objectInfoList1, (IEnumerable<int>) new List<int>((IEnumerable<int>) new int[1]
    {
      wfConsts.AttachmentRelationTypeID
    }), (IEnumerable<int>) childTypes, (IEnumerable<ColumnDescriptor>) ((IEnumerable<ColumnDescriptor>) columns).ToList<ColumnDescriptor>(), false, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) conds, "cad001e0-306c-11d8-b4e9-00304f19f545", (Dictionary<long, HybridDictionary>) null, 1);
  }
}
