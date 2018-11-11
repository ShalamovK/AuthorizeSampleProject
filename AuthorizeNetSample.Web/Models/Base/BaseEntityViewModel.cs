using System;

namespace AuthorizeNetSample.Web.Models.Base {
    public class BaseEntityViewModel<T> where T : IEquatable<T> {
        public T Id { get; set; }
    }
}