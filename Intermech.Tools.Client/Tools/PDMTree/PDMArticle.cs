// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMArticle
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.CADInterface.Proxies;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Navigator.Controls;
using Intermech.PdmConfigurator.Options;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Interop.CADInterface;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.PDMTree;

[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (IPDMArticle3))]
public sealed class PDMArticle : 
  PDMObject,
  IPDMArticle3,
  IPDMArticle2,
  IPDMArticle,
  IParametersContainer
{
  private readonly PDMDocument pdmDocument;
  private PDMParameterContainer paramContainer;
  private string stringIdCache;
  private CADDocumentProxy cadDocument;

  internal PDMArticle(long objectId, PDMDocument pdmDocumentOrNull, PDMSystem pdmSystem)
    : base(objectId, pdmSystem)
  {
    this.pdmDocument = pdmDocumentOrNull;
    if (this.pdmDocument != null)
      this.pdmDocument.ObjectIdChanged += new EventHandler(this.OnDocumentIdChanged);
    this.paramContainer = new PDMParameterContainer((IDBObjectRef) this, (IPDMSystemProvider) this);
  }

  public string GetID()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMArticle.GetID");
    this.PDMSystem.PrepareCall();
    try
    {
      if (this.stringIdCache == null)
        this.stringIdCache = $"{this.ObjectId};{(this.pdmDocument != null ? this.pdmDocument.ObjectId : 0L)}";
      return this.stringIdCache;
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void SetCADDocument(CADDocumentProxy cadDocument)
  {
    this.cadDocument = cadDocument != null ? cadDocument : throw new ArgumentNullException(nameof (cadDocument));
  }

  private void CheckCADDocumentIsSet()
  {
    if (this.cadDocument == null)
      throw new InvalidOperationException("The CADDocument is not set. Use the method SetCADDocument() first.");
  }

  public bool ComputeConditions(
    string bstrConfigString,
    string bstrConditionsGUID,
    out string pbstrConfigStringInUse)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMArticle.ComputeConditions");
    this.PDMSystem.PrepareCall();
    try
    {
      this.CheckCADDocumentIsSet();
      pbstrConfigStringInUse = string.Empty;
      PdmConfiguratorContext context = new PdmConfiguratorContext((object) bstrConfigString);
      byte[] bytes1 = this.cadDocument.ReadCustomData(bstrConditionsGUID);
      string source = bytes1 != null ? Encoding.UTF8.GetString(bytes1) : "";
      ObjectsApplicabilitiesCriterionsCollection criterionsCollection = new ObjectsApplicabilitiesCriterionsCollection();
      criterionsCollection.Assign((object) source);
      if (criterionsCollection.Evalute(context) != PdmConfiguratorResult.True)
        return false;
      byte[] bytes2 = this.cadDocument.ReadCustomData(bstrConditionsGUID + "x");
      pbstrConfigStringInUse = bytes2 != null ? Encoding.UTF8.GetString(bytes2) : "";
      return true;
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public string FullPath
  {
    get
    {
      if (PDMSystemTrace.General.TraceVerbose)
        Trace.WriteLine("PDMArticle.FullPath");
      this.PDMSystem.PrepareCall();
      try
      {
        string fullPath = this.GetFullPath();
        if (PDMSystemTrace.General.TraceVerbose)
        {
          Trace.Indent();
          Trace.WriteLine("property value = '{0}'", fullPath);
          Trace.Unindent();
        }
        return fullPath;
      }
      catch (Exception ex)
      {
        this.PDMSystem.ReportException(ex);
        throw;
      }
    }
  }

  private string GetFullPath()
  {
    if (this.pdmDocument != null)
    {
      IFileVault fileVaultService = this.PDMSystem.PDMSystemContext.FileVaultService;
      string configurationFile = DBDocumentHelper.GetCADConfigurationFile(this.ObjectId, this.pdmDocument.ObjectId);
      if (!string.IsNullOrEmpty(configurationFile))
      {
        string fullPath = Path.GetFullPath(Path.Combine(fileVaultService.WorkArea.AreaPath, configurationFile));
        if (File.Exists(fullPath))
          return fullPath;
      }
      string masterFileName = fileVaultService.DBFilesInfo.GetMasterFileName(this.pdmDocument.ObjectId, false);
      if (!string.IsNullOrEmpty(masterFileName) && ServiceUtils.GetService<IApplicationFileTypes>((object) this.PDMSystem.Integrator, true).IsApplicationFile(masterFileName))
      {
        string fullPath = Path.GetFullPath(Path.Combine(fileVaultService.WorkArea.AreaPath, masterFileName));
        if (File.Exists(fullPath))
          return fullPath;
      }
    }
    return string.Empty;
  }

  public IPDMDocument GetPDMDocument()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMArticle.GetPDMDocument");
    this.PDMSystem.PrepareCall();
    try
    {
      return (IPDMDocument) this.pdmDocument;
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void ShowConditionsDialog(IPDMArticle pPartArticle, ref string pbstrConditionGUID)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMArticle.ShowConditionsDialog");
    this.PDMSystem.PrepareCall();
    try
    {
      if (pPartArticle == null)
        throw new Exception("Объект для компонента не найден. Выполните расширенное сохранение для компонента и перестройте дерево.");
      this.CheckCADDocumentIsSet();
      long objectId = (pPartArticle as PDMArticle).ObjectId;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject parentObj = sessionKeeper.Session.GetObject(this.ObjectId);
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
        IDBRelation relation = sessionKeeper.Session.GetRelation(this.ObjectId, objectId, true);
        string pdmCriterion;
        string pdmContext;
        if (pbstrConditionGUID != "")
        {
          byte[] bytes1 = this.cadDocument.ReadCustomData(pbstrConditionGUID);
          pdmCriterion = bytes1 != null ? Encoding.UTF8.GetString(bytes1) : "";
          byte[] bytes2 = this.cadDocument.ReadCustomData(pbstrConditionGUID + "x");
          pdmContext = bytes2 != null ? Encoding.UTF8.GetString(bytes2) : "";
        }
        else
        {
          pdmCriterion = "";
          pdmContext = "";
        }
        if (DialogResult.OK != ObjectOptionsForm.Execute(dbObject, parentObj, relation, (System.IServiceProvider) null, ref pdmCriterion, ref pdmContext))
          return;
        if (pbstrConditionGUID != "")
        {
          this.cadDocument.DeleteCustomData(pbstrConditionGUID);
          this.cadDocument.DeleteCustomData(pbstrConditionGUID + "x");
        }
        else
        {
          Guid guid = Guid.NewGuid();
          pbstrConditionGUID = guid.ToString().Replace("-", "").Remove(0, 2);
        }
        if (pdmCriterion.Length > 0)
          this.cadDocument.WriteCustomData(pbstrConditionGUID, Encoding.UTF8.GetBytes(pdmCriterion));
        if (pdmContext.Length <= 0)
          return;
        this.cadDocument.WriteCustomData(pbstrConditionGUID + "x", Encoding.UTF8.GetBytes(pdmContext));
      }
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw ex;
    }
  }

  public void ShowSelectOptionsDialog(ref string pbstrConfigString)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMArticle.ShowSelectOptionsDialog");
    this.PDMSystem.PrepareCall();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectId);
        if (!MetaDataHelper.IsPdmConfigurableObjectType(dbObject.ObjectType))
          throw new Exception($"Тип объекта '{dbObject.ObjectType}' не поддерживает конфигурирование");
        string selectedOptions = pbstrConfigString;
        if (SelectObjectOptionsForm.Execute(dbObject, ref selectedOptions) == DialogResult.OK)
          pbstrConfigString = selectedOptions;
        else
          pbstrConfigString = "";
      }
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void EditParameters()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMArticle.EditParameters");
    this.PDMSystem.PrepareCall();
    try
    {
      PDMDocument pdmDocument = this.pdmDocument;
      if (pdmDocument != null)
      {
        int num1 = (int) PropertiesWindow.Execute(string.Empty, string.Empty, pdmDocument.ObjectId, "PDM.ArticlesView");
      }
      else
      {
        int num2 = (int) PropertiesWindow.Execute(string.Empty, string.Empty, this.ObjectId);
      }
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public string[] GetParameterNames(bool bConvertedNames)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMArticle.GetParameterNames");
    return this.paramContainer.GetParameterNames(bConvertedNames);
  }

  public void GetParameters(
    string[] pParameterNames,
    bool bConvertedNames,
    out object[] ppValues,
    out short[] ppIsReadOnly)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMArticle.GetParameters");
    this.paramContainer.GetParameters(pParameterNames, bConvertedNames, out ppValues, out ppIsReadOnly);
    if (!PDMSystemTrace.General.TraceVerbose || pParameterNames == null)
      return;
    Trace.Indent();
    for (int index = 0; index < pParameterNames.Length; ++index)
      Trace.WriteLine($"'{pParameterNames[index]}' = '{ppValues[index]}'");
    Trace.Unindent();
  }

  public void SetParameters(string[] pParameterNames, object[] pValues, bool bConvertedNames)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMArticle.SetParameters");
    this.paramContainer.SetParameters(pParameterNames, pValues, bConvertedNames);
  }

  public void DeleteParameters(string[] pParameterNames, bool bConvertedNames)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMArticle.DeleteParameters");
    this.paramContainer.DeleteParameters(pParameterNames, bConvertedNames);
  }

  protected override void OnObjectIdChanged()
  {
    base.OnObjectIdChanged();
    this.stringIdCache = (string) null;
  }

  private void OnDocumentIdChanged(object sender, EventArgs e)
  {
    this.stringIdCache = (string) null;
  }
}
