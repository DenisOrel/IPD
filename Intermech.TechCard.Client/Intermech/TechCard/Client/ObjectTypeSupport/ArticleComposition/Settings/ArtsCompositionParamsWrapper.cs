// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Settings.ArtsCompositionParamsWrapper
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.Localization;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using System;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Settings;

/// <summary>
/// 
/// </summary>
internal class ArtsCompositionParamsWrapper
{
  /// <summary>
  /// 
  /// </summary>
  private IArtsCompositionParams _params;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="params"></param>
  public ArtsCompositionParamsWrapper(IArtsCompositionParams @params)
  {
    this._params = @params;
    this.NotUsed = new ArtsCompositionStatusParamWrapper(@params.StatusParams.FirstOrDefault<IArtsCompositionStatusParams>((Func<IArtsCompositionStatusParams, bool>) (item => item.Status == ArtsCompositionItemStatus.NotUsed)));
    this.NotAllUsed = new ArtsCompositionStatusParamWrapper(@params.StatusParams.FirstOrDefault<IArtsCompositionStatusParams>((Func<IArtsCompositionStatusParams, bool>) (item => item.Status == ArtsCompositionItemStatus.NotAllUsed)));
    this.AllUsed = new ArtsCompositionStatusParamWrapper(@params.StatusParams.FirstOrDefault<IArtsCompositionStatusParams>((Func<IArtsCompositionStatusParams, bool>) (item => item.Status == ArtsCompositionItemStatus.AllUsed)));
    this.VersionNotAllUsed = new ArtsCompositionStatusParamWrapper(@params.StatusParams.FirstOrDefault<IArtsCompositionStatusParams>((Func<IArtsCompositionStatusParams, bool>) (item => item.Status == ArtsCompositionItemStatus.VersionNotAllUsed)));
    this.VersionAllUsed = new ArtsCompositionStatusParamWrapper(@params.StatusParams.FirstOrDefault<IArtsCompositionStatusParams>((Func<IArtsCompositionStatusParams, bool>) (item => item.Status == ArtsCompositionItemStatus.VersionAllUsed)));
    this.UsedOverLimit = new ArtsCompositionStatusParamWrapper(@params.StatusParams.FirstOrDefault<IArtsCompositionStatusParams>((Func<IArtsCompositionStatusParams, bool>) (item => item.Status == ArtsCompositionItemStatus.UsedOverLimit)));
  }

  [CustomCategory("Attribute.TechCard.Client_25")]
  [CustomDisplayName("Attribute.TechCard.ArtsCompositionParams_ShowRemainQty")]
  [CustomDescription("Attribute.TechCard.ArtsCompositionParams_ShowRemainQty_Info")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  public bool ShowRemainQty
  {
    get => this._params.ShowRemainQty;
    set => this._params.ShowRemainQty = value;
  }

  [CustomCategory("Attribute.TechCard.Client_25")]
  [CustomDisplayName("Attribute.TechCard.ArtsCompositionParams_DesignQuantityMode")]
  [CustomDescription("Attribute.TechCard.ArtsCompositionParams_DesignQuantityMode_Info")]
  [TypeConverter(typeof (EnumDescConverter))]
  public ArtsCompositionQuantityMode DesignQuantityMode
  {
    get => this._params.DesignQuantityMode;
    set => this._params.DesignQuantityMode = value;
  }

  /// <summary>Изделия не выбиралось в ТП</summary>
  [CustomCategory("Attribute.TechCard.Client_61")]
  [CustomDisplayName("Attribute.TechCard.Client_53")]
  [CustomDescription("Attribute.TechCard.Client_53")]
  public ArtsCompositionStatusParamWrapper NotUsed { get; }

  /// <summary>
  /// Изделие выбиралось в ТП, но не всё количество использовано
  /// </summary>
  [CustomCategory("Attribute.TechCard.Client_61")]
  [CustomDisplayName("Attribute.TechCard.Client_62")]
  [CustomDescription("Attribute.TechCard.Client_54")]
  public ArtsCompositionStatusParamWrapper NotAllUsed { get; }

  /// <summary>Всё количество изделий выбрано в ТП</summary>
  [CustomCategory("Attribute.TechCard.Client_61")]
  [CustomDisplayName("Attribute.TechCard.Client_63")]
  [CustomDescription("Attribute.TechCard.Client_55")]
  public ArtsCompositionStatusParamWrapper AllUsed { get; }

  /// <summary>
  /// Изделие выбиралось, но не всё количество использовано в ТП. Изделие в конструкторском составе имеет версию, отличающуюся от версии в ТП.
  /// </summary>
  [CustomCategory("Attribute.TechCard.Client_61")]
  [CustomDisplayName("Attribute.TechCard.Client_64")]
  [CustomDescription("Attribute.TechCard.Client_56")]
  public ArtsCompositionStatusParamWrapper VersionNotAllUsed { get; }

  /// <summary>
  /// Всё количество изделий выбрано в ТП. Изделие в конструкторском составе имеет версию, отличающуюся от версии в ТП
  /// </summary>
  [CustomCategory("Attribute.TechCard.Client_61")]
  [CustomDisplayName("Attribute.TechCard.Client_65")]
  [CustomDescription("Attribute.TechCard.Client_57")]
  public ArtsCompositionStatusParamWrapper VersionAllUsed { get; }

  /// <summary>
  /// Количество комплектующих в ТП превышает количество изделий в конструкторском составе.
  /// </summary>
  [CustomCategory("Attribute.TechCard.Client_61")]
  [CustomDisplayName("Attribute.TechCard.Client_66")]
  [CustomDescription("Attribute.TechCard.Client_58")]
  public ArtsCompositionStatusParamWrapper UsedOverLimit { get; }
}
