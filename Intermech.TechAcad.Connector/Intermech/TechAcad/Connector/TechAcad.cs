// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcad
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using ImPictEditSrv;
using ImSSP;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TechAcad.Connector;

public class TechAcad
{
  private static IMPictEditClass _pictEdit;

  private static IIMPictEdit GetPictEdit()
  {
    if (Intermech.TechAcad.Connector.TechAcad._pictEdit != null)
    {
      try
      {
        Intermech.TechAcad.Connector.TechAcad._pictEdit.GetServerStatus();
        return (IIMPictEdit) Intermech.TechAcad.Connector.TechAcad._pictEdit;
      }
      catch (Exception ex)
      {
        if (ex is COMException)
          Intermech.TechAcad.Connector.TechAcad._pictEdit = (IMPictEditClass) null;
        else
          throw;
      }
    }
    try
    {
      Intermech.TechAcad.Connector.TechAcad._pictEdit = new IMPictEditClass();
    }
    catch (Exception ex)
    {
      if (ex is COMException)
        throw new COMException(LocalizationHolder.rm.GetString(sc_19140.ssp_techacad_19141()) + ex.Message);
      throw;
    }
    return (IIMPictEdit) Intermech.TechAcad.Connector.TechAcad._pictEdit;
  }

  private static string GetInternalSketchName(string sketchName) => sketchName.Replace("OPR_", "");

  public static int GetServerStatus() => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().GetServerStatus();

  public static int OpenPictureEditor(string exFile, string parameters, string directory)
  {
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().OpenPictureEditor(exFile, parameters, directory);
  }

  public static int ClosePictureEditor() => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().ClosePictureEditor();

  public static int OpenPicture(string fileName, int readOnly)
  {
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().OpenPicture(fileName, readOnly);
  }

  public static int CreatePicture(string fileName, string prototypeName)
  {
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().CreatePicture(fileName, prototypeName);
  }

  public static int Test() => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().Test();

  public static int ShowOper(string dwgName, string layerCode, string nameOper)
  {
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().ShowOper(dwgName, Intermech.TechAcad.Connector.TechAcad.GetInternalSketchName(layerCode), nameOper);
  }

  public static int CopyOper(
    string layerCodeFrom,
    string nameOperFrom,
    string layerCodeTo,
    string nameOper)
  {
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().CopyOper(Intermech.TechAcad.Connector.TechAcad.GetInternalSketchName(layerCodeFrom), nameOperFrom, Intermech.TechAcad.Connector.TechAcad.GetInternalSketchName(layerCodeTo), nameOper);
  }

  public static int CopyOperFrom(
    string dwgFrom,
    string dwgTo,
    List<Tuple<string, string>> layersList)
  {
    object[,] codeList = layersList != null ? new object[2, layersList.Count] : throw new ArgumentNullException(nameof (layersList));
    for (int index = 0; index < layersList.Count; ++index)
    {
      codeList[0, index] = (object) Intermech.TechAcad.Connector.TechAcad.GetInternalSketchName(layersList[index].Item1);
      codeList[1, index] = (object) Intermech.TechAcad.Connector.TechAcad.GetInternalSketchName(layersList[index].Item2);
    }
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().CopyOperFrom(dwgFrom, dwgTo, (object) codeList);
  }

  public static int MoveOperFrom(
    string dwgFrom,
    string dwgTo,
    List<Tuple<string, string>> layersList)
  {
    object[,] codeList = layersList != null ? new object[2, layersList.Count] : throw new ArgumentNullException(nameof (layersList));
    for (int index = 0; index < layersList.Count; ++index)
    {
      codeList[0, index] = (object) Intermech.TechAcad.Connector.TechAcad.GetInternalSketchName(layersList[index].Item1);
      codeList[1, index] = (object) Intermech.TechAcad.Connector.TechAcad.GetInternalSketchName(layersList[index].Item2);
    }
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().MoveOperFrom(dwgFrom, dwgTo, (object) codeList);
  }

  public static int DeleteOper(string dwgName, List<string> layersList)
  {
    object[,] codeList = layersList != null ? new object[2, layersList.Count] : throw new ArgumentNullException(nameof (layersList));
    for (int index = 0; index < layersList.Count; ++index)
    {
      codeList[0, index] = (object) Intermech.TechAcad.Connector.TechAcad.GetInternalSketchName(layersList[index]);
      codeList[1, index] = (object) Intermech.TechAcad.Connector.TechAcad.GetInternalSketchName(layersList[index]);
    }
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().DeleteOper(dwgName, (object) codeList);
  }

  public static int ReplaceDimText(List<Tuple<string, string>> textList)
  {
    object[,] textList1 = textList != null ? new object[2, textList.Count] : throw new ArgumentNullException(nameof (textList));
    for (int index = 0; index < textList.Count; ++index)
    {
      textList1[0, index] = (object) $"[{textList[index].Item1}]";
      textList1[1, index] = (object) textList[index].Item2;
    }
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().ReplaceDimText((object) textList1);
  }

  public static int GetDimText(ref string dimText)
  {
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().GetDimText(ref dimText);
  }

  public static int GetText(ref string text) => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().GetText(ref text);

  public static int SetDraftName(string text) => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().SetDraftName(text);

  public static int GetDraftName(ref string text) => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().GetDraftName(ref text);

  public static int SetDrawingName(string text) => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().SetDrawingName(text);

  public static int GetAcadHWND() => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().GetAcadHWND();

  public static int RestoreACadHWND() => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().RestoreACadHWND();

  public static int SavePicture(string name) => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().SavePicture(name);

  public static int SavePictureAs(string name) => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().SavePictureAs(name);

  public static int GetTechCustomer(string fileName, ref string ttText)
  {
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().GetTechCustomer(fileName, ref ttText);
  }

  public static int SetInterfaceObject(object io) => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().SetInterfaceObject(io);

  public static int SelectStrElem(ref string imbaseCode)
  {
    return Intermech.TechAcad.Connector.TechAcad.GetPictEdit().SelectStdElem(ref imbaseCode);
  }

  public static int ClosePicture(string name) => Intermech.TechAcad.Connector.TechAcad.GetPictEdit().ClosePicture(name);
}
