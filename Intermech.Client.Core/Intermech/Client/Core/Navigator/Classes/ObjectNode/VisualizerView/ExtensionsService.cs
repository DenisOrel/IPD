
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ExtensionsService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;
using Intermech.Client.Core.ThumbnailDocs;
using Intermech.Client.Core.Visualizers;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;

/// <summary>Служба для работы с настройками просмотра файлов</summary>
public class ExtensionsService : IExtensionsService
{
  /// <summary>Имя секции в настройках для настроек просмотра файлов</summary>
  private const string SectionId = "ExtensionsSection";
  /// <summary>
  /// Имя секции для хранения непосредственного описания всех настроек просмотра файлов.
  /// Описание хранится  в формате имя параметра - порядковый номер значение - {used? "1":"0"}|{name}|{progID}|{extensions}
  /// </summary>
  private const string DataSectionId = "ExtensionsSettings";
  /// <summary>свойства для открытия просмотра</summary>
  private const string PropertiesParamName = "Properties";
  /// <summary>методы для открытия просмотра</summary>
  private const string MethodsParamName = "Methods";
  /// <summary>Отладочная информация</summary>
  private const string DebugModeParamName = "DebugMode";
  /// <summary>
  /// Записывать подписи и параметры в файл перед просмотром
  /// </summary>
  private const string WriteSignsAndParamsParamName = "WriteSignsAndParams";
  /// <summary>"Src,src,FileName,URL,Movie,DocumenFileName"</summary>
  private const string DefaultViewProperties = "Src,src,FileName,Filename,URL,Movie,DocumenFileName,sourceUrl";
  /// <summary>"Navigate,SetURL,LoadFile,Open"</summary>
  private const string DefaultViewMethods = "Navigate,SetURL,LoadFile,Open";
  /// <summary>Приоритетный просмотр аутентичных документов</summary>
  private const string PriorityViewAuthenticFileObjTypes = "ViewAuthFileObjTypes";
  /// <summary>Шаблон настроек по умолчанию</summary>
  private readonly Tuple<string, string, string>[] _defaultSettingsTemplate = new Tuple<string, string, string>[9]
  {
    new Tuple<string, string, string>("Adobe PDF Reader", "AcroPDF.PDF", "pdf"),
    new Tuple<string, string, string>("Autodesk Inventor", "Inventor.ViewControl.1", "iam;ipt;idw;ipn;ide"),
    new Tuple<string, string, string>("eDrawings Solidworks", "EModelView.EModelViewControl", "sldprt;sldasm;slddrw"),
    new Tuple<string, string, string>("Kompas3D", "KGAX.KGAXCtrl.1", "a3d;m3d;cdw"),
    new Tuple<string, string, string>("Kompas3D View", "KOMPAS.A3D", "a3d;cdw;frw;kdw;m3d;spw;t3d"),
    new Tuple<string, string, string>("Solid Edge", "SEPREVIEW.SEPreviewCtrl.1", "par;asm;psm;dft;pwd"),
    new Tuple<string, string, string>("Volo View", "AvViewX.AvViewX.1", "iam;ipt;idw;ipn;ide"),
    new Tuple<string, string, string>("JT2Go", "DirectModel.Document.jt", "jt"),
    new Tuple<string, string, string>("PTC Creo View", IntPtr.Size == 8 ? "PVIEW.pviewCtrl.2" : "PVIEW.pviewCtrl.1", "prt;asm;dgm;drw;frm;lay;mfg;mrk;pha;psf;eda;ed;edz;hpgl;ol;plt;pvs;pvz;sec;[1-9];[1-9][0-9]")
  };
  /// <summary>Объект для потокобезопасного доступа</summary>
  private readonly object _syncRoot = new object();
  /// <summary>
  /// Пользовательские настройки для расширений. Здесь храняться как пользовательские так и общие. Сделано так, потому что пользователи могут отключать общие настройки.
  /// </summary>
  private List<FileExtensionsInfo> _userFileExtInfo = new List<FileExtensionsInfo>();
  /// <summary>
  /// Общие настройки для расширений. Могут добавлять только пользователи с правами админа
  /// </summary>
  private List<FileExtensionsInfo> _commonFileExtInfo = new List<FileExtensionsInfo>();
  /// <summary>
  /// Несохраняемые настройки. Настройки просмотра из реестра и для родных форматов. Читаются каждый раз из реестра
  /// </summary>
  private List<FileExtensionsInfo> _notStoredFileExtInfo = new List<FileExtensionsInfo>();
  /// <summary>
  /// 
  /// </summary>
  private IReadOnlyCollection<FileExtensionsInfo> _allFileExtInfo = (IReadOnlyCollection<FileExtensionsInfo>) new FileExtensionsInfo[0];
  /// <summary>
  /// Кэш FileExtensionsInfo для расширений, после успешного отображения, чтобы повторно не перебирать все настройки для данного расширения
  /// </summary>
  private ConcurrentDictionary<string, FileExtensionsInfo> _extensionCache = new ConcurrentDictionary<string, FileExtensionsInfo>();

