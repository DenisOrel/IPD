// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator.TechObjectCreatorParams
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;

/// <summary>Доп. информация о контексте вызова создания объекта</summary>
/// <remarks>В данный момент используется для передачи параметров в автоподбор</remarks>
public class TechObjectCreatorParams : IObjectCreatorParams
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  public TechObjectCreatorParams(ISelectedItems items, IServiceProvider viewServices)
  {
    this.Items = items;
    this.ContextServices = viewServices;
    this.RelationMode = CompositionTargetMode.Add;
  }

  /// <summary>
  /// 
  /// </summary>
  public ISelectedItems Items { get; internal set; }

  /// <summary>
  /// 
  /// </summary>
  public IServiceProvider ContextServices { get; }

  /// <summary>Режим "асинхронного" создания объектов</summary>
  public bool AsyncMode { get; set; }

  /// <summary>Режим назначения сортировки</summary>
  public CompositionTargetMode RelationMode { get; set; }

  /// <summary>
  /// Режим создания заготовки объекта( как правило не требуется показывать диалоги создания)
  /// </summary>
  public bool RawMode => false;
}
