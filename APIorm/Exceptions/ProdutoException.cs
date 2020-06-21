using System;

namespace APIorm.Exceptions
{
    public class ProdutoException : Exception
    {
        public enum ProdutoExceptionReason
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
            CODIGO_INVALIDO,
            DB_CONNECTION_NOT_COMPLETED,
            DB_DISCONNECTION_NOT_COMPLETED,
        }

        private readonly ProdutoExceptionReason[] reason;

        public ProdutoException(ProdutoExceptionReason reason)
        {
            this.reason = new ProdutoExceptionReason[] { reason };
        }

        public ProdutoException(ProdutoExceptionReason[] reasons)
        {
            this.reason = reasons;
        }

        public ProdutoException(ProdutoExceptionReason reason, string message) : base(message)
        {
            this.reason = new ProdutoExceptionReason[] { reason };
        }

        public ProdutoException(ProdutoExceptionReason[] reasons, string message) : base(message)
        {
            this.reason = reasons;
        }

        /// <summary>
        /// Retrieves the reason of the exception </summary>
        /// <returns> A MALExceptionReason </returns>
        public ProdutoExceptionReason[] getReason()
        {
            return reason;
        }

    }
}