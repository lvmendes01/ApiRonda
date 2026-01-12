namespace RondaSegurancaBack.DTO
{
    public class LocalizacaoDto
    {
        
        public string DeviceId { get; set; } = null!;
        public long RondaId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? PrecisaoMetros { get; set; }
        public DateTime DataHoraCapturaUtc { get; set; }
    }

}
