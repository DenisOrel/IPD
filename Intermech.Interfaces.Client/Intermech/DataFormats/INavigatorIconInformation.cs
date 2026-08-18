// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.INavigatorIconInformation
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Интерфейс какой-то информации для определения значков узлам в "Навигаторе"
/// </summary>
public interface INavigatorIconInformation
{
  /// <summary>
  /// Какие-то данные, на основании которых может выполняться изменение стандартного значка
  /// для узла "Навигатора"
  /// </summary>
  object data { get; }
}
