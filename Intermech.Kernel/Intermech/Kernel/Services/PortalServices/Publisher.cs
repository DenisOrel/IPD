// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Publisher
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Kernel.Services.PortalServices;

public abstract class Publisher : IPublisher
{
  protected PublishType publishType;

  public abstract string PublicationInfo { get; }

  public Publisher(PublishType publishType) => this.publishType = publishType;

  public static ObjectXMLFileFormer GetXMLFileFormer(
    IUserSession session,
    ExtendedTransferedObject unit,
    IBackupWriter writer,
    IDBObject obj,
    Attributes4ObjectTag tag)
  {
    if (obj.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID)
      return (ObjectXMLFileFormer) new ImbaseTableXMLFileFormer(session, unit, writer, obj, tag);
    return obj.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID ? (ObjectXMLFileFormer) new ImbaseTableRefXMLFileFormer(session, unit, writer, obj, tag) : new ObjectXMLFileFormer(session, unit, writer, obj, tag);
  }

  protected virtual ObjectXMLFileFormer GetObjectXMLFileFormer(
    IUserSession session,
    ExtendedTransferedObject unit,
    IBackupWriter writer,
    IDBObject obj,
    Attributes4ObjectTag tag)
  {
    return Publisher.GetXMLFileFormer(session, unit, writer, obj, tag);
  }

  public static PublishComposition Composition(
    IUserSession session,
    List<long> rootObjects,
    ExtendedPublishOptions options,
    PublishType publishType)
  {
    if (!(session.GetCustomService(typeof (IPublishCompositionService)) is IPublishCompositionService customService))
      throw new Exception(LocalizationHolder.rm.GetString(sc_14134.ssp_webportal_14135()));
    Guid selectGUID = Guid.NewGuid();
    customService.Select(session.SessionGUID, selectGUID, rootObjects, options, publishType, true);
    CompositionInfo info;
    for (info = customService.GetInfo(selectGUID); info != null && !info.ErrorPresent && info.Percent < 100; info = customService.GetInfo(selectGUID))
      Thread.Sleep(25);
    return !info.ErrorPresent ? info.Result as PublishComposition : throw info.ErrorException;
  }

  public abstract ITransferedObject[] Pack(IUserSession session, IBackupWriter writer);

  public abstract ITask GetExportTask(
    IUserSession session,
    long userID,
    string taskName,
    Guid userGuid,
    TaskPriority priority,
    ITransferedObject[] transObjs,
    IDBAttribute attributeTaskFiles);

  public virtual void CheckBeforePublication(IUserSession session)
  {
  }
}
