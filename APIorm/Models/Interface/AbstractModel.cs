using System.Collections.Generic;

namespace APIorm.Models.Interface
{
    public struct ResponseCluster<T>
    {
        public T objValue;
        public IEnumerable<Erro> erros;

        public ResponseCluster(T objValue, IEnumerable<Erro> erros)
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