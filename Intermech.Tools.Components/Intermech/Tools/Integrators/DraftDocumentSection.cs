// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DraftDocumentSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.IO;
using Intermech.Tools.DataExchange;
using System;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Секция для описания данных, специфических для черновиков документов.
/// </summary>
internal sealed class DraftDocumentSection
{
  /// <summary>
  /// Ссылка на свойство ExternalFilePath для использования в условиях поиске по базе данных анализатора изменений.
  /// </summary>
  public static readonly SectionPropertyReference ExternalFilePathRef = new SectionPropertyReference(typeof (DraftDocumentSection), nameof (ExternalFilePath));
  private readonly string externalFilePath;

  /// <summary>Создает объект</summary>
  /// <param name="externalFilePath">Путь к внешнему файлу черновика документа. Путь задается в абсолютной форме.</param>
  /// <exception cref="T:ArgumentException">Параметр <paramref name="externalFilePath" /> не должен быть равен null или пустой строке</exception>
  public DraftDocumentSection(string externalFilePath)
  {
    DraftDocumentSection.CheckExternalFilePathArg(externalFilePath);
    this.externalFilePath = externalFilePath;
  }

  /// <summary>
  /// Возвращает путь к внешнему файлу черновика документа. Путь задается в абсолютной форме.
  /// </summary>
  [Indexable(IndexType.Auto, true)]
  [Comparer(typeof (ServiceObjectAttribute.NewObject), new object[] {typeof (PathComparer)})]
  public string ExternalFilePath => this.externalFilePath;

  /// <summary>
  /// Находит в базе данных анализатора изменений сущность черновика документа по пути к внешнему файлу черновика документа.
  /// </summary>
  /// <param name="database">База данных анализатора</param>
  /// <param name="externalFilePath">Пут к внешнему файлу черновика документа в абсолютной форме.</param>
  /// <returns>Найденная сущность документа или null</returns>
  public static SectionEntity FindByExternalFilePath(
    CaptureChangesDatabase database,
    string externalFilePath)
  {
    if (database == null)
      throw new ArgumentNullException(nameof (database));
    DraftDocumentSection.CheckExternalFilePathArg(externalFilePath);
    return database.QueryFirst((IQueryCondition) new BinaryCondition((object) DraftDocumentSection.ExternalFilePathRef, BinaryOperator.Equal, (object) externalFilePath));
  }

  private static void CheckExternalFilePathArg(string externalFilePath)
  {
    if (string.IsNullOrEmpty(externalFilePath))
      throw new ArgumentException("Путь к внешнему файлу черновика документа не задан.", nameof (externalFilePath));
    if (!Path.IsPathRooted(externalFilePath))
      throw new ArgumentException("Путь к внешнему файлу должен быть задан в абсолютной форме.", nameof (externalFilePath));
  }
}
