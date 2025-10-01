namespace Domain.Interfaces
{
    public interface ISoftDeleteAudited
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
