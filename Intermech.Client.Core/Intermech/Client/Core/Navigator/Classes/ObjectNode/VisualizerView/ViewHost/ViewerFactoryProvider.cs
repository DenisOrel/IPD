
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.ViewerFactoryProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;
using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers;
using Intermech.Interfaces;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost;

internal static class ViewerFactoryProvider
{
  public static void Register()
  {
    ViewerFactory.Instance.Register(StyleView.Native, typeof (Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer.MapViewer));
    ViewerFactory.Instance.Register(StyleView.Internal, typeof (InternalViewer));
    ViewerFactory.Instance.Register(StyleView.ActiveX, typeof (ActiveXViewer));
    ViewerFactory.Instance.Register(StyleView.PreView, typeof (Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer.PreviewHandlerViewer));
    ViewerFactory.Instance.Register(StyleView.PrevThumbnail, typeof (ThumbnailViewer));
    ViewerFactory.Instance.Register(StyleView.ExtractImage, typeof (Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.ExtractImageViewer.ExtractImageViewer));
    ViewerFactory.Instance.Register(StyleView.Shell, typeof (ShellViewer));
    ViewerFactory.Instance.Register(StyleView.Default, typeof (DefaultViewer));
    ViewerFactory.Instance.Register(StyleView.CommandLine, typeof (CommandLineViewer));
    ViewerFactory.Instance.Register(StyleView.InternalExtractView, typeof (PreviewExtractorViewer));
  }
}
