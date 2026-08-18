// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.Action.CommandAction
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Commands.Action;

internal abstract class CommandAction
{
  /// <summary>
  /// 
  /// </summary>
  protected readonly CommandActionParam _actionParam;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedItems"></param>
  protected CommandAction([NotNull] CommandActionParam actionParam)
  {
    this._actionParam = actionParam;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public abstract bool Execute(out IList<CategoryValue> modificationsList);
}
