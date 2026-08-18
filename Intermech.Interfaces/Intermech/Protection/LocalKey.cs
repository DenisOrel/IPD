
// Type: Intermech.Protection.LocalKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Aladdin.HASP;
using Intermech.ApplicationModel;
using Intermech.Localization;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;


namespace Intermech.Protection
{
    public class LocalKey : ProtectionKeyBase
    {
      protected static object LockSection = new object();
      internal ApplicationEntry _entry;
      protected int _appCount;
      protected int _addrSumm;

      internal Hasp Key => this._entry.Key;

      public LocalKey(int appId, byte[] query, byte[] reply)
        : base(appId, query, reply)
      {
        string appSetting = ConfigurationManager.AppSettings["UseLocalKey"];
        if (appSetting == null || appSetting != "1")
          throw new ProtectionException($"Использование локального ключа не разрешено.{Environment.NewLine}При необходимости добавьте : <add key=\"UseLocalKey\" value=\"1\"/> в файл конфигурации.");
        this._appCount = 0;
        this._addrSumm = 0;
        this.Connect();
        this.EnableTimer();
      }

      /// <summary>Инициализация объекта ключа и его настройка</summary>
      private void InitializePrimaryKey()
      {
        if (Win32.GetSystemMetrics(4096 /*0x1000*/) != 1)
          return;
        this.Stop();
      }

      private void Connect()
      {
        DateTime minValue = DateTime.MinValue;
        int daysLeft;
        lock (LocalKey.LockSection)
          KeyHelper.Connect(ref this._appCount, ref this._addrSumm, ref minValue, out daysLeft);
        if (minValue != DateTime.MinValue && daysLeft < 15)
        {
          IAlertMessageService alertService = ProtectionService.AlertService;
          if (alertService != null)
          {
            if (daysLeft == 0)
              alertService.ShowMessage(LocalizationHolder.rm.GetString("Interfaces_730"), LocalizationHolder.rm.GetString("Interfaces_731"), AlertMessageType.Warning);
            else
              alertService.ShowMessage(LocalizationHolder.rm.GetString("Interfaces_730"), string.Format(LocalizationHolder.rm.GetString("Interfaces_732"), (object) daysLeft, (object) Environment.NewLine, (object) minValue.Day, (object) minValue.Month, (object) minValue.Year), AlertMessageType.Warning);
          }
        }
        ApplicationEntry entry = KeyApplications.GetEntry(this._applicationId);
        if (entry == null || entry.Key == (object) null)
        {
          if (entry == null)
            throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_86"), (object) this._applicationId));
          throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_87"), (object) entry.ApplicationName));
        }
        this._entry = entry;
        this.CheckBaseQuery();
        this.CheckRegistration();
      }

      private void CheckKey() => this.CheckCommon(this._random.Next(10) > 5);

      /// <summary>Проверка ключа на алгоритме приложения</summary>
      private void CheckBaseQuery()
      {
        byte[] reply = new byte[this._querySize];
        ApplicationEntry entry = KeyApplications.GetEntry(this._applicationId);
        if (entry == null || entry.Key == (object) null)
          throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_88"), (object) this._applicationId));
        KeyHelper.CheckStatus(KeyHelper.Encrypt(entry.Key, this._query, reply));
        for (int index = 0; index < this._querySize; ++index)
        {
          if ((int) reply[index] != (int) this._reply[index])
            throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_89"), (object) this._applicationId));
        }
      }

