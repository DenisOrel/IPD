// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmCompositionTraceResult
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Флажки, означающие результаты трассировки элемента состава
/// </summary>
[Flags]
[Serializable]
public enum PdmCompositionTraceResult : long
{
  /// <summary>Никаких результатов нет</summary>
  None = 0,
  /// <summary>
  /// [Information] Объект состава имеет в своём составе один маршрут обработки
  /// </summary>
  HasOneRoute = 2,
  /// <summary>
  /// [Information] Уровнем продвижения объекта не является "Производство и эксплуатация"
  /// </summary>
  NotManufacturingLevel = 4,
  /// <summary>
  /// [Warning] Объект состава участвует в допустимых заменах
  /// </summary>
  HasSubstitutes = 65536, // 0x0000000000010000
  /// <summary>
  /// [Warning] Объект состава имеет в своём составе несколько маршрутов обработки
  /// </summary>
  HasSomeRoutes = 131072, // 0x0000000000020000
  /// <summary>[Warning] Не указано количество для объекта состава</summary>
  WithoutQuantity = 262144, // 0x0000000000040000
  /// <summary>
  /// [Error] У элемента есть ошибки конфигуратора составов IPS
  /// </summary>
  PdmConfiguratorError = 281474976710656, // 0x0001000000000000
  /// <summary>[Error] Экземпляр не может входить в состав партии</summary>
  InstanceInPartyError = 562949953421312, // 0x0002000000000000
}
