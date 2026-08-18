// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.AutoSelAttrCollEditor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.Interfaces.Document;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

[Serializable]
internal class AutoSelAttrCollEditor(System.Type t) : CollectionEditor(t)
{
  protected IAutoSelAttrType _attrType;

  protected override CollectionEditor.CollectionForm CreateCollectionForm()
  {
    CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
    collectionForm.Load += (EventHandler) ((_param1_1, _param2_1) =>
    {
      collectionForm.HelpButton = false;
      collectionForm.Text = LocalizationHolder.rm.GetString(sc_668.ssp_automatch_669());
      Label label = new Label();
      string str1 = LocalizationHolder.rm.GetString("AutoSelection.Client_29");
      string propertiesText = LocalizationHolder.rm.GetString("AutoSelection.Client_30");
      foreach (Control control1 in (ArrangedElementCollection) collectionForm.Controls)
      {
        foreach (Control control2 in (ArrangedElementCollection) control1.Controls)
        {
          if (control2.GetType().ToString() == "System.Windows.Forms.Label" && (control2.Text == "&Members:" || control2.Text == LocalizationHolder.rm.GetString("AutoSelection.Client_31")))
            control2.Text = str1;
          if (control2.GetType().ToString() == "System.Windows.Forms.Label")
          {
            string str2 = control2.Text ?? string.Empty;
            if (str2.Contains("&properties") || str2.Contains(LocalizationHolder.rm.GetString("AutoSelection.Client_32")))
            {
              label = (Label) control2;
              label.Text = propertiesText ?? string.Empty;
            }
          }
          if (control2.GetType().ToString() == "System.ComponentModel.Design.CollectionEditor+FilterListBox")
          {
            Label properties = label;
            ((ListBox) control2).SelectedIndexChanged += (EventHandler) ((_param1_2, _param2_2) => properties.Text = propertiesText ?? string.Empty);
          }
          if (control2.GetType().ToString() == "System.Windows.Forms.Design.VsPropertyGrid")
          {
            Label properties = label;
            ((PropertyGrid) control2).SelectedGridItemChanged += (SelectedGridItemChangedEventHandler) ((_param1_3, _param2_3) => properties.Text = propertiesText ?? string.Empty);
            ((PropertyGrid) control2).HelpVisible = true;
            ((PropertyGrid) control2).HelpBackColor = SystemColors.Info;
          }
        }
      }
    });
    collectionForm.FormClosing += (FormClosingEventHandler) ((_param1, _param2) => { });
    return collectionForm;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    this._attrType = value as IAutoSelAttrType;
    return base.EditValue(context, provider, value);
  }

  protected override object CreateInstance(System.Type itemType)
  {
    return Activator.CreateInstance(itemType, (object) this._attrType);
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return context == null || context.PropertyDescriptor is CustomPropertyDescriptor propertyDescriptor && propertyDescriptor.IsReadOnly ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.Modal;
  }
}
