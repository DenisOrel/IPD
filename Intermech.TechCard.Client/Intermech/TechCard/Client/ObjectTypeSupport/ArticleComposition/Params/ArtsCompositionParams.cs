// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params.ArtsCompositionParams
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Extensions;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;

/// <summary>Параметры для контекстных сборочных единиц</summary>
internal class ArtsCompositionParams : IArtsCompositionParams
{
  /// <summary>
  /// 
  /// </summary>
  private readonly List<IArtsCompositionStatusParams> _statusParams = new List<IArtsCompositionStatusParams>();

  /// <summary>
  /// 
  /// </summary>
  private void InitializeData()
  {
    foreach (int values in (IEnumerable<ArtsCompositionItemStatus>) EnumType.GetValuesList<ArtsCompositionItemStatus>())
    {
      ArtsCompositionStatusParams compositionStatusParams = new ArtsCompositionStatusParams((ArtsCompositionItemStatus) values);
      this._statusParams.Add((IArtsCompositionStatusParams) compositionStatusParams);
      DefaultColorAttribute attribute = ((ArtsCompositionItemStatus) values).GetAttribute<DefaultColorAttribute>();
      if (attribute != null)
        compositionStatusParams.Color = attribute.Color;
    }
  }

  /// <summary>Конструктор</summary>
  public ArtsCompositionParams() => this.InitializeData();

  /// <summary>
  /// Отображение оставшегося количества при добавлении комплектующих
  /// </summary>
  public bool ShowRemainQty { get; set; }

  /// <summary>
  /// Режим подсчета "конструкторского" количества для контекстной сборочной единицы
  /// </summary>
  public ArtsCompositionQuantityMode DesignQuantityMode { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public IReadOnlyList<IArtsCompositionStatusParams> StatusParams
  {
    get => (IReadOnlyList<IArtsCompositionStatusParams>) this._statusParams;
  }
}
