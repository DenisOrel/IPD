
// Type: Intermech.Navigator.Holder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator;

/// <summary>
/// Обеспечивает взаимодействие навигатора с универсальным клиентом
/// и всевозможными сервисами.
/// </summary>
internal sealed class Holder
{
  /// <summary>
  /// Сервис для хранения икон, привязанных к категориям и(или) типам.
  /// Иконы хранятся в двух параллельных ImageList размерами 16 и 32.
  /// Индексы в этих списках для одной иконы одинаковые.
  /// </summary>
  public static ICategoryTypeIconService IconService;
  /// <summary>
  /// Сервис для хранения изображений элементов навигации, привязанных к
  /// категориям, типам и состояниям элементов.
  /// </summary>
  public static ICategoryTypeStateImageService ImageService;
  /// <summary>Служба по управлению панелями управления и меню</summary>
  public static BarManager BarManager;
  /// <summary>Служба по управлению док-контролами</summary>
  public static DockManager DockManager;
  /// <summary>
  /// Интерфейс для связи INavigate скнопками управления на тоолбаре
  /// </summary>
  public static INavigateManager HistoryManager;
  /// <summary>Менеджер по управлению командами от кнопок и меню</summary>
  public static ICommandManager CommandManager;
  /// <summary>
  /// Класс для регистрации делегатов создания контентов системы докинга.
  /// Каждый дополнительный модуль, которому требуется, чтобы его окно докалось
  /// при последующей загрузке, запрашивает этот интерфейс и регистрирует делегат.
  /// </summary>
  public static IContentProvider ContentProvider;
  /// <summary>Сервис для работы с именоваными иконками</summary>
  public static INamedImageList NamedImageList;
  /// <summary>Сервис для работы с большими 48x48 картинками</summary>
  public static IBigImageList BigImageList;
  /// <summary>Служба по управлению конфигурациями</summary>
  public static IConfigurationManager ConfigurationManager;
  /// <summary>/// Интерфейс, управляющий коллекцией схем колонок</summary>
  public static IColumnSchemes ColumnSchemes;
  /// <summary>
  /// Интерфейс для отображения Guid в положительные числовые идентификаторы,
  /// уникальные для текущего сеанса работы программы. Отображение предназначено
  /// для ускорения работы программы и уменьшения объема используемой оперативной
  /// памяти.
  /// </summary>
  public static IGuidMapper GuidMapper;
  /// <summary>
  /// Интерфейс для отображения строк в положительные числовые идентификаторы,
  /// уникальные для текущего сеанса работы программы. Отображение предназначено
  /// для ускорения работы программы и уменьшения объема используемой оперативной
  /// памяти.
  /// </summary>
  public static IStringMapper StringMapper;
  /// <summary>
  /// Интерфейс сервиса для регистрации расширений навигатора, а также для
  /// создания зарегистрированных объектов-расширений.
  /// </summary>
  public static IFactory Factory;
  /// <summary>???</summary>
  public static IWellKnownNavigators WellKnownNavigators;
  /// <summary>Интерфейс службы уведомлений</summary>
  public static INotificationService NotificationService;
  /// <summary>
  /// Сервис, позволяющий клиентским плагинам передавать какую-о информацию на сторону сервера
  /// </summary>
  public static IClientPluginsService ClientPluginsService;
  /// <summary>
  /// Клиентская служба, которая позволяет считывать статусы для элементов
  /// </summary>
  public static IElementStatusesClientService ElementStatusesClientService;
  /// <summary>
  /// Интерфейс коллекции команд по умолчанию для указанных типов объектов
  /// </summary>
  public static IDefaultCommands4ObjTypes DefaultCommands4ObjTypes;
  /// <summary>Интерфейс кэша графических элементов для "Навигатора"</summary>
  public static INavGraphicsCache NavGraphicsCache;
  /// <summary>
  /// Интерфейс службы, управляющей множественным выбором в деревьях окон "Навигатора"
  /// </summary>
  public static IEnableTreeMultiSelectService EnableTreeMultiSelectService;
  /// <summary>
  /// Интерфейс службы, управляющей сортировкой в колонках в деревьях окон "Навигатора"
  /// </summary>
  public static IEnableTreeColumnsSortingService EnableTreeColumnsSortingService;
  /// <summary>
  /// Интерфейс службы, управляющей сворачиванием дерева "Навигатора" в открываемых окнах
  /// </summary>
  public static INavigatorTreeCollapseService NavigatorTreeCollapseService;
  /// <summary>
  /// Сервис "Навигатора", позволяющий брать на изменение группы объектов
  /// </summary>
  public static IObjectsCheckOutService ObjectsCheckOutService;
}
