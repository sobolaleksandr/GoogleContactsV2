namespace MVVM.Models
{
    /// <summary>
    /// Интерфейс группы.
    /// </summary>
    public interface IGroup : IContact
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        string FormattedName { get; }

        /// <summary>
        /// Количество членов группы.
        /// </summary>
        int MemberCount { get; }
    }
}