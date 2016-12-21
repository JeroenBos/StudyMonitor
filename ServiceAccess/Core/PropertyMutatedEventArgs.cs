using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JBSnorro
{
	/// <summary> Allows for obtaining the properties of a PropertyMutatedEventArgs&lt;T&gt; without knowing T compile-time. </summary>
	public interface IPropertyMutatedEventArgs
	{
		object OldValue { get; }
		object NewValue { get; }
	}
	/// <summary> A property changed event argument that also holds the old and new values of the mutated property. </summary>
	/// <see cref="http://stackoverflow.com/questions/7677854/notifypropertychanged-event-where-event-args-contain-the-old-value"/>
	public class PropertyMutatedEventArgs<T> : PropertyChangedEventArgs, IPropertyMutatedEventArgs
	{
		public virtual T OldValue { get; }
		public virtual T NewValue { get; }

		object IPropertyMutatedEventArgs.OldValue => OldValue;
		object IPropertyMutatedEventArgs.NewValue => NewValue;

		public PropertyMutatedEventArgs(string propertyName, T oldValue, T newValue)
			: base(propertyName)
		{
			OldValue = oldValue;
			NewValue = newValue;
		}
	}

	public static class PropertyMutatedEventArgsExtensions
	{
		/// <summary> Creates an instance of PropertyMutatedEventArgs&lt;T&gt; without knowing T at compile time. </summary>
		public static PropertyChangedEventArgs Create(string propertyName, Type genericParameterType, object oldValue, object newValue)
		{
			Contract.Requires(!string.IsNullOrEmpty(propertyName));
			Contract.Requires(genericParameterType != null);
			Contract.Requires(ReferenceEquals(oldValue, null) || genericParameterType.IsInstanceOfType(oldValue));
			Contract.Requires(ReferenceEquals(newValue, null) || genericParameterType.IsInstanceOfType(newValue));

			oldValue = oldValue ?? genericParameterType.GetDefault();
			newValue = newValue ?? genericParameterType.GetDefault();

			var result = typeof(PropertyMutatedEventArgs<>).MakeGenericType(genericParameterType)
														   .GetConstructor(new Type[] { typeof(string), genericParameterType, genericParameterType })
														   .Invoke(new[] { propertyName, oldValue, newValue });

			return (PropertyChangedEventArgs)result;
		}
	}
}
