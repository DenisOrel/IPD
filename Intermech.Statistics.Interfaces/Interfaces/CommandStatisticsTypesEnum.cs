// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.CommandStatisticsTypesEnum
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics.Interfaces;

[Serializable]
public enum CommandStatisticsTypesEnum
{
  [XmlEnum(Name = "None"), Description("Неизвестный тип объекта статистики")] None = -1, // 0xFFFFFFFF
  [XmlEnum(Name = "CreatedDate"), Description("По дате создания объектов")] CreatedDate = 0,
  [XmlEnum(Name = "SignDate"), Description("По дате подписания объектов")] SignDate = 1,
  [XmlEnum(Name = "LCStepDate"), Description("По дате перемещения на шаг ЖЦ")] LCStepDate = 2,
  [XmlEnum(Name = "LCLevelDate"), Description("По дате перемещения на уровень продвижения")] LCLevelDate = 3,
  [XmlEnum(Name = "DateAttrValue"), Description("По значению атрибута типа Дата")] DateAttrValue = 4,
  [XmlEnum(Name = "ProcessTemplate"), Description("Анализатор времени выполнения процессов на основе шаблонов")] ProcessTemplate = 5,
  [XmlEnum(Name = "TimeInTask"), Description("Анализатор времени выполнения задач внутри шаблона")] TimeInTask = 6,
  [XmlEnum(Name = "TimeOneTaskFormUsers"), Description("Анализатор времени выполнения одной задачи разными пользователями")] TimeOneTaskFormUsers = 7,
  [XmlEnum(Name = "RevertCountTask"), Description("Анализатор количества возвратов у задач")] RevertCountTask = 8,
}
