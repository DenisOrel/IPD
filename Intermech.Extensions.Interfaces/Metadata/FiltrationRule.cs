// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.FiltrationRule
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using System;

#nullable disable
namespace Intermech.Metadata;

public abstract class FiltrationRule
{
  public const string Unknown = null;
  public const string LatestVersions = "cad001df-306c-11d8-b4e9-00304f19f545";
  public const string LatestVersionsObject = "cad0069c-306c-11d8-b4e9-00304f19f545";
  public const string AllVersions = "cad001e0-306c-11d8-b4e9-00304f19f545";
  public const string AllVersionsObject = "cad001e3-306c-11d8-b4e9-00304f19f545";
  public const string BaseVersions = "cad00601-306c-11d8-b4e9-00304f19f545";
  public const string AllConcreteVersionsObject = "cad005ac-306c-11d8-b4e9-00304f19f5455";
  public const string SequentialModifications = "cad00602-306c-11d8-b4e9-00304f19f545";
  public const string UserDefaults = "cad001e2-306c-11d8-b4e9-00304f19f545";
  public const string DefaultVersionRule = "cad005aa-306c-11d8-b4e9-00304f19f545";
  public const string OverrideOwnerID = "{7196FEC5-A048-4118-AF15-73BEEAA63A87}";
  public const string OverrideEditingContext = "{76094280-391F-44AC-8B7B-9B6DEA501110}";

  public abstract class Guids
  {
    public static Guid Unknown;
    public static Guid LatestVersions = new Guid("cad001df-306c-11d8-b4e9-00304f19f545");
    public static Guid LatestVersionsObject = new Guid("cad0069c-306c-11d8-b4e9-00304f19f545");
    public static Guid AllVersions = new Guid("cad001e0-306c-11d8-b4e9-00304f19f545");
    public static Guid AllVersionsObject = new Guid("cad001e3-306c-11d8-b4e9-00304f19f545");
    public static Guid BaseVersions = new Guid("cad00601-306c-11d8-b4e9-00304f19f545");
    public static Guid AllConcreteVersionsObject = new Guid("cad005ac-306c-11d8-b4e9-00304f19f5455");
    public static Guid SequentialModifications = new Guid("cad00602-306c-11d8-b4e9-00304f19f545");
    public static Guid UserDefaults = new Guid("cad001e2-306c-11d8-b4e9-00304f19f545");
    public static Guid DefaultVersionRule = new Guid("cad005aa-306c-11d8-b4e9-00304f19f545");
    public static Guid OverrideOwnerID = new Guid("{7196FEC5-A048-4118-AF15-73BEEAA63A87}");
    public static Guid OverrideEditingContext = new Guid("{76094280-391F-44AC-8B7B-9B6DEA501110}");
  }
}
