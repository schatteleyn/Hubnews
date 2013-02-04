using System;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace hubnews
{
    public partial class NewsDetails : PhoneApplicationPage
    {
        private String url;
        private String title;
        private String desc;

        public NewsDetails()
        {
            InitializeComponent();
        }

        //Get values from URL
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("title")
                & NavigationContext.QueryString.ContainsKey("publishDate")
                & NavigationContext.QueryString.ContainsKey("desc"))
            {
                textBlock_Title.Text = NavigationContext.QueryString["title"];
                textBlock_PublishDate.Text = NavigationContext.QueryString["publishDate"];
                textBlock_Desc.Text = NavigationContext.QueryString["desc"];

                if (NavigationContext.QueryString["url"] != null)
                {
                    url = NavigationContext.QueryString["url"];
                }
                title = NavigationContext.QueryString["title"];
                desc = NavigationContext.QueryString["desc"];
            }
            base.OnNavigatedTo(e);
        }

        private void ApplicationBarMailButton_Click(object sender, EventArgs e)
        {
            EmailComposeTask emailcomposer = new EmailComposeTask();
            emailcomposer.Subject = title;
            emailcomposer.Body = desc;
            emailcomposer.Show();   
        }

        private void ApplicationBarBrowserButton_Click_1(object sender, EventArgs e)
        {
            Uri uri = new Uri(url);

            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = uri;
            webBrowserTask.Show();
        }
    }
}