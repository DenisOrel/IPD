// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.LaunchActions.IDynamicLaunchHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.LaunchActions;

/// <summary>
/// Дополнительный интерфейс для обработчиков, открывающих файлы объектов в сторонних приложениях.
/// Этот интерфейс позволяет службе команд запуска динамически подключить обработчик, даже если
/// соответствующая обработчику команда запуска не была настроена с помощью диалога настройки команд запуска.
/// </summary>
/// <remarks>
/// Реализация интерфейса должна учитывать, что у динамически подключаемого обработчика нет конфигурации,
/// т.е. соответствующий параметр в методе <see cref="M:Intermech.Tools.LaunchActions.ILaunchHandler.Launch(Intermech.Tools.LaunchActions.LaunchParams,System.Xml.XmlDocument)" />
/// будет содержать пустой объект типа <see cref="T:System.Xml.XmlDocument" />.
/// </remarks>
public interface IDynamicLaunchHandler
{
  /// <summary>Выполняет поиск обработчика.</summary>
  /// <param name="dynamicLaunchInfo">Параметры поиска динамически подключаемого обработчика</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="dynamicLaunchInfo" /> содержит null</exception>
  void Lookup(IDynamicLaunchInfo dynamicLaunchInfo);
}
