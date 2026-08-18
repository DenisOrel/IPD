// Decompiled with JetBrains decompiler
// Type: Intermech.ComponentModel.Design.Serialization.DesignSerializerInterceptor`1
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.ComponentModel.Design.Serialization;

#nullable disable
namespace Intermech.ComponentModel.Design.Serialization;

public class DesignSerializerInterceptor<T> : CodeDomSerializer
{
  [NotNull]
  protected static CodeDomSerializer GetOriginalSerializer([NotNull] IDesignerSerializationManager manager)
  {
    object serializer = manager.GetSerializer(typeof (T).BaseType, typeof (CodeDomSerializer));
    Check.Assert<ItemNotFoundException>(serializer is CodeDomSerializer, $"CodeDomSerializer not found for class {typeof (T).BaseType}");
    return (CodeDomSerializer) serializer;
  }
}
