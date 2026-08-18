// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IFactory
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс сервиса для регистрации расширений навигатора, а также для
/// создания зарегистрированных объектов-расширений.
/// </summary>
public interface IFactory : INodesFactory
{
  /// <summary>
  /// Регистрирует базовый тип элемента из пространства навигации для
  /// указанной категории.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="nodeType">Тип элемента навигации</param>
  void AddNodeType(int categoryID, Type nodeType);

  /// <summary>
  /// Регистрирует базовый тип элемента из пространства навигации для
  /// указанной категории.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="nodeType">Тип элемента навигации</param>
  /// <param name="inheritance">Интерфейс получения идентификаторов родительских типов</param>
  void AddNodeType(int categoryID, Type nodeType, ICategoryInheritance inheritance);

  /// <summary>
  /// Регистрирует специализированный тип элемента из пространства навигации
  /// для указанной категории
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="typeID">Идентификатор типа</param>
  /// <param name="nodeType">Тип элемента навигации</param>
  void AddNodeType(int categoryID, int typeID, Type nodeType);

  /// <summary>
  /// Регистрирует провайдер закладок, который будет использоваться для
  /// элементов навигации любой категории и типа.
  /// </summary>
  /// <param name="provider">Провайдер закладок</param>
  void AddViewsProvider(IViewsProvider provider);

  /// <summary>
  /// Регистрирует провайдер закладок, которых будет использоватсья для
  /// элементов навигации любого типа из указанной категории.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="provider">Провайдер закладок</param>
  void AddViewsProvider(int categoryID, IViewsProvider provider);

  /// <summary>
  /// Регистрирует провайдер закладок, который будет использоваться для
  /// элементов навигации указанной категории и типа.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="typeID">Идентификатор типа</param>
  /// <param name="provider">Провайдер закладок</param>
  void AddViewsProvider(int categoryID, int typeID, IViewsProvider provider);

  /// <summary>
  /// Регистрирует провайдер команд контекстного меню, который будет
  /// использоваться для элементов навигации любой категории и типа.
  /// </summary>
  /// <param name="provider">Провайдер команд</param>
  void AddCommandsProvider(ICommandsProvider provider);

  /// <summary>
  /// Регистрирует провайдер команд контекстного меню, который будет
  /// использоваться для элементов навигации любого типа из указанной
  /// категории.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="provider">Провайдер команд</param>
  void AddCommandsProvider(int categoryID, ICommandsProvider provider);

  /// <summary>
  /// Регистрирует провайдер команд контекстного меню, который будет
  /// использоваться для элементов навигации указанной категории и типа.
  /// категории.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="typeID">Идентификатор типа</param>
  /// <param name="provider">Провайдер команд</param>
  void AddCommandsProvider(int categoryID, int typeID, ICommandsProvider provider);

  /// <summary>
  /// Удаляет провайдер команд контекстного меню, который
  /// использовался для элементов навигации любой категории и типа.
  /// </summary>
  /// <param name="provider">Провайдер команд</param>
  void RemoveCommandsProvider(ICommandsProvider provider);

  /// <summary>
  /// Удаляет провайдер команд контекстного меню, который
  /// использовался для элементов навигации любого типа из указанной
  /// категории.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="provider">Провайдер команд</param>
  void RemoveCommandsProvider(int categoryID, ICommandsProvider provider);

  /// <summary>
  /// Удаляет провайдер команд контекстного меню, который
  /// использовался для элементов навигации указанной категории и типа.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="typeID">Идентификатор типа</param>
  /// <param name="provider">Провайдер команд</param>
  void RemoveCommandsProvider(int categoryID, int typeID, ICommandsProvider provider);

  /// <summary>
  /// Регистрирует элемент из пространства навигации, которых должен быть
  /// включен в корень основной иерархии навигатора "Информационное
  /// пространство".
  /// </summary>
  /// <param name="descriptorGuid">Глобальный идентификатор дескриптора</param>
  /// <param name="descriptor">Дескриптор, описывающий элемент</param>
  /// <param name="orderID">Положение дескриптора в списке дескрипторов.</param>
  void AddGlobalNode(Guid descriptorGuid, IDescriptor descriptor, int orderID);

  /// <summary>
  /// Возвращает массив провайдеров закладок для элемента навигации указанной
  /// категории и типа. Если ни одного зарегистрированного провайдера
  /// найти не удалось, то метод возвращает null.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории элемента</param>
  /// <param name="typeID">Идентификатор типа элемента</param>
  /// <returns>Массив провайдеров</returns>
  IViewsProvider[] GetViewsProviders(int categoryID, int typeID);

  /// <summary>
  /// Возвращает массив провайдеров закладок для элемента навигации указанной
  /// категории. Если ни одного зарегистрированного провайдера
  /// найти не удалось, то метод возвращает null.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории элемента</param>
  /// <returns>Массив провайдеров</returns>
  IViewsProvider[] GetViewsProviders(int categoryID);

  /// <summary>
  /// Возвращает массив провайдеров закладок. Если ни одного зарегистрированного провайдера
  /// найти не удалось, то метод возвращает null.
  /// </summary>
  /// <returns>Массив провайдеров</returns>
  IViewsProvider[] GetViewsProviders();

  /// <summary>
  /// Возвращает массив провайдеров команд контекстного меню для элемента навигации указанной
  /// категории и типа. Если ни одного зарегистрированного провайдера
  /// найти не удалось, то метод возвращает null.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории элемента</param>
  /// <param name="typeID">Идентификатор типа элемента</param>
  /// <returns>Массив провайдеров</returns>
  ICommandsProvider[] GetCommandsProviders(int categoryID, int typeID);

  /// <summary>
  /// Возвращает массив провайдеров команд контекстного меню для элемента навигации указанной
  /// категории. Если ни одного зарегистрированного провайдера
  /// найти не удалось, то метод возвращает null.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории элемента</param>
  /// <returns>Массив провайдеров</returns>
  ICommandsProvider[] GetCommandsProviders(int categoryID);

  /// <summary>
  /// Возвращает массив провайдеров команд контекстного меню. Если ни одного зарегистрированного провайдера
  /// найти не удалось, то метод возвращает null.
  /// </summary>
  /// <returns>Массив провайдеров</returns>
  ICommandsProvider[] GetCommandsProviders();

  /// <summary>Текущий шаблон контекстного меню</summary>
  MenuTemplate ContextMenuTemplate { get; }

  /// <summary>Шаблон контекстного меню по умолчанию</summary>
  [Obsolete("Use ContextMenuTemplate", false)]
  MenuTemplate ContextMenuTemplateDefault { get; }

  MenuTemplate ConfiguredContextMenuTemplate { get; set; }

  /// <summary>
  /// Событие генерируется перед каждым построением контекстных меню. Позволяет
  /// выполнять изменение элементов шаблона контекстного меню перед тем, как на
  /// их основе будет сформировано контекстное меню.
  /// </summary>
  event MenuTemplateNodeTransformEventHandler OnMenuTemplateNodeTransformEventHandler;

  /// <summary>
  /// Выполнить преобразование элемента шаблона контекстного меню, если есть обработчик
  /// </summary>
  /// <param name="node">Преобразуемый элемент шаблона контекстного меню</param>
  /// <param name="items">Коллекция выделенных элементов, на основе которых строится команда контекстного меню</param>
  /// <param name="services">Контейнер сервисов</param>
  void MenuTemplateNodeTransform(
    MenuTemplateNode node,
    ISelectedItems items,
    IServiceProvider services);
}
