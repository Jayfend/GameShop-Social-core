using System.Transactions;

namespace FRT.MasterDataCore.Customs
{
    public interface ITransactionCustom
    {
        TransactionScope CreateTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);
    }


    public class TransactionCustom : ITransactionCustom
    {
        public TransactionScope CreateTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            return new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions()
                {
                    IsolationLevel = isolationLevel

                },
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
