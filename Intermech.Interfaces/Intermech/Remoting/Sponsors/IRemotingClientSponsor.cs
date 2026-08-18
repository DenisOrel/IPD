
// Type: Intermech.Remoting.Sponsors.IRemotingClientSponsor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Remoting.Sponsors
{
    /// <summary>Расширенный интерфейс спонсора, используемый в IPS.</summary>
    public interface IRemotingClientSponsor
    {
      /// <summary>
      /// Регистрирует заданный серверный объект для спонсирования.
      /// Если объект не является серверным, то метод завершает выполнение без ошибок.
      /// </summary>
      /// <param name="obj">Серверный объект, для которого требуется продлевать срок жизни</param>
      /// <exception cref="T:System.Exception">При регистрации спонсора произошла ошибка. Возможно, что серверный объект уже был освобожден сервером</exception>
      void Register(object obj);

      /// <summary>
      ///  Отменяет регистрацию заданного серверного объекта для спонсирования.
      ///  Если объект не является серверным, то метод завершает выполнение без ошибок.
      /// </summary>
      /// <param name="obj">Серверный объект, для которого больше не надо продлевать время жизни</param>
      void Unregister(object obj);

      /// <summary>
      /// Отменяет регистрацию всех спонсируемых серверных объектов.
      /// </summary>
      void UnregisterAll();
    }
}
