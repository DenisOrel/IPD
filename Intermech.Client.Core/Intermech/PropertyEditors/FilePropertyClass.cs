
// Type: Intermech.PropertyEditors.FilePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>По-моему лишний класс</summary>
[Editor(typeof (FileEditor), typeof (UITypeEditor))]
public class FilePropertyClass : IElementInfo
{
  private string fileName = string.Empty;
  private long elementID;
  private AttributableElements attributableElement;
  private int attributeID;
  private int index;

  public string FileName => this.fileName;

  public long ElementID => this.elementID;

  public AttributableElements AttributableElement => this.attributableElement;

  public int AttributeID => this.attributeID;

  public int Index => this.index;

  public FilePropertyClass(
    string aFileName,
    long aElementID,
    AttributableElements aAttributableElement,
    int aAttributeID,
    int aIndex)
  {
    this.fileName = aFileName;
    this.elementID = aElementID;
    this.attributableElement = aAttributableElement;
    this.attributeID = aAttributeID;
    this.index = aIndex;
  }

  public override string ToString() => this.fileName;

  public long ElementIdentifier => this.elementID;

  public AttributableElements ElementKind => this.attributableElement;
}
