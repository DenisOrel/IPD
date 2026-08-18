
// Type: Intermech.Interfaces.VersionsRuleType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Тип правила подбора версий:
    /// vrtStandardRule - правило стандартное, создаётся пользователем,
    /// vrtLatestVersionsRule - правило, возвращающее последние версии объектов,
    /// vrtAllVersionsRule - правило, возвращающее все версии объектов.
    /// </summary>
    public enum VersionsRuleType
    {
      /// <summary>Правило системное. Создаётся сервисом-кэшем правил.</summary>
      vrtSystemRule = -3, // 0xFFFFFFFD
      /// <summary>
      /// Правило, возвращающее все версии объектов. Создаётся сервисом-кэшем правил.
      /// </summary>
      vrtAllVersionsRule = -2, // 0xFFFFFFFE
      /// <summary>
      /// Правило, возвращающее последние версии объектов. Создаётся сервисом-кэшем правил.
      /// </summary>
      vrtLatestVersionsRule = -1, // 0xFFFFFFFF
      /// <summary>
      /// Правило стандартное. Создаётся пользователем системы в редакторе правил подбора версий.
      /// </summary>
      vrtStandardRule = 0,
    }
}
