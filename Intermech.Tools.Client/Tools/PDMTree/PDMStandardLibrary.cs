// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMStandardLibrary
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using Intermech.Diagnostics;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal abstract class PDMStandardLibrary : IPDMStandardLibrary
{
  protected readonly IPDMSystemContext pdmSystemContext;
  protected readonly IIntegrator integrator;
  protected readonly ArticleLocatorBuilder articleLocatorBuilder;
  protected readonly IFileVault fileVault;
  protected readonly string modelFolderPath;
  private bool modelFolderIsAlreadyCreated;
  private IEventLogWriter log;

  public PDMStandardLibrary(
    IPDMSystemContext pdmSystemContext,
    IIntegrator integrator,
    ArticleLocatorBuilder articleLocatorBuilder)
  {
    if (pdmSystemContext == null)
      throw new ArgumentNullException(nameof (pdmSystemContext));
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (articleLocatorBuilder == null)
      throw new ArgumentNullException(nameof (articleLocatorBuilder));
    this.pdmSystemContext = pdmSystemContext;
    this.integrator = integrator;
    this.articleLocatorBuilder = articleLocatorBuilder;
    this.fileVault = pdmSystemContext.FileVaultService;
    this.modelFolderPath = StandardLibraryServices.GetModelFolderPath((IServiceProvider) this.integrator);
  }

  public IEventLogWriter Log
  {
    [DebuggerStepThrough] get => this.log;
    [DebuggerStepThrough] set => this.log = value;
  }

  public string BeginUpdatePart(string partNameOrKey, string modelFileName)
  {
    if (this.Log != null)
      this.Log.Write($"BeginUpdatePart('{partNameOrKey}', '{modelFileName}')");
    this.CheckModelFileName(modelFileName);
    try
    {
      this.LazyCreateModelFolder();
      string str = this.DoBeginUpdatePart(partNameOrKey, modelFileName);
      if (this.Log != null)
        this.Log.Write($"BeginUpdatePart finished with result '{str}'");
      return str;
    }
    catch (Exception ex)
    {
      if (this.Log != null)
        this.Log.Write(ExceptionServices.GetExtendedExceptionText(ex, "BeginUpdatePart failed with unhandled exception"), EventLogItemType.Error);
      throw;
    }
  }

  protected abstract string DoBeginUpdatePart(string partNameOrKey, string modelFileName);

  public void EndUpdatePart(string partNameOrKey, string modelFileName)
  {
    if (this.Log != null)
      this.Log.Write($"EndUpdatePart('{partNameOrKey}', '{modelFileName}')");
    this.CheckPartNameOrKeyArg(partNameOrKey);
    this.CheckModelFileName(modelFileName);
    try
    {
      this.DoEndUpdatePart(partNameOrKey, modelFileName);
      if (this.Log == null)
        return;
      this.Log.Write("EndUpdatePart finished with no result");
    }
    catch (Exception ex)
    {
      if (this.Log != null)
        this.Log.Write(ExceptionServices.GetExtendedExceptionText(ex, "EndUpdatePart failed with unhandled exception"), EventLogItemType.Error);
      throw;
    }
  }

  protected abstract void DoEndUpdatePart(string partNameOrKey, string modelFileName);

  private void LazyCreateModelFolder()
  {
    if (this.modelFolderIsAlreadyCreated)
      return;
    if (!Directory.Exists(this.modelFolderPath))
      Directory.CreateDirectory(this.modelFolderPath);
    this.modelFolderIsAlreadyCreated = true;
  }

  protected void CheckPartNameOrKeyArg(string partNameOrKey)
  {
    if (string.IsNullOrEmpty(partNameOrKey))
      throw new ArgumentException("Не задано наименование или ключ IMBASE для стандартного изделия.", nameof (partNameOrKey));
  }

  protected void CheckModelFileName(string modelFileName)
  {
    if (string.IsNullOrEmpty(modelFileName))
      throw new ArgumentException("Не задано имя файла для модели стандартного изделия.", nameof (modelFileName));
  }

  protected ObjectLocatorResult GetImbaseObject(string partNameOrKey)
  {
    this.CheckPartNameOrKeyArg(partNameOrKey);
    return ImbaseHelper.IsImbaseKey(partNameOrKey) ? this.GetImbaseObjectByImbaseKey(partNameOrKey) : this.GetImbaseObjectByPartName(partNameOrKey);
  }

  private ObjectLocatorResult GetImbaseObjectByPartName(string partName)
  {
    this.articleLocatorBuilder.DataProvider = (ArticleLocatorDataProvider) new CADArticleLocatorDataProvider(new ValueBag()
    {
      {
        (StringKey) IDCache.Default.Name.Text,
        (object) partName
      }
    });
    ObjectLocatorResult objectByPartName = this.articleLocatorBuilder.CreateLocator(ArticleProcessingMethod.NormalObject).LocateObject();
    if (objectByPartName == null)
      return (ObjectLocatorResult) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (string.Compare(sessionKeeper.Session.GetObject(objectByPartName.ObjectId, true).GetAttributeByID(IDCache.Default.Name.Id)?.AsString, partName, true) != 0)
        objectByPartName = (ObjectLocatorResult) null;
    }
    return objectByPartName;
  }

  private ObjectLocatorResult GetImbaseObjectByImbaseKey(string imbaseKey)
  {
    this.articleLocatorBuilder.DataProvider = (ArticleLocatorDataProvider) new CADArticleLocatorDataProvider(new ValueBag()
    {
      {
        (StringKey) IDCache.Default.ImbaseKey.Text,
        (object) imbaseKey
      }
    });
    return this.articleLocatorBuilder.CreateLocator(ArticleProcessingMethod.ImbaseObject).LocateObject();
  }

  protected string ConvertModelFileNameToAbsolutePath(string modelFileName)
  {
    if (!string.IsNullOrEmpty(Path.GetDirectoryName(modelFileName)))
      return modelFileName;
    string absolutePath = Path.Combine(this.modelFolderPath, modelFileName);
    if (this.Log != null)
      this.Log.Write($"Converting '{modelFileName}' to '{absolutePath}'");
    return absolutePath;
  }
}
