using System;

namespace APIorm.Exceptions
{
    public class ApiException : Exception
    {
        public enum ApiExceptionReason
        {
            CONNECTION_NOT_COMPLETED,
            DISCONNECTION_NOT_COMPLETED,
            XML_COULD_NOT_BE_READ,
            XML_COULD_NOT_BE_PARSED,
            INVALID_METER_TYPE,
            JSON_NOT_FOUND,
            WRONG_DLMS_STRUCTURE,
            INVALID_METER_RESPONSE,

            PRODUTO_NAO_ENCONTRADO,
            COMPRA_NAO_ENCONTRADA,

            CODIGO_INVALIDO,
            DB_CONNECTION_NOT_COMPLETED,
            DB_DISCONNECTION_NOT_COMPLETED,
        }

        private readonly ApiExceptionReason[] reason;

        public ApiException(ApiExceptionReason reason)
        {
            this.reason = new ApiExceptionReason[] { reason };
        }

        public ApiException(ApiExceptionReason[] reasons)
        {
            this.reason = reasons;
        }

        public ApiException(ApiExceptionReason reason, string message) : base(message)
        {
            this.reason = new ApiExceptionReason[] { reason };
        }

        public ApiException(ApiExceptionReason[] reasons, string message) : base(message)
        {
            this.reason = reasons;
        }

        /// <summary>
        /// Retrieves the reason of the exception </summary>
        /// <returns> A MALExceptionReason </returns>
        public ApiExceptionReason[] getReason()
        {
            return reason;
        }

    }
}