  private List<int> PriorViewAuthFilesObjTypes { get; } = new List<int>();

  /// <summary>Конструктор</summary>
  public ExtensionsService() => this.ReadAllParams();

  /// <summary>методы для открытия просмотра</summary>
  public string Methods { get; private set; } = string.Empty;

  /// <summary>свойства для открытия просмотра</summary>
  public string Properties { get; private set; } = string.Empty;

  /// <summary>
  /// Отладочный режим - включает/выключает запись отладочной информации при просмотре файлов
  /// </summary>
  public bool DebugMode { get; private set; }

  /// <summary>
  /// Записывать подписи и параметры в файл перед просмотром
  /// </summary>
  public bool WriteSignsAndParams { get; private set; }

  /// <summary>
  /// Сохраняемые настройки - беремся все что есть у пользователя и добавляем чего нет от общих настроек.
  /// </summary>
  /// <returns></returns>
  public IReadOnlyCollection<FileExtensionsInfo> GetStoredFileExtensionsInfo()
  {
    lock (this._syncRoot)
      return (IReadOnlyCollection<FileExtensionsInfo>) this._userFileExtInfo.Concat<FileExtensionsInfo>(this._commonFileExtInfo.Except<FileExtensionsInfo>((IEnumerable<FileExtensionsInfo>) this._userFileExtInfo)).ToList<FileExtensionsInfo>();
  }

  /// <summary>
  /// Получить перечень типов объектов, для которых приоритетный порядок отображения аутентичных файлов
  /// </summary>
  /// <param name="objTypes"></param>
  public IReadOnlyCollection<int> GetPriorityViewAuthenticFileObjTypes()
  {
    return (IReadOnlyCollection<int>) this.PriorViewAuthFilesObjTypes;
  }

  /// <summary>Изменить настройки для просмотра файлов</summary>
  /// <param name="newSettings">новые настройки</param>
  /// <param name="methods">новое значение методы для открытия просмотра</param>
  /// <param name="properties">новое значние свойства для открытия просмотра</param>
  /// <param name="debugMode"></param>
  /// <param name="writeSignsAndParams"></param>
  /// <param name="priorViewAuthFilesObjTypes"></param>
  /// 
  ///             приходят только сохраняемые настройки
  public void ChangeSettings(
    IReadOnlyCollection<FileExtensionsInfo> newSettings,
    string methods,
    string properties,
    bool debugMode,
    bool writeSignsAndParams,
    IReadOnlyCollection<int> priorViewAuthFilesObjTypes)
  {
    lock (this._syncRoot)
    {
      this.Methods = methods;
      this.Properties = properties;
      this.DebugMode = debugMode;
      this.WriteSignsAndParams = writeSignsAndParams;
      this.PriorViewAuthFilesObjTypes.Clear();
      this.PriorViewAuthFilesObjTypes.AddRange((IEnumerable<int>) priorViewAuthFilesObjTypes);
      this._userFileExtInfo.Clear();
      this._userFileExtInfo.AddRange((IEnumerable<FileExtensionsInfo>) newSettings);
      List<FileExtensionsInfo> list = newSettings.Where<FileExtensionsInfo>((System.Func<FileExtensionsInfo, bool>) (x => x.IsAllUser)).ToList<FileExtensionsInfo>();
      this._commonFileExtInfo.Clear();
      this._commonFileExtInfo.AddRange((IEnumerable<FileExtensionsInfo>) list);
      this.WriteConfigurations();
      this.ComputeAllFileExtInfo();
    }
  }

