// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIArticleFilesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сервис для работы с дополнительными файлами документа, относящимися к конкретному изделию.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="driver">Драйвер захвата изменений</param>
/// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
/// <exception cref="T:ArgumentNullException">driver or driverContext</exception>
internal sealed class CIArticleFilesService(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : MechanicalDriverService(driver, driverContext), IArticleFilesService
{
  /// <summary>
  /// Возвращает абсолютный путь к дополнительному файлу документа, который описывает указанное изделие. Открытие этого файла в приложении позволяет напрямую редактировать определение изделия.
  /// Если данная возможность не поддерживается приложением, то метод может вернуть null или пустую строку.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <returns>Абсолютный путь к дополнительному файлу документа или null</returns>
  /// <exception cref="T:System.ArgumentNullException">articleItem</exception>
  public string FindArticleMainFile(SectionEntity articleItem)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    return articleItem.Sections.Get<CIArticleData>().Configuration.FullPath;
  }
}
