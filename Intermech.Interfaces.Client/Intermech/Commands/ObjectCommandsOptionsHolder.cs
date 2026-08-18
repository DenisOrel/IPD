// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.ObjectCommandsOptionsHolder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Commands;

/// <summary>
/// Контейнер опций для команд, выполняемых над информационными объектами IPS
/// </summary>
[Serializable]
public sealed class ObjectCommandsOptionsHolder : ServiceProviderValueHolder<ObjectCommandsOptions>
{
  /// <summary>Создать пустой контейнер опций</summary>
  public ObjectCommandsOptionsHolder()
    : base(ObjectCommandsOptions.None)
  {
  }

  /// <summary>Создать заполненный контейнер опций</summary>
  /// <param name="options">Опции для команд, выполняемых над информационными объектами IPS</param>
  public ObjectCommandsOptionsHolder(ObjectCommandsOptions options)
    : base(options)
  {
  }

  /// <summary>
  /// Опции для команд, выполняемых над информационными объектами IPS
  /// </summary>
  [Obsolete("Use the property ObjectCommandsOptionsHolder.Value instead of this", true)]
  public ObjectCommandsOptions Options
  {
    get => this.Value;
    set => this.Value = value;
  }
}
