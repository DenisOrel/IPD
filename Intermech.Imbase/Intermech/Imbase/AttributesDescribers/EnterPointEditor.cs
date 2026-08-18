// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.EnterPointEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.WebPortal;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal sealed class EnterPointEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    ISitesCacheService customService = (ISitesCacheService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISitesCacheService));
    List<long> objectIDs = new List<long>();
    foreach (SiteInfo site in customService.Sites)
    {
      if (!site.Code.Equals(customService.Info.Code))
        objectIDs.Add(site.ID);
    }
    if (objectIDs.Count == 0)
      throw new Exception("Отсутсвуют доступные точки ввода!");
    if (!(SelectionWindow.Select("Выберите точку ввода для каталога", (IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, 0, "Точки ввода", (IList) objectIDs), typeof (IDBObjectID), SelectionOptions.Default | SelectionOptions.DisableMultiselect) is IDBObjectID[] dbObjectIdArray))
      return value;
    SiteInfo site1 = ((ISitesCacheService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISitesCacheService))).GetSite(dbObjectIdArray[0].Value);
    return (object) new EnterPoint(site1.Code.ToString(), site1.Caption);
  }
}