  /// <summary>Обновить настройки</summary>
  public void CheckDefaultFileExtensions()
  {
    lock (this._syncRoot)
    {
      FileExtensionsInfo[] array = this.GetDefaultFileExtensionInfo().Except<FileExtensionsInfo>((IEnumerable<FileExtensionsInfo>) this.GetStoredFileExtensionsInfo()).ToArray<FileExtensionsInfo>();
      if (array.Length != 0)
        this._userFileExtInfo.AddRange((IEnumerable<FileExtensionsInfo>) array);
      this.ComputeAllFileExtInfo();
    }
  }

  /// <summary>Перечитать настройки</summary>
  public void ReloadParams() => this.ReadAllParams();

  /// <summary>Получить настройки просмотра для расширения</summary>
  /// <param name="extension"></param>
  /// <returns></returns>
  public IReadOnlyCollection<FileExtensionsInfo> GetFileExtensionsInfo(string @extension)
  {
    FileExtensionsInfo fileExtensionsInfo;
    if (this._extensionCache.TryGetValue(@extension, out fileExtensionsInfo))
      return (IReadOnlyCollection<FileExtensionsInfo>) new List<FileExtensionsInfo>()
      {
        fileExtensionsInfo
      };
    return string.IsNullOrEmpty(@extension) ? (IReadOnlyCollection<FileExtensionsInfo>) null : (IReadOnlyCollection<FileExtensionsInfo>) this._allFileExtInfo.Where<FileExtensionsInfo>((System.Func<FileExtensionsInfo, bool>) (x => x.IsMatch(@extension) && x.Enabled)).OrderBy<FileExtensionsInfo, StyleView>((System.Func<FileExtensionsInfo, StyleView>) (x => x.Style)).ToList<FileExtensionsInfo>();
  }

  /// <summary>
  /// Добавить настройку в кэш спешно просматриваемых, для данного расшиерния
  /// </summary>
  /// <param name="extension"></param>
  /// <param name="fileExtensionsInfo"></param>
  public void AddFileExtensionInfoToCache(string @extension, FileExtensionsInfo fileExtensionsInfo)
  {
    this._extensionCache.TryAdd(@extension, fileExtensionsInfo);
  }

  /// <summary>Прочитать все параметры</summary>
  private void ReadAllParams()
  {
    lock (this._syncRoot)
    {
      this.ReadCurrentUserParams();
      this.ReadUserFileExtInfo();
      this.ReadCommonUsersParams();
      this.ReadCommonFileExtInfo();
      this.ReadNotStoredFileExtensionsInfo();
      this.ComputeAllFileExtInfo();
    }
  }

  /// <summary>Объединение свойств/методов открытия</summary>
  /// <param name="listStr"></param>
  /// <param name="newListStr"></param>
  /// <returns></returns>
  private string MergeLists(string listStr, string newListStr)
  {
    if (listStr == null)
      listStr = string.Empty;
    if (newListStr == null)
      newListStr = string.Empty;
    return string.Join(",", ((IEnumerable<string>) listStr.Split(',')).ToList<string>().Concat<string>((IEnumerable<string>) ((IEnumerable<string>) newListStr.Split(',')).ToList<string>()).Distinct<string>()).Trim(',');
  }

