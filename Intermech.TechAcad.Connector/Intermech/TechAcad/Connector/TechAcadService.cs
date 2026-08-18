// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadService
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using ImSSP;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechAcad;
using Intermech.Localization;
using Intermech.TechAcad.Interfaces;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechAcad.Connector;

internal class TechAcadService : ITechAcadService
{
  private const string WorkingDirName = "_TechAcad";
  private IFileVault _fileVault;
  private string _workingDirPath;
  private int _acadHwnd;
  private bool _acadExists;
  private int _lastError;

  private void InitData()
  {
    this.CheckAcadExists();
    this._fileVault = ServiceUtils.GetService<IFileVault>((object) ApplicationServices.Container, true);
    this._workingDirPath = Path.Combine(this._fileVault.WorkArea.AreaPath, "_TechAcad");
    if (Directory.Exists(this._workingDirPath))
      return;
    Directory.CreateDirectory(this._workingDirPath);
  }

  private void CheckAcadExists() => this.CheckAcadExists(out string _);

  private bool CheckAcadExists(out string errorMsg)
  {
    errorMsg = string.Empty;
    this._acadExists = false;
    TechAcadParamsItem techAcadSettings = this.GetTechAcadSettings();
    string path = techAcadSettings.ApplPath.Replace("\"", string.Empty);
    try
    {
      if (path == string.Empty)
        errorMsg = LocalizationHolder.rm.GetString(sc_19165.ssp_techacad_19166());
      else if (!File.Exists(path))
        errorMsg = string.Format(LocalizationHolder.rm.GetString(sc_19165.ssp_techacad_19167()), (object) path);
      if (path != string.Empty)
      {
        if (File.Exists(path))
          this._acadExists = true;
      }
    }
    catch (Exception ex)
    {
      if (ex is ArgumentException)
        techAcadSettings.ApplPath = string.Empty;
      else
        throw;
    }
    return this._acadExists;
  }

  private string GetAcadTextCustom(long objId, TechAcadCallProc customProc)
  {
    try
    {
      if (customProc == null)
        throw new TechAcadService.FailException();
      this.CheckDraftExists(objId);
      this.CheckAcadLoaded();
      string picture = this.ExtractPicture(objId);
      return customProc(objId, picture);
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return string.Empty;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.GetAcadTextCustom", ex);
      throw;
    }
  }

  private string GetTechDopProc(long objId, string fileName)
  {
    string empty = string.Empty;
    Intermech.TechAcad.Connector.TechAcad.GetTechCustomer(fileName, ref empty);
    return empty;
  }

  private string GetStdElementProc(long objId, string fileName)
  {
    string empty = string.Empty;
    Intermech.TechAcad.Connector.TechAcad.OpenPicture(fileName, 0);
    try
    {
      if (Intermech.TechAcad.Connector.TechAcad.SelectStrElem(ref empty) != 0)
        empty = string.Empty;
    }
    finally
    {
      Intermech.TechAcad.Connector.TechAcad.ClosePicture(fileName);
    }
    return empty;
  }

  public TechAcadService() => this.InitData();

  public bool AcadExists
  {
    get
    {
      if (!this._acadExists)
        this.CheckAcadExists();
      return this._acadExists;
    }
  }

