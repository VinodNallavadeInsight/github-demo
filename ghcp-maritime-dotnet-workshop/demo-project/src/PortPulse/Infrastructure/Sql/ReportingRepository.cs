using Microsoft.EntityFrameworkCore;
using PortPulse.Data;

namespace PortPulse.Infrastructure.Sql;

public interface IReportingRepository
{
    Task<List<(string ContainerNumber, int DwellDays)>> GetDwellTimePercentileAsync(string shippingLineCode, CancellationToken ct);
}

public class ReportingRepository : IReportingRepository
{
    private readonly string _conn;
    public ReportingRepository(IConfiguration config)
    {
        _conn = config.GetConnectionString("Default") ?? config["ConnectionStrings:Default"] ?? "";
    }

    public async Task<List<(string ContainerNumber, int DwellDays)>> GetDwellTimePercentileAsync(string shippingLineCode, CancellationToken ct)
    {
        var result = new List<(string ContainerNumber, int DwellDays)>();
        using var conn = new Microsoft.Data.SqlClient.SqlConnection(_conn);
        await conn.OpenAsync(ct);
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT c.ContainerNumber, DATEDIFF(day, c.GateInTime, ISNULL(c.GateOutTime, GETDATE())) as DwellDays FROM Containers c INNER JOIN ShippingLines s ON c.ShippingLineId = s.Id WHERE s.Code = @code";
        cmd.Parameters.AddWithValue("@code", shippingLineCode);
        using var reader = await cmd.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            result.Add((reader.GetString(0), reader.GetInt32(1)));
        }
        return result;
    }
}
