﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entitati
{
    public class ListaGen<T>: IEnumerable<T>
    {
        private class Nod
        {
            public ListaGen<T>.Nod Next { get; set; }
            public T Data { get; set; }

            //Constructor clasa interna nod
            public Nod(T t) {
                Next = null;
                Data = t;
            }
        }

        public uint Count {  get; set; }
        private ListaGen<T>.Nod Inceput { get; set; }

        //Constructor
        public ListaGen()
        {
            Inceput = null;
            Count = 0;
        }

        public void Add(T t)
        {
            Nod n = new Nod(t);
            n.Next = Inceput;
            Inceput = n;
            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            //traversare elementele listei inlantuite
            Nod current = Inceput;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        //implementare non-generica IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
