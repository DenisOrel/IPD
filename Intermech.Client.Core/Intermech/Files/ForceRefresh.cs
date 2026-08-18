
// Type: Intermech.Files.ForceRefresh
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Files;

[Serializable]
public sealed class ForceRefresh : ReplaceFilePolicyBase
{
  protected override FileDifferencePair Apply(
    IWorkArea workArea,
    DBObjectState dbObject,
    DBObjectState workObject,
    FileDifferencePair diffPair)
  {
    return new FileDifferencePair(FileDifferenceType.OutdatedFile, diffPair.LocalState, diffPair.RemoteState);
  }
}
