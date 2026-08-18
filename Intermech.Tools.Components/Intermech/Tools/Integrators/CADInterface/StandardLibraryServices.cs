// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.StandardLibraryServices
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Tools.Data;
using System;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public static class StandardLibraryServices
{
  public static StandardLibraryMode GetMode(IServiceProvider integrator)
  {
    return integrator != null ? ServiceUtils.GetService<IStandardPartLibraryService>((object) integrator, true).Mode : throw new ArgumentNullException();
  }

  public static string GetModelFolderName(IServiceProvider integrator)
  {
    return integrator != null ? ServiceUtils.GetService<IStandardPartLibraryService>((object) integrator, true).FolderName : throw new ArgumentNullException();
  }

  public static string GetModelFolderPath(IServiceProvider integrator)
  {
    if (integrator == null)
      throw new ArgumentNullException();
    return Path.Combine(ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true).WorkArea.AreaPath, ServiceUtils.GetService<IStandardPartLibraryService>((object) integrator, true).FolderName);
  }

  public static LocalId<int> GetModelType(IServiceProvider integrator)
  {
    return integrator != null ? (LocalId<int>) ServiceUtils.GetService<ICADSettingsService>((object) integrator, true).GetCADSettings().StandardPartType : throw new ArgumentNullException();
  }

  public static long FindModel(
    IServiceProvider integrator,
    string fileName,
    string versionsRuleOwner)
  {
    if (integrator == null)
      throw new ArgumentNullException();
    if (string.IsNullOrEmpty(fileName))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(versionsRuleOwner))
      throw new ArgumentException();
    IFileVault service1 = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
    IStandardPartLibraryService service2 = ServiceUtils.GetService<IStandardPartLibraryService>((object) integrator, true);
    if (Path.IsPathRooted(fileName))
    {
      if (!PathUtils.IsPlacedIn(fileName, service1.WorkArea.AreaPath))
        throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_365"), (object) fileName));
      fileName = PathUtils.GetRelativePath(fileName, service1.WorkArea.AreaPath, RelativePathOptions.ThrowIfNotPossible);
    }
    string directoryName = Path.GetDirectoryName(fileName);
    if (string.IsNullOrEmpty(directoryName) || !PathUtils.IsSamePath(directoryName, service2.FolderName))
      throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_366"), (object) fileName, (object) service2.FolderName));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long idByFileName = ServiceUtils.GetService<IFileNamesService>((object) sessionKeeper.Session, true).GetIDByFileName(fileName, sessionKeeper.Session.SessionGUID);
      return idByFileName != -1L ? sessionKeeper.Session.GetObjectByVersionsRule(idByFileName, versionsRuleOwner, true).ObjectID : 0L;
    }
  }

  public static long CreateModel(
    IServiceProvider integrator,
    int modelType,
    string designation,
    string name,
    string relativePath,
    string fullPath)
  {
    if (integrator == null)
      throw new ArgumentNullException();
    if (modelType == -1)
      throw new ArgumentException();
    if (designation == null)
      throw new ArgumentNullException(nameof (designation));
    if (string.IsNullOrEmpty(name))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(relativePath))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(fullPath))
      throw new ArgumentException();
    if (!File.Exists(fullPath))
      throw new FileNotFoundException($"Файл модели стандартного изделия '{fullPath}' не найден на диске.", fullPath);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(modelType).Create();
      if (!string.IsNullOrEmpty(designation))
        dbObject.GetAttributeByID(sessionKeeper.Session.IdentHelper.DesignationID).Value = (object) designation;
      dbObject.GetAttributeByID(sessionKeeper.Session.IdentHelper.NameID).Value = (object) name;
      StandardLibraryServices.UploadModelFile(dbObject.GetAttributeByID(sessionKeeper.Session.IdentHelper.FileAttributeID), relativePath, fullPath);
      dbObject.CommitCreation(true);
      return dbObject.ObjectID;
    }
  }

  private static void UploadModelFile(IDBAttribute blobAttr, string relativePath, string fullPath)
  {
    FileInfo fileInfo = new FileInfo(fullPath);
    BlobInformation aBlobInformation;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      TimeSpan timeZoneOffset = sessionKeeper.Session.TimeZoneOffset;
      aBlobInformation = new BlobInformation(fileInfo.Length, 0L, fileInfo.LastWriteTimeUtc + timeZoneOffset, relativePath, ArcMethods.ZLibPacked, string.Empty);
    }
    using (Stream aSourceStream = (Stream) new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
      new BlobProcWriter(blobAttr, 0, aBlobInformation, aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
  }

  public static Tuple<long, bool> LinkPartToModel(long partId, long modelId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(partId, modelId, true);
      if (relation != null)
        return new Tuple<long, bool>(relation.RelationID, false);
    }
    StandardLibraryServices.ValidateCreateLink(partId, modelId);
    return new Tuple<long, bool>(StandardLibraryServices.DoCreateLink(partId, modelId), true);
  }

  /// <summary>
  /// Проверяет возможность создания связи между стандартным изделием и моделью без взятия стандартного на изменение.
  /// </summary>
  /// <remarks>
  /// Это важно, так как стандартное изделие может находится на таком шаге жизненного цикла, где его нельзя менять
  /// без выпуска новой версии. А выпускать ее и не нужно, так как появление еще одной модели для этого стандартного
  /// ничего в нем не изменяет (так как у стандартных IPS несколько моделей - по одной для каждой из CAD-систем).
  /// </remarks>
  /// <param name="partId">Идентификатор версии стандартного изделия</param>
  /// <param name="modelId">Идентификатор версии модели</param>
  private static void ValidateCreateLink(long partId, long modelId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(partId, true);
      int objectType1 = dbObject1.ObjectType;
      IDBObject dbObject2 = sessionKeeper.Session.GetObject(modelId, true);
      int objectType2 = dbObject2.ObjectType;
      IDBRelationsApplicability applicability = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicability(IDCache.Default.ArticleToDocumentTree.Id, objectType2, objectType1);
      if (applicability == null || applicability.ApplicabilityMode == ApplicabilityModes.Disabled)
        throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_368"), (object) dbObject1.NameInMessages, (object) dbObject2.NameInMessages));
      if (applicability.IsContent)
        throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_369"), (object) dbObject1.NameInMessages, (object) dbObject2.NameInMessages));
    }
  }

  private static long DoCreateLink(long partId, long modelId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id).Create(partId, modelId).RelationID;
  }
}
