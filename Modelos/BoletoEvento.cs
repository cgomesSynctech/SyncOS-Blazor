using Microsoft.AspNetCore.Mvc;
using System;

namespace Modelos
{
    public class BoletoEvento
    {
        public BoletoEvento(int id, int boletoId, DateTime data, string arquivo, int statusId, string tipoEvento)
        {
            Id = id;
            BoletoId = boletoId;
            Data = data;
            Arquivo = arquivo;
            StatusId = statusId;
            TipoEvento = tipoEvento;
        }

        public int Id { get; set; }
        public int BoletoId { get; set; }
        public DateTime Data { get; set; }
        public string Arquivo { get; set; }
        public int StatusId { get; set; }
        public string TipoEvento { get; set; }
    }
}