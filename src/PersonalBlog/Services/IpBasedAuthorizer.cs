namespace PersonalBlog.Services
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using PersonalBlog.Interfaces;

    public class IpBasedAuthorizer : IAuthorizer
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IConfiguration configuration;

        public IpBasedAuthorizer(IHttpContextAccessor accessor, IConfiguration configuration)
        {
            this.accessor = accessor;
            this.configuration = configuration;
        }

        public bool IsAuthorized()
        {
            var ipV4 = this.accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            var validIp = this.configuration.GetSection("Security").GetValue<string>("ValidIp");

            return string.Compare(ipV4, validIp) == 0;
        }
    }
}
