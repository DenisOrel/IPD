// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.COM.SpdsAPI
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Pdm;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Cadmech.Integrator.COM;

[ComVisible(true)]
[Guid("C75A6B0F-E8DC-4280-96BC-1A03BB195BAA")]
[ProgId("IPS.SpdsAPI")]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (ISearchAPI))]
public sealed class SpdsAPI : SearchAPIBase
{
  protected override void CreateFileImportContext(string fullPath, string objectTypeCode)
  {
    AcadImportVars.ConstructionalOnly.Declare(true);
  }

  protected override List<LocalId<int>> GetSelectableDocumentTypes()
  {
    AcadIntegratorSettings settings = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this.Integrator, true).GetSettings();
    settings.ConstructionalSettings.CheckEnabled();
    return settings.ConstructionalSettings.GetAllDocumentTypes();
  }
}
