using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AvaloniaTest
{
    public class ArticleViewModel : INotifyPropertyChanged
    {
        private string statusMessage_;
        private string[] docItems_;
        private string searchQuery_;

        private string docId_;
        private string docTitle_;
        private string docAuthors_;
        private string docJournal_;
        private string docPublicationDate_;
        private string docAbstract_;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ArticleViewModel()
        {
            statusMessage_ = "";
            searchQuery_ = "";
            docId_ = "N/A";
            docTitle_ = "N/A";
            docAuthors_ = "N/A";
            docJournal_ = "N/A";
            docPublicationDate_ = "N/A";
            docAbstract_ = "N/A";
            docItems_ = Array.Empty<string>();
        }

        public string StatusMessage
        {
            get => statusMessage_;
            set
            {
                if (value != statusMessage_)
                {
                    statusMessage_ = value;

                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }

        public string SearchQuery
        {
            get => searchQuery_;
            set
            {
                if (value != searchQuery_)
                {
                    searchQuery_ = value;
                    OnPropertyChanged(nameof(SearchQuery));
                }
            }
        }

        public string[] DocItems
        {
            get => docItems_;
            set
            {
                docItems_ = value;
                OnPropertyChanged(nameof(DocItems));
            }
        }

        public string DocId
        {
            get => docId_;
            set
            {
                if (value != docId_)
                {
                    docId_ = value;
                    OnPropertyChanged(nameof(DocId));
                }
            }
        }

        public string DocTitle
        {
            get => docTitle_;
            set
            {
                if (value != docTitle_)
                {
                    docTitle_ = value;
                    OnPropertyChanged(nameof(DocTitle));
                }
            }
        }

        public string DocAuthors
        {
            get => docAuthors_;
            set
            {
                if (value != docAuthors_)
                {
                    docAuthors_ = value;
                    OnPropertyChanged(nameof(DocAuthors));
                }
            }
        }

        public string DocJournal
        {
            get => docJournal_;
            set
            {
                if (value != docJournal_)
                {
                    docJournal_ = value;
                    OnPropertyChanged(nameof(DocJournal));
                }
            }
        }

        public string DocPublicationDate
        {
            get => docPublicationDate_;
            set
            {
                if (value != docPublicationDate_)
                {
                    docPublicationDate_ = value;
                    OnPropertyChanged(nameof(DocPublicationDate));
                }
            }
        }

        public string DocAbstract
        {
            get => docAbstract_;
            set
            {
                if (value != docAbstract_)
                {
                    docAbstract_ = value;
                    OnPropertyChanged(nameof(DocAbstract));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