      private void CheckRegistration()
      {
        uint num1 = 0;
        Hasp mainKey = KeyHelper.MainKey;
        lock (LocalKey.LockSection)
        {
          string licenseFile = LocalKey.GetLicenseFile();
          string p = LocalKey.ReadLicense(licenseFile);
          byte[] buf = new byte[8];
          byte[] numArray1 = new byte[8];
          byte[] numArray2 = new byte[8];
          if (p.Length == 16 /*0x10*/)
            this.StringToByteArray(p, buf);
          this.StringToByteArray(this.GetLicenseInfo(), numArray1);
          Array.Copy((Array) numArray1, (Array) numArray2, numArray1.Length);
          KeyHelper.CheckStatus(KeyHelper.Encrypt(mainKey, numArray1, numArray2));
          for (int index1 = 0; index1 < 8; ++index1)
          {
            if ((int) numArray2[index1] != (int) buf[index1])
            {
              HaspFile file = mainKey.GetFile(HaspFileId.ReadWrite);
              KeyHelper.CheckStatus(file.Read(ref num1));
              uint num2 = (uint) this.PackDate(DateTime.Now);
              if (num1 == 0U)
              {
                num1 = num2;
                int num3 = (int) file.Write(num1);
              }
              int daysLeft = num1 > num2 || (uint) (30 - ((int) num2 - (int) num1)) < 0U ? 0 : 30 - ((int) num2 - (int) num1);
              bool cancel = false;
              string str = this.OnAutorize(daysLeft, ref cancel);
              if (cancel || str.Length == 0 && daysLeft <= 0)
                throw new ProtectionException(LocalizationHolder.rm.GetString("Interfaces_90"));
              if (str.Length > 1)
              {
                this.StringToByteArray(str, buf);
                int num4 = (int) KeyHelper.Encrypt(mainKey, numArray1, numArray2);
                for (int index2 = 0; index2 < 8; ++index2)
                {
                  if ((int) numArray2[index2] != (int) buf[index2])
                    throw new CriticalProtectionException(LocalizationHolder.rm.GetString("Interfaces_91"));
                }
                LocalKey.WriteLicense(licenseFile, str);
                break;
              }
              if (ProtectionService.HasUI)
                break;
              ProtectionService.AlertService?.ShowMessage(LocalizationHolder.rm.GetString("Interfaces_730"), string.Format(LocalizationHolder.rm.GetString("Interfaces_733"), (object) daysLeft), AlertMessageType.Warning);
              break;
            }
          }
        }
      }

      private static string GetLicenseFile()
      {
        string path2 = "Intermech.hasp.license";
        string appSetting = ConfigurationManager.AppSettings["LicensePath"];
        if (appSetting != null)
          path2 = Path.Combine(Environment.ExpandEnvironmentVariables(appSetting), path2);
        return path2;
      }

      private static void WriteLicense(string fileName, string data)
      {
        using (StreamWriter streamWriter = new StreamWriter(fileName, false, Encoding.ASCII))
        {
          streamWriter.Write(data);
          streamWriter.Flush();
        }
      }

      private static string ReadLicense(string fileName)
      {
        string str = string.Empty;
        if (File.Exists(fileName))
        {
          using (StreamReader streamReader = new StreamReader(fileName, Encoding.ASCII))
            str = streamReader.ReadLine();
        }
        return str;
      }

      public override void Dispose()
      {
        this.Cleanup();
        base.Dispose();
      }

      public override IntPtr CheckHibernate()
      {
        int num = 100;
        while (num-- > 0)
        {
          if (Monitor.TryEnter(this._criticalSection))
            return new IntPtr(1);
          Thread.Sleep(100);
        }
        return new IntPtr(0);
      }

      private void Cleanup() => this._entry = (ApplicationEntry) null;

      protected override void OnTimerTick()
      {
        lock (LocalKey.LockSection)
        {
          switch (this._random.Next(10))
          {
            case 0:
            case 1:
              this.CheckCommon(true);
              break;
            case 2:
            case 3:
              this.CheckCommon(false);
              break;
            case 4:
            case 5:
              this.CheckRandomCache();
              break;
            case 6:
            case 7:
            case 8:
              this.CheckRandom();
              break;
            case 9:
            case 10:
              this.CheckByApplication();
              break;
          }
        }
      }

      private void CheckCommon(bool random)
      {
        byte[] query = (byte[]) null;
        byte[] reply1 = (byte[]) null;
        int sz = !random ? KeyCodes.DateQueryLocal(ref query, ref reply1) : KeyCodes.RandomQuery(ref query, ref reply1);
        byte[] reply2 = new byte[sz];
        this.DoQuery(KeyHelper.MainKey, query, reply2, sz);
        if (random)
          return;
        for (int index = 0; index < sz; ++index)
        {
          if ((int) reply1[index] != (int) reply2[index])
            throw new ProtectionException(LocalizationHolder.rm.GetString("Interfaces_92"));
        }
      }

