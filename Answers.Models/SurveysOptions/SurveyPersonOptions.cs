namespace Answers.Models.SurveysOptions;

public sealed class SurveyPersonOptions
{
    public Guid SurveyOptionsId { get; set; }

    public Guid SurveyId { get; set; }

    public List<PersonalityOption> Options { get; set; }
}