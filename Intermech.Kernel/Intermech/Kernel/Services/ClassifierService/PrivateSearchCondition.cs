// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierService.PrivateSearchCondition
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;


namespace Intermech.Kernel.Services.ClassifierService;

internal sealed class PrivateSearchCondition : ISearchCondition
{
  private readonly bool _formulaPrivate;

  public PrivateSearchCondition(bool formulaPrivate) => this._formulaPrivate = formulaPrivate;

  public ConditionStructure[] GetConditions(IUserSession session, int objectType)
  {
    if (!this._formulaPrivate)
      return (ConditionStructure[]) null;
    return new ConditionStructure[1]
    {
      new ConditionStructure(0, RelationalOperators.ObjectTypeFilter, (object) objectType, LogicalOperators.AND, 0, false)
    };
  }
}
