// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.List_Element_Accord_Avs6_Ips
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.AVS.Victor;

public static class List_Element_Accord_Avs6_Ips
{
  public static XmlDocument _xmlDocument;
  public static int isAvs6 = 0;
  public static string fileIni6 = "";
  public static bool isPopytkaInits = false;
  public static List<Element_Accord_Avs6_Ips> list_Element_Accord_Avs6_Ips = new List<Element_Accord_Avs6_Ips>();

  /// Находим путь к папке с настройкаи и имя главного файла настроек
  ///             Чтение файла настроек AVS6Main.ini
  ///             Получаем ВСЕ списки полей AVS6
  ///             Получаем секцию DOC
  ///             Получаем обработанный список документов _list_ElDocList_Processed
  public static void Begin()
  {
    Vedomost_VB_Static.Begin_For_Avs6();
    if (Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr != null)
    {
      for (int index = 0; index < Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr.Count; ++index)
      {
        One_ImsObjectType_With_One_Ved_Nastr typeWithOneVedNastr = Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr[index];
        List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Add(new Element_Accord_Avs6_Ips()
        {
          Ips_Ims_ObjectName = typeWithOneVedNastr.imsObjectType.ObjectName,
          Ips_Ims_Guid = typeWithOneVedNastr.imsObjectType.Guid.ToString(),
          TypeDoc = Vedomost_VB.TypeDoc.Ved
        });
      }
    }
    if (Vedomost_VB_Static._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr != null)
    {
      for (int index = 0; index < Vedomost_VB_Static._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr.Count; ++index)
      {
        One_ImsObjectType_With_One_Ved_Nastr typeWithOneVedNastr = Vedomost_VB_Static._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr[index];
        List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Add(new Element_Accord_Avs6_Ips()
        {
          Ips_Ims_ObjectName = typeWithOneVedNastr.imsObjectType.ObjectName,
          Ips_Ims_Guid = typeWithOneVedNastr.imsObjectType.Guid.ToString(),
          TypeDoc = Vedomost_VB.TypeDoc.Tabl
        });
      }
    }
    for (int index = 0; index < List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Count; ++index)
    {
      Element_Accord_Avs6_Ips elementAccordAvs6Ip = List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips[index];
      switch (elementAccordAvs6Ip.Ips_Ims_ObjectName)
      {
        case "Ведомость спецификаций":
          elementAccordAvs6Ip.Avs6_Sysnumber = 18;
          elementAccordAvs6Ip.Avs6_Comment = "Ведомость спецификаций";
          elementAccordAvs6Ip.Avs6_FileType = "VS";
          break;
        case "Ведомость покупных изделий":
          elementAccordAvs6Ip.Avs6_Sysnumber = 19;
          elementAccordAvs6Ip.Avs6_Comment = "Ведомость покупных изделий";
          elementAccordAvs6Ip.Avs6_FileType = "VP";
          break;
        case "Общая спецификация (РСП)":
          elementAccordAvs6Ip.Avs6_Sysnumber = 20;
          elementAccordAvs6Ip.Avs6_Comment = "Ведомость Общая спецификация";
          elementAccordAvs6Ip.Avs6_FileType = "RS";
          break;
        case "Ведомость состава изделия":
          elementAccordAvs6Ip.Avs6_Sysnumber = 21;
          elementAccordAvs6Ip.Avs6_Comment = "Ведомость состава изделия";
          elementAccordAvs6Ip.Avs6_FileType = "VY";
          break;
        case "Таблица соединений":
          elementAccordAvs6Ip.Avs6_Sysnumber = 27;
          elementAccordAvs6Ip.Avs6_Comment = "Таблица соединений (развернутая)";
          elementAccordAvs6Ip.Avs6_FileType = "PA";
          break;
        case "Таблица соединений (сжатая)":
          elementAccordAvs6Ip.Avs6_Sysnumber = 28;
          elementAccordAvs6Ip.Avs6_Comment = "Таблица соединений (сжатая)";
          elementAccordAvs6Ip.Avs6_FileType = "PB";
          break;
      }
    }
    List_Element_Accord_Avs6_Ips.Dopoln_list_Element_Accord_Avs6_Ips(List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips);
    List_Element_Accord_Avs6_Ips.isPopytkaInits = true;
  }

