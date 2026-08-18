
// Type: Intermech.PropertyEditors.DateTimeDropDownListEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.Collections;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>Выбор из списка выпадающих дат</summary>
public class DateTimeDropDownListEditor : DropDownListEditor
{
  private DateTimeTypeConverter dateTimeTypeConverter;
  private IPossibleValuesHolder iPossibleValuesHolder;
  private EventsHolder.GetListDelegate getDateList;

  public DateTimeDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder)
    : this(aIPossibleValuesHolder, true)
  {
  }

  public DateTimeDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder, bool valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
    this.dateTimeTypeConverter = new DateTimeTypeConverter(aIPossibleValuesHolder, valCanNull);
  }

  public DateTimeDropDownListEditor(EventsHolder.GetListDelegate aGetDateList)
    : this(aGetDateList, true)
  {
  }

  public DateTimeDropDownListEditor(EventsHolder.GetListDelegate aGetDateList, bool valCanNull)
  {
    this.getDateList = aGetDateList;
    this.dateTimeTypeConverter = new DateTimeTypeConverter(aGetDateList, valCanNull);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    return this.dateTimeTypeConverter.GetStandardValuesCustomList(context, args);
  }
}
