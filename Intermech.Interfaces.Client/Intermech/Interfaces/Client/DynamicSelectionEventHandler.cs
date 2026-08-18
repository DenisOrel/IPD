// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DynamicSelectionEventHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Делегат обработки динамического выбора из окна выбора Навигатора.
/// Вызывается дважды для одного объекта. Сначала с режимом PreSelect,
/// и, если возвращено true, второй раз после обработки ядром с режимом Select
/// </summary>
/// <param name="selectedObjectId">Идентификатор выбранного объекта</param>
/// <param name="mode">Режим выбора объекта</param>
/// <returns>Возвращает true для продолжения обработки объекта, false, если обработка объекта не нужна</returns>
public delegate bool DynamicSelectionEventHandler(long selectedObjectId, DynamicSelectionMode mode);
