// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechParams.TechCardParamsCommon
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Interfaces.Configuration;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.TechCard.TechParams;

/// <summary>Класс для хранения "общих" параметров ТechCard</summary>
[Serializable]
public class TechCardParamsCommon : AppSettingsBase
{
  /// <summary>Уровень раскрытия состава дерева навигатора</summary>
  protected int _navTreeExpandLevel = 2;
  /// <summary>
  /// Принудительное включение техн. объектов в контекст редактирования
  /// </summary>
  protected bool _forceAddObj2Context = true;
  /// <summary>
  /// Флаг отображения всех форм для типа объекта не все зависимости от наличия форм для справочников Imbase
  /// </summary>
  protected bool _showAllForms4Type;
  /// <summary>
  /// Флаг отображения карточки для объектов, созданных из справочника Imbase при команде "Изменить"
  /// </summary>
  private bool _showCard4ImbaseEdit;
  /// <summary>Флаг отображения диалога выбора ИИ</summary>
  private bool _displayEcoVersionDialog = true;
  /// <summary>Фильтр Imbase по умолчанию</summary>
  protected TechCardParams.ImbaseFilterMode _defImbaseFilter;
  /// <summary>
  ///  Режим команды "Вставить" в навигаторе для объектов одинакового типа
  /// </summary>
  protected TechCardParams.NavigatorPasteMode _pasteCommandMode = TechCardParams.NavigatorPasteMode.After;

  /// <summary>Уровень раскрытия состава дерева навигатора</summary>
  public virtual int NavTreeExpandLevel
  {
    [DebuggerStepThrough] get => this._navTreeExpandLevel;
    set => this._navTreeExpandLevel = value;
  }

  /// <summary>
  /// Принудительное включение техн. объектов в контекст редактирования
  /// </summary>
  public bool ForceAddObj2Context
  {
    [DebuggerStepThrough] get => this._forceAddObj2Context;
    set => this._forceAddObj2Context = value;
  }

  /// <summary>
  /// Отображения всех форм для типа объекта вне зависимости от наличия форм для справочников Imbase
  /// </summary>
  public bool ShowAllForms4Type
  {
    [DebuggerStepThrough] get => this._showAllForms4Type;
    set => this._showAllForms4Type = value;
  }

  /// <summary>
  /// Флаг отображения карточки для объектов, созданных из справочника Imbase при команде "Изменить"
  /// </summary>
  public bool ShowCard4ImbaseEdit
  {
    [DebuggerStepThrough] get => this._showCard4ImbaseEdit;
    [DebuggerStepThrough] set => this._showCard4ImbaseEdit = value;
  }

  /// <summary>Фильтр Imbase по умолчанию</summary>
  public TechCardParams.ImbaseFilterMode DefImbaseFilter
  {
    [DebuggerStepThrough] get => this._defImbaseFilter;
    set => this._defImbaseFilter = value;
  }

  /// <summary>
  /// Режим команды "Вставить" в навигаторе для объектов одинакового типа
  /// </summary>
  public TechCardParams.NavigatorPasteMode PasteCommandMode
  {
    [DebuggerStepThrough] get => this._pasteCommandMode;
    set => this._pasteCommandMode = value;
  }

  /// <summary>Отображать диалог выбора ИИ</summary>
  public bool DisplayEcoVersionDialog
  {
    [DebuggerStepThrough] get => this._displayEcoVersionDialog;
    [DebuggerStepThrough] set => this._displayEcoVersionDialog = value;
  }
}
