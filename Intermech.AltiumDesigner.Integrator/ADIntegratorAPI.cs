// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADIntegratorAPI
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Commands;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Navigator.Controls;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.Tools.Integrators;
using Intermech.UI;
using System;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

[ComVisible(true)]
[Guid("1AFDC66E-9556-4D41-9943-E22E8C1CB306")]
[ProgId("IPS.ADIntegratorAPI")]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (IADIntegratorAPI))]
public class ADIntegratorAPI : SingleThreadedObject, IADIntegratorAPI
{
  public void CreateElementList(string projectFile)
  {
    this.Prepare();
    try
    {
      long documentId = this.FindDocumentId(projectFile, true);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        new ADElementList().Create(sessionKeeper.Session, documentId);
    }
    catch (Exception ex)
    {
      this.SetError(ex);
    }
  }

  public void CreateSpecification(string projectFile)
  {
    this.Prepare();
    try
    {
      long documentId = this.FindDocumentId(projectFile, true);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.CreateSpecificationWindow((ServiceUtils.GetService<IArticleService>((object) ServicesManager.ServiceContainer, true).FindBaseArticle(documentId, VersionsRuleSources.GetEditorRule().OwnerId, (object) sessionKeeper.Session) ?? throw new Exception($"Для документа не найдено базовое исполнение. Выполните для {sessionKeeper.Session.GetObject(documentId).NameInMessages} расширенное сохранение.")).ObjectID);
    }
    catch (Exception ex)
    {
      this.SetError(ex);
    }
  }

  public void ImportProject(string projectFile)
  {
    this.Prepare();
    try
    {
      this.CreateFileDocument(projectFile);
    }
    catch (Exception ex)
    {
      this.SetError(ex);
    }
  }

  public void SaveChanges(string projectFile)
  {
    this.Prepare();
    try
    {
      long documentId = this.FindDocumentId(projectFile, true);
      ObjectCommand saveChangesCommand = ObjectCommandFactory.CreateSaveChangesCommand(true);
      saveChangesCommand.ObjectId = documentId;
      saveChangesCommand.UpdateUI = false;
      saveChangesCommand.Execute();
    }
    catch (Exception ex)
    {
      this.SetError(ex);
    }
  }

  public void ExtendedSave(string projectFile)
  {
    this.Prepare();
    try
    {
      long projectID = this.FindDocumentId(projectFile, true);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(projectID);
        IExtendedSaveSupport saveSvc = ServiceUtils.GetService<IExtendedSaveSupport>((object) ClientContext.Integrators.GetIntegrator(new IntegratorObject(ADConsts.IntegratorId, ADConsts.IntegratorName), true), true);
        ProgressSinks.DialogService.Invoke($"Расширенное сохранение в {dbObject.NameInMessages}", ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink => saveSvc.CaptureChanges(projectID, new ExtendedSaveOptions(SaveChangesMode.Default)
        {
          WorkAreaPolicy = (IReplaceFilePolicy) new PreserveAnyFile(),
          ProgressSink = progressSink
        })));
      }
    }
    catch (Exception ex)
    {
      this.SetError(ex);
    }
  }

  public void ViewDocumentProperties(string projectFile)
  {
    this.Prepare();
    try
    {
      long documentId = this.FindDocumentId(projectFile, true);
      int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, documentId);
    }
    catch (Exception ex)
    {
      this.SetError(ex);
    }
  }

  protected void CreateFileDocument(string documentPath)
  {
    if (string.IsNullOrEmpty(documentPath))
      throw new ArgumentException("Не задан путь к регистрируемому документу.", nameof (documentPath));
    if (!File.Exists(documentPath))
      throw new FileNotFoundException($"Файл '{documentPath}' не найден на диске, его регистрация в IPS невозможна.");
    ServiceUtils.GetService<IFileImportService>((object) ServicesManager.ServiceContainer, true).ImportFile(documentPath);
  }

  protected long FindDocumentId(string documentPath, bool throwNotFound)
  {
    if (!string.IsNullOrEmpty(documentPath) && Path.IsPathRooted(documentPath))
    {
      IFileVault service = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
      if (service.FindArea(documentPath) == service.WorkArea)
      {
        FileOrigin fileOrigin = service.WorkArea.GetFileOrigin(documentPath, false);
        if (fileOrigin.OriginType == FileOriginType.WorkFile)
          return fileOrigin.WorkObject.ObjectId;
      }
    }
    if (throwNotFound)
      throw new DocumentNotRegisteredException(documentPath);
    return 0;
  }

  protected void CreateSpecificationWindow(long assemblyID)
  {
    ServiceUtils.GetService<IECADIntegratorsDocumentService>((object) ServicesManager.ServiceContainer, true).CreateSpecificationWindow(assemblyID);
  }

  protected void SetError(Exception ex)
  {
    this.ErrorCode = 1;
    this.ErrorMessage = ex.Message;
  }

  protected void Prepare()
  {
    this.ErrorCode = 0;
    this.ErrorMessage = string.Empty;
  }

  public int ErrorCode { get; private set; }

  public string ErrorMessage { get; private set; }
}
