// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Analogs.AnalogsSelectionStatuses
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Search.Pdm.Analogs;

[Flags]
public enum AnalogsSelectionStatuses
{
  None = 0,
  [Description("Действующий аналог. Объект является единственным аналогом, который назначен действующим на данную дату.")] ActualAnalog = 1,
  [Description("Приоритетный или единственный аналог. Объект является аналогом, назначенным в качестве приоритетного аналога при выполнении подбора аналогов для объекта. Либо на объект назначен только этот аналог.")] PriorityOrOneAnalog = 2,
  [Description("Аналог. Объект является просто одним из аналогов, назначенных на объект. Он не является приоритетным, на него либо не назначен период действия, либо его период действия не соответствует заданной дате.")] Analog = 4,
  [Description("На объект назначены аналоги. Этот статус рисуется рядом с объектом в составе, для которого в базе назначены аналоги.")] AnalogsExist = 8,
}
