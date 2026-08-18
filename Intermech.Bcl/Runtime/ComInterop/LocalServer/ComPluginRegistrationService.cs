
// Type: Intermech.Runtime.ComInterop.LocalServer.ComPluginRegistrationService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Collections;
using Intermech.Pools;
using Intermech.Text;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Содержит сервисы, используемые при регистрации COM-классов.
    /// </summary>
    internal sealed class ComPluginRegistrationService
    {
      private const string InprocServerKey = "InprocServer32";
      private const string LocalServerKey = "LocalServer32";
      private const string TypeLibKey = "Typelib";
      private Wow64RegistrationServices wow64Helper;

      /// <summary>Создает объект.</summary>
      public ComPluginRegistrationService() => this.wow64Helper = new Wow64RegistrationServices();

      /// <summary>Регистрирует COM-класс в реестре Windows.</summary>
      /// <param name="comClass">COM-класс</param>
      /// <param name="pluginContext">Контекст сборки с реализацией COM-класса</param>
      /// <exception cref="T:System.ArgumentNullException">Параметры <paramref name="comClass" />, <paramref name="pluginContext" /> не должны быть равны null</exception>
      public void AfterRegisterTypeCallback(Type comClass, RegisterComPluginContext pluginContext)
      {
        if (comClass == (Type) null)
          throw new ArgumentNullException(nameof (comClass));
        if (pluginContext == null)
          throw new ArgumentNullException(nameof (pluginContext));
        try
        {
          string name = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "CLSID\\{0}", (object) comClass.GUID.ToString("B"));
          using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree))
          {
            if (key == null)
            {
              pluginContext.ErrorList.AddError(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_CantOpenRegistryKey, (object) name));
              return;
            }
            this.DeleteSubKeyTree(key, "InprocServer32");
            this.DeleteSubKeyTree(key, "LocalServer32");
            this.DeleteSubKeyTree(key, "Typelib");
            try
            {
              using (RegistryKey subKey = key.CreateSubKey("LocalServer32", RegistryKeyPermissionCheck.ReadWriteSubTree))
              {
                if (subKey == null)
                  throw new ComServerRegistrationException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_CantCreateRegistryKey, (object) "LocalServer32"));
                subKey.SetValue(string.Empty, (object) pluginContext.ComServer.HostApplication.ExecutablePath);
              }
              Guid typeLibraryId = this.GetTypeLibraryId(comClass, pluginContext);
              if (typeLibraryId != Guid.Empty)
              {
                using (RegistryKey subKey = key.CreateSubKey("Typelib", RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                  if (subKey == null)
                    throw new ComServerRegistrationException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_CantCreateRegistryKey, (object) "Typelib"));
                  subKey.SetValue(string.Empty, (object) typeLibraryId.ToString("B"));
                }
              }
            }
            catch
            {
              this.DeleteSubKeyTree(key, "LocalServer32");
              this.DeleteSubKeyTree(key, "Typelib");
              throw;
            }
          }
          this.wow64Helper.ApplyFixToRegisterType(comClass, RegistrationClassContext.LocalServer);
        }
        catch (ComServerRegistrationException ex)
        {
          pluginContext.ErrorList.AddError(ex.Message);
        }
        catch (SecurityException ex)
        {
          this.SaveError(ComServerResources.SR_RegisterComClassError, comClass, (Exception) ex, pluginContext.ErrorList);
        }
        catch (UnauthorizedAccessException ex)
        {
          this.SaveError(ComServerResources.SR_RegisterComClassError, comClass, (Exception) ex, pluginContext.ErrorList);
        }
        catch (IOException ex)
        {
          this.SaveError(ComServerResources.SR_RegisterComClassError, comClass, (Exception) ex, pluginContext.ErrorList);
        }
      }

      /// <summary>Отменяет регистрацию COM-класса в реестре Windows.</summary>
      /// <param name="comClass">COM-класс</param>
      /// <param name="pluginContext">Контекст сборки с реализацией COM-класса</param>
      /// <exception cref="T:System.ArgumentNullException">Параметры <paramref name="comClass" />, <paramref name="pluginContext" /> не должны быть равны null</exception>
      public void AfterUnregisterTypeCallback(Type comClass, UnregisterComPluginContext pluginContext)
      {
        if (comClass == (Type) null)
          throw new ArgumentNullException(nameof (comClass));
        if (pluginContext == null)
          throw new ArgumentNullException(nameof (pluginContext));
        try
        {
          string name = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "CLSID\\{0}", (object) comClass.GUID.ToString("B"));
          using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree))
          {
            if (key != null)
            {
              this.DeleteSubKeyTree(key, "LocalServer32");
              this.DeleteSubKeyTree(key, "Typelib");
            }
          }
          this.wow64Helper.ApplyFixToUnregisterType(comClass, RegistrationClassContext.LocalServer);
        }
        catch (SecurityException ex)
        {
          this.SaveError(ComServerResources.SR_UnregisterComClassError, comClass, (Exception) ex, pluginContext.ErrorList);
        }
        catch (UnauthorizedAccessException ex)
        {
          this.SaveError(ComServerResources.SR_UnregisterComClassError, comClass, (Exception) ex, pluginContext.ErrorList);
        }
      }

      private void DeleteSubKeyTree(RegistryKey key, string subKeyName)
      {
        RegistryKey registryKey = key.OpenSubKey(subKeyName);
        if (registryKey == null)
          return;
        registryKey.Close();
        key.DeleteSubKeyTree(subKeyName);
      }

      private void SaveError(string captionFormat, Type comClass, Exception x, IErrorList errorList)
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.AppendFormat((IFormatProvider) CultureInfo.CurrentUICulture, captionFormat, (object) Marshal.GenerateProgIdForType(comClass), (object) comClass.AssemblyQualifiedName);
          stringBuilder.Append(' ');
          stringBuilder.Append(x.Message);
          errorList.AddError(stringBuilder.ToString());
        }
      }

      private Guid GetTypeLibraryId(Type comClass, RegisterComPluginContext pluginContext)
      {
        object[] customAttributes = comClass.GetCustomAttributes(typeof (TypeLibGuidAttribute), true);
        if (customAttributes != null && customAttributes.Length != 0)
        {
          TypeLibGuidAttribute libGuidAttribute = (TypeLibGuidAttribute) customAttributes[0];
          if (libGuidAttribute.TypeLibId != Guid.Empty && libGuidAttribute.RequiredVersion != (Version) null && !TypeLibServices.IsRegistered(libGuidAttribute.TypeLibId, (short) libGuidAttribute.RequiredVersion.Major, (short) libGuidAttribute.RequiredVersion.Minor))
            throw new ComServerRegistrationException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_UnknownTypeLibSpecified, (object) libGuidAttribute.TypeLibId, (object) libGuidAttribute.RequiredVersion));
          return libGuidAttribute.TypeLibId;
        }
        return pluginContext.TypeLibIdList.Count != 0 ? CollectionUtils.GetFirstItem((IEnumerable<Guid>) pluginContext.TypeLibIdList) : Guid.Empty;
      }
    }
}
