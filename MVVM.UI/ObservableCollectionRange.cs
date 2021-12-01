namespace MVVM.UI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public sealed class ObservableCollectionRange<T> : ObservableCollection<T>
    {
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            AddRange(items, false);
        }

        public void AddRange(IEnumerable<T> items, bool clearBeforeAdd)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            CheckReentrancy();
            if (clearBeforeAdd)
                Items.Clear();
            foreach (var data in items)
            {
                Items.Add(data);
            }

            var arguments = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(arguments);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            CheckReentrancy();
            foreach (var data in items)
            {
                Items.Remove(data);
            }

            var arguments = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(arguments);
        }
    }
}