// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.CompilerHelper
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

#nullable disable
namespace Intermech.Expert.Scenarios;

/// <summary>Вспомогательные статические методы компилятора</summary>
internal static class CompilerHelper
{
  public static ICompiler GetCompiler(ScenarioLangs langs)
  {
    return langs == ScenarioLangs.CSharp ? (ICompiler) new CSCompiler() : (ICompiler) null;
  }
}
