// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DArticleAttributesGuard
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Diagnostics;
using Intermech.Interfaces.Client;
using Intermech.Runtime;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DArticleAttributesGuard
{
  private IAttributesLockService attributesLockService;
  private IntegratorObject integratorRef;
  private bool isStarted;

  public Drawing2DArticleAttributesGuard(
    IAttributesLockService attributesLockService,
    IntegratorObject integratorRef)
  {
    if (attributesLockService == null)
      throw new ArgumentNullException(nameof (attributesLockService));
    if (integratorRef == null)
      throw new ArgumentNullException(nameof (integratorRef));
    this.attributesLockService = attributesLockService;
    this.integratorRef = integratorRef;
  }

  public void Start()
  {
    if (this.isStarted)
      throw new InvalidOperationException($"A handler '{this.GetType()}' is already started.");
    try
    {
      this.attributesLockService.GetLockedAttributesHandler += new EventHandler<AttributesLockArgs>(this.HandleEvent);
      this.isStarted = true;
    }
    catch
    {
      this.StopInternal();
      throw;
    }
  }

  public void Stop()
  {
    if (!this.isStarted)
      return;
    this.StopInternal();
    this.isStarted = false;
  }

  private void StopInternal()
  {
    this.attributesLockService.GetLockedAttributesHandler -= new EventHandler<AttributesLockArgs>(this.HandleEvent);
  }

  private void HandleEvent(object sender, AttributesLockArgs e)
  {
    K3DIntegratorSettings integratorSettings = this.TryGetIntegratorSettings();
    if (integratorSettings == null || !integratorSettings.EnableDrawings2DSupport || e.ElementKind != AttributableElements.Object || !PDMHelper.IsArticle(e.ElementType))
      return;
    IReadOnlyList<int> typesByArticleId = e.GetIntegratorDocumentTypesByArticleId();
    if (typesByArticleId.Count == 0)
      return;
    bool? nullable = this.DocumentsAreDrawings2D((IReadOnlyCollection<int>) typesByArticleId, integratorSettings);
    if (!nullable.HasValue || !nullable.Value)
      return;
    e.LockedAttributes.Add(IDCache.Default.Designation.Id);
    e.LockedAttributes.Add(IDCache.Default.OKPCode.Id);
    e.LockedAttributes.Add(IDCache.Default.Name.Id);
    if (!e.DoesArticleHaveInstances())
      return;
    e.UnlockedAttributes.Add(IDCache.Default.Mass.Id);
    e.UnlockedAttributes.Add(IDCache.Default.Material.Id);
    e.UnlockedAttributes.Add(IDCache.Default.MaterialReplacement1.Id);
    e.UnlockedAttributes.Add(IDCache.Default.MaterialReplacement2.Id);
  }

  private K3DIntegratorSettings TryGetIntegratorSettings()
  {
    K3DSettingsService service = IntegratorServices.GetService<K3DSettingsService>(this.integratorRef, true);
    try
    {
      return service.GetSettings();
    }
    catch (Exception ex)
    {
      string currentMethodName = this.GetCurrentMethodName(nameof (TryGetIntegratorSettings));
      SuppressedExceptions.TraceException(ex, currentMethodName);
      return (K3DIntegratorSettings) null;
    }
  }

  private bool? DocumentsAreDrawings2D(
    IReadOnlyCollection<int> documentsTypes,
    K3DIntegratorSettings integratorSettings)
  {
    if (!integratorSettings.EnableDrawings2DSupport)
      return new bool?();
    bool flag = true;
    foreach (int documentsType in (IEnumerable<int>) documentsTypes)
    {
      if (!integratorSettings.PartDrawings2D.ContainsType(documentsType) && !integratorSettings.AssemblyDrawings2D.ContainsType(documentsType))
      {
        flag = false;
        break;
      }
    }
    return new bool?(flag);
  }
}
