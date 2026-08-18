// Decompiled with JetBrains decompiler
// Type: Intermech.Client.Images
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Exceptions;
using IPS;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Client;

public abstract class Images
{
  [ContractAnnotation("throwExceptionIfNotFound:true => NotNull; throwExceptionIfNotFound:false => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static Image GetFromResources(
    [NotNull] Assembly assembly,
    [NotNull, NotWhitespace] string imageName,
    bool throwExceptionIfNotFound = true)
  {
    return Images.GetFromResources<Image>(assembly, imageName, throwExceptionIfNotFound);
  }

  [ContractAnnotation("throwExceptionIfNotFound:true => NotNull; throwExceptionIfNotFound:false => CanBeNull")]
  protected static TImage GetFromResources<TImage>(
    [NotNull] Assembly assembly,
    [NotNull, NotWhitespace] string imageName,
    bool throwExceptionIfNotFound = true)
    where TImage : Image
  {
    return Resources.CachedGet<TImage, ImageResourceNotFoundException>(assembly, imageName, (Func<Stream, TImage>) (stream =>
    {
      Image fromResources = Image.FromStream(stream);
      if (fromResources.RawFormat.Guid != ImageFormat.Icon.Guid && fromResources is Bitmap bitmap2)
        bitmap2.MakeTransparent();
      return (TImage) fromResources;
    }));
  }

  public static void LoadToNamedList(
    [NotNull] Assembly assembly,
    [NotNull, ItemNotEmpty, ItemNotWhitespace] params (string id, string name)[] nameAndIds)
  {
    foreach ((string id, string name) in nameAndIds)
      Images.LoadToNamedList(assembly, id, name);
  }

  public static int LoadToNamedList(
    [NotNull] Assembly assembly,
    [NotNull, NotWhitespace] string id,
    [NotNull, NotWhitespace] string name,
    bool throwExceptionIfNotFound = true)
  {
    string str = !Path.HasExtension(id) ? ".bmp" : string.Empty;
    Image fromResources = Images.GetFromResources(assembly, $".img.{id}{str}", throwExceptionIfNotFound);
    return fromResources != null ? Services.NamedList.Add(fromResources, name) : -1;
  }

  public static int LoadToNamedList([NotNull] Icon icon, [NotNull, NotWhitespace] string name)
  {
    return Services.NamedList.Add(icon, name);
  }
}
