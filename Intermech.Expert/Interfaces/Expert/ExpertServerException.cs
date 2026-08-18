// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.ExpertServerException
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>This exception is send by expert server</summary>
[Serializable]
public class ExpertServerException : Exception, ISerializable
{
  public ExpertServerException(string Message)
    : base(LocalizationHolder.rm.GetString("Interfaces_15") + Message)
  {
  }

  public ExpertServerException(string Message, Exception innerException)
    : base(LocalizationHolder.rm.GetString("Interfaces_15") + Message, innerException)
  {
  }

  protected ExpertServerException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
  }
}
