// Decompiled with JetBrains decompiler
// Type: Intermech.DBClean.Client.CleanEnum
// Assembly: Intermech.DBClean.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 973F13FD-72F3-4555-9BF9-74AC5C606885
// Assembly location: D:\IPS\Client\Intermech.DBClean.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.DBClean.Client.xml

using System.Xml.Serialization;

#nullable disable
namespace Intermech.DBClean.Client;

public enum CleanEnum
{
  [XmlEnum(Name = "n")] None,
  [XmlEnum(Name = "d")] Delete,
  [XmlEnum(Name = "c")] Clean,
}
