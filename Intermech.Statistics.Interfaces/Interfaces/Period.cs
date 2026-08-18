// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.Period
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics.Interfaces;

[Serializable]
public class Period
{
  /// <summary>Время начала подсчета статистики</summary>
  [XmlElement(ElementName = "StartDateTime")]
  public DateTime StartDateTime { get; }

  /// <summary>Время окончания подсчета статистики</summary>
  [XmlElement(ElementName = "EndDateTime")]
  public DateTime EndDateTime { get; }

  public Period(DateTime start, DateTime end)
  {
    this.StartDateTime = start;
    this.EndDateTime = end;
  }

  public Period() => this.StartDateTime = this.EndDateTime = DateTime.MinValue;

  public string ToString(string format)
  {
    DateTime dateTime = this.StartDateTime;
    string str1 = dateTime.ToString(format);
    dateTime = this.EndDateTime;
    string str2 = dateTime.ToString(format);
    return $"{str1} - {str2}";
  }
}
