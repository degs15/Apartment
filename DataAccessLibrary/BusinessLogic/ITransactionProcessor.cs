using System.Collections.Generic;
using DataAccessLibrary.Models;
using System;

namespace DataAccessLibrary.BusinessLogic
{
    public interface ITransactionProcessor
    {
        List<TransactionModel> getAllTransactions();
        void addTransactionForTenant(int tenantId, int recieptNum, string datePosted, string recievedBy, int paymentAmount, string billPeriod);
        void deleteTransactionForTenant(int TransactionId);
        List<TransactionModel> getAllTransactionForTenant(int tenantId);
        TransactionModel getSpecificTransactionForTenant (int transactionId);
        void updateTransactionForTenant(int transactionId, int tenantId, int recieptNum, string datePosted, string recievedBy, int paymentAmount, string billPeriod);
        TransactionModel getTransactionByReceiptNumber(int receiptNumber);
        TransactionModel getTransactionByReceiptNumber(int receiptNumber, int transactionId);
    }
}