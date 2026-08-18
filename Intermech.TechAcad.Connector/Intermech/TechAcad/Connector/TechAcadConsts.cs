// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadConsts
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System;

#nullable disable
namespace Intermech.TechAcad.Connector;

public sealed class TechAcadConsts
{
  public const int TechAcadApiVersion = 2;
  public const char Params_Separator = ';';
  public const string Editor_Params = "Editor_Params";
  public const string Editor_Program = "Editor_Program";
  public const string File_Prototype = "File_Prototype";
  public const string Replace_Extent = "Replace_Extent";
  public const string Working_Dir = "Working_Dir";
  public static Guid ObjTypeAcadDraft = new Guid("cad00900-306c-11d8-b4e9-00304f19f545");
  public static Guid ObjTypeAcadAssemblyDraft = new Guid("cad00901-306c-11d8-b4e9-00304f19f545");
  public static Guid attributeSketchName = new Guid("cad009e9-306c-11d8-b4e9-00304f19f545");

  private TechAcadConsts()
  {
  }
}
