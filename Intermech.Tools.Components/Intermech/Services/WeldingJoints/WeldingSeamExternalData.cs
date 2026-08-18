// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingSeamExternalData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>
/// Параметры сварного шва, полученные из внешней системы.
/// </summary>
internal sealed class WeldingSeamExternalData
{
  /// <summary>Создает объект.</summary>
  public WeldingSeamExternalData()
  {
    this.ConfigurationNames = new List<string>();
    this.Components = new List<WeldingSeamComponent>();
  }

  /// <summary>
  /// Возвращает или задает глобальный идентификатор якоря, к которому привязан шов.
  /// В документах CADMECH - это идентификатор атрибута поверхности.
  /// </summary>
  public Guid AnchorGuid { get; set; }

  /// <summary>
  /// Возвращает или задает признак, что это дополнительный шов на оборотной стороне.
  /// </summary>
  public bool IsOnBackSide { get; set; }

  /// <summary>
  /// Возвращает список имен конфигураций документа CAD-системы, в которых шов виден.
  /// </summary>
  public List<string> ConfigurationNames { get; private set; }

  /// <summary>Возвращает или задает 'Номер шва'.</summary>
  public string Number { get; set; }

  /// <summary>
  /// Возвращает или задает 'Стандарт на типы и конструктивные элементы швов'.
  /// </summary>
  public string StandardName { get; set; }

  /// <summary>Возвращает или задает 'Обозначение шва по стандарту'.</summary>
  public string DesignationByStandard { get; set; }

  /// <summary>
  /// Возвращает или задает 'Обозначение способа сварки по стандарту'.
  /// </summary>
  public string WeldingMethodDesignationByStandard { get; set; }

  /// <summary>
  /// Возвращает или задает 'Размер катета согласно стандарту'.
  /// </summary>
  public string LegSizeByStandard { get; set; }

  /// <summary>Возвращает или задает 'Верхнее отклонение катета'.</summary>
  public string LegUpperTolerance { get; set; }

  /// <summary>Возвращает или задает 'Нижнее отклонение катета'.</summary>
  public string LegLowerTolerance { get; set; }

  /// <summary>Возвращает или задает 'Дополнительные размеры шва'.</summary>
  public string ExtraDimensions { get; set; }

  /// <summary>Возвращает или задает 'Примечание'.</summary>
  public string Note { get; set; }

  /// <summary>Возвращает или задает 'Длина шва'.</summary>
  public string Length { get; set; }

  /// <summary>Возвращает или задает 'Количество швов'.</summary>
  public string Count { get; set; }

  /// <summary>
  /// Возвращает или задает 'Обозначение контрольного комплекса или категории контроля шва'.
  /// </summary>
  public string ControlComplexDesignation { get; set; }

  /// <summary>Возвращает или задает "Тип соединения"</summary>
  public WeldingSeamGeometryType GeometryType { get; set; }

  /// <summary>
  /// Возвращает или задает 'Длина соединения (полная длина участка сварки)'.
  /// </summary>
  public string FullLength { get; set; }

  /// <summary>Возвращает или задает 'Отступ слева'.</summary>
  public string LeftOffset { get; set; }

  /// <summary>Возвращает или задает 'Отступ справа'.</summary>
  public string RightOffset { get; set; }

  /// <summary>
  /// Возвращает или задает 'Тип шва (длина/шаг-способ задания сегментного шва)'.
  /// </summary>
  public WeldingSeamSegmentationType SegmentationType { get; set; }

  /// <summary>Возвращает или задает 'Шаг шва'.</summary>
  public string SegmentStep { get; set; }

  /// <summary>Возвращает или задает 'Длина сварного элемента'.</summary>
  public string SegmentLength { get; set; }

  /// <summary>Возвращает или задает 'Зазор шва'.</summary>
  public string Gap { get; set; }

  /// <summary>Возвращает или задает 'Толщина первой детали'.</summary>
  public string FirstPartThickness { get; set; }

  /// <summary>Возвращает или задает 'Толщина второй детали'.</summary>
  public string SecondPartThickness { get; set; }

  /// <summary>Возвращает или задает 'Вид соединения'.</summary>
  public string ConnectionKind { get; set; }

  /// <summary>
  /// Возвращает или задает 'Шов выполнить при монтаже изделия'.
  /// </summary>
  public bool MakeAtInstallationStage { get; set; }

  /// <summary>Возвращает или задает 'Шов по замкнутой линии'.</summary>
  public bool MakeClosed { get; set; }

  /// <summary>
  /// Возвращает или задает 'Усиление шва снять на лицевой стороне'.
  /// </summary>
  public bool RemoveReinforcementOnFrontSide { get; set; }

  /// <summary>
  /// Возвращает или задает 'Наплывы и неровности шва обработать с плавным переходом к основному металлу на лицевой стороне'.
  /// </summary>
  public bool ProcessIrregularitiesOnFrontSide { get; set; }

  /// <summary>
  /// Возвращает или задает 'Шов по незамкнутой линии на лицевой стороне'.
  /// </summary>
  public bool MakeOpenOnFrontSide { get; set; }

  /// <summary>
  /// Возвращает или задает 'Усиление шва снять на оборотной стороне'
  /// </summary>
  public bool? RemoveReinforcementOnBackSide { get; set; }

  /// <summary>
  /// Возвращает или задает 'Наплывы и неровности шва обработать с плавным переходом к основному металлу на оборотной стороне'
  /// </summary>
  public bool? ProcessIrregularitiesOnBackSide { get; set; }

  /// <summary>
  /// Возвращает или задает 'Шов по незамкнутой линии на оборотной стороне'
  /// </summary>
  public bool? MakeOpenOnBackSide { get; set; }

  /// <summary>Возвращает или задает 'Эскиз сварного шва'.</summary>
  public byte[] DxfSketch { get; set; }

  /// <summary>Возвращает список свариваемых компонентов.</summary>
  public List<WeldingSeamComponent> Components { get; private set; }
}
