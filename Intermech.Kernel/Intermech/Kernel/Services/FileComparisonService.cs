// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.FileComparisonService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Data;


namespace Intermech.Kernel.Services;

public sealed class FileComparisonService : LongLifeObject, IFileComparisonService
{
  public bool DocsAreComposite(Guid sessionGUID, long[] docIds)
  {
    DataTable dataTable = UserSession.GetSessionByID(sessionGUID) is UserSession sessionById ? sessionById.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad0057c-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-21, RelationalOperators.In, (object) docIds, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    })) : throw new KernelException($"Сессия с гуидом {sessionGUID} не найдена.");
    return dataTable != null && dataTable.Rows.Count > 0;
  }
}
