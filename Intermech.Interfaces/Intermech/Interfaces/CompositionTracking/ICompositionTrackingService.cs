
// Type: Intermech.Interfaces.CompositionTracking.ICompositionTrackingService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.CompositionTracking
{
    /// <summary>Composition tracking server service</summary>
    public interface ICompositionTrackingService
    {
      /// <summary>Get configuration value for type</summary>
      /// <param name="objTypeId">Object type ID</param>
      /// <param name="inObjTypeId">Owner object type ID</param>
      /// <param name="relTypeId">Relation type ID</param>
      /// <param name="value">Value</param>
      /// <param name="sessionGuid"></param>
      /// <returns>Is found in settings</returns>
      [Obsolete("Use method with IObjectTypeApplicabilityContext param")]
      bool GetConfigValue(
        int objTypeId,
        int inObjTypeId,
        int relTypeId,
        out CompositionsTrackingSettings value,
        Guid sessionGuid);

      /// <summary>Set configuration value for type</summary>
      /// <param name="objTypeId">Object type ID</param>
      /// <param name="inObjTypeId">Owner object type ID</param>
      /// <param name="relTypeId">Relation type ID</param>
      /// <param name="value">Value</param>
      /// <param name="sessionGuid"></param>
      [Obsolete("Use method with IObjectTypeApplicabilityContext param ")]
      void SetConfigValue(
        int objTypeId,
        int inObjTypeId,
        int relTypeId,
        CompositionsTrackingSettings value,
        Guid sessionGuid);

      /// <summary>Get configuration value for type</summary>
      /// <param name="objectTypeContext">Информация о типе объекта с контекстом</param>
      /// <param name="value">Value</param>
      /// <param name="sessionGuid"></param>
      /// <returns>Is found in settings</returns>
      bool GetConfigValue(
        Guid sessionGuid,
        IObjectTypeApplicabilityContext objectTypeContext,
        out CompositionsTrackingSettings value);

      /// <summary>Set configuration value for type</summary>
      /// <param name="objectTypeContext">Информация о типе объекта с контекстом</param>
      /// <param name="value">Value</param>
      /// <param name="sessionGuid"></param>
      void SetConfigValue(
        Guid sessionGuid,
        IObjectTypeApplicabilityContext objectTypeContext,
        CompositionsTrackingSettings value);

      /// <summary>
      /// Register tracking configuration for type ( show in configurator )
      /// </summary>
      /// <param name="objectTypeContext">Информация о типе объекта с контекстом</param>
      void RegisterTrackConfig(IObjectTypeApplicabilityContext objectTypeContext);

      /// <summary>
      /// Unregister tracking configuration for type ( hide in configurator )
      /// </summary>
      /// <param name="objectTypeContext">Информация о типе объекта с контекстом</param>
      /// <remarks>If was registered before</remarks>
      void UnregisterTrackConfig(IObjectTypeApplicabilityContext objectTypeContext);

      /// <summary>Is track configuration registered for type</summary>
      /// <param name="objectTypeContext">Информация о типе объекта с контекстом</param>
      /// <param name="inheritMode">Inherit mode - с проверкой наследования от родительских типов</param>
      /// <returns></returns>
      bool IsRegisteredTrackConfig(IObjectTypeApplicabilityContext objectTypeContext, bool inheritMode = true);

      /// <summary>Create composition tracking session</summary>
      /// <param name="sessionGuid">User session Guid</param>
      /// <returns></returns>
      ICompositionTrackingSession CreateTrackingSession(Guid sessionGuid);

      /// <summary>Dispose tracking session</summary>
      /// <param name="sessionGuid"></param>
      void DisposeTrackingSession(Guid sessionGuid);
    }
}
