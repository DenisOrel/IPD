// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ImbaseObjectArticleHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public class ImbaseObjectArticleHandler(
  MechanicalDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity articleItem) : ArticleHandlerBase(driver, ctx, articleItem)
{
  private ObjectSection articleObj;
  private AttributesSection articleAttrs;

  protected override IEnumerable<CooperativeState> Coroutine()
  {
    this.Initialize();
    this.BindToDBObject();
    this.EnsureDBObjectExists();
    this.ReadDBObjectData();
    yield return this.Call(new Func<IEnumerable<CooperativeState>>(this.ProcessRelations));
    yield return this.Call(new Func<IEnumerable<CooperativeState>>(this.WriteChangesToDisk));
    yield return this.Wait((IWaitObject) this.MechanicalDriver.SchedulerStages.UIStage);
    this.MechanicalDriver.Operations.Db.EmitUIActions(this.ctx, this.articleItem);
    EventHandler<ArticleEntityEventArgs> finished = this.Finished;
    if (finished != null)
      finished((object) this, new ArticleEntityEventArgs(this.articleItem));
  }

  protected override object GetUIReportOperationId() => (object) this.articleItem;

  protected virtual Tuple<long, int, string> FindOrCreateImbaseObject(ValueBag attributes)
  {
    return ImbaseHelper.FindOrCreateImbaseObject(attributes);
  }

  private void Initialize()
  {
    this.articleObj = this.articleItem.Sections.Get<ObjectSection>();
    this.articleAttrs = this.articleItem.Sections.Get<AttributesSection>();
  }

  private void BindToDBObject()
  {
    IArticleLocatorService articleLocatorService = this.MechanicalDriver.TryGetArticleLocatorService(this.articleItem);
    if (articleLocatorService == null)
      return;
    ArticleBinder.BindArticle(this.ctx, this.articleItem, articleLocatorService.CreateImbaseObjectLocator(this.articleItem), false);
  }

  private void EnsureDBObjectExists()
  {
    if (!this.articleObj.NewObject)
      return;
    this.RecreateImbaseObjects();
  }

  private void RecreateImbaseObjects()
  {
    Tuple<long, int, string> createImbaseObject;
    try
    {
      createImbaseObject = this.FindOrCreateImbaseObject(this.articleAttrs.WorkingSet);
    }
    catch (Exception ex)
    {
      throw new FaultException($"Для конфигурации '{DisplaySection.GetDisplayName(this.articleItem)}' не удалось найти в базе данных соответствующий ей объект Imbase. {ex.Message}", ex);
    }
    this.articleObj.ObjectId = createImbaseObject != null ? createImbaseObject.Item1 : throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_499"), (object) DisplaySection.GetDisplayName(this.articleItem)));
    this.articleObj.ObjectType = createImbaseObject.Item2;
    this.articleAttrs.WorkingSet.Update((StringKey) IDCache.Default.ImbaseKey.Text, (object) createImbaseObject.Item3);
    this.articleAttrs.WorkingSet.SetFlag((StringKey) IDCache.Default.ImbaseKey.Text, NamedFlags.ThrowSetException);
  }

  private void ReadDBObjectData()
  {
  }

  private IEnumerable<CooperativeState> ProcessRelations()
  {
    yield return this.Wait((IWaitObject) this.MechanicalDriver.SchedulerStages.RelationsStage);
    IArticleDocumentationService documentationService = this.MechanicalDriver.TryGetArticleDocumentationService(this.articleItem);
    if (documentationService != null)
      new SyncArticleDocumentationAction((DocumentCaptureChangesDriver) this.MechanicalDriver, this.ctx, documentationService, this.articleItem).Perform();
  }

  private IEnumerable<CooperativeState> WriteChangesToDisk()
  {
    yield return this.Wait((IWaitObject) this.MechanicalDriver.SchedulerStages.DiskWritesStage);
    this.EncodeChangedWorkingAttributes();
    this.WriteChangedFileProperties();
  }

  private void EncodeChangedWorkingAttributes()
  {
    if (!this.articleObj.NewObject)
      return;
    this.articleApiService.EncodeArticleAttributes(this.articleItem, (ICollection<StringKey>) new List<StringKey>()
    {
      (StringKey) IDCache.Default.ImbaseKey.Text
    }, this.articleAttrs.WorkingSet, this.articleAttrs.EmbeddedSet);
  }

  /// <summary>Событие завершения выполнения обработчика</summary>
  public event EventHandler<ArticleEntityEventArgs> Finished;
}
