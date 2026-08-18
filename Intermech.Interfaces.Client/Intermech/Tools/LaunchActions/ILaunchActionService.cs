// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.LaunchActions.ILaunchActionService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.LaunchActions;

/// <summary>
/// Клиентская служба, обслуживающая команды запуска приложений.
/// </summary>
public interface ILaunchActionService
{
  void Launch(LaunchParams launchParams);

  void Launch(LaunchParams launchParams, LaunchActionInfo actionInfo);

  void LaunchByShell(LaunchParams launchParams);

  ILaunchHandler GetHandler(Guid handlerId, bool throwIfNotFound);

  List<ILaunchHandler> GetHandlers();

  void RegisterHandler(ILaunchHandler handler);

  void UnregisterHandler(ILaunchHandler handler);
}
