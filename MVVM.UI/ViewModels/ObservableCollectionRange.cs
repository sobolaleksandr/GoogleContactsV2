namespace MVVM.UI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    /// <summary>
    /// <see cref="ObservableCollection{T}"/> с функций <see cref="AddRange"/>.
    /// </summary>
    public sealed class ObservableCollectionRange<T> : ObservableCollection<T>
    {
        /// <summary>
        /// Добавить коллекцию элементов, предварительно очистив коллекцию.
        /// </summary>
        /// <param name="items"> Коллекция элементов. </param>
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            CheckReentrancy();
            Items.Clear();
            foreach (var data in items)
            {
                Items.Add(data);
            }

            var arguments = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(arguments);
        }
    }
}