
// Type: Intermech.Remoting.Sponsors.OneShotSponsor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;


namespace Intermech.Remoting.Sponsors
{
    /// <summary>
    /// Реализует специализированный клиентский спонсор для продления времени жизни серверных (remoting) объектов при выполнении длительных операций.
    /// Он не передается на серверную сторону и не использует двунаправленный канал remoting между сервером и клиентом.
    /// Вместо этого он непосредственно изменяет время жизни серверного объекта, увеличивая или уменьшая его на
    /// большую величину (несколько часов).
    /// </summary>
    /// <remarks>Реализация является thread safe.</remarks>
    internal sealed class OneShotSponsor : MarshalByRefObject, IRemotingClientSponsor
    {
      private readonly ILeaseRenewalService leaseRenewalService;
      private readonly LinkedList<MarshalByRefObject> serverObjects;
      private readonly IRemotingClientSponsorLogger logger;
      private static readonly TimeSpan positiveBigTimeSpan = TimeSpan.FromHours(12.0);
      private static readonly TimeSpan negativeBigTimeSpan = OneShotSponsor.positiveBigTimeSpan.Negate();
      private static readonly string sponsorName = "one-shot sponsor";

      /// <summary>
      /// Создает спонсор с временем продления срока жизни серверных объектов равным 1 минуте.
      /// </summary>
      /// <param name="leaseRenewalService">Сервис для изменения времени жизни серверных объектов</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="leaseRenewalService" /> содержит значение null</exception>
      public OneShotSponsor(ILeaseRenewalService leaseRenewalService)
      {
        this.leaseRenewalService = leaseRenewalService != null ? leaseRenewalService : throw new ArgumentNullException(nameof (leaseRenewalService));
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
        if (this.leaseRenewalService.TryChangeLeaseTime(lifetimeService, OneShotSponsor.positiveBigTimeSpan))
        {
          this.LogRegister(mbr);
          return true;
        }
        this.LogMessage((Func<string>) (() => $"Unable to increase the lifetime for a single server-side object (uri = {RemotingServices.GetObjectUri(mbr)})"));
        return false;
      }

      private void LogRegister(MarshalByRefObject mbr)
      {
        try
        {
          this.logger.RegisterSponsor(mbr, OneShotSponsor.sponsorName);
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
        if (this.leaseRenewalService.TryChangeLeaseTime(lifetimeService, OneShotSponsor.negativeBigTimeSpan))
        {
          this.LogUnregister(mbr);
          return true;
        }
        this.LogMessage((Func<string>) (() => $"Unable to decrease the lifetime for a single server-side object (uri = {RemotingServices.GetObjectUri(mbr)})"));
        return false;
      }

      private void LogUnregister(MarshalByRefObject mbr)
      {
        try
        {
          this.logger.UnregisterSponsor(mbr, OneShotSponsor.sponsorName);
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

      private void LogMessage(Func<string> messageProvider)
      {
        try
        {
          this.logger.SponsorMessage(messageProvider());
        }
        catch (Exception ex)
        {
          string currentMethodName = this.GetCurrentMethodName(nameof (LogMessage));
          SuppressedExceptions.TraceException(ex, currentMethodName);
        }
      }
    }
}
