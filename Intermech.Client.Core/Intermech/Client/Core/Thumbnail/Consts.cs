
// Type: Intermech.Client.Core.Thumbnail.Consts
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;


namespace Intermech.Client.Core.Thumbnail;

/// <summary>Summary description for Const.</summary>
public class Consts
{
  public static readonly Guid ImageAttributeGUID = new Guid("cad0013e-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid LibImageAttributeGUID = new Guid("cad0013d-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ImageLibraryFolderTypeGUID = new Guid("cad0013f-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ImageLibraryItemTypeGUID = new Guid("cad00140-306c-11d8-b4e9-00304f19f545");
  public static int ImageAttTypeID;
  public static int LibImageAttTypeID;
  public static int ImageLibraryFolderTypeID;
  public static int ImageLibraryItemTypeID;

  public static void Initialize()
  {
    IServiceContainer serviceContainer = ServicesManager.ServiceContainer;
    IGuidMapper service1 = (IGuidMapper) serviceContainer.GetService(typeof (IGuidMapper));
    IPicturesCache serviceInstance = serviceContainer.GetService(typeof (IPicturesCache)) as IPicturesCache;
    INamedImageList service2 = (INamedImageList) serviceContainer.GetService(typeof (INamedImageList));
    IFactory service3 = (IFactory) serviceContainer.GetService(typeof (IFactory));
    ICategoryTypeIconService service4 = (ICategoryTypeIconService) serviceContainer.GetService(typeof (ICategoryTypeIconService));
    if (serviceInstance == null)
    {
      serviceInstance = (IPicturesCache) new PicturesCache();
      ServicesManager.AddService(typeof (IPicturesCache), (object) serviceInstance);
    }
    serviceInstance.RegisterPictureFile((IThumbImageCreator) new AcadSlideCreator(), "sld", "Autocad slide");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      Consts.ImageLibraryItemTypeID = session.GetObjectType(Consts.ImageLibraryItemTypeGUID).ObjectType;
      Consts.ImageLibraryFolderTypeID = session.GetObjectType(Consts.ImageLibraryFolderTypeGUID).ObjectType;
      Consts.ImageAttTypeID = session.GetAttributeType(Consts.ImageAttributeGUID).AttributeID;
      Consts.LibImageAttTypeID = session.GetAttributeType(Consts.LibImageAttributeGUID).AttributeID;
      IViewsProvider provider = (IViewsProvider) new ImageLibraryViewProvider();
      int num = Intermech.Navigator.Consts.ImageLibraryNodeTypeID = service1.Register(Intermech.Navigator.Consts.ImageLibraryNodeGuid);
      service3.AddNodeType(num, typeof (ImageLibraryRootNode));
      service3.AddViewsProvider(num, provider);
      service3.AddViewsProvider(4, Consts.ImageLibraryItemTypeID, provider);
      bool flag = true;
      string areaId1 = session.AreaID;
      if (!string.IsNullOrEmpty(areaId1))
      {
        string areaId2 = MetaDataHelper.GetObjectType(new Guid("cad00140-306c-11d8-b4e9-00304f19f545")).AreaID;
        if (!string.IsNullOrEmpty(areaId2) && areaId1.IndexOfAny(areaId2.ToCharArray()) < 0)
          flag = false;
      }
      if (flag)
      {
        using (Stream manifestResourceStream = typeof (Consts).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.ImageLibrary.ico"))
        {
          using (Icon icon = new Icon(manifestResourceStream))
          {
            service4.AddIcon(icon, num);
            service3.AddGlobalNode(new Guid("32FA2E4A-EC83-4b2c-B7A5-EFF2C72C61F4"), (IDescriptor) new ImageLibraryRootNodeDescriptor(), 60);
          }
        }
      }
      int libraryFolderTypeId = Consts.ImageLibraryFolderTypeID;
      service3.AddNodeType(1, libraryFolderTypeId, typeof (ImageLibraryFolderNode));
      service3.AddViewsProvider(1, libraryFolderTypeId, provider);
      ThumbnailView._imageIndex = service2.ImageIndex("imgThumbnails");
    }
  }
}
