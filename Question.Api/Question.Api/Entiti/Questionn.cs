namespace Question.Api.Entiti
{
    public class Questionn
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public string VariantA { get; set; }
        public string VariantB { get; set; }
        public string VariantC { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
