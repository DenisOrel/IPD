// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.FieldWithNameNotFoundException
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Extensions;

[Serializable]
public class FieldWithNameNotFoundException : InvalidOperationException, ISerializable
{
  [NotNull]
  [NotWhitespace]
  public readonly string FieldName;

  public FieldWithNameNotFoundException([NotNull, NotWhitespace] string fieldName)
    : base($"Поле с именем \"{fieldName}\" не найдено")
  {
    this.FieldName = fieldName;
  }

  public FieldWithNameNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.FieldName = info.GetNotWhitespaceString(nameof (FieldName));
  }

  public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("FieldName", (object) this.FieldName);
  }
}
