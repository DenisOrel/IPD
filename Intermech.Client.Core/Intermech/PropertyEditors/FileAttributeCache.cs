
// Type: Intermech.PropertyEditors.FileAttributeCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for FileAttributeCache.</summary>
public class FileAttributeCache : ArrayList
{
  public int GetFileAttributeCacheClassIndex(int aAttributeID)
  {
    int attributeCacheClassIndex = -1;
    for (int index = 0; index < this.Count; ++index)
    {
      if (((FileAttributeCacheClass) this[index]).AttributeID == aAttributeID)
      {
        attributeCacheClassIndex = index;
        break;
      }
    }
    return attributeCacheClassIndex;
  }

  public FileAttributeCacheClass GetFileAttributeCacheClass(int aAttributeID)
  {
    int attributeCacheClassIndex = this.GetFileAttributeCacheClassIndex(aAttributeID);
    return attributeCacheClassIndex != -1 ? (FileAttributeCacheClass) this[attributeCacheClassIndex] : (FileAttributeCacheClass) null;
  }
}
