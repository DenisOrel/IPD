// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.Tabl_Static
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.AVS.Victor;

public static class Tabl_Static
{
  public static One_Ved_Nastr Tabl_Nastr_Init(Guid guidTabl, Guid guidTemplateTabl, bool isWrite)
  {
    if (guidTabl == Guid.Empty && guidTemplateTabl == Guid.Empty)
      return (One_Ved_Nastr) null;
    One_Ved_Nastr ved_Nastr = new One_Ved_Nastr();
    if (guidTabl == Guid.Empty)
      guidTabl = Vedomost_VB_Static.Get_GuidVedomost_ByTemplateGuid(guidTemplateTabl);
    if (guidTabl == Guid.Empty)
      return (One_Ved_Nastr) null;
    ved_Nastr._guidTypeVed = guidTabl;
    ved_Nastr._imsObjectType = MetaDataHelper.GetObjectType(ved_Nastr._guidTypeVed);
    if (ved_Nastr._imsObjectType == null)
      return (One_Ved_Nastr) null;
    ved_Nastr._idTypeVed = MetaDataHelper.GetObjectTypeID(ved_Nastr._guidTypeVed);
    ved_Nastr._typeDoc = Vedomost_VB.TypeDoc.Tabl;
    bool flag = false;
    if (ved_Nastr._imsObjectType.Guid == Vedomost_VB_Static.GuidTablSoed)
    {
      Tabl_Static.Tb_Soed_Init(ved_Nastr);
      flag = true;
      XmlDocument xmlDocument = ved_Nastr.XmlDocument_create();
      if (isWrite)
        Vedomost_VB_Static.WriteXmlNastrToBase(xmlDocument, ved_Nastr._vedomostTemplateObjectGuid);
    }
    if (!flag && ved_Nastr._imsObjectType.Guid == Vedomost_VB_Static.GuidTablSoedSz)
    {
      Tabl_Static.Tb_SoedSz_Init(ved_Nastr);
      flag = true;
      XmlDocument xmlDocument = ved_Nastr.XmlDocument_create();
      if (isWrite)
        Vedomost_VB_Static.WriteXmlNastrToBase(xmlDocument, ved_Nastr._vedomostTemplateObjectGuid);
    }
    if (!flag)
      Tabl_Static.Default_Nastr_Init(ved_Nastr);
    ved_Nastr._accessLevel = 2;
    return ved_Nastr;
  }

  private static void Default_Nastr_Init(One_Ved_Nastr ved_Nastr)
  {
    ved_Nastr._nameVed = "Новая";
    ved_Nastr._vedomostTemplateObjectGuid = Guid.Empty;
    ved_Nastr._typeVed = Vedomost_VB.TypeVed.Undefined;
    ved_Nastr._guidParent = Guid.Empty;
    ved_Nastr._typeCreate = Vedomost_VB.TypeCreate.User;
    ved_Nastr._list_Ved_ID = Tabl_Static.Default_Tabl_Id_Init();
    ved_Nastr._bases_Options_Ved = new Vedomost_VB.Bases_Options_Ved();
    ved_Nastr._algorithmToPrint = Tabl_Static.AlgorithmToPrint_Based_Init();
    ved_Nastr._algorithmXml = Tabl_Static.AlgorithmXml_Tabl_Based_Init();
    ved_Nastr._algorithm_Avs6_To_Ips = new Vedomost_VB.Algorithm_Avs6_To_Ips();
    ved_Nastr._typeCreateNastr = TypeCreateNastr.Empty;
  }

