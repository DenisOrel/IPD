// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj.TechCardBaseObjectEditUserForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;

/// <summary>Диалог редактирования технологических объектов</summary>
[Obsolete]
public class TechCardBaseObjectEditUserForm : TechCardBaseObjectCreatorUserForm
{
  /// <summary>
  /// 
  /// </summary>
  private void InitData()
  {
    this._objNeedDelete = false;
    this._formControls = (ICollection<long>) null;
    this.Text = LocalizationHolder.rm.GetString(sc_19614.ssp_techcard_19617());
    string str = string.Empty;
    if (this._objID != 0L)
    {
      this.buttonFinish.Enabled = true;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        str = TechCardConsts.Utils.GetObjectString(this._objID, sessionKeeper.Session);
    }
    else
      this.buttonFinish.Enabled = false;
    this.Text = string.Format(this.Text, (object) str, (object) this._objID);
  }

  /// <summary>Конструктор.</summary>
  /// <param name="objTypeId">Тип редактируемого объекта</param>
  /// <param name="objId"> Идентификатор редактируемого объекта</param>
  public TechCardBaseObjectEditUserForm(int objTypeId, long objId)
    : base(objTypeId, objId)
  {
    this.InitData();
  }
}
