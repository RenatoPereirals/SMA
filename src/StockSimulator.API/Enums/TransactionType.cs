namespace StockSimulator.API.Enums
{
    /// <summary>
    /// Representa os tipos de transação no sistema.
    /// </summary>
    [Flags]
    public enum TransactionType
    {
        /// <summary>
        /// Nenhuma transação especificada.
        /// </summary>
        None = 0,

        /// <summary>
        /// Compra de um ativo.
        /// </summary>
        Buy = 1,

        /// <summary>
        /// Venda de um ativo.
        /// </summary>
        Sell = 2,
    }
}
