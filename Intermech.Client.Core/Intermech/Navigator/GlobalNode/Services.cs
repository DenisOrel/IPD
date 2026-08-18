
// Type: Intermech.Navigator.GlobalNode.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Views;
using System.Drawing;
using System.IO;


namespace Intermech.Navigator.GlobalNode;

internal sealed class Services
{
  public static void Start()
  {
    Intermech.Navigator.Consts.CategoryGlobalNode = Holder.GuidMapper.Register(Intermech.Navigator.Consts.CategoryGlobalNodeGuid);
    Holder.Factory.AddNodeType(Intermech.Navigator.Consts.CategoryGlobalNode, typeof (Node));
    Holder.Factory.AddViewsProvider(Intermech.Navigator.Consts.CategoryGlobalNode, (IViewsProvider) new ViewProvider());
    if (!(ServicesManager.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service))
      return;
    using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("MainIcon.ico"))
    {
      using (Icon icon = new Icon(resourceStream))
        service.AddIcon(icon, Intermech.Navigator.Consts.CategoryGlobalNode);
    }
  }

  public static void Stop()
  {
  }
}
