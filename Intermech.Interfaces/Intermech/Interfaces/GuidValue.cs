
// Type: Intermech.Interfaces.GuidValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для передачи в провайдеры СУБД значения guid в виде строки (чтобы провайдер не преобразовывал тип данных)
    /// </summary>
    public class GuidValue
    {
      public string GuidStr { get; private set; }

      public GuidValue(object guidVal) => this.GuidStr = guidVal.ToString();
    }
}
