
// Type: Intermech.PropertyEditors.PropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Data;


namespace Intermech.PropertyEditors;

public class PropertyClass
{
  protected object value;
  protected DataTable possibleValuesDataTable;
  protected string description = string.Empty;
  protected bool masked;
  protected string mask;

  public object Value => this.value;

  public bool Masked => this.masked;

  public string Mask => this.mask;
}
