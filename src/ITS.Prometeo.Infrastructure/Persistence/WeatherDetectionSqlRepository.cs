namespace ITS.Prometeo.Infrastructure.Persistence;

using Dapper;
using ITS.Prometeo.ApplicationCore.Entities;
using ITS.Prometeo.ApplicationCore.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal class WeatherDetectionSqlRepository : IWeatherDetectionRepository
{
    private readonly string _connectionString;

    public WeatherDetectionSqlRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("sql");
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<WeatherDetection?> GetByIdAsync(long id)
    {
        const string query = """
            SELECT 
                [id],
                [weather_station_id],
                [detection_type],
                [value],
                [date]
            FROM [Pascolat_WeatherDetection] WHERE [id] = @id;
            """;
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<WeatherDetection>(query, new {id});
    }

    public async Task<IEnumerable<WeatherDetection>> GetListAsync()
    {
        const string query = """
            SELECT 
                [id],
                [weather_station_id],
                [detection_type],
                [value],
                [date]
            FROM [Pascolat_WeatherDetection]
            """;
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<WeatherDetection>(query); 

    }

    public async Task InsertAsync(WeatherDetection detection)
    {
        const string query = """
            INSERT INTO [Pascolat_WeatherDetection] (
                 [weather_station_id],
                 [detection_type],
                 [value],
                 [date])
            VALUES
            (
                @WeatherStationId,
                @Type,
                @Value,
                @Date);
            """;
        using var connection = new SqlConnection( _connectionString );
        await connection.ExecuteAsync(query, detection);
    }
}
