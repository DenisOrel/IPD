// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.NormalDesignationParser
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System.Data;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class NormalDesignationParser : IIndParser
{
  private Regex normalDesignation = new Regex($"^(?'section'[{"OABDSPMK"}])(?'designation'(?'underscore'_?)(?'tail'.*))$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Singleline);

  public void Unpack(string indValue, DataRow row)
  {
    Match match = this.normalDesignation.Match(indValue);
    if (!match.Success)
      throw IndParserUtils.MakeCantParseException(indValue);
    row["TAGGING_MODE"] = (object) (match.Groups["underscore"].Length != 0 ? 1 : 0);
    row["TAG_SECTION_CODE"] = (object) match.Groups["section"].Value[0];
    row["TAG"] = (object) match.Groups["designation"].Value;
  }

  public bool CanUnpack(string indValue) => this.normalDesignation.IsMatch(indValue);
}
