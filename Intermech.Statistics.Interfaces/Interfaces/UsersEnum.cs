// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.UsersEnum
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics.Interfaces;

[Serializable]
public enum UsersEnum
{
  [XmlEnum(Name = "User")] User,
  [XmlEnum(Name = "UserGroup")] UserGroup,
  [XmlEnum(Name = "Department")] Department,
}
