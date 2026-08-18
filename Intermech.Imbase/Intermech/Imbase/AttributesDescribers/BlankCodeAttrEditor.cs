// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.BlankCodeAttrEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

public class BlankCodeAttrEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider sp,
    object value)
  {
    AdvancedServiceContainer nodesContext = new AdvancedServiceContainer(sp);
    nodesContext.AddService(typeof (ImbaseDisableCatalogsComposition), (object) new ImbaseDisableCatalogsComposition(DisableImbaseCategory.Folder));
    Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(Intermech.Imbase.Consts.ImbaseFolderTypeID, false), true);
    if (Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("IMH_SelectCatalogIMBASE"), (IDescriptor) new ImbaseRootNodeDescriptor(), typeof (IDBObjectID), (IServiceProvider) nodesContext, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect | SelectionOptions.ForceRebuildNavTree) is IDBObjectID[] dbObjectIdArray && dbObjectIdArray.Length != 0)
      value = (object) new BlankCodeAttrProxy(dbObjectIdArray[0].Value);
    return value;
  }
}
