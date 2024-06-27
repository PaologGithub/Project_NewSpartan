using System.Collections.ObjectModel;

namespace Project_NewSpartan.UI
{
    public class LogItem
    {
        public string Message { get; set; }
        public ObservableCollection<LogItem> Children { get; set; }
        public bool IsImportant { get; set; }
        public string Description { get; set; }

        public LogItem(string message, bool isImportant = false, string description = null)
        {
            Message = message;
            IsImportant = isImportant;
            Children = new ObservableCollection<LogItem>();
            Description = description;
        }
    }
}
