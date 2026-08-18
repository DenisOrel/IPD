// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.AccessNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class AccessNode(IUserSession session, XmlNode node) : 
  XMLPropertyNode<List<UpdateScriptAccessRight>>(session, node, "F_ACCESS")
{
  protected override void ReadValue(IUserSession session, XmlNode node)
  {
    List<UpdateScriptAccessRight> scriptAccessRightList = new List<UpdateScriptAccessRight>();
    if (node.HasChildNodes)
    {
      foreach (XmlNode childNode in node.ChildNodes)
      {
        if (childNode.Name == "PropValue")
        {
          UpdateScriptAccessRight scriptAccessRight = new UpdateScriptAccessRight();
          if (childNode.Attributes["RightType"] != null && childNode.Attributes["RightType"].Value != string.Empty)
            scriptAccessRight.RightType = Convert.ToInt32(childNode.Attributes["RightType"].Value);
          if (childNode.Attributes["RightID"] != null && childNode.Attributes["RightID"].Value != string.Empty)
            scriptAccessRight.RightID = Convert.ToInt32(childNode.Attributes["RightID"].Value);
          if (childNode.Attributes["UserID"] != null && GuidHelper.IsGuid(childNode.Attributes["UserID"].Value))
            scriptAccessRight.UserID = new Guid(childNode.Attributes["UserID"].Value);
          if (childNode.Attributes["OwnerID"] != null && GuidHelper.IsGuid(childNode.Attributes["OwnerID"].Value))
            scriptAccessRight.OwnerID = new Guid(childNode.Attributes["OwnerID"].Value);
          if (childNode.Attributes["BeginDate"] != null && childNode.Attributes["BeginDate"].Value != string.Empty)
            scriptAccessRight.BeginDate = Convert.ToDateTime(childNode.Attributes["BeginDate"].Value, (IFormatProvider) CultureInfo.InvariantCulture);
          if (childNode.Attributes["EndDate"] != null && childNode.Attributes["EndDate"].Value != string.Empty)
            scriptAccessRight.EndDate = Convert.ToDateTime(childNode.Attributes["EndDate"].Value, (IFormatProvider) CultureInfo.InvariantCulture);
          scriptAccessRightList.Add(scriptAccessRight);
        }
      }
    }
    this.Value = (object) scriptAccessRightList;
  }
}
