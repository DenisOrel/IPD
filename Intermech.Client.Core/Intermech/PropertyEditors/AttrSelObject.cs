
// Type: Intermech.PropertyEditors.AttrSelObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

public class AttrSelObject
{
  public int id = -1;
  public string name = "";
  public FieldTypes type;

  public AttrSelObject(int lid, FieldTypes ft, string lname)
  {
    this.id = lid;
    this.type = ft;
    this.name = lname;
  }

  public override string ToString() => this.name;
}
