// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.Espd_Static
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.Victor;

public static class Espd_Static
{
  public static void ESPD_Nastr_Init(One_Ved_Nastr ved_Nastr)
  {
    ved_Nastr._nameVed = "Программная спецификация";
    ved_Nastr._vedomostTemplateObjectGuid = Vedomost_VB_Static.GuidTemplateESPD;
    ved_Nastr._typeVed = Vedomost_VB.TypeVed.ESPD;
    ved_Nastr._guidParent = Guid.Empty;
    ved_Nastr._typeCreate = Vedomost_VB.TypeCreate.System;
    ved_Nastr._list_Usl_Read_From_SP = Espd_Static.List_Usl_Read_From_SP_ESPD_Init();
    ved_Nastr._list_Usl_Read_From_SP_Reference = Espd_Static.List_Usl_Read_From_SP_Reference_ESPD_Init();
    ved_Nastr._list_Ved_ID = Espd_Static.ListVed_ESPD_Id_Init();
    ved_Nastr._sbor_Options = Espd_Static.Sbor_Options_ESPD_Init();
    ved_Nastr._list_RazdelsVed = Espd_Static.List_Razdels_ESPD_Init();
    ved_Nastr._bases_Options_Ved = Espd_Static.Bases_Options_ESPD_Init();
    ved_Nastr._protection_From_Editing = Vedomost_VB_Static.Protection_From_Editing_All_Init();
    ved_Nastr._dopoln_Options_Ved = (Vedomost_VB.Dopoln_Options_Ved) null;
    ved_Nastr._zagolovki_Ved = Espd_Static.Zagolovki_Ved_ESPD_Init();
    ved_Nastr._sorting_Usl = Espd_Static.Sorting_Usl_ESPD_Init();
    ved_Nastr._sorting_Usl_Doc = Espd_Static.Sorting_Usl_Doc_ESPD_Init();
    ved_Nastr._merge_Usl2 = (Vedomost_VB.Merge_Usl2) null;
    ved_Nastr._algorithmToPrint = Espd_Static.AlgorithmToPrint_ESPD_Init();
    ved_Nastr._algorithmXml = Espd_Static.AlgorithmXml_ESPD_Init();
    ved_Nastr._typeCreateNastr = TypeCreateNastr.Default;
    ved_Nastr._autoSbor = 0;
  }

  public static Vedomost_VB.Bases_Options_Ved Bases_Options_ESPD_Init()
  {
    Vedomost_VB.Bases_Options_Ved basesOptionsVed = new Vedomost_VB.Bases_Options_Ved();
    basesOptionsVed._isInputDoc = true;
    basesOptionsVed._isInputIzd = false;
    basesOptionsVed._isInputMat = false;
    basesOptionsVed._list_quickObjectInfo = new List<QuickObjectInfo>();
    using (new SessionKeeper())
      ;
    return basesOptionsVed;
  }

  /// <summary> Условия ввода данных </summary>
  public static List<Vedomost_VB.Usl_Read_From_SP> List_Usl_Read_From_SP_ESPD_Init()
  {
    return new List<Vedomost_VB.Usl_Read_From_SP>();
  }

  /// <summary> Условия ввода данных по ссылкам </summary>
  public static List<Vedomost_VB.Usl_Read_From_SP> List_Usl_Read_From_SP_Reference_ESPD_Init()
  {
    return (List<Vedomost_VB.Usl_Read_From_SP>) null;
  }

  /// <summary> Список разделов  </summary>
  /// <returns></returns>
  public static List<Vedomost_VB.OneRazdelVed> List_Razdels_ESPD_Init()
  {
    return new List<Vedomost_VB.OneRazdelVed>()
    {
      new Vedomost_VB.OneRazdelVed()
      {
        _razdelVed = 1,
        _caption = "",
        _namePage = "",
        _name = "Документация"
      },
      new Vedomost_VB.OneRazdelVed()
      {
        _razdelVed = 2,
        _caption = "",
        _namePage = "",
        _name = "Комплексы"
      },
      new Vedomost_VB.OneRazdelVed()
      {
        _razdelVed = 3,
        _caption = "",
        _namePage = "",
        _name = "Компоненты"
      }
    };
  }

