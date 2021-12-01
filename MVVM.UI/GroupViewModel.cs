namespace MVVM.UI
{
    using System.ComponentModel;

    using MVVM.Models;

    public class GroupViewModel : ContactViewModel, IGroup, IDataErrorInfo
    {
        /// <summary>
        /// Поле свойства <see cref="FormattedName"/>
        /// </summary>
        private string _formattedName;

        private int _memberCount;

        public GroupViewModel(IContact contact) : base(contact)
        {
            if (!(contact is IGroup group))
                return;

            ApplyCommand = new ApplyCommand(contact);
            FormattedName = group.FormattedName;
            MemberCount = group.MemberCount;
        }

        /// <summary>
        /// Наименование атрибута <see cref="FormattedName"/>
        /// </summary>
        public static string FormattedNameTitle => "Имя";

        public string Error => this[nameof(FormattedName)];

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

        public override void ApplyFrom(IContact contact)
        {
            if (!(contact is GroupViewModel group))
                return;

            FormattedName = group.FormattedName;
        }

        public int MemberCount
        {
            get => _memberCount;
            set
            {
                _memberCount = value;
                OnPropertyChanged();
            }
        }
    }
}