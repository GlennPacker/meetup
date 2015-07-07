namespace MeetUp.Services.ApiModels
{
    public class ApiRSVP
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Guests { get; set; }
        public virtual ApiUserAccount Going { get; set; }
    }
}