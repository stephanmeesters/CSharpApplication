using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

using System;

namespace AvaloniaTest
{
    public partial class MainWindow : Window
    {
        PLOSOne.DocController docController;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ArticleViewModel() {
                StatusMessage = "...",
                DocItems = new string[] { "Loading items..." },
                SearchQuery = "DNA", // default query
                ShowPageOne = true,
                ShowPageTwo = false
            };

            this.docController = new PLOSOne.DocController();

#if DEBUG
            this.AttachDevTools();
#endif

            // Start loading the default query
            LoadContents();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            // prevent window resizing
            this.CanResize = false;

            Image img = new Image();
            
        }

        public async void LoadContents()
        {
            var context = this.DataContext as ArticleViewModel;
            if (context == null)
                return;

            var loadTask = docController.LoadData(context.SearchQuery);
            try
            {
                await loadTask;
            }
            catch(Exception ex)
            {
                context.StatusMessage = ex.Message;
                return;
            }
            context.ClearDoc();
            context.DocItems = docController.listAllDocNames().ToArray();

            long numResults = docController.numberOfResultsFound();
            context.StatusMessage = $"Number of results found: {numResults}";
            if (numResults > 100)
                context.StatusMessage += " (showing 100)";

            // Select the first item
            var control = this.FindControl<ListBox>("itemListBox");
            if (control != null && context.DocItems.Length > 0)
                control.Selection.Select(0);
        }

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            var context = this.DataContext as ArticleViewModel;
            if (context == null || !docController.IsDataAvailable())
                return;
            foreach (string color in args.AddedItems)
            {
                PLOSOne.Doc? doc = docController.findDoc(color);
                if(doc != null)
                {
                    context.DocId = doc.id;
                    context.DocTitle = doc.title_display;
                    if (doc.author_display != null)
                        context.DocAuthors = String.Join(", ", doc.author_display);
                    else
                        context.DocAuthors = "N/A";
                    context.DocJournal = doc.journal;
                    context.DocPublicationDate = doc.publication_date_formatted;
                    if (doc.@abstract != null)
                        context.DocAbstract = String.Join("", doc.@abstract).Trim();
                    else
                        context.DocAbstract = "N/A";
                }

                return;
            }
        }

        public void OnSearchClicked(object sender, RoutedEventArgs args)
        {
            var context = this.DataContext as ArticleViewModel;
            if (context == null)
                return;

            if(context.SearchQuery.Length > 0)
                LoadContents();
            else
                context.StatusMessage = @"Query must not be empty.";
        }

        public void OnSwitchPage(object sender, RoutedEventArgs args)
        {
            var context = this.DataContext as ArticleViewModel;
            if (context == null)
                return;

            var button = args.Source as Button;
            if(button != null)
            {
                if(button.Name == "Page1")
                {
                    context.ShowPageOne = true;
                    context.ShowPageTwo = false;
                }
                else if(button.Name == "Page2")
                {
                    context.ShowPageOne = false;
                    context.ShowPageTwo = true;
                }
            }
        }
    }
}
