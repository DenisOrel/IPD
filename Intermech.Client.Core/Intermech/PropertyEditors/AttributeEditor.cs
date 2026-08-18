
// Type: Intermech.PropertyEditors.AttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Holders;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class AttributeEditor : UITypeEditor, ISelectorFilter
{
  private bool excludeSystemAttributes;
  private FieldTypes[] filterByTypes;
  private int[] excludeAttrId;
  protected AttributesSelectDlg selectorForm;

  public bool ExcludeSystemAttributes
  {
    get => this.excludeSystemAttributes;
    set => this.excludeSystemAttributes = value;
  }

  public FieldTypes[] FilterByTypes
  {
    get => this.filterByTypes;
    set => this.filterByTypes = value;
  }

  public int[] ExcludeAttributeId
  {
    get => this.excludeAttrId;
    set => this.excludeAttrId = value;
  }

  public AttributeEditor()
  {
  }

  public AttributeEditor(
    bool aExcludeSystemAttributes,
    FieldTypes[] aFilterByTypes,
    int[] aExcludeAttrId)
    : this()
  {
    this.excludeSystemAttributes = aExcludeSystemAttributes;
    this.filterByTypes = aFilterByTypes;
    this.excludeAttrId = aExcludeAttrId;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    if (this.selectorForm == null)
    {
      this.selectorForm = new AttributesSelectDlg(false);
      this.selectorForm.SelectorFilter = this.excludeSystemAttributes || this.filterByTypes != null || this.excludeAttrId != null ? (ISelectorFilter) this : (ISelectorFilter) null;
    }
    return this.selectorForm.ShowDialog() == DialogResult.OK && this.selectorForm.SelectedAttributesID.Count > 0 ? (object) new AttributePropertyClass(this.selectorForm.SelectedAttributesID[0]) : value;
  }

  public bool IsInFilter(int category, object id) => !this.IsInFilterOld(category, id);

  private bool IsInFilterOld(int category, object id)
  {
    if (category != 3)
      return true;
    if (this.excludeSystemAttributes && (int) id < 0 || this.excludeAttrId != null && Array.IndexOf<int>(this.excludeAttrId, (int) id) != -1)
      return false;
    if (this.filterByTypes == null)
      return true;
    DataRow[] dataRowArray = DataHolders.AttributesHolder.DataTable.Select("F_ATTRIBUTE_ID=" + id.ToString());
    return dataRowArray != null && dataRowArray.Length != 0 && Array.IndexOf<FieldTypes>(this.filterByTypes, (FieldTypes) Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_TYPE"])) != -1;
  }
}
