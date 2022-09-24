namespace TodoApp.Application.Dto
{
    public class GetTodoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime Time { get; set; }
    }
}
