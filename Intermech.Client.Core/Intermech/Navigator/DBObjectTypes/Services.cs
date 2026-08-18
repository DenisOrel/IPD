
// Type: Intermech.Navigator.DBObjectTypes.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Drawing;
using System.IO;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>Внутренний класс</summary>
internal sealed class Services
{
  /// <summary>Инициализировать</summary>
  public static void Start()
  {
    ICurrentUserAndRole service = ServiceUtils.GetService<ICurrentUserAndRole>((object) ApplicationServices.Container, true);
    Holder.ColumnSchemes.Register(Intermech.Navigator.Consts.ObjectTypeColumnSchemeGuid, (INodeColumnScheme) new ObjectTypeColumnScheme());
    Holder.Factory.AddNodeType(4, typeof (ObjectTypeNode), Intermech.Navigator.DB.Helper.TypeInheritance);
    Holder.Factory.AddCommandsProvider(4, (ICommandsProvider) new ObjectTypeContextMenuProvider(service));
    Holder.Factory.AddViewsProvider(4, (IViewsProvider) new ObjectTypeViewsProvider());
    Intermech.Navigator.Consts.CategoryAllObjectTypes = Holder.GuidMapper.Register(Intermech.Navigator.Consts.CategoryAllObjectTypesGuid);
    Holder.Factory.AddNodeType(Intermech.Navigator.Consts.CategoryAllObjectTypes, typeof (AllObjectTypesNode));
    Holder.Factory.AddViewsProvider(Intermech.Navigator.Consts.CategoryAllObjectTypes, (IViewsProvider) new AllObjectTypesViewsProvider());
    Intermech.Navigator.Consts.CategoryObjectTypes = Holder.GuidMapper.Register(Intermech.Navigator.Consts.CategoryObjectTypesGuid);
    Holder.Factory.AddNodeType(Intermech.Navigator.Consts.CategoryObjectTypes, typeof (ObjectTypesNode));
    Holder.Factory.AddViewsProvider(Intermech.Navigator.Consts.CategoryObjectTypes, (IViewsProvider) new ObjectTypesViewsProvider());
    Holder.Factory.AddGlobalNode(new Guid("16FD0592-7203-4c58-998E-BE3E43453024"), (IDescriptor) new ObjectTypesNodeDescriptor(), 40);
    using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("ObjectTypes.ico"))
    {
      using (Icon icon = new Icon(resourceStream))
        Holder.IconService.AddIcon(icon, Intermech.Navigator.Consts.CategoryObjectTypes, 0, (object) null);
    }
  }
}
