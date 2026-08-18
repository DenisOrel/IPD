
// Type: Intermech.Protection.NetworkKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;


namespace Intermech.Protection
{
    public class NetworkKey : ProtectionKeyBase
    {
      private int _reconnectPause = 2000;
      private int _reconnectCount = 10;
      private int _keyRequestPeriod;
      private int _pipeHandle;
      private bool _terminalMode;
      private bool _useIP;
      private short _tcpIpMode = 1;
      private bool _keyPaused;
      private bool _allowReconnect;
      private NetworkKey.PipePacket _packet;
      private Socket _socket;
      private IPAddress _serverIP;
      private int _port;
      private byte _cryptKey;
      private string _cryptString;
      private string _serverName;
      private List<string> _serverNames = new List<string>();
      private int _serverNameIndex;
      private int _serverVersion;
      private string _pipeName;
      private byte[] _serverBytes;
      private byte[] _computerName;
      private Guid _sessionGuid;
      private List<NetworkKey.SubLicense> _subLicenses;
      private Exception _connectException;
      private static List<ExceptionInfo> DefferedExceptions = new List<ExceptionInfo>();
      private static bool _useSystemEvents = true;
      private static List<string> _spareServers = new List<string>();
      private static bool _informAdmins = true;
      private const int MANAGER_PORT = 1985;
      private const int OK_CODE = 0;
      private const int NOT_LOGGED_IN_CODE = 1;
      private const int KEY_NOT_FOUND_CODE = 2;
      private const int MAXIMUM_LICENSES_CODE = 3;
      private const int UNKNOWN_FUNCTION_CODE = 4;
      private const int SPRO_ERROR_CODE = 5;
      private const int NOT_AUTH = 6;
      internal const int IMLIC_CONNECT = 0;
      internal const int IMLIC_CONNECTSUB = 10;
      internal const int IMLIC_READ = 100;
      internal const int IMLIC_DECREMENT = 200;
      internal const int IMLIC_QUERY = 300;
      internal const int IMLIC_QUERY2 = 310;
      internal const int IMLIC_QUERY_PIPE = 320;
      internal const int IMLIC_QUERY_IP = 330;
      internal const int IMLIC_DISCONNECT = 400;
      internal const int IMLIC_MONITOR = 500;
      internal const int IMLIC_REQUEST = 600;
      internal const int IMLIC_SET = 700;
      internal const int IMLIC_DONE = 800;
      internal const int IMLIC_DONESUB = 810;
      internal const int IMLIC_CHECK = 900;
      internal const int IMLIC_CHECK2 = 905;
      internal const int IMLIC_CHECK_KA = 910;
      internal const int IMLIC_CHECK_ASP = 920;
      internal const int IMLIC_DENY_MODE = 930;
      internal const int IMLIC_DEADLINE = 150;

      public bool AllowReconnect
      {
        get => this._allowReconnect;
        set => this._allowReconnect = value;
      }

      /// <summary>Получения очередного имени сервера</summary>
      /// <returns></returns>
      private string NextServer()
      {
        if (this._serverNameIndex < this._serverNames.Count)
          ++this._serverNameIndex;
        else
          this._serverNameIndex = 0;
        return this._serverNames[this._serverNameIndex];
      }

      internal static void ClearDefferedExceptions() => NetworkKey.DefferedExceptions.Clear();

      internal static void AddDefferedException(Exception exception)
      {
        NetworkKey.DefferedExceptions.Add(new ExceptionInfo()
        {
          ComputerName = Environment.MachineName,
          Date = DateTime.Now,
          Exception = exception
        });
      }

      internal static List<ExceptionInfo> GetDefferedExceptions() => NetworkKey.DefferedExceptions;

      public static bool UseSystemEvents
      {
        get => NetworkKey._useSystemEvents;
        set => NetworkKey._useSystemEvents = value;
      }

      public NetworkKey(int applicationId, byte[] query, byte[] reply, string userCompName = null)
        : base(applicationId, query, reply)
      {
        this._pipeHandle = -1;
        this._keyRequestPeriod = 12;
        this._serverName = string.Empty;
        this._terminalMode = false;
        this._useIP = false;
        this._keyPaused = false;
        this._packet = new NetworkKey.PipePacket();
        this._cryptKey = (byte) 0;
        this._computerName = (byte[]) null;
        this.SetComputeName(userCompName);
        this._allowReconnect = false;
        this._port = 1985;
        this._subLicenses = new List<NetworkKey.SubLicense>();
        this.ReadServerSettings();
        int num = 0;
        while (true)
        {
          this._connectException = (Exception) null;
          try
          {
            this._connectException = (Exception) null;
            this.CreateSocket(false);
            this.CreatePipe(false);
            this.Connect(true);
            goto label_9;
          }
          catch (CriticalProtectionException ex)
          {
            NetworkKey.AddDefferedException((Exception) ex);
          }
          catch (ProtectionException ex)
          {
            NetworkKey.AddDefferedException((Exception) ex);
          }
          catch (Exception ex)
          {
            NetworkKey.AddDefferedException(ex);
            throw;
          }
          if (num < this._serverNames.Count)
            this.SetServerName(this._serverNames[num++]);
          else
            break;
        }
        throw new CriticalProtectionException("Ошибка подключения к менеджеру лицензий. Все известные сервера недоступны.");
    label_9:
        this._allowReconnect = true;
        if (NetworkKey._useSystemEvents)
          SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(this.SystemEvents_PowerModeChanged);
        this.EnableTimer();
      }

