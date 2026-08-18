
// Type: Intermech.PropertyEditors.GuidDropDownListEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.Collections;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

internal class GuidDropDownListEditor : DropDownListEditor
{
  private GuidTypeConverter guidTypeConverter;
  private IPossibleValuesHolder iPossibleValuesHolder;
  private EventsHolder.GetListDelegate getGuidList;

  public GuidDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder)
    : this(aIPossibleValuesHolder, true)
  {
  }

  public GuidDropDownListEditor(IPossibleValuesHolder aIPossibleValuesHolder, bool valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
    this.guidTypeConverter = new GuidTypeConverter(aIPossibleValuesHolder, valCanNull);
  }

  public GuidDropDownListEditor(EventsHolder.GetListDelegate aGetGuidList)
    : this(aGetGuidList, true)
  {
  }

  public GuidDropDownListEditor(EventsHolder.GetListDelegate aGetGuidList, bool valCanNull)
  {
    this.getGuidList = aGetGuidList;
    this.guidTypeConverter = new GuidTypeConverter(aGetGuidList, valCanNull);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    return this.guidTypeConverter.GetStandardValuesCustomList(context, args);
  }
}
