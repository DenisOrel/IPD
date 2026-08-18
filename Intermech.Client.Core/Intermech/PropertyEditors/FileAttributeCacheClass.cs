
// Type: Intermech.PropertyEditors.FileAttributeCacheClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.PropertyEditors;

public class FileAttributeCacheClass
{
  private int attributeID;
  private bool isList;
  private bool isOpened;
  public ArrayList BlobInfoArray;

  public int AttributeID => this.attributeID;

  public bool IsList
  {
    get => this.isList;
    set => this.isList = value;
  }

  public bool IsOpened
  {
    get => this.isOpened;
    set => this.isOpened = value;
  }

  public FileAttributeCacheClass(
    int aAttributeID,
    bool aIsOpened,
    bool aIsList,
    ArrayList aBlobInfoArray)
  {
    this.attributeID = aAttributeID;
    this.isOpened = aIsOpened;
    this.isList = aIsList;
    this.BlobInfoArray = aBlobInfoArray == null ? new ArrayList() : aBlobInfoArray;
  }
}
