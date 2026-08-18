// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Plugin
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Plugins;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Document.Client.Configs.Visual;
using Intermech.TechCard.Document.Client.Navigator.Commands;
using Intermech.TechCard.Document.Client.Setup;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services;
using Intermech.TechCard.Document.Interfaces.Generate.Interfaces;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Document.Client;

public class Plugin : IPackage, IConfigurable
{
  public string Name => LocalizationHolder.rm.GetString("TechCard.Document_013");

  public void Load(System.IServiceProvider serviceProvider)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (ITechCardDocumentService)) is ITechCardDocumentService))
      {
        this.Unload();
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Document_189"), LocalizationHolder.rm.GetString("TechCard.Document_190"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
    }
    BlankSetupObjectCreator.Register(ServiceUtils.GetService<IObjectCreatorService>((object) serviceProvider, true));
    IFactory service1 = ServiceUtils.GetService<IFactory>((object) serviceProvider, true);
    DocumentTechCardProvider.Register(service1);
    BlankSetupCommandProvider.Register(service1);
    ApplicationServices.Container.AddService<TechCardDocumentConfigSerializeService>(new TechCardDocumentConfigSerializeService());
    ApplicationServices.Container.AddService<TechCardDocumentConfigLoadService>(new TechCardDocumentConfigLoadService());
    IContentProvider service2 = ServiceUtils.GetService<IContentProvider>((object) ApplicationServices.Container, false);
    if (service2 == null)
      return;
    service2.ContentCallback += new GetContentCallback(BlankSetupPage.RestoreWindowCallback);
  }

  public void Unload()
  {
    BlankSetupObjectCreator.UnRegister(ServiceUtils.GetService<IObjectCreatorService>((object) ApplicationServices.Container, true));
  }

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
  }
}
