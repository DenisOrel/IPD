
// Type: Intermech.PropertyEditors.FileAttributeStatics
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class FileAttributeStatics
{
  private static IIconReader iIconReader;
  public static ImageList imageList;
  public static Hashtable imageHashtable;

  public static void InitImageList()
  {
    if (FileAttributeStatics.imageList != null)
      return;
    FileAttributeStatics.iIconReader = ServicesManager.GetService(typeof (IIconReader)) as IIconReader;
    FileAttributeStatics.imageList = new ImageList();
    FileAttributeStatics.imageList.ColorDepth = ColorDepth.Depth32Bit;
    FileAttributeStatics.imageList.TransparentColor = Color.Transparent;
    FileAttributeStatics.imageHashtable = new Hashtable();
    FileAttributeStatics.imageList.Images.Clear();
    FileAttributeStatics.imageHashtable.Clear();
    using (Stream manifestResourceStream = typeof (FileAttributeEditForm).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.EmptyDocument.ico"))
    {
      Icon icon = new Icon(manifestResourceStream);
      FileAttributeStatics.imageList.Images.Add(icon);
      FileAttributeStatics.imageHashtable.Add((object) "*0", (object) 0);
    }
    using (Stream manifestResourceStream = typeof (FileAttributeEditForm).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.ftFile.ico"))
    {
      Icon icon = new Icon(manifestResourceStream);
      FileAttributeStatics.imageList.Images.Add(icon);
      FileAttributeStatics.imageHashtable.Add((object) "*1", (object) 1);
    }
    using (Stream manifestResourceStream = typeof (FileAttributeEditForm).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.ftBlob.ico"))
    {
      Icon icon = new Icon(manifestResourceStream);
      FileAttributeStatics.imageList.Images.Add(icon);
      FileAttributeStatics.imageHashtable.Add((object) "*2", (object) 2);
    }
    using (Stream manifestResourceStream = typeof (FileAttributeEditForm).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.ftShortBlob.ico"))
    {
      Icon icon = new Icon(manifestResourceStream);
      FileAttributeStatics.imageList.Images.Add(icon);
      FileAttributeStatics.imageHashtable.Add((object) "*3", (object) 3);
    }
  }

  public static int FieldTypeToImageIndex(FieldTypes ft)
  {
    switch (ft)
    {
      case FieldTypes.ftShortBlob:
        return 3;
      case FieldTypes.ftFile:
        return 1;
      case FieldTypes.ftBlob:
        return 2;
      default:
        return -1;
    }
  }

  public static FieldTypes ImageIndexToFieldType(int index)
  {
    switch (index)
    {
      case 1:
        return FieldTypes.ftFile;
      case 2:
        return FieldTypes.ftBlob;
      case 3:
        return FieldTypes.ftShortBlob;
      default:
        return FieldTypes.ftUnknown;
    }
  }

  public static int GetExtImageIndex(string ext)
  {
    int extImageIndex = 0;
    FileAttributeStatics.InitImageList();
    if (ext != string.Empty && FileAttributeStatics.iIconReader != null)
    {
      object obj = FileAttributeStatics.imageHashtable[(object) ext];
      if (obj == null)
      {
        Icon iconByFileExt = FileAttributeStatics.iIconReader.GetIconByFileExt(ext);
        if (iconByFileExt != null)
        {
          FileAttributeStatics.imageList.Images.Add(iconByFileExt);
          FileAttributeStatics.imageHashtable.Add((object) ext, (object) (FileAttributeStatics.imageList.Images.Count - 1));
          extImageIndex = FileAttributeStatics.imageList.Images.Count - 1;
        }
      }
      else
        extImageIndex = (int) obj;
    }
    return extImageIndex;
  }
}
