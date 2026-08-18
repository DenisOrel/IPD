// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.DataType
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert;

/// <summary>ExpDataType is internal data type</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Expert_43")]
[Category("Expert System")]
public enum DataType
{
  [CustomDescription("Attribute.Expert_44")] Integer,
  [CustomDescription("Attribute.Expert_45")] Float,
  [CustomDescription("Attribute.Expert_46")] Measured,
  [CustomDescription("Attribute.Expert_47")] String,
  [CustomDescription("Attribute.Expert_48")] Date,
  [CustomDescription("Attribute.Expert_49")] Boolean,
  [CustomDescription("Attribute.Expert_50")] ObjectLink,
  [CustomDescription("Attribute.Expert_51")] Packet,
  [CustomDescription("Attribute.Expert_52")] Diap,
  [CustomDescription("Attribute.Expert_53")] Attribute,
  [CustomDescription("Attribute.Expert_54")] ObjType,
  [CustomDescription("Attribute.Expert_55")] RelType,
  [CustomDescription("Attribute.Expert_232")] ObjectIdLink,
  Unknown,
}
