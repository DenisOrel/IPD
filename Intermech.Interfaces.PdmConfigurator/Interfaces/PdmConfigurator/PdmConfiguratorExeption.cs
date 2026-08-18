// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmConfiguratorExeption
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Базовое исключение, генерируемое конфигуратором составов IPS
/// </summary>
[Serializable]
public class PdmConfiguratorExeption : Exception
{
  /// <summary>Создать экземпляр класса</summary>
  public PdmConfiguratorExeption()
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="message">Текст сообщения исключения</param>
  public PdmConfiguratorExeption(string message)
    : base(message)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="info">Дополнительная информация</param>
  /// <param name="context">Контекст</param>
  protected PdmConfiguratorExeption(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="message">Сообщение</param>
  /// <param name="innerException">Вложенное исключение</param>
  public PdmConfiguratorExeption(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
