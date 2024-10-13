using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork13_3.Parts
{
    internal class Part2
    {

        private readonly string _poemPart = "\nА это пшеница,\r\nКоторая в темном чулане хранится,\nВ доме,\nКоторый построил Джек.";
        public ImmutableList<string> Poem { get; private set; }
        public Part2(ImmutableList<string> poem)
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
