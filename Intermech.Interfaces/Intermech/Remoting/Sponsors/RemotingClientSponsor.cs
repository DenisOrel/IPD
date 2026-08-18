
// Type: Intermech.Remoting.Sponsors.RemotingClientSponsor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;


namespace Intermech.Remoting.Sponsors
{
    /// <summary>
    /// Реализует клиентский спонсор для продления времени жизни серверных (remoting) объектов при выполнении длительных операций.
    /// Это значит, что объект спонсора располагается на клиенте, а сервер обращается к нему по мере надобности.
    /// Для работы такого спонсора требуется двунаправленный канал remoting между сервером и клиентом.
    /// </summary>
    /// <remarks>Реализация является thread safe.</remarks>
    internal sealed class RemotingClientSponsor : MarshalByRefObject, ISponsor, IRemotingClientSponsor
    {
      private readonly LinkedList<MarshalByRefObject> serverObjects;
      private readonly TimeSpan renewalTime;
      private readonly IRemotingClientSponsorLogger logger;
      private static readonly string sponsorName = "classis sponsor";

      /// <summary>
      /// Создает спонсор с указанным временем продления срока жизни сервеных объектов.
      /// </summary>
      /// <param name="renewalTime">Интервал времени, на который продлевается время жизни серверных объектов</param>
      public RemotingClientSponsor(TimeSpan renewalTime)
      {
        this.renewalTime = renewalTime;
        this.serverObjects = new LinkedList<MarshalByRefObject>();
        this.logger = RemotingClientSponsorService.Default.Logger;
      }

      /// <summary>
      /// Инициализирует сервис по управлению сроком жизни самого спонсора.
      /// Метод возвращает null, также как и стандартная реализация от Microsoft.
      /// </summary>
      /// <returns>null, так как это long life object</returns>
      public override object InitializeLifetimeService() => (object) null;

      /// <summary>
      /// Регистрирует заданный серверный объект для спонсирования.
      /// Если объект не является серверным, то метод завершает выполнение без ошибок.
      /// </summary>
      /// <param name="obj">Серверный объект, для которого требуется продлевать срок жизни</param>
      /// <exception cref="T:System.Exception">При регистрации спонсора произошла ошибка. Возможно, что серверный объект уже был освобожден сервером</exception>
      public void Register(object obj)
      {
        MarshalByRefObject mbr = this.TryGetMbr(obj);
        if (mbr == null || !this.RegisterCore(mbr))
          return;
        lock (this.serverObjects)
          this.serverObjects.AddFirst(mbr);
      }

      private bool RegisterCore(MarshalByRefObject mbr)
      {
        ILease lifetimeService = (ILease) RemotingServices.GetLifetimeService(mbr);
        if (lifetimeService == null)
          return false;
        lifetimeService.Register((ISponsor) this);
        this.LogRegister(mbr);
        return true;
      }

      private void LogRegister(MarshalByRefObject mbr)
      {
        try
        {
          this.logger.RegisterSponsor(mbr, RemotingClientSponsor.sponsorName);
        }
        catch (Exception ex)
        {
          string currentMethodName = this.GetCurrentMethodName(nameof (LogRegister));
          SuppressedExceptions.TraceException(ex, currentMethodName);
        }
      }

      /// <summary>
      ///  Отменяет регистрацию заданного серверного объекта для спонсирования.
      ///  Если объект не является серверным, то метод завершает выполнение без ошибок.
      /// </summary>
      /// <param name="obj">Серверный объект, для которого больше не надо продлевать время жизни</param>
      public void Unregister(object obj)
      {
        MarshalByRefObject mbr = this.TryGetMbr(obj);
        if (mbr == null)
          return;
        this.UnregisterSilently(mbr);
        lock (this.serverObjects)
        {
          LinkedListNode<MarshalByRefObject> node = this.serverObjects.First;
          while (node != null)
          {
            if (node.Value == mbr)
            {
              LinkedListNode<MarshalByRefObject> next = node.Next;
              this.serverObjects.Remove(node);
              node = next;
            }
            else
              node = node.Next;
          }
        }
      }

      /// <summary>
      /// Отменяет регистрацию всех спонсируемых серверных объектов.
      /// </summary>
      public void UnregisterAll()
      {
        lock (this.serverObjects)
        {
          foreach (MarshalByRefObject serverObject in this.serverObjects)
            this.UnregisterSilently(serverObject);
          this.serverObjects.Clear();
        }
      }

      private bool UnregisterSilently(MarshalByRefObject mbr)
      {
        try
        {
          return this.UnregisterCore(mbr);
        }
        catch (Exception ex)
        {
          string currentMethodName = this.GetCurrentMethodName(nameof (UnregisterSilently));
          SuppressedExceptions.TraceException(ex, currentMethodName);
          return false;
        }
      }

      private bool UnregisterCore(MarshalByRefObject mbr)
      {
        ILease lifetimeService = (ILease) RemotingServices.GetLifetimeService(mbr);
        if (lifetimeService == null)
          return false;
        lifetimeService.Unregister((ISponsor) this);
        this.LogUnregister(mbr);
        return true;
      }

      private void LogUnregister(MarshalByRefObject mbr)
      {
        try
        {
          this.logger.UnregisterSponsor(mbr, RemotingClientSponsor.sponsorName);
        }
        catch (Exception ex)
        {
          string currentMethodName = this.GetCurrentMethodName(nameof (LogUnregister));
          SuppressedExceptions.TraceException(ex, currentMethodName);
        }
      }

      private MarshalByRefObject TryGetMbr(object obj)
      {
        return obj != null && !RemotingServices.IsTransparentProxy(obj) && obj is IServerObjectWrapper serverObjectWrapper ? serverObjectWrapper.GetServerObject() : obj as MarshalByRefObject;
      }

      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      TimeSpan ISponsor.Renewal(ILease lease)
      {
        this.RaiseRenewalSilently();
        this.LogRenewal();
        return this.renewalTime;
      }

      private void RaiseRenewalSilently()
      {
        try
        {
          EventHandler renewal = this.Renewal;
          if (renewal == null)
            return;
          renewal((object) this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
          string currentMethodName = this.GetCurrentMethodName(nameof (RaiseRenewalSilently));
          SuppressedExceptions.TraceException(ex, currentMethodName);
        }
      }

      private void LogRenewal()
      {
        try
        {
          lock (this.serverObjects)
            this.logger.Renewal((ICollection<MarshalByRefObject>) this.serverObjects);
        }
        catch (Exception ex)
        {
          string currentMethodName = this.GetCurrentMethodName(nameof (LogRenewal));
          SuppressedExceptions.TraceException(ex, currentMethodName);
        }
      }

      /// <summary>
      /// Срабатывает перед увеличением времени жизни очередного серверного объекта.
      /// </summary>
      public event EventHandler Renewal;
    }
}
