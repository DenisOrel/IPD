// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgCreator.DwgCreatorProvider
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Cadmech.Integrator.DwgCreator;

internal sealed class DwgCreatorProvider
{
  private IObjectCreatorService objectCreatorService;
  private Lazy<List<int>> documentListCache;
  private bool isEnabled;

  public DwgCreatorProvider(IObjectCreatorService objectCreatorService)
  {
    this.objectCreatorService = objectCreatorService != null ? objectCreatorService : throw new ArgumentNullException(nameof (objectCreatorService));
    this.documentListCache = new Lazy<List<int>>(new Func<List<int>>(this.GetPartDrawingDocumentTypes));
  }

  public bool Enabled
  {
    [DebuggerStepThrough] get => this.isEnabled;
    set
    {
      if (this.isEnabled == value)
        return;
      this.BeforeEnabledChanged(value);
      this.isEnabled = value;
    }
  }

  private void BeforeEnabledChanged(bool newValue)
  {
    if (newValue)
      this.objectCreatorService.SelectCustomServiceEvent += new EventHandler<ObjectCreatorCustomServiceEventArgs>(this.SelectCustomServiceHandler);
    else
      this.objectCreatorService.SelectCustomServiceEvent -= new EventHandler<ObjectCreatorCustomServiceEventArgs>(this.SelectCustomServiceHandler);
  }

  private void SelectCustomServiceHandler(object sender, ObjectCreatorCustomServiceEventArgs e)
  {
    if (e.Handled || !this.documentListCache.Value.Contains(e.ObjectTypeId))
      return;
    e.CustomServiceType = typeof (Intermech.Cadmech.Integrator.DwgCreator.DwgCreator);
    e.Handled = true;
  }

  private List<int> GetPartDrawingDocumentTypes()
  {
    return MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00261-306c-11d8-b4e9-00304f19f545"));
  }
}
