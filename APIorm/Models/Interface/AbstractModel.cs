using System.Collections.Generic;
using X.PagedList;

namespace APIorm.Models.Interface
{
    public struct ResponseCluster<T>
    {
        public T objValue;
        public IEnumerable<Erro> erros;
        public int totalItemCount;
        public PagedListMetaData metaData;

        public ResponseCluster(T objValue, IEnumerable<Erro> erros, int totalItemCount, PagedListMetaData metaData)
        {
            this.objValue = objValue;
            this.erros = erros;
            this.totalItemCount = totalItemCount;
            this.metaData = metaData;
        }
        public ResponseCluster(T objValue, int totalItemCount, PagedListMetaData metaData) : this()
        {
            this.objValue = objValue;
            this.totalItemCount = totalItemCount;
            this.metaData = metaData;
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