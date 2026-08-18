// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.OpenWithCommand
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Commands;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal sealed class OpenWithCommand : ObjectCommand
{
  public OpenWithCommand()
    : base("OpenWithTool")
  {
    this.DisplayName = LocalizationHolder.rm.GetString("Tools.Client_38");
  }

  protected override void DoExecute()
  {
    SelectToolForm selectToolForm = new SelectToolForm();
    selectToolForm.MakeDefault.Enabled = false;
    selectToolForm.MakeDefault.Checked = false;
    selectToolForm.NeedCheckOut.Enabled = true;
    selectToolForm.NeedCheckOut.Checked = true;
    int num;
    Guid guid;
    ITarget target;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this.ObjectId);
      num = !objectInfo.Empty ? objectInfo.ObjectTypeID : throw new ObjectNotFoundException(this.ObjectId);
      guid = ((IDBGuid) sessionKeeper.Session.GetObjectType(num, true)).GUID;
      ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      target = (ITarget) new UserTarget(service.UserID, service.UserGuid);
    }
    LaunchType[] values = (LaunchType[]) Enum.GetValues(typeof (LaunchType));
    for (int index = 0; index < values.Length; ++index)
    {
      List<LaunchActionInfo> toolInfos;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        toolInfos = ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true).LookupActionList(guid, target, values[index]);
      if (toolInfos.Count > 0)
        selectToolForm.RegisterTools(values[index], (IList) toolInfos);
    }
    if (selectToolForm.ShowDialog() != DialogResult.OK)
      return;
    LaunchType selectedLaunchType = selectToolForm.SelectedLaunchType;
    LaunchActionInfo selectedTool = (LaunchActionInfo) selectToolForm.SelectedTool;
    bool needCheckout = selectToolForm.NeedCheckOut.Checked;
    VersionsRulePackage versionsRule = selectedLaunchType == LaunchType.Edit ? VersionsRuleSources.GetEditorRule() : VersionsRuleSources.GetCurrentWindowRule();
    ClientContext.LaunchActions.Launch(new LaunchParams(selectedLaunchType, this.ObjectId, num, versionsRule, needCheckout), selectedTool);
  }
}
