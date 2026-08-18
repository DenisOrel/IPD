// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.StandardPartHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class StandardPartHandler(
  MechanicalDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity docItem) : FileOnlyDocumentHandler((DocumentCaptureChangesDriver) driver, ctx, docItem)
{
  private IDocumentCADApiService docApiService;

  private MechanicalDriver Driver
  {
    [DebuggerStepThrough] get => (MechanicalDriver) base.Driver;
  }

  /// <summary>
  /// Возвращает фасад API документов со стороны интегрируемого приложения.
  /// Значение свойства доступно только после инициализации текущего объекта.
  /// </summary>
  private IDocumentCADApiService DocumentApiService
  {
    [DebuggerStepThrough] get => this.docApiService;
  }

  /// <summary>Выполняет инициализацию обработчика.</summary>
  protected override void InitializeHandler()
  {
    base.InitializeHandler();
    this.docApiService = this.Driver.GetDocumentApiService(this.DocumentEntity);
  }

  /// <summary>
  /// Сохраняет измененные файлы документа на диск. Этот метод используется для сохранения на диск любых изменений в файле документа, а не только
  /// сделанных интегратором. Например, метод будет вызван, если пользователь не сохранил документ в приложении-редакторе перед вызовом команды IPS.
  /// </summary>
  protected override IEnumerable<CooperativeState> SaveModifiedDocumentFiles()
  {
    TopDownSaveFilesAction downSaveFilesAction = TopDownSaveFilesAction.GetOrCreate(this.DriverContext, true);
    downSaveFilesAction.RegisterDocument(this.DocumentEntity, (IAction) new MethodAction(new Action(this.SaveModifiedDocumentCallback)));
    yield return this.Wait(downSaveFilesAction.Complete);
  }

  private void SaveModifiedDocumentCallback()
  {
    this.DocumentApiService.SaveDocumentFile(this.DocumentEntity);
  }

  protected override void UpdateDBOnlyAttributes()
  {
    base.UpdateDBOnlyAttributes();
    this.RemoveDraftDocumentSigns();
  }

  private void RemoveDraftDocumentSigns()
  {
    if (!this.DocumentObject.NewObject || !this.DocumentEntity.Sections.Contains<DraftDocumentConvertationSection>())
      return;
    ValueBag databaseSet = this.DocumentAttributes.DatabaseSet;
    if (!databaseSet.CanUpdate((StringKey) IDCache.Default.Name.Text, typeof (string), false))
      return;
    string str = databaseSet.Read<string>((StringKey) IDCache.Default.Name.Text, string.Empty);
    if (string.IsNullOrEmpty(str) || !str.EndsWith("(заготовка для файла)"))
      return;
    string newValue = str.Remove(str.Length - "(заготовка для файла)".Length, "(заготовка для файла)".Length).TrimEnd();
    databaseSet.Update((StringKey) IDCache.Default.Name.Text, (object) newValue);
  }
}
