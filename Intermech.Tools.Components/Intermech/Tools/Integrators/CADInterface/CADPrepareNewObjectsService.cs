// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADPrepareNewObjectsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Client.Core;
using Intermech.Data;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Runtime;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сервиса интегратора, предназначенного для подготовки к использованию новых объектов, создаваемых внутри IPS.
/// </summary>
/// <remarks>
/// Реализация сервиса является thread safe. Так как у него нет состояния, то никакие блокировки не используются.
/// </remarks>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public class CADPrepareNewObjectsService(IIntegrator owner) : 
  PrepareNewObjectsService(owner),
  ICADPrepareNewObjectsService,
  IPrepareNewObjectsService
{
  private IFileVault fileVault;
  private ICADInterfaceService cadInterfaceService;
  private ValueBag valueToEraseArticleInfo;

  /// <summary>
  /// Возвращает или задает системный сервис файлового хранилища IPS. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public IFileVault FileVault
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.fileVault;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.fileVault = value;
      }
    }
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.FileVault == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileVault");
    this.cadInterfaceService = ServiceUtils.GetService<ICADInterfaceService>((object) this.Integrator, true);
    this.valueToEraseArticleInfo = this.CreateValuesToEraseArticleInfo();
  }

  private ValueBag CreateValuesToEraseArticleInfo()
  {
    ValueBag eraseArticleInfo = new ValueBag(5);
    eraseArticleInfo.Add((StringKey) IDCache.Default.Designation.Text, (object) string.Empty);
    eraseArticleInfo.Add((StringKey) IDCache.Default.OKPCode.Text, (object) string.Empty);
    eraseArticleInfo.Add((StringKey) IDCache.Default.Name.Text, (object) string.Empty);
    eraseArticleInfo.Add((StringKey) IDCache.Default.ImbaseKey.Text, (object) string.Empty);
    eraseArticleInfo.Add((StringKey) CADDocumentResources.EMB_ArticleExternalKey, (object) string.Empty);
    eraseArticleInfo.AcceptChanges();
    return eraseArticleInfo;
  }

  /// <summary>
  /// Позволяет обработать и настроить файлы объекта при создании по прототипу. Метод вызывается сразу после создания заготовки нового объекта.
  /// Как правило, обработка заключается в удалении из нового объекта идентифицирующих сведений, относящихся к объекту-прототипу.
  /// К таким сведениям относятся значения атрибутов "Обозначение", "Код ОКП", "Наименование" и др.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="prototypeId">Идентификатор прототипа объекта</param>
  public override void PreparePrototypedObjectFiles(long objectId, long prototypeId)
  {
    base.PreparePrototypedObjectFiles(objectId, prototypeId);
    string masterFileName = this.FileVault.DBFilesInfo.GetMasterFileName(objectId, false);
    if (string.IsNullOrEmpty(masterFileName))
      return;
    string fullName = this.FileVault.PublishTree(objectId, masterFileName, VersionsRuleSources.GetEditorRule(), (IFileArea) this.FileVault.WorkArea);
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadInterfaceService))
    {
      CADDocumentProxy cadDocument = cadApiSession.Application.OpenDocument(fullName, false);
      this.EraseArticleIdentities(objectId, cadDocument);
      if (!cadDocument.Modified || cadDocument.ReadOnly)
        return;
      cadDocument.Save();
    }
  }

  private void EraseArticleIdentities(long objectId, CADDocumentProxy cadDocument)
  {
    EncodeAttributesOptions encodeOptions = DocumentAttributesOptions.GetEncodeOptions(DBHelper.GetObjectType(objectId));
    encodeOptions.ReportErrorsOnly = false;
    if (!cadDocument.HasConfigurations)
      return;
    IAttributeCodec articleCodec = this.cadInterfaceService.GetArticleCodec(cadDocument);
    foreach (ModelConfigurationProxy allConfiguration in (IEnumerable<ModelConfigurationProxy>) cadDocument.GetAllConfigurations())
    {
      IValueBagContainer attributeContainer = this.cadInterfaceService.GetArticleAttributeContainer(allConfiguration);
      ContainerValues containerValues = articleCodec.ReadFileProperties(attributeContainer, (ICollection<StringKey>) this.valueToEraseArticleInfo.Keys);
      articleCodec.Encode(new EncodeAttributesParams(attributeContainer, (ICollection<StringKey>) this.valueToEraseArticleInfo.Keys, this.valueToEraseArticleInfo, containerValues, encodeOptions)
      {
        ContainerDisplayName = Path.GetFileName(allConfiguration.Document.FullName)
      });
      articleCodec.Formatter.Write(attributeContainer, containerValues);
    }
  }

  /// <summary>
  /// Возвращает набор значений для записи в конфигурацию 3D-модели, чтобы удалить из нее всю информацию об изделии IPS.
  /// Метод используется при создании 3D-моделей по прототипу для очистки файлов нового документа от
  /// данных документа-прототипа.
  /// </summary>
  /// <returns>Набор значений для записи в конфигурацию 3D-модели</returns>
  public ValueBag GetValuesToEraseArticleInfo()
  {
    this.RequireReadyState();
    return this.valueToEraseArticleInfo.Copy();
  }
}
