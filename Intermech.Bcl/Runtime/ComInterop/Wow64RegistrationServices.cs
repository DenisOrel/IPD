
// Type: Intermech.Runtime.ComInterop.Wow64RegistrationServices
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Win32;
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop
{
    public class Wow64RegistrationServices
    {
      private readonly bool is64bitOS;
      private readonly bool is64bitProcess;
      private RegistryView activeRegView;
      private RegistryView oppositeRegView;

      public Wow64RegistrationServices()
      {
        this.activeRegView = RegistryView.Default;
        this.oppositeRegView = RegistryView.Default;
        this.is64bitOS = Environment.Is64BitOperatingSystem;
        this.is64bitProcess = Environment.Is64BitProcess;
        if (!this.is64bitOS)
          return;
        this.DetectActiveRegistryView();
      }

      private void DetectActiveRegistryView()
      {
        if (this.is64bitProcess)
        {
          this.activeRegView = RegistryView.Registry64;
          this.oppositeRegView = RegistryView.Registry32;
        }
        else
        {
          this.activeRegView = RegistryView.Registry32;
          this.oppositeRegView = RegistryView.Registry64;
        }
      }

      public RegistryView ActiveRegistryView => this.activeRegView;

      public RegistryView OppositeRegistryView => this.oppositeRegView;

      public bool IsRegistrationFixRequired(Type comObjectType)
      {
        return this.IsRegistrationFixRequired(comObjectType, RegistrationClassContext.InProcessServer);
      }

      public bool IsRegistrationFixRequired(
        Type comObjectType,
        RegistrationClassContext registrationContext)
      {
        if (comObjectType == (Type) null)
          throw new ArgumentNullException(nameof (comObjectType));
        if (!this.is64bitOS)
          return false;
        return (registrationContext & RegistrationClassContext.LocalServer) != (RegistrationClassContext) 0 || Wow64RegistrationServices.IsAnyCpyAssembly(comObjectType.Assembly);
      }

      public void ApplyFixToRegisterType(Type comObjectType)
      {
        this.ApplyFixToRegisterType(comObjectType, RegistrationClassContext.InProcessServer);
      }

      public void ApplyFixToRegisterType(
        Type comObjectType,
        RegistrationClassContext registrationContext)
      {
        if (comObjectType == (Type) null)
          throw new ArgumentNullException(nameof (comObjectType));
        if (!this.IsRegistrationFixRequired(comObjectType, registrationContext))
          return;
        RegistryKeyLocation sourceKey = new RegistryKeyLocation(RegistryHive.ClassesRoot, Path.Combine("CLSID", comObjectType.GUID.ToString("B")).ToUpper(), this.activeRegView);
        new CopyRegistryKeyTask(sourceKey, sourceKey.GetDifferentView(this.oppositeRegView)).Perform();
      }

      public void ApplyFixToUnregisterType(Type comObjectType)
      {
        this.ApplyFixToUnregisterType(comObjectType, RegistrationClassContext.InProcessServer);
      }

      public void ApplyFixToUnregisterType(
        Type comObjectType,
        RegistrationClassContext registrationContext)
      {
        if (comObjectType == (Type) null)
          throw new ArgumentNullException(nameof (comObjectType));
        if (!this.IsRegistrationFixRequired(comObjectType, registrationContext))
          return;
        using (RegistryBuilder registryBuilder = new RegistryBuilder(new RegistryKeyLocation(RegistryHive.ClassesRoot, Path.Combine("CLSID", comObjectType.GUID.ToString("B")).ToUpper(), this.oppositeRegView), true))
          registryBuilder.DeleteKey();
      }

      private static bool IsAnyCpyAssembly(Assembly assembly)
      {
        PortableExecutableKinds peKind;
        assembly.ManifestModule.GetPEKind(out peKind, out ImageFileMachine _);
        return peKind == PortableExecutableKinds.ILOnly;
      }
    }
}
