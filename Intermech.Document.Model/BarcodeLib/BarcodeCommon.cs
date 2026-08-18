// Decompiled with JetBrains decompiler
// Type: BarcodeLib.BarcodeCommon
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace BarcodeLib;

internal abstract class BarcodeCommon
{
  protected string Raw_Data = "";
  protected List<string> _Errors = new List<string>();

  public string RawData => this.Raw_Data;

  public List<string> Errors => this._Errors;

  public void Error(string ErrorMessage)
  {
    this._Errors.Add(ErrorMessage);
    throw new Exception(ErrorMessage);
  }

  internal static bool CheckNumericOnly(string Data)
  {
    long result = 0;
    if (Data == null)
      return false;
    if (long.TryParse(Data, out result))
      return true;
    int num1 = 18;
    string str = Data;
    string[] strArray = new string[Data.Length / num1 + (Data.Length % num1 == 0 ? 0 : 1)];
    int num2 = 0;
    while (num2 < strArray.Length)
    {
      if (str.Length >= num1)
      {
        strArray[num2++] = str.Substring(0, num1);
        str = str.Substring(num1);
      }
      else
        strArray[num2++] = str.Substring(0);
    }
    foreach (string s in strArray)
    {
      if (!long.TryParse(s, out result))
        return false;
    }
    return true;
  }
}
