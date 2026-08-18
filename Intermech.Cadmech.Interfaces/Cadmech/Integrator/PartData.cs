// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.PartData
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Содержит расшифрованное описание объекта (детали или подсборки), описанного в чертеже и входящего в
/// состав сборочной единицы.
/// </summary>
public class PartData : ICloneable
{
  private TaggingModes taggingMode;
  private char originalSectionCode;
  private string originalTag;
  private string designation;
  private string imbaseKey;
  private string oldArticleId;
  private Guid partGuid;
  private string okpCode;
  private string name;
  private char sectionCode;
  private string docFormat;
  private string dimensions;
  private string posDesignations;
  private MeasuredValue mass;
  private long materialId;
  private long objectId;

  /// <summary>Создает объект.</summary>
  public PartData() => this.objectId = 0L;

  /// <summary>
  /// Возвращает или задает способ идентификации объекта в базе данных.
  /// </summary>
  public TaggingModes TaggingMode
  {
    get => this.taggingMode;
    set => this.taggingMode = value;
  }

  /// <summary>
  /// Возвращает или задает исходную букву раздела СП. Если объект идентифицируется с помощью
  /// ключа IMBASE, то значение этого свойства может быть равно 'I'.
  /// </summary>
  public char OriginalSectionCode
  {
    get => this.originalSectionCode;
    set => this.originalSectionCode = value;
  }

  /// <summary>
  /// Возвращает или задает идентификатор объекта так, как он был задан в исходном файле.
  /// </summary>
  public string OriginalTag
  {
    get => this.originalTag;
    set => this.originalTag = value;
  }

  /// <summary>
  /// Возвращает или задает обозначение детали или подсборки. Может быть равно String.Empty, если объект
  /// идентифицируется с помощью ключа IMBASE.
  /// </summary>
  public string Designation
  {
    get => this.designation;
    set => this.designation = value;
  }

  /// <summary>
  /// Возвращает или задает ключ IMBASE для  детали или подсборки. Может быть равно String.Empty, если объект
  /// идентифицируется с помощью обозначения.
  /// </summary>
  public string ImbaseKey
  {
    get => this.imbaseKey;
    set => this.imbaseKey = value;
  }

  /// <summary>
  /// Это поле используется или Cadmech'ом, или старым AVS. В создании спецификации IPS оно никак не участвует.
  /// </summary>
  public string OldArticleId
  {
    get => this.oldArticleId;
    set => this.oldArticleId = value;
  }

  /// <summary>
  /// Возвращает или задает уникальный идентификатор компонента. Он хранится в чертеже и используется для
  /// синхронизации проектного состава исполнений сборочной единицы.
  /// </summary>
  public Guid PartGuid
  {
    get => this.partGuid;
    set => this.partGuid = value;
  }

  /// <summary>
  /// Возвращает или задает букву, определяющую раздел СП. Если объект идентифицируется с помощью
  /// ключа IMBASE, то значение этого свойства может быть равно 'I'.
  /// </summary>
  public char SectionCode
  {
    get => this.sectionCode;
    set => this.sectionCode = value;
  }

  /// <summary>Возвращает или задает код ОКП детали или подсборки.</summary>
  public string OKP
  {
    get => this.okpCode;
    set => this.okpCode = value;
  }

  /// <summary>
  /// Возвращает или задает наименование детали или подсборки.
  /// </summary>
  public string Name
  {
    get => this.name;
    set => this.name = value;
  }

  /// <summary>
  /// Возвращает или задает формат документа (A3, A4 и т.д.).
  /// </summary>
  public string DocumentFormat
  {
    get => this.docFormat;
    set => this.docFormat = value;
  }

  /// <summary>Возвращает или задает строку с размерами или массой.</summary>
  public string Dimensions
  {
    get => this.dimensions;
    set => this.dimensions = value;
  }

  /// <summary>
  /// Возвращает или задает позиционные обозначения. Заполняется только при взаимодействии с
  /// приложениями ТЕХНИКОН.
  /// </summary>
  public string PosDesignations
  {
    get => this.posDesignations;
    set => this.posDesignations = value;
  }

  /// <summary>
  /// Возвращает или задает массу изделия.
  /// Значение свойства может быть не задано и равно null.
  /// </summary>
  public MeasuredValue Mass
  {
    get => this.mass;
    set => this.mass = value;
  }

  /// <summary>
  /// Возвращает или задает идентификатор версии материала изделия.
  /// </summary>
  public long MaterialId
  {
    get => this.materialId;
    set => this.materialId = value;
  }

  /// <summary>
  /// Возвращает или задает идентификатор версии детали или подсборки. Этого значения нет в исходном
  /// файле, оно заполняется во время работы с базой данных.
  /// </summary>
  public long ObjectId
  {
    get => this.objectId;
    set => this.objectId = value;
  }

  /// <summary>Клонирует объект.</summary>
  /// <returns>Клон</returns>
  public PartData Clone()
  {
    return new PartData()
    {
      taggingMode = this.taggingMode,
      originalSectionCode = this.originalSectionCode,
      originalTag = this.originalTag,
      designation = this.designation,
      imbaseKey = this.imbaseKey,
      oldArticleId = this.oldArticleId,
      partGuid = this.partGuid,
      okpCode = this.okpCode,
      name = this.name,
      sectionCode = this.sectionCode,
      docFormat = this.docFormat,
      dimensions = this.dimensions,
      posDesignations = this.posDesignations,
      mass = this.mass != null ? (MeasuredValue) this.mass.Clone() : (MeasuredValue) null,
      materialId = this.materialId,
      objectId = this.objectId
    };
  }

  /// <summary>Клонирует объект.</summary>
  /// <returns>Клон</returns>
  object ICloneable.Clone() => (object) this.Clone();
}
