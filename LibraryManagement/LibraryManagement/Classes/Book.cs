namespace LibraryManagement.Classes
{
    public class Book
    {
        public string name { get; }
        public string writer { get; }
        public string genre { get; }
        public int printingNumber { get; }
        public int count { get; set; }
      
        public Book(string name, string writer, string genre, int printingNumber, int count)
        {
            this.name = name;
            this.writer = writer;
            this.genre = genre;
            this.printingNumber = printingNumber;
            this.count = count;
        }
    }
}
