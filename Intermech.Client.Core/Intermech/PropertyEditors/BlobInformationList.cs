
// Type: Intermech.PropertyEditors.BlobInformationList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.PropertyEditors;

public class BlobInformationList : List<BlobInformation>
{
  public BlobInformationList()
  {
  }

  public BlobInformationList(BlobInformation[] list)
  {
    this.AddRange((IEnumerable<BlobInformation>) list);
  }

  public BlobInformationList Clone()
  {
    BlobInformationList blobInformationList = new BlobInformationList();
    for (int index = 0; index < this.Count; ++index)
      blobInformationList.Add(this[index].Clone());
    return blobInformationList;
  }

  public override string ToString()
  {
    string str1 = string.Empty;
    for (int index = 0; index < this.Count; ++index)
    {
      string str2 = index < this.Count - 1 ? ";" : string.Empty;
      str1 = str1 + this[index].FileName + str2;
    }
    return str1;
  }
}
