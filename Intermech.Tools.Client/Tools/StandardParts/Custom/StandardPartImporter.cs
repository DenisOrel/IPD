// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.StandardParts.Custom.StandardPartImporter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.IO;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Text;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

#nullable disable
namespace Intermech.Tools.StandardParts.Custom;

internal sealed class StandardPartImporter
{
  private readonly IFileVault fileVault;
  private readonly IPSAttributeLocalizer attrLocalizer;
  private readonly ArticleLocatorBuilder builder;
  private readonly CaptureChangesManager ccManager;
  private readonly List<string> partTypeNames;

  public StandardPartImporter()
  {
    this.fileVault = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
    this.attrLocalizer = new IPSAttributeLocalizer();
    this.builder = new ArticleLocatorBuilder();
    this.ccManager = new CaptureChangesManager();
    this.ccManager.KeepCheckedOut = false;
    this.partTypeNames = new List<string>();
    this.partTypeNames.Add(IDCache.Default.StandardArticles.Text);
    this.partTypeNames.Add(IDCache.Default.AssistiveArticles.Text);
  }

  public bool CanOpenModel(string modelFullPath, ImportContext ctx)
  {
    return this.IsStandardPartFile(modelFullPath, ctx);
  }

  public ImportedStandardPart ImportModel(string modelSourcePath, ImportContext ctx)
  {
    if (ctx.ImportHistory.AddOrGetIndex(modelSourcePath) >= 0)
      return (ImportedStandardPart) null;
    if (this.IsAlreadyImported(modelSourcePath, ctx))
      return (ImportedStandardPart) null;
    StandardPartImporter.ModelData mdata = this.PrepareModel(modelSourcePath, ctx);
    if (!string.IsNullOrEmpty(mdata.MasterFilePath))
      return this.ImportModel(mdata.MasterFilePath, ctx);
    if (mdata.Errors.Count <= 0)
      return this.ImportPreparedModel(mdata);
    this.EmitErrorProtocol(mdata);
    return (ImportedStandardPart) null;
  }

  private bool IsAlreadyImported(string sourcePath, ImportContext ctx)
  {
    return this.fileVault.WorkArea.GetFileOrigin(this.CalculateTargetPath(sourcePath, ctx), false).OriginType == FileOriginType.WorkFile;
  }

  private void EmitErrorProtocol(StandardPartImporter.ModelData mdata)
  {
    string str = string.Format(LocalizationHolder.rm.GetString("Tools.Client_195"), (object) this.CalculateRelativePath(mdata.ModelSourcePath, mdata.Ctx));
    if (mdata.Errors.Count == 1)
    {
      mdata.Ctx.Protocol.Add(str);
      mdata.Ctx.Protocol.Add(mdata.Errors[0]);
      mdata.Ctx.Protocol.Add(string.Empty);
    }
    else
    {
      mdata.Ctx.Protocol.Add(str);
      mdata.Ctx.Protocol.Add(LocalizationHolder.rm.GetString("Tools.Client_196"));
      for (int index = 0; index < mdata.Errors.Count; ++index)
        mdata.Ctx.Protocol.Add($"    - {mdata.Errors[index]}");
      mdata.Ctx.Protocol.Add(string.Empty);
    }
  }

  private StandardPartImporter.ModelData PrepareModel(string modelSourcePath, ImportContext ctx)
  {
    ICADInterfaceService service = ServiceUtils.GetService<ICADInterfaceService>((object) ctx.Integrator, true);
    try
    {
      StandardPartImporter.ModelData mdata = new StandardPartImporter.ModelData(modelSourcePath, ctx);
      using (CADApiSession cadApiSession = new CADApiSession(ctx.Integrator))
      {
        CADDocumentProxy cadDocumentProxy = cadApiSession.Application.OpenDocument(modelSourcePath, false);
        if (!cadDocumentProxy.IsMasterDocument)
        {
          mdata.MasterFilePath = cadDocumentProxy.MasterFile;
          return !string.IsNullOrEmpty(mdata.MasterFilePath) && File.Exists(mdata.MasterFilePath) ? mdata : throw new Exception(LocalizationHolder.rm.GetString("Tools.Client_197"));
        }
        mdata.SatelliteFiles.AddRange((IEnumerable<string>) cadDocumentProxy.GetSatelliteFiles());
        IAttributeCodec codec = service.OpenDocuments.GetCodec((IOpenDocument) CADInterfaceAdapters.AsOpenDocument(cadDocumentProxy));
        this.PrepareDocumentAttributes(cadDocumentProxy, codec, mdata);
        ICollection<StringKey> attributes = ServiceUtils.GetService<ICADSettingsService>((object) ctx.Integrator, true).SynchronizedArticleAttributes.GetAttributes();
        foreach (ModelConfigurationProxy allConfiguration in (IEnumerable<ModelConfigurationProxy>) cadDocumentProxy.GetAllConfigurations())
          this.PrepareArticleAttributes(service, cadDocumentProxy, allConfiguration, attributes, mdata);
        if (cadDocumentProxy.Modified && !cadDocumentProxy.ReadOnly)
          cadDocumentProxy.Save();
        return mdata;
      }
    }
    finally
    {
      this.CleanupCAD(ctx);
    }
  }

