using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6_OOP
{
    public class Storage
    {
        private int n, k; // размер и кол-во эл-в
        public СObject[] st;
        public Storage()
        {
            n = 1;
            st = new СObject[n];
            st[0] = null; // или default
            k = 0;
        }
        public Storage(int size)
        {
            n = size;
            st = new СObject[n];
            k = 0;
            for (int i = 0; i < n; ++i)
                st[i] = null;
        }
        public void add(СObject new_el)
        {
            if (k < n)
            {
                st[k] = new_el;
                k = k + 1;
            }
            else
            {
                n = n * 2;
                СObject[] st_ = new СObject[n];
                for (int i = 0; i < k; ++i)
                    st_[i] = st[i];
                st_[k] = new_el;
                k = k + 1;
                for (int i = k; i < n; ++i)
                    st_[i] = null;
                st = st_;
            }
        }
        public void del(int ind)
        {
            for (int i = ind; i < k - 1; ++i)
                st[i] = st[i + 1];
            k = k - 1;
            st[k] = null;
        }
        public СObject get_el(int ind)
        {
            return st[ind];
        }
        public int get_count()
        {
            return k;
        }
    };
}
