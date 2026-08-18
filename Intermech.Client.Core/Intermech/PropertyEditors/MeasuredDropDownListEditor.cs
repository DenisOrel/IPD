
// Type: Intermech.PropertyEditors.MeasuredDropDownListEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.Collections;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

public class MeasuredDropDownListEditor : DropDownListEditor
{
  protected MeasuredTypeConverter stringTypeConverter;
  protected IPossibleValuesHolder iPossibleValuesHolder;
  protected EventsHolder.GetListDelegate getStringList;

  public MeasuredDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder)
    : this(aIPossibleValuesHolder, true)
  {
  }

  public MeasuredDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder, bool valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
    this.stringTypeConverter = new MeasuredTypeConverter(aIPossibleValuesHolder, valCanNull);
  }

  public MeasuredDropDownListEditor(EventsHolder.GetListDelegate aGetStringList)
    : this(aGetStringList, true)
  {
  }

  public MeasuredDropDownListEditor(EventsHolder.GetListDelegate aGetStringList, bool valCanNull)
  {
    this.getStringList = aGetStringList;
    this.stringTypeConverter = new MeasuredTypeConverter(aGetStringList, valCanNull);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    return this.stringTypeConverter.GetStandardValuesCustomList(context, args);
  }
}
