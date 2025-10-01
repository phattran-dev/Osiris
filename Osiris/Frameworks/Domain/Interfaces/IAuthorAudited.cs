namespace Domain.Interfaces
{
    public interface IAuthorAudited<T>
    {
        public T CreatedBy { get; set; }
        public T UpdatedBy { get; set; }
    }
}
