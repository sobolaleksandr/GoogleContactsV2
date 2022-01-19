namespace MVVM.UI.ViewModels
{
    using System.ComponentModel;

    using MVVM.Models;

    public sealed class GroupViewModel : ContactViewModel, IGroup, IDataErrorInfo
    {
        /// <summary>
        /// Поле свойства <see cref="FormattedName"/>
        /// </summary>
        private string _formattedName;

        public GroupViewModel(IContact contact) : base(contact)
        {
            if (contact != null)
                SetProperties(contact);

            IsChanged = false;
        }

        /// <summary>
        /// Наименование атрибута <see cref="FormattedName"/>
        /// </summary>
        public static string FormattedNameTitle => "Имя";

        public override string Error => this[nameof(FormattedName)];

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;

                switch (columnName)
                {
                    case nameof(FormattedName):
                        if (string.IsNullOrEmpty(FormattedName))
                            error = $"Поле {FormattedNameTitle} не должно быть пустым!";
                        break;
                }

                return error;
            }
        }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string FormattedName
        {
            get => _formattedName;
            set
            {
                _formattedName = value;
                OnPropertyChanged();
            }
        }

        public int MemberCount { get; private set; }

        protected override void SetProperties(IContact contact)
        {
            if (!(contact is IGroup group))
                return;

            FormattedName = group.FormattedName;
            MemberCount = group.MemberCount;
        }
    }
}