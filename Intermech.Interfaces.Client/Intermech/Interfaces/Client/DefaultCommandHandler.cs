// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DefaultCommandHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Перечислитель указывает, чья команда используется в IDefaultCommand - контекстного меню или ICommandManager
/// </summary>
[Serializable]
public enum DefaultCommandHandler
{
  /// <summary>Команда обрабатывается ICommandManager</summary>
  ICommandManager,
  /// <summary>Команда обрабатывается контекстным меню</summary>
  ContectMenu,
}
