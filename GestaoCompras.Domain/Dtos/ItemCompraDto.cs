namespace GestaoCompras.Domain.Dtos
{
    public class ItemCompraDto
    {
        public long IdItemCompra { get; set; }
        public long IdCompra { get; set; }
        public long IdProduto { get; set; }
        public double ValorUnit { get; set; }
        public int Quantidade { get; set; }
        public double ValorTotal { get; set; }
        public ProdutoDto Produto { get; set; }
    }
}
