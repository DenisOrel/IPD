
// Type: Intermech.Interfaces.VersionsRuleConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;


namespace Intermech.Interfaces
{
    /// <summary>Константы для правил подбора версий</summary>
    public abstract class VersionsRuleConsts
    {
      /// <summary>[Последние версии объектов]</summary>
      public static readonly string ruleLatestVersions = LocalizationHolder.rm.GetString("Interfaces_527");
      /// <summary>[Все версии объектов]</summary>
      public static readonly string ruleAllVersions = LocalizationHolder.rm.GetString("Interfaces_528");
    }
}
