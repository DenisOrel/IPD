
// Type: Intermech.Runtime.ComInterop.LocalServer.SingleThreadedObject
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Базовый класс для COM-объектов приложения, использующих потоковую модель STA.
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class SingleThreadedObject : StandardOleMarshalObject
    {
      /// <summary>Создает объект.</summary>
      protected SingleThreadedObject()
        : this(true)
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="addToTracker">Признак, что созданный объект требуется добавить в трекер используемых COM-объектов</param>
      internal SingleThreadedObject(bool addToTracker)
      {
        if (!addToTracker)
          return;
        if (TraceSwitches.General.TraceVerbose)
          Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.Trace_NewObjectInstanceCreated, (object) this.GetType().AssemblyQualifiedName));
        LiveComObjectsTracker comObjectsTracker = ComProcess.Instance.LiveComObjectsTracker;
        if (!comObjectsTracker.IsActive)
          return;
        comObjectsTracker.AddObject((object) this);
      }

      /// <summary>
      /// Создает и возвращает объект, который содержит всю необходимую информацию для создания прокси-объекта,
      /// используемого для удаленного взаимодействия с текущим объектом.
      /// </summary>
      /// <param name="requestedType">Тип прокси-объекта</param>
      /// <returns>Созданный сервисный объект</returns>
      public override ObjRef CreateObjRef(Type requestedType) => (ObjRef) null;

      /// <summary>
      /// Вызывается при регистрации COM-объекта в реестре Windows.
      /// </summary>
      /// <param name="comClass">Тип COM-объекта</param>
      [ComRegisterFunction]
      internal static void RegisterComClass(Type comClass)
      {
        RegisterCommandContext globalContextOrFail = RegisterCommandContext.GetGlobalContextOrFail();
        globalContextOrFail.RegistrationService.AfterRegisterTypeCallback(comClass, globalContextOrFail.PluginContext);
      }

      /// <summary>
      /// Вызывается при отмене регистрации COM-объекта в реестре Windows.
      /// </summary>
      /// <param name="comClass">Тип COM-объекта</param>
      [ComUnregisterFunction]
      internal static void UnregisterComClass(Type comClass)
      {
        UnregisterCommandContext globalContextOrFail = UnregisterCommandContext.GetGlobalContextOrFail();
        globalContextOrFail.RegistrationService.AfterUnregisterTypeCallback(comClass, globalContextOrFail.PluginContext);
      }
    }
}
