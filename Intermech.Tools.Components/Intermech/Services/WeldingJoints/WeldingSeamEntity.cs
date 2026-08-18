// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingSeamEntity
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Experimental.Kernel.Entities;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>Доменнный объект 'Сварной шов'.</summary>
[DBObjectType("CADD98C1-306C-11D8-B4E9-00304F19F545")]
internal sealed class WeldingSeamEntity
{
  /// <summary>Создает объект.</summary>
  public WeldingSeamEntity()
  {
    this.ObjectId = 0L;
    this.DxfSketch = DBFileValue.Empty;
  }

  /// <summary>Возвращает или задает идентификатор версии объекта.</summary>
  [Key]
  [DBAttributeType("CAD00029-306C-11D8-B4E9-00304F19F545")]
  public long ObjectId { get; set; }

  /// <summary>Возвращает или задает 'Внешний ключ объекта IPS'.</summary>
  [DBAttributeType("CAD00378-306C-11D8-B4E9-00304F19F545")]
  public string ExternalKey { get; set; }

  /// <summary>
  /// Возвращает или задает атрибут 'Создано по CAD-модели'.
  /// </summary>
  [DBAttributeType("CAD0153E-306C-11D8-B4E9-00304F19F545")]
  public bool BasedOnCADModel { get; set; }

  /// <summary>Возвращает или задает 'Номер шва'.</summary>
  [DBAttributeType("CADD98C8-306C-11D8-B4E9-00304F19F545")]
  public string Number { get; set; }

  /// <summary>
  /// Возвращает или задает 'Стандарт на типы и конструктивные элементы швов'.
  /// </summary>
  [DBAttributeType("CADD98CD-306C-11D8-B4E9-00304F19F545")]
  public string StandardName { get; set; }

  /// <summary>Возвращает или задает 'Обозначение шва по стандарту'.</summary>
  [DBAttributeType("CADD98CB-306C-11D8-B4E9-00304F19F545")]
  public string DesignationByStandard { get; set; }

  /// <summary>
  /// Возвращает или задает 'Обозначение способа сварки по стандарту'.
  /// </summary>
  [DBAttributeType("CADD98CA-306C-11D8-B4E9-00304F19F545")]
  public string WeldingMethodDesignationByStandard { get; set; }

  /// <summary>
  /// Возвращает или задает 'Размер катета согласно стандарту'.
  /// </summary>
  [DBAttributeType("CADD98CC-306C-11D8-B4E9-00304F19F545")]
  public string LegSizeByStandard { get; set; }

  /// <summary>Возвращает или задает 'Верхнее отклонение катета'.</summary>
  [DBAttributeType("CADD996F-306C-11D8-B4E9-00304F19F545")]
  public string LegUpperTolerance { get; set; }

  /// <summary>Возвращает или задает 'Нижнее отклонение катета'.</summary>
  [DBAttributeType("CADD9970-306C-11D8-B4E9-00304F19F545")]
  public string LegLowerTolerance { get; set; }

  /// <summary>Возвращает или задает 'Дополнительные размеры шва'.</summary>
  [DBAttributeType("CADD98C5-306C-11D8-B4E9-00304F19F545")]
  public string ExtraDimensions { get; set; }

  /// <summary>Возвращает или задает 'Примечание'.</summary>
  [DBAttributeType("CAD00021-306C-11D8-B4E9-00304F19F545")]
  public string Note { get; set; }

  /// <summary>Возвращает или задает 'Длина шва'.</summary>
  [DBAttributeType("CADD98C4-306C-11D8-B4E9-00304F19F545")]
  public string Length { get; set; }

  /// <summary>
  /// Возвращает или задает 'Обозначение контрольного комплекса или категории контроля шва'.
  /// </summary>
  [DBAttributeType("CADD98C9-306C-11D8-B4E9-00304F19F545")]
  public string ControlComplexDesignation { get; set; }

  /// <summary>Возвращает или задает "Тип соединения"</summary>
  [DBAttributeType("CADD99E0-306C-11D8-B4E9-00304F19F545")]
  public string GeometryType { get; set; }

  /// <summary>
  /// Возвращает или задает 'Длина соединения (полная длина участка сварки)'.
  /// </summary>
  [DBAttributeType("CADD99DC-306C-11D8-B4E9-00304F19F545")]
  public string FullLength { get; set; }

  /// <summary>Возвращает или задает 'Отступ слева'.</summary>
  [DBAttributeType("CADD99DE-306C-11D8-B4E9-00304F19F545")]
  public string LeftOffset { get; set; }

