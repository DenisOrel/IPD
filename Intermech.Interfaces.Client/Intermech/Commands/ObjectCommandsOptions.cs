// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.ObjectCommandsOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Commands;

/// <summary>
/// Опции для команд, выполняемых над информационными объектами IPS
/// </summary>
[Flags]
[Serializable]
public enum ObjectCommandsOptions : long
{
  /// <summary>Никаких опций не задано</summary>
  None = 0,
  /// <summary>
  /// Не удалять рабочие копии объектов при выполнении команды "Завершить редактирование".
  /// Если флажок задан, у объектов на сервере вызывается IDBObject.SaveToArcCopy() вместо Checkin()
  /// </summary>
  PreserveWorkingCopies = 1,
  /// <summary>
  /// Не запрашивать у пользователя потверждения выполнения комманды
  /// </summary>
  /// <remarks>При условии что команда поддерживает данный режим</remarks>
  NoConfirmation = 4,
  /// <summary>
  /// Выполнить команду без отображения диалогов и индикаторов хода выполнения
  /// </summary>
  /// <remarks>При условии что команда поддерживает данный режим</remarks>
  NonInteractive = 8,
}
