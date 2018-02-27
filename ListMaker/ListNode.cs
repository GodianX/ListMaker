using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListMaker
{
    class ListNode
    {
        public ListNode Prev { get; set; }
        public ListNode Next { get; set; }
        public ListNode Rand { get; set; }
        public string Data { get; set; }

        public ListNode(string data, ListNode prev, ListNode next, ListNode rand)
        {
            Data = data;
            Prev = prev;
            Next = next;
            Rand = rand;
        }
    }
}
