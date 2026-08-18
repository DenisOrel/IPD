// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.GetCompositionFiltrationCommandEventArgs
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces.Client;
using Intermech.Search;

#nullable disable
namespace Intermech.Interfaces.Pdm;

public class GetCompositionFiltrationCommandEventArgs
{
  public IFiltrationService Filtration;
  public IMainMenuService MainMenuService;
  public INotificationService NotificationService;

  public GetCompositionFiltrationCommandEventArgs(
    IFiltrationService filtration,
    IMainMenuService mainMenuService,
    INotificationService notificationService)
  {
    this.Filtration = filtration;
    this.MainMenuService = mainMenuService;
    this.NotificationService = notificationService;
  }
}