  public static List<Vedomost_VB.OneFieldSpForRead> Default_Tabl_Id_Init()
  {
    return new List<Vedomost_VB.OneFieldSpForRead>()
    {
      new Vedomost_VB.OneFieldSpForRead(-2, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Long),
      new Vedomost_VB.OneFieldSpForRead(-7, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Int),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Format, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Name, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Class, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Razmery_I_Parametry, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Gost, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Note, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String)
    };
  }

  /// <summary> Инициализация для ведомости такого типа </summary>
  /// <param name="Vedomost_VB.TypeVed.TypeVed"></param>
  /// <param name="guidTemplateTabl"></param>
  /// <param name="isWrite"></param>
  /// <returns></returns>
  public static One_Ved_Nastr Tabl_Nastr_Init(
    Vedomost_VB.TypeVed TypeTabl,
    Guid guidTemplateTabl,
    bool isWrite)
  {
    if (TypeTabl == Vedomost_VB.TypeVed.Undefined)
      return (One_Ved_Nastr) null;
    Guid guidTabl = Tabl_Static.GuidTabl_By_TypeTabl(TypeTabl);
    return guidTabl == Guid.Empty ? (One_Ved_Nastr) null : Tabl_Static.Tabl_Nastr_Init(guidTabl, guidTemplateTabl, isWrite);
  }

  /// <summary> Заполнение списка полей  </summary>
  public static List<Vedomost_VB.OneFieldSpForRead> List_Id_Init()
  {
    return new List<Vedomost_VB.OneFieldSpForRead>()
    {
      new Vedomost_VB.OneFieldSpForRead(-2, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Long),
      new Vedomost_VB.OneFieldSpForRead(-7, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Int),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Format, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Name, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String)
    };
  }

  public static Vedomost_VB.Bases_Options_Ved Based_Options_Tabl_Init()
  {
    Vedomost_VB.Bases_Options_Ved basesOptionsVed = new Vedomost_VB.Bases_Options_Ved();
    basesOptionsVed._isInputDoc = false;
    basesOptionsVed._isInputIzd = true;
    basesOptionsVed._isInputMat = false;
    basesOptionsVed._list_quickObjectInfo = new List<QuickObjectInfo>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(new Guid("{2db39ba8-8f6f-4c51-a277-949be35ad568}"));
      basesOptionsVed._list_quickObjectInfo.Add(objectInfo);
    }
    return basesOptionsVed;
  }

  public static Vedomost_VB.AlgorithmToPrint AlgorithmToPrint_Based_Init()
  {
    return new Vedomost_VB.AlgorithmToPrint()
    {
      _tableName = "Главная таблица",
      _oneRecordToPrint_Info = Tabl_Static.oneRecordToPrint_Tabl_Based_Info_Init(),
      _oneRecordToPrintTitle = Tabl_Static.oneRecordToPrint_Tabl_Based_TitleX_Init("oneRecordToPrintTitle"),
      _oneRecordToPrintTitleVar = Tabl_Static.oneRecordToPrint_Tabl_Based_TitleX_Init("oneRecordToPrintTitleVar"),
      _oneRecordToPrintTitleIsp = Tabl_Static.oneRecordToPrint_Tabl_Based_TitleX_Init("oneRecordToPrintTitleIsp"),
      _oneRecordToPrintRemark = Tabl_Static.oneRecordToPrint_Tabl_Based_Remark_Init(),
      _oneRecordToPrintRemarkShort = Tabl_Static.oneRecordToPrint_Tabl_Based_RemarkShort_Init(),
      _oneRecordToPrintEmpty = Tabl_Static.oneRecordToPrint_Tabl_Based_Empty_Init(),
      _oneRecordToPrintAdditional1 = Tabl_Static.oneRecordToPrint_Tabl_Based_Additional_Init("1"),
      _oneRecordToPrintAdditional2 = Tabl_Static.oneRecordToPrint_Tabl_Based_Additional_Init("2"),
      _oneRecordToPrintAdditional3 = Tabl_Static.oneRecordToPrint_Tabl_Based_Additional_Init("3"),
      _oneRecordToPrintAdditional4 = Tabl_Static.oneRecordToPrint_Tabl_Based_Additional_Init("4"),
      _additional1 = 0,
      _additional2 = 0,
      _additional3 = 0,
      _additional4 = 0,
      _isDeleteIdenticalTexts = false,
      _isCheck = true,
      _isUnbrokenDefis = true,
      _oneRecordToPrintPasport = Tabl_Static.oneRecordToPrint_Pasport_Init()
    };
  }

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Based_Info_Init()
  {
    Vedomost_VB.OneRecordToPrint tablBasedInfoInit = new Vedomost_VB.OneRecordToPrint();
    tablBasedInfoInit._nameTypeRec = "oneRecordToPrintInfo";
    tablBasedInfoInit._parentId = "";
    tablBasedInfoInit._tableRowId = "Основная строка";
    tablBasedInfoInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Примечание",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Note
    });
    tablBasedInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return tablBasedInfoInit;
  }

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Based_TitleX_Init(string Name)
  {
    Vedomost_VB.OneRecordToPrint tablBasedTitleXInit = new Vedomost_VB.OneRecordToPrint();
    tablBasedTitleXInit._nameTypeRec = Name;
    tablBasedTitleXInit._parentId = "";
    tablBasedTitleXInit._tableRowId = "Заголовок";
    tablBasedTitleXInit._isVtorOblast = false;
    tablBasedTitleXInit._tableVtorOblastId = "";
    tablBasedTitleXInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    tablBasedTitleXInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    tablBasedTitleXInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Текст заголовка",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Name
    });
    tablBasedTitleXInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return tablBasedTitleXInit;
  }

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Based_Remark_Init()
  {
    Vedomost_VB.OneRecordToPrint tablBasedRemarkInit = new Vedomost_VB.OneRecordToPrint();
    tablBasedRemarkInit._nameTypeRec = "oneRecordToPrintRemark";
    tablBasedRemarkInit._parentId = "";
    tablBasedRemarkInit._tableRowId = "Длинная строка";
    tablBasedRemarkInit._isVtorOblast = false;
    tablBasedRemarkInit._tableVtorOblastId = "";
    tablBasedRemarkInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    tablBasedRemarkInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    tablBasedRemarkInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
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
    tablBasedRemarkInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return tablBasedRemarkInit;
  }

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Based_RemarkShort_Init()
  {
    Vedomost_VB.OneRecordToPrint basedRemarkShortInit = new Vedomost_VB.OneRecordToPrint();
    basedRemarkShortInit._nameTypeRec = "oneRecordToPrintRemarkShort";
    basedRemarkShortInit._parentId = "";
    basedRemarkShortInit._tableRowId = "Примечание короткое";
    basedRemarkShortInit._isVtorOblast = false;
    basedRemarkShortInit._tableVtorOblastId = "";
    basedRemarkShortInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    basedRemarkShortInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    basedRemarkShortInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Текст примечания короткого",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Name
    });
    basedRemarkShortInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return basedRemarkShortInit;
  }

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Based_Empty_Init()
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

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Based_Additional_Init(
    string number)
  {
    Vedomost_VB.OneRecordToPrint basedAdditionalInit = new Vedomost_VB.OneRecordToPrint();
    basedAdditionalInit._nameTypeRec = "Additional" + number;
    basedAdditionalInit._parentId = "";
    basedAdditionalInit._tableRowId = "Дополнительная " + number;
    basedAdditionalInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Примечание " + number,
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Note
    });
    basedAdditionalInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return basedAdditionalInit;
  }

  public static Vedomost_VB.AlgorithmXml AlgorithmXml_Tabl_Based_Init()
  {
    return new Vedomost_VB.AlgorithmXml()
    {
      _oneRecordXmlPasport = Tabl_Static.oneRecordXml_Tabl_Based_Pasport_Init(),
      _oneRecordXml_Info = Tabl_Static.oneRecordXml_Tabl_Based_Info_Init(),
      _oneRecordXmlTitle = Tabl_Static.oneRecordXml_Tabl_Based_Title_Init(),
      _oneRecordXmlTitleVar = Tabl_Static.oneRecordXml_Tabl_Based_TitleVar_Init(),
      _oneRecordXmlTitleIsp = Tabl_Static.oneRecordXml_Tabl_Based_TitleIsp_Init(),
      _oneRecordXmlRemark = Tabl_Static.oneRecordXml_Tabl_Based_Remark_Init(),
      _oneRecordXmlRemarkShort = Tabl_Static.oneRecordXml_Tabl_Based_RemarkShort_Init(),
      _oneRecordXmlAdditional1 = Tabl_Static.oneRecordXml_Tabl_Based_Additional_Init("1"),
      _oneRecordXmlAdditional2 = Tabl_Static.oneRecordXml_Tabl_Based_Additional_Init("2"),
      _oneRecordXmlAdditional3 = Tabl_Static.oneRecordXml_Tabl_Based_Additional_Init("3"),
      _oneRecordXmlAdditional4 = Tabl_Static.oneRecordXml_Tabl_Based_Additional_Init("4"),
      _afterInfo = 1,
      _afterRemark = 0,
      _passportOut = 0,
      _passportIn = 0,
      _folderXmlIn = ""
    };
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Based_Pasport_Init()
  {
    return new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Pasport",
      _tableRowId = "Основная надпись",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Based_Info_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Info",
      _tableRowId = "Основная строка",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
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

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Based_Title_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Title",
      _tableRowId = "Заголовок",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Текст заголовка",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Примечание",
      _nameToXml = "Remark",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Based_TitleVar_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "TitleVar",
      _tableRowId = "Заголовок",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Текст заголовка",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Примечание",
      _nameToXml = "Remark",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Based_TitleIsp_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "TitleIsp",
      _tableRowId = "Заголовок",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Текст заголовка",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Примечание",
      _nameToXml = "Remark",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Based_Remark_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Remark",
      _tableRowId = "Длинная строка",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Наименование",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Based_RemarkShort_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "RemarkShort",
      _tableRowId = "Примечание короткое",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Наименование",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Based_Additional_Init(string number)
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Additional" + number,
      _tableRowId = "Дополнительная " + number,
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
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

  public static Guid GuidTabl_By_TypeTabl(Vedomost_VB.TypeVed typeTabl)
  {
    if (typeTabl == Vedomost_VB.TypeVed.Undefined)
      return Guid.Empty;
    Guid guid = Guid.Empty;
    if (typeTabl != Vedomost_VB.TypeVed.TABLSOED)
    {
      if (typeTabl == Vedomost_VB.TypeVed.TABLSOEDSZ)
        guid = Vedomost_VB_Static.GuidTablSoedSz;
    }
    else
      guid = Vedomost_VB_Static.GuidTablSoed;
    return guid;
  }

  public static Guid GuidTemplateTabl_By_TypeTabl(Vedomost_VB.TypeVed typeTabl)
  {
    if (typeTabl == Vedomost_VB.TypeVed.Undefined)
      return Guid.Empty;
    Guid guid = Guid.Empty;
    if (typeTabl != Vedomost_VB.TypeVed.TABLSOED)
    {
      if (typeTabl == Vedomost_VB.TypeVed.TABLSOEDSZ)
        guid = Vedomost_VB_Static.GuidTemplateTablSoedSz;
    }
    else
      guid = Vedomost_VB_Static.GuidTemplateTablSoed;
    return guid;
  }

  public static Vedomost_VB.TypeVed TypeTabl_By_GuidTypeTabl(Guid guidTypeTabl)
  {
    if (guidTypeTabl == Guid.Empty)
      return Vedomost_VB.TypeVed.Undefined;
    Vedomost_VB.TypeVed typeVed = Vedomost_VB.TypeVed.Undefined;
    if (guidTypeTabl == Vedomost_VB_Static.GuidTablSoed)
      typeVed = Vedomost_VB.TypeVed.TABLSOED;
    if (guidTypeTabl == Vedomost_VB_Static.GuidTablSoedSz)
      typeVed = Vedomost_VB.TypeVed.TABLSOEDSZ;
    return typeVed;
  }

  public static Vedomost_VB.TypeVed TypeTabl_By_GuidTemplateTabl(Guid guidTemplateTabl)
  {
    if (guidTemplateTabl == Guid.Empty)
      return Vedomost_VB.TypeVed.Undefined;
    Vedomost_VB.TypeVed typeVed = Vedomost_VB.TypeVed.Undefined;
    if (guidTemplateTabl == Vedomost_VB_Static.GuidTemplateTablSoed)
      typeVed = Vedomost_VB.TypeVed.TABLSOED;
    if (guidTemplateTabl == Vedomost_VB_Static.GuidTemplateTablSoedSz)
      typeVed = Vedomost_VB.TypeVed.TABLSOEDSZ;
    return typeVed;
  }

  public static Vedomost_VB.Bases_Options_Ved Bases_Options_Ved_Init(Vedomost_VB.TypeVed TypeTabl)
  {
    if (TypeTabl == Vedomost_VB.TypeVed.Undefined)
      return (Vedomost_VB.Bases_Options_Ved) null;
    Guid guidTabl = Tabl_Static.GuidTabl_By_TypeTabl(TypeTabl);
    return guidTabl == Guid.Empty ? (Vedomost_VB.Bases_Options_Ved) null : Tabl_Static.Based_Options_Tabl_Init(guidTabl);
  }

  public static Vedomost_VB.Bases_Options_Ved Based_Options_Tabl_Init(Guid guidTabl)
  {
    if (guidTabl == Vedomost_VB_Static.GuidTablSoed)
      return Tabl_Static.Bases_Options_TabSoed_Init();
    return guidTabl == Vedomost_VB_Static.GuidTablSoedSz ? Tabl_Static.Bases_Options_TabSoedSz_Init() : (Vedomost_VB.Bases_Options_Ved) null;
  }

  public static List<Vedomost_VB.OneFieldSpForRead> List_Tabl_ID_Init(Vedomost_VB.TypeVed TypeTabl)
  {
    if (TypeTabl == Vedomost_VB.TypeVed.Undefined)
      return (List<Vedomost_VB.OneFieldSpForRead>) null;
    Guid guidTabl = Tabl_Static.GuidTabl_By_TypeTabl(TypeTabl);
    return guidTabl == Guid.Empty ? (List<Vedomost_VB.OneFieldSpForRead>) null : Tabl_Static.List_Tabl_ID_Init(guidTabl);
  }

  public static List<Vedomost_VB.OneFieldSpForRead> List_Tabl_ID_Init(Guid guidTabl)
  {
    if (guidTabl == Vedomost_VB_Static.GuidTablSoed)
      return Tabl_Static.ListTabl_Tabl_Soed_Id_Init();
    return guidTabl == Vedomost_VB_Static.GuidTablSoedSz ? Tabl_Static.ListTabl_Tabl_SoedSz_Id_Init() : (List<Vedomost_VB.OneFieldSpForRead>) null;
  }

  public static Vedomost_VB.AlgorithmToPrint AlgorithmToPrint_Init_By_GuidTabl(Guid guidTabl)
  {
    if (guidTabl == Vedomost_VB_Static.GuidTablSoed)
      return Tabl_Static.AlgorithmToPrint_Tabl_Soed_Init();
    return guidTabl == Vedomost_VB_Static.GuidTablSoedSz ? Tabl_Static.AlgorithmToPrint_Tabl_SoedSz_Init() : (Vedomost_VB.AlgorithmToPrint) null;
  }

  public static Vedomost_VB.AlgorithmToPrint AlgorithmToPrint_Init_By_TypeTabl(
    Vedomost_VB.TypeVed TypeTabl)
  {
    if (TypeTabl == Vedomost_VB.TypeVed.Undefined)
      return (Vedomost_VB.AlgorithmToPrint) null;
    Guid guidTabl = Tabl_Static.GuidTabl_By_TypeTabl(TypeTabl);
    return guidTabl == Guid.Empty ? (Vedomost_VB.AlgorithmToPrint) null : Tabl_Static.AlgorithmToPrint_Init_By_GuidTabl(guidTabl);
  }

  public static Vedomost_VB.AlgorithmXml AlgorithmXml_Init_By_GuidTabl(Guid guidTabl)
  {
    if (guidTabl == Vedomost_VB_Static.GuidTablSoed)
      return Tabl_Static.AlgorithmXml_Tabl_Soed_Init();
    return guidTabl == Vedomost_VB_Static.GuidTablSoedSz ? Tabl_Static.AlgorithmXml_Tabl_SoedSz_Init() : (Vedomost_VB.AlgorithmXml) null;
  }

  public static Vedomost_VB.Algorithm_Avs6_To_Ips Algorithm_Avs6_To_Ips_Init_By_GuidTabl(
    Guid guidTabl)
  {
    if (guidTabl == Vedomost_VB_Static.GuidTablSoed)
      return Tabl_Static.Algorithm_Avs6_To_Ips_Tabl_Soed_Init();
    return guidTabl == Vedomost_VB_Static.GuidTablSoedSz ? Tabl_Static.Algorithm_Avs6_To_Ips_Tabl_SoedSz_Init() : (Vedomost_VB.Algorithm_Avs6_To_Ips) null;
  }

  public static Vedomost_VB.AlgorithmXml AlgorithmXml_Init_By_TypeTabl(Vedomost_VB.TypeVed TypeTabl)
  {
    if (TypeTabl == Vedomost_VB.TypeVed.Undefined)
      return (Vedomost_VB.AlgorithmXml) null;
    Guid guidTabl = Tabl_Static.GuidTabl_By_TypeTabl(TypeTabl);
    return guidTabl == Guid.Empty ? (Vedomost_VB.AlgorithmXml) null : Tabl_Static.AlgorithmXml_Init_By_GuidTabl(guidTabl);
  }

  public static Vedomost_VB.Algorithm_Avs6_To_Ips Algorithm_Avs6_To_Ips_Init_By_TypeTabl(
    Vedomost_VB.TypeVed TypeTabl)
  {
    if (TypeTabl == Vedomost_VB.TypeVed.Undefined)
      return (Vedomost_VB.Algorithm_Avs6_To_Ips) null;
    Guid guidTabl = Tabl_Static.GuidTabl_By_TypeTabl(TypeTabl);
    return guidTabl == Guid.Empty ? (Vedomost_VB.Algorithm_Avs6_To_Ips) null : Tabl_Static.Algorithm_Avs6_To_Ips_Init_By_GuidTabl(guidTabl);
  }

  public static List<Vedomost_VB.OneFieldSpForRead> ListTabl_Tabl_Id_Init()
  {
    return new List<Vedomost_VB.OneFieldSpForRead>()
    {
      new Vedomost_VB.OneFieldSpForRead(-2, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Long),
      new Vedomost_VB.OneFieldSpForRead(-7, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Int),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Format, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Name, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Class, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Razmery_I_Parametry, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Gost, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Note, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Connection, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireData, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireFrom, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireWhere, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireDesignation, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String)
    };
  }

  /// <summary> Инициализация </summary>
  /// <param name="one_ved_Nastr"></param>
  private static void Tb_SoedSz_Init(One_Ved_Nastr ved_Nastr)
  {
    ved_Nastr._nameVed = "Таблица соединений (сжатая)";
    ved_Nastr._vedomostTemplateObjectGuid = Vedomost_VB_Static.GuidTemplateTablSoedSz;
    ved_Nastr._typeVed = Vedomost_VB.TypeVed.TABLSOEDSZ;
    ved_Nastr._guidParent = Guid.Empty;
    ved_Nastr._typeCreate = Vedomost_VB.TypeCreate.System;
    ved_Nastr._list_Ved_ID = Tabl_Static.ListTabl_Tabl_SoedSz_Id_Init();
    ved_Nastr._bases_Options_Ved = Tabl_Static.Bases_Options_TabSoedSz_Init();
    ved_Nastr._algorithmToPrint = Tabl_Static.AlgorithmToPrint_Tabl_SoedSz_Init();
    ved_Nastr._algorithmXml = Tabl_Static.AlgorithmXml_Tabl_SoedSz_Init();
    ved_Nastr._algorithm_Avs6_To_Ips = Tabl_Static.Algorithm_Avs6_To_Ips_Tabl_SoedSz_Init();
    ved_Nastr._typeCreateNastr = TypeCreateNastr.Default;
  }

  /// <summary> Заполнение списка полей для ВП </summary>
  public static List<Vedomost_VB.OneFieldSpForRead> ListTabl_Tabl_SoedSz_Id_Init()
  {
    return new List<Vedomost_VB.OneFieldSpForRead>()
    {
      new Vedomost_VB.OneFieldSpForRead(-2, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Long),
      new Vedomost_VB.OneFieldSpForRead(-7, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Int),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Format, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Name, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Class, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Razmery_I_Parametry, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Gost, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Note, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Clamp, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Package, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Connection, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireLength, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireData, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireFrom, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireWhere, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireDesignation, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_HarnessDesignatin, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String)
    };
  }

  public static Vedomost_VB.Bases_Options_Ved Bases_Options_TabSoedSz_Init()
  {
    Vedomost_VB.Bases_Options_Ved basesOptionsVed = new Vedomost_VB.Bases_Options_Ved();
    basesOptionsVed._isInputDoc = false;
    basesOptionsVed._isInputIzd = true;
    basesOptionsVed._isInputMat = false;
    basesOptionsVed._list_quickObjectInfo = new List<QuickObjectInfo>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(new Guid("{2db39ba8-8f6f-4c51-a277-949be35ad568}"));
      basesOptionsVed._list_quickObjectInfo.Add(objectInfo);
    }
    return basesOptionsVed;
  }

  /// <summary> Настройки ВЫВОДА  </summary>
  /// <returns></returns>
  public static Vedomost_VB.AlgorithmToPrint AlgorithmToPrint_Tabl_SoedSz_Init()
  {
    return new Vedomost_VB.AlgorithmToPrint()
    {
      _tableName = "Главная таблица",
      _oneRecordToPrint_Info = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_Info_Init(),
      _oneRecordToPrintTitle = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_TitleX_Init("oneRecordToPrintTitle"),
      _oneRecordToPrintTitleVar = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_TitleX_Init("oneRecordToPrintTitleVar"),
      _oneRecordToPrintTitleIsp = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_TitleX_Init("oneRecordToPrintTitleIsp"),
      _oneRecordToPrintRemark = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_Remark_Init(),
      _oneRecordToPrintRemarkShort = Tabl_Static.oneRecordToPrint_TabSoedSz_RemarkShort_Init(),
      _oneRecordToPrintEmpty = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_Empty_Init(),
      _oneRecordToPrintAdditional1 = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_Additional_Init("1"),
      _oneRecordToPrintAdditional2 = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_Additional_Init("2"),
      _oneRecordToPrintAdditional3 = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_Additional_Init("3"),
      _oneRecordToPrintAdditional4 = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_Additional_Init("4"),
      _additional1 = 0,
      _additional2 = 0,
      _additional3 = 0,
      _additional4 = 0,
      _isDeleteIdenticalTexts = false,
      _isCheck = true,
      _isUnbrokenDefis = true,
      _oneRecordToPrintPasport = Tabl_Static.oneRecordToPrint_Pasport_Init()
    };
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_SoedSz_Pasport()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
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

  /// <summary> Информационная запись в Таблице соединенийС </summary>
  /// <returns></returns>
  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_SoedSz_Info_Init()
  {
    Vedomost_VB.OneRecordToPrint tablSoedSzInfoInit = new Vedomost_VB.OneRecordToPrint();
    tablSoedSzInfoInit._nameTypeRec = "oneRecordToPrintInfo";
    tablSoedSzInfoInit._parentId = "";
    tablSoedSzInfoInit._tableRowId = "Основная строка";
    tablSoedSzInfoInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint1 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Обозначение провода",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint1._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_WireDesignation
    });
    tablSoedSzInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint1);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint2 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Соединение",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint2._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Connection
    });
    tablSoedSzInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint2);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint3 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Данные провода",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint3._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Razmery_I_Parametry
    });
    tablSoedSzInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint3);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint4 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Примечание",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint4._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Note
    });
    tablSoedSzInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint4);
    return tablSoedSzInfoInit;
  }

  /// <summary> Заголовок  </summary>
  /// <param name="Name"></param>
  /// <returns></returns>
  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_SoedSz_TitleX_Init(string Name)
  {
    Vedomost_VB.OneRecordToPrint soedSzTitleXInit = new Vedomost_VB.OneRecordToPrint();
    soedSzTitleXInit._nameTypeRec = Name;
    soedSzTitleXInit._parentId = "";
    soedSzTitleXInit._tableRowId = "Заголовок";
    soedSzTitleXInit._isVtorOblast = false;
    soedSzTitleXInit._tableVtorOblastId = "";
    soedSzTitleXInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    soedSzTitleXInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    soedSzTitleXInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Текст заголовка",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Name
    });
    soedSzTitleXInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return soedSzTitleXInit;
  }

  /// <summary> Запись Примечание  </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_SoedSz_Remark_Init()
  {
    Vedomost_VB.OneRecordToPrint soedSzRemarkInit = new Vedomost_VB.OneRecordToPrint();
    soedSzRemarkInit._nameTypeRec = "oneRecordToPrintRemark";
    soedSzRemarkInit._parentId = "";
    soedSzRemarkInit._tableRowId = "Длинная строка";
    soedSzRemarkInit._isVtorOblast = false;
    soedSzRemarkInit._tableVtorOblastId = "";
    soedSzRemarkInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    soedSzRemarkInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    soedSzRemarkInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
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
    soedSzRemarkInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return soedSzRemarkInit;
  }

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_TabSoedSz_RemarkShort_Init()
  {
    Vedomost_VB.OneRecordToPrint szRemarkShortInit = new Vedomost_VB.OneRecordToPrint();
    szRemarkShortInit._nameTypeRec = "oneRecordToPrintRemarkShort";
    szRemarkShortInit._parentId = "";
    szRemarkShortInit._tableRowId = "Примечание короткое";
    szRemarkShortInit._isVtorOblast = false;
    szRemarkShortInit._tableVtorOblastId = "";
    szRemarkShortInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    szRemarkShortInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    szRemarkShortInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Текст примечания короткого",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Name
    });
    szRemarkShortInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return szRemarkShortInit;
  }

  /// <summary> Пустая Запись </summary>
  /// <returns></returns>
  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_SoedSz_Empty_Init()
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

  public static Vedomost_VB.AlgorithmXml AlgorithmXml_Tabl_SoedSz_Init()
  {
    return new Vedomost_VB.AlgorithmXml()
    {
      _oneRecordXmlPasport = Tabl_Static.oneRecordXml_Tabl_SoedSz_Pasport_Init(),
      _oneRecordXml_Info = Tabl_Static.oneRecordXml_Tabl_SoedSz_Info_Init(),
      _oneRecordXmlTitle = Tabl_Static.oneRecordXml_Tabl_SoedSz_Title_Init(),
      _oneRecordXmlTitleVar = Tabl_Static.oneRecordXml_Tabl_SoedSz_TitleVar_Init(),
      _oneRecordXmlTitleIsp = Tabl_Static.oneRecordXml_Tabl_SoedSz_TitleIsp_Init(),
      _oneRecordXmlRemark = Tabl_Static.oneRecordXml_Tabl_SoedSz_Remark_Init(),
      _oneRecordXmlRemarkShort = Tabl_Static.oneRecordXml_Tabl_SoedSz_RemarkShort_Init(),
      _oneRecordXmlAdditional1 = Tabl_Static.oneRecordXml_Tabl_SoedSz_Additional_Init("1"),
      _oneRecordXmlAdditional2 = Tabl_Static.oneRecordXml_Tabl_SoedSz_Additional_Init("2"),
      _oneRecordXmlAdditional3 = Tabl_Static.oneRecordXml_Tabl_SoedSz_Additional_Init("3"),
      _oneRecordXmlAdditional4 = Tabl_Static.oneRecordXml_Tabl_SoedSz_Additional_Init("4"),
      _afterInfo = 1,
      _afterRemark = 0,
      _passportOut = 0,
      _passportIn = 0,
      _folderXmlIn = ""
    };
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_SoedSz_Pasport_Init()
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

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_SoedSz_Info_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Info",
      _tableRowId = "Основная строка",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Обозначение провода",
      _nameToXml = "WireDesignation",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Соединение",
      _nameToXml = "Connection",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Данные провода",
      _nameToXml = "Wire",
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

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_SoedSz_Title_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Title",
      _tableRowId = "Заголовок",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Текст заголовка",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Примечание",
      _nameToXml = "Remark",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_SoedSz_TitleVar_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "TitleVar",
      _tableRowId = "Заголовок",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Текст заголовка",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Примечание",
      _nameToXml = "Remark",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_SoedSz_TitleIsp_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "TitleIsp",
      _tableRowId = "Заголовок",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Текст заголовка",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Примечание",
      _nameToXml = "Remark",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_SoedSz_Remark_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Remark",
      _tableRowId = "Длинная строка",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Наименование",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_SoedSz_RemarkShort_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "RemarkShort",
      _tableRowId = "Примечание короткое",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Наименование",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_SoedSz_Additional_Init(string number)
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Additional" + number,
      _tableRowId = "Дополнительная " + number,
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Обозначение провода",
      _nameToXml = "WireDesignation",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Соединение",
      _nameToXml = "Connection",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Данные провода",
      _nameToXml = "Wire",
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

  /// <summary> Дополнительная запись в Таблице  </summary>
  /// <returns></returns>
  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_SoedSz_Additional_Init(
    string number)
  {
    Vedomost_VB.OneRecordToPrint szAdditionalInit = new Vedomost_VB.OneRecordToPrint();
    szAdditionalInit._nameTypeRec = "Additional" + number;
    szAdditionalInit._parentId = "";
    szAdditionalInit._tableRowId = "Дополнительная " + number;
    szAdditionalInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint1 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Обозначение провода " + number,
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint1._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_WireDesignation
    });
    szAdditionalInit._listOneGrafaToPrint.Add(oneGrafaToPrint1);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint2 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Соединение " + number,
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint2._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_WireFrom
    });
    szAdditionalInit._listOneGrafaToPrint.Add(oneGrafaToPrint2);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint3 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Данные провода " + number,
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint3._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Razmery_I_Parametry
    });
    szAdditionalInit._listOneGrafaToPrint.Add(oneGrafaToPrint3);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint4 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Примечание " + number,
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint4._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Note
    });
    szAdditionalInit._listOneGrafaToPrint.Add(oneGrafaToPrint4);
    return szAdditionalInit;
  }

  public static Vedomost_VB.Algorithm_Avs6_To_Ips Algorithm_Avs6_To_Ips_Tabl_SoedSz_Init()
  {
    return new Vedomost_VB.Algorithm_Avs6_To_Ips()
    {
      _tableName = "Главная таблица",
      _oneRecord_Avs6_To_Ips_Info = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_SoedSz_Info_Init(),
      _oneRecord_Avs6_To_Ips_Title = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_SoedSz_TitleX_Init('S'),
      _oneRecord_Avs6_To_Ips_TitleVar = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_SoedSz_TitleX_Init('V'),
      _oneRecord_Avs6_To_Ips_TitleIsp = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_Soed_TitleX_Init('N'),
      _oneRecord_Avs6_To_Ips_Remark = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_SoedSz_Remark_Init(),
      _oneRecord_Avs6_To_Ips_RemarkShort = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_SoedSz_RemarkShort_Init(),
      _oneRecord_Avs6_To_Ips_Additional1 = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_SoedSz_Additional_Init('X', "1"),
      _oneRecord_Avs6_To_Ips_Additional2 = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_SoedSz_Additional_Init('Y', "2"),
      _oneRecord_Avs6_To_Ips_Additional3 = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_SoedSz_Additional_Init('E', "3"),
      _oneRecord_Avs6_To_Ips_Additional4 = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_SoedSz_Additional_Init('F', "4")
    };
  }

  /// <summary> Информационная запись в Таблице соединенийС </summary>
  /// <returns></returns>
  public static Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs6_To_Ips_Tabl_SoedSz_Info_Init()
  {
    Vedomost_VB.OneRecord_Avs6_To_Ips tablSoedSzInfoInit = new Vedomost_VB.OneRecord_Avs6_To_Ips();
    tablSoedSzInfoInit._recordType_Avs6 = 'I';
    tablSoedSzInfoInit._nameTypeRec = "Info";
    tablSoedSzInfoInit._parentId = "";
    tablSoedSzInfoInit._tableRowId = "Основная строка";
    tablSoedSzInfoInit._listOneGrafa_Avs6_To_Ips = new List<Vedomost_VB.OneGrafa_Avs6_To_Ips>();
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps1 = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Обозначение провода",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps1._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 226
    });
    tablSoedSzInfoInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps1);
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps2 = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Соединение",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps2._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 227
    });
    tablSoedSzInfoInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps2);
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps3 = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Данные провода",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps3._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 213
    });
    tablSoedSzInfoInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps3);
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps4 = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Примечание",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps4._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 7
    });
    tablSoedSzInfoInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps4);
    return tablSoedSzInfoInit;
  }

  public static Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs6_To_Ips_Tabl_SoedSz_TitleX_Init(
    char typeRec)
  {
    Vedomost_VB.OneRecord_Avs6_To_Ips soedSzTitleXInit = new Vedomost_VB.OneRecord_Avs6_To_Ips();
    soedSzTitleXInit._recordType_Avs6 = typeRec;
    soedSzTitleXInit._nameTypeRec = "Title" + typeRec.ToString();
    soedSzTitleXInit._parentId = "";
    soedSzTitleXInit._tableRowId = "Заголовок";
    soedSzTitleXInit._listOneGrafa_Avs6_To_Ips = new List<Vedomost_VB.OneGrafa_Avs6_To_Ips>();
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Текст заголовка",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 5
    });
    soedSzTitleXInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps);
    return soedSzTitleXInit;
  }

  public static Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs6_To_Ips_Tabl_SoedSz_Remark_Init()
  {
    Vedomost_VB.OneRecord_Avs6_To_Ips soedSzRemarkInit = new Vedomost_VB.OneRecord_Avs6_To_Ips();
    soedSzRemarkInit._recordType_Avs6 = 'R';
    soedSzRemarkInit._nameTypeRec = "Remark";
    soedSzRemarkInit._parentId = "";
    soedSzRemarkInit._tableRowId = "Длинная строка";
    soedSzRemarkInit._listOneGrafa_Avs6_To_Ips = new List<Vedomost_VB.OneGrafa_Avs6_To_Ips>();
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Текст",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 5
    });
    soedSzRemarkInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps);
    return soedSzRemarkInit;
  }

  public static Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs6_To_Ips_Tabl_SoedSz_RemarkShort_Init()
  {
    Vedomost_VB.OneRecord_Avs6_To_Ips szRemarkShortInit = new Vedomost_VB.OneRecord_Avs6_To_Ips();
    szRemarkShortInit._recordType_Avs6 = 'T';
    szRemarkShortInit._nameTypeRec = "RemarkShort";
    szRemarkShortInit._parentId = "";
    szRemarkShortInit._tableRowId = "Длинная строка";
    szRemarkShortInit._listOneGrafa_Avs6_To_Ips = new List<Vedomost_VB.OneGrafa_Avs6_To_Ips>();
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Текст",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 5
    });
    szRemarkShortInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps);
    return szRemarkShortInit;
  }

  public static Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs6_To_Ips_Tabl_SoedSz_Additional_Init(
    char typeRec,
    string number)
  {
    Vedomost_VB.OneRecord_Avs6_To_Ips szAdditionalInit = new Vedomost_VB.OneRecord_Avs6_To_Ips();
    szAdditionalInit._recordType_Avs6 = typeRec;
    szAdditionalInit._nameTypeRec = "Additional" + number.ToString();
    szAdditionalInit._parentId = "";
    szAdditionalInit._tableRowId = "Дополнительная " + number;
    szAdditionalInit._listOneGrafa_Avs6_To_Ips = new List<Vedomost_VB.OneGrafa_Avs6_To_Ips>();
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Обозначение провода " + number,
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 5
    });
    szAdditionalInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps);
    return szAdditionalInit;
  }

  /// <summary> Инициализация </summary>
  /// <param name="one_ved_Nastr"></param>
  private static void Tb_Soed_Init(One_Ved_Nastr ved_Nastr)
  {
    ved_Nastr._nameVed = "Таблица соединений";
    ved_Nastr._vedomostTemplateObjectGuid = Vedomost_VB_Static.GuidTemplateTablSoed;
    ved_Nastr._typeVed = Vedomost_VB.TypeVed.TABLSOED;
    ved_Nastr._guidParent = Guid.Empty;
    ved_Nastr._typeCreate = Vedomost_VB.TypeCreate.System;
    ved_Nastr._list_Ved_ID = Tabl_Static.ListTabl_Tabl_Soed_Id_Init();
    ved_Nastr._bases_Options_Ved = Tabl_Static.Bases_Options_TabSoed_Init();
    ved_Nastr._algorithmToPrint = Tabl_Static.AlgorithmToPrint_Tabl_Soed_Init();
    ved_Nastr._algorithmXml = Tabl_Static.AlgorithmXml_Tabl_Soed_Init();
    ved_Nastr._algorithm_Avs6_To_Ips = Tabl_Static.Algorithm_Avs6_To_Ips_Tabl_Soed_Init();
    ved_Nastr._typeCreateNastr = TypeCreateNastr.Default;
  }

  /// <summary> Заполнение списка полей  </summary>
  public static List<Vedomost_VB.OneFieldSpForRead> ListTabl_Tabl_Soed_Id_Init()
  {
    return new List<Vedomost_VB.OneFieldSpForRead>()
    {
      new Vedomost_VB.OneFieldSpForRead(-2, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Long),
      new Vedomost_VB.OneFieldSpForRead(-7, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.Int),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Format, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Name, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Class, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Razmery_I_Parametry, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Gost, AttributeSourceTypes.Object, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Note, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Clamp, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Package, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_Connection, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireLength, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireData, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireFrom, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireWhere, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_WireDesignation, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String),
      new Vedomost_VB.OneFieldSpForRead(AvsIDCache.Attr_HarnessDesignatin, AttributeSourceTypes.Relation, Vedomost_VB.TypeDataSel.String)
    };
  }

  public static Vedomost_VB.Bases_Options_Ved Bases_Options_TabSoed_Init()
  {
    Vedomost_VB.Bases_Options_Ved basesOptionsVed = new Vedomost_VB.Bases_Options_Ved();
    basesOptionsVed._isInputDoc = false;
    basesOptionsVed._isInputIzd = true;
    basesOptionsVed._isInputMat = false;
    basesOptionsVed._list_quickObjectInfo = new List<QuickObjectInfo>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(new Guid("{2db39ba8-8f6f-4c51-a277-949be35ad568}"));
      basesOptionsVed._list_quickObjectInfo.Add(objectInfo);
    }
    return basesOptionsVed;
  }

  /// <summary> Настройки ВЫВОДА  </summary>
  /// <returns></returns>
  public static Vedomost_VB.AlgorithmToPrint AlgorithmToPrint_Tabl_Soed_Init()
  {
    return new Vedomost_VB.AlgorithmToPrint()
    {
      _tableName = "Главная таблица",
      _oneRecordToPrint_Info = Tabl_Static.oneRecordToPrint_Tabl_Soed_Info_Init(),
      _oneRecordToPrintTitle = Tabl_Static.oneRecordToPrint_Tabl_Soed_TitleX_Init("oneRecordToPrintTitle"),
      _oneRecordToPrintTitleVar = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_TitleX_Init("oneRecordToPrintTitleVar"),
      _oneRecordToPrintTitleIsp = Tabl_Static.oneRecordToPrint_Tabl_SoedSz_TitleX_Init("oneRecordToPrintTitleIsp"),
      _oneRecordToPrintRemark = Tabl_Static.oneRecordToPrint_Tabl_Soed_Remark_Init(),
      _oneRecordToPrintRemarkShort = Tabl_Static.oneRecordToPrint_Tabl_Soed_RemarkShort_Init(),
      _oneRecordToPrintEmpty = Tabl_Static.oneRecordToPrint_Tabl_Soed_Empty_Init(),
      _oneRecordToPrintAdditional1 = Tabl_Static.oneRecordToPrint_Tabl_Soed_Additional_Init("1"),
      _oneRecordToPrintAdditional2 = Tabl_Static.oneRecordToPrint_Tabl_Soed_Additional_Init("2"),
      _oneRecordToPrintAdditional3 = Tabl_Static.oneRecordToPrint_Tabl_Soed_Additional_Init("3"),
      _oneRecordToPrintAdditional4 = Tabl_Static.oneRecordToPrint_Tabl_Soed_Additional_Init("4"),
      _additional1 = 0,
      _additional2 = 0,
      _additional3 = 0,
      _additional4 = 0,
      _isDeleteIdenticalTexts = false,
      _isCheck = true,
      _isUnbrokenDefis = true,
      _oneRecordToPrintPasport = Tabl_Static.oneRecordToPrint_Pasport_Init()
    };
  }

  /// <summary> Информационная запись в Таблице соединенийС </summary>
  /// <returns></returns>
  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Soed_Info_Init()
  {
    Vedomost_VB.OneRecordToPrint tablSoedInfoInit = new Vedomost_VB.OneRecordToPrint();
    tablSoedInfoInit._nameTypeRec = "oneRecordToPrintInfo";
    tablSoedInfoInit._parentId = "";
    tablSoedInfoInit._tableRowId = "Основная строка";
    tablSoedInfoInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint1 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Обозначение провода",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint1._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_WireDesignation
    });
    tablSoedInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint1);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint2 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Откуда идет",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint2._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_WireFrom
    });
    tablSoedInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint2);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint3 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Куда поступает",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint3._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_WireWhere
    });
    tablSoedInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint3);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint4 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Данные провода",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint4._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Razmery_I_Parametry
    });
    tablSoedInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint4);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint5 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Примечание",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint5._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Note
    });
    tablSoedInfoInit._listOneGrafaToPrint.Add(oneGrafaToPrint5);
    return tablSoedInfoInit;
  }

  /// <summary> Заголовок  </summary>
  /// <param name="Name"></param>
  /// <returns></returns>
  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Soed_TitleX_Init(string Name)
  {
    Vedomost_VB.OneRecordToPrint tablSoedTitleXInit = new Vedomost_VB.OneRecordToPrint();
    tablSoedTitleXInit._nameTypeRec = Name;
    tablSoedTitleXInit._parentId = "";
    tablSoedTitleXInit._tableRowId = "Заголовок";
    tablSoedTitleXInit._isVtorOblast = false;
    tablSoedTitleXInit._tableVtorOblastId = "";
    tablSoedTitleXInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    tablSoedTitleXInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    tablSoedTitleXInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Текст заголовка",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Name
    });
    tablSoedTitleXInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return tablSoedTitleXInit;
  }

  /// <summary> Запись Примечание  </summary>
  /// <returns></returns>
  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Soed_Remark_Init()
  {
    Vedomost_VB.OneRecordToPrint tablSoedRemarkInit = new Vedomost_VB.OneRecordToPrint();
    tablSoedRemarkInit._nameTypeRec = "oneRecordToPrintRemark";
    tablSoedRemarkInit._parentId = "";
    tablSoedRemarkInit._tableRowId = "Длинная строка";
    tablSoedRemarkInit._isVtorOblast = false;
    tablSoedRemarkInit._tableVtorOblastId = "";
    tablSoedRemarkInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    tablSoedRemarkInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    tablSoedRemarkInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
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
    tablSoedRemarkInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return tablSoedRemarkInit;
  }

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Soed_RemarkShort_Init()
  {
    Vedomost_VB.OneRecordToPrint soedRemarkShortInit = new Vedomost_VB.OneRecordToPrint();
    soedRemarkShortInit._nameTypeRec = "oneRecordToPrintRemarkShort";
    soedRemarkShortInit._parentId = "";
    soedRemarkShortInit._tableRowId = "Примечание короткое";
    soedRemarkShortInit._isVtorOblast = false;
    soedRemarkShortInit._tableVtorOblastId = "";
    soedRemarkShortInit._oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null;
    soedRemarkShortInit._oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null;
    soedRemarkShortInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Текст примечания короткого",
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Name
    });
    soedRemarkShortInit._listOneGrafaToPrint.Add(oneGrafaToPrint);
    return soedRemarkShortInit;
  }

  /// <summary> Пустая Запись </summary>
  /// <returns></returns>
  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Soed_Empty_Init()
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

  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Pasport_Init()
  {
    return new Vedomost_VB.OneRecordToPrint()
    {
      _nameTypeRec = "oneRecordToPrintPasport",
      _parentId = "",
      _tableRowId = "Основная надпись",
      _isVtorOblast = false,
      _tableVtorOblastId = "",
      _oneRecordToPrint_Vtor = (Vedomost_VB.OneRecordToPrint) null,
      _oneRecordToPrint_Itogo = (Vedomost_VB.OneRecordToPrint) null,
      _listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>()
    };
  }

  public static Vedomost_VB.AlgorithmXml AlgorithmXml_Tabl_Soed_Init()
  {
    return new Vedomost_VB.AlgorithmXml()
    {
      _oneRecordXmlPasport = Tabl_Static.oneRecordXml_Tabl_Soed_Pasport_Init(),
      _oneRecordXml_Info = Tabl_Static.oneRecordXml_Tabl_Soed_Info_Init(),
      _oneRecordXmlTitle = Tabl_Static.oneRecordXml_Tabl_Soed_Title_Init(),
      _oneRecordXmlTitleVar = Tabl_Static.oneRecordXml_Tabl_Soed_TitleVar_Init(),
      _oneRecordXmlTitleIsp = Tabl_Static.oneRecordXml_Tabl_Soed_TitleIsp_Init(),
      _oneRecordXmlRemark = Tabl_Static.oneRecordXml_Tabl_Soed_Remark_Init(),
      _oneRecordXmlRemarkShort = Tabl_Static.oneRecordXml_Tabl_Soed_RemarkShort_Init(),
      _oneRecordXmlAdditional1 = Tabl_Static.oneRecordXml_Tabl_Soed_Additional_Init("1"),
      _oneRecordXmlAdditional2 = Tabl_Static.oneRecordXml_Tabl_Soed_Additional_Init("2"),
      _oneRecordXmlAdditional3 = Tabl_Static.oneRecordXml_Tabl_Soed_Additional_Init("3"),
      _oneRecordXmlAdditional4 = Tabl_Static.oneRecordXml_Tabl_Soed_Additional_Init("4"),
      _afterInfo = 1,
      _afterRemark = 0,
      _passportOut = 0,
      _passportIn = 0,
      _folderXmlIn = ""
    };
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Soed_Pasport_Init()
  {
    return new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Pasport",
      _tableRowId = "Основная надпись",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Soed_Info_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Info",
      _tableRowId = "Основная строка",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Обозначение провода",
      _nameToXml = "WireDesignation",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Откуда идет",
      _nameToXml = "ConnectionFrom",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Куда поступает",
      _nameToXml = "ConnectionWhere",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Данные провода",
      _nameToXml = "Wire",
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

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Soed_Title_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Title",
      _tableRowId = "Заголовок",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Текст заголовка",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Примечание",
      _nameToXml = "Remark",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Soed_TitleVar_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "TitleVar",
      _tableRowId = "Заголовок",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Текст заголовка",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Примечание",
      _nameToXml = "Remark",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Soed_TitleIsp_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "TitleIsp",
      _tableRowId = "Заголовок",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Текст заголовка",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Примечание",
      _nameToXml = "Remark",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Soed_Remark_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Remark",
      _tableRowId = "Длинная строка",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Наименование",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Soed_RemarkShort_Init()
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "RemarkShort",
      _tableRowId = "Примечание короткое",
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Наименование",
      _nameToXml = "Name",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    return oneRecordXml;
  }

  public static Vedomost_VB.OneRecordXml oneRecordXml_Tabl_Soed_Additional_Init(string number)
  {
    Vedomost_VB.OneRecordXml oneRecordXml = new Vedomost_VB.OneRecordXml()
    {
      _nameTypeRec = "Additional" + number,
      _tableRowId = "Дополнительная " + number,
      _listOneFieldXml = new List<Vedomost_VB.OneFieldXml>()
    };
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Обозначение провода",
      _nameToXml = "WireDesignation",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Откуда идет",
      _nameToXml = "ConnectionFrom",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Куда поступает",
      _nameToXml = "ConnectionWhere",
      _typeDataToXml = Vedomost_VB.TypeDataToXml.Field
    });
    oneRecordXml._listOneFieldXml.Add(new Vedomost_VB.OneFieldXml()
    {
      _nameToFile = "Данные провода",
      _nameToXml = "Wire",
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

  /// <summary> Дополнительная запись в Таблице  </summary>
  /// <returns></returns>
  public static Vedomost_VB.OneRecordToPrint oneRecordToPrint_Tabl_Soed_Additional_Init(
    string number)
  {
    Vedomost_VB.OneRecordToPrint soedAdditionalInit = new Vedomost_VB.OneRecordToPrint();
    soedAdditionalInit._nameTypeRec = "Additional" + number;
    soedAdditionalInit._parentId = "";
    soedAdditionalInit._tableRowId = "Дополнительная " + number;
    soedAdditionalInit._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint1 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Обозначение провода " + number,
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint1._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_WireDesignation
    });
    soedAdditionalInit._listOneGrafaToPrint.Add(oneGrafaToPrint1);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint2 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Откуда идет " + number,
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint2._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_WireFrom
    });
    soedAdditionalInit._listOneGrafaToPrint.Add(oneGrafaToPrint2);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint3 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Куда поступает " + number,
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint3._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_WireWhere
    });
    soedAdditionalInit._listOneGrafaToPrint.Add(oneGrafaToPrint3);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint4 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Данные провода " + number,
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint4._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Razmery_I_Parametry
    });
    soedAdditionalInit._listOneGrafaToPrint.Add(oneGrafaToPrint4);
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint5 = new Vedomost_VB.OneGrafaToPrint()
    {
      _cell_ID = "Примечание " + number,
      _listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>()
    };
    oneGrafaToPrint5._listOneDataFieldToPrint.Add(new Vedomost_VB.OneDataFieldToPrint()
    {
      _symbolRazd = "",
      _typeField = Vedomost_VB.TypeField.ObjectType,
      _objectType = AvsIDCache.Attr_Note
    });
    soedAdditionalInit._listOneGrafaToPrint.Add(oneGrafaToPrint5);
    return soedAdditionalInit;
  }

  public static Vedomost_VB.Algorithm_Avs6_To_Ips Algorithm_Avs6_To_Ips_Tabl_Soed_Init()
  {
    return new Vedomost_VB.Algorithm_Avs6_To_Ips()
    {
      _tableName = "Главная таблица",
      _oneRecord_Avs6_To_Ips_Info = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_Soed_Info_Init(),
      _oneRecord_Avs6_To_Ips_Title = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_Soed_TitleX_Init('S'),
      _oneRecord_Avs6_To_Ips_TitleVar = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_Soed_TitleX_Init('V'),
      _oneRecord_Avs6_To_Ips_TitleIsp = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_Soed_TitleX_Init('N'),
      _oneRecord_Avs6_To_Ips_Remark = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_Soed_Remark_Init(),
      _oneRecord_Avs6_To_Ips_RemarkShort = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_Soed_RemarkShort_Init(),
      _oneRecord_Avs6_To_Ips_Additional1 = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_Soed_Additional_Init('X', "1"),
      _oneRecord_Avs6_To_Ips_Additional2 = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_Soed_Additional_Init('Y', "2"),
      _oneRecord_Avs6_To_Ips_Additional3 = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_Soed_Additional_Init('E', "3"),
      _oneRecord_Avs6_To_Ips_Additional4 = Tabl_Static.oneRecord_Avs6_To_Ips_Tabl_Soed_Additional_Init('F', "4")
    };
  }

  /// <summary> Информационная запись в Таблице соединений </summary>
  /// <returns></returns>
  public static Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs6_To_Ips_Tabl_Soed_Info_Init()
  {
    Vedomost_VB.OneRecord_Avs6_To_Ips tablSoedInfoInit = new Vedomost_VB.OneRecord_Avs6_To_Ips();
    tablSoedInfoInit._recordType_Avs6 = 'I';
    tablSoedInfoInit._nameTypeRec = "Info";
    tablSoedInfoInit._parentId = "";
    tablSoedInfoInit._tableRowId = "Основная строка";
    tablSoedInfoInit._listOneGrafa_Avs6_To_Ips = new List<Vedomost_VB.OneGrafa_Avs6_To_Ips>();
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps1 = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Обозначение провода",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps1._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 226
    });
    tablSoedInfoInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps1);
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps2 = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Откуда идет",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps2._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 219
    });
    tablSoedInfoInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps2);
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps3 = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Куда поступает",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps3._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 220
    });
    tablSoedInfoInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps3);
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps4 = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Данные провода",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps4._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 213
    });
    tablSoedInfoInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps4);
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps5 = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Примечание",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps5._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 7
    });
    tablSoedInfoInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps5);
    return tablSoedInfoInit;
  }

  public static Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs6_To_Ips_Tabl_Soed_TitleX_Init(
    char typeRec)
  {
    Vedomost_VB.OneRecord_Avs6_To_Ips tablSoedTitleXInit = new Vedomost_VB.OneRecord_Avs6_To_Ips();
    tablSoedTitleXInit._recordType_Avs6 = typeRec;
    tablSoedTitleXInit._nameTypeRec = "Title" + typeRec.ToString();
    tablSoedTitleXInit._parentId = "";
    tablSoedTitleXInit._tableRowId = "Заголовок";
    tablSoedTitleXInit._listOneGrafa_Avs6_To_Ips = new List<Vedomost_VB.OneGrafa_Avs6_To_Ips>();
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Текст заголовка",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 5
    });
    tablSoedTitleXInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps);
    return tablSoedTitleXInit;
  }

  public static Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs6_To_Ips_Tabl_Soed_Remark_Init()
  {
    Vedomost_VB.OneRecord_Avs6_To_Ips tablSoedRemarkInit = new Vedomost_VB.OneRecord_Avs6_To_Ips();
    tablSoedRemarkInit._recordType_Avs6 = 'R';
    tablSoedRemarkInit._nameTypeRec = "Remark";
    tablSoedRemarkInit._parentId = "";
    tablSoedRemarkInit._tableRowId = "Длинная строка";
    tablSoedRemarkInit._listOneGrafa_Avs6_To_Ips = new List<Vedomost_VB.OneGrafa_Avs6_To_Ips>();
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Текст",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 5
    });
    tablSoedRemarkInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps);
    return tablSoedRemarkInit;
  }

  public static Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs6_To_Ips_Tabl_Soed_RemarkShort_Init()
  {
    Vedomost_VB.OneRecord_Avs6_To_Ips soedRemarkShortInit = new Vedomost_VB.OneRecord_Avs6_To_Ips();
    soedRemarkShortInit._recordType_Avs6 = 'T';
    soedRemarkShortInit._nameTypeRec = "RemarkShort";
    soedRemarkShortInit._parentId = "";
    soedRemarkShortInit._tableRowId = "Длинная строка";
    soedRemarkShortInit._listOneGrafa_Avs6_To_Ips = new List<Vedomost_VB.OneGrafa_Avs6_To_Ips>();
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Текст",
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 5
    });
    soedRemarkShortInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps);
    return soedRemarkShortInit;
  }

  public static Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs6_To_Ips_Tabl_Soed_Additional_Init(
    char typeRec,
    string number)
  {
    Vedomost_VB.OneRecord_Avs6_To_Ips soedAdditionalInit = new Vedomost_VB.OneRecord_Avs6_To_Ips();
    soedAdditionalInit._recordType_Avs6 = typeRec;
    soedAdditionalInit._nameTypeRec = "Additional" + number.ToString();
    soedAdditionalInit._parentId = "";
    soedAdditionalInit._tableRowId = "Дополнительная " + number;
    soedAdditionalInit._listOneGrafa_Avs6_To_Ips = new List<Vedomost_VB.OneGrafa_Avs6_To_Ips>();
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips()
    {
      _cell_ID = "Обозначение провода " + number,
      _listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>()
    };
    oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips.Add(new Vedomost_VB.OneDataField_Avs6_To_Ips()
    {
      _symbolRazd = "",
      _objectType = 5
    });
    soedAdditionalInit._listOneGrafa_Avs6_To_Ips.Add(oneGrafaAvs6ToIps);
    return soedAdditionalInit;
  }
}