  private void PrepareDocumentAttributes(
    CADDocumentProxy modelDocument,
    IAttributeCodec codec,
    StandardPartImporter.ModelData mdata)
  {
    CADInterfaceValueBagContainer container = CADInterfaceAdapters.AsValueBagContainer(modelDocument);
    List<StringKey> attributeKeys = new List<StringKey>();
    attributeKeys.Add((StringKey) this.attrLocalizer.ATTR_DocumentType);
    ContainerValues containerValues = codec.ReadFileProperties((IValueBagContainer) container, (ICollection<StringKey>) attributeKeys);
    DecodeAttributesParams decodeParams = new DecodeAttributesParams((IValueBagContainer) container, (ICollection<StringKey>) attributeKeys, containerValues, mdata.DecodeOptions);
    ValueBag attributes = codec.Decode(decodeParams);
    attributes.Update((StringKey) this.attrLocalizer.ATTR_DocumentType, (object) mdata.Ctx.StandardModelType.ToString());
    codec.Encode(new EncodeAttributesParams((IValueBagContainer) container, (ICollection<StringKey>) attributes.GetChangedItemsKeys(), attributes, containerValues, mdata.EncodeOptions)
    {
      ContainerDisplayName = Path.GetFileName(modelDocument.FullName)
    });
    codec.Formatter.Write((IValueBagContainer) container, containerValues);
  }

