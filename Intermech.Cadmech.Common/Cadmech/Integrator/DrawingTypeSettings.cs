// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DrawingTypeSettings
// Assembly: Intermech.Cadmech.Common, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3D1D989-0F34-4F5C-8A7E-7002449397DA
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Common.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Common.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>Описывает настройки типа чертежей AutoCAD.</summary>
public sealed class DrawingTypeSettings : ICloneable
{
  private readonly GlobalId<int> documentType;
  private XRefMode xrefMode;
  private string stmName;

  /// <summary>Создает объект.</summary>
  /// <param name="documentType">Маркер для типа документов IPS, соответствующего типу чертежей AutoCAD</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на маркер типа документов не может быть null</exception>
  public DrawingTypeSettings(GlobalId<int> documentType)
  {
    this.documentType = documentType != null ? documentType : throw new ArgumentNullException();
    this.xrefMode = XRefMode.Ignore;
    this.stmName = string.Empty;
  }

  /// <summary>Клонирует текущий объект.</summary>
  /// <returns>Клог текущего объекта</returns>
  public DrawingTypeSettings Clone()
  {
    return new DrawingTypeSettings(this.documentType)
    {
      xrefMode = this.xrefMode,
      stmName = this.stmName
    };
  }

  /// <summary>Клонирует текущий объект.</summary>
  /// <returns>Клог текущего объекта</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>
  /// Возвращает маркер для типа документов IPS, соответствующего типу чертежей AutoCAD.
  /// </summary>
  public GlobalId<int> DocumentType => this.documentType;

  /// <summary>
  /// Возвращает или задает режим регистрации внешних ссылок для этого типа чертежей AutoCAD.
  /// </summary>
  public XRefMode XRefMode
  {
    get => this.xrefMode;
    set => this.xrefMode = value;
  }

  /// <summary>
  /// Возвращает или задает имя файла с параметрами сканирования шапки для данного типа чертежей AutoCAD. Если значение этого
  /// свойства не определено, то выполнять сканирование штампа не нужно.
  /// </summary>
  public string StmName
  {
    get => this.stmName;
    set => this.stmName = value;
  }

  /// <summary>Возвращает хеш-код объекта.</summary>
  /// <returns>Значениесхеш-кода</returns>
  public override int GetHashCode() => this.documentType.GetHashCode();

  /// <summary>
  /// Проверяет, является ли текущий объекты эквивалентным указанному объекту.
  /// </summary>
  /// <param name="obj">Другой объект</param>
  /// <returns>Признак эквивалентности объектов</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is DrawingTypeSettings drawingTypeSettings))
      return base.Equals(obj);
    return drawingTypeSettings.documentType.Equals((LocalId<int>) this.documentType) && drawingTypeSettings.xrefMode == this.xrefMode && !(drawingTypeSettings.stmName != this.stmName);
  }

  /// <summary>
  /// Возвращает название типа документов IPS, соответсвующего типу чертежей AutoCAD.
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this.documentType.ToString();
}
