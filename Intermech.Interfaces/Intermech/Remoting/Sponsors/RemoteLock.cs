
// Type: Intermech.Remoting.Sponsors.RemoteLock
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Remoting.Sponsors
{
    /// <summary>
    /// <para>
    /// Вспомогательный класс для продления времени жизни серверных (remoting) объектов при выполнении длительных операций.
    /// Используeтся вместе с оператором using. Изнутри using объекты можно добавлять методом Add(objToLock).
    /// </para>
    /// <para>
    /// Реализация класса не является thread-safe.
    /// </para>
    /// </summary>
    public sealed class RemoteLock : IDisposable
    {
      private IRemotingClientSponsor sponsor;

      /// <summary>Создает пустой вспомогательный объект.</summary>
      public RemoteLock() => this.sponsor = RemotingClientSponsorService.Default.Factory.Create();

      /// <summary>
      /// Создает вспомогательный объект и включает в него указанный серверный объект. Если этот объект не является серверным,
      /// то метод завершает выполнение без ошибок.
      /// </summary>
      /// <param name="obj">Серверный объект, для которого требуется продлевать срок жизни</param>
      public RemoteLock(object obj)
        : this()
      {
        this.Add(obj);
      }

      /// <summary>
      /// Создает вспомогательный объект и включает в него указанные серверные объекты. Если како-либо из объектов не является серверным,
      /// то метод пропускает его.
      /// </summary>
      /// <param name="objList">Серверные объекты, для которого требуется продлевать срок жизни</param>
      /// <exception cref="T:System.ArgumentNullException">Ссылка на массив серверных объектов не должна быть null</exception>
      public RemoteLock(params object[] objList)
        : this()
      {
        if (objList == null)
          throw new ArgumentNullException(nameof (objList));
        foreach (object objToLock in objList)
          this.Add(objToLock);
      }

      /// <summary>
      /// Отменяет регистрацию всех спонсируемых серверных объектов.
      /// </summary>
      public void Dispose()
      {
        if (this.sponsor == null)
          return;
        this.sponsor.UnregisterAll();
        this.sponsor = (IRemotingClientSponsor) null;
      }

      private void CheckNotDisposed()
      {
        if (this.sponsor == null)
          throw new ObjectDisposedException(nameof (RemoteLock));
      }

      /// <summary>
      /// Регистрирует заданный серверный объект для спонсорства. Если объект не является серверным, то метод завершает выполнение
      /// без ошибок.
      /// </summary>
      /// <param name="objToLock">Серверный объект, для которого требуется продлевать срок жизни</param>
      /// <exception cref="T:System.ObjectDisposedException">Вспомогательный объект разрушен и не может больше использоваться для спонсирования серверных объектов</exception>
      public void Add(object objToLock)
      {
        this.CheckNotDisposed();
        this.sponsor.Register(objToLock);
      }

      /// <summary>
      ///  Отменяет регистрацию заданного серверного объекта для спонсорства. Если объект не является серверным, то метод завершает выполнение
      ///  без ошибок.
      /// </summary>
      /// <param name="objToLock">Серверный объект, для которого больше не надо продлевать время жизни</param>
      /// <exception cref="T:System.ObjectDisposedException">Вспомогательный объект разрушен и не может больше использоваться для спонсирования серверных объектов</exception>
      public void Remove(object objToLock)
      {
        this.CheckNotDisposed();
        this.sponsor.Unregister(objToLock);
      }
    }
}
