
// Type: Intermech.Redline.RedliningWorkflowHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Kernel.Search;
using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Redline;

public sealed class RedliningWorkflowHelper
{
  public Tuple<long, int, string, string> FindAnyActiveProcess(long documentId)
  {
    foreach (int activitiesWithAttachment in RedliningWorkflowHelper.InternalCaches.IDCache.UserActivitiesWithAttachments)
    {
      Tuple<long, string, string> anyActiveProcess = this.FindAnyActiveProcess(documentId, activitiesWithAttachment);
      if (anyActiveProcess != null)
        return Tuple.Create<long, int, string, string>(anyActiveProcess.Item1, activitiesWithAttachment, anyActiveProcess.Item2, anyActiveProcess.Item3);
    }
    return (Tuple<long, int, string, string>) null;
  }

  public Tuple<long, string, string> FindAnyActiveProcess(long documentId, int activityType)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(RedliningWorkflowHelper.InternalCaches.IDCache.ActionStatus.Id, RelationalOperators.In, (object) RedliningWorkflowHelper.InternalCaches.IDCache.RunningStatusValues, (object) null, LogicalOperators.NONE, 0, true, AttributeSourceTypes.Object)
    }, new object[3]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.CAPTION,
      (object) RedliningWorkflowHelper.InternalCaches.IDCache.ParentProcess.Id
    });
    paramSet.ColumnsInfo = new ColumnInfo[3]
    {
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, (object) null),
      new ColumnInfo((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, (object) null),
      new ColumnInfo((object) RedliningWorkflowHelper.InternalCaches.IDCache.ParentProcess.Id, AttributeSourceTypes.Object, (object) null)
    };
    paramSet.RecordCount = 1;
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(RedliningWorkflowHelper.InternalCaches.IDCache.Attachments.Id);
      relationCollection.ObjectTypeID = activityType;
      dataTable = relationCollection.EntersInVersion(paramSet, documentId);
    }
    if (dataTable.Rows.Count == 0)
      return (Tuple<long, string, string>) null;
    DataRow row = dataTable.Rows[0];
    return Tuple.Create<long, string, string>(Convert.ToInt64(row[0]), Convert.ToString(row[1]), Convert.ToString(row[2]));
  }

  private static class InternalCaches
  {
    private static readonly RedliningWorkflowHelper.InternalIDCache idCache = new RedliningWorkflowHelper.InternalIDCache(MetadataResolvers.Factory);

    public static RedliningWorkflowHelper.InternalIDCache IDCache
    {
      [DebuggerStepThrough] get => RedliningWorkflowHelper.InternalCaches.idCache;
    }
  }

  private sealed class InternalIDCache
  {
    public InternalIDCache(MetadataResolverFactory metadataResolvers)
    {
      this.ParentProcess = metadataResolvers.AttributeTypeResolver(new Guid("CAD002CE-306C-11D8-B4E9-00304F19F545"));
      this.ActionStatus = metadataResolvers.AttributeTypeResolver(new Guid("CAD002CD-306C-11D8-B4E9-00304F19F545"));
      this.Attachments = metadataResolvers.RelationTypeResolver(new Guid("CAD01329-306C-11D8-B4E9-00304F19F545"));
      this.Tasks = metadataResolvers.ObjectTypeResolver(new Guid("CAD002B5-306C-11D8-B4E9-00304F19F545"));
      this.Approvals = metadataResolvers.ObjectTypeResolver(new Guid("CAD002B4-306C-11D8-B4E9-00304F19F545"));
      this.RunningStatusValues = new int[5]{ 0, 1, 2, 3, 4 };
      this.UserActivitiesWithAttachments = new int[2]
      {
        this.Tasks.Id,
        this.Approvals.Id
      };
    }

    public AttributeTypeResolver ParentProcess { get; private set; }

    public AttributeTypeResolver ActionStatus { get; private set; }

    public RelationTypeResolver Attachments { get; private set; }

    public ObjectTypeResolver Tasks { get; private set; }

    public ObjectTypeResolver Approvals { get; private set; }

    public int[] RunningStatusValues { get; private set; }

    public int[] UserActivitiesWithAttachments { get; private set; }
  }
}
