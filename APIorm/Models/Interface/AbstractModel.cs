using System.Collections.Generic;

namespace APIorm.Models.Interface
{
    public struct ResponseCluster<T>
    {
        public T objValue;
        public IEnumerable<Erro> erros;
        public int totalItemCount;

        public ResponseCluster(T objValue, IEnumerable<Erro> erros, int totalItemCount)
        {
            this.objValue = objValue;
            this.erros = erros;
            this.totalItemCount = totalItemCount;
        }
        public ResponseCluster(T objValue, int totalItemCount) : this()
        {
            this.objValue = objValue;
            this.totalItemCount = totalItemCount;
        }
        public ResponseCluster(T objValue, IEnumerable<Erro> erros) : this()
        {
            this.objValue = objValue;
            this.erros = erros;
        }
        public ResponseCluster(T objValue) : this()
        {
            this.objValue = objValue;
        }
    }
}