  /// <summary>Чтение параметров "родных" форматов</summary>
  private void ReadNativeFormats()
  {
    List<string> values = ServiceUtils.GetService<IVisualizerService>((object) ServicesManager.ServiceContainer, true).SupportedExtensions();
    if (values.Count > 0)
      this._notStoredFileExtInfo.Add(new FileExtensionsInfo(true, string.Empty, "NativeHandler", string.Join(",", (IEnumerable<string>) values))
      {
        IsAllUser = false,
        NotPersist = true
      });
    string supportExtensions = ServiceUtils.GetService<IPreviewExtractService>((object) ServicesManager.ServiceContainer, true).GetAllSupportExtensions();
    if (!string.IsNullOrEmpty(supportExtensions))
      this._notStoredFileExtInfo.Add(new FileExtensionsInfo(true, string.Empty, "InternalExtractView", supportExtensions)
      {
        IsAllUser = false,
        NotPersist = true
      });
    List<string> list = InternalViewHostMapping.PreviewHostsMapping.SelectMany<Tuple<InternalViewerHost, List<string>>, string>((System.Func<Tuple<InternalViewerHost, List<string>>, IEnumerable<string>>) (x => (IEnumerable<string>) x.Item2)).ToList<string>();
    if (list.Count > 0)
      this._notStoredFileExtInfo.Add(new FileExtensionsInfo(true, string.Empty, "InternalHandler", string.Join(",", (IEnumerable<string>) list))
      {
        IsAllUser = false,
        NotPersist = true
      });
    this._notStoredFileExtInfo.Add(new FileExtensionsInfo(true, "ImViewer", "IMVIEWEROCX.IMViewerOCXCtrl.1", "imv")
    {
      IsAllUser = false,
      NotPersist = true
    });
  }

