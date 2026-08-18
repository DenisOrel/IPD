// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.MsoDocumentFormatter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using Intermech.Tools.Integrators.ComInterop;
using Intermech.Tools.MSOffice.ComInterop;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Tools.MSOffice;

internal sealed class MsoDocumentFormatter : OpenMetadataValueBagFormatter
{
  public override bool IsContainerSupported(IValueBagContainer container)
  {
    return container is OpenComDocument;
  }

  private OpenComDocument GetDocument(IValueBagContainer container) => (OpenComDocument) container;

  protected override ValueBag DoRead(IValueBagContainer container, ICollection<StringKey> valueKeys)
  {
    Tuple<object, object> propertiesCollections = this.GetDocumentPropertiesCollections(this.GetDocument(container));
    object obj1 = propertiesCollections.Item1;
    object obj2 = propertiesCollections.Item2;
    ValueBag valueBag1 = new ValueBag();
    foreach (StringKey valueKey in (IEnumerable<StringKey>) valueKeys)
    {
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, MsoDocumentFormatter, object, StringKey, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "FindProperty", (IEnumerable<Type>) null, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__0, this, obj1, valueKey);
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p2 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__1.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__1, obj3, (object) null);
      if (target1((CallSite) p2, obj4))
      {
        // ISSUE: reference to a compiler-generated field
        if (MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__3 = CallSite<Func<CallSite, MsoDocumentFormatter, object, StringKey, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "FindProperty", (IEnumerable<Type>) null, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        obj3 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__3.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__3, this, obj2, valueKey);
      }
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target2 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p5 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__5;
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__4.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__4, obj3, (object) null);
      if (!target2((CallSite) p5, obj5))
      {
        // ISSUE: reference to a compiler-generated field
        if (MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, MsoDocProperties>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (MsoDocProperties), typeof (MsoDocumentFormatter)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, MsoDocProperties> target3 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__7.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, MsoDocProperties>> p7 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__7;
        // ISSUE: reference to a compiler-generated field
        if (MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Type", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__6.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__6, obj3);
        Type dataType = MsoDocumentFormatter.TryConvertMsoPropertyTypeToDataType(target3((CallSite) p7, obj6));
        if (!(dataType == (Type) null))
        {
          try
          {
            // ISSUE: reference to a compiler-generated field
            if (MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__8 == null)
            {
              // ISSUE: reference to a compiler-generated field
              MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj7 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__8.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__8, obj3);
            if (obj7 != null)
            {
              valueBag1.Add(valueKey, Convert.ChangeType(obj7, dataType));
              // ISSUE: reference to a compiler-generated field
              if (MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__10 == null)
              {
                // ISSUE: reference to a compiler-generated field
                MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__10 = CallSite<Action<CallSite, ValueBag, StringKey, StringKey, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetFlag", (IEnumerable<Type>) null, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              Action<CallSite, ValueBag, StringKey, StringKey, object> target4 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__10.Target;
              // ISSUE: reference to a compiler-generated field
              CallSite<Action<CallSite, ValueBag, StringKey, StringKey, object>> p10 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__10;
              ValueBag valueBag2 = valueBag1;
              StringKey stringKey1 = valueKey;
              StringKey stringKey2 = NamedFlags.ReadOnly;
              // ISSUE: reference to a compiler-generated field
              if (MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__9 == null)
              {
                // ISSUE: reference to a compiler-generated field
                MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "LinkToContent", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              object obj8 = MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__9.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__2.\u003C\u003Ep__9, obj3);
              target4((CallSite) p10, valueBag2, stringKey1, stringKey2, obj8);
            }
          }
          catch (COMException ex)
          {
            if (ex.ErrorCode != -2147467259 /*0x80004005*/)
              throw;
          }
        }
      }
    }
    valueBag1.AcceptChanges();
    return valueBag1;
  }

  private static Type TryConvertMsoPropertyTypeToDataType(MsoDocProperties propType)
  {
    switch (propType)
    {
      case MsoDocProperties.msoPropertyTypeNumber:
        return typeof (int);
      case MsoDocProperties.msoPropertyTypeBoolean:
        return typeof (bool);
      case MsoDocProperties.msoPropertyTypeDate:
        return typeof (DateTime);
      case MsoDocProperties.msoPropertyTypeString:
        return typeof (string);
      case MsoDocProperties.msoPropertyTypeFloat:
        return typeof (double);
      default:
        return (Type) null;
    }
  }

  protected override void DoWrite(
    IValueBagContainer container,
    ContainerValues values,
    ICollection<StringKey> changedValues)
  {
    if (container == null)
      throw new ArgumentNullException(nameof (container));
    if (values == null)
      throw new ArgumentNullException(nameof (values));
    if (changedValues == null)
      throw new ArgumentNullException(nameof (changedValues));
    OpenComDocument document = this.GetDocument(container);
    Tuple<object, object> propertiesCollections = this.GetDocumentPropertiesCollections(document);
    object obj1 = propertiesCollections.Item1;
    object obj2 = propertiesCollections.Item2;
    List<ValueRecord> all = values.Bag.FindAll((Predicate<ValueRecord>) (record => changedValues.Contains(record.Key)));
    if (all.Count == 0)
      return;
    foreach (ValueRecord valueRecord in all)
    {
      string key = (string) valueRecord.Key;
      object obj3 = valueRecord.Value;
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, MsoDocumentFormatter, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "FindProperty", (IEnumerable<Type>) null, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__0, this, obj1, key);
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p2 = MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__1.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__1, obj4, (object) null);
      if (target1((CallSite) p2, obj5))
      {
        // ISSUE: reference to a compiler-generated field
        if (MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Value", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__3.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__3, obj4, obj3);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__4 = CallSite<Func<CallSite, MsoDocumentFormatter, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "FindProperty", (IEnumerable<Type>) null, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__4.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__4, this, obj2, key);
        // ISSUE: reference to a compiler-generated field
        if (MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target2 = MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__6.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p6 = MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__6;
        // ISSUE: reference to a compiler-generated field
        if (MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj8 = MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__5.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__5, obj7, (object) null);
        if (target2((CallSite) p6, obj8))
        {
          // ISSUE: reference to a compiler-generated field
          if (MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__7 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Value", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj9 = MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__7.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__7, obj7, obj3);
        }
        else
        {
          int msoPropertyType = (int) MsoDocumentFormatter.DataTypeToMsoPropertyType(valueRecord.DataType);
          // ISSUE: reference to a compiler-generated field
          if (MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__8 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__8 = CallSite<Action<CallSite, object, string, bool, int, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[6]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__8.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__8, obj2, key, false, msoPropertyType, obj3, (object) null);
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__9 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Saved", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj10 = MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__9.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__4.\u003C\u003Ep__9, document.ComObject, false);
  }

  private Tuple<object, object> GetDocumentPropertiesCollections(OpenComDocument doc)
  {
    // ISSUE: reference to a compiler-generated field
    if (MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Saved", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__0, doc.ComObject);
    object obj2;
    object obj3;
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "BuiltInDocumentProperties", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      obj2 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__1.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__1, doc.ComObject);
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "CustomDocumentProperties", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      obj3 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__2, doc.ComObject);
    }
    finally
    {
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__7.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p7 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__7;
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      object obj4;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__6.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__6, obj1))
      {
        // ISSUE: reference to a compiler-generated field
        if (MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target2 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__5.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p5 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__5;
        object obj5 = obj1;
        // ISSUE: reference to a compiler-generated field
        if (MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object> target3 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__4.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object>> p4 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__4;
        // ISSUE: reference to a compiler-generated field
        if (MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Saved", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__3.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__3, doc.ComObject);
        object obj7 = target3((CallSite) p4, obj6);
        obj4 = target2((CallSite) p5, obj5, obj7);
      }
      else
        obj4 = obj1;
      if (target1((CallSite) p7, obj4))
      {
        // ISSUE: reference to a compiler-generated field
        if (MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Saved", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj8 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__8.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__8, doc.ComObject, true);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__10 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, Tuple<object, object>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (Tuple<object, object>), typeof (MsoDocumentFormatter)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, Tuple<object, object>> target = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__10.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, Tuple<object, object>>> p10 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__10;
    // ISSUE: reference to a compiler-generated field
    if (MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__9 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__9 = CallSite<Func<CallSite, Type, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Create", (IEnumerable<Type>) new Type[2]
      {
        typeof (object),
        typeof (object)
      }, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj9 = MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__9.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__5.\u003C\u003Ep__9, typeof (Tuple), obj2, obj3);
    return target((CallSite) p10, obj9);
  }

  private object FindProperty(object documentProperties, string name)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p2 = MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target2 = MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p1 = MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__0, documentProperties);
      object obj2 = target2((CallSite) p1, obj1, 0);
      if (target1((CallSite) p2, obj2))
        return (object) null;
      // ISSUE: reference to a compiler-generated field
      if (MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Item", (IEnumerable<Type>) null, typeof (MsoDocumentFormatter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__3.Target((CallSite) MsoDocumentFormatter.\u003C\u003Eo__6.\u003C\u003Ep__3, documentProperties, name);
    }
    catch (ArgumentException ex)
    {
      return (object) null;
    }
  }

  private static MsoDocProperties DataTypeToMsoPropertyType(Type dataType)
  {
    if (dataType == typeof (bool))
      return MsoDocProperties.msoPropertyTypeBoolean;
    if (dataType == typeof (DateTime))
      return MsoDocProperties.msoPropertyTypeDate;
    if (dataType == typeof (double))
      return MsoDocProperties.msoPropertyTypeFloat;
    if (dataType == typeof (int))
      return MsoDocProperties.msoPropertyTypeNumber;
    if (dataType == typeof (string))
      return MsoDocProperties.msoPropertyTypeString;
    throw new NotSupportedException();
  }
}