      private void SetServerName(string serverName)
      {
        this._serverName = serverName;
        this._serverBytes = (byte[]) null;
        this._cryptKey = (byte) 0;
      }

      private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs pme)
      {
        if (pme.Mode != PowerModes.Suspend)
          return;
        lock (this._criticalSection)
        {
          this._allowReconnect = false;
          this._packet._appId = this._applicationId;
          this._packet._algId = 0;
          this._packet._bufLength = 0;
          this._packet._errorCode = 800;
          this._packet.Guid = this._sessionGuid;
          try
          {
            this.SendData(false);
          }
          finally
          {
            this._allowReconnect = true;
          }
        }
      }

      private bool Connect(bool needRegistration, bool quietMode = false)
      {
        lock (this._criticalSection)
        {
          this._packet._appId = this._applicationId;
          this._packet._bufLength = this._querySize;
          this._packet._errorCode = 0;
          this._packet._response32 = 257;
          int computerName = this.GetComputerName(this._packet._buf);
          if (this._terminalMode)
          {
            int pSessionId = 0;
            int num = computerName - 1;
            string str = Intermech.Protection.Win32.ProcessIdToSessionId(Intermech.Protection.Win32.GetCurrentProcessId(), ref pSessionId) ? pSessionId.ToString() : throw new Exception("Error.");
            for (int index = 0; index < str.Length; ++index)
              this._packet._buf[num + index] = (byte) str[index];
            this._packet._buf[num + str.Length] = (byte) 0;
          }
          this.StoreInPacket(this._query, 20);
          this.StoreInPacket(this._reply, 21 + this._querySize);
          this.SendData(true);
          if (this._packet._errorCode == 0 || this._packet._errorCode == 6)
          {
            this._cryptKey = (byte) (this._packet._algId & (int) byte.MaxValue);
            this._serverVersion = this._packet._appId;
            this._sessionGuid = this._packet.Guid;
            this._cryptKey = (byte) (this._packet._algId & (int) byte.MaxValue);
            if (this._packet._response32 == -2)
              this._cryptString = this._sessionGuid.ToString("B").ToUpper();
            else if (this._packet._response32 == -1)
            {
              this._cryptString = this._serverIP.ToString();
            }
            else
            {
              int length = this._serverName.IndexOf('.');
              this._cryptString = length == -1 ? this._serverName : this._serverName.Substring(0, length);
            }
            this._cryptString = this._cryptString.ToUpper();
            if (this._packet._errorCode == 6 || this._packet._buf[344] == (byte) 6)
            {
              int daysLeft = (int) this._packet._buf[345];
              string licenseData;
              while (true)
              {
                bool cancel = false;
                licenseData = this.OnAutorize(daysLeft, ref cancel);
                if (cancel || licenseData.Length == 0 && daysLeft == 0)
                {
                  if (ProtectionService.HasUI)
                  {
                    this.Dispose();
                    this.Stop();
                  }
                  else
                    goto label_27;
                }
                else
                  break;
              }
              if (licenseData.Length > 0)
              {
                this.SetLicenseInfo(licenseData);
                if (this._packet._errorCode == 6)
                {
                  if (daysLeft == 0)
                    throw new NotAuthorizedException(this._packet.Message);
                  throw new NotAuthorizedException(LocalizationHolder.rm.GetString("Interfaces_98"));
                }
              }
            }
    label_27:
            return true;
          }
          this._connectException = (Exception) new CriticalProtectionException($"Ошибка подключения к менеджеру лицензий \"{this._serverName}\". Текст ошибки: \"{this._packet.Message}\".");
          if (quietMode)
            return false;
          throw this._connectException;
        }
      }

