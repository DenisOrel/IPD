// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.SectionItem
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.PropertyEditors;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

internal class SectionItem
{
  public SectionItemList Parent;
  private object value;
  public object propValue;

  public SectionItem(object value, SectionItemList parent)
  {
    this.Parent = parent;
    this.Value = value;
  }

  public SectionItem(object value, bool propValue, SectionItemList parent)
  {
    this.Parent = parent;
    if (propValue)
      this.PropValue = value;
    else
      this.Value = value;
  }

  public object Value
  {
    get => this.value;
    set
    {
      this.value = value;
      this.propValue = this.Parent.Describer.GetPropDescriptorValue((IElementInfo) null, this.Parent.AttrId, value);
    }
  }

  public object PropValue
  {
    get => this.propValue;
    set
    {
      this.propValue = value;
      this.value = this.Parent.Describer.GetAttributeValue((IElementInfo) null, this.Parent.AttrId, value);
    }
  }
}
