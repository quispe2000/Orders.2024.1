using System.Net;

namespace Orders.Frontend.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error) 
            { 
                return null;
            }

            var statusCode = HttpResponseMessage.StatusCode;
            if (statusCode == HttpStatusCode.NotFound)

            {
                return "Recurso no encontrado";
            }
            if (statusCode == HttpStatusCode.BadRequest)

            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            if (statusCode == HttpStatusCode.Unauthorized)

            {
                return "tienes qu estar logueado para ejecutar esta opcion";
            }
            if (statusCode == HttpStatusCode.Forbidden)

            {
                return "No tienes perniso para hacer esta operacion";
            }

            return "Ha ocurido un error inesperado";
        }
    }
}
