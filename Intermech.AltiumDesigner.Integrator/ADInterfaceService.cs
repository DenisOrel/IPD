// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADInterfaceService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using ImSSP;
using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using Intermech.Runtime;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Tools.Integrators;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADInterfaceService(IIntegrator owner) : 
  ApplicationApiService<AddInProxy>(owner, ADConsts.ApplicationName),
  IDocumentApiService,
  IExternalApiService,
  IIntegratorService
{
  private string _applicationExePath = string.Empty;
  private readonly string _addInServerURL = "ipc://IPSAddIn/server.rem";
  private SettingsService _settingsSvc;
  private IApplicationFileTypes _fileTypeSvc;
  private IAttributeCodec _schemaDocumentCodec;
  private IAttributeCodec _pcbDocCodec;
  private IAttributeCodec _assemblyCodec;
  private IAttributeCodec _componentCodec;
  private IAttributeCodec _projectCodec;
  private OpenDocumentsApi openDocumentsApi;

  public SettingsService SettingsService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this._settingsSvc;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this._settingsSvc = value;
      }
    }
  }

  public IApplicationFileTypes FileTypeService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this._fileTypeSvc;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this._fileTypeSvc = value;
      }
    }
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.SettingsService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsService");
    if (this.FileTypeService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileTypeService");
    this._schemaDocumentCodec = (IAttributeCodec) new Intermech.AltiumDesigner.Integrator.SchemaDocumentCodec(this._settingsSvc);
    this._componentCodec = (IAttributeCodec) new SchemaComponentCodec(this._settingsSvc);
    this._assemblyCodec = (IAttributeCodec) new Intermech.AltiumDesigner.Integrator.AssemblyCodec(this._settingsSvc);
    this._projectCodec = (IAttributeCodec) new ProjectDocumentCodec(this._settingsSvc);
    this._pcbDocCodec = (IAttributeCodec) new PCBDocumentCodec(this._settingsSvc);
    this.openDocumentsApi = new OpenDocumentsApi(this._fileTypeSvc, (IExternalApiService) this);
    this.openDocumentsApi.OnFindOpenDocument += new Func<string, IOpenDocument>(this.FindOpenDocument);
    this.openDocumentsApi.OnOpenDocument += new Func<string, IOpenDocument>(this.OpenDocument);
    this.openDocumentsApi.OnValidateDocument += new Action<IOpenDocument>(this.ValidateDocument);
    this.openDocumentsApi.OnGetDocumentCodec += new Func<IOpenDocument, IAttributeCodec>(this.GetDocumentCodec);
    this.openDocumentsApi.OnGetDocumentAttributeContainer += new Func<IOpenDocument, IValueBagContainer>(this.GetDocumentAttributeContainer);
    this.openDocumentsApi.OnSaveDocument += new Action<IOpenDocument>(this.SaveDocument);
    this.openDocumentsApi.OnCloseDocument += new Action<IOpenDocument>(this.CloseDocument);
  }

  protected override bool IsInstalled() => this.GetApplicationExePath(false) != string.Empty;

  protected override bool IsRunning() => this.FindRunningAddin() != null;

  protected override AddInProxy DoCreateApplicationObject()
  {
    IIPSAddIn orCreateAddin = this.FindOrCreateAddin();
    this.CheckVersion(orCreateAddin);
    return new AddInProxy(orCreateAddin);
  }

  protected override void DoTestApplicationObject(AddInProxy proxy)
  {
    this.CheckAddinConnection(proxy.AddIn);
  }

  private void CheckAddinConnection(IIPSAddIn addin)
  {
    if (addin.GetVersion() == null)
      throw new RemotingException("Remoting object is dead.");
  }

  private void CheckVersion(IIPSAddIn addin)
  {
    string version = addin.GetVersion();
    if (version != "1.0.0.0")
      throw new ApplicationNotInstalledException(this.Integrator.DisplayName, string.Format(sc_361.ssp_altium_362(), (object) version, (object) this.ApplicationName));
  }

  private string GetApplicationExePath(bool throwException)
  {
    if (this._applicationExePath == string.Empty)
    {
      RegistryKey exePathRegistryKey = RegistryHelper.GetAltiumDesignerExePathRegistryKey(false);
      string path = exePathRegistryKey != null ? (string) exePathRegistryKey.GetValue(string.Empty) : string.Empty;
      if (path == string.Empty)
      {
        if (throwException)
          throw new Exception(string.Format(sc_361.ssp_altium_363(), (object) exePathRegistryKey.Name));
        return string.Empty;
      }
      if (!File.Exists(path))
      {
        if (throwException)
          throw new Exception(string.Format(sc_361.ssp_altium_364(), (object) path));
        return string.Empty;
      }
      this._applicationExePath = path;
    }
    return this._applicationExePath;
  }

  private IIPSAddIn FindOrCreateAddin()
  {
    try
    {
      IIPSAddIn runningAddin = this.FindRunningAddin();
      if (runningAddin == null)
      {
        string applicationExePath = this.GetApplicationExePath(true);
        new Process()
        {
          StartInfo = new ProcessStartInfo(applicationExePath)
        }.Start();
        for (int index = 0; index < 600; ++index)
        {
          try
          {
            runningAddin = this.FindRunningAddin();
            if (runningAddin != null)
            {
              Thread.Sleep(100);
              break;
            }
          }
          catch (RemotingException ex)
          {
          }
          Thread.Sleep(100);
        }
        if (runningAddin == null)
          throw new Exception(sc_361.ssp_altium_365());
      }
      return runningAddin;
    }
    catch (RemotingException ex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("Не удалось получить remoting объект для приложения {0}.", (object) this.ApplicationName);
      stringBuilder.Append(' ');
      stringBuilder.Append(ex.Message);
      throw new ApplicationProxyException(stringBuilder.ToString());
    }
  }

  private IIPSAddIn FindRunningAddin()
  {
    try
    {
      IIPSAddIn addin = (IIPSAddIn) Activator.GetObject(typeof (IIPSAddIn), this._addInServerURL);
      this.CheckAddinConnection(addin);
      return addin;
    }
    catch (RemotingException ex)
    {
      return (IIPSAddIn) null;
    }
  }

  public IAttributeCodec SchemaDocumentCodec
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this._schemaDocumentCodec;
    }
  }

  public IAttributeCodec AssemblyCodec
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this._assemblyCodec;
    }
  }

  public IAttributeCodec ComponentCodec
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this._componentCodec;
    }
  }

  public IAttributeCodec ProjectCodec
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this._projectCodec;
    }
  }

  public IAttributeCodec PCBDocCodec
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this._pcbDocCodec;
    }
  }

  public IOpenDocumentsApi OpenDocuments
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return (IOpenDocumentsApi) this.openDocumentsApi;
    }
  }

  private IOpenDocument FindOpenDocument(string fullPath)
  {
    AddInProxy applicationObject = this.GetApplicationObject();
    return applicationObject.AddIn.FindSCHObject(fullPath) == null ? (IOpenDocument) null : (IOpenDocument) new ADDocument(applicationObject, fullPath);
  }

  private IOpenDocument OpenDocument(string fullPath)
  {
    AddInProxy applicationObject = this.GetApplicationObject();
    applicationObject.OpenObject(fullPath);
    return (IOpenDocument) new ADDocument(applicationObject, fullPath);
  }

  private void ValidateDocument(IOpenDocument openDocument)
  {
    if (!(openDocument is ADDocument))
      throw new InvalidOperationException("Документы данного типа не поддерживаются интегратором.");
  }

  private void SaveDocument(IOpenDocument openDocument)
  {
    ADDocument adDocument = (ADDocument) openDocument;
    this.GetApplicationObject().AddIn.SaveObject(adDocument.FullPath);
  }

  private IAttributeCodec GetDocumentCodec(IOpenDocument openDocument)
  {
    switch (((ADDocument) openDocument).DocumentType)
    {
      case ADDocumentType.Project:
        return this._projectCodec;
      case ADDocumentType.SCH:
        return this._schemaDocumentCodec;
      case ADDocumentType.PCB:
        return this._pcbDocCodec;
      default:
        return (IAttributeCodec) null;
    }
  }

  private IValueBagContainer GetDocumentAttributeContainer(IOpenDocument openDocument)
  {
    return ((ADDocument) openDocument).Properties;
  }

  private void CloseDocument(IOpenDocument openDocument)
  {
    ADDocument adDocument = (ADDocument) openDocument;
    this.GetApplicationObject().AddIn.CloseObject(adDocument.FullPath);
  }
}
