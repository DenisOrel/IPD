// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.AreaImportAttributeHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class AreaImportAttributeHandler : ImportAttributeHandler
{
  public override void Handle(SpecHandleAttributeEventArgs e, Dictionary<string, object> tag)
  {
    string briefSubjAreas = Convert.ToString(e.Value.StringValue);
    object MetaData;
    if (tag != null && tag.TryGetValue("metadata", out MetaData))
    {
      e.Value.StringValue = (object) Intermech.Kernel.Briefcase.Helper.GetConformitySubjectAreas(e.Session, (DataSet) MetaData, briefSubjAreas);
    }
    else
    {
      string empty = string.Empty;
      foreach (char aSubjectAreaTypeID in briefSubjAreas.ToCharArray())
      {
        if (e.Session.GetSubjectAreaType(aSubjectAreaTypeID, false) != null)
          empty += aSubjectAreaTypeID.ToString();
      }
      e.Value.StringValue = (object) empty;
    }
    e.Handled = true;
  }
}
