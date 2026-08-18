// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechParams.TechCardParamsProcessRoute
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Interfaces.Configuration;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.TechCard.TechParams;

/// <summary>
/// Класс для хранения параметров TechCard для "Маршрута обработки"
/// </summary>
[Serializable]
public class TechCardParamsProcessRoute : AppSettingsBase
{
  /// <summary>
  /// 
  /// </summary>
  protected bool _autoCheckIn;
  /// <summary>
  /// 
  /// </summary>
  protected bool _uniqueTechProc = true;
  /// <summary>
  /// 
  /// </summary>
  protected bool _uniqueCehRoute = true;
  /// <summary>
  /// 
  /// </summary>
  protected bool _uniqueBillet = true;
  /// <summary>
  /// 
  /// </summary>
  protected bool _uniqueMemberSborkaZakaz;
  /// <summary>
  /// 
  /// </summary>
  protected bool _forbiddenInMultiArts = true;

  /// <summary>Авто Сommit</summary>
  public bool AutoCheckIn
  {
    [DebuggerStepThrough] get => this._autoCheckIn;
    set => this._autoCheckIn = value;
  }

  /// <summary>Уникальность ТП в МО</summary>
  public bool UniqueTechProc
  {
    [DebuggerStepThrough] get => this._uniqueTechProc;
    set => this._uniqueTechProc = value;
  }

  /// <summary>Уникальность РМ в МО</summary>
  public bool UniqueCehRoute
  {
    [DebuggerStepThrough] get => this._uniqueCehRoute;
    set => this._uniqueCehRoute = value;
  }

  /// <summary>Уникальность заготовки в МО</summary>
  public bool UniqueBillet
  {
    [DebuggerStepThrough] get => this._uniqueBillet;
    set => this._uniqueBillet = value;
  }

  /// <summary>Уникальность входимости в сборку / заказ</summary>
  public bool UniqueMemberSborkaZakaz
  {
    [DebuggerStepThrough] get => this._uniqueMemberSborkaZakaz;
    set => this._uniqueMemberSborkaZakaz = value;
  }

  /// <summary>
  /// Запрет добавление одного и того же МО в несколько разных изделий
  /// </summary>
  public bool ForbiddenInMultiArts
  {
    [DebuggerStepThrough] get => this._forbiddenInMultiArts;
    set => this._forbiddenInMultiArts = value;
  }
}
