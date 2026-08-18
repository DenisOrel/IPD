// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.ScenarioHelper
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Expert.Scenarios;

/// <summary>Вспомогательные статические методы для сценариев</summary>
public static class ScenarioHelper
{
  public static string ReadCodeFromAttribute(IDBObject scenario)
  {
    IMemoReader attributeByGuid = scenario.GetAttributeByGuid(ScenarioGUIDs.attributeScenarioCode) as IMemoReader;
    char[] chArray = (char[]) null;
    if (attributeByGuid.OpenMemo(0) > 0)
    {
      try
      {
        chArray = attributeByGuid.ReadDataBlock();
      }
      finally
      {
        attributeByGuid.CloseMemo();
      }
    }
    return chArray != null ? new string(chArray) : string.Empty;
  }
}
