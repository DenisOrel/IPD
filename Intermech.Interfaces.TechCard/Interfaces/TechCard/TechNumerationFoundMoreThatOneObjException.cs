// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumerationFoundMoreThatOneObjException
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>
/// Объявим свой Exception для случая, если требуется только один объект, а найдено больше
/// </summary>
[Serializable]
public class TechNumerationFoundMoreThatOneObjException : TechNumerationServerException
{
  /// <summary>Конструктор</summary>
  /// <param name="message">Текст сообщения</param>
  public TechNumerationFoundMoreThatOneObjException(string message)
    : base(message)
  {
  }

  /// <summary>Конструктор</summary>
  public TechNumerationFoundMoreThatOneObjException()
    : this(string.Empty)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected TechNumerationFoundMoreThatOneObjException(
    SerializationInfo info,
    StreamingContext context)
    : base(info, context)
  {
  }
}
