// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.Cadmech3DCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.CADInterface.Proxies;
using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.TechCard.Client.Commands;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// Контейнер команд контекстного меню для работы с параметрами моделей (интеграция с Cadmech 3D)
/// </summary>
internal abstract class Cadmech3DCommand : TechCardSelectedItemsCommand, IDisposable
{
  /// <summary>Список всех типов объектов - моделей</summary>
  private List<int> _modelObjectTypes;
  /// <summary>Метод загрузки / поиска объекта - модели</summary>
  private readonly Cadmech3DCommand.CadModelLoadDelegate _modelLoadDelegate;
  /// <summary>Описание объекта модели</summary>
  protected ObjInfoItem _cadModelObjInfo;
  /// <summary>Документ CAD-системы (CADInterface)</summary>
  protected CADDocumentProxy _cadDoc;
  /// <summary>Документ IMTEXT (CADMECH)</summary>
  protected IMTextDocumentProxy _imTextDoc;
  /// <summary>
  /// 
  /// </summary>
  protected readonly ServiceContainer _container = new ServiceContainer();

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData()
  {
    this._modelObjectTypes = new List<int>();
    IMSObjectType objectType1 = MetaDataHelper.GetObjectType(TechCardConsts.ObjectTypes.ExternalCADModelTypeGuid);
    if (objectType1 == null)
      return;
    if (objectType1.VersionsMode != ObjectVersionModes.Abstract)
      this._modelObjectTypes.Add(objectType1.ObjectTypeID);
    foreach (int objTypeID in MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectType1.ObjectTypeID))
    {
      IMSObjectType objectType2 = MetaDataHelper.GetObjectType(objTypeID);
      if (objectType2.VersionsMode != ObjectVersionModes.Abstract)
        this._modelObjectTypes.Add(objectType2.ObjectTypeID);
    }
    this._modelObjectTypes.Sort();
    if (this._container.GetService<IIMCadSettings>(false) != null)
      return;
    IIMCadSettings settings;
    new IMCadSettingsService().LoadSettings(out settings);
    if (settings == null)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString(sc_19222.ssp_techcard_19223()), (object) typeof (IIMCadSettings)));
    this._container.AddService(typeof (IIMCadSettings), (object) settings);
  }

  /// <summary>Проверка параметров команды</summary>
  /// <returns></returns>
  protected override bool ValidateCommandArgs()
  {
    return this.Items != null && this.ContextServices != null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool LoadCommandInfo()
  {
    return base.LoadCommandInfo() && this.LoadCadModelObjInfo() && !ObjInfoItem.IsEmpty((ITypedInfoItem) this._cadModelObjInfo);
  }

  /// <summary>Поиск информации о модели для тек. объекта</summary>
  /// <returns></returns>
  protected virtual bool LoadCadModelObjInfo()
  {
    return this._modelLoadDelegate != null && this._modelLoadDelegate(this, out this._cadModelObjInfo);
  }

  /// <summary>Реализация CAD команды / вызов интегратора</summary>
  protected override bool ExecuteCommand()
  {
    IntegratorObject integrator = IntegratorServices.Find(this._cadModelObjInfo.ObjTypeID);
    if (integrator == null)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(this._cadModelObjInfo.ObjTypeID);
      throw new Exception(string.Format(LocalizationHolder.rm.GetString(sc_19222.ssp_techcard_19224()), (object) objectType.ObjectTypeName));
    }
    string fullName = ClientContext.FileVault.PublishTree(this._cadModelObjInfo.ObjectID, ClientContext.FileVault.DBFilesInfo.GetMasterFileName(this._cadModelObjInfo.ObjectID, true), VersionsRuleSources.GetEditorRule(), (IFileArea) ClientContext.FileVault.WorkArea);
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) IntegratorServices.GetService<ICADInterfaceService>(integrator, true)))
    {
      this._cadDoc = cadApiSession.Application.OpenDocument(fullName, true);
      try
      {
        this._imTextDoc = this._cadDoc.GetIMTextDocument(true);
        return this.DoExecuteCadCommand();
      }
      finally
      {
        this.SwitchToThisApp();
        this._cadDoc.Close();
        this._cadDoc = (CADDocumentProxy) null;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  protected abstract bool DoExecuteCadCommand();

  /// <summary>
  /// 
  /// </summary>
  protected void SwitchToThisApp()
  {
    IMainFormUpdate service = (IMainFormUpdate) ApplicationServices.Container.GetService(typeof (IMainFormUpdate));
    if (service == null)
      return;
    ForegroundWindowHelper.Default.TrySetWindow(service.MainForm.Handle);
  }

  /// <summary>Конструктор</summary>
  /// <param name="name"></param>
  /// <param name="modelLoader"></param>
  protected Cadmech3DCommand(string name, Cadmech3DCommand.CadModelLoadDelegate modelLoader = null)
    : base(name)
  {
    this._modelLoadDelegate = modelLoader;
    this.InitializeData();
  }

  /// <summary>Поиск модели для текущего объекта</summary>
  /// <param name="command"></param>
  /// <param name="cadModelObjInfo"></param>
  /// <returns></returns>
  internal static bool FindModelForObject(Cadmech3DCommand command, out ObjInfoItem cadModelObjInfo)
  {
    cadModelObjInfo = (ObjInfoItem) null;
    List<ObjInfoItem> objInfoList = new List<ObjInfoItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<ObjInfoItem> articles4Object = TechCardObjUtils.Article.GetArticles4Object(command._selectedObjInfo, sessionKeeper.Session);
      if (articles4Object == null || articles4Object.Count == 0)
        return false;
      DBRecordSetParams dbRsp = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) command._modelObjectTypes.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.Text)
      });
      DataTable childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) articles4Object, sessionKeeper.Session, (IEnumerable<int>) new int[1]
      {
        MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545")
      }, false, dbRsp);
      if (childSostavData == null || childSostavData.Rows.Count == 0)
        return false;
      int columnIndex1 = childSostavData.Columns.IndexOf("F_OBJECT_ID");
      int columnIndex2 = childSostavData.Columns.IndexOf("F_OBJECT_TYPE");
      foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
      {
        int int32 = Convert.ToInt32(row[columnIndex2]);
        if (command._modelObjectTypes.BinarySearch(int32) >= 0)
        {
          long int64 = Convert.ToInt64(row[columnIndex1]);
          switch (int64)
          {
            case -1:
            case 0:
              continue;
            default:
              objInfoList.Add(new ObjInfoItem(int64, int32));
              continue;
          }
        }
      }
    }
    switch (objInfoList.Count)
    {
      case 0:
        string caption1;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          caption1 = sessionKeeper.Session.GetObjectInfo(command._selectedObjInfo.ObjectID).Caption;
        string.Format(LocalizationHolder.rm.GetString(sc_19222.ssp_techcard_19225()), (object) caption1, (object) command._selectedObjInfo.ObjectID);
        break;
      case 1:
        cadModelObjInfo = objInfoList[0];
        break;
      default:
        string caption2;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          caption2 = sessionKeeper.Session.GetObjectInfo(command._selectedObjInfo.ObjectID).Caption;
        string rootCaption = string.Format(LocalizationHolder.rm.GetString(sc_19222.ssp_techcard_19226()), (object) caption2);
        List<long> longList = TechCardClientConst.SelectObjectOnlyDlg(MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ExternalCADModelTypeGuid), (IList<ObjInfoItem>) objInfoList, rootCaption, LocalizationHolder.rm.GetString(sc_19222.ssp_techcard_19227()));
        if (longList == null || longList.Count == 0)
          return false;
        int index = objInfoList.IndexOf(new ObjInfoItem(longList[0]));
        cadModelObjInfo = objInfoList[index];
        break;
    }
    return !ObjInfoItem.IsEmpty((ITypedInfoItem) cadModelObjInfo);
  }

  /// <summary>Выбор модели из списка объектов</summary>
  /// <param name="command"></param>
  /// <param name="cadModelObjInfo"></param>
  /// <returns></returns>
  internal static bool SelectModelFromList(
    Cadmech3DCommand command,
    out ObjInfoItem cadModelObjInfo)
  {
    cadModelObjInfo = (ObjInfoItem) null;
    if (SelectionWindow.Select(LocalizationHolder.rm.GetString("TechCard.Client_498"), Intermech.Navigator.DBObjectTypes.Descriptor.CreateComposition((IEnumerable<int>) command._modelObjectTypes), typeof (IDBObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect | SelectionOptions.ForceRebuildNavTree) is IDBObjectID[] dbObjectIdArray && dbObjectIdArray.Length != 0)
    {
      cadModelObjInfo = new ObjInfoItem(dbObjectIdArray[0].Value);
      if (dbObjectIdArray[0] is IDBObjectTypeID dbObjectTypeId)
      {
        cadModelObjInfo.ObjTypeID = dbObjectTypeId.Value;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          ObjInfoHelper.UpdateUnknownType(cadModelObjInfo, sessionKeeper.Session);
      }
    }
    return !ObjInfoItem.IsEmpty((ITypedInfoItem) cadModelObjInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Dispose() => this._container?.Dispose();

  /// <summary>Кастом делегат загрузки информации о модели</summary>
  /// <param name="command"></param>
  /// <param name="cadModelObjInfo"></param>
  /// <returns></returns>
  internal delegate bool CadModelLoadDelegate(
    Cadmech3DCommand command,
    out ObjInfoItem cadModelObjInfo);
}
