
// Type: Intermech.Interfaces.Client.VersionsRuleSources
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using System.Runtime.CompilerServices;


namespace Intermech.Interfaces.Client;

/// <summary>
/// Предоставляет сервисы по получению наиболее распространенных правил подбора версий. Является
/// thread-safe.
/// </summary>
public static class VersionsRuleSources
{
  /// <summary>
  /// Включает и выключает кэширования выбора правила с помощью динамических переменных.
  /// </summary>
  public static readonly DynamicVariable<bool> AllowCache = new DynamicVariable<bool>("VersionsRuleSources.AllowCache", false);
  /// <summary>
  /// Включает создание теневой копии правила подбора. Используется только в том случае, если разрешено кэширование выбранного правила подбора.
  /// </summary>
  public static readonly DynamicVariable<bool> RequireCopy = new DynamicVariable<bool>("VersionsRuleSources.RequireCopy", false);
  private static readonly IVersionsRuleSource currentWindowRule = (IVersionsRuleSource) new CurrentWindowRule();
  private static readonly IVersionsRuleSource editorRule = (IVersionsRuleSource) new EditorRuleSource();
  private static readonly DynamicVariable<VersionsRulePackage> currentWindowRuleCache = new DynamicVariable<VersionsRulePackage>("VersionsRuleSources.currentWindowRuleCache", (VersionsRulePackage) null);
  private static readonly DynamicVariable<VersionsRulePackage> editorRuleCache = new DynamicVariable<VersionsRulePackage>("VersionsRuleSources.editorRuleCache", (VersionsRulePackage) null);

  public static VersionsRulePackage GetCurrentWindowRule()
  {
    return VersionsRuleSources.GetRule(VersionsRuleSources.currentWindowRule, VersionsRuleSources.currentWindowRuleCache);
  }

  public static VersionsRulePackage GetEditorRule()
  {
    return VersionsRuleSources.GetRule(VersionsRuleSources.editorRule, VersionsRuleSources.editorRuleCache);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  private static VersionsRulePackage GetRule(
    IVersionsRuleSource source,
    DynamicVariable<VersionsRulePackage> cacheVar)
  {
    if (!VersionsRuleSources.AllowCache.Value)
      return source.GetRule();
    VersionsRulePackage rule = cacheVar.Value;
    if (rule == null)
    {
      rule = source.GetRule();
      if (VersionsRuleSources.RequireCopy.Value)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IVersionRulesCacheService service = ServiceUtils.GetService<IVersionRulesCacheService>((object) sessionKeeper.Session, true);
          VersionsRulePackage versionsRulePackage = OwnerIdAllocator.Allocate();
          FiltrationSettings filtrationSettings = (FiltrationSettings) service.GetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, rule.OwnerId, true).Clone();
          filtrationSettings.OwnerID = versionsRulePackage.OwnerId;
          service.SetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, versionsRulePackage.OwnerId, filtrationSettings);
          rule = versionsRulePackage;
        }
      }
      cacheVar.Value = rule;
    }
    return rule;
  }
}
