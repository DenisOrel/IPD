
// Type: Intermech.Navigator.CustomNode.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Views;


namespace Intermech.Navigator.CustomNode;

internal sealed class Services
{
  public static void Start()
  {
    Intermech.Navigator.Consts.CategoryCustomNode = Holder.GuidMapper.Register(Intermech.Navigator.Consts.CategoryCustomNodeGuid);
    Holder.Factory.AddNodeType(Intermech.Navigator.Consts.CategoryCustomNode, typeof (Node));
    Holder.Factory.AddViewsProvider(Intermech.Navigator.Consts.CategoryCustomNode, (IViewsProvider) new ViewsProvider());
  }

  public static void Stop()
  {
  }
}
