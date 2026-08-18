// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.Client.XmlExchangeClientCache
// Assembly: Intermech.XmlExchange.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 60313882-D426-47E0-8CD2-E15037D75FF2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.XmlExchange.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.XmlExchange.Client;

/// <summary>Класс для хранения констант и кэша на клиенте</summary>
public static class XmlExchangeClientCache
{
  /// <summary>Кэш ссылок на сервисы</summary>
  internal static class Services
  {
    /// <summary>Factory для навигатора</summary>
    public static IFactory Factory;
    /// <summary>Сервис для "фоновых" задач</summary>
    public static IBackgroundTaskView BackgroundTaskView;
    /// <summary>Коллекция именованных значков</summary>
    public static INamedImageList NamedImageList;
  }
}
