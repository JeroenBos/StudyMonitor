using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JBSnorro
{
	public abstract class DefaultINotifyPropertyChanged : INotifyPropertyChanged
	{
		/// <summary> Indicates whether property changed notifications should be invoked immediately or when called upon. </summary>
		private bool postponeInvocation;
		/// <summary> A list of property event args whose associated invocations were postponed but for which listeners should have been notified. </summary>
		private readonly List<PropertyChangedEventArgs> postponedInvocationEventArgs = new List<PropertyChangedEventArgs>();

		private event PropertyChangedEventHandler propertyChanged;
		/// <summary> Note that the provided event args is of typeo PropertyMutatedEventArgs&lt;TProperty&gt; for obtaining the old value </summary>
		public event PropertyChangedEventHandler PropertyChanged
		{
			add { propertyChanged += value; }
			remove { propertyChanged -= value; }
		}

		/// <summary> Triggers the PropertyChanged event if the new value different from the current value. </summary>
		/// <param name="field"> A reference to the current value. </param>
		/// <param name="value"> The new value of the property. </param>
		/// <param name="propertyName"> The name of the property. </param>
		protected void Set<TParameter>(ref TParameter field, TParameter value, [CallerMemberName] string propertyName = null)
		{
			TParameter oldValue = field;
			if (!EqualityComparer<TParameter>.Default.Equals(oldValue, value))
			{
				field = value;
				var e = new PropertyMutatedEventArgs<TParameter>(propertyName, oldValue, value);
				OnPropertyChanged(e);
			}
		}
		protected void OnPropertyChanged<TParameter>(PropertyMutatedEventArgs<TParameter> e)
		{
			if (postponeInvocation)
			{
				postponedInvocationEventArgs.Add(e);
			}
			else
			{
				Invoke(e);
			}
		}
		private void Invoke(PropertyChangedEventArgs e)
		{
			propertyChanged?.Invoke(this, e);
		}
		/// <summary> Invokes the property changed event handler for the change of a read only property. So this method can only be called from a constructor 
		/// and it is assumed that the property is set only once in that constructor, i.e. that it had the value default(<typeparamref name="TProperty"/>).
		/// This does not actually set the value, and in fact, the value should be set before calling this method. </summary>
		/// <param name="value"> The value of the property. </param>
		/// <param name="propertyName"> The name of the property. </param>
		protected void ReadOnlyPropertySet<TProperty>(TProperty value, string propertyName)
		{
			TProperty initialValue = default(TProperty);
			Set(ref initialValue, value, propertyName);
		}

		/// <summary> Gets an object which removes the postponement invoked by calling this method upon its diposal. </summary>
		protected IDisposable PostponePropertyChangedInvocationsDuring()
		{
			return new DefaultINotifyPropertyChangedPostponementObject(this);
		}
		internal protected void PostponePropertyChangedInvocation()
		{
			postponeInvocation = true;
		}
		internal protected void RemovePostponement()
		{
			Contract.Assert(postponeInvocation || postponedInvocationEventArgs.Count == 0);

			// still postpones all notifications added due to invoking earlier notifications, and handles them once the earlier ones have been handled
			for (int i = 0; i < postponedInvocationEventArgs.Count; i++)
			{
				Invoke(postponedInvocationEventArgs[i]);
			}
			postponeInvocation = false;
			postponedInvocationEventArgs.Clear();
		}
	}
}
