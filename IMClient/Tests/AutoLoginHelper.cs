
// Type: IMClient.Tests.AutoLoginHelper




using System;
using System.Diagnostics;
using System.Threading;


namespace IMClient.Tests
{
    internal static class AutoLoginHelper
    {
      private static readonly Lazy<bool> isTestMode = new Lazy<bool>(new Func<bool>(AutoLoginHelper.DetectTestMode));
      private static readonly object syncRoot = new object();
      private static bool isTestAgentConnected;
      private static EventHandler<InitializeLoginInfoEventArgs> initializeLoginInfo;

      public static bool IsTestMode
      {
        [DebuggerStepThrough] get => AutoLoginHelper.isTestMode.Value;
      }

      private static bool DetectTestMode()
      {
        bool flag = string.Equals(Environment.GetEnvironmentVariable("IPS_TESTMODE"), "1");
        if (flag)
          AutoLoginHelper.WaitForTestAgentConnection();
        return flag;
      }

      private static void WaitForTestAgentConnection()
      {
        for (int index = 30000; !AutoLoginHelper.isTestAgentConnected && index > 0; index -= 50)
          Thread.Sleep(50);
      }

      public static bool IsTestAgentConnected
      {
        [DebuggerStepThrough] get
        {
          lock (AutoLoginHelper.syncRoot)
            return AutoLoginHelper.isTestAgentConnected;
        }
        [DebuggerStepThrough] set
        {
          lock (AutoLoginHelper.syncRoot)
            AutoLoginHelper.isTestAgentConnected = value;
        }
      }

      public static EventHandler<InitializeLoginInfoEventArgs> InitializeLoginInfo
      {
        [DebuggerStepThrough] get
        {
          lock (AutoLoginHelper.syncRoot)
            return AutoLoginHelper.initializeLoginInfo;
        }
        [DebuggerStepThrough] set
        {
          lock (AutoLoginHelper.syncRoot)
            AutoLoginHelper.initializeLoginInfo = value;
        }
      }
    }
}
