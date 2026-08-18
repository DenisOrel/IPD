// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.CollectPeriodsEnum
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
public enum CollectPeriodsEnum
{
  [XmlEnum(Name = "Hour"), Description("Час")] Hour,
  [XmlEnum(Name = "Day"), Description("День")] Day,
  [XmlEnum(Name = "Week"), Description("Неделя")] Week,
  [XmlEnum(Name = "Month"), Description("Месяц")] Month,
  [XmlEnum(Name = "Year"), Description("Год")] Year,
}
