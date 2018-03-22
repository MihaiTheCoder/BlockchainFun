using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib.MultipleTransactions
{
    public class TransactionPool
    {
        Queue<ITransaction> queue;
        public TransactionPool()
        {
            queue = new Queue<ITransaction>();
        }

        public void AddTransaction(ITransaction transaction)
        {
            queue.Enqueue(transaction);
        }

        public ITransaction GetTransaction()
        {
            return queue.Dequeue();
        }
    }

}
