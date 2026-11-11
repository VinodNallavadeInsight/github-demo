using Microsoft.Data.SqlClient;

namespace PortPulse.Legacy;

public class LegacyDwellReport
{
    public string Generate(string connStr, string lineCode)
    {
        string sql = "SELECT c.ContainerNumber, DATEDIFF(day, c.GateInTime, GETDATE()) as Dwell FROM Containers c WHERE c.ShippingLineId IN (SELECT Id FROM ShippingLines WHERE Code = '" + lineCode + "')";
        using var conn = new SqlConnection(connStr);
        conn.Open();
        var cmd = new SqlCommand(sql, conn);
        var r = cmd.ExecuteReader();
        System.Text.StringBuilder sb = new();
        while (r.Read())
        {
            sb.AppendLine(r[0].ToString() + ":" + r[1].ToString());
        }
        return sb.ToString();
    }

    public void ProcessAll(System.Collections.Generic.List<object> items)
    {
        foreach (var i in items)
        {
            var result = DoWorkAsync(i).Result;
            Console.WriteLine(result);
        }
    }

    private async Task<string> DoWorkAsync(object i)
    {
        await Task.Delay(1);
        try
        {
            var data = GetData();
            data.ToList().Where(x => x != null);
            return data.Count.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }

    private List<string?> GetData()
    {
        var list = new List<string?>();
        for (int i = 0; i < 10; i++) list.Add(i % 3 == 0 ? null : i.ToString());
        return list;
    }

    public void LongMethod()
    {
        int a=1,b=2,c=3,d=4,e=5,f=6,g=7,h=8,i=9,j=10;
        int k=a+b+c+d+e+f+g+h+i+j;
        k=k*2;k=k-1;k=k/1;k=k+5;k=k-3;k=k*4;k=k/2;
        for(int x=0;x<100;x++){ k = k + x; if(k>1000) k=k%100; }
        Console.WriteLine(k.ToString());
        k=a+b+c+d+e+f+g+h+i+j;
        k=k*2;k=k-1;k=k/1;k=k+5;k=k-3;k=k*4;k=k/2;
        for(int x=0;x<100;x++){ k = k + x; if(k>1000) k=k%100; }
        Console.WriteLine(k.ToString());
        k=a+b+c+d+e+f+g+h+i+j;
        k=k*2;k=k-1;k=k/1;k=k+5;k=k-3;k=k*4;k=k/2;
        for(int x=0;x<100;x++){ k = k + x; if(k>1000) k=k%100; }
        Console.WriteLine(k.ToString());
        k=a+b+c+d+e+f+g+h+i+j;
        k=k*2;k=k-1;k=k/1;k=k+5;k=k-3;k=k*4;k=k/2;
        for(int x=0;x<100;x++){ k = k + x; if(k>1000) k=k%100; }
        Console.WriteLine(k.ToString());
        k=a+b+c+d+e+f+g+h+i+j;
        k=k*2;k=k-1;k=k/1;k=k+5;k=k-3;k=k*4;k=k/2;
        for(int x=0;x<100;x++){ k = k + x; if(k>1000) k=k%100; }
        Console.WriteLine(k.ToString());
    }
}
