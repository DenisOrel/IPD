// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.DIObjectCreator
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class DIObjectCreator : IObjectCreatorCustomService
{
  public long CreateObjectDialog(
    int aObjectTypeID,
    long protoObjID,
    int[] linkTypesID,
    long[] relatedObjIDs,
    DateTime startRelationTime,
    bool IsVersion)
  {
    if (aObjectTypeID == RevHelper.idObj_DI)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_279"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
      return 0;
    }
    if (aObjectTypeID != RevHelper.idObj_DPI)
      return 0;
    int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_280"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
    return 0;
  }
}
