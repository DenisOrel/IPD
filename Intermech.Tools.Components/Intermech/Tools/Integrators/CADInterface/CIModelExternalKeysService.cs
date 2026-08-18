// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIModelExternalKeysService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Сервис для работы с внешними ключами изделий, описываемых 3D-моделью.
/// </summary>
public class CIModelExternalKeysService(
  CICaptureChangesDriver driver,
  CaptureChangesDriverContext driverContext) : CIBaseExternalKeysService(driver, driverContext)
{
  /// <summary>
  /// Проверяет, поддерживается ли указанное изделие механизмом внешних ключей.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <param name="modelItem">Рабочий элемент конструкторского документа</param>
  /// <returns>true, если механизм внешних ключей поддерживает указанное изделие</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  protected override bool DoHasExternalKeySupport(
    SectionEntity articleItem,
    SectionEntity modelItem)
  {
    return this.IsAssemblyOrPartModel(modelItem);
  }

  private bool IsAssemblyOrPartModel(SectionEntity modelItem)
  {
    MechanicalDocumentKind? mechanicalDocumentKind = this.Driver.TryGetMechanicalDocumentKind(modelItem);
    if (!mechanicalDocumentKind.HasValue)
      return false;
    return mechanicalDocumentKind.Value == MechanicalDocumentKind.AssemblyModel || mechanicalDocumentKind.Value == MechanicalDocumentKind.PartModel;
  }

  /// <summary>
  /// Возвращает уникальный идентификатор изделия внутри документа. Идентификатор должен быть постоянный, т.е. сохраняться при переоткрытии документа.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <returns>Уникальный идентификатор изделия внутри документа</returns>
  protected override string DoGetArticleInternalId(SectionEntity articleItem)
  {
    return (string) articleItem.Sections.Get<CIArticleData>().Configuration.Name;
  }
}
