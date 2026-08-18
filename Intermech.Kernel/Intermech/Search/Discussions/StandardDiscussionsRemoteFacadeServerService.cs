// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Discussions.StandardDiscussionsRemoteFacadeServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;


namespace Intermech.Search.Discussions;

public sealed class StandardDiscussionsRemoteFacadeServerService : 
  LongLifeObject,
  IDiscussionsRemoteFacadeServerService
{
  private readonly IDiscussionsRemoteFacade _discussionsRemoteFacade;

  public StandardDiscussionsRemoteFacadeServerService(
    IDiscussionsRemoteFacade discussionsRemoteFacade)
  {
    this._discussionsRemoteFacade = discussionsRemoteFacade != null ? discussionsRemoteFacade : throw new ArgumentNullException(nameof (discussionsRemoteFacade));
  }

  public MessageDto AddMessage(
    Guid userSessionGuid,
    long objectVersionID,
    string caption,
    string text)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this._discussionsRemoteFacade.AddMessage(objectVersionID, caption, text);
  }

  public MessageDto[] FindMessagesForAllObjectVersions(Guid userSessionGuid, long objectID)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this._discussionsRemoteFacade.FindMessagesForAllObjectVersions(objectID);
  }

  public MessageDto[] FindMessagesForObject(Guid userSessionGuid, long objectVersionID)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this._discussionsRemoteFacade.FindMessagesForObject(objectVersionID);
  }

  public MessageDto[] GetMessages(Guid userSessionGuid, long discussionVersionID)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this._discussionsRemoteFacade.GetMessages(discussionVersionID);
  }

  public void RemoveMessage(Guid userSessionGuid, MessageIdDto id)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      this._discussionsRemoteFacade.RemoveMessage(id);
  }

  public MessageDto ReplaceMessage(
    Guid userSessionGuid,
    MessageIdDto id,
    string caption,
    string text)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this._discussionsRemoteFacade.ReplaceMessage(id, caption, text);
  }

  public bool CanDiscuss(Guid userSessionGuid, long objectVersionId)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this._discussionsRemoteFacade.CanDiscuss(objectVersionId);
  }

  public MessageDto[] FindMessages(Guid userSessionGuid, MessageIdDto[] ids)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this._discussionsRemoteFacade.FindMessages(ids);
  }

  public AddImageResultDto AddImage(Guid userSessionGuid, AddImageParamsDto addImageParams)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this._discussionsRemoteFacade.AddImage(addImageParams);
  }
}
