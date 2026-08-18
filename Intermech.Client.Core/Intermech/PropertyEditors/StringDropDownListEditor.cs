
// Type: Intermech.PropertyEditors.StringDropDownListEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.Collections;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

public class StringDropDownListEditor : DropDownListEditor
{
  protected StringTypeConverter stringTypeConverter;
  protected IPossibleValuesHolder iPossibleValuesHolder;
  protected EventsHolder.GetListDelegate getStringList;

  public StringDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder)
    : this(aIPossibleValuesHolder, true)
  {
  }

  public StringDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder, bool valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
    this.stringTypeConverter = new StringTypeConverter(aIPossibleValuesHolder, valCanNull);
  }

  public StringDropDownListEditor(EventsHolder.GetListDelegate aGetStringList)
    : this(aGetStringList, true)
  {
  }

  public StringDropDownListEditor(EventsHolder.GetListDelegate aGetStringList, bool valCanNull)
  {
    this.getStringList = aGetStringList;
    this.stringTypeConverter = new StringTypeConverter(aGetStringList, valCanNull);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    return this.stringTypeConverter.GetStandardValuesCustomList(context, args);
  }
}
