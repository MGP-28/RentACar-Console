using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar
{
    class Cliente
    {
        private string _nome;
        private int _id;
        public string Nome { get => _nome; set => _nome = value; }
        public int Id { get => _id; set => _id = value; }
        private Cliente()
        {
        }
        public Cliente(string nome, int id)
        {
            Nome = nome;
            Id = id;
        }
        public override string ToString()
        {
            return $"{Id.ToString().PadLeft(4)} | {Nome}";
        }
        public string ToStringToFile()
        {
            return $"{Id}#{Nome}";
        }
    }
}
