// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierService.PrefixSearchCondition
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;


namespace Intermech.Kernel.Services.ClassifierService;

internal sealed class PrefixSearchCondition : ISearchCondition
{
  private readonly ConditionStructure[] _conditions;

  public PrefixSearchCondition(string prefix, int attributeID)
  {
    if (string.IsNullOrEmpty(prefix))
      return;
    this._conditions = new ConditionStructure[1]
    {
      new ConditionStructure(attributeID, RelationalOperators.StartString, (object) prefix, LogicalOperators.AND, 0, true)
    };
  }

  public ConditionStructure[] GetConditions(IUserSession session, int objectType)
  {
    return this._conditions;
  }
}
