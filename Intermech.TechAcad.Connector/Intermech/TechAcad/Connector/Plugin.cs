// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.Plugin
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Plugins;
using Intermech.Localization;
using Intermech.Protection;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.TechAcad.Interfaces;
using System;

#nullable disable
namespace Intermech.TechAcad.Connector;

public class Plugin : IPackage
{
  private static readonly string[] LineSeparators = new string[2]
  {
    "\r\n",
    "\n"
  };
  internal static string _categoryName = "";
  internal static IServiceProvider _serviceProvider = (IServiceProvider) null;
  internal static IOutputView outputView = (IOutputView) null;

  public void Load(IServiceProvider serviceProvider)
  {
    if (!(serviceProvider.GetService(typeof (ILicenser)) is ILicenser service1))
      throw new ProtectionException(LocalizationHolder.rm.GetString("TechAcad.Connector_17"));
    service1.AllocateLicense(357);
    IProtectionKey service2 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechAcadProtectionKey.b[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechAcadProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service2.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechAcadProtectionKey.b[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechAcad.Connector_9"), (object) LocalizationHolder.rm.GetString("TechAcad.Connector_3"), (object) num));
    Plugin._serviceProvider = serviceProvider;
    Plugin._categoryName = LocalizationHolder.rm.GetString(sc_19138.ssp_techacad_19139());
    Plugin.outputView = (IOutputView) serviceProvider.GetService(typeof (IOutputView));
    ApplicationServices.Container.AddService(typeof (ITechAcadService), (object) new TechAcadService());
    if (!ComHost.Configuration.ComSupportActive)
      return;
    ComHost.ActivateClassFactory(typeof (TechAcadApplication));
  }

  public void Unload()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechAcadProtectionKey.b[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechAcadProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechAcadProtectionKey.b[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechAcad.Connector_9"), (object) LocalizationHolder.rm.GetString("TechAcad.Connector_3"), (object) num));
    (ServiceUtils.GetService<ILicenser>((object) ApplicationServices.Container, false) ?? throw new ProtectionException(LocalizationHolder.rm.GetString("TechAcad.Connector_17"))).ReleaseLicense(37);
    Plugin._serviceProvider = (IServiceProvider) null;
  }

  public string Name => LocalizationHolder.rm.GetString("TechAcad.Connector_3");

  public static void LogMessage(string message)
  {
    if (Plugin.outputView == null)
      return;
    Plugin.LogMessageCore(Plugin._categoryName, message);
  }

  public static void LogError(string errorMessage)
  {
    if (Plugin.outputView == null)
      return;
    Plugin.LogMessageCore(Plugin._categoryName, errorMessage);
    Plugin.outputView.ShowView();
    Plugin.outputView.Activate(Plugin._categoryName);
  }

  private static void LogMessageCore(string category, string message)
  {
    foreach (string text in message.Split(Plugin.LineSeparators, StringSplitOptions.None))
      Plugin.outputView.WriteString(category, text);
  }
}
