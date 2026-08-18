// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Imbase.ITechCardImbaseObjectCreatorService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Imbase.Selection;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Imbase;

/// <summary>
/// 
/// </summary>
public interface ITechCardImbaseObjectCreatorService
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectTypeId"></param>
  /// <param name="items"></param>
  /// <param name="contextServices"></param>
  IList<ImbaseObjectInfoItem> SelectObjects(
    int objectTypeId,
    ISelectedItems items,
    IServiceProvider contextServices);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectTypeId"></param>
  /// <param name="items"></param>
  /// <param name="contextServices"></param>
  void CreateObjects(int objectTypeId, ISelectedItems items, IServiceProvider contextServices);
}
