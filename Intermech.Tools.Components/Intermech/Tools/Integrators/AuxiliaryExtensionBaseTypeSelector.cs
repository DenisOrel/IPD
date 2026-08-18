// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.AuxiliaryExtensionBaseTypeSelector
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

public sealed class AuxiliaryExtensionBaseTypeSelector : AuxiliaryDocumentTypeSelectorBase
{
  public AuxiliaryExtensionBaseTypeSelector()
    : base(true)
  {
  }

  protected override int[] GetPossibleDocumentTypes(SectionEntity docItem)
  {
    FilesSection filesSection = docItem.Sections.Get<FilesSection>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IDocumentTypeSettingsService>((object) sessionKeeper.Session, true).GetDocumentTypesByFileExt(sessionKeeper.Session.SessionGUID, Path.GetExtension(filesSection.MasterFile));
  }
}
