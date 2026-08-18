// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierService.LocalOnlySearchCondition
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;


namespace Intermech.Kernel.Services.ClassifierService;

internal sealed class LocalOnlySearchCondition : ISearchCondition
{
  private readonly ConditionStructure[] _conditions;

  public LocalOnlySearchCondition(IUserSession session)
  {
    if (!session.Configurations.ReadBool("CLIENT", "Classifiers", "LocalOnly", false, DBConfigMode.GlobalOnly))
      return;
    ISitesCacheService customService = (ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService));
    if (customService == null || customService.Info == null)
      return;
    this._conditions = new ConditionStructure[2]
    {
      new ConditionStructure(-17, RelationalOperators.Empty, (object) null, LogicalOperators.OR, 1, false),
      new ConditionStructure(-17, RelationalOperators.StartString, (object) customService.Info.Code, LogicalOperators.AND, -1, false)
    };
  }

  public ConditionStructure[] GetConditions(IUserSession session, int objectType)
  {
    return this._conditions;
  }
}
