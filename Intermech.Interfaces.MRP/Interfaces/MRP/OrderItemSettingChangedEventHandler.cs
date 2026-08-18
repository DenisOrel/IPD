// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.OrderItemSettingChangedEventHandler
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Делегат события об изменении в редакторе настройки для объекта состава производственного заказа
/// </summary>
/// <param name="sender">Контрол (редактор настройки)</param>
/// <param name="e">Аргументы события</param>
public delegate void OrderItemSettingChangedEventHandler(object sender, EventArgs e);
