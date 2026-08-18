
// Type: Intermech.Files.PreserveAnyChanges
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Files;

[Serializable]
public sealed class PreserveAnyChanges : IReplaceFilePolicy
{
  private readonly IReplaceFilePolicy confirmPolicy = (IReplaceFilePolicy) new ConfirmAnyRefresh();
  private readonly IReplaceFilePolicy preservePolicy = (IReplaceFilePolicy) new PreserveAnyFile();

  List<FileDifferencePair> IReplaceFilePolicy.Apply(
    IWorkArea workArea,
    DBObjectState dbObject,
    DBObjectState workObject,
    List<FileDifferencePair> askUserPairs)
  {
    if (dbObject == null)
      throw new ArgumentNullException(nameof (dbObject));
    return (!dbObject.IsEditableState || workObject == null || workObject.ObjectId != dbObject.ObjectId ? this.confirmPolicy : this.preservePolicy).Apply(workArea, dbObject, workObject, askUserPairs);
  }
}
