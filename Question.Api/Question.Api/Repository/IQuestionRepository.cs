using Question.Api.Entiti;

namespace Question.Api.Repository
{
    public interface IQuestionRepository
    {
        public List<Questionn>? GetAll();
        public void SaveAll(List<Questionn> question);
    }
}
