namespace ITS.Prometeo.Infrastructure.Persistence;

using Dapper;
using ITS.Prometeo.ApplicationCore.Entities;
using ITS.Prometeo.ApplicationCore.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal class WeatherStationsSqlRepository : IWeatherStationsRepository
{
    private string _connectionString;

    public WeatherStationsSqlRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("sql");
    }


    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<WeatherStation?> GetByIdAsync(int id)
    {
        const string query = """
            SELECT 
                [id]           as Id,
                [name]         as Name,
                [altitude]     as Altitude,
                [longitude]   as Longitude,
                [latitude]     as Latitude,
                [station_type] as StationType
            FROM [Pascolat_WeatherStation] WHERE Id = @id
            """;

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<WeatherStation>(query,new {id});

    }

    public async Task<IEnumerable<WeatherStation>> GetListAsync()
    {
        const string query = """
            SELECT 
                [id]           as Id,
                [name]         as Name,
                [altitude]     as Altitude,
                [longitude]   as Longitude,
                [latitude]     as Latitude,
                [station_type] as StationType
            FROM [Pascolat_WeatherStation]
            """;

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<WeatherStation>(query);

    }

    public async Task InsertAsync(WeatherStation station)
    {
        const string query = """
                INSERT INTO [Pascolat_WeatherStation] (
                    [name],
                    [altitude],
                    [longitude],
                    [latitude],
                    [station_type])
                VALUES (
                    @Name,
                    @Altitude,
                    @Longitude,
                    @Latitude,
                    @StationType);
                """;

        using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(query, station);
    }

    public Task UpdateAsync(WeatherStation station)
    {
        throw new NotImplementedException();
    }
}
