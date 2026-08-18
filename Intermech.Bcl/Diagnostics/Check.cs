
// Type: Intermech.Diagnostics.Check
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;


namespace Intermech.Diagnostics
{
    /// <summary>Runtime валидация условий</summary>
    /// <summary>Runtime валидация условий</summary>
    /// <summary>Runtime валидация условий</summary>
    /// <summary>Runtime валидация условий</summary>
    /// <summary>Runtime валидация условий</summary>
    /// <summary>Runtime валидация условий</summary>
    public abstract class Check
    {
      /// <summary>Производить ли проверки</summary>
      public static bool Enabled = true;

      /// <exception cref="T:System.ArgumentException" />
      /// <returns cref="T:System.ArgumentException" />
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static ArgumentException CreateArgumentException([CanBeNull] string valueName, [CanBeNull] string message)
      {
        return string.IsNullOrWhiteSpace(message) ? (string.IsNullOrWhiteSpace(valueName) ? new ArgumentException("Argument does not satisfy the conditions.") : new ArgumentException($"Argument {valueName} does not satisfy the conditions.", valueName)) : (string.IsNullOrWhiteSpace(valueName) ? new ArgumentException(message) : new ArgumentException(message, valueName));
      }

      /// <exception cref="T:System.ArgumentException" />
      /// <returns cref="T:System.ArgumentException" />
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static ArgumentException CreateArgumentException([CanBeNull] string message)
      {
        return !string.IsNullOrWhiteSpace(message) ? new ArgumentException(message) : new ArgumentException("Argument does not satisfy the conditions.");
      }

      /// <exception cref="T:System.ArgumentNullException" />
      /// <returns cref="T:System.ArgumentNullException" />
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static ArgumentNullException CreateArgumentNullException([CanBeNull] string valueName, [CanBeNull] string message)
      {
        return string.IsNullOrWhiteSpace(message) ? (string.IsNullOrWhiteSpace(valueName) ? new ArgumentNullException((string) null, "Argument cannot be null.") : new ArgumentNullException(valueName, $"Argument {valueName} cannot be null.")) : (string.IsNullOrWhiteSpace(valueName) ? new ArgumentNullException((string) null, message) : new ArgumentNullException(valueName, message));
      }

      /// <exception cref="T:System.ArgumentNullException" />
      /// <returns cref="T:System.ArgumentNullException" />
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static ArgumentNullException CreateArgumentNullException([CanBeNull] string message)
      {
        return string.IsNullOrWhiteSpace(message) ? new ArgumentNullException((string) null, "Argument cannot be null.") : new ArgumentNullException((string) null, message);
      }

      /// <exception cref="T:System.ArgumentOutOfRangeException" />
      /// <returns cref="T:System.ArgumentOutOfRangeException" />
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static ArgumentOutOfRangeException CreateArgumentOutOfRangeException(
        [CanBeNull] string valueName,
        [CanBeNull] string message)
      {
        return string.IsNullOrWhiteSpace(message) ? (string.IsNullOrWhiteSpace(valueName) ? new ArgumentOutOfRangeException((string) null, "Argument is out of the range of valid values.") : new ArgumentOutOfRangeException(valueName, $"Argument {valueName} is out of the range of valid values.")) : (string.IsNullOrWhiteSpace(valueName) ? new ArgumentOutOfRangeException((string) null, message) : new ArgumentOutOfRangeException(valueName, message));
      }

      /// <exception cref="T:System.ArgumentOutOfRangeException" />
      /// <returns cref="T:System.ArgumentOutOfRangeException" />
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static ArgumentOutOfRangeException CreateArgumentOutOfRangeException([CanBeNull] string message)
      {
        return string.IsNullOrWhiteSpace(message) ? new ArgumentOutOfRangeException((string) null, "Argument is out of the range of valid values.") : new ArgumentOutOfRangeException((string) null, message);
      }

      /// <exception cref="T:System.NullReferenceException" />
      /// <returns cref="T:System.NullReferenceException" />
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static NullReferenceException CreateNullReferenceException(
        [CanBeNull] string valueName,
        [CanBeNull] string message)
      {
        if (!string.IsNullOrWhiteSpace(message))
          return new NullReferenceException(message);
        return string.IsNullOrWhiteSpace(valueName) ? new NullReferenceException("Value cannot be null.") : new NullReferenceException($"Value {valueName} cannot be null.");
      }

      /// <exception cref="T:System.NullReferenceException" />
      /// <returns cref="T:System.NullReferenceException" />
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static NullReferenceException CreateNullReferenceException([CanBeNull] string message)
      {
        return string.IsNullOrWhiteSpace(message) ? new NullReferenceException("Value cannot be null.") : new NullReferenceException(message);
      }

      /// <exception cref="T:System.InvalidCastException" />
      /// <returns cref="T:System.InvalidCastException" />
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static InvalidCastException CreateInvalidCastException(
        [NotNull] Type type,
        [CanBeNull] string valueName,
        [CanBeNull] string message)
      {
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Invalid cast of {valueName} value to {type} type" : $"Invalid cast to {type} type") : message;
        return new InvalidCastException(message);
      }

