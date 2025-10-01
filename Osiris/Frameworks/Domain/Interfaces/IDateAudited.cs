namespace Domain.Interfaces
{
    public interface IDateAudited
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
