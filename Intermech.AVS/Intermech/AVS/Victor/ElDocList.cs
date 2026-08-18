// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.ElDocList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS.Victor;

public class ElDocList
{
  public string _fileType;
  public string _comment;
  public string _fileIni;
  public string _kod;
  public string _tree;
  public string _level;
  public int _sysNumber;
  public int _parentSysNumber;
  public string _guidSysNumber;
  public int _typDoc;
  public string _vidDoc;
  public string _title;
  public Vedomost_VB.TypeDoc typeDoc;

  public ElDocList Copy()
  {
    return new ElDocList()
    {
      _fileType = this._fileType,
      _comment = this._comment,
      _fileIni = this._fileIni,
      _kod = this._kod,
      _tree = this._tree,
      _level = this._level,
      _sysNumber = this._sysNumber,
      _parentSysNumber = this._parentSysNumber,
      _guidSysNumber = this._guidSysNumber,
      _typDoc = this._typDoc,
      _vidDoc = this._vidDoc,
      _title = this._title,
      typeDoc = this.typeDoc
    };
  }

  public string Title()
  {
    if (string.IsNullOrEmpty(this._tree))
      return "";
    string tree = this._tree;
    string str1 = "";
    string str2 = "";
    string str3 = "";
    int length1 = tree.IndexOf("#");
    if (length1 > -1)
    {
      str1 = tree.Substring(0, length1);
      string str4 = tree.Substring(length1 + 1);
      if (str4 != "")
      {
        int length2 = str4.IndexOf("#");
        if (length2 > -1)
        {
          str2 = str4.Substring(0, length2);
          string str5 = str4.Substring(length2 + 1);
          if (str5 != "")
          {
            int length3 = str5.IndexOf("#");
            if (length3 > -1)
              str3 = str5.Substring(0, length3);
          }
        }
      }
    }
    string str6;
    if (str2 == "")
      str6 = str1;
    else if (str2.IndexOf(str1) == 0)
      str6 = !(str3 != "") ? str2 : $"{str2} {str3}";
    else if (str3 == "")
      str6 = $"{str1} {str2}";
    else if (str3.IndexOf(str2) == 0)
      str6 = $"{str1} {str3}";
    else
      str6 = $"{str1} {str2} {str3}";
    return str6;
  }
}
