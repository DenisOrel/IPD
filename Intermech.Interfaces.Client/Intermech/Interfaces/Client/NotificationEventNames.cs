// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NotificationEventNames
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Названия событий службы уведомлений</summary>
public static class NotificationEventNames
{
  /// <summary>
  /// Уведомление "AttributeCreated" - созданы новые атрибуты
  /// </summary>
  public const string AttributeCreated = "AttributeCreated";
  /// <summary>Уведомление "AttributeChanged" - атрибуты изменены</summary>
  public const string AttributeChanged = "AttributeChanged";
  /// <summary>Уведомление "AttributeRemoved" - атрибуты удалены</summary>
  public const string AttributeRemoved = "AttributeRemoved";
  /// <summary>
  /// Уведомление "Attribute4ObjTypeEvent" - добавлены, изменены или удалены атрибуты для типа объектов
  /// </summary>
  public const string Attribute4ObjTypeEvent = "Attribute4ObjTypeEvent";
  /// <summary>
  /// Уведомление "Attribute4RelTypeEvent" - добавлены, изменены или удалены атрибуты для типа связи
  /// </summary>
  public const string Attribute4RelTypeEvent = "Attribute4RelTypeEvent";
  /// <summary>
  /// Уведомление "ObjectTypesCreated" - типы объектов созданы
  /// </summary>
  public const string ObjectTypesCreated = "ObjectTypesCreated";
  /// <summary>
  /// Уведомление "ObjectTypesChanged" - свойства типов объектов изменены
  /// </summary>
  public const string ObjectTypesChanged = "ObjectTypesChanged";
  /// <summary>
  /// Уведомление "ObjectTypesRemoved" - типы объектов удалены
  /// </summary>
  public const string ObjectTypesRemoved = "ObjectTypesRemoved";
  /// <summary>Уведомление "ObjectsCreated" - объекты созданы</summary>
  public const string ObjectsCreated = "ObjectsCreated";
  /// <summary>Уведомление "ObjectsChanged" - объекты изменены</summary>
  public const string ObjectsChanged = "ObjectsChanged";
  /// <summary>
  /// Уведомление "ManagedObjectsCreated" - объекты созданы, управляемое событие
  /// </summary>
  public const string ManagedObjectsCreated = "ManagedObjectsCreated";
  /// <summary>Уведомление "ObjectsRemoves" - объекты удалены</summary>
  public const string ObjectsRemoved = "ObjectsRemoved";
  /// <summary>
  /// Уведомление "ObjectsCheckedIn" - объекты возвращены в архив
  /// </summary>
  public const string ObjectsCheckedIn = "ObjectsCheckedIn";
  /// <summary>
  /// Уведомление "ObjectsChangesCancelled" - отмена изменений в объектах
  /// </summary>
  public const string ObjectsChangesCancelled = "ObjectsChangesCancelled";
  /// <summary>
  /// Уведомление "ObjectsCheckedOut" - объекты взяты на изменение
  /// </summary>
  public const string ObjectsCheckedOut = "ObjectsCheckedOut";
  /// <summary>Уведомление "RelationsCreated" - связи созданы</summary>
  public const string RelationsCreated = "RelationsCreated";
  /// <summary>Уведомление "RelationsChanged" - связи изменены</summary>
  public const string RelationsChanged = "RelationsChanged";
  /// <summary>
  /// Уведомление "ManagedRelationsCreated" - связи созданы, управляемое событие
  /// </summary>
  public const string ManagedRelationsCreated = "ManagedRelationsCreated";
  /// <summary>Уведомление "RelationsRemoves" - связи удалены</summary>
  public const string RelationsRemoved = "RelationsRemoved";
  /// <summary>
  /// Уведомление "ManagedRelationsInsert" - связи созданы,
  /// требуется добавить их в определённую позицию в дереве,
  /// управляемое событие
  /// </summary>
  public const string ManagedRelationsInsert = "ManagedRelationsInsert";
  /// <summary>
  /// /// Уведомление "SubstitutesChanged" - изменены связи, участвующие в допустимых заменителях
  /// </summary>
  public const string SubstitutesChanged = "SubstitutesChanged";
  /// <summary>
  /// <para>Уведомление "SortedRelationsChanged" - изменены связи, имеющие атрибут "Сортировка"</para>
  /// <para>Для корректной обработки требуется наличие всех ид. версии родительских объектов для измененных связей
  /// в списке RelationID рассылаемого события</para>
  /// </summary>
  public const string SortedRelationsChanged = "SortedRelationsChanged";
  /// <summary>
  /// Уведомление "FiltrationChanged" - изменены настройки фильтрации состава по правилам подбора версий
  /// </summary>
  public const string FiltrationChanged = "FiltrationChanged";
  /// <summary>
  /// Уведомление "ObjectsFiltrationChanged" - изменены настройки фильтрации списков объектов
  /// </summary>
  public const string ObjectsFiltrationChanged = "ObjectsFiltrationChanged";
  /// <summary>
  /// Уведомление "ObjectTypeAndRelationFiltrationChanged" - изменены настройки фильтрации типов объектов и связей
  /// </summary>
  public const string ObjectTypeAndRelationFiltrationChanged = "ObjectTypeAndRelationFiltrationChanged";
  /// <summary>
  /// Уведомление "ApplicationClosing" - приложение завершает свою работу
  /// </summary>
  public const string ApplicationClosing = "ApplicationClosing";
  /// <summary>
  /// Уведомление "ApplicationClosed" - приложение будет закрыто
  /// </summary>
  /// <remarks>Отличается от ApplicationClosing тем что закрытие окончательное и не может быть отменето</remarks>
  public const string ApplicationClosed = "ApplicationClosed";
  /// <summary>
  /// Уведомление "RecentObjectsChanged" - изменился список недавних объектов
  /// </summary>
  public const string RecentObjectsChanged = "RecentObjectsChanged";
  public const string RecentObjectsCleared = "RecentObjectsCleared";
  /// <summary>
  /// Уведомление "InternalRecentObjectsChanged" - изменился список недавних объектов (!НЕ ИСПОЛЬЗОВАТЬ!)
  /// </summary>
  public const string InternalRecentObjectsChanged = "InternalRecentObjectsChanged";
  /// <summary>
  /// Уведомление "DragDrop" - работа с Drag'N'Drop (работа выполняется с помощью буфера обмена)
  /// </summary>
  public const string DragDrop = "DragDrop";
  /// <summary>Уведомление об изменении состава Избранного.</summary>
  public const string FavoritesChanged = "FavoritesChanged";
  /// <summary>
  /// Уведомление об удалении типа из Избранного
  /// (выделено в отдельное уведомление, т.к. нужно обработать изменение узла дерева навигатора в этом случае отдельно,
  /// т.к. типы у нас - папки и работа с ними идет через анализаторы)
  /// </summary>
  public const string FavoritesRemoveType = "FavoritesRemoveType";
  /// <summary>
  /// Уведомление "ProjectChanged" - изменился текущий проект
  /// </summary>
  public const string ProjectChanged = "ProjectChanged";
  /// <summary>
  /// Уведомление "EditingContextChanged" - изменился текущий контекст редактирования
  /// </summary>
  public const string EditingContextChanged = "EditingContextChanged";
  /// <summary>
  /// Уведомление "ApplicabilityAdded" - создана связи между типами объектов
  /// (добавлен тип связи для объекта)
  /// </summary>
  public const string ApplicabilityAdded = "ApplicabilityAdded";
  /// <summary>
  /// Уведомление "ApplicabilityRemoved" - удалена связь между типами объектов
  /// (удален тип связи для объекта)
  /// </summary>
  public const string ApplicabilityRemoved = "ApplicabilityRemoved";
  /// <summary>
  /// Уведомление "ApplicabilityChanged" - изменение связи между типами объектов
  /// (изменены атрибуты типа связи для объекта)
  /// </summary>
  public const string ApplicabilityChanged = "ApplicabilityChanged";
  /// <summary>
  /// Уведомление "PublishObjectsRemoved" - Опубликованные объекты удалены
  /// </summary>
  public const string PublishObjectsRemoved = "PublishObjectsRemoved";
  /// <summary>
  /// Уведомление "OwnComplete" - Завершение владением опубликованными объектами
  /// </summary>
  public const string OwnComplete = "OwnComplete";
  /// <summary>Уведомление "FilesRenamed" - Файлы переименованы</summary>
  public const string FilesRenamed = "FilesRenamed";
  /// <summary>
  /// Уведомление "SnapshotsChanged" - Изменение в наборе итераций для объекта
  /// </summary>
  public const string SnapshotsChanged = "SnapshotsChanged";
  /// <summary>
  /// Уведомление "EmailAccauntChanged" - почтовый аккаунт изменен
  /// </summary>
  public const string EmailAccauntChanged = "EmailAccauntChanged";
  /// <summary>
  /// Уведомление "Внесены изменения в Redlining". Служба IClientRedliningService
  /// должна выполнить синхронизацию файлов "Red Line" с соответствующими
  /// объектами IPS
  /// </summary>
  public const string RedliningChanged = "RedliningChanged";
  /// <summary>
  /// Уведомление для контролов, которые реализуют интерфейс Intermech.Navigator.Controls.IToSelectItemsHost
  /// </summary>
  public const string ToSelectItemsChanges = "ToSelectItemsChanges";
  /// <summary>
  /// Уведомление о действии, произошедшем с одной или несколькими связями из стандартных команд "Навигатора"
  /// </summary>
  public const string NavigatorRelationCommand = "NavigatorRelationCommand";
  /// <summary>
  /// Уведомление об актуализации извещений - позволяет предупредить пользователя,
  /// если извещения были не актуализированы, а переведены на шаг "Ожидание срока изменения"
  /// </summary>
  public const string RevisionsActualized = "RevisionsActualized";
  public const string NavigatorWindowOpening = "NavigatorWindowOpening";
  public const string NavigatorWindowOpened = "NavigatorWindowOpened";
  public const string NavigatorWindowActivated = "NavigatorWindowActivated";
  public const string ConfigurationOptionChanged = "ConfigurationOptionChanged";
  /// <summary>
  /// Уведомление "FileReplaced" - заменён файл объекта или связи в файловом атрибуте
  /// </summary>
  public const string FileReplaced = "FileReplaced";
  /// <summary>
  /// Список названий событий, которые должны рассылаться в "Навигаторе" всем окнам
  /// независимо от настройки "Обновлять фоновые окна"
  /// </summary>
  public static List<string> CriticalEventNames = new List<string>();

  /// <summary>Статический конструктор</summary>
  static NotificationEventNames()
  {
    NotificationEventNames.CriticalEventNames.Add(nameof (ApplicationClosing));
    NotificationEventNames.CriticalEventNames.Add(nameof (AttributeRemoved));
    NotificationEventNames.CriticalEventNames.Add(nameof (ObjectsRemoved));
    NotificationEventNames.CriticalEventNames.Add(nameof (ObjectTypesRemoved));
    NotificationEventNames.CriticalEventNames.Add(nameof (RelationsRemoved));
    NotificationEventNames.CriticalEventNames.Add(nameof (ObjectsCheckedIn));
    NotificationEventNames.CriticalEventNames.Add(nameof (ObjectsCheckedOut));
    NotificationEventNames.CriticalEventNames.Add(nameof (ObjectsChangesCancelled));
    NotificationEventNames.CriticalEventNames.Add(nameof (RecentObjectsChanged));
    NotificationEventNames.CriticalEventNames.Add(nameof (ProjectChanged));
    NotificationEventNames.CriticalEventNames.Add(nameof (RecentObjectsChanged));
  }
}
