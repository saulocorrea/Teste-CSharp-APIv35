using Newtonsoft.Json;
using Questao2;
using System.Drawing;
using System.Net.Http.Json;
using static System.Formats.Asn1.AsnWriter;

public class Program
{
    private static readonly string URL_BASE = "https://jsonmock.hackerrank.com/api/football_matches";

    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014

        // Results changed in API
        // Team Paris Saint - Germain scored 62 goals in 2013
        // Team Chelsea scored 47 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int totalGols = 0;

        ResultadoDto? resultado = new();
        do
        {
            int page = resultado.Page + 1;

            resultado = ObterDadosDaApi(team, year, page);

            if (resultado == null)
            {
                break;
            }

            if (resultado.Data == null || resultado.Data.Count == 0)
            {
                break;
            }

            totalGols += resultado.Data.Sum(x => x.Team1goals);

        } while (resultado.Page <= resultado.TotalPages);

        return totalGols;
    }

    private static ResultadoDto? ObterDadosDaApi(string team, int year, int page)
    {
        ResultadoDto? resultado;
        HttpClient client = new();
        HttpResponseMessage httpResult = client.GetAsync($"{URL_BASE}?year={year}&team1={team}&page={page}").GetAwaiter().GetResult();
        string jsonString = httpResult.Content.ReadAsStringAsync().Result;

        resultado = JsonConvert.DeserializeObject<ResultadoDto>(jsonString);
        return resultado;
    }
}