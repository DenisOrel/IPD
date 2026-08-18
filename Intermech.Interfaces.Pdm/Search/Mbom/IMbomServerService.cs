// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Mbom.IMbomServerService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Search.Mbom;

public interface IMbomServerService
{
  void AddToMbom(Guid userSessionGuid, AddingToMbomParams addingToMbomParams);

  long CreateMbom(Guid userSessionGuid, long ebomVersionID);

  AddingToMbomInfo FindAddingToMbomInfo(Guid userSessionGuid, long ebomVersionID);

  long FindEbomForMbom(Guid userSessionGuid, long mbomVersionID);

  long FindMbomForEbom(Guid userSessionGuid, long ebomVersionID);

  void AddTauToMbom(Guid userSessionGuid, long mbomVersionID, long tauVersionID);

  void TransferTauToMbom(
    Guid userSessionGuid,
    long destinationMbomVersionID,
    long tauVersionID,
    long sourceRelationID);
}
