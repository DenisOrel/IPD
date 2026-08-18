// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.Integrator.AcadAuthenticFilesService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Files;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Cadmech.Integrator.Integrator;

internal class AcadAuthenticFilesService : IntegratorService, IAuthenticFilesService
{
  private CadApiService _apiService;
  private IFileVault _fileVaultService;

  public AcadAuthenticFilesService(IIntegrator owner, IFileVault fileVaultService)
    : base(owner)
  {
    this._fileVaultService = fileVaultService;
  }

  public IFileVault FileVaultService
  {
    get => this._fileVaultService;
    set => this._fileVaultService = value;
  }

  public CadApiService ApiService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this._apiService;
    }
    set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this._apiService = value;
      }
    }
  }

  public ICollection<string> GetPossibleFileTypes(int documentType)
  {
    return (ICollection<string>) new List<string>()
    {
      ".pdf"
    };
  }

  public string MakeFilePath(string documentFilePath, string authenticFileType)
  {
    if (string.IsNullOrEmpty(documentFilePath))
      throw new ArgumentNullException(nameof (documentFilePath));
    if (string.IsNullOrEmpty(authenticFileType))
      throw new ArgumentNullException(nameof (authenticFileType));
    this.RequireReadyState();
    return documentFilePath + authenticFileType;
  }

  public void MakeFile(string documentFilePath, string authenticFilePath)
  {
    if (string.IsNullOrEmpty(documentFilePath))
      throw new ArgumentNullException(nameof (documentFilePath));
    if (string.IsNullOrEmpty(authenticFilePath))
      throw new ArgumentNullException(nameof (authenticFilePath));
    if (this.FileVaultService == null)
      throw new ArgumentNullException("FileVaultService");
    this.RequireReadyState();
    using (AcadApiSession acadApiSession = new AcadApiSession((IApplicationApiService) this.ApiService))
    {
      ICadDocumentProxy cadDocumentProxy = acadApiSession.Application.OpenDocument(documentFilePath);
      try
      {
        CadmechRootProxy cadmechRootProxy;
        try
        {
          cadmechRootProxy = CadmechRootProxy.Create(false);
        }
        catch
        {
          cadmechRootProxy = (CadmechRootProxy) null;
        }
        cadDocumentProxy.ExportToPDF(authenticFilePath, this.FileVaultService.TempArea.AreaPath, cadmechRootProxy != null);
      }
      finally
      {
        cadDocumentProxy.Close(false);
      }
    }
  }
}
