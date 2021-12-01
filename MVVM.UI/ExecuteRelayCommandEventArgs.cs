namespace MVVM.UI
{
    using System;

    public sealed class ExecuteRelayCommandEventArgs : EventArgs
    {
        public ExecuteRelayCommandEventArgs(object parameter)
        {
            Parameter = parameter;
        }

        public object Parameter { get; }
    }
}