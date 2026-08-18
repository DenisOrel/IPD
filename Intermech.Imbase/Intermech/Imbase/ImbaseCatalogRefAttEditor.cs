// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseCatalogRefAttEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Imbase;

public class ImbaseCatalogRefAttEditor : UITypeEditor
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
    IDescriptor rootDescriptor = (IDescriptor) new ImbaseRootNodeDescriptor();
    AdvancedServiceContainer nodesContext = new AdvancedServiceContainer(sp);
    nodesContext.AddService(typeof (ImbaseDisableCatalogsComposition), (object) new ImbaseDisableCatalogsComposition(DisableImbaseCategory.Catalog));
    return !(SelectionWindow.Select(LocalizationHolder.rm.GetString("Imbase.Client_143"), rootDescriptor, typeof (IDBObjectID), (IServiceProvider) nodesContext, SelectionOptions.Default) is IDBObjectID[] dbObjectIdArray) || dbObjectIdArray.Length == 0 ? value : (object) new ImbaseCatalogRefAttProxy(dbObjectIdArray[0].Value);
  }
}
