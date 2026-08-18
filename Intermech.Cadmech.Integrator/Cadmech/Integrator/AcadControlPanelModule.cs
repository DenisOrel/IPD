// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadControlPanelModule
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadControlPanelModule : InitializerModule
{
  private IToolsControlPanel controlPanel;
  private CheckBox disableCreateArticlesOnPartDrawings;

  public bool CanInitialize()
  {
    return ServiceUtils.IsServiceAvailable((object) ServicesManager.ServiceContainer, typeof (IToolsControlPanel));
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.controlPanel = ServiceUtils.GetService<IToolsControlPanel>((object) ServicesManager.ServiceContainer, true);
    this.disableCreateArticlesOnPartDrawings = new CheckBox();
    this.disableCreateArticlesOnPartDrawings.Text = "Отключить создание изделий по чертежам деталей";
    this.disableCreateArticlesOnPartDrawings.DataBindings.Add(new Binding("Checked", (object) RuntimeOptions.DisableExtendedSave, "GlobalValue", false, DataSourceUpdateMode.OnPropertyChanged));
    this.disableCreateArticlesOnPartDrawings.AutoSize = true;
    this.controlPanel.AddItem(AcadConsts.IntegratorName, (Control) this.disableCreateArticlesOnPartDrawings);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this.controlPanel == null)
      return;
    this.controlPanel = (IToolsControlPanel) null;
  }
}
