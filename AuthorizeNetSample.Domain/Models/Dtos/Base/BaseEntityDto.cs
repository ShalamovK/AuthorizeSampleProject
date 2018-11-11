using System;

namespace AuthorizeNetSample.Domain.Models.Dtos.Base {
    public class BaseEntityDto<T> where T : IEquatable<T> {
        public T Id { get; set; }
    }
}
