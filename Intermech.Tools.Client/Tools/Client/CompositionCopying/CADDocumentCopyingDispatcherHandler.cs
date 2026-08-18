// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.CADDocumentCopyingDispatcherHandler
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Client.CompositionCopying.Model;
using Intermech.Tools.Client.CompositionCopying.Model.CAD;
using Intermech.Tools.Client.CompositionCopying.Model.Operations;
using Intermech.Tools.Client.CompositionCopying.Views;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.UI.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying;

internal sealed class CADDocumentCopyingDispatcherHandler
{
  private RootDocumentTypesCache rootDocumentTypesCache;
  private ICopyingSessionServices copyingSessionServices;
  private ICompositionCopyingWizardServices wizardServices;

  public CADDocumentCopyingDispatcherHandler(
    RootDocumentTypesCache rootDocumentTypesCache,
    ICopyingSessionServices copyingSessionServices,
    ICompositionCopyingWizardServices wizardServices)
  {
    this.rootDocumentTypesCache = rootDocumentTypesCache;
    this.copyingSessionServices = copyingSessionServices;
    this.wizardServices = wizardServices;
  }

  public void FindHandlerBySelectedItems(object sender, FindCompositionCopyingHandlerEventArgs e)
  {
    if (e == null)
      throw new ArgumentNullException(nameof (e));
    if (e.Handler != null || e.Items.Count != 1)
      return;
    IDBTypedObjectID rootObjectInfo = (IDBTypedObjectID) e.Items.GetItemData(0, typeof (IDBTypedObjectID));
    if (rootObjectInfo == null || !this.rootDocumentTypesCache.DocumentTypes.Contains(rootObjectInfo.ObjectType))
      return;
    e.Handler = (Action) (() => this.CreateCompositionByPrototype(rootObjectInfo));
  }

