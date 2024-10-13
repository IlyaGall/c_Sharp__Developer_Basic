using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork13_3.Parts
{
    public class Part7
    {
        private readonly string _poemPart = "\nА это старушка, седая и строгая,\r\nКоторая доит корову безрогую,\r\nЛягнувшую старого пса без хвоста,\r\nКоторый за шиворот треплет кота,\r\nКоторый пугает и ловит синицу,\r\nКоторая часто ворует пшеницу,\r\nКоторая в темном чулане хранится\r\nВ доме,\r\nКоторый построил Джек.";
        public ImmutableList<string> Poem { get; private set; }
        public Part7(ImmutableList<string> poem)
        {
            Poem = AddPart(poem);

        }
        private ImmutableList<string> AddPart(ImmutableList<string> prevPoem)
        {
            Poem = prevPoem.Add(_poemPart);
            return Poem;
        }
    }
}