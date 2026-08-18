
// Type: Intermech.Interfaces.ConfigParamKeyEx
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс с параметрами конфигурации с признаком юзерские они или общие (для клиентского кэша)
    /// </summary>
    public class ConfigParamKeyEx : ConfigParamKey
    {
      /// <summary>Является ли параметр общим</summary>
      public bool IsCommonParam { get; private set; }

      public ConfigParamKeyEx(string moduleName, string sectionName, string paramName, bool isCommon)
        : base(moduleName, sectionName, paramName)
      {
        this.IsCommonParam = isCommon;
      }

      public override bool Equals(object obj)
      {
        if (!(obj is ConfigParamKeyEx))
          return false;
        ConfigParamKeyEx configParamKeyEx = obj as ConfigParamKeyEx;
        return this.IsCommonParam == configParamKeyEx.IsCommonParam && this.ModuleName == configParamKeyEx.ModuleName && this.ParamName == configParamKeyEx.ParamName && this.SectionName == configParamKeyEx.SectionName;
      }

      public override int GetHashCode()
      {
        return this.ModuleName.GetHashCode() ^ this.ParamName.GetHashCode() ^ this.SectionName.GetHashCode() ^ this.IsCommonParam.GetHashCode();
      }
    }
}