  private void PrepareArticleAttributes(
    ICADInterfaceService cadService,
    CADDocumentProxy modelDocument,
    ModelConfigurationProxy configuration,
    ICollection<StringKey> allAttributes,
    StandardPartImporter.ModelData mdata)
  {
    IAttributeCodec articleCodec = cadService.GetArticleCodec(modelDocument);
    IValueBagContainer attributeContainer = cadService.GetArticleAttributeContainer(configuration);
    ContainerValues containerValues = articleCodec.ReadFileProperties(attributeContainer, allAttributes);
    DecodeAttributesParams decodeParams = new DecodeAttributesParams(attributeContainer, allAttributes, containerValues, mdata.DecodeOptions);
    ValueBag valueBag = articleCodec.Decode(decodeParams);
    if (mdata.Ctx.ClearDesignation)
      valueBag.Update((StringKey) this.attrLocalizer.GetAttributeNameByID(EAttributeID.ATTR_Designation), (object) string.Empty);
    string name = (string) configuration.Name;
    string str = valueBag.Read<string>((StringKey) this.attrLocalizer.GetAttributeNameByID(EAttributeID.ATTR_Name), (string) null);
    if (mdata.Ctx.FillNames && (string.IsNullOrEmpty(str) || !mdata.Ctx.FillEmptyNamesOnly))
    {
      str = TextServices.Trim(name);
      if (this.IsStandardPartFile(str, mdata.Ctx))
        str = Path.GetFileNameWithoutExtension(str);
      valueBag.Update((StringKey) this.attrLocalizer.GetAttributeNameByID(EAttributeID.ATTR_Name), (object) str);
    }
    if (string.IsNullOrEmpty(str))
    {
      mdata.Errors.Add(string.Format(LocalizationHolder.rm.GetString("Tools.Client_198"), (object) name));
    }
    else
    {
      this.builder.DataProvider = (ArticleLocatorDataProvider) new CADArticleLocatorDataProvider(valueBag);
      ObjectLocatorResult objectLocatorResult = this.builder.CreateLocator(ArticleProcessingMethod.ImbaseObject).LocateObject();
      long partId = 0;
      bool flag = false;
      if (objectLocatorResult != null)
      {
        partId = objectLocatorResult.ObjectId;
        long stdPartModel = this.GetStdPartModel(partId, mdata.Ctx);
        if (stdPartModel != 0L)
        {
          QuickObjectInfo objectInfo;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            objectInfo = sessionKeeper.Session.GetObjectInfo(stdPartModel);
          if (objectInfo.Empty)
            throw new InvalidOperationException();
          mdata.Errors.Add(string.Format(LocalizationHolder.rm.GetString("Tools.Client_199"), (object) str, (object) this.fileVault.DBFilesInfo.GetMasterFileName(stdPartModel, true), (object) stdPartModel));
          return;
        }
        flag = this.IsImbaseArticle(partId);
        if (!flag && mdata.Ctx.LinkToImbase)
        {
          mdata.Errors.Add(string.Format(LocalizationHolder.rm.GetString("Tools.Client_200"), (object) str));
          return;
        }
      }
      else if (mdata.Ctx.LinkToImbase)
      {
        Tuple<long, int, string> createImbaseObject = ImbaseHelper.FindOrCreateImbaseObject(valueBag);
        if (createImbaseObject != null)
        {
          partId = createImbaseObject.Item1;
          flag = true;
        }
        else
        {
          mdata.Errors.Add(string.Format(LocalizationHolder.rm.GetString("Tools.Client_201"), (object) str));
          return;
        }
      }
      if (partId == 0L && mdata.Ctx.CorrectPartTypes)
      {
        string partTypeName = valueBag.Read<string>((StringKey) this.attrLocalizer.GetAttributeNameByID(EAttributeID.ATTR_SPSection), (string) null);
        if (string.IsNullOrEmpty(partTypeName) || !this.partTypeNames.Exists((Predicate<string>) (item => string.Compare(item, partTypeName, true) == 0)))
        {
          partTypeName = this.partTypeNames[0];
          valueBag.Update((StringKey) this.attrLocalizer.GetAttributeNameByID(EAttributeID.ATTR_SPSection), (object) partTypeName);
        }
      }
      if (partId != 0L & flag)
      {
        string newValue = this.MakeImbaseKey(partId);
        valueBag.Update((StringKey) this.attrLocalizer.GetAttributeNameByID(EAttributeID.ATTR_IMBaseKey), (object) newValue);
      }
      articleCodec.Encode(new EncodeAttributesParams(attributeContainer, (ICollection<StringKey>) valueBag.GetChangedItemsKeys(), valueBag, containerValues, mdata.EncodeOptions)
      {
        ContainerDisplayName = Path.GetFileName(configuration.Document.FullName)
      });
      articleCodec.Formatter.Write(attributeContainer, containerValues);
    }
  }

