// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.ProcRouteThroughCreateCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Compositions;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>
/// 
/// </summary>
internal class ProcRouteThroughCreateCommand : ProcRouteThroughBaseCommand
{
  /// <summary>Конструктор</summary>
  public ProcRouteThroughCreateCommand()
    : base("throughCreateNode")
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override List<ObjInfoItem> GetOperInfo2LinkList() => this._unlinkedOperList;
}
