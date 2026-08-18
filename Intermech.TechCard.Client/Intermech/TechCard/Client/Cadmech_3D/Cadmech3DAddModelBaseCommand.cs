// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.Cadmech3DAddModelBaseCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// Базовый класс команда добавления типовых элементов указанной модели
/// </summary>
/// <summary>
/// 
/// </summary>
/// <param name="name"></param>
/// <param name="modelLoader"></param>
internal class Cadmech3DAddModelBaseCommand(
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
    IMTextFaceProxy[] allFaces = attrManager.GetAllFaces();
    if (allFaces == null)
      return false;
    ((IEnumerable<IMTextFaceProxy>) allFaces).FirstOrDefault<IMTextFaceProxy>((Func<IMTextFaceProxy, bool>) (item => item.GUID == IMCadConst.Global_Doc_Guid));
    IMTextFaceAttributeProxy[] allFaceAttrsByType = attrManager.GetAllFaceAttrsByType(IMTextFaceAttributeType.Parameter);
    List<IMTextFaceProxy> imParamFaceList = new List<IMTextFaceProxy>();
    if (allFaceAttrsByType != null)
    {
      this._createdRelInfoList.AddRange((IEnumerable<RelObjInfoItem>) Cadmech3DUtils.AddObjects_TemplateFaceAttr(allFaceAttrsByType, this._selectedObjInfo, (IServiceProvider) this._container));
      foreach (IMTextFaceAttributeProxy faceAttributeProxy in allFaceAttrsByType)
      {
        IMTextFaceProxy[] faces = faceAttributeProxy.Faces;
        if (faces != null)
          imParamFaceList.AddRange((IEnumerable<IMTextFaceProxy>) faces);
      }
    }
    IMTextFaceProxy[] array = ((IEnumerable<IMTextFaceProxy>) allFaces).Where<IMTextFaceProxy>((Func<IMTextFaceProxy, bool>) (item => !imParamFaceList.Contains(item))).ToArray<IMTextFaceProxy>();
    if (array.Length != 0)
      this._createdRelInfoList.AddRange((IEnumerable<RelObjInfoItem>) Cadmech3DUtils.AddObjects_Faces(array, this._selectedObjInfo, (IServiceProvider) this._container));
    return true;
  }
}
