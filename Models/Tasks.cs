namespace trilha_net_api_desafio.Models
{
    public class Tasks {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public EnumTaskStatus Status { get; set; }
    }
}