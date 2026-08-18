// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.AVS6_File
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

/// <summary> Прочитанный ФАЙЛ AVS6. Это не только спецификации, но и Ведомости, Перечни и т.д. </summary>
public class AVS6_File
{
  private Stream _stream;
  private string _fileName = "";
  public TypeAvs6Doc _typeAvs6Doc;
  private string _typFile;
  private byte[] _flags;
  public string _designation;
  public string _name;
  public AVSDocumentForm GroupForm;
  public RecordNew _pasport = new RecordNew();
  public RecordNew _titList;
  public RecordNew _utvList;
  public RecordNew _lizm;
  public List<RecordNew> _listRecords = new List<RecordNew>();

  public AVS6_File()
  {
    if (AVS6_From_Avs6Main._list_ElDocList != null && AVS6_From_Avs6Main._list_ElDocList.Count != 0)
      return;
    AVS6_From_Avs6Main.Read();
  }

  /// <summary> Чтение потока из файла формата AVS6 (ведомость, таблица) по его имени </summary>
  /// <param name="fileName1"></param>
  /// <returns></returns>
  public bool Read(string fileName1)
  {
    this._fileName = fileName1 != null && !(fileName1 == "") ? fileName1 : throw new ArgumentNullException("fileName = \"\"");
    if (!File.Exists(this._fileName))
      return false;
    using (Stream stream1 = (Stream) new FileStream(this._fileName, FileMode.Open, FileAccess.Read))
      return this.Read(stream1);
  }

