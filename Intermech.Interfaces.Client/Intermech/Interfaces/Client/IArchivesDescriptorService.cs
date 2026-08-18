// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IArchivesDescriptorService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Возвращает дескриптор всех архивов, используется для показа списка архивов
/// </summary>
public interface IArchivesDescriptorService
{
  /// <summary>Возвращает дескриптор всех архивов</summary>
  /// <returns></returns>
  IDescriptor GetDescriptor();

  /// <summary>
  /// Это должно быть добавлено в IServiceProvider, если вместо закладки "Документы" нужно видеть закладку "Архивы"
  /// </summary>
  object ViewArchives { get; }
}
