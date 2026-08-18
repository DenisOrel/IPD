// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IControlServiceContainer
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Diagnostics;
using System.ComponentModel.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Контейнер сервисов, привязанный к контролу и связанной с ней иерархией вложенности контролов
///   родительским провайдером для данного контейнера сервисов всегда выступает ближайший по иерархии вверх владелец связанного с интерфейсом контрола, поддерживающий IContextAware или IServiceProvider
/// наследуется от IAdvancedServiceContainer, то есть может иметь дополнительный список сервисов, связанным с логическим контекстом
/// </summary>
public interface IControlServiceContainer : 
  IAdvancedServiceContainer,
  IServiceContainer,
  System.IServiceProvider
{
  /// <summary>Ассоциированный с контейнером сервисов контрол</summary>
  [NotNull]
  Control Control { get; }

  /// <summary>Родительский сервис контейнеров</summary>
  [CanBeNull]
  System.IServiceProvider ParentControlServices { get; }
}
