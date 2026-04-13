using Question.Api.DTOs;

namespace Question.Api.Services
{
    public interface IQuestionService
    {

        public Guid Add(QuestionCreatDto questionCreatDto);
        public List<QuestionGetDto> GetAllQuestions();
        public bool UpdateQuestion(Guid questionId, QuestionUpdateDto questionNew);
        public bool DeleteQuestion(Guid questionId);
        public (bool, string) SolveQuestion(Guid questionId, string answer);
        public QuestionGetDto GetRandomQuestion();
        public int GetQuestionCount();

    }
}
