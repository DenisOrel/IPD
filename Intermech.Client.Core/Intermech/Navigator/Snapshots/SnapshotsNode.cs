
// Type: Intermech.Navigator.Snapshots.SnapshotsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Snapshots;

public class SnapshotsNode : CompositeNode, IContextAware
{
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _services;
  /// <summary>
  /// Идентификатор версии объекта, итерации которого хотим увидеть
  /// </summary>
  private long objectID;
  /// <summary>
  /// Идентификатор  объекта, итерации которого хотим увидеть
  /// </summary>
  private long id;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="id">Идентификатор объекта, итерации которого хотим увидеть</param>
  /// <param name="objectID">Идентификатор версии объекта, итерации которого хотим увидеть</param>
  public SnapshotsNode(long id, long objectID)
  {
    this.options |= NodeOptions.CanContainsObjectsList;
    this.id = id;
    this.objectID = objectID;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    get => this._services;
    set => this._services = value;
  }

  /// <summary>
  /// Создает и возвращает части, которые отвечают за элементы-не-папки.
  /// </summary>
  /// <returns>Коллекция частей</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new SnapshotsNodePart(this.id, this.objectID, this._services));
  }

  /// <summary>
  /// Возвращает коллекцию колонок, которые должны отображаться в гриде
  /// для данного элемента. Используется только в том случае, если для
  /// данного элемента нет сохраненных в конфиграции пользователя
  /// настроек отображения грида.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип содержимого грида</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    return base.GetDefaultColumns(content);
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип содержимого грида</param>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    return SnapshotConsts.SnapshotGridColumns();
  }
}
