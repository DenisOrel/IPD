// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADDBAutoSetupModule
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.StandaloneView;
using Intermech.Tools;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADDBAutoSetupModule(PluginContext pluginCtx) : DBAutoSetupModule(pluginCtx)
{
  protected override void CreateStandaloneViewSettings(IIntegrator integrator)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if ((ServiceUtils.GetService<IToolSecurity>((object) sessionKeeper.Session, true).GetUserRights() & ToolSecurityRights.EditPublicSettings) == ToolSecurityRights.None)
        return;
      IStandaloneViewSettingsService service = ServiceUtils.GetService<IStandaloneViewSettingsService>((object) ServicesManager.ServiceContainer, true);
      int objectTypeId = MetaDataHelper.GetObjectTypeID(AltiumObjectTypeGuids.Project);
      if (service.TryLoadSettings(objectTypeId) == null)
      {
        StandaloneViewObjectTypeSettings settings = new StandaloneViewObjectTypeSettings()
        {
          InjectSigns = new bool?(false),
          InjectFileChecksum = new bool?(false),
          InjectedAttributes = new StandaloneViewInjectedAttributesSettings()
        };
        settings.InjectedAttributes.Enabled = false;
        service.SaveSettings(objectTypeId, settings);
      }
      int[] numArray = new int[9]
      {
        MetaDataHelper.GetObjectTypeID(AltiumObjectTypeGuids.ElectricCircuitE0),
        MetaDataHelper.GetObjectTypeID(AltiumObjectTypeGuids.ElectricCircuitE1),
        MetaDataHelper.GetObjectTypeID(AltiumObjectTypeGuids.ElectricCircuitE2),
        MetaDataHelper.GetObjectTypeID(AltiumObjectTypeGuids.ElectricCircuitE3),
        MetaDataHelper.GetObjectTypeID(AltiumObjectTypeGuids.ElectricCircuitE4),
        MetaDataHelper.GetObjectTypeID(AltiumObjectTypeGuids.ElectricCircuitE5),
        MetaDataHelper.GetObjectTypeID(AltiumObjectTypeGuids.ElectricCircuitE6),
        MetaDataHelper.GetObjectTypeID(AltiumObjectTypeGuids.ElectricCircuitE7),
        MetaDataHelper.GetObjectTypeID(AltiumObjectTypeGuids.ElectricCircuitE)
      };
      foreach (int objectType in numArray)
      {
        if (service.TryLoadSettings(objectType) == null)
        {
          StandaloneViewObjectTypeSettings settings = new StandaloneViewObjectTypeSettings()
          {
            InjectSigns = new bool?(true),
            InjectFileChecksum = new bool?(true),
            InjectedAttributes = new StandaloneViewInjectedAttributesSettings()
          };
          settings.InjectedAttributes.Enabled = true;
          service.SaveSettings(objectType, settings);
        }
      }
    }
  }
}
