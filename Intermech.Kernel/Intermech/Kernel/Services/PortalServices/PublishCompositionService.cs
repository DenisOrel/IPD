// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.PublishCompositionService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

public class PublishCompositionService : 
  LongLifeObject,
  IPublishCompositionService,
  ICustomCompositionService
{
  private readonly List<int> _includeObjectsAlwaysObjectTypeIDs = new List<int>();
  private Dictionary<Guid, SelectPublishCompositionThread> _selectThreads = new Dictionary<Guid, SelectPublishCompositionThread>();

  public void Select(
    Guid userSessionGuid,
    Guid selectGUID,
    List<long> rootObjectIDs,
    ExtendedPublishOptions options,
    PublishType publishType,
    bool throwException)
  {
    if (rootObjectIDs == null || rootObjectIDs.Count == 0)
      throw new ArgumentNullException(nameof (rootObjectIDs));
    if (options == null)
      throw new ArgumentNullException(nameof (options));
    this.CheckOptions(userSessionGuid, options);
    lock (this)
    {
      SelectPublishCompositionThread compositionThread1 = new SelectPublishCompositionThread(selectGUID, userSessionGuid, rootObjectIDs, publishType, options, throwException);
      SelectPublishCompositionThread compositionThread2 = compositionThread1;
      compositionThread2.SelectThreadEnd = compositionThread2.SelectThreadEnd + new SelectThreadEndEvent(this.OnSelectThreadEnd);
      this._selectThreads.Add(selectGUID, compositionThread1);
      compositionThread1.Start();
    }
  }

  private void CheckOptions(Guid userSessionGuid, ExtendedPublishOptions options)
  {
    IUserSession sessionById = UserSession.GetSessionByID(userSessionGuid);
    IPublishTypesConfiguration service1 = ServerServices.GetService(typeof (IPublishTypesConfiguration)) as IPublishTypesConfiguration;
    if (options.CountLevels != 0 && (options.EnableRelationTypes == null || options.EnableRelationTypes.Count == 0))
    {
      if (service1.PublishRelationTypes == null || service1.PublishRelationTypes.Count <= 0)
        throw new Exception("Отсутсвуют допустимые типы связей для получения публикуемого состава");
      options.EnableRelationTypes = service1.PublishRelationTypes;
    }
    if (options.EnableTypes == null || options.EnableTypes.Count == 0)
      options.EnableTypes = service1.PublishObjectTypes;
    if (options.Filtration != null)
      return;
    IVersionRulesCacheService service2 = ServerServices.GetService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
    options.Filtration = service2.GetFiltrationSettings((object) sessionById, string.Empty, true);
  }

  private void OnSelectThreadEnd(object sender, SelectThreadEndEventArgs e)
  {
    lock (this._selectThreads)
    {
      if (!this._selectThreads.ContainsKey(e.GUID))
        return;
      this._selectThreads.Remove(e.GUID);
    }
  }

  public CompositionInfo GetInfo(Guid selectGUID)
  {
    lock (this)
    {
      SelectPublishCompositionThread compositionThread;
      if (!this._selectThreads.TryGetValue(selectGUID, out compositionThread))
        return (CompositionInfo) null;
      if (compositionThread.IsError)
      {
        CompositionInfo info = new CompositionInfo(compositionThread.ErrorException);
        this._selectThreads.Remove(selectGUID);
        return info;
      }
      if (!compositionThread.IsCompleted)
        return new CompositionInfo(compositionThread.Percent);
      CompositionInfo info1 = new CompositionInfo((object) compositionThread.Result);
      if (compositionThread.ErrorException != null)
        info1.ErrorException = compositionThread.ErrorException;
      this._selectThreads.Remove(selectGUID);
      return info1;
    }
  }

  public void CancelSelect(Guid selectGUID)
  {
    lock (this)
    {
      SelectPublishCompositionThread compositionThread;
      if (!this._selectThreads.TryGetValue(selectGUID, out compositionThread))
        return;
      compositionThread.Stop();
      this._selectThreads.Remove(selectGUID);
    }
  }

  public void RegisterIncludeObjectsAlwaysObjectType(int objectType)
  {
    if (this._includeObjectsAlwaysObjectTypeIDs.Contains(objectType))
      return;
    this._includeObjectsAlwaysObjectTypeIDs.Add(objectType);
  }

  public List<int> IncludeObjectsAlwaysObjectTypeIDs => this._includeObjectsAlwaysObjectTypeIDs;
}
