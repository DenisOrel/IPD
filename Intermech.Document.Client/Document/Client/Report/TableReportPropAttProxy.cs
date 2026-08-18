// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.TableReportPropAttProxy
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Document.Client.Report;

/// <summary>
/// 
/// </summary>
internal class TableReportPropAttProxy
{
  private char _separator = '|';
  private bool _isValid;
  private bool _resultItem;
  private bool _countItems;
  private bool _rowNumbers;
  private int _rowNumbersColumnWidth = 10;
  private string _rowNumbersColumnCaption = "№п.п.";
  private DatePrintFormats _datePrint;
  private PageNumberPosition _pageNumber;

  public bool IsValid => this._isValid;

  public bool ResultItem => this._resultItem;

  public bool CountItems => this._countItems;

  public DatePrintFormats DatePrint => this._datePrint;

  public PageNumberPosition PageNumber => this._pageNumber;

  public bool RowNumbers => this._rowNumbers;

  public int RowNumbersColumnWidth => this._rowNumbersColumnWidth;

  public string RowNumbersColumnCaption => this._rowNumbersColumnCaption;

  public TableReportPropAttProxy(string value)
  {
    if (value == null || !(value.ToString() != string.Empty))
      return;
    string[] strArray = value.Split(this._separator);
    if (strArray.Length < 4)
      return;
    this._resultItem = Convert.ToBoolean(Convert.ToInt32(strArray[0]));
    this._countItems = Convert.ToBoolean(Convert.ToInt32(strArray[1]));
    this._datePrint = (DatePrintFormats) Convert.ToInt32(strArray[2]);
    this._pageNumber = (PageNumberPosition) Convert.ToInt32(strArray[3]);
    if (strArray.Length >= 5)
      this._rowNumbers = Convert.ToBoolean(Convert.ToInt32(strArray[4]));
    if (strArray.Length == 7)
    {
      this._rowNumbersColumnWidth = Convert.ToInt32(strArray[5]);
      this._rowNumbersColumnCaption = Convert.ToString(strArray[6]);
    }
    this._isValid = true;
  }

  public TableReportPropAttProxy(
    bool resultItem,
    bool countItems,
    DatePrintFormats datePrint,
    PageNumberPosition pageNumber,
    bool rowNumbers,
    int rowNumbersColumnWidth,
    string rowNumbersColumnCaption)
  {
    this._resultItem = resultItem;
    this._countItems = countItems;
    this._datePrint = datePrint;
    this._pageNumber = pageNumber;
    this._rowNumbers = rowNumbers;
    this._rowNumbersColumnWidth = rowNumbersColumnWidth;
    this._rowNumbersColumnCaption = rowNumbersColumnCaption;
    this._isValid = true;
  }

  public override string ToString()
  {
    return !this._isValid ? LocalizationHolder.rm.GetString("Document.Client_43") : LocalizationHolder.rm.GetString("Document.Client_42");
  }

  public override bool Equals(object obj)
  {
    return obj is TableReportPropAttProxy reportPropAttProxy && reportPropAttProxy.ResultItem == this.ResultItem && reportPropAttProxy.CountItems == this.CountItems && reportPropAttProxy.DatePrint == this.DatePrint && reportPropAttProxy.PageNumber == this.PageNumber && reportPropAttProxy.RowNumbers == this.RowNumbers && reportPropAttProxy.RowNumbersColumnWidth == this.RowNumbersColumnWidth && reportPropAttProxy.RowNumbersColumnCaption == this.RowNumbersColumnCaption;
  }

  public override int GetHashCode()
  {
    bool flag = this.ResultItem;
    int hashCode1 = flag.GetHashCode();
    flag = this.CountItems;
    int hashCode2 = flag.GetHashCode();
    int num = hashCode1 ^ hashCode2 ^ this.DatePrint.GetHashCode() ^ this.PageNumber.GetHashCode();
    flag = this.RowNumbers;
    int hashCode3 = flag.GetHashCode();
    return num ^ hashCode3 ^ this.RowNumbersColumnWidth.GetHashCode() ^ this.RowNumbersColumnCaption.GetHashCode();
  }

  public string Value
  {
    get
    {
      return $"{(this.ResultItem ? (object) "1" : (object) "0")}|{(this.CountItems ? (object) "1" : (object) "0")}|{Convert.ToInt32((object) this._datePrint)}|{Convert.ToInt32((object) this._pageNumber)}|{(this.RowNumbers ? (object) "1" : (object) "0")}|{this.RowNumbersColumnWidth}|{this.RowNumbersColumnCaption}";
    }
  }
}
