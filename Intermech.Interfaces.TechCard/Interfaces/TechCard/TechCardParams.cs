// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechCardParams
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Interfaces.TechCard.TechParams;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Класс для хранения параметров ТechCard</summary>
[Serializable]
public class TechCardParams
{
  /// <summary>
  /// 
  /// </summary>
  private TechCardParamsCommon _common;
  /// <summary>Настройка маршрута обработки</summary>
  private TechCardParamsProcessRoute _processRoute;
  /// <summary>
  /// 
  /// </summary>
  private TechCardParamsTechProc _techProc;
  /// <summary>
  /// 
  /// </summary>
  private TechCardParamsZagot _zagot;
  /// <summary>
  /// 
  /// </summary>
  private TechCardParamsCehRoute _cehRoute;

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData()
  {
    this._common = new TechCardParamsCommon();
    this._processRoute = new TechCardParamsProcessRoute();
    this._techProc = new TechCardParamsTechProc();
    this._cehRoute = new TechCardParamsCehRoute();
    this._zagot = new TechCardParamsZagot();
    this.Portal = new TechCardParamsPortal();
  }

  /// <summary>
  /// 
  /// </summary>
  public TechCardParams() => this.InitializeData();

  /// <summary>"Общие" настройки</summary>
  public virtual TechCardParamsCommon Common
  {
    [DebuggerStepThrough] get => this._common;
  }

  /// <summary>Настройки портала</summary>
  public virtual TechCardParamsPortal Portal { get; private set; }

  /// <summary>Настройки для маршрута обработки</summary>
  public virtual TechCardParamsProcessRoute ProcessRoute
  {
    [DebuggerStepThrough] get => this._processRoute;
  }

  /// <summary>Настройки для ТП</summary>
  public virtual TechCardParamsTechProc TechProc
  {
    [DebuggerStepThrough] get => this._techProc;
  }

  /// <summary>Настройки для заготовки</summary>
  public virtual TechCardParamsZagot Zagot
  {
    [DebuggerStepThrough] get => this._zagot;
  }

  /// <summary>Настройки для РМ</summary>
  public virtual TechCardParamsCehRoute CehRoute
  {
    [DebuggerStepThrough] get => this._cehRoute;
  }

  /// <summary>Режимы работы фильтров Imbase</summary>
  public enum ImbaseFilterMode
  {
    /// <summary>Нет</summary>
    [CustomDescription("Attribute.Interfaces.TechCard_14")] None = 0,
    /// <summary>Общий</summary>
    [CustomDescription("Attribute.Interfaces.TechCard_15")] Common = 2,
    /// <summary>Пользователь</summary>
    [CustomDescription("Attribute.Interfaces.TechCard_16")] User = 3,
    /// <summary>Предметная область</summary>
    [CustomDescription("Attribute.Interfaces.TechCard_17")] Area = 4,
    /// <summary>Роль</summary>
    [CustomDescription("Attribute.Interfaces.TechCard_18")] Role = 5,
  }

  /// <summary>
  /// Режим вставки в навигаторе для объектов одинакового типа
  /// </summary>
  public enum NavigatorPasteMode
  {
    /// <summary>Вставить перед</summary>
    [CustomDescription("Attribute.Interfaces.TechCard_23")] Before,
    /// <summary>Вставить после</summary>
    [CustomDescription("Attribute.Interfaces.TechCard_24")] After,
    /// <summary>Отображение меню</summary>
    [CustomDescription("Attribute.Interfaces.TechCard_25")] ShowMenu,
  }

  /// <summary>Тип исходной системы узла портала</summary>
  [Flags]
  public enum PortalSourceSystemType
  {
    [CustomDescription("Attribute.Interfaces.TechCard_14")] None = 0,
    [Description("SEARCH")] Search = 1,
    [Description("IPS")] IPS = 2,
  }
}
