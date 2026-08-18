// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.RelationEventArgs
// Assembly: Intermech.Interfaces.AutoSelection, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A8A58CF2-90E0-4922-B0EB-2EB55893A867
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.xml

using Intermech.Interfaces.Compositions;
using System;

#nullable disable
namespace Intermech.Interfaces.AutoSelection;

/// <summary>
/// 
/// </summary>
public class RelationEventArgs : EventArgs
{
  /// <summary>
  /// 
  /// </summary>
  public RelObjInfoItem Relation;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relation"></param>
  public RelationEventArgs(RelObjInfoItem relation) => this.Relation = relation;
}
