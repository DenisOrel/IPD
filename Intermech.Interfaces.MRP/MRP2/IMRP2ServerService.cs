// Decompiled with JetBrains decompiler
// Type: Intermech.MRP2.IMRP2ServerService
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Interfaces.Pdm;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.MRP2;

public interface IMRP2ServerService
{
  void RecalculateProductionCopyHash(Guid sessionGuid, long objectId);

  void SetPLForCopy(Guid sessionGuid, long oldRelationID, long PlObjectID, long copyObjectID);

  void ReplacePartFromSubstitute(
    Guid sessionGuid,
    long relationID1,
    long copyGroupId,
    long versionPL,
    long projObjectID,
    List<long> relIds,
    SubstituteObjects substitutes);
}
