// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.AuthenticFilesLaunchHandler
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class AuthenticFilesLaunchHandler : ParameterlessLaunchHandler
{
  private readonly ILaunchActionService launchActionService;
  private readonly Lazy<ICurrentUserAndRole> currentUserAndRoleService;
  private readonly Lazy<IClientMetadataCache> clientMetadataCacheService;

  public AuthenticFilesLaunchHandler(
    ILaunchActionService launchActionService,
    Lazy<IClientMetadataCache> clientMetadataCacheService,
    Lazy<ICurrentUserAndRole> currentUserAndRoleService)
    : base(new Guid("6E6580B4-1499-4C1F-9702-7701D1FA3CA0"), "Просмотрщик аутентичных файлов")
  {
    this.launchActionService = launchActionService;
    this.currentUserAndRoleService = currentUserAndRoleService;
    this.clientMetadataCacheService = clientMetadataCacheService;
  }

  public override void Launch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    if (handlerData == null)
      throw new ArgumentNullException(nameof (handlerData));
    string caption = EnumTypeHelper.GetCaption((Enum) launchParams.LaunchType);
    string objectNameInMessages = DBHelper.GetObjectNameInMessages(launchParams.ObjectId);
    string authenticFile;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(launchParams.ObjectId, new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      if (objectAttributeByGuid == null)
      {
        int num = (int) MessageBox.Show($"Невозможно выполнить команду запуска '{caption}', так как у объекта '{objectNameInMessages}' отсутствует атрибут файл.", caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return;
      }
      authenticFile = this.FindAuthenticFile(objectAttributeByGuid);
    }
    if (string.IsNullOrEmpty(authenticFile))
    {
      LaunchActionInfo fallbackLaunchAction = this.FindFallbackLaunchAction(launchParams);
      if (fallbackLaunchAction != null)
      {
        this.launchActionService.Launch(launchParams, fallbackLaunchAction);
      }
      else
      {
        int num = (int) MessageBox.Show($"Невозможно выполнить команду запуска '{caption}', так как у объекта '{objectNameInMessages}' отсутствуют аутентичные файлы.", caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }
    else
    {
      launchParams.ObjectFileName = authenticFile;
      this.launchActionService.LaunchByShell(launchParams);
    }
  }

  private string FindAuthenticFile(IDBAttribute dbAttribute)
  {
    for (int index = 0; index < dbAttribute.ValuesCount; ++index)
    {
      dbAttribute.Index = index;
      if (dbAttribute is IBlobReader blobReader)
      {
        BlobInformation blobInformation = blobReader.OpenBlob(-1);
        try
        {
          if (blobInformation.FileType == FileTypes.ftAuthentical)
            return blobInformation.FileName;
        }
        finally
        {
          blobReader.CloseBlob();
        }
      }
    }
    return (string) null;
  }

  private LaunchActionInfo FindFallbackLaunchAction(LaunchParams launchParams)
  {
    Guid guid = this.clientMetadataCacheService.Value.GetObjectType(launchParams.ObjectTypeId).GUID;
    UserTarget userTarget = new UserTarget(this.currentUserAndRoleService.Value.UserID, this.currentUserAndRoleService.Value.UserGuid);
    List<LaunchActionInfo> launchActionInfoList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      launchActionInfoList = ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true).LookupActionList(guid, (ITarget) userTarget, launchParams.LaunchType);
    launchActionInfoList.RemoveAll((Predicate<LaunchActionInfo>) (x => x.HandlerId == this.Id));
    return launchActionInfoList.Count == 0 ? (LaunchActionInfo) null : launchActionInfoList[0];
  }
}
