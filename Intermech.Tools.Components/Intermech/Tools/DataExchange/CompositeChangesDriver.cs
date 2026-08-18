// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CompositeChangesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.UI;
using System;
using System.IO;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Позволяет реализовать захват изменений в документах, используя несколько схем обработки документов.
/// Для выбора схемы обработки используется тип головного документа.
/// </summary>
public abstract class CompositeChangesDriver : CaptureChangesDriver
{
  private ICaptureChangesDriver activeDriver;

  protected override void ClearDriver()
  {
    base.ClearDriver();
    if (this.activeDriver == null)
      return;
    this.activeDriver.EndAction();
    this.activeDriver = (ICaptureChangesDriver) null;
  }

  protected override void DoInvoke(
    CaptureChangesContext sharedCtx,
    IPercentageProgressSink progressSink)
  {
    SectionEntity sectionEntity = sharedCtx.Database.QueryFirst((IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (RootItemSection)));
    ObjectSection objectSection = sectionEntity != null ? sectionEntity.Sections.Get<ObjectSection>() : throw new InvalidOperationException();
    FilesSection filesSection = sectionEntity.Sections.Get<FilesSection>();
    if (objectSection.NewObject)
    {
      string str = this.ValidateRootFile(filesSection.MasterFile);
      this.activeDriver = !string.IsNullOrEmpty(str) && Path.IsPathRooted(str) && File.Exists(str) ? this.SelectDriver(str) : throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_409"));
    }
    else
    {
      this.ValidateRootFile(filesSection.MasterFile, objectSection.ObjectId);
      LocalId<int> objectTypeGid = (LocalId<int>) DBHelper.CreateObjectTypeGID(objectSection.ObjectType);
      this.activeDriver = this.SelectDriver(objectSection.ObjectId, objectTypeGid);
    }
    this.activeDriver.BeginAction();
    this.activeDriver.Invoke(sharedCtx, progressSink);
  }

  /// <summary>
  /// Удаляет данные драйвера из базы данных контекста. Это требуется, чтобы базу данных можно было безопасно вернуть в качестве результата выполнения.
  /// Этот метод вызывается даже в случае, когда процесс обработки прерывается по исключительной ситуации.
  /// </summary>
  /// <param name="database">База данных контекста</param>
  protected override void DoDetachDatabase(CaptureChangesDatabase database)
  {
    base.DoDetachDatabase(database);
    if (!this.activeDriver.Active)
      return;
    this.activeDriver.DetachDatabase(database);
  }

  /// <summary>
  /// Вызывается в самом конце после успешного завершения процесса.
  /// Метод может использоваться драйвером для извлечения полезных сведений из рабочего контекста.
  /// </summary>
  protected override void DoPostprocess()
  {
    base.DoPostprocess();
    if (!this.activeDriver.Active)
      return;
    this.activeDriver.Postprocess();
  }

  /// <summary>
  /// Реализует проверку стартового объекта и его мастер-файла перед началом сохранения изменений.
  /// Метод используется для отсеивания не поддерживаемых типов файлов. Если файл не подходит, то
  /// метод должен сбросить исключение типа Intermech.FaultException.
  /// </summary>
  /// <param name="rootFilePath">Полный путь к мастер-файлу стартового объекта</param>
  /// <param name="rootObjectId">Идентификатор версии стартового объекта</param>
  /// <exception cref="T:Intermech.FaultException">Неподходящий тип файла или его содержимое</exception>
  protected abstract void ValidateRootFile(string rootFilePath, long rootObjectId);

  /// <summary>
  /// Реализует проверку стартового файла перед началом импорта. Метод используется для отсеивания не поддерживаемых типов файлов.
  /// Если файл не подходит, то метод должен сбросить исключение типа Intermech.FaultException.
  /// Метод может также заменить стартовый файл, если это необходимо.
  /// </summary>
  /// <param name="rootFilePath">Полный путь к стартовому файлу</param>
  /// <returns>Исправленный путь к стартовому файлу. Как правило, совпадает со значением rootFilePath</returns>
  /// <exception cref="T:Intermech.FaultException">Неподходящий тип файла или его содержимое</exception>
  protected abstract string ValidateRootFile(string rootFilePath);

  /// <summary>
  /// Реализует выбор драйвера для обработки нового документа.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Выбранная схема обработки документов</returns>
  protected abstract ICaptureChangesDriver SelectDriver(long documentId, LocalId<int> documentType);

  /// <summary>
  /// Реализует выбор драйвера для обработки нового документа.
  /// </summary>
  /// <param name="fullPath">Абсолютный путь к файлу документа</param>
  /// <returns>Выбранная схема обработки документов</returns>
  protected abstract ICaptureChangesDriver SelectDriver(string fullPath);
}
