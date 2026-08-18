// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.ISubstitutesServerService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

public interface ISubstitutesServerService
{
  SubstitutePack FindSubstitutes(Guid userSessionGuid, long projectVersionID, int relationTypeID);

  void ActualizeSubstitute(Guid userSessionGuid, long relationID);

  AnalyzeSaveSubsitutesResult AnalyzeSaveSubstitutes(
    Guid userSessionGuid,
    SaveSubstitutesParams @params);

  void SaveSubstitutes(Guid userSessionGuid, SaveSubstitutesParams @params);

  void RemoveSubstitutes(Guid userSessionGuid, RemoveSubstitutesParams @params);

  long[] GetExistsSubstituteGroupNumbersFromOtherInstances(
    Guid userSessionGuid,
    long objectVersionID,
    int relationTypeID);
}
