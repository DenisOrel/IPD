// Decompiled with JetBrains decompiler
// Type: Intermech.Data.FieldIsEmptyException
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

#nullable disable
namespace Intermech.Data;

[Serializable]
public class FieldIsEmptyException : InvalidOperationException
{
  private const string ConstFieldNameValue = "FieldName";
  private const string ConstFieldIndexValue = "FieldIndex";
  [CanBeNull]
  public readonly string FieldName;
  public readonly int FieldIndex;

  public FieldIsEmptyException([CanBeNull] string fieldName)
    : base($"Field \"{fieldName ?? "{ошибка}"}\" value is empty")
  {
    this.FieldName = fieldName;
  }

  public FieldIsEmptyException(int fieldIndex)
    : base($"Field with index {fieldIndex} value is empty")
  {
    this.FieldIndex = fieldIndex;
  }

  [SecuritySafeCritical]
  protected FieldIsEmptyException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    foreach (SerializationEntry serializationEntry in info)
    {
      if (serializationEntry.Name == nameof (FieldName))
      {
        this.FieldName = Convert.ToString(serializationEntry.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      }
      if (serializationEntry.Name == nameof (FieldIndex))
      {
        this.FieldIndex = Convert.ToInt32(serializationEntry.Value);
        break;
      }
    }
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    if (this.FieldName != null)
      info.AddValue("FieldName", (object) this.FieldName);
    else
      info.AddValue("FieldIndex", this.FieldIndex);
  }

  public override string ToString()
  {
    return this.FieldName == null ? $"Field with index {this.FieldIndex} value is empty" : $"Field \"{this.FieldName}\" value is empty";
  }
}
