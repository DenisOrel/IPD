
// Type: Intermech.Interfaces.ConfigParamKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Ключ для параметра конфигурации</summary>
    public class ConfigParamKey
    {
      public string ModuleName { get; private set; }

      public string SectionName { get; private set; }

      public string ParamName { get; private set; }

      public ConfigParamKey(string moduleName, string sectionName, string paramName)
      {
        this.ModuleName = moduleName;
        this.SectionName = sectionName;
        this.ParamName = paramName;
      }

      public override bool Equals(object obj)
      {
        if (!(obj is ConfigParamKey))
          return false;
        ConfigParamKey configParamKey = obj as ConfigParamKey;
        return this.ModuleName == configParamKey.ModuleName && this.ParamName == configParamKey.ParamName && this.SectionName == configParamKey.SectionName;
      }

      public override int GetHashCode()
      {
        return this.ModuleName.GetHashCode() ^ this.ParamName.GetHashCode() ^ this.SectionName.GetHashCode();
      }
    }
}
