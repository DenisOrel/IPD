// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.ObjectEventArgs
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
public class ObjectEventArgs : EventArgs
{
  /// <summary>
  /// 
  /// </summary>
  public ObjInfoItem Object;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aObject"></param>
  public ObjectEventArgs(ObjInfoItem aObject) => this.Object = aObject;
}
