namespace HealthTrackerMVVM.Utils
{
    using System.Net.Http;

    public class ApiClient
    {
        public static readonly HttpClient Client = new HttpClient();
    }
}
