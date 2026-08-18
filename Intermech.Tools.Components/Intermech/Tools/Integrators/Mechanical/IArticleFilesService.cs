// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IArticleFilesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Необязательный сервис для работы с дополнительными файлами документа, относящимися к конкретному изделию.
/// </summary>
public interface IArticleFilesService
{
  /// <summary>
  /// Возвращает абсолютный путь к дополнительному файлу документа, который описывает указанное изделие. Открытие этого файла в приложении позволяет напрямую редактировать определение изделия.
  /// Если данная возможность не поддерживается приложением, то метод может вернуть null или пустую строку.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <returns>Абсолютный путь к дополнительному файлу документа или null</returns>
  /// <exception cref="T:System.ArgumentNullException">articleItem</exception>
  string FindArticleMainFile(SectionEntity articleItem);
}
