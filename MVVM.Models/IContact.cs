﻿namespace MVVM.Models
{
    public interface IContact
    {
        /// <summary>
        /// Идентификатор модели.
        /// </summary>
        string ETag { get; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        string ResourceName { get; }

        public Operation Operation { get; }
    }
}