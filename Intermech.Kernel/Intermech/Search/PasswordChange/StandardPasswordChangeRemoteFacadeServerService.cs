// Decompiled with JetBrains decompiler
// Type: Intermech.Search.PasswordChange.StandardPasswordChangeRemoteFacadeServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;


namespace Intermech.Search.PasswordChange;

public sealed class StandardPasswordChangeRemoteFacadeServerService : 
  LongLifeObject,
  IPasswordChangeRemoteFacadeServerService
{
  private readonly IPasswordChangeRemoteFacade _passwordChangeRemoteFacade;

  public StandardPasswordChangeRemoteFacadeServerService(
    IPasswordChangeRemoteFacade passwordChangeRemoteFacade)
  {
    this._passwordChangeRemoteFacade = passwordChangeRemoteFacade != null ? passwordChangeRemoteFacade : throw new ArgumentNullException(nameof (passwordChangeRemoteFacade));
  }

  public ChangePasswordResult ChangePassword(
    Guid userSessionGuid,
    string oldPassword,
    string newPassword)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this._passwordChangeRemoteFacade.ChangePassword(oldPassword, newPassword);
  }

  public bool GetPasswordChangeNeed(Guid userSessionGuid)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this._passwordChangeRemoteFacade.GetPasswordChangeNeed();
  }
}
