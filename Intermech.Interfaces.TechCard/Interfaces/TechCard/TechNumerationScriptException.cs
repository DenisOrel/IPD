// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumerationScriptException
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Исключение при выполнении скрипта нумерации</summary>
[Serializable]
public class TechNumerationScriptException : TechNumerationServerException
{
  /// <summary>Конструктор</summary>
  /// <param name="message"></param>
  public TechNumerationScriptException(string message)
    : this(message, (Exception) null)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="message"></param>
  /// <param name="innerException"></param>
  public TechNumerationScriptException(string message, Exception innerException)
    : base($"{LocalizationHolder.rm.GetString("Interfaces.TechCard_22")} - {message}", innerException)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected TechNumerationScriptException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
