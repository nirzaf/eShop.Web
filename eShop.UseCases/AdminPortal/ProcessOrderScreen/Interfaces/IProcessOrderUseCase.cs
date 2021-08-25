namespace eShop.UseCases.ProcessOrderScreen
{
    public interface IProcessOrderUseCase
    {   
        bool Execute(int orderId, string adminUserName);
    }
}