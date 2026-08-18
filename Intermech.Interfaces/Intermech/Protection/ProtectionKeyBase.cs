
// Type: Intermech.Protection.ProtectionKeyBase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Protection
{
    public abstract class ProtectionKeyBase : IProtectionKey, IDisposable, ILicenser
    {
      protected int _licensesCount;
      protected int _version;
      protected string _authorizationCode;
      protected int _applicationId;
      protected int _lastErrorCode;
      protected string _lastErrorMessage;
      protected object _criticalSection;
      protected byte[] _query;
      protected byte[] _reply;
      protected int _querySize;
      protected Random _random;
      private Timer _timer;
      private bool _timerTickRunning;
      protected bool _disposed;
      protected int _tagValue;
      internal const int MAX_QUERY_SIZE = 64 /*0x40*/;
      private const int TIMER_PERIOD = 600000;

      public ProtectionKeyBase()
      {
        this._authorizationCode = string.Empty;
        this._licensesCount = 0;
        this._version = 0;
        this._applicationId = 0;
        this._lastErrorCode = 0;
        this._lastErrorMessage = string.Empty;
        this._criticalSection = new object();
        this._query = new byte[64 /*0x40*/];
        this._reply = new byte[64 /*0x40*/];
        this._random = new Random();
        this._timer = new Timer(new TimerCallback(this.TimerTick), (object) null, -1, -1);
      }

      public ProtectionKeyBase(int applicationId, byte[] query, byte[] reply)
        : this()
      {
        if (query == null)
          throw new ArgumentException("Wrong parameter", nameof (query));
        if (reply == null)
          throw new ArgumentException("Wrong parameter", nameof (reply));
        if (query.Length != reply.Length)
          throw new ArgumentException("Reply and Query size missmatch");
        this._querySize = query.Length;
        Buffer.BlockCopy((Array) query, 0, (Array) this._query, 0, this._querySize);
        Buffer.BlockCopy((Array) reply, 0, (Array) this._reply, 0, this._querySize);
        this._applicationId = applicationId;
      }

      private void TimerTick(object stateInfo)
      {
        lock (this._criticalSection)
        {
          if (this._disposed || this._timerTickRunning)
            return;
          this._timerTickRunning = true;
          try
          {
            this.OnTimerTick();
          }
          catch
          {
          }
          finally
          {
            this._timerTickRunning = false;
          }
          this.EnableTimer();
        }
      }

      protected abstract void OnTimerTick();

      public abstract void PostLoad();

      public void BadKeyAnswer() => throw new Exception("The method or operation is not implemented.");

      public void CheckLicense() => throw new Exception("The method or operation is not implemented.");

      public void CheckLicense2() => throw new Exception("The method or operation is not implemented.");

      public void CheckLicense3() => throw new Exception("The method or operation is not implemented.");

      public void QueryDate(byte[] CodesArray, byte XorVar, int CodesQuantityVar, ref int IsKeyOK)
      {
        throw new Exception("The method or operation is not implemented.");
      }

      public int LicensesCount => this._licensesCount;

      public string MonitorFileName
      {
        get => throw new Exception("The method or operation is not implemented.");
      }

      public string MonitorFileName2
      {
        get => throw new Exception("The method or operation is not implemented.");
      }

      public int Version => this._version;

      public DateTime ServerDate => throw new Exception("The method or operation is not implemented.");

      public string GetUnHashedString(byte[] queryArray, byte[] xorArray, ushort AlgoID)
      {
        throw new Exception("The method or operation is not implemented.");
      }

      public void LoadRandomRequest(ref byte[] queryData, ref byte[] response, ref ushort len)
      {
        throw new Exception("The method or operation is not implemented.");
      }

      public bool RandomQuery() => throw new Exception("The method or operation is not implemented.");

      public bool RandomQuery(params byte[] P)
      {
        throw new Exception("The method or operation is not implemented.");
      }

      public void SaveRandomRequest(byte[] queryData, byte[] response, ushort len)
      {
        throw new Exception("The method or operation is not implemented.");
      }

      public int Decrement(bool QuietMode, ushort writePassword, ushort address)
      {
        throw new Exception("The method or operation is not implemented.");
      }

      protected int BlockQuery(byte[] query, byte[] reply, int blockSize)
      {
        int length1 = query.Length;
        if (reply == null)
          throw new ArgumentNullException(nameof (reply));
        int length2 = blockSize;
        byte[] numArray1 = new byte[length2];
        byte[] numArray2 = new byte[length2];
        int num1 = 0;
        int num2;
        do
        {
          if (num1 + length2 > length1)
            length2 = length1 - num1;
          Buffer.BlockCopy((Array) query, num1, (Array) numArray1, 0, length2);
          num2 = this.QueryInternal(false, this.ApplicationID, numArray1, numArray2, length2);
          Buffer.BlockCopy((Array) numArray2, 0, (Array) reply, num1, length2);
          num1 += length2;
        }
        while (num1 < length1);
        return num2;
      }

      public int LongQuery(byte[] query, byte[] reply) => this.BlockQuery(query, reply, 64 /*0x40*/);

      protected abstract int QueryInternal(
        bool quiet,
        int appId,
        byte[] query,
        byte[] reply,
        int size);

      public int Query(bool quiet, int appId, byte[] query, byte[] response)
      {
        if (query == null)
          throw new ArgumentNullException(nameof (query));
        return this.QueryInternal(quiet, appId, query, response, query.Length);
      }

      public int Read(bool QuietMode, int address, ref int Data)
      {
        throw new Exception("The method or operation is not implemented.");
      }

      public int ShortQuery(byte[] query, byte[] reply) => this.BlockQuery(query, reply, 3);

      public void ValidateExpiration(object aMainForm)
      {
        throw new Exception("The method or operation is not implemented.");
      }

      public int ApplicationID => this._applicationId;

      public int LastErrorCode => this._lastErrorCode;

      public string LastErrorMessage => this._lastErrorMessage;

      public abstract IntPtr CheckHibernate();

      protected void EnableTimer() => this._timer.Change(600000, -1);

      protected void DisableTimer() => this._timer.Change(-1, -1);

      protected int PackDate(DateTime dt)
      {
        return (dt.Year - 1980) * 512 /*0x0200*/ + dt.Month * 32 /*0x20*/ + dt.Day;
      }

      protected abstract string GetLicenseInfo();

      protected abstract void SetLicenseInfo(string licenseData);

      public IApplicationEntry[] Applications => KeyApplications.Applications;

      public virtual void Dispose()
      {
        if (this._disposed)
          return;
        this.DisableTimer();
        this._timer.Dispose();
        this._disposed = true;
      }

      protected string OnAutorize(int daysLeft, ref bool cancel)
      {
        string licenseInfo = this.GetLicenseInfo();
        if (ProtectionService.CanAuthorize)
          return ProtectionService.OnAuthorize(daysLeft, licenseInfo, ref cancel);
        cancel = daysLeft == 0;
        return string.Empty;
      }

      protected void Stop() => Process.GetCurrentProcess().Kill();

      protected void StringToByteArray(string p, byte[] buf)
      {
        string upper = p.ToUpper();
        int num1 = upper.Length / 2;
        if (buf == null)
          return;
        if (buf.Length < num1)
          throw new Exception("More data");
        int num2 = 0;
        for (int index = 0; index < num1 * 2; index += 2)
        {
          byte num3 = (byte) ((uint) (byte) upper[index] - 48U /*0x30*/);
          if (num3 > (byte) 9)
            num3 -= (byte) 7;
          byte num4 = (byte) ((uint) (byte) upper[index + 1] - 48U /*0x30*/);
          if (num4 > (byte) 9)
            num4 -= (byte) 7;
          Buffer.SetByte((Array) buf, num2++, (byte) ((uint) num3 << 4 | (uint) num4));
        }
      }

      public abstract bool AllocateLicense(int appId);

      public abstract bool ReleaseLicense(int appId);

      public int TagValue
      {
        get => this._tagValue;
        set => this._tagValue = value;
      }

      public static bool IsTerminal() => Win32.GetSystemMetrics(4096 /*0x1000*/) != 0;
    }
}
