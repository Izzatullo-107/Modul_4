using Microsoft.AspNetCore.Mvc;
using Question.Api.DTOs;
using Question.Api.Services;

namespace Question.Api.Controllers;

[Route("api/question")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService questionService;

    public QuestionController()
    {
        questionService = new QuestionService();
    }

    [HttpPost]
    public Guid Add(QuestionCreatDto questionCreatDto)
    {
        return questionService.Add(questionCreatDto);
    }

    [HttpGet]
    public List<QuestionGetDto> GetAlls()
    {
        return questionService.GetAllQuestions();
    }

    [HttpPut("questionId")] 
    public bool Update(Guid questionId, QuestionUpdateDto questionNew)
    {
        return questionService.UpdateQuestion(questionId, questionNew);
    }

    [HttpDelete("delete")]  
    public void Delete(Guid id)
    {
        questionService.DeleteQuestion(id);
    }

    [HttpPost("solve")]  
    public (bool,string) Solve(Guid questionId, string answer)
    {
         
        return questionService.SolveQuestion(questionId, answer);
    }

    [HttpGet("random")]  
    public QuestionGetDto GetRandom()
    {
        return questionService.GetRandomQuestion();
    }

    [HttpGet("count")]  
    public int GetCount()
    {
        return questionService.GetQuestionCount();
    }
}