  /// <summary> Default Список соответствия документов Avs6_Ips </summary>
  /// <returns></returns>
  public static List<Element_Accord_Avs6_Ips> Default()
  {
    List<Element_Accord_Avs6_Ips> list_Element_Accord_Avs6_Ips1 = new List<Element_Accord_Avs6_Ips>();
    Element_Accord_Avs6_Ips elementAccordAvs6Ips = new Element_Accord_Avs6_Ips();
    list_Element_Accord_Avs6_Ips1.Add(new Element_Accord_Avs6_Ips()
    {
      Avs6_Comment = "Ведомость спецификаций",
      Avs6_FileType = "VS",
      Avs6_GuidSysnumber = "",
      Avs6_Sysnumber = 18,
      Ips_Ims_Guid = "cad0082b-306c-11d8-b4e9-00304f19f545",
      Ips_Ims_ObjectName = "Ведомость спецификаций",
      TypeDoc = Vedomost_VB.TypeDoc.Ved
    });
    list_Element_Accord_Avs6_Ips1.Add(new Element_Accord_Avs6_Ips()
    {
      Avs6_Comment = "Ведомость покупных изделий",
      Avs6_FileType = "VP",
      Avs6_GuidSysnumber = "",
      Avs6_Sysnumber = 19,
      Ips_Ims_Guid = "cad00826-306c-11d8-b4e9-00304f19f545",
      Ips_Ims_ObjectName = "Ведомость покупных изделий",
      TypeDoc = Vedomost_VB.TypeDoc.Ved
    });
    list_Element_Accord_Avs6_Ips1.Add(new Element_Accord_Avs6_Ips()
    {
      Avs6_Comment = "",
      Avs6_FileType = "",
      Avs6_GuidSysnumber = "",
      Avs6_Sysnumber = -1,
      Ips_Ims_Guid = "cad0029d-306c-11d8-b4e9-00304f19f545",
      Ips_Ims_ObjectName = "Ведомость держателей подлинников",
      TypeDoc = Vedomost_VB.TypeDoc.Ved
    });
    list_Element_Accord_Avs6_Ips1.Add(new Element_Accord_Avs6_Ips()
    {
      Avs6_Comment = "",
      Avs6_FileType = "",
      Avs6_GuidSysnumber = "",
      Avs6_Sysnumber = -1,
      Ips_Ims_Guid = "cadd9a20-306c-11d8-b4e9-00304f19f545",
      Ips_Ims_ObjectName = "Ведомость держателей подлинников (экспорт)",
      TypeDoc = Vedomost_VB.TypeDoc.Ved
    });
    list_Element_Accord_Avs6_Ips1.Add(new Element_Accord_Avs6_Ips()
    {
      Avs6_Comment = "Ведомость состава изделия",
      Avs6_FileType = "VY",
      Avs6_GuidSysnumber = "",
      Avs6_Sysnumber = 21,
      Ips_Ims_Guid = "cadd99bd-306c-11d8-b4e9-00304f19f545",
      Ips_Ims_ObjectName = "Ведомость состава изделия",
      TypeDoc = Vedomost_VB.TypeDoc.Ved
    });
    list_Element_Accord_Avs6_Ips1.Add(new Element_Accord_Avs6_Ips()
    {
      Avs6_Comment = "",
      Avs6_FileType = "",
      Avs6_GuidSysnumber = "",
      Avs6_Sysnumber = -1,
      Ips_Ims_Guid = "cad00295-306c-11d8-b4e9-00304f19f545",
      Ips_Ims_ObjectName = "Ведомость ссылочных документов",
      TypeDoc = Vedomost_VB.TypeDoc.Ved
    });
    list_Element_Accord_Avs6_Ips1.Add(new Element_Accord_Avs6_Ips()
    {
      Avs6_Comment = "",
      Avs6_FileType = "",
      Avs6_GuidSysnumber = "",
      Avs6_Sysnumber = -1,
      Ips_Ims_Guid = "cadd9a21-306c-11d8-b4e9-00304f19f545",
      Ips_Ims_ObjectName = "Ведомость ссылочных документов (экспорт)",
      TypeDoc = Vedomost_VB.TypeDoc.Ved
    });
    list_Element_Accord_Avs6_Ips1.Add(new Element_Accord_Avs6_Ips()
    {
      Avs6_Comment = "Ведомость Общая спецификация",
      Avs6_FileType = "RS",
      Avs6_GuidSysnumber = "",
      Avs6_Sysnumber = 20,
      Ips_Ims_Guid = "cadd93cc-306c-11d8-b4e9-00304f19f545",
      Ips_Ims_ObjectName = "Общая спецификация (РСП)",
      TypeDoc = Vedomost_VB.TypeDoc.Ved
    });
    list_Element_Accord_Avs6_Ips1.Add(new Element_Accord_Avs6_Ips()
    {
      Avs6_Comment = "Таблица соединений (развернутая)",
      Avs6_FileType = "PA",
      Avs6_GuidSysnumber = "",
      Avs6_Sysnumber = 27,
      Ips_Ims_Guid = "cadd9a4a-306c-11d8-b4e9-00304f19f545",
      Ips_Ims_ObjectName = "Таблица соединений",
      TypeDoc = Vedomost_VB.TypeDoc.Tabl
    });
    list_Element_Accord_Avs6_Ips1.Add(new Element_Accord_Avs6_Ips()
    {
      Avs6_Comment = "Таблица соединений (сжатая)",
      Avs6_FileType = "PB",
      Avs6_GuidSysnumber = "",
      Avs6_Sysnumber = 28,
      Ips_Ims_Guid = "cadd9a92-306c-11d8-b4e9-00304f19f545",
      Ips_Ims_ObjectName = "Таблица соединений (сжатая)",
      TypeDoc = Vedomost_VB.TypeDoc.Tabl
    });
    List_Element_Accord_Avs6_Ips.Dopoln_list_Element_Accord_Avs6_Ips(list_Element_Accord_Avs6_Ips1);
    return list_Element_Accord_Avs6_Ips1;
  }