      protected override void OnTimerTick()
      {
        byte[] query = (byte[]) null;
        byte[] reply1 = (byte[]) null;
        int size = 0;
        bool flag = true;
        int appId = -1;
        switch (this._random.Next(4))
        {
          case 0:
            size = KeyCodes.RandomQuery(ref query, ref reply1);
            break;
          case 1:
            size = KeyCodes.DateQuery(ref query, ref reply1);
            break;
          case 2:
            size = this._random.Next(8, 63 /*0x3F*/);
            query = new byte[size];
            reply1 = new byte[size];
            this._random.NextBytes(query);
            flag = false;
            break;
          case 3:
            query = this._query;
            size = this._querySize;
            reply1 = this._reply;
            appId = this._applicationId;
            break;
        }
        byte[] reply2 = new byte[size];
        this.QueryInternal(false, appId, query, reply2, size);
        if (!flag)
          return;
        for (int index = 0; index < size; ++index)
        {
          if ((int) reply1[index] != (int) reply2[index])
            throw new ProtectionException(LocalizationHolder.rm.GetString("Interfaces_99"));
        }
      }

      private void CheckDateCodes()
      {
        int count = KeyCodes.Count;
        byte[] query = new byte[80 /*0x50*/];
        byte[] reply1 = new byte[80 /*0x50*/];
        byte[] reply2 = new byte[80 /*0x50*/];
        for (int pos = 0; pos < count; ++pos)
        {
          int codes = KeyCodes.GetCodes(pos, ref query, ref reply2);
          this.QueryInternal(false, -1, query, reply1, codes);
          for (int index = 0; index < codes; ++index)
          {
            if ((int) reply2[index] != (int) reply1[index])
              throw new ProtectionException(LocalizationHolder.rm.GetString("Interfaces_99"));
          }
        }
      }

      public static string DefferedExceptionsText
      {
        get
        {
          int num = 1;
          string empty = string.Empty;
          if (NetworkKey.DefferedExceptions.Count > 0)
          {
            StringBuilder stringBuilder = new StringBuilder(1024 /*0x0400*/);
            stringBuilder.Append("При иницализации системы лицензирования возникли следующие ошибки:" + Environment.NewLine);
            foreach (ExceptionInfo defferedException in NetworkKey.DefferedExceptions)
              stringBuilder.AppendLine($"{num++}) {defferedException.ToString()}");
            empty = stringBuilder.ToString();
          }
          return empty;
        }
      }

      public override void PostLoad()
      {
        if (NetworkKey.DefferedExceptions.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder(1024 /*0x0400*/);
          stringBuilder.Append("При иницализации системы лицензирования возникли следующие ошибки:" + Environment.NewLine);
          foreach (ExceptionInfo defferedException in NetworkKey.DefferedExceptions)
            stringBuilder.AppendLine(defferedException.ToString());
          string str = stringBuilder.ToString();
          if (NetworkKey.InformAdmins)
          {
            if (ProtectionService.GetService(typeof (IProtectionMessageService)) is IProtectionMessageService service)
              service.SendMessage("Ошибки системы лицензирования.", str);
          }
          else
          {
            KeyHelper.WriteLine("Ошибки системы лицензирования.");
            KeyHelper.WriteLine(str);
          }
        }
        NetworkKey.ClearDefferedExceptions();
      }

      public override IntPtr CheckHibernate() => new IntPtr(1);

      private bool SendData(bool readData)
      {
        int num1 = -1;
        byte[] buffer = this._packet.Buffer;
        byte[] numArray = new byte[1040];
        this.CryptBuffer(buffer);
        bool flag;
        try
        {
          if (this._useIP)
          {
            int num2 = Intermech.Protection.Win32.send(this._socket.Handle, buffer, buffer.Length, 0);
            flag = num2 != -1 && num2 > 0;
            if (flag & readData)
            {
              int num3 = Intermech.Protection.Win32.recv(this._socket.Handle, numArray, numArray.Length, 0);
              flag = num3 != -1 && num3 > 0;
            }
          }
          else
          {
            flag = Intermech.Protection.Win32.WriteFile(this._pipeHandle, buffer, buffer.Length, out num1, 0);
            if (flag & readData)
              flag = Intermech.Protection.Win32.ReadFile(this._pipeHandle, numArray, numArray.Length, out num1, 0);
          }
          if (!flag)
            throw new PipeIOException(LocalizationHolder.rm.GetString("Interfaces_100") + Intermech.Protection.Win32.GetErrorMessage(Marshal.GetLastWin32Error()), Marshal.GetLastWin32Error());
          this.CryptBuffer(numArray);
          this._packet.Buffer = numArray;
        }
        catch (PipeIOException ex)
        {
          if (this._allowReconnect)
          {
            try
            {
              this._allowReconnect = false;
              this.Reconnect();
              return this.SendData(readData);
            }
            finally
            {
              this._allowReconnect = true;
            }
          }
          else
            throw;
        }
        return flag;
      }

