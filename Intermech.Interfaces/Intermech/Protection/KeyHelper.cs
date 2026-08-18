
// Type: Intermech.Protection.KeyHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Aladdin.HASP;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Protection
{
    internal static class KeyHelper
    {
      private static ushort[] AdditionalKeys = new ushort[3]
      {
        (ushort) 2,
        (ushort) 3,
        (ushort) 4
      };
      private static bool _connected = false;
      internal static ulong _serialNo;
      internal static string _keyScope;
      private static Hasp _mainKey;
      private static IOutputView _outputView;

      private static IOutputView OutputView
      {
        get
        {
          if (KeyHelper._outputView == null)
            KeyHelper._outputView = ProtectionService.GetService(typeof (IOutputView)) as IOutputView;
          return KeyHelper._outputView;
        }
      }

      internal static void WriteLine(string str)
      {
        KeyHelper.OutputView?.WriteString("Protection", str);
      }

      internal static void Connect(
        ref int appCount,
        ref int addSumm,
        ref DateTime leaseDate,
        out int daysLeft)
      {
        if (Win32.GetSystemMetrics(4096 /*0x1000*/) == 1)
          Process.GetCurrentProcess().Kill();
        daysLeft = 0;
        leaseDate = DateTime.MinValue;
        if (KeyHelper._connected)
          return;
        KeyHelper._keyScope = Scopes.ById(KeyHelper._serialNo = KeyHelper.FindLocalKey());
        KeyHelper._mainKey = new Hasp(HaspFeature.FromFeature(0));
        KeyHelper.CheckStatus(KeyHelper._mainKey.Login(VendorCode.Code, KeyHelper._keyScope));
        KeyHelper.InternalInitializeKey(KeyHelper._mainKey);
        appCount = 0;
        addSumm = 0;
        KeyHelper.ScanMainKey(KeyHelper._mainKey, ref appCount, ref addSumm);
        KeyHelper.ScanAdditionalsKeys(ref appCount, ref addSumm);
        byte[] query = (byte[]) null;
        byte[] reply1 = (byte[]) null;
        int length = KeyCodes.DateQueryLocal(ref query, ref reply1);
        byte[] reply2 = new byte[length];
        bool flag = false;
        Hasp hasp = new Hasp(HaspFeature.FromFeature(65470));
        try
        {
          switch (hasp.Login(VendorCode.Code, KeyHelper._keyScope))
          {
            case HaspStatus.FeatureNotFound:
              break;
            case HaspStatus.FeatureExpired:
              throw new LocalKeyOutOfDateException(LocalizationHolder.rm.GetString("LocalKeyOutOfDate"));
            default:
              string empty = string.Empty;
              string format = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><haspformat root=\"hasp_info\"><hasp>        <attribute name=\"id\" />        <attribute name=\"type\" />        <feature>            <attribute name=\"id\" /> \t         <element name=\"license\" />        </feature>    </hasp></haspformat>";
              KeyHelper.CheckStatus(hasp.GetSessionInfo(format, ref empty));
              int num1 = empty.IndexOf("<license_type>expiration</license_type>") != -1 ? empty.IndexOf("<exp_date>") : throw new ProtectionException("Invalid data for temporary key.");
              int num2 = empty.IndexOf("</exp_date>");
              long num3 = long.Parse(empty.Substring(num1 + 10, num2 - num1 - 10));
              flag = true;
              leaseDate = new DateTime(1970, 1, 1);
              leaseDate = leaseDate.AddSeconds((double) num3);
              TimeSpan timeSpan = leaseDate - DateTime.Now;
              daysLeft = timeSpan.Days;
              break;
          }
        }
        finally
        {
          hasp.Dispose();
        }
        KeyHelper.CheckStatus(KeyHelper.Encrypt(KeyHelper._mainKey, query, reply2));
        for (int index = 0; index < length; ++index)
        {
          if ((int) reply1[index] != (int) reply2[index])
          {
            if (flag && daysLeft <= 0)
              throw new LocalKeyOutOfDateException(LocalizationHolder.rm.GetString("LocalKeyOutOfDate"));
            throw new ProtectionException(LocalizationHolder.rm.GetString("Interfaces_95"));
          }
        }
        KeyHelper._connected = true;
      }

      private static ulong FindLocalKey()
      {
        try
        {
          byte[] numArray1 = new byte[16 /*0x10*/];
          numArray1[15] = (byte) 1;
          byte[] numArray2 = numArray1;
          string format = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><haspformat root=\"haspscope\">    <hasp>        <attribute name=\"id\" />    </hasp></haspformat>";
          string empty = string.Empty;
          KeyHelper.CheckStatus(Hasp.GetInfo(Scopes.LocalScope, format, VendorCode.Code, ref empty));
          List<ulong> ulongList = new List<ulong>(8);
          int num1 = 0;
          while (true)
          {
            num1 = empty.IndexOf("<hasp id=", num1 + 1);
            int num2 = empty.IndexOf(" />", num1 + 1);
            if (num1 != -1 && num2 != -1)
            {
              string str = empty.Substring(num1 + 10, num2 - num1 - 11);
              ulongList.Add(Convert.ToUInt64(str));
            }
            else
              break;
          }
          int count = ulongList.Count;
          for (int index1 = 0; index1 < count; ++index1)
          {
            ulong haspId = ulongList[index1];
            string scope = Scopes.ById(haspId);
            Hasp other = new Hasp(HaspFeature.Default);
            if (other.Login(VendorCode.Code, scope) == HaspStatus.StatusOk)
            {
              HaspFile haspFile = new HaspFile(HaspFileId.ReadOnly, other);
              byte[] numArray3 = new byte[16 /*0x10*/];
              byte[] buffer = numArray3;
              int num3 = (int) haspFile.Read(buffer, 0, 16 /*0x10*/);
              bool flag = true;
              for (int index2 = 0; index2 < 16 /*0x10*/; ++index2)
              {
                if ((int) numArray3[index2] != (int) numArray2[index2])
                {
                  flag = false;
                  break;
                }
              }
              other.Dispose();
              if (flag)
                return haspId;
            }
          }
        }
        catch (Exception ex)
        {
        }
        return 0;
      }

      internal static HaspStatus Encrypt(Hasp key, byte[] query, byte[] reply)
      {
        HaspStatus haspStatus1;
        if (query.Length < 16 /*0x10*/)
        {
          byte[] numArray = new byte[16 /*0x10*/];
          Array.Copy((Array) query, (Array) numArray, query.Length);
          haspStatus1 = key.Encrypt(numArray);
          Array.Copy((Array) numArray, (Array) reply, query.Length);
        }
        else
        {
          Array.Copy((Array) query, (Array) reply, reply.Length);
          haspStatus1 = key.Encrypt(reply);
        }
        if (haspStatus1 != HaspStatus.BrokenSession)
          return haspStatus1;
        HaspFeature feature = key.Feature;
        Hasp key1 = new Hasp(feature);
        int num1 = 30;
        int num2 = 0;
        HaspStatus haspStatus2;
        while (true)
        {
          haspStatus2 = key1.Login(VendorCode.Code, KeyHelper._keyScope);
          if (haspStatus2 != HaspStatus.StatusOk)
          {
            if (num2++ <= num1)
              Thread.Sleep(5000);
            else
              break;
          }
          else
            goto label_9;
        }
        return haspStatus2;
    label_9:
        if (feature == KeyHelper._mainKey.Feature)
        {
          KeyHelper._mainKey = key1;
          return KeyHelper.Encrypt(key1, query, reply);
        }
        key1.Dispose();
        int featureId = feature.FeatureId;
        KeyApplications.Login(featureId);
        return KeyHelper.Encrypt(KeyApplications.GetEntry(featureId).Key, query, reply);
      }

      private static void ScanAdditionalsKeys(ref int appCount, ref int addrSumm)
      {
      }

      private static void ScanMainKey(Hasp key, ref int appCount, ref int addrSumm)
      {
        string format = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><haspformat root=\"hasp_info\">        <feature>            <attribute name=\"id\" />        </feature></haspformat>";
        string empty = string.Empty;
        appCount = 0;
        addrSumm = 0;
        KeyHelper.CheckStatus(Hasp.GetInfo(KeyHelper._keyScope, format, VendorCode.Code, ref empty));
        string[] strArray = empty.Split(new string[4]
        {
          "<feature id=\"",
          "\" />",
          "\n",
          " "
        }, StringSplitOptions.RemoveEmptyEntries);
        List<int> intList = new List<int>(32 /*0x20*/);
        foreach (string s in strArray)
        {
          int result;
          if (int.TryParse(s, out result) && !intList.Contains(result))
          {
            intList.Add(result);
            ++appCount;
            addrSumm += result;
          }
        }
        foreach (IApplicationEntry application in KeyApplications.Applications)
        {
          if (intList.Contains(application.Id))
            KeyApplications.Login(application.Id);
        }
      }

      internal static void InternalInitializeKey(Hasp key)
      {
      }

      public static void CheckStatus(HaspStatus status)
      {
        if (status != HaspStatus.StatusOk)
          throw new KeyException(status);
      }

      public static Hasp MainKey => KeyHelper._mainKey;

      internal static string KeyScope
      {
        get => KeyHelper._keyScope;
        set => KeyHelper._keyScope = value;
      }
    }
}
