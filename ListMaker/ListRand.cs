using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ListMaker
{
    /*Изменил доступы к полям класса с public на private, так как считаю что при такой реализации нет нужды кому-то извне иметь доступ к полям напрямую*/
    class ListRand 
    {
        private ListNode Head; 
        private ListNode Tail;
        private int Count;

        public ListRand()
        {
            Head = null;
            Tail = Head;
            Count = 0;
        }

        public void Serialize(FileStream s)//Разделяем элементы списка при записи с помощью [@]
        {
            ListNode Temp = Head;
            using (s)
            {
                for (int k=0; k<Count; k++)
                {
                     byte[] array = new byte[Temp.Data.Length];
                     array = Encoding.Default.GetBytes(Temp.Data+"[@]");
                     s.Write(array, 0, array.Length);
                     Temp = Temp.Next;                   
                }
            }
        }

        public void Deserialize(FileStream s)//Разбиваем строку на элементы, с помощью поиска [@] 
        {
            Count = 0;
            Head = null;
            Tail = null;
            using (s)
            {
                byte[] array = new byte[s.Length];
                s.Read(array,0,array.Length);
                string temp_str = Encoding.Default.GetString(array);
                String[] nodes = temp_str.Split(new string[] { "[@]" }, StringSplitOptions.RemoveEmptyEntries);
                for (int k= 0; k < nodes.Length; k++)
                {
                    if (Count==0)
                    {
                        ListNode Temp = new ListNode(nodes[k], null, Tail, null);
                        Head = Temp;
                        Tail = Temp;
                        Head.Rand = Head;
                        Count++;
                    }
                    else
                    {
                        Add(nodes[k]);
                        Count++;
                    }
                    
                }
            }
        }
        public void PrintList() // Вывод листа в консоль, для проверки его состояния
        {
            ListNode Temp = Head;
            if (Count == 0)
            {
                Console.WriteLine("Список пуст!");
            }
            else
            {
               Console.WriteLine($"Всего: {Count}");
            }

            ListNode Random;
            for (int k = 0; k < Count; k++)
            {
                if (Temp != null)
                {
                    Random = Temp.Rand;
                    Console.WriteLine($"Элементы списка: номер = {k + 1} Элемент = {Temp.Data} Рандомный элемент {Random.Data}");
                    Temp = Temp.Next;
                }
            }
                        
             
        }

        public void AddFromFile(string path) //Загрузка списка из файла
        {
            string textFromFile = null;
            if (ReadFile(path) != null)
            {
                textFromFile = ReadFile(path);
                for (int k = 0; k < textFromFile.Length; k++)
                {
                    Add(Convert.ToString(textFromFile[k]));
                }
            }
        }

        private string ReadFile(string path)//Читаем файл
        {
            string textFromFile=null;
            try
            {
                using (FileStream file = new FileStream(path, FileMode.Open))
                {
                    byte[] array = new byte[file.Length];
                    file.Read(array, 0, array.Length);
                    textFromFile = Encoding.Default.GetString(array);
                }
            }
            catch
            {
                Console.WriteLine("Файл не существует!");
            }
            return textFromFile;
        }

        public void Add(string data)// Добавляем новый элемент в список
        {
            if (Count == 0)
            {
                ListNode Temp = new ListNode(data, null, Tail, null);
                Head = Temp;
                Tail = Temp;
                Head.Rand = Head;
                Count++;
            }
            else
            {
                ListNode Temp = new ListNode(data, Tail, null, Head);
                Temp.Prev = Tail;
                Tail.Next = Temp;
                Tail = Temp;
                Count++;
                Tail.Rand = SetRandom();
            }
        }

        private ListNode SetRandom() //Генерируем случайную ссылку на объект из списка
        {
            ListNode Temp = Head;
            if (Count > 1)
            {
                Random rand = new Random();
                int val = rand.Next(1, Count);

                for (int k = 1; k < val; k++)
                {
                    if (Temp.Next != null)
                    {
                        Temp = Temp.Next;
                    }
                    
                }
            }
            return Temp;
        }

        public void RemoveByNumber(int numb) //Удаляем элемент по номеру в списке
        {
            if(numb <= 0 || numb > Count)
            {
                Console.WriteLine("Вне диапазона списка");
            }
            else
            {
                ListNode Temp;
                if(numb == 1) //Если элемент вначале списка
                {
                    Temp = Head;
                    Head = Head.Next;
                    Head.Prev = null;
                }
                else if (numb == Count) //Если в конце списка
                {
                    Temp = Tail;
                    Tail = Tail.Prev; 
                    Tail.Next = null;
                }
                else  //Если не начало и не конец списка
                {
                    Temp = Head;
                    for(int k = 1; k < numb; k++)
                    {
                        Temp = Temp.Next;
                    }
                    ListNode Left = Temp.Prev;
                    ListNode Right = Temp.Next;
                    Left.Next = Right;
                    Right.Prev = Left;
                }
                Count = Count -1;
                ListNode CurrentNode = Head;
                for (int k = 0; k < Count; k++)
                {
                    if (CurrentNode.Rand == Temp)
                    {
                        CurrentNode.Rand = SetRandom();
                    }
                }
            }
        }
    }
}
