// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.COM.SearchAPI
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Pdm;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Cadmech.Integrator.COM;

[ComVisible(true)]
[Guid("2669E7A9-8A63-4AB7-A7B8-E90041DC94E0")]
[ProgId("IPS.SearchAPI")]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (ISearchAPI))]
public sealed class SearchAPI : SearchAPIBase
{
  protected override void CreateFileImportContext(string fullPath, string objectTypeCode)
  {
    AcadImportVars.MechanicalOnly.Declare(true);
    if (string.IsNullOrEmpty(objectTypeCode))
      return;
    AcadImportVars.RootDocumentTypes.Declare(SearchAPI.TypeCodeToRootDocumentTypes(objectTypeCode));
  }

  private static Guid TypeCodeToRootDocumentTypes(string objectTypeCode)
  {
    if (string.IsNullOrEmpty(objectTypeCode))
      throw new ArgumentNullException();
    switch (objectTypeCode.ToLower())
    {
      case "asm":
        return MechanicalSettings.AssemblyDrawingsGroup;
      case "part":
        return MechanicalSettings.PartDrawingsGroup;
      default:
        throw new NotSupportedException($"Неизвестный подкласс документов CADMECH 2D '{objectTypeCode}'.");
    }
  }

  protected override List<LocalId<int>> GetSelectableDocumentTypes()
  {
    AcadIntegratorSettings settings = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this.Integrator, true).GetSettings();
    settings.MechanicalSettings.CheckEnabled();
    return settings.MechanicalSettings.GetAllDocumentTypes();
  }
}
