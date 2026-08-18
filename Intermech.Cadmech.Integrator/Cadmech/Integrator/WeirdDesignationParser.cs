// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.WeirdDesignationParser
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System.Data;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class WeirdDesignationParser : IIndParser
{
  private Regex weirdImbaseKey = new Regex($"^(?'section'[{"OABDSPMK"}])(?<key>(?'sign'(I6)|(IG))(?'keytail'.*))$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Singleline);

  public void Unpack(string indValue, DataRow row)
  {
    Match match = this.weirdImbaseKey.Match(indValue);
    if (!match.Success)
      throw IndParserUtils.MakeCantParseException(indValue);
    row["TAGGING_MODE"] = (object) 2;
    row["TAG_SECTION_CODE"] = (object) match.Groups["section"].Value[0];
    row["TAG"] = (object) match.Groups["key"].Value;
  }

  public bool CanUnpack(string indValue) => this.weirdImbaseKey.IsMatch(indValue);
}
