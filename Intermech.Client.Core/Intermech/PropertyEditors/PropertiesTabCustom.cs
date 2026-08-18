
// Type: Intermech.PropertyEditors.PropertiesTabCustom
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.PropertyGridInternal;


namespace Intermech.PropertyEditors;

public class PropertiesTabCustom : PropertiesTab, IObjectPropertyGridTab
{
  internal static readonly Guid PropertyTabGuid = new Guid("{6F05B9D0-D675-40f0-9355-ABEB8BFE9136}");
  private static Bitmap _bitmap = (Bitmap) null;

  public override string TabName => LocalizationHolder.rm.GetString("Client.Core_146");

  public override Bitmap Bitmap
  {
    get
    {
      string resource = typeof (PropertiesTab).Name + ".bmp";
      if (PropertiesTabCustom._bitmap == null)
        PropertiesTabCustom._bitmap = new Bitmap(typeof (PropertiesTab), resource);
      return PropertiesTabCustom._bitmap;
    }
  }

  public PropertyDescriptorCollection PropDescriptorCollection(object component)
  {
    return component is IObjectPropDescriptorHolder ? ((IObjectPropDescriptorHolder) component).PropDescriptorCollection : (PropertyDescriptorCollection) null;
  }

  public Guid TabGuid => PropertiesTabCustom.PropertyTabGuid;

  public GetAttributeValuesModes TabAttributeValuesModes => GetAttributeValuesModes.None;

  public void InitTab(GetAttributeValuesModes avm)
  {
  }
}
