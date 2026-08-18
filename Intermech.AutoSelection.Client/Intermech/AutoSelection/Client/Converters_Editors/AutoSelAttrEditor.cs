// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.AutoSelAttrEditor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.Client.Core;
using Intermech.Extensions.WinForms;
using Intermech.Interfaces;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class AutoSelAttrEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    IAutoSelAttrType attrType = context?.Instance is AutoSelAttr instance ? instance.AttrType : (IAutoSelAttrType) null;
    if (attrType == null)
    {
      using (AttributesSelectDlg form = new AttributesSelectDlg(false))
        return form.ShowTopDialog() != DialogResult.OK || form.SelectedAttributesID.Count == sc_671.ssp_automatch_672(1684844072) ? value : (object) MetaDataHelper.GetAttributeType(form.SelectedAttributesID[0]);
    }
    using (AttributesSelectDlg form = new AttributesSelectDlg(false))
    {
      form.SelectedAttributeIDOnStartup(MetaDataHelper.GetAttributeID((object) instance.AttrGuid));
      if (attrType.OwnerObject is IItemCommon ownerObject)
      {
        switch (attrType.TypeMode)
        {
          case AutoSelAttrTypeMode.asatObjectType:
            form.AllowedAttributesSourceTypes = AllowedAttrsSourceTypesEnum.Objects;
            form.LoadAttrDialogForObjectsTypes(ownerObject.ObjTypeGuid.Value);
            break;
          case AutoSelAttrTypeMode.asatRelationType:
            form.AllowedAttributesSourceTypes = AllowedAttrsSourceTypesEnum.Relations;
            form.LoadAttrDialogForRelationsTypes(ownerObject.RelTypeGuid.Value);
            break;
          default:
            return value;
        }
      }
      return form.ShowTopDialog() != DialogResult.OK || form.SelectedAttributesID.Count == sc_671.ssp_automatch_673(2005174511) ? value : (object) MetaDataHelper.GetAttributeTypeGuid(form.SelectedAttributesID[0]);
    }
  }
}
