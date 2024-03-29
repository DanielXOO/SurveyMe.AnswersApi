﻿using Answers.Models.Answers;
using Answers.Models.Questions;

namespace Answers.Models.Surveys;

public class Survey
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public ICollection<Question> Questions { get; set; }

    public ICollection<SurveyAnswer> Answers { get; set; }

    public Guid SurveyOptionId { get; set; }
}