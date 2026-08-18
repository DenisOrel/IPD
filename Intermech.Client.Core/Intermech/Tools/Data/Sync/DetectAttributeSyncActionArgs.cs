
// Type: Intermech.Tools.Data.Sync.DetectAttributeSyncActionArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Аргументы события по выбору направления и способа переноса атрибута из одной системы в другую.
/// </summary>
public class DetectAttributeSyncActionArgs : AttributeSyncTaskArgs
{
  private readonly AttributeSyncUnit attribute;
  private SyncDirection direction;
  private AttributeSyncAction action;

  /// <summary>Создает объект.</summary>
  /// <param name="taskData">Параметры переноса атрибутов из одной системы в другую</param>
  /// <param name="attribute">Описатель анализируемого атрибута</param>
  public DetectAttributeSyncActionArgs(AttributeSyncTaskData taskData, AttributeSyncUnit attribute)
    : base(taskData)
  {
    this.attribute = attribute != null ? attribute : throw new ArgumentNullException(nameof (attribute));
    this.direction = SyncDirection.Forward;
  }

  /// <summary>Возвращает описатель анализируемого атрибута</summary>
  public AttributeSyncUnit Attribute => this.attribute;

  /// <summary>
  /// Возвращает или задает направление переноса значения атрибута
  /// </summary>
  public SyncDirection Direction
  {
    get => this.direction;
    set => this.direction = value;
  }

  /// <summary>
  /// Возвращает или задает способ переноса значения атрибута
  /// </summary>
  public AttributeSyncAction Action
  {
    get => this.action;
    set => this.action = value;
  }
}