  /// <summary>Возвращает или задает 'Отступ справа'.</summary>
  [DBAttributeType("CADD99DF-306C-11D8-B4E9-00304F19F545")]
  public string RightOffset { get; set; }

  /// <summary>
  /// Возвращает или задает 'Тип шва (длина/шаг-способ задания сегментного шва)'.
  /// </summary>
  [DBAttributeType("CADD99E1-306C-11D8-B4E9-00304F19F545")]
  public string SegmentationType { get; set; }

  /// <summary>Возвращает или задает 'Шаг шва'.</summary>
  [DBAttributeType("CADD99E4-306C-11D8-B4E9-00304F19F545")]
  public string SegmentStep { get; set; }

  /// <summary>Возвращает или задает 'Длина сварного элемента'.</summary>
  [DBAttributeType("CADD99DB-306C-11D8-B4E9-00304F19F545")]
  public string SegmentLength { get; set; }

  /// <summary>Возвращает или задает 'Зазор шва'.</summary>
  [DBAttributeType("CADD99DD-306C-11D8-B4E9-00304F19F545")]
  public string Gap { get; set; }

  /// <summary>Возвращает или задает 'Толщина первой детали'.</summary>
  [DBAttributeType("CADD99E3-306C-11D8-B4E9-00304F19F545")]
  public string FirstPartThickness { get; set; }

  /// <summary>Возвращает или задает 'Толщина второй детали'.</summary>
  [DBAttributeType("CADD99E2-306C-11D8-B4E9-00304F19F545")]
  public string SecondPartThickness { get; set; }

  /// <summary>Возвращает или задает 'Вид соединения'.</summary>
  [DBAttributeType("CADD99DA-306C-11D8-B4E9-00304F19F545")]
  public string ConnectionKind { get; set; }

  /// <summary>
  /// Возвращает или задает 'Шов выполнить при монтаже изделия'.
  /// </summary>
  [DBAttributeType("CADD98D0-306C-11D8-B4E9-00304F19F545")]
  public bool MakeAtInstallationStage { get; set; }

  /// <summary>Возвращает или задает 'Шов по замкнутой линии'.</summary>
  [DBAttributeType("CADD98D1-306C-11D8-B4E9-00304F19F545")]
  public bool MakeClosed { get; set; }

  /// <summary>
  /// Возвращает или задает 'Усиление шва снять на лицевой стороне'.
  /// </summary>
  [DBAttributeType("CADD98CE-306C-11D8-B4E9-00304F19F545")]
  public bool RemoveReinforcementOnFrontSide { get; set; }

  /// <summary>
  /// Возвращает или задает 'Наплывы и неровности шва обработать с плавным переходом к основному металлу на лицевой стороне'.
  /// </summary>
  [DBAttributeType("CADD98C6-306C-11D8-B4E9-00304F19F545")]
  public bool ProcessIrregularitiesOnFrontSide { get; set; }

  /// <summary>
  /// Возвращает или задает 'Шов по незамкнутой линии на лицевой стороне'.
  /// </summary>
  [DBAttributeType("CADD98D2-306C-11D8-B4E9-00304F19F545")]
  public bool MakeOpenOnFrontSide { get; set; }

  /// <summary>
  /// Возвращает или задает 'Усиление шва снять на оборотной стороне'
  /// </summary>
  [DBAttributeType("CADD98CF-306C-11D8-B4E9-00304F19F545")]
  public bool? RemoveReinforcementOnBackSide { get; set; }

  /// <summary>
  /// Возвращает или задает 'Наплывы и неровности шва обработать с плавным переходом к основному металлу на оборотной стороне'
  /// </summary>
  [DBAttributeType("CADD98C7-306C-11D8-B4E9-00304F19F545")]
  public bool? ProcessIrregularitiesOnBackSide { get; set; }

  /// <summary>
  /// Возвращает или задает 'Шов по незамкнутой линии на оборотной стороне'
  /// </summary>
  [DBAttributeType("CADD98D3-306C-11D8-B4E9-00304F19F545")]
  public bool? MakeOpenOnBackSide { get; set; }

  /// <summary>
  /// Возвращает или задает 'Эскиз сварного шва', хранящийся в атрибуте 'Файл'.
  /// </summary>
  [DBAttributeType("CAD0004B-306C-11D8-B4E9-00304F19F545")]
  public DBFileValue DxfSketch { get; set; }

  /// <summary>Возвращает свариваемые компоненты.</summary>
  [InverseEntity(typeof (MechanicalArticleEntity))]
  [DBRelationType("CADD9A10-306C-11D8-B4E9-00304F19F545")]
  public List<WeldingSeamComponentOccurence> Components { get; set; }
}
