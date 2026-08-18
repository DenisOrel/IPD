// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Attributes.DBAttributeLCSecurity
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Attributes;

internal class DBAttributeLCSecurity : DBSessionable, IDBSecurity
{
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(3);

  public DBAttributeLCSecurity(
    UserSession uSession,
    int attributeID,
    int stepID,
    int objectTypeID)
    : base(uSession)
  {
    if (objectTypeID > 1048575 /*0x0FFFFF*/)
      throw new KernelException($"Для проверки прав доступа к атрибутам на шагах ЖЦ применительно к типам объектов идентификатор типа не должен превышать {1048575 /*0x0FFFFF*/}.");
    if (stepID > 1048575 /*0x0FFFFF*/)
      throw new KernelException($"Для проверки прав доступа к атрибутам на шагах ЖЦ применительно к типам объектов идентификатор шага ЖЦ не должен превышать {1048575 /*0x0FFFFF*/}.");
    this.InitSecurityOptions(29, Convert.ToInt64(attributeID) << 40 | Convert.ToInt64(stepID) << 20 | (long) objectTypeID);
  }

  public int AttributeID => Convert.ToInt32(this.CategoryID >> 40 & 16777215L /*0xFFFFFF*/);

  private int LCStepID => Convert.ToInt32(this.CategoryID >> 20 & 1048575L /*0x0FFFFF*/);

  private int ObjectTypeID => Convert.ToInt32(this.CategoryID & 1048575L /*0x0FFFFF*/);

  static DBAttributeLCSecurity()
  {
    DBAttributeLCSecurity.metadataActions.Add(ActionType.GetAccess, false);
    DBAttributeLCSecurity.metadataActions.Add(ActionType.SetAccess, false);
    DBAttributeLCSecurity.metadataActions.Add(ActionType.Write, true);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBAttributeLCSecurity.metadataActions);
  }

  public override string ObjectName
  {
    get
    {
      return $"Атрибут '{MetaDataHelper.GetAttributeTypeName(this.AttributeID)}' на шаге ЖЦ '{MetaDataHelper.GetLCStepName(this.LCStepID)}' применительно к типу объектов '{MetaDataHelper.GetObjectTypeName(this.ObjectTypeID)}'.";
    }
  }
}
