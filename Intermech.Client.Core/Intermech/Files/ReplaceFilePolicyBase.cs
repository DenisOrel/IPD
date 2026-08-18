
// Type: Intermech.Files.ReplaceFilePolicyBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Files;

[Serializable]
public abstract class ReplaceFilePolicyBase : IReplaceFilePolicy
{
  List<FileDifferencePair> IReplaceFilePolicy.Apply(
    IWorkArea workArea,
    DBObjectState dbObject,
    DBObjectState workObject,
    List<FileDifferencePair> askUserPairs)
  {
    if (workArea == null)
      throw new ArgumentNullException(nameof (workArea));
    if (dbObject == null)
      throw new ArgumentNullException(nameof (dbObject));
    List<FileDifferencePair> fileDifferencePairList = askUserPairs != null ? new List<FileDifferencePair>(askUserPairs.Count) : throw new ArgumentNullException(nameof (askUserPairs));
    for (int index = 0; index < askUserPairs.Count; ++index)
      fileDifferencePairList.Add(this.Apply(workArea, dbObject, workObject, askUserPairs[index]));
    return fileDifferencePairList;
  }

  protected abstract FileDifferencePair Apply(
    IWorkArea workArea,
    DBObjectState dbObject,
    DBObjectState workObject,
    FileDifferencePair diffPair);
}
