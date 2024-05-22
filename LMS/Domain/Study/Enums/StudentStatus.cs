namespace LMS.Domain.Study.Enums
{
    public enum StudentStatus
    {
        Active,
        Withdrawn,
        Graduated,
        OnLeave,
        // сделан для потоков, когда родители оплатили курс до начала курса
        Waiting
    }
}
