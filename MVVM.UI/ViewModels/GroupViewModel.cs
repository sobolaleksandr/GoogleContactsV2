namespace MVVM.UI.ViewModels
{
    using System.ComponentModel;

    using MVVM.Models;

    /// <summary>
    /// Вью модель группы.
    /// </summary>
    public sealed class GroupViewModel : ContactViewModel, IGroup, IDataErrorInfo
    {
        /// <summary>
        /// Поле свойства <see cref="FormattedName"/>
        /// </summary>
        private string _formattedName;

        /// <summary>
        /// Вью модель группы.
        /// </summary>
        /// <param name="contact"> Контакт. </param>
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

        /// <inheritdoc />
        public string Error => this[nameof(FormattedName)];

        /// <inheritdoc />
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

        /// <inheritdoc />
        public string FormattedName
        {
            get => _formattedName;
            set
            {
                _formattedName = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public int MemberCount { get; private set; }

        /// <inheritdoc />
        protected override void SetProperties(IContact contact)
        {
            if (!(contact is IGroup group))
                return;

            FormattedName = group.FormattedName;
            MemberCount = group.MemberCount;
        }
    }
}