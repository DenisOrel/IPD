// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Persistence.IStateFormatter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.IO;

#nullable disable
namespace Intermech.Navigator.Persistence;

/// <summary>
/// Позволяет реализовать форматтер объектов, позволяющий выполнять
/// эффективную сериализацию простых объектов.
/// </summary>
public interface IStateFormatter
{
  void Serialize(Stream stream, PersistentState state);

  PersistentState Deserialize(Stream stream);
}
