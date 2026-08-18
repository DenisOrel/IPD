// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IOrderItemSetting
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Интерфейс, реализуемый какой-то настройкой для объекта состава производственного заказа
/// </summary>
public interface IOrderItemSetting : IAssignable, ICloneable
{
  /// <summary>Объект для синхронизации</summary>
  object SyncRoot { get; }

  /// <summary>Редактируемые данные</summary>
  object Data { get; }
}
