// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.SelectObject
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal static class SelectObject
{
  public static QuickObjectInfo SelectObjectOfType(int objTypeId)
  {
    return SelectObject.SelectObjectOfType(MetaDataHelper.GetObjectType(objTypeId));
  }

  public static QuickObjectInfo SelectObjectOfType(Guid objTypeGuid)
  {
    return SelectObject.SelectObjectOfType(MetaDataHelper.GetObjectType(objTypeGuid));
  }

  public static QuickObjectInfo SelectObjectOfType(IMSObjectType objType)
  {
    QuickObjectInfo quickObjectInfo = new QuickObjectInfo();
    if (objType == null)
      return quickObjectInfo;
    long options = 0L + 16777216L /*0x01000000*/ + 256L /*0x0100*/;
    long[] numArray = SelectionWindow.SelectObjects(objType.ObjectName, "", objType.ObjectTypeID, (SelectionOptions) options);
    if (numArray != null && numArray.Length == 1)
    {
      long objectID = numArray[0];
      if (objectID != 0L)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          if (session == null)
            return quickObjectInfo;
          quickObjectInfo = session.GetObjectInfo(objectID);
        }
      }
    }
    return quickObjectInfo;
  }
}
