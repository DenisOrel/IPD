// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumerationServerException
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>This exception is send by numeration service</summary>
[Serializable]
public class TechNumerationServerException : ApplicationException
{
  /// <summary>Конструктор</summary>
  /// <param name="message"></param>
  public TechNumerationServerException(string message)
    : this(message, (Exception) null)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="message"></param>
  /// <param name="innerException"></param>
  public TechNumerationServerException(string message, Exception innerException)
    : base(LocalizationHolder.rm.GetString("Interfaces.TechCard_2") + message, innerException)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected TechNumerationServerException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
  }
}
