// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Synchronization.HierarchyCompareState
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Synchronization;

internal class HierarchyCompareState : BaseCompareState
{
  public override void Handle(SynchronizationAttributesAnalyzer context)
  {
    context.Log.AddMessage(MessageType.Extended, Environment.NewLine + "Анализ иерархии объектов Imbase");
    DataTable dataTable = (DataTable) null;
    IImbaseServer service = ApplicationServices.Container.GetService<IImbaseServer>();
    try
    {
      dataTable = service.GetFoldersForObjects(context.Session.SessionGUID, new long[1]
      {
        context.ImbaseObjectId
      }, (long[]) null);
    }
    catch
    {
    }
    if (dataTable == null || dataTable.Rows.Count <= 0)
      return;
    dataTable.DefaultView.Sort = $"{"F_PATH"} DESC";
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.DefaultView.ToTable().Rows)
    {
      long int64 = Convert.ToInt64(row["F_OBJECT_ID"]);
      IDBObject dbObject = context.Session.GetObject(int64);
      context.Log.AddMessage(MessageType.Extended, $"{Environment.NewLine}Анализ атрибутов объекта {dbObject.NameInMessages} [{dbObject.ObjectID}].");
      this.CompareWithObject(context, int64);
      if (context.FinishAnalyze)
        break;
    }
  }
}
