
// Type: Intermech.Navigator.EventLog.EventsNodePart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System.Collections.Specialized;


namespace Intermech.Navigator.EventLog;

public class EventsNodePart : EventsNodePartBase
{
  /// <summary>Список условий запроса</summary>
  private ConditionStructure[] conditions;
  /// <summary>Дополнительные параметры запроса</summary>
  private HybridDictionary conditionTags;

  public EventsNodePart(ConditionStructure[] conditions, HybridDictionary conditionTags)
  {
    this.conditions = conditions;
    this.conditionTags = conditionTags;
  }

  /// <summary>Список условий запроса</summary>
  protected override ConditionStructure[] Conditions => this.conditions;

  /// <summary>Дополнительные параметры запроса</summary>
  protected override HybridDictionary ConditionTags => this.conditionTags;
}
