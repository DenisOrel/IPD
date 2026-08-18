
// Type: Intermech.PropertyEditors.DoubleDropDownListEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.Collections;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

internal class DoubleDropDownListEditor : DropDownListEditor
{
  private DoubleTypeConverter doubleTypeConverter;
  private IPossibleValuesHolder iPossibleValuesHolder;
  private EventsHolder.GetListDelegate getDoubleList;

  public DoubleDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder)
    : this(aIPossibleValuesHolder, true)
  {
  }

  public DoubleDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder, bool valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
    this.doubleTypeConverter = new DoubleTypeConverter(aIPossibleValuesHolder, valCanNull);
  }

  public DoubleDropDownListEditor(EventsHolder.GetListDelegate aGetDoubleList)
    : this(aGetDoubleList, true)
  {
  }

  public DoubleDropDownListEditor(EventsHolder.GetListDelegate aGetDoubleList, bool valCanNull)
  {
    this.getDoubleList = aGetDoubleList;
    this.doubleTypeConverter = new DoubleTypeConverter(aGetDoubleList, valCanNull);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    return this.doubleTypeConverter.GetStandardValuesCustomList(context, args);
  }
}
