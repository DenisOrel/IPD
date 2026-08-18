// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.RelationVisualizer.UserSettings
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Pdm.RelationVisualizer;

/// <summary>
/// Класс для представления пользовательских настроек Визуализатора связей
/// </summary>
[Serializable]
public class UserSettings
{
  /// <summary>Максимальная отображаемая длина заголовка</summary>
  private uint maxCaptionLength = 10;
  /// <summary>Зачитывать ли невидимый состав/применяемость</summary>
  private bool needInvisibleTree = true;
  /// <summary>Разрешать ли создавать связи мышкой</summary>
  public bool allowCreatingRelations;
  /// <summary>Формула именования, если у объекта нет заголовка</summary>
  private RelVisPred.NoCaptionFormula noCaptionFormula;

  /// <summary>Форма именования объектов без Заголовков</summary>
  [TypeConverter(typeof (EnumDescConverter))]
  [CustomDisplayName("Attribute.Interfaces.Pdm_27")]
  public RelVisPred.NoCaptionFormula NoCaptionFormula
  {
    get => this.noCaptionFormula;
    set => this.noCaptionFormula = value;
  }

  /// <summary>Максимальная отображаемая длина заголовка</summary>
  [CustomDisplayName("Attribute.Interfaces.Pdm_18")]
  [CustomDescription("Attribute.Interfaces.Pdm_21")]
  public uint MaxCaptionLength
  {
    get => this.maxCaptionLength;
    set => this.maxCaptionLength = value;
  }

  /// <summary>Зачитывать ли невидимый состав/применяемость</summary>
  [CustomDisplayName("Attribute.Interfaces.Pdm_19")]
  [CustomDescription("Attribute.Interfaces.Pdm_20")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool NeedInvisibleTree
  {
    get => this.needInvisibleTree;
    set => this.needInvisibleTree = value;
  }
}