      /// <exception><cref>TException</cref></exception>
      /// <returns><cref>TException</cref></returns>
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static Exception CreateException<TException>(
        [CanBeNull] string message,
        [NotNull, InstantHandle] Func<string> defaultMessageConstructor)
        where TException : Exception
      {
        message = string.IsNullOrWhiteSpace(message) ? defaultMessageConstructor() : message;
        ConstructorInfo constructor1 = typeof (TException).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new Type[1]
        {
          typeof (string)
        }, (ParameterModifier[]) null);
        if (constructor1 != (ConstructorInfo) null)
          return (Exception) constructor1.Invoke(new object[1]
          {
            (object) message
          });
        ConstructorInfo constructor2 = typeof (TException).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new Type[2]
        {
          typeof (string),
          typeof (Exception)
        }, (ParameterModifier[]) null);
        if (constructor2 != (ConstructorInfo) null)
          return (Exception) constructor2.Invoke(new object[2]
          {
            (object) message,
            null
          });
        ConstructorInfo constructor3 = typeof (TException).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, Type.EmptyTypes, (ParameterModifier[]) null);
        return constructor3 != (ConstructorInfo) null ? (Exception) constructor3.Invoke(Array.Empty<object>()) : throw Check.CreateArgumentException((string) null, $"{typeof (TException)} has no correct constructor!{Environment.NewLine}{message}");
      }

      /// <exception><cref>TException</cref></exception>
      /// <returns><cref>TException</cref></returns>
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static Exception CreateException<TException>([CanBeNull] string message) where TException : Exception
      {
        ConstructorInfo constructor1 = typeof (TException).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new Type[1]
        {
          typeof (string)
        }, (ParameterModifier[]) null);
        if (constructor1 != (ConstructorInfo) null)
          return (Exception) constructor1.Invoke(new object[1]
          {
            (object) message
          });
        ConstructorInfo constructor2 = typeof (TException).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new Type[2]
        {
          typeof (string),
          typeof (Exception)
        }, (ParameterModifier[]) null);
        if (constructor2 != (ConstructorInfo) null)
          return (Exception) constructor2.Invoke(new object[2]
          {
            (object) message,
            null
          });
        ConstructorInfo constructor3 = typeof (TException).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, Type.EmptyTypes, (ParameterModifier[]) null);
        return constructor3 != (ConstructorInfo) null ? (Exception) constructor3.Invoke(Array.Empty<object>()) : throw Check.CreateArgumentException((string) null, $"{typeof (TException)} has no correct constructor!{Environment.NewLine}{message}");
      }

      /// <exception><cref>TException</cref></exception>
      /// <returns><cref>TException</cref></returns>
      [NotNull]
      [MustUseReturnValue]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static Exception CreateExceptionWithParams<TException>([NotNull, ItemNotNull] object[] exceptionParams) where TException : Exception
      {
        int length = exceptionParams.Length;
        Type[] types;
        if (length > 0)
        {
          types = new Type[length];
          for (int index = length - 1; index >= 0; --index)
            types[index] = exceptionParams[index].GetType();
        }
        else
          types = Array.Empty<Type>();
        ConstructorInfo constructor1 = typeof (TException).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, types, (ParameterModifier[]) null);
        if (constructor1 != (ConstructorInfo) null)
          return (Exception) constructor1.Invoke(exceptionParams);
        ConstructorInfo constructor2 = typeof (TException).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new Type[1]
        {
          typeof (string)
        }, (ParameterModifier[]) null);
        if (constructor2 != (ConstructorInfo) null)
          throw (object) (TException) constructor2.Invoke(new object[1]
          {
            (object) $"{typeof (TException)} has no correct constructor!"
          });
        throw Check.CreateArgumentException((string) null, $"{typeof (TException)} has no correct constructor!");
      }

      /// <summary>Проверка аргумента на null</summary>
      /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
      /// <param name="value">Объект, который не должен быть равен null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class
      {
        return !Check.Enabled || (object) value != null ? value : throw Check.CreateArgumentNullException(valueName, message);
      }

      /// <summary>Проверка аргумента на null</summary>
      /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
      /// <param name="value">Объект, который не должен быть равен null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentNotNull<T>([CanBeNull, NoEnumeration] T? value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct
      {
        return value.HasValue ? value.Value : throw Check.CreateArgumentNullException(valueName, message);
      }

      /// <summary>Проверка аргумента на null</summary>
      /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
      /// <param name="value">Объект, который не должен быть равен null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt; => NotNull")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentGenericNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || !typeof (T).IsByRef || (object) value != null)
          return value;
        throw Check.CreateArgumentNullException(valueName, message);
      }

      /// <summary>Проверка что аргумент не null</summary>
      /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
      /// <param name="value">Объект, который должен быть не null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentNotNull<T, TException>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        where T : class
        where TException : ArgumentNullException
      {
        return !Check.Enabled || (object) value != null ? value : throw Check.CreateException<TException>(message, (Func<string>) (() => string.IsNullOrWhiteSpace(valueName) ? "Argument is null." : $"Argument {valueName} is null."));
      }

      /// <summary>Проверка что аргумент не null</summary>
      /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
      /// <param name="value">Объект, который должен быть не null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentNotNull<T, TException>([CanBeNull, NoEnumeration] T? value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        where T : struct
        where TException : ArgumentNullException
      {
        return value.HasValue ? value.Value : throw Check.CreateException<TException>(message, (Func<string>) (() => string.IsNullOrWhiteSpace(valueName) ? "Argument is null." : $"Argument {valueName} is null."));
      }

      /// <summary>Проверка аргумента на значение по-умолчанию</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
      /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentValueNotEmpty<T>([NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct
      {
        return !Check.Enabled || !object.Equals((object) value, (object) default (T)) ? value : throw new ArgumentValueEmptyException(valueName, message);
      }

      /// <summary>Проверка что аргумент не пуст (пустое значение отличается от default)</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
      /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
      /// <param name="emptyValue">Пустое значение параметра</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentValueNotEmpty<T>(
        [NoEnumeration] T value,
        T emptyValue,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
        where T : struct
      {
        if (!Check.Enabled || !object.Equals((object) value, (object) emptyValue))
          return value;
        throw new ArgumentValueEmptyException(valueName, message);
      }

      /// <summary>Проверка что аргумент не пуст (пустое значение отличается от default)</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
      /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
      /// <param name="emptyValue1">Пустое значение параметра</param>
      /// <param name="emptyValue2">Пустое значение параметра</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentValueNotEmpty<T>(
        [NoEnumeration] T value,
        T emptyValue1,
        T emptyValue2,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
        where T : struct
      {
        if (!Check.Enabled || !object.Equals((object) value, (object) emptyValue1) && !object.Equals((object) value, (object) emptyValue2))
          return value;
        throw new ArgumentValueEmptyException(valueName, message);
      }

      /// <summary>Проверка что аргумент IntPtr не пуст (пустое значение отличается от IntPtr.Zero)</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == IntPtr.Zero</exception>
      /// <param name="value">Значение, которое не должно быть равно IntPtr.Zero</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IntPtr ArgumentValueNotEmpty(IntPtr value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || !object.Equals((object) value, (object) IntPtr.Zero))
          return value;
        throw new ArgumentValueEmptyException(valueName, message);
      }

      /// <summary>Проверка перечисление на отсутствие значений по-умолчанию</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если в перечислении присутствует значение == default(T)</exception>
      /// <param name="value">Перечисление значений, которые не должны быть равны значению по-умолчанию для своего типа</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ArgumentValuesNotEmpty<T>(
        [CanBeNull] IEnumerable<T> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
        where T : struct
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<IEnumerable<T>>(value, valueName);
        foreach (T objA in value)
        {
          if (object.Equals((object) objA, (object) default (T)))
            throw new ArgumentValueEmptyException(valueName, message);
        }
        return value;
      }

      /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
      /// переданное условие</exception>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="predicate">Условие</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ArgumentAll<T>(
        [CanBeNull] IEnumerable<T> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
        [NotNull, InstantHandle] Func<T, bool> predicate,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<IEnumerable<T>>(value, valueName);
        foreach (T obj in value)
        {
          if (!predicate(obj))
            throw new ArgumentItemValidationExceptionException(message);
        }
        return value;
      }

      /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="checkAction">Метод проверки условия</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ArgumentAll<T>(
        [CanBeNull] IEnumerable<T> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
        [NotNull, InstantHandle] Action<T> checkAction,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<IEnumerable<T>>(value, valueName);
        foreach (T obj in value)
          checkAction(obj);
        return value;
      }

      /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
      /// переданное условие</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="predicate">Условие</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ArgumentAll<T>(
        [CanBeNull] IEnumerable<T> value,
        [NotNull, InstantHandle] Func<T, bool> predicate,
        [CanBeNull] string message = null)
      {
        return Check.ArgumentAll<T>(value, (string) null, predicate, message);
      }

      /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
      /// переданное условие</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="checkAction">Метод проверки условия</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ArgumentAll<T>(
        [CanBeNull] IEnumerable<T> value,
        [NotNull, InstantHandle] Action<T> checkAction,
        [CanBeNull] string message = null)
      {
        return Check.ArgumentAll<T>(value, (string) null, checkAction, message);
      }

      /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
      /// переданное условие</exception>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="predicate">Условие</param>
      /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ArgumentAll<T>(
        [CanBeNull] IEnumerable<T> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
        [NotNull, InstantHandle] Func<T, bool> predicate,
        [NotNull] TemplateMessageFactory<T> messageFactory)
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<IEnumerable<T>>(value, valueName);
        foreach (T obj in value)
        {
          if (!predicate(obj))
            throw new ArgumentItemValidationExceptionException(messageFactory(obj));
        }
        return value;
      }

      /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
      /// переданное условие</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="predicate">Условие</param>
      /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ArgumentAll<T>(
        [CanBeNull] IEnumerable<T> value,
        [NotNull, InstantHandle] Func<T, bool> predicate,
        [NotNull] TemplateMessageFactory<T> messageFactory)
      {
        return Check.ArgumentAll<T>(value, (string) null, predicate, messageFactory);
      }

      /// <summary>Проверка что элементы коллекции не null</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentItemsNotNull<T>([CanBeNull] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<T>(value, valueName);
        foreach (object obj in (IEnumerable) value)
        {
          if (obj == null)
            throw new ArgumentItemNullsNotAllowedException((IEnumerable) value, valueName, message);
        }
        return value;
      }

      /// <summary>Проверка, что коллекция не пуста</summary>
      /// <exception cref="T:System.ArgumentNullException">Если коллекция равна null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentCollectionIsEmptyException">Если коллекция пуста</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TEnumerable ArgumentNotNullNotEmpty<TEnumerable>(
        [CanBeNull] TEnumerable value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull, CanBeEmpty] string message = null)
        where TEnumerable : class, IEnumerable
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<TEnumerable>(value, valueName);
        if (value is ICollection collection)
        {
          if (collection.Count == 0)
            throw new ArgumentCollectionIsEmptyException(valueName, message);
        }
        else
        {
          IEnumerator enumerator = value.GetEnumerator();
          try
          {
            if (!enumerator.MoveNext())
              throw new ArgumentCollectionIsEmptyException(valueName, message);
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        }
        return value;
      }

      /// <summary>Проверка что элементы коллекции не null</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ArgumentItemsNotNull<T>(
        [CanBeNull] IEnumerable<T?> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
        where T : struct
      {
        Check.ArgumentNotNull<IEnumerable<T?>>(value, valueName);
        foreach (T? nullable in value)
        {
          if (!nullable.HasValue)
            throw new ArgumentItemNullsNotAllowedException((IEnumerable) value, valueName, message);
        }
        return value.Cast<T>();
      }

      /// <summary>Проверка что коллекция не пуста</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
      /// <param name="value">Коллекция, которая не должна быть пустой</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentCollectionNotEmpty<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, ICollection
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<T>(value, valueName);
        return value.Count != 0 ? value : throw new ArgumentCollectionIsEmptyException(valueName, message);
      }

      /// <summary>Проверка что коллекция не пуста</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
      /// <param name="value">Коллекция, которая не должна быть пустой</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IReadOnlyCollection<T> ArgumentReadOnlyCollectionNotEmpty<T>(
        [CanBeNull, NoEnumeration] IReadOnlyCollection<T> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<IReadOnlyCollection<T>>(value, valueName);
        return value.Count != 0 ? value : throw new ArgumentCollectionIsEmptyException(valueName, message);
      }

      /// <summary>Проверка что коллекция не пуста</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
      /// <param name="value">Коллекция, которая не должна быть пустой</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TCollection ArgumentReadOnlyCollectionNotEmpty<TCollection, T>(
        [CanBeNull, NoEnumeration] TCollection value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
        where TCollection : class, IReadOnlyCollection<T>
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<TCollection>(value, valueName);
        return value.Count != 0 ? value : throw new ArgumentCollectionIsEmptyException(valueName, message);
      }

      /// <summary>Проверка что последовательность не пусто</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если последовательность пусто</exception>
      /// <param name="value">Последовательность, которая не должна быть пустой</param>
      /// <param name="valueName">Наименование последовательности</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentEnumerationNotEmpty<T>([CanBeNull] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<T>(value, valueName);
        if (value is ICollection collection)
        {
          if (collection.Count == 0)
            throw new ArgumentCollectionIsEmptyException(valueName, message);
        }
        else
        {
          IEnumerator enumerator = value.GetEnumerator();
          try
          {
            if (!enumerator.MoveNext())
              throw new ArgumentCollectionIsEmptyException(valueName, message);
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        }
        return value;
      }

      /// <summary>Проверка что все строки в последовательности не null и не пусты</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
      /// <param name="value">Последовательность строк, которые быть не должны быть равны null или string.Empty</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [ItemNotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<string> ArgumentStringsNotEmpty(
        [CanBeNull] IEnumerable<string> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<IEnumerable<string>>(value, valueName);
        foreach (string str in value)
        {
          if (str == null)
            throw new ArgumentItemNullsNotAllowedException((IEnumerable) value, valueName, message);
          if (str == string.Empty)
            throw new ArgumentItemEmptyStringNotAllowedException(value, valueName, message);
        }
        return value;
      }

      /// <summary>Проверка что все строки последовательности не null, не пустые строки и не строки состоящие из одних пробелов</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemWhitespaceNotAllowedException">Если в последовательности присутствуют строки не
      /// содержащие ничего кроме пробелов</exception>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <param name="value">Последовательность строк, которые быть не должны быть равны null, string.Empty или заполнены
      /// одними только пробелами</param>
      /// <param name="valueName">(Optional) Наименование коллекции</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [ItemNotWhitespace]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<string> ArgumentStringsNotWhitespace(
        [CanBeNull] IEnumerable<string> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<IEnumerable<string>>(value, valueName);
        foreach (string str in value)
        {
          if (str == null)
            throw new ArgumentItemNullsNotAllowedException((IEnumerable) value, valueName, message);
          if (str == string.Empty)
            throw new ArgumentItemEmptyStringNotAllowedException(value, valueName, message);
          if (string.IsNullOrWhiteSpace(str))
            throw new ArgumentItemWhitespaceNotAllowedException(value, valueName, message);
        }
        return value;
      }

      /// <summary>Проверка что элементы коллекции не null и не DBNull</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если коллекция содержит null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если коллекция содержит DBNull</exception>
      /// <param name="value">Коллекция, элементы которой должен быть не null</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [ItemNotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentItemsNotNullNotDbNull<T>([CanBeNull] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<T>(value, valueName);
        foreach (object objB in (IEnumerable) value)
        {
          if (objB == null)
            throw new ArgumentItemNullsNotAllowedException((IEnumerable) value, valueName, message);
          if (object.Equals((object) DBNull.Value, objB))
          {
            message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} collection cannot contains DBNull values." : "Argument collection cannot contains DBNull values.") : message;
            throw new ArgumentItemNullsNotAllowedException((IEnumerable) value, valueName, message);
          }
        }
        return value;
      }

      /// <summary>Проверка строкового аргумента на null и на равенство string.Empty</summary>
      /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
      /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string ArgumentNotNullOrEmpty([CanBeNull] string value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<string>(value, valueName);
        return !object.Equals((object) value, (object) string.Empty) ? value : throw new ArgumentEmptyStringNotAllowedException(valueName, message);
      }

      /// <summary>Проверка строкового аргумента на null и на равенство string.Empty или состоять только из пробелов</summary>
      /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
      /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty, или состоять только из пробелов</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [NotWhitespace]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string ArgumentNotNullOrWhitespace([CanBeNull] string value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<string>(value, valueName);
        if (value == string.Empty)
          throw new ArgumentEmptyStringNotAllowedException(valueName, message);
        return !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentWhitespaceNotAllowedException(valueName, message);
      }

      /// <summary>Проверка аргумента на null и DBNull</summary>
      /// <exception cref="T:System.ArgumentNullException">Если аргумент == null или == DBNull</exception>
      /// <param name="value">Объект, который не должен быть равен null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentNotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNull<T>(value);
        if (object.Equals((object) DBNull.Value, (object) value))
        {
          message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} collection cannot contains DBNull values." : "Argument collection cannot contains DBNull values.") : message;
          throw Check.CreateArgumentNullException(valueName, message);
        }
        return value;
      }

      /// <summary>Проверка аргумента</summary>
      /// <exception cref="T:System.ArgumentException">Если условие проверки не выполняется</exception>
      /// <param name="condition">Условие проверки значения аргумента</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Argument(bool condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (Check.Enabled && !condition)
          throw Check.CreateArgumentException(valueName, message);
      }

      /// <summary>Проверка аргумента</summary>
      /// <exception cref="T:System.ArgumentException">Если условие проверки не выполняется</exception>
      /// <param name="value">Возвращаемое значение если проверка будет выполнена</param>
      /// <param name="condition">Условие проверки значения аргумента</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt; value:null => null")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Argument<T>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || condition(value))
          return value;
        throw Check.CreateArgumentException(valueName, message);
      }

      /// <summary>Проверка условия</summary>
      /// <exception><cref>ArgumentException</cref>: Если условие не выполняется</exception>
      /// <param name="value">Возвращаемое значение если проверка пройдена</param>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="exceptionParams">Параметры, которые будут переданы в конструктор исключительной ситуации</param>
      [ContractAnnotation("condition:false => halt; value:null => null")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Argument<T, TException>(
        [CanBeNull, NoEnumeration] T value,
        [NotNull, InstantHandle] Func<T, bool> condition,
        [NotNull, ItemNotNull] object[] exceptionParams)
        where TException : ArgumentException
      {
        if (!Check.Enabled || condition(value))
          return value;
        throw Check.CreateExceptionWithParams<TException>(exceptionParams);
      }

      /// <summary>Проверка условия</summary>
      /// <exception><cref>ArgumentException</cref>: Если условие не выполняется</exception>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Argument<TException>(bool condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where TException : ArgumentException
      {
        if (!condition)
          throw Check.CreateException<TException>(message, (Func<string>) (() => string.IsNullOrWhiteSpace(valueName) ? "Argument does not satisfy the conditions." : $"Argument {valueName} does not satisfy the conditions."));
      }

      /// <summary>Проверка условия</summary>
      /// <exception><cref>ArgumentException</cref>: Если условие не выполняется</exception>
      /// <param name="value">Возвращаемое значение если проверка пройдена</param>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt; value:null => null")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Argument<T, TException>(
        [CanBeNull, NoEnumeration] T value,
        [NotNull, InstantHandle] Func<T, bool> condition,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
        where TException : ArgumentException
      {
        if (!Check.Enabled || condition(value))
          return value;
        throw Check.CreateException<TException>(message, (Func<string>) (() => string.IsNullOrWhiteSpace(valueName) ? "Argument does not satisfy the conditions." : $"Argument {valueName} does not satisfy the conditions."));
      }

      /// <summary>Проверка аргумента</summary>
      /// <exception cref="T:System.ArgumentException">Если условие проверки не выполняется</exception>
      /// <param name="condition">Условие проверки значения аргумента</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Argument([NotNull, InstantHandle] Func<bool> condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (Check.Enabled && !condition())
          throw Check.CreateArgumentException(valueName, message);
      }

      /// <summary>Проверка попадания значения аргумента в список допустимых значений</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
      /// <param name="condition">Условие проверки значения аргумента</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void ArgumentInRange(bool condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (Check.Enabled && !condition)
          throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка попадания значения аргумента в список допустимых значений</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
      /// <param name="value">Возвращаемое значение если проверка будет выполнена</param>
      /// <param name="condition">Условие проверки значения аргумента</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt; value:null => null")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentInRange<T>(
        [CanBeNull, NoEnumeration] T value,
        [NotNull, InstantHandle] Func<T, bool> condition,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled || condition(value))
          return value;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка попадания значения аргумента в список допустимых значений</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
      /// <param name="value">Возвращаемое значение если проверка будет выполнена</param>
      /// <param name="condition">Условие проверки значения аргумента</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt; value:null => null")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ArgumentInRange<T>(
        [CanBeNull, NoEnumeration] T value,
        [NotNull, InstantHandle] Func<bool> condition,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled || condition())
          return value;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
      /// <param name="index">Значение индекса</param>
      /// <param name="count">Число элементов коллекции</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int ArgumentIndexInRange(int index, int count, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return index;
        if (index < 0)
        {
          message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be zero or positive number." : "Index must be zero or positive number.") : message;
          throw new ArgumentOutOfRangeException(valueName, message);
        }
        if (index >= count)
        {
          message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} goes beyond the collection items count ({count})." : "Index in arguments goes beyond the collection items count ({count}).") : message;
          throw new ArgumentOutOfRangeException(valueName, message);
        }
        return index;
      }

      /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
      /// <param name="index">Значение индекса</param>
      /// <param name="count">Число элементов коллекции</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static long ArgumentIndexInRange(
        long index,
        long count,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return index;
        if (index < 0L)
        {
          message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be zero or positive number." : "Index must be zero or positive number.") : message;
          throw new ArgumentOutOfRangeException(valueName, message);
        }
        if (index >= count)
        {
          message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} goes beyond the collection items count ({count})." : "Index in arguments goes beyond the collection items count ({count}).") : message;
          throw new ArgumentOutOfRangeException(valueName, message);
        }
        return index;
      }

      /// <summary>Проверка что строка содержит guid</summary>
      /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
      /// <exception cref="T:System.ArgumentException">Если строка не содержит GUID</exception>
      /// <param name="guid">Строка, которая должна содержать Guid</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotNull]
      [GuidStr]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string ArgumentIsGuid([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return guid;
        Check.ArgumentNotNullOrWhitespace(guid, valueName);
        if (Guid.TryParse(guid, out Guid _))
          return guid;
        if (!string.IsNullOrWhiteSpace(valueName))
          throw new ArgumentException(!string.IsNullOrWhiteSpace(message) ? message : $"Value {valueName} must contain GUID!", valueName);
        throw new ArgumentException(!string.IsNullOrWhiteSpace(message) ? message : "Must contain GUID!");
      }

      /// <summary>Проверка что строка содержит непустой guid</summary>
      /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
      /// <exception cref="T:System.ArgumentException">Если строка не содержит GUID</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если GUID пуст</exception>
      /// <param name="guid">Строка, которая должна содержать непустой Guid</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotNull]
      [NotEmptyGuid]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string ArgumentGuidNotEmpty([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return guid;
        Check.NotNullOrWhitespace(guid, valueName);
        Guid result;
        if (!Guid.TryParse(guid, out result))
        {
          if (!string.IsNullOrWhiteSpace(valueName))
            throw new ArgumentException(!string.IsNullOrWhiteSpace(message) ? message : $"Value {valueName} must contain GUID!", valueName);
          throw new ArgumentException(!string.IsNullOrWhiteSpace(message) ? message : "Must contain GUID!");
        }
        if (result == Guid.Empty)
          throw new ArgumentValueEmptyException(!string.IsNullOrWhiteSpace(valueName) ? valueName : (string) null, !string.IsNullOrWhiteSpace(message) ? message : (!string.IsNullOrWhiteSpace(valueName) ? $"Value {valueName} must contain non-empty GUID!" : "Must contain non-empty GUID!"));
        return guid;
      }

      /// <summary>Проверка что guid не пуст</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если guid == Guid.Empty</exception>
      /// <param name="guid">Guid, который не должен быть равен Guid.Empty</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static Guid ArgumentGuidNotEmpty(Guid guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || !(guid == Guid.Empty))
          return guid;
        throw new ArgumentValueEmptyException(valueName, message);
      }

      /// <summary>Проверка что в словаре присутствует запись с переданным ключом</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNotFoundException">Если ключ не найден</exception>
      /// <param name="dictionary">Словарь</param>
      /// <param name="key">Ключ, который должен присутствовать в словаре</param>
      /// <param name="dictionaryName">Наименование словаря</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("dictionary:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IReadOnlyDictionary<TKey, TValue> ArgumentContainsKey<TKey, TValue>(
        [CanBeNull, NoEnumeration] IReadOnlyDictionary<TKey, TValue> dictionary,
        [NotNull, NoEnumeration] TKey key,
        [CanBeNull, NotWhitespace, InvokerParameterName] string dictionaryName,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return dictionary;
        Check.ArgumentNotNull<IReadOnlyDictionary<TKey, TValue>>(dictionary, nameof (dictionary));
        return dictionary.ContainsKey(key) ? dictionary : throw new ArgumentItemNotFoundException<TKey>(key, Check.ArgumentNotNullOrWhitespace(dictionaryName, nameof (dictionaryName)), message);
      }

      /// <summary>Проверка того, что значение больше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [PositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int ArgumentIsPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value > 0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a positive number." : "Argument value must be a positive number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение больше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [PositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static long ArgumentIsPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value > 0L)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a positive number." : "Argument value must be a positive number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение больше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [PositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static float ArgumentIsPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || (double) value > 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a positive number." : "Argument value must be a positive number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение больше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [PositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static double ArgumentIsPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value > 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a positive number." : "Argument value must be a positive number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение равно или больше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int ArgumentIsZeroOrPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value >= 0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a zero or positive number." : "Argument value must be a zero or positive number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение равно или больше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static long ArgumentIsZeroOrPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value >= 0L)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a zero or positive number." : "Argument value must be a zero or positive number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение равно или больше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static float ArgumentIsZeroOrPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || (double) value >= 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a zero or positive number." : "Argument value must be a zero or positive number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение равно или больше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static double ArgumentIsZeroOrPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value >= 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a zero or positive number." : "Argument value must be a zero or positive number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение меньше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NegativeNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int ArgumentIsNegative(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value < 0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a negative number." : "Argument value be a negative number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение меньше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NegativeNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static long ArgumentIsNegative(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value < 0L)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a negative number." : "Argument value must be a negative number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение меньше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NegativeNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static float ArgumentIsNegative(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || (double) value < 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a negative number." : "Argument value must be a negative number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение меньше нуля</summary>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NegativeNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static double ArgumentIsNegative(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value < 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Argument {valueName} must be a negative number." : "Argument value must be a negative number.") : message;
        throw Check.CreateArgumentOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка что строка содержит guid</summary>
      /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
      /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
      /// <exception cref="T:System.FormatException">Если строка не содержит GUID</exception>
      /// <param name="guid">Строка, которая должна содержать Guid</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotNull]
      [NotEmptyGuid]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string IsGuid([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return guid;
        Check.NotNullOrWhitespace(guid, valueName);
        return Guid.TryParse(guid, out Guid _) ? guid : throw new FormatException(!string.IsNullOrWhiteSpace(message) ? message : (!string.IsNullOrWhiteSpace(valueName) ? $"Value {valueName} must contain GUID!" : "Must contain GUID!"));
      }

      /// <summary>Проверка что строка содержит непустой guid</summary>
      /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
      /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
      /// <exception cref="T:System.FormatException">Если строка не содержит GUID</exception>
      /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если GUID пуст</exception>
      /// <param name="guid">Строка, которая должна содержать непустой Guid</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotNull]
      [NotEmptyGuid]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string GuidNotEmpty([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return guid;
        Check.NotNullOrWhitespace(guid, valueName);
        Guid result;
        if (!Guid.TryParse(guid, out result))
          throw new FormatException(!string.IsNullOrWhiteSpace(message) ? message : (!string.IsNullOrWhiteSpace(valueName) ? $"Value {valueName} must contain GUID!" : "Must contain GUID!"));
        if (result == Guid.Empty)
          throw new ValueEmptyException(!string.IsNullOrWhiteSpace(valueName) ? valueName : (string) null, !string.IsNullOrWhiteSpace(message) ? message : (!string.IsNullOrWhiteSpace(valueName) ? $"Value {valueName} must contain non-empty GUID!" : "Must contain non-empty GUID!"));
        return guid;
      }

      /// <summary>Проверка что guid не пуст</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если guid == Guid.Empty</exception>
      /// <param name="guid">Guid, который не должен быть равен Guid.Empty</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static Guid GuidNotEmpty(Guid guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || !(guid == Guid.Empty))
          return guid;
        throw new ValueEmptyException(message);
      }

      /// <summary>Проверка что объект не null</summary>
      /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
      /// <param name="value">Объект, который не должен быть равен null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T NotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class
      {
        return !Check.Enabled || (object) value != null ? value : throw Check.CreateNullReferenceException(valueName, message);
      }

      /// <summary>Проверка что объект не null</summary>
      /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
      /// <param name="value">Объект, который не должен быть равен null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T NotNull<T>([CanBeNull, NoEnumeration] T? value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct
      {
        return value.HasValue ? value.Value : throw Check.CreateNullReferenceException(valueName, message);
      }

      /// <summary>Проверка что объект не null</summary>
      /// <exception cref="T:System.NullReferenceException">Если аргумент == null</exception>
      /// <param name="value">Объект, который не должен быть равен null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value: null => halt; => NotNull")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T GenericNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || !typeof (T).IsByRef || (object) value != null)
          return value;
        throw Check.CreateNullReferenceException(valueName, message);
      }

      /// <summary>Проверка значения на значение по-умолчанию</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
      /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ValueNotEmpty<T>([NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct
      {
        return !Check.Enabled || !object.Equals((object) value, (object) default (T)) ? value : throw new ValueEmptyException(valueName, message);
      }

      /// <summary>Проверка что значение не пусто (пустое значение отличается от default)</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
      /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
      /// <param name="emptyValue">Пустое значение параметра</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ValueNotEmpty<T>([NoEnumeration] T value, T emptyValue, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct
      {
        if (!Check.Enabled || !object.Equals((object) value, (object) emptyValue))
          return value;
        throw new ValueEmptyException(valueName, message);
      }

      /// <summary>Проверка что значение не пусто (пустое значение отличается от default)</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
      /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
      /// <param name="emptyValue1">Пустое значение параметра</param>
      /// <param name="emptyValue2">Пустое значение параметра</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ValueNotEmpty<T>(
        [NoEnumeration] T value,
        T emptyValue1,
        T emptyValue2,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
        where T : struct
      {
        if (!Check.Enabled || !object.Equals((object) value, (object) emptyValue1) && !object.Equals((object) value, (object) emptyValue2))
          return value;
        throw new ValueEmptyException(valueName, message);
      }

      /// <summary>Проверка что значение IntPtr не пусто (пустое значение отличается от IntPtr.Zero)</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == IntPtr.Zero</exception>
      /// <param name="value">Значение, которое не должно быть равно IntPtr.Zero</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IntPtr ValueNotEmpty(IntPtr value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || !object.Equals((object) value, (object) IntPtr.Zero))
          return value;
        throw new ValueEmptyException(valueName, message);
      }

      /// <summary>Проверка попадания значения в допустимый диапазон</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если значение выходит за рамки допустимого диапазона значений</exception>
      /// <param name="value">Значение, которое должно попадать в допустимый диапазон</param>
      /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => null")]
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T InRange<T>([CanBeNull] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || condition(value))
          return value;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка попадания значения в допустимый диапазон</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если значение выходит за рамки допустимого диапазона значений</exception>
      /// <param name="value">Значение, которое должно попадать в допустимый диапазон</param>
      /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => null")]
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T InRange<T>([CanBeNull] T value, [NotNull, InstantHandle] Func<bool> condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || condition())
          return value;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка попадания значения в допустимый диапазон</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если значение выходит за рамки допустимого диапазона значений</exception>
      /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void InRange(bool condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (Check.Enabled && !condition)
          throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
      /// <param name="index">Значение индекса</param>
      /// <param name="count">Число элементов коллекции</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int IndexInRange(int index, int count, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return index;
        if (index < 0)
        {
          message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be zero or positive number." : "Index must be zero or positive number.") : message;
          throw new ValueOutOfRangeException(valueName, message);
        }
        if (index >= count)
        {
          message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"{valueName} goes beyond the collection items count ({count})." : "Index goes beyond the collection items count ({count}).") : message;
          throw new ValueOutOfRangeException(valueName, message);
        }
        return index;
      }

      /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
      /// <param name="index">Значение индекса</param>
      /// <param name="count">Число элементов коллекции</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static long IndexInRange(long index, long count, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return index;
        if (index < 0L)
        {
          message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be zero or positive number." : "Index must be zero or positive number.") : message;
          throw new ValueOutOfRangeException(valueName, message);
        }
        if (index >= count)
        {
          message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"{valueName} goes beyond the collection items count ({count})." : "Index goes beyond the collection items count ({count}).") : message;
          throw new ValueOutOfRangeException(valueName, message);
        }
        return index;
      }

      /// <summary>Проверка перечисление на отсутствие значений по-умолчанию</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если в перечислении присутствует значение == default(T)</exception>
      /// <param name="value">Перечисление значений, которые не должны быть равны значению по-умолчанию для своего типа</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ValuesNotEmpty<T>(
        [CanBeNull] IEnumerable<T> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
        where T : struct
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<IEnumerable<T>>(value, valueName);
        foreach (T objA in value)
        {
          if (object.Equals((object) objA, (object) default (T)))
            throw new ValueEmptyException(valueName, message);
        }
        return value;
      }

      /// <summary>Проверка строки на null и на равенство string.Empty</summary>
      /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
      /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string NotNullOrEmpty([CanBeNull] string value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        if (value == null)
          throw Check.CreateNullReferenceException(valueName, message);
        return !object.Equals((object) value, (object) string.Empty) ? value : throw new EmptyStringNotAllowedException(message);
      }

      /// <summary>Проверка строки на null и на равенство string.Empty или состоять только из пробелов</summary>
      /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
      /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
      /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty, или состоять только из
      /// пробелов</param>
      /// <param name="valueName">(Optional) Наименование проверяемого параметра</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [NotWhitespace]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string NotNullOrWhitespace([CanBeNull] string value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        if (value == null)
          throw Check.CreateNullReferenceException(valueName, message);
        if (value == string.Empty)
          throw new EmptyStringNotAllowedException(message);
        return !string.IsNullOrWhiteSpace(value) ? value : throw new WhitespaceNotAllowedException(message);
      }

      /// <summary>Проверка что объект не null и не DBNull</summary>
      /// <exception cref="T:System.NullReferenceException">Если объект == null или DBNull</exception>
      /// <param name="value">Объект, который не должен быть равен null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T NotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class
      {
        if (!Check.Enabled)
          return value;
        if ((object) value == null)
          throw Check.CreateNullReferenceException(valueName, message);
        if (object.Equals((object) DBNull.Value, (object) value))
        {
          message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Value {valueName} cannot be DBNull value." : "Value cannot be DBNull value.") : message;
          throw Check.CreateNullReferenceException(valueName, message);
        }
        return value;
      }

      /// <summary>Проверка что объект не null</summary>
      /// <exception><cref>TException</cref>Если условие не выполняется</exception>
      /// <param name="value">Объект, который должен быть не null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T NotNull<T, TException>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        where T : class
        where TException : NullReferenceException
      {
        return !Check.Enabled || (object) value != null ? value : throw Check.CreateException<TException>(message, (Func<string>) (() => string.IsNullOrWhiteSpace(valueName) ? "Value cannot be null." : $"Value {valueName} cannot be null."));
      }

      /// <summary>Проверка что объект не null</summary>
      /// <exception><cref>TException</cref>Если условие не выполняется</exception>
      /// <param name="value">Объект, который должен быть не null</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T NotNull<T, TException>([CanBeNull, NoEnumeration] T? value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        where T : struct
        where TException : NullReferenceException
      {
        return value.HasValue ? value.Value : throw Check.CreateException<TException>(message, (Func<string>) (() => string.IsNullOrWhiteSpace(valueName) ? "Value cannot be null." : $"Value {valueName} cannot be null."));
      }

      /// <summary>Проверка что элементы последовательности не null</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
      /// <param name="value">Коллекция, элементы которой должен быть не null</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ItemsNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<T>(value, valueName);
        foreach (object obj in (IEnumerable) value)
        {
          if (obj == null)
            throw new ItemNullsNotAllowedException((IEnumerable) value, valueName, message);
        }
        return value;
      }

      /// <summary>Проверка что элементы последовательности не null</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
      /// <param name="value">Коллекция, элементы которой должен быть не null</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ItemsNotNull<T>(
        [CanBeNull] IEnumerable<T?> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
        where T : struct
      {
        Check.NotNull<IEnumerable<T?>>(value, valueName);
        foreach (T? nullable in value)
        {
          if (!nullable.HasValue)
            throw new ItemNullsNotAllowedException((IEnumerable) value, valueName, message);
        }
        return value.Cast<T>();
      }

      /// <summary>Проверка что коллекция не пуста</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
      /// <param name="value">Коллекция, которая не должна быть пустой</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T CollectionNotEmpty<T>([CanBeNull] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, ICollection
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<T>(value, valueName);
        return value.Count != 0 ? value : throw new CollectionIsEmptyException(valueName, message);
      }

      /// <summary>Проверка что коллекция не пуста</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
      /// <param name="value">Коллекция, которая не должна быть пустой</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IReadOnlyCollection<T> ReadOnlyCollectionNotEmpty<T>(
        [CanBeNull, NoEnumeration] IReadOnlyCollection<T> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<IReadOnlyCollection<T>>(value, valueName);
        return value.Count != 0 ? value : throw new CollectionIsEmptyException(valueName, message);
      }

      /// <summary>Проверка что коллекция не пуста</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
      /// <param name="value">Коллекция, которая не должна быть пустой</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TCollection ReadOnlyCollectionNotEmpty<TCollection, T>(
        [CanBeNull, NoEnumeration] TCollection value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
        where TCollection : class, IReadOnlyCollection<T>
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<TCollection>(value, valueName);
        return value.Count != 0 ? value : throw new CollectionIsEmptyException(valueName, message);
      }

      /// <summary>Проверка что последовательность не пусто</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если последовательность пусто</exception>
      /// <param name="value">Последовательность, которая не должна быть пустой</param>
      /// <param name="valueName">Наименование последовательности</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T EnumerationNotEmpty<T>([CanBeNull] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<T>(value, valueName);
        if (value is ICollection collection)
        {
          if (collection.Count == 0)
            throw new CollectionIsEmptyException(valueName, message);
        }
        else
        {
          IEnumerator enumerator = value.GetEnumerator();
          try
          {
            if (!enumerator.MoveNext())
              throw new CollectionIsEmptyException(valueName, message);
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        }
        return value;
      }

      /// <summary>Проверка что все строки последовательности не null и не пусты</summary>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
      /// <param name="value">Последовательность строк, которые быть не должны быть равны null или string.Empty</param>
      /// <param name="valueName">Наименование последовательности строк</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [ItemNotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<string> StringsNotEmpty(
        [CanBeNull] IEnumerable<string> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<IEnumerable<string>>(value, valueName);
        foreach (string str in value)
        {
          if (str == null)
            throw new ItemNullsNotAllowedException((IEnumerable) value, valueName, message);
          if (str == string.Empty)
            throw new ItemEmptyStringNotAllowedException(value, valueName, message);
        }
        return value;
      }

      /// <summary>Проверка что все строки последовательности не null, не пустые строки и не строки состоящие из одних пробелов</summary>
      /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
      /// <exception cref="T:Intermech.Diagnostics.ItemWhitespaceNotAllowedException">Если в последовательности присутствуют строки не содержащие ничего
      /// кроме пробелов</exception>
      /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
      /// <param name="value">Последовательность строк, которые быть не должны быть равны null, string.Empty или заполнены
      /// одними только пробелами</param>
      /// <param name="valueName">(Optional) Наименование коллекции</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [ItemNotWhitespace]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<string> StringsNotWhitespace(
        [CanBeNull] IEnumerable<string> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<IEnumerable<string>>(value, valueName);
        foreach (string str in value)
        {
          if (str == null)
            throw new ItemNullsNotAllowedException((IEnumerable) value, valueName, message);
          if (str == string.Empty)
            throw new ItemEmptyStringNotAllowedException(value, valueName, message);
          if (string.IsNullOrWhiteSpace(str))
            throw new ItemWhitespaceNotAllowedException(value, valueName, message);
        }
        return value;
      }

      /// <summary>Проверка что элементы коллекции не null и не DBNull</summary>
      /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если коллекция содержит null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если коллекция содержит DBNull</exception>
      /// <param name="value">Коллекция, элементы которой должен быть не null</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [ItemNotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ItemsNotNullNotDbNull<T>([CanBeNull] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<T>(value, valueName);
        foreach (object objB in (IEnumerable) value)
        {
          if (objB == null)
            throw new ItemNullsNotAllowedException((IEnumerable) value, valueName, message);
          if (object.Equals((object) DBNull.Value, objB))
          {
            message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Collection {valueName} cannot contains DBNull values." : "Collection cannot contains DBNull values.") : message;
            throw new ItemNullsNotAllowedException((IEnumerable) value, valueName, message);
          }
        }
        return value;
      }

      /// <summary>Проверка условия</summary>
      /// <exception cref="T:System.Exception">Если условие не выполняется</exception>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Assert(bool condition, [CanBeNull, NotEmpty, InvokerParameterName] string message = null)
      {
        if (!condition)
          throw new Exception(message);
      }

      /// <summary>Проверка условия</summary>
      /// <exception cref="T:System.Exception">Если условие не выполняется</exception>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="messageConstructor">Метод-конструктор сообщения об ошибке</param>
      [ContractAnnotation("condition:false => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Assert(bool condition, [NotNull, NotEmpty] Func<string> messageConstructor)
      {
        if (!condition)
        {
          string message = messageConstructor();
          string.IsNullOrWhiteSpace(message);
          throw new Exception(message);
        }
      }

      /// <summary>Проверка условия</summary>
      /// <exception cref="T:System.Exception">Если условие не выполняется</exception>
      /// <param name="value">Возвращаемое значение если проверка пройдена</param>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt; value:null => null")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Assert<T>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull, NotEmpty, InvokerParameterName] string message = null)
      {
        if (!Check.Enabled || condition(value))
          return value;
        throw new Exception(message);
      }

      /// <summary>Проверка условия</summary>
      /// <exception cref="T:System.Exception">Если условие не выполняется</exception>
      /// <param name="value">Возвращаемое значение если проверка пройдена</param>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="messageConstructor">Метод-конструктор сообщения об ошибке</param>
      [ContractAnnotation("condition:false => halt; value:null => null")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Assert<T>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [NotNull, NotEmpty] Func<string> messageConstructor)
      {
        if (!Check.Enabled || condition(value))
          return value;
        string message = messageConstructor();
        string.IsNullOrWhiteSpace(message);
        throw new Exception(message);
      }

      /// <summary>Проверка условия</summary>
      /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="exceptionParams">Параметры, которые будут переданы в конструктор исключительной ситуации</param>
      [ContractAnnotation("condition:false => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Assert<TException>(bool condition, [NotNull, ItemNotNull] object[] exceptionParams) where TException : Exception
      {
        if (!condition)
          throw Check.CreateExceptionWithParams<TException>(exceptionParams);
      }

      /// <summary>Проверка условия</summary>
      /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="messageConstructor">Метод-конструктор сообщения об ошибке</param>
      [ContractAnnotation("condition:false => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Assert<TException>(bool condition, [NotNull, NotEmpty] Func<string> messageConstructor) where TException : Exception
      {
        if (!condition)
          throw Check.CreateException<TException>(messageConstructor());
      }

      /// <summary>Проверка условия</summary>
      /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
      /// <param name="value">Возвращаемое значение если проверка пройдена</param>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="exceptionParams">Параметры, которые будут переданы в конструктор исключительной ситуации</param>
      [ContractAnnotation("condition:false => halt; value:null => null")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Assert<T, TException>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [NotNull, ItemNotNull] object[] exceptionParams) where TException : Exception
      {
        if (!Check.Enabled || condition(value))
          return value;
        throw Check.CreateExceptionWithParams<TException>(exceptionParams);
      }

      /// <summary>Проверка условия</summary>
      /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Assert<TException>(bool condition, [CanBeNull] string message = null) where TException : Exception
      {
        if (!condition)
          throw Check.CreateException<TException>(message);
      }

      /// <summary>Проверка условия</summary>
      /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
      /// <param name="value">Возвращаемое значение если проверка пройдена</param>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt; value:null => null")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Assert<T, TException>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull] string message = null) where TException : Exception
      {
        if (!Check.Enabled || condition(value))
          return value;
        throw Check.CreateException<TException>(message);
      }

      /// <summary>Безопасная конвертация, поддерживающая например конвертацию IEnumerable из decimal в IEnumerable из long
      /// (обычный Cast выбрасывает exception). Критично для работы с СУБД, например Oracle числа возвращает в виде
      /// decimal</summary>
      [NotNull]
      [DebuggerStepThrough]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static IEnumerable<T> ConvertAll<T>(
        [NotNull] IEnumerable enumeration,
        [CanBeNull] IFormatProvider formatProvider = null)
      {
        return enumeration is IEnumerable<T> objs ? objs : Check._ConvertAll<T>(enumeration, formatProvider);
      }

      [NotNull]
      [ItemCanBeNull]
      [DebuggerStepThrough]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static IEnumerable<T> _ConvertAll<T>(
        [NotNull] IEnumerable enumeration,
        [CanBeNull] IFormatProvider formatProvider = null)
      {
        Type type = typeof (T);
        foreach (object obj in enumeration)
        {
          switch (obj)
          {
            case null:
            case DBNull _:
              yield return default (T);
              continue;
            default:
              yield return (T) Convert.ChangeType(obj, type, formatProvider);
              continue;
          }
        }
      }

      /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
      /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ItemsIs<T>([CanBeNull] IEnumerable value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        Check.NotNull<IEnumerable>(value, valueName);
        foreach (object obj in value)
        {
          if (obj != null && !(obj is T))
          {
            message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Collection {valueName} has item \"{obj}\", with is not {typeof (T)} type." : $"Collection has item \"{obj}\", with is not {typeof (T)} type.") : message;
            throw new InvalidCastException(message);
          }
        }
        return Check.ConvertAll<T>(value);
      }

      /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
      /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="messageFactory">Метод-фабрика сообщений об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ItemsIs<T>(
        [CanBeNull] IEnumerable value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
        [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
      {
        Check.NotNull<IEnumerable>(value, valueName);
        foreach (object obj in value)
        {
          switch (obj)
          {
            case null:
            case T _:
              continue;
            default:
              string str = messageFactory(obj);
              throw new InvalidCastException(string.IsNullOrWhiteSpace(str) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Collection {valueName} has item \"{obj}\", with is not {typeof (T)} type." : $"Collection has item \"{obj}\", with is not {typeof (T)} type.") : str);
          }
        }
        return Check.ConvertAll<T>(value);
      }

      /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
      /// <exception cref="T:System.Exception">Если обнаружен элемент не являющийся объектом нужного типа</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="messageFactory">Метод-фабрика сообщений об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [ItemNotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> ItemsIs<T>(
        [CanBeNull] IEnumerable value,
        [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
      {
        return Check.ItemsIs<T>(value, (string) null, messageFactory);
      }

      /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
      /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
      /// переданное условие</exception>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="predicate">Условие</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> All<T>(
        [CanBeNull] IEnumerable<T> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
        [NotNull, InstantHandle] Func<T, bool> predicate,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<IEnumerable<T>>(value, valueName);
        foreach (T obj in value)
        {
          if (!predicate(obj))
            throw new ItemValidationExceptionException(message);
        }
        return value;
      }

      /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
      /// переданное условие</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="predicate">Условие</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> All<T>(
        [CanBeNull] IEnumerable<T> value,
        [NotNull, InstantHandle] Func<T, bool> predicate,
        [CanBeNull] string message = null)
      {
        return Check.All<T>(value, (string) null, predicate, message);
      }

      /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
      /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
      /// переданное условие</exception>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="predicate">Условие</param>
      /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> All<T>(
        [CanBeNull] IEnumerable<T> value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
        [NotNull, InstantHandle] Func<T, bool> predicate,
        [NotNull] TemplateMessageFactory<T> messageFactory)
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<IEnumerable<T>>(value, valueName);
        foreach (T obj in value)
        {
          if (!predicate(obj))
            throw new ItemValidationExceptionException(messageFactory(obj));
        }
        return value;
      }

      /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
      /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
      /// переданное условие</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="predicate">Условие</param>
      /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> All<T>(
        [CanBeNull] IEnumerable<T> value,
        [NotNull, InstantHandle] Func<T, bool> predicate,
        [NotNull] TemplateMessageFactory<T> messageFactory)
      {
        return Check.All<T>(value, (string) null, predicate, messageFactory);
      }

      /// <summary>Проверка, что перечисление не пусто</summary>
      /// <exception cref="T:System.NullReferenceException">Если перечисление равно null</exception>
      /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если перечисление пусто</exception>
      /// <param name="value">Коллекция</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TEnumerable NotNullNotEmpty<TEnumerable>(
        [CanBeNull] TEnumerable value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull, CanBeEmpty] string message = null)
        where TEnumerable : class, IEnumerable
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<TEnumerable>(value, valueName);
        if (value is ICollection collection)
        {
          if (collection.Count == 0)
            throw new CollectionIsEmptyException(valueName, message);
        }
        else
        {
          IEnumerator enumerator = value.GetEnumerator();
          try
          {
            if (!enumerator.MoveNext())
              throw new CollectionIsEmptyException(valueName, message);
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        }
        return value;
      }

      /// <summary>Проверка состояния объекта (значений полей/свойств)</summary>
      /// <exception cref="T:System.InvalidOperationException">Если condition == false</exception>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void ObjectState(bool condition, [CanBeNull, InvokerParameterName] string message = null)
      {
        if (!condition)
          throw new InvalidOperationException(message);
      }

      /// <summary>Проверка состояния объекта (значений полей/свойств)</summary>
      /// <exception cref="T:System.NullReferenceException">Если value == null</exception>
      /// <exception cref="T:System.InvalidOperationException">Если condition == false</exception>
      /// <param name="value">Значение, которое будет возвращено, если проверка будет пройдена</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt; value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ObjectState<T>(
        [CanBeNull, NoEnumeration] T value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
        [NotNull, InstantHandle] Func<T, bool> condition,
        [CanBeNull, InvokerParameterName] string message = null)
      {
        if (!Check.Enabled)
          return value;
        if (object.Equals((object) value, (object) null))
          throw Check.CreateNullReferenceException(valueName, message);
        return condition(value) ? value : throw new InvalidOperationException(message);
      }

      /// <summary>Проверка состояния объекта (значений полей/свойств)</summary>
      /// <exception cref="T:System.NullReferenceException">Если value == null</exception>
      /// <exception cref="T:System.InvalidOperationException">Если condition == false</exception>
      /// <param name="value">Значение, которое будет возвращено, если проверка будет пройдена</param>
      /// <param name="condition">Условие, которое должно быть true</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("condition:false => halt; value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T ObjectState<T>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull, InvokerParameterName] string message = null)
      {
        return Check.ObjectState<T>(value, (string) null, condition, message);
      }

      /// <summary>Устаревшая версия проверки того, что значение является допустимым для данного типа перечня (enum)</summary>
      /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
      /// <param name="type">Тип значения</param>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [Obsolete("Удалите передачу типа Enum, начиная c C# 7.2 он не требуется")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T EnumInRange<T>([NotNull] Type type, T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct, Enum
      {
        return Check.EnumInRange<T>(value, valueName, message);
      }

      /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
      /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T EnumInRange<T>(T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct, Enum
      {
        if (!Check.Enabled || Enum.IsDefined(typeof (T), (object) value))
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"The value {valueName} = {Convert.ToInt64((object) value)} is invalid for Enum type '{typeof (T)}'." : $"The value ({Convert.ToInt64((object) value)}) is invalid for Enum type '{typeof (T)}'.") : message;
        throw new InvalidEnumArgumentException(message);
      }

      /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
      /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      protected internal static void EnumInRange<T>([NotNull] object value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct, Enum
      {
        if (Check.Enabled && !Enum.IsDefined(typeof (T), value))
        {
          message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"The value {valueName} = {Convert.ToInt64(value)} is invalid for Enum type '{typeof (T)}'." : $"The value ({Convert.ToInt64(value)}) is invalid for Enum type '{typeof (T)}'.") : message;
          throw new InvalidEnumArgumentException(message);
        }
      }

      /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
      /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
      /// <param name="value">Значение</param>
      /// <param name="getExceptionFunc">Метод-конструктор исключительной ситуации</param>
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T EnumInRangeCustom<T>(
        T value,
        [NotNull] EnumInRangeCustomExceptionFactory<T> getExceptionFunc)
        where T : struct, Enum
      {
        return !Check.Enabled || Enum.IsDefined(typeof (T), (object) value) ? value : throw getExceptionFunc(value);
      }

      /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
      /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
      /// <param name="values">Список значений</param>
      /// <param name="valueName">Наименование коллекции</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("values:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> AllEnumInRange<T>(
        [CanBeNull] IEnumerable<T> values,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        [CanBeNull] string message = null)
        where T : struct, Enum
      {
        if (!Check.Enabled)
          return values;
        foreach (T obj in values)
        {
          if (!Enum.IsDefined(typeof (T), (object) obj))
          {
            message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? $"Collection {valueName} contains value {Convert.ToInt64((object) obj)} with is invalid for Enum type '{typeof (T)}'." : $"Collection contains value {Convert.ToInt64((object) obj)} with is invalid for Enum type '{typeof (T)}'.") : message;
            throw new InvalidEnumArgumentException(message);
          }
        }
        return values;
      }

      /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
      /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
      /// <exception cref="T:System.InvalidCastException">Если тип переданного объекта не <see cref="!:T" /></exception>
      /// <param name="value">Проверяемый объект</param>
      /// <param name="valueName">Наименование переданного объекта</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Is<T>([CanBeNull] object value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        Check.NotNull<object>(value, valueName, message);
        return value is T obj ? obj : throw Check.CreateInvalidCastException(typeof (T), valueName, message);
      }

      /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
      /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
      /// <exception cref="T:System.InvalidCastException">Если тип переданного объекта не <see cref="!:T" /></exception>
      /// <param name="value">Проверяемый объект</param>
      /// <param name="valueName">Наименование переданного объекта</param>
      /// <param name="messageFactory">Внешняя ф-ия получения сообщения об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Is<T>([CanBeNull] object value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName, [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
      {
        Check.NotNull<object>(value, valueName);
        return value is T obj ? obj : throw new InvalidCastException(messageFactory(value));
      }

      /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
      /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
      /// <exception cref="T:System.Exception">Если тип переданного объекта не <see cref="!:T" /></exception>
      /// <param name="value">Проверяемый объект</param>
      /// <param name="messageFactory">Внешняя ф-ия получения сообщения об ошибке</param>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Is<T>([CanBeNull] object value, [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
      {
        return Check.Is<T>(value, (string) null, messageFactory);
      }

      /// <summary>Проверка того, что файл по указанному пути существует на диске</summary>
      /// <exception cref="T:System.ArgumentNullException">Если указанный путь == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если указанный путь == string.Empty</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если указанный путь состоит только из пробелов</exception>
      /// <exception cref="T:System.IO.FileNotFoundException">Если файл отсутствует на диске</exception>
      /// <param name="value">Путь к файлу</param>
      /// <param name="valueName">Наименование переданного значения</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      /// <returns>Путь к файлу</returns>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [NotWhitespace]
      [FileExists]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string FileExists([CanBeNull] string value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNullOrWhitespace(value, valueName);
        if (!File.Exists(value))
        {
          string str;
          if (!string.IsNullOrWhiteSpace(message))
            str = message;
          else if (!string.IsNullOrWhiteSpace(valueName))
            str = $"File \"{value}\" not found";
          else
            str = $"File {valueName}=\"{value}\" not found";
          message = str;
          throw new FileNotFoundException(message, value);
        }
        return value;
      }

      /// <summary>Проверка того, что папка по указанному пути существует на диске</summary>
      /// <exception cref="T:System.ArgumentNullException">Если указанный путь == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если указанный путь == string.Empty</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если указанный путь состоит только из пробелов</exception>
      /// <exception cref="T:System.IO.DirectoryNotFoundException">Если папка отсутствует на диске</exception>
      /// <param name="value">Путь к папке</param>
      /// <param name="valueName">Наименование переданного значения</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      /// <returns>Путь к папке</returns>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [NotWhitespace]
      [DirectoryExists]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string DirectoryExists([CanBeNull] string value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.NotNullOrWhitespace(value, valueName);
        if (!Directory.Exists(value))
        {
          string str;
          if (!string.IsNullOrWhiteSpace(message))
            str = message;
          else if (!string.IsNullOrWhiteSpace(valueName))
            str = $"Folder \"{value}\" not found";
          else
            str = $"Folder {valueName}=\"{value}\" not found";
          message = str;
          throw new DirectoryNotFoundException(message);
        }
        return value;
      }

      /// <summary>Проверка что стрим не равен null, что имеет ненулевую длину и текущая позиция не находится в конце стрима</summary>
      /// <exception cref="T:System.InvalidOperationException">Если длина стрима равна 0</exception>
      /// <exception cref="T:System.ArgumentNullException">Если переданный стрим == null</exception>
      /// <exception cref="T:System.IO.EndOfStreamException">Если позиция в преданном стриме находится в его конце</exception>
      /// <param name="value">Стрим</param>
      /// <param name="valueName">Наименование стрима</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      /// <returns>Стрим</returns>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [NotEmpty]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static Stream StreamNotEmpty([CanBeNull] Stream value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.NotNull<Stream>(value, valueName);
        if (!value.CanRead)
          throw new InvalidOperationException(string.IsNullOrWhiteSpace(message) ? $"Stream {valueName} of type {value.GetType()} can not be read." : message);
        if (value.Length == 0L)
          throw new InvalidOperationException(string.IsNullOrWhiteSpace(message) ? $"Stream {valueName} has zero length." : message);
        if (value.Position >= value.Length - 1L)
          throw !string.IsNullOrWhiteSpace(message) ? new EndOfStreamException(message) : new EndOfStreamException();
        return value;
      }

      /// <summary>Проверка что строка содержит корректный Uri</summary>
      /// <exception cref="T:Intermech.Diagnostics.InvalidUriException">Если Uri некорректен</exception>
      /// <exception cref="T:System.ArgumentNullException">Если строка описывающая Uri == null</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка описывающая Uri == string.Empty</exception>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка описывающая Uri состоит только из пробелов</exception>
      /// <param name="value">Строка, содержащая Uri</param>
      /// <param name="valueName">(Optional) Наименование строки</param>
      /// <param name="scheme">(Optional) Схема Uri которой должен соответствовать адрес. Например UriScheme.Http для Http
      /// адреса. Если null - схема не проверяется</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      /// <returns>Строка, содержащая Uri</returns>
      [ContractAnnotation("value:null => halt")]
      [NotNull]
      [NotWhitespace]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string UriCorrect(
        [CanBeNull] string value,
        [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
        UriScheme scheme = UriScheme.Any,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return value;
        Check.ArgumentNotNullOrWhitespace(value, valueName);
        Uri result;
        if (Uri.TryCreate(value, UriKind.Absolute, out result) && (scheme == UriScheme.Any || scheme == UriScheme.None || UriSchemes.Name2Value[result.Scheme] == scheme))
          return value;
        throw new InvalidUriException(value, valueName, message);
      }

      /// <summary>Проверка что в словаре присутствует запись с переданным ключом</summary>
      /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNotFoundException">Если ключ не найден</exception>
      /// <param name="dictionary">Словарь</param>
      /// <param name="key">Ключ, который должен присутствовать в словаре</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ContractAnnotation("dictionary:null => halt")]
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IReadOnlyDictionary<TKey, TValue> ContainsKey<TKey, TValue>(
        [CanBeNull, NoEnumeration] IReadOnlyDictionary<TKey, TValue> dictionary,
        [NotNull, NoEnumeration] TKey key,
        [CanBeNull] string message = null)
      {
        if (!Check.Enabled)
          return dictionary;
        Check.NotNull<IReadOnlyDictionary<TKey, TValue>>(dictionary, nameof (dictionary));
        return dictionary.ContainsKey(key) ? dictionary : throw new ItemNotFoundException<TKey>(key, message);
      }

      /// <summary>Проверка того, что значение больше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [PositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int IsPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value > 0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a positive number." : "Value must be a positive number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение больше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [PositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static long IsPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value > 0L)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a positive number." : "Value must be a positive number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение больше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [PositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static float IsPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || (double) value > 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a positive number." : "Value must be a positive number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение больше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [PositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static double IsPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value > 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a positive number." : "Value must be a positive number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение равно или больше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int IsZeroOrPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value >= 0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a zero or positive number." : "Value must be a zero or positive number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение равно или больше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static long IsZeroOrPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value >= 0L)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a zero or positive number." : "Value must be a zero or positive number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение равно или больше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static float IsZeroOrPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || (double) value >= 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a zero or positive number." : "Value must be a zero or positive number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение равно или больше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [ZeroOrPositiveNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static double IsZeroOrPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value >= 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a zero or positive number." : "Value must be a zero or positive number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение меньше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NegativeNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int IsNegative(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value < 0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a negative number." : "Value must be a negative number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение меньше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NegativeNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static long IsNegative(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value < 0L)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a negative number." : "Value must be a negative number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение меньше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NegativeNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static float IsNegative(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || (double) value < 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a negative number." : "Value must be a negative number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка того, что значение меньше нуля</summary>
      /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
      /// <param name="value">Значение</param>
      /// <param name="valueName">Наименование проверяемого параметра</param>
      /// <param name="message">Сообщение об ошибке</param>
      [NegativeNumber]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static double IsNegative(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
      {
        if (!Check.Enabled || value < 0.0)
          return value;
        message = string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? valueName + " must be a negative number." : "Value must be a negative number.") : message;
        throw new ValueOutOfRangeException(valueName, message);
      }

      /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
      /// Для контроля используется ссылка на объект, которая в Dispose устанавливается в null</summary>
      /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и notNullRef == null</exception>
      /// <param name="notNullRef">Контрольная ссылка, которая становится равной null после вызова Dispose</param>
      /// <param name="objectName">(Optional) Имя объекта</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("notNullRef:null => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void NotDisposed([CanBeNull] object notNullRef, [CanBeNull, NotWhitespace] string objectName = null, [CanBeNull] string message = null)
      {
        if (Check.Enabled && notNullRef == null)
          throw !string.IsNullOrWhiteSpace(message) ? new ObjectDisposedException(objectName, message) : new ObjectDisposedException(objectName);
      }

      /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
      /// Для контроля используется ссылка на объект, которая в Dispose устанавливается в null</summary>
      /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и notNullRef == null</exception>
      /// <param name="notNullRef">Контрольная ссылка, которая становится равной null после вызова Dispose</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("notNullRef:null => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void NotDisposed<T>([CanBeNull] object notNullRef, [CanBeNull] string message = null) where T : IDisposable
      {
        if (Check.Enabled && notNullRef == null)
          throw !string.IsNullOrWhiteSpace(message) ? new ObjectDisposedException(typeof (T).Name, message) : new ObjectDisposedException(typeof (T).Name);
      }

      /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
      /// Для контроля флаг, который устанавливается в True в самом начале Dispose</summary>
      /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и disposedFlag == true</exception>
      /// <param name="disposedFlag">Флаг, который устанавливается в True в самом начале Dispose</param>
      /// <param name="objectName">(Optional) Имя объекта</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("disposedFlag:true => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void NotDisposed(bool disposedFlag, [CanBeNull, NotWhitespace] string objectName = null, [CanBeNull] string message = null)
      {
        if (Check.Enabled & disposedFlag)
          throw !string.IsNullOrWhiteSpace(message) ? new ObjectDisposedException(objectName, message) : new ObjectDisposedException(objectName);
      }

      /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
      /// Для контроля флаг, который устанавливается в True в самом начале Dispose</summary>
      /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и disposedFlag == true</exception>
      /// <param name="disposedFlag">Флаг, который устанавливается в True в самом начале Dispose</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [ContractAnnotation("disposedFlag:true => halt")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void NotDisposed<T>(bool disposedFlag, [CanBeNull] string message = null) where T : IDisposable
      {
        if (Check.Enabled & disposedFlag)
          throw !string.IsNullOrWhiteSpace(message) ? new ObjectDisposedException(typeof (T).Name, message) : new ObjectDisposedException(typeof (T).Name);
      }

      /// <summary>Проверка что объект заблокирован конструкцией lock. Служит для проверки того, что контекст вызова
      /// потокобезопасен</summary>
      /// <exception cref="T:System.InvalidOperationException">Если объект не заблокирован конструкцией lock</exception>
      /// <param name="syncObject">Объект, который должен быть заблокирован конструкцией lock</param>
      /// <param name="syncObjectName">(Optional) Имя объекта, который используется для блокирования доступа к контексту вызова</param>
      /// <param name="message">(Optional) Сообщение об ошибке</param>
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T SyncLocked<T>([NotNull] T syncObject, [CanBeNull, NotWhitespace] string syncObjectName = null, [CanBeNull] string message = null) where T : class
      {
        Check.NotNull<T>(syncObject, syncObjectName);
        return !Check.Enabled || Monitor.IsEntered((object) syncObject) ? syncObject : throw new InvalidOperationException(!string.IsNullOrWhiteSpace(message) ? message : (!string.IsNullOrWhiteSpace(syncObjectName) ? syncObjectName + " must locked!" : "Sync object must be locked!"));
      }

      /// <summary>Валидация условий для значения передаваемого в сеттер свойств. В DEBUG билде проверки осуществляются, иначе - нет</summary>
      public abstract class SetValue
      {
        /// <summary>Проверка что строка содержит guid</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.ArgumentException">Если строка не содержит GUID</exception>
        /// <param name="guid">Строка, которая должна содержать Guid</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [GuidStr]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsGuid([CanBeNull] string guid, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIsGuid(guid, callerMemberName, message);
        }

        /// <summary>Проверка что строка содержит непустой guid</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.ArgumentException">Если строка не содержит GUID</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если GUID пуст</exception>
        /// <param name="guid">Строка, которая должна содержать непустой Guid</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GuidNotEmpty([CanBeNull] string guid, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentGuidNotEmpty(guid, callerMemberName, message);
        }

        /// <summary>Проверка что guid не пуст</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если guid == Guid.Empty</exception>
        /// <param name="guid">Guid, который не должен быть равен Guid.Empty</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GuidNotEmpty(Guid guid, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentGuidNotEmpty(guid, callerMemberName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class
        {
          Check.ArgumentNotNull<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T>([CanBeNull, NoEnumeration] T? value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : struct
        {
          Check.ArgumentNotNull<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка что значение, присваиваемое свойству, не null</summary>
        /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentGenericNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentGenericNotNull(value, callerMemberName, message);
        }

        /// <summary>Проверка значения на значение по-умолчанию</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValueNotEmpty<T>(T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : struct
        {
          Check.ArgumentValueNotEmpty<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка что значение не пусто (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue">Пустое значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValueNotEmpty<T>(
          T value,
          T emptyValue,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct
        {
          Check.ArgumentValueNotEmpty<T>(value, emptyValue, callerMemberName, message);
        }

        /// <summary>Проверка что значение не пусто (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue1">Пустое значение</param>
        /// <param name="emptyValue2">Пустое значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValueNotEmpty<T>(
          T value,
          T emptyValue1,
          T emptyValue2,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct
        {
          Check.ArgumentValueNotEmpty<T>(value, emptyValue1, emptyValue2, callerMemberName, message);
        }

        /// <summary>Проверка что значение IntPtr не пусто (пустое значение отличается от IntPtr.Zero)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == IntPtr.Zero</exception>
        /// <param name="value">Значение, которое не должно быть равно IntPtr.Zero</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValueNotEmpty(IntPtr value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentValueNotEmpty(value, callerMemberName, message);
        }

        /// <summary>Проверка попадания значения в допустимый диапазон</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
        /// <param name="value">Значение, которое должно попадать в допустимый диапазон</param>
        /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InRange<T>(
          [CanBeNull] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentInRange<T>(value, condition, callerMemberName, message);
        }

        /// <summary>Проверка попадания значения в допустимый диапазон</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если значение выходит за рамки допустимого диапазона значений</exception>
        /// <param name="value">Значение, которое должно попадать в допустимый диапазон</param>
        /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InRange<T>(
          [CanBeNull] T value,
          [NotNull, InstantHandle] Func<bool> condition,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentInRange<T>(value, condition, callerMemberName, message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [ZeroOrPositiveNumber]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IndexInRange(int index, int count, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIndexInRange(index, count, callerMemberName, message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [ZeroOrPositiveNumber]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IndexInRange(
          long index,
          long count,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIndexInRange(index, count, callerMemberName, message);
        }

        /// <summary>Проверка перечисление на отсутствие значений по-умолчанию</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если в перечислении присутствует значение == default(T)</exception>
        /// <param name="value">Перечисление значений, которые не должны быть равны значению по-умолчанию для своего типа</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValuesNotEmpty<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct
        {
          Check.ArgumentValuesNotEmpty<T>(value, callerMemberName, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> All<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return Check.ArgumentAll<T>(value, callerMemberName, predicate, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> All<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [NotNull] TemplateMessageFactory<T> messageFactory,
          [CanBeNull] string callerMemberName = null)
        {
          return Check.ArgumentAll<T>(value, callerMemberName, predicate, messageFactory);
        }

        /// <summary>Проверка строки на null и на равенство string.Empty</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty([CanBeNull] string value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentNotNullOrEmpty(value, callerMemberName, message);
        }

        /// <summary>Проверка строки на null и на равенство string.Empty или состоять только из пробелов</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty, или состоять только из
        /// пробелов</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrWhitespace([CanBeNull] string value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentNotNullOrWhitespace(value, callerMemberName, message);
        }

        /// <summary>Проверка что объект не null и не DBNull</summary>
        /// <exception cref="T:System.ArgumentNullException">Если объект == null или DBNull</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class
        {
          Check.ArgumentNotNullNotDbNull<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception><cref>TException</cref>Если условие не выполняется</exception>
        /// <param name="value">Объект, который должен быть не null</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T, TException>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
          where T : class
          where TException : ArgumentNullException
        {
          Check.ArgumentNotNull<T, TException>(value, callerMemberName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception><cref>TException</cref>Если условие не выполняется</exception>
        /// <param name="value">Объект, который должен быть не null</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T, TException>([CanBeNull, NoEnumeration] T? value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
          where T : struct
          where TException : ArgumentNullException
        {
          Check.ArgumentNotNull<T, TException>(value, callerMemberName, message);
        }

        /// <summary>Проверка что элементы последовательности не null</summary>
        /// <exception cref="T:System.ArgumentNullException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ItemsNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class, IEnumerable
        {
          Check.ArgumentItemsNotNull<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка что элементы последовательности не null</summary>
        /// <exception cref="T:System.ArgumentNullException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ItemsNotNull<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T?> value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct
        {
          Check.ArgumentItemsNotNull<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.ArgumentNullException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentCollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CollectionNotEmpty<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class, ICollection
        {
          Check.ArgumentCollectionNotEmpty<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.ArgumentNullException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentCollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReadOnlyCollectionNotEmpty<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IReadOnlyCollection<T> value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentReadOnlyCollectionNotEmpty<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка что последовательность не пусто</summary>
        /// <exception cref="T:System.ArgumentNullException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentCollectionIsEmptyException">Если последовательность пусто</exception>
        /// <param name="value">Последовательность, которая не должна быть пуста</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnumerationNotEmpty<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class, IEnumerable
        {
          Check.ArgumentEnumerationNotEmpty<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка что все строки последовательности не null и не пусты</summary>
        /// <exception cref="T:System.ArgumentNullException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <param name="value">Последовательность строк, которые быть не должны быть равны null или string.Empty</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StringsNotEmpty(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<string> value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentStringsNotEmpty(value, callerMemberName, message);
        }

        /// <summary>Проверка что все строки последовательности не null, не пустые строки и не строки состоящие из одних пробелов</summary>
        /// <exception cref="T:System.ArgumentNullException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemWhitespaceNotAllowedException">Если в последовательности присутствуют строки не
        /// содержащие ничего кроме пробелов</exception>
        /// <param name="value">Последовательность строк, которые быть не должны быть равны null, string.Empty или заполнены
        /// одними только пробелами</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StringsNotWhitespace(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<string> value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentStringsNotWhitespace(value, callerMemberName, message);
        }

        /// <summary>Проверка что элементы коллекции не null и не DBNull</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если коллекция содержит null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если коллекция содержит DBNull</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ItemsNotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class, IEnumerable
        {
          Check.ArgumentItemsNotNullNotDbNull<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception cref="T:System.ArgumentException">Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение, если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert<T>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          Check.Argument<T>(value, condition, string.IsNullOrEmpty(message) ? $"Attempt to assign {value} to {callerMemberName}" : message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение, если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="exceptionParams">Параметры, которые будут переданы в конструктор исключительной ситуации</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert<T, TException>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [NotNull, ItemCanBeNull] object[] exceptionParams)
          where TException : ArgumentException
        {
          Check.Argument<T, TException>(value, condition, exceptionParams);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение, если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert<T, TException>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where TException : ArgumentException
        {
          Check.Argument<T, TException>(value, condition, string.IsNullOrEmpty(message) ? $"Attempt to assign {value} to {callerMemberName}" : message);
        }

        /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
        /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ItemsIs<T>([CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ItemsIs<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
        /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="messageFactory">Метод-фабрика сообщений об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ItemsIs<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable value,
          [NotNull, InstantHandle] ObjectMessageFactory messageFactory,
          [CanBeNull] string callerMemberName = null)
        {
          Check.ItemsIs<T>(value, callerMemberName, messageFactory);
        }

        /// <summary>Проверка, что перечисление не пусто</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentCollectionIsEmptyException">Если перечисление пусто</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullNotEmpty<TEnumerable>(
          [CanBeNull, NoEnumeration] TEnumerable value,
          [CanBeNull, CanBeEmpty] string message = null,
          [CanBeNull] string callerMemberName = null)
          where TEnumerable : class, IEnumerable
        {
          Check.ArgumentNotNullNotEmpty<TEnumerable>(value, callerMemberName, message);
        }

        /// <summary>Проверка состояния объекта (значений полей/свойств)</summary>
        /// <exception cref="T:System.ArgumentNullException">Если value == null</exception>
        /// <exception cref="T:System.InvalidOperationException">Если condition == false</exception>
        /// <param name="value">Значение, которое будет возвращено, если проверка будет пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt; value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ObjectState<T>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull, InvokerParameterName] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          Check.ObjectState(value, callerMemberName, condition, string.IsNullOrWhiteSpace(message) ? $"Attempt to assign {value} to {callerMemberName}" : message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="type">Тип значения</param>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Obsolete("Удалите передачу типа Enum, начиная c C# 7.2 он не требуется")]
        [Pure]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnumInRange<T>([NotNull] Type type, T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : struct, Enum
        {
          Check.EnumInRange(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnumInRange<T>(T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : struct, Enum
        {
          Check.EnumInRange(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="value">Значение</param>
        /// <param name="getExceptionFunc">Метод-конструктор исключительной ситуации</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnumInRangeCustom<T>(
          T value,
          [NotNull] EnumInRangeCustomExceptionFactory<T> getExceptionFunc)
          where T : struct, Enum
        {
          Check.EnumInRangeCustom(value, getExceptionFunc);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="type">Тип значения</param>
        /// <param name="values">Список значений</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Obsolete("Удалите передачу типа Enum, начиная c C# 7.2 он не требуется")]
        [Pure]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("values:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AllEnumInRange<T>(
          [NotNull] Type type,
          [CanBeNull, NoEnumeration] IEnumerable<T> values,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct, Enum
        {
          Check.AllEnumInRange(values, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="values">Список значений</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("values:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AllEnumInRange<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> values,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct, Enum
        {
          Check.AllEnumInRange(values, callerMemberName, message);
        }

        /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
        /// <exception cref="T:System.ArgumentNullException">Если объект null</exception>
        /// <exception cref="T:System.InvalidCastException">Если тип переданного объекта не <see cref="!:T" /></exception>
        /// <param name="value">Проверяемый объект</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Is<T>([CanBeNull, NoEnumeration] object value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.Is<T>(value, callerMemberName, message);
        }

        /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
        /// <exception cref="T:System.ArgumentNullException">Если объект null</exception>
        /// <exception cref="T:System.InvalidCastException">Если тип переданного объекта не <see cref="!:T" /></exception>
        /// <param name="value">Проверяемый объект</param>
        /// <param name="messageFactory">Внешняя ф-ия получения сообщения об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Is<T>(
          [CanBeNull, NoEnumeration] object value,
          [NotNull, InstantHandle] ObjectMessageFactory messageFactory,
          [CanBeNull] string callerMemberName = null)
        {
          Check.Is<T>(value, callerMemberName, messageFactory);
        }

        /// <summary>Проверка того, что файл по указанному пути существует на диске</summary>
        /// <exception cref="T:System.ArgumentNullException">Если указанный путь == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если указанный путь == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если указанный путь состоит только из пробелов</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">Если файл отсутствует на диске</exception>
        /// <param name="value">Путь к файлу</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FileExists([CanBeNull] string value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.FileExists(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что папка по указанному пути существует на диске</summary>
        /// <exception cref="T:System.ArgumentNullException">Если указанный путь == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если указанный путь == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если указанный путь состоит только из пробелов</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">Если папка отсутствует на диске</exception>
        /// <param name="value">Путь к папке</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DirectoryExists([CanBeNull] string value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.DirectoryExists(value, callerMemberName, message);
        }

        /// <summary>Проверка что стрим не равен null, что имеет ненулевую длину и текущая позиция не находится в конце стрима</summary>
        /// <exception cref="T:System.ArgumentNullException">Если переданный стрим == null</exception>
        /// <exception cref="M:Intermech.Diagnostics.Check.SetValue.StreamNotEmpty(System.IO.Stream,System.String,System.String)">Если длина стрима равна 0</exception>
        /// <exception cref="T:System.IO.EndOfStreamException">Если позиция в преданном стриме находится в его конце</exception>
        /// <param name="value">Стрим</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, значение которого меняется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StreamNotEmpty([CanBeNull] Stream value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.StreamNotEmpty(value, callerMemberName, message);
        }

        /// <summary>Проверка что строка содержит корректный Uri</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка описывающая Uri == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка описывающая Uri == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка описывающая Uri состоит только из пробелов</exception>
        /// <exception cref="T:Intermech.Diagnostics.InvalidUriException">Если Uri некорректен</exception>
        /// <param name="value">Строка, содержащая Uri</param>
        /// <param name="scheme">(Optional) Схема Uri которой должен соответствовать адрес. Например UriScheme.Http для Http адреса. Если
        /// null - схема не проверяется</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) Наименование строки</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UriCorrect(
          [CanBeNull] string value,
          UriScheme scheme = UriScheme.Any,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          Check.UriCorrect(value, callerMemberName, scheme, message);
        }

        /// <summary>Проверка что в словаре присутствует запись с переданным ключом</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNotFoundException">Если ключ не найден</exception>
        /// <param name="dictionary">Словарь</param>
        /// <param name="key">Ключ, который должен присутствовать в словаре</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("dictionary:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ContainsKey<TKey, TValue>(
          [CanBeNull, NoEnumeration] IReadOnlyDictionary<TKey, TValue> dictionary,
          [NotNull, NoEnumeration] TKey key,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentContainsKey<TKey, TValue>(dictionary, key, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIsPositive(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIsPositive(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(float value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          double num = (double) Check.ArgumentIsPositive(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(double value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIsPositive(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsZeroOrPositive(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIsZeroOrPositive(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsZeroOrPositive(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIsZeroOrPositive(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsZeroOrPositive(float value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          double num = (double) Check.ArgumentIsZeroOrPositive(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsZeroOrPositive(double value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIsZeroOrPositive(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIsNegative(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIsNegative(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(float value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          double num = (double) Check.ArgumentIsNegative(value, callerMemberName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(double value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          Check.ArgumentIsNegative(value, callerMemberName, message);
        }
      }

      /// <summary>Конструктор сообщения об ошибке для ненулевого объекта</summary>
      [NotNull]
      public delegate string ObjectMessageFactory([NotNull] object value);

      /// <summary>Конструктор сообщения об ошибке</summary>
      [NotNull]
      public delegate string TemplateMessageFactory<in T>([CanBeNull] T value);

      [NotNull]
      public delegate Exception EnumInRangeCustomExceptionFactory<in T>(T value) where T : struct, Enum;

      /// <summary>Debug only валидация условий</summary>
      public abstract class Debug
      {
        /// <summary>Запуск действия только при активном заданном дефайне DEBUG и/или FULL_CHECK</summary>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Invoke([NotNull] Action action) => action();

        /// <summary>Проверка аргумента на null</summary>
        /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotNull<T>([CanBeNull, NoEnumeration] T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : class
        {
          Check.ArgumentNotNull(value, valueName, message);
        }

        /// <summary>Проверка аргумента на null</summary>
        /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotNull<T>([CanBeNull, NoEnumeration] T? value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct
        {
          Check.ArgumentNotNull(value, valueName, message);
        }

        /// <summary>Проверка аргумента на null</summary>
        /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentGenericNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentGenericNotNull(value, valueName, message);
        }

        /// <summary>Проверка, что коллекция не пуста</summary>
        /// <exception cref="T:System.ArgumentNullException">Если коллекция равна null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentCollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotNullNotEmpty<TEnumerable>(
          [CanBeNull, NoEnumeration] TEnumerable value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull, CanBeEmpty] string message = null)
          where TEnumerable : class, IEnumerable
        {
          Check.ArgumentNotNullNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка аргумента на значение по-умолчанию</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentValueNotEmpty<T>(T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct
        {
          Check.ArgumentValueNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что аргумент не пуст (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue">Пустое значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentValueNotEmpty<T>(
          [NoEnumeration] T value,
          T emptyValue,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
          where T : struct
        {
          Check.ArgumentValueNotEmpty(value, emptyValue, valueName, message);
        }

        /// <summary>Проверка что аргумент не пуст (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue1">Пустое значение</param>
        /// <param name="emptyValue2">Пустое значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentValueNotEmpty<T>(
          [NoEnumeration] T value,
          T emptyValue1,
          T emptyValue2,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
          where T : struct
        {
          Check.ArgumentValueNotEmpty(value, emptyValue1, emptyValue2, valueName, message);
        }

        /// <summary>Проверка что аргумент IntPtr не пуст (пустое значение отличается от IntPtr.Zero)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == IntPtr.Zero</exception>
        /// <param name="value">Значение, которое не должно быть равно IntPtr.Zero</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentValueNotEmpty(IntPtr value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.ArgumentValueNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка перечисление на отсутствие значений по-умолчанию</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если в перечислении присутствует значение == default(T)</exception>
        /// <param name="value">Перечисление значений, которые не должны быть равны значению по-умолчанию для своего типа</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentValuesNotEmpty<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
          where T : struct
        {
          Check.ArgumentValuesNotEmpty(value, valueName, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления
        /// не выполнится переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="predicate">Условие</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentAll<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [CanBeNull] string message = null)
        {
          Check.ArgumentAll(value, valueName, predicate, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentAll<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [CanBeNull] string message = null)
        {
          Check.ArgumentAll(value, predicate, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="action">Метод проверки условия</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentAll<T>([CanBeNull, NoEnumeration] IEnumerable<T> value, [NotNull, InstantHandle] Action<T> action, [CanBeNull] string message = null)
        {
          Check.ArgumentAll(value, action, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="predicate">Условие</param>
        /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentAll<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [NotNull] TemplateMessageFactory<T> messageFactory)
        {
          Check.ArgumentAll(value, valueName, predicate, messageFactory);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentAll<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [NotNull] TemplateMessageFactory<T> messageFactory)
        {
          Check.ArgumentAll(value, predicate, messageFactory);
        }

        /// <summary>Проверка что элементы коллекции не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentItemsNotNull<T>([CanBeNull, NoEnumeration] T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : class, IEnumerable
        {
          Check.ArgumentItemsNotNull(value, valueName, message);
        }

        /// <summary>Проверка что элементы коллекции не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentItemsNotNull<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T?> value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
          where T : struct
        {
          Check.ArgumentItemsNotNull(value, valueName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пустой</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentCollectionNotEmpty<T>([CanBeNull, NoEnumeration] T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : class, ICollection
        {
          Check.ArgumentCollectionNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пустой</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentReadOnlyCollectionNotEmpty<T>(
          [CanBeNull, NoEnumeration] IReadOnlyCollection<T> value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
        {
          Check.ArgumentReadOnlyCollectionNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что последовательность не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если последовательность пуста</exception>
        /// <param name="value">Последовательность, которая не должна быть пустой</param>
        /// <param name="valueName">Наименование последовательности</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentEnumerationNotEmpty(
          [CanBeNull, NoEnumeration] IEnumerable value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
        {
          Check.ArgumentEnumerationNotEmpty<IEnumerable>(value, valueName, message);
        }

        /// <summary>Проверка что все строки в последовательности не null и не пусты</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <param name="value">Последовательность строк, которые быть не должны быть равны null или string.Empty</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentStringsNotEmpty(
          [CanBeNull, NoEnumeration] IEnumerable<string> value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
        {
          Check.ArgumentStringsNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что все строки последовательности не null, не пустые строки и не строки состоящие из одних пробелов</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemWhitespaceNotAllowedException">Если в последовательности присутствуют строки не
        /// содержащие ничего кроме пробелов</exception>
        /// <param name="value">Последовательность строк, которые быть не должны быть равны null, string.Empty или заполнены
        /// одними только пробелами</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentStringsNotWhitespace(
          [CanBeNull, NoEnumeration] IEnumerable<string> value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
        {
          Check.ArgumentStringsNotWhitespace(value, valueName, message);
        }

        /// <summary>Проверка строкового аргумента на null и на равенство string.Empty</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotNullOrEmpty([CanBeNull] string value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.ArgumentNotNullOrEmpty(value, valueName, message);
        }

        /// <summary>Проверка строкового аргумента на null и на равенство string.Empty или состоять только из пробелов</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty, или состоять только из
        /// пробелов</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotNullOrWhitespace([CanBeNull] string value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.ArgumentNotNullOrWhitespace(value, valueName, message);
        }

        /// <summary>Проверка аргумента на null и DBNull</summary>
        /// <exception cref="T:System.ArgumentNullException">Если аргумент == null или == DBNull</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : class
        {
          Check.ArgumentNotNullNotDbNull(value, valueName, message);
        }

        /// <summary>Проверка аргумента</summary>
        /// <exception cref="T:System.ArgumentException">Если условие проверки не выполняется</exception>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Argument(bool condition, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.Argument(condition, valueName, message);
        }

        /// <summary>Проверка аргумента</summary>
        /// <exception cref="T:System.ArgumentException">Если условие проверки не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка будет выполнена</param>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Argument<T>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
        {
          Check.Argument(value, condition, valueName, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>ArgumentException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="exceptionParams">Параметры, которые будут переданы в конструктор исключительной ситуации</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Argument<T, TException>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [NotNull, ItemNotNull] object[] exceptionParams)
          where TException : ArgumentException
        {
          Check.Argument<T, TException>(value, condition, exceptionParams);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>ArgumentException</cref>: Если условие не выполняется</exception>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Argument<TException>(bool condition, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where TException : ArgumentException
        {
          Check.Argument<TException>(condition, valueName, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>ArgumentException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Argument<T, TException>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
          where TException : ArgumentException
        {
          Check.Argument<T, TException>(value, condition, valueName, message);
        }

        /// <summary>Проверка аргумента</summary>
        /// <exception cref="T:System.ArgumentException">Если условие проверки не выполняется</exception>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Argument([NotNull, InstantHandle] Func<bool> condition, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.Argument(condition, valueName, message);
        }

        /// <summary>Проверка попадания значения аргумента в список допустимых значений</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentInRange(bool condition, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.ArgumentInRange(condition, valueName, message);
        }

        /// <summary>Проверка попадания значения аргумента в список допустимых значений</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка будет выполнена</param>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentInRange<T>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
        {
          Check.ArgumentInRange(value, condition, valueName, message);
        }

        /// <summary>Проверка попадания значения аргумента в список допустимых значений</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка будет выполнена</param>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentInRange<T>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<bool> condition,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
        {
          Check.ArgumentInRange(value, condition, valueName, message);
        }

        /// <summary>Проверка попадания значения аргумента в список допустимых значений</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentInRange([NotNull, InstantHandle] Func<bool> condition, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.ArgumentInRange(condition(), valueName, message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIndexInRange(
          int index,
          int count,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          Check.ArgumentIndexInRange(index, count, valueName, message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIndexInRange(
          long index,
          long count,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          Check.ArgumentIndexInRange(index, count, valueName, message);
        }

        /// <summary>Проверка что строка содержит guid</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.ArgumentException">Если строка не содержит GUID</exception>
        /// <param name="guid">Строка, которая должна содержать Guid</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsGuid([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentIsGuid(guid, valueName, message);
        }

        /// <summary>Проверка что строка содержит непустой guid</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.ArgumentException">Если строка не содержит GUID</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если GUID пуст</exception>
        /// <param name="guid">Строка, которая должна содержать непустой Guid</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentGuidNotEmpty([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentGuidNotEmpty(guid, valueName, message);
        }

        /// <summary>Проверка что guid не пуст</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если guid == Guid.Empty</exception>
        /// <param name="guid">Guid, который не должен быть равен Guid.Empty</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentGuidNotEmpty(Guid guid, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.ArgumentGuidNotEmpty(guid, valueName, message);
        }

        /// <summary>Проверка что в словаре присутствует запись с переданным ключом</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNotFoundException">Если ключ не найден</exception>
        /// <param name="dictionary">Словарь</param>
        /// <param name="key">Ключ, который должен присутствовать в словаре</param>
        /// <param name="dictionaryName">Наименование словаря</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("dictionary:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentContainsKey<TKey, TValue>(
          [CanBeNull, NoEnumeration] IReadOnlyDictionary<TKey, TValue> dictionary,
          [NotNull, NoEnumeration] TKey key,
          [CanBeNull, NotWhitespace, InvokerParameterName] string dictionaryName,
          [CanBeNull] string message = null)
        {
          Check.ArgumentContainsKey(dictionary, key, dictionaryName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentIsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentIsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          double num = (double) Check.ArgumentIsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentIsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsZeroOrPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentIsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsZeroOrPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentIsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsZeroOrPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          double num = (double) Check.ArgumentIsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsZeroOrPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentIsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsNegative(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentIsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsNegative(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentIsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsNegative(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          double num = (double) Check.ArgumentIsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsNegative(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentIsNegative(value, valueName, message);
        }

        /// <summary>Проверка что тип T является ссылочным</summary>
        /// <param name="valueName">(Optional) Наименование проверяемого параметра</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsRefType<T>([CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          if (!typeof (T).IsByRef)
            throw !string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? new ArgumentException(message, valueName) : new ArgumentException(message)) : (!string.IsNullOrWhiteSpace(valueName) ? new ArgumentException($"Value {valueName} of type {typeof (T)} with is not reference type!", valueName) : new ArgumentException($"Type {typeof (T)} is not reference type!"));
        }

        /// <summary>Проверка что тип T является типом-значением</summary>
        /// <param name="valueName">(Optional) Наименование проверяемого параметра</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentIsValueType<T>([CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          if (!typeof (T).IsValueType)
            throw !string.IsNullOrWhiteSpace(message) ? (!string.IsNullOrWhiteSpace(valueName) ? new ArgumentException(message, valueName) : new ArgumentException(message)) : (!string.IsNullOrWhiteSpace(valueName) ? new ArgumentException($"Value {valueName} of type {typeof (T)} with is not value type!", valueName) : new ArgumentException($"Type {typeof (T)} is not reference type!"));
        }

        /// <summary>Проверка что строка содержит guid</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.FormatException">Если строка не содержит GUID</exception>
        /// <param name="guid">Строка, которая должна содержать Guid</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsGuid([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IsGuid(guid, valueName, message);
        }

        /// <summary>Проверка что строка содержит непустой guid</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.FormatException">Если строка не содержит GUID</exception>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если GUID пуст</exception>
        /// <param name="guid">Строка, которая должна содержать непустой Guid</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GuidNotEmpty([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.ArgumentGuidNotEmpty(guid, valueName, message);
        }

        /// <summary>Проверка что guid не пуст</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если guid == Guid.Empty</exception>
        /// <param name="guid">Guid, который не должен быть равен Guid.Empty</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GuidNotEmpty(Guid guid, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.GuidNotEmpty(guid, valueName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T>([CanBeNull, NoEnumeration] T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : class
        {
          Check.NotNull(value, valueName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T>([CanBeNull, NoEnumeration] T? value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct
        {
          Check.NotNull(value, valueName, message);
        }

        /// <summary>Проверка объект не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GenericNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.GenericNotNull(value, valueName, message);
        }

        /// <summary>Проверка значения на значение по-умолчанию</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValueNotEmpty<T>(T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct
        {
          Check.ValueNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что значение не пусто (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue">Пустое значение параметра</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValueNotEmpty<T>([NoEnumeration] T value, T emptyValue, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct
        {
          Check.ValueNotEmpty(value, emptyValue, valueName, message);
        }

        /// <summary>Проверка что значение не пусто (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue1">Пустое значение параметра</param>
        /// <param name="emptyValue2">Пустое значение параметра</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValueNotEmpty<T>(
          [NoEnumeration] T value,
          T emptyValue1,
          T emptyValue2,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
          where T : struct
        {
          Check.ValueNotEmpty(value, emptyValue1, emptyValue2, valueName, message);
        }

        /// <summary>Проверка что значение IntPtr не пусто (не равно IntPtr.Zero)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == IntPtr.Zero</exception>
        /// <param name="value">Значение, которое не должно быть равно IntPtr.Zero</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValueNotEmpty(IntPtr value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.ValueNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка попадания значения в допустимый диапазон</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если значение выходит за рамки допустимого диапазона значений</exception>
        /// <param name="value">Значение, которое должно попадать в допустимый диапазон</param>
        /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InRange<T>(
          [CanBeNull] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          Check.InRange(value, condition, valueName, message);
        }

        /// <summary>Проверка попадания значения в допустимый диапазон</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если значение выходит за рамки допустимого диапазона значений</exception>
        /// <param name="value">Значение, которое должно попадать в допустимый диапазон</param>
        /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InRange<T>([CanBeNull] T value, [NotNull, InstantHandle] Func<bool> condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.InRange(value, condition, valueName, message);
        }

        /// <summary>Проверка попадания значения в допустимый диапазон</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если значение выходит за рамки допустимого диапазона значений</exception>
        /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InRange([NotNull, InstantHandle] Func<bool> condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.InRange(condition(), valueName, message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IndexInRange(int index, int count, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IndexInRange(index, count, valueName, message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IndexInRange(long index, long count, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IndexInRange(index, count, valueName, message);
        }

        /// <summary>Проверка перечисление на отсутствие значений по-умолчанию</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если в перечислении присутствует значение == default(T)</exception>
        /// <param name="value">Перечисление значений, которые не должны быть равны значению по-умолчанию для своего типа</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValuesNotEmpty<T>([CanBeNull, NoEnumeration] IEnumerable<T> value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct
        {
          Check.ValuesNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка строки на null и на равенство string.Empty</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty([CanBeNull] string value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.NotNullOrEmpty(value, valueName, message);
        }

        /// <summary>Проверка строки на null и на равенство string.Empty или состоять только из пробелов</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty, или состоять только из
        /// пробелов</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrWhitespace([CanBeNull] string value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.NotNullOrWhitespace(value, valueName, message);
        }

        /// <summary>Проверка что объект не null и не DBNull</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект == null или DBNull</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : class
        {
          Check.NotNullNotDbNull(value, valueName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception><cref>TException</cref>Если условие не выполняется</exception>
        /// <param name="value">Объект, который должен быть не null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T, TException>([CanBeNull, NoEnumeration] T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
          where T : class
          where TException : NullReferenceException
        {
          Check.NotNull<T, TException>(value, valueName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception><cref>TException</cref>Если условие не выполняется</exception>
        /// <param name="value">Объект, который должен быть не null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T, TException>([CanBeNull, NoEnumeration] T? value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
          where T : struct
          where TException : NullReferenceException
        {
          Check.NotNull<T, TException>(value, valueName, message);
        }

        /// <summary>Проверка что элементы последовательности не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ItemsNotNull<T>([CanBeNull, NoEnumeration] T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : class, IEnumerable
        {
          Check.ItemsNotNull(value, valueName, message);
        }

        /// <summary>Проверка что элементы последовательности не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ItemsNotNull<T>([CanBeNull, NoEnumeration] IEnumerable<T?> value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct
        {
          Check.ItemsNotNull(value, valueName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CollectionNotEmpty([CanBeNull, NoEnumeration] ICollection value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.CollectionNotEmpty<ICollection>(value, valueName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReadOnlyCollectionNotEmpty<T>(
          [CanBeNull, NoEnumeration] IReadOnlyCollection<T> value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
        {
          Check.ReadOnlyCollectionNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что последовательность не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если последовательность пуста</exception>
        /// <param name="value">Последовательность, которая не должна быть пуста</param>
        /// <param name="valueName">Наименование последовательности</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnumerationNotEmpty([CanBeNull, NoEnumeration] IEnumerable value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.EnumerationNotEmpty<IEnumerable>(value, valueName, message);
        }

        /// <summary>Проверка что все строки последовательности не null и не пусты</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <param name="value">Последовательность строк, которые быть не должны быть равны null или string.Empty</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StringsNotEmpty([CanBeNull, NoEnumeration] IEnumerable<string> value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.StringsNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что все строки последовательности не null, не пустые строки и не строки состоящие из одних пробелов</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemWhitespaceNotAllowedException">Если в последовательности присутствуют строки не содержащие ничего
        /// кроме пробелов</exception>
        /// <param name="value">Последовательность строк, которые быть не должны быть равны null, string.Empty или заполнены
        /// одними только пробелами</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StringsNotWhitespace(
          [CanBeNull, NoEnumeration] IEnumerable<string> value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
        {
          Check.StringsNotWhitespace(value, valueName, message);
        }

        /// <summary>Проверка что элементы коллекции не null и не DBNull</summary>
        /// <exception cref="T:System.NullReferenceException">Если условие не выполняется</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ItemsNotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : class, IEnumerable
        {
          Check.ItemsNotNullNotDbNull(value, valueName, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception cref="T:System.Exception">Если условие не выполняется</exception>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert(bool condition, [CanBeNull, NotEmpty, InvokerParameterName] string message = null)
        {
          Check.Assert(condition, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception cref="T:System.Exception">Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert<T>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull, NotEmpty, InvokerParameterName] string message = null)
        {
          Check.Assert(value, condition, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="exceptionParams">Параметры, которые будут переданы в конструктор исключительной ситуации</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert<TException>(bool condition, [NotNull, ItemCanBeNull] object[] exceptionParams) where TException : Exception
        {
          Check.Assert<TException>(condition, exceptionParams);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="exceptionParams">Параметры, которые будут переданы в конструктор исключительной ситуации</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert<T, TException>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [NotNull, ItemCanBeNull] object[] exceptionParams)
          where TException : Exception
        {
          Check.Assert<T, TException>(value, condition, exceptionParams);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert<TException>(bool condition, [CanBeNull] string message = null) where TException : Exception
        {
          Check.Assert<TException>(condition, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert<T, TException>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull] string message = null) where TException : Exception
        {
          Check.Assert<T, TException>(value, condition, message);
        }

        /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
        /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ItemsIs<T>([CanBeNull, NoEnumeration] IEnumerable value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.ItemsIs<T>(value, valueName, message);
        }

        /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
        /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="messageFactory">Метод-фабрика сообщений об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ItemsIs<T>(
          [CanBeNull, NoEnumeration] IEnumerable value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
        {
          Check.ItemsIs<T>(value, valueName, messageFactory);
        }

        /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
        /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="messageFactory">Метод-фабрика сообщений об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ItemsIs<T>([CanBeNull, NoEnumeration] IEnumerable value, [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
        {
          Check.ItemsIs<T>(value, (string) null, messageFactory);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="predicate">Условие</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void All<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [CanBeNull] string message = null)
        {
          Check.All(value, valueName, predicate, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void All<T>([CanBeNull, NoEnumeration] IEnumerable<T> value, [NotNull, InstantHandle] Func<T, bool> predicate, [CanBeNull] string message = null)
        {
          Check.All(value, predicate, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="predicate">Условие</param>
        /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void All<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [NotNull] TemplateMessageFactory<T> messageFactory)
        {
          Check.All(value, valueName, predicate, messageFactory);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void All<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [NotNull] TemplateMessageFactory<T> messageFactory)
        {
          Check.All(value, (string) null, predicate, messageFactory);
        }

        /// <summary>Проверка, что перечисление не пусто</summary>
        /// <exception cref="T:System.NullReferenceException">Если перечисление равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если перечисление пусто</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullNotEmpty<TEnumerable>(
          [CanBeNull, NoEnumeration] TEnumerable value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull, CanBeEmpty] string message = null)
          where TEnumerable : class, IEnumerable
        {
          Check.NotNullNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка состояния объекта (значений полей/свойств)</summary>
        /// <exception cref="T:System.InvalidOperationException">Если condition == false</exception>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ObjectState(bool condition, [CanBeNull, InvokerParameterName] string message = null)
        {
          Check.ObjectState(condition, message);
        }

        /// <summary>Проверка состояния объекта (значений полей/свойств)</summary>
        /// <exception cref="T:System.NullReferenceException">Если value == null</exception>
        /// <exception cref="T:System.InvalidOperationException">Если condition == false</exception>
        /// <param name="value">Значение, которое будет возвращено, если проверка будет пройдена</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt; value:null => halt")]
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ObjectState<T>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull, InvokerParameterName] string message = null)
        {
          Check.ObjectState(value, valueName, condition, message);
        }

        /// <summary>Проверка состояния объекта (значений полей/свойств)</summary>
        /// <exception cref="T:System.NullReferenceException">Если value == null</exception>
        /// <exception cref="T:System.InvalidOperationException">Если condition == false</exception>
        /// <param name="value">Значение, которое будет возвращено, если проверка будет пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt; value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ObjectState<T>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull, InvokerParameterName] string message = null)
        {
          Check.ObjectState(value, (string) null, condition, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="type">Тип значения</param>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Obsolete("Удалите передачу типа Enum, начиная c C# 7.2 он не требуется")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnumInRange<T>([NotNull] Type type, T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct, Enum
        {
          Check.EnumInRange(value, valueName, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnumInRange<T>(T value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct, Enum
        {
          Check.EnumInRange(value, valueName, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnumInRange<T>(long value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct, Enum
        {
          Check.EnumInRange<T>((object) value, valueName, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnumInRange<T>(int value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct, Enum
        {
          Check.EnumInRange<T>((object) value, valueName, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="value">Значение</param>
        /// <param name="getExceptionFunc">Метод-конструктор исключительной ситуации</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnumInRangeCustom<T>(
          T value,
          [NotNull] EnumInRangeCustomExceptionFactory<T> getExceptionFunc)
          where T : struct, Enum
        {
          Check.EnumInRangeCustom(value, getExceptionFunc);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="type">Тип значения</param>
        /// <param name="values">Список значений</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Obsolete("Удалите передачу типа Enum, начиная c C# 7.2 он не требуется")]
        [ContractAnnotation("values:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AllEnumInRange<T>(
          [NotNull] Type type,
          [CanBeNull, NoEnumeration] IEnumerable<T> values,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [CanBeNull] string message = null)
          where T : struct, Enum
        {
          Check.AllEnumInRange(values, valueName, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="values">Список значений</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("values:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AllEnumInRange<T>([CanBeNull, NoEnumeration] IEnumerable<T> values, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct, Enum
        {
          Check.AllEnumInRange(values, valueName, message);
        }

        /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <exception cref="T:System.InvalidCastException">Если тип переданного объекта не <see cref="!:T" /></exception>
        /// <param name="value">Проверяемый объект</param>
        /// <param name="valueName">Наименование переданного объекта</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Is<T>([CanBeNull, NoEnumeration] object value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.Is<T>(value, valueName, message);
        }

        /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <exception cref="T:System.InvalidCastException">Если тип переданного объекта не <see cref="!:T" /></exception>
        /// <param name="value">Проверяемый объект</param>
        /// <param name="valueName">Наименование переданного объекта</param>
        /// <param name="messageFactory">Внешняя ф-ия получения сообщения об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Is<T>(
          [CanBeNull, NoEnumeration] object value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
        {
          Check.Is<T>(value, valueName, messageFactory);
        }

        /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <exception cref="T:System.Exception">Если тип переданного объекта не <see cref="!:T" /></exception>
        /// <param name="value">Проверяемый объект</param>
        /// <param name="messageFactory">Внешняя ф-ия получения сообщения об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Is<T>([CanBeNull, NoEnumeration] object value, [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
        {
          Check.Is<T>(value, (string) null, messageFactory);
        }

        /// <summary>Проверка того, что файл по указанному пути существует на диске</summary>
        /// <exception cref="T:System.ArgumentNullException">Если указанный путь == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если указанный путь == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если указанный путь состоит только из пробелов</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">Если файл отсутствует на диске</exception>
        /// <param name="value">Путь к файлу</param>
        /// <param name="valueName">Наименование переданного значения</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FileExists([CanBeNull] string value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.FileExists(value, valueName, message);
        }

        /// <summary>Проверка того, что папка по указанному пути существует на диске</summary>
        /// <exception cref="T:System.ArgumentNullException">Если указанный путь == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если указанный путь == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если указанный путь состоит только из пробелов</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">Если папка отсутствует на диске</exception>
        /// <param name="value">Путь к папке</param>
        /// <param name="valueName">Наименование переданного значения</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DirectoryExists([CanBeNull] string value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.DirectoryExists(value, valueName, message);
        }

        /// <summary>Проверка что стрим не равен null, что имеет ненулевую длину и текущая позиция не находится в конце стрима</summary>
        /// <exception cref="T:System.ArgumentNullException">Если переданный стрим == null</exception>
        /// <exception cref="M:Intermech.Diagnostics.Check.Debug.StreamNotEmpty(System.IO.Stream,System.String,System.String)">Если длина стрима равна 0</exception>
        /// <exception cref="T:System.IO.EndOfStreamException">Если позиция в преданном стриме находится в его конце</exception>
        /// <param name="value">Стрим</param>
        /// <param name="valueName">Наименование стрима</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StreamNotEmpty([CanBeNull] Stream value, [NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          Check.StreamNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что строка содержит корректный Uri</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка описывающая Uri == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка описывающая Uri == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка описывающая Uri состоит только из пробелов</exception>
        /// <exception cref="T:Intermech.Diagnostics.InvalidUriException">Если Uri некорректен</exception>
        /// <param name="value">Строка, содержащая Uri</param>
        /// <param name="valueName">Наименование строки</param>
        /// <param name="scheme">(Optional) Схема Uri которой должен соответствовать адрес. Например UriScheme.Http для Http
        /// адреса. Если null - схема не проверяется</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UriCorrect(
          [CanBeNull] string value,
          [NotNull, NotWhitespace, InvokerParameterName] string valueName,
          UriScheme scheme = UriScheme.Any,
          [CanBeNull] string message = null)
        {
          Check.UriCorrect(value, valueName, scheme, message);
        }

        /// <summary>Проверка что в словаре присутствует запись с переданным ключом</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNotFoundException">Если ключ не найден</exception>
        /// <param name="dictionary">Словарь</param>
        /// <param name="key">Ключ, который должен присутствовать в словаре</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("dictionary:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ContainsKey<TKey, TValue>(
          [CanBeNull, NoEnumeration] IReadOnlyDictionary<TKey, TValue> dictionary,
          [NotNull, NoEnumeration] TKey key,
          [CanBeNull] string message = null)
        {
          Check.ContainsKey(dictionary, key, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          double num = (double) Check.IsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsZeroOrPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsZeroOrPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsZeroOrPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          double num = (double) Check.IsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsZeroOrPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          double num = (double) Check.IsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          Check.IsNegative(value, valueName, message);
        }

        /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
        /// Для контроля используется ссылка на объект, которая в Dispose устанавливается в null</summary>
        /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и notNullRef == null</exception>
        /// <param name="notNullRef">Контрольная ссылка, которая становится равной null после вызова Dispose</param>
        /// <param name="objectName">(Optional) Имя объекта</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("notNullRef:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotDisposed([CanBeNull] object notNullRef, [CanBeNull, NotWhitespace] string objectName = null, [CanBeNull] string message = null)
        {
          Check.NotDisposed(notNullRef, objectName, message);
        }

        /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
        /// Для контроля используется ссылка на объект, которая в Dispose устанавливается в null</summary>
        /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и notNullRef == null</exception>
        /// <param name="notNullRef">Контрольная ссылка, которая становится равной null после вызова Dispose</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("notNullRef:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotDisposed<T>([CanBeNull] object notNullRef, [CanBeNull] string message = null) where T : IDisposable
        {
          Check.NotDisposed<T>(notNullRef, message);
        }

        /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
        /// Для контроля флаг, который устанавливается в True в самом начале Dispose</summary>
        /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и disposedFlag == true</exception>
        /// <param name="disposedFlag">Флаг, который устанавливается в True в самом начале Dispose</param>
        /// <param name="objectName">(Optional) Имя объекта</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("disposedFlag:true => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotDisposed(bool disposedFlag, [CanBeNull, NotWhitespace] string objectName = null, [CanBeNull] string message = null)
        {
          Check.NotDisposed(disposedFlag, objectName, message);
        }

        /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
        /// Для контроля флаг, который устанавливается в True в самом начале Dispose</summary>
        /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и disposedFlag == true</exception>
        /// <param name="disposedFlag">Флаг, который устанавливается в True в самом начале Dispose</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [ContractAnnotation("disposedFlag:true => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotDisposed<T>(bool disposedFlag, [CanBeNull] string message = null) where T : IDisposable
        {
          Check.NotDisposed<T>(disposedFlag, message);
        }

        /// <summary>Проверка что тип T является ссылочным</summary>
        /// <param name="valueName">(Optional) Наименование проверяемого параметра</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsRefType<T>([CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          if (!typeof (T).IsByRef)
            throw new InvalidOperationException(!string.IsNullOrWhiteSpace(message) ? message : (!string.IsNullOrWhiteSpace(valueName) ? $"Value {valueName} of type {typeof (T)} with is not reference type!" : $"Type {typeof (T)} is not reference type!"));
        }

        /// <summary>Проверка что тип T является типом-значением</summary>
        /// <param name="valueName">(Optional) Наименование проверяемого параметра</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsValueType<T>([CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          if (!typeof (T).IsValueType)
            throw new InvalidOperationException(!string.IsNullOrWhiteSpace(message) ? message : (!string.IsNullOrWhiteSpace(valueName) ? $"Value {valueName} of type {typeof (T)} with is not value type!" : $"Type {typeof (T)} is not reference type!"));
        }

        /// <summary>Проверка что объект заблокирован конструкцией lock. Служит для проверки того, что контекст вызова
        /// потокобезопасен</summary>
        /// <exception cref="T:System.InvalidOperationException">Если объект не заблокирован конструкцией lock</exception>
        /// <param name="syncObject">Объект, который должен быть заблокирован конструкцией lock</param>
        /// <param name="syncObjectName">(Optional) Имя объекта, который используется для блокирования доступа к контексту вызова</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [Conditional("DEBUG")]
        [Conditional("FULL_CHECK")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SyncLocked<T>([NotNull] T syncObject, [CanBeNull, NotWhitespace, InvokerParameterName] string syncObjectName = null, [CanBeNull] string message = null) where T : class
        {
          Check.SyncLocked(syncObject, syncObjectName, message);
        }
      }

      /// <summary>Условная валидация условий. Все методы работают только если у класса установлен статический флаг Enabled,
      /// иначе значения возвращаются прозрачно без проверки.</summary>
      public abstract class Optional
      {
        /// <summary>Производить ли опциональные проверки</summary>
        public static bool Enabled;

        /// <summary>Запуск действия только при активном заданном дефайне DEBUG и/или FULL_CHECK</summary>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Invoke([NotNull] Action action)
        {
          if (!Check.Optional.Enabled)
            return;
          action();
        }

        /// <summary>Проверка аргумента на null</summary>
        /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentNotNull(value, valueName, message);
        }

        /// <summary>Проверка аргумента на null</summary>
        /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentNotNull<T>([CanBeNull, NoEnumeration] T? value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct
        {
          return !Check.Optional.Enabled ? value.Value : Check.ArgumentNotNull(value, valueName, message);
        }

        /// <summary>Проверка что аргумент не null</summary>
        /// <exception><cref>TException</cref>Если условие не выполняется</exception>
        /// <param name="value">Объект, который должен быть не null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentNotNull<T, TException>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
          where T : class
          where TException : ArgumentNullException
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentNotNull<T, TException>(value, valueName, message);
        }

        /// <summary>Проверка что аргумент не null</summary>
        /// <exception><cref>TException</cref>Если условие не выполняется</exception>
        /// <param name="value">Объект, который должен быть не null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentNotNull<T, TException>([CanBeNull, NoEnumeration] T? value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
          where T : struct
          where TException : ArgumentNullException
        {
          return !Check.Optional.Enabled ? value.Value : Check.ArgumentNotNull<T, TException>(value, valueName, message);
        }

        /// <summary>Проверка аргумента на null</summary>
        /// <exception cref="T:System.ArgumentNullException">Если аргумент == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt; => NotNull")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentGenericNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentGenericNotNull(value, valueName, message);
        }

        /// <summary>Проверка, что коллекция не пуста</summary>
        /// <exception cref="T:System.ArgumentNullException">Если коллекция равна null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentCollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnumerable ArgumentNotNullNotEmpty<TEnumerable>(
          [CanBeNull, NoEnumeration] TEnumerable value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull, CanBeEmpty] string message = null)
          where TEnumerable : class, IEnumerable
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentNotNullNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка аргумента на значение по-умолчанию</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentValueNotEmpty<T>(T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentValueNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что аргумент не пуст (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue">Пустое значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentValueNotEmpty<T>(
          T value,
          T emptyValue,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
          where T : struct
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentValueNotEmpty(value, emptyValue, valueName, message);
        }

        /// <summary>Проверка что аргумент не пуст (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue1">Пустое значение</param>
        /// <param name="emptyValue2">Пустое значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentValueNotEmpty<T>(
          T value,
          T emptyValue1,
          T emptyValue2,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
          where T : struct
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentValueNotEmpty(value, emptyValue1, emptyValue2, valueName, message);
        }

        /// <summary>Проверка что аргумент не пусто (не равно IntPtr.Zero)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если аргумент == IntPtr.Zero</exception>
        /// <param name="value">Значение, которое не должно быть равно IntPtr.Zero</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr ArgumentValueNotEmpty(IntPtr value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentValueNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка перечисление на отсутствие значений по-умолчанию</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если в перечислении присутствует значение == default(T)</exception>
        /// <param name="value">Перечисление значений, которые не должны быть равны значению по-умолчанию для своего типа</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ArgumentValuesNotEmpty<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
          where T : struct
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentValuesNotEmpty(value, valueName, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="predicate">Условие</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ArgumentAll<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentAll(value, valueName, predicate, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ArgumentAll<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentAll(value, predicate, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="predicate">Условие</param>
        /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ArgumentAll<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [NotNull] TemplateMessageFactory<T> messageFactory)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentAll(value, valueName, predicate, messageFactory);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ArgumentAll<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [NotNull] TemplateMessageFactory<T> messageFactory)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentAll(value, predicate, messageFactory);
        }

        /// <summary>Проверка что элементы коллекции не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentItemsNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentItemsNotNull(value, valueName, message);
        }

        /// <summary>Проверка что элементы коллекции не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ArgumentItemsNotNull<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T?> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
          where T : struct
        {
          return !Check.Optional.Enabled ? value.Cast<T>() : Check.ArgumentItemsNotNull(value, valueName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentCollectionNotEmpty<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, ICollection
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentCollectionNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyCollection<T> ArgumentReadOnlyCollectionNotEmpty<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IReadOnlyCollection<T> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentReadOnlyCollectionNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TCollection ArgumentReadOnlyCollectionNotEmpty<TCollection, T>(
          [CanBeNull, NoEnumeration] TCollection value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
          where TCollection : class, IReadOnlyCollection<T>
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentReadOnlyCollectionNotEmpty<TCollection, T>(value, valueName, message);
        }

        /// <summary>Проверка что последовательность не пусто</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если последовательность пусто</exception>
        /// <param name="value">Последовательность, которая не должна быть пуста</param>
        /// <param name="valueName">Наименование последовательности</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentEnumerationNotEmpty<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentEnumerationNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что все строки в последовательности не null и не пусты</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <param name="value">Коллекция строк, которые быть не должны быть равны null или string.Empty</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [ItemNotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> ArgumentStringsNotEmpty(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<string> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentStringsNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что все строки последовательности не null, не пустые строки и не строки состоящие из одних пробелов</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemWhitespaceNotAllowedException">Если в последовательности присутствуют строки не
        /// содержащие ничего кроме пробелов</exception>
        /// <param name="value">Последовательность строк, которые быть не должны быть равны null, string.Empty или заполнены
        /// одними только пробелами</param>
        /// <param name="valueName">(Optional) Наименование коллекции</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [ItemNotWhitespace]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> ArgumentStringsNotWhitespace(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<string> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentStringsNotWhitespace(value, valueName, message);
        }

        /// <summary>Проверка что элементы коллекции не null и не DBNull</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если коллекция содержит null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNullsNotAllowedException">Если коллекция содержит DBNull</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [ItemNotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentItemsNotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentItemsNotNullNotDbNull(value, valueName, message);
        }

        /// <summary>Проверка строкового аргумента на null и на равенство string.Empty</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ArgumentNotNullOrEmpty([CanBeNull] string value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentNotNullOrEmpty(value, valueName, message);
        }

        /// <summary>Проверка строкового аргумента на null и на равенство string.Empty или состоять только из пробелов</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty, или состоять только из
        /// пробелов</param>
        /// <param name="valueName">(Optional) Наименование проверяемого параметра</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotWhitespace]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ArgumentNotNullOrWhitespace(
          [CanBeNull] string value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentNotNullOrWhitespace(value, valueName, message);
        }

        /// <summary>Проверка аргумента на null и DBNull</summary>
        /// <exception cref="T:System.ArgumentNullException">Если аргумент == null или == DBNull</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentNotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentNotNullNotDbNull(value, valueName, message);
        }

        /// <summary>Проверка аргумента</summary>
        /// <exception cref="T:System.ArgumentException">Если условие проверки не выполняется</exception>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Argument(bool condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          if (!Check.Optional.Enabled)
            return;
          Check.Argument(condition, valueName, message);
        }

        /// <summary>Проверка аргумента</summary>
        /// <exception cref="T:System.ArgumentException">Если условие проверки не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка будет выполнена</param>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt; value:null => null")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Argument<T>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.Argument(value, condition, valueName, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>ArgumentException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="exceptionParams">Параметры, которые будут переданы в конструктор исключительной ситуации</param>
        [ContractAnnotation("condition:false => halt; value:null => null")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Argument<T, TException>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [NotNull, ItemNotNull] object[] exceptionParams)
          where TException : ArgumentException
        {
          return !Check.Optional.Enabled ? value : Check.Argument<T, TException>(value, condition, exceptionParams);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>ArgumentException</cref>: Если условие не выполняется</exception>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Argument<TException>(bool condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where TException : ArgumentException
        {
          if (!Check.Optional.Enabled)
            return;
          Check.Argument<TException>(condition, valueName, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>ArgumentException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt; value:null => null")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Argument<T, TException>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
          where TException : ArgumentException
        {
          return !Check.Optional.Enabled ? value : Check.Argument<T, TException>(value, condition, valueName, message);
        }

        /// <summary>Проверка аргумента</summary>
        /// <exception cref="T:System.ArgumentException">Если условие проверки не выполняется</exception>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Argument([NotNull, InstantHandle] Func<bool> condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          if (!Check.Optional.Enabled)
            return;
          Check.Argument(condition, valueName, message);
        }

        /// <summary>Проверка попадания значения аргумента в список допустимых значений</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentInRange(bool condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          if (!Check.Optional.Enabled)
            return;
          Check.ArgumentInRange(condition, valueName, message);
        }

        /// <summary>Проверка попадания значения аргумента в список допустимых значений</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentInRange([NotNull, InstantHandle] Func<bool> condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          if (!Check.Optional.Enabled)
            return;
          Check.ArgumentInRange(condition(), valueName, message);
        }

        /// <summary>Проверка попадания значения аргумента в список допустимых значений</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка будет выполнена</param>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt; value:null => null")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentInRange<T>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentInRange(value, condition, valueName, message);
        }

        /// <summary>Проверка попадания значения аргумента в список допустимых значений</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если условие проверки не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка будет выполнена</param>
        /// <param name="condition">Условие проверки значения аргумента</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt; value:null => null")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentInRange<T>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<bool> condition,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentInRange(value, condition, valueName, message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ZeroOrPositiveNumber]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ArgumentIndexInRange(int index, int count, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? index : Check.ArgumentIndexInRange(index, count, valueName, message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ZeroOrPositiveNumber]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ArgumentIndexInRange(
          long index,
          long count,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          return Check.ArgumentIndexInRange(index, count, valueName, message);
        }

        /// <summary>Проверка что строка содержит guid</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.ArgumentException">Если строка не содержит GUID</exception>
        /// <param name="guid">Строка, которая должна содержать Guid</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotNull]
        [GuidStr]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ArgumentIsGuid([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? guid : Check.ArgumentIsGuid(guid, valueName, message);
        }

        /// <summary>Проверка что строка содержит непустой guid</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.ArgumentException">Если строка не содержит GUID</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если GUID пуст</exception>
        /// <param name="guid">Строка, которая должна содержать непустой Guid</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotNull]
        [NotEmptyGuid]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ArgumentGuidNotEmpty([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? guid : Check.ArgumentGuidNotEmpty(guid, valueName, message);
        }

        /// <summary>Проверка что guid не пуст</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentValueEmptyException">Если guid == Guid.Empty</exception>
        /// <param name="guid">Guid, который не должен быть равен Guid.Empty</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid ArgumentGuidNotEmpty(Guid guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? guid : Check.ArgumentGuidNotEmpty(guid, valueName, message);
        }

        /// <summary>Проверка что в словаре присутствует запись с переданным ключом</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNotFoundException">Если ключ не найден</exception>
        /// <param name="dictionary">Словарь</param>
        /// <param name="key">Ключ, который должен присутствовать в словаре</param>
        /// <param name="dictionaryName">Наименование словаря</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("dictionary:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<TKey, TValue> ArgumentContainsKey<TKey, TValue>(
          [CanBeNull, NoEnumeration] IReadOnlyDictionary<TKey, TValue> dictionary,
          [NotNull, NoEnumeration] TKey key,
          [CanBeNull, NotWhitespace, InvokerParameterName] string dictionaryName,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? dictionary : Check.ArgumentContainsKey(dictionary, key, dictionaryName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ArgumentIsPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ArgumentIsPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ArgumentIsPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ArgumentIsPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ArgumentIsZeroOrPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ArgumentIsZeroOrPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ArgumentIsZeroOrPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ArgumentIsZeroOrPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ArgumentIsNegative(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ArgumentIsNegative(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ArgumentIsNegative(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ArgumentIsNegative(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ArgumentIsNegative(value, valueName, message);
        }

        /// <summary>Проверка что строка содержит guid</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.FormatException">Если строка не содержит GUID</exception>
        /// <param name="guid">Строка, которая должна содержать Guid</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotNull]
        [GuidStr]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string IsGuid([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? guid : Check.IsGuid(guid, valueName, message);
        }

        /// <summary>Проверка что строка содержит непустой guid</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.FormatException">Если строка не содержит GUID</exception>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если GUID пуст</exception>
        /// <param name="guid">Строка, которая должна содержать непустой Guid</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotNull]
        [NotEmptyGuid]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GuidNotEmpty([CanBeNull] string guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? guid : Check.ArgumentGuidNotEmpty(guid, valueName, message);
        }

        /// <summary>Проверка что guid не пуст</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если guid == Guid.Empty</exception>
        /// <param name="guid">Guid, который не должен быть равен Guid.Empty</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid GuidNotEmpty(Guid guid, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? guid : Check.GuidNotEmpty(guid, valueName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class
        {
          return !Check.Optional.Enabled ? value : Check.NotNull(value, valueName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NotNull<T>([CanBeNull, NoEnumeration] T? value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct
        {
          return !Check.Optional.Enabled ? value.Value : Check.NotNull(value, valueName, message);
        }

        /// <summary>Проверка объект не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt; => NotNull")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GenericNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.GenericNotNull(value, valueName, message);
        }

        /// <summary>Проверка значения на значение по-умолчанию</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueNotEmpty<T>(T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct
        {
          return !Check.Optional.Enabled ? value : Check.ValueNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что значение не пусто (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue">Пустое значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueNotEmpty<T>(T value, T emptyValue, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct
        {
          return !Check.Optional.Enabled ? value : Check.ValueNotEmpty(value, emptyValue, valueName, message);
        }

        /// <summary>Проверка что значение не пусто (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue1">Пустое значение</param>
        /// <param name="emptyValue2">Пустое значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueNotEmpty<T>(
          T value,
          T emptyValue1,
          T emptyValue2,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
          where T : struct
        {
          return !Check.Optional.Enabled ? value : Check.ValueNotEmpty(value, emptyValue1, emptyValue2, valueName, message);
        }

        /// <summary>Проверка попадания значения в допустимый диапазон</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если значение выходит за рамки допустимого диапазона значений</exception>
        /// <param name="value">Значение, которое должно попадать в допустимый диапазон</param>
        /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition: false => halt; value:null => null")]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T InRange<T>([CanBeNull] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.InRange(value, condition, valueName, message);
        }

        /// <summary>Проверка попадания значения в допустимый диапазон</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если значение выходит за рамки допустимого диапазона значений</exception>
        /// <param name="value">Значение, которое должно попадать в допустимый диапазон</param>
        /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition: false => halt; value:null => null")]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T InRange<T>([CanBeNull] T value, [NotNull, InstantHandle] Func<bool> condition, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.InRange(value, condition, valueName, message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ZeroOrPositiveNumber]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexInRange(int index, int count, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? index : Check.IndexInRange(index, count, valueName, message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ZeroOrPositiveNumber]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long IndexInRange(long index, long count, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? index : Check.IndexInRange(index, count, valueName, message);
        }

        /// <summary>Проверка перечисление на отсутствие значений по-умолчанию</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если в перечислении присутствует значение == default(T)</exception>
        /// <param name="value">Перечисление значений, которые не должны быть равны значению по-умолчанию для своего типа</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ValuesNotEmpty<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
          where T : struct
        {
          return !Check.Optional.Enabled ? value : Check.ValuesNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка строки на null и на равенство string.Empty</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NotNullOrEmpty([CanBeNull] string value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.NotNullOrEmpty(value, valueName, message);
        }

        /// <summary>Проверка строки на null и на равенство string.Empty или состоять только из пробелов</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty, или состоять только из
        /// пробелов</param>
        /// <param name="valueName">(Optional) Наименование проверяемого параметра</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotWhitespace]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NotNullOrWhitespace([CanBeNull] string value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.NotNullOrWhitespace(value, valueName, message);
        }

        /// <summary>Проверка что объект не null и не DBNull</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект == null или DBNull</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class
        {
          return !Check.Optional.Enabled ? value : Check.NotNullNotDbNull(value, valueName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception><cref>TException</cref>Если условие не выполняется</exception>
        /// <param name="value">Объект, который должен быть не null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NotNull<T, TException>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
          where T : class
          where TException : NullReferenceException
        {
          return !Check.Optional.Enabled ? value : Check.NotNull<T, TException>(value, valueName, message);
        }

        /// <summary>Проверка что объект не null</summary>
        /// <exception><cref>TException</cref>Если условие не выполняется</exception>
        /// <param name="value">Объект, который должен быть не null</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NotNull<T, TException>([CanBeNull, NoEnumeration] T? value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
          where T : struct
          where TException : NullReferenceException
        {
          return !Check.Optional.Enabled ? value.Value : Check.NotNull<T, TException>(value, valueName, message);
        }

        /// <summary>Проверка что элементы последовательности не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ItemsNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
        {
          return !Check.Optional.Enabled ? value : Check.ItemsNotNull(value, valueName, message);
        }

        /// <summary>Проверка что элементы последовательности не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ItemsNotNull<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T?> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
          where T : struct
        {
          return !Check.Optional.Enabled ? value.Cast<T>() : Check.ItemsNotNull(value, valueName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CollectionNotEmpty<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, ICollection
        {
          return !Check.Optional.Enabled ? value : Check.CollectionNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyCollection<T> ReadOnlyCollectionNotEmpty<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IReadOnlyCollection<T> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ReadOnlyCollectionNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TCollection ReadOnlyCollectionNotEmpty<TCollection, T>(
          [CanBeNull, NoEnumeration] TCollection value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
          where TCollection : class, IReadOnlyCollection<T>
        {
          return !Check.Optional.Enabled ? value : Check.ReadOnlyCollectionNotEmpty<TCollection, T>(value, valueName, message);
        }

        /// <summary>Проверка что последовательность не пусто</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если последовательность пусто</exception>
        /// <param name="value">Последовательность, которая не должна быть пуста</param>
        /// <param name="valueName">Наименование последовательности</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EnumerationNotEmpty<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
        {
          return !Check.Optional.Enabled ? value : Check.EnumerationNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что все строки последовательности не null и не пусты</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <param name="value">Последовательность строк, которые быть не должны быть равны null или string.Empty</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [ItemNotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> StringsNotEmpty(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<string> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.StringsNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что все строки последовательности не null, не пустые строки и не строки состоящие из одних пробелов</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemWhitespaceNotAllowedException">Если в последовательности присутствуют строки не содержащие ничего
        /// кроме пробелов</exception>
        /// <param name="value">Последовательность строк, которые быть не должны быть равны null, string.Empty или заполнены
        /// одними только пробелами</param>
        /// <param name="valueName">(Optional) Наименование коллекции</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [ItemNotWhitespace]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> StringsNotWhitespace(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<string> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.StringsNotWhitespace(value, valueName, message);
        }

        /// <summary>Проверка что элементы коллекции не null и не DBNull</summary>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если коллекция содержит null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если коллекция содержит DBNull</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [ItemNotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ItemsNotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : class, IEnumerable
        {
          return !Check.Optional.Enabled ? value : Check.ItemsNotNullNotDbNull(value, valueName, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception cref="T:System.Exception">Если условие не выполняется</exception>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert(bool condition, [CanBeNull, NotEmpty, InvokerParameterName] string message = null)
        {
          if (!Check.Optional.Enabled)
            return;
          Check.Assert(condition, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception cref="T:System.Exception">Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt; value:null => null")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Assert<T>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull, NotEmpty, InvokerParameterName] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.Assert(value, condition, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="exceptionParams">Параметры, которые будут переданы в конструктор исключительной ситуации</param>
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert<TException>(bool condition, [NotNull, ItemCanBeNull] object[] exceptionParams) where TException : Exception
        {
          if (!Check.Optional.Enabled)
            return;
          Check.Assert<TException>(condition, exceptionParams);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="exceptionParams">Параметры, которые будут переданы в конструктор исключительной ситуации</param>
        [ContractAnnotation("condition:false => halt; value:null => null")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Assert<T, TException>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [NotNull, ItemCanBeNull] object[] exceptionParams)
          where TException : Exception
        {
          return !Check.Optional.Enabled ? value : Check.Assert<T, TException>(value, condition, exceptionParams);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert<TException>(bool condition, [CanBeNull] string message = null) where TException : Exception
        {
          if (!Check.Optional.Enabled)
            return;
          Check.Assert<TException>(condition, message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt; value:null => null")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Assert<T, TException>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull] string message = null) where TException : Exception
        {
          return !Check.Optional.Enabled ? value : Check.Assert<T, TException>(value, condition, message);
        }

        /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
        /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ItemsIs<T>([CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? Check.ConvertAll<T>(value) : Check.ItemsIs<T>(value, valueName, message);
        }

        /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
        /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="messageFactory">Метод-фабрика сообщений об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ItemsIs<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
        {
          return !Check.Optional.Enabled ? Check.ConvertAll<T>(value) : Check.ItemsIs<T>(value, valueName, messageFactory);
        }

        /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
        /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="messageFactory">Метод-фабрика сообщений об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ItemsIs<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable value,
          [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
        {
          return !Check.Optional.Enabled ? Check.ConvertAll<T>(value) : Check.ItemsIs<T>(value, (string) null, messageFactory);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="predicate">Условие</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> All<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.All(value, valueName, predicate, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> All<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.All(value, predicate, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="predicate">Условие</param>
        /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> All<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [NotNull] TemplateMessageFactory<T> messageFactory)
        {
          return !Check.Optional.Enabled ? value : Check.All(value, valueName, predicate, messageFactory);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> All<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [NotNull] TemplateMessageFactory<T> messageFactory)
        {
          return !Check.Optional.Enabled ? value : Check.All(value, (string) null, predicate, messageFactory);
        }

        /// <summary>Проверка, что перечисление не пусто</summary>
        /// <exception cref="T:System.NullReferenceException">Если перечисление равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если перечисление пусто</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnumerable NotNullNotEmpty<TEnumerable>(
          [CanBeNull, NoEnumeration] TEnumerable value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull, CanBeEmpty] string message = null)
          where TEnumerable : class, IEnumerable
        {
          return !Check.Optional.Enabled ? value : Check.NotNullNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка состояния объекта (значений полей/свойств)</summary>
        /// <exception cref="T:System.InvalidOperationException">Если condition == false</exception>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ObjectState(bool condition, [CanBeNull, InvokerParameterName] string message = null)
        {
          if (!Check.Optional.Enabled)
            return;
          Check.ObjectState(condition, message);
        }

        /// <summary>Проверка состояния объекта (значений полей/свойств)</summary>
        /// <exception cref="T:System.NullReferenceException">Если value == null</exception>
        /// <exception cref="T:System.InvalidOperationException">Если condition == false</exception>
        /// <param name="value">Значение, которое будет возвращено, если проверка будет пройдена</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt; value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ObjectState<T>(
          [CanBeNull, NoEnumeration] T value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull, InvokerParameterName] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ObjectState(value, valueName, condition, message);
        }

        /// <summary>Проверка состояния объекта (значений полей/свойств)</summary>
        /// <exception cref="T:System.NullReferenceException">Если value == null</exception>
        /// <exception cref="T:System.InvalidOperationException">Если condition == false</exception>
        /// <param name="value">Значение, которое будет возвращено, если проверка будет пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("condition:false => halt; value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ObjectState<T>([CanBeNull, NoEnumeration] T value, [NotNull, InstantHandle] Func<T, bool> condition, [CanBeNull, InvokerParameterName] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.ObjectState(value, (string) null, condition, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="type">Тип значения</param>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [Obsolete("Удалите передачу типа Enum, начиная c C# 7.2 он не требуется")]
        [Pure]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EnumInRange<T>([NotNull] Type type, T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct, Enum
        {
          return !Check.Optional.Enabled ? value : Check.EnumInRange(value, valueName, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EnumInRange<T>(T value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null) where T : struct, Enum
        {
          return !Check.Optional.Enabled ? value : Check.EnumInRange(value, valueName, message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="value">Значение</param>
        /// <param name="getExceptionFunc">Метод-конструктор исключительной ситуации</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EnumInRangeCustom<T>(
          T value,
          [NotNull] EnumInRangeCustomExceptionFactory<T> getExceptionFunc)
          where T : struct, Enum
        {
          return !Check.Optional.Enabled ? value : Check.EnumInRangeCustom(value, getExceptionFunc);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="values">Список значений</param>
        /// <param name="valueName">Наименование коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("values:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> AllEnumInRange<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> values,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          [CanBeNull] string message = null)
          where T : struct, Enum
        {
          return !Check.Optional.Enabled ? values : Check.AllEnumInRange(values, valueName, message);
        }

        /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <exception cref="T:System.InvalidCastException">Если тип переданного объекта не <see cref="!:T" /></exception>
        /// <param name="value">Проверяемый объект</param>
        /// <param name="valueName">Наименование переданного объекта</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Is<T>([CanBeNull, NoEnumeration] object value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? (T) value : Check.Is<T>(value, valueName, message);
        }

        /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <exception cref="T:System.InvalidCastException">Если тип переданного объекта не <see cref="!:T" /></exception>
        /// <param name="value">Проверяемый объект</param>
        /// <param name="valueName">Наименование переданного объекта</param>
        /// <param name="messageFactory">Внешняя ф-ия получения сообщения об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Is<T>(
          [CanBeNull, NoEnumeration] object value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName,
          [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
        {
          return !Check.Optional.Enabled ? (T) value : Check.Is<T>(value, valueName, messageFactory);
        }

        /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <exception cref="T:System.Exception">Если тип переданного объекта не <see cref="!:T" /></exception>
        /// <param name="value">Проверяемый объект</param>
        /// <param name="messageFactory">Внешняя ф-ия получения сообщения об ошибке</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Is<T>([CanBeNull, NoEnumeration] object value, [NotNull, InstantHandle] ObjectMessageFactory messageFactory)
        {
          return !Check.Optional.Enabled ? (T) value : Check.Is<T>(value, (string) null, messageFactory);
        }

        /// <summary>Проверка того, что файл по указанному пути существует на диске</summary>
        /// <exception cref="T:System.ArgumentNullException">Если указанный путь == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если указанный путь == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если указанный путь состоит только из пробелов</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">Если файл отсутствует на диске</exception>
        /// <param name="value">Путь к файлу</param>
        /// <param name="valueName">Наименование переданного значения</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <returns>Путь к файлу</returns>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotWhitespace]
        [FileExists]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FileExists([CanBeNull] string value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.FileExists(value, valueName, message);
        }

        /// <summary>Проверка того, что папка по указанному пути существует на диске</summary>
        /// <exception cref="T:System.ArgumentNullException">Если указанный путь == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если указанный путь == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если указанный путь состоит только из пробелов</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">Если папка отсутствует на диске</exception>
        /// <param name="value">Путь к папке</param>
        /// <param name="valueName">Наименование переданного значения</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <returns>Путь к папке</returns>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotWhitespace]
        [DirectoryExists]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string DirectoryExists([CanBeNull] string value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.DirectoryExists(value, valueName, message);
        }

        /// <summary>Проверка что стрим не равен null, что имеет ненулевую длину и текущая позиция не находится в конце стрима</summary>
        /// <exception cref="T:System.ArgumentNullException">Если переданный стрим == null</exception>
        /// <exception cref="M:Intermech.Diagnostics.Check.Optional.StreamNotEmpty(System.IO.Stream,System.String,System.String)">Если длина стрима равна 0</exception>
        /// <exception cref="T:System.IO.EndOfStreamException">Если позиция в преданном стриме находится в его конце</exception>
        /// <param name="value">Стрим</param>
        /// <param name="valueName">Наименование стрима</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <returns>Стрим</returns>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Stream StreamNotEmpty([CanBeNull] Stream value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.StreamNotEmpty(value, valueName, message);
        }

        /// <summary>Проверка что строка содержит корректный Uri</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка описывающая Uri == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка описывающая Uri == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка описывающая Uri состоит только из пробелов</exception>
        /// <exception cref="T:Intermech.Diagnostics.InvalidUriException">Если Uri некорректен</exception>
        /// <param name="value">Строка, содержащая Uri</param>
        /// <param name="valueName">(Optional) Наименование строки</param>
        /// <param name="scheme">(Optional) Схема Uri которой должен соответствовать адрес. Например UriScheme.Http для Http адреса. Если
        /// null - схема не проверяется</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <returns>Строка, содержащая Uri</returns>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotWhitespace]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string UriCorrect(
          [CanBeNull] string value,
          [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null,
          UriScheme scheme = UriScheme.Any,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.UriCorrect(value, valueName, scheme, message);
        }

        /// <summary>Проверка что в словаре присутствует запись с переданным ключом</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNotFoundException">Если ключ не найден</exception>
        /// <param name="dictionary">Словарь</param>
        /// <param name="key">Ключ, который должен присутствовать в словаре</param>
        /// <param name="message">Сообщение об ошибке</param>
        [ContractAnnotation("dictionary:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<TKey, TValue> ContainsKey<TKey, TValue>(
          [CanBeNull, NoEnumeration] IReadOnlyDictionary<TKey, TValue> dictionary,
          [NotNull, NoEnumeration] TKey key,
          [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? dictionary : Check.ContainsKey(dictionary, key, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long IsPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float IsPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double IsPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsZeroOrPositive(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long IsZeroOrPositive(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float IsZeroOrPositive(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double IsZeroOrPositive(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsZeroOrPositive(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsNegative(int value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long IsNegative(long value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float IsNegative(float value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsNegative(value, valueName, message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="valueName">Наименование проверяемого параметра</param>
        /// <param name="message">Сообщение об ошибке</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double IsNegative(double value, [CanBeNull, NotWhitespace, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
        {
          return !Check.Optional.Enabled ? value : Check.IsNegative(value, valueName, message);
        }

        /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
        /// Для контроля используется ссылка на объект, которая в Dispose устанавливается в null</summary>
        /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и notNullRef == null</exception>
        /// <param name="notNullRef">Контрольная ссылка, которая становится равной null после вызова Dispose</param>
        /// <param name="objectName">(Optional) Имя объекта</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("notNullRef:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotDisposed([CanBeNull] object notNullRef, [CanBeNull, NotWhitespace] string objectName = null, [CanBeNull] string message = null)
        {
          if (!Check.Optional.Enabled)
            return;
          Check.NotDisposed(notNullRef, objectName, message);
        }

        /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
        /// Для контроля используется ссылка на объект, которая в Dispose устанавливается в null</summary>
        /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и notNullRef == null</exception>
        /// <param name="notNullRef">Контрольная ссылка, которая становится равной null после вызова Dispose</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("notNullRef:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotDisposed<T>([CanBeNull] object notNullRef, [CanBeNull] string message = null) where T : IDisposable
        {
          if (!Check.Optional.Enabled)
            return;
          Check.NotDisposed<T>(notNullRef, message);
        }

        /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
        /// Для контроля флаг, который устанавливается в True в самом начале Dispose</summary>
        /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и disposedFlag == true</exception>
        /// <param name="disposedFlag">Флаг, который устанавливается в True в самом начале Dispose</param>
        /// <param name="objectName">(Optional) Имя объекта</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("disposedFlag:true => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotDisposed(bool disposedFlag, [CanBeNull, NotWhitespace] string objectName = null, [CanBeNull] string message = null)
        {
          if (!Check.Optional.Enabled)
            return;
          Check.NotDisposed(disposedFlag, objectName, message);
        }

        /// <summary>Проверка, что Dispose у объекта ещё не вызывался.
        /// Для контроля флаг, который устанавливается в True в самом начале Dispose</summary>
        /// <exception cref="T:System.ObjectDisposedException">Если Dispose уже был вызван и disposedFlag == true</exception>
        /// <param name="disposedFlag">Флаг, который устанавливается в True в самом начале Dispose</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        [ContractAnnotation("disposedFlag:true => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotDisposed<T>(bool disposedFlag, [CanBeNull] string message = null) where T : IDisposable
        {
          if (!Check.Optional.Enabled)
            return;
          Check.NotDisposed<T>(disposedFlag, message);
        }
      }

      /// <summary>Валидация условий для возвращаемых методами значений. В DEBUG билде проверки осуществляются, иначе - нет</summary>
      public abstract class Result
      {
        /// <summary>Производить ли проверку значений, возвращаемых методами</summary>
        public static bool Enabled;

        /// <summary>Проверка что строка, являющаяся результатом выполнения метода, содержит guid</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.FormatException">Если строка не содержит GUID</exception>
        /// <param name="guid">Строка, которая должна содержать Guid</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [NotNull]
        [GuidStr]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string IsGuid([CanBeNull] string guid, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? guid : Check.IsGuid(guid, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что строка, являющаяся результатом выполнения метода, не содержит непустой guid</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <exception cref="T:System.FormatException">Если строка не содержит GUID</exception>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если GUID пуст</exception>
        /// <param name="guid">Строка, которая должна содержать непустой Guid</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [NotNull]
        [NotEmptyGuid]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GuidNotEmpty([CanBeNull] string guid, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? guid : Check.ArgumentGuidNotEmpty(guid, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что guid являющийся результатом выполнения метода не пуст</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если guid == Guid.Empty</exception>
        /// <param name="guid">Guid, который не должен быть равен Guid.Empty</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid GuidNotEmpty(Guid guid, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? guid : Check.GuidNotEmpty(guid, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что результат выполнения метода не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если значение, присваиваемое свойству null</exception>
        /// <param name="value">Значение, которое пытаются присвоить свойству</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class
        {
          return !Check.Result.Enabled ? value : Check.NotNull(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что результат выполнения метода не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если значение, присваиваемое свойству null</exception>
        /// <param name="value">Значение, которое пытаются присвоить свойству</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода,
        /// результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NotNull<T>([CanBeNull, NoEnumeration] T? value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : struct
        {
          return !Check.Result.Enabled ? value.Value : Check.NotNull(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что результат выполнения метода не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если переданное значение == null</exception>
        /// <param name="value">Объект, который не должен быть равен null</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода,
        /// результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt; => NotNull")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GenericNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.GenericNotNull(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что результат выполнения метода не равен значению по-умолчанию для типа T</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueNotEmpty<T>(T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : struct
        {
          return !Check.Result.Enabled ? value : Check.ValueNotEmpty(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что значение не пусто (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue">Пустое значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueNotEmpty<T>(
          T value,
          T emptyValue,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct
        {
          return !Check.Result.Enabled ? value : Check.ValueNotEmpty(value, emptyValue, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что значение не пусто (пустое значение отличается от default)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == default(T)</exception>
        /// <param name="value">Значение, которое не должно быть равно значению по-умолчанию для своего типа</param>
        /// <param name="emptyValue1">Пустое значение</param>
        /// <param name="emptyValue2">Пустое значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueNotEmpty<T>(
          T value,
          T emptyValue1,
          T emptyValue2,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct
        {
          return !Check.Result.Enabled ? value : Check.ValueNotEmpty(value, emptyValue1, emptyValue2, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что значение IntPtr не пусто (пустое значение отличается от IntPtr.Zero)</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если аргумент == IntPtr.Zero</exception>
        /// <param name="value">Значение, которое не должно быть равно IntPtr.Zero</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr ValueNotEmpty(IntPtr value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.ValueNotEmpty(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка попадания значения в допустимый диапазон</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если значение выходит за рамки допустимого диапазона значений</exception>
        /// <param name="value">Значение, которое должно попадать в допустимый диапазон</param>
        /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("condition: false => halt; value:null => null")]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T InRange<T>(
          [CanBeNull] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.InRange(value, condition, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка попадания значения в допустимый диапазон</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если значение выходит за рамки допустимого диапазона значений</exception>
        /// <param name="value">Значение, которое должно попадать в допустимый диапазон</param>
        /// <param name="condition">Внешний метод проверки попадания значения в допустимый диапазон</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("condition: false => halt; value:null => null")]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T InRange<T>(
          [CanBeNull] T value,
          [NotNull, InstantHandle] Func<bool> condition,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.InRange(value, condition, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ZeroOrPositiveNumber]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexInRange(int index, int count, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? index : Check.IndexInRange(index, count, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что индекс не выходит за пределы коллекции</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если индекс выходит за пределы коллекции</exception>
        /// <param name="index">Значение индекса</param>
        /// <param name="count">Число элементов коллекции</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ZeroOrPositiveNumber]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long IndexInRange(
          long index,
          long count,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? index : Check.IndexInRange(index, count, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка перечисление на отсутствие значений по-умолчанию</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueEmptyException">Если в перечислении присутствует значение == default(T)</exception>
        /// <param name="value">Перечисление значений, которые не должны быть равны значению по-умолчанию для своего типа</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ValuesNotEmpty<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct
        {
          return !Check.Result.Enabled ? value : Check.ValuesNotEmpty(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка строки на null и на равенство string.Empty</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NotNullOrEmpty([CanBeNull] string value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.NotNullOrEmpty(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка строки на null и на равенство string.Empty или состоять только из пробелов</summary>
        /// <exception cref="T:System.NullReferenceException">Если строка == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.EmptyStringNotAllowedException">Если строка == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.WhitespaceNotAllowedException">Если строка состоит только из пробелов</exception>
        /// <param name="value">Строковый аргумент, который не должен быть равен null или string.Empty, или состоять только из пробелов</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotWhitespace]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NotNullOrWhitespace([CanBeNull] string value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.NotNullOrWhitespace(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что значение, присваиваемое свойству не null и не DBNull</summary>
        /// <exception cref="T:System.NullReferenceException">Если значение == null или DBNull</exception>
        /// <param name="value">Значение, которое пытаются присвоить свойству</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class
        {
          return !Check.Result.Enabled ? value : Check.NotNullNotDbNull(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что значение, присваиваемое свойству не null</summary>
        /// <exception><cref>TException</cref>Если условие не выполняется</exception>
        /// <param name="value">Значение, которое должно быть не null</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NotNull<T, TException>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
          where T : class
          where TException : NullReferenceException
        {
          return !Check.Result.Enabled ? value : Check.NotNull<T, TException>(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что значение, присваиваемое свойству не null</summary>
        /// <exception><cref>TException</cref>Если условие не выполняется</exception>
        /// <param name="value">Значение, которое должно быть не null</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NotNull<T, TException>([CanBeNull, NoEnumeration] T? value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
          where T : struct
          where TException : NullReferenceException
        {
          return !Check.Result.Enabled ? value.Value : Check.NotNull<T, TException>(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что элементы последовательности не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ItemsNotNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class, IEnumerable
        {
          return !Check.Result.Enabled ? value : Check.ItemsNotNull(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что элементы последовательности не null</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют элементы равные null</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ItemsNotNull<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T?> value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct
        {
          return !Check.Result.Enabled ? Check.ConvertAll<T>((IEnumerable) value) : Check.ItemsNotNull(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CollectionNotEmpty<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class, ICollection
        {
          return !Check.Result.Enabled ? value : Check.CollectionNotEmpty(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyCollection<T> ReadOnlyCollectionNotEmpty<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IReadOnlyCollection<T> value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.ReadOnlyCollectionNotEmpty(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что коллекция не пуста</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если коллекция пуста</exception>
        /// <param name="value">Коллекция, которая не должна быть пуста</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TCollection ReadOnlyCollectionNotEmpty<TCollection, T>(
          [CanBeNull, NoEnumeration] TCollection value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where TCollection : class, IReadOnlyCollection<T>
        {
          return !Check.Result.Enabled ? value : Check.ReadOnlyCollectionNotEmpty<TCollection, T>(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что последовательность не пусто</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если последовательность пусто</exception>
        /// <param name="value">Последовательность, которая не должна быть пуста</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EnumerationNotEmpty<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class, IEnumerable
        {
          return !Check.Result.Enabled ? value : Check.EnumerationNotEmpty(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что все строки последовательности не null и не пусты</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <param name="value">Последовательность строк, которые быть не должны быть равны null или string.Empty</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [ItemNotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> StringsNotEmpty(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<string> value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.StringsNotEmpty(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что все строки последовательности не null, не пустые строки и не строки состоящие из одних пробелов</summary>
        /// <exception cref="T:System.NullReferenceException">Если <see cref="!:value" /> == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если в последовательности присутствуют строки равные null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemEmptyStringNotAllowedException">Если в последовательности присутствуют пустые строки</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemWhitespaceNotAllowedException">Если в последовательности присутствуют строки не содержащие
        /// ничего кроме пробелов</exception>
        /// <param name="value">Последовательность строк, которые быть не должны быть равны null, string.Empty
        /// или заполнены одними только пробелами</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [ItemNotWhitespace]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> StringsNotWhitespace(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<string> value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.StringsNotWhitespace(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что элементы коллекции не null и не DBNull</summary>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если коллекция содержит null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemNullsNotAllowedException">Если коллекция содержит DBNull</exception>
        /// <param name="value">Коллекция, элементы которой должен быть не null</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [ItemNotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ItemsNotNullNotDbNull<T>([CanBeNull, NoEnumeration] T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : class, IEnumerable
        {
          return !Check.Result.Enabled ? value : Check.ItemsNotNullNotDbNull(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception cref="T:System.Exception">Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение, если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("condition:false => halt; value:null => null")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Assert<T>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.Assert(value, condition, string.IsNullOrEmpty(message) ? $"result of {callerMemberName} assertion" : message);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение, если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="exceptionParams">Параметры, которые будут переданы в конструктор исключительной ситуации</param>
        [ContractAnnotation("condition:false => halt; value:null => null")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Assert<T, TException>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [NotNull, ItemCanBeNull] object[] exceptionParams)
          where TException : Exception
        {
          return !Check.Result.Enabled ? value : Check.Assert<T, TException>(value, condition, exceptionParams);
        }

        /// <summary>Проверка условия</summary>
        /// <exception><cref>TException</cref>: Если условие не выполняется</exception>
        /// <param name="value">Возвращаемое значение, если проверка пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("condition:false => halt; value:null => null")]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Assert<T, TException>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where TException : Exception
        {
          return !Check.Result.Enabled ? value : Check.Assert<T, TException>(value, condition, string.IsNullOrEmpty(message) ? $"result of {callerMemberName} assertion" : message);
        }

        /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
        /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ItemsIs<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable value,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? Check.ConvertAll<T>(value) : Check.ItemsIs<T>(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что все элементы последовательности являются объектами нужного типа</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление равно null</exception>
        /// <exception cref="T:System.InvalidCastException">Если обнаружен элемент не являющийся объектом нужного типа</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="messageFactory">Метод-фабрика сообщений об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [ItemNotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ItemsIs<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable value,
          [NotNull, InstantHandle] ObjectMessageFactory messageFactory,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? Check.ConvertAll<T>(value) : Check.ItemsIs<T>(value, callerMemberName != null ? "result of " + callerMemberName : (string) null, messageFactory);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> All<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.All(value, callerMemberName != null ? "result of " + callerMemberName : (string) null, predicate, message);
        }

        /// <summary>Условие, которое должно выполняться для всех элементов перечисления</summary>
        /// <exception cref="T:System.ArgumentNullException">Если перечисление или условие проверки элемента равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ItemValidationExceptionException">Если для какого-нибудь элемента перечисления не выполнится
        /// переданное условие</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="predicate">Условие</param>
        /// <param name="messageFactory">Метод-конструктор сообщения об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> All<T>(
          [CanBeNull, ItemCanBeNull, NoEnumeration] IEnumerable<T> value,
          [NotNull, InstantHandle] Func<T, bool> predicate,
          [NotNull] TemplateMessageFactory<T> messageFactory,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.All(value, callerMemberName != null ? "result of " + callerMemberName : (string) null, predicate, messageFactory);
        }

        /// <summary>Проверка, что перечисление не пусто</summary>
        /// <exception cref="T:System.NullReferenceException">Если перечисление равно null</exception>
        /// <exception cref="T:Intermech.Diagnostics.CollectionIsEmptyException">Если перечисление пусто</exception>
        /// <param name="value">Коллекция</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnumerable NotNullNotEmpty<TEnumerable>(
          [CanBeNull, NoEnumeration] TEnumerable value,
          [CanBeNull, CanBeEmpty] string message = null,
          [CanBeNull] string callerMemberName = null)
          where TEnumerable : class, IEnumerable
        {
          return !Check.Result.Enabled ? value : Check.NotNullNotEmpty(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка состояния объекта (значений полей/свойств)</summary>
        /// <exception cref="T:System.NullReferenceException">Если value == null</exception>
        /// <exception cref="T:System.InvalidOperationException">Если condition == false</exception>
        /// <param name="value">Значение, которое будет возвращено, если проверка будет пройдена</param>
        /// <param name="condition">Условие, которое должно быть true</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("condition:false => halt; value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ObjectState<T>(
          [CanBeNull, NoEnumeration] T value,
          [NotNull, InstantHandle] Func<T, bool> condition,
          [CanBeNull, InvokerParameterName] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.ObjectState(value, callerMemberName != null ? "result of " + callerMemberName : (string) null, condition, string.IsNullOrWhiteSpace(message) ? $"result of {callerMemberName} invalid operation" : message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="type">Тип значения</param>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [Obsolete("Удалите передачу типа Enum, начиная c C# 7.2 он не требуется")]
        [Pure]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EnumInRange<T>([NotNull] Type type, T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : struct, Enum
        {
          return !Check.Result.Enabled ? value : Check.EnumInRange(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EnumInRange<T>(T value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null) where T : struct, Enum
        {
          return !Check.Result.Enabled ? value : Check.EnumInRange(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="value">Значение</param>
        /// <param name="getExceptionFunc">Метод-конструктор исключительной ситуации</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EnumInRangeCustom<T>(
          T value,
          [NotNull] EnumInRangeCustomExceptionFactory<T> getExceptionFunc)
          where T : struct, Enum
        {
          return !Check.Result.Enabled ? value : Check.EnumInRangeCustom(value, getExceptionFunc);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="type">Тип значения</param>
        /// <param name="values">Список значений</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [Obsolete("Удалите передачу типа Enum, начиная c C# 7.2 он не требуется")]
        [Pure]
        [ContractAnnotation("values:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> AllEnumInRange<T>(
          [NotNull] Type type,
          [CanBeNull, NoEnumeration] IEnumerable<T> values,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct, Enum
        {
          return !Check.Result.Enabled ? values : Check.AllEnumInRange(values, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение является допустимым для данного типа перечня (enum)</summary>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Если значение является недопустимым</exception>
        /// <param name="values">Список значений</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("values:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> AllEnumInRange<T>(
          [CanBeNull, NoEnumeration] IEnumerable<T> values,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
          where T : struct, Enum
        {
          return !Check.Result.Enabled ? values : Check.AllEnumInRange(values, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <exception cref="T:System.InvalidCastException">Если тип переданного объекта не <see cref="!:T" /></exception>
        /// <param name="value">Проверяемый объект</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Is<T>([CanBeNull, NoEnumeration] object value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? (T) value : Check.Is<T>(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка типа объекта, выбрасывает исключительную ситуацию если проверка не пройдена</summary>
        /// <exception cref="T:System.NullReferenceException">Если объект null</exception>
        /// <exception cref="T:System.InvalidCastException">Если тип переданного объекта не <see cref="!:T" /></exception>
        /// <param name="value">Проверяемый объект</param>
        /// <param name="messageFactory">Внешняя ф-ия получения сообщения об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Is<T>(
          [CanBeNull, NoEnumeration] object value,
          [NotNull, InstantHandle] ObjectMessageFactory messageFactory,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? (T) value : Check.Is<T>(value, callerMemberName != null ? "result of " + callerMemberName : (string) null, messageFactory);
        }

        /// <summary>Проверка того, что файл по указанному пути существует на диске</summary>
        /// <exception cref="T:System.ArgumentNullException">Если указанный путь == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если указанный путь == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если указанный путь состоит только из пробелов</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">Если файл отсутствует на диске</exception>
        /// <param name="value">Путь к файлу</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        /// <returns>Путь к файлу</returns>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [FileExists]
        [NotWhitespace]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FileExists([CanBeNull] string value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.FileExists(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что папка по указанному пути существует на диске</summary>
        /// <exception cref="T:System.ArgumentNullException">Если указанный путь == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если указанный путь == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если указанный путь состоит только из пробелов</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">Если папка отсутствует на диске</exception>
        /// <param name="value">Путь к папке</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        /// <returns>Путь к папке</returns>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [DirectoryExists]
        [NotWhitespace]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string DirectoryExists([CanBeNull] string value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.DirectoryExists(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что стрим не равен null, что имеет ненулевую длину и текущая позиция не находится в конце стрима</summary>
        /// <exception cref="T:System.ArgumentNullException">Если переданный стрим == null</exception>
        /// <exception cref="M:Intermech.Diagnostics.Check.Result.StreamNotEmpty(System.IO.Stream,System.String,System.String)">Если длина стрима равна 0</exception>
        /// <exception cref="T:System.IO.EndOfStreamException">Если позиция в преданном стриме находится в его конце</exception>
        /// <param name="value">Стрим</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование метода, результат работы которого контролируется</param>
        /// <returns>Стрим</returns>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotEmpty]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Stream StreamNotEmpty([CanBeNull] Stream value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.StreamNotEmpty(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка что строка содержит корректный Uri</summary>
        /// <exception cref="T:System.ArgumentNullException">Если строка описывающая Uri == null</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException">Если строка описывающая Uri == string.Empty</exception>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentWhitespaceNotAllowedException">Если строка описывающая Uri состоит только из пробелов</exception>
        /// <exception cref="T:Intermech.Diagnostics.InvalidUriException">Если Uri некорректен</exception>
        /// <param name="value">Строка, содержащая Uri</param>
        /// <param name="scheme">(Optional) Схема Uri которой должен соответствовать адрес. Например UriScheme.Http для Http
        /// адреса. Если null - схема не проверяется</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) Наименование строки</param>
        /// <returns>Строка, содержащая Uri</returns>
        [ContractAnnotation("value:null => halt")]
        [NotNull]
        [NotWhitespace]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string UriCorrect(
          [CanBeNull] string value,
          UriScheme scheme = UriScheme.Any,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.UriCorrect(value, callerMemberName != null ? "result of " + callerMemberName : (string) null, scheme, message);
        }

        /// <summary>Проверка что в словаре присутствует запись с переданным ключом</summary>
        /// <exception cref="T:Intermech.Diagnostics.ArgumentItemNotFoundException">Если ключ не найден</exception>
        /// <param name="dictionary">Словарь</param>
        /// <param name="key">Ключ, который должен присутствовать в словаре</param>
        /// <param name="message">(Optional) Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Optional) (Заполняется компилятором) Наименование метода, результат работы которого
        /// контролируется</param>
        [ContractAnnotation("dictionary:null => halt")]
        [NotNull]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<TKey, TValue> ContainsKey<TKey, TValue>(
          [CanBeNull, NoEnumeration] IReadOnlyDictionary<TKey, TValue> dictionary,
          [NotNull, NoEnumeration] TKey key,
          [CanBeNull] string message = null,
          [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? dictionary : Check.ContainsKey(dictionary, key, message ?? (callerMemberName != null ? $"result of {callerMemberName} not contains key {key}" : (string) null));
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsPositive(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsPositive(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long IsPositive(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsPositive(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float IsPositive(float value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsPositive(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double IsPositive(double value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsPositive(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsZeroOrPositive(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsZeroOrPositive(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long IsZeroOrPositive(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsZeroOrPositive(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float IsZeroOrPositive(float value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsZeroOrPositive(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение равно или больше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение меньше нуля</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double IsZeroOrPositive(double value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsZeroOrPositive(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsNegative(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsNegative(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long IsNegative(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsNegative(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float IsNegative(float value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsNegative(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }

        /// <summary>Проверка того, что значение меньше нуля</summary>
        /// <exception cref="T:Intermech.Diagnostics.ValueOutOfRangeException">Если переданное значение больше или равно нулю</exception>
        /// <param name="value">Значение</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="callerMemberName">(Заполняется компилятором) Наименование свойства, чьё значение изменяется</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double IsNegative(double value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
        {
          return !Check.Result.Enabled ? value : Check.IsNegative(value, callerMemberName != null ? "result of " + callerMemberName : "result", message);
        }
      }
    }
}
