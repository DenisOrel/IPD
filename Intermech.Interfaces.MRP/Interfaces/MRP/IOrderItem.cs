// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IOrderItem
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Интерфейс, описывающий владельца настройки</summary>
public interface IOrderItem
{
  /// <summary>Объект для синхронизации</summary>
  object SyncRoot { get; }

  /// <summary>
  /// Идентификатор версии объекта состава, к которому привязан данный экземпляр
  /// </summary>
  long F_PROJ_ID { get; }

  /// <summary>
  /// Идентификатор связи, к которой привязан данный экземпляр
  /// </summary>
  long F_PRJLINK_ID { get; }

  /// <summary>Идентификатор типа связи</summary>
  int F_RELATION_TYPE { get; }

  /// <summary>
  /// Список настроек, связанных с указанным объектом состава
  /// </summary>
  List<IOrderItemSetting> Settings { get; }

  /// <summary>
  /// Отыскать в списке Settings настройку указанного типа, реализующего интерфейс IOrderItemSetting
  /// </summary>
  /// <param name="t">Тип настройки, реализующий интерфейс IOrderItemSetting</param>
  /// <returns>Настройка указанного типа или null</returns>
  object GetSetting(Type t);

  /// <summary>Удалить из списка настройку указанного типа</summary>
  /// <param name="t">Тип настройки, реализующий интерфейс IOrderItemSetting</param>
  void RemoveSetting(Type t);

  /// <summary>Какие-то дополнительные свойства</summary>
  object Tag { get; set; }
}
