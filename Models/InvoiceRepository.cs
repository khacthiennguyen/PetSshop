using System.Data;
using Dapper;

namespace WebApp.Models;

public class InvoiceRepository : BaseRepository
{
    public InvoiceRepository(IDbConnection connection) : base(connection)
    {
    }

    // public IEnumerable<SalesByAge> GetSalesByAge()
    // {
    //     return connection.Query<SalesByAge>("GetSalesByAge", commandType: CommandType.StoredProcedure);
    // }

    // public IEnumerable<SalesAndExpenses> GetSalesAndExpenses()
    // {
    //     string sql = "select * from SalesAndExpenses";
    //     return connection.Query<SalesAndExpenses>(sql);

    // }

    public int Add(Invoice obj)
    {
        if (string.IsNullOrEmpty(obj.Status))
        {
            obj.Status = "Pending";
        }
        return connection.Execute("AddInvoice", new
        {
            obj.CartCode,           // Mã giỏ hàng
            obj.InvoiceId,          // ID hóa đơn
            obj.MemberId,           // ID thành viên (khách hàng)
            obj.InvoiceDate,        // Ngày hóa đơn
            obj.FullName,           // Tên người nhận
            obj.Email,              // Email người nhận
            obj.Phone,              // Số điện thoại người nhận
            obj.Address,// Địa chỉ người nhận
            obj.Status,
        }, commandType: CommandType.StoredProcedure);
    }



    public decimal GetAmountInvoice(long id)
    {
        return connection.ExecuteScalar<decimal>("GetAmountInvoice", new { InvoiceId = id }, commandType: CommandType.StoredProcedure);

    }


    public IEnumerable<Invoice> GetOrdersByMemberId(string memberId)
    {
        string query = @"
        SELECT i.InvoiceId, i.InvoiceDate, i.Fullname, i.Email, i.Phone, i.Address, i.Status, i.Amount
        FROM Invoice i
        WHERE i.MemberId = @MemberId
        ORDER BY i.InvoiceDate DESC;
    ";

        return connection.Query<Invoice>(query, new { MemberId = memberId });
    }






}