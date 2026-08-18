
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.AxHostProviders
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.AxHostWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

internal static class AxHostProviders
{
  public static void Register()
  {
    System.Type type1 = typeof (ImViewerAxHost);
    AxHostFactory.Instance.Register(new Guid(type1.GetAttributeValue<AxHost.ClsidAttribute, string>((Func<AxHost.ClsidAttribute, string>) (cl => cl.Value), true)), type1);
    System.Type type2 = typeof (KGAXHost);
    AxHostFactory.Instance.Register(new Guid(type2.GetAttributeValue<AxHost.ClsidAttribute, string>((Func<AxHost.ClsidAttribute, string>) (cl => cl.Value), true)), type2);
    System.Type type3 = typeof (InventorViewControlHost);
    AxHostFactory.Instance.Register(new Guid(type3.GetAttributeValue<AxHost.ClsidAttribute, string>((Func<AxHost.ClsidAttribute, string>) (cl => cl.Value), true)), type3);
    System.Type typeFromProgId = System.Type.GetTypeFromProgID("EModelView.EModelViewControl");
    if (!(typeFromProgId != (System.Type) null))
      return;
    System.Type axHostType = typeof (AxEModelViewControlHost);
    AxHostFactory.Instance.Register(typeFromProgId.GUID, axHostType);
  }

  private static bool CheckSuitableConstructor(System.Type type)
  {
    return ((IEnumerable<ConstructorInfo>) type.GetConstructors()).Select<ConstructorInfo, ParameterInfo[]>((Func<ConstructorInfo, ParameterInfo[]>) (x => x.GetParameters())).Where<ParameterInfo[]>((Func<ParameterInfo[], bool>) (x => ((IEnumerable<ParameterInfo>) x).Count<ParameterInfo>() == 1)).SelectMany<ParameterInfo[], ParameterInfo>((Func<ParameterInfo[], IEnumerable<ParameterInfo>>) (x => (IEnumerable<ParameterInfo>) x)).Any<ParameterInfo>((Func<ParameterInfo, bool>) (x => x.ParameterType == typeof (string)));
  }
}