  public static void Dopoln_list_Element_Accord_Avs6_Ips(
    List<Element_Accord_Avs6_Ips> list_Element_Accord_Avs6_Ips1)
  {
    Guid guid;
    if (Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr != null)
    {
      for (int index = 0; index < Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr.Count; ++index)
      {
        One_ImsObjectType_With_One_Ved_Nastr one_ImsObjectType_With_One_Ved_Nastr = Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr[index];
        if (!List_Element_Accord_Avs6_Ips.Find_To_list_Element_Accord_Avs6_Ips(one_ImsObjectType_With_One_Ved_Nastr, list_Element_Accord_Avs6_Ips1))
        {
          Element_Accord_Avs6_Ips elementAccordAvs6Ips1 = new Element_Accord_Avs6_Ips();
          elementAccordAvs6Ips1.Ips_Ims_ObjectName = one_ImsObjectType_With_One_Ved_Nastr.imsObjectType.ObjectName;
          Element_Accord_Avs6_Ips elementAccordAvs6Ips2 = elementAccordAvs6Ips1;
          guid = one_ImsObjectType_With_One_Ved_Nastr.imsObjectType.Guid;
          string str = guid.ToString();
          elementAccordAvs6Ips2.Ips_Ims_Guid = str;
          elementAccordAvs6Ips1.TypeDoc = Vedomost_VB.TypeDoc.Ved;
          list_Element_Accord_Avs6_Ips1.Add(elementAccordAvs6Ips1);
        }
      }
    }
    if (Vedomost_VB_Static._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr == null)
      return;
    for (int index = 0; index < Vedomost_VB_Static._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr.Count; ++index)
    {
      One_ImsObjectType_With_One_Ved_Nastr one_ImsObjectType_With_One_Ved_Nastr = Vedomost_VB_Static._list_Tabl_Arbeit_ImsObjectType_With_One_Ved_Nastr[index];
      if (!List_Element_Accord_Avs6_Ips.Find_To_list_Element_Accord_Avs6_Ips(one_ImsObjectType_With_One_Ved_Nastr, list_Element_Accord_Avs6_Ips1))
      {
        Element_Accord_Avs6_Ips elementAccordAvs6Ips3 = new Element_Accord_Avs6_Ips();
        elementAccordAvs6Ips3.Ips_Ims_ObjectName = one_ImsObjectType_With_One_Ved_Nastr.imsObjectType.ObjectName;
        Element_Accord_Avs6_Ips elementAccordAvs6Ips4 = elementAccordAvs6Ips3;
        guid = one_ImsObjectType_With_One_Ved_Nastr.imsObjectType.Guid;
        string str = guid.ToString();
        elementAccordAvs6Ips4.Ips_Ims_Guid = str;
        elementAccordAvs6Ips3.TypeDoc = Vedomost_VB.TypeDoc.Tabl;
        List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Add(elementAccordAvs6Ips3);
      }
    }
  }

