
// Type: Intermech.Protection.IProtectionKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Protection
{
    public interface IProtectionKey : IDisposable
    {
      void BadKeyAnswer();

      void CheckLicense();

      void CheckLicense2();

      void CheckLicense3();

      void QueryDate(byte[] CodesArray, byte XorVar, int CodesQuantityVar, ref int IsKeyOK);

      int LicensesCount { get; }

      string MonitorFileName { get; }

      string MonitorFileName2 { get; }

      int Version { get; }

      DateTime ServerDate { get; }

      string GetUnHashedString(byte[] queryArray, byte[] xorArray, ushort AlgoID);

      void LoadRandomRequest(ref byte[] queryData, ref byte[] response, ref ushort len);

      bool RandomQuery();

      bool RandomQuery(params byte[] P);

      void SaveRandomRequest(byte[] queryData, byte[] response, ushort len);

      int Decrement(bool QuietMode, ushort writePassword, ushort address);

      int LongQuery(byte[] queryData, byte[] response);

      int Query(bool QuietMode, int appId, byte[] queryData, byte[] response);

      int Read(bool QuietMode, int address, ref int Data);

      int ShortQuery(byte[] queryData, byte[] response);

      void ValidateExpiration(object aMainForm);

      int ApplicationID { get; }

      int LastErrorCode { get; }

      string LastErrorMessage { get; }

      IApplicationEntry[] Applications { get; }

      int TagValue { get; set; }

      IntPtr CheckHibernate();

      /// <summary>
      /// Вызывается после загрузки клиента/сервера для выполнения необходимых действий
      /// </summary>
      /// <param name=""></param>
      void PostLoad();
    }
}
