// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADExtendedSaveService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сервис расширенного сохранения изменений на основе CAD-интерфейса Интермех.
/// </summary>
public class CADExtendedSaveService : ExtendedSaveService<ICADSettingsService>
{
  private readonly CADCaptureChangesFactory factory;
  private CICaptureChangesDriver driver;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец сервиса</param>
  /// <param name="factory">Фабрика используемых объектов</param>
  public CADExtendedSaveService(IIntegrator owner, CADCaptureChangesFactory factory)
    : base(owner)
  {
    this.factory = factory != null ? factory : throw new ArgumentNullException(nameof (factory));
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.driver = this.factory.CreateDriver();
  }

  /// <summary>
  /// Собирает коллекцию типов документов, которые поддерживают расширенное сохранение.
  /// </summary>
  /// <returns>Коллекция идентификаторов типов документов</returns>
  protected override IList<LocalId<int>> CollectSupportedDocumentTypes()
  {
    IList<LocalId<int>> collection = base.CollectSupportedDocumentTypes();
    CADSettings cadSettings = this.SettingsService.GetCADSettings();
    collection.AddRange<LocalId<int>>((IEnumerable<LocalId<int>>) cadSettings.FileDocumentGroups.FindByName("Assembly", true).DocumentTypes);
    collection.AddRange<LocalId<int>>((IEnumerable<LocalId<int>>) cadSettings.FileDocumentGroups.FindByName("Part", true).DocumentTypes);
    collection.AddRange<LocalId<int>>((IEnumerable<LocalId<int>>) cadSettings.FileDocumentGroups.FindByName("AssemblyDrawing", true).DocumentTypes);
    collection.AddRange<LocalId<int>>((IEnumerable<LocalId<int>>) cadSettings.FileDocumentGroups.FindByName("PartDrawing", true).DocumentTypes);
    collection.Add((LocalId<int>) cadSettings.StandardPartType);
    return collection;
  }

  protected override ICaptureChangesDriver GetCaptureChangesDriver()
  {
    return (ICaptureChangesDriver) this.driver;
  }

  protected override void SetCaptureChangesParameters(long objectId, ExtendedSaveOptions options)
  {
    base.SetCaptureChangesParameters(objectId, options);
    this.driver.SaveChangesMode = options.Mode;
    this.driver.UpdateArticles = this.CalculateUpdateArticlesParameter(objectId, options);
    this.driver.RecalculateMass = options.RecalculateMass;
    this.captureManager.WorkAreaPolicy = options.WorkAreaPolicy;
  }

  protected override void ResetCaptureChangesParameters()
  {
    base.ResetCaptureChangesParameters();
    this.captureManager.WorkAreaPolicy = (IReplaceFilePolicy) null;
  }

  /// <summary>
  /// Вызывается после успешного завершения команды "Расширенное сохранение" и используется для запуска связанных процессов, которые не являются
  /// частью команды.
  /// </summary>
  /// <param name="result">Результаты захвата изменений</param>
  protected override void OnPostProcessCaptureChanges(CaptureChangesResult result)
  {
    base.OnPostProcessCaptureChanges(result);
    this.UpdateJTDocuments(result);
  }

  private void UpdateJTDocuments(CaptureChangesResult result)
  {
    List<Tuple<SectionEntity, long, int>> jtSourceDocuments = this.FindJTSourceDocuments(result.Database);
    if (jtSourceDocuments.Count == 0)
      return;
    foreach (Tuple<SectionEntity, long, int> tuple in jtSourceDocuments)
    {
      using (UIReport.CreateScope())
      {
        UIReportBuilder uiReportBuilder = new UIReportBuilder();
        uiReportBuilder.ReportStart($"Выполняется обновление JT-представлений для '{DisplaySection.GetQualifiedName(tuple.Item1)}'");
        try
        {
          MakeJTDocumentsAction jtDocumentsAction = new MakeJTDocumentsAction(tuple.Item2, tuple.Item3);
          jtDocumentsAction.Perform();
          foreach (Exception error in jtDocumentsAction.Errors)
            UIReport.ReportEvent(error.Message, TraceLevel.Warning);
          uiReportBuilder.ReportSuccess();
        }
        catch (Exception ex)
        {
          uiReportBuilder.ReportFail(ex);
        }
      }
    }
  }

  private List<Tuple<SectionEntity, long, int>> FindJTSourceDocuments(CaptureChangesDatabase db)
  {
    CompoundSetCondition condition = new CompoundSetCondition(CompoundSetOperator.Intersection, new IQueryCondition[2]
    {
      (IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (ObjectSection)),
      (IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (AttributesSection))
    });
    EntitySet entitySet = db.Query((IQueryCondition) condition);
    List<Tuple<SectionEntity, long, int>> jtSourceDocuments = new List<Tuple<SectionEntity, long, int>>(entitySet.Count);
    foreach (SectionEntity dbItem in (EnumerableAdapter<IEntity, SectionEntity>) new SectionEntityEnumAdapter(entitySet))
    {
      AttributesSection attributesSection = dbItem.Sections.Get<AttributesSection>();
      if (attributesSection.DatabaseSet != null && attributesSection.DatabaseSet.Read<bool>((StringKey) IDCache.Default.JTSourceDocumentMarker.Text, false))
      {
        ObjectActionsSection objectActionsSection = dbItem.Sections.Get<ObjectActionsSection>((ObjectActionsSection) null);
        if (objectActionsSection != null && (objectActionsSection.ObjectActions.ServerActions.Count != 0 || objectActionsSection.RelationActions.ServerActions.Count != 0))
          jtSourceDocuments.Add(Tuple.Create<SectionEntity, long, int>(dbItem, ObjectSection.GetObjectId(dbItem), ObjectSection.GetObjectType(dbItem)));
      }
    }
    return jtSourceDocuments;
  }
}
