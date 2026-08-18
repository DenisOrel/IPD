// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.LaunchActions.ILaunchHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Tools.LaunchActions;

/// <summary>Сервис для запуска инструмента</summary>
public interface ILaunchHandler
{
  /// <summary>Уникальный идентификатор</summary>
  Guid Id { get; }

  /// <summary>
  /// Возвращает имя обработчика запуска. Если обработчик является непараметризованным и обслуживает конкретное приложение, то его имя совпадает с именем приложения.
  /// Иначе, имя приложения содержится в серверном объекте.
  /// </summary>
  string DisplayName { get; }

  /// <summary>
  /// Возвращает шаблон для серверного объекта, описывающего запускаемое приложение, в форме xml-документа.
  /// Он используется при создании нового объекта интегратора в базе IPS.
  /// </summary>
  string GetServerObjectTemplate();

  /// <summary>Создать редактор настроек</summary>
  /// <returns>Контрол с редактором настроек</returns>
  DataEditorControl CreateSettingsEditor();

  /// <summary>Стартовать приложение службы инструментов</summary>
  /// <param name="launchParams">Описатель параметров запуска приложения</param>
  /// <param name="handlerData">Конфигурация для запускаемого приложения</param>
  void Launch(LaunchParams launchParams, XmlDocument handlerData);

  /// <summary>
  /// Метод вызывается перед взятием на изменение объекта и проверками на возможность редактирования
  /// </summary>
  /// <param name="launchParams">Описатель параметров запуска приложения</param>
  /// <param name="handlerData">Конфигурация для запускаемого приложения</param>
  void BeforeLaunch(LaunchParams launchParams, XmlDocument handlerData);
}
