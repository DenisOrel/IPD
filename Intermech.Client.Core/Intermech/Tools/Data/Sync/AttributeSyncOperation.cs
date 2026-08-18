
// Type: Intermech.Tools.Data.Sync.AttributeSyncOperation
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Описывает операцию по переносу атрибута из одной системы в другую.
/// </summary>
internal sealed class AttributeSyncOperation
{
  private readonly AttributeSyncUnit attribute;
  private readonly AttributeSyncAction action;

  /// <summary>Создает объект.</summary>
  /// <param name="attribute">Переносимый атрибут</param>
  /// <param name="action">Назначенное для него действие</param>
  internal AttributeSyncOperation(AttributeSyncUnit attribute, AttributeSyncAction action)
  {
    this.attribute = attribute;
    this.action = action;
  }

  /// <summary>Возвращает описатель переносимого атрибута.</summary>
  public AttributeSyncUnit Attribute => this.attribute;

  /// <summary>Возвращает действие, назначенное для атрибута.</summary>
  public AttributeSyncAction Action => this.action;
}
