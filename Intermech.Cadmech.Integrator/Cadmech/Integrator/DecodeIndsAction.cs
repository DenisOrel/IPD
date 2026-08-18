// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DecodeIndsAction
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class DecodeIndsAction : IDecodeAction
{
  public void Run(DecodeData decodeData)
  {
    this.WriteDesignations(decodeData.StructFile.Rows, decodeData.Job.BaseProjectDesignation, decodeData.Job.SuffixMode);
  }

  private void WriteDesignations(List<RowData> rows, string baseDesignation, bool suffixMode)
  {
    for (int index1 = 0; index1 < rows.Count; ++index1)
    {
      List<OccurenceRef> refs = rows[index1].Refs;
      for (int index2 = 0; index2 < refs.Count; ++index2)
        refs[index2].Designation = this.CalcDesignation(refs[index2], baseDesignation, suffixMode);
    }
  }

  private string CalcDesignation(OccurenceRef occRef, string baseDesignation, bool suffixMode)
  {
    if (occRef.Ind == "<AllProjects>")
      return string.Empty;
    if (occRef.Ind == "<BasicProject>")
      return baseDesignation;
    return suffixMode && occRef.Ind.Length < 4 ? $"{baseDesignation}-{occRef.Ind}" : occRef.Ind;
  }
}
