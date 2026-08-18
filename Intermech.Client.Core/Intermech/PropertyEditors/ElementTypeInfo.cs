
// Type: Intermech.PropertyEditors.ElementTypeInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

public class ElementTypeInfo
{
  private int _typeID = -1;
  private AttributableElements _kind;

  public ElementTypeInfo(int typeID, AttributableElements kind)
  {
    this._typeID = typeID;
    this._kind = kind;
  }

  public int TypeID => this._typeID;

  public AttributableElements Kind => this._kind;
}
