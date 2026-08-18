
// Type: Intermech.PropertyEditors.VisibilityPropTab
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Drawing;


namespace Intermech.PropertyEditors;

internal class VisibilityPropTab : ObjectPropertyGridTab
{
  private static GetAttributeValuesModes tabAttributeValuesModes = ClientConsts.GetAttributeValuesModes | GetAttributeValuesModes.IncludeOnlyInvisible;
  private Guid guid = Guid.NewGuid();
  private static Bitmap _bitmap = (Bitmap) null;

  public override GetAttributeValuesModes TabAttributeValuesModes
  {
    get => VisibilityPropTab.tabAttributeValuesModes;
  }

  public override Guid TabGuid => this.guid;

  public override string TabName => LocalizationHolder.rm.GetString("Client.Core_155");

  public override Bitmap Bitmap
  {
    get
    {
      if (VisibilityPropTab._bitmap == null)
      {
        INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
        if (service != null)
          VisibilityPropTab._bitmap = new Bitmap(service.ImageList.Images[service.ImageIndex("imgPrintPreview")]);
      }
      return VisibilityPropTab._bitmap;
    }
  }
}
