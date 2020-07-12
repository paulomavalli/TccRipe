using RIPE.Domain.Domains;

namespace RIPE.Application.Responses
{
    public class ReportResponse
    {
        public string NivelMaturidade { get; set; }
        public string PorcentagemRespostasPositivas { get; set; }
        public string PorcentagemRespostasNegativas { get; set; }
        public string PorcentagemRespostasNulas { get; set; }
        public BestHabits Recomendacoes { get; set; }
    }
}
