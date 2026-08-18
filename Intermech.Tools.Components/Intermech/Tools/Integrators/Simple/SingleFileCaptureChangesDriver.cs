// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFileCaptureChangesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

/// <summary>
/// Реализует драйвер захвата изменений для простых документов, состоящих только из мастер-файла и
/// дополнительных файлов и не требующих обмена атрибутами.
/// </summary>
public class SingleFileCaptureChangesDriver : SingleFileCaptureChangesBase
{
  private readonly IServiceProvider integrator;
  private IDocumentAttributesSettingsService settingsSvc;
  private IDocumentApiService apiSvc;
  private bool apiSessionIsOpen;

  public SingleFileCaptureChangesDriver(IServiceProvider integrator)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  protected override void InitializeDriver()
  {
    base.InitializeDriver();
    this.settingsSvc = ServiceUtils.GetService<IDocumentAttributesSettingsService>((object) this.integrator, true);
    this.apiSvc = ServiceUtils.GetService<IDocumentApiService>((object) this.integrator, true);
    this.OpenApplicationApiSession();
  }

  protected override void ClearDriver()
  {
    this.CloseApplicationApiSession();
    this.apiSvc = (IDocumentApiService) null;
    this.settingsSvc = (IDocumentAttributesSettingsService) null;
    base.ClearDriver();
  }

  /// <summary>
  /// Открывает сессию доступа к API интегрируемого приложения и конфигурирует приложение для работы в паре с IPS, используя стандартные опции подключения.
  /// </summary>
  protected virtual void OpenApplicationApiSession()
  {
    this.apiSvc.OpenApiSession();
    this.apiSessionIsOpen = true;
  }

  /// <summary>
  /// Закрывает сессию доступа к API интегрируемого приложения.
  /// </summary>
  protected virtual void CloseApplicationApiSession()
  {
    if (!this.apiSessionIsOpen)
      return;
    this.apiSessionIsOpen = false;
    this.apiSvc.CloseApiSession();
  }

  protected sealed override void OpenDocument(DocumentFileData documentFile)
  {
    base.OpenDocument(documentFile);
    IOpenDocument sectionObject = this.apiSvc.OpenDocuments.OpenDocument(documentFile.DocumentFilePath);
    documentFile.CustomSections.Set((object) sectionObject, typeof (IOpenDocument));
  }

  protected override IAction CreateTypedDocumentHandler(
    SectionEntity docItem,
    object documentKind,
    int documentType)
  {
    return (IAction) new SingleFileDocumentHandler((DocumentCaptureChangesDriver) this, this.DriverContext, docItem);
  }

  protected override void SetupDocumentHandler(SectionEntity docItem, IAction documentHandler)
  {
    if (documentHandler is SingleFileDocumentHandler fileDocumentHandler)
      fileDocumentHandler.Integrator = this.integrator;
    base.SetupDocumentHandler(docItem, documentHandler);
  }

  /// <summary>
  /// <para>
  /// Позволяет определить тип для нового импортируемого документа, прочитав его из файла документа. Если тип документа не может быть
  /// определен однозначно, то метод должен вернуть все возможные типы документов. Если множество возможных типов не является
  /// ограниченным, то этот метод должен вернуть пустой список, а фактический выбор типа для документа должен быть реализован в методе
  /// <see cref="M:DetectFallbackDocumentType" />.</para>
  /// <para>
  /// Этот метод вызывается даже тогда, когда метод <see cref="M:GetDocumentTypeParameterName" /> возвращает null или пустую строку.
  /// Так сделано потому, что иногда тип документа можно определить эвристически без явного хранения имени типа в файле документа.
  /// При реализации метода также нужно учитывать, что он вызывается в самом начале анализа импортируемого документа, и его рабочий элемент практически пуст.</para>
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Список возможных типов для импортируемого документа</returns>
  protected override List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    SingleFileSettings settingsObject = (SingleFileSettings) this.settingsSvc.GetSettingsObject();
    return this.FilterDocumentTypesByExtension(docItem, new List<LocalId<int>>((IEnumerable<LocalId<int>>) settingsObject.DocumentTypes.Items));
  }

  public override bool IsDocumentTypeSupported(int documentType)
  {
    return ((SingleFileSettings) this.settingsSvc.GetSettingsObject()).DocumentTypes.Items.Exists((Predicate<GlobalId<int>>) (item => item.Id == documentType));
  }

  /// <summary>
  /// Переводит тип документа IPS в вид документа приложения, который используется для выбора обработчика документа. Каждому виду документов соответствует свой обработчик.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Идентификатор вида документа приложения</returns>
  protected sealed override object DoMapDocumentTypeToKind(int documentType) => (object) null;

  protected IServiceProvider Integrator => this.integrator;

  protected IDocumentApiService ApiService => this.apiSvc;
}
