
// Type: Intermech.Files.ConfirmAnyRefresh
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.IO;
using System.Text;


namespace Intermech.Files;

[Serializable]
public sealed class ConfirmAnyRefresh : ReplaceFilePolicyBase
{
  protected override FileDifferencePair Apply(
    IWorkArea workArea,
    DBObjectState dbObject,
    DBObjectState workObject,
    FileDifferencePair diffPair)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat(LocalizationUtils.RestoreEscapes(LocalizationHolder.rm.GetString("Client.Core_1272")), (object) Path.Combine(workArea.AreaPath, diffPair.LocalState.FileName), (object) dbObject.Caption);
    stringBuilder.Append("\r\n");
    stringBuilder.Append(LocalizationUtils.RestoreEscapes(LocalizationHolder.rm.GetString("Client.Core_1273")));
    stringBuilder.AppendFormat(LocalizationUtils.RestoreEscapes(LocalizationHolder.rm.GetString("Client.Core_1274")), (object) diffPair.LocalState.LastWriteTimeUtc.ToLocalTime());
    stringBuilder.AppendFormat(LocalizationUtils.RestoreEscapes(LocalizationHolder.rm.GetString("Client.Core_1275")), (object) diffPair.RemoteState.LastWriteTimeUtc.ToLocalTime());
    stringBuilder.Append("\r\n");
    stringBuilder.Append(LocalizationHolder.rm.GetString("Client.Core_1276"));
    return new FileDifferencePair(new ConfirmAnyRefreshConfirmation("Перезапись файлов", stringBuilder.ToString()).ConfirmAction() ? FileDifferenceType.OutdatedFile : FileDifferenceType.UnchangedFile, diffPair.LocalState, diffPair.RemoteState);
  }
}
