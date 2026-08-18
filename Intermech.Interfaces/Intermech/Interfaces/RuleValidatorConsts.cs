
// Type: Intermech.Interfaces.RuleValidatorConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Константы для проверки правила подбора версий</summary>
    internal abstract class RuleValidatorConsts
    {
      /// <summary>
      /// Минимально допустимое количество обычных критериев подбора (без агрегатных функций)
      /// </summary>
      public const int MinStandardCriterions = 1;
      /// <summary>
      /// Минимально допустимое количество расширенных критериев подбора (с агрегатными функциями)
      /// </summary>
      public const int MinAggregateCriterions = 1;
      /// <summary>
      /// Максимально допустимое количество расширенных критериев подбора (с агрегатными функциями)
      /// </summary>
      public const int MaxAggregateCriterions = 1;
    }
}
