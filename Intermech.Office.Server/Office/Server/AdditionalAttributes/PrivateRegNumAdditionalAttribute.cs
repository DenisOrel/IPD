// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.AdditionalAttributes.PrivateRegNumAdditionalAttribute
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Office.Interfaces;
using System;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Office.Server.AdditionalAttributes;

internal class PrivateRegNumAdditionalAttribute : AdditionalActivitiesAttributes
{
  [CanBeNull]
  private string _regNum;

  protected override bool EnableCreate(
    [NotNull] IUserSession session,
    IProcess process,
    ResolutionProcessExecuteArgs args)
  {
    IOfficeGeneralSettingsService customService = session.GetCustomService<IOfficeGeneralSettingsService>();
    IOfficeRegistrationService registrationService = session.GetCustomService<IOfficeRegistrationService>();
    if (!customService.Settings.PrivateOffice)
      return false;
    IFiltrationTableService filtrationService = ApplicationServices.Container.GetService<IFiltrationTableService>();
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string str in args.ExecutorIDs.Select<long, long>((Func<long, long>) (executorID => registrationService.GetUserUnit(executorID))).Select<long, string>((Func<long, string>) (unitID => args.OfficeDocID == 0L ? (string) null : filtrationService.GetValue(((UserSession) session).DataManager, Math.Abs(args.OfficeDocID), Math.Abs(unitID)))).Where<string>((Func<string, bool>) (regNum => !string.IsNullOrEmpty(regNum))))
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append(", ");
      stringBuilder.Append(str);
    }
    if (stringBuilder.Length <= 0)
      return false;
    this._regNum = PrivateRegNumAdditionalAttribute.CheckLength(stringBuilder.ToString());
    return true;
  }

  [NotNull]
  private static string CheckLength([NotNull] string regNum)
  {
    int sizeType = (int) MetaDataHelper.GetAttributeType(OfficeConsts.AttrResolutionDocumentRegNumID).SizeType;
    if (regNum.Length <= sizeType)
      return regNum;
    return sizeType >= 7 ? regNum.Substring(0, sizeType - 3) + "..." : regNum.Substring(0, sizeType);
  }

  protected override int AdditionalAttribute => OfficeConsts.AttrResolutionDocumentRegNumID;

  protected override void AddValue([NotNull] IDBAttribute attribute)
  {
    attribute.AsString = this._regNum;
  }
}
