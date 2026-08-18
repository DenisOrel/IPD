// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ObjectsSelectionOptionsHolder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Контейнер опций, применяемых при работе с коллекциями объектов
/// </summary>
[Serializable]
public sealed class ObjectsSelectionOptionsHolder
{
  /// <summary>Опции, применяемые при работе с коллекциями объектов</summary>
  private ObjectsSelectionOptions _options;

  /// <summary>Опции, применяемые при работе с коллекциями объектов</summary>
  public ObjectsSelectionOptions Options
  {
    [DebuggerStepThrough] get => this._options;
  }

  /// <summary>Создать контейнер опций</summary>
  /// <param name="options">Опции, применяемые при работе с коллекциями объектов</param>
  public ObjectsSelectionOptionsHolder(ObjectsSelectionOptions options) => this._options = options;
}