  /// <summary> Чтение из потока </summary>
  /// <param name="stream1"></param>
  public bool Read(Stream stream1, bool pasportOnly = false)
  {
    this._stream = stream1 != null ? stream1 : throw new ArgumentNullException(nameof (stream1));
    using (BinaryReader br = new BinaryReader(this._stream))
    {
      this._typFile = new string(br.ReadChars(4));
      if (this._typFile[0] != 'i')
      {
        int num = (int) MessageBox.Show($"Файл\r\n\r\n{this._fileName}\r\n\r\nне документ AVS6", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      this._flags = br.ReadBytes(3);
      int num1 = (int) br.ReadInt16();
      br.ReadBytes(55);
      if (!this._pasport.Read(br, this._stream))
      {
        int num2 = (int) MessageBox.Show("Ошибка чтения паспорта файла AVS6", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      this.GroupForm = this.GetAVSDocumentForm();
      this._designation = this._pasport.FieldSByType((byte) 1);
      this._name = this._pasport.FieldSByType((byte) 2);
      if (!pasportOnly)
      {
        for (int index = 0; index < num1; ++index)
        {
          RecordNew recordNew = new RecordNew(this._stream);
          if (!recordNew.Read(br, this._stream))
            throw new Exception("Ошибка чтения записи (rec1.Read) файла AVS6");
          switch (recordNew._recordType_Avs6)
          {
            case 'K':
              this._titList = recordNew;
              continue;
            case 'L':
              this._utvList = recordNew;
              continue;
            case 'M':
              continue;
            default:
              this._listRecords.Add(recordNew);
              continue;
          }
        }
      }
    }
    return true;
  }

  /// <summary> Классификация файла (документа) по комментариям в AVS6MAIN_INI </summary>
  /// 
  ///             определяет AVS6file.TypeAvs6Doc и возвращает ElDocList
  public ElDocList Classification_By_Avs6Main()
  {
    if (this._designation == null)
      return (ElDocList) null;
    ElDocList elDocList1 = (ElDocList) null;
    string str1 = this._pasport.FieldSByType((byte) 201);
    string str2 = this._pasport.FieldSByType((byte) 202);
    string str3 = this._pasport.FieldSByType((byte) 203);
    string str4 = Path.GetExtension(this._fileName).ToUpper().Substring(1, 2);
    for (int index = 0; index < AVS6_From_Avs6Main._list_ElDocList_Processed.Count; ++index)
    {
      ElDocList elDocList2 = AVS6_From_Avs6Main._list_ElDocList_Processed[index];
      if (elDocList2._sysNumber > 1 && str1 == elDocList2._sysNumber.ToString() || elDocList2._sysNumber > 1 && str2 == elDocList2._sysNumber.ToString() || str3 != "" && str3 == elDocList2._guidSysNumber || elDocList2._fileType == str4)
      {
        if (elDocList2._tree.IndexOf("Спецификация") == 0)
        {
          if (elDocList2._tree.IndexOf("ЕСКД") > -1)
          {
            if (elDocList2._tree.IndexOf("Комплектации") > -1)
            {
              this._typeAvs6Doc = TypeAvs6Doc.SPCOMPLECTACII;
              elDocList1 = elDocList2;
              break;
            }
            if (elDocList2._tree.IndexOf("Заказа") > -1)
            {
              this._typeAvs6Doc = TypeAvs6Doc.SPZAKAZA;
              elDocList1 = elDocList2;
              break;
            }
            if (this._name.IndexOf("Комплект") == 0)
            {
              this._typeAvs6Doc = TypeAvs6Doc.SPCOMPLEKT;
              elDocList1 = elDocList2;
              break;
            }
            this._typeAvs6Doc = TypeAvs6Doc.SPESKD;
            elDocList1 = elDocList2;
            break;
          }
          if (elDocList2._tree.IndexOf("Автомобильная") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.SPAVTOM;
            elDocList1 = elDocList2;
            break;
          }
          if (elDocList2._tree.IndexOf("Судостроительная") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.SPSUDOSTR;
            elDocList1 = elDocList2;
            break;
          }
          if (elDocList2._tree.IndexOf("Экспортная") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.SPEXPORT;
            elDocList1 = elDocList2;
            break;
          }
          if (elDocList2._tree.IndexOf("ЕСПД") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.SPESPD;
            elDocList1 = elDocList2;
            break;
          }
          this._typeAvs6Doc = TypeAvs6Doc.SPOTHERS;
          elDocList1 = elDocList2;
          break;
        }
        if (elDocList2._tree.IndexOf("Перечень элементов") == 0)
        {
          this._typeAvs6Doc = TypeAvs6Doc.PE;
          elDocList1 = elDocList2;
          break;
        }
        if (elDocList2._tree.IndexOf("Таблица") == 0)
        {
          if (elDocList2._tree.IndexOf("Таблица соединений") > -1)
          {
            if (elDocList2._tree.IndexOf("жатая") > -1)
            {
              this._typeAvs6Doc = TypeAvs6Doc.TablSoedSzataia;
              elDocList1 = elDocList2;
              break;
            }
            if (elDocList2._tree.IndexOf("азвернутая") > -1)
            {
              this._typeAvs6Doc = TypeAvs6Doc.TablSoedRazvernutaia;
              elDocList1 = elDocList2;
              break;
            }
            this._typeAvs6Doc = TypeAvs6Doc.TBSOEDINENIJ;
            elDocList1 = elDocList2;
            break;
          }
          if (elDocList2._tree.IndexOf("аборов зажимов") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.TablNaborZazimov;
            elDocList1 = elDocList2;
            break;
          }
          if (elDocList2._tree.IndexOf("нешних соединений") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.TablVneshnihSoed;
            elDocList1 = elDocList2;
            break;
          }
          this._typeAvs6Doc = TypeAvs6Doc.TBOOTHERS;
          elDocList1 = elDocList2;
          break;
        }
        if (elDocList2._tree.IndexOf("Ведомость") == 0)
        {
          if (elDocList2._tree.IndexOf("Ведомость спецификаций") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.VS;
            elDocList1 = elDocList2;
            break;
          }
          if (elDocList2._tree.IndexOf("Ведомость покупных") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.VP;
            elDocList1 = elDocList2;
            break;
          }
          if (elDocList2._tree.IndexOf("Общая спецификация") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.RS;
            elDocList1 = elDocList2;
            break;
          }
          if (elDocList2._tree.IndexOf("Ведомость состава изделия") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.VY;
            elDocList1 = elDocList2;
            break;
          }
          this._typeAvs6Doc = TypeAvs6Doc.VEDOTHERS;
          elDocList1 = elDocList2;
          break;
        }
        if (elDocList2._tree.IndexOf("Разное") == 0)
        {
          if (elDocList2._tree.IndexOf("Лист утверждения ЕСКД") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.LISTUTVESKD;
            elDocList1 = elDocList2;
            break;
          }
          if (elDocList2._tree.IndexOf("Лист утверждения ЕСПД") > -1)
          {
            this._typeAvs6Doc = TypeAvs6Doc.LISTUTVESPD;
            elDocList1 = elDocList2;
            break;
          }
          this._typeAvs6Doc = TypeAvs6Doc.OTHERS;
        }
      }
    }
    return elDocList1;
  }

  /// <summary> Поиск соответствующей настройки для в настройках IPS </summary>
  /// <returns></returns>
  public Element_Accord_Avs6_Ips Classification_By_list_Element_Accord_Avs6_Ips()
  {
    if (this._designation == null)
      return (Element_Accord_Avs6_Ips) null;
    Element_Accord_Avs6_Ips elementAccordAvs6Ips = (Element_Accord_Avs6_Ips) null;
    string str1 = this._pasport.FieldSByType((byte) 201);
    string str2 = this._pasport.FieldSByType((byte) 202);
    string str3 = this._pasport.FieldSByType((byte) 203);
    string str4 = Path.GetExtension(this._fileName).ToUpper().Substring(1, 2);
    for (int index = 0; index < List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips.Count; ++index)
    {
      Element_Accord_Avs6_Ips elementAccordAvs6Ip = List_Element_Accord_Avs6_Ips.list_Element_Accord_Avs6_Ips[index];
      if (!string.IsNullOrEmpty(str1) && elementAccordAvs6Ip.Avs6_Sysnumber.ToString() == str1 || !string.IsNullOrEmpty(str2) && elementAccordAvs6Ip.Avs6_Sysnumber.ToString() == str2 || elementAccordAvs6Ip.Avs6_FileType == str4)
      {
        elementAccordAvs6Ips = elementAccordAvs6Ip;
        break;
      }
      if (!string.IsNullOrEmpty(str3) && elementAccordAvs6Ip.Avs6_GuidSysnumber.ToString() == str3)
      {
        elementAccordAvs6Ips = elementAccordAvs6Ip;
        break;
      }
    }
    return elementAccordAvs6Ips;
  }

  /// <summary> Определение групповой формы </summary>
  /// <returns></returns>
  private AVSDocumentForm GetAVSDocumentForm()
  {
    AVSDocumentForm avsDocumentForm = AVSDocumentForm.Single;
    string str1 = this._pasport.FieldSByType((byte) 204);
    if (str1 != "")
    {
      switch (str1)
      {
        case "0":
          avsDocumentForm = AVSDocumentForm.Single;
          break;
        case "1":
          avsDocumentForm = AVSDocumentForm.A;
          break;
        case "2":
          avsDocumentForm = AVSDocumentForm.B;
          break;
        case "3":
          avsDocumentForm = AVSDocumentForm.Mirror;
          break;
        case "4":
          avsDocumentForm = AVSDocumentForm.V;
          break;
        case "5":
          avsDocumentForm = AVSDocumentForm.G;
          break;
      }
      return avsDocumentForm;
    }
    string str2 = this._pasport.FieldSByType((byte) 6);
    if (!(str2 != ""))
      return avsDocumentForm;
    switch (str2)
    {
      case "0":
        avsDocumentForm = AVSDocumentForm.Single;
        break;
      case "1":
        avsDocumentForm = AVSDocumentForm.A;
        break;
      case "2":
        avsDocumentForm = AVSDocumentForm.B;
        break;
      case "3":
        avsDocumentForm = AVSDocumentForm.Mirror;
        break;
      case "4":
        avsDocumentForm = AVSDocumentForm.V;
        break;
      case "5":
        avsDocumentForm = AVSDocumentForm.G;
        break;
    }
    return avsDocumentForm;
  }

  /// <summary> Обозначение документа (спецификации) </summary>
  /// <returns></returns>
  public string Desigation() => this._pasport.FieldByType((byte) 1)._fieldText_Avs6;

  /// <summary> Наименование документа (спецификации) </summary>
  /// <returns></returns>
  public string Name() => this._pasport.FieldByType((byte) 2)._fieldText_Avs6;

  /// <summary> Поиск соответствующих objectId в объектах Ips </summary>
  public void Synchronization_With_IPS()
  {
    for (int index1 = 0; index1 < this._listRecords.Count; ++index1)
    {
      RecordNew listRecord = this._listRecords[index1];
      listRecord._articleID = Vedomost_VB_Static.FindArticleID_By_RecordNew(listRecord, true, true);
      if (listRecord._listR2 != null)
      {
        for (int index2 = 0; index2 < listRecord._listR2.Count; ++index2)
        {
          RecordNew recordNew = listRecord._listR2[index2];
          recordNew._articleID = Vedomost_VB_Static.FindArticleID_By_RecordNew(recordNew, false, false);
        }
      }
    }
  }
}