  public static bool Find_To_list_Element_Accord_Avs6_Ips(
    One_ImsObjectType_With_One_Ved_Nastr one_ImsObjectType_With_One_Ved_Nastr,
    List<Element_Accord_Avs6_Ips> list_Element_Accord_Avs6_Ips)
  {
    if (one_ImsObjectType_With_One_Ved_Nastr == null || list_Element_Accord_Avs6_Ips == null)
      return false;
    for (int index = 0; index < list_Element_Accord_Avs6_Ips.Count; ++index)
    {
      Element_Accord_Avs6_Ips elementAccordAvs6Ip = list_Element_Accord_Avs6_Ips[index];
      if (one_ImsObjectType_With_One_Ved_Nastr.imsObjectType.ObjectName == elementAccordAvs6Ip.Ips_Ims_ObjectName)
        return true;
    }
    return false;
  }

  public static bool Read_From_Base()
  {
    XmlDocument xmlDocument = Vedomost_VB_Static.Read_List_Element_Accord_Avs6_Ips_FromBase();
    return xmlDocument != null && List_Element_Accord_Avs6_Ips.Filled_FromXml(xmlDocument);
  }

  public static bool Write_To_Base()
  {
    bool flag = false;
    XmlDocument xmlDocument = List_Element_Accord_Avs6_Ips.XmlDocument_create();
    if (xmlDocument != null)
      flag = Vedomost_VB_Static.Write_List_Element_Accord_Avs6_Ips_ToBase(xmlDocument);
    return flag;
  }

