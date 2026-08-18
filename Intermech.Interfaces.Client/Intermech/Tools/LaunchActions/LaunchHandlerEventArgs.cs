// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.LaunchActions.LaunchHandlerEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Tools.LaunchActions;

/// <summary>
/// Аргументы событий для обработчиков команд запуска приложений.
/// </summary>
public class LaunchHandlerEventArgs : EventArgs
{
  private readonly LaunchParams launchParams;

  /// <summary>Создает объект.</summary>
  /// <param name="launchParams">Описатель параметров запуска приложения</param>
  public LaunchHandlerEventArgs(LaunchParams launchParams)
  {
    this.launchParams = launchParams != null ? launchParams : throw new ArgumentNullException(nameof (launchParams));
  }

  /// <summary>Возвращает параметры запуска приложения.</summary>
  public LaunchParams LaunchParams => this.launchParams;
}
