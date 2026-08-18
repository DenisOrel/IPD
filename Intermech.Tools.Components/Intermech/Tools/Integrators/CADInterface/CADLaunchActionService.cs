// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADLaunchActionService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Client.Core;
using Intermech.ControlFlow;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Runtime;
using Intermech.Tools.Data;
using Intermech.Tools.LaunchActions;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сервис интегратора, отвечающий за открытие документов для редактирования или просмотра. Для извлеченая файлов используется рабочая область пользователя.
/// Класс является thread-safe.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец сервиса</param>
public class CADLaunchActionService(IIntegrator owner) : Intermech.Tools.Integrators.LaunchActionService(owner)
{
  private ICADInterfaceService cadApiService;

  /// <summary>
  /// Возвращает или задает ссылку на сервис для доступа к API приложения. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public ICADInterfaceService ApiService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.cadApiService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.cadApiService = value;
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
    if (this.ApiService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ApiService");
  }

  /// <summary>Открывает файл документа в приложении.</summary>
  /// <param name="launchParams">Параметры команды открытия документа</param>
  protected override void OpenDocumentFileFromDisk(LaunchParams launchParams)
  {
    if (launchParams.LaunchContext.Get<bool>("OpenArticleConfiguration"))
      this.OpenArticleConfiguration(launchParams);
    else
      this.OpenDefaultFile(launchParams);
  }

  private void OpenDefaultFile(LaunchParams launchParams)
  {
    using (new DynamicScope())
    {
      IntegratorVars.ConserveAppResources.Declare(false);
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
      {
        CADSystemProxy application = cadApiSession.Application;
        CADDocumentProxy cadDocumentProxy = application.OpenDocument(launchParams.ResultFilePath, true);
        application.SwitchToApp();
        cadDocumentProxy.Activate();
      }
    }
  }

  private void OpenArticleConfiguration(LaunchParams launchParams)
  {
    if (launchParams.LaunchContext.Get<long>("ArticleId", 0L) == 0L)
      throw new InvalidOperationException($"Не указан идентификатор версии изделия, которое выпускается по документу '{DBHelper.GetObjectCaption(launchParams.ObjectId)}'.");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(launchParams.OriginalObjectId, launchParams.ObjectId, IDCache.Default.ArticleToDocumentTree.Id, true);
      IDBAttribute attributeByGuid1 = relation.GetAttributeByGuid(IDCache.Default.CADConfigurationFile.Guid);
      if (attributeByGuid1 != null)
      {
        string fullName = Path.Combine(ClientContext.FileVault.WorkArea.AreaPath, attributeByGuid1.AsString);
        using (new DynamicScope())
        {
          IntegratorVars.ConserveAppResources.Declare(false);
          using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
          {
            CADSystemProxy application = cadApiSession.Application;
            CADDocumentProxy cadDocumentProxy = application.OpenDocument(fullName, true);
            application.SwitchToApp();
            cadDocumentProxy.Activate();
          }
        }
      }
      else
      {
        IDBAttribute attributeByGuid2 = relation.GetAttributeByGuid(IDCache.Default.CADConfigurationName.Guid);
        if (attributeByGuid2 != null)
        {
          string fullName = Path.Combine(ClientContext.FileVault.WorkArea.AreaPath, ClientContext.FileVault.DBFilesInfo.GetMasterFileName(launchParams.ObjectId, true));
          using (new DynamicScope())
          {
            IntegratorVars.ConserveAppResources.Declare(false);
            using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
            {
              CADSystemProxy application = cadApiSession.Application;
              CADDocumentProxy cadDocumentProxy = application.OpenDocument(fullName, true);
              application.SwitchToApp();
              cadDocumentProxy.GetConfiguration(attributeByGuid2.AsString, true);
            }
          }
        }
        else
          this.OpenDefaultFile(launchParams);
      }
    }
  }
}