  /// <summary>Зачитать настройки просмотра из реестра</summary>
  private void ReadSettingsFromRegistry()
  {
    foreach (string subKeyName in Registry.ClassesRoot.GetSubKeyNames())
    {
      if (subKeyName.StartsWith("."))
      {
        string extensions = subKeyName.Remove(0, 1);
        Guid previewHandlerGuid = RegistryHelper.GetPreviewHandlerGUID(subKeyName);
        if (previewHandlerGuid != Guid.Empty)
        {
          string name1 = "CLSID\\" + previewHandlerGuid.ToString("B");
          RegistryKey registryKey1;
          string name2;
          using (registryKey1 = Registry.ClassesRoot.OpenSubKey(name1, false))
          {
            if (registryKey1 == null)
            {
              using (RegistryKey registryKey2 = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32))
                registryKey1 = registryKey2.OpenSubKey(name1, false);
            }
            if (registryKey1 != null)
              name2 = registryKey1.GetValue("") as string;
            else
              continue;
          }
          this._notStoredFileExtInfo.Add(new FileExtensionsInfo(true, name2, "PreviewHandler", extensions, previewHandlerGuid)
          {
            IsAllUser = false
          });
        }
        Guid extractImageGuid = RegistryHelper.GetExtractImageGUID(subKeyName);
        if (extractImageGuid != Guid.Empty)
          this._notStoredFileExtInfo.Add(new FileExtensionsInfo(true, "ExtractImage " + subKeyName, "ExtractImage", extensions, extractImageGuid)
          {
            IsAllUser = false
          });
        Guid thumbnailProviderGuid = RegistryHelper.GetThumbnailProviderGUID(subKeyName);
        if (thumbnailProviderGuid != Guid.Empty)
          this._notStoredFileExtInfo.Add(new FileExtensionsInfo(true, "PrevThumbnail " + subKeyName, "PrevThumbnail", extensions, thumbnailProviderGuid)
          {
            IsAllUser = false
          });
      }
    }
  }

  /// <summary>Прочитать настройки не хранящиеся в базе</summary>
  private void ReadNotStoredFileExtensionsInfo()
  {
    this._notStoredFileExtInfo.Clear();
    this.ReadNativeFormats();
    this.ReadSettingsFromRegistry();
  }

  /// <summary>Получить настройки для расширений по умолчанию</summary>
  /// <returns></returns>
  private IEnumerable<FileExtensionsInfo> GetDefaultFileExtensionInfo()
  {
    return (IEnumerable<FileExtensionsInfo>) ((IEnumerable<Tuple<string, string, string>>) this._defaultSettingsTemplate).Select<Tuple<string, string, string>, FileExtensionsInfo>((System.Func<Tuple<string, string, string>, FileExtensionsInfo>) (x => new FileExtensionsInfo(false, x.Item1, x.Item2, x.Item3))).Where<FileExtensionsInfo>((System.Func<FileExtensionsInfo, bool>) (x => !x.IsUnknown)).ToList<FileExtensionsInfo>();
  }

  /// <summary>
  /// Рассчитать все настройки - суммирует хранимые и нехранимые настройки
  /// </summary>
  private void ComputeAllFileExtInfo()
  {
    this._allFileExtInfo = (IReadOnlyCollection<FileExtensionsInfo>) this.GetStoredFileExtensionsInfo().Concat<FileExtensionsInfo>((IEnumerable<FileExtensionsInfo>) this._notStoredFileExtInfo).ToList<FileExtensionsInfo>();
  }

  /// <summary>Прочитать настройки текущего пользователя</summary>
  private void ReadCurrentUserParams()
  {
    IDBConfigurations service = ApplicationServices.Container.GetService<IDBConfigurations>();
    this.Methods = service.ReadString("COMMON", "ExtensionsSection", "Methods", "Navigate,SetURL,LoadFile,Open", DBConfigMode.UserOnly);
    this.Properties = service.ReadString("COMMON", "ExtensionsSection", "Properties", "Src,src,FileName,Filename,URL,Movie,DocumenFileName,sourceUrl", DBConfigMode.UserOnly);
    this.DebugMode = service.ReadBool("COMMON", "ExtensionsSection", "DebugMode", false, DBConfigMode.UserOnly);
  }

  /// <summary>
  /// Прочитать параметры для расширений текущего пользователя
  /// </summary>
  private void ReadUserFileExtInfo()
  {
    this._userFileExtInfo.Clear();
    DataTable source = ApplicationServices.Container.GetService<IDBConfigurations>().ReadSection("COMMON", "ExtensionsSettings", ApplicationServices.Container.GetService<ICurrentUserAndRole>().UserID);
    if (source.Rows.Count > 0)
    {
      FileExtensionsInfo[] array = source.AsEnumerable().Select<DataRow, string>((System.Func<DataRow, string>) (x => x.Field<string>("F_VALUE"))).Where<string>((System.Func<string, bool>) (x => !string.IsNullOrEmpty(x))).Select<string, FileExtensionsInfo>((System.Func<string, FileExtensionsInfo>) (x => new FileExtensionsInfo(x))).ToArray<FileExtensionsInfo>();
      this._userFileExtInfo.AddRange((IEnumerable<FileExtensionsInfo>) ((IEnumerable<FileExtensionsInfo>) array).Where<FileExtensionsInfo>((System.Func<FileExtensionsInfo, bool>) (x => !x.NotPersist)).ToArray<FileExtensionsInfo>());
      if (!((IEnumerable<FileExtensionsInfo>) array).Any<FileExtensionsInfo>((System.Func<FileExtensionsInfo, bool>) (x => x.NotPersist)))
        return;
      this.WriteUserFileExtInfo();
    }
    else
    {
      this._userFileExtInfo.AddRange(this.GetDefaultFileExtensionInfo());
      this.WriteUserFileExtInfo();
    }
  }

  /// <summary>Прочитать общие настройки</summary>
  private void ReadCommonUsersParams()
  {
    IDBConfigurations service = ApplicationServices.Container.GetService<IDBConfigurations>();
    string newListStr1 = service.ReadString("COMMON", "ExtensionsSection", "Methods", string.Empty, DBConfigMode.GlobalOnly);
    string newListStr2 = service.ReadString("COMMON", "ExtensionsSection", "Properties", string.Empty, DBConfigMode.GlobalOnly);
    this.Methods = this.MergeLists(this.Methods, newListStr1);
    this.Properties = this.MergeLists(this.Properties, newListStr2);
    this.WriteSignsAndParams = service.ReadBool("COMMON", "ExtensionsSection", "WriteSignsAndParams", false, DBConfigMode.GlobalOnly);
    string str = service.ReadString("COMMON", "ExtensionsSection", "ViewAuthFileObjTypes", string.Empty, DBConfigMode.GlobalOnly);
    if (string.IsNullOrEmpty(str))
      return;
    this.PriorViewAuthFilesObjTypes.Clear();
    this.PriorViewAuthFilesObjTypes.AddRange(((IEnumerable<string>) str.Split(',')).Select<string, int>((System.Func<string, int>) (x => Convert.ToInt32(x))).Where<int>((System.Func<int, bool>) (x => MetaDataHelper.GetObjectType(x) != null)).Distinct<int>());
  }

  /// <summary>Прочитать общие параметры для расширений</summary>
  private void ReadCommonFileExtInfo()
  {
    this._commonFileExtInfo.Clear();
    IDBConfigurations service1 = ApplicationServices.Container.GetService<IDBConfigurations>();
    ICurrentUserAndRole service2 = ApplicationServices.Container.GetService<ICurrentUserAndRole>();
    DataTable source = service1.ReadSection("COMMON", "ExtensionsSettings", 0L);
    if (source.Rows.Count <= 0)
      return;
    FileExtensionsInfo[] array = source.AsEnumerable().Select<DataRow, string>((System.Func<DataRow, string>) (x => x.Field<string>("F_VALUE"))).Where<string>((System.Func<string, bool>) (x => !string.IsNullOrEmpty(x))).Select<string, FileExtensionsInfo>((System.Func<string, FileExtensionsInfo>) (x => new FileExtensionsInfo(x))).Where<FileExtensionsInfo>((System.Func<FileExtensionsInfo, bool>) (x => x.IsAllUser)).ToArray<FileExtensionsInfo>();
    this._commonFileExtInfo.AddRange((IEnumerable<FileExtensionsInfo>) ((IEnumerable<FileExtensionsInfo>) array).Where<FileExtensionsInfo>((System.Func<FileExtensionsInfo, bool>) (x => !x.NotPersist)).ToArray<FileExtensionsInfo>());
    if (!((IEnumerable<FileExtensionsInfo>) array).Any<FileExtensionsInfo>((System.Func<FileExtensionsInfo, bool>) (x => x.NotPersist)) || !service2.IsAdmin)
      return;
    this.WriteCommonFileExtInfo();
  }

  /// <summary>Сохранение настроек для просмотра файлов</summary>
  private void WriteConfigurations()
  {
    lock (this._syncRoot)
    {
      this._extensionCache.Clear();
      this.WriteUserParams();
      this.WriteUserFileExtInfo();
      this.WriteCommonParams();
      this.WriteCommonFileExtInfo();
    }
  }

  /// <summary>Сохранить параметры пользователя</summary>
  private void WriteUserParams()
  {
    IDBConfigurations service1 = ApplicationServices.Container.GetService<IDBConfigurations>();
    ICurrentUserAndRole service2 = ApplicationServices.Container.GetService<ICurrentUserAndRole>();
    service1.WriteString("COMMON", "ExtensionsSection", "Methods", this.Methods, service2.UserID);
    service1.WriteString("COMMON", "ExtensionsSection", "Properties", this.Properties, service2.UserID);
    service1.WriteBool("COMMON", "ExtensionsSection", "DebugMode", this.DebugMode, service2.UserID);
  }

  /// <summary>Сохранить настройки для расширений пользователя</summary>
  private void WriteUserFileExtInfo()
  {
    IDBConfigurations service1 = ApplicationServices.Container.GetService<IDBConfigurations>();
    ICurrentUserAndRole service2 = ApplicationServices.Container.GetService<ICurrentUserAndRole>();
    DataTable table = new DataTable();
    table.BeginLoadData();
    table.Columns.Add("F_PARAM_NAME", typeof (string));
    table.Columns.Add("F_VALUE", typeof (string));
    List<FileExtensionsInfo> storedFileExtensionsInfo = this.GetStoredFileExtensionsInfo().ToList<FileExtensionsInfo>();
    storedFileExtensionsInfo.ForEach((Action<FileExtensionsInfo>) (x =>
    {
      DataRow row = table.NewRow();
      row["F_PARAM_NAME"] = (object) storedFileExtensionsInfo.IndexOf(x).ToString();
      row["F_VALUE"] = (object) Convert.ToString((object) x, (IFormatProvider) CultureInfo.InvariantCulture);
      table.Rows.Add(row);
    }));
    table.EndLoadData();
    table.AcceptChanges();
    DataTable table1 = table;
    long userId = service2.UserID;
    service1.WriteSection("COMMON", "ExtensionsSettings", table1, userId);
  }

  /// <summary>Сохранить общие параметры</summary>
  private void WriteCommonParams()
  {
    IDBConfigurations service = ApplicationServices.Container.GetService<IDBConfigurations>();
    if (!ApplicationServices.Container.GetService<ICurrentUserAndRole>().IsAdmin)
      return;
    service.WriteString("COMMON", "ExtensionsSection", "Methods", this.Methods, 0L);
    service.WriteString("COMMON", "ExtensionsSection", "Properties", this.Properties, 0L);
    service.WriteBool("COMMON", "ExtensionsSection", "WriteSignsAndParams", this.WriteSignsAndParams, 0L);
    string str = string.Join<int>(",", (IEnumerable<int>) this.PriorViewAuthFilesObjTypes.Where<int>((System.Func<int, bool>) (x => MetaDataHelper.GetObjectType(x) != null)).Distinct<int>().OrderBy<int, int>((System.Func<int, int>) (x => x)));
    service.WriteString("COMMON", "ExtensionsSection", "ViewAuthFileObjTypes", str, 0L);
  }

  /// <summary>Сохранить общие настройки для расширений</summary>
  private void WriteCommonFileExtInfo()
  {
    if (!ApplicationServices.Container.GetService<ICurrentUserAndRole>().IsAdmin)
      return;
    IDBConfigurations service = ApplicationServices.Container.GetService<IDBConfigurations>();
    DataTable table = new DataTable();
    table.BeginLoadData();
    table.Columns.Add("F_PARAM_NAME", typeof (string));
    table.Columns.Add("F_VALUE", typeof (string));
    this._commonFileExtInfo.ForEach((Action<FileExtensionsInfo>) (x =>
    {
      DataRow row = table.NewRow();
      row["F_PARAM_NAME"] = (object) this._commonFileExtInfo.IndexOf(x).ToString();
      row["F_VALUE"] = (object) Convert.ToString((object) x, (IFormatProvider) CultureInfo.InvariantCulture);
      table.Rows.Add(row);
    }));
    table.EndLoadData();
    table.AcceptChanges();
    DataTable table1 = table;
    service.WriteSection("COMMON", "ExtensionsSettings", table1, 0L);
  }
}
