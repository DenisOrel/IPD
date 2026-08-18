// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.AnalyzeSaveSubsitutesResult
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

[Serializable]
public sealed class AnalyzeSaveSubsitutesResult
{
  public AnalyzeSaveSubsitutesResult()
  {
    this.ChangesPackDictionary = new Dictionary<long, AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack>();
  }

  public Dictionary<long, AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack> ChangesPackDictionary { get; private set; }

  [Serializable]
  public sealed class SaveSubsitutesChangesPack
  {
    public SaveSubsitutesChangesPack()
    {
      this.ToAddRelations = new List<Relation>();
      this.ToChangeRelations = new List<Relation>();
      this.ToClearRelationIds = new List<long>();
      this.ToRemoveRelationIds = new List<long>();
    }

    public List<Relation> ToAddRelations { get; private set; }

    public List<Relation> ToChangeRelations { get; private set; }

    public List<long> ToClearRelationIds { get; private set; }

    public List<long> ToRemoveRelationIds { get; private set; }
  }
}
