
// Type: Intermech.Interfaces.AttributeAlias
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура для хранения алиаса атрибута. Юзается при получении атрибута по одному из его
    /// идентификаторов (имени, guid-у, алиасу...)
    /// </summary>
    /// <summary>Создать экземпляр структуры</summary>
    /// <param name="alias">Псевдоним атрибута</param>
    public struct AttributeAlias(string alias)
    {
      /// <summary>Псевдоним атрибута</summary>
      public string Alias = alias;

      /// <summary>Псевдоним атрибута</summary>
      /// <returns>Псевдоним атрибута</returns>
      public override string ToString() => this.Alias;
    }
}
