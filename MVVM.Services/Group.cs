namespace MVVM.Services
{
    using System;

    using Google.Apis.PeopleService.v1.Data;

    using MVVM.Models;

    /// <summary>
    /// Модель группы.
    /// </summary>
    public class Group : Contact, IGroup
    {
        /// <summary>
        /// Модель группы.
        /// </summary>
        /// <param name="group"> Группа. </param>
        /// <param name="error"> Ошибка. </param>
        public Group(ContactGroup group, string error = "") : base(error)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            ResourceName = group.ResourceName ?? string.Empty;
            ETag = group.ETag ?? string.Empty;
            MemberCount = group.MemberCount ?? 0;
            FormattedName = group.FormattedName ?? string.Empty;
        }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string FormattedName { get; }

        /// <summary>
        /// Количество членов группы.
        /// </summary>
        public int MemberCount { get; }
    }
}