  public bool LoadAcad(TechAcadLoadMode loadMode)
  {
    string errorMsg;
    if (!this.CheckAcadExists(out errorMsg))
    {
      if (loadMode == TechAcadLoadMode.Normal && errorMsg != string.Empty)
      {
        int num = (int) MessageBox.Show(errorMsg, LocalizationHolder.rm.GetString(sc_19165.ssp_techacad_19168()), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      return false;
    }
    try
    {
      this._acadHwnd = Intermech.TechAcad.Connector.TechAcad.GetAcadHWND();
    }
    catch
    {
      this._acadHwnd = 0;
    }
    if (this._acadHwnd == 3)
      this._acadHwnd = 0;
    if (this._acadHwnd != 0 && !TechAcadUtils.IsWindow(new HandleRef((object) null, (IntPtr) this._acadHwnd)))
      this._acadHwnd = 0;
    if (this._acadHwnd != 0)
    {
      if (loadMode == TechAcadLoadMode.Normal)
        this.ShowAcadWindow(WindowMode.ShowDefault);
      return true;
    }
    if (loadMode == TechAcadLoadMode.Normal && MessageBox.Show(LocalizationHolder.rm.GetString(sc_19165.ssp_techacad_19169()), LocalizationHolder.rm.GetString(sc_19165.ssp_techacad_19170()), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return false;
    try
    {
      TechAcadParamsItem techAcadSettings = this.GetTechAcadSettings();
      try
      {
        string exFile = $"\"{techAcadSettings.ApplPath}\"";
        string directory = $"\"{this._workingDirPath}\"";
        this._lastError = Intermech.TechAcad.Connector.TechAcad.OpenPictureEditor(exFile, techAcadSettings.Params, directory);
      }
      catch (Exception ex)
      {
        switch (ex)
        {
          case COMException _:
          case FileNotFoundException _:
          case DllNotFoundException _:
            if (loadMode == TechAcadLoadMode.Silent)
            {
              int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString(sc_19165.ssp_techacad_19171()), MessageBoxButtons.OK, MessageBoxIcon.Hand);
              break;
            }
            break;
        }
        throw;
      }
      if (this._lastError != 0)
        return false;
      this._acadHwnd = Intermech.TechAcad.Connector.TechAcad.GetAcadHWND();
      return true;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.LoadAcad", ex);
      throw;
    }
  }

  public bool UnloadAcad(bool askForUnload)
  {
    try
    {
      if (this.AcadExists && this._acadHwnd != 0)
      {
        if (askForUnload && MessageBox.Show(LocalizationHolder.rm.GetString(sc_19165.ssp_techacad_19172()), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          throw new TechAcadService.FailException();
        this._lastError = Intermech.TechAcad.Connector.TechAcad.ClosePictureEditor();
        this._acadHwnd = 0;
      }
      return true;
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return false;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.UnloadAcad", ex);
      throw;
    }
  }

  public string GetWorkingDirPath() => this._workingDirPath;

  public bool CreatePicture(long objId)
  {
    try
    {
      if (this._fileVault.DBFilesInfo.GetFileNames(objId).Count == 0)
      {
        this.CheckDraftExists(objId);
        this.CheckAcadLoaded();
        TechAcadParamsItem techAcadSettings = this.GetTechAcadSettings();
        if (techAcadSettings.PrototypeDraft == string.Empty || !File.Exists(techAcadSettings.PrototypeDraft))
          throw new TechAcadService.FailException(LocalizationHolder.rm.GetString(sc_19165.ssp_techacad_19173()), true);
        string draftFileName = this.CreateDraftFileName(objId, techAcadSettings);
        string str = Path.Combine(this._fileVault.WorkArea.AreaPath, draftFileName);
        int picture = Intermech.TechAcad.Connector.TechAcad.CreatePicture(str, techAcadSettings.PrototypeDraft);
        if (picture != 0)
          throw new TechAcadService.FailException($"ErrorCode = {picture}. Could not create draft file '{str}'", false);
        if (!File.Exists(str))
          throw new TechAcadService.FailException($"Draft file '{str}' wasn't created.", false);
        this.CreateDraftFilePlacement(objId, draftFileName);
        this._fileVault.WorkArea.Attach(objId);
        this._fileVault.WorkArea.Save(objId);
      }
      return true;
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return false;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.CreatePicture", ex);
      throw;
    }
  }

  private string CreateDraftFileName(long draftId, TechAcadParamsItem draftSettings)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(draftId);
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = string.IsNullOrEmpty(objectInfo.Caption) ? objectInfo.ID.ToString() : objectInfo.Caption.Trim();
      string str2 = str1.Substring(0, Math.Min(str1.Length, 60));
      stringBuilder.Append(str2);
      foreach (char invalidFileNameChar in Path.GetInvalidFileNameChars())
        stringBuilder.Replace(invalidFileNameChar, '_');
      if (!string.IsNullOrEmpty(draftSettings.FileExtention))
      {
        if (stringBuilder[stringBuilder.Length - 1] != '.' && draftSettings.FileExtention[0] != '.')
          stringBuilder.Append('.');
        stringBuilder.Append(draftSettings.FileExtention);
      }
      stringBuilder.Insert(0, Path.DirectorySeparatorChar);
      stringBuilder.Insert(0, "_TechAcad");
      string fileName = stringBuilder.ToString();
      return ServiceUtils.GetService<IFileNamesService>((object) sessionKeeper.Session, true).GetUniqueFileName(fileName, objectInfo.ID, sessionKeeper.Session.SessionGUID);
    }
  }

  private void CreateDraftFilePlacement(long draftId, string draftFileName)
  {
    if (this._fileVault.DBFilesInfo.GetFileNames(draftId).Count > 0)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_19165.ssp_techacad_19174()));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ((IBlobWriter) (sessionKeeper.Session.GetObjectAttributeByGuid(draftId, new Guid("cad0004b-306c-11d8-b4e9-00304f19f545")) ?? throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_19165.ssp_techacad_19175())))).OpenBlob(new BlobInformation(0L, 0L, DateTime.Today.AddYears(-10), draftFileName, ArcMethods.NotPacked, string.Empty), true);
  }

  public bool OpenPicture(long objId)
  {
    try
    {
      this.CheckDraftExists(objId);
      this.CheckAcadLoaded();
      Intermech.TechAcad.Connector.TechAcad.OpenPicture(this.ExtractPicture(objId), 0);
      return true;
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return false;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.LoadPicture", ex);
      throw;
    }
  }

  public bool ClosePicture(long objId)
  {
    try
    {
      this.CheckDraftExists(objId);
      this.CheckAcadLoaded();
      string pictureLocalPath = this.GetPictureLocalPath(objId);
      if (string.IsNullOrEmpty(pictureLocalPath))
        throw new TechAcadService.FailException();
      Intermech.TechAcad.Connector.TechAcad.ClosePicture(pictureLocalPath);
      this._fileVault.WorkArea.Unpublish(objId);
      return true;
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return false;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.ClosePicture", ex);
      throw;
    }
  }

  public bool SaveOnlyPicture(long objId)
  {
    try
    {
      this.CheckDraftExists(objId);
      this.CheckAcadLoaded();
      string pictureLocalPath = this.GetPictureLocalPath(objId);
      if (string.IsNullOrEmpty(pictureLocalPath))
        throw new TechAcadService.FailException();
      Intermech.TechAcad.Connector.TechAcad.SavePicture(pictureLocalPath);
      this._fileVault.WorkArea.Save(objId);
      return true;
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return false;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.SaveOnlyPicture", ex);
      throw;
    }
  }

  public bool SaveAndUnloadPicture(long objId)
  {
    try
    {
      this.CheckDraftExists(objId);
      this.CheckAcadLoaded();
      string pictureLocalPath = this.GetPictureLocalPath(objId);
      if (string.IsNullOrEmpty(pictureLocalPath))
        throw new TechAcadService.FailException();
      Intermech.TechAcad.Connector.TechAcad.SavePicture(pictureLocalPath);
      this._fileVault.WorkArea.Save(objId);
      this._fileVault.WorkArea.Unpublish(objId);
      return true;
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return false;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.SaveAndUnloadPicture", ex);
      throw;
    }
  }

  public bool UnloadPicture(long objId)
  {
    try
    {
      this.CheckDraftExists(objId);
      this.CheckAcadLoaded();
      if (this._fileVault.WorkArea.FindPublishedObjectByVersionId(objId) == null)
        throw new TechAcadService.FailException($"A draft object with id #{objId} is not found in then file vault.", false);
      this._fileVault.WorkArea.Unpublish(objId);
      return true;
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return false;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.UnloadPicture", ex);
      throw;
    }
  }

  public bool UnloadPicture(string fileName)
  {
    try
    {
      this.CheckAcadLoaded();
      long pictureObject = this.GetPictureObject(fileName);
      if (pictureObject == 0L)
        throw new TechAcadService.FailException($"Can't find draft object by file '{fileName}'", false);
      this._fileVault.WorkArea.Unpublish(pictureObject);
      return true;
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return false;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.UnloadPicture", ex);
      throw;
    }
  }

  public string GetAcadText()
  {
    try
    {
      this.CheckAcadLoaded();
      string empty = string.Empty;
      Intermech.TechAcad.Connector.TechAcad.GetText(ref empty);
      return empty;
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return string.Empty;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.GetAcadText", ex);
      throw;
    }
  }

  public string GetTechDop(long objId)
  {
    return this.GetAcadTextCustom(objId, new TechAcadCallProc(this.GetTechDopProc));
  }

  public string GetStdElemt(long obId)
  {
    return this.GetAcadTextCustom(obId, new TechAcadCallProc(this.GetStdElementProc));
  }

  public string GetPictureLocalPath(long draftId)
  {
    try
    {
      this.CheckDraftExists(draftId);
      if (!this._fileVault.WorkArea.IsObjectPublished(draftId))
        throw new TechAcadService.FailException();
      string masterFileName = this._fileVault.DBFilesInfo.GetMasterFileName(draftId, false);
      if (string.IsNullOrEmpty(masterFileName))
        throw new TechAcadService.FailException();
      string path = Path.Combine(this._fileVault.WorkArea.AreaPath, masterFileName);
      return File.Exists(path) ? path : throw new TechAcadService.FailException();
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return (string) null;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.GetPictureLocalPath", ex);
      throw;
    }
  }

  public string ExtractPicture(long objId)
  {
    try
    {
      this.CheckDraftExists(objId);
      return this._fileVault.PublishTree(objId, false, VersionsRuleSources.GetEditorRule(), (IFileArea) this._fileVault.WorkArea);
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return string.Empty;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.ExtractPicture", ex);
      throw;
    }
  }

  public long GetPictureObject(string draftLocalPath)
  {
    try
    {
      if (string.IsNullOrEmpty(draftLocalPath) || !Path.IsPathRooted(draftLocalPath))
        throw new TechAcadService.FailException("Bad draft path. It must be non empty and must contain the absolute path to the draft.", false);
      FileOrigin fileOrigin = this._fileVault.WorkArea.GetFileOrigin(draftLocalPath, false);
      if (fileOrigin.OriginType != FileOriginType.WorkFile)
        throw new TechAcadService.FailException();
      return this._fileVault.WorkArea.FindPublishedObjectById(fileOrigin.Id).ObjectId;
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return 0;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.GetPictureOject", ex);
      throw;
    }
  }

  public bool IsPictureEditable(long objId)
  {
    try
    {
      this.CheckDraftExists(objId);
      DBObjectState objectByVersionId = this._fileVault.WorkArea.FindPublishedObjectByVersionId(objId);
      return objectByVersionId != null && objectByVersionId.IsEditableState;
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
      return false;
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.IsPictureEditable", ex);
      throw;
    }
  }

  public string GetTextForObj(string fileName, long objId) => string.Empty;

  public void ReplaceDim(string fileName, string[] data)
  {
  }

  public void SetInterfaceObject(object obj)
  {
    try
    {
      this.CheckAcadLoaded();
      Intermech.TechAcad.Connector.TechAcad.SetInterfaceObject(obj);
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.SetInterfaceObject", ex);
      throw;
    }
  }

  private void CheckAcadLoaded()
  {
    if (!this.LoadAcad(TechAcadLoadMode.Silent))
      throw new TechAcadService.FailException("Draft editor is not loaded.", false);
  }

  private void CheckDraftExists(long draftId)
  {
    if (draftId == 0L)
      throw new TechAcadService.FailException("Draft version id is undefined.", false);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetObjectInfo(draftId).Empty)
        throw new TechAcadService.FailException($"Draft #{draftId} is not found in DB.", false);
    }
  }

  private TechAcadParamsItem GetTechAcadSettings()
  {
    TechAcadParamsItem techAcadSettings = new TechAcadParamsItem();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITechAcadParamsService service = ServiceUtils.GetService<ITechAcadParamsService>((object) sessionKeeper.Session, false);
      TechAcadParamsHelper.LoadData(techAcadSettings, sessionKeeper.Session, service);
    }
    return techAcadSettings;
  }

  private void ProcessFail(TechAcadService.FailException x)
  {
    if (string.IsNullOrEmpty(x.Message))
      return;
    Plugin.LogError($"Internal error: {x.Message}");
    if (!x.UserVisible)
      return;
    int num = (int) MessageBox.Show(x.Message, LocalizationHolder.rm.GetString(sc_19165.ssp_techacad_19176()));
  }

  private void LogError(string methodName, Exception x)
  {
    Plugin.LogError(string.Format(sc_19165.ssp_techacad_19177(), (object) methodName, (object) x));
  }

  public void ShowAcadWindow(WindowMode mode)
  {
    try
    {
      this.CheckAcadLoaded();
      TechAcadUtils.ShowWindow(this._acadHwnd, (int) mode);
    }
    catch (TechAcadService.FailException ex)
    {
      this.ProcessFail(ex);
    }
    catch (Exception ex)
    {
      this.LogError("ITechAcadService.MinimizeAcad", ex);
      throw;
    }
  }

  private sealed class FailException : Exception
  {
    private readonly bool _userVisible;

    public FailException(string message, bool userVisible)
      : base(message)
    {
      this._userVisible = userVisible;
    }

    public FailException()
      : this(string.Empty, false)
    {
    }

    public bool UserVisible => this._userVisible;
  }
}
