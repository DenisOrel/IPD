// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.DocOperType
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Expert;

[TypeConverter(typeof (EnumDescConverter))]
[Category("Expert System")]
[Flags]
[Serializable]
public enum DocOperType
{
  Created = 0,
  Changed = 1,
  Deleted = 2,
}
