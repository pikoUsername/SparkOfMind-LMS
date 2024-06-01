namespace LMS.Domain.Study.Services
{
    public static class InvitationService
    {
        public static string CreateLink(Guid invitationId)
        {
            return System.Net.Dns.GetHostName() + "/invitation/accept/" + invitationId.ToString();
        }
    }
}
