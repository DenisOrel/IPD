// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.DocumentWithArticlesHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public class DocumentWithArticlesHandler : DocumentHandler
{
  private bool enableUnusedArticlesProcessing;

  /// <summary>Создает объект.</summary>
  /// <param name="driver">Стратегия анализа изменений</param>
  /// <param name="ctx">Рабочий контекст анализатора</param>
  /// <param name="docItem">Объект документа в базе данных анализатора</param>
  public DocumentWithArticlesHandler(
    MechanicalDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity docItem)
    : base(driver, ctx, docItem)
  {
    this.enableUnusedArticlesProcessing = true;
  }

  /// <summary>
  /// Включает и выключает управление связями с исполнениями изделия, которые больше не описаны в документе.
  /// Такие изделия отвязываются и от документа, и от других исполнений изделия.
  /// По умолчанию, значение свойства установлено в true.
  /// </summary>
  /// <remarks>
  /// Обработка неописанных исполнений изделия может быть отключена в случаях, когда конструкторский документ не может служить
  /// источником информации об исполнениях изделия.
  /// </remarks>
  public bool EnableUnusedArticlesProcessing
  {
    [DebuggerStepThrough] get => this.enableUnusedArticlesProcessing;
    [DebuggerStepThrough] set => this.enableUnusedArticlesProcessing = value;
  }

  /// <summary>
  /// Позволяет обработать другие объекты, связанные с документом.
  /// </summary>
  protected override void ProcessDerivedObjects()
  {
    base.ProcessDerivedObjects();
    if (this.Driver.UpdateArticles)
      this.ProcessDerivedArticles();
    if (this.Driver.SidecarObjectsExtensions.Count == 0)
      return;
    SidecarObjectsGeneratorAction.GetOrCreate(this.Driver, this.DriverContext).AddSourceDocument(this.DocumentEntity);
  }

  private void ProcessDerivedArticles()
  {
    ICollection<InitialArticleData> initialArticleDatas = this.DocumentApiService.ReadArticles(this.DocumentEntity);
    if (CollectionUtils.TryGetFirstItem<InitialArticleData>((IEnumerable<InitialArticleData>) initialArticleDatas) == null)
      return;
    new ArticleGenerator(this.Driver).MakeArticleEntities(this.DriverContext, (IEnumerable<InitialArticleData>) initialArticleDatas, this.DocumentEntity);
  }

  protected override void ProcessRelations()
  {
    base.ProcessRelations();
    if (!this.Driver.UpdateArticles || !this.enableUnusedArticlesProcessing)
      return;
    this.DetachUnusedArticleDocumentation();
  }

  private void DetachUnusedArticleDocumentation()
  {
    foreach (DataRow row in (InternalDataCollectionBase) DBDocumentHelper.FindDocumentArticles(this.DocumentObject.ObjectId, VersionsRuleSources.GetEditorRule(), true).Rows)
    {
      Guid guid = new Guid(Convert.ToString(row[0]));
      long int64 = Convert.ToInt64(row[1]);
      int int32 = Convert.ToInt32(row[2]);
      if (ObjectSection.FindByObjectId(this.DriverContext.Database, int64, true) == null)
      {
        if ((Convert.IsDBNull(row[3]) ? 0L : Convert.ToInt64(row[3])) == Math.Abs(this.DocumentObject.ObjectId))
          this.EmitActionsToDetachUnusedArticleDocumentation(this.DriverContext.Database.AddReferencedDBObject(int64, int32));
        else if (UIReport.Enabled)
          UIReport.ReportEvent($"Невозможно удалить связь между изделием '{DBHelper.GetObjectCaption(int64)}' и документом '{DisplaySection.GetDisplayName(this.DocumentEntity)}' так как на ней отсутствует конкретизация подбора версий. Для исправления ошибки откройте дерево версий для указанного изделия, и для каждой версии изделия конкретизируйте ее связь с указанным документом.", TraceLevel.Warning);
      }
    }
  }

  private void EmitActionsToDetachUnusedArticleDocumentation(SectionEntity articleItem)
  {
    DBObjectEntityRef dbObjectEntityRef = new DBObjectEntityRef(articleItem);
    ObjectActionsSection objectActionsSection1 = articleItem.Sections.Get<ObjectActionsSection>();
    if (this.Driver.Operations.Checkout.RequireCheckoutOnObjectAttribute(ObjectSection.GetObjectType(articleItem), (StringKey) IDCache.Default.InstanceGroupId.Text))
    {
      ObjectActionsSection objectActionsSection2 = objectActionsSection1;
      objectActionsSection2.RequireCheckout = ((objectActionsSection2.RequireCheckout ? 1 : 0) | 1) != 0;
    }
    objectActionsSection1.ObjectActions.ServerActions.Add((IAction) new WriteObjectAttributesAction((IDBObjectRef) dbObjectEntityRef, new AttributeValues[1]
    {
      new AttributeValues(IDCache.Default.InstanceGroupId.Id, (object) null)
      {
        AttributeName = IDCache.Default.InstanceGroupId.Text,
        IsNew = true,
        ThrowSetException = true
      }
    }));
    objectActionsSection1.ObjectActions.ClientActions.Add((IAction) new FireObjectModifiedAction((IDBObjectRef) dbObjectEntityRef, this.DriverContext.UINotifications));
    foreach (Tuple<Guid, long> articleDocument in DBDocumentHelper.FindArticleDocuments(ObjectSection.GetObjectId(articleItem), true, true, VersionsRuleSources.GetEditorRule()))
    {
      Guid relationGuid = articleDocument.Item1;
      long objectId = articleDocument.Item2;
      if (this.Driver.Operations.Checkout.RequireCheckoutOnRelationModification(IDCache.Default.ArticleToDocumentTree.Id, articleItem, (IDBObjectRef) new DirectDBObjectRef(objectId)))
      {
        ObjectActionsSection objectActionsSection3 = objectActionsSection1;
        objectActionsSection3.RequireCheckout = ((objectActionsSection3.RequireCheckout ? 1 : 0) | 1) != 0;
      }
      DeleteRelationAction relationRef = new DeleteRelationAction((IDBObjectRef) dbObjectEntityRef, relationGuid, IDCache.Default.ArticleToDocumentTree.Id);
      objectActionsSection1.RelationActions.ServerActions.Add((IAction) relationRef);
      objectActionsSection1.RelationActions.ClientActions.Add((IAction) new FireRelationRemovedAction((IDBRelationRef) relationRef, this.DriverContext.UINotifications));
    }
  }
}
