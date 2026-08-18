// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.FuncData
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// FuncData is used for internal data array (TempFormula.funcs)
/// </summary>
[Serializable]
public class FuncData
{
  public FormulaFunc func;
  public string text;
  public DataType[] parmTypes;
  public DataType result;
  public string description;
  public int info;

  public FuncData(FormulaFunc func, string text, DataType[] parmTypes, DataType result)
  {
    this.func = func;
    this.text = text;
    this.parmTypes = (DataType[]) Array.CreateInstance(typeof (DataType), parmTypes.Length);
    parmTypes.CopyTo((Array) this.parmTypes, 0);
    this.result = result;
    this.description = "";
    this.info = 0;
  }

  public FuncData(FormulaFunc func, string text, string parmTypes, char result, int info)
  {
    this.func = func;
    this.text = text;
    this.result = FuncData.CharToDT(result);
    this.parmTypes = (DataType[]) Array.CreateInstance(typeof (DataType), parmTypes.Length);
    for (int index = 0; index < parmTypes.Length; ++index)
      this.parmTypes[index] = FuncData.CharToDT(parmTypes[index]);
    this.description = "";
    this.info = info;
  }

  public string GetFuncTemplate()
  {
    string str = this.text + "(";
    for (int index = 0; index < this.parmTypes.Length; ++index)
    {
      str += FuncData.DTToChar(this.parmTypes[index]).ToString();
      if (index < this.parmTypes.Length - 1)
        str += ", ";
    }
    return str + ")";
  }

  public static DataType CharToDT(char ch)
  {
    switch (ch)
    {
      case 'A':
        return DataType.Attribute;
      case 'B':
        return DataType.Boolean;
      case 'D':
        return DataType.Date;
      case 'F':
        return DataType.Float;
      case 'I':
        return DataType.Integer;
      case 'M':
        return DataType.Measured;
      case 'O':
        return DataType.ObjectLink;
      case 'P':
        return DataType.Packet;
      case 'Q':
        return DataType.ObjectIdLink;
      case 'R':
        return DataType.RelType;
      case 'S':
        return DataType.String;
      case 'T':
        return DataType.ObjType;
      default:
        throw new FuncData.WrongFormatException();
    }
  }

  public static char DTToChar(DataType dt)
  {
    switch (dt)
    {
      case DataType.Integer:
        return 'I';
      case DataType.Float:
        return 'F';
      case DataType.Measured:
        return 'M';
      case DataType.String:
        return 'S';
      case DataType.Date:
        return 'D';
      case DataType.Boolean:
        return 'B';
      case DataType.ObjectLink:
        return 'O';
      case DataType.Packet:
        return 'P';
      case DataType.Attribute:
        return 'A';
      case DataType.ObjType:
        return 'T';
      case DataType.RelType:
        return 'R';
      case DataType.ObjectIdLink:
        return 'Q';
      default:
        throw new FuncData.WrongFormatException();
    }
  }

  public class WrongFormatException : Exception
  {
  }
}
