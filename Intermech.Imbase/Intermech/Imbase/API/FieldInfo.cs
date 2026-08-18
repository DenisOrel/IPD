// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.FieldInfo
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Imbase.API;

[DebuggerDisplay("{LongName}[{ShortName}] {FieldType} {FieldKind}")]
[Serializable]
internal struct FieldInfo
{
  public const int CADMECH_FLAGS = 67170304 /*0x0400F000*/;
  public string LongName;
  public string ShortName;
  public int AttributeId;
  public int Flags;
  public string Units;
  public bool Required;
  public FieldType FieldType;
  public FieldKind FieldKind;
  public int DataSize;
  public int DataOffset;
}
