using System;
using System.Collections.Generic;
using System.Text;

namespace TCP_Server
{
    using System;

    namespace SimpleRestService
    {
        public class Book
        {
            //Instance Field
            private string _title;
            private string _author;
            private int _pageNo;
            private string _isbn13;

            //Constructor
            public Book(string title, string author, int pageNo, string isbn13)
            {
                Title = title;
                Author = author;
                PageNo = pageNo;
                Isbn13 = isbn13;
            }

            public Book()
            {

            }

            //Properties
            public string Title
            {
                get => _title;
                set
                {
                    if (value == null) throw new NullReferenceException("Bogen skal have en titel");
                    _title = value;
                }
            }

            public string Author
            {
                get => _author;
                set
                {
                    if (value == null) throw new NullReferenceException("Skriv et navn");
                    if (value.Length < 2) throw new ArgumentException("Forfatter navn skal være minimum 2 tegn langt");
                    _author = value;
                }

            }

            public int PageNo
            {
                get => _pageNo;
                set
                {
                    if (value <= 4) throw new ArgumentOutOfRangeException("Bogen er for kort");
                    if (value >= 1000) throw new ArgumentOutOfRangeException("Bogen er for lang");
                    _pageNo = value;
                }
            }

            public string Isbn13
            {
                get => _isbn13;

                set

                {
                    _isbn13 = value;
                    if (value.Length == 13) _isbn13 = value;
                    else if (value.Length != 13) throw new ArgumentException("ISBN skal være 13 cifre");

                }
            }
        }
    }
}



