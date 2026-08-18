// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.Cadmech3DAddSurfaceCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Interfaces.Compositions;
using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// 
/// </summary>
/// <param name="name"></param>
/// <param name="modelLoader"></param>
internal class Cadmech3DAddSurfaceCommand(
  string name,
  Cadmech3DCommand.CadModelLoadDelegate modelLoader = null) : Cadmech3DCommand(name, modelLoader)
{
  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool DoExecuteCadCommand()
  {
    if (this._imTextDoc == null)
      return false;
    IMTextAttributeManagerProxy attrManager = this._imTextDoc.GetAttrManager();
    if (attrManager == null)
      return false;
    this._cadDoc.CADSystem.SwitchToApp();
    object obj;
    try
    {
      obj = attrManager.SelectObject(LocalizationHolder.rm.GetString("TechCard.Client_499"), new IMTextEntityId[1]
      {
        IMTextEntityId.Surface
      });
    }
    finally
    {
      this.SwitchToThisApp();
    }
    if (!(obj is IMTextFaceProxy imTextFaceProxy))
      return false;
    this._createdRelInfoList.AddRange((IEnumerable<RelObjInfoItem>) Cadmech3DUtils.AddObjects_Faces(new IMTextFaceProxy[1]
    {
      imTextFaceProxy
    }, this._selectedObjInfo, (IServiceProvider) this._container));
    return true;
  }
}
