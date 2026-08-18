// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.Drawing2DArticleAttributesGuard
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Diagnostics;
using Intermech.Interfaces.Client;
using Intermech.Runtime;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

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
    AcadIntegratorSettings integratorSettings = this.TryGetIntegratorSettings();
    if (integratorSettings == null || e.ElementKind != AttributableElements.Object || !PDMHelper.IsArticle(e.ElementType))
      return;
    IReadOnlyList<int> typesByArticleId = e.GetIntegratorDocumentTypesByArticleId();
    if (typesByArticleId.Count == 0)
      return;
    bool? nullable = this.DocumentsAreDrawings2D((IReadOnlyCollection<int>) typesByArticleId, integratorSettings);
    if (!nullable.HasValue || !nullable.Value || !e.DoesArticleHaveInstances())
      return;
    e.UnlockedAttributes.Add(IDCache.Default.Mass.Id);
    e.UnlockedAttributes.Add(IDCache.Default.Material.Id);
    e.UnlockedAttributes.Add(IDCache.Default.MaterialReplacement1.Id);
    e.UnlockedAttributes.Add(IDCache.Default.MaterialReplacement2.Id);
  }

  private AcadIntegratorSettings TryGetIntegratorSettings()
  {
    AcadIntegratorSettingsService service = IntegratorServices.GetService<AcadIntegratorSettingsService>(this.integratorRef, true);
    try
    {
      return service.GetSettings();
    }
    catch (Exception ex)
    {
      string currentMethodName = this.GetCurrentMethodName(nameof (TryGetIntegratorSettings));
      SuppressedExceptions.TraceException(ex, currentMethodName);
      return (AcadIntegratorSettings) null;
    }
  }

  private bool? DocumentsAreDrawings2D(
    IReadOnlyCollection<int> documentsTypes,
    AcadIntegratorSettings integratorSettings)
  {
    bool flag = true;
    foreach (int documentsType in (IEnumerable<int>) documentsTypes)
    {
      if (integratorSettings.MechanicalSettings.IsEnabled)
        integratorSettings.MechanicalSettings.GetGroupTypeByDrawingType(documentsType, false);
      else if (integratorSettings.ConstructionalSettings.IsEnabled)
      {
        integratorSettings.ConstructionalSettings.GetGroupTypeByDrawingType(documentsType, false);
      }
      else
      {
        flag = false;
        break;
      }
    }
    return new bool?(flag);
  }
}