  private bool IsImbaseArticle(long partId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(partId, true);
      IDBAttribute attributeById1 = dbObject.GetAttributeByID(IDCache.Default.ImbaseTable.Id);
      IDBAttribute attributeById2 = dbObject.GetAttributeByID(IDCache.Default.ImbaseRecord.Id);
      return attributeById1 != null && !attributeById1.IsNull && attributeById2 != null && !attributeById2.IsNull;
    }
  }

  private string MakeImbaseKey(long partId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return $"IG{sessionKeeper.Session.GetObject(partId, true).GUID:D}";
  }

  private ImportedStandardPart ImportPreparedModel(StandardPartImporter.ModelData mdata)
  {
    string targetPath = this.CalculateTargetPath(mdata.ModelSourcePath, mdata.Ctx);
    try
    {
      if (!PathUtils.IsSamePath(mdata.ModelSourcePath, targetPath))
      {
        this.CopyFileToWorkArea(mdata.ModelSourcePath, targetPath);
        foreach (string satelliteFile in mdata.SatelliteFiles)
          this.CopyFileToWorkArea(satelliteFile, this.CalculateTargetPath(satelliteFile, mdata.Ctx));
      }
      long modelId = this.ImportModelAndArticles(targetPath, mdata.Ctx);
      List<long> stdPartArticles = this.GetStdPartArticles(modelId, mdata.Ctx);
      this.CleanupCAD(mdata.Ctx);
      return new ImportedStandardPart(modelId, (IList<long>) stdPartArticles);
    }
    catch
    {
      this.CleanupCAD(mdata.Ctx);
      if (!PathUtils.IsSamePath(mdata.ModelSourcePath, targetPath))
      {
        this.DeleteFileFromWorkArea(targetPath);
        foreach (string satelliteFile in mdata.SatelliteFiles)
          this.DeleteFileFromWorkArea(this.CalculateTargetPath(satelliteFile, mdata.Ctx));
      }
      throw;
    }
  }

  private string CalculateRelativePath(string sourcePath, ImportContext ctx)
  {
    return PathUtils.GetRelativePath(sourcePath, Path.GetDirectoryName(ctx.RootPath), RelativePathOptions.ThrowIfNotPossible);
  }

  private string CalculateTargetPath(string sourcePath, ImportContext ctx)
  {
    return Path.Combine(this.fileVault.WorkArea.AreaPath, this.CalculateRelativePath(sourcePath, ctx));
  }

  private void CopyFileToWorkArea(string sourcePath, string targetPath)
  {
    string directoryName = Path.GetDirectoryName(targetPath);
    if (Directory.Exists(directoryName))
      this.DeleteFileFromWorkArea(targetPath);
    else
      Directory.CreateDirectory(directoryName);
    File.Copy(sourcePath, targetPath);
  }

  private void DeleteFileFromWorkArea(string targetPath)
  {
    if (!File.Exists(targetPath))
      return;
    File.SetAttributes(targetPath, FileAttributes.Normal);
    File.Delete(targetPath);
  }

  private long ImportModelAndArticles(string modelTargetPath, ImportContext ctx)
  {
    CICaptureChangesDriver captureChangesDriver = new CICaptureChangesDriver(ctx.Integrator);
    captureChangesDriver.UpdateArticles = true;
    captureChangesDriver.RecalculateMass = false;
    try
    {
      this.ccManager.Driver = (ICaptureChangesDriver) captureChangesDriver;
      return this.ccManager.ImportFile(new ImportFileActionParameters()
      {
        FullPath = modelTargetPath
      }).ObjectId;
    }
    finally
    {
      this.ccManager.Driver = (ICaptureChangesDriver) null;
    }
  }

  private List<long> GetStdPartArticles(long modelId, ImportContext ctx)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    paramSet.ColumnsInfo = new ColumnInfo[1]
    {
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, (object) null)
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id);
      relationCollection.FiltrationOwnerID = ctx.VersionsRule.OwnerId;
      relationCollection.ObjectTypeID = IDCache.Default.AllArticles.Id;
      dataTable = relationCollection.EntersInVersion(paramSet, modelId);
    }
    List<long> stdPartArticles = new List<long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      stdPartArticles.Add(Convert.ToInt64(row[0]));
    return stdPartArticles;
  }

  private long GetStdPartModel(long partId, ImportContext ctx)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = 1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id);
      relationCollection.FiltrationOwnerID = ctx.VersionsRule.OwnerId;
      relationCollection.ObjectTypeID = ctx.StandardModelType.Id;
      DataTable dataTable = relationCollection.ConsistFrom(paramSet, partId);
      return dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : 0L;
    }
  }

  private void CleanupCAD(ImportContext ctx)
  {
    using (CADApiSession cadApiSession = new CADApiSession(ctx.Integrator))
    {
      CADSystemProxy application = cadApiSession.Application;
      ICollection<string> openFiles = application.GetOpenFiles(false);
      if (openFiles.Count == 0)
        return;
      application.CloseFiles(openFiles);
    }
  }

  private bool IsStandardPartFile(string fileName, ImportContext ctx)
  {
    string ext = Path.GetExtension(fileName);
    return !string.IsNullOrEmpty(ext) && Array.Exists<string>(ctx.PartModelExtensions, (Predicate<string>) (partExt => PathUtils.IsSamePath(partExt, ext)));
  }

  private sealed class ModelData
  {
    public readonly ImportContext Ctx;
    public readonly string ModelSourcePath;
    public string MasterFilePath;
    public readonly List<string> SatelliteFiles;
    public readonly List<string> Errors;
    public readonly DecodeAttributesOptions DecodeOptions;
    public readonly EncodeAttributesOptions EncodeOptions;

    public ModelData(string modelSourcePath, ImportContext ctx)
    {
      this.Ctx = ctx;
      this.ModelSourcePath = modelSourcePath;
      this.SatelliteFiles = new List<string>();
      this.Errors = new List<string>();
      this.DecodeOptions = DocumentAttributesOptions.GetDecodeOptions(ctx.StandardModelType.Id);
      this.EncodeOptions = DocumentAttributesOptions.GetEncodeOptions(ctx.StandardModelType.Id);
      this.EncodeOptions.ReportErrorsOnly = true;
    }
  }
}
