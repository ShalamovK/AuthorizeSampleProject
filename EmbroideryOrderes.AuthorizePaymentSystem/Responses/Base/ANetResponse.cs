namespace EmbroideryOrderes.AuthorizePaymentSystem.Responses.Base {
    public class ANetResponse {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }

    public class ANetResponse<T> : ANetResponse {
        public T ResponseObject { get; set; }
    }
}
