// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Persistence.IPersistable
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Persistence;

/// <summary>
/// Интерфейс, который должны реализовывать сериализуемые объекты.
/// </summary>
public interface IPersistable
{
  /// <summary>Формирует сериализованное представление объекта.</summary>
  /// <param name="state">Контейнер значений для хранения сериализованного представления объекта</param>
  void GetObjectData(PersistentState state);
}
