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

        public GroupViewModel(IContact contact) : base(contact)
        {
            if (contact == null)
            {
                CreateContact();
                return;
            }

            SetProperties(contact);
        }

        private void SetProperties(IContact contact)
        {
            if (!(contact is IGroup group))
                return;

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

        public int MemberCount { get; private set; }

        public override void ApplyFrom(IContact contact, Operation operation)
        {
            base.ApplyFrom(contact, operation);
            if (operation == Operation.Create)
            {
                CreateContact();
                return;
            }

            SetProperties(contact);
        }

        private void CreateContact()
        {
            FormattedName = string.Empty;
            ResourceName = string.Empty;
            ETag = string.Empty;
        }
    }
}