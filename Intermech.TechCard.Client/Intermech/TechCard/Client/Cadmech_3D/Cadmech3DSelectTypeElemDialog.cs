// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.Cadmech3DSelectTypeElemDialog
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Interfaces.Compositions;
using Intermech.TechCard.Client.UI.Forms;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// 
/// </summary>
internal class Cadmech3DSelectTypeElemDialog
{
  /// <summary>Вызов диалога выбора типовых элементов модели</summary>
  /// <param name="modelObjInfo"></param>
  /// <param name="imCadAttrMgr"></param>
  /// <param name="selectedElems"></param>
  /// <returns></returns>
  public static bool ShowDialog(
    ObjInfoItem modelObjInfo,
    IMTextAttributeManagerProxy imCadAttrMgr,
    out IMTextFaceAttributeProxy[] selectedElems)
  {
    selectedElems = (IMTextFaceAttributeProxy[]) null;
    using (Cadmech3DSelectTypeElemForm techContrl = new Cadmech3DSelectTypeElemForm(modelObjInfo))
    {
      techContrl.LoadTypeElemInfo(imCadAttrMgr);
      TechCardFormUtils.LoadSettings((Control) techContrl, TechCardFormUtils.Mode.All);
      int num = (int) techContrl.ShowDialog();
      TechCardFormUtils.SaveSettings((Control) techContrl, TechCardFormUtils.Mode.All);
      if (num != 1)
        return false;
      selectedElems = techContrl.SelectedElems;
      return true;
    }
  }
}