      private void Reconnect()
      {
        byte[] buffer = this._packet.Buffer;
        bool flag1 = this._sessionGuid.Equals(this._packet.Guid);
        bool flag2 = false;
        bool flag3 = false;
        int num1 = 0;
        int num2 = 0;
        NetworkKey.ClearDefferedExceptions();
        KeyHelper.WriteLine(LocalizationHolder.rm.GetString("Interfaces_101"));
        try
        {
          while (!flag2)
          {
            this._connectException = (Exception) null;
            this._keyPaused = true;
            while (!flag3)
            {
              flag3 = !this._useIP ? this.CreatePipe(true) : this.CreateSocket(true);
              if (!flag3)
              {
                if (this._connectException != null)
                {
                  KeyHelper.WriteLine($"Исключение {this._connectException.GetType().ToString()}:{this._connectException.Message}");
                  if (NetworkKey.InformAdmins)
                    NetworkKey.AddDefferedException(this._connectException);
                  this._connectException = (Exception) null;
                }
                int millisecondsTimeout = this._reconnectPause * ++num1;
                KeyHelper.WriteLine(string.Format(LocalizationHolder.rm.GetString("Interfaces_102"), (object) (millisecondsTimeout / 1000)));
                Thread.Sleep(millisecondsTimeout);
                if (this._reconnectCount < num1)
                {
                  num1 = 0;
                  if (num2 < this._serverNames.Count)
                  {
                    this.SetServerName(this._serverNames[num2++]);
                  }
                  else
                  {
                    num2 = 0;
                    if (!ProtectionService.AskYesNo(LocalizationHolder.rm.GetString("Interfaces_103") + Environment.NewLine + LocalizationHolder.rm.GetString("Interfaces_104"), LocalizationHolder.rm.GetString("Interfaces_105")))
                      this.Stop();
                  }
                }
              }
            }
            this._cryptKey = (byte) 0;
            this._serverBytes = (byte[]) null;
            this._connectException = (Exception) null;
            if (this.Connect(false, true))
            {
              this.RestoreSublicenses();
              break;
            }
            if (this._connectException != null)
              NetworkKey.AddDefferedException(this._connectException);
          }
        }
        finally
        {
          this._keyPaused = false;
          this._packet.Buffer = buffer;
          if (flag1)
            this._packet.Guid = this._sessionGuid;
        }
        KeyHelper.WriteLine(LocalizationHolder.rm.GetString("Interfaces_106"));
        if (num2 == 0 || !NetworkKey.InformAdmins || NetworkKey.DefferedExceptions.Count <= 0)
          return;
        StringBuilder stringBuilder = new StringBuilder(1024 /*0x0400*/);
        stringBuilder.Append("В работе системы лицензирования возникли следующие ошибки:" + Environment.NewLine);
        foreach (ExceptionInfo defferedException in NetworkKey.DefferedExceptions)
          stringBuilder.AppendLine(defferedException.ToString());
        string text = stringBuilder.ToString();
        if (!(ProtectionService.GetService(typeof (IProtectionMessageService)) is IProtectionMessageService service))
          return;
        service.SendMessage("Ошибки системы лицензирования.", text);
      }

      private void RestoreSublicenses()
      {
        int appId = 0;
        string str = string.Empty;
        List<NetworkKey.SubLicense> collection = new List<NetworkKey.SubLicense>((IEnumerable<NetworkKey.SubLicense>) this._subLicenses);
        try
        {
          this._subLicenses.Clear();
          int count = collection.Count;
          for (int index = 0; index < count; ++index)
          {
            NetworkKey.SubLicense subLicense = collection[index];
            int refCnt = subLicense._refCnt;
            appId = subLicense._appId;
            ApplicationEntry entry = KeyApplications.GetEntry(appId);
            if (entry != null)
              str = entry.ApplicationName;
            KeyHelper.WriteLine($"Восстановление сублицензии  '{str}' ({appId}).");
            this.InternalAllocateLiense(subLicense._appId);
            this.FindSublicense(subLicense._appId)._refCnt = refCnt;
            KeyHelper.WriteLine("Ok");
          }
        }
        catch (Exception ex)
        {
          this._subLicenses.Clear();
          this._subLicenses.AddRange((IEnumerable<NetworkKey.SubLicense>) collection);
          KeyHelper.WriteLine($"Ошибка восстановления сублицензии '{str}' ({appId}).");
          KeyHelper.WriteLine(ex.Message);
        }
      }

