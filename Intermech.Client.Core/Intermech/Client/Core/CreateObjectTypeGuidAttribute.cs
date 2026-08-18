
// Type: Intermech.Client.Core.CreateObjectTypeGuidAttribute
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core;

internal class CreateObjectTypeGuidAttribute : Attribute
{
  public Guid ObjectTypeGuid = Guid.Empty;

  public CreateObjectTypeGuidAttribute(string objTypeGuid)
  {
    this.ObjectTypeGuid = new Guid(objTypeGuid);
  }
}
