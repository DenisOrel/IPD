// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.AreaNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Text;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class AreaNode(IUserSession session, XmlNode node, string directory) : 
  XMLPropertyNode<string>(session, node, "F_AREA_ID")
{
  protected override string GetValue(IUserSession session, string nodeAttributeValue)
  {
    if (!string.IsNullOrEmpty(nodeAttributeValue))
    {
      string[] strArray = nodeAttributeValue.Split('|');
      if (strArray.Length != 0)
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(strArray.Length))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          for (int index = 0; index < strArray.Length; ++index)
          {
            if (GuidHelper.IsGuid(strArray[index]))
            {
              IDBSubjectAreaType subjectAreaType = session.GetSubjectAreaType(new Guid(strArray[index]));
              stringBuilder.Append(subjectAreaType.AreaID);
            }
          }
          return stringBuilder.ToString();
        }
      }
    }
    return string.Empty;
  }
}
