// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ServiceHolder
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Bars;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Archives;

/// <summary>Хранилище сервисов</summary>
public class ServiceHolder
{
  /// <summary>Хранилище для навигатора</summary>
  public static IFactory Factory;
  /// <summary>Хранилище Guid'ов</summary>
  public static IGuidMapper GuidMapper;
  /// <summary>Хранилище иконок привязанных к категориям и типам</summary>
  public static ICategoryTypeIconService CategoryTypeIconService;
  /// <summary>
  /// Хранилище главного BarManager (для прорисовки менюшек в одинаковом стиле)
  /// </summary>
  public static BarManager BarManager;
  /// <summary>для работы с ресурсами</summary>
  public static ResourceManager rm = new ResourceManager("Intermech.Archives.ArchivesResources", Assembly.GetExecutingAssembly());
}
