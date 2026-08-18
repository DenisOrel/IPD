
// Type: Intermech.Client.Core.ScriptTypes
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core;

/// <summary>Тип скрипта / сценария</summary>
public enum ScriptTypes
{
  /// <summary>Неизвестно</summary>
  Unknown,
  /// <summary>Скрипт Workflow</summary>
  [CreateObjectTypeGuid("cad0036a-306c-11d8-b4e9-00304f19f545")] Workflow,
  /// <summary>Сценарий генерации документа</summary>
  [CreateObjectTypeGuid("cadd939d-306c-11d8-b4e9-00304f19f545")] ExpertReport,
  /// <summary>Сценарий генерации комплекта документов</summary>
  [CreateObjectTypeGuid("cadd939c-306c-11d8-b4e9-00304f19f545")] ExpertComplectDoc,
  /// <summary>Скрипт планировщика задач</summary>
  [CreateObjectTypeGuid("cadd94cd-306c-11d8-b4e9-00304f19f545")] Scheduler,
  /// <summary>Cкрипт нумерации технологических объектов</summary>
  [CreateObjectTypeGuid("cad001c5-306c-11d8-b4e9-00304f19f545")] TechNumeration,
  /// <summary>Cкрипт шагов жизненного цикла</summary>
  [CreateObjectTypeGuid("cadd94ff-306c-11d8-b4e9-00304f19f545")] LCStep,
  /// <summary>Сценарий автоподбора</summary>
  [CreateObjectTypeGuid("cadd98d5-306c-11d8-b4e9-00304f19f545")] AutoSelection,
  /// <summary>Сценарии для кнопок форм редактирования</summary>
  [CreateObjectTypeGuid("cadd9962-306c-11d8-b4e9-00304f19f545")] ScriptsForButtons,
  /// <summary>Скрипт Workflow</summary>
  [CreateObjectTypeGuid("cadd996d-306c-11d8-b4e9-00304f19f545")] WorkflowLocal,
  /// <summary>Скрипт Workflow</summary>
  [CreateObjectTypeGuid("cadd996e-306c-11d8-b4e9-00304f19f545")] WorkflowCommon,
  /// <summary>Сценарии для экспертной системы</summary>
  [CreateObjectTypeGuid("cadd94bb-306c-11d8-b4e9-00304f19f545")] ExpertScenario,
  /// <summary>Сценарии для отчетов о сравнении объектов</summary>
  [CreateObjectTypeGuid("cadd9b67-306c-11d8-b4e9-00304f19f545")] ReportCompareObjects,
}