  private void CreateCompositionByPrototype(IDBTypedObjectID rootObjectInfo)
  {
    DBObjectRecord rootDocument = new DBObjectRecord(rootObjectInfo.ObjectID, rootObjectInfo.ObjectType, rootObjectInfo.Caption);
    IntegratorObject integratorObject = IntegratorServices.Find(rootObjectInfo.ObjectType);
    string text = this.CheckPrerequisites(rootDocument, integratorObject);
    if (text != null)
    {
      int num1 = (int) MessageBox.Show(text, "Состав по прототипу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      ICADSettingsService service = IntegratorServices.GetService<ICADSettingsService>(integratorObject, true);
      CADSettings cadSettings = service.GetCADSettings();
      long uniqueID = DateTime.UtcNow.Ticks;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        uniqueID = ServiceUtils.GetService<IFileNamesService>((object) sessionKeeper.Session, true).GetNextFileID(sessionKeeper.Session.SessionGUID);
      CADHeuristics integratorHeuristics = this.CreateIntegratorHeuristics(service.Integrator, this.copyingSessionServices);
      CopyingSession session = new CopyingSession(uniqueID, this.copyingSessionServices, service.Integrator, cadSettings, integratorHeuristics);
      session.VersionsRule = VersionsRuleSources.GetEditorRule();
      this.BuildSessionGraph(session, rootDocument);
      this.ApplyCopyingSelectorHeuristics(session);
      CompositionCopyingWizardVM compositionCopyingWizardVm = new CompositionCopyingWizardVM(session, this.wizardServices);
      using (CompositionCopyingWizardForm copyingWizardForm = new CompositionCopyingWizardForm())
      {
        copyingWizardForm.MainViewModel = (WizardVM) compositionCopyingWizardVm;
        int num2 = (int) copyingWizardForm.ShowDialog();
      }
    }
  }

  private string CheckPrerequisites(DBObjectRecord rootDocument, IntegratorObject integratorRef)
  {
    if (IntegratorServices.GetService<ICompositionCopyingService>(integratorRef, false) == null)
      return $"Команда не может быть выполнена, так как в CAD-интерфейсе для '{rootDocument.Caption}' отсутствует поддержка копирования составных документов.";
    ICADInterfaceService service = IntegratorServices.GetService<ICADInterfaceService>(integratorRef, false);
    return service == null || !service.IsApplicationInstalled ? $"Команда не может быть выполнена, так как в CAD-система для '{rootDocument.Caption}' не установлена на этом компьютере." : (string) null;
  }

  private void BuildSessionGraph(CopyingSession session, DBObjectRecord rootDocument)
  {
    CADSettings integratorSettings = session.IntegratorSettings;
    CADDocumentGraphBuilder documentGraphBuilder = new CADDocumentGraphBuilder(session, rootDocument);
    foreach (DocumentGroup fileDocumentGroup in (Collection<DocumentGroup>) integratorSettings.FileDocumentGroups)
      documentGraphBuilder.DocumentTypes.AddRange<int>((IEnumerable<int>) fileDocumentGroup.AsIdList());
    if (integratorSettings.StandardPartType != null)
      documentGraphBuilder.DocumentTypes.Add(integratorSettings.StandardPartType.Id);
    documentGraphBuilder.Build();
    session.DeferredEventDispatcher.RaiseAll();
  }

  private void ApplyCopyingSelectorHeuristics(CopyingSession session)
  {
    foreach (CopyingSelectorHeuristics selectorHeuristics in this.CreateCopyingSelectorHeuristicsList(session.IntegratorSettings))
      selectorHeuristics.Apply(session);
    session.DeferredEventDispatcher.RaiseAll();
  }

  private List<CopyingSelectorHeuristics> CreateCopyingSelectorHeuristicsList(
    CADSettings cadSettings)
  {
    List<CopyingSelectorHeuristics> selectorHeuristicsList = new List<CopyingSelectorHeuristics>();
    if (cadSettings.StandardPartType != null)
    {
      selectorHeuristicsList.Add((CopyingSelectorHeuristics) new DenyCopyingForCadmechStandardParts(cadSettings.StandardPartType.Id));
      selectorHeuristicsList.Add((CopyingSelectorHeuristics) new DenyCopyingForUsersStandardParts(cadSettings.StandardPartType.Id));
    }
    selectorHeuristicsList.Add((CopyingSelectorHeuristics) new DenyCopyingForMinorMaterialModels(this.GetAllMaterialsTypes()));
    selectorHeuristicsList.Add((CopyingSelectorHeuristics) new DenyCopyingForNonCADDocuments(this.GetAllCADDocumentTypes(cadSettings)));
    selectorHeuristicsList.Add((CopyingSelectorHeuristics) new ForceCopyingForRootCADDocument());
    return selectorHeuristicsList;
  }

  private ICollection<int> GetAllCADDocumentTypes(CADSettings cadSettings)
  {
    List<int> cadDocumentTypes = new List<int>(16 /*0x10*/);
    foreach (DocumentGroup fileDocumentGroup in (Collection<DocumentGroup>) cadSettings.FileDocumentGroups)
    {
      if (((IEnumerable<string>) fileDocumentGroup.Flags).Contains<string>("model") || ((IEnumerable<string>) fileDocumentGroup.Flags).Contains<string>("drawing"))
      {
        foreach (GlobalId<int> documentType in fileDocumentGroup.DocumentTypes)
          cadDocumentTypes.Add(documentType.Id);
      }
    }
    if (cadSettings.StandardPartType != null)
      cadDocumentTypes.Add(cadSettings.StandardPartType.Id);
    return (ICollection<int>) cadDocumentTypes;
  }

  private ICollection<int> GetAllMaterialsTypes()
  {
    return (ICollection<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(this.copyingSessionServices.IntegratorsIDCache.AllMaterials.Id);
  }

  private CADHeuristics CreateIntegratorHeuristics(
    IIntegrator integrator,
    ICopyingSessionServices services)
  {
    if (integrator.Id == new Guid("A6C782D1-DDF3-4d85-9F5F-A3F5148127B4"))
      return (CADHeuristics) new AIHeuristics(integrator, services);
    if (integrator.Id == new Guid("FDBE0FD7-D10B-41f6-99CC-9841FF2D52F8"))
      return (CADHeuristics) new SWHeuristics(integrator, services);
    return integrator.Id == new Guid("713D84FC-EDD2-4F39-A121-08F4CE1C357E") ? (CADHeuristics) new NXHeuristics(integrator, services) : (CADHeuristics) new AgnosticHeuristics(integrator, services);
  }
}
