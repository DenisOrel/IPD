// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.StatisticsUsers
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics.Interfaces;

/// <summary>
/// Класс, описывающий пользователя статистики.
/// StatisticsUsers может быть пользователем, группой, либо подразделением.
/// </summary>
[Serializable]
public class StatisticsUsers
{
  /// <summary>Тип пользователей</summary>
  [XmlElement(ElementName = "UserType")]
  public UsersEnum UserType { get; set; }

  [XmlElement(ElementName = "ObjectID")]
  public long ObjectID { get; set; }

  [XmlElement(ElementName = "ID")]
  public long ID { get; set; }

  [XmlElement(ElementName = "Caption")]
  public string Caption { get; set; }
}
