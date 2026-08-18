// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.Edit.EditCommandActionParam
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Commands.Action;
using System;

#nullable disable
namespace Intermech.TechCard.Client.Commands.Edit;

/// <summary>
/// 
/// </summary>
/// <param name="selectedItems"></param>
internal class EditCommandActionParam(
  [NotNull] ISelectedItems selectedItems,
  IServiceProvider contextServices) : CommandActionParam(selectedItems, contextServices)
{
}
