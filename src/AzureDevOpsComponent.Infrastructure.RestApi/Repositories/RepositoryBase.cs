using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Exceptions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Withywoods.Serialization.Json;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Repositories
{
    public abstract class RepositoryBase
    {
        #region Protected properties & constructors

        protected IAzureDevOpsRestApiConfiguration Configuration { get; private set; }

        protected ILogger Logger { get; private set; }

        protected IHttpClientFactory HttpClientFactory { get; private set; }

        protected IMapper Mapper { get; private set; }

        protected RepositoryBase(IAzureDevOpsRestApiConfiguration configuration, ILogger logger, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            Configuration = configuration;
            Logger = logger;
            HttpClientFactory = httpClientFactory;
            Mapper = mapper;
        }

        #endregion

        #region Abstract properties

        protected abstract string ResourceName { get; }

        #endregion

        #region Protected methods

        protected string GenerateUrl(string prefix = "", string arguments = "")
        {
            return $"{Configuration.BaseUrl}{prefix}/{ResourceName}?api-version={Configuration.ApiVersion}{arguments}";
        }

        protected virtual async Task<T> GetAsync<T>(string url) where T : class
        {
            var client = HttpClientFactory.CreateClient(Configuration.HttpClientName);

            var response = await client.GetAsync(url);

            var stringResult = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(stringResult))
            {
                throw new ConnectivityException($"Empty response received while calling {url}");
            }

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogDebug($"Status code doesn't indicate success [HttpRequestUrl={url}] [HttpResponseContent={stringResult}]");
                response.EnsureSuccessStatusCode();
            }

            try
            {
                return stringResult.FromJson<T>();
            }
            catch (Exception exc)
            {
                Logger.LogWarning($"Cannot deserialize GET call response content [HttpRequestUrl={url}] [HttpResponseContent={stringResult}] [SerializationType={typeof(T).ToString()}] [ExceptionMessage={exc.Message}]");
                Logger.LogDebug($"[Stacktrace={exc.StackTrace}]");
                throw new ConnectivityException($"Invalid data received when calling \"{url}\". {exc.Message}.", exc);
            }
        }

        protected virtual async Task<T> PostAsync<T>(string url, object body, string httpClientName = null) where T : class
        {
            var client = HttpClientFactory.CreateClient(Configuration.HttpClientName);

            var response = await client.PostAsync(url, new StringContent(body.ToJson(), Encoding.UTF8, "application/json"));

            var stringResult = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(stringResult))
            {
                throw new Exception($"Empty response received while calling {url}");
            }

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogDebug($"Status code doesn't indicate success [HttpRequestUrl={url}] [HttpStatusCode={response.StatusCode}] [HttpResponseContent={stringResult}]");
                response.EnsureSuccessStatusCode();
            }

            try
            {
                return stringResult.FromJson<T>();
            }
            catch (Exception exc)
            {
                Logger.LogWarning($"Cannot deserialize POST call response content [HttpRequestUrl={url}] [HttpResponseContent={stringResult}] [SerializationType={typeof(T).ToString()}] [ExceptionMessage={exc.Message}]");
                Logger.LogDebug($"[Stacktrace={exc.StackTrace}]");
                throw new Exception($"Invalid data received when calling \"{url}\". {exc.Message}.", exc);
            }
        }

        #endregion
    }
}
