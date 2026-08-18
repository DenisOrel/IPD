
// Type: Intermech.PropertyEditors.SecurityLevelPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

public class SecurityLevelPropertyClass
{
  private int securityLevel;

  public int SecurityLevel => this.securityLevel;

  public SecurityLevelPropertyClass(int aSecLevel) => this.securityLevel = aSecLevel;

  public override string ToString()
  {
    return SecurityLevelHolder.GetDescriptionBySecurityLevel(this.securityLevel);
  }
}