  public static Vedomost_VB.Bases_Options_Ved Bases_Options_Ved_ESPD_Init()
  {
    Vedomost_VB.Bases_Options_Ved basesOptionsVed = new Vedomost_VB.Bases_Options_Ved();
    basesOptionsVed._isMainSort1 = false;
    basesOptionsVed._isMainSummOdinakovyh = false;
    basesOptionsVed._isMainSort2 = false;
    basesOptionsVed._isMainCreateVtorRecords = false;
    basesOptionsVed._isMainSumm = false;
    basesOptionsVed._isOnlyUroven1 = false;
    basesOptionsVed._isVedSortGroup = false;
    basesOptionsVed._isVedMergerIsp = false;
    basesOptionsVed._isVedAddFuncGroup = false;
    basesOptionsVed._isVedSort1 = false;
    basesOptionsVed._isVedUnion = false;
    basesOptionsVed._isVedExtrectionVtor = false;
    basesOptionsVed._isVedMergerVtor = false;
    basesOptionsVed._isVedSortVtor = false;
    basesOptionsVed._isVedSummVtor = false;
    basesOptionsVed._isVedCreateZagolIspoln = false;
    basesOptionsVed._isVedCreateZagolSvoiaVed = false;
    basesOptionsVed._isVedCreateZagolPoPriznaku = false;
    basesOptionsVed._is_Extended_List_Names = false;
    basesOptionsVed._isVedAddToSp = false;
    basesOptionsVed._isFor_ZIP_SB_Raskr = false;
    basesOptionsVed._isFor_ZIP_SB_Add = false;
    basesOptionsVed._isFor_ZIP_COMPL_Raskr = false;
    basesOptionsVed._isFor_ZIP_COMPL_Add = false;
    basesOptionsVed._isVedAddToRazdel = 0;
    basesOptionsVed._isInputDoc = true;
    basesOptionsVed._isInputIzd = false;
    basesOptionsVed._isInputMat = false;
    basesOptionsVed._isReadOrInit_isMain = true;
    basesOptionsVed._list_quickObjectInfo = new List<QuickObjectInfo>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(Vedomost_VB_Static.GuidImbaseConctructorsky);
      basesOptionsVed._list_quickObjectInfo.Add(objectInfo);
    }
    return basesOptionsVed;
  }

  public static List<Vedomost_VB.OneFieldSpForRead> ListVed_ESPD_Id_Init()
  {
    return new List<Vedomost_VB.OneFieldSpForRead>()
    {
      new Vedomost_VB.OneFieldSpForRead(-2, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Long),
      new Vedomost_VB.OneFieldSpForRead(-7, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Int),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_ArticleGroupID, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Guid),
      new Vedomost_VB.OneFieldSpForRead(-20, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Int, 2),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_NameProg, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_NameDoc, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_TypePD, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String)
    };
  }

  public static Vedomost_VB.Sbor_Options Sbor_Options_ESPD_Init()
  {
    return new Vedomost_VB.Sbor_Options()
    {
      _is_Vydeliat_Sami_Komplekty = false,
      _is_Vydeliat_Therez_Komplekty = false,
      _isRaskrSP_s_takoi_Ved = 0,
      _isDopZam = 0,
      _isAllocateDopZam = 1,
      _isSamuSP_ne_iz_spiska_zanosit = false,
      _isReference_Show = false
    };
  }

  public static Vedomost_VB.Sorting_Usl Sorting_Usl_ESPD_Init()
  {
    return new Vedomost_VB.Sorting_Usl()
    {
      Sorting_Usl_VedOsn = Espd_Static.Sorting_Usl_VedOsn_ESPD_Init()
    };
  }

  public static Vedomost_VB.Sorting_Usl_Doc Sorting_Usl_Doc_ESPD_Init()
  {
    Vedomost_VB.Sorting_Usl_Doc sortingUslDoc = new Vedomost_VB.Sorting_Usl_Doc();
    Vedomost_VB.Sorting_Usl_Doc_OneRazdel sortingUslDocOneRazdel1 = new Vedomost_VB.Sorting_Usl_Doc_OneRazdel()
    {
      _razdelNum = 1,
      _list_sorting_Usl_Doc_OneRazdel = new List<Vedomost_VB.Sorting_Usl_Doc_OneGrafa>()
    };
    sortingUslDocOneRazdel1._list_sorting_Usl_Doc_OneRazdel.Add(new Vedomost_VB.Sorting_Usl_Doc_OneGrafa()
    {
      _grafa = "Обозначение",
      _beginSravn = Vedomost_VB.BeginSravn.S_begin,
      _num_symb_ot = 0,
      _endSravn = Vedomost_VB.EndSravn.Do_end,
      _sravnenie = Vedomost_VB.Sravnenie.Symbol,
      _pustyeStroki = Vedomost_VB.PustyeStroki.Vnathale
    });
    sortingUslDoc._list_sorting_Usl_Doc.Add(sortingUslDocOneRazdel1);
    Vedomost_VB.Sorting_Usl_Doc_OneRazdel sortingUslDocOneRazdel2 = new Vedomost_VB.Sorting_Usl_Doc_OneRazdel()
    {
      _razdelNum = 2,
      _list_sorting_Usl_Doc_OneRazdel = new List<Vedomost_VB.Sorting_Usl_Doc_OneGrafa>()
    };
    sortingUslDocOneRazdel2._list_sorting_Usl_Doc_OneRazdel.Add(new Vedomost_VB.Sorting_Usl_Doc_OneGrafa()
    {
      _grafa = "Обозначение",
      _beginSravn = Vedomost_VB.BeginSravn.S_begin,
      _num_symb_ot = 0,
      _endSravn = Vedomost_VB.EndSravn.Do_end,
      _sravnenie = Vedomost_VB.Sravnenie.Symbol,
      _pustyeStroki = Vedomost_VB.PustyeStroki.Vnathale
    });
    sortingUslDoc._list_sorting_Usl_Doc.Add(sortingUslDocOneRazdel2);
    Vedomost_VB.Sorting_Usl_Doc_OneRazdel sortingUslDocOneRazdel3 = new Vedomost_VB.Sorting_Usl_Doc_OneRazdel()
    {
      _razdelNum = 3,
      _list_sorting_Usl_Doc_OneRazdel = new List<Vedomost_VB.Sorting_Usl_Doc_OneGrafa>()
    };
    sortingUslDocOneRazdel3._list_sorting_Usl_Doc_OneRazdel.Add(new Vedomost_VB.Sorting_Usl_Doc_OneGrafa()
    {
      _grafa = "Обозначение",
      _beginSravn = Vedomost_VB.BeginSravn.S_begin,
      _num_symb_ot = 0,
      _endSravn = Vedomost_VB.EndSravn.Do_end,
      _sravnenie = Vedomost_VB.Sravnenie.Symbol,
      _pustyeStroki = Vedomost_VB.PustyeStroki.Vnathale
    });
    sortingUslDoc._list_sorting_Usl_Doc.Add(sortingUslDocOneRazdel3);
    return sortingUslDoc;
  }

  public static Vedomost_VB.Sorting_Usl_One_From4 Sorting_Usl_VedOsn_ESPD_Init()
  {
    Vedomost_VB.Sorting_Usl_One_From4 sortingUslOneFrom4 = new Vedomost_VB.Sorting_Usl_One_From4();
    sortingUslOneFrom4._name = "Sorting_Usl_VedOsn";
    sortingUslOneFrom4._list_sorting_Usl_OneRazdel = new List<Vedomost_VB.Sorting_Usl_OneRazdel>();
    Vedomost_VB.Sorting_Usl_OneRazdel sorting_Usl_OneRazdel1 = new Vedomost_VB.Sorting_Usl_OneRazdel();
    sorting_Usl_OneRazdel1._razdelNum = 1L;
    sorting_Usl_OneRazdel1._list_sorting_Usl_One = new List<Vedomost_VB.Sorting_Usl_One>();
    Vedomost_VB_Static.Sorting_Usl_One_FullStr(Vedomost_VB.TypeField.ObjectType, AvsIDCache.Attr_Designation, Vedomost_VB.TypeFieldVedRec.Undefined, sorting_Usl_OneRazdel1, Vedomost_VB.Sravnenie.Symbol);
    sortingUslOneFrom4._list_sorting_Usl_OneRazdel.Add(sorting_Usl_OneRazdel1);
    Vedomost_VB.Sorting_Usl_OneRazdel sorting_Usl_OneRazdel2 = new Vedomost_VB.Sorting_Usl_OneRazdel();
    sorting_Usl_OneRazdel2._razdelNum = 2L;
    sorting_Usl_OneRazdel2._list_sorting_Usl_One = new List<Vedomost_VB.Sorting_Usl_One>();
    Vedomost_VB_Static.Sorting_Usl_One_FullStr(Vedomost_VB.TypeField.ObjectType, AvsIDCache.Attr_Designation, Vedomost_VB.TypeFieldVedRec.Undefined, sorting_Usl_OneRazdel2, Vedomost_VB.Sravnenie.Symbol);
    sortingUslOneFrom4._list_sorting_Usl_OneRazdel.Add(sorting_Usl_OneRazdel2);
    Vedomost_VB.Sorting_Usl_OneRazdel sorting_Usl_OneRazdel3 = new Vedomost_VB.Sorting_Usl_OneRazdel();
    sorting_Usl_OneRazdel3._razdelNum = 3L;
    sorting_Usl_OneRazdel3._list_sorting_Usl_One = new List<Vedomost_VB.Sorting_Usl_One>();
    Vedomost_VB_Static.Sorting_Usl_One_FullStr(Vedomost_VB.TypeField.ObjectType, AvsIDCache.Attr_Designation, Vedomost_VB.TypeFieldVedRec.Undefined, sorting_Usl_OneRazdel3, Vedomost_VB.Sravnenie.Symbol);
    sortingUslOneFrom4._list_sorting_Usl_OneRazdel.Add(sorting_Usl_OneRazdel3);
    return sortingUslOneFrom4;
  }

  public static Vedomost_VB.Zagolovki_Ved Zagolovki_Ved_ESPD_Init()
  {
    Vedomost_VB.Zagolovki_Ved zagolovkiVed = new Vedomost_VB.Zagolovki_Ved()
    {
      _typeField = Vedomost_VB.TypeField.TypeFieldVedRec,
      _typeCompare = Vedomost_VB.TypeCompare.Int,
      _vyvodit_PodZagolovki = false,
      _userZagolovki = false,
      _locationZagolovki = true,
      _typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Razdel_Ved,
      _list_One_Zagolovok = new List<Vedomost_VB.One_Zagolovok>(),
      _include_Name = ""
    };
    zagolovkiVed._list_One_Zagolovok.Add(new Vedomost_VB.One_Zagolovok()
    {
      _granicaPriznaka = "1",
      _name = "Документация"
    });
    zagolovkiVed._list_One_Zagolovok.Add(new Vedomost_VB.One_Zagolovok()
    {
      _granicaPriznaka = "2",
      _name = "Комплексы"
    });
    zagolovkiVed._list_One_Zagolovok.Add(new Vedomost_VB.One_Zagolovok()
    {
      _granicaPriznaka = "3",
      _name = "Компоненты"
    });
    return zagolovkiVed;
  }

  public static Vedomost_VB.ESPD Espd_Init()
  {
    return new Vedomost_VB.ESPD()
    {
      _isAddLU = true,
      _isCreateLU = true,
      _isOpenLU = true,
      _isAddToSpLU = true,
      _isAddRemark = true,
      _textRemark = "Размножать по указанию"
    };
  }

  public static Guid GuidEspd_By_TypeEspd(Vedomost_VB.TypeVed Espd)
  {
    if (Espd == Vedomost_VB.TypeVed.Undefined)
      return Guid.Empty;
    Guid guid = Guid.Empty;
    if (Espd == Vedomost_VB.TypeVed.ESPD)
      guid = Vedomost_VB_Static.GuidSPESPD;
    return guid;
  }

  public static Guid GuidTemplateEspd_By_TypeEspd(Vedomost_VB.TypeVed espd)
  {
    if (espd == Vedomost_VB.TypeVed.Undefined)
      return Guid.Empty;
    Guid guid = Guid.Empty;
    if (espd == Vedomost_VB.TypeVed.ESPD)
      guid = Vedomost_VB_Static.GuidTemplateESPD;
    return guid;
  }

  public static Vedomost_VB.TypeVed TypeEspd_By_GuidTypeEspd(Guid guidTypeEspd)
  {
    if (guidTypeEspd == Guid.Empty)
      return Vedomost_VB.TypeVed.Undefined;
    Vedomost_VB.TypeVed typeVed = Vedomost_VB.TypeVed.Undefined;
    if (guidTypeEspd == Vedomost_VB_Static.GuidSPESPD)
      typeVed = Vedomost_VB.TypeVed.ESPD;
    return typeVed;
  }

  public static Vedomost_VB.TypeVed TypeEspd_By_GuidTemplateEspd(Guid guidTemplateEspd)
  {
    if (guidTemplateEspd == Guid.Empty)
      return Vedomost_VB.TypeVed.Undefined;
    Vedomost_VB.TypeVed typeVed = Vedomost_VB.TypeVed.Undefined;
    if (guidTemplateEspd == Vedomost_VB_Static.GuidTemplateESPD)
      typeVed = Vedomost_VB.TypeVed.ESPD;
    return typeVed;
  }

  public static Vedomost_VB.Bases_Options_Ved Bases_Options_ESPD_Init(Vedomost_VB.TypeVed TypeEspd)
  {
    if (TypeEspd == Vedomost_VB.TypeVed.Undefined)
      return (Vedomost_VB.Bases_Options_Ved) null;
    Guid guidEspd = Espd_Static.GuidEspd_By_TypeEspd(TypeEspd);
    return guidEspd == Guid.Empty ? (Vedomost_VB.Bases_Options_Ved) null : Espd_Static.Based_Options_ESPD_Init(guidEspd);
  }

  public static Vedomost_VB.Bases_Options_Ved Based_Options_ESPD_Init(Guid guidEspd)
  {
    return guidEspd == Vedomost_VB_Static.GuidSPESPD ? Espd_Static.Bases_Options_ESPD_Init() : (Vedomost_VB.Bases_Options_Ved) null;
  }

  public static Vedomost_VB.AlgorithmToPrint AlgorithmToPrint_ESPD_Init()
  {
    Vedomost_VB.AlgorithmToPrint printEspdInit = new Vedomost_VB.AlgorithmToPrint()
    {
      _tableName = "Главная таблица",
      _list_OneRazdelToPrint = new List<Vedomost_VB.OneRazdelToPrint>()
    };
    printEspdInit._list_OneRazdelToPrint.Add(new Vedomost_VB.OneRazdelToPrint()
    {
      _razdelVed = 1,
      _oneRecordToPrint_Info = Espd_Static.oneRecordToPrint_ESPD_Info_Init()
    });
    printEspdInit._list_OneRazdelToPrint.Add(new Vedomost_VB.OneRazdelToPrint()
    {
      _razdelVed = 2,
      _oneRecordToPrint_Info = Espd_Static.oneRecordToPrint_ESPD_Info_Init()
    });
    printEspdInit._list_OneRazdelToPrint.Add(new Vedomost_VB.OneRazdelToPrint()
    {
      _razdelVed = 3,
      _oneRecordToPrint_Info = Espd_Static.oneRecordToPrint_ESPD_Info_Init()
    });
    printEspdInit._oneRecordToPrintIncluded = (Vedomost_VB.OneRecordToPrint) null;
    printEspdInit._oneRecordToPrintTitleIncluded = (Vedomost_VB.OneRecordToPrint) null;
    printEspdInit._oneRecordToPrintTitleVar = (Vedomost_VB.OneRecordToPrint) null;
    printEspdInit._oneRecordToPrintTitleIsp = (Vedomost_VB.OneRecordToPrint) null;
    printEspdInit._oneRecordToPrintTitle = Espd_Static.oneRecordToPrint_ESPD_TitleX_Init("oneRecordToPrintTitle");
    printEspdInit._oneRecordToPrintRemark = Espd_Static.oneRecordToPrint_ESPD_Remark_Init();
    printEspdInit._oneRecordToPrintRemarkShort = Espd_Static.oneRecordToPrint_ESPD_RemarkShort_Init();
    printEspdInit._oneRecordToPrintPasport = Vedomost_VB_Static.oneRecordToPrint_Pasport_Init();
    printEspdInit._oneRecordToPrintEmpty = Espd_Static.oneRecordToPrint_ESPD_Empty_Init();
    printEspdInit._oneRecordToPrintAdditional1 = (Vedomost_VB.OneRecordToPrint) null;
    printEspdInit._oneRecordToPrintAdditional2 = (Vedomost_VB.OneRecordToPrint) null;
    printEspdInit._oneRecordToPrintAdditional3 = (Vedomost_VB.OneRecordToPrint) null;
    printEspdInit._oneRecordToPrintAdditional4 = (Vedomost_VB.OneRecordToPrint) null;
    printEspdInit._additional1 = 0;
    printEspdInit._additional2 = 0;
    printEspdInit._additional3 = 0;
    printEspdInit._additional4 = 0;
    printEspdInit._isDeleteIdenticalTexts = false;
    printEspdInit._isCheck = true;
    printEspdInit._isUnbrokenDefis = true;
    return printEspdInit;
  }

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_ESPD_Info_Init()
  {
    Vedomost_VB.OneRecordToPrint printEspdInfoInit = new Vedomost_VB.OneRecordToPrint();
    printEspdInfoInit._nameTypeRec = "oneRecordToPrintInfo";
    printEspdInfoInit._parentId = "";
    printEspdInfoInit._tableRowId = "Основная строка";
    printEspdInfoInit._isVtorOblast = false;
    printEspdInfoInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint1 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Обозначение",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint1._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Designation
    });
    printEspdInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint1);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint2 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Наименование",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint2._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_NameProg
    });
    oneGrafaToPrint2._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "\r\n",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_NameDoc
    });
    oneGrafaToPrint2._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "\r\n",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_TypePD
    });
    printEspdInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint2);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint3 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Примечание",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint3._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Note
    });
    printEspdInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint3);
    return printEspdInfoInit;
  }

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_ESPD_TitleX_Init(string Name)
  {
    Vedomost_VB.OneRecordToPrint printEspdTitleXInit = new Vedomost_VB.OneRecordToPrint();
    printEspdTitleXInit._nameTypeRec = Name;
    printEspdTitleXInit._parentId = "";
    printEspdTitleXInit._tableRowId = "Заголовок";
    printEspdTitleXInit._isVtorOblast = false;
    printEspdTitleXInit._tableVtorOblastId = "";
    printEspdTitleXInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    printEspdTitleXInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    printEspdTitleXInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Наименование заголовок",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Name
    });
    printEspdTitleXInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return printEspdTitleXInit;
  }

  /// <summary> Запись Примечание </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_ESPD_Remark_Init()
  {
    Vedomost_VB.OneRecordToPrint printEspdRemarkInit = new Vedomost_VB.OneRecordToPrint();
    printEspdRemarkInit._nameTypeRec = "oneRecordToPrintRemark";
    printEspdRemarkInit._parentId = "";
    printEspdRemarkInit._tableRowId = "Длинная строка";
    printEspdRemarkInit._isVtorOblast = false;
    printEspdRemarkInit._tableVtorOblastId = "";
    printEspdRemarkInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    printEspdRemarkInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    printEspdRemarkInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Текст",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Name
    });
    printEspdRemarkInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return printEspdRemarkInit;
  }

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_ESPD_RemarkShort_Init()
  {
    Vedomost_VB.OneRecordToPrint espdRemarkShortInit = new Vedomost_VB.OneRecordToPrint();
    espdRemarkShortInit._nameTypeRec = "oneRecordToPrintRemarkShort";
    espdRemarkShortInit._parentId = "";
    espdRemarkShortInit._tableRowId = "Примечание короткое";
    espdRemarkShortInit._isVtorOblast = false;
    espdRemarkShortInit._tableVtorOblastId = "";
    espdRemarkShortInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    espdRemarkShortInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    espdRemarkShortInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Наименование примечания короткого",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Name
    });
    espdRemarkShortInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return espdRemarkShortInit;
  }

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_ESPD_Empty_Init()
  {
    return new Vedomost_VB.OneRecordToPrint()
    {
      _nameTypeRec = "oneRecordToPrintEmpty",
      _parentId = "",
      _tableRowId = "Пустая строка",
      _isVtorOblast = false,
      _tableVtorOblastId = "",
      _oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null,
      _oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null
    };
  }

  public static Vedomost_VB.AlgorithmXml AlgorithmXml_ESPD_Init()
  {
    return new Vedomost_VB.AlgorithmXml()
    {
      _oneRecordXmlPasport = Espd_Static.oneRecordXml_ESPD_Pasport_Init(),
      _oneRecordXml_Info = Espd_Static.oneRecordXml_ESPD_Info_Init(),
      _oneRecordXmlTitle = Espd_Static.oneRecordXml_ESPD_Title_Init(),
      _oneRecordXmlTitleVar = (Vedomost_VB.OneRecordXml) null,
      _oneRecordXmlTitleIsp = (Vedomost_VB.OneRecordXml) null,
      _oneRecordXmlIncluded = (Vedomost_VB.OneRecordXml) null,
      _oneRecordXmlTitleIncluded = (Vedomost_VB.OneRecordXml) null,
      _oneRecordXmlRemark = Espd_Static.oneRecordXml_ESPD_Remark_Init(),
      _oneRecordXmlRemarkShort = Espd_Static.oneRecordXml_ESPD_RemarkShort_Init(),
      _oneRecordXmlAdditional1 = (Vedomost_VB.OneRecordXml) null,
      _oneRecordXmlAdditional2 = (Vedomost_VB.OneRecordXml) null,
      _oneRecordXmlAdditional3 = (Vedomost_VB.OneRecordXml) null,
      _oneRecordXmlAdditional4 = (Vedomost_VB.OneRecordXml) null,
      _afterInfo = 1,
      _afterRemark = 0,
      _passportOut = 0,
      _passportIn = 0,
      _folderXmlIn = ""
    };
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_ESPD_Pasport_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Pasport",
      _tableRowId = "Основная надпись",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Обозначение документа",
      _nameToXml = "DocumentDesignation",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Наименование изделия",
      _nameToXml = "NameArticle",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_ESPD_Info_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Info",
      _tableRowId = "Основная строка",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Обозначение",
      _nameToXml = "Designation",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Наименование",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Примечание",
      _nameToXml = "Remark",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "ObjectIdIzd",
      _nameToXml = "ObjectIdIzd",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Attribute
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_ESPD_Title_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Title",
      _tableRowId = "Заголовок",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Наименование",
      _nameToXml = "Title",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_ESPD_Remark_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Remark",
      _tableRowId = "Длинная строка",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Текст",
      _nameToXml = "Remark",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_ESPD_RemarkShort_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "RemarkShort",
      _tableRowId = "Примечание короткое",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Наименование примечания короткого",
      _nameToXml = "RemarkShort",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }
}
