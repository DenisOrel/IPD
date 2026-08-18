// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IIntegrator
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Drawing;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать интегратор с приложением. Все свойства и методы реализации этого интерфейса
/// должны быть thread-safe.
/// </summary>
public interface IIntegrator : IServiceProvider
{
  /// <summary>
  /// Возвращает глобальный идентификатор объекта интегратора в базе IPS.
  /// </summary>
  Guid Id { get; }

  /// <summary>Возвращает название интегратора.</summary>
  string DisplayName { get; }

  /// <summary>
  /// Возвращает шаблон для серверного объекта интегратора в форме xml-документа.
  /// Он используется при создании нового объекта интегратора в базе IPS.
  /// </summary>
  /// <returns>Шаблон для серверного объекта интегратора в форме xml-документа</returns>
  string GetServerObjectTemplate();

  /// <summary>
  /// Создает и возвращает визуальный редактор настроек интегратора.
  /// </summary>
  /// <returns>Элемент управления</returns>
  DataEditorControl CreateSettingsEditor();

  /// <summary>
  /// Возвращает изображение для иконки приложения, с которым осуществляется интеграция.
  /// Метод может вернуть null, если изображения запрошенного размера нет.
  /// </summary>
  /// <param name="imageSize">Размер изображения</param>
  /// <returns>Изображение иконки приложения или null</returns>
  Image GetApplicationImage(AppImageSize imageSize);

  /// <summary>
  /// Возвращает объект для обеспечения сервисами интегратора поточной безопасности (thread-safe).
  /// </summary>
  object SyncRoot { get; }

  /// <summary>Инициализирует интегратор перед использованием.</summary>
  void Initialize();
}
