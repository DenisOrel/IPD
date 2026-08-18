// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.StatesRow
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase;

[TypeConverter(typeof (EnumDescConverter))]
[Flags]
public enum StatesRow
{
  None = 0,
  RequiredCahged = 1,
  Added = 2,
  Removed = 4,
}