      protected override int QueryInternal(
        bool quiet,
        int appId,
        byte[] query,
        byte[] reply,
        int size)
      {
        lock (LocalKey.LockSection)
        {
          if (appId != 0)
          {
            ApplicationEntry entry = KeyApplications.GetEntry(appId);
            if (entry == null || entry.Key == (object) null)
            {
              if (entry == null)
                throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_93"), (object) appId));
              throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_93"), (object) $"{appId.ToString()}  '{entry.ApplicationName}'"));
            }
            this.DoQuery(entry.Key, query, reply, size);
          }
          else
            this.DoQuery(KeyHelper.MainKey, query, reply, size);
        }
        return 0;
      }

      private void DoQuery(Hasp key, byte[] query, byte[] reply, int sz)
      {
        KeyHelper.CheckStatus(KeyHelper.Encrypt(key, query, reply));
      }

      private void CheckRandom()
      {
        int sz = this._random.Next(8, 50);
        byte[] numArray = new byte[sz];
        byte[] reply = new byte[sz];
        this._random.NextBytes(numArray);
        if (numArray[0] > (byte) 128 /*0x80*/)
          this.DoQuery(this._entry.Key, numArray, reply, sz);
        else
          this.DoQuery(KeyHelper.MainKey, numArray, reply, sz);
        RandomCache.Add(numArray, reply, sz);
      }

      private void CheckRandomCache()
      {
        if (RandomCache.Count <= 0)
          return;
        byte[] query = RandomCache.Get(this._random.Next(RandomCache.Count - 1));
        int length = query.Length;
        byte[] numArray = RandomCache.Get(query);
        byte[] reply = new byte[length];
        if (query[0] > (byte) 128 /*0x80*/)
          this.DoQuery(this._entry.Key, query, reply, length);
        else
          this.DoQuery(KeyHelper.MainKey, query, reply, length);
        for (int index = 0; index < length; ++index)
        {
          if ((int) reply[index] != (int) numArray[index])
            throw new ProtectionException("Bad answer");
        }
      }

      private void CheckByApplication()
      {
        int querySize = this._querySize;
        byte[] reply = new byte[querySize];
        this.DoQuery(this._entry.Key, this._query, reply, querySize);
        for (int index = 0; index < querySize; ++index)
        {
          if ((int) this._reply[index] != (int) reply[index])
            throw new ProtectionException(LocalizationHolder.rm.GetString("Interfaces_94"));
        }
      }

      public override bool AllocateLicense(int appId) => true;

      public override bool ReleaseLicense(int appId) => true;

      protected override string GetLicenseInfo()
      {
        int volumeSerial = LocalKey.GetVolumeSerial();
        int num1 = (int) KeyHelper._serialNo & (int) ushort.MaxValue;
        int num2 = 0;
        KeyApplications.CheckedCount(ref num2);
        return $"{num1:X4}{this._appCount:X4}{this._addrSumm & (int) ushort.MaxValue:X4}{volumeSerial:X4}";
      }

      protected override void SetLicenseInfo(string licenseData)
      {
        LocalKey.WriteLicense(LocalKey.GetLicenseFile(), licenseData);
      }

      protected static int GetVolumeSerial()
      {
        StringBuilder lpVolumeNameBuffer = new StringBuilder(512 /*0x0200*/);
        StringBuilder lpFileSystemNameBuffer = new StringBuilder(512 /*0x0200*/);
        int lpVolumeSerialNumber = 0;
        int lpMaximumComponentLength = 512 /*0x0200*/;
        int lpFileSystemFlags = 0;
        Win32.GetVolumeInformation("C:\\", lpVolumeNameBuffer, lpVolumeNameBuffer.Capacity, ref lpVolumeSerialNumber, ref lpMaximumComponentLength, ref lpFileSystemFlags, lpFileSystemNameBuffer, lpFileSystemNameBuffer.Capacity);
        return lpVolumeSerialNumber & (int) ushort.MaxValue;
      }

      public override void PostLoad()
      {
      }
    }
}
