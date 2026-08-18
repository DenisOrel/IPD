// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.IndParserUtils
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal static class IndParserUtils
{
  public const string NormalTagFormat = "{0}{1}";
  public const string SectionCodes = "OABDSPMK";

  public static StructFileParsingException MakeCantParseException(string indValue)
  {
    return new StructFileParsingException($"Неизвестный формат значения '{indValue}' в поле IND обменного файла.");
  }
}
