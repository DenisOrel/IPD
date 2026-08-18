// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IStateStreams
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.IO;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Сервис, предоставляемый навигатором, позволяющий его составным частям (закладкам и др.)
/// сохранять свое состояние, а также восстанавливать его из потока.
/// </summary>
public interface IStateStreams
{
  /// <summary>
  /// Создает и возвращает новый пустой поток, который можно использовать для сохранения
  /// состояния. Если поток с таким именем уже существует, то метод вернет null.
  /// </summary>
  /// <param name="name">Имя потока.</param>
  /// <returns>Поток состояния.</returns>
  Stream Create(string name);

  /// <summary>
  /// Возвращает существующий поток, который можно использовать для восстановления
  /// состояния. Если поток с таким именем не существует, то метод вернет null.
  /// </summary>
  /// <param name="name">Имя потока.</param>
  /// <returns>Поток состояния.</returns>
  Stream this[string name] { get; }

  /// <summary>
  /// Удаляет существующий поток состояния с указанным именем.
  /// </summary>
  /// <param name="name">Имя потока.</param>
  void Remove(string name);
}
