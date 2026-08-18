// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.SectionItemList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.PropertyEditors;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

internal class SectionItemList : List<SectionItem>
{
  public int AttrId;
  private TypeConverter converter;
  private IAttributePropertyDescriber describer;
  private UITypeEditor editor;

  public SectionItemList(int attrId) => this.AttrId = attrId;

  public TypeConverter Converter
  {
    get
    {
      if (this.converter == null)
        this.converter = this.Describer.GetPropDescriptorConverter(this.AttrId);
      return this.converter ?? (this.converter = new TypeConverter());
    }
  }

  public IAttributePropertyDescriber Describer
  {
    get
    {
      if (this.describer == null)
        this.describer = SpecSectionsEditor.attributePropertyDescriberService.GetDescriber(this.AttrId);
      return this.describer;
    }
  }

  public UITypeEditor Editor
  {
    get
    {
      if (this.editor == null && this.Describer != null)
        this.editor = this.Describer.GetPropDescriptorEditor(this.AttrId) as UITypeEditor;
      return this.editor;
    }
  }
}
