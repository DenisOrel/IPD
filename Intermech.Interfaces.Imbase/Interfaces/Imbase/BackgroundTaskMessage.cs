// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.BackgroundTaskMessage
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// Класс для хранения сообщения об обработке одного объекта.
/// </summary>
[Serializable]
public class BackgroundTaskMessage
{
  /// <summary>Сообщение пользователю.</summary>
  public string Message { get; set; }

  /// <summary>Ошибка (если при обработке объекта произошла ошибка).</summary>
  public Exception Exception { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="message">Сообщение</param>
  public BackgroundTaskMessage(string message) => this.Message = message;
}
