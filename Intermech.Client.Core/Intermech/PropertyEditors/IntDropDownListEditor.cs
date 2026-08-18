
// Type: Intermech.PropertyEditors.IntDropDownListEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.Collections;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

internal class IntDropDownListEditor : DropDownListEditor
{
  private IntTypeConverter intTypeConverter;
  private IPossibleValuesHolder iPossibleValuesHolder;
  private EventsHolder.GetListDelegate getIntList;

  public IntDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder)
    : this(aIPossibleValuesHolder, true)
  {
  }

  public IntDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder, bool valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
    this.intTypeConverter = new IntTypeConverter(aIPossibleValuesHolder, valCanNull);
  }

  public IntDropDownListEditor(EventsHolder.GetListDelegate aGetIntList)
    : this(aGetIntList, true)
  {
  }

  public IntDropDownListEditor(EventsHolder.GetListDelegate aGetIntList, bool valCanNull)
  {
    this.getIntList = aGetIntList;
    this.intTypeConverter = new IntTypeConverter(aGetIntList, valCanNull);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    return this.intTypeConverter.GetStandardValuesCustomList(context, args);
  }
}