  public static XmlDocument XmlDocument_create()
  {
    List_Element_Accord_Avs6_Ips._xmlDocument = new XmlDocument();
    XmlDeclaration xmlDeclaration = List_Element_Accord_Avs6_Ips._xmlDocument.CreateXmlDeclaration("1.0", "windows-1251", "yes");
    XmlElement documentElement = List_Element_Accord_Avs6_Ips._xmlDocument.DocumentElement;
    List_Element_Accord_Avs6_Ips._xmlDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) documentElement);
    XmlElement element = List_Element_Accord_Avs6_Ips._xmlDocument.CreateElement(string.Empty, "List_Accord_Avs6_Ips", string.Empty);
    XmlAttribute attribute1 = List_Element_Accord_Avs6_Ips._xmlDocument.CreateAttribute("isAvs6");
    attribute1.Value = List_Element_Accord_Avs6_Ips.isAvs6.ToString();
    element.Attributes.Append(attribute1);
    if (!string.IsNullOrEmpty(List_Element_Accord_Avs6_Ips.fileIni6))
    {
      XmlAttribute attribute2 = List_Element_Accord_Avs6_Ips._xmlDocument.CreateAttribute("fileIni6");
      attribute2.Value = List_Element_Accord_Avs6_Ips.fileIni6;
      element.Attributes.Append(attribute2);
    }
    for (int index = 0; index < List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Count; ++index)
    {
      Element_Accord_Avs6_Ips elementAccordAvs6Ip = List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips[index];
      XmlElement newChild = List_Element_Accord_Avs6_Ips.Xml_Element_Accord_Avs6_Ips(List_Element_Accord_Avs6_Ips._xmlDocument, "Element_Accord_Avs6", elementAccordAvs6Ip);
      if (newChild != null)
        element.AppendChild((XmlNode) newChild);
    }
    if (element != null)
      List_Element_Accord_Avs6_Ips._xmlDocument.AppendChild((XmlNode) element);
    return List_Element_Accord_Avs6_Ips._xmlDocument;
  }

  public static XmlElement Xml_Element_Accord_Avs6_Ips(
    XmlDocument xmlDocument,
    string name,
    Element_Accord_Avs6_Ips element_Accord_Avs6_Ips)
  {
    if (xmlDocument == null || element_Accord_Avs6_Ips == null || string.IsNullOrEmpty(name))
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, name, string.Empty);
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("Avs6_Comment");
    attribute1.Value = element_Accord_Avs6_Ips.Avs6_Comment;
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("Avs6_Sysnumber");
    attribute2.Value = element_Accord_Avs6_Ips.Avs6_Sysnumber.ToString();
    element.Attributes.Append(attribute2);
    XmlAttribute attribute3 = xmlDocument.CreateAttribute("Avs6_GuidSysnumber");
    attribute3.Value = element_Accord_Avs6_Ips.Avs6_GuidSysnumber;
    element.Attributes.Append(attribute3);
    XmlAttribute attribute4 = xmlDocument.CreateAttribute("Avs6_FileType");
    attribute4.Value = element_Accord_Avs6_Ips.Avs6_FileType;
    element.Attributes.Append(attribute4);
    XmlAttribute attribute5 = xmlDocument.CreateAttribute("Ips_Ims_ObjectName");
    attribute5.Value = element_Accord_Avs6_Ips.Ips_Ims_ObjectName;
    element.Attributes.Append(attribute5);
    XmlAttribute attribute6 = xmlDocument.CreateAttribute("Ips_Ims_Guid");
    attribute6.Value = element_Accord_Avs6_Ips.Ips_Ims_Guid;
    element.Attributes.Append(attribute6);
    XmlAttribute attribute7 = xmlDocument.CreateAttribute("TypeDoc");
    attribute7.Value = element_Accord_Avs6_Ips.TypeDoc.ToString();
    element.Attributes.Append(attribute7);
    return element;
  }

  public static bool Filled_FromXml(XmlDocument xmlDocument)
  {
    if (xmlDocument == null)
      return false;
    List_Element_Accord_Avs6_Ips.isAvs6 = 0;
    List_Element_Accord_Avs6_Ips.fileIni6 = "";
    if (xmlDocument.DocumentElement.Name != "List_Accord_Avs6_Ips")
      return false;
    if (List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips == null)
      List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips = new List<Element_Accord_Avs6_Ips>();
    else
      List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Clear();
    for (int i = 0; i < xmlDocument.DocumentElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlDocument.DocumentElement.Attributes[i];
      string name = attribute.Name;
      string s = attribute.Value.ToString();
      switch (name)
      {
        case "isAvs6":
          List_Element_Accord_Avs6_Ips.isAvs6 = int.Parse(s);
          break;
        case "fileIni6":
          List_Element_Accord_Avs6_Ips.fileIni6 = s;
          break;
      }
    }
    foreach (XmlElement childNode in xmlDocument.DocumentElement.ChildNodes)
    {
      string name = childNode.Name;
      if (childNode.Name == "Element_Accord_Avs6")
      {
        Element_Accord_Avs6_Ips elementAccordAvs6Ips = List_Element_Accord_Avs6_Ips.Element_Accord_Avs6_Ips_ReadFromXml(xmlDocument, childNode);
        if (elementAccordAvs6Ips != null)
          List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Add(elementAccordAvs6Ips);
      }
    }
    return true;
  }

  public static Element_Accord_Avs6_Ips Element_Accord_Avs6_Ips_ReadFromXml(
    XmlDocument xmlDocument,
    XmlElement xmlElement)
  {
    if (xmlElement == null)
      return (Element_Accord_Avs6_Ips) null;
    Element_Accord_Avs6_Ips elementAccordAvs6Ips = new Element_Accord_Avs6_Ips();
    for (int i = 0; i < xmlElement.Attributes.Count; ++i)
    {
      XmlAttribute attribute = xmlElement.Attributes[i];
      string name = attribute.Name;
      string str = attribute.Value.ToString();
      switch (name)
      {
        case "Avs6_Comment":
          elementAccordAvs6Ips.Avs6_Comment = str;
          break;
        case "Avs6_FileType":
          elementAccordAvs6Ips.Avs6_FileType = str;
          break;
        case "Avs6_GuidSysnumber":
          elementAccordAvs6Ips.Avs6_GuidSysnumber = str;
          break;
        case "Avs6_Sysnumber":
          elementAccordAvs6Ips.Avs6_Sysnumber = !(str != "0") ? -1 : (int) Convert.ToInt16(str);
          break;
        case "Ips_Ims_Guid":
          elementAccordAvs6Ips.Ips_Ims_Guid = str;
          break;
        case "Ips_Ims_ObjectName":
          elementAccordAvs6Ips.Ips_Ims_ObjectName = str;
          break;
        case "TypeDoc":
          switch (str)
          {
            case "Undefined":
              elementAccordAvs6Ips.TypeDoc = Vedomost_VB.TypeDoc.Undefined;
              continue;
            case "Tabl":
              elementAccordAvs6Ips.TypeDoc = Vedomost_VB.TypeDoc.Tabl;
              continue;
            case "Ved":
              elementAccordAvs6Ips.TypeDoc = Vedomost_VB.TypeDoc.Ved;
              continue;
            default:
              continue;
          }
      }
    }
    return elementAccordAvs6Ips;
  }

  public static List<Element_Accord_Avs6_Ips> List_Element_Accord_Avs6_Ips_Copy()
  {
    List<Element_Accord_Avs6_Ips> elementAccordAvs6IpsList = new List<Element_Accord_Avs6_Ips>();
    for (int index = 0; index < List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Count; ++index)
    {
      Element_Accord_Avs6_Ips elementAccordAvs6Ip = List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips[index];
      elementAccordAvs6IpsList.Add(new Element_Accord_Avs6_Ips()
      {
        Avs6_Comment = elementAccordAvs6Ip.Avs6_Comment,
        Avs6_FileType = elementAccordAvs6Ip.Avs6_FileType,
        Avs6_GuidSysnumber = elementAccordAvs6Ip.Avs6_GuidSysnumber,
        Avs6_Sysnumber = elementAccordAvs6Ip.Avs6_Sysnumber,
        Ips_Ims_Guid = elementAccordAvs6Ip.Ips_Ims_Guid,
        Ips_Ims_ObjectName = elementAccordAvs6Ip.Ips_Ims_ObjectName,
        TypeDoc = elementAccordAvs6Ip.TypeDoc
      });
    }
    return elementAccordAvs6IpsList;
  }

  public static bool Find(string docAvs6)
  {
    if (string.IsNullOrEmpty(docAvs6))
      return false;
    docAvs6 = docAvs6.ToUpper();
    for (int index = 0; index < List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Count; ++index)
    {
      Element_Accord_Avs6_Ips elementAccordAvs6Ip = List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips[index];
      string upper = elementAccordAvs6Ip.Ips_Ims_ObjectName.ToUpper();
      if (docAvs6 == upper)
        return !string.IsNullOrEmpty(elementAccordAvs6Ip.Avs6_Comment);
    }
    return false;
  }
}
