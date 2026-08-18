// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.InfoRequiredService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.WebPortal;


namespace Intermech.Kernel.Services.PortalServices;

internal abstract class InfoRequiredService
{
  protected bool infoRequired;

  public InfoRequiredService(bool infoRequired) => this.infoRequired = infoRequired;

  protected void AddReasonInfo(PublishCompositionObject pco, string info)
  {
    if (!this.infoRequired)
      return;
    if (pco.ReasonInfo != null && pco.ReasonInfo != string.Empty)
      pco.ReasonInfo += ", ";
    else
      pco.ReasonInfo = string.Empty;
    pco.ReasonInfo += info;
  }

  protected void AddNoAccessTypeMessage(PublishCompositionObject pco)
  {
    this.AddReasonInfo(pco, "Объект запрещен к публикации по настройкам публикуемых типов объектов и связей");
  }

  protected void AddNoAccessMessage(PublishCompositionObject pco)
  {
    this.AddReasonInfo(pco, "Объект запрещен к публикации по уровню доступа");
  }

  protected void AddForbiddenMessage(PublishCompositionObject pco)
  {
    this.AddReasonInfo(pco, "Объект запрещен к публикации пользователем");
  }

  protected void AddFilteredOTDMessage(PublishCompositionObject pco)
  {
    this.AddReasonInfo(pco, "Объект запрещен к публикации по листу рассылки ОТД");
  }

  protected void AddNoChangedsMessage(PublishCompositionObject pco)
  {
    this.AddReasonInfo(pco, "С момента последней публикации изменения объекта не производились");
  }

  protected bool HandleFilterIncludes(PublishCompositionObject pco, bool withoutMessage)
  {
    if (pco.Include == IncludeTypes.FilteredByTypes)
    {
      if (!withoutMessage)
        this.AddNoAccessTypeMessage(pco);
    }
    else if (pco.Include == IncludeTypes.NoAccess)
    {
      if (!withoutMessage)
        this.AddNoAccessMessage(pco);
    }
    else if (pco.Include == IncludeTypes.Forbidden)
    {
      if (!withoutMessage)
        this.AddForbiddenMessage(pco);
    }
    else if (pco.Include == IncludeTypes.FilteredByOTD)
    {
      if (!withoutMessage)
        this.AddFilteredOTDMessage(pco);
    }
    else
    {
      if (pco.Include != IncludeTypes.FilteredCompositionByOTD)
        return false;
      if (!withoutMessage)
        this.AddFilteredOTDMessage(pco);
    }
    return true;
  }
}
