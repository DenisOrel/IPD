
// Type: Intermech.PropertyEditors.FileAttributeBoxCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Collections;


namespace Intermech.PropertyEditors;

/// <summary>Хранит кэш Caption файловых шкафов по их Id</summary>
public class FileAttributeBoxCache
{
  private static Hashtable boxHashtable = new Hashtable();

  public static string GetBoxCaption(long aBoxId)
  {
    string boxCaption = string.Empty;
    if (FileAttributeBoxCache.boxHashtable.ContainsKey((object) aBoxId))
    {
      boxCaption = FileAttributeBoxCache.boxHashtable[(object) aBoxId].ToString();
    }
    else
    {
      QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(aBoxId);
      if (!objectInfo.Empty)
      {
        boxCaption = objectInfo.Caption;
        FileAttributeBoxCache.boxHashtable.Add((object) aBoxId, (object) boxCaption);
      }
    }
    return boxCaption;
  }
}
