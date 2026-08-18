// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.ScriptProcHandler
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Generic delegate for user procedures (for scripts)</summary>
/// <param name="ti">TaskInfo, в котором есть вся необходимая информация</param>
/// <param name="context">Список объектов контекста</param>
/// <param name="dTable">Локальная таблица данных</param>
/// <param name="objType">Идентификатор типа объекта (-1 если нет)</param>
/// <param name="attrType">Идентификатор типа атрибута (-1 если нет)</param>
/// <param name="Value">Значение-параметр (null если нет)</param>
public delegate void ScriptProcHandler(
  object ti,
  long[] context,
  HybridTableExp dTable,
  int objType,
  int attrType,
  object Value);
