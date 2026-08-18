// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileOnlyDocumentHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Localization;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует общий для всех интеграторов обработчик вспомогательных документов.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="driver">Стратегия анализа изменений</param>
/// <param name="ctx">Контекст обработки</param>
/// <param name="docItem">Рабочий элемент для обрабатываемого документа</param>
public class FileOnlyDocumentHandler(
  DocumentCaptureChangesDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity docItem) : DocumentHandlerBase(driver, ctx, docItem)
{
  protected override void ProcessDependencies()
  {
  }

  /// <summary>Читает значения свойств из файла документа.</summary>
  /// <returns>Контейнер со значениями свойств. Если у файла нет свойств, либо нет соответствующего API, то метод должен вернуть пустой контейнер</returns>
  protected override ContainerValues ReadFileProperties()
  {
    return new ContainerValues(new ValueBag(), false);
  }

  /// <summary>
  /// Записывает измененные значения свойств в файл документа. Этот метод вызывается только при наличии изменений в свойствах.
  /// Если поддерживается только чтение свойств, то этот метод должен сбросить исключение.
  /// </summary>
  /// <param name="fileProperties">Контейнер со значениями свойств</param>
  /// <returns>true, если запись в файл была произведена</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на контейнер не может быть null</exception>
  /// <exception cref="T:System.NotSupportedException">Запись свойств в файл документа не поддерживается</exception>
  protected override bool WriteFileProperties(ContainerValues fileProperties)
  {
    throw new NotSupportedException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_461"), (object) this.DocumentFiles.MasterFile));
  }

  /// <summary>
  /// Выполняет декодирование значений атрибутов документа из свойств файла.
  /// </summary>
  /// <param name="fileProperties">Контейнер со свойствами файла</param>
  /// <returns>Контейнер с значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на контейнер со свойствами файла не может быть null</exception>
  protected override ValueBag DecodeDocumentAttributes(ContainerValues fileProperties)
  {
    return new ValueBag();
  }

  /// <summary>
  /// Выполняет обратное кодирование значений атрибутов документа в значения свойств файла. Если поддерживается
  /// только чтение свойств, но не запись, то этот метод может не выполнять кодирование. Исключение при этом сбрасываться не должно.
  /// </summary>
  /// <param name="attributeKeys">Список имен кодируемых атрибутов</param>
  /// <param name="attributes">Контейнер с значениями атрибутов</param>
  /// <param name="fileProperties">Контейнер со свойствами файла</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на контейнеры не могут быть null</exception>
  protected override void EncodeDocumentAttributes(
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
  }

  /// <summary>
  /// Корректирует значения атрибутов, прочитанные из файла документа, перед переносом значений атрибутов в объект документа.
  /// </summary>
  protected override void CorrectAttributes()
  {
    base.CorrectAttributes();
    new FillEmptyCaptionHandler(this.DocumentEntity).Perform();
  }

  /// <summary>
  /// Возвращает список ключей атрибутов, значения которых должны быть перенесены из файла в объект документа IPS.
  /// Как правило, этот список задается в настройках интегратора.
  /// </summary>
  /// <returns>Список ключей атрибутов</returns>
  protected override ICollection<StringKey> GetTransferableAttributes()
  {
    ICollection<StringKey> transferableAttributes = base.GetTransferableAttributes();
    transferableAttributes.AddRange<StringKey>((IEnumerable<StringKey>) this.DocumentAttributes.WorkingSet.GetChangedItemsKeys());
    return transferableAttributes;
  }

  /// <summary>
  /// Сохраняет измененные файлы документа на диск. Этот метод используется для сохранения на диск любых изменений в файле документа, а не только
  /// сделанных интегратором. Например, метод будет вызван, если пользователь не сохранил документ в приложении-редакторе перед вызовом команды IPS.
  /// </summary>
  protected override IEnumerable<CooperativeState> SaveModifiedDocumentFiles()
  {
    yield break;
  }
}