      private void CryptBuffer(byte[] buffer)
      {
        if (this._cryptKey == (byte) 0)
          return;
        int num1 = Buffer.ByteLength((Array) buffer);
        byte[] serverBytes = this.ServerBytes;
        int length = serverBytes.Length;
        for (int index = 0; index < num1; ++index)
        {
          byte num2 = serverBytes[index % length];
          byte num3 = (byte) ((uint) Buffer.GetByte((Array) buffer, index) ^ (uint) (byte) index ^ (uint) num2 ^ (uint) this._cryptKey);
          Buffer.SetByte((Array) buffer, index, num3);
        }
      }

      private byte[] ServerBytes
      {
        get
        {
          if (this._serverBytes == null)
          {
            string cryptString = this._cryptString;
            int length = cryptString.Length;
            this._serverBytes = new byte[length];
            for (int index = 0; index < length; ++index)
              Buffer.SetByte((Array) this._serverBytes, index, (byte) cryptString[index]);
          }
          return this._serverBytes;
        }
      }

      private bool Paused => this._keyPaused;

      private void StoreInPacket(byte[] data, int offset)
      {
        Buffer.BlockCopy((Array) data, 0, (Array) this._packet._buf, offset, this._querySize);
      }

      private void SetComputeName(string aCompName)
      {
        if (string.IsNullOrWhiteSpace(aCompName))
          return;
        char[] charArray = aCompName.ToUpperInvariant().ToCharArray();
        int index1 = charArray.Length;
        if (index1 > 17)
          index1 = 17;
        this._computerName = new byte[index1 + 1];
        for (int index2 = 0; index2 < index1; ++index2)
          this._computerName[index2] = Convert.ToByte(charArray[index2]);
        this._computerName[index1] = (byte) 0;
      }

      private int GetComputerName(byte[] p)
      {
        if (this._computerName != null)
        {
          int length = this._computerName.Length;
          Buffer.BlockCopy((Array) this._computerName, 0, (Array) p, 0, length);
        }
        else
        {
          int length = p.Length;
          Intermech.Protection.Win32.GetComputerName(p, ref length);
          this._computerName = new byte[length + 1];
          Buffer.BlockCopy((Array) p, 0, (Array) this._computerName, 0, length + 1);
          this._computerName[length] = (byte) 0;
        }
        return this._computerName.Length;
      }

      private int CreatePipeHandle()
      {
        this._pipeName = this.CreatePipeName(this._serverName);
        if (this._pipeHandle != -1)
          Intermech.Protection.Win32.CloseHandle(this._pipeHandle);
        return Intermech.Protection.Win32.CreateFile(this._pipeName, -1073741824 /*0xC0000000*/, 3, 0, 3, 2684354560U /*0xA0000000*/, 0);
      }

      private bool CreatePipe(bool quietMode)
      {
        if (this._useIP)
          return false;
        this._pipeHandle = this.CreatePipeHandle();
        if (Marshal.GetLastWin32Error() == 231)
        {
          for (int index = 0; index < 100; ++index)
          {
            Thread.Sleep(100);
            if (Intermech.Protection.Win32.WaitNamedPipe(this._pipeName, 100))
            {
              this._pipeHandle = this.CreatePipeHandle();
              if (this._pipeHandle != -1 || Marshal.GetLastWin32Error() != 231)
                break;
            }
          }
        }
        if (this._pipeHandle == -1)
        {
          this._connectException = (Exception) new CriticalProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_107"), (object) Marshal.GetLastWin32Error(), (object) this._serverName, (object) Environment.NewLine, Intermech.Protection.Win32.GetErrorMessage(Marshal.GetLastWin32Error())));
          if (quietMode)
            return false;
          throw this._connectException;
        }
        int lpMode = 2;
        if (Intermech.Protection.Win32.SetNamedPipeHandleState(this._pipeHandle, ref lpMode, 0, 0))
          return true;
        this._connectException = (Exception) new CriticalProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_108"), (object) Marshal.GetLastWin32Error()));
        if (quietMode)
          return false;
        throw this._connectException;
      }

