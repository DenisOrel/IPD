// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.Action.CommandActionParam
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Client.Commands.Action;

internal class CommandActionParam
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedItems"></param>
  public CommandActionParam([NotNull] ISelectedItems selectedItems, IServiceProvider contextServices)
  {
    this.SelectedItems = selectedItems;
    this.ContextServices = contextServices;
  }

  /// <summary>
  /// 
  /// </summary>
  public ISelectedItems SelectedItems { get; }

  /// <summary>
  /// 
  /// </summary>
  public IServiceProvider ContextServices { get; }
}
