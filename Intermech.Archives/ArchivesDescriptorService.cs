// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchivesDescriptorService
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Возвращает дескриптор всех архивов, используется для показа списка архивов
/// </summary>
internal class ArchivesDescriptorService : IArchivesDescriptorService
{
  /// <summary>Возвращает дескриптор всех архивов</summary>
  /// <returns></returns>
  public IDescriptor GetDescriptor() => (IDescriptor) new HiveDescriptor();

  /// <summary>
  /// Это должно быть добавлено в IServiceProvider, если вместо закладки "Документы" нужно видеть закладку "Архивы"
  /// </summary>
  public object ViewArchives => (object) new Intermech.Archives.ViewArchives();
}
