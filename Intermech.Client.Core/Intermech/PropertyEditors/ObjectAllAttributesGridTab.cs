
// Type: Intermech.PropertyEditors.ObjectAllAttributesGridTab
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

/// <summary>
/// Реализует закладку для редактора атрибутов объекта, отображающую
/// все атрибуты объекта.
/// </summary>
public class ObjectAllAttributesGridTab : ObjectPropertyGridTab
{
  private static readonly Guid tabGuid = new Guid("7E74807E-46B8-4b26-AEB1-A222F9DED49E");
  private static readonly string tabName = LocalizationHolder.rm.GetString("Client.Core_219");
  private static Bitmap tabBitmap = (Bitmap) null;
  private static readonly GetAttributeValuesModes tabAttributeValuesModes = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.IncludeOnlyInvisible | GetAttributeValuesModes.IncludeCaption | GetAttributeValuesModes.CheckReadAccess;

  public override GetAttributeValuesModes TabAttributeValuesModes
  {
    get => ObjectAllAttributesGridTab.tabAttributeValuesModes;
  }

  public override Guid TabGuid => ObjectAllAttributesGridTab.tabGuid;

  public override string TabName => ObjectAllAttributesGridTab.tabName;

  public override Bitmap Bitmap
  {
    get
    {
      if (ObjectAllAttributesGridTab.tabBitmap == null)
      {
        INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
        if (service != null)
          ObjectAllAttributesGridTab.tabBitmap = new Bitmap(service.ImageList.Images[service.ImageIndex("imgPrintPreview")]);
      }
      return ObjectAllAttributesGridTab.tabBitmap;
    }
  }
}
