// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.UnpairedDocumentTypesCheck
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Tools.Settings;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует валидатор для свойства "Непарные типы документов" в настройках интегратора.
/// В этой настройке допускается указывать только типы объектов, обрабатываемые интегратором.
/// </summary>
internal sealed class UnpairedDocumentTypesCheck : CADSettingsCheck
{
  /// <summary>
  /// Выполняет проверку свойства интегратора с типом объекта IPS для моделей стандартных изделий.
  /// </summary>
  /// <param name="settings">Объект с настройками интегратора</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <returns>null, если проверка успешно пройдена, иначе - текст с детальным описанием проблемы</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект настроек не может быть null</exception>
  protected override string DoPerformCheck(CADSettings settings, SettingsValidatorContext context)
  {
    foreach (GlobalId<int> documentType in settings.UnpairedDocumentTypes.DocumentTypes)
    {
      if (settings.FileDocumentGroups.FindByDocumentType(documentType.Id, false) == null)
        return $"Тип документа '{documentType}', указанный в настройке 'Непарные типы документов', не является типом документов, поддерживаемых интегратором.";
    }
    return (string) null;
  }
}
