// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CObjectLinkAttributeType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

internal class CObjectLinkAttributeType(ClientSession uSession, int aAttributeID) : 
  CAttributeType(uSession, aAttributeID),
  IDBObjectLinkAttributeType
{
  protected override void DoGetPropertiesStructure(ref AttributeTypeProperties atProperties)
  {
    int[] mdValuesInt = this.GetMDValuesInt("OBJ_LINKS_ID");
    if (mdValuesInt.Length == 0)
      return;
    atProperties.MetadataExtensions[(object) "OBJ_LINKS_ID"] = (object) mdValuesInt;
  }

  public void ValidateObjectType(int objectTypeID)
  {
    this._clientSession.Guard.ValidateCall();
    if (objectTypeID == this._clientSession.IdentHelper.objtypeIncompleteObject)
      return;
    int[] validObjectTypes = this.GetValidObjectTypes();
    if (validObjectTypes.Length == 0)
      return;
    bool flag = false;
    for (int index = 0; index < validObjectTypes.Length; ++index)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(objectTypeID, validObjectTypes[index]))
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      throw new KernelExceptionID(374, (object) this.Name, (object) this._clientSession.GetObjectType(objectTypeID, true).ObjectTypeName);
  }

  public int[] GetValidObjectTypes()
  {
    this._clientSession.Guard.ValidateCall();
    if (this.SizeType <= 0L)
      return this.GetMDValuesInt("OBJ_LINKS_ID");
    return new int[1]{ Convert.ToInt32(this.SizeType) };
  }
}
