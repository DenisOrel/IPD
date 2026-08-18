// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechCardConsts
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Collections;
using Intermech.Expert;
using Intermech.Extensions;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Глобальные константы TechCard</summary>
public static class TechCardConsts
{
  /// <summary>
  /// 
  /// </summary>
  static TechCardConsts()
  {
    TechCardConsts.MetaDataHelper = (IMetaDataHelper) MetaDataHelperService.Instance;
  }

  /// <summary>Инициализация статических данных класса</summary>
  /// <param name="metaDataHelper"></param>
  public static void Init(IMetaDataHelper metaDataHelper)
  {
    TechCardConsts.MetaDataHelper = metaDataHelper ?? throw new ArgumentNullException(nameof (metaDataHelper));
    TechCardConsts.ObjectTypes.InitData(metaDataHelper);
    TechCardConsts.AttributeTypes.InitData(metaDataHelper);
    TechCardConsts.RelTypes.InitData(metaDataHelper);
  }

  /// <summary>Интерфейс для доступа к метаданным</summary>
  internal static IMetaDataHelper MetaDataHelper { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public static class Consts
  {
    /// <summary>Packet size in SQL command</summary>
    public static int SQL_PACKET_SIZE = 400;
  }

  /// <summary>
  /// 
  /// </summary>
  public static class Params
  {
    /// <summary>
    /// Имя параметра расширения метаданных - "Наследовать права доступа архива техпроцесса"
    /// </summary>
    public static readonly string MdeInheritArchiveRightsFromTechProc = "Techcard.InheritArchiveRights";
    /// <summary>
    /// Наименование ключа пользовательской сессии для получения списка объектов, для которых возможно копирование эскизов Cadmech
    /// </summary>
    public static readonly string ObjWithCadmechDraft2CopyPluginData = nameof (ObjWithCadmechDraft2CopyPluginData);
  }

  /// <summary>Techcard caches class</summary>
  public static class Caches
  {
    /// <summary>
    /// 
    /// </summary>
    public static readonly IReadOnlyDictionary<Guid, Keys> TechCardHotKeys = (IReadOnlyDictionary<Guid, Keys>) new Dictionary<Guid, Keys>()
    {
      {
        TechCardConsts.ObjectTypes.OborudGUID,
        Keys.B | Keys.Alt
      },
      {
        TechCardConsts.ObjectTypes.PerehodGUID,
        Keys.O | Keys.Alt
      },
      {
        TechCardConsts.ObjectTypes.OsnastkaGUID,
        Keys.T | Keys.Alt
      },
      {
        TechCardConsts.ObjectTypes.RegimGUID,
        Keys.R | Keys.Alt
      }
    };

    [Obsolete("Use ObjectTypes.TechNotInheritedBaseObjTypes instead", false)]
    public static IReadOnlyList<int> TechNotInrehitedBaseObjTypes
    {
      get => TechCardConsts.ObjectTypes.TechNotInheritedBaseObjTypes;
    }

    /// <summary>Список типов связей TechCard</summary>
    [Obsolete("Use RelTypes.TechAllRelationTypes instead", false)]
    public static IReadOnlyList<int> TechRelationTypes
    {
      get => TechCardConsts.RelTypes.TechAllRelationTypes;
    }

    [Obsolete("Use TechCardHotKeys instead", false)]
    public static IReadOnlyDictionary<Guid, Keys> TechHotKeys
    {
      get => TechCardConsts.Caches.TechCardHotKeys;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public static class Objects
  {
    /// <summary>
    /// Объект типа "Вид производства" - "Слесарно-сборочные работы"
    /// </summary>
    public static readonly Guid AssemblingProductionObjectGUID = new Guid("cad0141d-306c-11d8-b4e9-00304f19f545");
    /// <summary>Физическая величина "Время"</summary>
    public static readonly Guid TimeMeasureObjectGuid = new Guid("cad002e0-306c-11d8-b4e9-00304f19f545");
  }

  /// <summary>
  /// 
  /// </summary>
  public class ObjectTypes
  {
    /// <summary>
    /// 
    /// </summary>
    private static IReadOnlyList<int> _techBaseUserObjectIds;
    /// <summary>
    /// 
    /// </summary>
    private static IReadOnlyList<int> _techSpecialObjectIds;
    /// <summary>тип объектов "Технологический объект"</summary>
    [IsObjectType(true)]
    public static readonly Guid TechBaseObjectGUID = new Guid("cad00163-306c-11d8-b4e9-00304f19f545");
    /// <summary>Перечень технологических пользовательских объектов</summary>
    /// <remarks>Для возможности изменение иерархии пользовательских типов</remarks>
    public static readonly Guid[] TechBaseUserObjectGuids = new Guid[17]
    {
      new Guid("cadd997c-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9be0-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9be1-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9be2-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9be3-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9be4-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9be5-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9be6-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9be7-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9be8-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9be9-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bea-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9beb-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bec-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bed-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bee-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bef-306c-11d8-b4e9-00304f19f545")
    };
    /// <summary>Перечень "специальных" технологических объектов,</summary>
    /// <remarks>
    /// При создании данных объектов требуется показывать карточку и диалог выбора из Imbase по команде "Добавить"
    /// </remarks>
    public static readonly Guid[] TechSpecialObjectGuids = new Guid[13]
    {
      new Guid("cadd9bf3-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bf4-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bf5-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bf6-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bf7-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bf8-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bf9-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bfa-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bfb-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bfc-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bfd-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bfe-306c-11d8-b4e9-00304f19f545"),
      new Guid("cadd9bff-306c-11d8-b4e9-00304f19f545")
    };
    /// <summary>тип объектов "Дополнительный прием"</summary>
    [IsObjectType(true)]
    public static readonly Guid DopPriemGUID = new Guid("cad00164-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Нормирование на дополнительный прием"</summary>
    [IsObjectType(true)]
    public static readonly Guid DopPriemNormGUID = new Guid("cad005c1-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Единица состава изделия"</summary>
    [IsObjectType(true)]
    public static readonly Guid EdinicaSostavaGUID = new Guid("cad00165-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Комплектующая единица"</summary>
    [IsObjectType(true)]
    public static readonly Guid KomlEdinicaGUID = new Guid("cad00166-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Собираемая единица"</summary>
    [IsObjectType(true)]
    public static readonly Guid SobirEdinicaGUID = new Guid("cad00167-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Комментарий"</summary>
    [IsObjectType(true)]
    public static readonly Guid CommentaryGUID = new Guid("cad00168-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Альбом КТД"</summary>
    [IsObjectType(true)]
    public static readonly Guid AlbumKTDGUID = new Guid("cad001a8-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Комплект ведомостей"</summary>
    [IsObjectType(true)]
    public static readonly Guid KomlectVedomGUID = new Guid("cad0016a-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Комплект технологических документов"</summary>
    [IsObjectType(true)]
    public static readonly Guid KomplectTechDocGUID = new Guid("cad0016b-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Комплект документов ТП"</summary>
    [IsObjectType(true)]
    public static readonly Guid KomplectTPDocGUID = new Guid("cad009ed-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Контролируемый параметр базовый"</summary>
    [IsObjectType(true)]
    public static readonly Guid ContrParamBaseGUID = new Guid("cad0016c-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Группа контролируемых параметров"</summary>
    [IsObjectType(true)]
    public static readonly Guid GrupContrParamGUID = new Guid("cad0016d-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Контролируемый параметр"</summary>
    [IsObjectType(true)]
    public static readonly Guid ContrParamGUID = new Guid("cad0016e-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип объектов "Объект технологического маршрута"</summary>
    [IsObjectType(true)]
    public static readonly Guid TechRoutingObjectGUID = new Guid("cadd9bba-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Маршрут обработки"</summary>
    [IsObjectType(true)]
    public static readonly Guid ProcRoutingGUID = new Guid("cad0016f-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Входимость маршрута обработки"</summary>
    [IsObjectType(true)]
    public static readonly Guid ProcRoutingEntryGUID = new Guid("cadd9bbb-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Маршрут обработки"</summary>
    [IsObjectType(true)]
    [Obsolete("Use ProcRoutingGUID instead")]
    public static readonly Guid MarshrObrabGUID = TechCardConsts.ObjectTypes.ProcRoutingGUID;
    /// <summary>тип объектов "Материал базовый"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid MaterialBaseGUID = new Guid("cad00170-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Марка"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid MarkaGUID = new Guid("cad00171-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Материал"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid MaterialGUID = new Guid("cad00172-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Материал составной"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid MaterialSostavnGUID = new Guid("cad00173-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Группа материалов"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid MaterialGroupGUID = new Guid("cadd9ab5-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Набор материалов"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid MaterialSetGUID = new Guid("cadd9abc-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Нормирование"</summary>
    [IsObjectType(true)]
    public static readonly Guid NormirovanieGUID = new Guid("cad00174-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Оборудование базовое"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid OborudBaseGUID = new Guid("cad00175-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Группа оборудования"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid GrupOborudGUID = new Guid("cad00176-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Оборудование"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid OborudGUID = new Guid("cad00177-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Операция"</summary>
    [IsObjectType(true)]
    public static readonly Guid OperaciyaGUID = new Guid("cad00178-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Нормирование на операцию"</summary>
    [IsObjectType(true)]
    public static readonly Guid OperationNormGUID = new Guid("CAD005C2-306C-11D8-B4E9-00304F19F545");
    /// <summary>тип объектов "Оснастка базовая"</summary>
    [IsObjectType(true)]
    public static readonly Guid OsnastBaseGUID = new Guid("cad00179-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Инструмент"</summary>
    [IsObjectType(true)]
    public static readonly Guid InstrumentGUID = new Guid("cad0017a-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Инструментальная позиция"</summary>
    [IsObjectType(true)]
    public static readonly Guid InstrumPosGUID = new Guid("cad0017b-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Оснастка"</summary>
    [IsObjectType(true)]
    public static readonly Guid OsnastkaGUID = new Guid("cad0017c-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Переход"</summary>
    [IsObjectType(true)]
    public static readonly Guid PerehodGUID = new Guid("cad0017d-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Нормирование на переход"</summary>
    [IsObjectType(true)]
    public static readonly Guid PerehodNormGUID = new Guid("CAD005C3-306C-11D8-B4E9-00304F19F545");
    /// <summary>тип объектов "Персонал базовый"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid PersonalBaseGUID = new Guid("cad0017e-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Группа персонала"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid GrupPersonalGUID = new Guid("cad0017f-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Персонал"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid PersonalGUID = new Guid("cad00180-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Поверхность базовая"</summary>
    [IsObjectType(true)]
    public static readonly Guid SurfaceBaseGUID = new Guid("cad00181-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Поверхность дополнительная"</summary>
    [IsObjectType(true)]
    public static readonly Guid SurfaceSlaveGUID = new Guid("cad00182-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Поверхность основная"</summary>
    [IsObjectType(true)]
    public static readonly Guid SurfaceMasterGUID = new Guid("cad00183-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип объектов "Параметры поверхности"</summary>
    [IsObjectType(true)]
    public static readonly Guid SurfaceParamGUID = new Guid("cadd961a-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Режим"</summary>
    [IsObjectType(true)]
    public static readonly Guid RegimGUID = new Guid("cad00184-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Техпроцесс базовый"</summary>
    [IsObjectType(true)]
    public static readonly Guid TechProcBaseGUID = new Guid("cad00185-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип объекта "Нормирование на техпроцесс"</summary>
    public static readonly Guid TechProcNormGUID = new Guid("cad005c4-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Техпроцесс групповой"</summary>
    [IsObjectType(true)]
    public static readonly Guid TechProcGroupGUID = new Guid("cad00186-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Техпроцесс единичный"</summary>
    [IsObjectType(true)]
    public static readonly Guid TechProcEdinGUID = new Guid("cad00187-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Техпроцесс типовой"</summary>
    [IsObjectType(true)]
    public static readonly Guid TechProcTipovGUID = new Guid("cad00188-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Требования базовые"</summary>
    [IsObjectType(true)]
    public static readonly Guid TrebovanBaseGUID = new Guid("cad00189-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Технические требования базовые"</summary>
    [IsObjectType(true)]
    public static readonly Guid TechnTrebovanBaseGUID = new Guid("cad0018a-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Группа технических требований"</summary>
    [IsObjectType(true)]
    public static readonly Guid GrTechnTrebovanGUID = new Guid("cad0018b-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Технические требования"</summary>
    [IsObjectType(true)]
    public static readonly Guid TechnTrebovanGUID = new Guid("cad0018c-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Технические условия базовые"</summary>
    [IsObjectType(true)]
    public static readonly Guid TechnUslovBaseGUID = new Guid("cad0018d-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Группа технических условий"</summary>
    [IsObjectType(true)]
    public static readonly Guid GrTechnUslovGUID = new Guid("cad0018e-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Технические условия"</summary>
    [IsObjectType(true)]
    public static readonly Guid TechnUslovGUID = new Guid("cad0018f-306c-11d8-b4e9-00304f19f545");
    /// <summary>
    /// тип объектов "Требования по технике безопасности базовые"
    /// </summary>
    [IsObjectType(true)]
    public static readonly Guid TrebovTechnBezopasnBaseGUID = new Guid("cad00190-306c-11d8-b4e9-00304f19f545");
    /// <summary>
    /// тип объектов "Группа требований по технике безопасности"
    /// </summary>
    [IsObjectType(true)]
    public static readonly Guid GrTrebovTechnBezopasnGUID = new Guid("cad00191-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Требования по технике безопасности"</summary>
    [IsObjectType(true)]
    public static readonly Guid TrebovTechnBezopasnGUID = new Guid("cad00192-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Эскиз базовый"</summary>
    [IsObjectType(true)]
    public static readonly Guid DraftBaseGUID = new Guid("cad00193-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Группа эскизов"</summary>
    [IsObjectType(true)]
    public static readonly Guid DraftGroupGUID = new Guid("cad00194-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Эскиз"</summary>
    [IsObjectType(true)]
    public static readonly Guid DraftGUID = new Guid("cad00195-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Эскиз OLE"</summary>
    [IsObjectType(true)]
    public static readonly Guid DraftOLEGUID = new Guid("cad005bc-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Эскиз Cadmech-T"</summary>
    [IsObjectType(true)]
    public static readonly Guid DraftCadmechGUID = new Guid("cad005bd-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Технологические документы"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid TechBaseDocGUID = new Guid("cad009ec-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Ведомость технологическая"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid TechDocReportGUID = new Guid("cad00197-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Технологический документ"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid TechDocGUID = new Guid("cad00198-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Комплект документов базовый"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid ComplectDocBaseGUID = new Guid("cad00199-306c-11d8-b4e9-00304f19f545");
    /// <summary>
    /// тип объектов "Комплект технологических документов базовый"
    /// </summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid ComlectTechDocBaseGUID = new Guid("cad00169-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Типовой элемент техпроцесса"</summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static readonly Guid TechProcElemBaseGUID = new Guid("cad001a2-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Заготовка"</summary>
    [IsObjectType(true)]
    public static readonly Guid ZagotGUID = new Guid("cad001da-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Расцеховочный объект"</summary>
    [IsObjectType(true)]
    public static readonly Guid CehBaseRouteGUID = new Guid("cad001e4-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Расцеховочный маршрут"</summary>
    [IsObjectType(true)]
    public static readonly Guid CehRouteGUID = new Guid("cad001e5-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Шаблон изготовления"</summary>
    [IsObjectType(true)]
    public static readonly Guid MkRouteGUID = new Guid("cad001e6-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Шаблон сборки"</summary>
    [IsObjectType(true)]
    public static readonly Guid SbRouteGUID = new Guid("cad001e7-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Расцеховочный элемент"</summary>
    [IsObjectType(true)]
    public static readonly Guid ElemRouteGUID = new Guid("cad001e8-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип объекта "Шаблон расцеховочного элемента"</summary>
    [IsObjectType(true)]
    public static readonly Guid ElemRouteTemplateGuid = new Guid("cadd9b7f-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Шаблон расцеховки базовый"</summary>
    [IsObjectType(true)]
    public static readonly Guid TemplRouteBaseGUID = new Guid("cad001fd-306c-11d8-b4e9-00304f19f545");
    /// <summary>
    /// Тип объекта "Заявка на специальную технологическую оснастку - СТО"
    /// </summary>
    [IsObjectType(true)]
    [NotInheritedBaseTechObjType(true)]
    public static Guid SpecialToolOrderGuid = new Guid("cadd951e-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип объектов "Специальная оснастка"</summary>
    private static readonly Guid SpecialToolGuid = new Guid("cadd955a-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Цехозаход"</summary>
    [IsObjectType(true)]
    public static readonly Guid CehZahodObjectGUID = new Guid("cad001ff-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Групповая заготовка"</summary>
    [IsObjectType(true)]
    public static readonly Guid ZagotGroupGUID = new Guid("cadd9c00-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Заготовка в ТП"</summary>
    [IsObjectType(true)]
    public static readonly Guid ZagotInTpGUID = new Guid("cadd9c72-306c-11d8-b4e9-00304f19f545");
    /// <summary>
    /// тип объектов "Формы редактирования атрибутов объектов и связей"
    /// </summary>
    [IsObjectType(false)]
    public static readonly Guid AttrFormGUID = new Guid("cad0011c-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Технологический объект представления"</summary>
    [IsObjectType(false)]
    public static readonly Guid NotionObjectGUID = new Guid("cad001bc-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Технологическое правило нумерации"</summary>
    [IsObjectType(false)]
    public static readonly Guid NumerationObjectBaseGUID = new Guid("cad001c3-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Правило нумерации"</summary>
    [IsObjectType(false)]
    public static readonly Guid NumerationRuleGUID = new Guid("cad001c4-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Элемент правила нумерации"</summary>
    [IsObjectType(false)]
    public static readonly Guid NumerationObjectGUID = new Guid("cad001c5-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип объекта "Изделия"</summary>
    /// <remarks>Базовый тип</remarks>
    [IsObjectType(false)]
    public static readonly Guid ArticleBaseGUID = new Guid("cad00268-306c-11d8-b4e9-00304f19f545");
    /// <summary>Производственная копия изделия</summary>
    [IsObjectType(false)]
    public static readonly Guid ArticleCopyBaseGUID = new Guid("cadd9a5d-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Вид работ/производства базовый"</summary>
    [IsObjectType(false)]
    public static readonly Guid WorkTypeBaseObjectGUID = new Guid("cad005ad-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Вид производства"</summary>
    [IsObjectType(false)]
    public static readonly Guid ProductTypeObjectGUID = new Guid("cad005ae-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Вид работ"</summary>
    [IsObjectType(false)]
    public static readonly Guid WorkTypeObjectGUID = new Guid("cad005af-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип объектов "Заказ"</summary>
    [IsObjectType(false)]
    public static readonly Guid ZakazObjectGUID = new Guid("cad00580-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип объектов "Электронные модели деталей"</summary>
    public static readonly Guid ExternalCADModelTypeGuid = new Guid("cad0078f-306c-11d8-b4e9-00304f19f545");

    /// <summary>
    /// 
    /// </summary>
    private static void InitFields(IMetaDataHelper metaDataHelper)
    {
      TechCardConsts.ObjectTypes.TechBaseObjectID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechBaseObjectGUID);
      TechCardConsts.ObjectTypes.TechProcBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechProcBaseGUID);
      TechCardConsts.ObjectTypes.TechProcGroupID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechProcGroupGUID);
      TechCardConsts.ObjectTypes.TechProcTipovID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechProcTipovGUID);
      TechCardConsts.ObjectTypes.TechProcEdinID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechProcEdinGUID);
      TechCardConsts.ObjectTypes.TechProcElemBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechProcElemBaseGUID);
      TechCardConsts.ObjectTypes.ProcRoutingID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ProcRoutingGUID);
      TechCardConsts.ObjectTypes.ProcRoutingEntryID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ProcRoutingEntryGUID);
      HashSet<int> hashSet1 = new HashSet<int>();
      foreach (Guid baseUserObjectGuid in TechCardConsts.ObjectTypes.TechBaseUserObjectGuids)
      {
        int objectTypeId = metaDataHelper.GetObjectTypeID(baseUserObjectGuid);
        if (objectTypeId != -1)
          hashSet1.Add(objectTypeId);
      }
      TechCardConsts.ObjectTypes._techBaseUserObjectIds = (IReadOnlyList<int>) hashSet1.ToArray<int>();
      HashSet<int> hashSet2 = new HashSet<int>();
      foreach (Guid specialObjectGuid in TechCardConsts.ObjectTypes.TechSpecialObjectGuids)
      {
        int objectTypeId = metaDataHelper.GetObjectTypeID(specialObjectGuid);
        if (objectTypeId != -1)
          hashSet2.Add(objectTypeId);
      }
      TechCardConsts.ObjectTypes._techSpecialObjectIds = (IReadOnlyList<int>) hashSet2.ToArray<int>();
      TechCardConsts.ObjectTypes.OperaciyaID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.OperaciyaGUID);
      TechCardConsts.ObjectTypes.PerehodID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.PerehodGUID);
      TechCardConsts.ObjectTypes.ElemRouteID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ElemRouteGUID);
      TechCardConsts.ObjectTypes.CehRouteID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.CehRouteGUID);
      TechCardConsts.ObjectTypes.TemplRouteBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TemplRouteBaseGUID);
      TechCardConsts.ObjectTypes.ElemRouteTemplateId = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ElemRouteTemplateGuid);
      TechCardConsts.ObjectTypes.DraftBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.DraftBaseGUID);
      TechCardConsts.ObjectTypes.DraftCadmechID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.DraftCadmechGUID);
      TechCardConsts.ObjectTypes.DraftOLEID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.DraftOLEGUID);
      TechCardConsts.ObjectTypes.DopPriemID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.DopPriemGUID);
      TechCardConsts.ObjectTypes.EdinicaSostavaID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.EdinicaSostavaGUID);
      TechCardConsts.ObjectTypes.SobirEdinicaID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.SobirEdinicaGUID);
      TechCardConsts.ObjectTypes.KomlEdinicaID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.KomlEdinicaGUID);
      TechCardConsts.ObjectTypes.ArticleBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ArticleBaseGUID);
      TechCardConsts.ObjectTypes.ArticleCopyBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ArticleCopyBaseGUID);
      TechCardConsts.ObjectTypes.CehZahodObjectID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.CehZahodObjectGUID);
      TechCardConsts.ObjectTypes.WorkTypeBaseObjectID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.WorkTypeBaseObjectGUID);
      TechCardConsts.ObjectTypes.NumerationRuleID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.NumerationRuleGUID);
      TechCardConsts.ObjectTypes.NumerationObjectID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.NumerationObjectGUID);
      TechCardConsts.ObjectTypes.OborudBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.OborudBaseGUID);
      TechCardConsts.ObjectTypes.NormirovanieID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.NormirovanieGUID);
      TechCardConsts.ObjectTypes.MaterialBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.MaterialBaseGUID);
      TechCardConsts.ObjectTypes.MaterialGroupID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.MaterialGroupGUID);
      TechCardConsts.ObjectTypes.MaterialSetID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.MaterialSetGUID);
      TechCardConsts.ObjectTypes.ZagotID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ZagotGUID);
      TechCardConsts.ObjectTypes.ZagotGroupID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ZagotGroupGUID);
      TechCardConsts.ObjectTypes.ZagotInTpID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ZagotInTpGUID);
      TechCardConsts.ObjectTypes.CehBaseRouteID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.CehBaseRouteGUID);
      TechCardConsts.ObjectTypes.ZakazObjectID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ZakazObjectGUID);
      TechCardConsts.ObjectTypes.WorkTypeObjectID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.WorkTypeObjectGUID);
      TechCardConsts.ObjectTypes.TechBaseDocID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechBaseDocGUID);
      TechCardConsts.ObjectTypes.TechDocReportID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechDocReportGUID);
      TechCardConsts.ObjectTypes.TechDocID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechDocGUID);
      TechCardConsts.ObjectTypes.ComplectDocBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ComplectDocBaseGUID);
      TechCardConsts.ObjectTypes.ComlectTechDocBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ComlectTechDocBaseGUID);
      TechCardConsts.ObjectTypes.PersonalBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.PersonalBaseGUID);
      TechCardConsts.ObjectTypes.GrupPersonalID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.GrupPersonalGUID);
      TechCardConsts.ObjectTypes.PersonalID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.PersonalGUID);
      TechCardConsts.ObjectTypes.SurfaceBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.SurfaceBaseGUID);
      TechCardConsts.ObjectTypes.SurfaceSlaveID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.SurfaceSlaveGUID);
      TechCardConsts.ObjectTypes.SurfaceMasterID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.SurfaceMasterGUID);
      TechCardConsts.ObjectTypes.SurfaceParamID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.SurfaceParamGUID);
      TechCardConsts.ObjectTypes.SpecialToolID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.SpecialToolGuid);
      TechCardConsts.ObjectTypes.SpecialToolOrderID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.SpecialToolOrderGuid);
      TechCardConsts.ObjectTypes.DocumentBaseID = metaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
      TechCardConsts.ObjectTypes.TechnTrebovanBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechnTrebovanBaseGUID);
      TechCardConsts.ObjectTypes.TechnUslovBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechnUslovBaseGUID);
      TechCardConsts.ObjectTypes.OsnastBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.OsnastBaseGUID);
      TechCardConsts.ObjectTypes.CommentaryID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.CommentaryGUID);
      TechCardConsts.ObjectTypes.ContrParamBaseID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ContrParamBaseGUID);
      TechCardConsts.ObjectTypes.InstrumPosID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.InstrumPosGUID);
      TechCardConsts.ObjectTypes.RegimID = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.RegimGUID);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="metaDataHelper"></param>
    private static void InitLists(IMetaDataHelper metaDataHelper)
    {
      TechCardConsts.ObjectTypes.InitNotInheritedTypes(metaDataHelper);
      TechCardConsts.ObjectTypes.TechCompositionGtpNonCloneTypes = (IReadOnlyList<int>) new int[1]
      {
        TechCardConsts.ObjectTypes.DraftCadmechID
      };
      TechCardConsts.ObjectTypes.TechCompositionNonCloneTypes = (IReadOnlyList<int>) new int[0];
      TechCardConsts.ObjectTypes.TechArtCompositionTypes = (IReadOnlyList<int>) new int[3]
      {
        TechCardConsts.ObjectTypes.ArticleBaseID,
        TechCardConsts.ObjectTypes.MaterialBaseID,
        TechCardConsts.ObjectTypes.ArticleCopyBaseID
      };
      HashSet<int> hashSet = new HashSet<int>();
      hashSet.Add(TechCardConsts.ObjectTypes.ArticleBaseID);
      hashSet.Add(TechCardConsts.ObjectTypes.ArticleCopyBaseID);
      List<IMSApplicability> parentApplicabilities = metaDataHelper.GetObjectTypeParentApplicabilities(TechCardConsts.ObjectTypes.ProcRoutingID);
      if (parentApplicabilities != null)
        hashSet.AddRange<int>(parentApplicabilities.Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (item => item.RelationTypeID == TechCardConsts.RelTypes.TechRelationID)).Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (item => item.InObjectType)));
      TechCardConsts.ObjectTypes.ArticleObjectTypes = (IReadOnlyList<int>) hashSet.ToArray<int>();
      List<int> intList = new List<int>();
      intList.Add(TechCardConsts.ObjectTypes.TechBaseObjectID);
      intList.AddRange((IEnumerable<int>) TechCardConsts.ObjectTypes.TechNotInheritedBaseObjTypes);
      intList.AddRange(TechCardConsts.ObjectTypes.TechBaseUserObjectIds);
      TechCardConsts.ObjectTypes.TechAllBaseObjTypes = (IReadOnlyList<int>) intList;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="metaDataHelper"></param>
    private static void InitNotInheritedTypes(IMetaDataHelper metaDataHelper)
    {
      HashedList<int> hashedList = new HashedList<int>();
      FieldInfo[] fields = typeof (TechCardConsts.ObjectTypes).GetFields();
      int objectTypeId1 = metaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechBaseObjectGUID);
      foreach (FieldInfo fieldInfo in fields)
      {
        if (!(fieldInfo.FieldType != typeof (Guid)) && fieldInfo.IsStatic)
        {
          object[] customAttributes1 = fieldInfo.GetCustomAttributes(typeof (IsObjectTypeAttribute), false);
          IsObjectTypeAttribute objectTypeAttribute = customAttributes1.Length != 0 ? customAttributes1[0] as IsObjectTypeAttribute : (IsObjectTypeAttribute) null;
          if (objectTypeAttribute != null && objectTypeAttribute.IsTechCardType)
          {
            int objectTypeId2 = metaDataHelper.GetObjectTypeID((Guid) fieldInfo.GetValue((object) null));
            if (objectTypeId2 != -1)
            {
              if (!metaDataHelper.IsObjectTypeChildOf(objectTypeId2, objectTypeId1) && !hashedList.Contains(objectTypeId2))
                hashedList.Add(objectTypeId2);
            }
            else
              continue;
          }
          object[] customAttributes2 = fieldInfo.GetCustomAttributes(typeof (NotInheritedBaseTechObjType), false);
          NotInheritedBaseTechObjType inheritedBaseTechObjType = customAttributes2.Length != 0 ? customAttributes2[0] as NotInheritedBaseTechObjType : (NotInheritedBaseTechObjType) null;
          if (inheritedBaseTechObjType != null && inheritedBaseTechObjType.Value)
          {
            int objectTypeId3 = metaDataHelper.GetObjectTypeID((Guid) fieldInfo.GetValue((object) null));
            if (objectTypeId3 != -1 && !hashedList.Contains(objectTypeId3))
              hashedList.Add(objectTypeId3);
          }
        }
      }
      TechCardConsts.ObjectTypes.TechNotInheritedBaseObjTypes = (IReadOnlyList<int>) hashedList;
    }

    /// <summary>
    /// 
    /// </summary>
    static ObjectTypes() => TechCardConsts.ObjectTypes.InitData(TechCardConsts.MetaDataHelper);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="metaDataHelper"></param>
    internal static void InitData(IMetaDataHelper metaDataHelper)
    {
      TechCardConsts.ObjectTypes.InitFields(metaDataHelper);
      TechCardConsts.ObjectTypes.InitLists(metaDataHelper);
    }

    /// <summary>
    /// Тип объектов "Технологический пользовательский объект"
    /// </summary>
    /// <remarks>Для возможности изменение иерархии пользовательских типов</remarks>
    [Obsolete("Use TechBaseUserObjectGuids instead. Will be removed in IPS 8.0", true)]
    public static Guid TechBaseUserObjectGuid
    {
      get
      {
        return ((IEnumerable<Guid>) TechCardConsts.ObjectTypes.TechBaseUserObjectGuids).FirstOrDefault<Guid>();
      }
    }

    /// <summary>тип объектов "Технологический объект"</summary>
    public static int TechBaseObjectID { get; private set; }

    /// <summary>
    /// тип объектов "Технологический пользовательский объект"
    /// </summary>
    [Obsolete("User TechBaseUserObjectIds instead. Will be removed in IPS 8.0", true)]
    public static int TechBaseUserObjectId
    {
      get
      {
        return !TechCardConsts.ObjectTypes.TechBaseUserObjectIds.Any<int>() ? -1 : TechCardConsts.ObjectTypes.TechBaseUserObjectIds.First<int>();
      }
    }

    /// <summary>тип объектов "Техпроцесс базовый"</summary>
    public static int TechProcBaseID { get; private set; }

    /// <summary>тип объектов "Техпроцесс групповой"</summary>
    public static int TechProcGroupID { get; private set; }

    /// <summary>тип объектов "Техпроцесс типовой"</summary>
    public static int TechProcTipovID { get; private set; }

    /// <summary>тип объектов "Техпроцесс единичный"</summary>
    public static int TechProcEdinID { get; private set; }

    /// <summary>тип объектов "Типовой элемент техпроцесса"</summary>
    public static int TechProcElemBaseID { get; private set; }

    /// <summary>тип объектов "Маршрут обработки"</summary>
    public static int ProcRoutingID { get; private set; }

    /// <summary>тип объектов "Входимость маршрута обработки"</summary>
    public static int ProcRoutingEntryID { get; private set; }

    /// <summary>тип объектов "Маршрут обработки"</summary>
    [Obsolete("Use ProcRoutingID instead")]
    public static int MarshrObrabID => TechCardConsts.ObjectTypes.ProcRoutingID;

    /// <summary>тип объектов "Операция"</summary>
    public static int OperaciyaID { get; private set; }

    /// <summary>Тип объектов "Переход"</summary>
    public static int PerehodID { get; private set; }

    /// <summary>тип объектов "Расцеховочный элемент"</summary>
    public static int ElemRouteID { get; private set; }

    /// <summary>Тип объекта "Шаблон расцеховочного элемента"</summary>
    public static int ElemRouteTemplateId { get; private set; }

    /// <summary>тип объектов "Шаблон расцеховки базовый"</summary>
    public static int TemplRouteBaseID { get; private set; }

    /// <summary>Тип объекта "Изделия"</summary>
    public static int ArticleBaseID { get; private set; }

    /// <summary>Тип объекта "Производственная копия изделия"</summary>
    public static int ArticleCopyBaseID { get; private set; }

    /// <summary>тип объектов "Расцеховочный маршрут"</summary>
    public static int CehRouteID { get; private set; }

    /// <summary>тип объектов "Эскиз базовый"</summary>
    public static int DraftBaseID { get; private set; }

    /// <summary>тип объектов "Эскиз Cadmech-T"</summary>
    public static int DraftCadmechID { get; private set; }

    /// <summary>тип объектов "Эскиз OLE"</summary>
    public static int DraftOLEID { get; private set; }

    /// <summary>тип объектов "Дополнительный прием"</summary>
    public static int DopPriemID { get; private set; }

    /// <summary>тип объектов "Единица состава изделия"</summary>
    public static int EdinicaSostavaID { get; private set; }

    /// <summary>тип объектов "Собираемая единица"</summary>
    public static int SobirEdinicaID { get; private set; }

    /// <summary>тип объектов "Комплектующая единица"</summary>
    public static int KomlEdinicaID { get; private set; }

    /// <summary>тип объектов "Оборудование базовое"</summary>
    public static int OborudBaseID { get; private set; }

    /// <summary>тип объектов "Материал базовый"</summary>
    public static int MaterialBaseID { get; private set; }

    /// <summary>тип объектов "Группа материалов"</summary>
    public static int MaterialGroupID { get; private set; }

    /// <summary>тип объектов "Набор материалов"</summary>
    public static int MaterialSetID { get; private set; }

    /// <summary>тип объектов "Заготовка"</summary>
    public static int ZagotID { get; private set; }

    /// <summary>тип объектов "Расцеховочный объект"</summary>
    public static int CehBaseRouteID { get; private set; }

    /// <summary>тип объектов "Персонал базовый"</summary>
    public static int PersonalBaseID { get; private set; }

    /// <summary>тип объектов "Группа персонала"</summary>
    public static int GrupPersonalID { get; private set; }

    /// <summary>Тип объектов "Персонал"</summary>
    public static int PersonalID { get; private set; }

    /// <summary>Тип объекта нормирование</summary>
    public static int NormirovanieID { get; private set; }

    /// <summary>Тип объектов "Поверхность базовая"</summary>
    public static int SurfaceBaseID { get; private set; }

    /// <summary>Тип объектов "Поверхность дополнительная"</summary>
    public static int SurfaceSlaveID { get; private set; }

    /// <summary>Тип объектов "Поверхность основная"</summary>
    public static int SurfaceMasterID { get; private set; }

    /// <summary>Тип объектов "Параметр поверхности"</summary>
    public static int SurfaceParamID { get; private set; }

    /// <summary>тип объектов "Цехозаход"</summary>
    public static int CehZahodObjectID { get; private set; }

    /// <summary>тип объектов "Технические требования базовые"</summary>
    public static int TechnTrebovanBaseID { get; private set; }

    /// <summary>тип объектов "Технические условия базовые"</summary>
    public static int TechnUslovBaseID { get; private set; }

    /// <summary>тип объектов "Оснастка базовая"</summary>
    public static int OsnastBaseID { get; private set; }

    /// <summary>тип объектов "Комментарий"</summary>
    public static int CommentaryID { get; private set; }

    /// <summary>тип объектов "Контролируемый параметр базовый"</summary>
    public static int ContrParamBaseID { get; private set; }

    /// <summary>тип объектов "Инструментальная позиция"</summary>
    public static int InstrumPosID { get; private set; }

    /// <summary>тип объектов "Режим"</summary>
    public static int RegimID { get; private set; }

    /// <summary>тип объекта "Групповая заготовка"</summary>
    public static int ZagotGroupID { get; private set; }

    /// <summary>тип объекта "Заготовка в ТП"</summary>
    public static int ZagotInTpID { get; private set; }

    /// <summary>Вид работ / вид производства базовый</summary>
    public static int WorkTypeBaseObjectID { get; private set; }

    /// <summary>тип объектов "Правило нумерации"</summary>
    public static int NumerationRuleID { get; private set; }

    /// <summary>тип объектов "Элемент правила нумерации"</summary>
    public static int NumerationObjectID { get; private set; }

    /// <summary>тип объектов "Заказ"</summary>
    public static int ZakazObjectID { get; private set; }

    /// <summary>тип объектов "Вид работ"</summary>
    public static int WorkTypeObjectID { get; private set; }

    /// <summary>тип объектов "Технологические документы"</summary>
    public static int TechBaseDocID { get; private set; }

    /// <summary>тип объектов "Ведомость технологическая"</summary>
    public static int TechDocReportID { get; private set; }

    /// <summary>тип объектов "Технологический документ"</summary>
    public static int TechDocID { get; private set; }

    /// <summary>тип объектов "Комплект документов базовый"</summary>
    public static int ComplectDocBaseID { get; private set; }

    /// <summary>
    /// тип объектов "Комплект технологических документов базовый"
    /// </summary>
    public static int ComlectTechDocBaseID { get; private set; }

    /// <summary>
    /// Тип объекта "Заявка на специальную технологическую оснастку - СТО"
    /// </summary>
    public static int SpecialToolOrderID { get; private set; }

    /// <summary>Тип объектов "Специальная оснастка"</summary>
    public static int SpecialToolID { get; private set; }

    /// <summary>тип объектов "Документы"</summary>
    public static int DocumentBaseID { get; private set; }

    /// <summary>
    /// Список всех базовых типов для изделий (включая производственные копии),
    /// для которых возможно создаение МО
    /// </summary>
    public static IReadOnlyList<int> ArticleObjectTypes { get; private set; }

    /// <summary>
    /// Список всех базовых технологических типов (включая не наследуемые)
    /// </summary>
    public static IReadOnlyList<int> TechAllBaseObjTypes { get; private set; }

    /// <summary>
    /// All techcard object parent types not inherited from base techcard type
    /// </summary>
    public static IReadOnlyList<int> TechNotInheritedBaseObjTypes { get; private set; }

    /// <summary>
    /// Список типов объектов из состава ГТП, по которым не создаются
    /// объекты "клоны"
    /// </summary>
    public static IReadOnlyList<int> TechCompositionGtpNonCloneTypes { get; private set; }

    /// <summary>
    /// Перечень технологических пользовательских типов объектов
    /// </summary>
    public static IEnumerable<int> TechBaseUserObjectIds
    {
      get => (IEnumerable<int>) TechCardConsts.ObjectTypes._techBaseUserObjectIds;
    }

    /// <summary>
    /// Перечень "специальных" технологических пользовательских типов объектов
    /// </summary>
    public static IEnumerable<int> TechSpecialObjectIds
    {
      get => (IEnumerable<int>) TechCardConsts.ObjectTypes._techSpecialObjectIds;
    }

    /// <summary>
    /// Список типов объектов из состава ГТП, по которым не создаются
    /// объекты "клоны"
    /// </summary>
    [Obsolete("Use TechCompositionGtpNonCloneTypes instead", false)]
    public static int[] TechCompGtpNonClone
    {
      get => TechCardConsts.ObjectTypes.TechCompositionGtpNonCloneTypes.ToArray<int>();
    }

    /// <summary>
    /// Список типов объектов, для которых не создаются копии объектов при копировании составов
    /// </summary>
    /// <remarks>Используется в частности при создании объектов по прототипу</remarks>
    public static IReadOnlyList<int> TechCompositionNonCloneTypes { get; private set; }

    /// <summary>
    /// Список типов объектов, для которых не создаются копии объектов при копировании составов
    /// </summary>
    /// <remarks>Используется в частности при создании объектов по прототипу</remarks>
    [Obsolete("Use TechCompositionNonCloneTypes instead", false)]
    public static int[] TechCompNotCopy
    {
      get => TechCardConsts.ObjectTypes.TechCompositionNonCloneTypes.ToArray<int>();
    }

    /// <summary>
    /// Список типов объектов, для которых возможно создание комплектующих / сборочных технологических единиц
    /// </summary>
    public static IReadOnlyList<int> TechArtCompositionTypes { get; private set; }

    /// <summary>
    /// Список типов объектов, для которых возможно создание комплектующих / сборочных технологических единиц
    /// </summary>
    [Obsolete("Use TechArtCompositionTypes instead", false)]
    public static int[] TechArtCompTypes
    {
      get => TechCardConsts.ObjectTypes.TechArtCompositionTypes.ToArray<int>();
    }
  }

  /// <summary>TechCard attribute types</summary>
  public static class AttributeTypes
  {
    /// <summary>Тип атрибута "Вид производства"</summary>
    [IsAttributeType(true)]
    public static readonly Guid ProductionAttrGUID = new Guid("cad0019c-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Список форм редактирования"</summary>
    [IsAttributeType(true)]
    public static readonly Guid FormListAttrGUID = new Guid("cad0019d-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Предыдущий уровень ЖЦ"</summary>
    [IsAttributeType(true)]
    public static readonly Guid LifeCycleStepPrevGUID = new Guid("cad001bb-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Ссылка на связь группового/типового ТП"</summary>
    [IsAttributeType(true)]
    public static readonly Guid TechProcGroupRelAttrGUID = new Guid("cad009ee-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Входимость - Версия сборки"</summary>
    [IsAttributeType(true)]
    public static readonly Guid MemberOfAssemblyVersionAttrGUID = new Guid("cad001d5-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Входимость - Сборка" (не версия)</summary>
    [IsAttributeType(true)]
    public static readonly Guid MemberOfAssemblyObjectAttrGUID = new Guid("cadd9c2a-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Входимость - Сборка"</summary>
    [IsAttributeType(true)]
    [Obsolete("Use MemberOfAssemblyVersionAttrGUID instead")]
    public static readonly Guid MemberOfSborkaObjectAttrGUID = TechCardConsts.AttributeTypes.MemberOfAssemblyVersionAttrGUID;
    /// <summary>Тип атрибута "Входимость - Версия заказа"</summary>
    [IsAttributeType(true)]
    public static readonly Guid MemberOfOrderVersionAttrGUID = new Guid("cad001d6-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Входимость - Заказ" (не версия)</summary>
    [IsAttributeType(true)]
    public static readonly Guid MemberOfOrderObjectAttrGUID = new Guid("cadd9c2b-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Входимость - Заказ"</summary>
    [IsAttributeType(true)]
    [Obsolete("Use MemberOfOrderVersionAttrGUID instead")]
    public static readonly Guid MemberOfZakazObjectAttrGUID = TechCardConsts.AttributeTypes.MemberOfOrderVersionAttrGUID;
    /// <summary>Тип атрибута "Входимость - Головное изделие"</summary>
    [IsAttributeType(true)]
    public static readonly Guid MemberOfMainObjectAttrGUID = new Guid("cad009f0-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Входимость - ТП"</summary>
    [IsAttributeType(true)]
    public static Guid MemberOfTechProcObjAttrGuid = new Guid("cadd9a3a-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Входимость - ПК Сборки"</summary>
    [IsAttributeType(true)]
    public static readonly Guid MemberOfAssemblyCopyAttrGUID = new Guid("cadd9bc9-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Входимость - Выходная Сборка"</summary>
    [IsAttributeType(true)]
    public static readonly Guid MemberOfExitAssemblyAttrGUID = new Guid("cadd9bca-306c-11d8-b4e9-00304f19f545");
    /// <summary>
    /// Тип атрибута "Входимость - Объект производственной ведомости" (не версия)
    /// </summary>
    [IsAttributeType(true)]
    public static readonly Guid MemberOfProductionReportObjectAttrGUID = new Guid("cadd9bda-306c-11d8-b4e9-00304f19f545");
    /// <summary>
    /// Тип атрибута "Входимость - Версия производственной ведомости"
    /// </summary>
    [IsAttributeType(true)]
    public static readonly Guid MemberOfProductionReportVersionAttrGUID = new Guid("cadd9bcb-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Идентификатор ПК ДСЕ"</summary>
    [IsAttributeType(true)]
    public static readonly Guid ProductionObjectUIDAttrGuid = new Guid("cadd9bcc-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Дата ввода"</summary>
    [IsAttributeType(true)]
    public static readonly Guid DateStartAttrGUID = new Guid("cad001e9-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Дата аннулирования"</summary>
    [IsAttributeType(true)]
    public static readonly Guid DateFinishAttrGUID = new Guid("cad001ea-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Тип расцеховочного маршрута"</summary>
    [IsAttributeType(true)]
    public static readonly Guid RouteTypeAttrGUID = new Guid("cad001eb-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Назначение расцеховочного маршрута"</summary>
    [IsAttributeType(true)]
    public static readonly Guid RoutePurposeAttrGUID = new Guid("cad001ec-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Вид расцеховочного маршрута"</summary>
    [IsAttributeType(true)]
    public static readonly Guid RouteKindAttrGUID = new Guid("cad001ed-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Строка маршрута"</summary>
    [IsAttributeType(true)]
    public static readonly Guid RouteStringAttrGUID = new Guid("cad001ef-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Цех"</summary>
    [IsAttributeType(true)]
    public static readonly Guid CehRouteAttrGUID = new Guid("cad001fb-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Участок"</summary>
    [IsAttributeType(true)]
    public static readonly Guid AreaRouteAttrGUID = new Guid("cad001fc-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Вид работ"</summary>
    [IsAttributeType(true)]
    public static readonly Guid WorkTypeAttrGuid = new Guid("cad005b2-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Признак основного ТП к расцеховке"</summary>
    [IsAttributeType(true)]
    public static readonly Guid TP2RouteTypeAttrGuid = new Guid("cad005b4-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Вид изделия"</summary>
    [IsAttributeType(true)]
    public static readonly Guid ArtTypeAttrGuid = new Guid("cad005d1-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Вид заготовки"</summary>
    [IsAttributeType(true)]
    public static readonly Guid ZagTypeAttrGuid = new Guid("cad005d0-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Ссылка на идентификатор объекта"</summary>
    [IsAttributeType(true)]
    public static readonly Guid ObjectGUIDAttrGuid = new Guid("cad005ba-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Список эскизов"</summary>
    [IsAttributeType(true)]
    public static readonly Guid SketchListGUIDAttrGuid = new Guid("cad009e8-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Имя эскиза"</summary>
    [IsAttributeType(true)]
    public static readonly Guid SketchNameGuid = new Guid("cad009e9-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Количество по входимости"</summary>
    [IsAttributeType(true)]
    public static readonly Guid Count4CompositionAttrGuid = new Guid("cad009f2-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Количество по ТП"</summary>
    [IsAttributeType(true)]
    public static readonly Guid Count4TechProcAttrGuid = new Guid("cad009f3-306c-11d8-b4e9-00304f19f545");
    /// <summary>
    /// тип атрибута ""Количество по конструкторскому составу"
    /// </summary>
    [IsAttributeType(true)]
    public static readonly Guid Count4ArticleAttrGuid = new Guid("cad009f4-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Оставшееся количество"</summary>
    [IsAttributeType(true)]
    public static readonly Guid CountRemainAttrGuid = new Guid("cad009f5-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Признак объекта в контексте ГТП/ТТП"</summary>
    [IsAttributeType(true)]
    public static readonly Guid GtpContextAttrGuid = new Guid("cadd93fb-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Код атрибута поверхности Cadmech"</summary>
    [IsAttributeType(true)]
    public static readonly Guid CadmechAttrTypeAttrGuid = new Guid("cadd9619-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута Текст перехода"</summary>
    [IsAttributeType(true)]
    public static readonly Guid PerehTextAttrGuid = new Guid("cad005ce-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута Текст перехода дополнительный"</summary>
    [IsAttributeType(true)]
    public static readonly Guid PerehTextExtraAttrGuid = new Guid("cad005cf-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Признак не нумеруемого объекта"</summary>
    [IsAttributeType(true)]
    public static readonly Guid NonNumerationFlagAttrGuid = new Guid("cadd9710-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Заголовок РЭ"</summary>
    public static readonly Guid ElemRouteCaptionAttrGuid = new Guid("cad005e9-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Ссылка на шаблон элемента (расцеховки)"</summary>
    [IsAttributeType(true)]
    public static readonly Guid ElemRouteTemplateReferenceAttrGuid = new Guid("cadd9c24-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Ключ IMBASE"</summary>
    /// <remarks>Не путать с атрибутом "Код ImBase"</remarks>
    public static readonly Guid ImbaseKeyAttrGuid = new Guid("cad00162-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Код ImBase"</summary>
    /// <remarks>Не путать с атрибутом "Ключ IMBASE"</remarks>
    public static readonly Guid ImbaseCodeAttrGuid = new Guid("cad0020f-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Ссылка на объект IMBASE"</summary>
    public static readonly Guid ImbaseObjectAttrGuid = new Guid("cad00209-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Ссылка на объект"</summary>
    [IsAttributeType(false)]
    public static readonly Guid ObjectRefAttrGuid = new Guid("cad001be-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Ссылка на изделие"</summary>
    [IsAttributeType(false)]
    public static readonly Guid ArticleAttrGuid = new Guid("cad001ee-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Тип нумерации"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationTypeAttrGuid = new Guid("cad001c7-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Количество знаков в номере"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationNumberLengthAttrGuid = new Guid("cad001c8-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Список букв"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationCharListAttrGuid = new Guid("cad001c9-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Первоначальный номер"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationFirtNumberAttrGuid = new Guid("cad001ca-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Шаг номеров"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationStepAttrGuid = new Guid("cad001cb-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Область нумерации"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationAreaAttrGuid = new Guid("cad001cc-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Разделитель в номере"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationSeparatorAttrGuid = new Guid("cad001cd-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Тип нумерации заменителя"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationTypeVariantAttrGuid = new Guid("cad001ce-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Номер основного объекта"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationUseBaseNumberAttrGuid = new Guid("cad001cf-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Способ нумерации"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationMethodAttrGuid = new Guid("cad001d1-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Режим нумерации"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationModeAttrGuid = new Guid("cadd9669-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Вызов нумерации при удалении"</summary>
    [IsAttributeType(false)]
    public static readonly Guid NumerationOnDeleteAttrGuid = new Guid("cadd99b4-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Изображение"</summary>
    [IsAttributeType(false)]
    public static readonly Guid ImageAttrGuid = new Guid("cad0013e-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Library_Image"</summary>
    [IsAttributeType(false)]
    public static readonly Guid libImageAttrGuid = new Guid("cad0013d-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Контекст изменения"</summary>
    [IsAttributeType(false)]
    public static readonly Guid ContextAttrGuid = new Guid("cad001fe-306c-11d8-b4e9-00304f19f545");
    /// <summary>
    /// тип атрибута "Идентификатор связи расцеховочного элемента"
    /// </summary>
    [IsAttributeType(false)]
    public static readonly Guid ElemRouteLinkAttrGuid = new Guid("cad005b1-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Маршрут обработки по умолчанию"</summary>
    [IsAttributeType(false)]
    public static readonly Guid ProcRouteDefaultAttrGuid = new Guid("cad005b9-306c-11d8-b4e9-00304f19f545");
    /// <summary>Группа атрибутов "Технологические атрибуты"</summary>
    [IsAttributeType(false)]
    public static readonly Guid TechcardAttrGroupGuid = new Guid("cad005b3-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Ссылка на условие"</summary>
    [IsAttributeType(false)]
    public static readonly Guid LinkCondAttrGuid = new Guid("cad005bb-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "OLE объект"</summary>
    [IsAttributeType(false)]
    public static readonly Guid OLEObjectAttrGuid = new Guid("cad005be-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Номер объекта"</summary>
    [IsAttributeType(false)]
    public static readonly Guid ObjectNumAttrGuid = new Guid("cad009e6-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Сортировка"</summary>
    [IsAttributeType(false)]
    public static readonly Guid SortAttrTypeGuid = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип атрибута "Количество"</summary>
    [IsAttributeType(false)]
    public static readonly Guid CountAttrTypeGuid = new Guid("cad00267-306c-11d8-b4e9-00304f19f545");
    /// <summary>Атрибут "дополнительные объекты" для извещения</summary>
    [IsAttributeType(false)]
    public static readonly Guid EcoAuxObjAttrGuid = new Guid("cadd93b7-306c-11d8-b4e9-00304f19f545");
    /// <summary>Атрибут "Обозначение ЕТП для ГТП"</summary>
    [IsAttributeType(false)]
    public static readonly Guid DesignationEtpObj4Gtp = new Guid("cadd9736-306c-11d8-b4e9-00304f19f545");
    /// <summary>Атрибут "Производственные ведомости - аналоги"</summary>
    public static readonly Guid ProductionReportAnalogGuid = new Guid("cadd9a7f-306c-11d8-b4e9-00304f19f545");

    /// <summary>
    /// 
    /// </summary>
    static AttributeTypes()
    {
      TechCardConsts.AttributeTypes.InitData(TechCardConsts.MetaDataHelper);
    }

    /// <summary>Constructor</summary>
    internal static void InitData(IMetaDataHelper metaDataHelper)
    {
      TechCardConsts.AttributeTypes.ProductionAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ProductionAttrGUID);
      TechCardConsts.AttributeTypes.TechProcGroupRelAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.TechProcGroupRelAttrGUID);
      TechCardConsts.AttributeTypes.FormListAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.FormListAttrGUID);
      TechCardConsts.AttributeTypes.LifeCycleStepPrevID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.LifeCycleStepPrevGUID);
      TechCardConsts.AttributeTypes.DesignationAttrTypeID = metaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545");
      TechCardConsts.AttributeTypes.NameAttrTypeID = metaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545");
      TechCardConsts.AttributeTypes.ImbaseObjectAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ImbaseObjectAttrGuid);
      TechCardConsts.AttributeTypes.ObjectRefAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ObjectRefAttrGuid);
      TechCardConsts.AttributeTypes.ContextAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ContextAttrGuid);
      TechCardConsts.AttributeTypes.ElemRouteLinkAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ElemRouteLinkAttrGuid);
      TechCardConsts.AttributeTypes.ProcRouteDefaultAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ProcRouteDefaultAttrGuid);
      TechCardConsts.AttributeTypes.MemberOfTechProcObjAttrId = TechCardConsts.MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.MemberOfTechProcObjAttrGuid);
      TechCardConsts.AttributeTypes.CehRouteAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.CehRouteAttrGUID);
      TechCardConsts.AttributeTypes.AreaRouteAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.AreaRouteAttrGUID);
      TechCardConsts.AttributeTypes.SortAttrTypeID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.SortAttrTypeGuid);
      TechCardConsts.AttributeTypes.CountAttrTypeID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.CountAttrTypeGuid);
      TechCardConsts.AttributeTypes.MemberOfAssemblyVersionAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.MemberOfAssemblyVersionAttrGUID);
      TechCardConsts.AttributeTypes.MemberOfAssemblyObjectAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.MemberOfAssemblyObjectAttrGUID);
      TechCardConsts.AttributeTypes.MemberOfOrderVersionAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.MemberOfOrderVersionAttrGUID);
      TechCardConsts.AttributeTypes.MemberOfOrderObjectAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.MemberOfOrderObjectAttrGUID);
      TechCardConsts.AttributeTypes.MemberOfMainObjectAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.MemberOfMainObjectAttrGUID);
      TechCardConsts.AttributeTypes.MemberOfAssemblyCopyAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.MemberOfAssemblyCopyAttrGUID);
      TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrGUID);
      TechCardConsts.AttributeTypes.MemberOfProductionReportObjectAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.MemberOfProductionReportObjectAttrGUID);
      TechCardConsts.AttributeTypes.MemberOfProductionReportVersionAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.MemberOfProductionReportVersionAttrGUID);
      TechCardConsts.AttributeTypes.ProductionObjectUIDAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ProductionObjectUIDAttrGuid);
      TechCardConsts.AttributeTypes.ProductionReportAnalogID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ProductionReportAnalogGuid);
      TechCardConsts.AttributeTypes.Count4CompositionAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.Count4CompositionAttrGuid);
      TechCardConsts.AttributeTypes.Count4TechProcAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.Count4TechProcAttrGuid);
      TechCardConsts.AttributeTypes.Count4ArticleAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.Count4ArticleAttrGuid);
      TechCardConsts.AttributeTypes.CountRemainAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.CountRemainAttrGuid);
      TechCardConsts.AttributeTypes.GtpContextAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.GtpContextAttrGuid);
      TechCardConsts.AttributeTypes.RouteStringAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.RouteStringAttrGUID);
      TechCardConsts.AttributeTypes.ContextVersionID = metaDataHelper.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545");
      TechCardConsts.AttributeTypes.FileAttrTypeID = metaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545");
      TechCardConsts.AttributeTypes.ArchiveAttrID = metaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeArchive);
      TechCardConsts.AttributeTypes.NameAttrTypeID = metaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545");
      TechCardConsts.AttributeTypes.DesignationAttrTypeID = metaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545");
      TechCardConsts.AttributeTypes.NonNumerationFlagAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.NonNumerationFlagAttrGuid);
      TechCardConsts.AttributeTypes.ElemRouteTemplateReferenceAttrID = metaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ElemRouteTemplateReferenceAttrGuid);
    }

    /// <summary>Тип атрибута "Вид производства"</summary>
    public static int ProductionAttrID { get; private set; }

    /// <summary>Тип атрибута "Ссылка на связь группового/типового ТП"</summary>
    public static int TechProcGroupRelAttrID { get; private set; }

    /// <summary>Тип атрибута "Список форм редактирования"</summary>
    public static int FormListAttrID { get; private set; }

    /// <summary>Тип атрибута "Предыдущий уровень ЖЦ"</summary>
    public static int LifeCycleStepPrevID { get; private set; }

    /// <summary>Тип атрибута "Цех"</summary>
    public static int CehRouteAttrID { get; private set; }

    /// <summary>Тип атрибута "Участок"</summary>
    public static int AreaRouteAttrID { get; private set; }

    /// <summary>Тип атрибута "Входимость - Версия сборки"</summary>
    public static int MemberOfAssemblyVersionAttrID { get; private set; }

    /// <summary>Тип атрибута "Входимость - Сборка" (не версия)</summary>
    public static int MemberOfAssemblyObjectAttrID { get; private set; }

    /// <summary>Тип атрибута "Входимость - Сборка"</summary>
    [Obsolete("Use MemberOfAssemblyVersionAttrID instead")]
    public static int MemberOfSborkaObjectAttrID
    {
      get => TechCardConsts.AttributeTypes.MemberOfAssemblyVersionAttrID;
    }

    /// <summary>Тип атрибута "Входимость - ПК Сборки"</summary>
    public static int MemberOfAssemblyCopyAttrID { get; private set; }

    /// <summary>Тип атрибута "Входимость - Выходная сборка"</summary>
    public static int MemberOfExitAssemblyAttrID { get; private set; }

    /// <summary>
    /// Тип атрибута "Входимость - Объект производственной ведомости" (не версия)
    /// </summary>
    public static int MemberOfProductionReportObjectAttrID { get; private set; }

    /// <summary>
    /// Тип атрибута "Входимость - Версия производственной ведомости"
    /// </summary>
    public static int MemberOfProductionReportVersionAttrID { get; private set; }

    /// <summary>Тип атрибута "Идентификатор ПК ДСЕ"</summary>
    public static int ProductionObjectUIDAttrID { get; private set; }

    /// <summary>Тип атрибута "Входимость - Версия заказа"</summary>
    public static int MemberOfOrderVersionAttrID { get; private set; }

    /// <summary>Тип атрибута "Входимость - Заказ" (не версия)</summary>
    public static int MemberOfOrderObjectAttrID { get; private set; }

    /// <summary>Тип атрибута "Входимость - Заказ"</summary>
    [Obsolete("Use MemberOfOrderVersionAttrID instead")]
    public static int MemberOfZakazObjectAttrID
    {
      get => TechCardConsts.AttributeTypes.MemberOfOrderVersionAttrID;
    }

    /// <summary>Тип атрибута "Входимость - Головное изделие"</summary>
    public static int MemberOfMainObjectAttrID { get; private set; }

    /// <summary>Тип атрибута "Входимость - ТП"</summary>
    public static int MemberOfTechProcObjAttrId { get; private set; }

    /// <summary>тип атрибута "Количество по входимости"</summary>
    public static int Count4CompositionAttrID { get; private set; }

    /// <summary>тип атрибута "Количество по ТП"</summary>
    public static int Count4TechProcAttrID { get; private set; }

    /// <summary>
    /// тип атрибута ""Количество по конструкторскому составу"
    /// </summary>
    public static int Count4ArticleAttrID { get; private set; }

    /// <summary>тип атрибута "Оставшееся количество"</summary>
    public static int CountRemainAttrID { get; private set; }

    /// <summary>тип атрибута "Признак объекта в контексте ГТП/ТТП"</summary>
    public static int GtpContextAttrID { get; set; }

    /// <summary>Тип атрибута "Строка маршрута"</summary>
    public static int RouteStringAttrID { get; private set; }

    /// <summary>Тип атрибута "Признак не нумеруемого объекта"</summary>
    public static int NonNumerationFlagAttrID { get; private set; }

    /// <summary>Тип атрибута "Ссылка на шаблон элемента (расцеховки)"</summary>
    public static int ElemRouteTemplateReferenceAttrID { get; private set; }

    /// <summary>тип атрибута "Обозначение"</summary>
    public static int DesignationAttrTypeID { get; private set; }

    /// <summary>тип атрибута "Наименование"</summary>
    public static int NameAttrTypeID { get; private set; }

    /// <summary>тип атрибута "Ссылка на объект Imbase"</summary>
    public static int ImbaseObjectAttrID { get; private set; }

    /// <summary>тип атрибута "Ссылка на объект"</summary>
    public static int ObjectRefAttrID { get; private set; }

    /// <summary>тип атрибута "Контекст изменения"</summary>
    public static int ContextAttrID { get; private set; }

    /// <summary>
    /// тип атрибута "Идентификатор связи расцеховочного элемента"
    /// </summary>
    public static int ElemRouteLinkAttrID { get; private set; }

    /// <summary>тип атрибута "Маршрут обработки по умолчанию"</summary>
    public static int ProcRouteDefaultAttrID { get; private set; }

    /// <summary>тип атрибута "Сортировка"</summary>
    public static int SortAttrTypeID { get; private set; }

    /// <summary>тип атрибута "Количество"</summary>
    public static int CountAttrTypeID { get; private set; }

    /// <summary>атрибут "Идентификатор версии в составе"</summary>
    public static int ContextVersionID { get; private set; }

    /// <summary>Атрибут "Файл"</summary>
    public static int FileAttrTypeID { get; private set; }

    /// <summary>Атрибут "Архив"</summary>
    public static int ArchiveAttrID { get; private set; }

    /// <summary>Атрибут "Производственные ведомости - аналоги"</summary>
    public static int ProductionReportAnalogID { get; private set; }
  }

  /// <summary>
  /// 
  /// </summary>
  public static class LcLevel
  {
    /// <summary>Уровень продвижения "Типовой элемент техпроцесса"</summary>
    public static readonly Guid ListCycleLevelProcElemBase = new Guid("cad001ba-306c-11d8-b4e9-00304f19f545");
    /// <summary>Уровень продвижения "Хранение"</summary>
    public static readonly Guid LifeCycleLevelStoring = new Guid("cad009de-306c-11d8-b4e9-00304f19f545");
    /// <summary>Уровень продвижения "Аннулировано"</summary>
    public static readonly Guid LifeCycleLevelAnnulled = new Guid("cad00012-306c-11d8-b4e9-00304f19f545");
  }

  /// <summary>
  /// 
  /// </summary>
  public static class RelTypes
  {
    /// <summary>Список типов связей TechCard</summary>
    private static IReadOnlyList<int> _techAllRelationTypes;
    /// <summary>
    /// 
    /// </summary>
    private static IReadOnlyList<int> _techCompositionGtpRelations;
    /// <summary>
    /// Типы связей, по которым ищем конструкторский состав для создания комплектующих единиц
    /// </summary>
    private static IReadOnlyList<int> _artsCompositionRelations;
    /// <summary>тип связи "Технологический состав"</summary>
    [IsRelationType(true)]
    public static readonly Guid TechRelationGuid = new Guid("cad0019f-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Собираемые технологические данные"</summary>
    [IsRelationType(true)]
    public static readonly Guid TechCombinedRelationGuid = new Guid("cad0019e-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Собираемые технологические данные"</summary>
    [Obsolete("Use TechCombinedRelationGuid instead")]
    public static readonly Guid TechSobirRelationGuid = TechCardConsts.RelTypes.TechCombinedRelationGuid;
    /// <summary>тип связи "Сквозной маршрут обработки"</summary>
    [IsRelationType(true)]
    public static readonly Guid TechThroughMORelationGuid = new Guid("cad001e1-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Технологический сквозной ТП"</summary>
    [IsRelationType(true)]
    public static readonly Guid TechThroughtTPRelationGuid = new Guid("cadd9403-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Технологическая связь с расцеховкой"</summary>
    [IsRelationType(true)]
    public static readonly Guid TechRouteRelationGuid = new Guid("cad005b0-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Технологическая связь с элементом ГТП"</summary>
    [IsRelationType(true)]
    public static readonly Guid TechLinkGTPObjRelationGuid = new Guid("cad005b8-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Технологическая связь с эскизом"</summary>
    [IsRelationType(true)]
    public static readonly Guid TechDraftRelationGuid = new Guid("cad009e7-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Пользовательский технологический состав"</summary>
    /// <remarks>Связь может отсутствовать в базе</remarks>
    [IsRelationType(false)]
    public static readonly Guid CustomTechRelationGuid = new Guid("cadd9ac5-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Технологическое представление"</summary>
    [IsRelationType(false)]
    public static readonly Guid NotionRelationGuid = new Guid("cad001bd-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Проектная связь" (состав изделий)</summary>
    [IsRelationType(false)]
    public static readonly Guid ProektRelationGuid = new Guid("cad00023-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Состав ЭС ПВ" (производственная ведомость)</summary>
    [IsRelationType(false)]
    public static readonly Guid ProductReportRelationGuid = new Guid("cadd9a57-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Связь с технологической ЭСИ"</summary>
    [IsRelationType(false)]
    public static readonly Guid MbomBindingRelationTypeGuid = new Guid("cadd98ab-306c-11d8-b4e9-00304f19f545");
    /// <summary>тип связи "Состав технологической ЭСИ"</summary>
    [IsRelationType(false)]
    public static readonly Guid MbomCompositionRelationTypeGuid = new Guid("cadd98ac-306c-11d8-b4e9-00304f19f545");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="metaDataHelper"></param>
    private static void InitLists(IMetaDataHelper metaDataHelper)
    {
      TechCardConsts.RelTypes._techCompositionGtpRelations = (IReadOnlyList<int>) new int[2]
      {
        TechCardConsts.RelTypes.TechRelationID,
        TechCardConsts.RelTypes.TechDraftRelationID
      };
      TechCardConsts.RelTypes._artsCompositionRelations = (IReadOnlyList<int>) new int[4]
      {
        TechCardConsts.RelTypes.ProektRelationID,
        TechCardConsts.RelTypes.ProductReportRelationID,
        TechCardConsts.RelTypes.MbomBindingRelationTypeId,
        TechCardConsts.RelTypes.MbomCompositionRelationTypeId
      };
      List<int> intList = new List<int>();
      intList.Add(TechCardConsts.RelTypes.TechRelationID);
      if (TechCardConsts.RelTypes.CustomTechRelationID != -1)
        intList.Add(TechCardConsts.RelTypes.CustomTechRelationID);
      TechCardConsts.RelTypes._techAllRelationTypes = (IReadOnlyList<int>) intList.ToArray();
    }

    /// <summary>
    /// 
    /// </summary>
    static RelTypes() => TechCardConsts.RelTypes.InitData(TechCardConsts.MetaDataHelper);

    /// <summary>
    /// 
    /// </summary>
    internal static void InitData(IMetaDataHelper metaDataHelper)
    {
      TechCardConsts.RelTypes.TechThroughMORelationID = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechThroughMORelationGuid);
      TechCardConsts.RelTypes.TechRelationID = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechRelationGuid);
      TechCardConsts.RelTypes.TechSobirRelationID = TechCardConsts.RelTypes.TechCombinedRelationID = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechCombinedRelationGuid);
      TechCardConsts.RelTypes.TechRouteRelationID = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechRouteRelationGuid);
      TechCardConsts.RelTypes.TechLinkGTPObjRelationID = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechLinkGTPObjRelationGuid);
      TechCardConsts.RelTypes.TechDraftRelationID = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechDraftRelationGuid);
      TechCardConsts.RelTypes.TechThroughtTPRelationID = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechThroughtTPRelationGuid);
      TechCardConsts.RelTypes.CustomTechRelationID = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.CustomTechRelationGuid);
      TechCardConsts.RelTypes.SimpleRelationID = metaDataHelper.GetRelationTypeID(new Guid("cad00022-306c-11d8-b4e9-00304f19f545"));
      TechCardConsts.RelTypes.ProektRelationID = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.ProektRelationGuid);
      TechCardConsts.RelTypes.SortedRelationID = metaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545"));
      TechCardConsts.RelTypes.ProductReportRelationID = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.ProductReportRelationGuid);
      TechCardConsts.RelTypes.MbomBindingRelationTypeId = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.MbomBindingRelationTypeGuid);
      TechCardConsts.RelTypes.MbomCompositionRelationTypeId = metaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.MbomCompositionRelationTypeGuid);
      TechCardConsts.RelTypes.InitLists(metaDataHelper);
    }

    /// <summary>тип связи "Технологический состав"</summary>
    public static int TechRelationID { get; private set; }

    /// <summary>тип связи "Собираемые технологические данные"</summary>
    public static int TechCombinedRelationID { get; private set; }

    /// <summary>тип связи "Собираемые технологические данные"</summary>
    [Obsolete("Use TechCombinedRelationID instead")]
    public static int TechSobirRelationID { get; private set; }

    /// <summary>тип связи "Сквозной маршрут обработки"</summary>
    public static int TechThroughMORelationID { get; private set; }

    /// <summary>тип связи "Технологическая связь с расцеховкой"</summary>
    public static int TechRouteRelationID { get; private set; }

    /// <summary>тип связи "Технологическая связь с элементом ГТП"</summary>
    public static int TechLinkGTPObjRelationID { get; private set; }

    /// <summary>тип связи "Технологическая связь с эскизом"</summary>
    public static int TechDraftRelationID { get; private set; }

    /// <summary>Тип связи "Технологический сквозной ТП"</summary>
    public static int TechThroughtTPRelationID { get; set; }

    /// <summary>Пользовательская технологическая связь</summary>
    public static int CustomTechRelationID { get; private set; }

    /// <summary>Тип связи "Простая связь"</summary>
    public static int SimpleRelationID { get; private set; }

    /// <summary>тип связи "Проектная связь" (состав изделий)</summary>
    public static int ProektRelationID { get; private set; }

    /// <summary>тип связи "Состав ЭС ПВ" (производственная ведомость)</summary>
    public static int ProductReportRelationID { get; private set; }

    /// <summary>Тип связи "Простая связь с сортировкой"</summary>
    public static int SortedRelationID { get; private set; }

    /// <summary>Тип связи "Связь с технологической ЭСИ"</summary>
    public static int MbomBindingRelationTypeId { get; private set; }

    /// <summary>Тип связи "Состав технологической ЭСИ"</summary>
    public static int MbomCompositionRelationTypeId { get; private set; }

    /// <summary>Список всех технологических связей</summary>
    public static IReadOnlyList<int> TechAllRelationTypes
    {
      get => TechCardConsts.RelTypes._techAllRelationTypes;
    }

    /// <summary>
    /// Типы связей для получения состава ГТП, по которому могут быть созданы
    /// объекты ЕТП (привязка объектов ГТП к соотв. объектам ЕТП)
    /// </summary>
    [Obsolete("TechCompositionGtpRelations", false)]
    public static int[] TechCompGtpRelations
    {
      get => TechCardConsts.RelTypes._techCompositionGtpRelations.ToArray<int>();
    }

    /// <summary>
    /// Типы связей для получения состава ГТП, по которому могут быть созданы
    /// объекты ЕТП (привязка объектов ГТП к соотв. объектам ЕТП)
    /// </summary>
    public static IReadOnlyList<int> TechCompositionGtpRelations
    {
      get => TechCardConsts.RelTypes._techCompositionGtpRelations;
    }

    /// <summary>
    /// Типы связей, по которым ищем конструкторский состав для создания комплектующих единиц
    /// </summary>
    public static IReadOnlyList<int> ArtsCompositionRelations
    {
      get => TechCardConsts.RelTypes._artsCompositionRelations;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public static class Utils
  {
    /// <summary>
    /// Кеш для проверки на принадлежность к технологическим типам
    /// </summary>
    private static readonly IDictionary<int, bool> ObjTypeCache = (IDictionary<int, bool>) new ConcurrentDictionary<int, bool>();
    /// <summary>
    /// Кеш для проверки на принадлежность к технологическим типам, не унаследованных от базового техн. типа
    /// </summary>
    private static readonly IDictionary<int, bool> TechNotInheritObjTypeCache = (IDictionary<int, bool>) new ConcurrentDictionary<int, bool>();

    /// <summary>
    /// 
    /// </summary>
    private static void InitCaches()
    {
      if (TechCardConsts.ObjectTypes.TechNotInheritedBaseObjTypes == null)
        return;
      foreach (int inheritedBaseObjType in (IEnumerable<int>) TechCardConsts.ObjectTypes.TechNotInheritedBaseObjTypes)
        TechCardConsts.Utils.TechNotInheritObjTypeCache[inheritedBaseObjType] = true;
    }

    /// <summary>Конструктор</summary>
    static Utils() => TechCardConsts.Utils.InitCaches();

    /// <summary>Получение общих атрибутов для типов объектов</summary>
    /// <param name="sourceTypeId">Ид. типа - источника</param>
    /// <param name="destinationTypeId">Ид. типа - результата</param>
    /// <param name="checkAnyAttrMode">Проверять режим любого атрибута</param>
    /// <param name="attrIds">Общий список атрибутов</param>
    /// <returns></returns>
    public static bool GetCommonObjTypeAttrs(
      int sourceTypeId,
      int destinationTypeId,
      bool checkAnyAttrMode,
      out List<int> attrIds)
    {
      attrIds = new List<int>();
      if (sourceTypeId == -1 || destinationTypeId == -1)
        return false;
      IMSObjectType objectType = TechCardConsts.MetaDataHelper.GetObjectType(destinationTypeId);
      if (objectType == null)
        return false;
      List<IMSAttribute4ObjectType> attribute4ObjectTypeList = TechCardConsts.MetaDataHelper.GetAttribute4ObjectTypeList(sourceTypeId);
      if (attribute4ObjectTypeList == null)
        return false;
      foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList)
      {
        if (attribute4ObjectType != null)
        {
          int attributeId = attribute4ObjectType.AttributeID;
          if ((objectType.AnyAttributes & checkAnyAttrMode ? 1 : (TechCardConsts.MetaDataHelper.GetAttribute4ObjectType(destinationTypeId, attributeId) != null ? 1 : 0)) != 0)
            attrIds.Add(attributeId);
        }
      }
      return true;
    }

    /// <summary>Получение общих атрибутов для типов связей / объектов</summary>
    /// <param name="sourceTypeId">Ид. типа - источника</param>
    /// <param name="destinationTypeId">Ид. типа - результата</param>
    /// <param name="checkAnyAttrMode">Проверять режим любого атрибута</param>
    /// <param name="attrIds">Общий список атрибутов</param>
    /// <returns></returns>
    public static bool GetCommonRelTypeAttrs(
      int sourceTypeId,
      int destinationTypeId,
      bool checkAnyAttrMode,
      out List<int> attrIds)
    {
      attrIds = new List<int>();
      if (sourceTypeId == -1 || destinationTypeId == -1)
        return false;
      IMSRelationType relationType = TechCardConsts.MetaDataHelper.GetRelationType(destinationTypeId);
      if (relationType == null)
        return false;
      List<IMSAttribute4RelationType> relationTypeList = TechCardConsts.MetaDataHelper.GetAttribute4RelationTypeList(sourceTypeId);
      if (relationTypeList == null)
        return false;
      foreach (IMSAttribute4RelationType attribute4RelationType in relationTypeList)
      {
        if (attribute4RelationType != null)
        {
          int attributeId = attribute4RelationType.AttributeID;
          if ((relationType.AnyAttributes & checkAnyAttrMode ? 1 : (TechCardConsts.MetaDataHelper.GetAttribute4RelationType(destinationTypeId, attributeId) != null ? 1 : 0)) != 0)
            attrIds.Add(attributeId);
        }
      }
      return true;
    }

    /// <summary>Возвращает идентификатор типа объекта по его Guid</summary>
    /// <param name="objTypeGuid">Guid типа объекта</param>
    /// <param name="userSession">Сессия подключения к базе</param>
    /// <returns>Идентификатор типа объекта, либо -1 если ничего не найдено</returns>
    public static int ObjectTypeByGuid(Guid objTypeGuid, IUserSession userSession)
    {
      return TechCardConsts.MetaDataHelper.GetObjectTypeID(objTypeGuid);
    }

    /// <summary>Возвращает идентификатор типа атрибута по его Guid</summary>
    /// <param name="attrTypeGuid">Guid типа атрибута</param>
    /// <param name="userSession">Сессия подключения к базе</param>
    /// <returns>Идентификатор типа атрибута, либо -1 если ничего не найдено</returns>
    public static int AttributeTypeByGuid(Guid attrTypeGuid, IUserSession userSession)
    {
      return TechCardConsts.MetaDataHelper.GetAttributeTypeID(attrTypeGuid);
    }

    /// <summary>Возвращает идентификатор типа связи по его Guid</summary>
    /// <param name="relationTypeGuid">Guid типа связи</param>
    /// <param name="userSession">Сессия подключения к базе</param>
    /// <returns>Идентификатор типа связи, либо -1 если ничего не найдено</returns>
    public static int RelationTypeByGuid(Guid relationTypeGuid, IUserSession userSession)
    {
      return TechCardConsts.MetaDataHelper.GetRelationTypeID(relationTypeGuid);
    }

    /// <summary>Проверяет является ли тип объекта - типом TechCard</summary>
    /// <param name="objectType">Тип объекта (Can be Int32, Guid, IDBObjectType)</param>
    /// <returns></returns>
    [Obsolete("Use IsTechcardObjectType method", false)]
    public static bool isTechcardObjectType(object objectType)
    {
      return TechCardConsts.Utils.IsTechcardObjectType(objectType);
    }

    /// <summary>
    /// Проверяет является ли тип объекта - "не унаследованным" типом TechCard
    /// (не унаследованным от базового тех. типа)
    /// </summary>
    /// <param name="objectType">Тип объекта (Can be Int32, Guid, IDBObjectType)</param>
    /// <returns></returns>
    [Obsolete("Use IsTechcardNotInheridObjType method", false)]
    public static bool isTechcardNotInheridObjType(object objectType)
    {
      return TechCardConsts.Utils.IsTechcardNotInheridObjType(objectType);
    }

    /// <summary>Проверяет является ли тип объекта - типом TechCard</summary>
    /// <param name="objectType">Тип объекта (Can be Int32, Guid, IDBObjectType)</param>
    /// <returns></returns>
    public static bool IsTechcardObjectType(object objectType)
    {
      int objectType1 = TechCardConsts.Utils.GetObjectType(objectType);
      if (objectType1.Equals(-1))
        return false;
      bool flag;
      if (TechCardConsts.Utils.ObjTypeCache.TryGetValue(objectType1, out flag))
        return flag;
      try
      {
        foreach (int techAllBaseObjType in (IEnumerable<int>) TechCardConsts.ObjectTypes.TechAllBaseObjTypes)
        {
          if (TechCardConsts.MetaDataHelper.IsObjectTypeChildOf(objectType1, techAllBaseObjType))
          {
            flag = true;
            return true;
          }
        }
      }
      finally
      {
        TechCardConsts.Utils.ObjTypeCache[objectType1] = flag;
      }
      return false;
    }

    /// <summary>
    /// Проверяет является ли тип объекта - "не унаследованным" типом TechCard
    /// (не унаследованным от базового тех. типа)
    /// </summary>
    /// <param name="objectType">Тип объекта (Can be Int32, Guid, IDBObjectType)</param>
    /// <returns></returns>
    public static bool IsTechcardNotInheridObjType(object objectType)
    {
      int objectType1 = TechCardConsts.Utils.GetObjectType(objectType);
      if (!TechCardConsts.Utils.IsTechcardObjectType((object) objectType1))
        return false;
      bool flag;
      if (TechCardConsts.Utils.TechNotInheritObjTypeCache.TryGetValue(objectType1, out flag))
        return flag;
      try
      {
        foreach (int inheritedBaseObjType in (IEnumerable<int>) TechCardConsts.ObjectTypes.TechNotInheritedBaseObjTypes)
        {
          if (TechCardConsts.MetaDataHelper.IsObjectTypeChildOf(objectType1, inheritedBaseObjType))
          {
            flag = true;
            return true;
          }
        }
      }
      finally
      {
        TechCardConsts.Utils.TechNotInheritObjTypeCache[objectType1] = flag;
      }
      return false;
    }

    /// <summary>Получение ид. типа объекта по входным данным</summary>
    /// <param name="objectType"></param>
    /// <remarks>В качестве типа входного параметра могут быть Int32, Guid, IDBObjectType</remarks>
    /// <returns></returns>
    public static int GetObjectType(object objectType)
    {
      int objectType1 = -1;
      switch (objectType)
      {
        case null:
          return objectType1;
        case int num:
          objectType1 = num;
          break;
        case Guid objTypeGuid:
          objectType1 = TechCardConsts.MetaDataHelper.GetObjectTypeID(objTypeGuid);
          break;
        case IDBObjectType dbObjectType:
          objectType1 = dbObjectType.ObjectType;
          break;
      }
      return objectType1;
    }

    /// <summary>Получение ид. типа связи по входным данным</summary>
    /// <param name="relationType"></param>
    /// <remarks>В качестве типа входного параметра могут быть Int32, Guid, IDBRelationType</remarks>
    /// <returns></returns>
    public static int GetRelationType(object relationType)
    {
      int relationType1 = -1;
      switch (relationType)
      {
        case null:
          return relationType1;
        case int num:
          relationType1 = num;
          break;
        case Guid relTypeGuid:
          relationType1 = TechCardConsts.MetaDataHelper.GetRelationTypeID(relTypeGuid);
          break;
        case IDBRelationType dbRelationType:
          relationType1 = dbRelationType.RelationType;
          break;
      }
      return relationType1;
    }

    /// <summary>Получение для объекта строки заголовка</summary>
    /// <param name="objectId"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public static string GetObjectString(long objectId, IUserSession session)
    {
      return TechCardConsts.Utils.GetObjectString(objectId, session, false);
    }

    /// <summary>Получение для объекта строки заголовка</summary>
    /// <param name="objectId"></param>
    /// <param name="session"></param>
    /// <param name="includeVerInfo"></param>
    /// <returns></returns>
    public static string GetObjectString(long objectId, IUserSession session, bool includeVerInfo)
    {
      string objectString = string.Empty;
      if (objectId != 0L && objectId != -1L)
      {
        if (includeVerInfo)
          return TechCardConsts.Utils.GetObjectString(session.GetObject(objectId, false), true);
        QuickObjectInfo objectInfo = session.GetObjectInfo(objectId);
        if (!objectInfo.Empty)
        {
          objectString = objectInfo.Caption;
          if (objectString.Equals(string.Empty))
            objectString = string.Format(LocalizationHolder.rm.GetString("Interfaces.TechCard_10"), (object) objectInfo.ObjectID);
        }
        else
          objectString = string.Empty;
      }
      return objectString;
    }

    /// <summary>Получение для объекта строки заголовка</summary>
    /// <param name="dbObject"></param>
    /// <param name="includeVerInfo"></param>
    /// <returns></returns>
    public static string GetObjectString(IDBObject dbObject, bool includeVerInfo)
    {
      string empty = string.Empty;
      if (dbObject == null)
        return empty;
      string objectString = dbObject.Caption;
      if (!includeVerInfo)
      {
        if (objectString.Equals(string.Empty))
          objectString = string.Format(LocalizationHolder.rm.GetString("Interfaces.TechCard_10"), (object) dbObject.ObjectID);
      }
      else
        objectString = !objectString.Equals(string.Empty) ? objectString + string.Format(LocalizationHolder.rm.GetString("Interfaces.TechCard_21"), (object) dbObject.VersionID) : string.Format(LocalizationHolder.rm.GetString("Interfaces.TechCard_10"), (object) dbObject.ObjectID);
      return objectString;
    }

    /// <summary>Получение для объектов строк заголовков</summary>
    /// <param name="dbObjects"></param>
    /// <param name="includeVerInfo"></param>
    /// <returns></returns>
    public static List<string> GetObjectStrings(List<IDBObject> dbObjects, bool includeVerInfo)
    {
      List<string> objectStrings = new List<string>();
      if (dbObjects == null || dbObjects.Count == 0)
        return objectStrings;
      objectStrings.Capacity = dbObjects.Count;
      foreach (IDBObject dbObject in dbObjects)
      {
        string objectString = TechCardConsts.Utils.GetObjectString(dbObject, includeVerInfo);
        if (!(objectString == string.Empty))
          objectStrings.Add(objectString);
      }
      return objectStrings;
    }

    /// <summary>Получение для объектов строк заголовка</summary>
    /// <param name="objIDs">Перечень ид. версий объектов</param>
    /// <param name="objTypeIds">Ид. типа объектов (для ускорения поиска)</param>
    /// <param name="objCaptions">Результат</param>
    /// <param name="session"></param>
    /// <returns></returns>
    public static bool GetObjectString(
      List<long> objIDs,
      int objTypeIds,
      IUserSession session,
      out Dictionary<long, string> objCaptions)
    {
      objCaptions = new Dictionary<long, string>();
      if (objIDs == null || objIDs.Count == 0)
        return false;
      List<long> objIdList = new List<long>(2 * objIDs.Count);
      objIdList.AddRange((IEnumerable<long>) objIDs);
      objIdList.AddRange(objIDs.Where<long>((System.Func<long, bool>) (item => item > 0L)).Select<long, long>((System.Func<long, long>) (item => -item)));
      foreach (long objId in objIDs)
      {
        if (objId >= 0L)
          objIdList.Add(-objId);
      }
      DataTable objectData = DataHelper.GetObjectData(objTypeIds, session, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>()
      {
        new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -50, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
      }, (IEnumerable<long>) objIdList);
      if (objectData == null || objectData.Rows.Count == 0)
        return false;
      foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
      {
        long key = DataSetProcessor.GetInt64Value(row, 0, 0L);
        string str = row[1].ToString();
        if (str.Equals(string.Empty))
          str = string.Format(LocalizationHolder.rm.GetString("Interfaces.TechCard_10"), (object) key);
        if (!objIDs.Contains(key))
          key = -key;
        if (!objCaptions.ContainsKey(key))
          objCaptions.Add(key, str);
      }
      return true;
    }

    /// <summary>Get "linked" object types</summary>
    /// <returns></returns>
    public static List<int> GetLinkedObjectTypes()
    {
      List<int> linkedObjectTypes = new List<int>();
      int objectTypeId1 = TechCardConsts.MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.OsnastBaseGUID);
      linkedObjectTypes.Add(objectTypeId1);
      linkedObjectTypes.AddRange((IEnumerable<int>) TechCardConsts.MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId1));
      int objectTypeId2 = TechCardConsts.MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.OborudBaseGUID);
      linkedObjectTypes.Add(objectTypeId2);
      linkedObjectTypes.AddRange((IEnumerable<int>) TechCardConsts.MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId2));
      int objectTypeId3 = TechCardConsts.MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.PersonalBaseGUID);
      linkedObjectTypes.Add(objectTypeId3);
      linkedObjectTypes.AddRange((IEnumerable<int>) TechCardConsts.MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId3));
      return linkedObjectTypes;
    }

    /// <summary>Get "linked" object types</summary>
    /// <returns></returns>
    public static List<int> GetGroupTpObjectTypes()
    {
      List<int> groupTpObjectTypes = new List<int>();
      int objectTypeId1 = TechCardConsts.MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechProcGroupGUID);
      groupTpObjectTypes.Add(objectTypeId1);
      groupTpObjectTypes.AddRange((IEnumerable<int>) TechCardConsts.MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId1));
      int objectTypeId2 = TechCardConsts.MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechProcTipovGUID);
      groupTpObjectTypes.Add(objectTypeId2);
      groupTpObjectTypes.AddRange((IEnumerable<int>) TechCardConsts.MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId2));
      return groupTpObjectTypes;
    }
  }
}
