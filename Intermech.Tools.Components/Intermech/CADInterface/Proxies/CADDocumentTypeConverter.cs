// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADDocumentTypeConverter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;
using System;

#nullable disable
namespace Intermech.CADInterface.Proxies;

internal static class CADDocumentTypeConverter
{
  public static CADDocumentType ToProxyDocumentType(ECADDocType appDocType)
  {
    switch (appDocType)
    {
      case ECADDocType.CDT_Undefined:
        return CADDocumentType.Undefined;
      case ECADDocType.CDT_DefinedByTemplate:
        return CADDocumentType.DefinedByTemplate;
      case ECADDocType.CDT_Part:
        return CADDocumentType.Part;
      case ECADDocType.CDT_Assembly:
        return CADDocumentType.Assembly;
      case ECADDocType.CDT_Drawing:
        return CADDocumentType.Drawing;
      case ECADDocType.CDT_Skeleton:
        return CADDocumentType.Skeleton;
      case ECADDocType.CDT_Layout:
        return CADDocumentType.Layout;
      case ECADDocType.CDT_AssemblyInterchange:
        return CADDocumentType.AssemblyInterchange;
      case ECADDocType.CDT_Manufacturing:
        return CADDocumentType.Manufacturing;
      default:
        throw new NotSupportedEnumException((Enum) appDocType);
    }
  }

  public static ECADDocType ToNativeDocumentType(CADDocumentType proxyDocType)
  {
    switch (proxyDocType)
    {
      case CADDocumentType.Undefined:
        return ECADDocType.CDT_Undefined;
      case CADDocumentType.DefinedByTemplate:
        return ECADDocType.CDT_DefinedByTemplate;
      case CADDocumentType.Part:
        return ECADDocType.CDT_Part;
      case CADDocumentType.Assembly:
        return ECADDocType.CDT_Assembly;
      case CADDocumentType.Drawing:
        return ECADDocType.CDT_Drawing;
      case CADDocumentType.Skeleton:
        return ECADDocType.CDT_Skeleton;
      case CADDocumentType.Layout:
        return ECADDocType.CDT_Layout;
      case CADDocumentType.AssemblyInterchange:
        return ECADDocType.CDT_AssemblyInterchange;
      case CADDocumentType.Manufacturing:
        return ECADDocType.CDT_Manufacturing;
      default:
        throw new NotSupportedEnumException((Enum) proxyDocType);
    }
  }
}
