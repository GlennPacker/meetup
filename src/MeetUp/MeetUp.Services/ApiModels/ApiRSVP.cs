namespace MeetUp.Services.ApiModels
{
    public class ApiRSVP
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual ApiUserAccount Going { get; set; }
    }
}