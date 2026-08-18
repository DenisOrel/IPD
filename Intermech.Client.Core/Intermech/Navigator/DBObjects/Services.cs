
// Type: Intermech.Navigator.DBObjects.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Data;
using System.Drawing;


namespace Intermech.Navigator.DBObjects;

internal sealed class Services
{
  private static Image stateVarianceBitmap;
  private static Image stateCorrespondingBitmap;

  public static void Start()
  {
    Holder.ColumnSchemes.Register(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (INodeColumnScheme) new ObjectObligatoryColumnScheme());
    Holder.ColumnSchemes.Register(Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid, (INodeColumnScheme) new RelationObligatoryColumnScheme());
    Holder.ColumnSchemes.Register(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (INodeColumnScheme) new ObjectColumnScheme());
    Holder.ColumnSchemes.Register(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (INodeColumnScheme) new CurrentObjectColumnScheme());
    Holder.ColumnSchemes.Register(Intermech.Navigator.Consts.RelationColumnSchemeGuid, (INodeColumnScheme) new RelationColumnScheme());
    Holder.ColumnSchemes.Register(Intermech.Navigator.Consts.CurrentRelationColumnSchemeGuid, (INodeColumnScheme) new CurrentRelationColumnScheme());
    Holder.ImageService.FindStateImage += new FindStateImageEventHandler(Services.FindObjectStateImage);
    Holder.Factory.AddNodeType(1, typeof (ObjectNode), Intermech.Navigator.DB.Helper.TypeInheritance);
    ContextCommandProvider provider = new ContextCommandProvider();
    Holder.Factory.AddCommandsProvider(1, (ICommandsProvider) provider);
    Holder.Factory.AddViewsProvider(1, (IViewsProvider) new ViewProvider());
    Holder.Factory.AddViewsProvider(1, (IViewsProvider) new SecurityProvider());
    Holder.Factory.AddCommandsProvider((ICommandsProvider) new ContextSearchCommandProvider());
    Holder.Factory.AddCommandsProvider(1, (ICommandsProvider) new ViewWithOptionsCommandProvider());
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(sessionKeeper.Session.IdentHelper.WorkspaceTypeID, new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(-7, RelationalOperators.Equal, (object) sessionKeeper.Session.IdentHelper.WorkspaceTypeID, LogicalOperators.AND, 0, false),
        new ConditionStructure(-8, RelationalOperators.Equal, (object) sessionKeeper.Session.UserID, LogicalOperators.NONE, 0, false)
      })
      {
        Columns = new object[2]{ (object) -2, (object) -50 }
      });
      if (dataTable.Rows.Count > 0)
        Holder.Factory.AddGlobalNode(new Guid("EC776C91-A26A-415e-9F1E-D65FF75CDC88"), (IDescriptor) new Descriptor(Convert.ToInt64(dataTable.Rows[0][0])), 10);
      Holder.Factory.AddGlobalNode(Intermech.Navigator.Consts.CategoryCurrentProjectNodeGuid, (IDescriptor) new CurrentProjectNodeDescriptor(), 15);
      Holder.Factory.AddGlobalNode(Intermech.Navigator.Consts.CategoryCurrentContextNodeGuid, (IDescriptor) new CurrentContextNodeDescriptor(), 15);
      provider.AssignSystemGuidCommandHandler = new AssignSystemGuidCommandHandler();
      provider.AssignSystemGuidCommandHandler.Initialize(sessionKeeper.Session);
    }
  }

  public static void Stop()
  {
    if (Services.stateVarianceBitmap != null)
    {
      Services.stateVarianceBitmap.Dispose();
      Services.stateVarianceBitmap = (Image) null;
    }
    if (Services.stateCorrespondingBitmap == null)
      return;
    Services.stateCorrespondingBitmap.Dispose();
    Services.stateCorrespondingBitmap = (Image) null;
  }

  private static Image FindObjectStateImage(int categoryId, int typeId, object data, object state)
  {
    if (state is ObjectFiltrationState objectFiltrationState)
    {
      if (objectFiltrationState == ObjectFiltrationState.fsVariance)
        return Services.StateVarianceBitmap;
      if (objectFiltrationState == ObjectFiltrationState.fsCorresponding)
        return Services.StateCorrespondingBitmap;
    }
    return (Image) null;
  }

  private static Image StateVarianceBitmap
  {
    get
    {
      if (Services.stateVarianceBitmap == null)
        Services.stateVarianceBitmap = Services.GetCenterBitmap("StateVariance.ico");
      return Services.stateVarianceBitmap;
    }
  }

  private static Image StateCorrespondingBitmap
  {
    get
    {
      if (Services.stateCorrespondingBitmap == null)
        Services.stateCorrespondingBitmap = Services.GetCenterBitmap("StateCorresponding.ico");
      return Services.stateCorrespondingBitmap;
    }
  }

  private static Image GetCenterBitmap(string iconName)
  {
    Image image = (Image) new Bitmap(7, 16 /*0x10*/);
    using (Graphics graphics = Graphics.FromImage(image))
    {
      using (Brush brush = (Brush) new SolidBrush(Color.Transparent))
        graphics.FillRectangle(brush, 0, 0, 7, 16 /*0x10*/);
      using (Image bitmap = (Image) new Icon(Intermech.Navigator.Services.GetResourceStream(iconName)).ToBitmap())
        graphics.DrawImageUnscaled(bitmap, 0, 5);
    }
    return image;
  }
}