      private bool CreateSocket(bool quietMode)
      {
        if (Intermech.Protection.Win32.GetSystemMetrics(4096 /*0x1000*/) != 0)
        {
          this._useIP = false;
          this._tcpIpMode = (short) 0;
          this._terminalMode = true;
        }
        if (!this._useIP)
          return false;
        if (this._socket != null)
          this._socket.Close();
        this._socket = (Socket) null;
        this._serverIP = (IPAddress) null;
        try
        {
          if (IPAddress.TryParse(this._serverName, out this._serverIP))
          {
            this._serverIP = IPAddress.Parse(this._serverName);
            IPEndPoint remoteEP = new IPEndPoint(this._serverIP, this._port);
            this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 120000);
            this._socket.Connect((EndPoint) remoteEP);
          }
          else
          {
            IPHostEntry hostEntry = Dns.GetHostEntry(this._serverName);
            if (hostEntry != null)
            {
              IPAddress[] addressList = hostEntry.AddressList;
              if (addressList != null)
              {
                if (addressList.Length != 0)
                {
                  this._serverIP = this.GetIPv4Address(addressList);
                  IPEndPoint remoteEP = new IPEndPoint(this._serverIP, this._port);
                  this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                  this._socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 120000);
                  this._socket.Connect((EndPoint) remoteEP);
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          this._connectException = (Exception) new CriticalProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_109"), (object) this._serverName, (object) Environment.NewLine, (object) ex.Message));
          if (!quietMode)
            throw this._connectException;
          return false;
        }
        if (this._socket != null && this._socket.Connected)
          return true;
        this._connectException = (Exception) new CriticalProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_110"), (object) this._serverName));
        if (!quietMode)
          throw this._connectException;
        return false;
      }

      /// <summary>Из списка адресов хоста выбирается IPv4 совместимый</summary>
      /// <param name="list">список доступных адресов</param>
      /// <returns>адрес IPv4 или null</returns>
      private IPAddress GetIPv4Address(IPAddress[] list)
      {
        if (list == null)
          return (IPAddress) null;
        int length = list.Length;
        for (int index = 0; index < length; ++index)
        {
          if (list[index].AddressFamily == AddressFamily.InterNetwork)
            return list[index];
        }
        return (IPAddress) null;
      }

      private void ReadServerSettings()
      {
        NameValueCollection appSettings = ConfigurationManager.AppSettings;
        string name = appSettings["ServerName"];
        if (!string.IsNullOrEmpty(name))
          name = Environment.ExpandEnvironmentVariables(name);
        if (string.IsNullOrEmpty(name))
          name = "localhost";
        this._serverName = name.ToString().Trim().ToUpper();
        if (this._serverName.Length == 0)
          throw new CriticalProtectionException(LocalizationHolder.rm.GetString("Interfaces_111"));
        this.CreateServersList();
        string s = appSettings["TcpIp"];
        this._tcpIpMode = (short) 0;
        if (short.TryParse(s, out this._tcpIpMode))
          this._useIP = this._tcpIpMode != (short) 0;
        int result;
        if (int.TryParse(appSettings["ManagerPort"], out result))
          this._port = result;
        if (int.TryParse(appSettings["ReconectTimeout"], out result))
          this._reconnectPause = result;
        if (!int.TryParse(appSettings["ReconnectCount"], out result))
          return;
        this._reconnectCount = result;
      }

      private void CreateServersList()
      {
        this._serverNames.Clear();
        string upper1 = this._serverName.ToUpper();
        foreach (string spareServer in NetworkKey.SpareServers)
        {
          string upper2 = spareServer.ToUpper();
          if (upper2 != upper1 && !this._serverNames.Contains(upper2))
            this._serverNames.Add(upper2);
        }
        this._serverNameIndex = 0;
      }

      private string CreatePipeName(string compName) => $"\\\\{compName}\\pipe\\imlic5";

      public int KeyRequestPeriod
      {
        get => this._keyRequestPeriod;
        set => this._keyRequestPeriod = value;
      }

      /// <summary>Есть ли дополнительные сервера</summary>
      /// <returns>true если есть</returns>
      private bool HasSpareServers() => NetworkKey.SpareServers.Count > 0;

      /// <summary>Используется ли дополнительный сервер</summary>
      /// <returns>true если используется</returns>
      private bool SpareServerUsed() => this._serverNameIndex > 0;

      public static void SetSpareServers(string value)
      {
        if (string.IsNullOrEmpty(value))
          return;
        string[] collection = value.Split(new char[1]{ ';' }, StringSplitOptions.RemoveEmptyEntries);
        NetworkKey._spareServers.AddRange((IEnumerable<string>) collection);
      }

      public static List<string> SpareServers => NetworkKey._spareServers;

      public static void SetInformAdmins(string value)
      {
        if (string.IsNullOrEmpty(value))
          return;
        value = value.ToUpper();
        switch (value)
        {
          case "1":
          case "TRUE":
            NetworkKey._informAdmins = true;
            break;
          case "0":
          case "FALSE":
            NetworkKey._informAdmins = false;
            break;
        }
      }

      internal static bool InformAdmins
      {
        get => NetworkKey._informAdmins;
        set => NetworkKey._informAdmins = value;
      }

      public override void Dispose()
      {
        if (this._disposed)
          return;
        if (!this._keyPaused)
        {
          if (NetworkKey._useSystemEvents)
            SystemEvents.PowerModeChanged -= new PowerModeChangedEventHandler(this.SystemEvents_PowerModeChanged);
          lock (this._criticalSection)
          {
            this.DisableTimer();
            this._allowReconnect = false;
            this._packet._appId = this._applicationId;
            this._packet._algId = 0;
            this._packet._bufLength = 0;
            this._packet._errorCode = 800;
            this._packet.Guid = this._sessionGuid;
            try
            {
              this.SendData(false);
            }
            finally
            {
              this._subLicenses.Clear();
              if (this._socket != null)
                this._socket.Close();
              if (this._pipeHandle != -1)
                Intermech.Protection.Win32.CloseHandle(this._pipeHandle);
              this._socket = (Socket) null;
              this._pipeHandle = -1;
            }
          }
        }
        base.Dispose();
      }

      protected override string GetLicenseInfo()
      {
        lock (this._criticalSection)
        {
          this._lastErrorCode = 0;
          this._lastErrorMessage = string.Empty;
          this._packet._appId = this._applicationId;
          this._packet._bufLength = 0;
          this._packet._errorCode = 600;
          try
          {
            this.SendData(true);
            return this._packet.Message;
          }
          catch (Exception ex)
          {
            throw new CriticalProtectionException(ex.Message);
          }
        }
      }

      protected override int QueryInternal(
        bool QuietMode,
        int appId,
        byte[] query,
        byte[] reply,
        int size)
      {
        lock (this._criticalSection)
        {
          this._lastErrorCode = 0;
          this._lastErrorMessage = string.Empty;
          if (query == null)
            throw new ArgumentException("Wrong parameter", nameof (query));
          if (reply == null)
            throw new ArgumentException("Wrong parameter", nameof (reply));
          int count = size;
          if (count > reply.Length)
            throw new ArgumentException("Reply and Query size missmatch");
          if (this._applicationId == appId || appId == -1)
            this.CopyMainGuid();
          else if (this.FindSublicense(appId) == null)
            throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_112"), (object) appId));
          this._packet._appId = appId;
          this._packet._bufLength = count;
          Buffer.BlockCopy((Array) query, 0, (Array) this._packet._buf, 0, count);
          this._packet._errorCode = 300;
          int num = this._random.Next(10);
          if (num > 5)
            this._packet._errorCode = num <= 7 ? 310 : (!this._useIP ? 320 : 330);
          try
          {
            this.SendData(true);
            Buffer.BlockCopy((Array) this._packet._buf, 0, (Array) reply, 0, count);
          }
          catch (Exception ex)
          {
          }
          return this._lastErrorCode;
        }
      }

      private void CopyMainGuid() => this._packet.Guid = this._sessionGuid;

      protected override void SetLicenseInfo(string licenseData)
      {
        this._packet._appId = this.ApplicationID;
        this._packet._errorCode = 700;
        this._packet._bufLength = licenseData.Length;
        this._packet.Message = licenseData;
        this.SendData(true);
      }

      private NetworkKey.SubLicense FindSublicense(int appId)
      {
        int count = this._subLicenses.Count;
        for (int index = 0; index < count; ++index)
        {
          NetworkKey.SubLicense subLicense = this._subLicenses[index];
          if (subLicense._appId == appId)
            return subLicense;
        }
        return (NetworkKey.SubLicense) null;
      }

      private void InternalAllocateLiense(int appId)
      {
        lock (this._criticalSection)
        {
          this._packet._appId = this._applicationId;
          this._packet._algId = appId;
          this._packet._bufLength = 0;
          this._packet.Guid = this._sessionGuid;
          this._packet._errorCode = 10;
          this.SendData(true);
        }
        if (this._packet._errorCode == 0)
        {
          this._subLicenses.Add(new NetworkKey.SubLicense(appId, this._packet._guid));
        }
        else
        {
          if (this._packet.Message.Contains("Invalid AppID!!!"))
          {
            string str = string.Empty;
            ApplicationEntry entry = KeyApplications.GetEntry(appId);
            if (entry != null)
              str = entry.ApplicationName;
            throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces_734"), (object) appId, (object) str));
          }
          throw new ProtectionException(this._packet.Message);
        }
      }

      private void CloseSublicense(NetworkKey.SubLicense sl)
      {
        lock (this._criticalSection)
        {
          this._packet._appId = this._applicationId;
          this._packet._algId = sl._appId;
          this._packet._bufLength = 0;
          this._packet.Guid = new Guid(sl._guid);
          this._packet._errorCode = 810;
          this.SendData(true);
        }
        if (this._packet._errorCode != 0)
          throw new ProtectionException(this._packet.Message);
      }

      private void InternalReleaseLicense(NetworkKey.SubLicense sl)
      {
        if (this._subLicenses.IndexOf(sl) == -1)
          return;
        this.CloseSublicense(sl);
        this._subLicenses.Remove(sl);
      }

      internal void SendMessage(string subject, string text)
      {
        if (NetworkKey.InformAdmins)
        {
          if (!(ProtectionService.GetService(typeof (IProtectionMessageService)) is IProtectionMessageService service))
            return;
          service.SendMessage(subject, text);
        }
        else
        {
          KeyHelper.WriteLine(subject);
          KeyHelper.WriteLine(text);
        }
      }

      public override bool AllocateLicense(int appId)
      {
        lock (this._criticalSection)
        {
          NetworkKey.SubLicense sublicense = this.FindSublicense(appId);
          if (sublicense != null)
          {
            sublicense.AddRef();
            return false;
          }
          this.InternalAllocateLiense(appId);
        }
        return true;
      }

      public override bool ReleaseLicense(int appId)
      {
        lock (this._criticalSection)
        {
          NetworkKey.SubLicense sublicense = this.FindSublicense(appId);
          if (sublicense != null)
          {
            if (sublicense.Release() == 0)
            {
              this.InternalReleaseLicense(sublicense);
              return true;
            }
          }
        }
        return false;
      }

      /// <summary>
      /// Класс-обертка над пакетом для передачи данных между
      /// сервером лицензий
      /// </summary>
      internal class PipePacket
      {
        internal int _appId;
        internal int _algId;
        internal int _errorCode;
        internal int _bufLength;
        internal int _response32;
        internal byte[] _guid;
        internal byte[] _buf;
        private const int BUFFSIZE = 1000;
        internal const int GUID_SIZE = 16 /*0x10*/;
        private const int HEADER_SIZE = 40;
        internal const int TOTAL_SIZE = 1040;

        public PipePacket()
        {
          this._appId = 0;
          this._algId = 0;
          this._errorCode = 0;
          this._response32 = 0;
          this._bufLength = 0;
          this._guid = new byte[16 /*0x10*/];
          this._buf = new byte[1000];
        }

        public byte[] Buffer
        {
          get
          {
            int count = this._bufLength + 1;
            int length = 40 + this._bufLength + 1;
            if (this._errorCode == 0)
            {
              length = 1040;
              count = 1000;
            }
            byte[] buffer = new byte[length];
            using (MemoryStream output = new MemoryStream(buffer))
            {
              using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
              {
                binaryWriter.Write(this._appId);
                binaryWriter.Write(this._algId);
                binaryWriter.Write(this._errorCode);
                binaryWriter.Write(this._bufLength);
                binaryWriter.Write(this._response32);
                binaryWriter.Write(this._guid);
                binaryWriter.Write(this._buf, 0, count);
              }
            }
            return buffer;
          }
          set
          {
            using (MemoryStream input = new MemoryStream(value))
            {
              using (BinaryReader binaryReader = new BinaryReader((Stream) input))
              {
                this._appId = binaryReader.ReadInt32();
                this._algId = binaryReader.ReadInt32();
                this._errorCode = binaryReader.ReadInt32();
                this._bufLength = binaryReader.ReadInt32();
                this._response32 = binaryReader.ReadInt32();
                this._guid = binaryReader.ReadBytes(16 /*0x10*/);
                binaryReader.Read(this._buf, 0, this._buf.Length);
              }
            }
          }
        }

        public string Message
        {
          get
          {
            int length = Array.IndexOf<byte>(this._buf, (byte) 0);
            if (length == 0)
              return string.Empty;
            char[] lpWideCharStr = new char[length];
            Intermech.Protection.Win32.MultiByteToWideChar(0, 0, this._buf, length, lpWideCharStr, length);
            return new string(lpWideCharStr);
          }
          set
          {
            int length = value.Length;
            Intermech.Protection.Win32.WideCharToMultiByte(0, 0, value, length, this._buf, length, 0, 0);
          }
        }

        public Guid Guid
        {
          get => new Guid(this._guid);
          set => Buffer.BlockCopy((Array) value.ToByteArray(), 0, (Array) this._guid, 0, 16 /*0x10*/);
        }
      }

      /// <summary>
      /// Лицензия, распределенная через
      /// основной коннект к менеджеру лицензий
      /// </summary>
      internal class SubLicense
      {
        internal int _appId;
        internal byte[] _guid;
        internal int _refCnt;

        public SubLicense(int appId, byte[] guid)
        {
          this._appId = appId;
          this._guid = (byte[]) guid.Clone();
          this._refCnt = 1;
        }

        internal int AddRef()
        {
          ++this._refCnt;
          return this._refCnt;
        }

        internal int Release()
        {
          if (this._refCnt > 0)
            --this._refCnt;
          return this._refCnt;
        }
      }
    }
}
