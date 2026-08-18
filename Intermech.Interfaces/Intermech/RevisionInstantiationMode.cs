
// Type: Intermech.RevisionInstantiationMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech
{
    /// <summary>
    /// Режимы конкретизации версии в составе. Указывает ядру, как обрабатывать атрибут связи "Идентификатор версии в составе".
    /// </summary>
    public enum RevisionInstantiationMode
    {
      /// <summary>
      /// По умолчанию - фактический режим конкретизации определяется настройками применяемости связи между типами объектов
      /// </summary>
      /// <remarks>
      /// Если настройки применяемости связи между типами объектов разрешают пользователям задавать конкретизацию связей, то
      /// связь с таким значением атрибута должна обрабатываться как мягко конкретизированная. Во всех остальных случаях
      /// это значение обозначает жесткую конкретизацию связи
      /// </remarks>
      Default,
      /// <summary>Жесткая конкретизация связи</summary>
      Hard,
    }
}
