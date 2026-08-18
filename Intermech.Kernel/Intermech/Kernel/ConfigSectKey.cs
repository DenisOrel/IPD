// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ConfigSectKey
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel;

internal class ConfigSectKey
{
  public string ModuleName { get; private set; }

  public string SectionName { get; private set; }

  public ConfigSectKey(string moduleName, string sectionName)
  {
    this.ModuleName = moduleName;
    this.SectionName = sectionName;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is ConfigSectKey))
      return false;
    ConfigSectKey configSectKey = obj as ConfigSectKey;
    return this.SectionName == configSectKey.SectionName && this.ModuleName == configSectKey.ModuleName;
  }

  public override int GetHashCode()
  {
    return this.ModuleName.GetHashCode() ^ this.SectionName.GetHashCode();
  }
}
