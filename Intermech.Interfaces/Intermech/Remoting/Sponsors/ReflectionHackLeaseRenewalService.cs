
// Type: Intermech.Remoting.Sponsors.ReflectionHackLeaseRenewalService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Threading;


namespace Intermech.Remoting.Sponsors
{
    /// <summary>
    /// Специализированная реализация сервиса управления временем жизни объектов remoting.
    /// Она позволяет не только увеличивать, но и уменьшать время жизни. Для этого
    /// используется reflection hack.
    /// </summary>
    /// <remarks>
    /// Реализация является thread safe и long life, так как предоставляется сервером remoting.
    /// </remarks>
    public sealed class ReflectionHackLeaseRenewalService : MarshalByRefObject, ILeaseRenewalService
    {
      private readonly Lazy<ReflectionHackLeaseRenewalService.LeaseHackTool> leaseHackToolCache;
      private readonly Lazy<ReflectionHackLeaseRenewalService.LeaseManagerHackTool> leaseManagerHackToolCache;

      /// <summary>Создает объект.</summary>
      public ReflectionHackLeaseRenewalService()
      {
        this.leaseHackToolCache = new Lazy<ReflectionHackLeaseRenewalService.LeaseHackTool>(LazyThreadSafetyMode.PublicationOnly);
        this.leaseManagerHackToolCache = new Lazy<ReflectionHackLeaseRenewalService.LeaseManagerHackTool>(LazyThreadSafetyMode.PublicationOnly);
      }

      /// <summary>
      /// Инициализирует сервис управления временем жизни текущего объекта.
      /// </summary>
      /// <returns>null, так как это long life object</returns>
      public override object InitializeLifetimeService() => (object) null;

      /// <summary>
      /// Увеличивает или уменьшает время жизни объекта remoting.
      /// </summary>
      /// <param name="lease">Объект для управления временем жизни объекта</param>
      /// <param name="delta">Приращение для текущего значения время жизни для объекта. Значение параметра может быть отрицательным</param>
      /// <returns>Признак успешного/неуспешного изменения времени жизни объекта</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="lease" /> содержит null</exception>
      public bool TryChangeLeaseTime(ILease lease, TimeSpan delta)
      {
        if (lease == null)
          throw new ArgumentNullException(nameof (lease));
        ReflectionHackLeaseRenewalService.LeaseHackTool leaseHackTool = this.leaseHackToolCache.Value;
        ReflectionHackLeaseRenewalService.LeaseManagerHackTool leaseManagerHackTool = this.leaseManagerHackToolCache.Value;
        if (!leaseHackTool.IsAvailable || !leaseManagerHackTool.IsAvailable || RemotingServices.IsTransparentProxy((object) lease) || lease.GetType() != leaseHackTool.LeaseType)
          return false;
        lock (lease)
        {
          if ((LeaseState) leaseHackTool.StateField.GetValue((object) lease) == LeaseState.Expired)
            return false;
          DateTime dateTime1 = (DateTime) leaseHackTool.LeaseTimeField.GetValue((object) lease);
          object obj = leaseHackTool.LeaseManagerField.GetValue((object) lease);
          Hashtable hashtable = (Hashtable) leaseManagerHackTool.LeaseToTimeTableField.GetValue(obj);
          DateTime dateTime2 = dateTime1 + delta;
          lock (hashtable)
            hashtable[(object) lease] = (object) dateTime2;
          leaseHackTool.LeaseTimeField.SetValue((object) lease, (object) dateTime2);
          leaseHackTool.StateField.SetValue((object) lease, (object) LeaseState.Active);
        }
        return true;
      }

      private sealed class LeaseHackTool
      {
        public const string LeaseTypeName = "System.Runtime.Remoting.Lifetime.Lease";

        public LeaseHackTool()
        {
          this.LeaseType = Type.GetType("System.Runtime.Remoting.Lifetime.Lease", false);
          if (this.LeaseType != (Type) null)
          {
            this.StateField = this.LeaseType.GetField("state", BindingFlags.Instance | BindingFlags.NonPublic);
            this.LeaseTimeField = this.LeaseType.GetField("leaseTime", BindingFlags.Instance | BindingFlags.NonPublic);
            this.LeaseManagerField = this.LeaseType.GetField("leaseManager", BindingFlags.Instance | BindingFlags.NonPublic);
          }
          this.IsAvailable = this.LeaseType != (Type) null && this.StateField != (FieldInfo) null && this.StateField.FieldType == typeof (LeaseState) && this.LeaseTimeField != (FieldInfo) null && this.LeaseTimeField.FieldType == typeof (DateTime) && this.LeaseManagerField != (FieldInfo) null && this.LeaseManagerField.FieldType.FullName == "System.Runtime.Remoting.Lifetime.LeaseManager";
        }

        public Type LeaseType { get; private set; }

        public FieldInfo LeaseTimeField { get; private set; }

        public FieldInfo StateField { get; private set; }

        public FieldInfo LeaseManagerField { get; private set; }

        public bool IsAvailable { get; private set; }
      }

      private sealed class LeaseManagerHackTool
      {
        public const string LeaseManagerTypeName = "System.Runtime.Remoting.Lifetime.LeaseManager";

        public LeaseManagerHackTool()
        {
          this.LeaseManagerType = Type.GetType("System.Runtime.Remoting.Lifetime.LeaseManager", false);
          if (this.LeaseManagerType != (Type) null)
            this.LeaseToTimeTableField = this.LeaseManagerType.GetField("leaseToTimeTable", BindingFlags.Instance | BindingFlags.NonPublic);
          this.IsAvailable = this.LeaseManagerType != (Type) null && this.LeaseToTimeTableField != (FieldInfo) null && this.LeaseToTimeTableField.FieldType == typeof (Hashtable);
        }

        public Type LeaseManagerType { get; private set; }

        public FieldInfo LeaseToTimeTableField { get; private set; }

        public bool IsAvailable { get; private set; }
      }
    }
}
