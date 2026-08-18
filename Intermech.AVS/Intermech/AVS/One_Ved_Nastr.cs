// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.One_Ved_Nastr
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Victor;
using Intermech.Document.Client;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание для сбора одной ведомости </summary>
public class One_Ved_Nastr
{
  public string _nameVed;
  public Guid _guidTypeVed;
  public Guid _vedomostTemplateObjectGuid;
  public Guid _vedomostTemplateObjectGuid_B;
  public int _idTypeVed;
  public IMSObjectType _imsObjectType;
  public Vedomost_VB.TypeVed _typeVed;
  public Vedomost_VB.TypeDoc _typeDoc;
  public Guid _guidParent;
  public Vedomost_VB.TypeCreate _typeCreate;
  public XmlDocument _xmlDocument;
  public Vedomost_VB.Bases_Options_Ved _bases_Options_Ved;
  public Vedomost_VB.Dopoln_Options_Ved _dopoln_Options_Ved;
  public Vedomost_VB.Protection_From_Editing _protection_From_Editing;
  public List<Vedomost_VB.OneFieldSpForRead> _list_Ved_ID;
  public List<Vedomost_VB.Usl_Read_From_SP> _list_Usl_Read_From_SP;
  public List<Vedomost_VB.Usl_Read_From_SP> _list_Usl_Read_From_SP_Reference;
  public Vedomost_VB.Sbor_Options _sbor_Options;
  public Vedomost_VB.ESPD _espd;
  public List<Vedomost_VB.OneRazdelVed> _list_RazdelsVed;
  public Vedomost_VB.Zagolovki_Ved _zagolovki_Ved;
  public Vedomost_VB.Sorting_Usl _sorting_Usl;
  public Vedomost_VB.Sorting_Usl_Doc _sorting_Usl_Doc;
  public Vedomost_VB.Merge_Usl2 _merge_Usl2;
  public Vedomost_VB.AlgorithmToPrint _algorithmToPrint;
  public Vedomost_VB.AlgorithmToPrint _algorithmToPrint_B;
  public Vedomost_VB.AlgorithmXml _algorithmXml;
  public Vedomost_VB.Algorithm_Avs6_To_Ips _algorithm_Avs6_To_Ips;
  public Vedomost_VB.Algorithm_Avs6_To_Ips _algorithm_Avs6_To_Ips_B;
  public int _autoSbor = 1;
  public string _dateIni;
  public int _accessLevel = 3;
  public int _isCreateDumpAuto = 1;
  public TypeCreateNastr _typeCreateNastr;

  public One_Ved_Nastr()
  {
    this._bases_Options_Ved = new Vedomost_VB.Bases_Options_Ved();
    this._protection_From_Editing = new Vedomost_VB.Protection_From_Editing();
    this._dopoln_Options_Ved = new Vedomost_VB.Dopoln_Options_Ved();
    this._list_Ved_ID = new List<Vedomost_VB.OneFieldSpForRead>();
    this._list_Usl_Read_From_SP = new List<Vedomost_VB.Usl_Read_From_SP>();
    this._list_Usl_Read_From_SP_Reference = new List<Vedomost_VB.Usl_Read_From_SP>();
    this._sbor_Options = new Vedomost_VB.Sbor_Options();
    this._espd = new Vedomost_VB.ESPD();
    this._list_RazdelsVed = new List<Vedomost_VB.OneRazdelVed>();
    this._zagolovki_Ved = new Vedomost_VB.Zagolovki_Ved();
    this._zagolovki_Ved._list_One_Zagolovok = new List<Vedomost_VB.One_Zagolovok>();
    this._sorting_Usl = new Vedomost_VB.Sorting_Usl();
    this._algorithmToPrint = new Vedomost_VB.AlgorithmToPrint();
    this._algorithmToPrint_B = (Vedomost_VB.AlgorithmToPrint) null;
    this._algorithmXml = (Vedomost_VB.AlgorithmXml) null;
    this._algorithm_Avs6_To_Ips = (Vedomost_VB.Algorithm_Avs6_To_Ips) null;
    this._algorithm_Avs6_To_Ips_B = (Vedomost_VB.Algorithm_Avs6_To_Ips) null;
    this._typeCreate = Vedomost_VB.TypeCreate.User;
  }

  public One_Ved_Nastr(bool isEmpty, bool isKudaVhoditInfo, bool isItogoInfo = false)
  {
    this._list_Ved_ID = Vedomost_VB_Static.ListObligatoryId_Filled();
    this._list_RazdelsVed = Vedomost_VB_Static.List_Razdels_Ved_Based_Init();
    this._sorting_Usl = new Vedomost_VB.Sorting_Usl();
    this._sorting_Usl.Sorting_Usl_VedOsn = new Vedomost_VB.Sorting_Usl_One_From4();
    this._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel = new List<Vedomost_VB.Sorting_Usl_OneRazdel>();
    this._algorithmToPrint = Vedomost_VB_Static.AlgorithmToPrint_Based_Init(isKudaVhoditInfo, isItogoInfo);
    this._espd = Espd_Static.Espd_Init();
    this._bases_Options_Ved = new Vedomost_VB.Bases_Options_Ved();
    this._protection_From_Editing = new Vedomost_VB.Protection_From_Editing();
    this._dopoln_Options_Ved = new Vedomost_VB.Dopoln_Options_Ved();
    this._list_Usl_Read_From_SP = new List<Vedomost_VB.Usl_Read_From_SP>();
    this._list_Usl_Read_From_SP_Reference = new List<Vedomost_VB.Usl_Read_From_SP>();
    this._sbor_Options = new Vedomost_VB.Sbor_Options();
    this._espd = new Vedomost_VB.ESPD();
    this._zagolovki_Ved = new Vedomost_VB.Zagolovki_Ved();
    this._zagolovki_Ved._list_One_Zagolovok = new List<Vedomost_VB.One_Zagolovok>();
    this._algorithmToPrint_B = (Vedomost_VB.AlgorithmToPrint) null;
    this._algorithmXml = Vedomost_VB_Static.AlgorithmXml_Based_Init(isKudaVhoditInfo, isItogoInfo);
    this._algorithm_Avs6_To_Ips = (Vedomost_VB.Algorithm_Avs6_To_Ips) null;
    this._algorithm_Avs6_To_Ips_B = (Vedomost_VB.Algorithm_Avs6_To_Ips) null;
    this._typeCreate = Vedomost_VB.TypeCreate.User;
  }

  public One_Ved_Nastr(bool isEmpty)
  {
    this._list_Ved_ID = Tabl_Static.List_Id_Init();
    this._algorithmToPrint = Tabl_Static.AlgorithmToPrint_Based_Init();
    this._algorithmXml = Tabl_Static.AlgorithmXml_Tabl_Based_Init();
    this._espd = (Vedomost_VB.ESPD) null;
    this._bases_Options_Ved = Tabl_Static.Based_Options_Tabl_Init();
    this._algorithm_Avs6_To_Ips = (Vedomost_VB.Algorithm_Avs6_To_Ips) null;
    this._typeCreate = Vedomost_VB.TypeCreate.User;
  }

