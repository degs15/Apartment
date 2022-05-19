using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.DataAccess;


namespace DataAccessLibrary.BusinessLogic
{
    public class TransactionProcessor : ITransactionProcessor
    {
        ISqlDataAccess _isqlDataAccess;

        public TransactionProcessor(ISqlDataAccess isqlDataAccess)
        {
            _isqlDataAccess = isqlDataAccess;
        }

        public List<TransactionModel> getAllTransactions()
        {
            var sql = @"Select * from [dbo].[Transaction]";

            var output = _isqlDataAccess.loadData<TransactionModel>(sql);

            return output;
        }

        public List<TransactionModel> getAllTransactionForTenant(int tenantId)
        {
            var sql = @"Select * from [dbo].[Transaction] where TenantId=" + tenantId;

            var output = _isqlDataAccess.loadData<TransactionModel>(sql);

            return output;
        }


        public TransactionModel getSpecificTransactionForTenant(int transactionId)
        {
            var sql = @"Select * from [dbo].[Transaction] where TransactionId=" + transactionId;

            var output = _isqlDataAccess.loadData<TransactionModel>(sql).FirstOrDefault();

            return output;
        }

        public void addTransactionForTenant(int tenantId, int recieptNum, string datePosted, string recievedBy, int paymentAmount, string billPeriod)
        {
            TransactionModel data = new TransactionModel();
            data.TenantId = tenantId;
            data.ReceiptNumber = recieptNum;
            data.DatePosted = datePosted;
            data.ReceivedBy = recievedBy;
            data.PaymentAmount = paymentAmount;
            data.BillPeriod = billPeriod;

            var sql = @"Insert into [dbo].[Transaction] (TenantId,ReceiptNumber,DatePosted,ReceivedBy,PaymentAmount,BillPeriod) values (@TenantId,@ReceiptNumber,@DatePosted,@ReceivedBy,@PaymentAmount,@BillPeriod)";

            _isqlDataAccess.SaveData(data, sql);
        }

        public void updateTransactionForTenant(int transactionId, int tenantId, int recieptNum, string datePosted, string recievedBy, int paymentAmount, string billPeriod)
        {
            TransactionModel data = new TransactionModel();
            data.TransactionId = transactionId;
            data.TenantId = tenantId;
            data.ReceiptNumber = recieptNum;
            data.DatePosted = datePosted;
            data.ReceivedBy = recievedBy;
            data.PaymentAmount = paymentAmount;
            data.BillPeriod = billPeriod;

            var sql = @"Update [dbo].[Transaction] set TenantId = @TenantId ,ReceiptNumber = @ReceiptNumber,DatePosted = @DatePosted ,ReceivedBy = @ReceivedBy,PaymentAmount = @PaymentAmount, BillPeriod = @BillPeriod
                        where TransactionId=" + data.TransactionId;

            _isqlDataAccess.SaveData(data, sql);
        }

        public void deleteTransactionForTenant(int TransactionId)
        {
            string sql = @"Delete from [dbo].[Transaction] where TransactionId=" + TransactionId;

            _isqlDataAccess.DeleteData(sql);

        }

        public TransactionModel getTransactionByReceiptNumber(int receiptNumber)
        {
            var sql = @"Select * from [dbo].[Transaction] where ReceiptNumber=" + receiptNumber;

            var output = _isqlDataAccess.loadData<TransactionModel>(sql).FirstOrDefault();

            return output;
        }

        public TransactionModel getTransactionByReceiptNumber(int receiptNumber, int transactionId)
        {
            var sql = @"Select * from [dbo].[Transaction] where ReceiptNumber=" + receiptNumber + " and TransactionId !="+transactionId;

            var output = _isqlDataAccess.loadData<TransactionModel>(sql).FirstOrDefault();

            return output;
        }



    }
}
