// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.BarCodes.ParityEnum
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Archives.BarCodes;

[TypeConverter(typeof (EnumDescConverter))]
public enum ParityEnum
{
  [Description("Нет")] None,
  [Description("Отрицательная четность(odd)")] Odd,
  [Description("По нулевому биту(even)")] Even,
  [Description("По единичному биту(mark)")] Mark,
  [Description("По нулевому биту(space)")] Space,
}
