// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.SetDescriptorStatusesEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Аргументы для события "Установить/изменить значения в статусах дескриптора корневого элемента пространства навигации"
/// </summary>
public sealed class SetDescriptorStatusesEventArgs : EventArgs
{
  /// <summary>
  /// Интерфейс, позволяющий управлять статусами дескриптора корневого элемента пространства навигации
  /// </summary>
  private IDescriptorElementStatuses _descriptor;

  /// <summary>
  /// Интерфейс, позволяющий управлять статусами дескриптора корневого элемента пространства навигации
  /// </summary>
  public IDescriptorElementStatuses RootDescriptor
  {
    [DebuggerStepThrough] get => this._descriptor;
  }

  /// <summary>
  /// Создать аргументы события "Установить/изменить значения в статусах дескриптора корневого элемента пространства навигации"
  /// </summary>
  /// <param name="descriptor">Интерфейс, позволяющий управлять статусами дескриптора корневого элемента пространства навигации</param>
  public SetDescriptorStatusesEventArgs(IDescriptorElementStatuses descriptor)
  {
    this._descriptor = descriptor;
  }
}
