// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.BarCodes.StopBitsEnum
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Archives.BarCodes;

[TypeConverter(typeof (EnumDescConverter))]
public enum StopBitsEnum
{
  [Description("Нет")] None,
  [Description("1 стоп бит")] One,
  [Description("2 стоп бита")] Two,
  [Description("1.5 стоп бита")] OnePointFive,
}
