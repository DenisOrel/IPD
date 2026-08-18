// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.CheckOutCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Commands;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// 
/// </summary>
internal class CheckOutCommand : CheckoutItemsCommand
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void DoAfterProceedItems(IUserSession session)
  {
    base.DoAfterProceedItems(session);
    TechCardSelectedItemsCommand.ClearCheckedItems(this.ContextServices);
  }
}
