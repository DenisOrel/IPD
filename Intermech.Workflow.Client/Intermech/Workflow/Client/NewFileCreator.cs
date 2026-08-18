// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.NewFileCreator
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces.Client;
using Intermech.Workflow.Design;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Client;

internal class NewFileCreator : IObjectCreatorCustomService
{
  public long CreateObjectDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    List<long> longList = wfFunx.AddFileToObject(wfConsts.FileTypeID, 0L, false);
    return longList.Count > 0 ? longList[0] : 0L;
  }
}
