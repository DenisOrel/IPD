// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.ScenarioTrace
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.Expert.Scenarios;

internal static class ScenarioTrace
{
  internal static readonly TraceSwitch General = new TraceSwitch("Expert.Scenarios", string.Empty, "0");
}