  /// <summary> Создается _xml_Document Внутри One_Ved_Nastr </summary>
  /// <returns></returns>
  public XmlDocument XmlDocument_create()
  {
    this._xmlDocument = new XmlDocument();
    this._xmlDocument.InsertBefore((XmlNode) this._xmlDocument.CreateXmlDeclaration("1.0", "windows-1251", "yes"), (XmlNode) this._xmlDocument.DocumentElement);
    XmlElement element = this._xmlDocument.CreateElement(string.Empty, "ONE_VED_NASTR", string.Empty);
    this._xmlDocument.AppendChild((XmlNode) element);
    if (this._imsObjectType != null)
    {
      if (this._nameVed != null)
      {
        XmlAttribute attribute = this._xmlDocument.CreateAttribute("nameVed");
        attribute.Value = this._nameVed.ToString();
        element.Attributes.Append(attribute);
      }
      XmlAttribute attribute1 = this._xmlDocument.CreateAttribute("guid");
      attribute1.Value = this._imsObjectType.Guid.ToString();
      element.Attributes.Append(attribute1);
      if (this._vedomostTemplateObjectGuid != Guid.Empty)
      {
        XmlAttribute attribute2 = this._xmlDocument.CreateAttribute("vedomostTemplateObjectGuid");
        attribute2.Value = this._vedomostTemplateObjectGuid.ToString();
        element.Attributes.Append(attribute2);
      }
      Guid templateObjectGuidB = this._vedomostTemplateObjectGuid_B;
      XmlAttribute attribute3 = this._xmlDocument.CreateAttribute("vedomostTemplateObjectGuid_B");
      attribute3.Value = this._vedomostTemplateObjectGuid_B.ToString();
      element.Attributes.Append(attribute3);
      XmlAttribute attribute4 = this._xmlDocument.CreateAttribute("objectTypeID");
      attribute4.Value = this._idTypeVed.ToString();
      element.Attributes.Append(attribute4);
      XmlAttribute attribute5 = this._xmlDocument.CreateAttribute("objectTypeName");
      attribute5.Value = this._nameVed;
      element.Attributes.Append(attribute5);
      XmlAttribute attribute6 = this._xmlDocument.CreateAttribute("typeVed");
      attribute6.Value = this._typeVed.ToString();
      element.Attributes.Append(attribute6);
      XmlAttribute attribute7 = this._xmlDocument.CreateAttribute("_typeDoc");
      attribute7.Value = this._typeDoc.ToString();
      element.Attributes.Append(attribute7);
      XmlAttribute attribute8 = this._xmlDocument.CreateAttribute("typeCreate");
      attribute8.Value = this._typeCreate.ToString();
      element.Attributes.Append(attribute8);
      XmlAttribute attribute9 = this._xmlDocument.CreateAttribute("guidParent");
      attribute9.Value = this._guidParent.ToString();
      element.Attributes.Append(attribute9);
      XmlAttribute attribute10 = this._xmlDocument.CreateAttribute("accessLevel");
      attribute10.Value = this._accessLevel.ToString();
      element.Attributes.Append(attribute10);
      XmlAttribute attribute11 = this._xmlDocument.CreateAttribute("autoSbor");
      attribute11.Value = this._autoSbor.ToString();
      element.Attributes.Append(attribute11);
      XmlAttribute attribute12 = this._xmlDocument.CreateAttribute("isCreateDumpAuto");
      attribute12.Value = this._isCreateDumpAuto.ToString();
      element.Attributes.Append(attribute12);
    }
    string str = DateTime.Now.ToString();
    XmlAttribute attribute13 = this._xmlDocument.CreateAttribute("dateIni");
    attribute13.Value = str;
    element.Attributes.Append(attribute13);
    XmlElement newChild1 = this.Xml_Sbor_Options_Ved(this._xmlDocument);
    if (newChild1 != null)
      element.AppendChild((XmlNode) newChild1);
    XmlElement newChild2 = this.Xml_Espd(this._xmlDocument);
    if (newChild2 != null)
      element.AppendChild((XmlNode) newChild2);
    if (this._bases_Options_Ved != null)
    {
      XmlElement newChild3 = this.Xml_Bases_Options_Ved(this._xmlDocument);
      if (newChild3 != null)
        element.AppendChild((XmlNode) newChild3);
    }
    else
    {
      this._bases_Options_Ved = new Vedomost_VB.Bases_Options_Ved();
      if (this._typeDoc == Vedomost_VB.TypeDoc.Ved || this._typeDoc == Vedomost_VB.TypeDoc.Undefined || this._typeDoc == Vedomost_VB.TypeDoc.Espd)
      {
        XmlAttribute attribute14 = this._xmlDocument.CreateAttribute("isReadOrInit_isMain");
        attribute14.Value = "True";
        element.Attributes.Append(attribute14);
        XmlAttribute attribute15 = this._xmlDocument.CreateAttribute("isMainSort1");
        attribute15.Value = this._bases_Options_Ved._isMainSort1.ToString();
        element.Attributes.Append(attribute15);
        XmlAttribute attribute16 = this._xmlDocument.CreateAttribute("isMainSort2");
        attribute16.Value = this._bases_Options_Ved._isMainSort2.ToString();
        element.Attributes.Append(attribute16);
        XmlAttribute attribute17 = this._xmlDocument.CreateAttribute("isMainSummOdinakovyh");
        attribute17.Value = this._bases_Options_Ved._isMainSummOdinakovyh.ToString();
        element.Attributes.Append(attribute17);
        XmlAttribute attribute18 = this._xmlDocument.CreateAttribute("isMainCreateVtorRecords");
        attribute18.Value = this._bases_Options_Ved._isMainCreateVtorRecords.ToString();
        element.Attributes.Append(attribute18);
        XmlAttribute attribute19 = this._xmlDocument.CreateAttribute("isMainSumm");
        attribute19.Value = this._bases_Options_Ved._isMainSumm.ToString();
        element.Attributes.Append(attribute19);
        XmlAttribute attribute20 = this._xmlDocument.CreateAttribute("isOnlyUroven1");
        attribute20.Value = this._bases_Options_Ved._isOnlyUroven1.ToString();
        element.Attributes.Append(attribute20);
        XmlAttribute attribute21 = this._xmlDocument.CreateAttribute("is_Specification_Instrument");
        attribute21.Value = this._bases_Options_Ved._is_Specification_Instrument.ToString();
        element.Attributes.Append(attribute21);
        XmlAttribute attribute22 = this._xmlDocument.CreateAttribute("isVedSortGroup");
        attribute22.Value = this._bases_Options_Ved._isVedSortGroup.ToString();
        element.Attributes.Append(attribute22);
        XmlAttribute attribute23 = this._xmlDocument.CreateAttribute("isVedMergerIsp");
        attribute23.Value = this._bases_Options_Ved._isVedMergerIsp.ToString();
        element.Attributes.Append(attribute23);
        XmlAttribute attribute24 = this._xmlDocument.CreateAttribute("isAddFuncGroup");
        attribute24.Value = this._bases_Options_Ved._isVedAddFuncGroup.ToString();
        element.Attributes.Append(attribute24);
        XmlAttribute attribute25 = this._xmlDocument.CreateAttribute("isVedSort1");
        attribute25.Value = this._bases_Options_Ved._isVedSort1.ToString();
        element.Attributes.Append(attribute25);
        XmlAttribute attribute26 = this._xmlDocument.CreateAttribute("isVedUnion");
        attribute26.Value = this._bases_Options_Ved._isVedUnion.ToString();
        element.Attributes.Append(attribute26);
        XmlAttribute attribute27 = this._xmlDocument.CreateAttribute("isVedExtrectionVtor");
        attribute27.Value = this._bases_Options_Ved._isVedExtrectionVtor.ToString();
        element.Attributes.Append(attribute27);
        XmlAttribute attribute28 = this._xmlDocument.CreateAttribute("isVedMergerVtor");
        attribute28.Value = this._bases_Options_Ved._isVedMergerVtor.ToString();
        element.Attributes.Append(attribute28);
        XmlAttribute attribute29 = this._xmlDocument.CreateAttribute("isVedSortVtor");
        attribute29.Value = this._bases_Options_Ved._isVedSortVtor.ToString();
        element.Attributes.Append(attribute29);
        XmlAttribute attribute30 = this._xmlDocument.CreateAttribute("isVedSummVtor");
        attribute30.Value = this._bases_Options_Ved._isVedSummVtor.ToString();
        element.Attributes.Append(attribute30);
        XmlAttribute attribute31 = this._xmlDocument.CreateAttribute("isVedCreateZagolIspoln");
        attribute31.Value = this._bases_Options_Ved._isVedCreateZagolIspoln.ToString();
        element.Attributes.Append(attribute31);
        XmlAttribute attribute32 = this._xmlDocument.CreateAttribute("isVedCreateZagolSvoiaVed");
        attribute32.Value = this._bases_Options_Ved._isVedCreateZagolSvoiaVed.ToString();
        element.Attributes.Append(attribute32);
        XmlAttribute attribute33 = this._xmlDocument.CreateAttribute("isVedCreateZagolPoPriznaku");
        attribute33.Value = this._bases_Options_Ved._isVedCreateZagolPoPriznaku.ToString();
        element.Attributes.Append(attribute33);
        XmlAttribute attribute34 = this._xmlDocument.CreateAttribute("is_Extended_List_Names");
        attribute34.Value = this._bases_Options_Ved._is_Extended_List_Names.ToString();
        element.Attributes.Append(attribute34);
        XmlAttribute attribute35 = this._xmlDocument.CreateAttribute("isVedAddToSp");
        attribute35.Value = this._bases_Options_Ved._isVedAddToSp.ToString();
        element.Attributes.Append(attribute35);
        XmlAttribute attribute36 = this._xmlDocument.CreateAttribute("isFor_ZIP_SB_Raskr");
        attribute36.Value = this._bases_Options_Ved._isFor_ZIP_SB_Raskr.ToString();
        element.Attributes.Append(attribute36);
        XmlAttribute attribute37 = this._xmlDocument.CreateAttribute("isFor_ZIP_SB_Add");
        attribute37.Value = this._bases_Options_Ved._isFor_ZIP_SB_Add.ToString();
        element.Attributes.Append(attribute37);
        XmlAttribute attribute38 = this._xmlDocument.CreateAttribute("isFor_ZIP_COMPL_Raskr");
        attribute38.Value = this._bases_Options_Ved._isFor_ZIP_COMPL_Raskr.ToString();
        element.Attributes.Append(attribute38);
        XmlAttribute attribute39 = this._xmlDocument.CreateAttribute("isFor_ZIP_COMPL_Add");
        attribute39.Value = this._bases_Options_Ved._isFor_ZIP_COMPL_Add.ToString();
        element.Attributes.Append(attribute39);
        XmlAttribute attribute40 = this._xmlDocument.CreateAttribute("isVedAddToRazdel");
        attribute40.Value = this._bases_Options_Ved._isVedAddToRazdel.ToString();
        element.Attributes.Append(attribute40);
        XmlAttribute attribute41 = this._xmlDocument.CreateAttribute("isInputDoc");
        attribute41.Value = this._bases_Options_Ved._isInputDoc.ToString();
        element.Attributes.Append(attribute41);
        XmlAttribute attribute42 = this._xmlDocument.CreateAttribute("isInputIzd");
        attribute42.Value = this._bases_Options_Ved._isInputIzd.ToString();
        element.Attributes.Append(attribute42);
        XmlAttribute attribute43 = this._xmlDocument.CreateAttribute("isInputMat");
        attribute43.Value = this._bases_Options_Ved._isInputMat.ToString();
        element.Attributes.Append(attribute43);
      }
      this._bases_Options_Ved._list_quickObjectInfo = new List<QuickObjectInfo>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._bases_Options_Ved._list_quickObjectInfo.Add(sessionKeeper.Session.GetObjectInfo(Vedomost_VB_Static.GuidImbaseConctructorsky));
        this._bases_Options_Ved._list_quickObjectInfo.Add(sessionKeeper.Session.GetObjectInfo(Vedomost_VB_Static.GuidImbaseMaterialy));
      }
      if (this._bases_Options_Ved._opеning_Sections != null)
      {
        XmlElement newChild4 = this.Xml_list_Opening_Sections(this._xmlDocument);
        if (newChild4 != null)
          element.AppendChild((XmlNode) newChild4);
      }
    }
    if (this._protection_From_Editing != null)
    {
      XmlElement newChild5 = this.Xml_Protection_From_Editing(this._xmlDocument);
      if (newChild5 != null)
        element.AppendChild((XmlNode) newChild5);
    }
    else
    {
      this._protection_From_Editing = new Vedomost_VB.Protection_From_Editing();
      XmlAttribute attribute44 = this._xmlDocument.CreateAttribute("isFullProhibition");
      attribute44.Value = "False";
      element.Attributes.Append(attribute44);
      XmlAttribute attribute45 = this._xmlDocument.CreateAttribute("isProhibition_DocRowWithObj");
      attribute45.Value = "False";
      element.Attributes.Append(attribute45);
      XmlAttribute attribute46 = this._xmlDocument.CreateAttribute("isProtectionCommand");
      attribute46.Value = "False";
      element.Attributes.Append(attribute46);
    }
    if (this._dopoln_Options_Ved != null)
    {
      XmlElement newChild6 = this.Xml_Dopoln_Options_Ved(this._xmlDocument);
      if (newChild6 != null)
        element.AppendChild((XmlNode) newChild6);
    }
    if (this._algorithmToPrint != null)
    {
      XmlElement print = this._algorithmToPrint.Xml_AlgorithmToPrint(this._xmlDocument, "ALGORITHMTOPRINT");
      if (print != null)
        element.AppendChild((XmlNode) print);
    }
    if (this._algorithm_Avs6_To_Ips != null)
    {
      XmlElement ips = this._algorithm_Avs6_To_Ips.Xml_Algorithm_Avs6_To_Ips(this._xmlDocument, "ALGORITHM_Avs6_To_Ips");
      if (ips != null)
        element.AppendChild((XmlNode) ips);
    }
    if ((this._typeDoc == Vedomost_VB.TypeDoc.Ved || this._typeDoc == Vedomost_VB.TypeDoc.Undefined || this._typeDoc == Vedomost_VB.TypeDoc.Espd) && this._algorithm_Avs6_To_Ips_B != null)
    {
      XmlElement ips = this._algorithm_Avs6_To_Ips_B.Xml_Algorithm_Avs6_To_Ips(this._xmlDocument, "ALGORITHM_Avs6_To_Ips_B");
      if (ips != null)
        element.AppendChild((XmlNode) ips);
    }
    if (this._algorithmXml != null)
    {
      XmlElement newChild7 = this._algorithmXml.Xml_AlgorithmXml(this._xmlDocument, "ALGORITHMXML");
      if (newChild7 != null)
        element.AppendChild((XmlNode) newChild7);
    }
    if ((this._typeDoc == Vedomost_VB.TypeDoc.Ved || this._typeDoc == Vedomost_VB.TypeDoc.Undefined || this._typeDoc == Vedomost_VB.TypeDoc.Espd) && this._algorithmToPrint_B != null)
    {
      XmlElement print = this._algorithmToPrint_B.Xml_AlgorithmToPrint(this._xmlDocument, "ALGORITHMTOPRINT_B");
      if (print != null)
        element.AppendChild((XmlNode) print);
    }
    if ((this._typeDoc == Vedomost_VB.TypeDoc.Ved || this._typeDoc == Vedomost_VB.TypeDoc.Undefined || this._typeDoc == Vedomost_VB.TypeDoc.Espd) && this._list_Usl_Read_From_SP != null)
    {
      XmlElement newChild8 = this.Xml_List_Usl_Read_From_SP(this._xmlDocument);
      if (newChild8 != null)
        element.AppendChild((XmlNode) newChild8);
    }
    if ((this._typeDoc == Vedomost_VB.TypeDoc.Ved || this._typeDoc == Vedomost_VB.TypeDoc.Undefined || this._typeDoc == Vedomost_VB.TypeDoc.Espd) && this._list_Usl_Read_From_SP_Reference != null)
    {
      XmlElement newChild9 = this.Xml_List_Usl_Read_From_SP_Reference(this._xmlDocument);
      if (newChild9 != null)
        element.AppendChild((XmlNode) newChild9);
    }
    if (this._list_Ved_ID != null)
    {
      XmlElement newChild10 = this.Xml_List_Ved_ID(this._xmlDocument);
      if (newChild10 != null)
        element.AppendChild((XmlNode) newChild10);
    }
    if ((this._typeDoc == Vedomost_VB.TypeDoc.Ved || this._typeDoc == Vedomost_VB.TypeDoc.Undefined || this._typeDoc == Vedomost_VB.TypeDoc.Espd) && this._list_RazdelsVed != null)
    {
      XmlElement newChild11 = this.Xml_List_RazdelsVed(this._xmlDocument);
      if (newChild11 != null)
        element.AppendChild((XmlNode) newChild11);
    }
    if ((this._typeDoc == Vedomost_VB.TypeDoc.Ved || this._typeDoc == Vedomost_VB.TypeDoc.Undefined || this._typeDoc == Vedomost_VB.TypeDoc.Espd) && this._zagolovki_Ved != null)
    {
      XmlElement newChild12 = this.Xml_Zagolovki_Ved(this._xmlDocument);
      if (newChild12 != null)
        element.AppendChild((XmlNode) newChild12);
    }
    if (this._typeDoc == Vedomost_VB.TypeDoc.Ved || this._typeDoc == Vedomost_VB.TypeDoc.Undefined || this._typeDoc == Vedomost_VB.TypeDoc.Espd)
    {
      if (this._sorting_Usl != null)
      {
        XmlElement newChild13 = this.Xml_Sorting_Usl(this._xmlDocument);
        if (newChild13 != null)
          element.AppendChild((XmlNode) newChild13);
      }
      if (this._sorting_Usl_Doc != null)
      {
        XmlElement newChild14 = this.Xml_Sorting_Usl_Doc(this._xmlDocument);
        if (newChild14 != null)
          element.AppendChild((XmlNode) newChild14);
      }
    }
    if (this._merge_Usl2 != null)
    {
      XmlElement newChild15 = this.Xml_Merge_Usl2(this._xmlDocument);
      if (newChild15 != null)
        element.AppendChild((XmlNode) newChild15);
    }
    return this._xmlDocument;
  }

  /// <summary> Из xmlDocument Заполняем файл настройки </summary>
  /// <param name="xmlDocument"></param>
  /// <returns></returns>
  public bool Filled_One_Ved_Nastr_FromXml(XmlDocument xmlDocument)
  {
    if (xmlDocument == null || xmlDocument.DocumentElement.Name.ToUpper() != "ONE_VED_NASTR")
      return false;
    if (this._sbor_Options == null)
      this._sbor_Options = new Vedomost_VB.Sbor_Options();
    if (this._espd == null)
      this._espd = new Vedomost_VB.ESPD();
    for (int i = 0; i < xmlDocument.DocumentElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlDocument.DocumentElement.Attributes[i];
      string name = attribute.Name;
      attribute.Value.ToString();
    }
    for (int i = 0; i < xmlDocument.DocumentElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlDocument.DocumentElement.Attributes[i];
      string name = attribute.Name;
      string g = attribute.Value.ToString();
      switch (name)
      {
        case "nameVed":
          this._nameVed = g;
          break;
        case "guid":
          this._guidTypeVed = new Guid(g);
          break;
        case "vedomostTemplateObjectGuid":
          this._vedomostTemplateObjectGuid = new Guid(g);
          Guid fromImDocSettings = DocumentEditorPlugin.GetDocumentTemplateIDFromIMDocSettings(this._guidTypeVed);
          if (fromImDocSettings != Guid.Empty && fromImDocSettings != this._vedomostTemplateObjectGuid)
          {
            this._vedomostTemplateObjectGuid = fromImDocSettings;
            break;
          }
          break;
        case "vedomostTemplateObjectGuid_B":
          this._vedomostTemplateObjectGuid_B = new Guid(g);
          break;
        case "objectTypeID":
          this._idTypeVed = Convert.ToInt32(g);
          break;
        case "accessLevel":
          this._accessLevel = Convert.ToInt32(g);
          break;
        case "autoSbor":
          this._autoSbor = Convert.ToInt32(g);
          break;
        case "isCreateDumpAuto":
          this._isCreateDumpAuto = Convert.ToInt32(g);
          break;
        case "typeVed":
          if (g == "Undefined")
            this._typeVed = Vedomost_VB.TypeVed.Undefined;
          if (g == "VS")
            this._typeVed = Vedomost_VB.TypeVed.VS;
          if (g == "VP")
            this._typeVed = Vedomost_VB.TypeVed.VP;
          if (g == "RS")
            this._typeVed = Vedomost_VB.TypeVed.RS;
          if (g == "VD")
            this._typeVed = Vedomost_VB.TypeVed.VD;
          if (g == "VDE")
            this._typeVed = Vedomost_VB.TypeVed.VDE;
          if (g == "DP")
            this._typeVed = Vedomost_VB.TypeVed.DP;
          if (g == "DPE")
            this._typeVed = Vedomost_VB.TypeVed.DPE;
          if (g == "VSI")
            this._typeVed = Vedomost_VB.TypeVed.VSI;
          if (g == "VM")
            this._typeVed = Vedomost_VB.TypeVed.VM;
          if (g == "VR")
            this._typeVed = Vedomost_VB.TypeVed.VR;
          if (g == "VDZ")
            this._typeVed = Vedomost_VB.TypeVed.VDZ;
          if (g == "ZI")
            this._typeVed = Vedomost_VB.TypeVed.ZI;
          if (g == "ED")
            this._typeVed = Vedomost_VB.TypeVed.ED;
          if (g == "TABL")
            this._typeVed = Vedomost_VB.TypeVed.TABL;
          if (g == "TABLSOED")
            this._typeVed = Vedomost_VB.TypeVed.TABLSOED;
          if (g == "TABLSOEDSZ")
            this._typeVed = Vedomost_VB.TypeVed.TABLSOEDSZ;
          if (g == "ESPD" || this._nameVed == "Программная спецификация")
          {
            this._typeVed = Vedomost_VB.TypeVed.ESPD;
            this._typeCreate = Vedomost_VB.TypeCreate.System;
          }
          if (g == "Others")
          {
            this._typeVed = Vedomost_VB.TypeVed.Others;
            break;
          }
          break;
        case "_typeDoc":
          if (g == "Undefined")
            this._typeDoc = Vedomost_VB.TypeDoc.Undefined;
          if (g == "Ved")
            this._typeDoc = Vedomost_VB.TypeDoc.Ved;
          if (g == "Tabl")
            this._typeDoc = Vedomost_VB.TypeDoc.Tabl;
          if (g == "Espd" || this._nameVed == "Программная спецификация")
          {
            this._typeDoc = Vedomost_VB.TypeDoc.Espd;
            break;
          }
          break;
        default:
          if (name == "typeCreate" && this._typeCreate != Vedomost_VB.TypeCreate.System)
          {
            if (g == "Undefined")
              this._typeCreate = Vedomost_VB.TypeCreate.Undefined;
            if (g == "System")
              this._typeCreate = Vedomost_VB.TypeCreate.System;
            if (g == "User")
            {
              this._typeCreate = Vedomost_VB.TypeCreate.User;
              break;
            }
            break;
          }
          if (name == "guidParent")
          {
            this._guidParent = new Guid(g);
            break;
          }
          if (name == "is_Vydeliat_Sami_Komplekty")
          {
            this._sbor_Options._is_Vydeliat_Sami_Komplekty = g == "True";
            break;
          }
          if (name == "is_Vydeliat_Therez_Komplekty")
          {
            this._sbor_Options._is_Vydeliat_Therez_Komplekty = g == "True";
            break;
          }
          if (name == "isSamuSP_ne_iz_spiska_zanosit")
          {
            this._sbor_Options._isSamuSP_ne_iz_spiska_zanosit = g == "True";
            break;
          }
          if (name == "isReference_Show")
          {
            this._sbor_Options._isReference_Show = g == "True";
            break;
          }
          if (name == "isRaskrSP_s_takoi_Ved")
          {
            this._sbor_Options._isRaskrSP_s_takoi_Ved = Convert.ToInt32(g);
            break;
          }
          if (name == "isDopZam")
          {
            this._sbor_Options._isDopZam = Convert.ToInt32(g);
            break;
          }
          if (name == "isAllocateDopZam")
          {
            this._sbor_Options._isAllocateDopZam = Convert.ToInt32(g);
            break;
          }
          if (name == "dateIni")
            this._dateIni = g;
          if (name == "isReadOrInit_isMain")
          {
            if (this._bases_Options_Ved == null)
              this._bases_Options_Ved = new Vedomost_VB.Bases_Options_Ved();
            this._bases_Options_Ved._isReadOrInit_isMain = g == "True";
            break;
          }
          if (name == "isMainSort1")
          {
            this._bases_Options_Ved._isMainSort1 = g == "True";
            break;
          }
          if (name == "isMainSort2")
          {
            this._bases_Options_Ved._isMainSort2 = g == "True";
            break;
          }
          if (name == "isMainSummOdinakovyh")
          {
            this._bases_Options_Ved._isMainSummOdinakovyh = g == "True";
            break;
          }
          if (name == "isMainCreateVtorRecords")
          {
            this._bases_Options_Ved._isMainCreateVtorRecords = g == "True";
            break;
          }
          if (name == "isMainSumm")
          {
            this._bases_Options_Ved._isMainSumm = g == "True";
            break;
          }
          if (name == "isOnlyUroven1")
          {
            this._bases_Options_Ved._isOnlyUroven1 = g == "True";
            break;
          }
          if (name == "is_Specification_Instrument")
          {
            this._bases_Options_Ved._is_Specification_Instrument = g == "True";
            break;
          }
          if (name == "isVedSortGroup")
          {
            this._bases_Options_Ved._isVedSortGroup = g == "True";
            break;
          }
          if (name == "isVedMergerIsp")
          {
            this._bases_Options_Ved._isVedMergerIsp = g == "True";
            break;
          }
          if (name == "isAddFuncGroup")
          {
            this._bases_Options_Ved._isVedAddFuncGroup = g == "True";
            break;
          }
          if (name == "isVedSort1")
          {
            this._bases_Options_Ved._isVedSort1 = g == "True";
            break;
          }
          if (name == "isVedUnion")
          {
            this._bases_Options_Ved._isVedUnion = g == "True";
            break;
          }
          if (name == "isVedExtrectionVtor")
          {
            this._bases_Options_Ved._isVedExtrectionVtor = g == "True";
            break;
          }
          if (name == "isVedMergerVtor")
          {
            this._bases_Options_Ved._isVedMergerVtor = g == "True";
            break;
          }
          if (name == "isVedSortVtor")
          {
            this._bases_Options_Ved._isVedSortVtor = g == "True";
            break;
          }
          if (name == "isVedSummVtor")
          {
            this._bases_Options_Ved._isVedSummVtor = g == "True";
            break;
          }
          if (name == "isVedCreateZagolIspoln")
          {
            this._bases_Options_Ved._isVedCreateZagolIspoln = g == "True";
            break;
          }
          if (name == "isVedCreateZagolSvoiaVed")
          {
            this._bases_Options_Ved._isVedCreateZagolSvoiaVed = g == "True";
            break;
          }
          if (name == "isVedCreateZagolPoPriznaku")
          {
            this._bases_Options_Ved._isVedCreateZagolPoPriznaku = g == "True";
            break;
          }
          if (name == "is_Extended_List_Names")
          {
            this._bases_Options_Ved._is_Extended_List_Names = g == "True";
            break;
          }
          if (name == "isVedAddToSp")
          {
            this._bases_Options_Ved._isVedAddToSp = g == "True";
            break;
          }
          if (name == "isFor_ZIP_SB_Raskr")
          {
            this._bases_Options_Ved._isFor_ZIP_SB_Raskr = g == "True";
            break;
          }
          if (name == "isFor_ZIP_SB_Add")
          {
            this._bases_Options_Ved._isFor_ZIP_SB_Add = g == "True";
            break;
          }
          if (name == "isFor_ZIP_COMPL_Raskr")
          {
            this._bases_Options_Ved._isFor_ZIP_COMPL_Raskr = g == "True";
            break;
          }
          if (name == "isFor_ZIP_COMPL_Add")
          {
            this._bases_Options_Ved._isFor_ZIP_COMPL_Add = g == "True";
            break;
          }
          if (name == "isVedAddToRazdel")
            this._bases_Options_Ved._isVedAddToRazdel = Convert.ToInt32(g);
          if (name == "isInputDoc")
          {
            this._bases_Options_Ved._isInputDoc = g == "True";
            break;
          }
          if (name == "isInputIzd")
          {
            this._bases_Options_Ved._isInputIzd = g == "True";
            break;
          }
          if (name == "isInputMat")
          {
            this._bases_Options_Ved._isInputMat = g == "True";
            break;
          }
          break;
      }
    }
    foreach (XmlElement childNode in xmlDocument.DocumentElement.ChildNodes)
    {
      string name = childNode.Name;
      if (childNode.Name.ToUpper() == "SBOR_OPTIONS_VED")
        this._sbor_Options = this.Sbor_Options_Ved_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "ESPD")
        this._espd = this.Espd_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "BASES_OPTIONS_VED")
        this._bases_Options_Ved = this.Bases_Options_Ved_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "PROTECTION_FROM_EDITING")
        this._protection_From_Editing = this.Protection_From_Editing_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "DOPOLN_OPTIONS_VED")
        this._dopoln_Options_Ved = this.Dopoln_Options_Ved_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "ALGORITHMTOPRINT")
        this._algorithmToPrint = this.AlgorithmToPrint_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "ALGORITHMTOPRINT_B")
        this._algorithmToPrint_B = this.AlgorithmToPrint_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "ALGORITHM_AVS6_TO_IPS")
        this._algorithm_Avs6_To_Ips = this.Algorithm_Avs6_To_Ips_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "ALGORITHM_AVS6_TO_IPS_B")
        this._algorithm_Avs6_To_Ips_B = this.Algorithm_Avs6_To_Ips_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "LIST_USL_READ_FROM_SP")
        this._list_Usl_Read_From_SP = this.List_Usl_Read_From_SP_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "LIST_USL_READ_FROM_SP_REFERENCE")
        this._list_Usl_Read_From_SP_Reference = this.List_Usl_Read_From_SP_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "LIST_VED_ID")
        this._list_Ved_ID = this.List_Ved_ID_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "LIST_RAZDELS_VED")
        this._list_RazdelsVed = this.List_RazdelsVed_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "ZAGOLOVKI_VED")
        this._zagolovki_Ved = this.Zagolovki_Ved_ReadFromXml(childNode);
      if (childNode.Name.ToUpper() == "SORTING_USL")
        this._sorting_Usl = this.Sorting_Usl_ReafFromXml((XmlNode) childNode);
      if (childNode.Name.ToUpper() == "SORTING_USL_DOC")
        this._sorting_Usl_Doc = this.Sorting_Usl_Doc_ReafFromXml((XmlNode) childNode);
      if (childNode.Name.ToUpper() == "MERGE_USL2")
        this._merge_Usl2 = this.Merge_Usl2_ReafFromXml((XmlNode) childNode);
      if (childNode.Name.ToUpper() == "ALGORITHMXML")
        this._algorithmXml = this.AlgorithmXml_ReadFromXml(childNode);
    }
    if (string.IsNullOrEmpty(this._nameVed))
      this._nameVed = Vedomost_VB_Static.TypeVed_string(this._typeVed);
    if (string.IsNullOrEmpty(this._nameVed))
      this._nameVed = this._imsObjectType.ObjectName;
    this._xmlDocument = (XmlDocument) null;
    this.XmlDocument_create();
    this._typeCreateNastr = TypeCreateNastr.Read;
    return true;
  }

  /// <summary> На основе прочтенного XML сформировать Vedomost_VB.AlgorithmToPrint </summary>
  /// <param _nameTypeRec="algorithmXml"></param>
  /// <returns></returns>
  public Vedomost_VB.AlgorithmToPrint AlgorithmToPrint_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (Vedomost_VB.AlgorithmToPrint) null;
    Vedomost_VB.AlgorithmToPrint printReadFromXml = new Vedomost_VB.AlgorithmToPrint();
    for (int i = 0; i < xmlElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlElement.Attributes[i];
      string name = attribute.Name;
      string s = attribute.Value.ToString();
      if (name == "TABLENAME")
        printReadFromXml._tableName = s;
      if (name == "kolGraf")
        printReadFromXml._kolGraf = int.Parse(s);
      if (name == "iLIZM")
        printReadFromXml._iLIZM = int.Parse(s);
      if (name == "includedLizmInDoc")
        printReadFromXml._includedLizmInDoc = int.Parse(s);
      if (name == "afterInfo")
        printReadFromXml._afterInfo = int.Parse(s);
      if (name == "afterRemark")
        printReadFromXml._afterRemark = int.Parse(s);
      if (name == "additional1")
        printReadFromXml._additional1 = !(s == "1") ? 0 : int.Parse(s);
      if (name == "additional2")
        printReadFromXml._additional2 = !(s == "1") ? 0 : int.Parse(s);
      if (name == "additional3")
        printReadFromXml._additional3 = !(s == "1") ? 0 : int.Parse(s);
      if (name == "additional4")
        printReadFromXml._additional4 = !(s == "1") ? 0 : int.Parse(s);
      if (name == "isDeleteIdenticalTexts")
        printReadFromXml._isDeleteIdenticalTexts = s == "True";
      if (name == "isCheck")
        printReadFromXml._isCheck = s == "True";
      if (name == "isUnbrokenDefis")
        printReadFromXml._isUnbrokenDefis = s == "True";
    }
    foreach (XmlElement childNode1 in xmlElement.ChildNodes)
    {
      string name1 = childNode1.Name;
      if (name1 == "ONERECORDTOPRINT")
      {
        Vedomost_VB.OneRecordToPrint printFromXml = this.OneRecordToPrintFromXml((XmlNode) childNode1);
        if (printFromXml._nameTypeRec == "oneRecordToPrintTitleSection")
          printFromXml._nameTypeRec = "oneRecordToPrintTitle";
        if (printFromXml._nameTypeRec == "oneRecordToPrintInfo")
          printReadFromXml._oneRecordToPrint_Info = printFromXml;
        if (printFromXml._nameTypeRec == "oneRecordToPrintTitleIncluded")
          printReadFromXml._oneRecordToPrintTitleIncluded = printFromXml;
        if (printFromXml._nameTypeRec == "oneRecordToPrintIncluded")
          printReadFromXml._oneRecordToPrintIncluded = printFromXml;
        if (printFromXml._nameTypeRec == "oneRecordToPrintTitleVar")
          printReadFromXml._oneRecordToPrintTitleVar = printFromXml;
        if (printFromXml._nameTypeRec == "oneRecordToPrintTitleIsp")
          printReadFromXml._oneRecordToPrintTitleIsp = printFromXml;
        if (printFromXml._nameTypeRec == "oneRecordToPrintTitle")
          printReadFromXml._oneRecordToPrintTitle = printFromXml;
        if (printFromXml._nameTypeRec == "oneRecordToPrintTitlePodSection")
          printReadFromXml._oneRecordToPrintTitlePodSection = printFromXml;
        if (printFromXml._nameTypeRec == "oneRecordToPrintRemark")
          printReadFromXml._oneRecordToPrintRemark = printFromXml;
        if (printFromXml._nameTypeRec == "oneRecordToPrintRemarkShort")
          printReadFromXml._oneRecordToPrintRemarkShort = printFromXml;
        if (printFromXml._nameTypeRec == "oneRecordToPrintPasport")
          printReadFromXml._oneRecordToPrintPasport = printFromXml;
        if (printFromXml._nameTypeRec == "oneRecordToPrintEmpty")
          printReadFromXml._oneRecordToPrintEmpty = printFromXml;
        if (printFromXml._nameTypeRec == "oneRecordToPrintTitlePart")
          printReadFromXml._oneRecordToPrintTitlePart = printFromXml;
        if (printFromXml._nameTypeRec == "Additional1")
          printReadFromXml._oneRecordToPrintAdditional1 = printFromXml;
        if (printFromXml._nameTypeRec == "Additional2")
          printReadFromXml._oneRecordToPrintAdditional2 = printFromXml;
        if (printFromXml._nameTypeRec == "Additional3")
          printReadFromXml._oneRecordToPrintAdditional3 = printFromXml;
        if (printFromXml._nameTypeRec == "Additional4")
          printReadFromXml._oneRecordToPrintAdditional4 = printFromXml;
      }
      else
      {
        if (name1 == "List_OneRazdelToPrint")
        {
          printReadFromXml._list_OneRazdelToPrint = new List<Vedomost_VB.OneRazdelToPrint>();
          foreach (XmlElement childNode2 in childNode1.ChildNodes)
          {
            Vedomost_VB.OneRazdelToPrint oneRazdelToPrint = new Vedomost_VB.OneRazdelToPrint();
            for (int i = 0; i < childNode2.Attributes.Count; ++i)
            {
              XmlAttribute attribute = childNode2.Attributes[i];
              name1 = attribute.Name;
              string str = attribute.Value.ToString();
              if (name1 == "RazdelVed")
                oneRazdelToPrint._razdelVed = Convert.ToInt32(str);
              if (name1 == "NamePage_First")
                oneRazdelToPrint._namePage_First = str;
              if (name1 == "NamePage_Next")
                oneRazdelToPrint._namePage_Next = str;
            }
            foreach (XmlElement childNode3 in childNode2.ChildNodes)
            {
              if (childNode3.Name == "ONERECORDTOPRINT")
              {
                Vedomost_VB.OneRecordToPrint printFromXml = this.OneRecordToPrintFromXml((XmlNode) childNode3);
                if (printFromXml._nameTypeRec == "oneRecordToPrintInfo")
                  oneRazdelToPrint._oneRecordToPrint_Info = printFromXml;
              }
            }
            printReadFromXml._list_OneRazdelToPrint.Add(oneRazdelToPrint);
          }
        }
        if (name1 == "List_OneRazdelToPrintAdditional")
        {
          printReadFromXml._list_OneRazdelToPrintAdditional = new List<Vedomost_VB.OneRazdelToPrintAdditional>();
          foreach (XmlElement childNode4 in childNode1.ChildNodes)
          {
            Vedomost_VB.OneRazdelToPrintAdditional toPrintAdditional = new Vedomost_VB.OneRazdelToPrintAdditional();
            for (int i = 0; i < childNode4.Attributes.Count; ++i)
            {
              XmlAttribute attribute = childNode4.Attributes[i];
              string name2 = attribute.Name;
              string str = attribute.Value.ToString();
              if (name2 == "RazdelVed")
                toPrintAdditional._razdelVed = Convert.ToInt32(str);
            }
            foreach (XmlElement childNode5 in childNode4.ChildNodes)
            {
              if (childNode5.Name == "ONERECORDTOPRINT")
              {
                Vedomost_VB.OneRecordToPrint additionalFromXml = this.OneRecordToPrintAdditionalFromXml((XmlNode) childNode5);
                if (additionalFromXml != null)
                {
                  switch (additionalFromXml._nameTypeRec)
                  {
                    case "Additional1":
                      toPrintAdditional._oneRecordToPrint_Additional1 = additionalFromXml;
                      continue;
                    case "Additional2":
                      toPrintAdditional._oneRecordToPrint_Additional2 = additionalFromXml;
                      continue;
                    case "Additional3":
                      toPrintAdditional._oneRecordToPrint_Additional3 = additionalFromXml;
                      continue;
                    case "Additional4":
                      toPrintAdditional._oneRecordToPrint_Additional4 = additionalFromXml;
                      continue;
                    default:
                      continue;
                  }
                }
              }
            }
            printReadFromXml._list_OneRazdelToPrintAdditional.Add(toPrintAdditional);
          }
        }
      }
    }
    return printReadFromXml;
  }

  public Vedomost_VB.OneRecordToPrint OneRecordToPrintFromXml(XmlNode XmlTableOneRecordToPrint)
  {
    if (XmlTableOneRecordToPrint == null)
      return (Vedomost_VB.OneRecordToPrint) null;
    Vedomost_VB.OneRecordToPrint printFromXml1 = new Vedomost_VB.OneRecordToPrint();
    for (int i = 0; i < XmlTableOneRecordToPrint.Attributes.Count; ++i)
    {
      XmlAttribute attribute = XmlTableOneRecordToPrint.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "NAME")
        printFromXml1._nameTypeRec = str;
      if (name == "TABLEROWID")
        printFromXml1._tableRowId = str;
      if (name == "ISVTOROBLAST")
        printFromXml1._isVtorOblast = str == "True";
      if (name == "TABLEVTOROBLASTID")
        printFromXml1._tableVtorOblastId = str;
      if (name == "PARENTID")
        printFromXml1._parentId = str;
    }
    foreach (XmlNode childNode1 in XmlTableOneRecordToPrint.ChildNodes)
    {
      if (childNode1.Name == "LISTONEGRAFATOPRINT")
      {
        printFromXml1._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
        for (int i = 0; i < childNode1.ChildNodes.Count; ++i)
        {
          XmlNode childNode2 = childNode1.ChildNodes[i];
          if (childNode2.Name == "oneGrafaToPrint")
          {
            Vedomost_VB.OneGrafaToPrint printFromXml2 = this.OneGrafaToPrintFromXml(childNode2);
            if (printFromXml2 != null)
              printFromXml1._listOneGrafaToPrint.Add(printFromXml2);
          }
        }
      }
      else if (childNode1.Name == "ONERECORDTOPRINT")
      {
        Vedomost_VB.OneRecordToPrint printFromXml3 = this.OneRecordToPrintFromXml(childNode1);
        if (printFromXml3 != null)
        {
          if (printFromXml3._nameTypeRec == "oneRecordToPrintVtor")
            printFromXml1._oneRecordToPrint_Vtor = printFromXml3;
          if (printFromXml3._nameTypeRec == "oneRecordToPrintItogo")
            printFromXml1._oneRecordToPrint_Itogo = printFromXml3;
        }
      }
    }
    return printFromXml1;
  }

  public Vedomost_VB.OneRecordToPrint OneRecordToPrintAdditionalFromXml(
    XmlNode XmlTableOneRecordToPrint)
  {
    if (XmlTableOneRecordToPrint == null)
      return (Vedomost_VB.OneRecordToPrint) null;
    Vedomost_VB.OneRecordToPrint additionalFromXml = new Vedomost_VB.OneRecordToPrint();
    for (int i = 0; i < XmlTableOneRecordToPrint.Attributes.Count; ++i)
    {
      XmlAttribute attribute = XmlTableOneRecordToPrint.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "NAME")
        additionalFromXml._nameTypeRec = str;
      if (name == "TABLEROWID")
        additionalFromXml._tableRowId = str;
      int num = name == "ADDITIONAL" ? 1 : 0;
      if (name == "ISVTOROBLAST")
        additionalFromXml._isVtorOblast = str == "True";
      if (name == "TABLEVTOROBLASTID")
        additionalFromXml._tableVtorOblastId = str;
      if (name == "PARENTID")
        additionalFromXml._parentId = str;
    }
    foreach (XmlNode childNode1 in XmlTableOneRecordToPrint.ChildNodes)
    {
      if (childNode1.Name == "LISTONEGRAFATOPRINT")
      {
        additionalFromXml._listOneGrafaToPrint = new List<Vedomost_VB.OneGrafaToPrint>();
        for (int i = 0; i < childNode1.ChildNodes.Count; ++i)
        {
          XmlNode childNode2 = childNode1.ChildNodes[i];
          if (childNode2.Name == "oneGrafaToPrint")
          {
            Vedomost_VB.OneGrafaToPrint printFromXml = this.OneGrafaToPrintFromXml(childNode2);
            if (printFromXml != null)
              additionalFromXml._listOneGrafaToPrint.Add(printFromXml);
          }
        }
      }
      else if (childNode1.Name == "ONERECORDTOPRINT")
      {
        Vedomost_VB.OneRecordToPrint printFromXml = this.OneRecordToPrintFromXml(childNode1);
        if (printFromXml != null)
        {
          if (printFromXml._nameTypeRec == "oneRecordToPrintVtor")
            additionalFromXml._oneRecordToPrint_Vtor = printFromXml;
          if (printFromXml._nameTypeRec == "oneRecordToPrintItogo")
            additionalFromXml._oneRecordToPrint_Itogo = printFromXml;
        }
      }
    }
    return additionalFromXml;
  }

  public Vedomost_VB.OneGrafaToPrint OneGrafaToPrintFromXml(XmlNode XmlTableOneGrafaToPrint)
  {
    if (XmlTableOneGrafaToPrint == null)
      return (Vedomost_VB.OneGrafaToPrint) null;
    Vedomost_VB.OneGrafaToPrint printFromXml = new Vedomost_VB.OneGrafaToPrint();
    for (int i = 0; i < XmlTableOneGrafaToPrint.Attributes.Count; ++i)
    {
      XmlAttribute attribute = XmlTableOneGrafaToPrint.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "cellNumber")
        printFromXml._cell_ID = str;
    }
    foreach (XmlNode childNode1 in XmlTableOneGrafaToPrint.ChildNodes)
    {
      if (childNode1.Name == "listOneDataFieldToPrint")
      {
        printFromXml._listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>();
        for (int i1 = 0; i1 < childNode1.ChildNodes.Count; ++i1)
        {
          XmlNode childNode2 = childNode1.ChildNodes[i1];
          if (childNode2.Name == "oneDataFieldToPrint")
          {
            Vedomost_VB.OneDataFieldToPrint dataFieldToPrint = new Vedomost_VB.OneDataFieldToPrint();
            for (int i2 = 0; i2 < childNode2.Attributes.Count; ++i2)
            {
              XmlAttribute attribute = childNode2.Attributes[i2];
              string name = attribute.Name;
              string s = attribute.Value;
              switch (name)
              {
                case "symbolRazd":
                  dataFieldToPrint._symbolRazd = s;
                  break;
                case "typeField":
                  if (s == "ObjectType")
                    dataFieldToPrint._typeField = Vedomost_VB.TypeField.ObjectType;
                  if (s == "TypeFieldVedRec")
                    dataFieldToPrint._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
                  if (s == "TypeFieldVedPasport")
                  {
                    dataFieldToPrint._typeField = Vedomost_VB.TypeField.TypeFieldVedPasport;
                    break;
                  }
                  break;
                case "objectType":
                  int num1 = int.Parse(s);
                  if (this._typeVed == Vedomost_VB.TypeVed.DP || this._typeVed == Vedomost_VB.TypeVed.DPE || this._typeVed == Vedomost_VB.TypeVed.VD || this._typeVed == Vedomost_VB.TypeVed.VDE)
                  {
                    if (num1 == 19335)
                      num1 = AvsIDCache.Attr_DerzPodl;
                    if (num1 == 19238)
                      num1 = AvsIDCache.Attr_TypeNTD;
                  }
                  dataFieldToPrint._objectType = num1;
                  break;
                case "typeFieldVedRec":
                  int num2 = int.Parse(s);
                  dataFieldToPrint._typeFieldVedRec = (Vedomost_VB.TypeFieldVedRec) num2;
                  break;
                case "typeFieldVedPasport":
                  int num3 = int.Parse(s);
                  dataFieldToPrint._typeFieldVedPasport = (Vedomost_VB.TypeFieldVedPasport) num3;
                  break;
              }
            }
            printFromXml._listOneDataFieldToPrint.Add(dataFieldToPrint);
          }
        }
      }
    }
    return printFromXml;
  }

  /// <summary> На основе прочтенного XML сформировать Vedomost_VB.Algorithm_Avs6_To_Ips </summary>
  /// <param _nameTypeRec="algorithmXml"></param>
  /// <returns></returns>
  public Vedomost_VB.Algorithm_Avs6_To_Ips Algorithm_Avs6_To_Ips_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (Vedomost_VB.Algorithm_Avs6_To_Ips) null;
    Vedomost_VB.Algorithm_Avs6_To_Ips ipsReadFromXml = new Vedomost_VB.Algorithm_Avs6_To_Ips();
    for (int i = 0; i < xmlElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlElement.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "TABLENAME")
        ipsReadFromXml._tableName = str;
    }
    foreach (XmlElement childNode1 in xmlElement.ChildNodes)
    {
      switch (childNode1.Name)
      {
        case "ONERECORD_Avs6_To_Ips":
          Vedomost_VB.OneRecord_Avs6_To_Ips ipsFromXml1 = this.OneRecord_Avs6_To_IpsFromXml((XmlNode) childNode1);
          if (ipsFromXml1._nameTypeRec == "oneRecord_Avs6_To_IpsTitleSection")
            ipsFromXml1._nameTypeRec = "oneRecord_Avs6_To_IpsTitle";
          if (ipsFromXml1._nameTypeRec == "Info")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_Info = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "TitleC")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_TitleIncluded = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "Included")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_Included = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "TitleV")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_TitleVar = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "TitleN")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_TitleIsp = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "TitleS")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_Title = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "Remark")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_Remark = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "RemarkShort")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_RemarkShort = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "Pasport")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_Pasport = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "Empty")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_Empty = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "TitleP")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_TitlePart = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "Additional1")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_Additional1 = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "Additional2")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_Additional2 = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "Additional3")
            ipsReadFromXml._oneRecord_Avs6_To_Ips_Additional3 = ipsFromXml1;
          if (ipsFromXml1._nameTypeRec == "Additional4")
          {
            ipsReadFromXml._oneRecord_Avs6_To_Ips_Additional4 = ipsFromXml1;
            continue;
          }
          continue;
        case "List_OneRazdel_Avs6_To_Ips":
          ipsReadFromXml._list_OneRazdel_Avs6_To_Ips = new List<Vedomost_VB.OneRazdel_Avs6_To_Ips>();
          IEnumerator enumerator = childNode1.ChildNodes.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              XmlElement current = (XmlElement) enumerator.Current;
              Vedomost_VB.OneRazdel_Avs6_To_Ips oneRazdelAvs6ToIps = new Vedomost_VB.OneRazdel_Avs6_To_Ips();
              for (int i = 0; i < current.Attributes.Count; ++i)
              {
                XmlAttribute attribute = current.Attributes[i];
                string name = attribute.Name;
                string str = attribute.Value.ToString();
                if (name == "RazdelVed")
                  oneRazdelAvs6ToIps._razdelVed = Convert.ToInt32(str);
              }
              foreach (XmlElement childNode2 in current.ChildNodes)
              {
                if (childNode2.Name == "ONERECORD_Avs6_To_Ips")
                {
                  Vedomost_VB.OneRecord_Avs6_To_Ips ipsFromXml2 = this.OneRecord_Avs6_To_IpsFromXml((XmlNode) childNode2);
                  if (ipsFromXml2._nameTypeRec == "oneRecord_Avs6_To_IpsInfo")
                    oneRazdelAvs6ToIps._oneRecord_Avs6_To_Ips_Info = ipsFromXml2;
                }
              }
              ipsReadFromXml._list_OneRazdel_Avs6_To_Ips.Add(oneRazdelAvs6ToIps);
            }
            continue;
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        default:
          continue;
      }
    }
    return ipsReadFromXml;
  }

  public Vedomost_VB.OneRecord_Avs6_To_Ips OneRecord_Avs6_To_IpsFromXml(
    XmlNode XmlTableOneRecord_Avs6_To_Ips)
  {
    if (XmlTableOneRecord_Avs6_To_Ips == null)
      return (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    Vedomost_VB.OneRecord_Avs6_To_Ips ipsFromXml1 = new Vedomost_VB.OneRecord_Avs6_To_Ips();
    for (int i = 0; i < XmlTableOneRecord_Avs6_To_Ips.Attributes.Count; ++i)
    {
      XmlAttribute attribute = XmlTableOneRecord_Avs6_To_Ips.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "NAME")
        ipsFromXml1._nameTypeRec = str;
      if (name == "RECORDTYPE_AVS6")
        ipsFromXml1._recordType_Avs6 = str[0];
      if (name == "TABLEROWID")
        ipsFromXml1._tableRowId = str;
      if (name == "ISVTOROBLAST")
        ipsFromXml1._isVtorOblast = str == "True";
      if (name == "TABLEVTOROBLASTID")
        ipsFromXml1._tableVtorOblastId = str;
      if (name == "PARENTID")
        ipsFromXml1._parentId = str;
    }
    foreach (XmlNode childNode1 in XmlTableOneRecord_Avs6_To_Ips.ChildNodes)
    {
      if (childNode1.Name == "LISTONEGRAFA_Avs6_To_Ips")
      {
        ipsFromXml1._listOneGrafa_Avs6_To_Ips = new List<Vedomost_VB.OneGrafa_Avs6_To_Ips>();
        for (int i = 0; i < childNode1.ChildNodes.Count; ++i)
        {
          XmlNode childNode2 = childNode1.ChildNodes[i];
          if (childNode2.Name == "oneGrafa_Avs6_To_Ips")
          {
            Vedomost_VB.OneGrafa_Avs6_To_Ips ipsFromXml2 = this.OneGrafa_Avs6_To_Ips_FromXml(childNode2);
            if (ipsFromXml2 != null)
              ipsFromXml1._listOneGrafa_Avs6_To_Ips.Add(ipsFromXml2);
          }
        }
      }
      else if (childNode1.Name == "ONERECORD_Avs6_To_Ips")
      {
        Vedomost_VB.OneRecord_Avs6_To_Ips ipsFromXml3 = this.OneRecord_Avs6_To_IpsFromXml(childNode1);
        if (ipsFromXml3 != null)
        {
          if (ipsFromXml3._nameTypeRec == "InfoVtor" || ipsFromXml3._nameTypeRec == "Vtor")
            ipsFromXml1._oneRecord_Avs6_To_Ips_Vtor = ipsFromXml3;
          if (ipsFromXml3._nameTypeRec == "InfoItogo" || ipsFromXml3._nameTypeRec == "Itogo")
            ipsFromXml1._oneRecord_Avs6_To_Ips_Itogo = ipsFromXml3;
        }
      }
    }
    return ipsFromXml1;
  }

  public Vedomost_VB.OneGrafa_Avs6_To_Ips OneGrafa_Avs6_To_Ips_FromXml(
    XmlNode XmlTableOneGrafa_Avs6_To_Ips)
  {
    if (XmlTableOneGrafa_Avs6_To_Ips == null)
      return (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    Vedomost_VB.OneGrafa_Avs6_To_Ips ipsFromXml = new Vedomost_VB.OneGrafa_Avs6_To_Ips();
    for (int i = 0; i < XmlTableOneGrafa_Avs6_To_Ips.Attributes.Count; ++i)
    {
      XmlAttribute attribute = XmlTableOneGrafa_Avs6_To_Ips.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "cellNumber")
        ipsFromXml._cell_ID = str;
    }
    foreach (XmlNode childNode1 in XmlTableOneGrafa_Avs6_To_Ips.ChildNodes)
    {
      if (childNode1.Name == "listOneDataField_Avs6_To_Ips")
      {
        ipsFromXml._listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>();
        for (int i1 = 0; i1 < childNode1.ChildNodes.Count; ++i1)
        {
          XmlNode childNode2 = childNode1.ChildNodes[i1];
          if (childNode2.Name == "oneDataField_Avs6_To_Ips")
          {
            Vedomost_VB.OneDataField_Avs6_To_Ips dataFieldAvs6ToIps = new Vedomost_VB.OneDataField_Avs6_To_Ips();
            for (int i2 = 0; i2 < childNode2.Attributes.Count; ++i2)
            {
              XmlAttribute attribute = childNode2.Attributes[i2];
              string name = attribute.Name;
              string s = attribute.Value;
              switch (name)
              {
                case "symbolRazd":
                  dataFieldAvs6ToIps._symbolRazd = s;
                  break;
                case "objectType":
                  int num = int.Parse(s);
                  if (this._typeVed == Vedomost_VB.TypeVed.DP || this._typeVed == Vedomost_VB.TypeVed.DPE || this._typeVed == Vedomost_VB.TypeVed.VD || this._typeVed == Vedomost_VB.TypeVed.VDE)
                  {
                    if (num == 19335)
                      num = AvsIDCache.Attr_DerzPodl;
                    if (num == 19238)
                      num = AvsIDCache.Attr_TypeNTD;
                  }
                  dataFieldAvs6ToIps._objectType = num;
                  break;
              }
            }
            ipsFromXml._listOneDataField_Avs6_To_Ips.Add(dataFieldAvs6ToIps);
          }
        }
      }
    }
    return ipsFromXml;
  }

  public Vedomost_VB.AlgorithmXml AlgorithmXml_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (Vedomost_VB.AlgorithmXml) null;
    Vedomost_VB.AlgorithmXml algorithmXml = new Vedomost_VB.AlgorithmXml();
    for (int i = 0; i < xmlElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlElement.Attributes[i];
      if (!string.IsNullOrEmpty(attribute.Name) && !string.IsNullOrEmpty(attribute.Value))
      {
        string name = attribute.Name;
        string s = attribute.Value;
        if (name == "afterInfo")
          algorithmXml._afterInfo = int.Parse(s);
        if (name == "afterRemark")
          algorithmXml._afterRemark = int.Parse(s);
        if (name == "passportOut")
          algorithmXml._passportOut = int.Parse(s);
        if (name == "passportIn")
          algorithmXml._passportIn = int.Parse(s);
        if (name == "folderXmlIn")
          algorithmXml._folderXmlIn = s;
      }
    }
    foreach (XmlElement childNode in xmlElement.ChildNodes)
    {
      Vedomost_VB.OneRecordXml oneRecordXml = this.OneRecordXmlFromXml(childNode);
      switch (childNode.GetAttribute("NAME"))
      {
        case "oneRecordXmlPasport":
          algorithmXml._oneRecordXmlPasport = oneRecordXml;
          continue;
        case "oneRecordXml_Info":
          algorithmXml._oneRecordXml_Info = oneRecordXml;
          continue;
        case "oneRecordXmlIncluded":
          algorithmXml._oneRecordXmlIncluded = oneRecordXml;
          continue;
        case "oneRecordXmlTitleIncluded":
          algorithmXml._oneRecordXmlTitleIncluded = oneRecordXml;
          continue;
        case "oneRecordXmlTitleVar":
          algorithmXml._oneRecordXmlTitleVar = oneRecordXml;
          continue;
        case "oneRecordXmlTitleIsp":
          algorithmXml._oneRecordXmlTitleIsp = oneRecordXml;
          continue;
        case "oneRecordXmlTitle":
          algorithmXml._oneRecordXmlTitle = oneRecordXml;
          continue;
        case "oneRecordXmlTitlePodSection":
          algorithmXml._oneRecordXmlTitlePodSection = oneRecordXml;
          continue;
        case "oneRecordXmlRemark":
          algorithmXml._oneRecordXmlRemark = oneRecordXml;
          continue;
        case "oneRecordXmlTitlePart":
          algorithmXml._oneRecordXmlTitlePart = oneRecordXml;
          continue;
        case "oneRecordXmlAdditional1":
          algorithmXml._oneRecordXmlAdditional1 = oneRecordXml;
          continue;
        case "oneRecordXmlAdditional2":
          algorithmXml._oneRecordXmlAdditional2 = oneRecordXml;
          continue;
        case "oneRecordXmlAdditional3":
          algorithmXml._oneRecordXmlAdditional3 = oneRecordXml;
          continue;
        case "oneRecordXmlAdditional4":
          algorithmXml._oneRecordXmlAdditional4 = oneRecordXml;
          continue;
        case "oneRecordXmlEmpty":
          algorithmXml._oneRecordXmlEmpty = oneRecordXml;
          continue;
        default:
          continue;
      }
    }
    return algorithmXml;
  }

  private Vedomost_VB.OneRecordXml OneRecordXmlFromXml(XmlElement xml_OneRecordXml)
  {
    if (xml_OneRecordXml == null)
      return (Vedomost_VB.OneRecordXml) null;
    Vedomost_VB.OneRecordXml oneRecordXml1 = new Vedomost_VB.OneRecordXml();
    for (int i = 0; i < xml_OneRecordXml.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xml_OneRecordXml.Attributes[i];
      if (!string.IsNullOrEmpty(attribute.Name) && !string.IsNullOrEmpty(attribute.Value))
      {
        string name = attribute.Name;
        string str = attribute.Value;
        if (name == "nameTypeRec")
          oneRecordXml1._nameTypeRec = str;
        if (name == "tableRowId")
          oneRecordXml1._tableRowId = str;
      }
    }
    foreach (XmlElement childNode in xml_OneRecordXml.ChildNodes)
    {
      if (childNode.Name == "LISTONEFIELDXML")
      {
        List<Vedomost_VB.OneFieldXml> oneFieldXmlList = this.ListOneFieldXml(childNode);
        if (oneFieldXmlList != null)
          oneRecordXml1._listOneFieldXml = oneFieldXmlList;
      }
      else
      {
        switch (childNode.GetAttribute("NAME"))
        {
          case "oneRecordXml_Vtor":
            Vedomost_VB.OneRecordXml oneRecordXml2 = this.OneRecordXmlFromXml(childNode);
            if (oneRecordXml2 != null)
            {
              oneRecordXml1._oneRecordXml_Vtor = oneRecordXml2;
              continue;
            }
            continue;
          case "oneRecordXml_Itogo":
            Vedomost_VB.OneRecordXml oneRecordXml3 = this.OneRecordXmlFromXml(childNode);
            if (oneRecordXml3 != null)
            {
              oneRecordXml1._oneRecordXml_Itogo = oneRecordXml3;
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    return oneRecordXml1;
  }

  private List<Vedomost_VB.OneFieldXml> ListOneFieldXml(XmlElement xmlElement2)
  {
    if (xmlElement2 == null)
      return (List<Vedomost_VB.OneFieldXml>) null;
    List<Vedomost_VB.OneFieldXml> oneFieldXmlList = new List<Vedomost_VB.OneFieldXml>();
    foreach (XmlElement childNode in xmlElement2.ChildNodes)
    {
      Vedomost_VB.OneFieldXml oneFieldXml = new Vedomost_VB.OneFieldXml();
      oneFieldXml._nameToXml = childNode.GetAttribute("nameToXml");
      oneFieldXml._nameToFile = childNode.GetAttribute("nameToFile");
      string attribute = childNode.GetAttribute("typeDataToXml");
      if (attribute == "Field")
        oneFieldXml._typeDataToXml = Vedomost_VB.TypeDataToXml.Field;
      if (attribute == "Attribute")
        oneFieldXml._typeDataToXml = Vedomost_VB.TypeDataToXml.Attribute;
      if (!string.IsNullOrEmpty(oneFieldXml._nameToXml) && !string.IsNullOrEmpty(oneFieldXml._nameToFile))
        oneFieldXmlList.Add(oneFieldXml);
    }
    return oneFieldXmlList;
  }

  public void OneDataFieldToZagol_FromXml(
    XmlNode XmlOneDataFieldToZagol,
    Vedomost_VB.Zagolovki_Ved zagolovki_Ved)
  {
    if (XmlOneDataFieldToZagol == null)
      return;
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint();
    for (int i = 0; i < XmlOneDataFieldToZagol.Attributes.Count; ++i)
    {
      XmlAttribute attribute = XmlOneDataFieldToZagol.Attributes[i];
      string name = attribute.Name;
      string s = attribute.Value.ToString();
      switch (name)
      {
        case "typeField":
          if (s == "ObjectType")
            zagolovki_Ved._typeField = Vedomost_VB.TypeField.ObjectType;
          if (s == "TypeFieldVedRec")
            zagolovki_Ved._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
          if (s == "TypeFieldVedPasport")
          {
            zagolovki_Ved._typeField = Vedomost_VB.TypeField.TypeFieldVedPasport;
            break;
          }
          break;
        case "objectType":
          int num1 = int.Parse(s);
          zagolovki_Ved._objectType = num1;
          break;
        case "typeFieldVedRec":
          int num2 = int.Parse(s);
          zagolovki_Ved._typeFieldVedRec = (Vedomost_VB.TypeFieldVedRec) num2;
          break;
        case "vyvodit_PodZagolovki":
          zagolovki_Ved._vyvodit_PodZagolovki = s == "True";
          break;
        case "userZagolovki":
          zagolovki_Ved._userZagolovki = s == "True";
          break;
        case "locationZagolovki":
          zagolovki_Ved._locationZagolovki = s == "True";
          break;
        case "typeCompare":
          zagolovki_Ved._typeCompare = !(s == "Symbol") ? Vedomost_VB.TypeCompare.Int : Vedomost_VB.TypeCompare.Symbol;
          break;
        case "include_Name":
          zagolovki_Ved._include_Name = s;
          break;
      }
    }
  }

  /// <summary> Вывод в XML условий ввода </summary>
  /// <param name="xmlDocument"></param>
  /// <returns></returns>
  public XmlElement Xml_List_Usl_Read_From_SP(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "LIST_USL_READ_FROM_SP", string.Empty);
    for (int index = 0; index < this._list_Usl_Read_From_SP.Count; ++index)
    {
      Vedomost_VB.Usl_Read_From_SP usl_Read_From_SP = this._list_Usl_Read_From_SP[index];
      XmlElement newChild = this.Xml_Usl_Read_From_SP(xmlDocument, usl_Read_From_SP);
      if (newChild != null)
        element.AppendChild((XmlNode) newChild);
    }
    return element;
  }

  /// <summary> Вывод в XML условий ввода ссылок </summary>
  /// <param name="xmlDocument"></param>
  /// <returns></returns>
  public XmlElement Xml_List_Usl_Read_From_SP_Reference(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "LIST_USL_READ_FROM_SP_REFERENCE", string.Empty);
    for (int index = 0; index < this._list_Usl_Read_From_SP_Reference.Count; ++index)
    {
      Vedomost_VB.Usl_Read_From_SP usl_Read_From_SP = this._list_Usl_Read_From_SP_Reference[index];
      XmlElement newChild = this.Xml_Usl_Read_From_SP(xmlDocument, usl_Read_From_SP);
      if (newChild != null)
        element.AppendChild((XmlNode) newChild);
    }
    return element;
  }

  /// <summary> Список условий для ОДНОГО раздела СП </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="usl_Read_From_SP"></param>
  /// <returns></returns>
  public XmlElement Xml_Usl_Read_From_SP(
    XmlDocument xmlDocument,
    Vedomost_VB.Usl_Read_From_SP usl_Read_From_SP)
  {
    if (usl_Read_From_SP == null)
      return (XmlElement) null;
    XmlElement element1 = xmlDocument.CreateElement(string.Empty, nameof (usl_Read_From_SP), string.Empty);
    XmlAttribute attribute = xmlDocument.CreateAttribute("section");
    attribute.Value = usl_Read_From_SP._section_SP.ToString();
    element1.Attributes.Append(attribute);
    XmlElement element2 = xmlDocument.CreateElement(string.Empty, "List_Usl_Read_From_SP_One", string.Empty);
    element1.AppendChild((XmlNode) element2);
    for (int index = 0; index < usl_Read_From_SP._list_Usl_Read_From_SP_One.Count; ++index)
    {
      Vedomost_VB.Usl_Read_From_SP_One usl_Read_From_SP_One = usl_Read_From_SP._list_Usl_Read_From_SP_One[index];
      XmlElement newChild = this.Xml_Usl_Read_From_SP_One(xmlDocument, usl_Read_From_SP_One);
      if (newChild != null)
        element2.AppendChild((XmlNode) newChild);
    }
    return element1;
  }

  /// <summary> Берем одно поле и сравниваем его с каким-то текстом </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="usl_Read_From_SP_One"></param>
  /// <returns></returns>
  public XmlElement Xml_Usl_Read_From_SP_One(
    XmlDocument xmlDocument,
    Vedomost_VB.Usl_Read_From_SP_One usl_Read_From_SP_One)
  {
    if (usl_Read_From_SP_One == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, nameof (usl_Read_From_SP_One), string.Empty);
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("uslovie");
    attribute1.Value = usl_Read_From_SP_One._uslovie.ToString();
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("text");
    attribute2.Value = usl_Read_From_SP_One._text.ToString();
    element.Attributes.Append(attribute2);
    XmlAttribute attribute3 = xmlDocument.CreateAttribute("or_and");
    attribute3.Value = usl_Read_From_SP_One._or_and.ToString();
    element.Attributes.Append(attribute3);
    XmlElement newChild = this.Xml_OneFieldSpForRead(xmlDocument, usl_Read_From_SP_One._oneFieldSpForRead);
    if (newChild != null)
      element.AppendChild((XmlNode) newChild);
    return element;
  }

  /// <summary> Описание поля для чтения из спецификации </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="oneFieldSpForRead"></param>
  /// <returns></returns>
  public XmlElement Xml_OneFieldSpForRead(
    XmlDocument xmlDocument,
    Vedomost_VB.OneFieldSpForRead oneFieldSpForRead)
  {
    if (oneFieldSpForRead == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, nameof (oneFieldSpForRead), string.Empty);
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("guid");
    attribute1.Value = oneFieldSpForRead._guid.ToString();
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("name");
    attribute2.Value = oneFieldSpForRead._name.ToString();
    element.Attributes.Append(attribute2);
    XmlAttribute attribute3 = xmlDocument.CreateAttribute("perv_vtor");
    attribute3.Value = oneFieldSpForRead._perv_Vtor.ToString();
    element.Attributes.Append(attribute3);
    XmlAttribute attribute4 = xmlDocument.CreateAttribute("attributeSourceTypes");
    int attributeSourceTypes = (int) oneFieldSpForRead._attributeSourceTypes;
    attribute4.Value = attributeSourceTypes.ToString();
    element.Attributes.Append(attribute4);
    XmlAttribute attribute5 = xmlDocument.CreateAttribute("type");
    int type = (int) oneFieldSpForRead._type;
    attribute5.Value = type.ToString();
    element.Attributes.Append(attribute5);
    XmlAttribute attribute6 = xmlDocument.CreateAttribute("id");
    attribute6.Value = oneFieldSpForRead._id.ToString();
    element.Attributes.Append(attribute6);
    return element;
  }

  /// <summary> Описание раздела </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="oneFieldSpForRead"></param>
  /// <returns></returns>
  public XmlElement Xml_OneRazdelVed(XmlDocument xmlDocument, Vedomost_VB.OneRazdelVed oneRazdelVed)
  {
    if (oneRazdelVed == null)
      return (XmlElement) null;
    XmlElement element1 = xmlDocument.CreateElement(string.Empty, nameof (oneRazdelVed), string.Empty);
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("razdelVed");
    attribute1.Value = oneRazdelVed._razdelVed.ToString();
    element1.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("name");
    attribute2.Value = oneRazdelVed._name.ToString();
    element1.Attributes.Append(attribute2);
    XmlAttribute attribute3 = xmlDocument.CreateAttribute("caption");
    if (oneRazdelVed._caption != null)
      attribute3.Value = oneRazdelVed._caption.ToString();
    element1.Attributes.Append(attribute3);
    XmlAttribute attribute4 = xmlDocument.CreateAttribute("namePage");
    if (oneRazdelVed._namePage != null)
      attribute4.Value = oneRazdelVed._namePage.ToString();
    element1.Attributes.Append(attribute4);
    if (oneRazdelVed._list_onePodRazdels != null && oneRazdelVed._list_onePodRazdels.Count > 0)
    {
      XmlElement element2 = xmlDocument.CreateElement(string.Empty, "List_onePodRazdels", string.Empty);
      element1.AppendChild((XmlNode) element2);
      for (int index = 0; index < oneRazdelVed._list_onePodRazdels.Count; ++index)
      {
        Vedomost_VB.OnePodRazdelVed listOnePodRazdel = oneRazdelVed._list_onePodRazdels[index];
        XmlElement newChild = this.Xml_OnePodRazdelVed(xmlDocument, listOnePodRazdel);
        if (newChild != null)
          element2.AppendChild((XmlNode) newChild);
      }
    }
    return element1;
  }

  /// <summary> Один подраздел </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="onePodRazdelVed"></param>
  /// <returns></returns>
  public XmlElement Xml_OnePodRazdelVed(
    XmlDocument xmlDocument,
    Vedomost_VB.OnePodRazdelVed onePodRazdelVed)
  {
    if (onePodRazdelVed == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, nameof (onePodRazdelVed), string.Empty);
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("PodRazdelVed");
    attribute1.Value = onePodRazdelVed._podRazdelVed.ToString();
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("name");
    attribute2.Value = onePodRazdelVed._name.ToString();
    element.Attributes.Append(attribute2);
    return element;
  }

  public List<Vedomost_VB.Usl_Read_From_SP> List_Usl_Read_From_SP_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (List<Vedomost_VB.Usl_Read_From_SP>) null;
    List<Vedomost_VB.Usl_Read_From_SP> uslReadFromSpList = new List<Vedomost_VB.Usl_Read_From_SP>();
    foreach (XmlElement childNode in xmlElement.ChildNodes)
    {
      Vedomost_VB.Usl_Read_From_SP uslReadFromSp = this.Usl_Read_From_SP_ReadFromXml(childNode);
      if (uslReadFromSp != null)
        uslReadFromSpList.Add(uslReadFromSp);
    }
    return uslReadFromSpList;
  }

  public Vedomost_VB.Usl_Read_From_SP Usl_Read_From_SP_ReadFromXml(XmlElement XmlUsl_Read_From_SP)
  {
    if (XmlUsl_Read_From_SP == null)
      return (Vedomost_VB.Usl_Read_From_SP) null;
    Vedomost_VB.Usl_Read_From_SP uslReadFromSp = new Vedomost_VB.Usl_Read_From_SP();
    uslReadFromSp._list_Usl_Read_From_SP_One = new List<Vedomost_VB.Usl_Read_From_SP_One>();
    for (int i = 0; i < XmlUsl_Read_From_SP.Attributes.Count; ++i)
    {
      XmlAttribute attribute = XmlUsl_Read_From_SP.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "section")
        uslReadFromSp._section_SP = str;
    }
    for (int i1 = 0; i1 < XmlUsl_Read_From_SP.ChildNodes.Count; ++i1)
    {
      XmlNode childNode = XmlUsl_Read_From_SP.ChildNodes[i1];
      if (childNode.Name == "List_Usl_Read_From_SP_One")
      {
        uslReadFromSp._list_Usl_Read_From_SP_One = new List<Vedomost_VB.Usl_Read_From_SP_One>();
        for (int i2 = 0; i2 < childNode.ChildNodes.Count; ++i2)
        {
          Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne = this.Usl_Read_From_SP_One_ReafFromXml(childNode.ChildNodes[i2]);
          if (uslReadFromSpOne != null)
            uslReadFromSp._list_Usl_Read_From_SP_One.Add(uslReadFromSpOne);
        }
      }
    }
    return uslReadFromSp;
  }

  public Vedomost_VB.Usl_Read_From_SP_One Usl_Read_From_SP_One_ReafFromXml(
    XmlNode Xml_usl_Read_From_SP_One)
  {
    if (Xml_usl_Read_From_SP_One == null)
      return (Vedomost_VB.Usl_Read_From_SP_One) null;
    Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne = new Vedomost_VB.Usl_Read_From_SP_One();
    for (int i = 0; i < Xml_usl_Read_From_SP_One.Attributes.Count; ++i)
    {
      XmlAttribute attribute = Xml_usl_Read_From_SP_One.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "uslovie")
        uslReadFromSpOne._uslovie = str;
      if (name == "text")
        uslReadFromSpOne._text = str;
      if (name == "or_and")
        uslReadFromSpOne._or_and = str == "True";
    }
    Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this.OneFieldSpForRead_ReadFromXml(Xml_usl_Read_From_SP_One.ChildNodes[0]);
    uslReadFromSpOne._oneFieldSpForRead = oneFieldSpForRead;
    return uslReadFromSpOne;
  }

  public Vedomost_VB.OneFieldSpForRead OneFieldSpForRead_ReadFromXml(XmlNode xmlNode)
  {
    if (xmlNode == null)
      return (Vedomost_VB.OneFieldSpForRead) null;
    AttributeSourceTypes attr = AttributeSourceTypes.Object;
    Vedomost_VB.TypeDataSel attrType = Vedomost_VB.TypeDataSel.Int;
    int id = 0;
    string name1 = "";
    Guid guid = Guid.Empty;
    for (int i = 0; i < xmlNode.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlNode.Attributes[i];
      string name2 = attribute.Name;
      string g = attribute.Value.ToString();
      if (name2 == "guid")
        guid = new Guid(g);
      if (name2 == "name")
        name1 = g;
      if (name2 == "perv_vtor")
        Convert.ToInt32(g);
      if (name2 == "attributeSourceTypes")
        attr = (AttributeSourceTypes) Convert.ToInt32(g);
      if (name2 == "type")
      {
        switch (Convert.ToInt32(g))
        {
          case 0:
          case 1:
          case 2:
          case 3:
          case 4:
          case 5:
          case 6:
          case 7:
          case 8:
            attrType = (Vedomost_VB.TypeDataSel) Convert.ToInt32(g);
            break;
          default:
            attrType = Vedomost_VB.TypeDataSel.Int;
            break;
        }
      }
      if (name2 == "id")
      {
        id = Convert.ToInt32(g);
        if (this._typeVed == Vedomost_VB.TypeVed.DP || this._typeVed == Vedomost_VB.TypeVed.DPE || this._typeVed == Vedomost_VB.TypeVed.VD || this._typeVed == Vedomost_VB.TypeVed.VDE)
        {
          if (id == 19335)
            id = AvsIDCache.Attr_DerzPodl;
          if (id == 19238)
            id = AvsIDCache.Attr_TypeNTD;
        }
      }
    }
    return new Vedomost_VB.OneFieldSpForRead(id, guid, name1, attr, attrType);
  }

  /// <summary> Вывод в XML Списка вводимых из СП полей </summary>
  /// <param name="xmlDocument"></param>
  /// <returns></returns>
  public XmlElement Xml_List_Ved_ID(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "LIST_VED_ID", string.Empty);
    for (int index = 0; index < this._list_Ved_ID.Count; ++index)
    {
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this._list_Ved_ID[index];
      XmlElement newChild = this.Xml_OneFieldSpForRead(xmlDocument, oneFieldSpForRead);
      if (newChild != null)
        element.AppendChild((XmlNode) newChild);
    }
    return element;
  }

  /// <summary> Ввод списка разделов </summary>
  /// <param name="xmlElement"></param>
  /// <returns></returns>
  public List<Vedomost_VB.OneRazdelVed> List_RazdelsVed_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (List<Vedomost_VB.OneRazdelVed>) null;
    List<Vedomost_VB.OneRazdelVed> oneRazdelVedList = new List<Vedomost_VB.OneRazdelVed>();
    foreach (XmlNode childNode in xmlElement.ChildNodes)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed = this.OneRazdelVed_ReadFromXml(childNode);
      if (oneRazdelVed != null)
        oneRazdelVedList.Add(oneRazdelVed);
    }
    if (oneRazdelVedList.Count == 0)
      oneRazdelVedList.Add(new Vedomost_VB.OneRazdelVed()
      {
        _name = "Общий",
        _razdelVed = 1
      });
    return oneRazdelVedList;
  }

  /// <summary> Ввод описания одного раздела </summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public Vedomost_VB.OneRazdelVed OneRazdelVed_ReadFromXml(XmlNode xmlNode)
  {
    if (xmlNode == null)
      return (Vedomost_VB.OneRazdelVed) null;
    int num = 0;
    string str1 = "";
    string str2 = "";
    string str3 = "";
    for (int i = 0; i < xmlNode.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlNode.Attributes[i];
      string name = attribute.Name;
      string str4 = attribute.Value.ToString();
      if (name == "name")
        str1 = str4;
      if (name == "razdelVed")
        num = Convert.ToInt32(str4);
      if (name == "caption")
        str2 = str4;
      if (name == "namePage")
        str3 = str4;
    }
    Vedomost_VB.OneRazdelVed oneRazdelVed = new Vedomost_VB.OneRazdelVed();
    oneRazdelVed._razdelVed = num;
    oneRazdelVed._name = str1;
    oneRazdelVed._caption = str2;
    oneRazdelVed._namePage = str3;
    for (int i = 0; i < xmlNode.ChildNodes.Count; ++i)
    {
      if (oneRazdelVed._list_onePodRazdels == null)
        oneRazdelVed._list_onePodRazdels = new List<Vedomost_VB.OnePodRazdelVed>();
      XmlElement childNode = (XmlElement) xmlNode.ChildNodes[i];
      if (childNode.Name == "List_onePodRazdels")
      {
        List<Vedomost_VB.OnePodRazdelVed> onePodRazdelVedList = this.List_PodRazdelsVed_ReadFromXml(childNode);
        if (onePodRazdelVedList != null && onePodRazdelVedList.Count > 0)
          oneRazdelVed._list_onePodRazdels = onePodRazdelVedList;
      }
    }
    return oneRazdelVed;
  }

  /// <summary> Ввод списка подразделов </summary>
  /// <param name="xmlElement"></param>
  /// <returns></returns>
  public List<Vedomost_VB.OnePodRazdelVed> List_PodRazdelsVed_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (List<Vedomost_VB.OnePodRazdelVed>) null;
    List<Vedomost_VB.OnePodRazdelVed> onePodRazdelVedList = new List<Vedomost_VB.OnePodRazdelVed>();
    foreach (XmlNode childNode in xmlElement.ChildNodes)
    {
      Vedomost_VB.OnePodRazdelVed onePodRazdelVed = this.OnePodRazdelVed_ReadFromXml(childNode);
      if (onePodRazdelVed != null)
        onePodRazdelVedList.Add(onePodRazdelVed);
    }
    return onePodRazdelVedList;
  }

  /// <summary> Ввод описания одного ПОДраздела </summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public Vedomost_VB.OnePodRazdelVed OnePodRazdelVed_ReadFromXml(XmlNode xmlNode)
  {
    if (xmlNode == null)
      return (Vedomost_VB.OnePodRazdelVed) null;
    int num = 0;
    string str1 = "";
    for (int i = 0; i < xmlNode.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlNode.Attributes[i];
      string name = attribute.Name;
      string str2 = attribute.Value.ToString();
      if (name == "name")
        str1 = str2;
      if (name == "PodRazdelVed")
        num = Convert.ToInt32(str2);
    }
    return new Vedomost_VB.OnePodRazdelVed()
    {
      _podRazdelVed = num,
      _name = str1
    };
  }

  /// <summary> Вывод в XML Списка разделов </summary>
  /// <param name="xmlDocument"></param>
  /// <returns></returns>
  public XmlElement Xml_List_RazdelsVed(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    if (this._list_RazdelsVed == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "LIST_RAZDELS_VED", string.Empty);
    for (int index = 0; index < this._list_RazdelsVed.Count; ++index)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed = this._list_RazdelsVed[index];
      XmlElement newChild = this.Xml_OneRazdelVed(xmlDocument, oneRazdelVed);
      if (newChild != null)
        element.AppendChild((XmlNode) newChild);
    }
    return element;
  }

  public List<Vedomost_VB.OneFieldSpForRead> List_Ved_ID_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (List<Vedomost_VB.OneFieldSpForRead>) null;
    List<Vedomost_VB.OneFieldSpForRead> oneFieldSpForReadList = new List<Vedomost_VB.OneFieldSpForRead>();
    int num = 0;
    foreach (XmlNode childNode in xmlElement.ChildNodes)
    {
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this.OneFieldSpForRead_ReadFromXml(childNode);
      if (oneFieldSpForRead != null && oneFieldSpForRead._id != -10000)
        oneFieldSpForReadList.Add(oneFieldSpForRead);
      ++num;
    }
    return oneFieldSpForReadList;
  }

  public XmlElement Xml_Sbor_Options_Ved(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "SBOR_OPTIONS_VED", string.Empty);
    if (this._typeDoc == Vedomost_VB.TypeDoc.Ved || this._typeDoc == Vedomost_VB.TypeDoc.Undefined || this._typeDoc == Vedomost_VB.TypeDoc.Espd)
    {
      XmlAttribute attribute1 = this._xmlDocument.CreateAttribute("is_Vydeliat_Sami_Komplekty");
      attribute1.Value = this._sbor_Options._is_Vydeliat_Sami_Komplekty.ToString();
      element.Attributes.Append(attribute1);
      XmlAttribute attribute2 = this._xmlDocument.CreateAttribute("is_Vydeliat_Therez_Komplekty");
      attribute2.Value = this._sbor_Options._is_Vydeliat_Therez_Komplekty.ToString();
      element.Attributes.Append(attribute2);
      XmlAttribute attribute3 = this._xmlDocument.CreateAttribute("isSamuSP_ne_iz_spiska_zanosit");
      attribute3.Value = this._sbor_Options._isSamuSP_ne_iz_spiska_zanosit.ToString();
      element.Attributes.Append(attribute3);
      XmlAttribute attribute4 = this._xmlDocument.CreateAttribute("isReference_Show");
      attribute4.Value = this._sbor_Options._isReference_Show.ToString();
      element.Attributes.Append(attribute4);
      XmlAttribute attribute5 = this._xmlDocument.CreateAttribute("isRaskrSP_s_takoi_Ved");
      attribute5.Value = this._sbor_Options._isRaskrSP_s_takoi_Ved.ToString();
      element.Attributes.Append(attribute5);
      XmlAttribute attribute6 = this._xmlDocument.CreateAttribute("isDopZam");
      attribute6.Value = this._sbor_Options._isDopZam.ToString();
      element.Attributes.Append(attribute6);
      XmlAttribute attribute7 = this._xmlDocument.CreateAttribute("isAllocateDopZam");
      attribute7.Value = this._sbor_Options._isAllocateDopZam.ToString();
      element.Attributes.Append(attribute7);
    }
    return element;
  }

  public XmlElement Xml_Espd(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "ESPD", string.Empty);
    if (this._typeDoc == Vedomost_VB.TypeDoc.Espd)
    {
      XmlAttribute attribute1 = this._xmlDocument.CreateAttribute("isAddLU");
      attribute1.Value = this._espd._isAddLU.ToString();
      element.Attributes.Append(attribute1);
      XmlAttribute attribute2 = this._xmlDocument.CreateAttribute("isCreateLU");
      attribute2.Value = this._espd._isCreateLU.ToString();
      element.Attributes.Append(attribute2);
      XmlAttribute attribute3 = this._xmlDocument.CreateAttribute("isOpenLU");
      attribute3.Value = this._espd._isOpenLU.ToString();
      element.Attributes.Append(attribute3);
      XmlAttribute attribute4 = this._xmlDocument.CreateAttribute("isAddToSpLU");
      attribute4.Value = this._espd._isAddToSpLU.ToString();
      element.Attributes.Append(attribute4);
      XmlAttribute attribute5 = this._xmlDocument.CreateAttribute("isAddRemark");
      attribute5.Value = this._espd._isAddRemark.ToString();
      element.Attributes.Append(attribute5);
      XmlAttribute attribute6 = this._xmlDocument.CreateAttribute("textRemark");
      attribute6.Value = this._espd._textRemark.ToString();
      element.Attributes.Append(attribute6);
    }
    return element;
  }

  /// <summary> Сохранение в XML Базовые опции </summary>
  /// <param name="xmlDocument"></param>
  /// <returns></returns>
  public XmlElement Xml_Bases_Options_Ved(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "BASES_OPTIONS_VED", string.Empty);
    if (this._typeDoc == Vedomost_VB.TypeDoc.Ved || this._typeDoc == Vedomost_VB.TypeDoc.Undefined || this._typeDoc == Vedomost_VB.TypeDoc.Espd)
    {
      XmlAttribute attribute1 = this._xmlDocument.CreateAttribute("isReadOrInit_isMain");
      attribute1.Value = "True";
      element.Attributes.Append(attribute1);
      XmlAttribute attribute2 = this._xmlDocument.CreateAttribute("isMainSort1");
      attribute2.Value = this._bases_Options_Ved._isMainSort1.ToString();
      element.Attributes.Append(attribute2);
      XmlAttribute attribute3 = this._xmlDocument.CreateAttribute("isMainSort2");
      attribute3.Value = this._bases_Options_Ved._isMainSort2.ToString();
      element.Attributes.Append(attribute3);
      XmlAttribute attribute4 = this._xmlDocument.CreateAttribute("isMainSummOdinakovyh");
      attribute4.Value = this._bases_Options_Ved._isMainSummOdinakovyh.ToString();
      element.Attributes.Append(attribute4);
      XmlAttribute attribute5 = this._xmlDocument.CreateAttribute("isMainCreateVtorRecords");
      attribute5.Value = this._bases_Options_Ved._isMainCreateVtorRecords.ToString();
      element.Attributes.Append(attribute5);
      XmlAttribute attribute6 = this._xmlDocument.CreateAttribute("isMainSumm");
      attribute6.Value = this._bases_Options_Ved._isMainSumm.ToString();
      element.Attributes.Append(attribute6);
      XmlAttribute attribute7 = this._xmlDocument.CreateAttribute("isOnlyUroven1");
      attribute7.Value = this._bases_Options_Ved._isOnlyUroven1.ToString();
      element.Attributes.Append(attribute7);
      XmlAttribute attribute8 = this._xmlDocument.CreateAttribute("is_Specification_Instrument");
      attribute8.Value = this._bases_Options_Ved._is_Specification_Instrument.ToString();
      element.Attributes.Append(attribute8);
      XmlAttribute attribute9 = this._xmlDocument.CreateAttribute("isVedSortGroup");
      attribute9.Value = this._bases_Options_Ved._isVedSortGroup.ToString();
      element.Attributes.Append(attribute9);
      XmlAttribute attribute10 = this._xmlDocument.CreateAttribute("isVedMergerIsp");
      attribute10.Value = this._bases_Options_Ved._isVedMergerIsp.ToString();
      element.Attributes.Append(attribute10);
      XmlAttribute attribute11 = this._xmlDocument.CreateAttribute("isAddFuncGroup");
      attribute11.Value = this._bases_Options_Ved._isVedAddFuncGroup.ToString();
      element.Attributes.Append(attribute11);
      XmlAttribute attribute12 = this._xmlDocument.CreateAttribute("isVedSort1");
      attribute12.Value = this._bases_Options_Ved._isVedSort1.ToString();
      element.Attributes.Append(attribute12);
      XmlAttribute attribute13 = this._xmlDocument.CreateAttribute("isVedUnion");
      attribute13.Value = this._bases_Options_Ved._isVedUnion.ToString();
      element.Attributes.Append(attribute13);
      XmlAttribute attribute14 = this._xmlDocument.CreateAttribute("isVedExtrectionVtor");
      attribute14.Value = this._bases_Options_Ved._isVedExtrectionVtor.ToString();
      element.Attributes.Append(attribute14);
      XmlAttribute attribute15 = this._xmlDocument.CreateAttribute("isVedMergerVtor");
      attribute15.Value = this._bases_Options_Ved._isVedMergerVtor.ToString();
      element.Attributes.Append(attribute15);
      XmlAttribute attribute16 = this._xmlDocument.CreateAttribute("isVedSortVtor");
      attribute16.Value = this._bases_Options_Ved._isVedSortVtor.ToString();
      element.Attributes.Append(attribute16);
      XmlAttribute attribute17 = this._xmlDocument.CreateAttribute("isVedSummVtor");
      attribute17.Value = this._bases_Options_Ved._isVedSummVtor.ToString();
      element.Attributes.Append(attribute17);
      XmlAttribute attribute18 = this._xmlDocument.CreateAttribute("isVedCreateZagolIspoln");
      attribute18.Value = this._bases_Options_Ved._isVedCreateZagolIspoln.ToString();
      element.Attributes.Append(attribute18);
      XmlAttribute attribute19 = this._xmlDocument.CreateAttribute("isVedCreateZagolSvoiaVed");
      attribute19.Value = this._bases_Options_Ved._isVedCreateZagolSvoiaVed.ToString();
      element.Attributes.Append(attribute19);
      XmlAttribute attribute20 = this._xmlDocument.CreateAttribute("isVedCreateZagolPoPriznaku");
      attribute20.Value = this._bases_Options_Ved._isVedCreateZagolPoPriznaku.ToString();
      element.Attributes.Append(attribute20);
      XmlAttribute attribute21 = this._xmlDocument.CreateAttribute("is_Extended_List_Names");
      attribute21.Value = this._bases_Options_Ved._is_Extended_List_Names.ToString();
      element.Attributes.Append(attribute21);
      XmlAttribute attribute22 = this._xmlDocument.CreateAttribute("isVedAddToSp");
      attribute22.Value = this._bases_Options_Ved._isVedAddToSp.ToString();
      element.Attributes.Append(attribute22);
      XmlAttribute attribute23 = this._xmlDocument.CreateAttribute("isFor_ZIP_SB_Raskr");
      attribute23.Value = this._bases_Options_Ved._isFor_ZIP_SB_Raskr.ToString();
      element.Attributes.Append(attribute23);
      XmlAttribute attribute24 = this._xmlDocument.CreateAttribute("isFor_ZIP_SB_Add");
      attribute24.Value = this._bases_Options_Ved._isFor_ZIP_SB_Add.ToString();
      element.Attributes.Append(attribute24);
      XmlAttribute attribute25 = this._xmlDocument.CreateAttribute("isFor_ZIP_COMPL_Raskr");
      attribute25.Value = this._bases_Options_Ved._isFor_ZIP_COMPL_Raskr.ToString();
      element.Attributes.Append(attribute25);
      XmlAttribute attribute26 = this._xmlDocument.CreateAttribute("isFor_ZIP_COMPL_Add");
      attribute26.Value = this._bases_Options_Ved._isFor_ZIP_COMPL_Add.ToString();
      element.Attributes.Append(attribute26);
      XmlAttribute attribute27 = this._xmlDocument.CreateAttribute("isVedAddToRazdel");
      attribute27.Value = this._bases_Options_Ved._isVedAddToRazdel.ToString();
      element.Attributes.Append(attribute27);
      XmlAttribute attribute28 = this._xmlDocument.CreateAttribute("isInputDoc");
      attribute28.Value = this._bases_Options_Ved._isInputDoc.ToString();
      element.Attributes.Append(attribute28);
      XmlAttribute attribute29 = this._xmlDocument.CreateAttribute("isInputIzd");
      attribute29.Value = this._bases_Options_Ved._isInputIzd.ToString();
      element.Attributes.Append(attribute29);
      XmlAttribute attribute30 = this._xmlDocument.CreateAttribute("isInputMat");
      attribute30.Value = this._bases_Options_Ved._isInputMat.ToString();
      element.Attributes.Append(attribute30);
    }
    XmlElement newChild1 = this.Xml_list_quickObjectInfo_Ved(xmlDocument);
    if (newChild1 != null)
      element.AppendChild((XmlNode) newChild1);
    XmlElement newChild2 = this.Xml_list_Opening_Sections(xmlDocument);
    if (newChild2 != null)
      element.AppendChild((XmlNode) newChild2);
    return element;
  }

  public XmlElement Xml_Protection_From_Editing(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "PROTECTION_FROM_EDITING", string.Empty);
    XmlAttribute attribute1 = this._xmlDocument.CreateAttribute("isFullProhibition");
    attribute1.Value = this._protection_From_Editing._isFullProhibition.ToString();
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = this._xmlDocument.CreateAttribute("isProhibition_DocRowWithObj");
    attribute2.Value = this._protection_From_Editing._isProhibition_DocRowWithObj.ToString();
    element.Attributes.Append(attribute2);
    XmlAttribute attribute3 = this._xmlDocument.CreateAttribute("isProtectionCommand");
    attribute3.Value = this._protection_From_Editing._isProtectionCommand.ToString();
    element.Attributes.Append(attribute3);
    return element;
  }

  /// <summary> Список раскрываемых разделов </summary>
  /// <param name="xmlDocument"></param>
  /// <returns></returns>
  public XmlElement Xml_list_Opening_Sections(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element1 = xmlDocument.CreateElement(string.Empty, "LIST_OPENING_SECTION", string.Empty);
    if (this._bases_Options_Ved._opеning_Sections != null)
    {
      for (int index = 0; index < this._bases_Options_Ved._opеning_Sections.Count; ++index)
      {
        string opеningSection = this._bases_Options_Ved._opеning_Sections[index];
        XmlElement element2 = xmlDocument.CreateElement(string.Empty, "SECTION", string.Empty);
        XmlAttribute attribute = xmlDocument.CreateAttribute("SectionName");
        attribute.Value = opеningSection;
        element2.Attributes.Append(attribute);
        element1.AppendChild((XmlNode) element2);
      }
    }
    return element1;
  }

  /// <summary> Вывод в XML списка Guid каталогов Imbase заголовков </summary>
  /// <param name="xmlDocument"></param>
  /// <returns></returns>
  public XmlElement Xml_list_quickObjectInfo_Ved(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element1 = xmlDocument.CreateElement(string.Empty, "LIST_QUICKOBJECTINFO", string.Empty);
    if (this._bases_Options_Ved._list_quickObjectInfo != null)
    {
      for (int index = 0; index < this._bases_Options_Ved._list_quickObjectInfo.Count; ++index)
      {
        QuickObjectInfo quickObjectInfo = this._bases_Options_Ved._list_quickObjectInfo[index];
        XmlElement element2 = xmlDocument.CreateElement(string.Empty, "QuickObjectInfo", string.Empty);
        XmlAttribute attribute1 = xmlDocument.CreateAttribute("VersionGuid");
        attribute1.Value = quickObjectInfo.VersionGuid.ToString();
        element2.Attributes.Append(attribute1);
        if (!string.IsNullOrEmpty(quickObjectInfo.Caption))
        {
          XmlAttribute attribute2 = xmlDocument.CreateAttribute("Caption");
          attribute2.Value = quickObjectInfo.Caption.ToString();
          element2.Attributes.Append(attribute2);
        }
        element1.AppendChild((XmlNode) element2);
      }
    }
    return element1;
  }

  public XmlElement Xml_Dopoln_Options_Ved(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "DOPOLN_OPTIONS_VED", string.Empty);
    XmlAttribute attribute1 = this._xmlDocument.CreateAttribute("text1");
    attribute1.Value = this._dopoln_Options_Ved._text1;
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = this._xmlDocument.CreateAttribute("text2");
    attribute2.Value = this._dopoln_Options_Ved._text2;
    element.Attributes.Append(attribute2);
    XmlAttribute attribute3 = this._xmlDocument.CreateAttribute("int1");
    attribute3.Value = this._dopoln_Options_Ved._int1.ToString();
    element.Attributes.Append(attribute3);
    XmlAttribute attribute4 = this._xmlDocument.CreateAttribute("int2");
    attribute4.Value = this._dopoln_Options_Ved._int2.ToString();
    element.Attributes.Append(attribute4);
    XmlAttribute attribute5 = this._xmlDocument.CreateAttribute("double1");
    attribute5.Value = this._dopoln_Options_Ved._double1.ToString();
    element.Attributes.Append(attribute5);
    XmlAttribute attribute6 = this._xmlDocument.CreateAttribute("double2");
    attribute6.Value = this._dopoln_Options_Ved._double2.ToString();
    element.Attributes.Append(attribute6);
    XmlAttribute attribute7 = this._xmlDocument.CreateAttribute("bool1");
    attribute7.Value = this._dopoln_Options_Ved._bool1.ToString();
    element.Attributes.Append(attribute7);
    XmlAttribute attribute8 = this._xmlDocument.CreateAttribute("bool2");
    attribute8.Value = this._dopoln_Options_Ved._bool2.ToString();
    element.Attributes.Append(attribute8);
    return element;
  }

  public Vedomost_VB.Sbor_Options Sbor_Options_Ved_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (Vedomost_VB.Sbor_Options) null;
    Vedomost_VB.Sbor_Options sborOptions = new Vedomost_VB.Sbor_Options();
    for (int i = 0; i < xmlElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlElement.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      switch (name)
      {
        case "is_Vydeliat_Sami_Komplekty":
          sborOptions._is_Vydeliat_Sami_Komplekty = str == "True";
          break;
        case "is_Vydeliat_Therez_Komplekty":
          sborOptions._is_Vydeliat_Therez_Komplekty = str == "True";
          break;
        case "isSamuSP_ne_iz_spiska_zanosit":
          sborOptions._isSamuSP_ne_iz_spiska_zanosit = str == "True";
          break;
        case "isReference_Show":
          sborOptions._isReference_Show = str == "True";
          break;
        case "isRaskrSP_s_takoi_Ved":
          sborOptions._isRaskrSP_s_takoi_Ved = Convert.ToInt32(str);
          break;
        case "isDopZam":
          sborOptions._isDopZam = Convert.ToInt32(str);
          break;
        case "isAllocateDopZam":
          sborOptions._isAllocateDopZam = Convert.ToInt32(str);
          break;
      }
    }
    return sborOptions;
  }

  public Vedomost_VB.ESPD Espd_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (Vedomost_VB.ESPD) null;
    Vedomost_VB.ESPD espd = new Vedomost_VB.ESPD();
    for (int i = 0; i < xmlElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlElement.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      switch (name)
      {
        case "isAddLU":
          espd._isAddLU = str == "True";
          break;
        case "isCreateLU":
          espd._isCreateLU = str == "True";
          break;
        case "isOpenLU":
          espd._isOpenLU = str == "True";
          break;
        case "isAddToSpLU":
          espd._isAddToSpLU = str == "True";
          break;
        case "isAddRemark":
          espd._isAddRemark = str == "True";
          break;
        case "textRemark":
          espd._textRemark = str;
          break;
      }
    }
    return espd;
  }

  public Vedomost_VB.Bases_Options_Ved Bases_Options_Ved_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (Vedomost_VB.Bases_Options_Ved) null;
    Vedomost_VB.Bases_Options_Ved basesOptionsVed = new Vedomost_VB.Bases_Options_Ved();
    basesOptionsVed._isInputDoc = true;
    basesOptionsVed._isInputIzd = true;
    basesOptionsVed._isInputMat = true;
    for (int i = 0; i < xmlElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlElement.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "isReadOrInit_isMain")
        basesOptionsVed._isReadOrInit_isMain = str == "True";
      else if (name == "isMainSort1")
        basesOptionsVed._isMainSort1 = str == "True";
      else if (name == "isMainSort2")
        basesOptionsVed._isMainSort2 = str == "True";
      else if (name == "isMainSummOdinakovyh")
        basesOptionsVed._isMainSummOdinakovyh = str == "True";
      else if (name == "isMainCreateVtorRecords")
        basesOptionsVed._isMainCreateVtorRecords = str == "True";
      else if (name == "isMainSumm")
        basesOptionsVed._isMainSumm = str == "True";
      else if (name == "isOnlyUroven1")
        basesOptionsVed._isOnlyUroven1 = str == "True";
      else if (name == "is_Specification_Instrument")
        basesOptionsVed._is_Specification_Instrument = str == "True";
      else if (name == "isVedSortGroup")
        basesOptionsVed._isVedSortGroup = str == "True";
      else if (name == "isVedMergerIsp")
        basesOptionsVed._isVedMergerIsp = str == "True";
      else if (name == "isAddFuncGroup")
        basesOptionsVed._isVedAddFuncGroup = str == "True";
      else if (name == "isVedSort1")
        basesOptionsVed._isVedSort1 = str == "True";
      else if (name == "isVedUnion")
        basesOptionsVed._isVedUnion = str == "True";
      else if (name == "isVedExtrectionVtor")
        basesOptionsVed._isVedExtrectionVtor = str == "True";
      else if (name == "isVedMergerVtor")
        basesOptionsVed._isVedMergerVtor = str == "True";
      else if (name == "isVedSortVtor")
        basesOptionsVed._isVedSortVtor = str == "True";
      else if (name == "isVedSummVtor")
        basesOptionsVed._isVedSummVtor = str == "True";
      else if (name == "isVedCreateZagolIspoln")
        basesOptionsVed._isVedCreateZagolIspoln = str == "True";
      else if (name == "isVedCreateZagolSvoiaVed")
        basesOptionsVed._isVedCreateZagolSvoiaVed = str == "True";
      else if (name == "isVedCreateZagolPoPriznaku")
        basesOptionsVed._isVedCreateZagolPoPriznaku = str == "True";
      else if (name == "is_Extended_List_Names")
        basesOptionsVed._is_Extended_List_Names = str == "True";
      else if (name == "isVedAddToSp")
        basesOptionsVed._isVedAddToSp = str == "True";
      else if (name == "isFor_ZIP_SB_Raskr")
        basesOptionsVed._isFor_ZIP_SB_Raskr = str == "True";
      else if (name == "isFor_ZIP_SB_Add")
        basesOptionsVed._isFor_ZIP_SB_Add = str == "True";
      else if (name == "isFor_ZIP_COMPL_Raskr")
        basesOptionsVed._isFor_ZIP_COMPL_Raskr = str == "True";
      else if (name == "isFor_ZIP_COMPL_Add")
      {
        basesOptionsVed._isFor_ZIP_COMPL_Add = str == "True";
      }
      else
      {
        if (name == "isVedAddToRazdel")
          basesOptionsVed._isVedAddToRazdel = Convert.ToInt32(str);
        if (name == "isInputDoc")
          basesOptionsVed._isInputDoc = str == "True";
        else if (name == "isInputIzd")
          basesOptionsVed._isInputIzd = str == "True";
        else if (name == "isInputMat")
          basesOptionsVed._isInputMat = str == "True";
      }
    }
    basesOptionsVed._list_quickObjectInfo = this.List_QuickObjectInfo_Ved_ReadFromXml(xmlElement);
    basesOptionsVed._opеning_Sections = this.List_Opening_Section_Ved_ReadFromXml(xmlElement);
    if (basesOptionsVed._opеning_Sections == null || basesOptionsVed._opеning_Sections.Count < 1)
    {
      basesOptionsVed._opеning_Sections = new List<string>();
      basesOptionsVed._opеning_Sections.Add("Комплексы");
      basesOptionsVed._opеning_Sections.Add("Сборочные единицы");
      basesOptionsVed._opеning_Sections.Add("Комплекты");
    }
    return basesOptionsVed;
  }

  public Vedomost_VB.Protection_From_Editing Protection_From_Editing_ReadFromXml(
    XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (Vedomost_VB.Protection_From_Editing) null;
    Vedomost_VB.Protection_From_Editing protectionFromEditing = new Vedomost_VB.Protection_From_Editing();
    for (int i = 0; i < xmlElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlElement.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      switch (name)
      {
        case "isFullProhibition":
          protectionFromEditing._isFullProhibition = str == "True";
          break;
        case "isProhibition_DocRowWithObj":
          protectionFromEditing._isProhibition_DocRowWithObj = str == "True";
          break;
        case "isProtectionCommand":
          protectionFromEditing._isProtectionCommand = str == "True";
          break;
      }
    }
    return protectionFromEditing;
  }

  public List<QuickObjectInfo> List_QuickObjectInfo_Ved_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (List<QuickObjectInfo>) null;
    List<QuickObjectInfo> quickObjectInfoList = new List<QuickObjectInfo>();
    if (xmlElement.ChildNodes.Count < 1)
      return quickObjectInfoList;
    XmlNode childNode1 = xmlElement.ChildNodes[0];
    if (childNode1.Name != "LIST_QUICKOBJECTINFO")
      return quickObjectInfoList;
    foreach (XmlNode childNode2 in childNode1.ChildNodes)
    {
      if (childNode2 != null)
      {
        QuickObjectInfo quickObjectInfo = new QuickObjectInfo();
        for (int i = 0; i < childNode2.Attributes.Count; ++i)
        {
          XmlAttribute attribute = childNode2.Attributes[i];
          string name = attribute.Name;
          string g = attribute.Value.ToString();
          if (name == "Caption")
            quickObjectInfo.Caption = g;
          if (name == "VersionGuid")
            quickObjectInfo.VersionGuid = new Guid(g);
        }
        quickObjectInfoList.Add(quickObjectInfo);
      }
    }
    return quickObjectInfoList;
  }

  public List<string> List_Opening_Section_Ved_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (List<string>) null;
    List<string> stringList = new List<string>();
    if (xmlElement.ChildNodes.Count < 2)
      return (List<string>) null;
    XmlNode childNode1 = xmlElement.ChildNodes[1];
    if (childNode1.Name != "LIST_OPENING_SECTION")
      return (List<string>) null;
    foreach (XmlNode childNode2 in childNode1.ChildNodes)
    {
      if (childNode2 != null)
      {
        for (int i = 0; i < childNode2.Attributes.Count; ++i)
        {
          XmlAttribute attribute = childNode2.Attributes[i];
          string name = attribute.Name;
          string str = attribute.Value.ToString();
          if (name == "SectionName" && !string.IsNullOrEmpty(str))
          {
            stringList.Add(str);
            break;
          }
        }
      }
    }
    return stringList.Count > 0 ? stringList : (List<string>) null;
  }

  public Vedomost_VB.Dopoln_Options_Ved Dopoln_Options_Ved_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (Vedomost_VB.Dopoln_Options_Ved) null;
    Vedomost_VB.Dopoln_Options_Ved dopolnOptionsVed = new Vedomost_VB.Dopoln_Options_Ved();
    dopolnOptionsVed._text1 = "";
    dopolnOptionsVed._text2 = "";
    dopolnOptionsVed._int1 = 0;
    dopolnOptionsVed._int2 = 0;
    dopolnOptionsVed._double1 = 0.0;
    dopolnOptionsVed._double2 = 0.0;
    dopolnOptionsVed._bool1 = false;
    dopolnOptionsVed._bool2 = false;
    for (int i = 0; i < xmlElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlElement.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      switch (name)
      {
        case "text1":
          dopolnOptionsVed._text1 = str;
          break;
        case "text2":
          dopolnOptionsVed._text2 = str;
          break;
        case "int1":
          dopolnOptionsVed._int1 = Convert.ToInt32(str);
          break;
        case "int2":
          dopolnOptionsVed._int2 = Convert.ToInt32(str);
          break;
        case "double1":
          dopolnOptionsVed._double1 = Convert.ToDouble(str);
          break;
        case "double2":
          dopolnOptionsVed._double2 = Convert.ToDouble(str);
          break;
        case "bool1":
          dopolnOptionsVed._bool1 = str == "True";
          break;
        case "bool2":
          dopolnOptionsVed._bool2 = str == "True";
          break;
      }
    }
    return dopolnOptionsVed;
  }

  /// <summary> Вывод в XML списка заголовков </summary>
  /// <param name="xmlDocument"></param>
  /// <returns></returns>
  public XmlElement Xml_Zagolovki_Ved(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element1 = xmlDocument.CreateElement(string.Empty, "ZAGOLOVKI_VED", string.Empty);
    XmlElement zagol = this._zagolovki_Ved.Xml_OneDataFieldToZagol(xmlDocument);
    if (zagol != null)
      element1.AppendChild((XmlNode) zagol);
    XmlElement element2 = xmlDocument.CreateElement(string.Empty, "List_One_Zagolovok", string.Empty);
    for (int index = 0; index < this._zagolovki_Ved._list_One_Zagolovok.Count; ++index)
    {
      Vedomost_VB.One_Zagolovok oneZagolovok = this._zagolovki_Ved._list_One_Zagolovok[index];
      XmlElement element3 = xmlDocument.CreateElement(string.Empty, "One_Zagolovok", string.Empty);
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("granicaPriznaka");
      attribute1.Value = oneZagolovok._granicaPriznaka.ToString();
      element3.Attributes.Append(attribute1);
      XmlAttribute attribute2 = xmlDocument.CreateAttribute("name");
      attribute2.Value = oneZagolovok._name.ToString();
      element3.Attributes.Append(attribute2);
      element2.AppendChild((XmlNode) element3);
    }
    element1.AppendChild((XmlNode) element2);
    return element1;
  }

  public XmlElement Xml_Merge_Usl2(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element1 = xmlDocument.CreateElement(string.Empty, "MERGE_USL2", string.Empty);
    XmlElement element2 = xmlDocument.CreateElement(string.Empty, "LIST_MERGE_USL2", string.Empty);
    for (int index = 0; index < this._merge_Usl2._list_Merge_Usl2.Count; ++index)
    {
      Vedomost_VB.Merge_Usl_One mergeUslOne = this._merge_Usl2._list_Merge_Usl2[index];
      XmlElement element3 = xmlDocument.CreateElement(string.Empty, "MERGE_USL_ONE", string.Empty);
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("typeField");
      attribute1.Value = mergeUslOne._typeField.ToString();
      element3.Attributes.Append(attribute1);
      XmlAttribute attribute2 = xmlDocument.CreateAttribute("objectType");
      attribute2.Value = mergeUslOne._objectType.ToString();
      element3.Attributes.Append(attribute2);
      XmlAttribute attribute3 = xmlDocument.CreateAttribute("typeFieldVedRec");
      int typeFieldVedRec = (int) mergeUslOne._typeFieldVedRec;
      attribute3.Value = typeFieldVedRec.ToString();
      element3.Attributes.Append(attribute3);
      element2.AppendChild((XmlNode) element3);
    }
    element1.AppendChild((XmlNode) element2);
    return element1;
  }

  public Vedomost_VB.Zagolovki_Ved Zagolovki_Ved_ReadFromXml(XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (Vedomost_VB.Zagolovki_Ved) null;
    if (xmlElement.ChildNodes.Count < 2)
      return (Vedomost_VB.Zagolovki_Ved) null;
    Vedomost_VB.Zagolovki_Ved zagolovki_Ved = new Vedomost_VB.Zagolovki_Ved();
    XmlNode childNode1 = xmlElement.ChildNodes[0];
    if (childNode1.Name == "oneDataFieldToZagol")
      this.OneDataFieldToZagol_FromXml(childNode1, zagolovki_Ved);
    XmlNode childNode2 = xmlElement.ChildNodes[1];
    if (childNode2 == null)
      return (Vedomost_VB.Zagolovki_Ved) null;
    zagolovki_Ved._list_One_Zagolovok = new List<Vedomost_VB.One_Zagolovok>();
    foreach (XmlNode childNode3 in childNode2.ChildNodes)
    {
      Vedomost_VB.One_Zagolovok oneZagolovok = new Vedomost_VB.One_Zagolovok();
      for (int i = 0; i < childNode3.Attributes.Count; ++i)
      {
        XmlAttribute attribute = childNode3.Attributes[i];
        string name = attribute.Name;
        string str = attribute.Value.ToString();
        if (name == "granicaPriznaka")
          oneZagolovok._granicaPriznaka = str;
        if (name == "name")
          oneZagolovok._name = str;
      }
      zagolovki_Ved._list_One_Zagolovok.Add(oneZagolovok);
    }
    return zagolovki_Ved;
  }

  public XmlElement Xml_Sorting_Usl(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "SORTING_USL", string.Empty);
    XmlElement newChild = this.Xml_sorting_Usl_One_From4(xmlDocument, this._sorting_Usl.Sorting_Usl_VedOsn);
    if (newChild != null)
      element.AppendChild((XmlNode) newChild);
    return element;
  }

  public XmlElement Xml_sorting_Usl_One_From4(
    XmlDocument xmlDocument,
    Vedomost_VB.Sorting_Usl_One_From4 sorting_Usl_One_From4)
  {
    if (sorting_Usl_One_From4 == null)
      return (XmlElement) null;
    string name = sorting_Usl_One_From4._name;
    if (name == "")
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, name, string.Empty);
    XmlAttribute attribute = xmlDocument.CreateAttribute("name");
    attribute.Value = name;
    element.Attributes.Append(attribute);
    if (sorting_Usl_One_From4._list_sorting_Usl_OneRazdel != null)
    {
      for (int index = 0; index < sorting_Usl_One_From4._list_sorting_Usl_OneRazdel.Count; ++index)
      {
        Vedomost_VB.Sorting_Usl_OneRazdel sorting_Usl_OneRazdel = sorting_Usl_One_From4._list_sorting_Usl_OneRazdel[index];
        XmlElement newChild = this.Xml_sorting_Usl_OneRazdel(xmlDocument, sorting_Usl_OneRazdel);
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
    }
    return element;
  }

  public XmlElement Xml_sorting_Usl_OneRazdel(
    XmlDocument xmlDocument,
    Vedomost_VB.Sorting_Usl_OneRazdel sorting_Usl_OneRazdel)
  {
    if (sorting_Usl_OneRazdel == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "Sorting_Usl_OneRazdel", string.Empty);
    XmlAttribute attribute = xmlDocument.CreateAttribute("razdelNum");
    attribute.Value = sorting_Usl_OneRazdel._razdelNum.ToString();
    element.Attributes.Append(attribute);
    if (sorting_Usl_OneRazdel._list_sorting_Usl_One != null)
    {
      for (int index = 0; index < sorting_Usl_OneRazdel._list_sorting_Usl_One.Count; ++index)
      {
        Vedomost_VB.Sorting_Usl_One sorting_Usl_One = sorting_Usl_OneRazdel._list_sorting_Usl_One[index];
        XmlElement newChild = this.Xml_sorting_Usl_One(xmlDocument, sorting_Usl_One);
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
    }
    return element;
  }

  public XmlElement Xml_sorting_Usl_One(
    XmlDocument xmlDocument,
    Vedomost_VB.Sorting_Usl_One sorting_Usl_One)
  {
    if (sorting_Usl_One == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "Sorting_Usl_One", string.Empty);
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("typeField");
    attribute1.Value = sorting_Usl_One._typeField.ToString();
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("objectType");
    attribute2.Value = sorting_Usl_One._objectType.ToString();
    element.Attributes.Append(attribute2);
    XmlAttribute attribute3 = xmlDocument.CreateAttribute("typeFieldVedRec");
    int typeFieldVedRec = (int) sorting_Usl_One._typeFieldVedRec;
    attribute3.Value = typeFieldVedRec.ToString();
    element.Attributes.Append(attribute3);
    XmlAttribute attribute4 = xmlDocument.CreateAttribute("beginSravn");
    attribute4.Value = sorting_Usl_One._beginSravn.ToString();
    element.Attributes.Append(attribute4);
    if (sorting_Usl_One._symb_ot != null)
    {
      XmlAttribute attribute5 = xmlDocument.CreateAttribute("symb_ot");
      attribute5.Value = sorting_Usl_One._symb_ot.ToString();
      element.Attributes.Append(attribute5);
    }
    XmlAttribute attribute6 = xmlDocument.CreateAttribute("num_symb_ot");
    attribute6.Value = sorting_Usl_One._num_symb_ot.ToString();
    element.Attributes.Append(attribute6);
    XmlAttribute attribute7 = xmlDocument.CreateAttribute("endSravn");
    attribute7.Value = sorting_Usl_One._endSravn.ToString();
    element.Attributes.Append(attribute7);
    if (sorting_Usl_One._symb_do != null)
    {
      XmlAttribute attribute8 = xmlDocument.CreateAttribute("symb_do");
      attribute8.Value = sorting_Usl_One._symb_do.ToString();
      element.Attributes.Append(attribute8);
    }
    XmlAttribute attribute9 = xmlDocument.CreateAttribute("num_symb_do");
    attribute9.Value = sorting_Usl_One._num_symb_do.ToString();
    element.Attributes.Append(attribute9);
    XmlAttribute attribute10 = xmlDocument.CreateAttribute("sravnenie");
    attribute10.Value = sorting_Usl_One._sravnenie.ToString();
    element.Attributes.Append(attribute10);
    XmlAttribute attribute11 = xmlDocument.CreateAttribute("poriadokSortirovki");
    attribute11.Value = sorting_Usl_One._poriadokSortirovki.ToString();
    element.Attributes.Append(attribute11);
    XmlAttribute attribute12 = xmlDocument.CreateAttribute("pustyeStroki");
    attribute12.Value = sorting_Usl_One._pustyeStroki.ToString();
    element.Attributes.Append(attribute12);
    return element;
  }

  public Vedomost_VB.Sorting_Usl Sorting_Usl_ReafFromXml(XmlNode Xml_sorting_Usl)
  {
    if (Xml_sorting_Usl == null)
      return (Vedomost_VB.Sorting_Usl) null;
    Vedomost_VB.Sorting_Usl sortingUsl = new Vedomost_VB.Sorting_Usl();
    for (int i = 0; i < Xml_sorting_Usl.ChildNodes.Count; ++i)
    {
      Vedomost_VB.Sorting_Usl_One_From4 sortingUslOneFrom4 = this.Sorting_Usl_One_From4_ReafFromXml(Xml_sorting_Usl.ChildNodes[i]);
      if (sortingUslOneFrom4 != null)
      {
        if (sortingUslOneFrom4._name == "Sorting_Usl_MainOsn")
          sortingUsl.Sorting_Usl_MainOsn = sortingUslOneFrom4;
        if (sortingUslOneFrom4._name == "Sorting_Usl_MainVtor")
          sortingUsl.Sorting_Usl_MainVtor = sortingUslOneFrom4;
        if (sortingUslOneFrom4._name == "Sorting_Usl_VedOsn")
          sortingUsl.Sorting_Usl_VedOsn = sortingUslOneFrom4;
        if (sortingUslOneFrom4._name == "Sorting_Usl_VedVtor")
          sortingUsl.Sorting_Usl_VedVtor = sortingUslOneFrom4;
      }
    }
    return sortingUsl;
  }

  public Vedomost_VB.Sorting_Usl_One_From4 Sorting_Usl_One_From4_ReafFromXml(
    XmlNode Xml_sorting_Usl_One_From4)
  {
    if (Xml_sorting_Usl_One_From4 == null)
      return (Vedomost_VB.Sorting_Usl_One_From4) null;
    Vedomost_VB.Sorting_Usl_One_From4 sortingUslOneFrom4 = new Vedomost_VB.Sorting_Usl_One_From4();
    sortingUslOneFrom4._list_sorting_Usl_OneRazdel = new List<Vedomost_VB.Sorting_Usl_OneRazdel>();
    for (int i = 0; i < Xml_sorting_Usl_One_From4.Attributes.Count; ++i)
    {
      XmlAttribute attribute = Xml_sorting_Usl_One_From4.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "name")
        sortingUslOneFrom4._name = str;
    }
    for (int i = 0; i < Xml_sorting_Usl_One_From4.ChildNodes.Count; ++i)
    {
      XmlNode childNode = Xml_sorting_Usl_One_From4.ChildNodes[i];
      if (!(childNode.Name != "Sorting_Usl_OneRazdel"))
      {
        Vedomost_VB.Sorting_Usl_OneRazdel sortingUslOneRazdel = this.Sorting_Usl_OneRazdel_ReafFromXml(childNode);
        sortingUslOneFrom4._list_sorting_Usl_OneRazdel.Add(sortingUslOneRazdel);
      }
    }
    return sortingUslOneFrom4;
  }

  public Vedomost_VB.Sorting_Usl_OneRazdel Sorting_Usl_OneRazdel_ReafFromXml(
    XmlNode Xml_sorting_Usl_OneRazdel)
  {
    if (Xml_sorting_Usl_OneRazdel == null)
      return (Vedomost_VB.Sorting_Usl_OneRazdel) null;
    Vedomost_VB.Sorting_Usl_OneRazdel sortingUslOneRazdel = new Vedomost_VB.Sorting_Usl_OneRazdel();
    sortingUslOneRazdel._list_sorting_Usl_One = new List<Vedomost_VB.Sorting_Usl_One>();
    for (int i = 0; i < Xml_sorting_Usl_OneRazdel.Attributes.Count; ++i)
    {
      XmlAttribute attribute = Xml_sorting_Usl_OneRazdel.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "razdelNum")
        sortingUslOneRazdel._razdelNum = (long) Convert.ToInt32(str);
    }
    for (int i = 0; i < Xml_sorting_Usl_OneRazdel.ChildNodes.Count; ++i)
    {
      XmlNode childNode = Xml_sorting_Usl_OneRazdel.ChildNodes[i];
      if (!(childNode.Name != "Sorting_Usl_One"))
      {
        Vedomost_VB.Sorting_Usl_One sortingUslOne = this.Sorting_Usl_One_ReadFromXml(childNode);
        if (sortingUslOne != null)
          sortingUslOneRazdel._list_sorting_Usl_One.Add(sortingUslOne);
      }
    }
    return sortingUslOneRazdel;
  }

  public Vedomost_VB.Sorting_Usl_One Sorting_Usl_One_ReadFromXml(XmlNode xmlNode)
  {
    if (xmlNode == null)
      return (Vedomost_VB.Sorting_Usl_One) null;
    Vedomost_VB.Sorting_Usl_One sortingUslOne = new Vedomost_VB.Sorting_Usl_One();
    for (int i = 0; i < xmlNode.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlNode.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "objectType")
        sortingUslOne._objectType = Convert.ToInt32(str);
      if (name == "typeField")
      {
        switch (str)
        {
          case "ObjectType":
            sortingUslOne._typeField = Vedomost_VB.TypeField.ObjectType;
            continue;
          case "TypeFieldVedRec":
            sortingUslOne._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
            continue;
          default:
            return (Vedomost_VB.Sorting_Usl_One) null;
        }
      }
      else if (name == "typeFieldVedRec")
        sortingUslOne._typeFieldVedRec = (Vedomost_VB.TypeFieldVedRec) Convert.ToInt32(str);
      else if (name == "beginSravn")
      {
        switch (str)
        {
          case "S_begin":
            sortingUslOne._beginSravn = Vedomost_VB.BeginSravn.S_begin;
            continue;
          case "S_pozicii":
            sortingUslOne._beginSravn = Vedomost_VB.BeginSravn.S_pozicii;
            continue;
          case "Ot_symbola":
            sortingUslOne._beginSravn = Vedomost_VB.BeginSravn.Ot_symbola;
            continue;
          case "Ot_symbola_s_konca":
            sortingUslOne._beginSravn = Vedomost_VB.BeginSravn.Ot_symbola_s_konca;
            continue;
          default:
            return (Vedomost_VB.Sorting_Usl_One) null;
        }
      }
      else if (name == "symb_ot")
        sortingUslOne._symb_ot = str;
      else if (name == "num_symb_ot")
        sortingUslOne._num_symb_ot = Convert.ToInt32(str);
      else if (name == "endSravn")
      {
        switch (str)
        {
          case "Skolko":
            sortingUslOne._endSravn = Vedomost_VB.EndSravn.Skolko;
            continue;
          case "Do_symbola":
            sortingUslOne._endSravn = Vedomost_VB.EndSravn.Do_symbola;
            continue;
          case "Do_symbola_s_konca":
            sortingUslOne._endSravn = Vedomost_VB.EndSravn.Do_symbola_s_konca;
            continue;
          case "Do_end":
            sortingUslOne._endSravn = Vedomost_VB.EndSravn.Do_end;
            continue;
          default:
            return (Vedomost_VB.Sorting_Usl_One) null;
        }
      }
      else
      {
        if (name == "symb_do")
          sortingUslOne._symb_do = str;
        if (name == "num_symb_do")
          sortingUslOne._num_symb_do = Convert.ToInt32(str);
        if (name == "sravnenie")
        {
          switch (str)
          {
            case "Symbol":
              sortingUslOne._sravnenie = Vedomost_VB.Sravnenie.Symbol;
              continue;
            case "Number":
              sortingUslOne._sravnenie = Vedomost_VB.Sravnenie.Number;
              continue;
            default:
              return (Vedomost_VB.Sorting_Usl_One) null;
          }
        }
        else if (name == "poriadokSortirovki")
        {
          switch (str)
          {
            case "Vozrastanie":
              sortingUslOne._poriadokSortirovki = Vedomost_VB.PoriadokSortirovki.Vozrastanie;
              continue;
            case "Ubyvanie":
              sortingUslOne._poriadokSortirovki = Vedomost_VB.PoriadokSortirovki.Ubyvanie;
              continue;
            default:
              return (Vedomost_VB.Sorting_Usl_One) null;
          }
        }
        else if (name == "pustyeStroki")
        {
          switch (str)
          {
            case "Vnathale":
              sortingUslOne._pustyeStroki = Vedomost_VB.PustyeStroki.Vnathale;
              continue;
            case "Vkonce":
              sortingUslOne._pustyeStroki = Vedomost_VB.PustyeStroki.Vkonce;
              continue;
            default:
              return (Vedomost_VB.Sorting_Usl_One) null;
          }
        }
      }
    }
    return sortingUslOne;
  }

  public XmlElement Xml_Sorting_Usl_Doc(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "SORTING_USL_DOC", string.Empty);
    if (this._sorting_Usl_Doc._list_sorting_Usl_Doc != null)
    {
      for (int index = 0; index < this._sorting_Usl_Doc._list_sorting_Usl_Doc.Count; ++index)
      {
        Vedomost_VB.Sorting_Usl_Doc_OneRazdel sorting_Usl_Doc_OneRazdel = this._sorting_Usl_Doc._list_sorting_Usl_Doc[index];
        XmlElement newChild = this.Xml_sorting_Usl_Doc_OneRazdel(xmlDocument, sorting_Usl_Doc_OneRazdel);
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
    }
    return element;
  }

  public XmlElement Xml_sorting_Usl_Doc_OneRazdel(
    XmlDocument xmlDocument,
    Vedomost_VB.Sorting_Usl_Doc_OneRazdel sorting_Usl_Doc_OneRazdel)
  {
    if (sorting_Usl_Doc_OneRazdel == null || sorting_Usl_Doc_OneRazdel._list_sorting_Usl_Doc_OneRazdel == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "Sorting_Usl_Doc_OneRazdel", string.Empty);
    XmlAttribute attribute = xmlDocument.CreateAttribute("razdelNum");
    attribute.Value = sorting_Usl_Doc_OneRazdel._razdelNum.ToString();
    element.Attributes.Append(attribute);
    for (int index = 0; index < sorting_Usl_Doc_OneRazdel._list_sorting_Usl_Doc_OneRazdel.Count; ++index)
    {
      Vedomost_VB.Sorting_Usl_Doc_OneGrafa sorting_Usl_Doc_OneGrafa = sorting_Usl_Doc_OneRazdel._list_sorting_Usl_Doc_OneRazdel[index];
      XmlElement newChild = this.Xml_sorting_Usl_Doc_One(xmlDocument, sorting_Usl_Doc_OneGrafa);
      if (newChild != null)
        element.AppendChild((XmlNode) newChild);
    }
    return element;
  }

  public XmlElement Xml_sorting_Usl_Doc_One(
    XmlDocument xmlDocument,
    Vedomost_VB.Sorting_Usl_Doc_OneGrafa sorting_Usl_Doc_OneGrafa)
  {
    if (sorting_Usl_Doc_OneGrafa == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, "Sorting_Usl_Doc_One", string.Empty);
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("grafa");
    attribute1.Value = sorting_Usl_Doc_OneGrafa._grafa.ToString();
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("beginSravn");
    attribute2.Value = sorting_Usl_Doc_OneGrafa._beginSravn.ToString();
    element.Attributes.Append(attribute2);
    if (sorting_Usl_Doc_OneGrafa._symb_ot != null)
    {
      XmlAttribute attribute3 = xmlDocument.CreateAttribute("symb_ot");
      attribute3.Value = sorting_Usl_Doc_OneGrafa._symb_ot.ToString();
      element.Attributes.Append(attribute3);
    }
    XmlAttribute attribute4 = xmlDocument.CreateAttribute("num_symb_ot");
    attribute4.Value = sorting_Usl_Doc_OneGrafa._num_symb_ot.ToString();
    element.Attributes.Append(attribute4);
    XmlAttribute attribute5 = xmlDocument.CreateAttribute("endSravn");
    attribute5.Value = sorting_Usl_Doc_OneGrafa._endSravn.ToString();
    element.Attributes.Append(attribute5);
    if (sorting_Usl_Doc_OneGrafa._symb_do != null)
    {
      XmlAttribute attribute6 = xmlDocument.CreateAttribute("symb_do");
      attribute6.Value = sorting_Usl_Doc_OneGrafa._symb_do.ToString();
      element.Attributes.Append(attribute6);
    }
    XmlAttribute attribute7 = xmlDocument.CreateAttribute("num_symb_do");
    attribute7.Value = sorting_Usl_Doc_OneGrafa._num_symb_do.ToString();
    element.Attributes.Append(attribute7);
    XmlAttribute attribute8 = xmlDocument.CreateAttribute("sravnenie");
    attribute8.Value = sorting_Usl_Doc_OneGrafa._sravnenie.ToString();
    element.Attributes.Append(attribute8);
    XmlAttribute attribute9 = xmlDocument.CreateAttribute("poriadokSortirovki");
    attribute9.Value = sorting_Usl_Doc_OneGrafa._poriadokSortirovki.ToString();
    element.Attributes.Append(attribute9);
    XmlAttribute attribute10 = xmlDocument.CreateAttribute("pustyeStroki");
    attribute10.Value = sorting_Usl_Doc_OneGrafa._pustyeStroki.ToString();
    element.Attributes.Append(attribute10);
    return element;
  }

  public Vedomost_VB.Sorting_Usl_Doc Sorting_Usl_Doc_ReafFromXml(XmlNode Xml_sorting_Usl_Doc)
  {
    if (Xml_sorting_Usl_Doc == null)
      return (Vedomost_VB.Sorting_Usl_Doc) null;
    Vedomost_VB.Sorting_Usl_Doc sortingUslDoc = new Vedomost_VB.Sorting_Usl_Doc();
    sortingUslDoc._list_sorting_Usl_Doc = new List<Vedomost_VB.Sorting_Usl_Doc_OneRazdel>();
    for (int i = 0; i < Xml_sorting_Usl_Doc.ChildNodes.Count; ++i)
    {
      XmlNode childNode = Xml_sorting_Usl_Doc.ChildNodes[i];
      Vedomost_VB.Sorting_Usl_Doc_OneRazdel sortingUslDocOneRazdel = this.Sorting_Usl_Doc_One_Razdel_ReafFromXml(childNode);
      if (childNode != null)
        sortingUslDoc._list_sorting_Usl_Doc.Add(sortingUslDocOneRazdel);
    }
    return sortingUslDoc;
  }

  public Vedomost_VB.Sorting_Usl_Doc_OneRazdel Sorting_Usl_Doc_One_Razdel_ReafFromXml(
    XmlNode xml_sorting_Usl_Doc_OneRazdel)
  {
    if (xml_sorting_Usl_Doc_OneRazdel == null)
      return (Vedomost_VB.Sorting_Usl_Doc_OneRazdel) null;
    Vedomost_VB.Sorting_Usl_Doc_OneRazdel sortingUslDocOneRazdel = new Vedomost_VB.Sorting_Usl_Doc_OneRazdel();
    sortingUslDocOneRazdel._list_sorting_Usl_Doc_OneRazdel = new List<Vedomost_VB.Sorting_Usl_Doc_OneGrafa>();
    for (int i = 0; i < xml_sorting_Usl_Doc_OneRazdel.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xml_sorting_Usl_Doc_OneRazdel.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "razdelNum")
        sortingUslDocOneRazdel._razdelNum = (long) Convert.ToInt32(str);
    }
    for (int i = 0; i < xml_sorting_Usl_Doc_OneRazdel.ChildNodes.Count; ++i)
    {
      XmlNode childNode = xml_sorting_Usl_Doc_OneRazdel.ChildNodes[i];
      if (!(childNode.Name != "Sorting_Usl_Doc_One"))
      {
        Vedomost_VB.Sorting_Usl_Doc_OneGrafa sortingUslDocOneGrafa = this.Sorting_Usl_Doc_One_ReadFromXml(childNode);
        if (sortingUslDocOneGrafa != null)
          sortingUslDocOneRazdel._list_sorting_Usl_Doc_OneRazdel.Add(sortingUslDocOneGrafa);
      }
    }
    return sortingUslDocOneRazdel;
  }

  public Vedomost_VB.Sorting_Usl_Doc_OneGrafa Sorting_Usl_Doc_One_ReadFromXml(
    XmlNode xml_sorting_Usl_Doc_One_Grafa)
  {
    if (xml_sorting_Usl_Doc_One_Grafa == null)
      return (Vedomost_VB.Sorting_Usl_Doc_OneGrafa) null;
    Vedomost_VB.Sorting_Usl_Doc_OneGrafa sortingUslDocOneGrafa = new Vedomost_VB.Sorting_Usl_Doc_OneGrafa();
    for (int i = 0; i < xml_sorting_Usl_Doc_One_Grafa.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xml_sorting_Usl_Doc_One_Grafa.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      if (name == "grafa")
        sortingUslDocOneGrafa._grafa = str;
      else if (name == "beginSravn")
      {
        switch (str)
        {
          case "S_begin":
            sortingUslDocOneGrafa._beginSravn = Vedomost_VB.BeginSravn.S_begin;
            continue;
          case "S_pozicii":
            sortingUslDocOneGrafa._beginSravn = Vedomost_VB.BeginSravn.S_pozicii;
            continue;
          case "Ot_symbola":
            sortingUslDocOneGrafa._beginSravn = Vedomost_VB.BeginSravn.Ot_symbola;
            continue;
          case "Ot_symbola_s_konca":
            sortingUslDocOneGrafa._beginSravn = Vedomost_VB.BeginSravn.Ot_symbola_s_konca;
            continue;
          default:
            return (Vedomost_VB.Sorting_Usl_Doc_OneGrafa) null;
        }
      }
      else if (name == "symb_ot")
        sortingUslDocOneGrafa._symb_ot = str;
      else if (name == "num_symb_ot")
        sortingUslDocOneGrafa._num_symb_ot = Convert.ToInt32(str);
      else if (name == "endSravn")
      {
        switch (str)
        {
          case "Skolko":
            sortingUslDocOneGrafa._endSravn = Vedomost_VB.EndSravn.Skolko;
            continue;
          case "Do_symbola":
            sortingUslDocOneGrafa._endSravn = Vedomost_VB.EndSravn.Do_symbola;
            continue;
          case "Do_symbola_s_konca":
            sortingUslDocOneGrafa._endSravn = Vedomost_VB.EndSravn.Do_symbola_s_konca;
            continue;
          case "Do_end":
            sortingUslDocOneGrafa._endSravn = Vedomost_VB.EndSravn.Do_end;
            continue;
          default:
            return (Vedomost_VB.Sorting_Usl_Doc_OneGrafa) null;
        }
      }
      else
      {
        if (name == "symb_do")
          sortingUslDocOneGrafa._symb_do = str;
        if (name == "num_symb_do")
          sortingUslDocOneGrafa._num_symb_do = Convert.ToInt32(str);
        if (name == "sravnenie")
        {
          switch (str)
          {
            case "Symbol":
              sortingUslDocOneGrafa._sravnenie = Vedomost_VB.Sravnenie.Symbol;
              continue;
            case "Number":
              sortingUslDocOneGrafa._sravnenie = Vedomost_VB.Sravnenie.Number;
              continue;
            default:
              return (Vedomost_VB.Sorting_Usl_Doc_OneGrafa) null;
          }
        }
        else if (name == "poriadokSortirovki")
        {
          switch (str)
          {
            case "Vozrastanie":
              sortingUslDocOneGrafa._poriadokSortirovki = Vedomost_VB.PoriadokSortirovki.Vozrastanie;
              continue;
            case "Ubyvanie":
              sortingUslDocOneGrafa._poriadokSortirovki = Vedomost_VB.PoriadokSortirovki.Ubyvanie;
              continue;
            default:
              return (Vedomost_VB.Sorting_Usl_Doc_OneGrafa) null;
          }
        }
        else if (name == "pustyeStroki")
        {
          switch (str)
          {
            case "Vnathale":
              sortingUslDocOneGrafa._pustyeStroki = Vedomost_VB.PustyeStroki.Vnathale;
              continue;
            case "Vkonce":
              sortingUslDocOneGrafa._pustyeStroki = Vedomost_VB.PustyeStroki.Vkonce;
              continue;
            default:
              return (Vedomost_VB.Sorting_Usl_Doc_OneGrafa) null;
          }
        }
      }
    }
    return sortingUslDocOneGrafa;
  }

  public Vedomost_VB.Merge_Usl2 Merge_Usl2_ReafFromXml(XmlNode Xml_Merge_Usl2)
  {
    Vedomost_VB.Merge_Usl2 mergeUsl2 = new Vedomost_VB.Merge_Usl2();
    mergeUsl2._list_Merge_Usl2 = new List<Vedomost_VB.Merge_Usl_One>();
    if (Xml_Merge_Usl2 == null || Xml_Merge_Usl2.ChildNodes.Count <= 0)
      return mergeUsl2;
    XmlNode childNode = Xml_Merge_Usl2.ChildNodes[0];
    if (childNode.Name == "LIST_MERGE_USL2")
    {
      for (int i = 0; i < childNode.ChildNodes.Count; ++i)
      {
        Vedomost_VB.Merge_Usl_One mergeUslOne = this.Merge_Usl_One_ReadFromXml(childNode.ChildNodes[i]);
        if (mergeUslOne != null)
          mergeUsl2._list_Merge_Usl2.Add(mergeUslOne);
      }
    }
    return mergeUsl2;
  }

  public Vedomost_VB.Merge_Usl_One Merge_Usl_One_ReadFromXml(XmlNode xml_Merge_Usl_One)
  {
    if (xml_Merge_Usl_One == null)
      return (Vedomost_VB.Merge_Usl_One) null;
    Vedomost_VB.Merge_Usl_One mergeUslOne = new Vedomost_VB.Merge_Usl_One();
    for (int i = 0; i < xml_Merge_Usl_One.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xml_Merge_Usl_One.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      switch (name)
      {
        case "objectType":
          mergeUslOne._objectType = Convert.ToInt32(str);
          break;
        case "typeField":
          switch (str)
          {
            case "ObjectType":
              mergeUslOne._typeField = Vedomost_VB.TypeField.ObjectType;
              continue;
            case "TypeFieldVedRec":
              mergeUslOne._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
              continue;
            default:
              return (Vedomost_VB.Merge_Usl_One) null;
          }
        case "typeFieldVedRec":
          mergeUslOne._typeFieldVedRec = (Vedomost_VB.TypeFieldVedRec) Convert.ToInt32(str);
          break;
      }
    }
    return mergeUslOne;
  }

  /// <summary>Сохранение (вывод) xmlDocument в БАЗЕ</summary>
  /// <param name="xmlDocument"></param>
  /// <param name="vedomostTemplateObjectGuid"></param>
  /// <param name="settingsAttributeGuid"></param>
  /// <returns></returns>
  public bool WriteXmlNastrToBase(
    XmlDocument xmlDocument,
    Guid vedomostTemplateObjectGuid,
    Guid settingsAttributeGuid)
  {
    if (xmlDocument == null || vedomostTemplateObjectGuid.ToString() == "" || settingsAttributeGuid.ToString() == "")
      return false;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(settingsAttributeGuid);
    if (attributeTypeId == -10000)
      return false;
    long objectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(vedomostTemplateObjectGuid, false);
      if (dbObject == null)
        return false;
      objectId = dbObject.ObjectID;
      if (dbObject.GetAttributeByID(attributeTypeId) == null)
      {
        bool flag = false;
        if (dbObject.CheckoutBy == 0L && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
        {
          dbObject = dbObject.CheckOut();
          if (dbObject == null)
            return false;
          flag = true;
        }
        dbObject.Attributes.AddAttribute(attributeTypeId, false);
        if (flag)
          dbObject.CheckIn();
      }
    }
    MemoryStream memoryStream = new MemoryStream();
    try
    {
      xmlDocument.Save((Stream) memoryStream);
      BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, string.Empty);
      new BlobProcWriter(objectId, AttributableElements.Object, attributeTypeId, 0, 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      memoryStream.Position = 0L;
    }
    finally
    {
      memoryStream.Close();
    }
    return true;
  }

  /// <summary> Возможен ли автосбор из спецификации? </summary>
  /// <returns></returns>
  public bool IsAutoSbor()
  {
    return this._list_Usl_Read_From_SP != null && this._list_Usl_Read_From_SP.Count != 0 || this._list_Usl_Read_From_SP_Reference != null && this._list_Usl_Read_From_SP_Reference.Count != 0;
  }
}
