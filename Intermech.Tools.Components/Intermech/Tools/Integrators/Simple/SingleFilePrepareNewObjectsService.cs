// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFilePrepareNewObjectsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Runtime;
using Intermech.Tools.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

/// <summary>
/// Реализует сервиса интегратора, предназначенного для подготовки к использованию новых объектов, создаваемых внутри IPS.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public class SingleFilePrepareNewObjectsService(IIntegrator owner) : PrepareNewObjectsService(owner)
{
  private IApplicationFileTypes fileTypeService;

  /// <summary>
  /// Возвращает или задает ссылку на сервис типов файлов интегратора. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public IApplicationFileTypes FileTypeService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.fileTypeService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.fileTypeService = value;
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
    if (this.FileTypeService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileTypeService");
  }

  /// <summary>
  /// Позволяет обработать и настроить новый объект (а также его файловую копию, если это документ) сразу после создания.
  /// Данный метод вызывается для всех объектов, обрабатываемых интегратором.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  public override void PrepareNewObject(long objectId)
  {
    base.PrepareNewObject(objectId);
    DBObjectState objectState = ClientContext.FileVault.DBObjectsInfo.GetObjectState(objectId, false);
    if (objectState == null || !objectState.IsEditableState)
      return;
    string masterFileName = ClientContext.FileVault.DBFilesInfo.GetMasterFileName(objectId, false);
    if (masterFileName == null || !this.fileTypeService.IsApplicationFile(masterFileName))
      return;
    AttributeValues[] attributeValues = DBAttributeHelper.ToAttributeValues(new ValueRecord((StringKey) IDCache.Default.OwnedByIntegrator.Text, (object) true));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(objectId).SetAttributesValues(attributeValues);
  }
}
