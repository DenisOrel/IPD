// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.AttachmentsObjectsQuery
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;
using Intermech.Remoting;
using System;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Design;

internal sealed class AttachmentsObjectsQuery(
  INodeQuerySupport support,
  int objTypeID,
  ConditionStructure[] conditions,
  IServiceProvider services) : ObjectsQuery(support, objTypeID, conditions, services)
{
  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    RemotingCallContext.SetData("X-IPS-NoFilterQuery", "true");
    try
    {
      return base.GetDataTable(queryParams);
    }
    finally
    {
      RemotingCallContext.FreeNamedDataSlot("X-IPS-NoFilterQuery");
    }
  }
}
