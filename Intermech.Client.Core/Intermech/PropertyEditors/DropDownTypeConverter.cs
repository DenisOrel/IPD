
// Type: Intermech.PropertyEditors.DropDownTypeConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.Collections;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ParentEditor.</summary>
public class DropDownTypeConverter : TypeConverter, IComparer
{
  protected bool sortValues;
  public ArrayList DropDownList;
  protected static CaseInsensitiveComparer _comparer = new CaseInsensitiveComparer();
  private EventsHolder.GetListDelegate GetList;
  protected bool valueCanNull = true;

  public DropDownTypeConverter()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  public DropDownTypeConverter(EventsHolder.GetListDelegate getListDelegate)
    : this(getListDelegate, true)
  {
  }

  public DropDownTypeConverter(EventsHolder.GetListDelegate getListDelegate, bool valueCanNull)
  {
    this.valueCanNull = valueCanNull;
    this.GetList = getListDelegate;
  }

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    ArrayList arrayList = (ArrayList) null;
    if (this.GetList != null)
      arrayList = this.GetList((object) this);
    if (arrayList == null)
      arrayList = this.GetStandardValuesCustomList(context);
    if (this.sortValues)
      arrayList.Sort((IComparer) this);
    this.DropDownList = new ArrayList((ICollection) arrayList);
    return new TypeConverter.StandardValuesCollection((ICollection) arrayList);
  }

  public virtual ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    return (ArrayList) null;
  }

  public virtual int Compare(object x, object y)
  {
    if (x == null && y == null)
      return 0;
    if (x == null)
      return -1;
    return y == null ? 1 : DropDownTypeConverter._comparer.Compare((object) x.ToString(), (object) y.ToString());
  }
}
