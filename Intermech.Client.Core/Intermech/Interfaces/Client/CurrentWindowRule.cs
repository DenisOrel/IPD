
// Type: Intermech.Interfaces.Client.CurrentWindowRule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;


namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует источник правил подбора версий, предоставляющий правила из текущего окна IPS.
/// </summary>
public sealed class CurrentWindowRule : IVersionsRuleSource
{
  /// <summary>
  /// Возвращает правило подбора версий, установленного в текущем окне IPS.
  /// </summary>
  /// <returns>Описание правила подбора версий</returns>
  /// <exception cref="T:Intermech.Interfaces.Client.RuleNotAvailableException">В IPS нет открытых окон, либо текущее окно не поддерживает работу с правилами подбора</exception>
  /// <exception cref="T:System.Exception">Возникла ошибка при получении правила</exception>
  public VersionsRulePackage GetRule()
  {
    IFiltrationService service = ServiceUtils.GetService<IFiltrationService>((object) ServicesManager.ServiceContainer, true);
    string ownerId = service != null ? service.FiltrationServiceOwnerID : throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1568"), (object) typeof (IFiltrationService)));
    if (string.IsNullOrEmpty(ownerId))
      throw new RuleNotAvailableException(LocalizationHolder.rm.GetString("Client.Core_1569"));
    if (!service.RuleValid || !service.RuleCompatible || service.RuleErrorCode != CurrentRuleErrors.Valid && service.RuleErrorCode != CurrentRuleErrors.FilteringIsOff)
      throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1570")));
    return new VersionsRulePackage(ownerId);
  }
}
